using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class triangleCameraControl : MonoBehaviour
{
    public GameObject doorToNextLevel;
    public GameObject mainCamera;

    public float speedM;

    //------------------------------------
    float clicked = 0;
    float clicktime = 0;
    float clickdelay = 0.5f;
    bool stop = true;

    bool DoubleClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clicked++;
            if (clicked == 1) clicktime = Time.time;
        }
        if (clicked > 1 && Time.time - clicktime < clickdelay)
        {
            clicked = 0;
            clicktime = 0;
            return true;
        }
        else if (clicked > 2 || Time.time - clicktime > 1) clicked = 0;
        return false;
    }
    //---------------------------------------------

    void LateUpdate()
    {

        float s = speedM/2;

        if (DoubleClick())
        {
            stop = !stop;
        }
        if (stop)
        {
            s = Input.GetAxis("space") * (speedM);
        }
        this.transform.position += (mainCamera.transform.position - transform.position).normalized * -s * Time.deltaTime * 50;
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
            //this.transform.position *= 0.7f;
            gameObject.GetComponent<Rigidbody>().useGravity = false;
        }
    }

    public void loadMenu()
    {
        SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Additive);
    }


}
