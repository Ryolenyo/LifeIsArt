using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

  [SerializeField]
  private float _Speed;
  [SerializeField]
  private float _MaxDistance;


  private Vector3 _Target;
  private bool _IsNormalMove = true;
  private bool _UsingLerpMove = false;

  void FixedUpdate()
  {
    UpdatePosition();
    UpdateRotation();
  }

  private void UpdateRotation()
  {
    LookAtMouse();
  }

  private void LookAtMouse()
  {
    Vector3 direction = new Vector3(transform.position.x - _Target.x, transform.position.y - _Target.y);
    transform.up = direction;
  }

  private void UpdatePosition()
  {
    _Target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    _Target.z = transform.position.z;
    //LerpMovingTo(_Target);

    //for change mode of moving
    if (Input.GetKeyDown("z"))
    {
      if (!_UsingLerpMove)
      {
        _UsingLerpMove = true;
      }
      else
      {
        _UsingLerpMove = false;
      }
    }

    if (_UsingLerpMove)
    {
      LerpMovingTo(_Target);
    }
    else
    {
      MovingToward(_Target);
    }
  }

  private void LerpMovingTo(Vector3 target)
  {
    transform.position = Vector3.Lerp(transform.position, target, _Speed);
  }

  private void MovingToward(Vector3 target)
  {
    transform.position = Vector3.MoveTowards(transform.position, target, _MaxDistance);
  }
}
