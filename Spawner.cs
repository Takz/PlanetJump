﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public List<GameObject> planets = new List<GameObject>();
    public List<GameObject> planetPoints;
    public GameObject planet, coin;
    public int planetDistance, distanceBelowCamForMovePos = 25, widthDisBetweenPlanets = 20, maxCoins = 4;
    public MeshFilter[] meshes;
    public Material mat;
    private Vector3 planetPosition;
    private int numberOfPlanets = 4, numberOfCoins;
    private bool lastPlanetInListPassed = false;

	// Use this for initialization
	void Start ()
    {
        MainGameSpawnLoop();

    }

    private void MainGameSpawnLoop()
    {
        for (int i = 0; i < numberOfPlanets; i++)
        {
            planetPosition = new Vector3(Random.Range(planets.Last<GameObject>().transform.position.x - 10, planets.Last<GameObject>().transform.position.x + 10),
            planets.Last<GameObject>().transform.position.y + planetDistance, 0);
            GameObject newPlanet = Instantiate(planet, planetPosition, transform.rotation);
            RandomMesh(newPlanet);
            RandomPlanetScale(1, 1, newPlanet);
            planets.Add(newPlanet);

            BonusPlanet(newPlanet);
        }

        planetPoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("Planet").OrderBy((c) => c.transform.position.y));

        for (int numberOfCoins = 0; numberOfCoins < maxCoins; numberOfCoins++)
        {
            Instantiate(coin, planetPoints[numberOfCoins].transform.position * 0.5f + planetPoints[numberOfCoins + 1].transform.position * 0.5f, transform.rotation);

        }
    }

    private void BonusPlanet(GameObject newPlanet)
    {
        float randomRoll = Random.Range(0, 100);

        if (randomRoll > 50)
        {
            int inversePlanetPos = 1;
            if (newPlanet.transform.position.x > 0)
            {
                inversePlanetPos = -inversePlanetPos;
            }

            Vector3 planetPosX = new Vector3(Random.Range(newPlanet.transform.position.x + 15f, newPlanet.transform.position.x + 30f), newPlanet.transform.position.y);
            GameObject bonusPlanet = Instantiate(planet, planetPosX * inversePlanetPos, transform.rotation);
            bonusPlanet.name = "Bonus Planet";
        }
    }

    // Update is called once per frame
    void Update () {

		foreach(GameObject planet in planets.ToList<GameObject>())
        {
            if(planet.transform.position.y < Camera.main.transform.position.y - distanceBelowCamForMovePos)
            {
                planet.transform.position = planetPosition = new Vector3(Random.Range(-widthDisBetweenPlanets, widthDisBetweenPlanets), planets.Last<GameObject>().transform.position.y + planetDistance, 0);
                RandomPlanetScale(1, 1, planet);
                RandomMesh(planet);
                planets.Add(planet);

                BonusPlanet(planet);
            }


        }

        //if (Camera.main.transform.position.y > planetPoints.Last<GameObject>().transform.position.y && !lastPlanetInListPassed)
        //{
        //    planetPoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("Planet").OrderBy((c) => c.transform.position.y));

        //    from = 0;
        //    numberOfCoins = 0;

        //    for (int numberOfCoins = 0; numberOfCoins < maxCoins; numberOfCoins++)
        //    {
        //        Instantiate(coin, planetPoints[from].transform.position * 0.5f + planetPoints[from + 1].transform.position * 0.5f, transform.rotation);

        //        from++;

        //    }
        //    lastPlanetInListPassed = true;
        //}
        //else
        //    lastPlanetInListPassed = false;

    }

    private void RandomMesh(GameObject planet)
    {
        int meshIndex = Random.Range(0, meshes.Length);
        MeshFilter randomMesh = meshes[meshIndex];
        planet.GetComponent<MeshFilter>().sharedMesh = randomMesh.sharedMesh;
        planet.GetComponent<MeshRenderer>().material = mat;
    }

    private void RandomPlanetScale(int min, int max, GameObject planet)
    {
        int randomPlanetScale = Random.Range(min, max);
        planet.transform.localScale = new Vector3(randomPlanetScale, randomPlanetScale, randomPlanetScale);
    }


}