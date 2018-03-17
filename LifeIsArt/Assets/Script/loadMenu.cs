using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class loadMenu: MonoBehaviour
{

    public Button nextButton;
    // Use this for initialization
    void Start()
    {
        Scene sceneLoaded = SceneManager.GetActiveScene();
        nextButton.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        Scene sceneLoaded = SceneManager.GetActiveScene();
        SceneManager.LoadScene(sceneLoaded.buildIndex - 4);
    }
}
