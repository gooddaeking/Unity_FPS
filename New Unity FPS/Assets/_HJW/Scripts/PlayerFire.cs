using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public Vector3 firePosition;
    public GameObject bombPrefab;

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

        }
        if(Input.GetMouseButtonDown(1))
        {

        }
    }
}
