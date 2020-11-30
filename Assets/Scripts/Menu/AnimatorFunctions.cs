using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorFunctions : MonoBehaviour
{
    [SerializeField] MenuButtonController menuButtonController;
    public bool disableOnce;

    void PlaySound(AudioClip sound)
    {
        if (!disableOnce)
            menuButtonController.buttonSounds.PlayOneShot(sound);
        else
            disableOnce = false;
    }
}
