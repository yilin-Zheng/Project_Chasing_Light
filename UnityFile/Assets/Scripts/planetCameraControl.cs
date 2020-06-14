using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class planetCameraControl : MonoBehaviour
{
    //public int Small_cubes { get; set; }
    int Small_cubes;

    public GameObject mainCamera;
    public GameObject mark;
    public GameObject SplitCube;
    public GameObject planet;
    public GameObject sound;

    //[HideInInspector]
    //public Vector3 hitPosition;
    private Planet planetScript;
    //NoiseSettings noiseSettings;
    public float speedR;
    public float speedM;

  
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
        this.gameObject.GetComponent<Rigidbody>().useGravity = true;
        planetScript = planet.GetComponent<Planet>();

        Small_cubes = 0;
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

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bounce"))
        {
            this.gameObject.GetComponent<Rigidbody>().useGravity = false;
            Instantiate(mark, this.transform.position + new Vector3(0, 0, -3), this.transform.rotation);
            gameObject.GetComponent<AudioSource>().pitch = Random.Range(0.5f, 1.5f);
            gameObject.GetComponent<AudioSource>().Play();
            cube_split();
            planetScript.hitPosition = this.transform.position;
            this.transform.position *= 0.6f;
       
            planetScript.PleaseSmash();         
        }
        if (other.gameObject.CompareTag("door"))
        {
            //Debug.Log("hithithit");

            //GameObject[] obj = SceneManager.GetSceneByName("planetScene").GetRootGameObjects();
            //foreach (GameObject g in obj)
            //{
            //    Destroy(g);
            //}
            SceneManager.UnloadSceneAsync("planetScene");
            if (SceneManager.GetSceneByName("worldScene").isLoaded != true)
            {
                SceneManager.LoadScene("worldScene");
            }

            GameObject[] objs = SceneManager.GetSceneByName("worldScene").GetRootGameObjects();
            foreach (GameObject g in objs)
            {
                g.SetActive(true);
            }
            //Resources.UnloadUnusedAssets();
        }
    }

    void cube_split()
        {
            for (int i = 0; i < 10; i++)
            {
                Instantiate(SplitCube, this.transform.position + new Vector3(0, 0, 1), this.transform.rotation);
                Small_cubes++;
            }
            //Debug.Log("new cube created  " + Small_cubes);
        }


}

