﻿using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEngine;
using SharpDX;


using Perceptron;

namespace SCCoreSystems
{
    public class SC_AI_Start //: MonoBehaviour
    {
        public static int numberOfNeurons = 3;

        SC_AI[] SC_AI = new SC_AI[numberOfNeurons];

        //SC_AI.data_input data_input;
        //public Transform drone;
        //public Transform player;

        public static float speed = 1; //0.0001f

        //Vector3 playerpos = Vector3.Zero;// (0, 0, 0);// Matrix.Identity;
        //Vector3 dronepos = Vector3.Zero;// Matrix.Identity;

        Matrix dronepos = Matrix.Identity;
        Matrix playerpos = Matrix.Identity;

        public SC_AI_Start(Matrix _dronepos , Matrix _playerpos)
        {
            playerpos = _playerpos;
            dronepos = _dronepos;

            for (int i = 0; i < SC_AI.Length; i++) // using 10 instances of SC_AI
            {
                //TO READD WHEN USING PERCEPTRON VEC2 SCRIPT
                /*SC_AI[i] = new SC_AI(playerpos, dronepos);
                SC_AI[i].sccsaiguessInitVariables(numberOfNeurons, 0.001f);
                SC_AI[i].swtchwaypointtype = 1;*/
                SC_AI[i] = new SC_AI(numberOfNeurons, 0.001f); //playerpos, dronepos, 
                //SC_AI[i].swtchwaypointtype = 1;
                //SC_AI[i].Starter();
                //SC_AI[i].sccsaiguessInitVariables(numberOfNeurons, 0.001f);
                //SC_AI[i].swtchwaypointtype = 1;
            }
        }

        int totalRight = 0;
        int totalLeft = 0;

        public int consoleDebugMessageFrameCounter = 0;

        Vector3 rotRight = new Vector3(15f * Math.Abs(speed), 0, 0);
        Vector3 rotLeft = new Vector3(-15f * Math.Abs(speed), 0, 0);

        float rollDegree = 0;//
        float pitchDegree = 0;//(float)((Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq)));//  * (180 / Math.PI));//
        float yawDegree = 0;//(float)((Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq)));//  * (180 / Math.PI));

        float rollDegreeModded = 0;//
        float pitchDegreeModded = 0;//(float)((Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq)));//  * (180 / Math.PI));//
        float yawDegreeModded = 0;//(float)((Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq)));//  * (180 / Math.PI));

        float totalDOTGoal = 0;



        public Matrix Update(SC_AI.data_input data_input,Matrix InitialDroneMatrix, out Matrix rotatedDroneMatrix)
        {
            //data_input.dronePos = new Vector3(InitialDroneMatrix.M41, InitialDroneMatrix.M42, InitialDroneMatrix.M43);
            //data_input.playerPos = new Vector3(InitialDroneMatrix.M41, InitialDroneMatrix.M42, InitialDroneMatrix.M43);

            /*Quaternion _testQuator;
            Quaternion.RotationMatrix(ref InitialDroneMatrix, out _testQuator);

            float xq = _testQuator.X;
            float yq = _testQuator.Y;
            float zq = _testQuator.Z;
            float wq = _testQuator.W;

            _testQuator.Normalize();*/

            //rollDegree = (float)((Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq)));// * (180 / Math.PI));
            //pitchDegree = (float)((Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq)));//  * (180 / Math.PI));//
            //yawDegree = (float)((Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq)));//  * (180 / Math.PI));      

            //rollDegree = (float)(Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq));// * (180.0f / Math.PI));
            //pitchDegree = (float)(Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq));// * (180.0f / Math.PI));
            //yawDegree = (float)(Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq));// * (180.0f / Math.PI));

            // Vector3 vecPitchYawRoll = sc_maths.QuaternionToEuler(_testQuator);
            //Vector3 vecPitchYawRoll = sc_maths.quatToPitchYawRoll(_testQuator);

            pitchDegree = data_input.angleX;// vecPitchYawRoll.X;
            yawDegree = data_input.angleY;// vecPitchYawRoll.Y;
            rollDegree = data_input.angleZ;// vecPitchYawRoll.Z;

            //rollDegree = (float)(Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq) * (180.0f / Math.PI));
            //pitchDegree = (float)(Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq) * (180.0f / Math.PI));
            //yawDegree = (float)(Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq) * (180.0f / Math.PI));


            //rollDegree = (float)(sc_maths._normalize_degrees(rollDegree) * (180 / Math.PI));
            //pitchDegree = (float)(sc_maths._normalize_degrees(pitchDegree) * (180 / Math.PI));
            //yawDegree = (float)(sc_maths._normalize_degrees(yawDegree) * (180 / Math.PI));

            totalRight = 0;
            totalLeft = 0;
            totalDOTGoal = 0;

            //data_input.angleX = pitchDegree;
            //data_input.angleY = yawDegree;
            //data_input.angleZ = rollDegree;

            //totalDOTGoal = Math.Abs(totalDOTGoal);
            //if (Math.Abs(totalRight - totalLeft) > 3)
            //if ((totalDOTGoal) > 0 && totalDOTGoal < 0.49f && (totalDOTGoal) < 0 && totalDOTGoal > -0.49f)

            /*if (totalDOTGoal < 0.49f && totalDOTGoal > -0.49f)
            {
                
            }
            else
            {
                rotatedDroneMatrix = InitialDroneMatrix;
            }*/

            /*if (float.IsInfinity(totalDOTGoal) || float.IsNaN(totalDOTGoal) || float.IsNegativeInfinity(totalDOTGoal) || float.IsPositiveInfinity(totalDOTGoal))
            {
                Console.WriteLine("NUll float");
            }*/

            //totalDOTGoal = Math.Abs(totalDOTGoal);

            rotatedDroneMatrix = InitialDroneMatrix;

            if (data_input.CompassTypeSwitch == 0)
            {
                for (int i = 0; i < SC_AI.Length; i++)
                {
                    SC_AI[i].swtchwaypointtype = 1;
                    SC_AI[i].angleSwitch = 1;
                    SC_AI[i].UpdatePerceptron(data_input);
                    totalRight += SC_AI[i]._guessedCorrectRight;
                    totalLeft += SC_AI[i]._guessedCorrectLeft;
                    //speed = SC_AI[i]._dotGoal;
                    totalDOTGoal += SC_AI[i]._dotGoal;
                }

                totalDOTGoal /= SC_AI.Length;

                if (totalRight > totalLeft)
                {
                    //Console.WriteLine("player is RIGHt 0-0");
                    //drone.transform.Rotate(new Vector3(0, 0, -15f * Mathf.Abs(speed)), Space.World);

                    yawDegree += 1 * Math.Abs(speed) * totalDOTGoal;
                    //pitchDegree += 1 * Math.Abs(speed) * totalDOTGoal;
                    //rollDegree += 1 * Math.Abs(speed) * totalDOTGoal;
                    //yawDegree = (float)(Math.PI * yawDegree / 180.0f);
                    //yawDegree = (float)(sc_maths._normalize_Radians(yawDegree) * (180 / Math.PI));

                    //rollDegree -= 1 * Math.Abs(speed);
                    //pitchDegree -= 1 * Math.Abs(speed);

                    //float pitchRad = (float)(Math.PI * pitchDegree / 180.0f); ///RotationX
                    //float yawRad = (float)(Math.PI * yawDegree / 180.0f); // RotationY //(yawDegree + yawDegreeModded)
                    //float rollRad = (float)(Math.PI * rollDegree / 180.0f); //RotationZ

                    float pitchRad = (float)(Math.PI * 0 / 180.0f); ///RotationX
                    float yawRad = (float)(Math.PI * yawDegree / 180.0f); // RotationY //(yawDegree + yawDegreeModded)
                    float rollRad = (float)(Math.PI * 0 / 180.0f); //RotationZ

                    rotatedDroneMatrix = SharpDX.Matrix.RotationYawPitchRoll(yawRad, pitchRad, rollRad);
                    //Console.WriteLine("yawDegree: " + yawDegree + " swtchIndex: " + data_input.CompassTypeSwitch + " DOT: " + totalDOTGoal + " TL: " + totalLeft + " TR: " + totalRight);
                    /*rollDegree += 1 * Math.Abs(speed) * totalDOTGoal;
                    //yawDegree = (float)(Math.PI * yawDegree / 180.0f);
                    //yawDegree = (float)(sc_maths._normalize_Radians(yawDegree) * (180 / Math.PI));

                    //rollDegree -= 1 * Math.Abs(speed);
                    //pitchDegree -= 1 * Math.Abs(speed);

                    //float pitchRad = (float)(Math.PI * pitchDegree / 180.0f); ///RotationX
                    //float yawRad = (float)(Math.PI * yawDegree / 180.0f); // RotationY //(yawDegree + yawDegreeModded)
                    //float rollRad = (float)(Math.PI * rollDegree / 180.0f); //RotationZ

                    float pitchRad = (float)(Math.PI * 0 / 180.0f); ///RotationX
                    float yawRad = (float)(Math.PI * 0 / 180.0f); // RotationY //(yawDegree + yawDegreeModded)
                    float rollRad = (float)(Math.PI * rollDegree / 180.0f); //RotationZ

                    rotatedDroneMatrix = SharpDX.Matrix.RotationYawPitchRoll(yawRad, pitchRad, rollRad);*/

                    /*pitchDegree += 1 * Math.Abs(speed) * totalDOTGoal;
                    //yawDegree = (float)(Math.PI * yawDegree / 180.0f);
                    //yawDegree = (float)(sc_maths._normalize_Radians(yawDegree) * (180 / Math.PI));

                    //rollDegree -= 1 * Math.Abs(speed);
                    //pitchDegree -= 1 * Math.Abs(speed);

                    //float pitchRad = (float)(Math.PI * pitchDegree / 180.0f); ///RotationX
                    //float yawRad = (float)(Math.PI * yawDegree / 180.0f); // RotationY //(yawDegree + yawDegreeModded)
                    //float rollRad = (float)(Math.PI * rollDegree / 180.0f); //RotationZ

                    float pitchRad = (float)(Math.PI * pitchDegree / 180.0f); ///RotationX
                    float yawRad = (float)(Math.PI * 0 / 180.0f); // RotationY //(yawDegree + yawDegreeModded)
                    float rollRad = (float)(Math.PI * 0 / 180.0f); //RotationZ

                    rotatedDroneMatrix = SharpDX.Matrix.RotationYawPitchRoll(yawRad, pitchRad, rollRad);*/
                }
                else if (totalRight < totalLeft)
                {
                    //Console.WriteLine("player is LEFT 0-0");
                    //drone.transform.Rotate(new Vector3(0, 0, 15f * Mathf.Abs(speed)), Space.World);
                    //rollDegree = (float)(sc_maths._normalize_degrees(rollDegree) * (180 / Math.PI));
                    //pitchDegree = (float)(sc_maths._normalize_degrees(rollDegree) * (180 / Math.PI));
                    //yawDegree = (float)(sc_maths._normalize_degrees(rollDegree) * (180 / Math.PI));

                    yawDegree += 1 * Math.Abs(speed) * totalDOTGoal;
                    //pitchDegree += 1 * Math.Abs(speed) * totalDOTGoal;
                    //rollDegree += 1 * Math.Abs(speed) * totalDOTGoal;
                    //yawDegree = (float)(Math.PI * yawDegree / 180.0f);
                    //yawDegree = (float)(sc_maths._normalize_Radians(yawDegree) * (180 / Math.PI));

                    //rollDegree -= 1 * Math.Abs(speed);
                    //pitchDegree -= 1 * Math.Abs(speed);

                    //float pitchRad = (float)(Math.PI * pitchDegree / 180.0f); ///RotationX
                    //float yawRad = (float)(Math.PI * yawDegree / 180.0f); // RotationY //(yawDegree + yawDegreeModded)
                    //float rollRad = (float)(Math.PI * rollDegree / 180.0f); //RotationZ

                    float pitchRad = (float)(Math.PI * 0 / 180.0f); ///RotationX
                    float yawRad = (float)(Math.PI * yawDegree / 180.0f); // RotationY //(yawDegree + yawDegreeModded)
                    float rollRad = (float)(Math.PI * 0 / 180.0f); //RotationZ

                    rotatedDroneMatrix = SharpDX.Matrix.RotationYawPitchRoll(yawRad, pitchRad, rollRad);
                    //Console.WriteLine("yawDegree: " + yawDegree + " swtchIndex: " + data_input.CompassTypeSwitch + " DOT: " + totalDOTGoal + " TL: " + totalLeft + " TR: " + totalRight);



                    /*rollDegree += 1 * Math.Abs(speed) * totalDOTGoal;
                    //yawDegree = (float)(Math.PI * yawDegree / 180.0f);
                    //yawDegree = (float)(sc_maths._normalize_Radians(yawDegree) * (180 / Math.PI));

                    //rollDegree -= 1 * Math.Abs(speed);
                    //pitchDegree -= 1 * Math.Abs(speed);

                    //float pitchRad = (float)(Math.PI * pitchDegree / 180.0f); ///RotationX
                    //float yawRad = (float)(Math.PI * yawDegree / 180.0f); // RotationY //(yawDegree + yawDegreeModded)
                    //float rollRad = (float)(Math.PI * rollDegree / 180.0f); //RotationZ

                    float pitchRad = (float)(Math.PI * 0 / 180.0f); ///RotationX
                    float yawRad = (float)(Math.PI * 0 / 180.0f); // RotationY //(yawDegree + yawDegreeModded)
                    float rollRad = (float)(Math.PI * rollDegree / 180.0f); //RotationZ

                    rotatedDroneMatrix = SharpDX.Matrix.RotationYawPitchRoll(yawRad, pitchRad, rollRad);*/


                    /*pitchDegree += 1 * Math.Abs(speed) * totalDOTGoal;
                    //yawDegree = (float)(Math.PI * yawDegree / 180.0f);
                    //yawDegree = (float)(sc_maths._normalize_Radians(yawDegree) * (180 / Math.PI));

                    //rollDegree -= 1 * Math.Abs(speed);
                    //pitchDegree -= 1 * Math.Abs(speed);

                    //float pitchRad = (float)(Math.PI * pitchDegree / 180.0f); ///RotationX
                    //float yawRad = (float)(Math.PI * yawDegree / 180.0f); // RotationY //(yawDegree + yawDegreeModded)
                    //float rollRad = (float)(Math.PI * rollDegree / 180.0f); //RotationZ

                    float pitchRad = (float)(Math.PI * pitchDegree / 180.0f); ///RotationX
                    float yawRad = (float)(Math.PI * 0 / 180.0f); // RotationY //(yawDegree + yawDegreeModded)
                    float rollRad = (float)(Math.PI * 0 / 180.0f); //RotationZ

                    rotatedDroneMatrix = SharpDX.Matrix.RotationYawPitchRoll(yawRad, pitchRad, rollRad);*/

                    //Matrix.RotationAxis(rotRight,);
                }
                else
                {
                    rotatedDroneMatrix = InitialDroneMatrix;
                }

                if (consoleDebugMessageFrameCounter >= 49)
                {
                    //TO READD FOR TESTS - NINEKORN NOTE 2021-JULY-27
                    //Console.WriteLine("yawDegree: " + yawDegree + " swtchIndex: " + data_input.CompassTypeSwitch + " DOT: " + totalDOTGoal + " TL: " + totalLeft + " TR: " + totalRight);
                    //TO READD FOR TESTS - NINEKORN NOTE 2021 - JULY - 27



                    //Console.WriteLine("degree: " + rollDegree + " swtchIndex: " + data_input.CompassTypeSwitch + " DOT: " + totalDOTGoal + " TL: " + totalLeft + " TR: " + totalRight);
                    //Console.WriteLine("degree: " + rollDegree + " swtchIndex: " + data_input.CompassTypeSwitch + " DOT: " + totalDOTGoal + " TL: " + totalLeft + " TR: " + totalRight);

                    consoleDebugMessageFrameCounter = 0;
                }
            }
            else if (data_input.CompassTypeSwitch == 1)
            {
                for (int i = 0; i < SC_AI.Length; i++)
                {
                    SC_AI[i].swtchwaypointtype = 0;
                    SC_AI[i].angleSwitch = 0;
                    SC_AI[i].UpdatePerceptron(data_input);
                    totalRight += SC_AI[i]._guessedCorrectRight;
                    totalLeft += SC_AI[i]._guessedCorrectLeft;
                    //speed = SC_AI[i]._dotGoal;
                    totalDOTGoal += SC_AI[i]._dotGoal;
                }

                totalDOTGoal /= SC_AI.Length;

                //Console.WriteLine("testing CompassTypeSwitch TB");

                if (totalRight > totalLeft)
                {
                    // Console.WriteLine("player is RIGHt 0-0");
                    //drone.transform.Rotate(new Vector3(0, 0, -15f * Mathf.Abs(speed)), Space.World);

                    /*pitchDegree += 1 * Math.Abs(speed) * totalDOTGoal;
                    //yawDegree = (float)(Math.PI * yawDegree / 180.0f);
                    //yawDegree = (float)(sc_maths._normalize_Radians(yawDegree) * (180 / Math.PI));

                    //rollDegree -= 1 * Math.Abs(speed);
                    //pitchDegree -= 1 * Math.Abs(speed);

                    //float pitchRad = (float)(Math.PI * pitchDegree / 180.0f); ///RotationX
                    //float yawRad = (float)(Math.PI * yawDegree / 180.0f); // RotationY //(yawDegree + yawDegreeModded)
                    //float rollRad = (float)(Math.PI * rollDegree / 180.0f); //RotationZ

                    float pitchRad = (float)(Math.PI * pitchDegree / 180.0f); ///RotationX
                    float yawRad = (float)(Math.PI * 0 / 180.0f); // RotationY //(yawDegree + yawDegreeModded)
                    float rollRad = (float)(Math.PI * 0 / 180.0f); //RotationZ

                    rotatedDroneMatrix = SharpDX.Matrix.RotationYawPitchRoll(yawRad, pitchRad, rollRad);*/

                    /*rollDegree += 1 * Math.Abs(speed) * totalDOTGoal;
                    //yawDegree = (float)(Math.PI * yawDegree / 180.0f);
                    //yawDegree = (float)(sc_maths._normalize_Radians(yawDegree) * (180 / Math.PI));

                    //rollDegree -= 1 * Math.Abs(speed);
                    //pitchDegree -= 1 * Math.Abs(speed);

                    //float pitchRad = (float)(Math.PI * pitchDegree / 180.0f); ///RotationX
                    //float yawRad = (float)(Math.PI * yawDegree / 180.0f); // RotationY //(yawDegree + yawDegreeModded)
                    //float rollRad = (float)(Math.PI * rollDegree / 180.0f); //RotationZ

                    float pitchRad = (float)(Math.PI * 0 / 180.0f); ///RotationX
                    float yawRad = (float)(Math.PI * 0 / 180.0f); // RotationY //(yawDegree + yawDegreeModded)
                    float rollRad = (float)(Math.PI * rollDegree / 180.0f); //RotationZ

                    rotatedDroneMatrix = SharpDX.Matrix.RotationYawPitchRoll(yawRad, pitchRad, rollRad);*/

                    /*pitchDegree += 1 * Math.Abs(speed) * totalDOTGoal;

                    //yawDegree = (float)(Math.PI * yawDegree / 180.0f);
                    //yawDegree = (float)(sc_maths._normalize_Radians(yawDegree) * (180 / Math.PI));

                    //rollDegree -= 1 * Math.Abs(speed);
                    //pitchDegree -= 1 * Math.Abs(speed);

                    //float pitchRad = (float)(Math.PI * pitchDegree / 180.0f); ///RotationX
                    //float yawRad = (float)(Math.PI * yawDegree / 180.0f); // RotationY //(yawDegree + yawDegreeModded)
                    //float rollRad = (float)(Math.PI * rollDegree / 180.0f); //RotationZ

                    float pitchRad = (float)(Math.PI * pitchDegree / 180.0f); ///RotationX
                    float yawRad = (float)(Math.PI * 0 / 180.0f); // RotationY //(yawDegree + yawDegreeModded)
                    float rollRad = (float)(Math.PI * 0 / 180.0f); //RotationZ

                    rotatedDroneMatrix = SharpDX.Matrix.RotationYawPitchRoll(yawRad, pitchRad, rollRad);*/

                    rollDegree += 1 * Math.Abs(speed) * totalDOTGoal;
                    float pitchRad = (float)(Math.PI * 0 / 180.0f); ///RotationX
                    float yawRad = (float)(Math.PI * 0 / 180.0f); // RotationY //(yawDegree + yawDegreeModded)
                    float rollRad = (float)(Math.PI * rollDegree / 180.0f); //RotationZ
                    rotatedDroneMatrix = SharpDX.Matrix.RotationYawPitchRoll(yawRad, pitchRad, rollRad);



                    //Console.WriteLine("pitchRad: " + pitchDegree + " swtchIndex: " + data_input.CompassTypeSwitch + " DOT: " + totalDOTGoal + " TL: " + totalLeft + " TR: " + totalRight);
                }
                else if (totalRight < totalLeft)
                {
                    //Console.WriteLine("player is LEFT 0-0");
                    //drone.transform.Rotate(new Vector3(0, 0, 15f * Mathf.Abs(speed)), Space.World);
                    //rollDegree = (float)(sc_maths._normalize_degrees(rollDegree) * (180 / Math.PI));
                    //pitchDegree = (float)(sc_maths._normalize_degrees(rollDegree) * (180 / Math.PI));
                    //yawDegree = (float)(sc_maths._normalize_degrees(rollDegree) * (180 / Math.PI));

                    /*pitchDegree += 1 * Math.Abs(speed) * totalDOTGoal;
                    //yawDegree = (float)(Math.PI * yawDegree / 180.0f);
                    //yawDegree = (float)(sc_maths._normalize_Radians(yawDegree) * (180 / Math.PI));

                    //rollDegree -= 1 * Math.Abs(speed);
                    //pitchDegree -= 1 * Math.Abs(speed);

                    //float pitchRad = (float)(Math.PI * pitchDegree / 180.0f); ///RotationX
                    //float yawRad = (float)(Math.PI * yawDegree / 180.0f); // RotationY //(yawDegree + yawDegreeModded)
                    //float rollRad = (float)(Math.PI * rollDegree / 180.0f); //RotationZ

                    float pitchRad = (float)(Math.PI * pitchDegree / 180.0f); ///RotationX
                    float yawRad = (float)(Math.PI * 0 / 180.0f); // RotationY //(yawDegree + yawDegreeModded)
                    float rollRad = (float)(Math.PI * 0 / 180.0f); //RotationZ

                    rotatedDroneMatrix = SharpDX.Matrix.RotationYawPitchRoll(yawRad, pitchRad, rollRad);*/


                    /*rollDegree += 1 * Math.Abs(speed) * totalDOTGoal;
                    //yawDegree = (float)(Math.PI * yawDegree / 180.0f);
                    //yawDegree = (float)(sc_maths._normalize_Radians(yawDegree) * (180 / Math.PI));

                    //rollDegree -= 1 * Math.Abs(speed);
                    //pitchDegree -= 1 * Math.Abs(speed);

                    //float pitchRad = (float)(Math.PI * pitchDegree / 180.0f); ///RotationX
                    //float yawRad = (float)(Math.PI * yawDegree / 180.0f); // RotationY //(yawDegree + yawDegreeModded)
                    //float rollRad = (float)(Math.PI * rollDegree / 180.0f); //RotationZ

                    float pitchRad = (float)(Math.PI * 0 / 180.0f); ///RotationX
                    float yawRad = (float)(Math.PI * 0 / 180.0f); // RotationY //(yawDegree + yawDegreeModded)
                    float rollRad = (float)(Math.PI * rollDegree / 180.0f); //RotationZ

                    rotatedDroneMatrix = SharpDX.Matrix.RotationYawPitchRoll(yawRad, pitchRad, rollRad);*/
                    //Matrix.RotationAxis(rotRight,);

                    /*pitchDegree += 1 * Math.Abs(speed) * totalDOTGoal;

                    //yawDegree = (float)(Math.PI * yawDegree / 180.0f);
                    //yawDegree = (float)(sc_maths._normalize_Radians(yawDegree) * (180 / Math.PI));

                    //rollDegree -= 1 * Math.Abs(speed);
                    //pitchDegree -= 1 * Math.Abs(speed);

                    //float pitchRad = (float)(Math.PI * pitchDegree / 180.0f); ///RotationX
                    //float yawRad = (float)(Math.PI * yawDegree / 180.0f); // RotationY //(yawDegree + yawDegreeModded)
                    //float rollRad = (float)(Math.PI * rollDegree / 180.0f); //RotationZ

                    float pitchRad = (float)(Math.PI * pitchDegree / 180.0f); //RotationX
                    float yawRad = (float)(Math.PI * 0 / 180.0f); //RotationY //(yawDegree + yawDegreeModded)
                    float rollRad = (float)(Math.PI * 0 / 180.0f); //RotationZ

                    rotatedDroneMatrix = SharpDX.Matrix.RotationYawPitchRoll(yawRad, pitchRad, rollRad);*/



                    rollDegree += 1 * Math.Abs(speed) * totalDOTGoal;
                    float pitchRad = (float)(Math.PI * 0 / 180.0f); ///RotationX
                    float yawRad = (float)(Math.PI * 0 / 180.0f); // RotationY //(yawDegree + yawDegreeModded)
                    float rollRad = (float)(Math.PI * rollDegree / 180.0f); //RotationZ

                    rotatedDroneMatrix = SharpDX.Matrix.RotationYawPitchRoll(yawRad, pitchRad, rollRad);
                    //Console.WriteLine("pitchRad: " + pitchDegree + " swtchIndex: " + data_input.CompassTypeSwitch + " DOT: " + totalDOTGoal + " TL: " + totalLeft + " TR: " + totalRight);

                }
                else
                {
                    //Console.WriteLine("player is middle 0-0");
                    rotatedDroneMatrix = InitialDroneMatrix;
                }

                if (consoleDebugMessageFrameCounter >= 49)
                {
                    //TO READD FOR TESTS - NINEKORN NOTE 2021 - JULY - 27
                    //Console.WriteLine("rollDegree: " + rollDegree + " swtchIndex: " + data_input.CompassTypeSwitch + " DOT: " + totalDOTGoal + " TL: " + totalLeft + " TR: " + totalRight);
                    //TO READD FOR TESTS - NINEKORN NOTE 2021 - JULY - 27


                    //Console.WriteLine("degree: " + rollDegree + " swtchIndex: " + data_input.CompassTypeSwitch + " DOT: " + totalDOTGoal + " TL: " + totalLeft + " TR: " + totalRight);
                    //Console.WriteLine("degree: " + rollDegree + " swtchIndex: " + data_input.CompassTypeSwitch + " DOT: " + totalDOTGoal + " TL: " + totalLeft + " TR: " + totalRight);
                    consoleDebugMessageFrameCounter = 0;
                }
            }
            else if (data_input.CompassTypeSwitch == 2)
            {
                //pitchDegree = data_input.angleZ;// vecPitchYawRoll.X;
                //yawDegree = data_input.angleY;// vecPitchYawRoll.Y;
                //rollDegree = data_input.angleX;// vecPitchYawRoll.Z;

                for (int i = 0; i < SC_AI.Length; i++)
                {
                    SC_AI[i].swtchwaypointtype = 2;
                    SC_AI[i].angleSwitch = 2;
                    SC_AI[i].UpdatePerceptron(data_input);
                    totalRight += SC_AI[i]._guessedCorrectRight;
                    totalLeft += SC_AI[i]._guessedCorrectLeft;
                    //speed = SC_AI[i]._dotGoal;
                    totalDOTGoal += SC_AI[i]._dotGoal;
                }

                totalDOTGoal /= SC_AI.Length;

                if (totalRight > totalLeft)
                {
                    //Console.WriteLine("player is RIGHt 0-0");
                    //drone.transform.Rotate(new Vector3(0, 0, -15f * Mathf.Abs(speed)), Space.World);

                    /*rollDegree += 1 * Math.Abs(speed) * totalDOTGoal;
                    //yawDegree = (float)(Math.PI * yawDegree / 180.0f);
                    //yawDegree = (float)(sc_maths._normalize_Radians(yawDegree) * (180 / Math.PI));

                    //rollDegree -= 1 * Math.Abs(speed);
                    //pitchDegree -= 1 * Math.Abs(speed);

                    //float pitchRad = (float)(Math.PI * pitchDegree / 180.0f); ///RotationX
                    //float yawRad = (float)(Math.PI * yawDegree / 180.0f); // RotationY //(yawDegree + yawDegreeModded)
                    //float rollRad = (float)(Math.PI * rollDegree / 180.0f); //RotationZ

                    float pitchRad = (float)(Math.PI * 0 / 180.0f); ///RotationX
                    float yawRad = (float)(Math.PI * 0 / 180.0f); // RotationY //(yawDegree + yawDegreeModded)
                    float rollRad = (float)(Math.PI * rollDegree / 180.0f); //RotationZ

                    rotatedDroneMatrix = SharpDX.Matrix.RotationYawPitchRoll(yawRad, pitchRad, rollRad);*/


                    //yawDegree = (float)(Math.PI * yawDegree / 180.0f);
                    //yawDegree = (float)(sc_maths._normalize_Radians(yawDegree) * (180 / Math.PI));

                    //rollDegree -= 1 * Math.Abs(speed);
                    //pitchDegree -= 1 * Math.Abs(speed);

                    //float pitchRad = (float)(Math.PI * pitchDegree / 180.0f); ///RotationX
                    //float yawRad = (float)(Math.PI * yawDegree / 180.0f); // RotationY //(yawDegree + yawDegreeModded)
                    //float rollRad = (float)(Math.PI * rollDegree / 180.0f); //RotationZ

                    /*rollDegree += 1 * Math.Abs(speed) * totalDOTGoal;
                    float pitchRad = (float)(Math.PI * 0 / 180.0f); ///RotationX
                    float yawRad = (float)(Math.PI * 0 / 180.0f); // RotationY //(yawDegree + yawDegreeModded)
                    float rollRad = (float)(Math.PI * rollDegree / 180.0f); //RotationZ

                    rotatedDroneMatrix = SharpDX.Matrix.RotationYawPitchRoll(yawRad, pitchRad, rollRad);*/

                    pitchDegree += 1 * Math.Abs(speed) * totalDOTGoal;

                    if (consoleDebugMessageFrameCounter >= 49)
                    {

                        //TO READD FOR TESTS - NINEKORN NOTE 2021 - JULY - 27
                        //Console.WriteLine("pitchDegreeR: " + pitchDegree + " swtchIndex: " + data_input.CompassTypeSwitch + " DOT: " + totalDOTGoal + " TL: " + totalLeft + " TR: " + totalRight);
                        //TO READD FOR TESTS - NINEKORN NOTE 2021 - JULY - 27


                        //Console.WriteLine("pitchDegree: " + pitchDegree + " yawDegree: " + yawDegree + " rollDegree: " + rollDegree + " swtchIndex: " + data_input.CompassTypeSwitch + " DOT: " + totalDOTGoal + " TL: " + totalLeft + " TR: " + totalRight);

                        //Console.WriteLine("degree: " + rollDegree + " swtchIndex: " + data_input.CompassTypeSwitch + " DOT: " + totalDOTGoal + " TL: " + totalLeft + " TR: " + totalRight);
                        //Console.WriteLine("degree: " + rollDegree + " swtchIndex: " + data_input.CompassTypeSwitch + " DOT: " + totalDOTGoal + " TL: " + totalLeft + " TR: " + totalRight);
                        consoleDebugMessageFrameCounter = 0;
                    }

                    float pitchRad = (float)(Math.PI * pitchDegree / 180.0f); ///RotationX
                    float yawRad = (float)(Math.PI * 0 / 180.0f); // RotationY //(yawDegree + yawDegreeModded)
                    float rollRad = (float)(Math.PI * 0 / 180.0f); //RotationZ

                    rotatedDroneMatrix = SharpDX.Matrix.RotationYawPitchRoll(yawRad, pitchRad, rollRad);
                }
                else if (totalRight < totalLeft)
                {
                    //Console.WriteLine("player is LEFT 0-0");
                    //drone.transform.Rotate(new Vector3(0, 0, 15f * Mathf.Abs(speed)), Space.World);
                    //rollDegree = (float)(sc_maths._normalize_degrees(rollDegree) * (180 / Math.PI));
                    //pitchDegree = (float)(sc_maths._normalize_degrees(rollDegree) * (180 / Math.PI));
                    //yawDegree = (float)(sc_maths._normalize_degrees(rollDegree) * (180 / Math.PI));

                    /*rollDegree += 1 * Math.Abs(speed) * totalDOTGoal;
                    //yawDegree = (float)(Math.PI * yawDegree / 180.0f);
                    //yawDegree = (float)(sc_maths._normalize_Radians(yawDegree) * (180 / Math.PI));

                    //rollDegree -= 1 * Math.Abs(speed);
                    //pitchDegree -= 1 * Math.Abs(speed);

                    //float pitchRad = (float)(Math.PI * pitchDegree / 180.0f); ///RotationX
                    //float yawRad = (float)(Math.PI * yawDegree / 180.0f); // RotationY //(yawDegree + yawDegreeModded)
                    //float rollRad = (float)(Math.PI * rollDegree / 180.0f); //RotationZ

                    float pitchRad = (float)(Math.PI * 0 / 180.0f); ///RotationX
                    float yawRad = (float)(Math.PI * 0 / 180.0f); // RotationY //(yawDegree + yawDegreeModded)
                    float rollRad = (float)(Math.PI * rollDegree / 180.0f); //RotationZ

                    rotatedDroneMatrix = SharpDX.Matrix.RotationYawPitchRoll(yawRad, pitchRad, rollRad);*/

                    /*rollDegree += 1 * Math.Abs(speed) * totalDOTGoal;
                    float pitchRad = (float)(Math.PI * 0 / 180.0f); ///RotationX
                    float yawRad = (float)(Math.PI * 0 / 180.0f); // RotationY //(yawDegree + yawDegreeModded)
                    float rollRad = (float)(Math.PI * rollDegree / 180.0f); //RotationZ
                    */

                    pitchDegree += 1 * Math.Abs(speed) * totalDOTGoal;


                    if (consoleDebugMessageFrameCounter >= 49)
                    {
                        //TO READD FOR TESTS - NINEKORN NOTE 2021 - JULY - 27
                        //Console.WriteLine("pitchDegreeL: " + pitchDegree + " swtchIndex: " + data_input.CompassTypeSwitch + " DOT: " + totalDOTGoal + " TL: " + totalLeft + " TR: " + totalRight);
                        //TO READD FOR TESTS - NINEKORN NOTE 2021 - JULY - 27

                        //Console.WriteLine("pitchDegree: " + pitchDegree + " yawDegree: " + yawDegree + " rollDegree: " + rollDegree + " swtchIndex: " + data_input.CompassTypeSwitch + " DOT: " + totalDOTGoal + " TL: " + totalLeft + " TR: " + totalRight);

                        //Console.WriteLine("degree: " + rollDegree + " swtchIndex: " + data_input.CompassTypeSwitch + " DOT: " + totalDOTGoal + " TL: " + totalLeft + " TR: " + totalRight);
                        //Console.WriteLine("degree: " + rollDegree + " swtchIndex: " + data_input.CompassTypeSwitch + " DOT: " + totalDOTGoal + " TL: " + totalLeft + " TR: " + totalRight);
                        consoleDebugMessageFrameCounter = 0;
                    }

                    float pitchRad = (float)(Math.PI * pitchDegree / 180.0f); ///RotationX
                    float yawRad = (float)(Math.PI * 0 / 180.0f); // RotationY //(yawDegree + yawDegreeModded)
                    float rollRad = (float)(Math.PI * 0 / 180.0f); //RotationZ

                    rotatedDroneMatrix = SharpDX.Matrix.RotationYawPitchRoll(yawRad, pitchRad, rollRad);
                }
                else
                {
                    rotatedDroneMatrix = InitialDroneMatrix;
                }

                /*if (consoleDebugMessageFrameCounter >= 49)
                {
                    //Console.WriteLine("pitchDegree: " + pitchDegree + " swtchIndex: " + data_input.CompassTypeSwitch + " DOT: " + totalDOTGoal + " TL: " + totalLeft + " TR: " + totalRight);
                    //Console.WriteLine("pitchDegree: " + pitchDegree + " yawDegree: " + yawDegree + " rollDegree: " + rollDegree + " swtchIndex: " + data_input.CompassTypeSwitch + " DOT: " + totalDOTGoal + " TL: " + totalLeft + " TR: " + totalRight);

                    //Console.WriteLine("degree: " + rollDegree + " swtchIndex: " + data_input.CompassTypeSwitch + " DOT: " + totalDOTGoal + " TL: " + totalLeft + " TR: " + totalRight);
                    //Console.WriteLine("degree: " + rollDegree + " swtchIndex: " + data_input.CompassTypeSwitch + " DOT: " + totalDOTGoal + " TL: " + totalLeft + " TR: " + totalRight);
                    consoleDebugMessageFrameCounter = 0;
                }*/
            }
            else
            {
                rotatedDroneMatrix = InitialDroneMatrix;
            }


            /*if (consoleDebugMessageFrameCounter >= 49)
            {
                Console.WriteLine("pitchDegree: " + pitchDegree + " swtchIndex: " + data_input.CompassTypeSwitch + " DOT: " + totalDOTGoal + " TL: " + totalLeft + " TR: " + totalRight);
                //Console.WriteLine("pitchDegree: " + pitchDegree + " yawDegree: " + yawDegree + " rollDegree: " + rollDegree + " swtchIndex: " + data_input.CompassTypeSwitch + " DOT: " + totalDOTGoal + " TL: " + totalLeft + " TR: " + totalRight);

                //Console.WriteLine("degree: " + rollDegree + " swtchIndex: " + data_input.CompassTypeSwitch + " DOT: " + totalDOTGoal + " TL: " + totalLeft + " TR: " + totalRight);
                //Console.WriteLine("degree: " + rollDegree + " swtchIndex: " + data_input.CompassTypeSwitch + " DOT: " + totalDOTGoal + " TL: " + totalLeft + " TR: " + totalRight);
                consoleDebugMessageFrameCounter = 0;
            }*/


            /*if (totalRight > totalLeft)
            {
                //Console.WriteLine("player is RIGHt 0-0");
                //drone.transform.Rotate(new Vector3(0, 0, -15f * Mathf.Abs(speed)), Space.World);

                yawDegree += 1 * Math.Abs(speed) * totalDOTGoal;
                //rollDegree -= 1 * Math.Abs(speed);
                //pitchDegree -= 1 * Math.Abs(speed);

                float pitchRad = (float)(Math.PI * 0 / 180.0f); ///RotationX
                float yawRad = (float)(Math.PI * yawDegree / 180.0f); // RotationY //(yawDegree + yawDegreeModded)
                float rollRad = (float)(Math.PI * 0 / 180.0f); //RotationZ

                rotatedDroneMatrix = SharpDX.Matrix.RotationYawPitchRoll(yawRad, pitchRad, rollRad);
            }
            else if (totalRight < totalLeft)
            {
                //Console.WriteLine("player is LEFT 0-0");
                //drone.transform.Rotate(new Vector3(0, 0, 15f * Mathf.Abs(speed)), Space.World);
                //rollDegree = (float)(sc_maths._normalize_degrees(rollDegree) * (180 / Math.PI));
                //pitchDegree = (float)(sc_maths._normalize_degrees(rollDegree) * (180 / Math.PI));
                //yawDegree = (float)(sc_maths._normalize_degrees(rollDegree) * (180 / Math.PI));

                yawDegree += 1 * Math.Abs(speed) * totalDOTGoal;
                //rollDegree += 1 * Math.Abs(speed);
                //pitchDegree += 1 * Math.Abs(speed);

                float pitchRad = (float)(Math.PI * 0 / 180.0f); ///RotationX
                float yawRad = (float)(Math.PI * yawDegree / 180.0f); // RotationY //(yawDegree + yawDegreeModded)
                float rollRad = (float)(Math.PI * 0 / 180.0f); //RotationZ

                rotatedDroneMatrix = SharpDX.Matrix.RotationYawPitchRoll(yawRad, pitchRad, rollRad);
                //Matrix.RotationAxis(rotRight,);
            }
            else
            {
                rotatedDroneMatrix = InitialDroneMatrix;
            }*/

            //rotatedDroneMatrix.Invert();
            /*if (consoleDebugMessageFrameCounter >= 49)
            {
                //Console.WriteLine("degree: " + rollDegree + " swtchIndex: " + data_input.CompassTypeSwitch + " DOT: " + totalDOTGoal + " TL: " + totalLeft + " TR: " + totalRight);
                //Console.WriteLine("degree: " + rollDegree + " swtchIndex: " + data_input.CompassTypeSwitch + " DOT: " + totalDOTGoal + " TL: " + totalLeft + " TR: " + totalRight);
                //Console.WriteLine("degree: " + rollDegree + " swtchIndex: " + data_input.CompassTypeSwitch + " DOT: " + totalDOTGoal + " TL: " + totalLeft + " TR: " + totalRight);

                consoleDebugMessageFrameCounter = 0;
            }*/
            consoleDebugMessageFrameCounter++;

            return rotatedDroneMatrix;
        }
    }
}
