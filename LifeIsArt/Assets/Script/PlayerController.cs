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
  private bool _IsTackle = false;
  private bool _UsingLerpMove = false;
  private float _CountDashCooldown = 0.0f;
  private float _CountTackleCooldown = 0.0f;

  void Start()
  {
    _CountDashCooldown = _DashCooldown;
  }

  void FixedUpdate()
  {
    CheckAction();
    UpdatePosition();
    UpdateRotation();
  }

  private void UpdateRotation()
  {
    LookAtMouse();
  }

  private void UpdatePosition()
  {
    _Target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    _Target.z = transform.position.z;

    MovingToward(_Target);
  }

  private void LookAtMouse()
  {
    Vector3 direction = new Vector3(transform.position.x - _Target.x, transform.position.y - _Target.y);
    transform.up = direction;
  }

  private void MovingToward(Vector3 target)
  {
    if (_IsNormalMove) transform.position = Vector3.MoveTowards(transform.position, target, _MaxDistance);
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
    Debug.Log("Tackling !!");
    StartCoroutine("TackleReady");
    yield return null;
  }

  IEnumerator TackleReady()
  {
    Debug.Log("Tackle ready.");
    yield return new WaitForSeconds(0.3f);
    StartCoroutine("TackleCharge");
  }

  IEnumerator TackleCharge()
  {
    Debug.Log("Tackle charging.");
    yield return new WaitForSeconds(0.2f);
    _IsTackle = true;
    StartCoroutine("TackleAttack");
  }

  IEnumerator TackleAttack()
  {
    Debug.Log("Tackle attacking.");
    yield return new WaitForSeconds(0.2f);
    _IsNormalMove = true;
    _IsTackle = false;
  }
}
