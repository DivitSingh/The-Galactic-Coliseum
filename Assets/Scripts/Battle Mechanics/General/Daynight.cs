using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Daynight : MonoBehaviour {
    public Transform Transpos;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.RotateAround(Transpos.transform.position, Vector3.right, 2.0f*Time.deltaTime); //rotates around the building 
        transform.LookAt(Transpos.transform.position);//makes sure it points the light at the direction of building
	}
}
