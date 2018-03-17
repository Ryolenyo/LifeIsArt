using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class loadSceneplay : DestroyPlayer {

    public Button nextButton;
    public float time;
    // Use this for initialization

    void Start () {
        Scene sceneLoaded = SceneManager.GetActiveScene();
        if (sceneLoaded.name != "Play" && sceneLoaded.name != "GameOver" && sceneLoaded.name != "Prototype")
        {
            nextButton.onClick.AddListener(TaskOnClick);
        }
    }

   //  Update is called once per frame
    private void FixedUpdate()
    {
        if (!base.die)
        {
            time += Time.deltaTime;
        }
        if(time > 2.0f ||  base.die)
        {
            Scene sceneLoaded = SceneManager.GetActiveScene();
            SceneManager.LoadScene(sceneLoaded.buildIndex + 1);
        }
    }

    void TaskOnClick () {
        Scene sceneLoaded = SceneManager.GetActiveScene();
        SceneManager.LoadScene(sceneLoaded.buildIndex + 1);
    }
}
