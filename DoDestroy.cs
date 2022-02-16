using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoDestroy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Killbox")
        {
            return;
        }

        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}