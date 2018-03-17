using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlayer : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
