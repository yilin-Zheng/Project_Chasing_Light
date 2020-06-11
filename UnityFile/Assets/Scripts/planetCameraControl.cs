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
    mainCameraControl MC_script;
    public GameObject mark;
    public GameObject SplitCube;
    public GameObject planet;
    //[HideInInspector]
    //public Vector3 hitPosition;
    private Planet planetScript;
    //NoiseSettings noiseSettings;
    public float speedR;
    public float speedM;

    private float yaw = 0.0f;
    private float pitch = 0.0f;
    
    void Start()
    {
        this.gameObject.GetComponent<Rigidbody>().useGravity = true;
        planetScript = planet.GetComponent<Planet>();
        MC_script = mainCamera.GetComponent<mainCameraControl>();

        Small_cubes = 0;
    }

    void Update()
    {
        //MC_script.Cube_Num = Small_cubes;

        //yaw += speedR * Input.GetAxis("Mouse X");
        //pitch -= speedR * Input.GetAxis("Mouse Y");
        ////transform.rotation = new Quaternion(pitch, yaw, 0.0f, 1.0f);
        //transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        //Vector3 m_p = Input.mousePosition;
        //float rotation_x = (m_p.x / Screen.width - 0.5f) * 360f;
        //float rotation_y = (m_p.y / Screen.height - 0.5f) * 180f;
        //transform.eulerAngles = new Vector3(rotation_y, -rotation_x, 0.0f);

        float s = Input.GetAxis("space") * speedM;
        transform.Translate(0, 0, s);

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bounce"))
        {
            this.gameObject.GetComponent<Rigidbody>().useGravity = false;
            Instantiate(mark, this.transform.position + new Vector3(0, 0, -3), this.transform.rotation);
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

