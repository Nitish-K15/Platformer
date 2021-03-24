using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    private void FixedUpdate()
    {
        float Xpos = player.position.x;
        float cameraX = Mathf.Clamp(Xpos, -1 , 100);
        transform.position = new Vector3(cameraX, 0, -15);
    }
}
