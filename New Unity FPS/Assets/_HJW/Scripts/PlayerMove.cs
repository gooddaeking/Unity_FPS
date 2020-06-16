using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    private float h = 0.0f;
    private float v = 0.0f;

    public float moveSpeed = 10.0f;
    CharacterController cc;

    //중력적용
    public float gravity = -5.0f;
    float velocityY;        //낙하속도(벨로시티는 방향과 힘을 들고있다)
    float jumpPower = 7.0f;
    bool[] jump = { false, false };

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        
    }

    private void Move()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        //Vector3 Dir = (Vector3.forward * v) + (Vector3.right * h);
        Vector3 dir = new Vector3(h, 0, v);
        dir.Normalize();    //대각선 이동 속도를 상화좌우 속도와 동일하게 만들기
        //게임에 따라 변동될 수 있음
        //transform.Translate(dir.normalized * moveSpeed * Time.deltaTime, Space.Self);

        dir = Camera.main.transform.TransformDirection(dir);
        //transform.Translate(dir * moveSpeed * Time.deltaTime);
        //심각한 문제 : 하늘 날아다님, 땅 뚫음, 충돌처리 안됨
        //cc.Move(dir * moveSpeed * Time.deltaTime);

        //중력 적용
        velocityY += gravity * Time.deltaTime;
        dir.y = velocityY;
        cc.Move(dir * moveSpeed * Time.deltaTime);

        //캐릭터 점프
        //점프버튼을 누르면 수직속도에 점프파워를 넣는다
        //땅에 닿으면 0 으로 초기화
        if(cc.isGrounded)   //땅에 닿았나?
        {

        }
        if(cc.collisionFlags == CollisionFlags.Below)
        {
            velocityY = 0;
            jump[0] = jump[1] = false;
        }
        if(Input.GetButtonDown("Jump") && !jump[0] && !jump[1])
        {
            velocityY = jumpPower;
            jump[0] = true;
        }
        else if(Input.GetButtonDown("Jump") && jump[0] && !jump[1])
        {
            velocityY = jumpPower;
            jump[1] = true;
        }
        //CollisionFlags.Above;
        //CollisionFlags.Below;
        //CollisionFlags.Sides;
    }
}
