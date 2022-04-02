using System.Collections;
using System.Collections.Generic;
//using UnityEngine;
using System;

using SharpDX; 

using Perceptron;
//using Perceptron = Perceptron.Perceptron;


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

        public float angleZ;

    }



    //Transform player;
    //Transform drone;

    Matrix player_matrix;
    Matrix drone_matrix;
    public SC_AI(Matrix dronePos , Matrix playerPos)
    {
        this.player_matrix = playerPos;
        this.drone_matrix = dronePos;
        Starter();
    }

    //float[] inputsOne = new float[2];
    //float[] biasOne = new float[3];
    //float[][] weightsOne = new float[3][];
    //float[][] weightsTwo = new float[2][];

    const int SC_Angle_Divider = 4;
    const int SC_anglesNumber = 360;
    const int SC_anglesQuarterNumber = SC_anglesNumber * SC_Angle_Divider;

    //3x number of valves to 
    const int inputsNumber = 3; //send only 3 random position to the Perceptron Training
    const int weightsNumber = inputsNumber;

    float[][] saveCurrentWeightForTheCurrentAngleInsideOfQuarterUnitsOf360 = new float[SC_anglesQuarterNumber][];

    float perceptronLearningRate = 0.001f;

    Perceptron.Perceptron perc;

    float[] weights;

    float xmin, xmax, ymin, ymax;
    Trainer[] training = new Trainer[inputsNumber];
    int count = 0;

    public int _guessedCorrectRight = 0;
    public int _guessedCorrectLeft = 0;

    public float _dotGoal;


    Vector3 player_pos = Vector3.Zero;
    Vector3 drone_pos = Vector3.Zero;

    Vector3 direction_forward_player;
    Vector3 direction_right_player;
    Vector3 direction_up_player;

    Vector3 direction_forward_drone;
    Vector3 direction_right_drone;
    Vector3 direction_up_drone;

    Quaternion otherQuat;

    void Starter()
    {
   
        Quaternion.RotationMatrix(ref player_matrix, out otherQuat);
        direction_forward_player = _getDirection(Vector3.ForwardRH, otherQuat);
        direction_right_player = _getDirection(Vector3.Right, otherQuat);
        direction_up_player = _getDirection(Vector3.Up, otherQuat);

        Quaternion.RotationMatrix(ref drone_matrix, out otherQuat);
        direction_forward_drone = _getDirection(Vector3.ForwardRH, otherQuat);
        direction_right_drone = _getDirection(Vector3.Right, otherQuat);
        direction_up_drone = _getDirection(Vector3.Up, otherQuat);


        player_pos.X = player_matrix.M41;
        player_pos.Y = player_matrix.M42;
        player_pos.Z = player_matrix.M43;

        drone_pos.X = drone_matrix.M41;
        drone_pos.Y = drone_matrix.M42;
        drone_pos.Z = drone_matrix.M43;





        randomer = new System.Random();
        random = new System.Random();
        perc = new Perceptron.Perceptron(3, 0.05f);

        perceptronLearningRate = getSomeRandNumThousandDecimal();

        for (int i = 0;i < saveCurrentWeightForTheCurrentAngleInsideOfQuarterUnitsOf360.Length;i++)
        {
            weights= perc.GetWeights();
            saveCurrentWeightForTheCurrentAngleInsideOfQuarterUnitsOf360[i] = weights;
        }
        //weights = perc.GetWeights();
    }

    //public Transform waypointTest;

    Vector2 waypointPos;

    Vector2 getFormationWaypoint(data_input _data_input)
    {
        //Vector2 coordsPlayer = new Vector2(player_pos.Y, player_pos.Y);
        //Vector2 rightDirPlayer = new Vector2(direction_right_player.X, -player.right.z);

        var pointRightX = _data_input.playerPos.X + ((_data_input.playerDirRight.X * _data_input.formationWaypointOffsetX));
        var pointRightY = _data_input.playerPos.Y + ((_data_input.playerDirRight.Y * _data_input.formationWaypointOffsetY));


        return new Vector2(pointRightX, pointRightY);
    }

    float Dot(float aX, float aY, float bX, float bY)
    {
        return (aX * bX) + (aY * bY);
    }

    public static float Clamp0360(float eulerAngles)
    {
        float result = eulerAngles - (int)Math.Round(Math.Ceiling(eulerAngles / (360f * SC_Angle_Divider))) * (360f * SC_Angle_Divider);
        if (result < 0)
        {
            result += (360f * SC_Angle_Divider);
        }
        return result;
    }

    float f(float x)
    {
        return (float)(1 * x);
    }

    int answer;
    System.Random random;

    float lastAngle = -999;

    //Vector2 playerPos, Vector2 dronePos
    public void UpdatePerceptron(data_input _data_input)
    {
        _guessedCorrectRight = 0;
        _guessedCorrectLeft = 0;

        waypointPos = getFormationWaypoint(_data_input);

        //Vector2 playerPos = new Vector2(player.transform.position.x,player.transform.position.y);
        //Vector2 dronePos = new Vector2(drone.transform.position.x, drone.transform.position.y);

        var angle = Clamp0360(_data_input.angleZ); //drone.transform.eulerAngles.z

        var angleRounded = (float)Math.Round(angle);
        var currentDiff = (angle - angleRounded);
        var currentQuarterRoundedAngle = (float)Math.Round(currentDiff * SC_Angle_Divider) / SC_Angle_Divider; //Mathf.Round(angle * 4) / 4;
        currentQuarterRoundedAngle *= 100;


        currentQuarterRoundedAngle = (angle * SC_Angle_Divider);


        weights = perc.GetWeights();
        perc._SC_Perceptron_SetRotWeights(saveCurrentWeightForTheCurrentAngleInsideOfQuarterUnitsOf360[(int)currentQuarterRoundedAngle]);



        float pointForwardDirNPCX = (float)(1 * Math.Cos(Math.PI * angle / 180.0)) + _data_input.dronePos.X; // * Math.PI / 180
        float pointForwardDirNPCY = (float)(1 * Math.Sin(Math.PI * angle / 180.0)) + _data_input.dronePos.Y;

        Vector2 dirForwardNPC = new Vector2(pointForwardDirNPCX - _data_input.dronePos.X, pointForwardDirNPCY - _data_input.dronePos.Y);

        Vector2 dirRightNPC = new Vector2(pointForwardDirNPCY - _data_input.dronePos.Y,-1* (pointForwardDirNPCX - _data_input.dronePos.X));

        Vector2 dirNPCToPlayer = new Vector2(_data_input.playerPos.X - _data_input.dronePos.X, _data_input.playerPos.Y - _data_input.dronePos.Y);

        var someOtherMAG = (float)Math.Sqrt((dirNPCToPlayer.X * dirNPCToPlayer.X) + (dirNPCToPlayer.Y * dirNPCToPlayer.Y));
        dirNPCToPlayer.X /= someOtherMAG;
        dirNPCToPlayer.Y /= someOtherMAG;

        _dotGoal = Dot(dirRightNPC.X, dirRightNPC.Y, dirNPCToPlayer.X, dirNPCToPlayer.Y);

        for (int i = 0; i < training.Length; i++)
        {
            double angleInRadians = random.Next(360) / (2 * Math.PI);

            int x = (int)(0.001f * Math.Cos(angleInRadians) + _data_input.dronePos.X);
            int y = (int)(0.001f * Math.Sin(angleInRadians) + _data_input.dronePos.Y);

            Vector2 dirToGuessedWaypoint = new Vector2(x - _data_input.dronePos.X, y - _data_input.dronePos.Y);

            //waypointTest.transform.position = new Vector3(x, y, 0);

            float _currentGuesseDot = Dot(dirRightNPC.X, dirRightNPC.Y, dirToGuessedWaypoint.X, dirToGuessedWaypoint.Y);
            
            if (_dotGoal > 0.001f)
            {
                //Debug.Log("player is right" + _dotGoal);
                answer = 1;
            }
            else if (_dotGoal < -0.001f)
            {
                //Debug.Log("player is left" + _dotGoal);
                answer = -1;
            }

            training[i] = new Trainer(weightsNumber, x, y, answer);
        }

        perc.Train(training[count].inputs, training[count].answer0);

        count = (count + 1) % training.Length;

        int guessedCorrect = 0;
        int guessedWrong = 0;

        int turnRight = 0;
        int turnLeft = 0;

        for (int i = 0; i < count; i++)
        {
            int guess = perc.Guess(training[i].inputs);
            Vector2 neededPos = new Vector2(training[i].inputs[0], training[i].inputs[1]);

            //waypointTest.transform.position = new Vector3(neededPos.x, neededPos.y, 0);

            if (training[i].answer0 == 1)
            {
                turnRight++;
            }
            else if (training[i].answer0 == -1)
            {
                turnLeft++;
            }

            if (guess > 0)
            {
              
                guessedCorrect++;
            }
            else
            {
                guessedWrong++;
            }
        }

        if (guessedCorrect >= (training.Length * 0.5f) - 5 && guessedCorrect <= (training.Length)||
            guessedWrong >= (training.Length * 0.5f) - 5 && guessedWrong <= (training.Length))
        {
            if (turnRight >= (training.Length * 0.5f) - 5 && turnRight <= (training.Length) ||
                turnLeft >= (training.Length * 0.5f) - 5 && turnLeft <= (training.Length))
            {
                if (turnRight > turnLeft)
                {
                    _guessedCorrectRight++;
                    //Debug.Log("player is Right 0-0");
                    //drone.transform.Rotate(new Vector3(0, 0, -2f), Space.World);       
                }
                else if (turnRight < turnLeft)
                {
                    _guessedCorrectLeft++;
                    //Debug.Log("player is Left 0-0");
                    //drone.transform.Rotate(new Vector3(0, 0, 2f), Space.World);
                }
            }
            else if (turnRight <= (training.Length * 0.5f) + 5 && turnRight >= (training.Length) ||
                    turnLeft <= (training.Length * 0.5f) + 5 && turnLeft >= (training.Length))
            {
                if (turnRight > turnLeft)
                {
                    _guessedCorrectRight++;
                    //Debug.Log("player is Right 0-0");
                    //drone.transform.Rotate(new Vector3(0, 0, -2f), Space.World);       
                }
                else if (turnRight < turnLeft)
                {
                    _guessedCorrectLeft++;
                    //Debug.Log("player is Left 0-0");
                    //drone.transform.Rotate(new Vector3(0, 0, 2f), Space.World);
                }
            }
            else
            {
                //Debug.Log("Data is too similar");
            }
        }
        else if (guessedCorrect <= (training.Length * 0.5f) + 5 && guessedCorrect >= (training.Length) ||
                guessedWrong <= (training.Length * 0.5f) + 5 && guessedWrong >= (training.Length))
        {

            if (turnRight >= (training.Length * 0.5f) - 5 && turnRight <= (training.Length) ||
                turnLeft >= (training.Length * 0.5f) - 5 && turnLeft <= (training.Length))
            {
                if (turnRight > turnLeft)
                {
                    _guessedCorrectRight++;
                    //Debug.Log("player is Right 0-0");
                    //drone.transform.Rotate(new Vector3(0, 0, -2f), Space.World);       
                }
                else if (turnRight < turnLeft)
                {
                    _guessedCorrectLeft++;
                    //Debug.Log("player is Left 0-0");
                    //drone.transform.Rotate(new Vector3(0, 0, 2f), Space.World);
                }
            }
            else if (turnRight <= (training.Length * 0.5f) + 5 && turnRight >= (training.Length) ||
                    turnLeft <= (training.Length * 0.5f) + 5 && turnLeft >= (training.Length))
            {
                if (turnRight > turnLeft)
                {
                    _guessedCorrectRight++;
                    //Debug.Log("player is Right 0-0");
                    //drone.transform.Rotate(new Vector3(0, 0, -2f), Space.World);       
                }
                else if (turnRight < turnLeft)
                {
                    _guessedCorrectLeft++;
                    //Debug.Log("player is Left 0-0");
                    //drone.transform.Rotate(new Vector3(0, 0, 2f), Space.World);
                }
            }
            else
            {
                //Debug.Log("Data is too similar");
            }
        }
        else
        {
            //Debug.Log("Data is too similar");
        }
        saveCurrentWeightForTheCurrentAngleInsideOfQuarterUnitsOf360[(int)currentQuarterRoundedAngle] = perc.GetWeights();




    }



    //https://pastebin.com/fAFp6NnN // Also found on the unity3D forums.
    public static Vector3 _getDirection(Vector3 value, SharpDX.Quaternion rotation)
    {
        Vector3 vector;
        double num12 = rotation.X + rotation.X;
        double num2 = rotation.Y + rotation.Y;
        double num = rotation.Z + rotation.Z;
        double num11 = rotation.W * num12;
        double num10 = rotation.W * num2;
        double num9 = rotation.W * num;
        double num8 = rotation.X * num12;
        double num7 = rotation.X * num2;
        double num6 = rotation.X * num;
        double num5 = rotation.Y * num2;
        double num4 = rotation.Y * num;
        double num3 = rotation.Z * num;
        double num15 = ((value.X * ((1f - num5) - num3)) + (value.Y * (num7 - num9))) + (value.Z * (num6 + num10));
        double num14 = ((value.X * (num7 + num9)) + (value.Y * ((1f - num8) - num3))) + (value.Z * (num4 - num11));
        double num13 = ((value.X * (num6 - num10)) + (value.Y * (num4 + num11))) + (value.Z * ((1f - num8) - num5));
        vector.X = (float)num15;
        vector.Y = (float)num14;
        vector.Z = (float)num13;
        return vector;
    }







    /*if (currentQuarterRoundedAngle >= 0 && currentQuarterRoundedAngle <= 249)
{
angle = (Mathf.Floor(angle)* 2) + 0;
}
else if (currentQuarterRoundedAngle >= 249 && currentQuarterRoundedAngle < 498)
{
angle = (Mathf.Floor(angle) * 2) + 1;
}
else if (currentQuarterRoundedAngle >= 499 && currentQuarterRoundedAngle < 747)
{
angle = (Mathf.Floor(angle) * 2) + 2;
}
else if (currentQuarterRoundedAngle >= 748 && currentQuarterRoundedAngle < 999)
{
angle = (Mathf.Floor(angle) * 2) + 3;
}

currentQuarterRoundedAngle = (angleRounded * 2) + angle;*/
    //Debug.Log(currentQuarterRoundedAngle);
    /*if (lastAngle!= currentQuarterRoundedAngle)
{        
//weights = angleWeights[(int)currentQuarterRoundedAngle];
//angleWeights[(int)currentQuarterRoundedAngle] = weights;
}*/

    ///var angle = Vector2.Angle(); //Clamp0360(drone.transform.eulerAngles.z);
    //Debug.Log(angle);

    /*perc.Train(training[count].inputs, training[count].answer);
    count = (count + 1) % training.Length;

    for (int i = 0; i < count; i++)
    {
        int guess = perc.Guess(training[i].inputs);
        //Point p = CartesianToDefault(training[i].inputs[0], training[i].inputs[1]);
        if (guess > 0)
        {

        }
        //g.DrawEllipse(Pens.Blue, p.X, p.Y, 8, 8);
        //else g.FillEllipse(Brushes.Blue, p.X, p.Y, 8, 8);
    }*/
    //double anglerRandom = 2.0 * Math.PI * random.NextDouble();
    //Debug.Log(anglerRandom);

    //float x = (float)((pointForwardDirNPCX) * (1 * Math.Cos(anglerRandom))); //* Math.PI / 180
    //float y = (float)((pointForwardDirNPCY) * (1 * Math.Sin(anglerRandom)));
    //waypointTest.transform.position = new Vector3(x, y, 0);






    /*private void DrawBrain(Graphics g)
    {
        float[] weights = perc.GetWeights();
        //float x1 = xmin;
        //float y1 = (-weights[2] - weights[0] * x1) / weights[1];
        //float x2 = xmax;
        //float y2 = (-weights[2] - weights[0] * x2) / weights[1];
        //g.DrawLine(Pens.Black, CartesianToDefault(x1, y1), CartesianToDefault(x2, y2));
    }*/

    /*private void canvas_Paint(object sender, PaintEventArgs e)
    {
        ///Graphics g = e.Graphics;
        //g.DrawLine(Pens.Black, CartesianToDefault(xmin, f(xmin)), CartesianToDefault(xmax, f(xmax)));
        //DrawBrain(g);

        perc.Train(training[count].inputs, training[count].answer);
        count = (count + 1) % training.Length;

        // Draw all the points based on what the Perceptron would "guess"
        // Does not use the "known" correct answer
        for (int i = 0; i < count; i++)
        {
            int guess = perc.Guess(training[i].inputs);
            Point p = CartesianToDefault(training[i].inputs[0], training[i].inputs[1]);
            if (guess > 0) g.DrawEllipse(Pens.Blue, p.X, p.Y, 8, 8);
            else g.FillEllipse(Brushes.Blue, p.X, p.Y, 8, 8);
        }
    }*/





    /*unsafe double invSQRT(double x)
    {
        //float xhalf = 0.5f * x;
        //var i = *(int*)&x;
        //Debug.Log((i >> 1));
        //i = 0x5f3759df - (i >> 1);
        double xhalf = 0.5f * x;
        int i = *(int*)&x;  // store floating-point bits in integer
        i = 0x5f3759df - (i >> 1);    // initial guess for Newton's method
        x = *(float*)&i;            // convert new bits into float
        x = x * (1.5 - xhalf * x * x);        // One round of Newton's method
        //x = x * (1.5 - xhalf * x * x);      // One round of Newton's method
        //x = x * (1.5 - xhalf * x * x);      // One round of Newton's method
        //x = x * (1.5 - xhalf * x * x);      // One round of Newton's method
        return x;
    }*/


    /*float FastInvSqrt(float x)
    {
        float xhalf = 0.5F * x;
        int i = *(int*)&x;  // store floating-point bits in integer
        i = 0x5f3759df - (i >> 1);    // initial guess for Newton's method
        x = *(float*)&i;            // convert new bits into float
        x = x * (1.5 - xhalf * x * x);        // One round of Newton's method
        //x = x * (1.5 - xhalf * x * x);      // One round of Newton's method
        //x = x * (1.5 - xhalf * x * x);      // One round of Newton's method
        //x = x * (1.5 - xhalf * x * x);      // One round of Newton's method
        return x;
    }*/

    /*var buf = new ArrayBuffer(4),
    f32 = new Float32Array(buf),
    u32 = new Uint32Array(buf);
    function q2(x)
    {
        var x2 = 0.5 * (f32[0] = x);
        u32[0] = (0x5f3759df - (u32[0] >> 1));
        var y = f32[0];
        y = y * (1.5 - (x2 * y * y));
        return y;
    }*/

    /*public static float Sqrt2(float z)
    {
        if (z == 0) return 0;
        FloatIntUnion u;
        u.tmp = 0;
        float xhalf = 0.5f * z;
        u.f = z;
        u.tmp = 0x5f375a86 - (u.tmp >> 1);
        u.f = u.f * (1.5f - xhalf * u.f * u.f);
        return u.f * z;
    }*/

    /*float InvSqrt(float x)
    {
        float xhalf = 0.5f * x;
        int i = *(int*)&x;            // store floating-point bits in integer
        i = 0x5f3759df - (i >> 1);    // initial guess for Newton's method
        x = *(float*)&i;              // convert new bits into float
        x = x * (1.5f - xhalf * x * x);     // One round of Newton's method
        return x;
    }*/

    /*float inverse_rsqrt(float number)
    {
        const float threehalfs = 1.5F;

        float x2 = number * 0.5F;
        float y = number;

        // evil floating point bit level hacking 
        long i = *(long*)&y;

        // value is pre-assumed 
        i = 0x5f3759df - (i >> 1);
        y = *(float*)&i;

        // 1st iteration 
        y = y * (threehalfs - (x2 * y * y));

        // 2nd iteration, this can be removed 
        // y = y * ( threehalfs - ( x2 * y * y ) ); 

        return y;
    }*/

    double ActivationFunction(double x)
    {
        return (1 / (1 + Math.Exp((float)-x)));
    }

    System.Random randomer;
    double getSomeRandNum()
    {
        var num = Math.Floor(randomer.NextDouble() * 999999999) + 1; // this will get a number between 1 and 99;
        num *= Math.Floor(randomer.NextDouble() * 2) == 1 ? 1 : -1; // this will add minus sign in 50% of cases
        if (num == 0)
        {
            return getSomeRandNum();
        }
        return num * 0.000000001;
    }

    float getSomeRandNumThousandDecimal()
    {
        var num = Math.Floor(randomer.NextDouble() * 999) + 1; // this will get a number between 1 and 99;
        num *= Math.Floor(randomer.NextDouble() * 2) == 1 ? 1 : -1; // this will add minus sign in 50% of cases
        if (num == 0)
        {
            return (float)getSomeRandNum();
        }
        return (float)(num * 0.001);
    }

}
