using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triagle_smash : MonoBehaviour
{
    public GameObject triangleToReplace;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MainCamera"))
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
            triangleToReplace.SetActive(true);
        }
    }
}
