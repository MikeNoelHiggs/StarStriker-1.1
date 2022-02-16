using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }

    public void SetBrightness(float brightness)
    {
        Screen.brightness = brightness;
    }

    public void SetContrast(float contrast)
    {

    }

    public void SetFPS(int frames)
    {
        Application.targetFrameRate = frames;
    }
}
