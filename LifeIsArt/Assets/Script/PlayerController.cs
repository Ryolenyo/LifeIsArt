using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour {
  [SerializeField]
  private float _MaxDistance;
  [SerializeField]
  private CinemachineVirtualCamera _VCam;
  [SerializeField]
  private Transform _MouseReference;
  [SerializeField]
  private Sprite[] _SpriteTackle;
  [SerializeField]
  private GameObject _SlamPower;
  private readonly float _DefaultSpeed = 0.5f;
  private readonly float _DashSpeed = 2.0f;
  private readonly float _DashCooldown = 2.0f;
  private readonly float _TackleCooldown = 2.0f;
  private readonly float _SlamCooldown = 2.0f;
  private readonly float _BlinkCooldown = 4.0f;
  private readonly float _TackleChargeTime = 1.0f;
  private readonly float _TackleChargeRange = 1.0f;
  private readonly float _TackleAttackRange = 2.0f;
  private readonly float _TackleAttackSpeed = 1.5f;
  private readonly float _SelectBlinkAreaTime = 1.0f;

  private Vector3 _Target;
  private Vector3 _TeleportTarget;
  private bool _IsNormalMove = true;
  private bool _IsTackle = false;
  private bool _IsBlinking = false;
  private bool _IsSlaming = false;
  private float _CountDashCooldown = 0.0f;
  private float _CountTackleCooldown = 0.0f;
  private float _CountBlinkCooldown = 0.0f;
  private float _CountSlamCooldown = 0.0f;
  private Animator _Animator;

  void Start()
  {
    _CountDashCooldown = _DashCooldown;
    _CountTackleCooldown = _TackleCooldown;
    _CountBlinkCooldown = _BlinkCooldown;
    _CountSlamCooldown = _SlamCooldown;
    _Animator = GetComponent<Animator>();
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

    if (_CountBlinkCooldown < _BlinkCooldown + 0.1f)
    {
      _CountBlinkCooldown += Time.deltaTime;
    }

    if (_CountSlamCooldown < _SlamCooldown + 0.1f)
    {
      _CountSlamCooldown += Time.deltaTime;
    }

    if (Input.GetKeyDown(KeyCode.LeftShift))
    {
      UsingBuffSpeed();
    }

    if (Input.GetKeyDown(KeyCode.Space))
    {
      UsingSlam();
    }

    if (Input.GetKeyDown(KeyCode.E))
    {
      UsingBlink();
    }

    if (Input.GetKeyDown(KeyCode.Mouse0) && _IsBlinking)
    {
      _TeleportTarget = new Vector3(_MouseReference.position.x, _MouseReference.position.y, transform.position.z);
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

  private void UsingBlink()
  {
    if (_CountBlinkCooldown > _BlinkCooldown)
    {
      _TeleportTarget = transform.position;
      _IsNormalMove = false;
      StartCoroutine("Blink");
      _CountBlinkCooldown = 0.0f;
    }
  }

  private void UsingSlam()
  {
    if (_CountSlamCooldown > _SlamCooldown)
    {
      _IsNormalMove = false;
      StartCoroutine("Slam");
      _CountSlamCooldown = 0.0f;
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
    //if hit something shake the camera
    yield return new WaitForSeconds(0.2f);
    _IsNormalMove = true;
    _IsTackle = false;
  }

  IEnumerator Blink()
  {
    yield return new WaitForSeconds(0.2f);
    _IsBlinking = true;
    StartCoroutine("SelectBlinkArea");
  }

  IEnumerator SelectBlinkArea()
  {
    _VCam.Follow = _MouseReference;
    yield return new WaitForSeconds(0.5f);
    transform.position = _TeleportTarget;
    _VCam.Follow = transform;
    _IsNormalMove = true;
    _IsBlinking = false;
  }

  IEnumerator Slam()
  {
    yield return new WaitForSeconds(0.1f);
    StartCoroutine("SlamColliderActive");
  }

  IEnumerator SlamColliderActive()
  {
    _Animator.SetBool("Slam", true);
    _SlamPower.SetActive(true);
    yield return new WaitForSeconds(0.5f);
    _SlamPower.SetActive(false);
    _Animator.SetBool("Slam", false);
    _IsNormalMove = true;
   }

  public void OnDestroy()
  {
    GameObject.Find("Canvas").GetComponent<loadSceneplay>().die = true;
    Destroy(gameObject);
  }
}
