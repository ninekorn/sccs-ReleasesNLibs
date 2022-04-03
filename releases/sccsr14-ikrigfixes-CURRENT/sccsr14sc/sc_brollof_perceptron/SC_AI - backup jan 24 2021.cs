//using UnityEngine;
using System;
using Perceptron;
using System.Collections;

using SharpDX;

namespace SCCoreSystems
{
    public class SC_AI//: MonoBehaviour
    {

        public struct data_input
        {
            public Vector2 playerPos;
            public Vector2 playerDirForward;
            public Vector2 playerDirRight;
            public Vector2 playerDirUp;

            public Vector2 dronePos;
            public Vector2 dronePosDirForward;
            public Vector2 dronePosDirRight;
            public Vector2 dronePosDirUp;

            public Vector2 formationWaypoint;

            public float formationWaypointOffsetX;
            public float formationWaypointOffsetY;
            public float formationWaypointOffsetZ;

            //public Vector2? oculusRiftPos;

            public float angleX;
            public float angleY;
            public float angleZ;

        }





        public int linearframeguessselection = 0;

        public int inputsNumber = 3; // 3 minimum i think
        public int SC_Angle_Divider = 4;
        public int SC_anglesNumber = 360;
        public int errormargin = 3;
        public int weightsNumber = 3;

        public int SC_anglesQuarterNumber; //SC_Angle_Divider * SC_anglesNumber

        public float[][] saveCurrentWeightForTheCurrentAngleInsideOfUnitsOf360;// = new float[SC_anglesQuarterNumber][];

        public int swtchwaypointtype = 0;
        public Vector3 northpoletransform;
        public Vector3 compasspivot;
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
        float waitforseconds = 0;



        /*Matrix player_matrix;
        Matrix drone_matrix;
        public SC_AI(Matrix dronePos, Matrix playerPos)
        {
            this.player_matrix = playerPos;
            this.drone_matrix = dronePos;
            Starter();
        }*/


        public SC_AI(Vector3 compassOriginPos, Vector3 northpoleOrBullseyePos) //, float waitforsecondsswtch_, float waitforseconds_
        {
            //training = new Trainer[inputsNumber];
            this.northpoletransform = northpoleOrBullseyePos;
            this.compasspivot = compassOriginPos;

            //this.waitforseconds = waitforseconds_;
            //initWaitforSeconds(waitforsecondsswtch_);
            //waitforsecondsclass = new WaitForSeconds(waitforsecondsswtch_);
            //Starter(maxPerceptronInstancesneurons, perceptronLearningRate);

            //sccsaiguessInitVariables();
        }



        public void sccsaiguessInitVariables(int maxPerceptronInstancesneurons, float perceptronLearningRate)
        {
            training = new Trainer[inputsNumber];
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

            //initWaitforSeconds(waitforsecondsswtch_);
            //waitforsecondsclass = new WaitForSeconds(waitforsecondsswtch_);
            //Starter(maxPerceptronInstancesneurons, perceptronLearningRate);
        }

        /*void Starter(int maxPerceptronInstancesneurons, float perceptronLearningRate)
        {
            SC_anglesQuarterNumber = SC_anglesNumber * SC_Angle_Divider;
            saveCurrentWeightForTheCurrentAngleInsideOfUnitsOf360 = new float[SC_anglesQuarterNumber][];

            random = new System.Random();
            perc = new Perceptron.BrollofPerceptron(maxPerceptronInstancesneurons, perceptronLearningRate);

            //perceptronLearningRate = sc_maths.getSomeRandNumThousandDecimal();

            for (int i = 0; i < saveCurrentWeightForTheCurrentAngleInsideOfUnitsOf360.Length; i++)
            {
                weights = perc.GetWeights();
                saveCurrentWeightForTheCurrentAngleInsideOfUnitsOf360[i] = weights;
            }
        }*/

        //public void initWaitforSeconds(float waitforsecondsswtch_)
        //{
        //    //waitforsecondsclass = new WaitForSeconds(waitforsecondsswtch_);
        //}

        //WaitForSeconds waitforsecondsclass;// = new WaitForSeconds();
        //int waitforsecondsswtch = 0;



        float pointRightX = 0;
        float pointRightY = 0;

        Vector2 getFormationWaypoint(data_input _data_input)
        {
            //Vector2 coordsPlayer = new Vector2(player_pos.Y, player_pos.Y);
            //Vector2 rightDirPlayer = new Vector2(direction_right_player.X, -player.right.z);

            pointRightX = _data_input.playerPos.X + ((_data_input.playerDirRight.X * _data_input.formationWaypointOffsetX));
            pointRightY = _data_input.playerPos.Y + ((_data_input.playerDirRight.Y * _data_input.formationWaypointOffsetY));

            return new Vector2(pointRightX, pointRightY);
        }


        public void UpdatePerceptron(data_input _data_input)
        //public void UpdatePerceptron(int waitforsecondsswtch_, float waitforseconds_) //IEnumerator
        {

            /*if (waitforsecondsswtch_ == 1)
            {
                waitforsecondsswtch = 1;
            }

            if (waitforsecondsswtch == 1)
            {
                waitforsecondsclass = new WaitForSeconds(waitforsecondsswtch_);
                waitforsecondsswtch = 0;
            }*/

            try
            {
                if (perc != null)
                {
                    _guessedCorrectRight = 0;
                    _guessedCorrectLeft = 0;

                    /*if (compasspivot != null && compasspivot.transform != null)
                    {

                    }*/

                    compassPos = new Vector2(_data_input.dronePos.X, _data_input.dronePos.Y);
                    northpolepos = new Vector2(_data_input.playerPos.X, _data_input.playerPos.Y);
                    this.northpoletransform = new Vector3(northpolepos.X, northpolepos.Y, 0);
                    this.compasspivot = new Vector3(compassPos.X, compassPos.Y, 0);

                    float currentQuarterRoundedAngle = 0.0f;
                    float angle = 0.0f;

                    if (swtchwaypointtype == 0)
                    {
                        angle = sc_maths.ClampValue(_data_input.angleZ, 0, SC_anglesQuarterNumber); //compasspivot.transform.eulerAngles.x
                        var angleRounded = Math.Round(angle);
                        var currentDiff = (angle - angleRounded);
                        currentQuarterRoundedAngle = (float)(Math.Round(currentDiff * SC_Angle_Divider) / SC_Angle_Divider);
                        currentQuarterRoundedAngle *= 100;
                        currentQuarterRoundedAngle = (angle * SC_Angle_Divider);
                        currentQuarterRoundedAngle = sc_maths.ClampValue(currentQuarterRoundedAngle, 0, SC_anglesQuarterNumber);
                        weights = perc.GetWeights();
                    }
                    else if (swtchwaypointtype == 1)
                    {
                        angle = sc_maths.ClampValue(_data_input.angleZ, 0, SC_anglesQuarterNumber); //compasspivot.transform.eulerAngles.z
                        var angleRounded = Math.Round(angle);
                        var currentDiff = (angle - angleRounded);
                        currentQuarterRoundedAngle = (float)(Math.Round(currentDiff * SC_Angle_Divider) / SC_Angle_Divider);
                        currentQuarterRoundedAngle *= 100;
                        currentQuarterRoundedAngle = (angle * SC_Angle_Divider);
                        currentQuarterRoundedAngle = sc_maths.ClampValue(currentQuarterRoundedAngle, 0, SC_anglesQuarterNumber);
                        weights = perc.GetWeights();
                    }

                    if ((int)currentQuarterRoundedAngle < saveCurrentWeightForTheCurrentAngleInsideOfUnitsOf360.Length)
                    {
                        //Console.WriteLine("test0");
                        perc._SC_Perceptron_SetRotWeights(saveCurrentWeightForTheCurrentAngleInsideOfUnitsOf360[(int)currentQuarterRoundedAngle]);

                        float pointForwardDirNPCX = (float)(1 * Math.Cos(Math.PI * angle / 180.0)) + compassPos.X; // * Math.PI / 180
                        float pointForwardDirNPCY = (float)(1 * Math.Sin(Math.PI * angle / 180.0)) + compassPos.Y;

                        Vector2 dirRightNPC = new Vector2(pointForwardDirNPCY - compassPos.Y, -1 * (pointForwardDirNPCX - compassPos.X));
                        Vector2 dirNPCToPlayer = new Vector2(northpolepos.X - compassPos.X, northpolepos.Y - compassPos.Y);

                        var someOtherMAG = (float)Math.Sqrt((dirNPCToPlayer.X * dirNPCToPlayer.X) + (dirNPCToPlayer.Y * dirNPCToPlayer.Y));
                        dirNPCToPlayer.X /= someOtherMAG;
                        dirNPCToPlayer.Y /= someOtherMAG;

                        if (swtchwaypointtype == 0)
                        {
                            /*Vector2 dirbulletprimerright = new Vector2(compasspivot.transform.right.z, -compasspivot.transform.right.x);
                            dirbulletprimerright.Normalize();

                            //var alignedDirectionWithPlayerDirectionRIGHTDOT = SC_Utilities.Dot(nData.nForward.y, -nData.nForward.x, pData.pForward.x, pData.pForward.y);
                            //Vector2 dirbulletprimerforward = new Vector2(compasspivot.transform.up.x, compasspivot.transform.up.y);
                            //dirbulletprimerforward.Normalize();

                            Vector2 dirprimertonorthpoletransform = new Vector2(compasspivot.position.x, compasspivot.position.y) - new Vector2(northpoletransform.x, northpoletransform.y);
                            dirprimertonorthpoletransform.Normalize();

                            //Vector3 dirprimertonorthpoletransform = new Vector3(northpoletransform.x, northpoletransform.y, northpoletransform.z) - new Vector3(compasspivot.position.x, compasspivot.position.y, compasspivot.position.z);
                            //dirprimertonorthpoletransform.Normalize();

                            _dotGoal = sc_maths.Dot(dirbulletprimerright.x, dirbulletprimerright.y, dirprimertonorthpoletransform.x, dirprimertonorthpoletransform.y);

                            if (_dotGoal >= 0) // 0.001f
                            {
                                answer = 1;
                            }
                            else if (_dotGoal < 0) //-0.001f
                            {
                                answer = -1;
                            }*/

                            /*Vector2 dirbulletprimerright = new Vector2(compasspivot.transform.forward.z, compasspivot.transform.forward.y);
                            dirbulletprimerright.Normalize();

                            //var alignedDirectionWithPlayerDirectionRIGHTDOT = SC_Utilities.Dot(nData.nForward.y, -nData.nForward.x, pData.pForward.x, pData.pForward.y);
                            //Vector2 dirbulletprimerforward = new Vector2(compasspivot.transform.up.x, compasspivot.transform.up.y);
                            //dirbulletprimerforward.Normalize();

                            Vector2 dirprimertonorthpoletransform = new Vector2(compasspivot.position.z, compasspivot.position.y) - new Vector2(northpoletransform.z, northpoletransform.y);
                            dirprimertonorthpoletransform.Normalize();

                            _dotGoal = sc_maths.Dot(dirbulletprimerright.x, dirbulletprimerright.y, dirprimertonorthpoletransform.x, dirprimertonorthpoletransform.y);

                            if (_dotGoal >= 0) // 0.001f
                            {
                                answer = 1;
                            }
                            else if (_dotGoal < 0) // -0.001f
                            {
                                answer = -1;
                            }*/

                            /*Vector3 dirprimertonorthpoletransform = compasspivot.position - northpoletransform;
                            dirprimertonorthpoletransform.Normalize();

                            Vector3 forward = compasspivot.transform.right;
                            forward.y = 0;
                            forward.z = 0;

                            //dirprimertonorthpoletransform.y = 0;
                            dirprimertonorthpoletransform.z = 0;

                            _dotGoal = Vector3.Dot(forward, dirprimertonorthpoletransform);
                            */

                            //Debug.Log(_dotGoal);
                            /*Vector2 dirbulletprimerright = new Vector2(compasspivot.transform.forward.x, compasspivot.transform.forward.z);
                            dirbulletprimerright.Normalize();

                            //var alignedDirectionWithPlayerDirectionRIGHTDOT = SC_Utilities.Dot(nData.nForward.y, -nData.nForward.x, pData.pForward.x, pData.pForward.y);

                            //Vector2 dirbulletprimerforward = new Vector2(compasspivot.transform.up.x, compasspivot.transform.up.y);
                            //dirbulletprimerforward.Normalize();

                            Vector2 dirprimertonorthpoletransform = new Vector2(compasspivot.position.x, compasspivot.position.y) - new Vector2(northpoletransform.x, northpoletransform.y);
                            dirprimertonorthpoletransform.Normalize();

                            //Vector3 dirprimertonorthpoletransform = new Vector3(northpoletransform.x, northpoletransform.y, northpoletransform.z) - new Vector3(compasspivot.position.x, compasspivot.position.y, compasspivot.position.z);
                            //dirprimertonorthpoletransform.Normalize();

                            _dotGoal = sc_maths.Dot(dirbulletprimerright.x, dirbulletprimerright.y, dirprimertonorthpoletransform.x, dirprimertonorthpoletransform.y);

                            if (_dotGoal >= 0) // 0.001f
                            {
                                answer = 1;
                            }
                            else if (_dotGoal < 0) //-0.001f
                            {
                                answer = -1;
                            }*/

                            Vector2 dirbulletprimerright = new Vector2(compasspivot.Z, compasspivot.Y);
                            dirbulletprimerright.Normalize();
                            //dirbulletprimerright.y = 0;

                            Vector2 dirprimertonorthpoletransform = new Vector2(northpoletransform.X, northpoletransform.Y) - new Vector2(compasspivot.X, compasspivot.Y);
                            dirprimertonorthpoletransform.Normalize();
                            //dirprimertonorthpoletransform.y = 0;

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
                        else if (swtchwaypointtype == 1)
                        {
                            Vector2 dirbulletprimerright = new Vector2(compasspivot.X, compasspivot.Z);
                            dirbulletprimerright.Normalize();

                            //var alignedDirectionWithPlayerDirectionRIGHTDOT = SC_Utilities.Dot(nData.nForward.y, -nData.nForward.x, pData.pForward.x, pData.pForward.y);
                            //Vector2 dirbulletprimerforward = new Vector2(compasspivot.transform.up.x, compasspivot.transform.up.y);
                            //dirbulletprimerforward.Normalize();

                            Vector2 dirprimertonorthpoletransform = new Vector2(compasspivot.X, compasspivot.Z) - new Vector2(northpoletransform.X, northpoletransform.Z);
                            dirprimertonorthpoletransform.Normalize();

                            //Vector3 dirprimertonorthpoletransform = new Vector3(northpoletransform.x, northpoletransform.y, northpoletransform.z) - new Vector3(compasspivot.position.x, compasspivot.position.y, compasspivot.position.z);
                            //dirprimertonorthpoletransform.Normalize();

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

                        //incomplete
                        if (swtchwaypointtype == 0)
                        {
                            compassPos = new Vector2(compasspivot.Z, compasspivot.Y);

                            //if (linearframeguessselection == 0)
                            {
                                for (int i = 0; i < training.Length; i++)
                                {
                                    if (i == 0) // Left
                                    {
                                        //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                        //double angleDeg = random.Next(360) / (2 * Math.PI);

                                        // randomly getting a point at the location of the compass
                                        float x = (float)(0.001f * Math.Cos(179.123f) + compassPos.X); //179.9999876543210123456789f
                                        float y = (float)(0.001f * Math.Sin(179.123f) + compassPos.Y); //179.9999876543210123456789f

                                        training[i] = new Trainer(weightsNumber, x, y, answer);
                                        perc.Train(training[i].inputs, training[i].answer);
                                    }
                                    else if (i == 1) //right 
                                    {
                                        //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                        //double angleDeg = random.Next(360) / (2 * Math.PI);

                                        // randomly getting a point at the location of the compass
                                        float x = (float)(0.001f * Math.Cos(0.00123f) + compassPos.X);
                                        float y = (float)(0.001f * Math.Sin(0.00123f) + compassPos.Y);

                                        training[i] = new Trainer(weightsNumber, x, y, answer);
                                        perc.Train(training[i].inputs, training[i].answer);
                                    }
                                    else if (i == 2)
                                    {
                                        //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                        double angleDeg = random.Next(360) / (2 * Math.PI);

                                        // randomly getting a point at the location of the compass
                                        float x = (float)(0.001f * Math.Cos(angleDeg) + compassPos.X);
                                        float y = (float)(0.001f * Math.Sin(angleDeg) + compassPos.Y);

                                        training[i] = new Trainer(weightsNumber, x, y, answer);
                                        perc.Train(training[i].inputs, training[i].answer);
                                    }
                                }
                                //linearframeguessselection = 1;
                            }



                            /*else if (linearframeguessselection == 1)
                            {
                                for (int i = 0; i < training.Length; i++)
                                {
                                    if (i == 2) // Left
                                    {
                                        //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                        //double angleDeg = random.Next(360) / (2 * Math.PI);

                                        // randomly getting a point at the location of the compass
                                        float x = (float)(0.001f * Math.Cos(179.123f) + compassPos.X); //179.9999876543210123456789f
                                        float y = (float)(0.001f * Math.Sin(179.123f) + compassPos.Y); //179.9999876543210123456789f

                                        training[i] = new Trainer(weightsNumber, x, y, answer);
                                        perc.Train(training[i].inputs, training[i].answer);
                                    }
                                    else if (i == 0) //right 
                                    {
                                        //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                        //double angleDeg = random.Next(360) / (2 * Math.PI);

                                        // randomly getting a point at the location of the compass
                                        float x = (float)(0.001f * Math.Cos(0.00123f) + compassPos.X);
                                        float y = (float)(0.001f * Math.Sin(0.00123f) + compassPos.Y);

                                        training[i] = new Trainer(weightsNumber, x, y, answer);
                                        perc.Train(training[i].inputs, training[i].answer);
                                    }
                                    else if (i == 1)
                                    {
                                        //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                        double angleDeg = random.Next(360) / (2 * Math.PI);

                                        // randomly getting a point at the location of the compass
                                        float x = (float)(0.001f * Math.Cos(angleDeg) + compassPos.X);
                                        float y = (float)(0.001f * Math.Sin(angleDeg) + compassPos.Y);

                                        training[i] = new Trainer(weightsNumber, x, y, answer);
                                        perc.Train(training[i].inputs, training[i].answer);
                                    }
                                }
                                linearframeguessselection = 2;
                            }
                            else if (linearframeguessselection == 2)
                            {

                                for (int i = 0; i < training.Length; i++)
                                {
                                    if (i == 1) // Left
                                    {
                                        //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                        //double angleDeg = random.Next(360) / (2 * Math.PI);

                                        // randomly getting a point at the location of the compass
                                        float x = (float)(0.001f * Math.Cos(179.123f) + compassPos.X); //179.9999876543210123456789f
                                        float y = (float)(0.001f * Math.Sin(179.123f) + compassPos.Y); //179.9999876543210123456789f

                                        training[i] = new Trainer(weightsNumber, x, y, answer);
                                        perc.Train(training[i].inputs, training[i].answer);
                                    }
                                    else if (i == 2) //right 
                                    {
                                        //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                        //double angleDeg = random.Next(360) / (2 * Math.PI);

                                        // randomly getting a point at the location of the compass
                                        float x = (float)(0.001f * Math.Cos(0.00123f) + compassPos.X);
                                        float y = (float)(0.001f * Math.Sin(0.00123f) + compassPos.Y);

                                        training[i] = new Trainer(weightsNumber, x, y, answer);
                                        perc.Train(training[i].inputs, training[i].answer);
                                    }
                                    else if (i == 0)
                                    {
                                        //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                        double angleDeg = random.Next(360) / (2 * Math.PI);

                                        // randomly getting a point at the location of the compass
                                        float x = (float)(0.001f * Math.Cos(angleDeg) + compassPos.X);
                                        float y = (float)(0.001f * Math.Sin(angleDeg) + compassPos.Y);

                                        training[i] = new Trainer(weightsNumber, x, y, answer);
                                        perc.Train(training[i].inputs, training[i].answer);
                                    }
                                }
                                linearframeguessselection = 3;
                            }
                            else if (linearframeguessselection == 3)
                            {

                                for (int i = 0; i < training.Length; i++)
                                {
                                    if (i == 2) // Left
                                    {
                                        //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                        //double angleDeg = random.Next(360) / (2 * Math.PI);

                                        // randomly getting a point at the location of the compass
                                        float x = (float)(0.001f * Math.Cos(179.123f) + compassPos.X); //179.9999876543210123456789f
                                        float y = (float)(0.001f * Math.Sin(179.123f) + compassPos.Y); //179.9999876543210123456789f

                                        training[i] = new Trainer(weightsNumber, x, y, answer);
                                        perc.Train(training[i].inputs, training[i].answer);
                                    }
                                    else if (i == 1) //right 
                                    {
                                        //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                        //double angleDeg = random.Next(360) / (2 * Math.PI);

                                        // randomly getting a point at the location of the compass
                                        float x = (float)(0.001f * Math.Cos(0.00123f) + compassPos.X);
                                        float y = (float)(0.001f * Math.Sin(0.00123f) + compassPos.Y);

                                        training[i] = new Trainer(weightsNumber, x, y, answer);
                                        perc.Train(training[i].inputs, training[i].answer);
                                    }
                                    else if (i == 0)
                                    {
                                        //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                        double angleDeg = random.Next(360) / (2 * Math.PI);

                                        // randomly getting a point at the location of the compass
                                        float x = (float)(0.001f * Math.Cos(angleDeg) + compassPos.X);
                                        float y = (float)(0.001f * Math.Sin(angleDeg) + compassPos.Y);

                                        training[i] = new Trainer(weightsNumber, x, y, answer);
                                        perc.Train(training[i].inputs, training[i].answer);
                                    }
                                }
                                linearframeguessselection = 4;
                            }
                            else if (linearframeguessselection == 4)
                            {

                                for (int i = 0; i < training.Length; i++)
                                {
                                    if (i == 1) // Left
                                    {
                                        //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                        //double angleDeg = random.Next(360) / (2 * Math.PI);

                                        // randomly getting a point at the location of the compass
                                        float x = (float)(0.001f * Math.Cos(179.123f) + compassPos.X); //179.9999876543210123456789f
                                        float y = (float)(0.001f * Math.Sin(179.123f) + compassPos.Y); //179.9999876543210123456789f

                                        training[i] = new Trainer(weightsNumber, x, y, answer);
                                        perc.Train(training[i].inputs, training[i].answer);
                                    }
                                    else if (i == 0) //right 
                                    {
                                        //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                        //double angleDeg = random.Next(360) / (2 * Math.PI);

                                        // randomly getting a point at the location of the compass
                                        float x = (float)(0.001f * Math.Cos(0.00123f) + compassPos.X);
                                        float y = (float)(0.001f * Math.Sin(0.00123f) + compassPos.Y);

                                        training[i] = new Trainer(weightsNumber, x, y, answer);
                                        perc.Train(training[i].inputs, training[i].answer);
                                    }
                                    else if (i == 2)
                                    {
                                        //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                        double angleDeg = random.Next(360) / (2 * Math.PI);

                                        // randomly getting a point at the location of the compass
                                        float x = (float)(0.001f * Math.Cos(angleDeg) + compassPos.X);
                                        float y = (float)(0.001f * Math.Sin(angleDeg) + compassPos.Y);

                                        training[i] = new Trainer(weightsNumber, x, y, answer);
                                        perc.Train(training[i].inputs, training[i].answer);
                                    }
                                }
                                linearframeguessselection = 5;
                            }
                            else if (linearframeguessselection >= 5)
                            {

                                for (int i = 0; i < training.Length; i++)
                                {
                                    if (i == 0) // Left
                                    {
                                        //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                        //double angleDeg = random.Next(360) / (2 * Math.PI);

                                        // randomly getting a point at the location of the compass
                                        float x = (float)(0.001f * Math.Cos(179.123f) + compassPos.X); //179.9999876543210123456789f
                                        float y = (float)(0.001f * Math.Sin(179.123f) + compassPos.Y); //179.9999876543210123456789f

                                        training[i] = new Trainer(weightsNumber, x, y, answer);
                                        perc.Train(training[i].inputs, training[i].answer);
                                    }
                                    else if (i == 2) //right 
                                    {
                                        //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                        //double angleDeg = random.Next(360) / (2 * Math.PI);

                                        // randomly getting a point at the location of the compass
                                        float x = (float)(0.001f * Math.Cos(0.00123f) + compassPos.X);
                                        float y = (float)(0.001f * Math.Sin(0.00123f) + compassPos.Y);

                                        training[i] = new Trainer(weightsNumber, x, y, answer);
                                        perc.Train(training[i].inputs, training[i].answer);
                                    }
                                    else if (i == 1)
                                    {
                                        //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                        double angleDeg = random.Next(360) / (2 * Math.PI);

                                        // randomly getting a point at the location of the compass
                                        float x = (float)(0.001f * Math.Cos(angleDeg) + compassPos.X);
                                        float y = (float)(0.001f * Math.Sin(angleDeg) + compassPos.Y);

                                        training[i] = new Trainer(weightsNumber, x, y, answer);
                                        perc.Train(training[i].inputs, training[i].answer);
                                    }
                                }
                                linearframeguessselection = 0;
                            }*/



                            /*compassPos = new Vector2(compasspivot.x, compasspivot.y);
                            for (int i = 0; i < training.Length; i++)
                            {
                                double angleDeg = random.Next(360) / (2 * Math.PI);

                                // randomly getting a point at the location of the compass
                                float x = (int)(0.001f * Math.Cos(angleDeg) + compassPos.X);
                                float y = (int)(0.001f * Math.Sin(angleDeg) + compassPos.Y);

                                training[i] = new Trainer(weightsNumber, x, y, answer);
                                perc.Train(training[i].inputs, training[i].answer);
                            }*/
                        }
                        else if (swtchwaypointtype == 1)
                        {
                            compassPos = new Vector2(compasspivot.X, compasspivot.Z);
                            //if (linearframeguessselection == 0)
                            {
                                for (int i = 0; i < training.Length; i++)
                                {
                                    if (i == 0) // Left
                                    {
                                        //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                        //double angleDeg = random.Next(360) / (2 * Math.PI);

                                        // randomly getting a point at the location of the compass
                                        float x = (float)(0.001f * Math.Cos(179.123f) + compassPos.X); //179.9999876543210123456789f
                                        float y = (float)(0.001f * Math.Sin(179.123f) + compassPos.Y); //179.9999876543210123456789f

                                        training[i] = new Trainer(weightsNumber, x, y, answer);
                                        perc.Train(training[i].inputs, training[i].answer);
                                    }
                                    else if (i == 1) //right 
                                    {
                                        //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                        //double angleDeg = random.Next(360) / (2 * Math.PI);

                                        // randomly getting a point at the location of the compass
                                        float x = (float)(0.001f * Math.Cos(0.00123f) + compassPos.X);
                                        float y = (float)(0.001f * Math.Sin(0.00123f) + compassPos.Y);

                                        training[i] = new Trainer(weightsNumber, x, y, answer);
                                        perc.Train(training[i].inputs, training[i].answer);
                                    }
                                    else if (i == 2)
                                    {
                                        //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                        double angleDeg = random.Next(360) / (2 * Math.PI);

                                        // randomly getting a point at the location of the compass
                                        float x = (float)(0.001f * Math.Cos(angleDeg) + compassPos.X);
                                        float y = (float)(0.001f * Math.Sin(angleDeg) + compassPos.Y);

                                        training[i] = new Trainer(weightsNumber, x, y, answer);
                                        perc.Train(training[i].inputs, training[i].answer);
                                    }
                                }
                                //linearframeguessselection = 1;
                            }
                            /*else if (linearframeguessselection == 1)
                            {

                                for (int i = 0; i < training.Length; i++)
                                {
                                    if (i == 2) // Left
                                    {
                                        //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                        //double angleDeg = random.Next(360) / (2 * Math.PI);

                                        // randomly getting a point at the location of the compass
                                        float x = (float)(0.001f * Math.Cos(179.123f) + compassPos.X); //179.9999876543210123456789f
                                        float y = (float)(0.001f * Math.Sin(179.123f) + compassPos.Y); //179.9999876543210123456789f

                                        training[i] = new Trainer(weightsNumber, x, y, answer);
                                        perc.Train(training[i].inputs, training[i].answer);
                                    }
                                    else if (i == 0) //right 
                                    {
                                        //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                        //double angleDeg = random.Next(360) / (2 * Math.PI);

                                        // randomly getting a point at the location of the compass
                                        float x = (float)(0.001f * Math.Cos(0.00123f) + compassPos.X);
                                        float y = (float)(0.001f * Math.Sin(0.00123f) + compassPos.Y);

                                        training[i] = new Trainer(weightsNumber, x, y, answer);
                                        perc.Train(training[i].inputs, training[i].answer);
                                    }
                                    else if (i == 1)
                                    {
                                        //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                        double angleDeg = random.Next(360) / (2 * Math.PI);

                                        // randomly getting a point at the location of the compass
                                        float x = (float)(0.001f * Math.Cos(angleDeg) + compassPos.X);
                                        float y = (float)(0.001f * Math.Sin(angleDeg) + compassPos.Y);

                                        training[i] = new Trainer(weightsNumber, x, y, answer);
                                        perc.Train(training[i].inputs, training[i].answer);
                                    }
                                }
                                linearframeguessselection = 2;
                            }
                            else if (linearframeguessselection == 2)
                            {

                                for (int i = 0; i < training.Length; i++)
                                {
                                    if (i == 1) // Left
                                    {
                                        //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                        //double angleDeg = random.Next(360) / (2 * Math.PI);

                                        // randomly getting a point at the location of the compass
                                        float x = (float)(0.001f * Math.Cos(179.123f) + compassPos.X); //179.9999876543210123456789f
                                        float y = (float)(0.001f * Math.Sin(179.123f) + compassPos.Y); //179.9999876543210123456789f

                                        training[i] = new Trainer(weightsNumber, x, y, answer);
                                        perc.Train(training[i].inputs, training[i].answer);
                                    }
                                    else if (i == 2) //right 
                                    {
                                        //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                        //double angleDeg = random.Next(360) / (2 * Math.PI);

                                        // randomly getting a point at the location of the compass
                                        float x = (float)(0.001f * Math.Cos(0.00123f) + compassPos.X);
                                        float y = (float)(0.001f * Math.Sin(0.00123f) + compassPos.Y);

                                        training[i] = new Trainer(weightsNumber, x, y, answer);
                                        perc.Train(training[i].inputs, training[i].answer);
                                    }
                                    else if (i == 0)
                                    {
                                        //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                        double angleDeg = random.Next(360) / (2 * Math.PI);

                                        // randomly getting a point at the location of the compass
                                        float x = (float)(0.001f * Math.Cos(angleDeg) + compassPos.X);
                                        float y = (float)(0.001f * Math.Sin(angleDeg) + compassPos.Y);

                                        training[i] = new Trainer(weightsNumber, x, y, answer);
                                        perc.Train(training[i].inputs, training[i].answer);
                                    }
                                }
                                linearframeguessselection = 3;
                            }
                            else if (linearframeguessselection == 3)
                            {

                                for (int i = 0; i < training.Length; i++)
                                {
                                    if (i == 2) // Left
                                    {
                                        //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                        //double angleDeg = random.Next(360) / (2 * Math.PI);

                                        // randomly getting a point at the location of the compass
                                        float x = (float)(0.001f * Math.Cos(179.123f) + compassPos.X); //179.9999876543210123456789f
                                        float y = (float)(0.001f * Math.Sin(179.123f) + compassPos.Y); //179.9999876543210123456789f

                                        training[i] = new Trainer(weightsNumber, x, y, answer);
                                        perc.Train(training[i].inputs, training[i].answer);
                                    }
                                    else if (i == 1) //right 
                                    {
                                        //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                        //double angleDeg = random.Next(360) / (2 * Math.PI);

                                        // randomly getting a point at the location of the compass
                                        float x = (float)(0.001f * Math.Cos(0.00123f) + compassPos.X);
                                        float y = (float)(0.001f * Math.Sin(0.00123f) + compassPos.Y);

                                        training[i] = new Trainer(weightsNumber, x, y, answer);
                                        perc.Train(training[i].inputs, training[i].answer);
                                    }
                                    else if (i == 0)
                                    {
                                        //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                        double angleDeg = random.Next(360) / (2 * Math.PI);

                                        // randomly getting a point at the location of the compass
                                        float x = (float)(0.001f * Math.Cos(angleDeg) + compassPos.X);
                                        float y = (float)(0.001f * Math.Sin(angleDeg) + compassPos.Y);

                                        training[i] = new Trainer(weightsNumber, x, y, answer);
                                        perc.Train(training[i].inputs, training[i].answer);
                                    }
                                }
                                linearframeguessselection = 4;
                            }
                            else if (linearframeguessselection == 4)
                            {

                                for (int i = 0; i < training.Length; i++)
                                {
                                    if (i == 1) // Left
                                    {
                                        //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                        //double angleDeg = random.Next(360) / (2 * Math.PI);

                                        // randomly getting a point at the location of the compass
                                        float x = (float)(0.001f * Math.Cos(179.123f) + compassPos.X); //179.9999876543210123456789f
                                        float y = (float)(0.001f * Math.Sin(179.123f) + compassPos.Y); //179.9999876543210123456789f

                                        training[i] = new Trainer(weightsNumber, x, y, answer);
                                        perc.Train(training[i].inputs, training[i].answer);
                                    }
                                    else if (i == 0) //right 
                                    {
                                        //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                        //double angleDeg = random.Next(360) / (2 * Math.PI);

                                        // randomly getting a point at the location of the compass
                                        float x = (float)(0.001f * Math.Cos(0.00123f) + compassPos.X);
                                        float y = (float)(0.001f * Math.Sin(0.00123f) + compassPos.Y);

                                        training[i] = new Trainer(weightsNumber, x, y, answer);
                                        perc.Train(training[i].inputs, training[i].answer);
                                    }
                                    else if (i == 2)
                                    {
                                        //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                        double angleDeg = random.Next(360) / (2 * Math.PI);

                                        // randomly getting a point at the location of the compass
                                        float x = (float)(0.001f * Math.Cos(angleDeg) + compassPos.X);
                                        float y = (float)(0.001f * Math.Sin(angleDeg) + compassPos.Y);

                                        training[i] = new Trainer(weightsNumber, x, y, answer);
                                        perc.Train(training[i].inputs, training[i].answer);
                                    }
                                }
                                linearframeguessselection = 5;
                            }
                            else if (linearframeguessselection == 5)
                            {

                                for (int i = 0; i < training.Length; i++)
                                {
                                    if (i == 0) // Left
                                    {
                                        //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                        //double angleDeg = random.Next(360) / (2 * Math.PI);

                                        // randomly getting a point at the location of the compass
                                        float x = (float)(0.001f * Math.Cos(179.123f) + compassPos.X); //179.9999876543210123456789f
                                        float y = (float)(0.001f * Math.Sin(179.123f) + compassPos.Y); //179.9999876543210123456789f

                                        training[i] = new Trainer(weightsNumber, x, y, answer);
                                        perc.Train(training[i].inputs, training[i].answer);
                                    }
                                    else if (i == 2) //right 
                                    {
                                        //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                        //double angleDeg = random.Next(360) / (2 * Math.PI);

                                        // randomly getting a point at the location of the compass
                                        float x = (float)(0.001f * Math.Cos(0.00123f) + compassPos.X);
                                        float y = (float)(0.001f * Math.Sin(0.00123f) + compassPos.Y);

                                        training[i] = new Trainer(weightsNumber, x, y, answer);
                                        perc.Train(training[i].inputs, training[i].answer);
                                    }
                                    else if (i == 1)
                                    {
                                        //sccsgimbal v1.0 and v1.1 were using this random point around the position of the compass itself for a random guess. it's one way to do it but i prefer the instant choice of 2 points only, one left and one right and maybe a third random one.
                                        double angleDeg = random.Next(360) / (2 * Math.PI);

                                        // randomly getting a point at the location of the compass
                                        float x = (float)(0.001f * Math.Cos(angleDeg) + compassPos.X);
                                        float y = (float)(0.001f * Math.Sin(angleDeg) + compassPos.Y);

                                        training[i] = new Trainer(weightsNumber, x, y, answer);
                                        perc.Train(training[i].inputs, training[i].answer);
                                    }
                                }
                            }*/
                            linearframeguessselection = 0;
                        }





                        int guessedCorrect = 0;
                        int guessedWrong = 0;

                        int turnRight = 0;
                        int turnLeft = 0;

                        for (int i = 0; i < training.Length; i++)
                        {
                            int guess = 0;

                            if (perc != null)
                            {
                                if (training[i] != null)
                                {
                                    if (training[i].inputs != null)
                                    {
                                        guess = perc.Guess(training[i].inputs); //int guess = perc.Guess(training[i].inputs);

                                    }
                                    else
                                    {
                                        Console.WriteLine("null training inputs");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("null training " + i);
                                }
                            }
                            else
                            {
                                Console.WriteLine("null perc");
                            }


                            //Vector2 neededPos = new Vector2(training[i].inputs[0], training[i].inputs[1]);

                            /*if (training[i].answer == 1)
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
                            }*/
                        }

                        if (guessedCorrect >= (training.Length * 0.5f) - errormargin || // if the guess is higher than half of training.length
                           guessedWrong >= (training.Length * 0.5f) - errormargin ||
                           guessedCorrect <= (training.Length * 0.5f) + errormargin ||
                           guessedWrong <= (training.Length * 0.5f) + errormargin)
                        {
                            if (turnRight >= (training.Length * 0.5f) - errormargin ||
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
                                    randguess = (int)(Math.Floor(sc_maths.getSomeRandNumThousandDecimal(0, 2, 0, 0))); // random value between 0 and 1

                                    if (randguess == 0)
                                    {
                                        _guessedCorrectRight++;
                                    }
                                    else
                                    {
                                        _guessedCorrectLeft++;
                                    }
                                    //Debug.Log("Data is too similar");
                                    //Debug.Log(randguess);
                                }
                            }
                            else
                            {
                                randguess = (int)(Math.Floor(sc_maths.getSomeRandNumThousandDecimal(0, 2, 0, 0))); // random value between 0 and 1

                                if (randguess == 0)
                                {
                                    _guessedCorrectRight++;
                                }
                                else
                                {
                                    _guessedCorrectLeft++;
                                }
                                //Debug.Log("Data is too similar");
                                //Debug.Log(randguess);
                            }
                        }
                        else
                        {
                            randguess = (int)(Math.Floor(sc_maths.getSomeRandNumThousandDecimal(0, 2, 0,0))); // random value between 0 and 1

                            if (randguess == 0)
                            {
                                _guessedCorrectRight++;
                            }
                            else
                            {
                                _guessedCorrectLeft++;
                            }
                            //Debug.Log("Data is too similar 0");
                            //Debug.Log(randguess);
                        }
                        saveCurrentWeightForTheCurrentAngleInsideOfUnitsOf360[(int)currentQuarterRoundedAngle] = perc.GetWeights();
                    }
                    else
                    {
                        //Debug.Log("out of range: " + currentQuarterRoundedAngle);
                    }


                    //linearframeguessselection++;
                    //yield return waitforsecondsclass;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}