using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 5.0f;
    public float gravity = -20;
    float velocityY = 0;
    CharacterController cc;
    void Start()
    {
        //캐릭터 컨트롤러 컴포넌트 가져오기
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(hor, 0, ver);
        //this.transform.Translate(dir * speed * Time.deltaTime);

        //카메라가 보는 방향으로 이동해야 한다.
        dir = Camera.main.transform.TransformDirection(dir);
        //transform.Translate(dir * speed * Time.deltaTime);
        dir.y = velocityY;
        if(cc.collisionFlags == CollisionFlags.Below)
        {
            velocityY = 0;
        }
        else
        {
            velocityY -= gravity * Time.deltaTime;
        }
        cc.Move(dir * speed * Time.deltaTime);
        
    }
}
