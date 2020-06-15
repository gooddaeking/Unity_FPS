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
        
        cc.Move(dir * moveSpeed * Time.deltaTime);
    }
}
