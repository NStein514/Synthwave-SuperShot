using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class StartButtonText : MonoBehaviour
{
    // Start is called before the first frame update
    public Button button;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "GreyBox")
        {
            button.GetComponent<TextMeshPro>().text = "Resume";
            

        }
        else if (currentScene.name == "UI test")
        {
            button.GetComponent<TextMeshPro>().text = "Start";
        }
    }
}
