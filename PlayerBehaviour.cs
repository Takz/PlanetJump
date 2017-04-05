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
    public bool boost = false, deccelerationComplete = false;
    private float inputTime, deccelerationRate = 2f, speedToDeccelerateTo = 0.5f;
    private int invertDirection = 1, lineLength = 20;
    private LineRenderer line;
    private Vector3 enterOrbitVelocity;

    Ray rayUp;
    RaycastHit rayHit;
    Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        line = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        RayCasting();

        UserInput();

        LineRender();

        print(rotationDirection);
    }

    private void LineRender()
    {
        line.SetPosition(0, transform.position);
        line.SetPosition(1, transform.position + transform.up * lineLength);

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

            //Calculate angle between up of player and Vector up of world
            float angleBetweenPlayerAndWorld = Vector3.Angle(transform.up, Vector3.up);
            //Cross product between them
            Vector3 crossProduct = Vector3.Cross(transform.up, Vector3.up);

            //If the cross product on z is less than 0, the player is at a negative angle, below 0
            if (crossProduct.z < 0) 
            {
                angleBetweenPlayerAndWorld = -angleBetweenPlayerAndWorld;
            }
            
            if (rb.velocity.x < 0 && angleBetweenPlayerAndWorld < 0)
            {
                rotationDirection = -1;
                print("Left");
            }
            else
            {
                rotationDirection = 1;
                print("Right");
            }

            // Once in planet near Orbit, reduce velocity to 0 and check decceleration complete
            rb.velocity = new Vector3(0, 0, 0);
            deccelerationComplete = true;

            line.enabled = true;
        }

        if (other.gameObject.tag == "Slow")
        {
            // Add the velocity to a variable
            enterOrbitVelocity = rb.velocity;
        }

    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Rotator")
        {
            transform.parent = collision.gameObject.transform;            

            if (Physics.Raycast(transform.position, transform.right*-1, out rayHit, 50f) || Physics.Raycast(transform.position, transform.right, out rayHit, 50f))
            {
               
                if (rayHit.transform.tag == "Planet" || rayHit.transform.tag == "Bonus Planet")
                {
                    
                }              
            }
            else 
                transform.Rotate(0, 0, Time.smoothDeltaTime * rotationSpeed * rotationDirection *-1);
        }

        if(collision.gameObject.tag == "Slow" && !deccelerationComplete)
        {
            Vector3 currentVelocity = rb.velocity;

            //The velocity is reduce towards 0 in outer orbit
            rb.velocity = new Vector3(Mathf.Lerp(currentVelocity.x,  currentVelocity.x - currentVelocity.x, Time.deltaTime * deccelerationRate), Mathf.Lerp(currentVelocity.y, enterOrbitVelocity.y - currentVelocity.y, Time.deltaTime * deccelerationRate),
                Mathf.Lerp(currentVelocity.z, currentVelocity.z - enterOrbitVelocity.z, Time.deltaTime * deccelerationRate));

        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        transform.parent = null;

        if(other.gameObject.tag == "Slow")
        {
            deccelerationComplete = false;
        }

        line.enabled = false;
    }
}
