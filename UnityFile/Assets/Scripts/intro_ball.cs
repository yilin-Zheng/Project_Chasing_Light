using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class intro_ball : MonoBehaviour
{
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

    void Start()
    {

    }

    void Update()
    {
        if (DoubleClick())
        {
            stop = !stop;
        }
        float s = 1.8f;
        if (stop)
        {
            s = Input.GetAxis("space");
        }
        this.transform.Rotate(s + 0.2f, 0.2f, 0);
    }

    public void loadMenu()
    {
        SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Additive);
    }
}

