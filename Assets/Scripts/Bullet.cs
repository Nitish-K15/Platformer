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
        rgbd.velocity = new Vector2(4, rgbd.velocity.y);
        //rgbd.AddForce(new Vector2(5, 0));
    }
    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
