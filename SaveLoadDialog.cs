using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadDialog : MonoBehaviour
{

    [SerializeField] private GameObject empty;
    [SerializeField] private GameObject saveGameShip;
    [SerializeField] private Image saveGameSprite;
    [SerializeField] private Ship[] ships;
    [SerializeField] private Text saveInfoText;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI saveOverwriteText;
    [SerializeField] private PlayerProfile profile;
    [SerializeField] private int saveSlot = 1;
    [SerializeField] private Enum state;
    [SerializeField] private SceneLoader sceneLoader;

    enum State
    {
        Saving,
        Loading
    }



    public void GetSaveInfo(int slot)
    {
        if (slot == 1)
        {
            try
            {
                PlayerProfile profile = SaveSystem.LoadFromSlotOne();
                Load(profile);
            }

            catch (Exception e)
            {
                if (e is FileNotFoundException || e is SerializationException)
                {
                    PlayerProfile profile = null;
                    Load(profile);
                }
            }
        }

        else if (slot == 2)
        {
            try
            {
                PlayerProfile profile = SaveSystem.LoadFromSlotTwo();
                Load(profile);
            }

            catch (Exception e)
            {
                if (e is FileNotFoundException || e is SerializationException)
                {
                    PlayerProfile profile = null;
                    Load(profile);
                }
            }
        }

        else if (slot == 3)
        {
            try
            {
                PlayerProfile profile = SaveSystem.LoadFromSlotThree();
                Load(profile);
            }
            catch (Exception e)
            {
                if (e is FileNotFoundException || e is SerializationException)
                {
                    PlayerProfile profile = null;
                    Load(profile);
                }
            }
        }

        else
        {
            Debug.LogError("Invalid Save Slot");
        }

        void Load(PlayerProfile profile)
        {
            if (profile != null)
            {
                empty.SetActive(false);
                saveGameShip.SetActive(true);

                string text = "Character: " + profile.chosenCharacter + "\nShip: " + profile.shipName +
                "\nLevel: " + profile.level + "\nUpgrades: " + profile.upgrades.Where(c => c == true).Count<bool>() + "/" + profile.upgrades.Length +
                "\nStages: " + profile.stages.Where(c => c == true).Count<bool>() + "/" + profile.stages.Length;

                Ship ship = ships[0];

                foreach (Ship s in ships)
                {
                    if (s.shipName == profile.shipName)
                    {
                        ship = s;
                    }
                }

                saveGameSprite.sprite = ship.GetShipSprite();

                saveInfoText.text = text;
            }

            else
            {
                saveInfoText.text = "";
                saveGameShip.SetActive(false);
                empty.SetActive(true);
            }
        }
    }

    public void CreateNewSaveProfile()
    {
        profile.shipName = ActivePlayerProfile.shipName;
        profile.level = 1;
        profile.experience = 0;
        profile.maxHealth = profile.currentHealth = ActivePlayerProfile.maxHealth;
        profile.firePower = ActivePlayerProfile.firePower;
        profile.speed = ActivePlayerProfile.speed;
        profile.chosenCharacter = ActivePlayerProfile.chosenCharacter;
        profile.upgrades = new bool[18] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        profile.stages = new bool[6] { true, false, false, false, false, false };
        profile.upgradeTokens = 0;
        profile.stageInfo = ActivePlayerProfile.stageInfo;
        ActivePlayerProfile.saveSlot = saveSlot;
        SaveProfile(saveSlot);
    }

    public void CreateSaveProfile()
    {
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
        ActivePlayerProfile.saveSlot = saveSlot;
        SaveProfile(saveSlot); 
    }


    public void LoadSaveProfile()
    {
        if (state is State.Loading)
        {
            SaveProfile(saveSlot);
        }
    }

    public void SaveProfile(int slot)
    {
        if (state is State.Saving)
        {
            if (slot == 1)
            {
                SaveSystem.SaveToSlotOne(profile);
            }
            else if (slot == 2)
            {
                SaveSystem.SaveToSlotTwo(profile);
            }
            else if (slot == 3)
            {
                SaveSystem.SaveToSlotThree(profile);
            }
        }
        else if (state is State.Loading)
        {
            if (slot == 1 && saveInfoText.text != "")
            {
                profile = SaveSystem.LoadFromSlotOne();
                LoadToActiveProfile();
                sceneLoader.LoadHubScene();
            }
            else if (slot == 2 && saveInfoText.text != "")
            {
                profile = SaveSystem.LoadFromSlotTwo();
                LoadToActiveProfile();
                sceneLoader.LoadHubScene();
            }
            else if (slot == 3 && saveInfoText.text != "")
            {
                profile = SaveSystem.LoadFromSlotThree();
                LoadToActiveProfile();
                sceneLoader.LoadHubScene();
            }
            if (saveInfoText.text == "")
            {
                saveOverwriteText.text = "Cannot load. No Save data.";
            }
        }
        

        void LoadToActiveProfile()
        {

            foreach (Ship s in ships)
            {
                if (s.shipName == profile.shipName)
                {
                    ActivePlayerProfile.shipSprite = s.GetShipSprite();
                }
            }

            ActivePlayerProfile.shipName = profile.shipName;
            ActivePlayerProfile.level = profile.level;
            ActivePlayerProfile.experience = profile.experience;
            ActivePlayerProfile.maxHealth = ActivePlayerProfile.currentHealth = profile.maxHealth;
            ActivePlayerProfile.firePower = profile.firePower;
            ActivePlayerProfile.speed = profile.speed;
            ActivePlayerProfile.chosenCharacter = profile.chosenCharacter;
            ActivePlayerProfile.upgradeTokens = profile.upgradeTokens;
            ActivePlayerProfile.upgrades = profile.upgrades;
            ActivePlayerProfile.stages = profile.stages;
            ActivePlayerProfile.stageInfo = profile.stageInfo;
            ActivePlayerProfile.saveSlot = slot;
        }
    }

    public void ChangeSaveSlot(int slot)
    {
        saveSlot = slot;
        if (state is State.Saving)
        {
            saveOverwriteText.text = "Are you sure you want to overwrite slot " + slot + "?";
        }
        else if (state is State.Loading)
        {
            saveOverwriteText.text = "Are you sure you want to load slot " + slot + "?";
        }
    }

    public void LoadState()
    {
        this.state = State.Loading;
        titleText.text = "Load Save";
    }

    public void SaveState()
    {
        this.state = State.Saving;
        titleText.text = "Overwrite Save";
    }
}



