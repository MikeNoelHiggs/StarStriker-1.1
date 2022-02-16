using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject txtPaused;
    [SerializeField] private GameObject bossHealthBarObj;
    [SerializeField] private GameObject shieldIcon;
    [SerializeField] private GameObject shieldHealthIcon;
    [SerializeField] private GameObject missileIcon;
    [SerializeField] private GameObject iceMissileIcon;
    [SerializeField] private GameObject endStageDialog;
    [SerializeField] private AudioSource pickupSound;
    [SerializeField] private AudioSource ExplosionSound;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider expBar;
    [SerializeField] private Slider bossHPBar;
    [SerializeField] private Ship[] ships;
    [SerializeField] private Text shieldQuantityText;
    [SerializeField] private Text shieldHealthText;
    [SerializeField] private Text missileQuantityText;
    [SerializeField] private Text txtScoreValues;
    [SerializeField] private Text txtFinalScore;
    [SerializeField] private Text txtUpgrades;
    [SerializeField] private GameObject[] hazards;
    [SerializeField] private GameObject boss;
    public Text txtScore;
    public Text txtHP;
    public Text txtEXP;
    public Text txtLevel;
    public Text txtLevelUpStats;
    public Player player;
    public GameObject tmpLevelUp;
    public GameObject playerObject;
    public SpriteRenderer ShipImage;
    public LevelSystem levelSystem;
    public ScoreRecorder scoreRecorder;
    public Vector2 spawnValues;
    public int hazardCount;
    public float spawnStart;
    public float spawnTimer;
    public float waveWait;
    private bool once;
    private bool bossSpawned = false;
    private bool levelComplete = false;
    private bool pauseState;
    private int cumulativeScore = 0;
    private int Token1Score;
    private int Token2Score;
    private int Token3Score;


    public void Start()
    {
        txtScore.text = "Score:";
        playerObject = GameObject.FindGameObjectWithTag("Player");
        player = (Player)playerObject.GetComponent(typeof(Player));
        once = false;
        pauseState = false;
        StartCoroutine(SpawnWaves(ActivePlayerProfile.selectedStage));
        SetStageTokenScores(ActivePlayerProfile.selectedStage);
        CreatePlayerFromActiveProfile();
        HideIcons();
        QualitySettings.vSyncCount = 0;
        Screen.SetResolution(1920, 1080, true, 144);
    }

    public void Update()
    {
        UpdateUI();
        SpawnBoss(ActivePlayerProfile.selectedStage);
        GameOver();
    }

    private void CreatePlayerFromActiveProfile()
    {
        SetLevelSystem(levelSystem);
        Sprite playerShip = player.GetComponent<Sprite>();
        playerShip = ActivePlayerProfile.shipSprite;
        ShipImage.sprite = playerShip;
        player.SetLevel(ActivePlayerProfile.level);
        player.SetHealth(ActivePlayerProfile.currentHealth);
        player.SetMaxHealth(ActivePlayerProfile.maxHealth);
        player.SetFirePower(ActivePlayerProfile.firePower);
        player.SetSpeed(ActivePlayerProfile.speed);
        levelSystem.SetLevel(player.GetLevel(), ActivePlayerProfile.experience);
    }

    private void LevelSystem_OnLevelChanged(object sender, System.EventArgs e)
    {
        int firePowerUp = 0;
        int healthUp = 0;
        float speedUp = 0;

        player.SetLevel(levelSystem.GetLevel());
        calculateIncr();
        player.AddFirePower(firePowerUp);
        player.AddSpeed(speedUp);
        player.AddHealth(healthUp);
        player.AddMaxHealth(healthUp); 
        ActivePlayerProfile.upgradeTokens += 2;

        tmpLevelUp.SetActive(true);
        txtLevelUpStats.text = "LEVEL: " + player.GetLevel() + "\n\nMax HP + " + healthUp + "!\nFire Power + " + firePowerUp + "!\nSpeed + " + speedUp + "!" +"\n Upgrade Tokens + 2";
        Invoke("HideLevelUp", 5f);

        void calculateIncr()
        {
            if (ActivePlayerProfile.shipName.Contains("StarStriker"))
            {
                firePowerUp = 20;
                healthUp = 20;
                speedUp = 0.25f;
            }
            else if (ActivePlayerProfile.shipName.Contains("StarSpeeder"))
            {
                firePowerUp = 15;
                healthUp = 10;
                speedUp = 0.30f;
            }
            else if (ActivePlayerProfile.shipName.Contains("StarSlicer"))
            {
                firePowerUp = 25;
                healthUp = 15;
                speedUp = 0.20f;
            }
        }
    }

    private void LevelSystem_OnExperienceChanged(object sender, System.EventArgs e)
    {
        expBar.value = levelSystem.GetExperienceNormalized();
    }

    private void HideLevelUp()
    {
        tmpLevelUp.SetActive(false);
    }

    public LevelSystem GetLevelSystem()
    {
        return this.levelSystem;
    }

    private void SetLevelSystem(LevelSystem levelSystem)
    {
        this.levelSystem = levelSystem;

        // Subscribe to the events
        levelSystem.OnExperienceChanged += LevelSystem_OnExperienceChanged;
        levelSystem.OnLevelChanged += LevelSystem_OnLevelChanged;

        UpdateUI();
    }

    public Player GetPlayer()
    {
        return this.player;
    }


    /// <summary>Updates the various UI elements within the game scene based on the current state of the player</summary>
    private void UpdateUI()
    {
        canvas.SetActive(true);
        int playerLevel = player.GetLevel();
        int playerScore = player.GetScore();
        int playerHealth = player.GetHealth();
        int maxPlayerHealth = player.GetMaxHealth();
        float hpNormalised = player.GetHPNormalised();
        float expNormalised = levelSystem.GetExperienceNormalized();


        // Update the score, level, HP and EXP;
        txtScore.text = "Score: " + playerScore;
        txtEXP.text = levelSystem.GetCurrentExperience() + "/" + levelSystem.GetExperienceToNextLevel(playerLevel);
        txtLevel.text = "LV. " + playerLevel;
        txtHP.text = playerHealth + "/" + maxPlayerHealth;
        healthBar.value = hpNormalised;
        expBar.value = expNormalised;
        shieldQuantityText.text = "x " + player.GetShields().ToString();
        missileQuantityText.text = player.GetMissiles() + " / " + player.GetMissileCapacity();

        if (player.shield.GetActive() == true)
        {
            shieldHealthIcon.SetActive(true);
            shieldHealthText.text = player.shield.GetShieldHP().ToString();
        }
        
        if (player.shield.GetShieldHP() <= 0)
        {
            shieldHealthIcon.SetActive(false);
        }
    }

    public void UpdateBossHP(float value)
    {
        if (bossSpawned == true && levelComplete == false)
        {
            bossHPBar.value = value;
        }
    }


    internal void AwardScore(int enemyScoreValue)
    {
        int score = player.GetScore();
        score += enemyScoreValue;
        player.SetScore(score);
    }

    internal void AwardExp(int enemyEXPValue)
    {
        if (player.GetLevel() < 100)
        {
            levelSystem.AddExperiance(enemyEXPValue);
        }
    }

    public void AddToCumulativeScore(int value)
    {
        cumulativeScore += value;
    }

    public void SpawnBoss(int stage)
    {
        Vector2 spawnPosition = new Vector2(7, spawnValues.y);
        Quaternion spawnRotation = new Quaternion();

            if (stage == 1 && cumulativeScore >= 30000)
            {
                StopCoroutinesAndSpawn();
            }
            if (stage == 2 && cumulativeScore >= 50000)
            {
                StopCoroutinesAndSpawn();
            }
            if (stage == 3 && cumulativeScore >= 75000)
            {
                StopCoroutinesAndSpawn();
            }
            if (stage == 4 && cumulativeScore >= 100000)
            {
                StopCoroutinesAndSpawn();
            }
            if (stage == 5 && cumulativeScore == 150000)
            {
                StopCoroutinesAndSpawn();
            }
            if (stage == 6 && cumulativeScore == 200000)
            {
                StopCoroutinesAndSpawn();
            }
        

        void StopCoroutinesAndSpawn()
        {
            if (bossSpawned == false)
            {
                StopAllCoroutines();
                Instantiate(boss, spawnPosition, spawnRotation);
                bossHealthBarObj.SetActive(true);
                bossSpawned = true;
            }
        }
    }

    public void SetStageTokenScores(int stage)
    {
        if (stage == 1)
        {
            Token1Score = 12000;
            Token2Score = 15000;
            Token3Score = 21000;
        }
        else if (stage == 2)
        {
            Token1Score = 20000;
            Token2Score = 25000;
            Token3Score = 35000;
        }
        else if (stage == 3)
        {
            Token1Score = 30000;
            Token2Score = 37500;
            Token3Score = 52500;
        }
        else if (stage == 4)
        {
            Token1Score = 40000;
            Token2Score = 50000;
            Token3Score = 70000;
        }
        else if (stage == 5)
        {
            Token1Score = 60000;
            Token2Score = 75000;
            Token3Score = 105000;
        }
        else
        {
            Token1Score = 80000;
            Token2Score = 100000;
            Token3Score = 140000;
        }
    }

    public void EndStage(int stage)
    {
        StopAllCoroutines();
        endStageDialog.SetActive(true);
        levelComplete = true;
        if (stage < 6)
        {
            ActivePlayerProfile.stages[stage] = true;
        }
        
        UpdateActiveProfile();

        if (player.GetScore() >= Token3Score && ActivePlayerProfile.stageInfo[stage - 1].upgrades[2] == false)
        {
            ActivePlayerProfile.upgradeTokens += 3;
            ActivePlayerProfile.stageInfo[stage - 1].upgrades[0] = true;
            ActivePlayerProfile.stageInfo[stage - 1].upgrades[1] = true;
            ActivePlayerProfile.stageInfo[stage - 1].upgrades[2] = true;
        }
        else if (player.GetScore() >= Token2Score && ActivePlayerProfile.stageInfo[stage -1].upgrades[1] == false)
        {
            ActivePlayerProfile.upgradeTokens += 2;
            ActivePlayerProfile.stageInfo[stage - 1].upgrades[0] = true;
            ActivePlayerProfile.stageInfo[stage - 1].upgrades[1] = true;
        }
        else if (player.GetScore() >= Token1Score && ActivePlayerProfile.stageInfo[stage -1].upgrades[0] == false)
        {
            ActivePlayerProfile.upgradeTokens++;
            ActivePlayerProfile.stageInfo[stage - 1].upgrades[0] = true;
        }

        txtScoreValues.text = Token1Score + "\n\n" + Token2Score + "\n\n" + Token3Score;
        txtFinalScore.text = "You Scored: " + player.GetScore();
        txtUpgrades.text = "Upgrade Tokens: " + ActivePlayerProfile.stageInfo[stage-1].upgrades.Where(u => u == true).Count<bool>() + " / " + ActivePlayerProfile.stageInfo[stage-1].upgrades.Length;

        if (player.GetScore() > ActivePlayerProfile.stageInfo[stage-1].highscore)
        {
            ActivePlayerProfile.stageInfo[stage - 1].highscore = player.GetScore();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
        txtPaused.SetActive(true);
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        txtPaused.SetActive(false);
    }

    public bool GetPauseState()
    {
        return pauseState;
    }

    public void SetPauseState(bool pauseState)
    {
        this.pauseState = pauseState;
    }

    public void PlayPickupSound()
    {
        pickupSound.Play();
    }

    public void PlayExplosionSound()
    {
        ExplosionSound.Play();
    }

    private void SaveGame()
    {
        UpdateActiveProfile();
        PlayerProfile profile = SaveSystem.LoadFromSlotOne();
        
        profile.shipName = ActivePlayerProfile.shipName;
        profile.level = ActivePlayerProfile.level;
        profile.experience = ActivePlayerProfile.experience;
        profile.maxHealth = profile.currentHealth = ActivePlayerProfile.maxHealth;
        profile.firePower = ActivePlayerProfile.firePower;
        profile.speed = ActivePlayerProfile.speed;
        profile.chosenCharacter = ActivePlayerProfile.chosenCharacter;
        profile.upgrades = ActivePlayerProfile.upgrades;
        profile.stages = ActivePlayerProfile.stages;
        profile.upgradeTokens = ActivePlayerProfile.upgradeTokens;
        profile.stageInfo = ActivePlayerProfile.stageInfo;

        if (ActivePlayerProfile.saveSlot == 1)
        {
            SaveSystem.SaveToSlotOne(profile);
        }
        else if (ActivePlayerProfile.saveSlot == 2)
        {
            SaveSystem.SaveToSlotTwo(profile);
        }
        else
        {
            SaveSystem.SaveToSlotThree(profile);
        }
    }

    /// <summary> Updates the ActivePlayerProfile struct with new data from the gameplay session so that this data can persist between scenes.</summary>
    private void UpdateActiveProfile()
    {
        ActivePlayerProfile.level = levelSystem.GetLevel();
        ActivePlayerProfile.experience = levelSystem.GetCurrentExperience();
        ActivePlayerProfile.maxHealth = player.GetMaxHealth();
        ActivePlayerProfile.firePower = player.GetFirePower();
        ActivePlayerProfile.speed = player.GetSpeed();
    }

    private void HideIcons()
    {
        if (ActivePlayerProfile.upgrades[3] == false)
        {
            shieldIcon.SetActive(false);
            shieldHealthIcon.SetActive(false);
        }
        if (ActivePlayerProfile.upgrades[12] == false)
        {
            missileIcon.SetActive(false);
        }
    }

    public void ToggleIceIcon()
    {
        if (iceMissileIcon.activeInHierarchy == false)
        {
            iceMissileIcon.SetActive(true);
        }
        else
        {
            iceMissileIcon.SetActive(false);
        }
    }

    private void GameOver()
    {
        // GameOver
        if (player.GetHealth() <= 0)
        {
            scoreRecorder.SetScore(player);
            player.SetHealth(0);
            SaveGame();
            if (once == false)
            {
                StopAllCoroutines();
            }
            once = true;
            playerObject.GetComponent<Renderer>().enabled = false;
            playerObject.GetComponentInChildren<ParticleSystem>().Stop();
            StartCoroutine(WaitForAnimation());
        }
    }

    private IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(2);
        SceneLoader.GameOver(); 
    }

    
    private IEnumerator SpawnWaves(int stage)
    {
        if (stage == 1)
        {
            while (true)
            {
                for (int i = 0; i < hazardCount; i++)
                {
                    int RNG;
                    if (player.GetLevel() < 3)
                    {
                        RNG = Random.Range(0, 3);
                    }
                    else if (player.GetLevel() < 7)
                    {
                        RNG = Random.Range(0, 5);
                    }
                    else if (player.GetLevel() < 10)
                    {
                        RNG = Random.Range(0, 6);
                    }
                    else
                    {
                        RNG = Random.Range(0, 9);
                    }
                    yield return new WaitForSeconds(spawnStart - 0.05f * 3);
                    Spawn(RNG);
                    yield return new WaitForSeconds(spawnTimer);
                }
                yield return new WaitForSeconds(waveWait);
            }
        }
        else if (stage == 2)
        {
            // Duplicate code here, however a coroutine will continue to run once it has started so code cannot be called via another method, this would start several coroutines.
            while (true)
            {
                for (int i = 0; i < hazardCount; i++)
                {
                    int RNG;
                    RNG = Random.Range(0, 11);
                    yield return new WaitForSeconds(spawnStart - 0.05f * 4);
                    Spawn(RNG);
                    yield return new WaitForSeconds(spawnTimer);
                }
                yield return new WaitForSeconds(waveWait);
            }
        }
        else if (stage == 3)
        {
            while (true)
            {
                for (int i = 0; i < hazardCount; i++)
                {
                    int RNG;
                    RNG = Random.Range(0, 7);
                    yield return new WaitForSeconds(spawnStart - 0.05f * 5);
                    Spawn(RNG);
                    yield return new WaitForSeconds(spawnTimer);
                }
                yield return new WaitForSeconds(waveWait);
            }
        }
        else if (stage == 4)
        {
            while (true)
            {
                for (int i = 0; i < hazardCount; i++)
                {
                    int RNG;
                    RNG = Random.Range(0, 15);
                    yield return new WaitForSeconds(spawnStart - 0.05f * 6);
                    Spawn(RNG);
                    yield return new WaitForSeconds(spawnTimer);
                }
                yield return new WaitForSeconds(waveWait);
            }
        }
        else if (stage == 5)
        {
            while (true)
            {
                for (int i = 0; i < hazardCount; i++)
                {
                    int RNG;
                    RNG = Random.Range(0, 15);
                    yield return new WaitForSeconds(spawnStart - 0.05f * 7);
                    Spawn(RNG);
                    yield return new WaitForSeconds(spawnTimer);
                }
                yield return new WaitForSeconds(waveWait);
            }
        }
        else if (stage == 6)
        {
            while (true)
            {
                for (int i = 0; i < hazardCount; i++)
                {
                    int RNG;
                    RNG = Random.Range(0, 5);
                    yield return new WaitForSeconds(spawnStart - 0.05f * 8);
                    Spawn(RNG);
                    yield return new WaitForSeconds(spawnTimer);
                }
                yield return new WaitForSeconds(waveWait);
            }
        }

        void Spawn(int RNG)
        {
            Vector2 spawnPosition = new Vector2(Random.Range(2, spawnValues.x), spawnValues.y);
            Quaternion spawnRotation = new Quaternion();
            Instantiate(hazards[RNG], spawnPosition, spawnRotation);
        }
    }
}    


