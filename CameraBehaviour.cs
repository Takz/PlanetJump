using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CameraBehaviour : MonoBehaviour {

    public Transform target;
    public float cameraSpeed;

    Spawner spawn;

    // Use this for initialization
    IEnumerator Start () {
        spawn = FindObjectOfType<Spawner>().GetComponent<Spawner>();
        yield return new WaitForEndOfFrame();
    }
	
	// Update is called once per frame
	void Update ()
    {
        CameraBehaviourWhenGameIsRunning();

        PlayerOnScreen();

    }

    private void CameraBehaviourWhenGameIsRunning()
    {
        if (GameState.IsRunning)
        {
            if (PlayerBehaviour.cameraPos.y > transform.position.y)
            {
                transform.position = Vector3.MoveTowards(transform.position, PlayerBehaviour.cameraPos + transform.position, cameraSpeed * Time.smoothDeltaTime);
            }
        }
    }

    private void PlayerOnScreen()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(target.position);

        bool onScreen = screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

        if (!onScreen)
        {
            GameState.ChangeState(GameState.States.GameOver);
            print("GameOver");
        }
    }
}
