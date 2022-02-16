using Assets.Scripts;
using playerControls;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{ 
    public GameObject shot;
    public GameObject tailLaser;
    public GameObject missile;
    public GameObject iceMissile;
    public GameObject explosionAnimation;
    public Transform shotSpawn;
    public Transform shotSpawnB;
    public Transform shotSpawnC;
    public Transform shotSpawnD;
    public GameController gameController;
    public Shield shield;
    private PlayerControls playerControls;
    private SpriteRenderer spriteRenderer;
    private Color defaultColour;
    private float yMovementInput;
    private float xMovementInput;
    private Enum iceToggle = IceToggle.Off;
    [SerializeField] private int score;
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private int level;
    [SerializeField] private int firePower;
    [SerializeField] private float speed;
    [SerializeField] private int shields = 0;
    [SerializeField] private int missiles = 0;
    [SerializeField] private int maxMissiles = 0;
    [SerializeField] private float screenWidthInUits;

    private enum IceToggle
    {
        On,
        Off
    }


    // Mono Behavior Methods

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Get the sprite renderer and materials to allow swapping its color.
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultColour = spriteRenderer.color;
        level = gameController.GetLevelSystem().GetLevel();
        GetUpgrades();  // Give shields and/or missiles to the player if they are unlocked.   
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        FireShots();
        FireMissiles();
        GenerateShield();
        ToggleIceMissiles();
        Pause();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }


    // Methods for ther player

    /// <summary>Read movement vlaues from the input system and move the player based on this value.</summary>
    private void PlayerMovement()
    {
        // Read movement values
        yMovementInput = playerControls.Global.YMovement.ReadValue<float>();
        xMovementInput = playerControls.Global.XMovement.ReadValue<float>();

        // Move the player
        Vector3 playerPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        playerPos.y += yMovementInput * speed * Time.deltaTime;
        playerPos.x += xMovementInput * speed * Time.deltaTime;
        transform.position = playerPos;

        // Stop the player going off screen

        if (playerPos.x > screenWidthInUits)
        {
            playerPos.x = screenWidthInUits;
            transform.position = playerPos;
        }
        else if (playerPos.x < 2)
        {
            playerPos.x = 2;
            transform.position = playerPos;
        }
        if (playerPos.y > 7)
        {
            playerPos.y = 7;
            transform.position = playerPos;
        }
        else if (playerPos.y < 0)
        {
            playerPos.y = 0;
            transform.position = playerPos;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        // Handle colision with different behavior based on Gameobject's tag.
        if (collision.tag == "Laser" || collision.tag == "Killbox" || collision.tag == "Player")
        {
            return;
        }
        else if (collision.tag == "Pickup")
        {
            gameController.PlayPickupSound();
            if (health > maxHealth)
            {
                health = maxHealth;
                return;
            }
        }
        else if (collision.tag == "Enemy Laser")
        {
            if (!shield.GetActive())
            {
                SubHealth(20 * ActivePlayerProfile.selectedStage);
                ChangeColor();
                Destroy(collision.gameObject);
                return;
            }
        }   
        else if (collision.tag == "Enemy" || collision.tag == "Astroid")
        {
            ChangeColor();
        } 

        if (health <= 0)
        {
            Instantiate(explosionAnimation, transform.position, transform.rotation);
        }

        void ChangeColor()
        {
            if (shield.GetActive() == false)
            {
                spriteRenderer.color = Color.red;
                Invoke("ResetColor", .2f);
            }
        }    
    }

    private void ResetColor()
    {
        spriteRenderer.color = defaultColour;
    }
    
    /// <summary>Fire shots from the ship when the input system detects an input event, fire varying amounts of lasers based on upgrades unlocked.</summary>
    private void FireShots()
    {

        // Fire Shots
        if (playerControls.Global.Fire.triggered && health > 0 && gameController.GetPauseState() != true)
        {
            // Single Laser
            if (ActivePlayerProfile.upgrades[0] == false)
            {
                Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            }
            
            // Quad Lasers
            else if (ActivePlayerProfile.upgrades[2] == true)
            {
                Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
                Instantiate(shot, shotSpawnB.position, shotSpawnB.rotation);
                Instantiate(shot, shotSpawnC.position, shotSpawnB.rotation);
                Instantiate(tailLaser, shotSpawnD.position, shotSpawnD.rotation);
            }

            // Triple Lasers
            else if (ActivePlayerProfile.upgrades[1] == true)
            {
                Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
                Instantiate(shot, shotSpawnB.position, shotSpawnB.rotation);
                Instantiate(shot, shotSpawnC.position, shotSpawnB.rotation);
            }

            // Dual Lasers
            else if (ActivePlayerProfile.upgrades[0] == true)
            {
                Instantiate(shot, shotSpawnB.position, shotSpawnB.rotation);
                Instantiate(shot, shotSpawnC.position, shotSpawnB.rotation);
            }
        }
    }

    private void FireMissiles()
    {
        if (playerControls.Global.Missiles.triggered && health > 0 && gameController.GetPauseState() != true)
        {
            if (ActivePlayerProfile.upgrades[12] == true && missiles > 0 && iceToggle is IceToggle.Off)
            {
                Instantiate(missile, shotSpawn.position, shotSpawn.rotation);
                missiles--;
            }
            else if (ActivePlayerProfile.upgrades[17] == true && missiles > 0 && iceToggle is IceToggle.On)
            {
                Instantiate(iceMissile, shotSpawn.position, shotSpawn.rotation);
                missiles--;
            }
        }
    }

    private void ToggleIceMissiles()
    {
        if (ActivePlayerProfile.upgrades[17] == true && playerControls.Global.IceToggle.triggered)
        {
            if (iceToggle is IceToggle.Off)
            {
                iceToggle = IceToggle.On;
                gameController.ToggleIceIcon();
            }
            else
            {
                iceToggle = IceToggle.Off;
                gameController.ToggleIceIcon();
            }
        }
    }

    private void GenerateShield()
    {

        // Generate a shield
        if (playerControls.Global.Shield.triggered && shields > 0 && health > 0)
        {
            if (!shield.GetActive())
            {
                if (ActivePlayerProfile.upgrades[5] == true)
                {
                    shield.SetShieldStr(3);
                    shield.GenerateShield();
                    shields -= 1;
                }
                else if (ActivePlayerProfile.upgrades[4] == true)
                {
                    shield.SetShieldStr(2);
                    shield.GenerateShield();
                    shields -= 1;
                }
                else if (ActivePlayerProfile.upgrades[3] == true)
                {
                    shield.SetShieldStr(1);
                    shield.GenerateShield();
                    shields -= 1;
                }
            }
        }
        
    }

    /// <summary>Check if the Active Player Profile has upgrades unlocked and grant varying quantities of these upgrades if they are. </summary>
    private void GetUpgrades()
    {
        if (ActivePlayerProfile.upgrades[5] == true || ActivePlayerProfile.upgrades[4] == true || ActivePlayerProfile.upgrades[3] == true)
        {
            shields = 1;
        }


        if (ActivePlayerProfile.upgrades[16] == true)
        {
            missiles = 15;
            maxMissiles = 25;
        }
        else if (ActivePlayerProfile.upgrades[15] == true)
        {
            missiles = 10;
            maxMissiles = 20;
        }
        else if (ActivePlayerProfile.upgrades[14] == true)
        {
            missiles = 5;
            maxMissiles = 15;
        }
        else if (ActivePlayerProfile.upgrades[13] == true)
        {
            missiles = 5;
            maxMissiles = 10;
        }
        else if (ActivePlayerProfile.upgrades[12] == true)
        {
            missiles = 5;
            maxMissiles = 5;
        }
        else
        {
            missiles = 0;
            maxMissiles = 0;
        }      
    }

    private void Pause()
    {
        if (playerControls.Global.Pause.triggered && gameController.GetPauseState() == false)
        { 
            gameController.PauseGame();
            gameController.SetPauseState(!gameController.GetPauseState());
        }
        else if (playerControls.Global.Pause.triggered && gameController.GetPauseState() == true)
        {
            gameController.UnpauseGame();
            gameController.SetPauseState(false);      
        }
    }

    public int GetLevel()
    {
        return level;
    }

    public void SetLevel(int level)
    {
        this.level = level;
    }

    public int GetScore()
    {
        return score;
    }

    public void SetScore(int score)
    {
        this.score = score;
    }


    public int GetHealth()
    {
        return this.health;
    }


    public void SetHealth(int health)
    {
        this.health = health;
    }

    public void AddHealth(int value)
    {
        this.health += value;
    }

    public void SubHealth(int value)
    {
        this.health -= value;
    }

    public int GetMaxHealth()
    {
        return this.maxHealth;
    }

    public void SetMaxHealth(int maxHealth)
    {
        this.maxHealth = maxHealth;
    }

    public void AddMaxHealth(int value)
    {
        this.maxHealth += value;
    }

    public int GetFirePower()
    {
        return firePower;
    }

    public void AddFirePower(int value)
    {
        this.firePower += value; 
    }

    public void SetFirePower(int value)
    {
        this.firePower = value;
    }

    public float GetSpeed()
    {
        return this.speed;
    }


    public void AddSpeed(float value)
    {
        this.speed += value;
    }

    public void SetSpeed(float value)
    {
        this.speed = value;
    }

    public int GetShields()
    {
        return this.shields;
    }

    public int GetMissiles()
    {
        return this.missiles;
    }

    public int GetMissileCapacity()
    {
        return this.maxMissiles;
    }

    public void AddMissiles(int value)
    {
        this.missiles += value;
    }

    public void SetMissiles(int value)
    {
        this.missiles = value;
    }    

    public float GetHPNormalised()
    {
        return (float) health / maxHealth;
    }

    public void AddShield()
    {
        shields++;
    }

    public GameController GetGameController()
    {
        return gameController;
    }
}
