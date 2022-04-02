using System;
using System.Collections.Generic;
using System.Text;

namespace SC_skYaRk_VR_V007
{
    class Rot_Visual_ThumbStick
    {
        /*if (thumbStickRight[1].X< 0|| thumbStickRight[1].X> 0|| thumbStickRight[1].Y< 0|| thumbStickRight[1].Y> 0)
                {
                    if (thumbStickRight[1].X > 0)
                    {
                        float lengthOfX = thumbStickRight[1].X;
        float lengthOfY = thumbStickRight[1].Y;

        var newRotationZ = (180 / Math.PI) * (Math.Atan(lengthOfY / lengthOfX));

                        if (newRotationZ > 0)
                        {
                            newRotationZ = (90 - newRotationZ) * -1;
                        }
                        else
                        {
                            newRotationZ = -90 - newRotationZ;
                        }
Console.WriteLine(newRotationZ);

                        RotationZ = newRotationZ;

                        pitch = (float) (RotationX* 0.0174532925f);
                        yaw = (float) (RotationY* 0.0174532925f);
                        roll = (float) (newRotationZ* 0.0174532925f);

                        rotatingMatrix = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);
                    }
                    else if (thumbStickRight[1].X< 0)
                    {
                        float lengthOfX = thumbStickRight[1].X;
float lengthOfY = thumbStickRight[1].Y;

var newRotationZ = (180 / Math.PI) * (Math.Atan(lengthOfX / lengthOfY));

                        if (newRotationZ > 0)
                        {
                            newRotationZ = (newRotationZ);
                        }
                        else
                        {
                            newRotationZ = (newRotationZ* -1);
                        }

                        Console.WriteLine(newRotationZ);

                        RotationZ = newRotationZ;

                        pitch = (float) (RotationX* 0.0174532925f);
                        yaw = (float) (RotationY* 0.0174532925f);
                        roll = (float) (newRotationZ* 0.0174532925f);

                        rotatingMatrix = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);
                    }

                    if (thumbStickRight[1].Y > 0)
                    {

                        float lengthOfX = thumbStickRight[1].X;
float lengthOfY = thumbStickRight[1].Y;

var newRotationX = (180 / Math.PI) * (Math.Atan(lengthOfY / lengthOfX));

                        if (newRotationX > 0)
                        {
                            newRotationX = ((newRotationX)) * -1;
                        }
                        else
                        {
                            newRotationX = newRotationX;// ((newRotationX) * -1);
                        }
                        //Console.WriteLine(newRotationX);


                        RotationX = newRotationX;

                        pitch = (float) (newRotationX* 0.0174532925f);
                        yaw = (float) (RotationY* 0.0174532925f);
                        roll = (float) (RotationZ* 0.0174532925f);

                        rotatingMatrix = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);
                    }
                    else if (thumbStickRight[1].Y< 0)
                    {
                        float lengthOfX = thumbStickRight[1].X;
float lengthOfY = thumbStickRight[1].Y;

var newRotationX = (180 / Math.PI) * (Math.Atan(lengthOfY / lengthOfX));

                        if (newRotationX > 0)
                        {
                            newRotationX = newRotationX;
                        }
                        else
                        {
                            newRotationX = newRotationX* -1;
                        }

                        RotationX = newRotationX;

                        pitch = (float) (newRotationX* 0.0174532925f);
                        yaw = (float) (RotationY* 0.0174532925f);
                        roll = (float) (RotationZ* 0.0174532925f);

                        rotatingMatrix = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);
                    }
                }
                else
                {
                    RotationX = 0;
                    RotationY = 0;
                    RotationZ = 0;

                    pitch = (float) (RotationX* 0.0174532925f);
                    yaw = (float) (RotationY* 0.0174532925f);
                    roll = (float) (RotationZ* 0.0174532925f);

                    rotatingMatrix = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);

                }*/
    }
}
