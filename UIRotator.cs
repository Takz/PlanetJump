using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UIRotator : MonoBehaviour {

    Spawner spawner;
    GameObject player;

	// Use this for initialization
	void Start () {

        player = GameObject.FindObjectOfType<PlayerBehaviour>().gameObject;
	}
	
	// Update is called once per frame
	void Update()
    {
        UIRotation();


    }

    private void UIRotation()
    {
        float rotation = Vector3.Angle(player.transform.up, Vector3.up);

        Vector3 crossProduct = Vector3.Cross(player.transform.up, Vector3.up);

        if (crossProduct.z > 0)
        {
            rotation = -rotation;
        }

        transform.rotation = Quaternion.Euler(0, 0, rotation );

        //print("Cross: " + crossProduct + "Rotation: " + rotation);
    }
}
