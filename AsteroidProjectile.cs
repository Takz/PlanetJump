using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidProjectile : MonoBehaviour {

    bool moving = false;
    Vector3 startLocation;
    float asteroidSpeed;
    int directionInvert = -1;
    Rigidbody rb;
    SphereCollider sc;

    // Use this for initialization
    void Start () {
        startLocation = transform.localPosition;
        rb = GetComponent<Rigidbody>();
        sc = GetComponent<SphereCollider>();
        InvokeRepeating("AsteroidSpawn", 1f, 3f);
    }
	
	// Update is called once per frame
	void Update () {
        if (moving && !OnScreen.ObjectOnScreen(this.gameObject, true))
        {
            //transform.position = new Vector3(transform.position.x + Time.deltaTime * asteroidSpeed * directionInvert, transform.position.y, 0);
            this.rb.AddForce(asteroidSpeed * directionInvert, 0, 0, ForceMode.Acceleration);
            transform.Rotate(transform.rotation.x * Time.deltaTime * 20f, transform.rotation.y * Time.deltaTime * 20f, transform.rotation.z * Time.deltaTime * 20f);
            sc.enabled = true;

        }
        else if(!moving)
            this.transform.position = new Vector3(Camera.main.transform.position.x + 30f * directionInvert  , transform.position.y, 0);
            //this.transform.localPosition = new Vector3(transform.localPosition.x, startLocation.y, transform.localPosition.z);
            
    }

    void AsteroidSpawn()
    {
        if (StageManager.IsStage(StageManager.Stage.AsteroidField))
        {               
            if (!OnScreen.ObjectOnScreen(this.gameObject, true))
            {
                if (OnScreen.ObjectLeftOfScreen(this.gameObject, true) && !moving)
                {
                    directionInvert = 1;

                }

                if (OnScreen.ObjectRightOfScreen(this.gameObject, true) && !moving)
                {
                    directionInvert = -1;
                }

                int randomRoll = Random.Range(0, 100);

                if (randomRoll > 40 && !moving)
                {
                    
                    asteroidSpeed = 4f;
                    moving = true;

                }
                else
                {
                    this.rb.velocity = new Vector3(0, 0, 0);
                    asteroidSpeed = 0;
                    sc.enabled = false;
                    moving = false;
                }
            }
        }
    }
}
