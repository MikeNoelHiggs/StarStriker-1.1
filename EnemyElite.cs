using Assets.Scripts;
using System.Collections;
using UnityEngine;

public class EnemyElite : Hazard
{
    [SerializeField] private GameObject shot;
    [SerializeField] private Transform shotSpawnA;
    [SerializeField] private Transform shotSpawnB;
    [SerializeField] private Player player;
    
    // Start is called before the first frame update
    void Start()
    {
        ridgidBody = GetComponent<Rigidbody2D>();
        /// TODO: Find Tag and find GameController then use that to find player when required in methods
        GetGameControllerFromTag();
        StartCoroutine(ShotDelay());
        gameController.AddToCumulativeScore(scoreValue);
        EnemyBehavior();
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
        ridgidBody.velocity = -transform.up * speed;
        
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
        }
    }

    private IEnumerator ShotDelay()
    {
        while (gameObject.activeInHierarchy)
        {
            ShootA();
            ShootB();
            yield return new WaitForSeconds(1);
            ShootB();
            ShootA();
            yield return new WaitForSeconds(2);
        }
    }

    private void ShootA()
    {
        Instantiate(shot, shotSpawnA.position, shotSpawnA.rotation);
    }

    private void ShootB()
    {
        Instantiate(shot, shotSpawnB.position, shotSpawnB.rotation);
    }

    private void DestroyOnHPZero()
    {
        if (hp <= 0)
        {
            Instantiate<GameObject>(explosionAnimation, transform.position, transform.rotation);
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

    public int GetScoreValue()
    {
        return scoreValue;
    }
}
