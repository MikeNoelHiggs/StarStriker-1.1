using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : BoltMovement
{
    [SerializeField] private GameObject explosion;


    private void Start()
    {
        GetGameController();
        Behaviour();
    }


    void Behaviour()
    {

        if (gameObject.tag == "Missile" || gameObject.tag  == "Ice Missile")
        {
            rb.velocity = transform.up * speed;
        }
        else if (gameObject.tag == "E-Missile")
        {
            rb.velocity = -transform.up * speed;
        }
        ac.Play();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.tag == "Missile" || gameObject.tag == "Ice Missile")
        {
            if (other.tag == "Killbox" || other.tag == "Player" || other.tag == "Player Laser" || other.tag == "Enemy Laser" || other.tag == "Shield" || other.tag == "Pickup")
            {
                return;
            }
            else
            {
                gameController.PlayExplosionSound();
                Instantiate(explosion, transform.position, transform.rotation);
            }
        }
        else if (gameObject.tag == "E-Missile")
        {
            if (other.tag == "Killbox" || other.tag == "Enemy" || other.tag == "Boss" || other.tag == "Player Laser" || other.tag == "Enemy Laser" || other.tag == "Pickup" || other.tag == "Shield")
            {
                return;
            }
            else if (other.tag == "Player")
            {
                if (gameController.GetPlayer().shield.GetActive() == true)
                {
                    gameController.GetPlayer().SubHealth(50);
                }
                else if (gameController.GetPlayer().shield.GetActive() == false)
                {
                    gameController.GetPlayer().SubHealth(100);
                }

                gameController.PlayExplosionSound();
                Instantiate(explosion, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}
