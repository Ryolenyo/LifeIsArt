using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showTime : MonoBehaviour {

    Text text;
    int time;
    float timef;
    // Use this for initialization
    void Awake () {
        text = GetComponent<Text>();


	}
	
	// Update is called once per frame
	void Update () {
        timef += Time.deltaTime;
        time = Mathf.RoundToInt(timef);
        text.text = "Score: " + time;		
	}
}
