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
        Physics.gravity = new Vector3(0, -25, 0); 
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("wall"))
        {
            //gameObject.GetComponent<Rigidbody>().useGravity = false;

            gameObject.GetComponent<AudioSource>().pitch = Random.Range(1f, 1.7f);
            //gameObject.GetComponent<AudioSource>().Play();
            count++;
            if (count <4)
            {
                Instantiate(SMSH, this.transform.position, Quaternion.identity);
            }
        }
    }
}
