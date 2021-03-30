using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void Start()
    {
        Invoke("DestroySelf", 1f);  //Destroy Bullet after set time
    }

    public void StartShoot(bool FacingLeft)
    {
        Rigidbody2D rgbd = GetComponent<Rigidbody2D>();
        if (FacingLeft)                                    //Check to spawn bullets in the correct direction
        {
            rgbd.velocity = new Vector2(-3, 0);
        }
        else
        {
            rgbd.velocity = new Vector2(3, 0);
        }
    }
    private void DestroySelf()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "EditorOnly")   //Destroy Bullets if they touch a wall or fall of map
            Destroy(gameObject);
    }
}
