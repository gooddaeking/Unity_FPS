using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    //플레이어에 카메라를 붙여서 이동해도 되지만
    //드라마틱한 연출이 필요한 경우에 타겟을 따라다니도록 하는게 쉽지않다
    //순간이동이 아닌 꼬랑지가 따라다니는 느낌으로 하는 효과도 연출할 수 있다
    //카메라가 따라다닐 오브젝트
    public Transform target;    //플레이어를 따라다닐거다
    public Transform third;
    public float followSpeed = 10.0f;
    bool fps = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //카메라 위치를 강제로 타겟위치에 고정해둔다
        //transform.position = target.position;

        FollowTarget();
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            fps = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            fps = false;
        }
    }

    private void FollowTarget()
    {
        if (fps)
        {
            //타겟 방향 구하기 (백터의 뺄셈)
            //방향 = 타겟 - 자기자신
            Vector3 dir = target.position - transform.position;
            //dir.Normalize(); // 노멀라이즈해주지 않으면 순간이동
            transform.Translate(dir * followSpeed * Time.deltaTime);

            //문제점 : 덜덜 떨림
            if (Vector3.Distance(transform.position, target.position) < 2.0f)
            {
                transform.position = target.position;
            }
        }
        else
        {
            //타겟 방향 구하기 (백터의 뺄셈)
            //방향 = 타겟 - 자기자신
            Vector3 dir = third.position - transform.position;
            //dir.Normalize(); // 노멀라이즈해주지 않으면 순간이동
            transform.Translate(dir * followSpeed * Time.deltaTime);

            //문제점 : 덜덜 떨림
            if (Vector3.Distance(transform.position, third.position) < 2.0f)
            {
                transform.position = third.position;
            }
        }
    }
}
