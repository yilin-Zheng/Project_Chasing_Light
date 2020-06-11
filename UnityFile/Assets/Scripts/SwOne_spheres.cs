using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwOne_spheres : MonoBehaviour
{
    public GameObject smashTriangle;
    public GameObject cam;
    Color c;
    Color e;
    float t;

    void Start()
    {

        t = Time.time;
        c = gameObject.GetComponent<MeshRenderer>().material.color;
        e = gameObject.GetComponent<MeshRenderer>().material.GetColor("_EmissionColor");
    }

    // Update is called once per frame
    void Update()
    {
        float a;
        a = Mathf.Sin(Time.time - t)* 0.4f + 0.2f;
        c.a = a ;
        gameObject.GetComponent<MeshRenderer>().material.color = c;//SetColor("_EmissionColor", c);
        gameObject.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", e + new Color (a,a,a,a)/2);//Vector4.Scale(e,new Vector4(a,1.0f,a,1.0f))
    }
    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            Instantiate(smashTriangle, cam.transform.position, Quaternion.identity);

        }
    }
}