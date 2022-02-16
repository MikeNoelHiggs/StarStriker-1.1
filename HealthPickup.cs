using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthPickup : MonoBehaviour
{
    [SerializeField] public GameController gameController;
    [SerializeField] public Rigidbody2D rigidBody;
    [SerializeField] private Enum size;
    [SerializeField] private Sprite largeSprite;
    

    enum Size
    {
        Small,
        Large
    }

    private void Start()
    {
        // TODO: Tag Game controller and get that instead of player.
        GetGameControllerFromTag();

        rigidBody.angularVelocity = 100;
        rigidBody.velocity = -transform.up * 2;
  
        int RNG = UnityEngine.Random.Range(1, 100);

        if (RNG <= 20)
        {
            size = Size.Large;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = largeSprite;

        }
        if (RNG >= 21)
        {
            size = Size.Small;
        }

    }

    private void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player player = gameController.GetPlayer();
            if (size is Size.Small)
            {
                player.AddHealth(100 * ActivePlayerProfile.selectedStage);
            }
            else
            {
                player.AddHealth(player.GetMaxHealth() - player.GetHealth());
            }
            Destroy(gameObject);
        }
        else 
        {
            return;
        }
    }

    public void GetGameControllerFromTag()
    {
        GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        gameController = (GameController)gameControllerObject.GetComponent(typeof(GameController));
    }

}