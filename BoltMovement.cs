using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltMovement : MonoBehaviour
{

    public Rigidbody2D rb;
    public GameController gameController;
    public AudioSource ac;
    public float speed;


    // Start is called before the first frame update
    void Start()
    {
        GetGameController();
        Behaviour();
    }
    
    private void Behaviour()
    {
        rb = GetComponent<Rigidbody2D>();
        if (gameObject.tag == "Laser")
        {
            rb.velocity = transform.up * speed;
            ac.Play();
        }
        else if (gameObject.tag == "Enemy Laser")
        {
            rb.velocity = -transform.up * speed;
            ac.Play();
        }
    }

    public void GetGameController()
    {
        /// TODO: When implemented use controller to create temporary player variable in scope of GetPlayer(). Rename class to "GetController()".
        try
        {
            GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
            gameController = (GameController) gameControllerObject.GetComponent(typeof(GameController));
        }
        catch (NullReferenceException e)
        {
            Debug.LogError(e);
        }
    }
}
