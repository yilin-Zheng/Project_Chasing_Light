using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SwOne_CameraControl : MonoBehaviour
{

    public GameObject doorToNextLevel;
    public GameObject sphereToSmash;
    public GameObject mainCamera;
    public GameObject longCamera;
    public GameObject lightTrigger;
    public GameObject longLight;
    public Button winTxt;
    public Canvas canv;

    public float speedR = 1.0f;
    public float speedM = 1.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    int planet_entered;
    int planet_smashed;
    int control = 0;
    int num_to_instantiate = 0;
    float time;
    float arcTimer;
    float pause_time;
    bool inPlanet = true;
    public Vector3 planetCentre = new Vector3(0, 0, 0);
    Vector3 rabitPosition = new Vector3(0, 0, 0);

    float planetRadius;


    public GameObject pOne;
    public GameObject pTwo;
    public GameObject pThree;
    GameObject[] planet;


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
        Vector3 p = new Vector3(0, 0, 0);
        planet_entered = 3;
        time = Time.time;
        planet = new GameObject[] { pOne, pTwo, pThree };
    }

    /*------------------------------
     * create planet (0,0,0), bounce inside until a specific position
     * create another in direction of camera,
     * move in arc, change to longCamera
     * after movement, back to near camera;
     ------------------------------- */

    void Update()
    {

        //yaw += speedR * Input.GetAxis("Mouse X");
        //pitch -= speedR * Input.GetAxis("Mouse Y");
        ////transform.rotation = new Quaternion(pitch, yaw, 0.0f, 1.0f);
        //if (pitch > 90 || pitch < -90)
        //{
        //    yaw -= speedR * Input.GetAxis("Mouse X");
        //    pitch += speedR * Input.GetAxis("Mouse Y");
        //}
        //transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);


        Vector3 m_p = Input.mousePosition;
        float rotation_x = (m_p.x / Screen.width - 0.5f) * 540f;
        float rotation_y = (m_p.y / Screen.height - 0.5f) * 240;
        transform.eulerAngles = new Vector3(rotation_y, -rotation_x, 0.0f);

        //-----------------------------
        float s = speedM / 4 ;

        if (DoubleClick())
        {
            stop = !stop;
        }
        if (stop)
        {
            s = Input.GetAxis("space") * (speedM /2);
        }
        transform.Translate(0, 0, s * Time.deltaTime * 100);
        //-------------------------------

        if (inPlanet == true)
        {
            
        }

        //-------------------------------

        if (inPlanet != true)
        {
            this.transform.position = PosiiotnOfRabbit(Time.time - arcTimer);
            if (Time.time - time > 0.04)
            {
                GameObject g = Instantiate(sphereToSmash, this.transform.position,
                    this.transform.rotation);
                //g.transform.Translate(pos(time));
                time = Time.time;
            }

            if (Vector3.Distance(this.transform.position,planetCentre)< 50)
            {
                longCamera.SetActive(false);
                longLight.SetActive(false);
                mainCamera.SetActive(true);
                this.transform.position = planetCentre;
                inPlanet = true;

                if (planet_smashed == 3 || planet_smashed == 8 || planet_smashed == 9)
                {
                    string[] b = { "planetScene", "TriangleOne", "sphereOne" };
                    int i = Random.Range(0, 3);

                    GameObject[] objs = SceneManager.GetSceneByName("worldScene").GetRootGameObjects();
                    foreach (GameObject g in objs)
                    {
                        g.SetActive(false);
                    }
                    SceneManager.LoadSceneAsync(b[i], LoadSceneMode.Additive);
                }
            }
        }


        //if (planet_smashed == 4)
        //{
        //    Debug.Log("the fourth");
        //    //Button btn = Instantiate(winTxt, canv.transform.position, Quaternion.identity);
        //    //btn.transform.parent = canv.transform;

        //    //doorToNextLevel = Instantiate(doorToNextLevel, new Vector3(0, 0, 0), Quaternion.identity);
        //    //need a spetacular smashing animation
        //}
    }


    private Vector3 PosiiotnOfRabbit(float t)
    {
        //towards new centre

        float f = t * 120;

        Vector3 direction =  (planetCentre - rabitPosition);
        Debug.Log(planetCentre);

        float arcRad = Mathf.Sin(Mathf.PI * (f /direction.magnitude));
        Vector3 arcDirection = longCamera.transform.position.normalized * arcRad * f;

        //return new Quaternion(x, y, z, w);
        return rabitPosition + direction * (f /direction.magnitude) + arcDirection;
    }

    void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.CompareTag("door"))
        //{
        //    doorToNextLevel.SetActive(false);
        //    SceneManager.LoadSceneAsync("doorSphere", LoadSceneMode.Additive);
    
        if (other.gameObject.CompareTag("trigger"))
        {
            Debug.Log("trigger trigger");
            inPlanet = false;
            longCamera.SetActive(true);
            longLight.SetActive(true);
            mainCamera.SetActive(false);
            longCamera.transform.position = planetCentre * 2;

            time = Time.time;
            arcTimer = Time.time;
            planetCentre += new Vector3(Random.Range(-100, 100), Random.Range(-100, 100), Random.Range(-100,100))
                + lightTrigger.transform.position * 5;//??????
            rabitPosition = this.transform.position;
            num_to_instantiate += 6;
            planet_smashed++;

            int a = Random.Range(0, 3);
            planetRadius = Random.Range(75, 140);
            GameObject gg = Instantiate(planet[a], planetCentre, this.transform.rotation);
            gg.transform.localScale = new Vector3(10, 10, 10) * planetRadius * 0.9f;
            Instantiate(lightTrigger,
                planetCentre + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * planetRadius * 0.9f,
                Quaternion.identity);

           
        }
    }

    public void loadMenu()
    {
        SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Additive);
    }

}
