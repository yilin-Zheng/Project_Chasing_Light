﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planet_BounceMark : MonoBehaviour
{
    public GameObject planet;
    private Planet planetScript;
    private static Vector3 primary_pos;
    void Start()
    {
        primary_pos = this.transform.position;
        planetScript = planet.GetComponent<Planet>();
        //this.transform.position = planetScript.CalculatePointOnPlanet(primary_pos) / 21 ;
        
        //shapeGenerator = new ShapeGenerator(planetScript.settings);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
