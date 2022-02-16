using Assets.Scripts;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField] public float rotZ;
    [SerializeField] public float rotSpeed;
    [SerializeField] private Transform shotSpawnB;
    [SerializeField] private Transform shotSpawnC;
    [SerializeField] private Transform shotSpawnD;
    [SerializeField] private SpriteRenderer shieldSprite;
    [SerializeField] private bool shieldActive;
    [SerializeField] private int shieldHP = 5000;
    
    

    // Start is called before the first frame update
    void Start()
    {
        GetGameControllerFromTag();
        StartCoroutine(ShotDelay());
        if (ActivePlayerProfile.selectedStage == 3 || ActivePlayerProfile.selectedStage == 6)
        {
            shieldActive = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        EnemyBehavior();
        DestroyOnHPZero();
    }

    private IEnumerator ShotDelay()
    {
        while (gameObject.activeInHierarchy)
        {
            Shoot();
            yield return new WaitForSeconds(2);
            Shoot();
            yield return new WaitForSeconds(1);
            Shoot();
            yield return new WaitForSeconds(1);
        }
    }

    void EnemyBehavior()
    {
        if (ActivePlayerProfile.selectedStage != 6)
        {
            rotZ += Time.deltaTime * rotSpeed;
            Vector3 pos = new Vector3(transform.position.x, transform.position.y, rotZ);
            rigidBody.transform.rotation = Quaternion.Euler(Vector3.forward * pos.z);
        }

        if (transform.position.y <= 15)
        {
            rigidBody.velocity = -transform.up * speed;
        }
        

        if (seeker == true)
        {          
            AIDestinationSetter ads = GetComponent<AIDestinationSetter>();
            ads.target = gameController.GetPlayer().GetComponent<Transform>();
        }
    }

    private void Shoot()
    {
        if (ActivePlayerProfile.selectedStage == 1)
        {
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        }
        else if (ActivePlayerProfile.selectedStage == 2)
        {
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            Instantiate(shot, shotSpawnB.position, shotSpawnB.rotation);
            Instantiate(shot, shotSpawnC.position, shotSpawnC.rotation);
        }
        else if (ActivePlayerProfile.selectedStage == 3)
        {
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            Instantiate(shot, shotSpawnB.position, shotSpawnB.rotation);
            Instantiate(shot, shotSpawnC.position, shotSpawnC.rotation);
            Instantiate(shot, shotSpawnD.position, shotSpawnD.rotation);
        }
        else if (ActivePlayerProfile.selectedStage == 4)
        {
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            Instantiate(shot, shotSpawnB.position, shotSpawnB.rotation);
            Instantiate(shot, shotSpawnC.position, shotSpawnC.rotation);
            Instantiate(shot, shotSpawnD.position, shotSpawnD.rotation);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = gameController.GetPlayer();
        
        if (other.tag is "Killbox" || other.tag is "Enemy Laser")
        {
            return;
        }
        else if (other.tag == "Astroid")
        {
            hp--;
            return;
        }
        else if (other.tag == "Player")
        {
            player.SubHealth(200);
            return;
        }
        else if (other.tag is "Laser" || other.tag is "Missile")
        {
            Destroy(other.gameObject);

            if (other.tag is "Laser")
            {
                if (!shieldActive)
                {
                    hp -= player.GetFirePower();
                    gameController.UpdateBossHP(GetHPNormalised());
                }
                else
                {
                    shieldHP -= player.GetFirePower();
                    if (shieldHP <= 0)
                    {
                        shieldSprite.enabled = false;
                        shieldActive = false;
                        Invoke("GenerateShield", 10);
                    }
                }
            }
            else if (other.tag is "Missile")
            {
                if (!shieldActive)
                {
                    hp -= (player.GetFirePower() * 4);
                    gameController.UpdateBossHP(GetHPNormalised());
                }
                else
                {
                    hp -= (player.GetFirePower() * 2);
                    shieldHP /= 2;
                    gameController.UpdateBossHP(GetHPNormalised());
                    
                    if (shieldHP <= 0)
                    {
                        shieldSprite.enabled = false;
                        shieldActive = false;
                        Invoke("GenerateShield", 10);
                    }
                }
            }
        }

        
    }

    private void DestroyOnHPZero()
    {
        if (hp <= 0)
        {
            Instantiate<GameObject>(explosionAnimation, transform.position, transform.rotation);
            Destroy(gameObject);
            gameController.EndStage(ActivePlayerProfile.selectedStage);
        }
    }

    public float GetHPNormalised()
    {
        if (ActivePlayerProfile.selectedStage == 1)
        {
            return (float) hp / 10000;
        }
        else if (ActivePlayerProfile.selectedStage == 2)
        {
            return (float) hp / 30000;
        }
        else if (ActivePlayerProfile.selectedStage == 3)
        {
            return (float)hp / 100000;
        }
        else
        {
            return (float) hp / 100000;
        }
    }

    public void GenerateShield()
    {
        shieldHP = 5000;
        shieldSprite.enabled = true;
        shieldActive = true;
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

}
