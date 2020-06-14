using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


public class menu : MonoBehaviour
{
    public Button restart;
    public Button back;
    public Button scene_one;
    public Button scene_two;
    public Button scene_three;

    // Start is called before the first frame update
    void Start()
    {
        restart.onClick.AddListener(sceneSphere);
        back.onClick.AddListener(backToGame);
        scene_one.onClick.AddListener(triangleOne);
        scene_two.onClick.AddListener(sphereOne);
        scene_three.onClick.AddListener(planet);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void sceneSphere()
    {
        gameObject.GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("worldScene");
    }

    public void backToGame()
    {
        gameObject.GetComponent<AudioSource>().Play();
        SceneManager.UnloadSceneAsync("Menu");
    }


    public void triangleOne()
    {
        //gameObject.GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("TriangleOne");
    }
    public void sphereOne()
    {
        //gameObject.GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("sphereOne");
    }
    public void planet()
    {
        //gameObject.GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("planetScene");
    }

}
