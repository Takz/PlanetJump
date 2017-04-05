using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnBullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("DestroyBullet", 3f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void DestroyBullet()
    {
        Destroy(gameObject);
    }

}
