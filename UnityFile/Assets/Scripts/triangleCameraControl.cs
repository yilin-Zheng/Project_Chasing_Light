using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class triangleCameraControl : MonoBehaviour
{
    public GameObject doorToNextLevel;
    public int Cube_Num { get; set; }

    public float speedR;
    public float speedM;

    private float yaw = 0.0f;
    private float pitch = 0.0f;


   
    void Update()
    {

        yaw += speedR * Input.GetAxis("Mouse X");
        pitch -= speedR * Input.GetAxis("Mouse Y");
        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        //Vector3 m_p = Input.mousePosition;
        //float rotation_x = (m_p.x / Screen.width - 0.5f) * 540f;
        //float rotation_y = (m_p.y / Screen.height - 0.5f) * 240;
        //transform.eulerAngles = new Vector3(rotation_y, -rotation_x, 0.0f);

        float s = Input.GetAxis("space") * speedM;
        transform.Translate(0, 0, s);
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("door"))
        {
            SceneManager.UnloadSceneAsync("TriangleOne");

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
        if (other.gameObject.CompareTag("wall"))
        {
            Debug.Log("is Kinematic");
            this.transform.position *= 0.9f;
            gameObject.GetComponent<Rigidbody>().useGravity = false;
        }
    }

    public void loadMenu()
    {
        SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Additive);
    }


}
