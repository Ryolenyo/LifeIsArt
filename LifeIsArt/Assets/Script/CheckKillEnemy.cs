using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckKillEnemy : MonoBehaviour {

  void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.CompareTag("E1") || other.gameObject.CompareTag("E2") || other.gameObject.CompareTag("E3") || other.gameObject.CompareTag("E4"))
    {
      Destroy(other.gameObject);
    }
  }
}
