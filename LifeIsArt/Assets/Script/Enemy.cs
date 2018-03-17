using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseEnemy {

  protected override void OnMove()
  {
    base.OnMove();
  }

  protected override void OnUpdate()
  {
    base.OnUpdate();
  }

  internal void SetSpeed(float speed)
  {
    _Speed = speed;
  }

  internal void SetScale(float scale)
  {
    _Scale = scale;
  }
  /*void OnTriggerEnter(Collider other)
  {
    if (other.tag == "Player")
    {
       Destroy(other.gameObject);
    }
  }*/
}
