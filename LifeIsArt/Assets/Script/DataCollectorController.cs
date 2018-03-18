using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCollectorController : MonoBehaviour {

  public void AddStat(string action)
  {
    PlayerPrefs.SetInt(action, PlayerPrefs.GetInt(action, 0) + 1);
  }
}
