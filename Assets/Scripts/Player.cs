using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    Animator animator;
    private float speed = 4f;
    private float jumpspeed = 6f;
    private bool Isgrounded = true;
    private Rigidbody2D rigidBody2D;
    private SpriteRenderer spriteRenderer;
    public Text score;
    public static int count = 0;
    public Transform GroundCheck,GroundCheckL,GroundCheckR;
    Object bulletref;         //source.clip = clips[] source.play
    bool power = false;
    public AudioClip[] clips = new AudioClip[5];
    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        bulletref = Resources.Load("Prefabs/Bullet");
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        var h = Input.GetAxis("Horizontal");
        rigidBody2D.velocity = new Vector2(h * speed, rigidBody2D.velocity.y);
        if ((Physics2D.Linecast(transform.position, GroundCheck.position, 1 << LayerMask.NameToLayer("Ground")))||
            (Physics2D.Linecast(transform.position, GroundCheckR.position, 1 << LayerMask.NameToLayer("Ground")))||
            (Physics2D.Linecast(transform.position, GroundCheckL.position, 1 << LayerMask.NameToLayer("Ground"))))
        {
            Isgrounded = true;
        }
        else
        {
            Isgrounded = false;
            if (power == true)
                animator.Play("Fire_Jump");
            else
                animator.Play("Jump");
        }
 
        if (Input.GetKeyDown(KeyCode.Space) && Isgrounded == true)
        {
            rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, jumpspeed);
            if (power == true)
            {
                source.clip = clips[2];
                source.Play();
                animator.Play("Fire_Jump");
            }
            else
            {
                source.clip = clips[2];
                source.Play();
                animator.Play("Jump");
            }
        }
   
        if ((h < 0))
        {
            if (Isgrounded && power == true)
                animator.Play("Fire_Run");
            else if (Isgrounded)
                animator.Play("Run");
            spriteRenderer.flipX = true;
        }
        else if ((h > 0)) 
        {
            if (Isgrounded && power == true)
                animator.Play("Fire_Run");
            else if (Isgrounded)
                animator.Play("Run");
            spriteRenderer.flipX = false;
        }
       /* else if (Input.GetKeyDown("f") && power == true)
        {
            GameObject bullet = (GameObject)Instantiate(bulletref);
            bullet.transform.position = new Vector2(transform.position.x + 0.4f, transform.position.y + 0.4f);
        }*/
        else
        {
            if(Isgrounded && power == true)
            {
                if (Input.GetKeyDown("f"))
                {
                    animator.Play("Shoot");
                    GameObject bullet = (GameObject)Instantiate(bulletref);
                    bullet.transform.position = new Vector2(transform.position.x + 0.4f, transform.position.y + 0.4f);
                }
                else
                    animator.Play("Fire_Idle");
            }
            else if (Isgrounded)
                animator.Play("Idle");
            rigidBody2D.velocity = new Vector2(0, rigidBody2D.velocity.y);
        }

        score.text ="Score: " + count.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Trigger")
        {
            Destroy(gameObject);
            SceneManager.LoadScene("Game Over");
        }
    }

    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.tag == "GameController")
        {
            rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, 4f);
            source.clip = clips[4];
            source.Play();
        }
        if(collision.gameObject.tag == "Collectible")
        {
            source.clip = clips[0];
            source.Play();
            Player.count++;
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.tag == "PowerUp")
        {
            source.clip = clips[3];
            source.Play();
            Destroy(collision.gameObject);
            power = true;
        }

        if(collision.gameObject.tag == "Finish")
        {
            SceneManager.LoadScene("Win");
        }
    }
}
