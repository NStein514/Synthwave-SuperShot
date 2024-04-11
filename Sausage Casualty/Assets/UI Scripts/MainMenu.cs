using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject controlsMenu;
    public AudioSource menuTheme;
    public AudioSource battleTheme;
    // Start is called before the first frame update
    void Start()
    {
        //MAKE SURE THERE IS THE EVENT MANAGER IN THE HIERARCHY
        //it should just be click and drag and it works
        
        var currentScene = SceneManager.GetActiveScene();
        Debug.Log("Open scene: " + currentScene.name);
        if(currentScene.name == "UI test")
        {
            Debug.Log("Title is open");
            mainMenu.SetActive(true);
            controlsMenu.SetActive(false);
        }
        else
        {
            MainMenuButton();
            mainMenu.SetActive(false);
            controlsMenu.SetActive(false);
        }

        //so the biggest problem is that when the starting scene is UI Test, switching to GreyBox makes the lighting get fucked
        //but starting in GreyBox, switching between scenes is just fine, so what gives?
        //go into lighting settings and change them (screenshots are in discord now) and now it looks good
    } 

    private void Update()
    {
        //menu is off by default, pressing tab opens/closes it
        //.active is obsolete but still works so fuck you i guess
        //PLUS this is where the game has to pause
        //now i could make it so that pressing tab changes to a UI scene
        //or i could keep UI as a canvas and tell the scene to pause 
        //is that a thing? 
        //hol up i found something called Time.timeScale = 0 which supposedly pauses a scene
        //UMM I MIGHT'VE FOUND THE WAY TO DO BULLET/DRAGON/WHATEVER-THE-FUCK TIME HOLY SHIT
        //YEAH I DID NO FUCKING WAY
        var currentScene = SceneManager.GetActiveScene();
        
            if (Input.GetKeyDown(KeyCode.Tab) && mainMenu.active == false && currentScene.name  == "GreyBox")
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                mainMenu.SetActive(true);
                controlsMenu.SetActive(false);
                Time.timeScale = 0.0f; //pause
                //this is probably where messing with music would happen (pause it or play an options song?)
            }
            else if (Input.GetKeyDown(KeyCode.Tab) && mainMenu.active == true && currentScene.name == "GreyBox")
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                mainMenu.SetActive(false);
                controlsMenu.SetActive(false);
                Time.timeScale = 1; //resume
            }
            /*else if (currentScene.name == "UI test")
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                mainMenu.SetActive(true);
            }*/
            //because it looks for scene name tab will only work when it's all true (so tabbing is not allowed in title screen)
    }

    public void StartButton()
    {
        //checks which scene is active and then switches to the other one
        var currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "GreyBox")
        {
            //UnityEngine.SceneManagement.SceneManager.LoadScene("UI test");
            //MAKE SURE IN BUILD SETTINGS THE SCENE IS IN THE LIST

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            mainMenu.SetActive(false);
            controlsMenu.SetActive(false);
            Time.timeScale = 1; //resume
            //if it's the game, resume game

        }
        else if(currentScene.name == "UI test")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GreyBox");
            Time.timeScale = 1.0f;
            //if it's the title screen, go to game scene and begin
        }

    }

    public void ControlsButton()
    {
        mainMenu.SetActive(false);
        controlsMenu.SetActive(true);
    }

    public void MainMenuButton()
    {
        mainMenu.SetActive(true);
        controlsMenu.SetActive(false);
    }

    public void QuitButton()
    {
        var currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "GreyBox")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("UI test");
            MainMenuButton();
            //MAKE SURE IN BUILD SETTINGS THE SCENE IS IN THE LIST
            //if it's the game, go to title screen
        }
        else if (currentScene.name == "UI test")
        {
            Application.Quit();
            //if it's the title scene, end application
        }
    }
}
