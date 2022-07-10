using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sccsr15forms
{
    public class indextest
    {
        public indextest()
        {

            int minx = -5;
            int miny = 0;
            int minz = -5;

            int maxx = 5;
            int maxy = 5;
            int maxz = 5;

            int divx = 2;
            int divy = 2;
            int divz = 2;

            int incrementsdivx = (-minx + maxx) / divx;
            int incrementsdivy = (-miny + maxy) / divy;
            int incrementsdivz = (-minz + maxz) / divz;

            int[][] somearray = new int[divx* divy* divz][];

            for (int i = 0;i < somearray.Length;i++)
            {
                somearray[i] = new int[incrementsdivx * incrementsdivy * incrementsdivz];
            }

            int xis = 0;
            int yis = 0;
            int zis = 0;

            int xi = 0;
            int yi = 0;
            int zi = 0;

            int someswtc = 0;
            int someotherindex = 0;
            int someotherindexx = 0;

            for (var x = minx; x < maxx; x++)
            {
                for (var y = miny; y < maxy; y++)
                {
                    for (var z = minz; z < maxz; z++)
                    {
                        int xx = x;
                        int yy = y;
                        int zz = z;

                        if (xx < 0)
                        {
                            xx *= -1;
                            xx = xx + (maxx - 1);
                        }

                        if (yy < 0)
                        {
                            yy *= -1;
                            yy = yy + (maxy - 1);
                        }
                        if (zz < 0)
                        {
                            zz *= -1;
                            zz = zz + (maxz - 1);
                        }

                        int someindex = xx + (-minx + maxx) * (yy + (-miny + maxy) * zz);

                        someotherindex = xis + (incrementsdivx) * (yis + (incrementsdivy) * zis);          

                        //somearray[someotherindexx][someotherindex] = 1;

                        if (someswtc == 0)
                        {
                            //Console.WriteLine(someotherindex);

                            for (var zu = z; zu < z + incrementsdivz; zu++)
                            {
                                for (var xu = x; xu < x + incrementsdivx; xu++)
                                {
                                    for (var yu = y; yu < y + incrementsdivy; yu++)
                                    {
                                        int xxi = xu;
                                        int yyi = yu;
                                        int zzi = zu;

                                        if (xxi < 0)
                                        {
                                            xxi *= -1;
                                            xxi = xxi + (maxx - 1);
                                        }

                                        if (yyi < 0)
                                        {
                                            yyi *= -1;
                                            yyi = yyi + (maxy - 1);
                                        }
                                        if (zzi < 0)
                                        {
                                            zzi *= -1;
                                            zzi = zzi + (maxz - 1);
                                        }

                                        int indexof = xxi + (-minx + maxx) * (yyi + (-miny + maxy) * zzi);


                                        Console.WriteLine("mi:" + someindex + "/alti:" + indexof);

                                    }
                                }
                            }


















                            /*for (var zu = 0; zu < incrementsdivz; zu++)
                            {

                                for (var xu = 0; xu < incrementsdivx; xu++)
                                {

                                    for (var yu = 0; yu < incrementsdivy; yu++)
                                    {
                                        someotherindex = xu + (incrementsdivx) * (yu + (incrementsdivy) * zu);

                                        somearray[someotherindexx][someotherindex] = 1;

                                        zi++;
                                        if (zi == (divz))
                                        {
                                            xi++;
                                            zi = 0;
                                        }
                                        if (xi == divx)
                                        {
                                            yi++;
                                            xi = 0;
                                        }
                                        if (yi == (divy)) // 
                                        {
                                            someswtc = 1;
                                            yi = 0;
                                        }
                                        someotherindexx = xi + (divx) * (yi + (divy) * zi);
                                    }
                                }
                            }*/


                            int sometestx = xis + x;
                            int sometesty = yis + y;
                            int sometestz = zis + z;









                            int somenewindex = sometestx + (-minx + maxx) * (sometesty + (-miny + maxy) * sometestz);

                            Console.WriteLine("index:" + someindex + "/newindex:" + somenewindex);

                            zis++;
                            if (zis == (incrementsdivz))
                            {
                                xis++;
                                zis = 0;
                            }
                            if (xis == incrementsdivx)
                            {
                                yis++;
                                xis = 0;
                            }
                            if (yis == (incrementsdivy)) // 
                            {
                                zi++;
                                if (zi == (divz))
                                {
                                    xi++;
                                    zi = 0;
                                }
                                if (xi == divx)
                                {
                                    yi++;
                                    xi = 0;
                                }
                                if (yi == (divy)) // 
                                {
                                    someswtc = 1;
                                    yi = 0;
                                }

                                someotherindexx = xi + (divx) * (yi + (divy) * zi);
                                //Console.WriteLine(someotherindexx);

                                yis = 0;
                            }










                            /*
                           zis++;
                           if (zis == (incrementsdivz))
                           {
                               xis++;
                               zis = 0;
                           }
                           if (xis == incrementsdivx)
                           {
                               yis++;
                               xis = 0;
                           }
                           if (yis == (incrementsdivy)) // 
                           {                            
                               zi++;
                               if (zi == (divz))
                               {
                                   xi++;
                                   zi = 0;
                               }
                               if (xi == divx)
                               {
                                   yi++;
                                   xi = 0;
                               }
                               if (yi == (divy)) // 
                               {
                                   someswtc = 1;
                                   yi = 0;
                               }

                               someotherindexx = xi + (divx) * (yi + (divy) * zi);
                               //Console.WriteLine(someotherindexx);

                               yis = 0;                               
                           }*/




                        }
                    }
                }
            }



            for (int i = 0; i < somearray.Length; i++)
            {
                for (int ii = 0; ii < somearray[i].Length; ii++)
                {
                    Console.WriteLine(somearray[i][ii]);
                }
            }


        }

    }
}



/*

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sccsr15forms
{
    public class indextest
    {
        public indextest()
        {

            int minx = -5;
            int miny = 0;
            int minz = -5;

            int maxx = 5;
            int maxy = 5;
            int maxz = 5;

            int divx = 2;
            int divy = 2;
            int divz = 2;

            int incrementsdivx = (-minx + maxx) / divx;
            int incrementsdivy = (-miny + maxy) / divy;
            int incrementsdivz = (-minz + maxz) / divz;

            int[][] somearray = new int[divx* divy* divz][];

            for (int i = 0;i < somearray.Length;i++)
            {
                somearray[i] = new int[incrementsdivx * incrementsdivy * incrementsdivz];
            }

            int xis = 0;
            int yis = 0;
            int zis = 0;

            int xi = 0;
            int yi = 0;
            int zi = 0;

            int someswtc = 0;
            int someotherindex = 0;
            int someotherindexx = 0;


            for (var x = minx; x < maxx; x++)
            {
                for (var y = miny; y < maxy; y++)
                {
                    for (var z = minz; z < maxz; z++)
                    {
                        int someindex = x + (-minx + maxx) * (y + (-miny + maxy) * z);

                        someotherindex = xis + (incrementsdivx) * (yis + (incrementsdivy) * zis);



                        somearray[someotherindexx][someotherindex] = 1;

                        if (someswtc == 0)
                        {
                            //Console.WriteLine(someotherindex);
                            zis++;
                            if (zis == (incrementsdivz))
                            {
                                xis++;
                                zis = 0;
                            }

                            if (xis == incrementsdivx)
                            {
                                yis++;
                                xis = 0;
                            }
                            if (yis == (incrementsdivy)) // 
                            {                            
                                zi++;
                                if (zi == (divz))
                                {
                                    xi++;
                                    zi = 0;
                                }
                                if (xi == divx)
                                {
                                    yi++;
                                    xi = 0;
                                }
                                if (yi == (divy)) // 
                                {
                                    someswtc = 1;
                                    yi = 0;
                                }

                                someotherindexx = xi + (divx) * (yi + (divy) * zi);
                                //Console.WriteLine(someotherindexx);

                                yis = 0;                               
                            }
                        }
                    }
                }
            }



            for (int i = 0; i < somearray.Length; i++)
            {
                for (int ii = 0; ii < somearray[i].Length; ii++)
                {
                    Console.WriteLine(somearray[i][ii]);



                }
            }


        }

    }
}
*/
