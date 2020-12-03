using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonController : MonoBehaviour
{
    public int index = 0;
    public AudioSource buttonSounds;

    [SerializeField] int maxIndex;
    [SerializeField] string scene;
    [SerializeField] LevelLoader levelLoader;
    [SerializeField] Animator musicTransition;

    void Start()
    {
        PlayerInputs.instance.UpdateControllerMap("Menu");
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerInputs.instance.menuUp)
            index--;
        else if (PlayerInputs.instance.menuDown)
            index++;

        if (index > maxIndex)
            index = 0;
        else if (index < 0)
            index = maxIndex;

        if (PlayerInputs.instance.menuSelect)
            switch (index)
            {
                case 0:
                    levelLoader.LoadNextLevel();
                    musicTransition.SetTrigger("Fade");
                    return;
                case 1:
                    return;
                case 2:
                    return;
                case 3:
                    Application.Quit();
                    return;
            }
    }
}
