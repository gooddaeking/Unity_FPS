using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject fxFactory;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject fx = Instantiate(fxFactory);
        fx.transform.position = transform.position;
    }

}
