using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class Goomba : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rg2d;
    public Boolean isStomp = false;
    int speed = 1;
    public Sprite Dead;
    void Start()
    {
        rg2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rg2d.velocity = new Vector2(speed, rg2d.velocity.y);
        /*if (isStomp == true)
        {
            Player.count++; 
            Destroy(gameObject);
        }*/
    }

    public void DeadEnemy()
    {
        GetComponent<Animator>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = Dead;
        rg2d.velocity = new Vector2(0, 0);
        Invoke("DestroySelf", 0.2f);
    }

    private void DestroySelf()
    {
        Player.count++;
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if(collision.gameObject.tag == "Boundary")
        {
            speed = speed * -1;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
            Player.count++;
        }
    }

}
