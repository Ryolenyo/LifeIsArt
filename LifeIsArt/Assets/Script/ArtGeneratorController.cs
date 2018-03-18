using System.Collections;
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

  void Awake()
  {
    _CanvasWidth = GetComponent<RectTransform>().rect.width;
    _CanvasHeight = GetComponent<RectTransform>().rect.height;
  }

  private void GenerateArt()
  {
    int size = Random.Range(5, 15);
    for (int i = 0; i < size; i++)
    {
      //choose brush type
      int type = Random.Range(1,5);
      //random brush.color
      int color = Random.Range(1, 5);
      //random place
    }
  }
}
