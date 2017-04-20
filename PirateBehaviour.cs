using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateBehaviour : MonoBehaviour {

    public GameObject bullet, player, pirateChild;
    public int bulletForce, pirateSpeed = 3;

    public static bool pirateOnScreen;
    private Vector3 playerPos;
    private int bulletSpeed;
    private float pirateXPos;

	// Use this for initialization
	void Start () {
        InvokeRepeating("PlayerLock", 1, 3);
        InvokeRepeating("ProjectileFire", 1.25f, 3f);
        InvokeRepeating("SwitchXAxisPos", 1f, 4f);
        pirateChild = transform.FindChild("Pirate").gameObject;

        pirateXPos = 15.7f;
    }

    // Update is called once per frame
    void Update ()
    {
        PlayerOnScreen();
        PirateMovement();
    }

    private void PirateMovement()
    {
        pirateChild.transform.LookAt(player.transform.position);

        transform.localPosition = new Vector3(Mathf.MoveTowards(transform.localPosition.x, pirateXPos, Time.deltaTime * 5) , transform.localPosition.y - Time.deltaTime * pirateSpeed, transform.localPosition.z);
    }

    void SwitchXAxisPos()
    {
        pirateXPos = -pirateXPos;

    }

    void PlayerLock()
    {
        if (pirateOnScreen)
        {
            playerPos = player.transform.position;

        }
    }

    void ProjectileFire()
    {
        if (pirateOnScreen)
        {
            GameObject missile = Instantiate(bullet, transform.position, transform.rotation);
            Rigidbody missileRB = missile.GetComponent<Rigidbody>();
            missileRB.AddForce((playerPos - transform.position).normalized * bulletForce, ForceMode.Impulse);
        }
    }

    private void PlayerOnScreen()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);

        pirateOnScreen = screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

        if (pirateOnScreen)
        {
            //print("I am on screen");
        }
    }
}
