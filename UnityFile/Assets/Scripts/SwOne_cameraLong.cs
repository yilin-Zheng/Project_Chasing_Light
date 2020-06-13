using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwOne_cameraLong : MonoBehaviour
{
    public GameObject mainCamera_parent;
    SwOne_CameraControl camScript;

    void Start()
    {
        camScript = mainCamera_parent.GetComponent<SwOne_CameraControl>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(camScript.planetCentre);
        //(camScript.planetCentre + camScript.rabitPosition)/2 +;
        transform.position = new Vector3(Mathf.Sin(Time.time /10), Mathf.Cos(Time.time/10), Mathf.Sin(Time.time/20)).normalized * 333
             + camScript.planetCentre;
        transform.LookAt(mainCamera_parent.transform.position, Vector3.left);
    }
}
