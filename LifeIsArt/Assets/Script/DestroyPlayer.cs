﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlayer : MonoBehaviour {

    public bool die = false;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag.Equals("Player"))
        {
            Destroy(other.gameObject);
            die = true;
        }
    }

}