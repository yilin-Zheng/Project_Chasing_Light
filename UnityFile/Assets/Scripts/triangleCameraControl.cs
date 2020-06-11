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
   
    void Update()
    {

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
