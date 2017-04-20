using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CameraBehaviour : MonoBehaviour {

    Transform target;
    public float cameraSpeed, camToPlayerDistance, camSmooth;

    GameObject player;
    Spawner spawn;

    // Use this for initialization
    IEnumerator Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        spawn = FindObjectOfType<Spawner>().GetComponent<Spawner>();
        yield return new WaitForEndOfFrame();
    }
    
    // Update is called once per frame
    void FixedUpdate ()
    {
        camToPlayerDistance = player.transform.position.y - transform.position.y;

        if (player.gameObject.transform.parent)
        {
            if (player.gameObject.transform.parent.name == "Planet Rotate")
            {
                target = player.gameObject.transform.parent;
            }
 
        }
        else
            target = player.transform;

        CameraBehaviourWhenGameIsRunning();

        PlayerOnScreen();
    }

    private void CameraBehaviourWhenGameIsRunning()
    {
        if (GameState.IsRunning)
        {
            //TODO look at ways to make the camera y axis better once the player has approached the top of the screen

            if (camToPlayerDistance > 20f) // && player.transform.parent == null)
            {
                transform.position = new Vector3(Mathf.SmoothStep(transform.position.x, target.transform.position.x, Time.smoothDeltaTime * camSmooth), Mathf.SmoothStep(transform.position.y, target.transform.position.y, Time.smoothDeltaTime * camSmooth * 0.5f), transform.position.z);
            }

            transform.position = new Vector3(Mathf.SmoothStep(transform.position.x, target.transform.position.x, Time.smoothDeltaTime * camSmooth), transform.position.y + Time.smoothDeltaTime * cameraSpeed, transform.position.z);

        }
    }

    private void PlayerOnScreen()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(target.position);

        bool onScreen = screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

        if (!onScreen)
        {
            GameState.ChangeState(GameState.States.GameOver);

        }
    }

    public float AddToCameraSpeed
    {
        get
        {
            return cameraSpeed;
        }

        set
        {
            cameraSpeed += value;
        }
    }
}
