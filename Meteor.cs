using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : Hazard
{


    [SerializeField] private float tumble;


    private void Start()
    {
        GetGameControllerFromTag();

        scoreValue = 10;
        expValue = 10;
        speed = 1.2f;
        hp = 25;
        ridgidBody = GetComponent<Rigidbody2D>();
        ridgidBody.angularVelocity = Random.Range(-10f, 10f) * tumble;
        ridgidBody.velocity = -transform.up * speed;
        gameController.AddToCumulativeScore(scoreValue);
    }

    private void Update()
    {
        DestoryOnHPZero();
    }

    private void OnDestroy()
    {
        if (hp <= 0)
        {
            gameController.AwardScore(scoreValue);
            gameController.AwardExp(expValue);
        }
    }

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = gameController.GetPlayer();
        if (other.tag == "Killbox" || other.tag == "Enemy" || other.tag == "Shield")
        {
            return;
        }
        else if (other.tag == "Player")
        {
            if (!player.shield.GetActive())
            {
                player.SubHealth(10);
                return;
            }
        }
        else if (other.tag == "Enemy Laser")
        {
            Destroy(other.gameObject);
            return;
        }
        else if (other.tag == "Laser")
        {
            hp -= player.GetFirePower();
        }
        else if (other.tag == "Missile")
        {
            Destroy(other.gameObject);
            hp -= 1000;
        }
    }

    private void DestoryOnHPZero()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }


    public int GetScoreValue()
    {
        return this.scoreValue;
    }
}