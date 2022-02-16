using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPickup : HealthPickup
{
    [SerializeField] public float rotX;
    [SerializeField] public float rotSpeed = 20;
    void Start()
    {
        GetGameControllerFromTag();
    }

    void Update()
    {
        Spin();
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player player = gameController.GetPlayer();
            if (ActivePlayerProfile.upgrades[5] && player.GetShields() < 3)
            {
                player.AddShield();
            }
            else if (ActivePlayerProfile.upgrades[4] && player.GetShields() < 2)
            {
                player.AddShield();
            }
            else if (player.GetShields() < 1)
            {
                player.AddShield();
            }
            Destroy(gameObject);
        }
    }

    public void Spin()
    {
        rotX += Time.deltaTime * rotSpeed;
        Vector3 pos = new Vector3(rotX, transform.position.y, transform.position.z);
        rigidBody.transform.rotation = Quaternion.Euler(Vector3.up * pos.x);
        rigidBody.velocity = -transform.up * 1;
    }

}
