using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class intro_Particle : MonoBehaviour
{
    float speedR = 2.0f;
    float speedM = 0.1f;
    private float yaw = 0.0f;
    private float pitch = 0.0f;

    //public GameObject particle;
    float control =11f;

    void Start()
    {
     
    }

    void Update()
    {
        //yaw += speedR * Input.GetAxis("Mouse X");
        //pitch -= speedR * Input.GetAxis("Mouse Y");
        //transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        float s = Input.GetAxis("space") * speedM;
        this.transform.localPosition += new Vector3(0,0,s);
        control += s;

        //if (control > 10)
        //{
        //    Instantiate(gameObject, this.transform.position, this.transform.rotation);
        //    control -= 10;
        //}

     
        if (this.transform.localPosition.z > 10)
        {
            Destroy(gameObject);
            //Instantiate(gameObject, this.transform.position, Quaternion.identity);
        }
    }
}
