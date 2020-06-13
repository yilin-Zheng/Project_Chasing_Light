using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sphereOne_particles : MonoBehaviour
{

    public GameObject SMSH;
    int count;

    void Start()
    {
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (count > 3)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("wall"))
        {
            Instantiate(SMSH , this.transform.position, Quaternion.identity);
            count++;
            Debug.Log(count);
        }
    }
}
