using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class StartMenu : MonoBehaviour
{
    GameObject startButton; 
    GameObject exitButton; 
    bool startSelected = true;

    // Start is called before the first frame update
    void Start()
    {
        startButton = GameObject.Find("StartButton");
        exitButton = GameObject.Find("ExitButton");
        
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateButtonText();
        moveCurser();
    }

    public void UpdateButtonText()
    {
        while (startSelected)
        {
            string text = startButton.GetComponent<TMPro.TextMeshPro>().text;

            text.Insert(0, "[ ");
            text += " ]";

            startButton.GetComponentInChildren<TextMeshPro>().text = text;
        }

        while (startSelected)
        {
            string text = exitButton.GetComponent<TMPro.TextMeshPro>().text;

            text.Insert(0, "[ ");
            text += " ]";

            exitButton.GetComponent<TMPro.TextMeshPro>().text = text;
        }
    }

    public void moveCurser()
    {
        if (startSelected)
        {
            if (Input.GetKeyDown("down") || Input.GetKeyDown("up"))
            {
                startSelected = false;
            }
            if (Input.GetKeyDown("up") || Input.GetKeyDown("down"))
            {
                startSelected = false;
            }
        }
        else
        {
            if (Input.GetKeyDown("down") || Input.GetKeyDown("up"))
            {
                startSelected = true;
            }
            if (Input.GetKeyDown("up") || Input.GetKeyDown("down"))
            {
                startSelected = true;
            }
        }
    }
}
