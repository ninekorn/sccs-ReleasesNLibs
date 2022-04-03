

namespace Perceptron
{
    public class Trainer
    {
        public float[] inputs;
        public int answer;

        public float x = 0;
        public float y = 0;


        public Trainer(int neurons,float _x, float _y, int a0)
        {
            x = _x;
            y = _y;

            inputs = new float[neurons];
            inputs[0] = x;
            inputs[1] = y;
            inputs[2] = 1;
            answer = a0;
        }



        //int neurons, 
        public void setTrainer(float _x, float _y, int a0)
        {
            x = _x;
            y = _y;

            //inputs = new float[neurons];
            inputs[0] = x;
            inputs[1] = y;
            inputs[2] = 1;
            answer = a0;
        }



    }
}
