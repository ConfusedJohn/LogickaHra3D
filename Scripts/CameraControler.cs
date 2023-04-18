using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
    public float sensitivity;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
    void FixedUpdate()
    {
        float rotateHorizontal = Input.GetAxis("Mouse X");
        float rotateVertical = Input.GetAxis("Mouse Y");
        transform.RotateAround(player.transform.position, -Vector3.up, -rotateHorizontal * sensitivity);
        transform.RotateAround(Vector3.zero, transform.right, -rotateVertical * sensitivity);
        //transform.Rotate(-transform.up * rotateHorizontal * sensitivity);//use  instead if you dont want the camera to rotate around the player
        //transform.Rotate(transform.right * rotateVertical * sensitivity);// again, use  if you don't want the camera to rotate around the player
    }
}
