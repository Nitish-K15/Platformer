using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialBox : MonoBehaviour
{
    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)   //Check if box is hidden from beneath
    {
        if (collision.gameObject.tag == "Player")
        {
           GetComponentInParent<Special>().BlockBounce();
        }
    }
}
