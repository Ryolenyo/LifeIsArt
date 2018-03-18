﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtGeneratorController : MonoBehaviour {

  [SerializeField]
  private GameObject[] _BrushType1;
  [SerializeField]
  private GameObject[] _BrushType2;
  [SerializeField]
  private GameObject[] _BrushType3;
  [SerializeField]
  private GameObject[] _BrushType4;

  private float _CanvasWidth;
  private float _CanvasHeight;
  private string[] _AllAction = new string[4] { "Dash", "Blink", "Slam", "Shoot" }; //TODO: Shoot didnt using yet


  void Awake()
  {
    _CanvasWidth = GetComponent<RectTransform>().rect.width;
    _CanvasHeight = GetComponent<RectTransform>().rect.height;

    Debug.Log(_CanvasWidth);
    Debug.Log(_CanvasHeight);
  }

  void Start()
  {
    GenerateArt();
  }

  private void GenerateArt()
  {
    int allAction = SumAllAction();
    allAction = Mathf.Clamp(allAction, 5, 15);

    for (int i = 0; i < allAction; i++)
    {
      //choose brush type
      int type = Random.Range(1,5);
      //random brush.color
      int color = Random.Range(1, 5);
      //random place
      float x = Random.Range(-_CanvasWidth/2, _CanvasWidth/2);
      float y = Random.Range(-_CanvasHeight/2, _CanvasHeight/2);
      Vector3 coor = new Vector3(x, y, 0.0f);
      GameObject obj = Instantiate(SelectBrush(type, color), transform);
      obj.transform.localPosition = coor;
      obj.GetComponent<Image>().SetNativeSize();
      obj.transform.localScale = new Vector3(0.5f, 0.5f,0.0f);
      float randx = Random.Range(0.0f, 20.0f);
      float randy = Random.Range(0.0f, 20.0f);
      float randz = Random.Range(0.0f, 180.0f);
      obj.transform.rotation = new Quaternion(randx, randy, randz, 0.0f);
    }
  }

  private GameObject SelectBrush(int type, int color)
  {
    switch (type)
    {
      case 1:
        return _BrushType1[color];
      case 2:
        return _BrushType2[color];
      case 3:
        return _BrushType3[color];
      case 4:
        return _BrushType4[color];
      default:
        return null;
    }
  }

  private int SumAllAction()
  {
    int total = 0;
    for(int i = 0; i < _AllAction.Length; i++)
    {
      total += PlayerPrefs.GetInt(_AllAction[i], 0);
    }

    return total;
  }
}
