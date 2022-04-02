using System;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;

using System.IO.Compression;
using System.Windows;

namespace sccscleansolution
{
    public class Program
    {

        public static int swtcthreadstartonce = 0;



        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(IntPtr h, string m, string c, int type);

        static int _init_main = 1;


        static void Main(string[] args)
        {
        //Console.WriteLine("Hello World!");
        //Console.WriteLine("Hello World!");



        _main_thread_Loop_x00:

            if (_init_main == 1)
            {
                try
                {






                    if (swtcthreadstartonce == 0)
                    {
                        //BULK ZIP FILES 
                        //BULK ZIP FILES 
                        //BULK ZIP FILES 
                        //string path = @"C:\Users\steve\Documents\GitHub\2020-april-coding-challenge.-development-of-sccoresystems-instancing-engine";// @"C:\Users\steve\Documents\GitHub\sccs\cchallenges";
                        //string path = @"C:\Users\steve\Documents\GitHub\sccs\cchallenges\cc-2020\2020-04-09";
                        //string path = @"C:\Users\steve\Documents\GitHub\sccs\cchallenges\cc-2020\2020-04-10";
                        string path = @"C:\Users\steve\Documents\GitHub\sccs\cchallenges\cc-feb-2022";


                        try
                        {
                            if (Directory.Exists(path))
                            {
                                //Directory.GetDirectories

                                string[] dirs = Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly); //@"c:\" //"p*"
                                Console.WriteLine("The number of directories starting with packages is {0}.", dirs.Length);


                                for (int i = 0; i < dirs.Length; i++) //
                                {
                                    //string somefilename = dirs[i] + "-2020-04-09";// dirs[i].Substring(0, dirs[i].Length - 19) + "keybor00";
                                    //Directory.Delete(dirs[i], true);
                                    //System.IO.File.Move(dirs[i], somefilename);

                                    ZipFile.CreateFromDirectory(dirs[i], dirs[i] + ".zip");
                                }
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                        MessageBox((IntPtr)0, "bulk zip finished", "Oculus Error", 0);
                        //BULK ZIP FILES 
                        //BULK ZIP FILES 
                        //BULK ZIP FILES

                        swtcthreadstartonce = 1;
                    }























                    //BULK FOLDER CHANGE
                    //BULK FOLDER CHANGE
                    //BULK FOLDER CHANGE
                    /*string FILENAME = "sccoresystems-workingkeyboard-r00";// "SC_instancedChunk_shader_final - 2021-july-15backup.cs";
                    //string moddedFILENAME = "SC_instancedChunk_shader_final2021july15.cs";

                    string path = @"C:\Users\steve\Documents\GitHub\sccs\cchallenges";

                    try
                    {
                        if (Directory.Exists(path))
                        {
                            //Directory.GetDirectories

                            string[] files = Directory.GetDirectories(path, FILENAME, SearchOption.AllDirectories); //@"c:\" //"p*"
                     
                            //string[] files = Directory.GetFiles(path, FILENAME, SearchOption.AllDirectories);

                            Console.WriteLine("The number of directories starting with that is {0}.", files.Length);

                            for (int i = 0; i < files.Length; i++)
                            {
                                Console.WriteLine(files[i]);
                                //int indexofscriptname = files[i].IndexOf("SC_instancedChunk_shader_final");
                                string somefilename = files[i].Substring(0, files[i].Length - 19) + "keybor00";
                                //Directory.Delete(dirs[i], true);
                                System.IO.File.Move(files[i], somefilename);
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    MessageBox((IntPtr)0, "folder name change finished", "Oculus Error", 0);*/
                    //BULK FOLDER CHANGE
                    //BULK FOLDER CHANGE
                    //BULK FOLDER CHANGE



                    //BULK FOLDER NAME CHANGE
                    //BULK FOLDER NAME CHANGE
                    //BULK FOLDER NAME CHANGE
                    //string FILENAME = "sccoresystems-workingkeyboard-r00";// "SC_instancedChunk_shader_final - 2021-july-15backup.cs";
                    //string moddedFILENAME = "SC_instancedChunk_shader_final2021july15.cs";

                    /*string path = @"C:\Users\steve\Documents\GitHub\2020-april-coding-challenge.-development-of-sccoresystems-instancing-engine";// @"C:\Users\steve\Documents\GitHub\sccs\cchallenges";

                    try
                    {
                        if (Directory.Exists(path))
                        {
                            //Directory.GetDirectories

                            string[] files = Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly); //@"c:\" //"p*"

                            //string[] files = Directory.GetFiles(path, FILENAME, SearchOption.AllDirectories);

                            Console.WriteLine("The number of directories starting with that is {0}.", files.Length);

                            for (int i = 0; i < files.Length; i++)
                            {
                                Console.WriteLine(files[i]);
                                //int indexofscriptname = files[i].IndexOf("SC_instancedChunk_shader_final");
                                string somefilename =files[i] + "-cc-";
                                //Directory.Delete(dirs[i], true);
                                System.IO.File.Move(files[i], somefilename);
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    MessageBox((IntPtr)0, "folder name change finished", "Oculus Error", 0);*/
                    //BULK FOLDER NAME CHANGE
                    //BULK FOLDER NAME CHANGE
                    //BULK FOLDER NAME CHANGE










                    //string FILENAME = "SC_Console_GRAPHICS - backup working instances - cubes only - Copy.cs";

                    //SC_instancedChunk_shader_final - 2021-july-15backup.cs

                    //sccoresystems-workingkeyboard-r00


                    //BULK FILENAME CHANGE
                    //BULK FILENAME CHANGE
                    //BULK FILENAME CHANGE
                    /*string FILENAME = "SC_instancedChunk_shader_final - 2021-july-15backup.cs";
                    string moddedFILENAME = "SC_instancedChunk_shader_final2021july15.cs";

                    string path = @"C:\Users\steve\Documents\GitHub\sccs\cchallenges";

                    try
                    {
                        if (Directory.Exists(path))
                        {
                            //Directory.GetDirectories

                            //string[] dirs = Directory.GetDirectories(path, "packages*", SearchOption.AllDirectories); //@"c:\" //"p*"

                            string[] files = Directory.GetFiles(path, FILENAME, SearchOption.AllDirectories);

                            Console.WriteLine("The number of directories starting with packages is {0}.", files.Length);

                            for (int i = 0; i < files.Length; i++)
                            {
                                Console.WriteLine(files[i]);
                                //int indexofscriptname = files[i].IndexOf("SC_instancedChunk_shader_final");
                                string somefilename = files[i].Substring(0, files[i].Length - 23) + "-2021july15.cs";
                                //Directory.Delete(dirs[i], true);
                                System.IO.File.Move(files[i], somefilename);
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    MessageBox((IntPtr)0, "packages cleaning finished", "Oculus Error", 0);*/
                    //BULK FILENAME CHANGE
                    //BULK FILENAME CHANGE
                    //BULK FILENAME CHANGE















                    Thread _mainTasker00 = new Thread((tester0000) =>
                    {



                        //CLEAN BIN/OBJ/PACKAGES FOR A FOLDERS AND SUBFOLDER.
                        //CLEAN BIN/OBJ/PACKAGES FOR A FOLDERS AND SUBFOLDER.
                        //CLEAN BIN/OBJ/PACKAGES FOR A FOLDERS AND SUBFOLDER.
                        //string path = @"C:\Users\steve\Documents\GitHub\sccs\cchallenges";
                        /*string path = @"C:\Users\steve\Documents\GitHub\2020-april-coding-challenge.-development-of-sccoresystems-instancing-engine";

                        if (Directory.Exists(path))
                        {
                            //Directory.GetDirectories

                            string[] dirs = Directory.GetDirectories(path, "packages*", SearchOption.AllDirectories); //@"c:\" //"p*"
                            Console.WriteLine("The number of directories starting with packages is {0}.", dirs.Length);

                            for (int i = 0; i < dirs.Length; i++)
                            {
                                Directory.Delete(dirs[i], true);
                            }
                        }
                        try
                        {

                        }
                        catch (Exception ex)
                        {

                        }
                        MessageBox((IntPtr)0, "packages cleaning finished", "Oculus Error", 0);



                        //path = @"C:\Users\steve\Documents\GitHub\sccs\cchallenges";
                        if (Directory.Exists(path))
                        {
                            //Directory.GetDirectories

                            string[] dirs = Directory.GetDirectories(path, "bin*", SearchOption.AllDirectories); //@"c:\" //"p*"
                            Console.WriteLine("The number of directories starting with bin is {0}.", dirs.Length);


                            for (int i = 0; i < dirs.Length; i++)
                            {
                                Directory.Delete(dirs[i], true);
                            }

                        }
                        try
                        {

                        }
                        catch (Exception ex)
                        {

                        }
                        MessageBox((IntPtr)0, "bin cleaning finished", "Oculus Error", 0);





                        //path = @"C:\Users\steve\Documents\GitHub\sccs\cchallenges";

                        //path = @"C:\Users\steve\Documents\GitHub\2020-april-coding-challenge.-development-of-sccoresystems-instancing-engine\";
                        if (Directory.Exists(path))
                        {
                            //Directory.GetDirectories
                            int resursiveloop = 1;

                            for (int j = 0; j < resursiveloop; j++)
                            {
                                string[] dirs = Directory.GetDirectories(path, "obj", SearchOption.AllDirectories); //@"c:\" //"p*"
                                Console.WriteLine("The number of directories starting with obj is {0}.", dirs.Length);
                                MessageBox((IntPtr)0, "The number of directories starting with obj is {0}.", "Oculus Error", 0);

                                for (int i = 0; i < dirs.Length; i++)
                                {
                                    Directory.Delete(dirs[i], true);
                                }
                            }


                        }

                        try
                        {

                        }
                        catch (Exception ex)
                        {

                        }
                        MessageBox((IntPtr)0, "obj cleaning finished", "Oculus Error", 0);







                        //path = @"C:\Users\steve\Documents\GitHub\sccs\cchallenges";
                        if (Directory.Exists(path))
                        {
                            //Directory.GetDirectories

                            string[] dirs = Directory.GetDirectories(path, ".vs*", SearchOption.AllDirectories); //@"c:\" //"p*"
                            Console.WriteLine("The number of directories starting with .vs is {0}.", dirs.Length);


                            for (int i = 0; i < dirs.Length; i++)
                            {
                                Directory.Delete(dirs[i], true);
                            }

                        }
                        try
                        {

                        }
                        catch (Exception ex)
                        {

                        }
                        MessageBox((IntPtr)0, ".vs cleaning finished", "Oculus Error", 0);

                        //path = @"C:\Users\steve\Documents\GitHub\sccs\cchallenges";
                        if (Directory.Exists(path))
                        {
                            //Directory.GetDirectories

                            string[] dirs = Directory.GetDirectories(path, ".idea*", SearchOption.AllDirectories); //@"c:\" //"p*"
                            Console.WriteLine("The number of directories starting with .idea is {0}.", dirs.Length);


                            for (int i = 0; i < dirs.Length; i++)
                            {
                                Directory.Delete(dirs[i], true);
                            }

                        }
                        try
                        {

                        }
                        catch (Exception ex)
                        {

                        }
                        MessageBox((IntPtr)0, ".idea cleaning finished", "Oculus Error", 0);
                        //CLEAN BIN/OBJ/PACKAGES FOR A FOLDERS AND SUBFOLDER.
                        //CLEAN BIN/OBJ/PACKAGES FOR A FOLDERS AND SUBFOLDER.
                        //CLEAN BIN/OBJ/PACKAGES FOR A FOLDERS AND SUBFOLDER.*/





                        /*
                        if (swtcthreadstartonce == 0)
                        {
                            //BULK ZIP FILES 
                            //BULK ZIP FILES 
                            //BULK ZIP FILES 
                            string path = @"C:\Users\steve\Documents\GitHub\2020-april-coding-challenge.-development-of-sccoresystems-instancing-engine";// @"C:\Users\steve\Documents\GitHub\sccs\cchallenges";
                            //string path = @"C:\Users\steve\Documents\GitHub\sccs\cchallenges\cc-2020\2020-04-09";

                            try
                            {
                                if (Directory.Exists(path))
                                {
                                    //Directory.GetDirectories

                                    string[] dirs = Directory.GetDirectories(path, "*", SearchOption.AllDirectories); //@"c:\" //"p*"
                                    Console.WriteLine("The number of directories starting with packages is {0}.", dirs.Length);


                                    for (int i = 0; i < dirs.Length; i++)
                                    {
                                        ZipFile.CreateFromDirectory(dirs[i], dirs[i] + ".zip");
                                    }


                                }
                            }
                            catch (Exception ex)
                            {

                            }
                            MessageBox((IntPtr)0, "packages cleaning finished", "Oculus Error", 0);
                            //BULK ZIP FILES 
                            //BULK ZIP FILES 
                            //BULK ZIP FILES

                            swtcthreadstartonce = 1;
                        }*/



                    _thread_main_loop:




                        Thread.Sleep(1);
                        goto _thread_main_loop;

                    }, 0);

                    _mainTasker00.IsBackground = true;
                    _mainTasker00.Priority = ThreadPriority.Normal;
                    _mainTasker00.SetApartmentState(ApartmentState.STA);
                    _mainTasker00.Start();
                }
                catch (Exception ex)
                {
                    //MessageBox((IntPtr)0, "" + ex.ToString(), "Oculus Error", 0);
                }

                _init_main = 2;
            }

            if (_init_main == 2)
            {
                //found with a google search that when using this option it at least makes you able to have a "prompt" or something in order for the console 
                //to have a looping "main" thread so i just added a goto loop to "catch" this first frame and to loop it.
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    goto _main_thread_Loop_x00;
                }
                else
                {
                    goto _main_thread_Loop_x00;
                }
            }
            else
            {
                //System.Windows.MessageBox.Show("lOOp", "CONSOLE");
                goto _main_thread_Loop_x00;
            }
            Console.WriteLine("nope... no program!");

        }
    }
}



/*var startSomeThread = new Action(() =>
                        {
                            //threadOneGrammarLoad();
                            Thread _thread;
                            _thread = new Thread(() => _mainThreadStarter());
                            _thread.IsBackground = false;
                            _thread.SetApartmentState(ApartmentState.STA);// =;
                            _thread.Start();
                        });

                        System.Windows.Application.Current.Dispatcher.Invoke(startSomeThread);*/
