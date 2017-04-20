using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarBehaviour : MonoBehaviour {

    private ParticleSystem ps;

	// Use this for initialization
	void Start () {
        ps = GetComponent<ParticleSystem>();

	}
	
	// Update is called once per frame
	void Update () {
        var main = ps.main;
        //main.startColor = new Color(hSliderValueR, hSliderValueG, hSliderValueB, hSliderValueA);

        if (StageManager.IsStage(StageManager.Stage.Ice))
        {
            main.startColor = new Color(84f/255f, 121f/255f, 255f/255f, 84f/255f);

        }

        if (StageManager.IsStage(StageManager.Stage.Start))
        {
            main.startColor = new Color(255f/255f, 170f/255f, 105f/255f, 84f/255f);

        }

        if (StageManager.IsStage(StageManager.Stage.AsteroidField))
        {
            main.startColor = new Color(188f / 255f, 23f / 255f, 23f / 255f, 84f / 255f);

        }

        if (StageManager.IsStage(StageManager.Stage.Pirates))
        {
            main.startColor = new Color(124f / 255f, 26f / 255f, 0f / 255f, 84f / 255f);

        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            StageManager.ChangeStage(StageManager.Stage.AsteroidField);
        }
    }
}
