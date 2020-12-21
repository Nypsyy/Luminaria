using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer mainMixer;
    public Slider slider;

    public void SetVolume(float value)
    {
        mainMixer.SetFloat("MasterVolume", value * 6);
    }
}
