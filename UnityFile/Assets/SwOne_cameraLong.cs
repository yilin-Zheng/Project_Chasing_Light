using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwOne_cameraLong : MonoBehaviour
{
    public GameObject mainCamera;
    SwOne_CameraControl camScript;

    void Start()
    {
        camScript = mainCamera.GetComponent<SwOne_CameraControl>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Sin(Time.time /10), Mathf.Cos(Time.time/10), Mathf.Sin(Time.time/20)).normalized * 555;
        transform.LookAt(mainCamera.transform.position, Vector3.left);
    }
}
