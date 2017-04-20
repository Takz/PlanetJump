using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {

    public static PlayerBehaviour instance = null;
    public static int boostSpeed = 2;
    public static int rotationDirection;
    public int orbitOffset, force = 100, scoreThreshold = 150;
    public float rotationSpeed = 10f, gravityStrength = 0.25f;
    public LayerMask layerMask;
    public static Vector3 cameraPos;
    public bool boost = false, deccelerationComplete = false, changeStage = false;
    private float inputTime, speedToDeccelerateTo = 0.5f;
    private int invertDirection = 1, lineLength = 10;
    private LineRenderer line;
    private Vector3 enterOrbitVelocity, fixedPositionOnOrbit;

    public Transform target;
    public float dirNum;

    ScoreManager sm;
    Ray rayUp;
    RaycastHit rayHit;
    Rigidbody rb;
    CameraBehaviour camera;
    IceAsteroidProjectile iceAst;

    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start () {
        sm = GameObject.FindObjectOfType<ScoreManager>().GetComponent<ScoreManager>();
        rb = GetComponent<Rigidbody>();
        line = GetComponent<LineRenderer>();
        camera = GameObject.FindObjectOfType<CameraBehaviour>().GetComponent<CameraBehaviour>();
        iceAst = GameObject.FindObjectOfType<IceAsteroidProjectile>().GetComponent<IceAsteroidProjectile>();
        scoreThreshold = 150;


    }
	
	// Update is called once per frame
	void Update ()
    {
        IncrementScoreThresholdAndCameraSpeed();

        ChangeStage();

        if (!GameState.IsGameOver)
        {
            UserInput();
        }

        RayCasting();

        LineRender();
    }

    private void ChangeStage()
    {
        if (changeStage)
        {
            StageManager.Stage stage = StageManager.GetRandomEnum<StageManager.Stage>();
            if (stage == StageManager.stageState)
            {
                return;
            }

            StageManager.ChangeStage(stage);
            changeStage = false;
        }
    }

    private void IncrementScoreThresholdAndCameraSpeed()
    {
        if (sm.AddToDistance > scoreThreshold)
        {
            scoreThreshold += 150;
            iceAst.AddToAsteroidSpeed = 2.5f;
            print("Increasing speed" + "Score Threshold: " + scoreThreshold + "Camera Speed: " + camera.AddToCameraSpeed);
            camera.AddToCameraSpeed = 0.5f;
            changeStage = true;
        }
    }

    float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up)
    {
        Vector3 perp = Vector3.Cross(fwd, targetDir);

        float dir = Vector3.Dot(perp, up);
 
        if (dir > 0f)
        {
            return 1f;
        }
        else if (dir < 0f)
        {
            return -1f;
        }
        else
        {
            return 0f;
        }
    }

    private void LineRender()
    {
        line.SetPosition(0, transform.position);
        line.SetPosition(1, transform.position + transform.up * lineLength);

    }

    private void UserInput()
    {
        if (inputTime > 15f)
        {
            boost = true;

        }
        else if (inputTime >= 0f && inputTime < 25f)
        {
            boost = false;
            boostSpeed = 1;
        }

        if ((Input.GetKey(KeyCode.Space) || Input.GetButton("Fire1")) && boost)
        {
            boostSpeed = 2;
        }
        else if ((Input.GetKeyUp(KeyCode.Space) || Input.GetButtonUp("Fire1")) && !boost)
        {
            rb.AddForce(transform.up * force * invertDirection, ForceMode.Acceleration);
            if (!GameState.IsRunning)
            {
                GameState.ChangeState(GameState.States.Running);
            }
        }

        if ((Input.GetKey(KeyCode.Space) || Input.GetButton("Fire1")))
        {
            inputTime += Time.deltaTime * 60f;
        }
        else if ((Input.GetKeyUp(KeyCode.Space) || Input.GetButtonUp("Fire1")))
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
        if(other.gameObject.tag == "Bullet")
        {
            GameState.ChangeState(GameState.States.GameOver);
        }

        if(other.gameObject.tag == "Coin")
        {
            sm.AddToCoin = +1;
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Rotator")
        {

            sm.AddToPlanetsJumped = +1;
            sm.AddToDistance = transform.position.y;
            //The position of the planet
            cameraPos = other.transform.position;

            Vector3 heading = other.transform.position - transform.position;
            dirNum = AngleDir(transform.forward, heading, transform.up);

            if (dirNum > 0)
            {
                rotationDirection = -1;

            }
            else
            {
                rotationDirection = 1;

            }

            // Once in planet near Orbit, reduce velocity to 0 and check decceleration complete
            rb.velocity = new Vector3(0, 0, 0);
            deccelerationComplete = true;
            fixedPositionOnOrbit = transform.position;

            line.enabled = true;
        }

        if (other.gameObject.tag == "Slow")
        {
            // Add the velocity to a variable
            enterOrbitVelocity = rb.velocity;
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, Time.deltaTime * 25f);
            rb.AddForce((other.gameObject.transform.position - transform.position) * gravityStrength, ForceMode.Acceleration);
        }

    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Rotator")
        {
            transform.parent = collision.gameObject.transform;            

            if (Physics.Raycast(transform.position, transform.right*-1, out rayHit, 7.5f) || Physics.Raycast(transform.position, transform.right, out rayHit, 7.5f))
            {
               
                if (rayHit.transform.tag == "Planet" || rayHit.transform.tag == "Bonus Planet")
                {
                    transform.position = fixedPositionOnOrbit;
                }              
            }
            else 
                transform.Rotate(0, 0, Time.smoothDeltaTime * rotationSpeed * rotationDirection *-1);
        }

        if(collision.gameObject.tag == "Slow" && !deccelerationComplete)
        {
            Vector3 currentVelocity = rb.velocity;
            //TODO Do some work here to create a gravitational pull, perhaps use move towards instead of velocity

            //The velocity is reduce towards 0 in outer orbit
            //transform.position = new Vector3(Mathf.Lerp(transform.position.x,  collision.transform.position.x, Time.deltaTime * gravityStrength), Mathf.Lerp(transform.position.y, collision.transform.position.y, Time.deltaTime * gravityStrength),
            //    Mathf.Lerp(currentVelocity.z, currentVelocity.z - enterOrbitVelocity.z, Time.deltaTime * gravityStrength));

            //rb.AddForce(collision.gameObject.transform.position - transform.position * gravityStrength, ForceMode.Acceleration);

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
