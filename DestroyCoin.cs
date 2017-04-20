using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCoin : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(OnScreen.ObjectBelowScreen(this.gameObject, true) && transform.position.y > -30f)
        {
            Destroy(gameObject, 3f);
        }
	}
}
