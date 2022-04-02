using System.Collections;
using System.Collections.Generic;
//using UnityEngine;
using SharpDX;

public class SC_AI_Start //: MonoBehaviour
{
    SC_AI[] SC_AI = new SC_AI[10];

    //public Transform drone;
    //public Transform player;

    float speed = 0.0001f;


    Matrix playerpos = Matrix.Identity;
    Matrix dronepos = Matrix.Identity;

    void Start()
    {
        for (int i = 0;i < SC_AI.Length;i++) // using 10 instances of SC_AI
        {
            SC_AI[i] = new SC_AI(playerpos, dronepos);
        }
    }

    int totalRight = 0;
    int totalLeft = 0;

    void Update()
    {
        totalRight = 0;
        totalLeft = 0;

        for (int i = 0; i < SC_AI.Length; i++)
        {
            SC_AI[i].UpdatePerceptron();
            totalRight += SC_AI[i]._guessedCorrectRight;
            totalLeft += SC_AI[i]._guessedCorrectLeft;
            speed = SC_AI[i]._dotGoal;
        }

        if (totalRight > totalLeft)
        {
            //Debug.Log("player is RIGHt 0-0");
            //drone.transform.Rotate(new Vector3(0, 0, -15f * Mathf.Abs(speed)), Space.World);
        } 
        else if(totalRight < totalLeft)
        {
            //Debug.Log("player is LEFT 0-0");
            //drone.transform.Rotate(new Vector3(0, 0, 15f * Mathf.Abs(speed)), Space.World);
        }
    }
}
