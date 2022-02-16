using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


/// <summary>
/// Score Recorder is passed to the next scene and will display how the player performed.
/// </summary>
public class ScoreRecorder : MonoBehaviour
{
    public Text displayScore;
    public int score;
    public int level;


    public void SetScore(Player player)
    {
        score = player.GetScore();
        level = player.GetLevel();
    }

    private void Update()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        
        if (scene == 9)
        {
           GameObject go = GameObject.FindGameObjectWithTag("Score Text");
           displayScore = go.GetComponent<Text>();
           displayScore.text = "You Scored: " + score + "!" + "\nAnd reached level: " + level + "!";
            if (score > ActivePlayerProfile.stageInfo[ActivePlayerProfile.selectedStage - 1].highscore)
            {
                ActivePlayerProfile.stageInfo[ActivePlayerProfile.selectedStage - 1].highscore = score;
            }
            
            PlayerProfile profile = SaveSystem.LoadFromSlotTwo();

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
    }

    public int GetScore()
    {
        return this.score;
    }
    public int GetLevel()
    {
        return this.level;
    }
    
}