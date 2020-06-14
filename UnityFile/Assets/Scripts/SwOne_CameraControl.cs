using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class SwOne_CameraControl : MonoBehaviour
{

    public GameObject rabbitBall;
    public GameObject sphereToSmash;
    public GameObject mainCamera;
    public GameObject longCamera;
    public GameObject lightTrigger;
    public GameObject longLight;
    public Button winTxt;
    public Canvas canv;

    public float speedR = 1.0f;
    public float speedM = 1.0f;

    int planet_entered;
    int planet_smashed;
    int control = 0;
    int num_to_instantiate = 0;
    float time;
    float arcTimer;
    float pause_time;
    bool inPlanet = true;
    public Vector3 planetCentre { get; set; }
    public Vector3 rabitPosition { get; set; }
    Vector3 lightTrigger_Position = new Vector3(0, 0, 0);

    float planetRadius;
    float smashPitch = 1f;


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
        planetCentre = new Vector3(0, 0, 0);
        rabitPosition = new Vector3(0, 0, 0);

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

        float s = speedM *3 ;

        if (DoubleClick())
        {
            stop = !stop;
        }
        if (stop)
        {
            s = Input.GetAxis("space") * (speedM);
        }
        this.transform.position += (mainCamera.transform.position - transform.position).normalized * -s *  Time.deltaTime * 50;
        //transform.Translate(0, 0, s);
        //Debug.Log(mainCamera.transform.localRotation);
        //-------------------------------

        if (inPlanet == true)
        {
            
        }

        //-------------------------------

        if (inPlanet != true)
        {
            this.transform.position = PosiiotnOfRabbit(Time.time - arcTimer);
            if (Time.time - time > 0.06)
            {
                GameObject g = Instantiate(sphereToSmash, PosiiotnOfRabbit(Time.time - arcTimer),
                    this.transform.rotation);
                g.GetComponent<AudioSource>().pitch = smashPitch * Random.Range(0.7f, 1.2f);
                time = Time.time;
            }

            if (Vector3.Distance(this.transform.position,planetCentre)< 50)
            {
                longCamera.SetActive(false);
                longLight.SetActive(false);
                mainCamera.SetActive(true);
                this.transform.position = planetCentre;
                inPlanet = true;

                if (planet_smashed == 3 || planet_smashed == 6 || planet_smashed == 8
                    || planet_smashed == 12 || planet_smashed == 13 || planet_smashed == 17 || planet_smashed == 20)
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

                //if(planet_smashed == 9)
                //{
                //    //infinity mode  or back to game
                //    //load menu
                //    //button: back to game or see more scense
                //}
            }
        }

    }


    private Vector3 PosiiotnOfRabbit(float t)
    {
        //towards new centre

        float f = t * 90;

        Vector3 direction =  (planetCentre - rabitPosition);

        float arcRad = Mathf.Sin(Mathf.PI * (f /direction.magnitude));
        Vector3 arcDirection = longCamera.transform.position.normalized * arcRad * f;

        //return new Quaternion(x, y, z, w);
        return rabitPosition + direction * (f /direction.magnitude) + arcDirection * 0.8f;
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
            planetCentre += new Vector3(Random.Range(-100, 100), Random.Range(-100, 100), Random.Range(-100, 100))*5;//??????
            rabitPosition = this.transform.position;
            num_to_instantiate += 6;
            planet_smashed++;

            int a = Random.Range(0, 3);
            planetRadius = Random.Range(75, 140);
            GameObject gg = Instantiate(planet[a], planetCentre, this.transform.rotation);
            gg.transform.localScale = new Vector3(10, 10, 10) * planetRadius;
            lightTrigger_Position = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * planetRadius * 0.9f;
            Instantiate(lightTrigger, planetCentre + lightTrigger_Position, Quaternion.identity);  
        }
        if (other.gameObject.CompareTag("wall"))
        {
            this.transform.position *= 0.8f;
        }
    }

    public void loadMenu()
    {
        SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Additive);
    }

}
