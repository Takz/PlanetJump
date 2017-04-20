using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHolder : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = new Vector3(Camera.main.transform.position.x + 8f, Camera.main.transform.position.y - 10f, transform.position.z);

	}
}
