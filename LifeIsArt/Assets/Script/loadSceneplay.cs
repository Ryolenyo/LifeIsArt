using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class loadSceneplay : MonoBehaviour {

    public Button nextButton;
    public float time;
    // Use this for initialization

    void Start () {
        Scene sceneLoaded = SceneManager.GetActiveScene();
        if (sceneLoaded.name == "Start")
        {
            nextButton.onClick.AddListener(TaskOnClick);
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        time += Time.deltaTime;
        if(time > 2.0f)
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
