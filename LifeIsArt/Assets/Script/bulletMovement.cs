using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletMovement : MonoBehaviour {

    private Vector3 _PlayerDirection;
    private GameObject _Target;
    Vector3 direction = Vector3.zero;
    protected float _Speed = 5;

    // Use this for initialization
    void Start () {
        _Target = GameObject.FindGameObjectWithTag("Player");
        direction = new Vector3(transform.position.x - _Target.transform.position.x, transform.position.y - _Target.transform.position.y);
        _PlayerDirection = Vector3.Normalize(direction);
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x - (_PlayerDirection.x * _Speed), transform.position.y - (_PlayerDirection.y * _Speed), 0.0f);
    }
}
