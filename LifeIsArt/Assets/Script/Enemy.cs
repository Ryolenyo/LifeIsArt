using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseEnemy {

  protected override void OnMove()
  {
    base.OnMove();
  }

  internal void SetSpeed(float speed)
  {
    _Speed = speed;
  }

  internal void SetScale(float scale)
  {
    _Scale = scale;
  }
}
