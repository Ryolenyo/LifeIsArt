using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showTime : DestroyPlayer {

    Text text;
    public int time = 0;
    float timef;
    // Use this for initialization
    void Awake () {
        text = GetComponent<Text>();


	}
	
	// Update is called once per frame
	void Update () {
        if(!base.die)
            timef += Time.deltaTime;
        time = Mathf.RoundToInt(timef);
        text.text = "SCORE\n" + time;
        Debug.Log("hey" + time);
    }

    public void SetLastestScore()
  {
    PlayerPrefs.SetInt("Score", time);
  }
}
