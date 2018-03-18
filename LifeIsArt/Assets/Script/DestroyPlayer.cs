using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlayer : MonoBehaviour {

    public bool die = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
          other.gameObject.GetComponent<PlayerController>().OnDestroy();
        }

        Debug.Log("state : " + die);
    }

}
