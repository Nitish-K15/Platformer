using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stomp : MonoBehaviour
{
    public bool isKoopa = false;          //Check to see if box is applied to Koopa or Goomba
    
    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (isKoopa == true)
            {
                GetComponentInParent<Koopa>().DeadEnemy();
                GetComponentInParent<Koopa>().isStomp = true;
                Destroy(gameObject);
            }
            else
                GetComponentInParent<Goomba>().DeadEnemy();
        }
    }
}
