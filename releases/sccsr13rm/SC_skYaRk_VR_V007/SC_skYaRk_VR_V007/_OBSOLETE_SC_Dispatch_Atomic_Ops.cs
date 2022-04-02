using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

//using _console_writer_message_queue = SC_skYaRk_VR_Edition_v003.SC_Console_WRITER._console_writer_message_queue;

namespace SC_skYaRk_VR_Edition_v003
{
    public class SC_Dispatch_Atomic_Ops
    {
        const int LOWERBOUND = 0;
        const int UPPERBOUND = 1001;

        static Object lockObj = new Object();
        static Random rnd = new Random();

        static int totalCount = 0;
        static int totalMidpoint = 0;
        static int midpointCount = 0;

        //static public List<_console_writer_message_queue> _Console_writer_message_queue;
        //static public _console_writer_message_queue _msg_wr = new _console_writer_message_queue();

        public SC_Dispatch_Atomic_Ops(SC_Console_CORE _CONSOLE_CORE)
        {
            /*_Console_writer_message_queue = _CONSOLE_CORE._CONSOLE_WRITER._Console_writer_message_queue;
            _msg_wr = new _console_writer_message_queue();

            _msg_wr._message = "_IN_1000_x00 starting Console";
            _msg_wr._lineX = 0;
            _msg_wr._lineY = 5;
            lock (_Console_writer_message_queue)
            {
                _Console_writer_message_queue.Add(_msg_wr);
            }
            System.Windows.MessageBox.Show("Console TEST", "Console");
            */

            List<Task> tasks = new List<Task>();
            // Start three tasks. 
            for (int ctr = 0; ctr <= 2; ctr++)
                tasks.Add(Task.Run(() => {
                    int midpoint = (UPPERBOUND - LOWERBOUND) / 2;
                    int value = 0;
                    int total = 0;
                    int midpt = 0;

                    do
                    {
                        lock (lockObj)
                        {
                            value = rnd.Next(LOWERBOUND, UPPERBOUND);
                        }
                        if (value == midpoint)
                        {
                            Interlocked.Increment(ref midpointCount);
                            midpt++;
                        }
                        total++;
                    } while (midpointCount < 50000);

                    Interlocked.Add(ref totalCount, total);
                    Interlocked.Add(ref totalMidpoint, midpt);

                    /*string s = String.Format("Task {0}:\n", Task.CurrentId) +
                               String.Format("   Random Numbers: {0:N0}\n", total) +
                               String.Format("   Midpoint values: {0:N0} ({1:P3})", midpt,
                                             ((double)midpt) / total);
                    Console.WriteLine(s);*/

                }));

            Task.WaitAll(tasks.ToArray());
            /*Console.WriteLine();
            Console.WriteLine("Total midpoint values:  {0,10:N0} ({1:P3})",
                              totalMidpoint, totalMidpoint / ((double)totalCount));
            Console.WriteLine("Total number of values: {0,10:N0}",
                              totalCount);*/
        }

        // The example displays output like the following:
        //       Task 3:
        //          Random Numbers: 10,855,250
        //          Midpoint values: 10,823 (0.100 %)
        //       Task 1:
        //          Random Numbers: 15,243,703
        //          Midpoint values: 15,110 (0.099 %)
        //       Task 2:
        //          Random Numbers: 24,107,425
        //          Midpoint values: 24,067 (0.100 %)
        //       
        //       Total midpoint values:      50,000 (0.100 %)
        //       Total number of values: 50,206,378
    }
}
