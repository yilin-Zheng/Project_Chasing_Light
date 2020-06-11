using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class doorSphere_camera : MonoBehaviour
{

    float timer;
    int action;

    void Start()
    {
        timer = Time.time;
        action = 0;
    }


    // Update is called once per frame
    void Update()
    {
        if (Time.time - timer > 2 && action ==0)
        {
            SceneManager.UnloadSceneAsync("worldScene");
            action++;
        }
        if (Time.time - timer > 5 && action ==1)
        {
            SceneManager.LoadScene("worldTriangle");
            action++;
        }
    }
}
