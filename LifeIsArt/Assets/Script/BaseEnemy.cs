using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour {

  [SerializeField]
  protected float _Speed;
  [SerializeField]
  protected float _Scale;

  private GameObject _Target;
  private Vector3 _PlayerDirection;

  public float min;
  public float max;

  public  float firstpox;
  public  float firstpoy;
    public int dir;



    void Awake()
  {
    _Target = GameObject.FindGameObjectWithTag("Player");
    Vector3 player = _Target.transform.position;
    Vector3 direction = new Vector3(transform.position.x - _Target.transform.position.x, transform.position.y - _Target.transform.position.y);
    transform.up = direction;
    _PlayerDirection = Vector3.Normalize(direction);
    firstpox = transform.position.x;
    firstpoy = transform.position.y;

    System.Random rand = new System.Random();
    dir = rand.Next(0, 4);

    }

  void FixedUpdate()
  {
    OnMove();
    OnScaling();
    OnUpdate();
  }

  protected virtual void OnUpdate()
  {
  }

  protected virtual void OnMove()
  {
        if (transform.tag == "E1")
        {
            sinMove();
        }
        else if (transform.tag == "E2")
        {
            MovingToPlayerPosition();
        }
        else if (transform.tag == "E4")
        {
            MovingToPlayerPosition();
            spinMove();
        }
  }

  protected virtual void OnScaling()
  {
    ScaleEnemy();
  }

    private float choose = 0.5f;

    private void spinMove()
    {
        transform.Rotate(0,0,90);
    }
    private void sinMove()
  {
        float pox;
        float poy;
        if ((firstpoy + min > transform.position.y || firstpoy + max < transform.position.y) && (dir == 0 || dir == 1))
        {
            choose = choose * -1;
        }
        else if ((firstpox + min > transform.position.x || firstpox + max < transform.position.x) && (dir == 2 || dir == 3))
        {
            choose = choose * -1;
        }

        if (dir == 0)
        {
            pox = transform.position.x + 0.5f;
            poy = transform.position.y + choose;
        }
        else if (dir == 1)
        {
            pox = transform.position.x - 0.5f;
            poy = transform.position.y + choose;
        }
        else if (dir == 2)
        {
            pox = transform.position.x + choose;
            poy = transform.position.y + 0.5f;
        }
        else
        {
            pox = transform.position.x + choose;
            poy = transform.position.y - 0.5f;
        }
        

        transform.position = new Vector3(pox, poy, 0.0f);
    }

  private void MovingToPlayerPosition()
  {
    transform.position = new Vector3(transform.position.x - (_PlayerDirection.x * _Speed), transform.position.y - (_PlayerDirection.y * _Speed), 0.0f);
  }

  private void ScaleEnemy()
  {
    transform.localScale = new Vector3(_Scale, _Scale, 0.0f);
  }
}
