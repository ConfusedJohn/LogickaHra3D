using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    void Start()
    {
        transform.Rotate(new Vector3(45, 45, 45));
    }

    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45)*Time.deltaTime);
    }
}
