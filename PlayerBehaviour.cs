using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {

    public static int boostSpeed = 2;
    public static int rotationDirection;
    public int orbitOffset, force = 100;
    public float rotationSpeed = 10f;
    public LayerMask layerMask;
    public static Vector3 cameraPos;
    public bool boost = false;
    private float inputTime;
    private int invertDirection = 1;

    Ray rayUp;
    RaycastHit rayHit;
    Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        RayCasting();

        UserInput();

    }

    private void UserInput()
    {
        if (inputTime > 10f)
        {
            boost = true;

        }
        else if (inputTime >= 0f && inputTime < 20f)
        {
            boost = false;
            boostSpeed = 1;
        }

        if (Input.GetKey(KeyCode.Space) && boost)
        {
            boostSpeed = 2;
        }
        else if (Input.GetKeyUp(KeyCode.Space) && !boost)
        {
            rb.AddForce(transform.up * force * invertDirection, ForceMode.Force);
            if (!GameState.IsRunning)
            {
                GameState.ChangeState(GameState.States.Running);
            }
        }

        if (Input.GetKey(KeyCode.Space))
        {
            inputTime += Time.deltaTime * 60f;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            inputTime = 0;
        }
    }

    public void RayCasting()
    {
        rayUp = new Ray(transform.position, transform.up);      

       
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Coin")
        {
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Rotator")
        {
            //The position of the planet
            cameraPos = other.transform.position;

            //TODO, gradually reduce velocity, rather than instantly stop
            rb.velocity = new Vector3(0, 0, 0);

            //TODO Check transform up against Vector up, base the whether orbit is clockwise or vice versa on this

            float angleBetweenPlayerAndWorld = Vector3.Angle(transform.up, other.transform.up);
            Vector3 crossProduct = Vector3.Cross(transform.up, other.transform.up);

            if ((crossProduct.z < 0 && transform.position.y < other.transform.position.y - 1f) || (crossProduct.z < 0 && transform.position.y > other.transform.position.y - 1f && transform.position.x < other.transform.position.x))
            {
                angleBetweenPlayerAndWorld = -angleBetweenPlayerAndWorld;
            }

            if (angleBetweenPlayerAndWorld < 0) //&& transform.position.x > other.transform.position.x && transform.position.y > other.transform.position.y - 2f)
            {
                rotationDirection = -1;
                print("Left");
            }
            else
                rotationDirection = 1;
            print("Right");
        }

    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Rotator")
        {
            transform.parent = collision.gameObject.transform;

            if(Physics.Raycast(transform.position, transform.right*-1, out rayHit, 500f) || Physics.Raycast(transform.position, transform.right, out rayHit, 500f))
            {
               
                if (rayHit.transform.tag == "Planet" || rayHit.transform.tag == "Bonus Planet")
                {

                }              
            }
            else
                transform.Rotate(0, 0, Time.smoothDeltaTime * rotationSpeed * rotationDirection *-1);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        transform.parent = null;
    }
}
