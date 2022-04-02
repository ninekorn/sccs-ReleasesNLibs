using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
/*using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
*/
using Ab3d.DirectX;
/*using Ab3d.DirectX.Client.Diagnostics;
using Ab3d.DirectX.Client.Settings;
using Ab3d.DirectX.Controls;*/
//using Ab3d.DXEngine.Wpf.Samples;
//using Ab3d.DXEngine.Wpf.Samples.Properties;

//using Ab3d.DXEngine.Wpf.Samples.Common;

using System.Configuration;

using System.IO;
using System.Threading;

//using System.Windows.Media.Media3D;
//using Ab3d.Cameras;
using Ab3d.Common;
//using Ab3d.Common.Cameras;
//using Ab3d.Controls;
using Ab3d.OculusWrap;
//using Ab3d.Visuals;
using SharpDX.Direct3D;
using SharpDX.DXGI;
using Result = Ab3d.OculusWrap.Result;

using Ab3d.DXEngine.OculusWrap;

using System.Data;
using System.Drawing;
using System.Threading.Tasks;
//using System.Windows.Forms;
//using System.Speech.Recognition;
//using System.Speech.Synthesis; ///For program to speak back




using System.Xml;

using System.Text.RegularExpressions;
//using _console_writer_message_queue = SC_SkYaRk_VR_Editionv002.SC_Console_WRITER._console_writer_message_queue;

//using Microsoft.DirectX;
//using Microsoft.DirectX.DirectSound;



using System.Runtime.InteropServices;



namespace SC_skYaRk_VR_V007
{
    public class SC_AI_Language
    {
        //SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();
        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(IntPtr h, string m, string c, int type);


        bool mathSwitch = false;
         string[] stringOfMathOps;
         bool firstInput = false;
         bool secondInput = false;
         List<string> currentListOfCommands = new List<string>();

         int functionCounter00 = 1;
         int functionCounter01 = 1;
         int functionCounter02 = 1;
         int functionCounter03 = 1;

         string lastWord = "";

         string totalCombination = "";

         string currentWord = "";

         int counterTotalWords = 0;

         int frameCounterForVoiceRecognition = 0;
         int frameCounterForVoiceRecognitionRecognizedWords = 0;


        public void threadOneGrammarLoad()
        {





            //MessageBox((IntPtr)0, "tester" + frameCounterForVoiceRecognition, "My Message Box", 0);
            //frameCounterForVoiceRecognition++;

            //MessageBox((IntPtr)0, "tester", "My Message Box", 0);

            //consoleMessageQueue messageQueue00 = new consoleMessageQueue(_AlphabetConsoleLineTitle, _AlphabetConsoleLineTitleConsoleCursorPosX, _AlphabetConsoleLineTitleConsoleCursorPosY);

            /*Choices commands = new Choices();
            commands.Add(new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" });
            //commands.Add(new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" });

            //"letter", "letters", "alphabet", 

            //commands.Add(new string[] { "number", "numbers", "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" });
            //commands.Add(new string[] { "hundred", "thousand", "million", "billion", "trillion" });
            //commands.Add(new string[] { "math", "maths", "plus", "minus", "divide", "multiply", "multiplied", "equal", "square", "square root", "cos", "sin", "tan" });
            //commands.Add(new string[] { "console", "program", "directory", "start", "plus" });

            ////////////////COMMANDS FOR CALCULATOR/////////////////////
            ////////////////////////////////////////////////////////////
            //commands.Add(new string[] { "calculations", "calculator" });
            ////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////
            //List<string> words = System.IO.File.ReadAllText("MyWords.txt").Split(new string[] { Environment.NewLine }).ToList();
            ///ok wow. i dont need to build my own spectrum crap and how many days/weeks/months/years would it take me to code it... but damn i learned a ton
            ///in the process anyway. Next step. How do we load a c# library in Unity and use it? OMG this stuff is powerfull.
            GrammarBuilder gBuilder = new GrammarBuilder();
            gBuilder.Append(commands);
            Grammar grammar = new Grammar(gBuilder);

            recEngine.LoadGrammarAsync(grammar);
            recEngine.SetInputToDefaultAudioDevice();
            recEngine.RecognizeAsync(RecognizeMode.Single);
            recEngine.SpeechRecognized += recEngine_SpeechRecognized;*/
        }

        string _AlphabetConsoleLineTitle = "Alphabet";
         int _AlphabetConsoleLineTitleConsoleCursorPosX = 0;
         int _AlphabetConsoleLineTitleConsoleCursorPosY = 10;

        /* void recEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            //consoleMessageQueue messageQueue00 = new consoleMessageQueue(_AlphabetConsoleLineTitle, _AlphabetConsoleLineTitleConsoleCursorPosX, _AlphabetConsoleLineTitleConsoleCursorPosY);

            //currentWord = e.Result.Text;

            switch (e.Result.Text)
            {
                case "a":
                    lastWord = "a";
                    //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
                    Console.WriteLine(lastWord);
                    //totalCombination += lastWord;
                    functionCounter03++;
                    counterTotalWords++;
                    frameCounterForVoiceRecognitionRecognizedWords++;
                    break;
                case "b":
                    lastWord = "b";
                    //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
                    Console.WriteLine(lastWord);
                    //totalCombination += lastWord;
                    functionCounter03++;
                    counterTotalWords++;
                    frameCounterForVoiceRecognitionRecognizedWords++;
                    break;
                case "c":
                    lastWord = "c";
                    //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
                    Console.WriteLine(lastWord);
                    //totalCombination += lastWord;
                    functionCounter03++;
                    counterTotalWords++;
                    frameCounterForVoiceRecognitionRecognizedWords++;
                    break;
                case "d":
                    lastWord = "d";
                    //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
                    Console.WriteLine(lastWord);
                    //totalCombination += lastWord;
                    functionCounter03++;
                    counterTotalWords++;
                    frameCounterForVoiceRecognitionRecognizedWords++;
                    break;
                case "e":
                    lastWord = "e";
                    //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
                    Console.WriteLine(lastWord);
                    //totalCombination += lastWord;
                    functionCounter03++;
                    counterTotalWords++;
                    frameCounterForVoiceRecognitionRecognizedWords++;
                    break;
                case "f":
                    lastWord = "f";
                    //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
                    Console.WriteLine(lastWord);
                    //totalCombination += lastWord;
                    functionCounter03++;
                    counterTotalWords++;
                    frameCounterForVoiceRecognitionRecognizedWords++;
                    break;
                case "g":
                    lastWord = "g";
                    //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
                    Console.WriteLine(lastWord);
                    //totalCombination += lastWord;
                    functionCounter03++;
                    counterTotalWords++;
                    frameCounterForVoiceRecognitionRecognizedWords++;
                    break;
                case "h":
                    lastWord = "h";
                    //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
                    Console.WriteLine(lastWord);
                    //totalCombination += lastWord;
                    functionCounter03++;
                    counterTotalWords++;
                    frameCounterForVoiceRecognitionRecognizedWords++;
                    break;
                case "i":
                    lastWord = "i";
                    //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
                    Console.WriteLine(lastWord);
                    //totalCombination += lastWord;
                    functionCounter03++;
                    counterTotalWords++;
                    frameCounterForVoiceRecognitionRecognizedWords++;
                    break;
                case "j":
                    lastWord = "j";
                    //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
                    Console.WriteLine(lastWord);
                    //totalCombination += lastWord;
                    functionCounter03++;
                    counterTotalWords++;
                    frameCounterForVoiceRecognitionRecognizedWords++;
                    break;
                case "k":
                    lastWord = "k";
                    //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
                    Console.WriteLine(lastWord);
                    //totalCombination += lastWord;
                    functionCounter03++;
                    counterTotalWords++;
                    frameCounterForVoiceRecognitionRecognizedWords++;
                    break;
                case "l":
                    lastWord = "l";
                    //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
                    Console.WriteLine(lastWord);
                    //totalCombination += lastWord;
                    functionCounter03++;
                    counterTotalWords++;
                    frameCounterForVoiceRecognitionRecognizedWords++;
                    break;
                case "m":
                    lastWord = "m";
                    //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
                    Console.WriteLine(lastWord);
                    //totalCombination += lastWord;
                    functionCounter03++;
                    counterTotalWords++;
                    frameCounterForVoiceRecognitionRecognizedWords++;
                    break;
                case "n":
                    lastWord = "n";
                    //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
                    Console.WriteLine(lastWord);
                    //totalCombination += lastWord;
                    functionCounter03++;
                    counterTotalWords++;
                    frameCounterForVoiceRecognitionRecognizedWords++;
                    break;
                case "o":
                    lastWord = "o";
                    //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
                    Console.WriteLine(lastWord);
                    //totalCombination += lastWord;
                    functionCounter03++;
                    counterTotalWords++;
                    frameCounterForVoiceRecognitionRecognizedWords++;
                    break;
                case "p":
                    lastWord = "p";
                    //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
                    Console.WriteLine(lastWord);
                    //totalCombination += lastWord;
                    functionCounter03++;
                    counterTotalWords++;
                    frameCounterForVoiceRecognitionRecognizedWords++;
                    break;
                case "q":
                    lastWord = "q";
                    //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
                    Console.WriteLine(lastWord);
                    //totalCombination += lastWord;
                    functionCounter03++;
                    counterTotalWords++;
                    frameCounterForVoiceRecognitionRecognizedWords++;
                    break;
                case "r":
                    lastWord = "r";
                    //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
                    Console.WriteLine(lastWord);
                    //totalCombination += lastWord;
                    functionCounter03++;
                    counterTotalWords++;
                    frameCounterForVoiceRecognitionRecognizedWords++;
                    break;
                case "s":
                    lastWord = "s";
                    //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
                    Console.WriteLine(lastWord);
                    //totalCombination += lastWord;
                    functionCounter03++;
                    counterTotalWords++;
                    frameCounterForVoiceRecognitionRecognizedWords++;
                    break;
                case "t":
                    lastWord = "t";
                    //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
                    Console.WriteLine(lastWord);
                    //totalCombination += lastWord;
                    functionCounter03++;
                    counterTotalWords++;
                    frameCounterForVoiceRecognitionRecognizedWords++;
                    break;
                case "u":
                    lastWord = "u";
                    //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
                    Console.WriteLine(lastWord);
                    //totalCombination += lastWord;
                    functionCounter03++;
                    counterTotalWords++;
                    frameCounterForVoiceRecognitionRecognizedWords++;
                    break;
                case "v":
                    lastWord = "v";
                    //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
                    Console.WriteLine(lastWord);
                    //totalCombination += lastWord;
                    functionCounter03++;
                    counterTotalWords++;
                    frameCounterForVoiceRecognitionRecognizedWords++;
                    break;
                case "w":
                    lastWord = "w";
                    //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
                    Console.WriteLine(lastWord);
                    //totalCombination += lastWord;
                    functionCounter03++;
                    counterTotalWords++;
                    frameCounterForVoiceRecognitionRecognizedWords++;
                    break;
                case "x":
                    lastWord = "x";
                    //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
                    Console.WriteLine(lastWord);
                    //totalCombination += lastWord;
                    functionCounter03++;
                    counterTotalWords++;
                    frameCounterForVoiceRecognitionRecognizedWords++;
                    break;
                case "y":
                    lastWord = "y";
                    //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
                    Console.WriteLine(lastWord);
                    //totalCombination += lastWord;
                    functionCounter03++;
                    counterTotalWords++;
                    frameCounterForVoiceRecognitionRecognizedWords++;
                    break;
                case "z":
                    lastWord = "z";
                    //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
                    Console.WriteLine(lastWord);
                    //totalCombination += lastWord;
                    functionCounter03++;
                    counterTotalWords++;
                    frameCounterForVoiceRecognitionRecognizedWords++;
                    break;

            }
        }*/
    }








    /*case "calculator":
        lastWord = "calculator";
        //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
        Console.WriteLine(lastWord);
        totalCombination += lastWord;
        functionCounter03++;
        counterTotalWords++;
        frameCounterForVoiceRecognitionRecognizedWords++;
        break;





    case "zero":
        lastWord = "zero";
        //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
        Console.WriteLine(lastWord);
        totalCombination += lastWord;
        functionCounter00++;
        counterTotalWords++;
        frameCounterForVoiceRecognitionRecognizedWords++;
        break;

    case "one":
        lastWord = "one";
        Console.WriteLine(lastWord);
        //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
        totalCombination += lastWord;
        functionCounter00++;
        counterTotalWords++;
        frameCounterForVoiceRecognitionRecognizedWords++;
        break;

    case "two":
        lastWord = "two";
        Console.WriteLine(lastWord);
        //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
        totalCombination += lastWord;
        functionCounter00++;
        counterTotalWords++;
        frameCounterForVoiceRecognitionRecognizedWords++;
        break;
    case "three":
        lastWord = "three";
        //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);

        totalCombination += lastWord;
        functionCounter00++;
        counterTotalWords++;
        frameCounterForVoiceRecognitionRecognizedWords++;
        break;
    case "four":
        lastWord = "four";
        //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);

        totalCombination += lastWord;
        functionCounter00++;
        counterTotalWords++;
        frameCounterForVoiceRecognitionRecognizedWords++;
        break;
    case "five":
        lastWord = "five";
        ////SC_Console.WriteAt(lastWord, 0, 20, false);
        ////SC_Console.WriteAt("one calls: " + functionCounter00, 0, 19, false);
        //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
        totalCombination += lastWord;
        functionCounter00++;
        counterTotalWords++;
        frameCounterForVoiceRecognitionRecognizedWords++;
        break;
    case "six":
        lastWord = "six";
        //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
        totalCombination += lastWord;
        functionCounter00++;
        counterTotalWords++;
        frameCounterForVoiceRecognitionRecognizedWords++;
        break;
    case "seven":
        lastWord = "seven";
        //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
        totalCombination += lastWord;
        functionCounter00++;
        counterTotalWords++;
        frameCounterForVoiceRecognitionRecognizedWords++;
        break;
    case "eight":
        lastWord = "eight";
        //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
        totalCombination += lastWord;
        functionCounter00++;
        counterTotalWords++;
        frameCounterForVoiceRecognitionRecognizedWords++;
        break;
    case "nine":
        lastWord = "nine";
        //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
        totalCombination += lastWord;
        functionCounter00++;
        counterTotalWords++;
        frameCounterForVoiceRecognitionRecognizedWords++;
        break;


    case "plus":
        lastWord = "plus";
        //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
        totalCombination += lastWord;
        functionCounter00++;
        counterTotalWords++;
        frameCounterForVoiceRecognitionRecognizedWords++;
        break;











    //"number", "numbers", "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety"

    case "console":
        //functionCounter++;
        lastWord = "console";
        ////SC_Console.WriteAt(lastWord, 0, 16, false);
        ////SC_Console.WriteAt("console calls: " + functionCounter01, 0, 15, false);
        //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
        //richTextBox1_TextChanged(sender,e);
        //richTextBox1.Text = "start";
        //MessageBox.Show("start");
        //currentListOfCommands.Add("start");
        totalCombination += lastWord;
        functionCounter01++;
        counterTotalWords++;
        frameCounterForVoiceRecognitionRecognizedWords++;
        break;
    case "start":
        //functionCounter++;
        lastWord = "start";
        ////SC_Console.WriteAt(lastWord, 0, 18, false);
        ////SC_Console.WriteAt("start calls: " + functionCounter02, 0, 17, false);
        //SC_Console.WriteAt(functionCounter00 + "", 0, 14, false);
        //richTextBox1_TextChanged(sender,e);
        //richTextBox1.Text = "start";
        //MessageBox.Show("start");
        //currentListOfCommands.Add("start");
        totalCombination += lastWord;
        functionCounter02++;
        counterTotalWords++;
        frameCounterForVoiceRecognitionRecognizedWords++;
        break;*/




















    /*case "start":

        //SC_Console.WriteAt("start calls: " + functionCounter, 0, 15, false);
        functionCounter++;

        //richTextBox1_TextChanged(sender,e);
        //richTextBox1.Text = "start";
        //MessageBox.Show("start");
        //currentListOfCommands.Add("start");
        break;

    case "addPerceptron":
        startPerceptron = true;
        //SC_Console.WriteAt("addPerceptron: " + functionCounter, 0, 15, false);
        functionCounter++;
        break;*/

    /*case "program":
        //richTextBox1_TextChanged(sender,e);
        //richTextBox1.Text = "program";
        //MessageBox.Show("program");
        break;*/

    /*case "directory":
        //richTextBox1_TextChanged(sender,e);
        string[] directories = Directory.GetDirectories("C:\\");

        for (int i = 0; i < directories.Length; i++)
        {
            //richTextBox1.Text += "\r" + directories[i];
            /*for (int j = 0; j < directories[i].Length; j++)
            {


            }   
        }
        break;
        /////////////////////////////////////////////////
        /////////////////////////////////////////////////
}
frameCounterForVoiceRecognition++;*/
}
