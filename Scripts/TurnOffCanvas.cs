using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffCanvas : MonoBehaviour
{
    void Start()
    {
        GameObject.Find("CanvasSettings").gameObject.SetActive(false);
    }
}
