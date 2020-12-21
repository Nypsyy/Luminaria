using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void Continue()
    {
        PlayerInputs.instance.UpdateControllerMap("World Exploration");
        Time.timeScale = 1;
    }

    public void Quit()
    {
        Debug.Log("Quitting...");
        Application.Quit();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
