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

    Debug.Log(_CanvasWidth);
    Debug.Log(_CanvasHeight);
  }

  void Start()
  {
    GenerateArt();
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
      float x = Random.Range(-_CanvasWidth/2, _CanvasWidth/2);
      float y = Random.Range(-_CanvasHeight/2, _CanvasHeight/2);
      Vector3 coor = new Vector3(x, y, 0.0f);
      GameObject obj = Instantiate(_BrushType1[0], transform);
      obj.transform.localPosition = coor;
      obj.GetComponent<Image>().SetNativeSize();
      obj.transform.localScale = new Vector3(0.5f, 0.5f,0.0f);
    }
  }
}
