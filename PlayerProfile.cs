using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable] 
public class PlayerProfile 
{
    public string chosenCharacter = "";
    public string shipName = "";
    public int upgradeTokens = 0;
    public int level = 1;
    public int experience = 0;
    public int maxHealth = 1;
    public int currentHealth = 1;
    public int firePower = 1;
    public float speed = 1;
    public bool[] upgrades = new bool[16];
    public bool[] stages = new bool [6];
    public Stage[] stageInfo;

    public PlayerProfile(PlayerProfile profile)    
    {
        this.level = profile.level;
        this.chosenCharacter = profile.chosenCharacter;
        this.upgradeTokens = profile.upgradeTokens;
        this.maxHealth = profile.maxHealth;
        this.currentHealth = profile.currentHealth;
        this.firePower = profile.firePower;
        this.speed = profile.speed;
        this.upgrades = profile.upgrades;
        this.stages = profile.stages;
    }
}
