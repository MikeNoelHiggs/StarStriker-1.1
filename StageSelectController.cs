using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectController : MonoBehaviour
{
    [SerializeField] private Image characterImage;
    [SerializeField] private Image ShipImage;
    [SerializeField] private Sprite[] characterImages;
    [SerializeField] private Ship[] ships;
    [SerializeField] private TextMeshProUGUI characterNameText;
    [SerializeField] private TextMeshProUGUI currentText;
    [SerializeField] private TextMeshProUGUI shipNameText;
    [SerializeField] private TextMeshProUGUI stageInfoText;
    [SerializeField] private TextMeshProUGUI characterPanalStatsText;
    [SerializeField] private TextMeshProUGUI stageInfoPreviewText;
    [SerializeField] private TextMeshProUGUI buyText;
    [SerializeField] private TextMeshProUGUI txtLaunch;
    [SerializeField] private Text itemInfoText;
    [SerializeField] private Text itemDescriptionText;
    [SerializeField] private int selectedStage = 1;
    [SerializeField] private Upgrade selectedUpgrade;
    [SerializeField] private Text tokensText;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip purchaseSound;
    [SerializeField] private AudioClip incorrectSound;
    [SerializeField] private SceneLoader sl;

    private void Start()
    {
        SetupProfile();
        ChangeStageInfoText();
        tokensText.text = "Tokens: " + ActivePlayerProfile.upgradeTokens;
        ActivePlayerProfile.selectedStage = 1;
    }

    ///<summary>Read the values from the ActivePlayerProfile struct and display them to the user on the Stage Select Scene</summary>
    private void SetupProfile()
    {
        characterNameText.text = ActivePlayerProfile.chosenCharacter;
        shipNameText.text = ActivePlayerProfile.shipName;
        characterPanalStatsText.text = ActivePlayerProfile.level 
            + "\n" + ActivePlayerProfile.currentHealth + "/" + ActivePlayerProfile.maxHealth 
            + "\n" + ActivePlayerProfile.firePower + "\n" + ActivePlayerProfile.speed
            + "\n" + ActivePlayerProfile.upgradeTokens  
            + "\n" + ActivePlayerProfile.upgrades.Where(u => u == true).Count<bool>() + "/" + ActivePlayerProfile.upgrades.Length 
            + "\n" + ActivePlayerProfile.stages.Where(s => s == true).Count<bool>() + "/" + ActivePlayerProfile.stages.Length;
        ChangeImages();
    }

    /// <summary> Used to swap the ship and character sprites based on the players chosen character.</summary>
    private void ChangeImages()
    {
        // Setup profile changes the text property of CharacterNameText, adjust the sprite based on the text, get the ship sprite from the struct.
        if (characterNameText.text == "Greg Starstriker")
        {
            characterImage.sprite = characterImages[0];
        }
        else
        {
            characterImage.sprite = characterImages[1];
        }

        ShipImage.sprite = ActivePlayerProfile.shipSprite;
    }

    /// <summary>Called on a mouse hover event updates the highscore for the player when they are selecting a stage to choose.</summary>
    /// <param name="stage">A stage to have it's data displayed for, passed in automatically via mouse hover.</param>
    /// <returns>The highscore stored in the struct representing the player.</returns>
    private int GetSelectedStageHighScore(int stage)
    {
        if (stage == 1)
        {
            return ActivePlayerProfile.stageInfo[0].highscore;
        }
        else if (stage == 2)
        {
            return ActivePlayerProfile.stageInfo[1].highscore;
        }
        else if (stage == 3)
        {
            return ActivePlayerProfile.stageInfo[2].highscore;
        }
        else if (stage == 4)
        {
            return ActivePlayerProfile.stageInfo[3].highscore;
        }
        else if (stage == 5)
        {
            return ActivePlayerProfile.stageInfo[4].highscore;
        }
        else
        {
            return ActivePlayerProfile.stageInfo[5].highscore;
        }
    }

    /// <summary>Called on a mouse hover event updates the unlock tokens aquired on a stage for the player when they are selecting a stage to choose</summary>
    /// <param name="stage">A stage to have its data displayed for, pased in automatically via mouse hover.</param>
    /// <returns>The upgrades unlocked by the player for that stage.</returns>
    private string GetSelectedStageUpgrades(int stage)
    {
        if (stage == 1)
        {
            return ActivePlayerProfile.stageInfo[0].upgrades.Where(u => u == true).Count<bool>() + "/" + ActivePlayerProfile.stageInfo[0].upgrades.Length;
        }
        else if (stage == 2)
        {
            return ActivePlayerProfile.stageInfo[1].upgrades.Where(u => u == true).Count<bool>() + "/" + ActivePlayerProfile.stageInfo[1].upgrades.Length;
        }
        else if (stage == 3)
        {
            return ActivePlayerProfile.stageInfo[2].upgrades.Where(u => u == true).Count<bool>() + "/" + ActivePlayerProfile.stageInfo[2].upgrades.Length;
        }
        else if (stage == 4)
        {
            return ActivePlayerProfile.stageInfo[3].upgrades.Where(u => u == true).Count<bool>() + "/" + ActivePlayerProfile.stageInfo[3].upgrades.Length;
        }
        else if (stage == 5)
        {
            return ActivePlayerProfile.stageInfo[4].upgrades.Where(u => u == true).Count<bool>() + "/" + ActivePlayerProfile.stageInfo[4].upgrades.Length;
        }
        else
        {
            return ActivePlayerProfile.stageInfo[5].upgrades.Where(u => u == true).Count<bool>() + "/" + ActivePlayerProfile.stageInfo[5].upgrades.Length;
        }
    }

    /// <summary>Updates the global variable selectedStage, this tells the controller which level to load.</summary>
    /// <param name="stage">The stage the player has selected to play.</param>
    public void ChangeStage(int stage)
    {
        selectedStage = stage;
        ActivePlayerProfile.selectedStage = stage;
        txtLaunch.text = "Launch";
    }

    /// <summary>Displays info at the bottom of the scene about the stage the player has chosen to play. This is shown on the main scene when no additional menus are open.</summary>
    public void ChangeStageInfoText()
    {
        stageInfoText.text = "Selected Stage: Stage " + selectedStage + "\nHighscore: "
            + GetSelectedStageHighScore(selectedStage) + "\nUpgrade Tokens: " + GetSelectedStageUpgrades(selectedStage);
        currentText.text = "Stage " + selectedStage;

    }

    public void LaunchLevel()
    {
        if (ActivePlayerProfile.stages[selectedStage -1] == true)
        {
            sl.LoadLevel();
        }
        else
        {
            txtLaunch.text = "LOCKED";
        }
    }

    /// <summary>Uses the GetStageInfo() method to then display that data at the bottom on the stage Select dialog. </summary>
    /// <param name="stage">The stage regarding the information displayed.</param>
    public void UpdateInfoPreview(int stage)
    {

        // Call a private function to retrieve the string for the text to be updated to, we are looking in the relevant area of the stageInfo array and 
        // getting the amount of upgrades the user has back from another function GetSelectedStageUpgrades. i+1 as stages start at 1 not zero like the array.
        if (stage == 1)
        {
            GetStageInfo(0);
        }
        else if (stage == 2)
        {
            GetStageInfo(1);
        }
        else if (stage == 3)
        {
            GetStageInfo(2);
        }
        else if (stage == 4)
        {
            GetStageInfo(3);
        }
        else if (stage == 5)
        {
            GetStageInfo(4);
        }
        else
        {
            GetStageInfo(5);
        }


        void GetStageInfo(int i)
        {
            int highscore = ActivePlayerProfile.stageInfo[i].highscore;
            string upgrades = GetSelectedStageUpgrades(i + 1);
            stageInfoPreviewText.text = "Highscore: " + highscore + "\nUpgrade Tokens: " + upgrades;
        }
    }
    public void UpdateItemText(Upgrade upgrade)
    {
        itemInfoText.text = upgrade.GetName() + "\n" + upgrade.GetLevelRequired() + "\n" + upgrade.GetPrice() + " Tokens";
        itemDescriptionText.text = upgrade.GetDescription();
        buyText.text = "Buy";
        selectedUpgrade = upgrade;
        if (CheckUpgradeUnlocked(selectedUpgrade.GetName()))
        {
            buyText.text = "PURCHASED";
        }

    }

    public void Buy()
    {
        Buy(selectedUpgrade);

        void Buy(Upgrade upgrade)
        {
            if (CheckUpgradeUnlocked(upgrade.GetName()))
            {
                buyText.text = "PURCHASED";
            }
            else if (ActivePlayerProfile.upgradeTokens < upgrade.GetPrice())
            {
                buyText.text = "NOT ENOUGH TOKENS";
                PlayInvalidSound();

            }
            else if (ActivePlayerProfile.level < upgrade.GetLevelRequired())
            {
                buyText.text = "HIGHER LEVEL REQUIRED";
                PlayInvalidSound();
            }
            else if (upgrade.GetName() == "Triple Lasers" && ActivePlayerProfile.upgrades[0] == false)
            {
                buyText.text = "PURCHASE DUAL LASERS FIRST";
                PlayInvalidSound();
            }
            else if (upgrade.GetName() == "Tail Laser" && ActivePlayerProfile.upgrades[1] == false)
            {
                buyText.text = "PURCHASE TRIPLE LASERS FIRST";
                PlayInvalidSound();
            }
            else if (upgrade.GetName() == "Shields II" && ActivePlayerProfile.upgrades[3] == false)
            {
                buyText.text = "PURCHASE SHIELDS I FIRST";
                PlayInvalidSound();
            }
            else if (upgrade.GetName() == "Shields III" && ActivePlayerProfile.upgrades[4] == false)
            {
                buyText.text = "PURCHASE SHIELDS II FIRST";
                PlayInvalidSound();
            }
            else if (upgrade.GetName() == "HP+ II" && ActivePlayerProfile.upgrades[6] == false)
            {
                buyText.text = "PURCHASE HP+ I FIRST";
                PlayInvalidSound();
            }
            else if (upgrade.GetName() == "HP+ III" && ActivePlayerProfile.upgrades[7] == false)
            {
                buyText.text = "PURCHASE HP+ II FIRST";
                PlayInvalidSound();
            }
            else if (upgrade.GetName() == "HP+ IV" && ActivePlayerProfile.upgrades[8] == false)
            {
                buyText.text = "PURCHASE HP+ III FIRST";
                PlayInvalidSound();
            }
            else if (upgrade.GetName() == "HP+ V" && ActivePlayerProfile.upgrades[9] == false)
            {
                buyText.text = "PURCHASE HP+ IV FIRST";
                PlayInvalidSound();
            }
            else if (upgrade.GetName() == "HP+ VI" && ActivePlayerProfile.upgrades[10] == false)
            {
                buyText.text = "PURCHASE HP+ V FIRST";
                PlayInvalidSound();
            }
            else if (upgrade.GetName() == "Ice Missiles" && ActivePlayerProfile.upgrades[12] == false)
            {
                buyText.text = "PURCHASE MISSILES FIRST";
                PlayInvalidSound();
            }
            else if (upgrade.GetName() == "Missile Expansion I" && ActivePlayerProfile.upgrades[12] == false)
            {
                buyText.text = "PURCHASE MISSILES FIRST";
                PlayInvalidSound();
            }
            else if (upgrade.GetName() == "Missile Expansion II" && ActivePlayerProfile.upgrades[13] == false)
            {
                buyText.text = "PURCHASE MISSILE EXPANSION I FIRST";
                PlayInvalidSound();
            }
            else if (upgrade.GetName() == "Missile Expansion III" && ActivePlayerProfile.upgrades[14] == false)
            {
                buyText.text = "PURCHASE MISSILE EXPANSION II FIRST";
                PlayInvalidSound();
            }
            else if (upgrade.GetName() == "Missile Expansion IV" && ActivePlayerProfile.upgrades[15] == false)
            {
                buyText.text = "PURCHASE MISSILE EXPANSION III FIRST";
                PlayInvalidSound();
            }
            else
            {
                ActivePlayerProfile.upgradeTokens -= upgrade.GetPrice();
                UnlockUpgrade();
                SetupProfile();
                tokensText.text = "Tokens: " + ActivePlayerProfile.upgradeTokens;
                buyText.text = "PURCHASED";
                audioSource.clip = purchaseSound;
                audioSource.Play();
            }
        }

        void PlayInvalidSound()
        {
            audioSource.clip = incorrectSound;
            audioSource.Play();
        }
    }


    public void UnlockUpgrade()
    {
        // 00 Duel Lasers
        // 01 Triple Lasers
        // 02 Tail Bomb
        // 03 Shields I
        // 04 Shields II
        // 05 Shields III
        // 06 HP+ I
        // 07 HP+ II
        // 08 HP+ III
        // 09 HP+ IV
        // 10 HP+ V
        // 11 HP+ VI
        // 12 Missiles
        // 13 Missile Expansion I
        // 14 Missile Expansion II
        // 15 Missile Expansion III
        // 16 Missile Expansion IV
        // 17 Ice Missiles

        if (selectedUpgrade.GetName() == "Dual Lasers")
        {
            ActivePlayerProfile.upgrades[0] = true;
        }
        else if (selectedUpgrade.GetName() == "Triple Lasers")
        {
            ActivePlayerProfile.upgrades[1] = true;
        }
        else if (selectedUpgrade.GetName() == "Tail Laser")
        {
            ActivePlayerProfile.upgrades[2] = true;
        }
        else if (selectedUpgrade.GetName() == "Shields I")
        {
            ActivePlayerProfile.upgrades[3] = true;
        }
        else if (selectedUpgrade.GetName() == "Shields II")
        {
            ActivePlayerProfile.upgrades[4] = true;
        }
        else if (selectedUpgrade.GetName() == "Shields III")
        {
            ActivePlayerProfile.upgrades[5] = true;
        }
        else if (selectedUpgrade.GetName() == "HP+ I")
        {
            ActivePlayerProfile.upgrades[6] = true;
            ActivePlayerProfile.maxHealth += 100;
            ActivePlayerProfile.currentHealth = ActivePlayerProfile.maxHealth;
        }
        else if (selectedUpgrade.GetName() == "HP+ II")
        {
            ActivePlayerProfile.upgrades[7] = true;
            ActivePlayerProfile.maxHealth += 200;
            ActivePlayerProfile.currentHealth = ActivePlayerProfile.maxHealth;
        }
        else if (selectedUpgrade.GetName() == "HP+ III")
        {
            ActivePlayerProfile.upgrades[8] = true;
            ActivePlayerProfile.maxHealth += 500;
            ActivePlayerProfile.currentHealth = ActivePlayerProfile.maxHealth;
        }
        else if (selectedUpgrade.GetName() == "HP+ IV")
        {
            ActivePlayerProfile.upgrades[9] = true;
            ActivePlayerProfile.maxHealth += 1000;
            ActivePlayerProfile.currentHealth = ActivePlayerProfile.maxHealth;
        }
        else if (selectedUpgrade.GetName() == "HP+ V")
        {
            ActivePlayerProfile.upgrades[10] = true;
            ActivePlayerProfile.maxHealth += 2500;
            ActivePlayerProfile.currentHealth = ActivePlayerProfile.maxHealth;
        }
        else if (selectedUpgrade.GetName() == "HP+ VI")
        {
            ActivePlayerProfile.upgrades[11] = true;
            ActivePlayerProfile.maxHealth += 5000;
            ActivePlayerProfile.currentHealth = ActivePlayerProfile.maxHealth;
        }
        else if (selectedUpgrade.GetName() == "Missiles")
        {
            ActivePlayerProfile.upgrades[12] = true;
        }
        else if (selectedUpgrade.GetName() == "Missile Expansion I")
        {
            ActivePlayerProfile.upgrades[13] = true;
        }        
        else if (selectedUpgrade.GetName() == "Missile Expansion II")
        {
            ActivePlayerProfile.upgrades[14] = true;
        }  
        else if (selectedUpgrade.GetName() == "Missile Expansion III")
        {
            ActivePlayerProfile.upgrades[15] = true;
        }
        else if (selectedUpgrade.GetName() == "Missile Expansion IV")
        {
            ActivePlayerProfile.upgrades[16] = true;
        }
        else if (selectedUpgrade.GetName() == "Ice Missiles")
        {
            ActivePlayerProfile.upgrades[17] = true;
        }
    }

    /// <summary>A method for checking if an upgrade has been unlocked by the player or not.</summary>
    /// <param name="upgradeName">The string representing the upgrade name to check against.</param>
    /// <returns>True if the upgrade has been unlocked, else false.</returns>
    public bool CheckUpgradeUnlocked(string upgradeName)
    {
        if (upgradeName == "Dual Lasers")
        {
            return UpgradeUnlocked(0);
        }
        else if (upgradeName == "Triple Lasers")
        {
            return UpgradeUnlocked(1);
        }
        else if (upgradeName == "Tail Laser")
        {
            return UpgradeUnlocked(2);
        }
        else if (upgradeName == "Shields I")
        {
            return UpgradeUnlocked(3);
        }
        else if (upgradeName == "Shields II")
        {
            return UpgradeUnlocked(4);
        }
        else if (upgradeName == "Shields III")
        {
            return UpgradeUnlocked(5);
        }
        else if (upgradeName == "HP+ I")
        {
            return UpgradeUnlocked(6);
        }
        else if (upgradeName == "HP+ II")
        {
            return UpgradeUnlocked(7);
        }
        else if (upgradeName == "HP+ III")
        {
            return UpgradeUnlocked(8);
        }
        else if (upgradeName == "HP+ IV")
        {
            return UpgradeUnlocked(9);
        }
        else if (upgradeName == "HP+ V")
        {
            return UpgradeUnlocked(10);
        }
        else if (upgradeName == "HP+ VI")
        {
            return UpgradeUnlocked(11);
        }
        else if (upgradeName == "Missiles")
        {
            return UpgradeUnlocked(12);
        }
        else if (upgradeName == "Missile Expansion I")
        {
            return UpgradeUnlocked(13);
        }
        else if (upgradeName == "Missile Expansion II")
        {
            return UpgradeUnlocked(14);
        }
        else if (upgradeName == "Missile Expansion III")
        {
            return UpgradeUnlocked(15);
        }
        else if (upgradeName == "Missile Expansion IV")
        {
            return UpgradeUnlocked(16);
        }
        else if (upgradeName == "Ice Missiles")
        {
            return UpgradeUnlocked(17);
        }
        else
        {
            return false;
        }
        

        bool UpgradeUnlocked(int slot)
        {
            if (ActivePlayerProfile.upgrades[slot] == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }


}
