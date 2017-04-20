using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceAsteroidProjectile : MonoBehaviour
{

    bool moving = false;
    Vector3 startLocation;
    float asteroidSpeed = 100f, xPos, distanceFromIceToCam;
    int directionInvert = 1;
    SphereCollider sc;
    MeshRenderer mr;
    Rigidbody rb;
    // Use this for initialization
    void Start()
    {
        InvokeRepeating("AsteroidSpawn", 1f, 3f);
        sc = GetComponent<SphereCollider>();
        mr = GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            //transform.position = new Vector3(xPos, transform.position.y + Time.deltaTime * asteroidSpeed * directionInvert, transform.position.z);
            if (OnScreen.ObjectBelowScreen(gameObject, false))
            {
                rb.AddForce(0, Time.deltaTime * asteroidSpeed * directionInvert, 0, ForceMode.Acceleration);
            }
            transform.Rotate(transform.rotation.x * Time.deltaTime * 20f, transform.rotation.y * Time.deltaTime * 20f, transform.rotation.z * Time.deltaTime * 20f);
            sc.enabled = true;
            mr.enabled = true;
            print("Adding Force");
        }
        else
            transform.position = new Vector3(transform.position.x, Camera.main.transform.position.y - 40f * directionInvert, transform.position.z);

        distanceFromIceToCam = Camera.main.transform.position.x - transform.position.x;

    }

    void AsteroidSpawn()
    {
        if (StageManager.IsStage(StageManager.Stage.Ice))
        {
            if (OnScreen.ObjectBelowScreen(gameObject, true))
            {
                int randomRoll = Random.Range(0, 100);

                if (randomRoll > 10 && !moving)
                {
                    asteroidSpeed = AddToAsteroidSpeed;
                    xPos = Random.Range(-17, 17);
                    transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
                    moving = true;

                }

            }

            else if (OnScreen.ObjectAboveScreen(gameObject, true) || Mathf.Abs(distanceFromIceToCam) > 30f)
            {
                sc.enabled = false;
                mr.enabled = false;
                rb.velocity = Vector3.zero;
                moving = false;
            }
        }
    }

    public float AddToAsteroidSpeed
    {
        get
        {
            return asteroidSpeed;
        }

        set
        {
            asteroidSpeed += value;
        }
    }
}
