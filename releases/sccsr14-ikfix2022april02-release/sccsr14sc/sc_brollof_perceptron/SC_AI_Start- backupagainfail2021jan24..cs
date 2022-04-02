using System;
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

        SC_AI.data_input data_input;
        //public Transform drone;
        //public Transform player;

        public static float speed = 0.01f; //0.0001f

        //Vector3 playerpos = Vector3.Zero;// (0, 0, 0);// Matrix.Identity;
        //Vector3 dronepos = Vector3.Zero;// Matrix.Identity;

        Matrix dronepos = Matrix.Identity;
        Matrix playerpos = Matrix.Identity;


        public int switchWaypoint = 0;

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
                SC_AI[i].swtchwaypointtype = switchWaypoint;
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
            //switchWaypoint = data_input.swtchwaypointtype;
            //data_input.dronePos = new Vector3(InitialDroneMatrix.M41, InitialDroneMatrix.M42, InitialDroneMatrix.M43);
            //data_input.playerPos = new Vector3(InitialDroneMatrix.M41, InitialDroneMatrix.M42, InitialDroneMatrix.M43);

            Quaternion _testQuator;
            Quaternion.RotationMatrix(ref InitialDroneMatrix, out _testQuator);

            float xq = _testQuator.X;
            float yq = _testQuator.Y;
            float zq = _testQuator.Z;
            float wq = _testQuator.W;

            //rollDegree = (float)((Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq)));// * (180 / Math.PI));
            //pitchDegree = (float)((Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq)));//  * (180 / Math.PI));//
            //yawDegree = (float)((Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq)));//  * (180 / Math.PI));      

            //rollDegree = (float)(Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq));// * (180.0f / Math.PI));
            //pitchDegree = (float)(Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq));// * (180.0f / Math.PI));
            //yawDegree = (float)(Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq));// * (180.0f / Math.PI));

            rollDegree = (float)(Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq) * (180.0f / Math.PI));
            pitchDegree = (float)(Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq) * (180.0f / Math.PI));
            yawDegree = (float)(Math.Atan2(2 * yq * wq - 2 * xq * zq, 1 - 2 * yq * yq - 2 * zq * zq) * (180.0f / Math.PI));

            //rollDegree = (float)(sc_maths._normalize_degrees(rollDegree) * (180 / Math.PI));
            //pitchDegree = (float)(sc_maths._normalize_degrees(pitchDegree) * (180 / Math.PI));
            //yawDegree = (float)(sc_maths._normalize_degrees(yawDegree) * (180 / Math.PI));

            totalRight = 0;
            totalLeft = 0;
            totalDOTGoal = 0;

            //data_input.angleX = pitchDegree;
            //data_input.angleY = yawDegree;
            //data_input.angleZ = rollDegree;

            for (int i = 0; i < SC_AI.Length; i++)
            {
                SC_AI[i].UpdatePerceptron(data_input);
                SC_AI[i].swtchwaypointtype = data_input.swtchwaypointtype;
                totalRight += SC_AI[i]._guessedCorrectRight;
                totalLeft += SC_AI[i]._guessedCorrectLeft;
                speed = SC_AI[i]._dotGoal;
                totalDOTGoal += SC_AI[i]._dotGoal;
            }

            totalDOTGoal /= SC_AI.Length;

            //totalDOTGoal = Math.Abs(totalDOTGoal);
            //if (Math.Abs(totalRight - totalLeft) > 3)
            //if ((totalDOTGoal) > 0 && totalDOTGoal < 0.99f && (totalDOTGoal) < 0 && totalDOTGoal > -0.99f)

            /*if (totalDOTGoal < 0.99f && totalDOTGoal > -0.99f)
            {
                
            }
            else
            {
                rotatedDroneMatrix = InitialDroneMatrix;
            }*/

            if (float.IsInfinity(totalDOTGoal) || float.IsNaN(totalDOTGoal) || float.IsNegativeInfinity(totalDOTGoal) || float.IsPositiveInfinity(totalDOTGoal))
            {
                Console.WriteLine("NUll float");
            }

            //totalDOTGoal = Math.Abs(totalDOTGoal);

            rotatedDroneMatrix = InitialDroneMatrix;

            if (data_input.swtchwaypointtype == 0)
            {
                if (totalRight > totalLeft)
                {
                    //Console.WriteLine("player is RIGHt 0-0");
                    //drone.transform.Rotate(new Vector3(0, 0, -15f * Mathf.Abs(speed)), Space.World);

                    rollDegree += 1 * Math.Abs(speed) * totalDOTGoal;
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

                    rotatedDroneMatrix = SharpDX.Matrix.RotationYawPitchRoll(yawRad, pitchRad, rollRad);
                }
                else if (totalRight < totalLeft)
                {
                    //Console.WriteLine("player is LEFT 0-0");
                    //drone.transform.Rotate(new Vector3(0, 0, 15f * Mathf.Abs(speed)), Space.World);
                    //rollDegree = (float)(sc_maths._normalize_degrees(rollDegree) * (180 / Math.PI));
                    //pitchDegree = (float)(sc_maths._normalize_degrees(rollDegree) * (180 / Math.PI));
                    //yawDegree = (float)(sc_maths._normalize_degrees(rollDegree) * (180 / Math.PI));

                    rollDegree += 1 * Math.Abs(speed) * totalDOTGoal;
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

                    rotatedDroneMatrix = SharpDX.Matrix.RotationYawPitchRoll(yawRad, pitchRad, rollRad);
                    //Matrix.RotationAxis(rotRight,);
                }
                else
                {
                    rotatedDroneMatrix = InitialDroneMatrix;
                }
            }
            else if (data_input.swtchwaypointtype == 1)
            {
                if (totalRight > totalLeft)
                {
                    //Console.WriteLine("player is RIGHt 0-0");
                    //drone.transform.Rotate(new Vector3(0, 0, -15f * Mathf.Abs(speed)), Space.World);

                    yawDegree += 1 * Math.Abs(speed) * totalDOTGoal;
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
                }
                else if (totalRight < totalLeft)
                {
                    //Console.WriteLine("player is LEFT 0-0");
                    //drone.transform.Rotate(new Vector3(0, 0, 15f * Mathf.Abs(speed)), Space.World);
                    //rollDegree = (float)(sc_maths._normalize_degrees(rollDegree) * (180 / Math.PI));
                    //pitchDegree = (float)(sc_maths._normalize_degrees(rollDegree) * (180 / Math.PI));
                    //yawDegree = (float)(sc_maths._normalize_degrees(rollDegree) * (180 / Math.PI));

                    yawDegree += 1 * Math.Abs(speed) * totalDOTGoal;
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
                    //Matrix.RotationAxis(rotRight,);
                }
                else
                {
                    rotatedDroneMatrix = InitialDroneMatrix;
                }
            }
            else if (data_input.swtchwaypointtype == 2)
            {

                if (totalRight > totalLeft)
                {
                    //Console.WriteLine("player is RIGHt 0-0");
                    //drone.transform.Rotate(new Vector3(0, 0, -15f * Mathf.Abs(speed)), Space.World);

                    pitchDegree += 1 * Math.Abs(speed) * totalDOTGoal;
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

                    rotatedDroneMatrix = SharpDX.Matrix.RotationYawPitchRoll(yawRad, pitchRad, rollRad);
                }
                else if (totalRight < totalLeft)
                {
                    //Console.WriteLine("player is LEFT 0-0");
                    //drone.transform.Rotate(new Vector3(0, 0, 15f * Mathf.Abs(speed)), Space.World);
                    //rollDegree = (float)(sc_maths._normalize_degrees(rollDegree) * (180 / Math.PI));
                    //pitchDegree = (float)(sc_maths._normalize_degrees(rollDegree) * (180 / Math.PI));
                    //yawDegree = (float)(sc_maths._normalize_degrees(rollDegree) * (180 / Math.PI));

                    pitchDegree += 1 * Math.Abs(speed) * totalDOTGoal;
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
                                                                   //Matrix.RotationAxis(rotRight,);
                    rotatedDroneMatrix = SharpDX.Matrix.RotationYawPitchRoll(yawRad, pitchRad, rollRad);
                }
                else
                {
                    rotatedDroneMatrix = InitialDroneMatrix;
                }
            }
            else
            {
                rotatedDroneMatrix = InitialDroneMatrix;
            }




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
            if (consoleDebugMessageFrameCounter >= 99)
            {
                Console.WriteLine("DOT: " + totalDOTGoal + " TL: " + totalLeft + " TR: " + totalRight);
                consoleDebugMessageFrameCounter = 0;
            }
            consoleDebugMessageFrameCounter++;

            return rotatedDroneMatrix;
        }
    }
}
