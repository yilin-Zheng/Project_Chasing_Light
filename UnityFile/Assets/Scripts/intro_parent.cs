using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class intro_parent : MonoBehaviour
{
    public GameObject ball;
    public GameObject ballParts;
    public GameObject particle;
    public Button startBtn;
    public Text guideTxt;

    public float speedR = 1.0f;
    public float speedM = 1.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    float control = 0.0f;
    int step = 0;
    float sp_ball = 0f;
    float time = 10000f;

    //------------------------------------
    float clicked = 0;
    float clicktime = 0;
    float clickdelay = 0.5f;
    bool stop = false;

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
        startBtn.gameObject.SetActive(false);
        ball.SetActive(false);
        ballParts.SetActive(false);
    }

    void Update()
    {
        Debug.Log(step);

        yaw += speedR * Input.GetAxis("Mouse X");
        pitch -= speedR * Input.GetAxis("Mouse Y");
        if (pitch > 90 || pitch < -90)
        {
            yaw -= speedR * Input.GetAxis("Mouse X");
            pitch += speedR * Input.GetAxis("Mouse Y");
        }
        this.transform.eulerAngles = new Vector3(-pitch, yaw, 0.0f);


        float s = Input.GetAxis("space") * speedM;

        if (Mathf.Abs(yaw) > 30 && step==0)
        {
            step = 1;
            gameObject.GetComponent<AudioSource>().Play();
            guideTxt.GetComponent<Text>().text = "CLICK\n or press SPACK";
            // print click or press space to move
        }

        if (DoubleClick())
        {
            stop = !stop;
            if (step == 2)
            {
                step++;
                gameObject.GetComponent<AudioSource>().Play();
                guideTxt.GetComponent<Text>().text = "";
                startBtn.gameObject.SetActive(true);
            }
        }
        if (stop)
        {
            s = speedM;
        }

        control += s;
        if (control > 20)
        {
            if (step == 1)
            {
                step = 2;
                gameObject.GetComponent<AudioSource>().Play();
                guideTxt.GetComponent<Text>().text = "Double CLICK";
                ///double click
            }

            float x = Random.Range(-8, 8);
            float y = Random.Range(-8, 8);
            float z = -10; //Random.Range(-5, 5);
            GameObject g = Instantiate(particle, new Vector3(0,0,0), Quaternion.identity);
            g.transform.parent = gameObject.transform;
            g.transform.localPosition += new Vector3(x, y, z);
            control = 0;
        }

        if (ball.activeSelf)
        {
            ball.transform.LookAt(gameObject.transform, Vector3.up);
            ball.transform.Translate(0, 0, sp_ball);
        }
        if(Vector3.Distance(ball.transform.position,new Vector3(0,0,0))< 3)
        {
            ballParts.SetActive(true);
            //ballParts.transform.Translate(0, 0, -300);
            sp_ball = 0f;
            ball.transform.position = new Vector3(0, 0, 0);
        }
        if(Time.time - time > 3)
        {
            SceneManager.LoadScene("worldScene");
        }

    }

    public void loadGame()
    {
        ball.SetActive(true);
        startBtn.GetComponent<AudioSource>().Play();
        sp_ball = 5f;
        ball.transform.rotation = this.transform.rotation;
        ball.transform.Translate(0, 0, -300);
        time = Time.time;
    }


    //UI： 1. look  arround 2. double click 3. double click again: stop
}
