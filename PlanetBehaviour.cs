using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetBehaviour : MonoBehaviour {

    public GameObject coin, coinSet;

    [SerializeField]
    private float rotationSpeed = 1f;
    GameObject rotator;
    bool coinSpawned, coinSetSpawned = false;
    Vector3 posWhereCoinSpawned;


	// Use this for initialization
	void Start () {
        coin = GameObject.FindGameObjectWithTag("Coin");
        coinSet = GameObject.FindGameObjectWithTag("CoinSet");
        rotator = GameObject.FindObjectOfType<UIRotator>().gameObject;
    }
	
	// Update is called once per frame
	void Update ()
    {
        RotatePlanetIfPlayerIsChild();

        RayCastAndInstantiateCoins();
    }


    private void RayCastAndInstantiateCoins()
    {
        if(transform.position.y > posWhereCoinSpawned.y)
        {
            coinSpawned = false;
        }

        Ray ray = new Ray(transform.position, transform.up);

        Quaternion q = Quaternion.AngleAxis(Time.time * 500f, Vector3.forward);
        Vector3 direction = Vector3.up;
        direction = q * direction;
        RaycastHit hit;

        Debug.DrawRay(transform.position, direction, Color.blue, 200f);

        if (Physics.Raycast(transform.position, direction, out hit, 30f) && !coinSpawned)
        {
            if ((hit.transform.tag == "Planet" || hit.transform.tag == "Bonus Planet") && hit.transform.position.y > transform.position.y)
            {
                hit = InstantiateCoins(hit, q);
                coinSpawned = true;
            }
        }
    }

    private RaycastHit InstantiateCoins(RaycastHit hit, Quaternion direction)
    {
        if(hit.transform.tag == "Bonus Planet")
        {
            coin = coinSet;
        }

        Instantiate(coin, transform.position * 0.5f + hit.transform.position * 0.5f, direction);
        posWhereCoinSpawned = transform.position;
        return hit;
    }

    private void RotatePlanetIfPlayerIsChild()
    {
        if (transform.FindChild("Player"))
        {
            //rotator.transform.parent = transform.parent;
            //rotator.transform.position = transform.parent.position;
            transform.Rotate(0, 0, Time.deltaTime * rotationSpeed * PlayerBehaviour.boostSpeed * PlayerBehaviour.rotationDirection);

        }
        else
        {

        }
    }
}
