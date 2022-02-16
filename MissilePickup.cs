using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissilePickup : ShieldPickup
{

    // Start is called before the first frame update
    void Start()
    {
        GetGameControllerFromTag();
    }

    // Update is called once per frame
    void Update()
    {
        Spin();
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player player = gameController.GetPlayer();
            player.AddMissiles(5);
            if (player.GetMissiles() > player.GetMissileCapacity())
            {
                player.SetMissiles(player.GetMissileCapacity());
            }
            Destroy(this.gameObject);
        }
    }
}