using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMesh : MonoBehaviour {

    public int rotateSpeed = 10;

    MeshRenderer mesh;

	// Use this for initialization
	void Start () {
        mesh = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        //mesh.transform.rotation = Quaternion.Euler(transform.rotation.x + Time.deltaTime * rotateSpeed, transform.rotation.y + Time.deltaTime * rotateSpeed, transform.rotation.z + Time.deltaTime * rotateSpeed);
        transform.Rotate(Vector3.forward * Time.deltaTime * rotateSpeed);
	}
}
