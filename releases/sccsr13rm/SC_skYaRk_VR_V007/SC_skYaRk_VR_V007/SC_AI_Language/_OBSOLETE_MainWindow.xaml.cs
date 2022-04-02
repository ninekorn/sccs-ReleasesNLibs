using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Threading;

using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;

using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Devices;

using System.Text.RegularExpressions;

using System.Speech.Recognition;
using System.Speech.Synthesis; ///For program to speak back
using System.Xml;

//using AI.Perceptron;
//using System.Threading.Tasks;
//using System.Speech;
//using System.Speech.Recognition;

namespace SC_test_to_delete
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static MainWindow _mainWindow;

        bool _isComponents;

        public static int _processorCount = 0;

        public static int _workerThreadsTotal;
        public static int _portThreadsTotal;


        Thread _thread;

        public static int _totalThreads = 0;
        public static bool _mainFrameStarterItemz = true;


        public MainWindow()
        {
            _mainWindow = this;
            //INITIALIZING COMPONENTS//
            for (int j = 0; j < 1; j++)
            {
                try
                {
                    InitializeComponent();
                    _isComponents = true;
                }
                catch
                {
                    _isComponents = false;
                    break;
                }
            }
            //INITIALIZING COMPONENTS//



            //CREATING SC_Console//
            for (int j = 0; j < 1; j++)
            {
                try
                {
                    SC_Console.createConsole();
                    //SC_GCGollect collector = new SC_GCGollect();
                }
                catch
                {
                    break;
                }
            }
            //CREATING SC_Console//






            for (int j = 0; j < 1; j++)
            {
                try
                {
                    _processorCount = SC_SystemInfoSeeker.getSystemProcessorCount();
                }
                catch //(Exception ex)
                {
                    break;
                }
            }







            for (int j = 0; j < 1; j++)
            {
                try
                {
                    ThreadPool.GetMaxThreads(out _workerThreadsTotal, out _portThreadsTotal);
                    ThreadPool.GetAvailableThreads(out _workerThreadsTotal, out _portThreadsTotal);
                }
                catch
                {
                    break;
                }
            }

            //_totalThreads = (int)(_portThreadsTotal * 0.01f);
            _totalThreads = 1;

            //SC_Console.consoleMessageQueue messageQueue1 = new SC_Console.consoleMessageQueue(_totalThreads + "", 0, 0);

            //ContentFrame2.Source = new Uri("Customizations/SC_AI_VR.xaml", UriKind.Relative);
            //ContentFrame1.Source = new Uri("Customizations/SC_desktopDupe.xaml", UriKind.Relative);

            for (int j = 0; j < 1; j++)
            {
                try
                {
                   




                        /*if (_mainFrameStarterItemz)
                        {
                            //Creating a thread individually... It has access to the UI by using System.Windows.Application.Current.Dispatcher.Invoke and is infinite in Time.
                            _thread = new Thread(() => _mainThreadStarter());
                            _thread.IsBackground = true;
                            _thread.Start();
                            _mainFrameStarterItemz = false;
                        }*/
                    }
                catch (Exception ex)
                {
                    //SC_Console.consoleMessageQueue messageQueue0 = new SC_Console.consoleMessageQueue(ex.ToString(), 0, 20);
                }
            }




            //ARE COMPONENTS LOADED???
            if (_isComponents == false)
            {
                //the main documents/page wpf cannot be loaded. // we continue the program anyway. Later on, my program will start from DOS... fuck it.
            }
            else
            {
                //the main documents/page wpf is loaded. // 
            }
        }

        public static bool _mainThreadStarterItemsBool = true;
        public static int _mainThreadFrameCounter = 0;

        public static workerThreads _workThread;

        public void _mainThreadStarter()
        {
            try
            {
                while (true)
                {

                    var updateMainUITitle = new Action(() =>
                    {
                        if (_UIStarterItemz)
                        {
                            threadOneGrammarLoad();
                            _UIStarterItemz = false;
                        }

                        _mainUpdateThread();
                    });

                    System.Windows.Application.Current.Dispatcher.Invoke(updateMainUITitle);

                    if (_mainThreadStarterItemsBool)
                    {
                        //Creating multiple threads. Can be sent with invoke or without it seems. If created inside of another thread, I think it would be best.
                        for (int i = 0; i < _totalThreads; i++)
                        {
                            _workThread = new workerThreads(i);
                        }
                        _mainThreadStarterItemsBool = false;
                    }

                    _mainThreadFrameCounter++;
                    Thread.Sleep(1);
                }
            }
            catch
            {

            }
        }

        public static bool _UIStarterItemz = true;

        //UI THREAD TEST
        //////////////////////////////////
        //////////////////////////////////
        public static void _mainUpdateThread()
        {
           

            _mainWindow.Title = "" + _mainThreadFrameCounter.ToString();

            //SC_GCGollect.GCCollectUtility(100);
        }
        //////////////////////////////////
        //////////////////////////////////



        public class workerThreads
        {
            public int _mainFrameCounterThreadOne = 0;
            public int _availableThreads = 0;
            public ThreadStart _threadStart;
            public Thread[] _listOfThreads;
            public int _threadID;
            public Thread _thread;


            public workerThreads(int threadID)
            {

                this._threadID = threadID;

                _threadStart = new ThreadStart(() =>
                {
                    threadOneMainDispatcherUpdate(_threadID);
                });

                _thread = new Thread(_threadStart);
                _thread.IsBackground = true;
                _thread.SetApartmentState(ApartmentState.STA);
                _thread.Start();
            }


            public static int _whosFirst = 0;
            public static int _threadCreationCounter = 0;
            public static bool _canLoadStarterItems = true;
            //[STAThread]
            void threadOneMainDispatcherUpdate(int threadIndex)
            {
                int _threadIndex = threadIndex;
                int _workerFrame = 0;

                try
                {
                    SC_Console.consoleMessageQueue messageQueue00 = new SC_Console.consoleMessageQueue("SC_AvailableThreads#" + _totalThreads, 0, 0);
                    SC_Console.consoleMessageQueue messageQueue0 = new SC_Console.consoleMessageQueue("#" + _threadIndex + "", 0, 3 + _threadIndex);


                    while (true)
                    {
                        //List of functions that the multithreaded app will start every frames.
                        ////////////////////
                        _threadUpdateTest();
                        ////////////////////
                        if (_canLoadStarterItems)
                        {
                            
                            //SC_AI_Language.threadOneGrammarLoad();
                            _canLoadStarterItems = false;
                        }


                        if (_workerFrame > -1)
                        {
                            _threadCreationCounter++;
                            _workerFrame = 0;
                        }

                        _workerFrame++;
                        Thread.Sleep(1);
                    }
                }
//#pragma warning disable CS0168 // The variable 'ex' is declared but never used
                catch (Exception ex)
//#pragma warning restore CS0168 // The variable 'ex' is declared but never used
                {
                    SC_Console.consoleMessageQueue messageQueue0 = new SC_Console.consoleMessageQueue(ex.ToString(), 0, 20);
                }
            }
        }


        static SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();

        static bool mathSwitch = false;
        static string[] stringOfMathOps;
        static bool firstInput = false;
        static bool secondInput = false;
        static List<string> currentListOfCommands = new List<string>();

        static int functionCounter00 = 1;
        static int functionCounter01 = 1;
        static int functionCounter02 = 1;
        static int functionCounter03 = 1;

        static string lastWord = "";

        static string totalCombination = "";

        static string currentWord = "";

        static int counterTotalWords = 0;

        static int frameCounterForVoiceRecognition = 0;
        static int frameCounterForVoiceRecognitionRecognizedWords = 0;

        public void threadOneGrammarLoad()
        {
            Choices commands = new Choices();
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
            recEngine.RecognizeAsync(RecognizeMode.Multiple);
            recEngine.SpeechRecognized += recEngine_SpeechRecognized;
        }

        private static int _hitCounter = 0;
        void recEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            //currentWord = e.Result.Text;
            //Console.WriteLine("test"+ _hitCounter);
            _hitCounter++;
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
        }

















        //SC_GCGollect _GCollector;

        public static void _threadUpdateTest()
        {



            lookForKeyPress();
            //SC_Console.consoleMessageQueue messageQueue0 = new SC_Console.consoleMessageQueue("V" + "", 0, 1);
        }
















        //KEY PRESS AND VOICE RECORDER/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //********************************
        //********************************
        //********************************
        [DllImport("winmm.dll", EntryPoint = "mciSendStringA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern int mciSendString(string lpstrCommand, string lpstrReturnString, int uReturnLength, int hwndCallback);

        static bool mainCaptureSwitch = true;
        static Computer c = new Computer();
        static Computer a = new Computer();
        static string fullSubFolderPath = "";
        static string folderName = "";
        static int lastFrame = -1;
        static Process p;
        static int numberOfStartedRecordings = 0;

        static bool _pressOnce = true;
        static int _iterations = 0;


        public static void lookForKeyPress()
        {

            for (int i = 0; i < 1; i++)
            {

                if (System.Windows.Input.Keyboard.IsKeyDown(Key.R))
                {
                    if (_iterations > 0)
                    {
                        break;
                    }
                    SC_Console.consoleMessageQueue messageQueue0 = new SC_Console.consoleMessageQueue("test" + _iterations, 0, 15 + _iterations);
                    //SC_Console.consoleMessageQueue messageQueue0 = new SC_Console.consoleMessageQueue("#", 0, 15);


                    // training set for an NAND function
                    TrainingItem[] trainingSet =
                    {
                          new TrainingItem(true, 1, 0, 0),
                          new TrainingItem(true, 1, 0, 1),
                          new TrainingItem(true, 1, 1, 0),
                          new TrainingItem(false, 1, 1, 1)
                    };
                    // create a perceptron with 3 inputs
                    var perceptron = new BinaryPerceptron(3);

                    int attemptCount = 0;
                    // teach the neural network until all the inputs are correctly clasified
                    int _numberOfIterations = 0;

                    int errorCount = 0;

                    /*for (int j = 0; j < trainingSet.Length; j++)
                    {
                        var output = perceptron.Learn(trainingSet[j].Output, trainingSet[j].Inputs);
                        SC_Console.consoleMessageQueue messageQueue00 = new SC_Console.consoleMessageQueue("Fail\t {0} & {1} & {2} != {3}" + trainingSet[j].Inputs[0] + trainingSet[j].Inputs[1] + trainingSet[j].Inputs[2] + output, 0, 15 + errorCount + _numberOfIterations);
                        errorCount++;
                    }*/




                    _sometest:


                    for (int j = 0; j < trainingSet.Length; j++)
                    {
                        var output = perceptron.Learn(trainingSet[j].Output, trainingSet[j].Inputs);


                        /*object[] _someObject = new object[3];
                        _someObject[0] = "test";
                        _someObject[1] = 20;
                        _someObject[2] = 1;*/

                        //SC_Console.consoleMessageQueue messageQueue0 = new SC_Console.consoleMessageQueue(ex.ToString(), 0, 20);

                        //SC_Console.consoleMessageQueue messageQueue0;


                        if (output != trainingSet[j].Output)
                        {
                            byte[] arr = System.Text.Encoding.ASCII.GetBytes(trainingSet[j].Inputs[0].ToString());

                            /*for (int k = 0; k < arr.Length; k++)
                            {
                                Console.WriteLine(arr[k]);
                            }*/
                            int test = 0xff; // 0x0 = 0//0x33 = 51 //0xff = 255

                            Console.WriteLine(test);
                            //SC_Console.consoleMessageQueue messageQueue00 = new SC_Console.consoleMessageQueue("Fail\t {0} & {1} & {2} != {3}" + trainingSet[j].Inputs[0] + trainingSet[j].Inputs[1] + trainingSet[j].Inputs[2] + output, 0, 15 + errorCount + _numberOfIterations);
                            //SC_Console.consoleMessageQueue messageQueue00 = new SC_Console.consoleMessageQueue("Fail\t {0} & {1} & {2} != {3}" + item.Inputs[0] + item.Inputs[1] + item.Inputs[2] + output, 0, 15 + errorCount + _numberOfIterations);
                            //Console.WriteLine("Fail\t {0} & {1} & {2} != {3}", item.Inputs[0], item.Inputs[1], item.Inputs[2], output);
                            errorCount++;
                        }
                        else
                        {
                            //SC_Console.consoleMessageQueue messageQueue00 = new SC_Console.consoleMessageQueue("Pass\t {0} & {1} & {2} = {3}" + trainingSet[j].Inputs[0] + trainingSet[j].Inputs[1] + trainingSet[j].Inputs[2] + output, 0, 15 + errorCount + _numberOfIterations);
                            //SC_Console.consoleMessageQueue messageQueue01 = new SC_Console.consoleMessageQueue("Pass\t {0} & {1} & {2} = {3}" + item.Inputs[0] + item.Inputs[1] + item.Inputs[2] + output, 0, 15 + errorCount + _numberOfIterations);
                            //Console.WriteLine("Pass\t {0} & {1} & {2} = {3}", item.Inputs[0], item.Inputs[1], item.Inputs[2], output);
                            _numberOfIterations++;
                        }




                        //SC_Console.consoleMessageQueue messageQueue00 = new SC_Console.consoleMessageQueue("Fail\t {0} & {1} & {2} != {3}" + trainingSet[j].Inputs[0] + trainingSet[j].Inputs[1] + trainingSet[j].Inputs[2] + output, 0, 15 + errorCount + _numberOfIterations);
                        //SC_Console.consoleMessageQueue messageQueue00 = new SC_Console.consoleMessageQueue("Fail\t {0} & {1} & {2} != {3}", 0, 15 + errorCount + _numberOfIterations);
                        errorCount++;
                    }






                    //while (true)
                    //{
                    //bool output = false;
                    //SC_Console.consoleMessageQueue messageQueue00 = new SC_Console.consoleMessageQueue("Fail\t {0} & {1} & {2} != {3}", 0, 15 + errorCount + _numberOfIterations);
                    //errorCount++;
                    //SC_Console.consoleMessageQueue messageQueue00 = new SC_Console.consoleMessageQueue("-- Attempt: " + (++attemptCount), 5, 2 + attemptCount);

                    /*foreach (var item in trainingSet)
                    {
                        bool output = false;
                        SC_Console.consoleMessageQueue messageQueue00 = new SC_Console.consoleMessageQueue("Fail\t {0} & {1} & {2} != {3}" + item.Inputs[0] + item.Inputs[1] + item.Inputs[2] + output, 0, 15 + errorCount + _numberOfIterations);
                        errorCount++;
                        // teach the perceptron to which class given inputs belong
                        /*var output = perceptron.Learn(item.Output, item.Inputs);
                        // check that the inputs were classified correctly
                        if (output != item.Output)
                        {
                            SC_Console.consoleMessageQueue messageQueue00 = new SC_Console.consoleMessageQueue("Fail\t {0} & {1} & {2} != {3}" + item.Inputs[0] + item.Inputs[1] + item.Inputs[2] + output, 0, 15 + errorCount + _numberOfIterations);
                            //Console.WriteLine("Fail\t {0} & {1} & {2} != {3}", item.Inputs[0], item.Inputs[1], item.Inputs[2], output);
                            errorCount++;
                        }
                        else
                        {
                            SC_Console.consoleMessageQueue messageQueue01 = new SC_Console.consoleMessageQueue("Pass\t {0} & {1} & {2} = {3}" + item.Inputs[0] + item.Inputs[1] + item.Inputs[2] + output, 0, 15 + errorCount + _numberOfIterations);
                            //Console.WriteLine("Pass\t {0} & {1} & {2} = {3}", item.Inputs[0], item.Inputs[1], item.Inputs[2], output);
                            _numberOfIterations++;
                        }
                    }*/
                    //break;
                    // only quit when there were no unexpected outputs detected
                  
                    if (errorCount == 0)
                        break;
                    goto _sometest;
                    _iterations++;
                    //}

                    /*// ensure perceptron is working
                    Debug.Assert(perceptron.GetResult(1, 0, 0) == true);
                    Debug.Assert(perceptron.GetResult(1, 0, 1) == true);
                    Debug.Assert(perceptron.GetResult(1, 1, 0) == true);
                    Debug.Assert(perceptron.GetResult(1, 1, 1) == false);
                    //GC.Collect();*/


                }             
            }


           





















            /*if (System.Windows.Input.Keyboard.IsKeyDown(Key.Enter))
            {
                GC.Collect(0);
                GC.Collect(1);
                GC.Collect(2);
                GC.Collect(10);
            }*/














            /*if (_iterations > 0)
            {
                _pressOnce = true;
            }*/



            /*if (System.Windows.Input.Keyboard.IsKeyDown(Key.R))
            {
                if (_pressOnce)
                {
                    //SC_Console.WriteAt("r", 1, 1, false, 0);
                    SC_Console.consoleMessageQueue messageQueue0 = new SC_Console.consoleMessageQueue("#", 0, 1);

                    mciSendString("open new Type waveaudio Alias recsound", "", 0, 0);
                    mciSendString("record recsound", "", 0, 0);
                    numberOfStartedRecordings++;
                    _pressOnce = false;
                }
            }


            else if (System.Windows.Input.Keyboard.IsKeyDown(Key.Y))
            {
                if (mainCaptureSwitch)
                {
                    string strWorkingDirectory = Directory.GetCurrentDirectory();

                    string soundDirectory = "\\SC_AI_AudioSaves";

                    fullSubFolderPath = strWorkingDirectory + soundDirectory;

                    //Directory.GetFiles(fullSubFolderPath);

                    if (!Directory.Exists(fullSubFolderPath))
                    {
                        Directory.CreateDirectory(fullSubFolderPath);
                    }


                    //SC_Console.WriteAt("y", 1, 1, false, 0);
                    SC_Console.consoleMessageQueue messageQueue0 = new SC_Console.consoleMessageQueue("y", 0, 2);
                    folderName = fullSubFolderPath + "\\" + "SC_AI_AudioSaves" + numberOfStartedRecordings + ".wav";

                    mciSendString("save recsound" + " " + folderName, "", 0, 0);
                    mciSendString("close recsound ", "", 0, 0);
                    //Computer a = new Computer();
                    c.Audio.Stop();
                    lastFrame = numberOfStartedRecordings;

                    p = new Process();
                    p.StartInfo = new ProcessStartInfo()
                    {
                        FileName = fullSubFolderPath //"c:\\"
                    };

                    p.Start();
                    p.Refresh();
                    _pressOnce = true;
                    mainCaptureSwitch = false;
                }
            }

            else if (System.Windows.Input.Keyboard.IsKeyDown(Key.P))
            {
                //SC_Console.WriteAt("p", 1, 1, false, 0);
                SC_Console.consoleMessageQueue messageQueue0 = new SC_Console.consoleMessageQueue("p", 0, 3);

                if (Directory.Exists(fullSubFolderPath))
                {
                    //string[] dirs = Directory.GetFiles(@"c:\", "c*");
                    //string[] dirs = Directory.GetFiles(fullSubFolderPath, "*SC_AI_AudioSaves" + frameRate + "*");
                    //string[] dirs = Directory.GetFiles(fullSubFolderPath, "*SC_AI_AudioSaves" + frameRate+".wav");
                    string[] dirs = Directory.GetFiles(fullSubFolderPath, "*" + lastFrame + "*" + ".wav");
                    SC_Console.WriteAt(dirs.Length.ToString(), 0, 4, false, 0);
                    //Computer aa = new Computer();
                    c.Audio.Play(dirs[0]);

                }
                mainCaptureSwitch = true;
            }*/
        }
        //********************************
        //********************************
        //********************************
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////





























        private void ContentFrame_OnNavigated(object sender, NavigationEventArgs e)
        {
            // Prevent navigation (for example clicking back button) because our ListBox is not updated when this navigation occurs
            // We prevent navigation with clearing the navigation history each time navigation item changes
            //ContentFrame0.NavigationService.RemoveBackEntry();
            //ContentFrame1.NavigationService.RemoveBackEntry();
        }

    }
}
