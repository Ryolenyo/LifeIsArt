using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPointController : MonoBehaviour {

  [SerializeField]
  private float _SetNewValueTime;
  [SerializeField]
  private GameObject _EnemyPrefab;

  //This variable is up to position in hierachy of spawn point.
  private float _MaxRandomRangeX;
  private float _MaxRandomRangeY;
  private int _NumberOfEnemy;
  private Dictionary<int, float> _CooldownPerGate;
  private Dictionary<int, Vector3> _SpawnPoints;
  private float _Time;
  private Dictionary<int, float> _GateCooldown;

  void Awake()
  {
    _Time = 0.0f;
    _MaxRandomRangeY = GameObject.Find("BG").GetComponent<SpriteRenderer>().bounds.size.y;
    _MaxRandomRangeX = GameObject.Find("BG").GetComponent<SpriteRenderer>().bounds.size.x;
  }

  void Start()
  {
    SetNewValue();
    SpawnEnemy();
  }

  void FixedUpdate()
  {
    SpawnEnemy();

    _Time += Time.deltaTime;
    if (_Time > _SetNewValueTime)
    {
      SetNewValue();
      _Time = 0;
    }
  }

  private void SpawnEnemy()
  {
    foreach(KeyValuePair<int, float> entry in _CooldownPerGate)
    {
      _GateCooldown[entry.Key] += Time.deltaTime;
      if (_GateCooldown[entry.Key] > entry.Value)
      {
        GameObject enemy = Instantiate(_EnemyPrefab, _SpawnPoints[entry.Key], Quaternion.identity);
        enemy.GetComponent<Enemy>().SetSpeed(Random.Range(0.1f, 0.3f));
        enemy.GetComponent<Enemy>().SetScale(Random.Range(1.0f, 6.0f)); // making for variant size TODO: have to make big boss size that create at normal distribution.
        _GateCooldown[entry.Key] = 0.0f;
      }
    }
  }

  private void SetNewValue()
  {
    _SpawnPoints = new Dictionary<int, Vector3>();
    _CooldownPerGate = new Dictionary<int, float>();
    _GateCooldown = new Dictionary<int, float>();

    System.Random rng = new System.Random();
    //random number of enemy.
    _NumberOfEnemy = rng.Next(3,9);

    int x = 0;
    int y = 0;
    for(int i = 0; i < _NumberOfEnemy; i++)
    {
      // random up down.
      if (rng.Next(0, 2) == 0)
      {
        //up
        y = rng.Next(Mathf.RoundToInt(_MaxRandomRangeY / 2 ) - 6, Mathf.RoundToInt(_MaxRandomRangeY / 2));
      }
      else
      {
        //down
        y = rng.Next(-Mathf.RoundToInt(_MaxRandomRangeY / 2), -Mathf.RoundToInt(_MaxRandomRangeY / 2 ) + 6);
      }

      // random left right.
      if (rng.Next(0, 2) == 0)
      {
        //left
        x = rng.Next(-Mathf.RoundToInt(_MaxRandomRangeX / 2), -Mathf.RoundToInt(_MaxRandomRangeX / 2) + 6);
      }
      else
      {
        //right
        x = rng.Next(Mathf.RoundToInt(_MaxRandomRangeX / 2) - 6, Mathf.RoundToInt(_MaxRandomRangeX / 2));
      }

      _SpawnPoints[i] = new Vector3(x, y);
      _CooldownPerGate[i] = rng.Next(1,3);
      _GateCooldown[i] = 0.0f;
    }
  }
}
