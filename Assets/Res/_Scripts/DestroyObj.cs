using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObj : MonoBehaviour
{
    public bool isPlayer = false;
    bool doble = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && !doble)
        {
            doble= true;
            isPlayer = true;
        }
    }
    private void Update()
    {
        if(isPlayer)
        {
            Destroy(gameObject, 3f);
        }
    }
}
