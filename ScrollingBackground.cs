using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour {

    Renderer mr;

	// Use this for initialization
	void Start () {
        mr = GetComponent<Renderer>();

        
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x, transform.position.y - Time.deltaTime * 0.5f, transform.position.z);

        if(transform.localPosition.y < -133)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, 133, transform.localPosition.z);
        }

        DynamicGI.SetEmissive(mr, new Color(0.145f, 0.274f, 0.58f) * 1f);
    }
}
