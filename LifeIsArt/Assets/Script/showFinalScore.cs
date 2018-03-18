using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showFinalScore : showTime {

    Text textx;
    public int scoreLast = 0;
    // Use this for initialization
    void start() {
       // scoreLast = base.time;
        Debug.Log("heyyy" + base.time);
        textx.text = "SCORE\n";
    }
	
	// Update is called once per frame
	void Update () {
  
    }
}
