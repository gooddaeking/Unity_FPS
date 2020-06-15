﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : MonoBehaviour
{
    //카메라를 마우스 움직이는 방향으로 회전
    public float speed = 150.0f;    //회전속도(Time.DeltaTime)
    //회전각도를 직접 제어하자
    float angleX, angleY;
  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");
        //Vector3 dir = new Vector3(h, v, 0);
        //회전은 각각의 축을 기반
        Vector3 dir = new Vector3(-v, h, 0);
        //transform.Rotate(dir * speed * Time.deltaTime);

        //유니티 엔진에서 제공해주는 함수를 사용함에 있어
        //Tranlate함수는 그래도 사용하는데 큰 불편함이 없는데
        //회전을 담당하는 Rotate함수를 사용하면
        //우리가 제어하기 힘들다
        //인스펙터창의 로테이션값은 우리가 보기편하게 오일러각도로 표시
        //내부적으로는 쿼터니온으로 회전처리가 되고있다
        //쿼터니온을 사용하는 이유는 짐벌락현상을 방지할 수 있기 때문에
        //회전을 직접 제어할때는 Rotate함수는 사용하지 않고
        //트렌스폼의 오일러앵글을 사용하면 된다

        //P = P0 + vt;
        //transform.position += dir * speed * Time.deltaTime;
        //각도또한 똑같다
        //transform.eulerAngles += dir * speed * Time.deltaTime;
        //하지만 이 또한 문제가있다 (-90 ~ 90 도에 고정됐다 풀렸다 한다)
        //직접 회전각도를 제한해서 처리해야한다.
        //Vector3 angle = transform.eulerAngles;
        //angle += dir * speed * Time.deltaTime;
        //if (angle.x > 60) angle.x = 60;
        //if (angle.x < -60) angle.x = -60;
        //transform.eulerAngles = angle;

        //여기에는 또 문제가 있다
        //유니티 내부적으로 =각도는 360도를 더해서 처리된다
        //내가 만든 각도를 가지고 계산처리해야한다
        angleX += h * speed * Time.deltaTime;
        angleY += v * speed * Time.deltaTime;
        angleY = Mathf.Clamp(angleY, -60, 60);
        transform.eulerAngles = new Vector3(-angleY, angleX, 0);
    }
}