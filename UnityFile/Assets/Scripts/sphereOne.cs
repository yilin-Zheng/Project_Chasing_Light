using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sphereOne : MonoBehaviour
{
    public GameObject shadow1;
    public GameObject shadow2;
    public GameObject balls;
    public GameObject mainCamera;

    public float speedR ;
    public float speedM ;


    //---------------------------------------------

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

    void Start()
    {


    }

    void Update()
    {
        float s = speedM / 2;
        if (DoubleClick())
        {
            stop = !stop;
        }
        if (stop)
        {
            s = Input.GetAxis("space") * (speedM);
        }
        this.transform.position += (mainCamera.transform.position - transform.position).normalized * -s * Time.deltaTime * 50;

        shadow1.transform.Rotate(0, 0.1f, 0.1f);
        shadow2.transform.Rotate(0.1f, s *10, 0.2f);
        //balls.transform.Rotate(0, 0.2f, 0.1f);

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
        if (other.gameObject.CompareTag("wall"))
        {
            gameObject.GetComponent<AudioSource>().Play(0);
        }
        if (other.gameObject.CompareTag("planet"))
        {
            gameObject.GetComponent<AudioSource>().Play(0);
        }

    }

}
