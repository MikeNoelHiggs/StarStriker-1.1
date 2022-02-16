using Assets.Scripts;
using System.Collections;
using System;
using UnityEngine;
using Pathfinding;

public class Enemy : Hazard
{

    [SerializeField] public GameObject shot;
    [SerializeField] public Transform shotSpawn;
    [SerializeField] private GameObject healthDrop;
    [SerializeField] private GameObject shieldDrop;
    [SerializeField] private GameObject missileDrop;
    [SerializeField] public Rigidbody2D rigidBody;
    [SerializeField] public bool seeker = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        GetGameControllerFromTag();
        shotSpawn = GetComponent<Transform>();
        StartCoroutine(ShotDelay());
        EnemyBehavior();
        gameController.AddToCumulativeScore(scoreValue);

        if (seeker == true)
        {
            AIDestinationSetter ads = GetComponent<AIDestinationSetter>();
            ads.target = gameController.GetPlayer().GetComponent<Transform>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        EnemyBehavior();
        DestroyOnHPZero();
    }

    private void EnemyBehavior()
    {
            ShotDelay();
            rigidBody.velocity = -transform.up * speed;    
    }

    public IEnumerator ShotDelay()
    {
        while (gameObject.activeInHierarchy)
        {
            Shoot();
            yield return new WaitForSeconds(2);
            Shoot();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Killbox" || other.tag == "Enemy" || other.tag == "Enemy Laser")
        {
            return;
        }
        else if (other.tag == "Player")
        {
            if (!gameController.GetPlayer().shield.GetActive())
            {
                gameController.GetPlayer().SubHealth(10);
                return;
            }
        }
        else if (other.tag == "Laser")
        {
            hp -= gameController.GetPlayer().GetFirePower();
            Destroy(other.gameObject);
        }
        else if (other.tag == "Missile")
        {
            hp /= 2;
            hp -= gameController.GetPlayer().GetFirePower();
            Destroy(other.gameObject);
        }
        else if (other.tag == "Ice Missile")
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = Color.cyan;
            hp /= 2;
            speed = 1;
            Destroy(other.gameObject);

            if (seeker == true)
            {
                AIPath aip = (AIPath) GetComponent<AIPath>() as AIPath;
                aip.maxSpeed = 1;
            }
        }
    }

    private void Shoot()
    {
        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
    }

    private void DestroyOnHPZero()
    {

        if (hp <= 0)
        {
            DropItem();
            Destroy(gameObject);  
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
        if (hp <= 0)
        {
            gameController.AwardScore(scoreValue);
            gameController.AwardExp(expValue);
        }
    }

    private void DropItem()
    {
        int RNG = UnityEngine.Random.Range(1, 100);

        if (RNG >= 95 && ActivePlayerProfile.upgrades[12] == true)
        {
            // 5% chance to drop Missile pickup
            Instantiate<GameObject>(missileDrop, transform.position, transform.rotation);
        }
        if (RNG > 5 && RNG < 15)
        {
            // 10% chance to drop a helath pickup
            Instantiate<GameObject>(healthDrop, transform.position, transform.rotation);
        }
        if (RNG <= 5 && ActivePlayerProfile.upgrades[3] == true)
        {
            // 5% chance to drop a shield pickup.
            Instantiate<GameObject>(shieldDrop, transform.position, transform.rotation);
        }
        // We instatiate an explosion when the ship is destory regardless of if we drop an item or not.
        Instantiate<GameObject>(explosionAnimation, transform.position, transform.rotation);
    }


    public int GetScoreValue()
    {
        return scoreValue;
    }
}
