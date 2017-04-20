using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public List<GameObject> planets = new List<GameObject>();
    public List<GameObject> bonusPlanets = new List<GameObject>();
    public List<GameObject> planetPoints;
    public GameObject planet, coin, pirate;
    public int numberOfPlanets = 4, planetDistance, distanceBelowCamForMovePos = 25, widthDisBetweenPlanets = 20, maxCoins = 4;
    public MeshFilter[] meshes;
    public Material mat;
    private Vector3 planetPosition;
    private TrailRenderer tempTrail, bonusTempTrail;
    private int numberOfCoins;
    private bool lastPlanetInListPassed = false;

	// Use this for initialization
	void Start ()
    {
        StageManager.ChangeStage(StageManager.Stage.Start);

        MainGameSpawnLoop();

    }

    private void MainGameSpawnLoop()
    {
        for (int i = 0; i < numberOfPlanets; i++)
        {
            planetPosition = new Vector3(Random.Range(planets.Last<GameObject>().transform.position.x - 10, planets.Last<GameObject>().transform.position.x + 10),
            planets.Last<GameObject>().transform.position.y + planetDistance, 0);
            GameObject newPlanet = Instantiate(planet, planetPosition, transform.rotation);
            MeshAndPlanetDistance(newPlanet);
            RandomPlanetScale(1, 2, newPlanet);
            planets.Add(newPlanet);

            int inversePlanetPos = 1;
            if (newPlanet.transform.position.x > 0)
            {
                inversePlanetPos = -inversePlanetPos;
            }

            Vector3 planetPosX = new Vector3(Random.Range(newPlanet.transform.position.x + 20f * inversePlanetPos, newPlanet.transform.position.x + 30f * inversePlanetPos), newPlanet.transform.position.y);
            GameObject bonusPlanet = Instantiate(planet, planetPosX, transform.rotation);


            GameObject childPlanet = bonusPlanet.transform.FindChild("Planet Rotate").gameObject;
            GameObject childPlanetCore = childPlanet.transform.FindChild("PlanetCore").gameObject;
            bonusPlanet.name = "Bonus Planet";
            childPlanetCore.tag = "Bonus Planet";
            bonusPlanets.Add(bonusPlanet);
        }

    }

    private void BonusPlanet(GameObject newPlanet)
    {
        foreach (GameObject bonusPlanet in bonusPlanets.ToList<GameObject>())
        {
            if (bonusPlanet.transform.position.y < Camera.main.transform.position.y - distanceBelowCamForMovePos)
            {
                //float randomRoll = Random.Range(0, 100);

                //if (randomRoll > 50)
                //{
                    int inversePlanetPos = 1;
                    if (newPlanet.transform.position.x > 0)
                    {
                        inversePlanetPos = -inversePlanetPos;
                    }


                    Vector3 planetPosX = new Vector3(Random.Range(newPlanet.transform.position.x + 20f * inversePlanetPos, newPlanet.transform.position.x + 25f * inversePlanetPos), newPlanet.transform.position.y + Random.Range(5, 10));
                    bonusPlanet.transform.position = planetPosX;
                    Rigidbody rb = bonusPlanet.GetComponent<Rigidbody>();
                    rb.velocity = Vector3.zero;
                    ResetBonusPlanetTrail(bonusPlanet);
                    //Scales the planet randomly after changing position
                    RandomPlanetScale(1, 2, bonusPlanet);
                    //Assigns random mesh
                    //TODO Random Mesh should no longer be random, spawn based on currentStage
                    MeshAndPlanetDistance(bonusPlanet);

                // }
            }
        }
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown(KeyCode.I))
        {
            StageManager.ChangeStage(StageManager.Stage.Ice);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            StageManager.ChangeStage(StageManager.Stage.Pirates);
        }

        foreach (GameObject planet in planets.ToList<GameObject>())
        {//As the planets pass below the camera
            if (planet.transform.position.y < Camera.main.transform.position.y - distanceBelowCamForMovePos)
            {
                //Change their position between a random X value assigned in the inspector, and of a Y value based on the previous planet
                planet.transform.position = planetPosition = new Vector3(Random.Range(-widthDisBetweenPlanets, widthDisBetweenPlanets), planets.Last<GameObject>().transform.position.y + planetDistance, 0);
                Rigidbody rb = planet.GetComponent<Rigidbody>();
                rb.velocity = Vector3.zero;

                //Reset their trail to avoid visual bug
                ResetPlanetTrail(planet);
                //Scales the planet randomly after changing position
                RandomPlanetScale(1, 2, planet);
                //Assigns random mesh
                //TODO Random Mesh should no longer be random, spawn based on currentStage
                MeshAndPlanetDistance(planet);
                //Add the planet to the current list, making this the last planet to be added to the list
                planets.Add(planet);
                //spawns a bonus planet on chance
                BonusPlanet(planet);

                if (StageManager.IsStage(StageManager.Stage.Pirates))
                {
                    PirateSpawning();
                }

            }
        }


    }

    private void PirateSpawning()
    {
        //TODO Recreate the logic for the pirate, try have the pirate appear at the sides of the screen for periods of time
        float randomRoll = Random.Range(0, 100);

        if (randomRoll > 40 && !PirateBehaviour.pirateOnScreen)
        {
            //pirate.transform.position = new Vector3(planets.Last<GameObject>().transform.position.x + Random.Range(20 * inversePiratePos, 15 * inversePiratePos), planets.Last<GameObject>().transform.position.y + 10f);
            float randomPiratePos = Random.Range(0, 100);
            float pirateXPos = 15.7f;
            if (randomPiratePos > 50)
            {
                pirateXPos = 15.7f;
            }
            else
                pirateXPos = -pirateXPos;

            pirate.transform.localPosition = new Vector3(pirateXPos, 38f, 24);

        }
    }

    private void ResetPlanetTrail(GameObject planetTrail)
    {
        tempTrail = planetTrail.GetComponent<FindTrailRender>().trail;
        tempTrail.enabled = false;
        Invoke("EnableTrail", 1f);
    }

    private void ResetBonusPlanetTrail(GameObject bonusPlanetTrail)
    {
        this.bonusTempTrail = bonusPlanetTrail.GetComponent<FindTrailRender>().trail;
        this.bonusTempTrail.enabled = false;
        Invoke("EnableBonusTrail", 1f);
    }

    private void EnableTrail()
    {
        this.tempTrail.enabled = true;
    }

    private void EnableBonusTrail()
    {
        this.bonusTempTrail.enabled = true;
    }

    private void MeshAndPlanetDistance(GameObject planet)
    {
        //0 Egiypt 5 Orange planet - Pirate area and close to sun / 1 and 2 and 6 Forest - State area 
        // 3 and 4 Ice
        planetDistance = Random.Range(20, 30);

        if (StageManager.IsStage(StageManager.Stage.Ice))
        {
            RandomIndexPlanetMesh(planet, 3, 4);
        }
        else if (StageManager.IsStage(StageManager.Stage.Start))
        {
            RandomIndexPlanetMesh(planet, 1, 2);
            
        }




    }

    private void RandomIndexPlanetMesh(GameObject planet, int min, int max)
    {
        //Code for random mesh generation
        int meshIndex = Random.Range(min, max);
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
