using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static bool ObjectOnScreen(GameObject obj, bool onScreen)
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(obj.transform.position);

        onScreen = screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

        return onScreen;
    }

    public static bool ObjectBelowScreen(GameObject obj, bool belowScreen)
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(obj.transform.position);

        belowScreen = screenPoint.y < 0;

        return belowScreen;
    }

    public static bool ObjectAboveScreen(GameObject obj, bool aboveScreen)
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(obj.transform.position);

        aboveScreen = screenPoint.y > 1;

        return aboveScreen;
    }

    public static bool ObjectRightOfScreen(GameObject obj, bool belowScreen)
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(obj.transform.position);

        belowScreen = screenPoint.x > 1;

        return belowScreen;
    }

    public static bool ObjectLeftOfScreen(GameObject obj, bool belowScreen)
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(obj.transform.position);

        belowScreen = screenPoint.x < 0;

        return belowScreen;
    }
}
