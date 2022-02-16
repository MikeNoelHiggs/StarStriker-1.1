using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] public Transform player;
    [SerializeField] public Vector3 offset;


    // Update is called once per frame
    void Update()
    {
        // make the camera follow the player.
        //transform.position = new Vector2(transform.position.x + offset.x, player.position.y + offset.y);

        // Don't allow camera to move out of the level bounds
        if (transform.position.y < 4.16)
        {
           // transform.position.y = 4.16;
        }

    }
}