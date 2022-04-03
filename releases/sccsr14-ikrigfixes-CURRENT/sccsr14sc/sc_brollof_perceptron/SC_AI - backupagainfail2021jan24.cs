//by steve chassé aka ninekorn

//using UnityEngine;
using System;
using Perceptron;

using SharpDX;


namespace SCCoreSystems
{
    public class SC_AI //: MonoBehaviour
    {
        public struct data_input
        {
            public Vector3 playerPos;
            public Vector3 playerDirForward;
            public Vector3 playerDirRight;
            public Vector3 playerDirUp;

            public Vector3 dronePos;
            public Vector3 dronePosDirForward;
            public Vector3 dronePosDirRight;
            public Vector3 dronePosDirUp;

            public Vector3 formationWaypoint;

            public float formationWaypointOffsetX;
            public float formationWaypointOffsetY;
            public float formationWaypointOffsetZ;

            //public Vector3? oculusRiftPos;

            public float angleX;
            public float angleY;
            public float angleZ;

            public int swtchwaypointtype;

        }



        public int inputsNumber = 20; // 3 minimum i think
        public int SC_Angle_Divider = 4;
        public int SC_anglesNumber = 360;
        public int errormargin = 5;
        public int weightsNumber = 10;

        public int SC_anglesQuarterNumber = 0; //SC_Angle_Divider * SC_anglesNumber

        public float[][] saveCurrentWeightForTheCurrentAngleInsideOfUnitsOf360;// = new float[SC_anglesQuarterNumber][];

        public int swtchwaypointtype = 0;
        Vector3 northpoletransform;
        Vector3 compasspivot;
        public Vector2 waypointpos;
        Perceptron.Perceptron perc;
        float[] weights;
        float xmin, xmax, ymin, ymax;
        Trainer[] training;// = new Trainer[inputsNumber];
        public int _guessedCorrectRight = 0;
        public int _guessedCorrectLeft = 0;
        public float _dotGoal;
        int answer;
        System.Random random;
        float lastAngle = 0;
        Vector2 northpolepos;
        Vector2 compassPos;
        float randguess = 0;

        int guessedCorrect = 0;
        int guessedWrong = 0;

        int turnRight = 0;
        int turnLeft = 0;




        public SC_AI(int maxPerceptronInstancesneurons, float perceptronLearningRate) //Vector3 compass, Vector3 northpole, 
        {
            training = new Trainer[inputsNumber];
 
            Starter(maxPerceptronInstancesneurons, perceptronLearningRate);
        }

       public void Starter(int maxPerceptronInstancesneurons,float perceptronLearningRate)
        {
            SC_anglesQuarterNumber = SC_anglesNumber * SC_Angle_Divider;
            saveCurrentWeightForTheCurrentAngleInsideOfUnitsOf360 = new float[SC_anglesQuarterNumber][];

            random = new System.Random();
            perc = new Perceptron.Perceptron(maxPerceptronInstancesneurons, perceptronLearningRate);

            //perceptronLearningRate = sc_maths.getSomeRandNumThousandDecimal();

            for (int i = 0; i < saveCurrentWeightForTheCurrentAngleInsideOfUnitsOf360.Length; i++)
            {
                weights = perc.GetWeights();
                saveCurrentWeightForTheCurrentAngleInsideOfUnitsOf360[i] = weights;
            }
        }
        float angle = 0.0f;
        float angleRounded = 0.0f;
        float currentDiff = 0.0f;
        float currentQuarterRoundedAngle = 0.0f;




        public void UpdatePerceptron(data_input _data_input)
        {
            swtchwaypointtype = _data_input.swtchwaypointtype;

            this.northpoletransform = _data_input.playerPos;
            this.compasspivot = _data_input.dronePos;


            _guessedCorrectRight = 0;
            _guessedCorrectLeft = 0;

            compassPos = new Vector2(compasspivot.X, compasspivot.Y);

            if (swtchwaypointtype == 0)
            {   
                /*var right = compasspivot.transform.right;
                 right.y = 0;
                 right *= Mathf.Sign(compasspivot.transform.up.y);
                 var fwd = Vector3.Cross(right, Vector3.up).normalized;
                 float pitch = Vector3.Angle(fwd, compasspivot.transform.forward) * Mathf.Sign(compasspivot.transform.forward.y);
                 */

                angle = sc_maths.ClampValue(_data_input.angleZ, 0, SC_anglesQuarterNumber); //compasspivot.transform.eulerAngles.z
            }
            else if (swtchwaypointtype == 1)
            {
                angle = sc_maths.ClampValue(_data_input.angleY, 0, SC_anglesQuarterNumber); //compasspivot.transform.eulerAngles.y

            }
            else if (swtchwaypointtype == 2)
            {
                angle = sc_maths.ClampValue(_data_input.angleX, 0, SC_anglesQuarterNumber); //compasspivot.transform.eulerAngles.x
            }



            angleRounded = (float)Math.Round(angle);
            currentDiff = (angle - angleRounded);
            currentQuarterRoundedAngle = (float)(Math.Round(currentDiff * SC_Angle_Divider) / SC_Angle_Divider);
            currentQuarterRoundedAngle *= 100;
            currentQuarterRoundedAngle = (angle * SC_Angle_Divider);
            currentQuarterRoundedAngle = sc_maths.ClampValue(currentQuarterRoundedAngle, 0, SC_anglesQuarterNumber);

            weights = perc.GetWeights();

            if ((int)currentQuarterRoundedAngle < saveCurrentWeightForTheCurrentAngleInsideOfUnitsOf360.Length)
            {
                perc._SC_Perceptron_SetRotWeights(saveCurrentWeightForTheCurrentAngleInsideOfUnitsOf360[(int)currentQuarterRoundedAngle]);

                if (swtchwaypointtype == 0)
                {
                    //Vector2 dirbulletprimerright = new Vector2(compasspivot.transform.right.x, compasspivot.transform.right.y);

                    Vector2 dirbulletprimerright = new Vector2(_data_input.dronePosDirRight.X, _data_input.dronePosDirRight.Y);
                    
                    dirbulletprimerright.Normalize();

                    Vector2 dirprimertonorthpoletransform = new Vector2(northpoletransform.X, northpoletransform.Y) - new Vector2(compasspivot.X, compasspivot.Y);
                    dirprimertonorthpoletransform.Normalize();

                    _dotGoal = sc_maths.Dot(dirbulletprimerright.X, dirbulletprimerright.Y, dirprimertonorthpoletransform.X, dirprimertonorthpoletransform.Y);

                    if (_dotGoal >= 0) // 0.001f
                    {
                        answer = 1;
                    }
                    else if (_dotGoal < 0) //-0.001f
                    {
                        answer = -1;
                    }
                }
                else if (swtchwaypointtype == 1)
                {
                    //Vector2 dirbulletprimerright = new Vector2(-compasspivot.transform.right.z,compasspivot.transform.right.x);
                    Vector2 dirbulletprimerright = new Vector2(-_data_input.dronePosDirRight.Z, _data_input.dronePosDirRight.X);

                    dirbulletprimerright.Normalize();

                    Vector2 dirprimertonorthpoletransform = new Vector2(northpoletransform.X, northpoletransform.Z) - new Vector2(compasspivot.X, compasspivot.Z);
                    dirprimertonorthpoletransform.Normalize();

                    _dotGoal = sc_maths.Dot(dirbulletprimerright.X, dirbulletprimerright.Y, dirprimertonorthpoletransform.X, dirprimertonorthpoletransform.Y);

                    if (_dotGoal >= 0) // 0.001f
                    {
                        answer = 1;
                    }
                    else if (_dotGoal < 0)//-0.001f
                    {
                        answer = -1;
                    }
                }
                else if (swtchwaypointtype == 2)
                {
                    Vector2 dirbulletprimerright = new Vector2(-_data_input.dronePosDirForward.Z, _data_input.dronePosDirForward.Y);
                    //Vector2 dirbulletprimerright = new Vector2(compasspivot.transform.forward.z, compasspivot.transform.forward.y);
                    dirbulletprimerright.Normalize();

                    Vector2 dirprimertonorthpoletransform =new Vector2(compasspivot.Z, compasspivot.Y) - new Vector2(northpoletransform.Z, northpoletransform.Y);
                    dirprimertonorthpoletransform.Normalize();

                    _dotGoal = sc_maths.Dot(dirbulletprimerright.X, dirbulletprimerright.Y, dirprimertonorthpoletransform.X, dirprimertonorthpoletransform.Y);

                    if (_dotGoal >= 0) // 0.001f
                    {
                        answer = 1;
                    }
                    else if (_dotGoal < 0) //-0.001f
                    {
                        answer = -1;
                    }
                }









                if (swtchwaypointtype ==0)
                {
                    compassPos = new Vector2(compasspivot.X, compasspivot.Y);

                    for (int i = 0; i < training.Length; i++)
                    {
                        double angleInRadians = random.Next(360) / (2 * Math.PI);

                        // randomly getting a point at the location of the compass
                        float x = (float)(0.001f * Math.Cos(angleInRadians) + compassPos.X);
                        float y = (float)(0.001f * Math.Sin(angleInRadians) + compassPos.Y);

                        training[i] = new Trainer(weightsNumber, x, y, answer);
                        perc.Train(training[i].inputs, training[i].answer);
                    }
                }
                else if (swtchwaypointtype == 1)
                {
                    compassPos = new Vector2(-compasspivot.Z, compasspivot.X);

                    for (int i = 0; i < training.Length; i++)
                    {
                        double angleInRadians = random.Next(360) / (2 * Math.PI);

                        // randomly getting a point at the location of the compass
                        float x = (float)(0.001f * Math.Cos(angleInRadians) + compassPos.X);
                        float y = (float)(0.001f * Math.Sin(angleInRadians) + compassPos.Y);

                        training[i] = new Trainer(weightsNumber, x, y, answer);
                        perc.Train(training[i].inputs, training[i].answer);
                    }
                }
                else if (swtchwaypointtype == 2)
                {
                    compassPos = new Vector2(compasspivot.Z, compasspivot.Y);

                    for (int i = 0; i < training.Length; i++)
                    {
                        double angleInRadians = random.Next(360) / (2 * Math.PI);

                        // randomly getting a point at the location of the compass
                        float x = (float)(0.001f * Math.Cos(angleInRadians) + compassPos.X);
                        float y = (float)(0.001f * Math.Sin(angleInRadians) + compassPos.Y);

                        training[i] = new Trainer(weightsNumber, x, y, answer);
                        perc.Train(training[i].inputs, training[i].answer);
                    }
                }








                guessedCorrect = 0;
                guessedWrong = 0;

                turnRight = 0;
                turnLeft = 0;

                for (int i = 0; i < training.Length; i++)
                {
                    int guess = perc.Guess(training[i].inputs);
                    //Vector2 neededPos = new Vector2(training[i].inputs[0], training[i].inputs[1]);

                    if (training[i].answer == 1)
                    {
                        turnRight++;
                    }
                    else if (training[i].answer == -1)
                    {
                        turnLeft++;
                    }

                    if (guess >= 0)
                    {

                        guessedCorrect++;
                    }
                    else
                    {
                        guessedWrong++;
                    }
                }

                if (guessedCorrect >= (training.Length * 0.5f) - errormargin|| // if the guess is higher than half of training.length
                   guessedWrong >= (training.Length * 0.5f) - errormargin ||
                   guessedCorrect <= (training.Length * 0.5f) + errormargin ||
                   guessedWrong <= (training.Length * 0.5f) + errormargin)
                {
                    if (turnRight >= (training.Length * 0.5f) - errormargin||
                        turnLeft >= (training.Length * 0.5f) - errormargin ||
                        turnRight <= (training.Length * 0.5f) + errormargin ||
                        turnLeft <= (training.Length * 0.5f) + errormargin)
                    {
                        if (turnRight > turnLeft)
                        {
                            _guessedCorrectRight++;
                        }
                        else if (turnRight < turnLeft)
                        {
                            _guessedCorrectLeft++;
                        }
                        else
                        {
                            randguess = (int)(Math.Floor(sc_maths.getSomeRandNumThousandDecimal(0, 2, 0))); // random value between 0 and 1

                            if (randguess == 0)
                            {
                                _guessedCorrectRight++;
                            }
                            else
                            {
                                _guessedCorrectLeft++;
                            }
                            //Debug.Log("Data is too similar");
                        }
                    }
                    else
                    {
                        randguess = (int)(Math.Floor(sc_maths.getSomeRandNumThousandDecimal(0, 2, 0))); // random value between 0 and 1

                        if (randguess == 0)
                        {
                            _guessedCorrectRight++;
                        }
                        else
                        {
                            _guessedCorrectLeft++;
                        }
                        //Debug.Log("Data is too similar");
                    }
                }
                else
                {
                    randguess = (int)(Math.Floor(sc_maths.getSomeRandNumThousandDecimal(0, 2,0))); // random value between 0 and 1

                    if (randguess == 0)
                    {
                        _guessedCorrectRight++;
                    }
                    else
                    {
                        _guessedCorrectLeft++;
                    }
                    //Debug.Log("Data is too similar 0");
                }
                saveCurrentWeightForTheCurrentAngleInsideOfUnitsOf360[(int)currentQuarterRoundedAngle] = perc.GetWeights();
            }
            else
            {
                Console.WriteLine("out of range: " + currentQuarterRoundedAngle);
            }
        }
    }
}
