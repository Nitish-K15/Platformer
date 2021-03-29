using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rgbd;
   
    // Start is called before the first frame update
    void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
        Invoke("DestroySelf", 1f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
            rgbd.velocity = new Vector2(5f, rgbd.velocity.y);
    }
    private void DestroySelf()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
            Destroy(gameObject);
    }
}
