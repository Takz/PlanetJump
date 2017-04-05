using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateBehaviour : MonoBehaviour {

    public GameObject bullet, player;
    public int bulletForce;

    public static bool pirateOnScreen;
    private Vector3 playerPos;
    private int bulletSpeed;

	// Use this for initialization
	void Start () {
        InvokeRepeating("PlayerLock", 1, 4);
        InvokeRepeating("ProjectileFire", 1.25f, 4);

    }

    // Update is called once per frame
    void Update () {
        PlayerOnScreen();
	}

    void PlayerLock()
    {
        if (pirateOnScreen)
        {
            playerPos = player.transform.position;
            print("Lock");
        }
    }

    void ProjectileFire()
    {
        if (pirateOnScreen)
        {
            GameObject missile = Instantiate(bullet, transform.position, transform.rotation);
            Rigidbody missileRB = missile.GetComponent<Rigidbody>();
            missileRB.AddForce((playerPos - transform.position).normalized * bulletForce, ForceMode.Impulse);
            print("Fire");
        }
    }

    private void PlayerOnScreen()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);

        pirateOnScreen = screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

        if (pirateOnScreen)
        {
            print("I am on screen");
        }
    }
}
