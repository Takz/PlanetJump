using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindTrailRender : MonoBehaviour {

    public TrailRenderer trail;

	// Use this for initialization
	void Start () {
        GameObject findChild = transform.FindChild("TrailRotator").gameObject;
        GameObject findNextChild = findChild.transform.FindChild("Trail").gameObject;

        trail = findNextChild.GetComponent<TrailRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
