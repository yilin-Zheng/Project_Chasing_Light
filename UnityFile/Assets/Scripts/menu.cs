using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        SceneManager.LoadScene("worldScene");
    }

    public void backToGame()
    {
        SceneManager.UnloadSceneAsync("Menu");
    }


    public void triangleOne()
    {
        SceneManager.LoadScene("TriangleOne");
    }
    public void sphereOne()
    {
        SceneManager.LoadScene("sphereOne");
    }
    public void planet()
    {
        SceneManager.LoadScene("planetScene");
    }

}
