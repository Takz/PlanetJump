using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAsteroid : MonoBehaviour {

    GameObject ice,asteroid, pirate;
    float asteroidRotationSpeed = 10f, layer1Speed = 10f, layer2Speed = 7.5f, offScreenSpeed = 14f;
    Vector3 localStartPos;

	// Use this for initialization
	void Start () {

        if (transform.parent.name == "BackgroundIceAsteroids")
        {
            ice = this.gameObject;
            
        }
        else if (transform.parent.name == "BackgroundPirates")
        {
            pirate = this.gameObject;
        }
        else
        {
            asteroid = this.gameObject;
        }

        localStartPos = transform.localPosition;

        if(localStartPos.z > -17f)
        {
            layer1Speed = layer2Speed;
        }

        print(layer1Speed);

    }
	
	// Update is called once per frame
	void Update ()
    {
        MainBackgroundObjectLogic();

        ResetAteroidPositionWhenBelowScreen();

        if (this.gameObject != pirate)
            transform.Rotate(transform.rotation.x * Time.deltaTime * asteroidRotationSpeed, transform.rotation.y * Time.deltaTime * asteroidRotationSpeed, transform.rotation.z * Time.deltaTime * asteroidRotationSpeed);
    }

    private void MainBackgroundObjectLogic()
    {
        if (StageManager.IsStage(StageManager.Stage.Ice))
        {
            AsteroidStageChangeBehaviour(StageManager.Stage.Ice, ice);
        }
        else
            ObjectSpeedOffScreen(ice);

        if (StageManager.IsStage(StageManager.Stage.AsteroidField))
        {
            AsteroidStageChangeBehaviour(StageManager.Stage.AsteroidField, asteroid);
        }
        else
            ObjectSpeedOffScreen(asteroid);

        if (StageManager.IsStage(StageManager.Stage.Pirates))
        {
            AsteroidStageChangeBehaviour(StageManager.Stage.Pirates, pirate);
        }
        else
            ObjectSpeedOffScreen(pirate);
    }

    private void AsteroidStageChangeBehaviour(StageManager.Stage stage, GameObject obj)
    {
        if (StageManager.IsStage(stage) && obj != null)
        {
            obj.transform.position = new Vector3(transform.position.x, transform.position.y - Time.deltaTime * layer1Speed, transform.position.z);
        }       
    }

    private void ObjectSpeedOffScreen(GameObject obj)
    {
        if(obj != null && transform.localPosition.y < localStartPos.y)
        obj.transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - Time.deltaTime * 19f, transform.localPosition.z);
    }

    private void AsteroidStageChangeBehaviour()
    {
        if (StageManager.IsStage(StageManager.Stage.Ice) && ice != null)
        {
            ice.transform.position = new Vector3(transform.position.x, transform.position.y - Time.deltaTime * layer1Speed, transform.position.z);
        }
        else if (StageManager.IsStage(StageManager.Stage.AsteroidField) && asteroid != null)
        {
            asteroid.transform.position = new Vector3(transform.position.x, transform.position.y - Time.deltaTime * layer1Speed, transform.position.z);
        }
        else if (StageManager.IsStage(StageManager.Stage.Pirates) && pirate != null)
        {
            pirate.transform.position = new Vector3(transform.position.x, transform.position.y - Time.deltaTime * layer1Speed, transform.position.z);
        }
        else if (transform.localPosition.y < localStartPos.y)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - Time.deltaTime * offScreenSpeed, transform.localPosition.z);
        }
    }

    private void ResetAteroidPositionWhenBelowScreen()
    {
        if (OnScreen.ObjectBelowScreen(gameObject, true))
        {
            transform.localPosition = new Vector3(transform.localPosition.x, localStartPos.y, transform.localPosition.z);
        }
    }
}
