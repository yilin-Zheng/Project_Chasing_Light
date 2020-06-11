using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sphereOne : MonoBehaviour
{
    public GameObject shadow1;
    public GameObject shadow2;
    public GameObject balls;

    public float speedR ;
    public float speedM ;

    private float yaw = 0.0f;
    private float pitch = 0.0f;


    void Start()
    {


    }

    void Update()
    {
        shadow1.transform.Rotate(0, 0.1f, 0.1f);
        shadow2.transform.Rotate(0.1f, 0, 0.2f);
        balls.transform.Rotate(0, 0.2f, 0.1f);

        yaw += speedR * Input.GetAxis("Mouse X");
        pitch -= speedR * Input.GetAxis("Mouse Y");
       
        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        //Vector3 m_p = Input.mousePosition;
        //float rotation_x = (m_p.x / Screen.width - 0.5f) * 540f;
        //float rotation_y = (m_p.y / Screen.height - 0.5f) * 90f;
        //transform.eulerAngles = new Vector3(rotation_y, -rotation_x, 0.0f);

        float s = Input.GetAxis("space") * speedM;
        transform.Translate(0, 0, s);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("door"))
        {
            SceneManager.UnloadSceneAsync("sphereOne");

            if (SceneManager.GetSceneByName("worldScene").isLoaded != true)
            {
                SceneManager.LoadScene("worldScene");
            }
            GameObject[] objs = SceneManager.GetSceneByName("worldScene").GetRootGameObjects();
            foreach (GameObject g in objs)
            {
                g.SetActive(true);
            }
        }
    }

}
