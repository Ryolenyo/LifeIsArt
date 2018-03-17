using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{

    [SerializeField]
    protected float _Speed;
    [SerializeField]
    protected float _Scale;

    private GameObject _Target;
    private Vector3 _PlayerDirection;

    public float min;
    public float max;

    public float firstpox;
    public float firstpoy;
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
        else if (transform.tag == "E3")
        {
            DashToPlayer();
        }
        else if (transform.tag == "E4")
        {
            MovingToPlayerPosition();
            spinMove();
        }
    }

    public float radius1 = 10;
    public float radius2 = 5;

    protected virtual void OnScaling()
    {
        ScaleEnemy();
    }

    float timeCounter = 0;
    float poxSpin = 0;
    float poySpin = 0;
    float spd = 10;

    private void spinMove()
    {
        UnityEngine.Random xx = new UnityEngine.Random();
        float someX = 0.5f;
        float someY = 0.1f;
        //transform.position += Vector3.up * 0.5f;
        transform.Rotate(0, 0, 2);

        if (firstpox < 0.000001 && firstpoy > 0.000001) // up left
        {
            someY = -someY;
        }
        else if (firstpox > 0.000001 && firstpoy > 0.000001) // up right
        {
            someX = -someX;
            someY = -someY;
        }
        else if (firstpox < 0.000001 && firstpoy < 0.000001) // down left
        {

        }
        else // down right
        {
            someX = -someX;
        }

        timeCounter = timeCounter + Time.deltaTime;
        float gx = Mathf.Cos(timeCounter);
        float gy = Mathf.Sin(timeCounter);

        transform.position = new Vector3(gx * spd + poxSpin + firstpox, gy * spd + poySpin + firstpoy, 0.0f);
        poxSpin += someX;
        poySpin += someY;


    }

    private float choose = 0.5f;

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

    int on = 0;

    private void DashToPlayer()
    {
        transform.position = new Vector3(transform.position.x - (_PlayerDirection.x * _Speed), transform.position.y - (_PlayerDirection.y * _Speed), 0.0f);
        float distant = Mathf.Sqrt(Mathf.Pow((transform.position.x - _Target.transform.position.x), 2) + Mathf.Pow((transform.position.y - _Target.transform.position.y), 2));
        //Debug.Log(distant);
        if (distant < 20 && on == 0)
        {
            _Speed = _Speed * -1.5f;
            on = 1;
        }
        if (_Speed < 0.00001f && distant > 40)
        {
            _Speed = _Speed * -3f;

        }
    }

    private void ScaleEnemy()
    {
        transform.localScale = new Vector3(_Scale, _Scale, 0.0f);
    }
}
