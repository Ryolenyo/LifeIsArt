using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCollectorController : MonoBehaviour {

  public static void AddStat(string action)
  {
    Debug.Log("Stat was added. --> " + action);
    Debug.Log(PlayerPrefs.GetInt(action, 0));
    PlayerPrefs.SetInt(action, PlayerPrefs.GetInt(action, 0) + 1);
    Debug.Log(PlayerPrefs.GetInt(action, 0));

  }
}
