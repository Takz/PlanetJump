using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidHolder : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x, Camera.main.transform.position.y + 130f, transform.position.z);
	}
}
