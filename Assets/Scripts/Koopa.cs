using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Koopa : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rg2d;
    public bool isStomp = false;
    int speed = 1;
    int shellSpeed = 5;
    public Sprite Dead;
    SpriteRenderer spriteRenderer;
    private bool facingLeft = false;
    void Start()
    {
        rg2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        rg2d.velocity = new Vector2(speed, rg2d.velocity.y);
        if(isStomp == true)                                                  
            rg2d.velocity = new Vector2(shellSpeed, rg2d.velocity.y);       // Shell gains velocity like in the original game
    }

    public void DeadEnemy()                                                 //Turn into shell after being hit
    {
        GetComponent<Animator>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = Dead;
        rg2d.velocity = new Vector2(0, 0);
        Player.count++;
    }

    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.tag == "Boundary" && isStomp == false)
        {
            speed = speed * -1;                                         
            if (!facingLeft)                                            //Change sprite according to direction faced
            {
                spriteRenderer.flipX = true;
                facingLeft = true;
            }
            else
            {
                spriteRenderer.flipX = false;
                facingLeft = false;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            Player.count++;
        }

        if (collision.gameObject.tag == "Wall")
            shellSpeed = shellSpeed * -1;
        if (collision.gameObject.tag == "Enemy")              //Shell destroys enemies on being hit like the original
            Destroy(collision.gameObject);
    }
}
