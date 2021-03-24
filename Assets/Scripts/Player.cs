using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    Animator animator;
    private float speed = 4f;
    private float jumpspeed = 5f;
    private bool Isgrounded = true;
    private Rigidbody2D rigidBody2D;
    private SpriteRenderer spriteRenderer;
    public Text score;
    public static int count = 0;
    public Transform GroundCheck;
    Object bulletref;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        bulletref = Resources.Load("Prefabs/Bullet");
    }

    // Update is called once per frame
    void Update()
    {
        var h = Input.GetAxis("Horizontal");
        rigidBody2D.velocity = new Vector2(h * speed, rigidBody2D.velocity.y);
        if (Physics2D.Linecast(transform.position, GroundCheck.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            Isgrounded = true;
        }
        else
        {
            Isgrounded = false;
            animator.Play("Jump");
        }
 
        if (Input.GetKeyDown(KeyCode.Space) && Isgrounded == true)
        {
            rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, jumpspeed);
            animator.Play("Jump");
        }

        if ((h < 0))
        {
            //transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y, transform.localScale.z);
            if(Isgrounded)
                animator.Play("Run");
            spriteRenderer.flipX = true;
        }
        else if ((h > 0)) 
        {
            //transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * 1, transform.localScale.y, transform.localScale.z);
            if(Isgrounded)
                animator.Play("Run");
            spriteRenderer.flipX = false;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                GameObject bullet = (GameObject)Instantiate (bulletref);
                bullet.transform.position = new Vector2(transform.position.x + 0.4f, transform.position.y + 0.4f);
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
}
