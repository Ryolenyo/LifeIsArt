using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour {

  [SerializeField]
  protected float _Speed;

  private GameObject _Target;
  private Vector3 _PlayerDirection;

  void Awake()
  {
    _Target = GameObject.FindGameObjectWithTag("Player");
    Vector3 player = _Target.transform.position;
    Vector3 direction = new Vector3(transform.position.x - _Target.transform.position.x, transform.position.y - _Target.transform.position.y);
    transform.up = direction;
    _PlayerDirection = Vector3.Normalize(direction);
  }

  void FixedUpdate()
  {
    OnMove();
  }

  protected virtual void OnMove()
  {
    MovingToPlayerPosition();
  }

  private void MovingToPlayerPosition()
  {
    transform.position = new Vector3(transform.position.x - (_PlayerDirection.x * _Speed), transform.position.y - (_PlayerDirection.y * _Speed), 0.0f);
  }
}
