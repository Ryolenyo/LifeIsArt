using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Quit : MonoBehaviour {
    public Button quitButton;
    // Use this for initialization
    void Start () {
        quitButton.onClick.AddListener(TaskOnClick);
       
    }
	
	// Update is called once per frame
	void TaskOnClick() {
        Debug.Log("Quit");
        Application.Quit();
    }
}
