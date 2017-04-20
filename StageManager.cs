using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {

    public enum Stage
    {
        Start, Ice, AsteroidField, Pirates// CloseToSun, IonStorm,
    }

    public static Stage stageState = Stage.Start;

    private void Start()
    {
        stageState = Stage.Start;
    }

    public static void ChangeStage(Stage stageTo)
    {
        if (stageState == stageTo)
            return;
        stageState = stageTo;
    }

    public static bool IsStage(Stage stage)
    {
        if (stage == stageState)
            return true;
        else
            return false;
    }

    //Generic class for selecting a random Enum
    public static T GetRandomEnum<T>()
    {
        //Establish an array that takes the values from the enum in question
        System.Array A = System.Enum.GetValues(typeof(T));
        //The generic type then randomly selects the values from the array
        T V = (T)A.GetValue(UnityEngine.Random.Range(0, A.Length));
        //returns the values from the array
        return V;
    }
}
