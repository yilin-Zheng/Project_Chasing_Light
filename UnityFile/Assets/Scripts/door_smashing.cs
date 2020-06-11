using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door_smashing : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), Random.Range(-5f, 5f));
        this.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0.7f, 0.4f, 0.5f);  
    }
}
