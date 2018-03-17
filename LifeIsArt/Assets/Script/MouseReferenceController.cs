using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseReferenceController : MonoBehaviour {
  void FixedUpdate()
  {
    transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
  }
}
