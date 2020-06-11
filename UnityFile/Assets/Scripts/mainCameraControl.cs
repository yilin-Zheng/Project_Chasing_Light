using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class mainCameraControl : MonoBehaviour
{

    public GameObject doorToNextLevel;
    public Button winTxt;
    public Canvas canv;
    public int Cube_Num { get; set; }

    public float speedR = 1f;
    public float speedM = 1f;
    public float a;
    public float b;
    public float c;
    public float d;
    private float yaw = 0.0f;
    private float pitch = 0.0f;

    int planet_entered;

    void Start()
    {
        planet_entered = 3;
    }

    void Update()
    {
        yaw += speedR * Input.GetAxis("Mouse X");
        pitch -= speedR * Input.GetAxis("Mouse Y");
        //transform.rotation = new Quaternion(pitch, yaw, 0.0f , 1.0f);
        if(pitch > 85 || pitch < -85)
        {
            yaw -= speedR * Input.GetAxis("Mouse X");
            pitch += speedR * Input.GetAxis("Mouse Y");
        }
        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        //Vector3 m_p = Input.mousePosition;
        //float rotation_x = (m_p.x / Screen.width - 0.5f) * 540f;
        //float rotation_y = (m_p.y / Screen.height - 0.5f) * 240;
        //transform.eulerAngles = new Vector3(rotation_y, -rotation_x, 0.0f);

        float s = Input.GetAxis("space") * speedM;
        transform.Translate(0, 0, s);
        //this.transform.rotation = new Quaternion(a, b, c, d);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("planet"))
        {
            Debug.Log("changeScene");

            string[] a = {"planetScene", "TriangleOne","sphereOne"};
            int i = Random.Range(0, 3);

            planet_entered++;
            if (planet_entered == 4)
            {
                //Button btn = Instantiate(winTxt, canv.transform.position, Quaternion.identity);
                //btn.transform.parent = canv.transform;

                doorToNextLevel = Instantiate(doorToNextLevel,new Vector3(0,0,0),Quaternion.identity);
                //need a spetacular smashing animation
            }

            GameObject[] objs = SceneManager.GetSceneByName("worldScene").GetRootGameObjects();
            foreach (GameObject g in objs)
            {
                g.SetActive(false);
            }
            SceneManager.LoadSceneAsync(a[i], LoadSceneMode.Additive);
        }
        if (other.gameObject.CompareTag("door"))
        {
            doorToNextLevel.SetActive(false);
            SceneManager.LoadSceneAsync("doorSphere", LoadSceneMode.Additive);
        }
    }

    public void loadMenu()
    {
        SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Additive);
    }


}
