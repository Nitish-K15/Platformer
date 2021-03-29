using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            animator.SetBool("Flag",true);
        }
    }
}
