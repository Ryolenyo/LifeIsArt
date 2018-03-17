using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
  [SerializeField]
  private float _MaxDistance;

  private readonly float _DefaultSpeed = 0.5f;
  private readonly float _DashSpeed = 2.0f;
  private readonly float _DashCooldown = 2.0f;
  private readonly float _TackleCooldown = 2.0f;
  private readonly float _TackleChargeTime = 1.0f;
  private readonly float _TackleChargeRange = 1.0f;
  private readonly float _TackleAttackRange = 2.0f;
  private readonly float _TackleAttackSpeed = 1.5f;

  private Vector3 _Target;
  private bool _IsNormalMove = true;
  private bool _UsingLerpMove = false;
  private float _CountDashCooldown = 0.0f;
  private float _CountTackleCooldown = 0.0f;

  void Start()
  {
    _CountDashCooldown = _DashCooldown;
  }

  void FixedUpdate()
  {
    UpdatePosition();
    UpdateRotation();
    CheckAction();
  }

  private void UpdateRotation()
  {
    LookAtMouse();
  }

  private void UpdatePosition()
  {
    _Target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    _Target.z = transform.position.z;

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

    MovingToward(_Target);
  }

  private void LookAtMouse()
  {
    Vector3 direction = new Vector3(transform.position.x - _Target.x, transform.position.y - _Target.y);
    transform.up = direction;
  }

  private void MovingToward(Vector3 target)
  {
    transform.position = Vector3.MoveTowards(transform.position, target, _MaxDistance);
  }

  private void CheckAction()
  {
    if (_CountTackleCooldown < _TackleCooldown + 0.1f)
    {
      _CountTackleCooldown += Time.deltaTime;
    }

    if (_CountDashCooldown < _DashCooldown + 0.1f)
    {
      _CountDashCooldown += Time.deltaTime;
    }

    if (Input.GetKeyDown(KeyCode.LeftShift))
    {
      UsingBuffSpeed();
    }

    if (Input.GetKeyDown(KeyCode.Space))
    {
      UsingTackle();
    }
  }

  private void UsingBuffSpeed()
  {
    if (_CountDashCooldown > _DashCooldown)
    {
      StartCoroutine("BuffSpeed");
      _CountDashCooldown = 0.0f;
    }
  }

  private void UsingTackle()
  {
    if (_CountTackleCooldown > _TackleCooldown)
    {
      _IsNormalMove = false;
      StartCoroutine("Tackle");
      _CountTackleCooldown = 0.0f;
    }
  }

  IEnumerator BuffSpeed()
  {
    _MaxDistance = _DashSpeed;
    GetComponent<ShadowTrail>().isShadowTrailActive = true;
    yield return new WaitForSeconds(0.2f);
    GetComponent<ShadowTrail>().isShadowTrailActive = false;
    _MaxDistance = _DefaultSpeed;
  }

  IEnumerator Tackle()
  {

    yield return new WaitForSeconds(0.5f);
  }
}
