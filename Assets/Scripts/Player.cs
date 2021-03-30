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
    public Transform GroundCheck,GroundCheckL,GroundCheckR,BulletSpawnPosL,BulletSpawnPosR;
    public GameObject bulletref;        
    bool power = false;                 //Check if Mario has power flower or not
    public AudioClip[] clips = new AudioClip[5];
    private AudioSource source;
    private bool isShooting;
    public bool FacingLeft;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        var h = Input.GetAxis("Horizontal");
        rigidBody2D.velocity = new Vector2(h * speed, rigidBody2D.velocity.y);
        if ((Physics2D.Linecast(transform.position, GroundCheck.position, 1 << LayerMask.NameToLayer("Ground")))||     //Raycast to check if grounded
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
                source.clip = clips[1];
                source.Play();
                animator.Play("Fire_Jump");
            }
            else
            {
                source.clip = clips[1];
                source.Play();
                animator.Play("Jump");
            }
        }

        if(Input.GetKey(KeyCode.F) && power == true)   //Shoot if Mario has powerup
        {
            animator.Play("Shoot");
            if (isShooting)
                return;
            //shoot 
            isShooting = true;

            //instantiate and shoot bullet
            GameObject bullet = Instantiate(bulletref);
            bullet.GetComponent<Bullet>().StartShoot(FacingLeft);
            if (FacingLeft)
                bullet.transform.position = BulletSpawnPosL.position;             
            else
                bullet.transform.position = BulletSpawnPosR.position;
            Invoke("ResetShoot",0.5f);               //Add delay between shots
        }
   
        if ((h < 0))
        {
            if (Isgrounded && power == true)
                animator.Play("Fire_Run");
            else if (Isgrounded)
                animator.Play("Run");
            spriteRenderer.flipX = true;
            FacingLeft = true;
        }
        else if ((h > 0)) 
        {
            if (Isgrounded && power == true)
                animator.Play("Fire_Run");
            else if (Isgrounded)
                animator.Play("Run");
            spriteRenderer.flipX = false;
            FacingLeft = false;
        }
        else if(Isgrounded)
        {
            if (power == true)
            {
                animator.Play("Fire_Idle");
            }
            else
            {
                animator.Play("Idle");
            }
            rigidBody2D.velocity = new Vector2(0, rigidBody2D.velocity.y);
        }

        score.text ="Score: " + count.ToString();       
    }

    private void ResetShoot()
    {
        isShooting = false;
        animator.Play("Idle");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")             //Die if come in contact with Enemy
        {
            Destroy(gameObject);
            SceneManager.LoadScene("Game Over");
        }
        if(collision.gameObject.tag == "EditorOnly")       // Die if fall off the map
        {
            Destroy(gameObject);
            SceneManager.LoadScene("Game Over");
        }
    }

    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.tag == "GameController")      // Get upward feedback after stomping an enemy
        {
            rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, 4f);
            source.clip = clips[3];
            source.Play();
        }
        if(collision.gameObject.tag == "Collectible")
        {
            source.clip = clips[0];
            source.Play();
            Player.count++;
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.tag == "PowerUp")        // Powers up after collecting flower
        {
            source.clip = clips[2];
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
