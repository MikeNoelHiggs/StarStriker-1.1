using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    public EventHandler OnExperienceChanged;
    public EventHandler OnLevelChanged;

    private int level;
    private int experience;
    private int experienceToNextLevel;

    public LevelSystem()
    {
        level = 1;
        experience = 0;
        experienceToNextLevel = 100;
    }

    public LevelSystem(int level, int experience)
    {
        this.level = level;
        this.experience = experience;
        this.experienceToNextLevel = GetExperienceToNextLevel(level);
    }

    public void AddExperiance(int amount)
    {
        if (!IsMaxLevel())
        {
            experience += amount;
            if (experience >= experienceToNextLevel)
            {
                // Level Up
                while (!IsMaxLevel() && experience >= GetExperienceToNextLevel(level))
                {
                    level++;
                    experience -= experienceToNextLevel;
                    experienceToNextLevel = GetExperienceToNextLevel(level);
                    if (OnLevelChanged != null) OnLevelChanged(this, EventArgs.Empty);
                }
            }

            if (OnExperienceChanged != null) OnExperienceChanged(this, EventArgs.Empty);
        }
    }

    public int GetLevel()
    {
        return level;
    }

    public void SetLevel(int level, int exp)
    {
        if (level > 0 && level <= 100)
        {
            this.level = level;
            this.experience = exp;
            experienceToNextLevel = GetExperienceToNextLevel(level);
        }
        else
        {
            this.level = 1;
        }
    }

    public int GetCurrentExperience()
    {
        return experience;
    }


    public int GetExperienceToNextLevel(int level)
    {
        if (level < 100)
        {
            return (int)Mathf.Floor(100 * level * Mathf.Pow(level, 0.5f));
        }
        else
        {
            return 0;
        }
    }

    public float GetExperienceNormalized()
    {
        if (!IsMaxLevel())
        {
            return (float)experience / experienceToNextLevel;
        }
        else
        {
            return 1f;
        }
    }

    public bool IsMaxLevel()
    {
       return IsMaxLevel(level);
    }

    public bool IsMaxLevel(int level)
    {
        return level == 100;
    }
}
