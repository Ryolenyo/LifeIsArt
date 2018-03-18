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
  private string[] _AllAction = new string[5] { "Dash", "Blink", "Slam", "Shoot", "Score"}; //TODO: Shoot didnt using yet


  void Awake()
  {
    _CanvasWidth = GetComponent<RectTransform>().rect.width;
    _CanvasHeight = GetComponent<RectTransform>().rect.height;

  }

  void Start()
  {
    GameObject.Find("Score").GetComponent<Text>().text = "SCORE : " + PlayerPrefs.GetInt("Score", 0);
    GenerateArt();
  }

  private void GenerateArt()
  {
    int allAction = SumAllAction();
    Debug.Log("Sccore --> " + PlayerPrefs.GetInt("Score", 0));
    if (PlayerPrefs.GetInt("Score", 0) - allAction < PlayerPrefs.GetInt("Score", 0) / 2)
    {
      if ((PlayerPrefs.GetInt("Score", 0) / 2) < 1)
      {
        PlayerPrefs.SetInt("Score", 1);
      }
      else
      {
        PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score", 0) / 2);
      }
    }
    else
    {
      PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score", 0) - allAction);
    }


    for (int i = 0; i < _AllAction.Length; i++)
    {
      for (int j = 0; j < PlayerPrefs.GetInt(_AllAction[i], 0); j++)
      {
        Debug.Log(_AllAction[i]);
        int type = Random.Range(0, 8);
        float x = Random.Range(-_CanvasWidth / 2, _CanvasWidth / 2);
        float y = Random.Range(-_CanvasHeight / 2, _CanvasHeight / 2);
        Vector3 coor = new Vector3(x, y, 0.0f);
        GameObject obj = Instantiate(SelectBrush(_AllAction[i], type), transform);
        obj.transform.localPosition = coor;
        obj.GetComponent<Image>().SetNativeSize();
        obj.transform.localScale = new Vector3(0.5f, 0.5f, 0.0f);
        float randx = Random.Range(0.0f, 20.0f);
        float randy = Random.Range(0.0f, 20.0f);
        float randz = Random.Range(0.0f, 180.0f);
        obj.transform.rotation = new Quaternion(randx, randy, randz, 0.0f);
      }
    }
  }

  private GameObject SelectBrush(string type, int color)
  {
    switch (type)
    {
      case "Dash":
        return _BrushType1[color];
      case "Score":
        return _BrushType2[color];
      case "Slam":
        return _BrushType3[color];
      case "Blink":
        return _BrushType4[color];
      default:
        return _BrushType3[color];
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

  private int DashRatio()
  {
    return Mathf.RoundToInt(PlayerPrefs.GetInt("Dash", 0) / SumAllAction());
  }

  private int SlamRatio()
  {
    if (PlayerPrefs.GetInt("Slam", 0) != 0)
    {
      if (Mathf.RoundToInt(PlayerPrefs.GetInt("Slam", 0) / SumAllAction()) <= 0 )
      {
        return 1;
      }
    }

    return Mathf.RoundToInt(PlayerPrefs.GetInt("Slam", 0) / SumAllAction());
  }

  private int BlinkRatio()
  {
    return Mathf.RoundToInt(PlayerPrefs.GetInt("Blink", 0) / SumAllAction());
  }
}
