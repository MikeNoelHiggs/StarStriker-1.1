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

public class NewGame : MonoBehaviour
{
    [SerializeField] public string[] characters = { "Greg", "Linda" };
    [SerializeField] public string chosenCharacter = "Greg";
    [SerializeField] private Sprite[] characterImages;
    [SerializeField] private Ship[] ships;
    [SerializeField] public Ship chosenShip;
    [SerializeField] public Image chosenCharacterImage;
    [SerializeField] public Image chosenShipImage;
    [SerializeField] private TextMeshProUGUI characterPanalStatsText;
    [SerializeField] private TextMeshProUGUI chosenCharacterText;
    [SerializeField] private TextMeshProUGUI chosenShipText;
    [SerializeField] private TextMeshProUGUI currentCharacterText;
    [SerializeField] private Text shipStatsText;
    [SerializeField] private Text storyText;
    [SerializeField] private GameObject characterPanel;

    void Start()
    {
        chosenCharacter = characters[0];
        chosenCharacterImage.sprite = characterImages[0];
        chosenCharacterText.text = characters[0] + " Starstriker";
        currentCharacterText.text = chosenCharacterText.text;
        characterPanel.SetActive(true);
        chosenCharacter = chosenCharacterText.text;
        SetStoryText();
    }


    public void ChangeCharacter(int characterID)
    {
        chosenCharacterImage.sprite = characterImages[characterID];
        chosenCharacterText.text = characters[characterID] + " Starstriker";
        currentCharacterText.text = chosenCharacterText.text;
        chosenCharacter = chosenCharacterText.text;
        SetStoryText();
    }

   /* public void ChangeCharacterToLinda()
    {
        chosenCharacterImage.sprite = characterImages[1];
        chosenCharacterText.text = characters[1] + " Starstriker";
        currentCharacterText.text = chosenCharacterText.text;
        chosenCharacter = chosenCharacterText.text;
        SetStoryText();
    }*/


    public void UpdateShipStats(Ship ship)
    {
        shipStatsText.text = ship.GetShipName() + "\n" + ship.GetShipMaxHealth() + "/" + ship.GetShipMaxHealth() + "\n" + ship.GetShipFirePower() + "\n" + ship.GetShipSpeed();
    }


    public void CreateActiveProfile()
    {
        ActivePlayerProfile.shipName = GetChosenShip().GetShipName();
        ActivePlayerProfile.shipSprite = GetChosenShip().GetShipSprite();
        ActivePlayerProfile.level = 1;
        ActivePlayerProfile.experience = 0;
        ActivePlayerProfile.maxHealth = ActivePlayerProfile.currentHealth = chosenShip.GetShipMaxHealth();
        ActivePlayerProfile.firePower = chosenShip.GetShipFirePower();
        ActivePlayerProfile.speed = chosenShip.GetShipSpeed();
        ActivePlayerProfile.chosenCharacter = GetChosenCharacterName();
        ActivePlayerProfile.upgradeTokens = 0;
        ActivePlayerProfile.upgrades = new bool[18] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        ActivePlayerProfile.stages = new bool[6] { true, false, false, false, false, false };
        ActivePlayerProfile.stageInfo = new Stage[6];
        ActivePlayerProfile.stageInfo[0] = new Stage(1, 0, new bool[3] { false, false, false });
        ActivePlayerProfile.stageInfo[1] = new Stage(2, 0, new bool[3] { false, false, false });
        ActivePlayerProfile.stageInfo[2] = new Stage(3, 0, new bool[3] { false, false, false });
        ActivePlayerProfile.stageInfo[3] = new Stage(4, 0, new bool[3] { false, false, false });
        ActivePlayerProfile.stageInfo[4] = new Stage(5, 0, new bool[3] { false, false, false });
        ActivePlayerProfile.stageInfo[5] = new Stage(6, 0, new bool[3] { false, false, false });
    }

    public void SetPlayerShip(Ship ship)
    {
        chosenShip = ship;
        chosenShipImage.sprite = ship.GetShipSprite();
        chosenShipText.text = ship.GetShipName();
        characterPanalStatsText.text = "1\n" + ship.GetShipMaxHealth() + "/" + ship.GetShipMaxHealth() + "\n" + ship.GetShipFirePower() + "\n" + ship.GetShipSpeed() + "\n0/18" + "\n0";
    }


    public Image GetChosenCharacterImage()
    {
        return this.chosenCharacterImage;
    }

    public string GetChosenCharacterName()
    {
        return this.chosenCharacterText.text;
    }

    public Ship GetChosenShip()
    {
        return this.chosenShip;
    }

    public void SetStoryText()
    {
        string spouse;
        string pronoun;

        if (chosenCharacter == "Linda Starstriker")
        {
            spouse = "husband";
            pronoun = "her";
        }
        else
        {
            spouse = "wife";
            pronoun = "him";
        }

        storyText.text = "The year is 2166... The invasive race known as the liberators  is attempting to invade earth again. With the O.D.G (Orbital Defence Grid) down to its last member - " 
            + chosenCharacter + ". \n\nIt's is up to " + pronoun +  " to protect earth from the waves of invaders. Their " + spouse + " has been captured and is on board the liberator " +
            "mother ship, with one of the legendary O.D.G StarStriker ships can " + chosenCharacter + " protect earth and rescue their love?";
    }

}