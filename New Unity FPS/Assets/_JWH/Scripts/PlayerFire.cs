using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject bulletImpactPrefab;   //총알임팩트 프리팹
    public GameObject grenadePrefab;
    public GameObject throwPoint;
    public float throwPower = 100.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    private void Fire()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray(Camera.main.transform.position,Camera.main.transform.forward);
            RaycastHit hitInfo;
            //레이랑 충돌했냐?
            if(Physics.Raycast(ray, out hitInfo))
            {
                print("충돌오브젝트 : " + hitInfo.collider.name);

                

                //충돌 지점에 총알 이펙트 생성한다.
                //총알 파편 이펙트 생성
                GameObject bulletImpact = Instantiate(bulletImpactPrefab);
                bulletImpact.transform.position = hitInfo.point;
                //파편이펙트
                //파편이 부딪힌 지점이 향하는 방향으로 튀게 해줘야 한다.
                bulletImpact.transform.forward = hitInfo.normal;

                //내총알에 충돌했으니 몬스터 체력을 깎기
                EnemyFSM enemy = hitInfo.collider.GetComponent<EnemyFSM>();
                enemy.hitDamage(10);
                //hitInfo.collider.gameObject.GetComponent<EnemyFSM>().hitDamage(10);
                //hitInfo.transform.GetComponent<EnemyFSM>().hitDamage(10);
            }

            //레이어 마스크 사용 충돌처리(최적화)
            //유니티 내부적으로 속도 향상을 위해 비트연산 처리가 된다.
            //총 32비트를 사용하기 때문에 레이어도 32개까지 추가 가능함
            int layer = gameObject.layer;
            layer = 1 << 8;
            //0000 0000 0000 0001 = > 0000 0001 0000 0000
            layer = 1 << 8 | 1 << 9 | 1 << 12;
            //0000 0001 0000 0000 => player
            //0000 0010 0000 0000 => enemy
            //0001 0000 0000 0000 => boss
            //*최적화 기법 : 오브젝트 풀링 , 로우 폴리 , 비트 연산 , 스타트 함수 안쓰면 지우기

            //0001 0011 0000 0001 => p,e,b 모두 충돌처리
            //if (Physics.Raycast(ray, out hitInfo,100,layer))
            //{
            //
            //}
            //if (Physics.Raycast(ray, out hitInfo, 100, ~layer)) layer만 충돌안함
           

        }
        if (Input.GetMouseButtonDown(1))
        {
            //폭탄 생성
            GameObject grenade = Instantiate(grenadePrefab);
            grenade.transform.position = throwPoint.transform.position;
            //폭탄은 플레이어가 던지기 때문에
            //폭탄의 리지드 바디를 이용해서 던지면 된다.
            Rigidbody rb = grenade.GetComponent<Rigidbody>();
            //전방으로 물리적인 힘을 가한다.
            //rb.AddForce(Camera.main.transform.forward * throwPower, ForceMode.Impulse);

            //ForceMode.Acceleration = > 연속적인 힘을 가한다 (질량 없음)
            //ForceMode.Force = > 연속적인 힘을 가한다 (질량 영향)
            //ForceMode.Impulse = > 순간적인 힘을 가한다 (질량 영향)
            //ForceMode.VelocityChange = > 순간적인 힘을 가한다 (질량 없음)

            //45도 각도 발사
            //각도 주려면?
            Vector3 dir = Camera.main.transform.forward + Camera.main.transform.up;
            dir.Normalize();
            rb.AddForce(dir * throwPower, ForceMode.Impulse);
        }
    }
}
