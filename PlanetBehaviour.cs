using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetBehaviour : MonoBehaviour {

    [SerializeField]
    private float rotationSpeed = 1f;
    GameObject rotator;

	// Use this for initialization
	void Start () {
        rotator = GameObject.FindObjectOfType<UIRotator>().gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        if (transform.FindChild("Player"))
        {
            //Transform playerChild = transform.FindChild("Player");
            
            rotator.transform.parent = transform.parent;
            rotator.transform.position = transform.parent.position;
            transform.Rotate(0, 0, Time.deltaTime * rotationSpeed * PlayerBehaviour.boostSpeed * PlayerBehaviour.rotationDirection);
            print("Player is at new position: " + transform.position);
        }
        else
        {

        }
	}
}
