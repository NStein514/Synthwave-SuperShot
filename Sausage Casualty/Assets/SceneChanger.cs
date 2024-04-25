using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    private void Start()
    {
        var currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "GreyBox")
        {
            //Lock Cursor in Game
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

        }
        else if (currentScene.name == "UI test")
        {
            //Unlock Cursor to use menu
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void ChangeScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
