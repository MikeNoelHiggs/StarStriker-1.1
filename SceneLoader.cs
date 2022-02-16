using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary> Class SceneLoader Manages the Scenes in the applciation using an index. </summary>
public class SceneLoader : MonoBehaviour
{

    /// <summary>LoadNextScene Set the current scene as the active one, then load the next scene in the index. </summary>
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    /// <summary> Load the first scene in the index (position 0). </summary>
    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
    }


    public void LoadHubScene()
    {
        SceneManager.LoadScene(2);
    }

    /// <summary>Loads the first level of the game. </summary>
    public void LoadLevel()
    {
        SceneManager.LoadScene(ActivePlayerProfile.selectedStage + 2);
    }

    /// <summary> Method<c> Quit the application </c> </summary>
    public static void Quit()
    {
        Application.Quit();
    }


    public static void GameOver()
    {
        SceneManager.LoadScene(9);
    }
}
