using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rg2d;
    public Boolean isStomp = false;
    int speed = 1;
    void Start()
    {
        rg2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rg2d.velocity = new Vector2(speed, rg2d.velocity.y);
        if (isStomp == true)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if(collision.gameObject.tag == "GameController")
        {
            speed = speed * -1;
        }
    }

}
