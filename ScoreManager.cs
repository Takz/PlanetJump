using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

	private int planetsJumped, coinCount;
	private float startDistance, startPosition, distanceTravelled;
	GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player").gameObject;
		planetsJumped = 0;
		coinCount = 0;
		startPosition = player.transform.position.y;
		startDistance = startPosition - startPosition;
		distanceTravelled = startDistance;
	}
	
	// Update is called once per frame
	void Update () {
		//print("Planets: " + planetsJumped + "Distance: " + distanceTravelled);
	}

	public int AddToPlanetsJumped
	{

		get
		{
			return planetsJumped;
		}

		set
		{
			planetsJumped += value;
		}
	}

	public float AddToDistance
	{
		get
		{
			return distanceTravelled;
		}
		set
		{
			distanceTravelled = value + startPosition;
		}
	}

	public int AddToCoin
	{
		get
		{
			return coinCount;
		}
		set
		{
			coinCount += value;
		}
	}
}
