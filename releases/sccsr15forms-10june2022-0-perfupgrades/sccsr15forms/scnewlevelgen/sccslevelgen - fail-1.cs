using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;


namespace sccsr15forms
{
    public class sccslevelgen
    {

        int[] arrayofcoords;

        public int somerw;
        public int somerh;
        public int somerd;
        int istypeofl = -2;
        int istypeofr = -2;
        int istypeoft = -2;
        int istypeofb = -2;
        //int istile = -1;

        int istypeoflt = -2;
        int istypeofrt = -2;
        int istypeoflb = -2;
        int istypeofrb = -2;

        public int somewidth;
        public int someheight;
        public int somedepth;

        public int[] levelmap;
        public int[] levelmapsortingtiles;
        public int[] levelmapsortingtilesremains;
        public int[] toremove;
        public int[] adjacenttiles;

        public int maxx;
        public int maxy;
        public int maxz;

        public static int wallheightsize = 10;

        public int maxtileamount = 10;


        public int minx;
        public int miny;
        public int minz;



        public void createlevel()
        {

            int minw = 15;
            int minh = 9;
            int mind = 15;

            int maxw = 20;
            int maxh = 12;
            int maxd = 20;

            somerw = (int)sc_maths.getSomeRandNumThousandDecimal(minw, maxw, 1, 0, 1);
            somerh = (int)sc_maths.getSomeRandNumThousandDecimal(minh, maxh, 1, 0, 1);
            somerd = (int)sc_maths.getSomeRandNumThousandDecimal(mind, maxd, 1, 0, 1);

            minx = (int)sc_maths.getSomeRandNumThousandDecimal(5, 10, 1, 2, 1);
            miny = 0;// (int)sc_maths.getSomeRandNumThousandDecimal(9, 13, 1, 2, 1);
            minz = (int)sc_maths.getSomeRandNumThousandDecimal(5, 10, 1, 2, 1);

            maxx = minx + somerw;
            maxy = miny + somerh;
            maxz = minz + somerd;

            wallheightsize = 10;


            somewidth = (int)(maxx - minx);
            someheight = (int)(maxy - miny);
            somedepth = (int)(maxz - minz);



            if (somewidth < 0)
            {
                somewidth *= -1;
            }
            if (someheight < 0)
            {
                someheight *= -1;
            }
            if (somedepth < 0)
            {
                somedepth *= -1;
            }


            //somewidth += 3;
            //someheight += 3;
            //somedepth += 3;

            maxtileamount = (somewidth * somedepth) / 1;

            levelmap = new int[somewidth * someheight * somedepth];
            levelmapsortingtiles = new int[somewidth * someheight * somedepth];
            levelmapsortingtilesremains = new int[somewidth * someheight * somedepth];
            toremove = new int[somewidth * someheight * somedepth];
            adjacenttiles = new int[somewidth * someheight * somedepth];


            List<int[]> listoftileloc = new List<int[]>();

            int somepointermax = 1;// maxtileamount / 10; //maxtileamount / 10

            for (int x = 0; x < somepointermax; x++)
            {
                int randx = (int)sc_maths.getSomeRandNumThousandDecimal(2, somewidth - 2, 1, 0, 0);
                int randy = (int)sc_maths.getSomeRandNumThousandDecimal(2, someheight - 2, 1, 0, 0);
                int randz = (int)sc_maths.getSomeRandNumThousandDecimal(2, somedepth - 2, 1, 0, 0);

                int posx = minx + randx;
                int posy = miny + randy;
                int posz = minz + randz;

                /* int[] somepos = new int[3];
                 somepos[0] = posx;
                 somepos[1] = posy;
                 somepos[2] = posz;

                 listoftileloc.Add(somepos);*/


                int[] somepos = new int[3];
                somepos[0] = 0;
                somepos[1] = 0;
                somepos[2] = 0;

                listoftileloc.Add(somepos);

                //Console.WriteLine("rx:" + randx + "/ry:" + randy + "/rz:" + randz);
                //Console.WriteLine("px:" + posx + "/py:" + posy + "/pz:" + posz);
            }














            int neighbooraddx = 3;
            int neighbooraddz = 3;

            int outofrange = 0;

            /* for (var i = 0; i < levelmap.Length; i++)
             {

                 levelmap[i] = -9;
             }
             */

            int[] leveltypes = new int[wallheightsize];
            for (int i = 0; i < leveltypes.Length; i++)
            {
                leveltypes[i] = i * -1;
            }


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
                        int indexinarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                        for (int i = 0; i < leveltypes.Length; i++)
                        {
                            if (leveltypes[i] * -1 == y)
                            {
                                if (y == 0)
                                {
                                    levelmap[indexinarray] = 999;
                                    levelmapsortingtiles[indexinarray] = 999;
                                    levelmapsortingtilesremains[indexinarray] = 999;
                                }
                                else
                                {
                                    levelmap[indexinarray] = leveltypes[i];
                                    levelmapsortingtiles[indexinarray] = leveltypes[i];
                                    levelmapsortingtilesremains[indexinarray] = leveltypes[i];
                                }
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                }
            }












            int countermaxtile = 0;
            for (var x = minx; x < maxx; x++)
            {
                for (var y = miny; y < maxy; y++)
                {
                    for (var z = minz; z < maxz; z++)
                    {

                        //Console.WriteLine(y);

                        if (countermaxtile >= maxtileamount)
                        {
                            Console.WriteLine("reached max0");
                            x = maxx;
                            y = maxy;
                            z = maxz;
                            break;
                        }
                        else
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

                            int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles



                            //for (int t = 0; t < maxtileamount; t++)
                            {
                                for (int p = 0; p < somepointermax; p++)
                                {

                                    /*if (countermaxtile >= maxtileamount)
                                    {
                                        Console.WriteLine("reached max1");
                                        x = maxx;
                                        y = maxy;
                                        z = maxz;
                                        //xi = neighbooraddx + 1;
                                        //zi = neighbooraddz + 1;
                                        break;
                                    }
                                    else
                                    {
                                        int neighboorx = listoftileloc[p][0];// (int)Math.Round(listoftileloc[p].X) + xi;
                                        //int neighboory = y;
                                        int neighboorz = listoftileloc[p][2];// (int)Math.Round(listoftileloc[p].Z) + zi;

                                        //Vector3 tilepos = new Vector3(neighboorx, 0, neighboorz);

                                        //arrayofcoords = new int[3];

                                        //arrayofcoords[0] = (neighboorx);
                                        //arrayofcoords[1] = (neighboory);
                                        //arrayofcoords[2] = (neighboorz);

                                        int xxi = neighboorx;// (int)Math.Round(tilepos.X);
                                        int yyi = 0;// (int)Math.Round(tilepos.Y);
                                        int zzi = neighboorz;//(int)Math.Round(tilepos.Z);

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

                                        int indexinarray = xxi + somewidth * (yyi + someheight * zzi); //y is always 0 on floor tiles

                                        if (indexinarray < somewidth * someheight * somedepth)
                                        {
                                            //levelmap[indexinarray] = 0;

                                            if (levelmap[indexinarray] == 999)
                                            {
                                                Console.WriteLine("/x:" + listoftileloc[p][0] + "/y:" + listoftileloc[p][1] + "/z:" + listoftileloc[p][2]);
                                                levelmap[indexinarray] = 0;
                                                countermaxtile++;
                                                /*if (xxi == maxx - 1)
                                                {
                                                    levelmap[indexinarray] = 2;
                                                }
                                                if (xxi == minx)
                                                {
                                                    levelmap[indexinarray] = 1;
                                                }
                                                if (zzi == maxz - 1)
                                                {
                                                    levelmap[indexinarray] = 4;
                                                }
                                                if (zzi == minz)
                                                {
                                                    levelmap[indexinarray] = 3;
                                                }
                                            }

                                            /*if (y == 0)
                                            {
                                                if (xxi == maxx - 1)
                                                {
                                                    levelmap[indexinarray] = 2;
                                                }
                                                else if (xxi == minx)
                                                {
                                                    levelmap[indexinarray] = 1;
                                                }
                                                /*if (zzi == maxz - 1)
                                                {
                                                    levelmap[indexinarray] = 4;
                                                }
                                                else if (zzi == minz)
                                                {
                                                    levelmap[indexinarray] = 3;
                                                }
                                                //
                                            }

                                            //levelmap[indexinarray] = countofarray;

                                            //levelmap[indexinarray] = 0;

                                            //levelmapfloor[xi][zi] = 1;
                                            //typeoftiles.Add(arrayofcoords, 0);
                                            //forsortingtiles.Add(arrayofcoords, 0);
                                        }
                                        else
                                        {
                                            //Console.WriteLine("OUT OF RANGE");
                                            outofrange = 1;
                                        }

                                    }*/







                                    outofrange = 0;
                                    for (int xi = -neighbooraddx; xi <= neighbooraddx; xi++)
                                    {
                                        //for (int yi = y; yi <= y; yi++)
                                        {
                                            for (int zi = -neighbooraddz; zi <= neighbooraddz; zi++)
                                            {
                                                if (countermaxtile >= maxtileamount)
                                                {
                                                    Console.WriteLine("reached max1");
                                                    x = maxx;
                                                    y = maxy;
                                                    z = maxz;
                                                    xi = neighbooraddx + 1;
                                                    zi = neighbooraddz + 1;
                                                    break;
                                                }
                                                int neighboorx = listoftileloc[p][0];// (int)Math.Round(listoftileloc[p].X) + xi;
                                                //int neighboory = y;
                                                int neighboorz = listoftileloc[p][2];// (int)Math.Round(listoftileloc[p].Z) + zi;

                                                //Vector3 tilepos = new Vector3(neighboorx, 0, neighboorz);

                                                //arrayofcoords = new int[3];

                                                //arrayofcoords[0] = (neighboorx);
                                                //arrayofcoords[1] = (neighboory);
                                                //arrayofcoords[2] = (neighboorz);

                                                int xxi = neighboorx;// (int)Math.Round(tilepos.X);
                                                int yyi = y;// (int)Math.Round(tilepos.Y);
                                                int zzi = neighboorz;//(int)Math.Round(tilepos.Z);

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

                                                int indexinarray = xxi + somewidth * (yyi + someheight * zzi); //y is always 0 on floor tiles

                                                if (indexinarray < somewidth * someheight * somedepth)
                                                {
                                                    //levelmap[indexinarray] = 0;

                                                    if (levelmap[indexinarray] == 999)
                                                    {
                                                        levelmapsortingtilesremains[indexinarray] = 0;
                                                        levelmapsortingtiles[indexinarray] = 0;
                                                        //Console.WriteLine(listoftileloc[p][1]);
                                                        levelmap[indexinarray] = 0;
                                                        countermaxtile++;
                                                        /*if (xxi == maxx - 1)
                                                        {
                                                            levelmap[indexinarray] = 2;
                                                        }
                                                        if (xxi == minx)
                                                        {
                                                            levelmap[indexinarray] = 1;
                                                        }
                                                        if (zzi == maxz - 1)
                                                        {
                                                            levelmap[indexinarray] = 4;
                                                        }
                                                        if (zzi == minz)
                                                        {
                                                            levelmap[indexinarray] = 3;
                                                        }*/
                                                    }

                                                    /*if (y == 0)
                                                    {
                                                        if (xxi == maxx - 1)
                                                        {
                                                            levelmap[indexinarray] = 2;
                                                        }
                                                        else if (xxi == minx)
                                                        {
                                                            levelmap[indexinarray] = 1;
                                                        }
                                                        /*if (zzi == maxz - 1)
                                                        {
                                                            levelmap[indexinarray] = 4;
                                                        }
                                                        else if (zzi == minz)
                                                        {
                                                            levelmap[indexinarray] = 3;
                                                        }


                                                        //
                                                    }*/





                                                    //levelmap[indexinarray] = countofarray;

                                                    //levelmap[indexinarray] = 0;

                                                    //levelmapfloor[xi][zi] = 1;
                                                    //typeoftiles.Add(arrayofcoords, 0);
                                                    //forsortingtiles.Add(arrayofcoords, 0);
                                                }
                                                else
                                                {
                                                    //Console.WriteLine("OUT OF RANGE");
                                                    //outofrange = 1;
                                                }
                                            }
                                        }
                                    }







                                    if (outofrange == 0)
                                    {
                                        float dirlr = (float)sc_maths.getSomeRandNumThousandDecimal(0, 2, 1, 0, 0);
                                        float dirfb = (float)sc_maths.getSomeRandNumThousandDecimal(0, 2, 1, 0, 0);
                                        //float dirldrd = (float)sc_maths.getSomeRandNumThousandDecimal(0, 2, 0.1f, 0, 0);
                                        //float dirfdbd = (float)sc_maths.getSomeRandNumThousandDecimal(0, 2, 0.1f, 0, 0);

                                        float decidedir = (float)sc_maths.getSomeRandNumThousandDecimal(0, 2, 1, 0, 0);

                                        if (decidedir == 0)
                                        {
                                            if (dirlr == 0)
                                            {

                                                int[] somevec = listoftileloc[p];
                                                somevec[0] -= 1;
                                                if (somevec[0] > minx)
                                                {
                                                    //Console.WriteLine("option 0");
                                                    listoftileloc[p] = somevec;
                                                }
                                                else
                                                {
                                                    //somevec = new int[3];
                                                    listoftileloc[p][0] = 0;
                                                    listoftileloc[p][1] = 0;
                                                    listoftileloc[p][2] = 0;

                                                    //listoftileloc[p] = somevec;// Vector3.Zero;
                                                }
                                            }
                                            else if (dirlr == 1)
                                            {
                                                //Console.WriteLine("option 1");
                                                int[] somevec = listoftileloc[p];
                                                somevec[0] += 1;
                                                if (somevec[0] < maxx)
                                                {
                                                    //Console.WriteLine("option 1");
                                                    listoftileloc[p] = somevec;
                                                }
                                                else
                                                {
                                                    //somevec = new int[3];
                                                    listoftileloc[p][0] = 0;
                                                    listoftileloc[p][1] = 0;
                                                    listoftileloc[p][2] = 0;

                                                    //listoftileloc[p] = somevec;// Vector3.Zero;
                                                }
                                            }
                                        }
                                        else if (decidedir == 1)
                                        {
                                            if (dirfb == 0)
                                            {

                                                int[] somevec = listoftileloc[p];
                                                somevec[2] -= 1;
                                                if (somevec[2] > minz)
                                                {
                                                    //Console.WriteLine("option 2");
                                                    listoftileloc[p] = somevec;
                                                }
                                                else
                                                {
                                                    //somevec = new int[3];
                                                    listoftileloc[p][0] = 0;
                                                    listoftileloc[p][1] = 0;
                                                    listoftileloc[p][2] = 0;

                                                    //listoftileloc[p] = somevec;// Vector3.Zero;
                                                }
                                            }
                                            else if (dirfb == 1)
                                            {

                                                int[] somevec = listoftileloc[p];
                                                somevec[2] += 1;
                                                if (somevec[2] > maxz)
                                                {
                                                    //Console.WriteLine("option 3");
                                                    listoftileloc[p] = somevec;
                                                }
                                                else
                                                {
                                                    //somevec = new int[3];
                                                    listoftileloc[p][0] = 0;
                                                    listoftileloc[p][1] = 0;
                                                    listoftileloc[p][2] = 0;

                                                    //listoftileloc[p] = somevec;// Vector3.Zero;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        int[] somevec = new int[3];
                                        somevec[0] = 0;
                                        somevec[1] = 0;
                                        somevec[2] = 0;

                                        listoftileloc[p] = somevec;// Vector3.Zero;
                                    }
                                }
                            }

                        }
                    }
                }
            }


































            /*
            for (var x = minx; x < maxx; x++)
            {
                for (var y = miny; y <= miny; y++)
                {
                    for (var z = minz; z < maxz; z++)
                    {

                        //Console.WriteLine(y);


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

                        int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                        for (int p = 0; p < somepointermax; p++)
                        {
                            /*int neighboorx = (listoftileloc[p][0]);
                            int neighboorz = (listoftileloc[p][2]);

                            int xxi = neighboorx;// (int)Math.Round(tilepos.X);
                            int yyi = y;// (int)Math.Round(tilepos.Y);
                            int zzi = neighboorz;//(int)Math.Round(tilepos.Z);

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

                            int indexinarray = xxi + somewidth * (0 + someheight * zzi); //y is always 0 on floor tiles

                            if (indexinarray < somewidth * someheight * somedepth)
                            {


                                if (yyi == 0)
                                {
                                    levelmap[indexinarray] = 0;


                                    /*if (xxi == maxx - 1)
                                    {
                                        Console.WriteLine("wall right");
                                        levelmap[indexinarray] = 2;
                                    }
                                    else if (xxi == minx)
                                    {
                                        levelmap[indexinarray] = 1;
                                    }

                                    if (zzi == maxz - 1)
                                    {
                                        levelmap[indexinarray] = 4;
                                    }
                                    else if (zzi == minz)
                                    {
                                        levelmap[indexinarray] = 3;
                                    }


                                    //
                                }
                            }
                            else
                            {
                                Console.WriteLine("OUT OF RANGE");
                                outofrange = 1;
                            }



                            outofrange = 0;
                            for (int xi = listoftileloc[p][0] - 1; xi <= listoftileloc[p][0]; xi++)
                            {
                                //for (int yi = y; yi <= y; yi++)
                                {
                                    for (int zi = listoftileloc[p][2] - 1; zi <= listoftileloc[p][2]; zi++)
                                    {
                                        //int neighboorx = listoftileloc[p][0] + xi;// (int)Math.Round(listoftileloc[p].X) + xi;
                                        //int neighboory = y;
                                        //int neighboorz = listoftileloc[p][2] + zi;// (int)Math.Round(listoftileloc[p].Z) + zi;

                                        //Vector3 tilepos = new Vector3(neighboorx, 0, neighboorz);

                                        //arrayofcoords = new int[3];

                                        //arrayofcoords[0] = (neighboorx);
                                        //arrayofcoords[1] = (neighboory);
                                        //arrayofcoords[2] = (neighboorz);

                                        int xxi = xi;// (int)Math.Round(tilepos.X);
                                        int yyi = y;// (int)Math.Round(tilepos.Y);
                                        int zzi = zi;//(int)Math.Round(tilepos.Z);

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

                                        int indexinarray = xxi + somewidth * (yyi + someheight * zzi); //y is always 0 on floor tiles

                                        if (indexinarray < somewidth * someheight * somedepth)
                                        {
                                            //levelmap[indexinarray] = 0;

                                            if (yyi == 0 && y == 0 && yy == 0)
                                            {
                                                //Console.WriteLine(listoftileloc[p][1]);
                                                levelmap[indexinarray] = 0;

                                                /*if (xxi == maxx - 1)
                                                {
                                                    levelmap[indexinarray] = 2;
                                                }
                                                if (xxi == minx)
                                                {
                                                    levelmap[indexinarray] = 1;
                                                }
                                                if (zzi == maxz - 1)
                                                {
                                                    levelmap[indexinarray] = 4;
                                                }
                                                if (zzi == minz)
                                                {
                                                    levelmap[indexinarray] = 3;
                                                }
                                            }






                                            /*if (y == 0)
                                            {
                                                if (xxi == maxx - 1)
                                                {
                                                    levelmap[indexinarray] = 2;
                                                }
                                                else if (xxi == minx)
                                                {
                                                    levelmap[indexinarray] = 1;
                                                }
                                                /*if (zzi == maxz - 1)
                                                {
                                                    levelmap[indexinarray] = 4;
                                                }
                                                else if (zzi == minz)
                                                {
                                                    levelmap[indexinarray] = 3;
                                                }


                                                //
                                            }





                                            //levelmap[indexinarray] = countofarray;

                                            //levelmap[indexinarray] = 0;

                                            //levelmapfloor[xi][zi] = 1;
                                            //typeoftiles.Add(arrayofcoords, 0);
                                            //forsortingtiles.Add(arrayofcoords, 0);
                                        }
                                        else
                                        {
                                            //Console.WriteLine("OUT OF RANGE");
                                            outofrange = 1;
                                        }
                                    }
                                }
                            }







                            if (outofrange == 0)
                            {
                                float dirlr = (float)sc_maths.getSomeRandNumThousandDecimal(0, 2, 1, 0, 0);
                                float dirfb = (float)sc_maths.getSomeRandNumThousandDecimal(0, 2, 1, 0, 0);
                                //float dirldrd = (float)sc_maths.getSomeRandNumThousandDecimal(0, 2, 0.1f, 0, 0);
                                //float dirfdbd = (float)sc_maths.getSomeRandNumThousandDecimal(0, 2, 0.1f, 0, 0);

                                float decidedir = (float)sc_maths.getSomeRandNumThousandDecimal(0, 2, 1, 0, 0);

                                if (decidedir == 0)
                                {
                                    if (dirlr == 0)
                                    {
                                        int[] somevec = listoftileloc[p];
                                        somevec[0] -= 1;
                                        if (somevec[0] > minx)
                                        {
                                            listoftileloc[p] = somevec;
                                        }
                                        else
                                        {
                                            somevec = new int[3];
                                            somevec[0] = 0;
                                            somevec[1] = 0;
                                            somevec[2] = 0;

                                            //listoftileloc[p] = somevec;// Vector3.Zero;
                                        }
                                    }
                                    else if (dirlr == 1)
                                    {

                                        int[] somevec = listoftileloc[p];
                                        somevec[0] += 1;
                                        if (somevec[0] < maxx)
                                        {
                                            listoftileloc[p] = somevec;
                                        }
                                        else
                                        {
                                            somevec = new int[3];
                                            somevec[0] = 0;
                                            somevec[1] = 0;
                                            somevec[2] = 0;

                                            //listoftileloc[p] = somevec;// Vector3.Zero;
                                        }
                                    }
                                }
                                else if (decidedir == 1)
                                {
                                    if (dirfb == 0)
                                    {
                                        int[] somevec = listoftileloc[p];
                                        somevec[2] -= 1;
                                        if (somevec[2] > minz)
                                        {
                                            listoftileloc[p] = somevec;
                                        }
                                        else
                                        {
                                            somevec = new int[3];
                                            somevec[0] = 0;
                                            somevec[1] = 0;
                                            somevec[2] = 0;

                                            //listoftileloc[p] = somevec;// Vector3.Zero;
                                        }
                                    }
                                    else if (dirfb == 1)
                                    {
                                        int[] somevec = listoftileloc[p];
                                        somevec[2] += 1;
                                        if (somevec[2] > maxz)
                                        {
                                            listoftileloc[p] = somevec;
                                        }
                                        else
                                        {
                                            somevec = new int[3];
                                            somevec[0] = 0;
                                            somevec[1] = 0;
                                            somevec[2] = 0;

                                            //listoftileloc[p] = somevec;// Vector3.Zero;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                int[] somevec = new int[3];
                                somevec[0] = 0;
                                somevec[1] = 0;
                                somevec[2] = 0;

                                listoftileloc[p] = somevec;// Vector3.Zero;
                            }
                        }
                    }
                }
            }


            Console.WriteLine("finished setting floor area.");

            /*
            var enumerator0 = typeoftiles.GetEnumerator();
            while (enumerator0.MoveNext())
            {
                var currentTuile = enumerator0.Current;
                var currentTile = currentTuile.Key;
                checkAllSides(currentTile);

            }
            //Console.WriteLine("NOT OUT OF RANGE1");

            createfinal();*/






            for (var x = minx; x < maxx; x++)
            {
                for (var y = miny; y < maxy; y++)
                {
                    for (var z = minz; z < maxz; z++)
                    {
                        /*if (y == 1)
                        {
                            Console.WriteLine("FAIL Y");
                        }*/
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

                        int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles


                        if (levelmap[indexinlevelarray] == 0)
                        {
                            checkAllSides(x, y, z);
                        }
                    }
                }
            }

            createfinal();

            /*
            for (var x = minx; x < maxx; x++)
            {
                for (var y = miny; y < maxy; y++)
                {
                    for (var z = minz; z < maxz; z++)
                    {
                        /*if (y == 1)
                        {
                            Console.WriteLine("FAIL Y");
                        }
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

                        int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles


                        //if (levelmap[indexinlevelarray] == 1001)
                        {
                           
                        }
                    }
                }
            }*/


































            //unLOADING CHUNK to XML
            //unLOADING CHUNK to XML
            string pathofrelease = Directory.GetCurrentDirectory();
            //Console.WriteLine(pathofrelease);
            string pathofchunkmap = pathofrelease + @"\chunkmaps\";

            if (!Directory.Exists(pathofchunkmap))
            {
                //Console.WriteLine("created directory");
                Directory.CreateDirectory(pathofchunkmap);
            }

            //int writetofilecounter = 0;

            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            var path = pathofchunkmap + @"\levelfloordata" + ".xml";

            var writer = new XmlTextWriter(path, System.Text.Encoding.UTF8);

            writer.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"UTF-8\"");
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 2;

            writer.WriteStartElement("root"); // open 0

            writer.WriteStartElement("size"); //open 4 //"\r\n" + 
            writer.WriteStartElement("w");
            writer.WriteValue(somewidth);
            writer.WriteEndElement();
            writer.WriteStartElement("h");
            writer.WriteValue(someheight);
            writer.WriteEndElement();
            writer.WriteStartElement("d");
            writer.WriteValue(somedepth);
            writer.WriteEndElement();
            writer.WriteStartElement("minx");
            writer.WriteValue(minx);
            writer.WriteEndElement();
            writer.WriteStartElement("maxx");
            writer.WriteValue(maxx);
            writer.WriteEndElement();
            writer.WriteStartElement("minz");
            writer.WriteValue(minz);
            writer.WriteEndElement();
            writer.WriteStartElement("maxz");
            writer.WriteValue(maxz);
            writer.WriteEndElement();
            writer.WriteStartElement("miny");
            writer.WriteValue(miny);
            writer.WriteEndElement();
            writer.WriteStartElement("maxy");
            writer.WriteValue(maxy);
            writer.WriteEndElement();

            writer.WriteEndElement(); //open 4 //"\r\n" + 

            writer.WriteStartElement("intmap"); //open 4 //"\r\n" + 
            writer.WriteValue("\r\n");
            //for (int x = 0; x < levelmapfloor.Length; x++)
            //{
            //    writer.WriteValue(levelmapfloor[x]);
            //    writer.WriteValue("\r\n");
            //}
            writer.WriteValue(levelmap);


            writer.WriteEndElement();
            writer.WriteEndElement(); //close 2
            writer.Close();

            Console.WriteLine("generated new level");
        }






























        int checky = 0;
        void checkAllSides(int xi, int yi, int zi)
        {
            for (int x = -1; x <= 1; x++)
            {
                for (int z = -1; z <= 1; z++)
                {
                    int checkx = (((xi + x)));

                    int checkz = (((zi + z)));

                    //float checkx = ((currentTilePos.x + x));
                    //float checkz = ((currentTilePos.z + z));

                    if (checkx == xi && checkz == zi + (1) ||
                        checkx == xi && checkz == zi - (1) ||
                        checkx == xi + (1) && checkz == zi ||
                        checkx == xi - (1) && checkz == zi ||

                        checkx == xi + (1) && checkz == zi + (1) ||
                        checkx == xi - (1) && checkz == zi + (1) ||
                        checkx == xi + (1) && checkz == zi - (1) ||
                        checkx == xi - (1) && checkz == zi - (1))
                    {
                        int xiii = checkx;
                        int ziii = checkz;

                        if (xiii < 0)
                        {
                            xiii *= -1;
                            xiii = xiii + (maxx - 1);
                        }
                        if (ziii < 0)
                        {
                            ziii *= -1;
                            ziii = ziii + (maxz - 1);
                        }

                        int indexinarray0 = xiii + somewidth * (yi + someheight * ziii); //y is always 0 on floor tiles

                        if (indexinarray0 < somewidth * someheight * somedepth)
                        {
                            if (levelmap[indexinarray0] == 999)
                            {
                                adjacenttiles[indexinarray0] = 1001;

                                //Console.WriteLine("testing function");
                                istypeofl = -2;
                                istypeofr = -2;
                                istypeoft = -2;
                                istypeofb = -2;

                                istypeoflt = -2;
                                istypeofrt = -2;
                                istypeoflb = -2;
                                istypeofrb = -2;

                                int tilex = x - 1;
                                int tiley = yi;
                                int tilez = z;

                                int xii = tilex;
                                int zii = tilez;

                                if (xii < 0)
                                {
                                    xii *= -1;
                                    xii = xii + (maxx - 1);
                                }
                                if (zii < 0)
                                {
                                    zii *= -1;
                                    zii = zii + (maxz - 1);
                                }
                                int indextile = xii + somewidth * (yi + someheight * zii);

                                if (indextile < somewidth * someheight * somedepth)
                                {
                                    if (levelmapsortingtiles[indextile] == 0)
                                    {
                                        istypeofl = 0;
                                    }
                                    else if (levelmapsortingtiles[indextile] == 998)
                                    {
                                        istypeofl = 1;
                                    }
                                    else
                                    {
                                        istypeofl = -1;
                                    }
                                }
                                else
                                {
                                    istypeofl = -1;
                                }


                                tilex = x + 1;
                                tiley = yi;
                                tilez = z;

                                xii = tilex;
                                zii = tilez;

                                if (xii < 0)
                                {
                                    xii *= -1;
                                    xii = xii + (maxx - 1);
                                }
                                if (zii < 0)
                                {
                                    zii *= -1;
                                    zii = zii + (maxz - 1);
                                }
                                indextile = xii + somewidth * (yi + someheight * zii);

                                if (indextile < somewidth * someheight * somedepth)
                                {
                                    if (levelmapsortingtiles[indextile] == 0)
                                    {
                                        istypeofr = 0;
                                    }
                                    else if (levelmapsortingtiles[indextile] == 998)
                                    {
                                        istypeofr = 1;
                                    }
                                    else
                                    {
                                        istypeofr = -1;
                                    }
                                }
                                else
                                {
                                    istypeofr = -1;
                                }


                                tilex = x;
                                tiley = yi;
                                tilez = z + 1;


                                xii = tilex;
                                zii = tilez;

                                if (xii < 0)
                                {
                                    xii *= -1;
                                    xii = xii + (maxx - 1);
                                }
                                if (zii < 0)
                                {
                                    zii *= -1;
                                    zii = zii + (maxz - 1);
                                }
                                indextile = xii + somewidth * (yi + someheight * zii);

                                if (indextile < somewidth * someheight * somedepth)
                                {
                                    if (levelmapsortingtiles[indextile] == 0)
                                    {
                                        istypeoft = 0;
                                    }
                                    else if (levelmapsortingtiles[indextile] == 998)
                                    {
                                        istypeoft = 1;
                                    }
                                    else
                                    {
                                        istypeoft = -1;
                                    }
                                }
                                else
                                {
                                    istypeoft = -1;
                                }



                                tilex = x;
                                tiley = yi;
                                tilez = z - 1;

                                xii = tilex;
                                zii = tilez;

                                if (xii < 0)
                                {
                                    xii *= -1;
                                    xii = xii + (maxx - 1);
                                }
                                if (zii < 0)
                                {
                                    zii *= -1;
                                    zii = zii + (maxz - 1);
                                }

                                indextile = xii + somewidth * (yi + someheight * zii);

                                if (indextile < somewidth * someheight * somedepth)
                                {
                                    if (levelmapsortingtiles[indextile] == 0)
                                    {
                                        istypeofb = 0;
                                    }
                                    else if (levelmapsortingtiles[indextile] == 998)
                                    {
                                        istypeofb = 1;
                                    }
                                    else
                                    {
                                        istypeofb = -1;
                                    }
                                }
                                else
                                {
                                    istypeofb = -1;
                                }
                                /////LEFT RIGHT FRONT BACK



                                tilex = x - 1;
                                tiley = yi;
                                tilez = z - 1;


                                xii = tilex;
                                zii = tilez;

                                if (xii < 0)
                                {
                                    xii *= -1;
                                    xii = xii + (maxx - 1);
                                }
                                if (zii < 0)
                                {
                                    zii *= -1;
                                    zii = zii + (maxz - 1);
                                }
                                indextile = xii + somewidth * (yi + someheight * zii);

                                if (indextile < somewidth * someheight * somedepth)
                                {
                                    if (levelmapsortingtiles[indextile] == 0)
                                    {
                                        istypeoflb = 0;
                                    }
                                    else if (levelmapsortingtiles[indextile] == 998)
                                    {
                                        istypeoflb = 1;
                                    }
                                    else
                                    {
                                        istypeoflb = -1;
                                    }
                                }
                                else
                                {
                                    istypeoflb = -1;
                                }



                                tilex = x + 1;
                                tiley = yi;
                                tilez = z - 1;


                                xii = tilex;
                                zii = tilez;

                                if (xii < 0)
                                {
                                    xii *= -1;
                                    xii = xii + (maxx - 1);
                                }
                                if (zii < 0)
                                {
                                    zii *= -1;
                                    zii = zii + (maxz - 1);
                                }
                                indextile = xii + somewidth * (yi + someheight * zii);

                                if (indextile < somewidth * someheight * somedepth)
                                {
                                    if (levelmapsortingtiles[indextile] == 0)
                                    {
                                        istypeofrb = 0;
                                    }
                                    else if (levelmapsortingtiles[indextile] == 998)
                                    {
                                        istypeofrb = 1;
                                    }
                                    else
                                    {
                                        istypeofrb = -1;
                                    }
                                }
                                else
                                {
                                    istypeofrb = -1;
                                }


                                tilex = x - 1;
                                tiley = yi;
                                tilez = z + 1;


                                xii = tilex;
                                zii = tilez;

                                if (xii < 0)
                                {
                                    xii *= -1;
                                    xii = xii + (maxx - 1);
                                }
                                if (zii < 0)
                                {
                                    zii *= -1;
                                    zii = zii + (maxz - 1);
                                }
                                indextile = xii + somewidth * (yi + someheight * zii);

                                if (indextile < somewidth * someheight * somedepth)
                                {
                                    if (levelmapsortingtiles[indextile] == 0)
                                    {
                                        istypeoflt = 0;
                                    }
                                    else if (levelmapsortingtiles[indextile] == 998)
                                    {
                                        istypeoflt = 1;
                                    }
                                    else
                                    {
                                        istypeoflt = -1;
                                    }
                                }
                                else
                                {
                                    istypeoflt = -1;
                                }



                                tilex = x + 1;
                                tiley = yi;
                                tilez = z + 1;


                                xii = tilex;
                                zii = tilez;

                                if (xii < 0)
                                {
                                    xii *= -1;
                                    xii = xii + (maxx - 1);
                                }
                                if (zii < 0)
                                {
                                    zii *= -1;
                                    zii = zii + (maxz - 1);
                                }
                                indextile = xii + somewidth * (yi + someheight * zii);

                                if (indextile < somewidth * someheight * somedepth)
                                {
                                    if (levelmapsortingtiles[indextile] == 0)
                                    {
                                        istypeofrt = 0;
                                    }
                                    else if (levelmapsortingtiles[indextile] == 998)
                                    {
                                        istypeofrt = 1;
                                    }
                                    else
                                    {
                                        istypeofrt = -1;
                                    }
                                }
                                else
                                {
                                    istypeofrt = -1;
                                }






                                //walls to the right
                                /////////////////////////////////////////////////////////////
                                if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
                                    istypeofl == 0 &&  /*/////////////*/ istypeofr == 0 &&
                                    istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0)
                                {

                                    int xx = (checkx);
                                    int yy = (yi);
                                    int zz = (checkz);

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

                                    int indexinarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                                    levelmapsortingtiles[indexinarray] = 0;

                                    //Console.WriteLine("test");
                                    /*typeoftiles.Add(currentTile, 0);
                                    //leftWall.Add(currentTile);
                                    //buildWallLeft();
                                    forsortingtiles.Remove(currentTile);*/

                                }
                                /*else if (istypeoflt == -1 || istypeoft == -1 || istypeofrt == -1 ||
                                        istypeofl == -1 || istypeofr == -1 ||
                                        istypeoflb == -1 || istypeofb == -1 || istypeofrb == -1)
                                {
                                    int xx = (checkx);
                                    int yy = (yi);
                                    int zz = (checkz);

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

                                    int indexinarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                                    levelmapsortingtiles[indexinarray] = 998;
                                }*/
                                else
                                {
                                    int xx = (checkx);
                                    int yy = (yi);
                                    int zz = (checkz);

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

                                    int indexinarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                                    levelmapsortingtiles[indexinarray] = 998;
                                }
                            }
                        }
                    }
                }
            }
        }
















        int counter = 1;
        public void createfinal()
        {
            if (counter == 1)
            {



                //floor tiles not discovered by random floor tiles pointers/movers are 999 but for simplication they are 9
                //TILE FLOOR MARKED E AS EXAMPLE
                //TILE FLOOR DISCOVERED MARKED F

                //99999999999
                //99999999999
                //9999FFF9999
                //9999FEF9999
                //9999FFF9999
                //99999999999
                //99999999999

                //ADJACENT TILE BUILD OBVIOUS FLOOR TILES OUT OF THAT
                //ADJACENT TILE BUILD OBVIOUS FLOOR TILES OUT OF THAT
                //ADJACENT TILE BUILD OBVIOUS FLOOR TILES OUT OF THAT
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

                            int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles


                            if (adjacenttiles[indexinlevelarray] == 1001) 
                            {
                                //Console.WriteLine("tile adjacent");
                                buildsomefloortiles(x, y, z); //,xx,yy,zz, indexinlevelarray
                                //buildWallsRerolled(x, y, z); //,xx,yy,zz, indexinlevelarray
                            }
                        }
                    }
                }
                //ADJACENT TILE BUILD OBVIOUS FLOOR TILES OUT OF THAT
                //ADJACENT TILE BUILD OBVIOUS FLOOR TILES OUT OF THAT
                //ADJACENT TILE BUILD OBVIOUS FLOOR TILES OUT OF THAT



                //floor tiles not discovered by random floor tiles pointers/movers are 999 but for simplication they are 9
                //TILE FLOOR MARKED E AS EXAMPLE
                //TILE FLOOR WITH AT LEAST 1 PROBABLE WALL WILL BE MARKED AS PROBABLE WALL TILE OR FLOOR TILE

                //99999999999
                //99999999999
                //9999???9999
                //9999?E?9999
                //9999???9999
                //99999999999
                //99999999999

                //ADJACENT TILE TAG TILES AS PROBABLE WALLS
                //ADJACENT TILE TAG TILES AS PROBABLE WALLS
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

                            int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                            if (adjacenttiles[indexinlevelarray] == 1001)
                            {
                                //Console.WriteLine("tile adjacent");
                                buildsomewallremains(x, y, z); //,xx,yy,zz, indexinlevelarray
                            }
                        }
                    }
                }
                //ADJACENT TILE TAG TILES AS PROBABLE WALLS
                //ADJACENT TILE TAG TILES AS PROBABLE WALLS
                


                //ADJACENT TILE BUILD WALL TILES LEFT/RIGHT/FRONT/BACK/DIAGLEFT/DIAGRIGHT/DIAGFRONT/DIAGBACK/ OUT OF THAT
                //ADJACENT TILE BUILD WALL TILES LEFT/RIGHT/FRONT/BACK/DIAGLEFT/DIAGRIGHT/DIAGFRONT/DIAGBACK/ OUT OF THAT
                //ADJACENT TILE BUILD WALL TILES LEFT/RIGHT/FRONT/BACK/DIAGLEFT/DIAGRIGHT/DIAGFRONT/DIAGBACK/ OUT OF THAT
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

                            int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles


                            if (adjacenttiles[indexinlevelarray] == 1001) 
                            {
                                //Console.WriteLine("tile adjacent");
                                buildWallsRerolled(x, y, z); //,xx,yy,zz, indexinlevelarray
                                //levelmap[indexinlevelarray] = 1;
                            }
                        }
                    }
                }
                //ADJACENT TILE BUILD WALL TILES LEFT/RIGHT/FRONT/BACK/DIAGLEFT/DIAGRIGHT/DIAGFRONT/DIAGBACK/ OUT OF THAT
                //ADJACENT TILE BUILD WALL TILES LEFT/RIGHT/FRONT/BACK/DIAGLEFT/DIAGRIGHT/DIAGFRONT/DIAGBACK/ OUT OF THAT
                //ADJACENT TILE BUILD WALL TILES LEFT/RIGHT/FRONT/BACK/DIAGLEFT/DIAGRIGHT/DIAGFRONT/DIAGBACK/ OUT OF THAT



                //paste adjacent tiles tagged 1001 unto tiles to remove.
                //paste adjacent tiles tagged 1001 unto tiles to remove.
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

                            int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                            if (adjacenttiles[indexinlevelarray] == 1001)
                            {
                                toremove[indexinlevelarray] = adjacenttiles[indexinlevelarray];
                            }
                            else
                            {
                                toremove[indexinlevelarray] = 995;
                            }
                        }
                    }
                }
                //paste adjacent tiles tagged 1001 unto tiles to remove.
                //paste adjacent tiles tagged 1001 unto tiles to remove.




                //FROM TILES ALREADY DISCOVERED FLOOR AND WALL TILES, TAG TOREMOVE ARRAY AS TILES ALREADY DISCOVERED AND DISCARD AS 995
                //FROM TILES ALREADY DISCOVERED FLOOR AND WALL TILES, TAG TOREMOVE ARRAY AS TILES ALREADY DISCOVERED AND DISCARD AS 995
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

                            int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                            if (levelmap[indexinlevelarray] == 1101 ||
                               levelmap[indexinlevelarray] == 1102 ||
                               levelmap[indexinlevelarray] == 1103 ||
                               levelmap[indexinlevelarray] == 1104 ||
                               levelmap[indexinlevelarray] == 1105 ||
                               levelmap[indexinlevelarray] == 1106 ||
                               levelmap[indexinlevelarray] == 1107 ||
                               levelmap[indexinlevelarray] == 1108 ||
                               levelmap[indexinlevelarray] == 1109 ||
                               levelmap[indexinlevelarray] == 1110 ||
                               levelmap[indexinlevelarray] == 1111 ||
                               levelmap[indexinlevelarray] == 1112)
                            {
                                if (toremove[indexinlevelarray] == 1001)
                                {
                                    toremove[indexinlevelarray] = 995;
                                }
                                
                            }

                            if (levelmapsortingtilesremains[indexinlevelarray] == 998)
                            {
                                if (toremove[indexinlevelarray] == 1001)
                                {
                                    toremove[indexinlevelarray] = 995;
                                }
                            }
                        }
                    }
                }
                //FROM TILES ALREADY DISCOVERED FLOOR AND WALL TILES, TAG TOREMOVE ARRAY AS TILES ALREADY DISCOVERED AND DISCARD AS 995
                //FROM TILES ALREADY DISCOVERED FLOOR AND WALL TILES, TAG TOREMOVE ARRAY AS TILES ALREADY DISCOVERED AND DISCARD AS 995










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

                            int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                            if (levelmap[indexinlevelarray] != 1101 &&
                                levelmap[indexinlevelarray] != 1102 &&
                                levelmap[indexinlevelarray] != 1103 &&
                                levelmap[indexinlevelarray] != 1104 &&
                                levelmap[indexinlevelarray] != 1105 &&
                                levelmap[indexinlevelarray] != 1106 &&
                                levelmap[indexinlevelarray] != 1107 &&
                                levelmap[indexinlevelarray] != 1108 &&
                                levelmap[indexinlevelarray] != 1109 &&
                                levelmap[indexinlevelarray] != 1110 &&
                                levelmap[indexinlevelarray] != 1111 &&
                                levelmap[indexinlevelarray] != 1112 &&
                                levelmap[indexinlevelarray] != 0)
                            {
                                if (toremove[indexinlevelarray] == 1001)
                                {
                                    levelmap[indexinlevelarray] = 0;
                                    levelmapsortingtiles[indexinlevelarray] = 0;
                                }
                            }
                        }
                    }
                }


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

                            int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                            //levelmapsortingtilesremains[indexinlevelarray] == 998 && 
                            if (levelmapsortingtilesremains[indexinlevelarray] == 998) //ADJACENT TILE BUILD WALL TILES LEFT/RIGHT/FRONT/BACK/DIAGLEFT/DIAGRIGHT/DIAGFRONT/DIAGBACK/ OUT OF THAT
                            {
                                buildWallsRerolled(x, y, z); //,xx,yy,zz, indexinlevelarray


                                /*if (levelmap[indexinlevelarray] != 1101 &&
                                   levelmap[indexinlevelarray] != 1102 &&
                                   levelmap[indexinlevelarray] != 1103 &&
                                   levelmap[indexinlevelarray] != 1104 &&
                                   levelmap[indexinlevelarray] != 1105 &&
                                   levelmap[indexinlevelarray] != 1106 &&
                                   levelmap[indexinlevelarray] != 1107 &&
                                   levelmap[indexinlevelarray] != 1108 &&
                                   levelmap[indexinlevelarray] != 1109 &&
                                   levelmap[indexinlevelarray] != 1110 &&
                                   levelmap[indexinlevelarray] != 1111 &&
                                   levelmap[indexinlevelarray] != 1112 &&
                                   levelmap[indexinlevelarray] != 0)
                                {
                                    buildWallsRerolled(x, y, z); //,xx,yy,zz, indexinlevelarray

                                }*/
                            }
                        }
                    }
                }



                /*

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

                            int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                            //levelmapsortingtilesremains[indexinlevelarray] == 998 && 
                            if (levelmapsortingtilesremains[indexinlevelarray] == 998) //ADJACENT TILE BUILD WALL TILES LEFT/RIGHT/FRONT/BACK/DIAGLEFT/DIAGRIGHT/DIAGFRONT/DIAGBACK/ OUT OF THAT
                            {
                                levelmap[indexinlevelarray] = 1101;
                            }
                        }
                    }
                }*/














                /*for (var x = minx; x < maxx; x++)
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

                            int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                            if (levelmap[indexinlevelarray] != 1101 &&
                               levelmap[indexinlevelarray] != 1102 &&
                               levelmap[indexinlevelarray] != 1103 &&
                               levelmap[indexinlevelarray] != 1104 &&
                               levelmap[indexinlevelarray] != 1105 &&
                               levelmap[indexinlevelarray] != 1106 &&
                               levelmap[indexinlevelarray] != 1107 &&
                               levelmap[indexinlevelarray] != 1108 &&
                               levelmap[indexinlevelarray] != 1109 &&
                               levelmap[indexinlevelarray] != 1110 &&
                               levelmap[indexinlevelarray] != 1111 &&
                               levelmap[indexinlevelarray] != 1112 &&
                               levelmap[indexinlevelarray] != 0 &&
                               levelmap[indexinlevelarray] != 998 &&
                               levelmap[indexinlevelarray] != 999)
                            {
                                levelmap[indexinlevelarray] = toremove[indexinlevelarray];
                            }
                        }
                    }
                }*/













                /*
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

                            int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                            if (toremove[indexinlevelarray] == 1101 ||
                               toremove[indexinlevelarray] == 1102 ||
                               toremove[indexinlevelarray] == 1103 ||
                               toremove[indexinlevelarray] == 1104 ||
                               toremove[indexinlevelarray] == 1105 ||
                               toremove[indexinlevelarray] == 1106 ||
                               toremove[indexinlevelarray] == 1107 ||
                               toremove[indexinlevelarray] == 1108 ||
                               toremove[indexinlevelarray] == 1109 ||
                               toremove[indexinlevelarray] == 1110 ||
                               toremove[indexinlevelarray] == 1111 ||
                               toremove[indexinlevelarray] == 1112)
                            {

                                //toremove[indexinlevelarray] = 995;

                            }
                            else
                            {
                                if (levelmap[indexinlevelarray] != 0)
                                {
                                    levelmap[indexinlevelarray] = toremove[indexinlevelarray];
                                }
                            }

                            /*if (levelmapsortingtilesremains[indexinlevelarray] == 998)
                            {
                                toremove[indexinlevelarray] = 995;
                            }
                        }
                    }
                }*/





                /*
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

                            int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                            //levelmapsortingtilesremains[indexinlevelarray] == 998 && 
                            if (levelmapsortingtilesremains[indexinlevelarray] == 998) //ADJACENT TILE BUILD WALL TILES LEFT/RIGHT/FRONT/BACK/DIAGLEFT/DIAGRIGHT/DIAGFRONT/DIAGBACK/ OUT OF THAT
                            {
                                //levelmapsortingtiles[indexinlevelarray] = 998;
                                //Console.WriteLine("tile adjacent");
                                buildWallsRerolled(x, y, z); //,xx,yy,zz, indexinlevelarray
                                //levelmap[indexinlevelarray] = 1101;
                                //levelmapsortingtilesremains[indexinlevelarray] = 995;
                            }
                        }
                    }
                }
                */







                /*
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

                            int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                            toremove[indexinlevelarray] = levelmap[indexinlevelarray];
                        }
                    }
                }


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

                            int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                            if (levelmap[indexinlevelarray] == 1101 ||
                               levelmap[indexinlevelarray] == 1102 ||
                               levelmap[indexinlevelarray] == 1103 ||
                               levelmap[indexinlevelarray] == 1104 ||
                               levelmap[indexinlevelarray] == 1105 ||
                               levelmap[indexinlevelarray] == 1106 ||
                               levelmap[indexinlevelarray] == 1107 ||
                               levelmap[indexinlevelarray] == 1108 ||
                               levelmap[indexinlevelarray] == 1109 ||
                               levelmap[indexinlevelarray] == 1110 ||
                               levelmap[indexinlevelarray] == 1111 ||
                               levelmap[indexinlevelarray] == 1112)
                            {
                                toremove[indexinlevelarray] = 995;
                            }

                            if (levelmapsortingtilesremains[indexinlevelarray] == 998)
                            {
                                toremove[indexinlevelarray] = 995;
                            }

                        }
                    }
                }




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

                            int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles


                            if (toremove[indexinlevelarray] == 1001) //ADJACENT TILE BUILD WALL TILES LEFT/RIGHT/FRONT/BACK/DIAGLEFT/DIAGRIGHT/DIAGFRONT/DIAGBACK/ OUT OF THAT
                            {
                                //Console.WriteLine("tile adjacent");
                                //buildsomefinalfloortiles(x, y, z); //,xx,yy,zz, indexinlevelarray
                                levelmap[indexinlevelarray] = 0;
                                levelmapsortingtiles[indexinlevelarray] = 0;
                            }
                        }
                    }
                }



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

                            int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles


                            if (levelmapsortingtilesremains[indexinlevelarray] == 998) //ADJACENT TILE BUILD WALL TILES LEFT/RIGHT/FRONT/BACK/DIAGLEFT/DIAGRIGHT/DIAGFRONT/DIAGBACK/ OUT OF THAT
                            {
                                //levelmapsortingtiles[indexinlevelarray] = 998;
                                //Console.WriteLine("tile adjacent");
                                buildWallsRerolled(x, y, z); //,xx,yy,zz, indexinlevelarray
                                //levelmap[indexinlevelarray] = 1101;
                                //levelmapsortingtilesremains[indexinlevelarray] = 995;
                            }
                        }
                    }
                }*/






















                /*
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

                            int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles


                            if (levelmap[indexinlevelarray] == 1001) //ADJACENT TILE BUILD WALL TILES LEFT/RIGHT/FRONT/BACK/DIAGLEFT/DIAGRIGHT/DIAGFRONT/DIAGBACK/ OUT OF THAT
                            {
                                //levelmapsortingtiles[indexinlevelarray] = 998;
                                //Console.WriteLine("tile adjacent");
                                //buildWallsRerolled(x, y, z); //,xx,yy,zz, indexinlevelarray
                                levelmap[indexinlevelarray] = 1101;
                                //levelmapsortingtilesremains[indexinlevelarray] = 995;
                            }
                        }
                    }
                }
                */





















                /*
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

                            int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles


                            if (levelmapsortingtilesremains[indexinlevelarray] == 998) //ADJACENT TILE BUILD WALL TILES LEFT/RIGHT/FRONT/BACK/DIAGLEFT/DIAGRIGHT/DIAGFRONT/DIAGBACK/ OUT OF THAT
                            {
                                //Console.WriteLine("tile adjacent");
                                buildWallsRerolled(x, y, z); //,xx,yy,zz, indexinlevelarray
                                //levelmap[indexinlevelarray] = 1101;
                            }
                        }
                    }
                }
                */













                /*
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

                            int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles


                            if (toremove[indexinlevelarray] == 1001) //ADJACENT TILE BUILD WALL TILES LEFT/RIGHT/FRONT/BACK/DIAGLEFT/DIAGRIGHT/DIAGFRONT/DIAGBACK/ OUT OF THAT
                            {
                                //Console.WriteLine("tile adjacent");
                                buildsomefinalfloortiles(x, y, z); //,xx,yy,zz, indexinlevelarray
                                //levelmap[indexinlevelarray] = 1;
                            }
                        }
                    }
                }*/





                /*
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

                            int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                            if (toremove[indexinlevelarray] != 1101 ||
                              toremove[indexinlevelarray] != 1102 ||
                              toremove[indexinlevelarray] != 1103 ||
                              toremove[indexinlevelarray] != 1104 ||
                              toremove[indexinlevelarray] != 1105 ||
                              toremove[indexinlevelarray] != 1106 ||
                              toremove[indexinlevelarray] != 1107 ||
                              toremove[indexinlevelarray] != 1108 ||
                              toremove[indexinlevelarray] != 1109 ||
                              toremove[indexinlevelarray] != 1110 ||
                              toremove[indexinlevelarray] != 1111 ||
                              toremove[indexinlevelarray] != 1112 ||
                              toremove[indexinlevelarray] != 998)
                            {
                                if (toremove[indexinlevelarray] >= 0 && toremove[indexinlevelarray] != 995 && toremove[indexinlevelarray] != 999 )
                                {
                                    //Console.WriteLine(toremove[indexinlevelarray]);
                                    //levelmap[indexinlevelarray] = 0;
                                    buildsomefinalfloortiles(x, y, z);

                                }
                            }
                        }
                    }
                }*/



                /*
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

                            int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                            if (levelmap[indexinlevelarray] == 1001)
                            {
                                levelmap[indexinlevelarray] = 1;
                                //levelmapsortingtiles[indexinlevelarray] = 0;
                            }
                        }
                    }
                }
                */

























                /*
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

                            int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                            if (toremove[indexinlevelarray] == 1001)
                            {
                                levelmap[indexinlevelarray] = 0;
                                levelmapsortingtiles[indexinlevelarray] = 0;
                            }
                        }
                    }
                }*/


                /*

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

                            int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles


                            if (toremove[indexinlevelarray] == 1001) //ADJACENT TILE BUILD WALL TILES LEFT/RIGHT/FRONT/BACK/DIAGLEFT/DIAGRIGHT/DIAGFRONT/DIAGBACK/ OUT OF THAT
                            {
                                //Console.WriteLine("tile adjacent");
                                buildWallsRerolled(x, y, z); //,xx,yy,zz, indexinlevelarray
                                //levelmap[indexinlevelarray] = 1;
                            }
                        }
                    }
                }
                 */









                /*
                //REMOVING THE EASILY SORTABLE TILES FROM THE ADJACENTWALL LIST AND ADDING THEM TO THE typeoftiles LIST
                //REMOVING THE EASILY SORTABLE TILES FROM THE ADJACENTWALL LIST AND ADDING THEM TO THE typeoftiles LIST
                //REMOVING THE EASILY SORTABLE TILES FROM THE ADJACENTWALL LIST AND ADDING THEM TO THE typeoftiles LIST
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

                            int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles


                            if (levelmap[indexinlevelarray] == 1001) //ADJACENT TILE BUILD FLOOR TILES OUT OF THAT
                            {
                                //Console.WriteLine("tile adjacent");
                                buildsomefloortiles(x, y, z); //,xx,yy,zz, indexinlevelarray
                                //buildWallsRerolled(x, y, z); //,xx,yy,zz, indexinlevelarray
                            }
                        }
                    }
                }*/

















                /*
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

                            int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                            if (levelmap[indexinlevelarray] == 1101 ||
                               levelmap[indexinlevelarray] == 1102 ||
                               levelmap[indexinlevelarray] == 1103 ||
                               levelmap[indexinlevelarray] == 1104 ||
                               levelmap[indexinlevelarray] == 1105 ||
                               levelmap[indexinlevelarray] == 1106 ||
                               levelmap[indexinlevelarray] == 1107 ||
                               levelmap[indexinlevelarray] == 1108 ||
                               levelmap[indexinlevelarray] == 1109 ||
                               levelmap[indexinlevelarray] == 1110 ||
                               levelmap[indexinlevelarray] == 1111 ||
                               levelmap[indexinlevelarray] == 1112 ||
                               levelmapsortingtilesremains[indexinlevelarray] == 998)
                            {
                                toremove[indexinlevelarray] = 995;
                            }
                            else if (levelmap[indexinlevelarray] == 1001)
                            {
                                toremove[indexinlevelarray] = 1001;
                            }
                        }
                    }
                }*/


















                /*
                for (var i = 0; i < levelmap.Length; i++)
                {
                    toremove[i] = levelmap[i];
                }


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

                            int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                            if (levelmap[indexinlevelarray] == 1101 ||
                               levelmap[indexinlevelarray] == 1102 ||
                               levelmap[indexinlevelarray] == 1103 ||
                               levelmap[indexinlevelarray] == 1104 ||
                               levelmap[indexinlevelarray] == 1105 ||
                               levelmap[indexinlevelarray] == 1106 ||
                               levelmap[indexinlevelarray] == 1107 ||
                               levelmap[indexinlevelarray] == 1108 ||
                               levelmap[indexinlevelarray] == 1109 ||
                               levelmap[indexinlevelarray] == 1110 ||
                               levelmap[indexinlevelarray] == 1111 ||
                               levelmap[indexinlevelarray] == 1112 ||
                               levelmapsortingtilesremains[indexinlevelarray] == 998)
                            {
                                toremove[indexinlevelarray] = 995;
                            }
                        }
                    }
                }


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

                            int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                            if (toremove[indexinlevelarray] == 1001)
                            {
                                levelmap[indexinlevelarray] = 0;
                                levelmapsortingtiles[indexinlevelarray] = 0;
                            }
                        }
                    }
                }
                */









                /*
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

                            int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                            if (levelmapsortingtiles[indexinlevelarray] == 1001)
                            {
                                levelmap[indexinlevelarray] = 1;
                                //levelmapsortingtiles[indexinlevelarray] = 0;
                            }
                        }
                    }
                }*/



                /*
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

                            int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                            if (levelmap[indexinlevelarray] == 1001)
                            {
                                levelmap[indexinlevelarray] = 0;
                                levelmapsortingtiles[indexinlevelarray] = 0;
                            }
                        }
                    }
                }*/

                /*
                for (var i = 0; i < levelmap.Length; i++)
                {
                    toremove[i] = levelmap[i];
                }

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

                            int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                            if (toremove[indexinlevelarray] == 1001)
                            {
                                levelmap[indexinlevelarray] = 0;
                                levelmapsortingtiles[indexinlevelarray] = 0;
                            }
                        }
                    }
                }*/



























                /*
                //REMOVING THE EASILY SORTABLE TILES FROM THE ADJACENTWALL LIST AND ADDING THEM TO THE typeoftiles LIST
                //REMOVING THE EASILY SORTABLE TILES FROM THE ADJACENTWALL LIST AND ADDING THEM TO THE typeoftiles LIST
                //REMOVING THE EASILY SORTABLE TILES FROM THE ADJACENTWALL LIST AND ADDING THEM TO THE typeoftiles LIST
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

                            int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles


                            if (levelmap[indexinlevelarray] == 1001) //ADJACENT TILE BUILD FLOOR TILES OUT OF THAT
                            {
                                //Console.WriteLine("tile adjacent");
                                buildsomefloortiles(x, y, z); //,xx,yy,zz, indexinlevelarray
                            }
                        }
                    }
                }





                //REMOVING THE EASILY SORTABLE TILES FROM THE ADJACENTWALL LIST AND ADDING THEM TO THE typeoftiles LIST
                //REMOVING THE EASILY SORTABLE TILES FROM THE ADJACENTWALL LIST AND ADDING THEM TO THE typeoftiles LIST
                //REMOVING THE EASILY SORTABLE TILES FROM THE ADJACENTWALL LIST AND ADDING THEM TO THE typeoftiles LIST
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

                            int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles


                            if (levelmap[indexinlevelarray] == 1001)//ADJACENT TILE TAG TILES AS PROBABLE WALLS
                            {
                                //Console.WriteLine("tile adjacent");
                                buildsomewallremains(x, y, z); //,xx,yy,zz, indexinlevelarray
                            }
                        }
                    }
                }




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

                            int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles


                            if (levelmap[indexinlevelarray] == 1001) //ADJACENT TILE BUILD WALL TILES LEFT/RIGHT/FRONT/BACK/DIAGLEFT/DIAGRIGHT/DIAGFRONT/DIAGBACK/ OUT OF THAT
                            {
                                //Console.WriteLine("tile adjacent");
                                buildWallsRerolled(x, y, z); //,xx,yy,zz, indexinlevelarray
                            }
                        }
                    }
                }

                for (var i = 0; i < toremove.Length; i++)
                {
                    toremove[i] = levelmap[i];
                }


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

                            int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                            if (levelmap[indexinlevelarray] == 1101 ||
                               levelmap[indexinlevelarray] == 1102 ||
                               levelmap[indexinlevelarray] == 1103 ||
                               levelmap[indexinlevelarray] == 1104 ||
                               levelmap[indexinlevelarray] == 1105 ||
                               levelmap[indexinlevelarray] == 1106 ||
                               levelmap[indexinlevelarray] == 1107 ||
                               levelmap[indexinlevelarray] == 1108 ||
                               levelmap[indexinlevelarray] == 1109 ||
                               levelmap[indexinlevelarray] == 1110 ||
                               levelmap[indexinlevelarray] == 1111 ||
                               levelmap[indexinlevelarray] == 1112 ||
                               levelmapsortingtilesremains[indexinlevelarray] == 998)
                            {
                                toremove[indexinlevelarray] = 995;
                            }
                        }
                    }
                }

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

                            int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                            if (toremove[indexinlevelarray] == 1001)
                            {
                                //Console.WriteLine("tile adjacent");
                                //buildsomefloortiles(x, y, z); //,xx,yy,zz, indexinlevelarray
                                levelmap[indexinlevelarray] = 0;
                                levelmapsortingtiles[indexinlevelarray] = 0;
                                levelmapsortingtilesremains[indexinlevelarray] = 0;
                            }
                        }
                    }
                }



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

                            int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                            if (levelmap[indexinlevelarray] == 1001)
                            {
                                buildWallsRerolled(x, y, z); //,xx,yy,zz, indexinlevelarray
                            }
                        }
                    }
                }


                /*

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

                            int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                            if (toremove[indexinlevelarray] == 1001)
                            {
                                Console.WriteLine("x:" + xx + "/y:" + yy + "/z:" + zz);
                            }
                        }
                    }
                }*/





                /*
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

                            int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                            if (toremove[indexinlevelarray] == 1001)
                            {
                                levelmap[indexinlevelarray] = 1101;
                            }
                        }
                    }
                }*/


















                /*
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

                            int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                            if (toremove[indexinlevelarray] != 1101 ||
                              toremove[indexinlevelarray] != 1102 ||
                              toremove[indexinlevelarray] != 1103 ||
                              toremove[indexinlevelarray] != 1104 ||
                              toremove[indexinlevelarray] != 1105 ||
                              toremove[indexinlevelarray] != 1106 ||
                              toremove[indexinlevelarray] != 1107 ||
                              toremove[indexinlevelarray] != 1108 ||
                              toremove[indexinlevelarray] != 1109 ||
                              toremove[indexinlevelarray] != 1110 ||
                              toremove[indexinlevelarray] != 1111 ||
                              toremove[indexinlevelarray] != 1112 ||
                              toremove[indexinlevelarray] != 998)
                            {
                                if (toremove[indexinlevelarray] >= 0 && toremove[indexinlevelarray] != 995 && toremove[indexinlevelarray] != 999)
                                {
                                    Console.WriteLine(toremove[indexinlevelarray]);
                                }

                            }
                        }
                    }
                }*/








                /*
                for (var i = 0; i < levelmapsortingtilesremains.Length; i++)
                {
                    if (levelmapsortingtilesremains[i] == 998)
                    {
                        levelmapsortingtiles[i] = levelmapsortingtilesremains[i];
                    }
                }*/




                /*
               */



                /*
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

                            int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                            if (levelmapsortingtilesremains[indexinlevelarray] == 998)
                            {
                                levelmap[indexinlevelarray] = 1;
                            }
                        }
                    }
                }*/


                /*
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

                            int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles


                            if (levelmapsortingtilesremains[indexinlevelarray] == 998 || toremove[indexinlevelarray] ==1001) //ADJACENT TILE BUILD WALL TILES LEFT/RIGHT/FRONT/BACK/DIAGLEFT/DIAGRIGHT/DIAGFRONT/DIAGBACK/ OUT OF THAT
                            {
                                //Console.WriteLine("tile adjacent");
                                buildWallsRerolled(x, y, z); //,xx,yy,zz, indexinlevelarray
                                //levelmap[indexinlevelarray] = 1101;
                            }
                        }
                    }
                }
                */








                /*




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

                            int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                            if (toremove[indexinlevelarray] == 1001)
                            {
                                //Console.WriteLine("tile adjacent");
                                //buildsomefloortiles(x, y, z); //,xx,yy,zz, indexinlevelarray
                                levelmap[indexinlevelarray] = 0;
                                levelmapsortingtiles[indexinlevelarray] = 0;
                            }
                        }
                    }
                }


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

                            int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles


                            if (levelmapsortingtilesremains[indexinlevelarray] == 998) //ADJACENT TILES TAGGED AS PROBABLE WALLS CAN NOW BUILD WALL TILES OUT OF THAT
                            {
                                //Console.WriteLine("tile adjacent");
                                buildWallsRerolled(x, y, z); //,xx,yy,zz, indexinlevelarray
                            }
                        }
                    }
                }







                /*
                //UNKNOWN TYPE OF TILES
                //UNKNOWN TYPE OF TILES
                //UNKNOWN TYPE OF TILES
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

                            int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles


                            if (levelmap[indexinlevelarray] == 1002) //ADJACENT TILES TAGGED AS UNKNOWN PATTERN (COULDNT FIND THE PATTERN YET) BUILD AS UNKNOWN TILES WITH FULL BYTES
                            {
                                levelmap[indexinlevelarray] = -2; // -2
                                //Console.WriteLine("tile adjacent");
                                //buildWallsRerolled(x, y, z); //,xx,yy,zz, indexinlevelarray
                            }
                        }
                    }
                }*/
                //UNKNOWN TYPE OF TILES
                //UNKNOWN TYPE OF TILES
                //UNKNOWN TYPE OF TILES
































                /*
                Dictionary<int[], int> topwalllayer = new Dictionary<int[], int>();

                var enumerator0 = typeoftiles.GetEnumerator();
                //Vector3? tls0 = null;     
                while (enumerator0.MoveNext())
                {
                    var tls0 = enumerator0.Current;
                    var somekey = tls0.Key;
                    var someval = tls0.Value;

                    if (someval != 0) //&& someval != 16
                    {
                        for (int tw = 1; tw <= wallheightsize - 1; tw++)
                        {
                            arrayofcoords = new int[3];
                            arrayofcoords[0] = (somekey[0]);
                            arrayofcoords[1] = (somekey[1]) + tw;
                            arrayofcoords[2] = (somekey[2]);

                            if (!topwalllayer.Keys.Any(key => key.SequenceEqual(arrayofcoords)))
                            {
                                topwalllayer.Add(arrayofcoords, someval);
                            }
                        }
                    }
                }



                var enumerator1 = topwalllayer.GetEnumerator();
                //Vector3? tls0 = null;     
                while (enumerator1.MoveNext())
                {
                    var tls0 = enumerator1.Current;
                    var somekey = tls0.Key;
                    var someval = tls0.Value;

                    int countofarray = typeoftiles.Count;
                    if (!typeoftiles.Keys.Any(key => key.SequenceEqual(arrayofcoords)))
                    {
                        int xx = (somekey[0]);
                        int yy = (somekey[1]);
                        int zz = (somekey[2]);

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

                        int indexinarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                        levelmap[indexinarray] = countofarray;


                        typeoftiles.Add(somekey, someval);
                        //leftWall.Add(currentTile);
                        //buildWallLeft();
                        //forsortingtiles.Remove(currentTile);
                    }
                }


                //TOP ROOF IS INVERTED FLOOR
                //TOP ROOF IS INVERTED FLOOR
                //TOP ROOF IS INVERTED FLOOR
                Dictionary<int[], int> toprooflayer = new Dictionary<int[], int>();

                var enumerator2 = typeoftiles.GetEnumerator();
                //Vector3? tls0 = null;     
                while (enumerator2.MoveNext())
                {
                    var tls0 = enumerator2.Current;
                    var somekey = tls0.Key;
                    var someval = tls0.Value;

                    if (someval == 0)
                    {
                        arrayofcoords = new int[3];
                        arrayofcoords[0] = (somekey[0]);
                        arrayofcoords[1] = (somekey[1]) + wallheightsize - 1;
                        arrayofcoords[2] = (somekey[2]);

                        if (!toprooflayer.Keys.Any(key => key.SequenceEqual(arrayofcoords)))
                        {
                            toprooflayer.Add(arrayofcoords, someval);
                        }
                    }
                }

                var enumerator3 = toprooflayer.GetEnumerator();
                //Vector3? tls0 = null;     
                while (enumerator3.MoveNext())
                {
                    var tls0 = enumerator3.Current;
                    var somekey = tls0.Key;
                    var someval = tls0.Value;

                    int countofarray = typeoftiles.Count;
                    if (!typeoftiles.Keys.Any(key => key.SequenceEqual(arrayofcoords)))
                    {

                        int xx = (somekey[0]);
                        int yy = (somekey[1]);
                        int zz = (somekey[2]);

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

                        int indexinarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                        levelmap[indexinarray] = countofarray;

                        typeoftiles.Add(somekey, 15);
                        //leftWall.Add(currentTile);
                        //buildWallLeft();
                        //forsortingtiles.Remove(currentTile);
                    }
                }
                //TOP ROOF IS INVERTED FLOOR
                //TOP ROOF IS INVERTED FLOOR
                //TOP ROOF IS INVERTED FLOOR
                */

                counter = 2;
            }
        }








        void buildsomefloortiles(int x, int y, int z) //, int xx, int yy, int zz, int mainindexofintinmap
        {
            //Console.WriteLine("testing function");
            istypeofl = -2;
            istypeofr = -2;
            istypeoft = -2;
            istypeofb = -2;

            istypeoflt = -2;
            istypeofrt = -2;
            istypeoflb = -2;
            istypeofrb = -2;

            int tilex = x - 1;
            int tiley = y;
            int tilez = z;

            int xii = tilex;
            int zii = tilez;

            if (xii < 0)
            {
                xii *= -1;
                xii = xii + (maxx - 1);
            }
            if (zii < 0)
            {
                zii *= -1;
                zii = zii + (maxz - 1);
            }
            int indextile = xii + somewidth * (y + someheight * zii);

            if (indextile < somewidth * someheight * somedepth)
            {
                if (levelmapsortingtiles[indextile] == 0)
                {
                    istypeofl = 0;
                }
                else if (levelmapsortingtiles[indextile] == 998)
                {
                    istypeofl = 1;
                }
                else
                {
                    istypeofl = -1;
                }
            }
            else
            {
                istypeofl = -1;
            }


            tilex = x + 1;
            tiley = y;
            tilez = z;

            xii = tilex;
            zii = tilez;

            if (xii < 0)
            {
                xii *= -1;
                xii = xii + (maxx - 1);
            }
            if (zii < 0)
            {
                zii *= -1;
                zii = zii + (maxz - 1);
            }
            indextile = xii + somewidth * (y + someheight * zii);

            if (indextile < somewidth * someheight * somedepth)
            {
                if (levelmapsortingtiles[indextile] == 0)
                {
                    istypeofr = 0;
                }
                else if (levelmapsortingtiles[indextile] == 998)
                {
                    istypeofr = 1;
                }
                else
                {
                    istypeofr = -1;
                }
            }
            else
            {
                istypeofr = -1;
            }


            tilex = x;
            tiley = y;
            tilez = z + 1;


            xii = tilex;
            zii = tilez;

            if (xii < 0)
            {
                xii *= -1;
                xii = xii + (maxx - 1);
            }
            if (zii < 0)
            {
                zii *= -1;
                zii = zii + (maxz - 1);
            }
            indextile = xii + somewidth * (y + someheight * zii);

            if (indextile < somewidth * someheight * somedepth)
            {
                if (levelmapsortingtiles[indextile] == 0)
                {
                    istypeoft = 0;
                }
                else if (levelmapsortingtiles[indextile] == 998)
                {
                    istypeoft = 1;
                }
                else
                {
                    istypeoft = -1;
                }
            }
            else
            {
                istypeoft = -1;
            }



            tilex = x;
            tiley = y;
            tilez = z - 1;

            xii = tilex;
            zii = tilez;

            if (xii < 0)
            {
                xii *= -1;
                xii = xii + (maxx - 1);
            }
            if (zii < 0)
            {
                zii *= -1;
                zii = zii + (maxz - 1);
            }

            indextile = xii + somewidth * (y + someheight * zii);

            if (indextile < somewidth * someheight * somedepth)
            {
                if (levelmapsortingtiles[indextile] == 0)
                {
                    istypeofb = 0;
                }
                else if (levelmapsortingtiles[indextile] == 998)
                {
                    istypeofb = 1;
                }
                else
                {
                    istypeofb = -1;
                }
            }
            else
            {
                istypeofb = -1;
            }
            /////LEFT RIGHT FRONT BACK



            tilex = x - 1;
            tiley = y;
            tilez = z - 1;


            xii = tilex;
            zii = tilez;

            if (xii < 0)
            {
                xii *= -1;
                xii = xii + (maxx - 1);
            }
            if (zii < 0)
            {
                zii *= -1;
                zii = zii + (maxz - 1);
            }
            indextile = xii + somewidth * (y + someheight * zii);

            if (indextile < somewidth * someheight * somedepth)
            {
                if (levelmapsortingtiles[indextile] == 0)
                {
                    istypeoflb = 0;
                }
                else if (levelmapsortingtiles[indextile] == 998)
                {
                    istypeoflb = 1;
                }
                else
                {
                    istypeoflb = -1;
                }
            }
            else
            {
                istypeoflb = -1;
            }



            tilex = x + 1;
            tiley = y;
            tilez = z - 1;


            xii = tilex;
            zii = tilez;

            if (xii < 0)
            {
                xii *= -1;
                xii = xii + (maxx - 1);
            }
            if (zii < 0)
            {
                zii *= -1;
                zii = zii + (maxz - 1);
            }
            indextile = xii + somewidth * (y + someheight * zii);

            if (indextile < somewidth * someheight * somedepth)
            {
                if (levelmapsortingtiles[indextile] == 0)
                {
                    istypeofrb = 0;
                }
                else if (levelmapsortingtiles[indextile] == 998)
                {
                    istypeofrb = 1;
                }
                else
                {
                    istypeofrb = -1;
                }
            }
            else
            {
                istypeofrb = -1;
            }


            tilex = x - 1;
            tiley = y;
            tilez = z + 1;


            xii = tilex;
            zii = tilez;

            if (xii < 0)
            {
                xii *= -1;
                xii = xii + (maxx - 1);
            }
            if (zii < 0)
            {
                zii *= -1;
                zii = zii + (maxz - 1);
            }
            indextile = xii + somewidth * (y + someheight * zii);

            if (indextile < somewidth * someheight * somedepth)
            {
                if (levelmapsortingtiles[indextile] == 0)
                {
                    istypeoflt = 0;
                }
                else if (levelmapsortingtiles[indextile] == 998)
                {
                    istypeoflt = 1;
                }
                else
                {
                    istypeoflt = -1;
                }
            }
            else
            {
                istypeoflt = -1;
            }



            tilex = x + 1;
            tiley = y;
            tilez = z + 1;


            xii = tilex;
            zii = tilez;

            if (xii < 0)
            {
                xii *= -1;
                xii = xii + (maxx - 1);
            }
            if (zii < 0)
            {
                zii *= -1;
                zii = zii + (maxz - 1);
            }
            indextile = xii + somewidth * (y + someheight * zii);

            if (indextile < somewidth * someheight * somedepth)
            {
                if (levelmapsortingtiles[indextile] == 0)
                {
                    istypeofrt = 0;
                }
                else if (levelmapsortingtiles[indextile] == 998)
                {
                    istypeofrt = 1;
                }
                else
                {
                    istypeofrt = -1;
                }
            }
            else
            {
                istypeofrt = -1;
            }


            //walls to the right
            /////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 0 &&  /*/////////////*/ istypeofr == 0 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0)
            {
                //int countofarray = typeoftiles.Count;
                //Console.WriteLine("sometest");
                //if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {

                    int xx = (x);
                    int yy = (y);
                    int zz = (z);

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

                    int indexinarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                    levelmap[indexinarray] = 0;
                    levelmapsortingtiles[indexinarray] = 996;

                    //Console.WriteLine("test");
                    /*typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);*/
                }
            }
        }













        void buildsomefinalfloortiles(int x, int y, int z) //, int xx, int yy, int zz, int mainindexofintinmap
        {
            //Console.WriteLine("testing function");
            istypeofl = -2;
            istypeofr = -2;
            istypeoft = -2;
            istypeofb = -2;

            istypeoflt = -2;
            istypeofrt = -2;
            istypeoflb = -2;
            istypeofrb = -2;

            int tilex = x - 1;
            int tiley = y;
            int tilez = z;

            int xii = tilex;
            int zii = tilez;

            if (xii < 0)
            {
                xii *= -1;
                xii = xii + (maxx - 1);
            }
            if (zii < 0)
            {
                zii *= -1;
                zii = zii + (maxz - 1);
            }
            int indextile = xii + somewidth * (y + someheight * zii);

            if (indextile < somewidth * someheight * somedepth)
            {
                if (levelmapsortingtiles[indextile] == 0)
                {
                    istypeofl = 0;
                }
                else if (levelmapsortingtiles[indextile] == 998)
                {
                    istypeofl = 1;
                }
                else
                {
                    istypeofl = -1;
                }
            }
            else
            {
                istypeofl = -1;
            }


            tilex = x + 1;
            tiley = y;
            tilez = z;

            xii = tilex;
            zii = tilez;

            if (xii < 0)
            {
                xii *= -1;
                xii = xii + (maxx - 1);
            }
            if (zii < 0)
            {
                zii *= -1;
                zii = zii + (maxz - 1);
            }
            indextile = xii + somewidth * (y + someheight * zii);

            if (indextile < somewidth * someheight * somedepth)
            {
                if (levelmapsortingtiles[indextile] == 0)
                {
                    istypeofr = 0;
                }
                else if (levelmapsortingtiles[indextile] == 998)
                {
                    istypeofr = 1;
                }
                else
                {
                    istypeofr = -1;
                }
            }
            else
            {
                istypeofr = -1;
            }


            tilex = x;
            tiley = y;
            tilez = z + 1;


            xii = tilex;
            zii = tilez;

            if (xii < 0)
            {
                xii *= -1;
                xii = xii + (maxx - 1);
            }
            if (zii < 0)
            {
                zii *= -1;
                zii = zii + (maxz - 1);
            }
            indextile = xii + somewidth * (y + someheight * zii);

            if (indextile < somewidth * someheight * somedepth)
            {
                if (levelmapsortingtiles[indextile] == 0)
                {
                    istypeoft = 0;
                }
                else if (levelmapsortingtiles[indextile] == 998)
                {
                    istypeoft = 1;
                }
                else
                {
                    istypeoft = -1;
                }
            }
            else
            {
                istypeoft = -1;
            }



            tilex = x;
            tiley = y;
            tilez = z - 1;

            xii = tilex;
            zii = tilez;

            if (xii < 0)
            {
                xii *= -1;
                xii = xii + (maxx - 1);
            }
            if (zii < 0)
            {
                zii *= -1;
                zii = zii + (maxz - 1);
            }

            indextile = xii + somewidth * (y + someheight * zii);

            if (indextile < somewidth * someheight * somedepth)
            {
                if (levelmapsortingtiles[indextile] == 0)
                {
                    istypeofb = 0;
                }
                else if (levelmapsortingtiles[indextile] == 998)
                {
                    istypeofb = 1;
                }
                else
                {
                    istypeofb = -1;
                }
            }
            else
            {
                istypeofb = -1;
            }
            /////LEFT RIGHT FRONT BACK



            tilex = x - 1;
            tiley = y;
            tilez = z - 1;


            xii = tilex;
            zii = tilez;

            if (xii < 0)
            {
                xii *= -1;
                xii = xii + (maxx - 1);
            }
            if (zii < 0)
            {
                zii *= -1;
                zii = zii + (maxz - 1);
            }
            indextile = xii + somewidth * (y + someheight * zii);

            if (indextile < somewidth * someheight * somedepth)
            {
                if (levelmapsortingtiles[indextile] == 0)
                {
                    istypeoflb = 0;
                }
                else if (levelmapsortingtiles[indextile] == 998)
                {
                    istypeoflb = 1;
                }
                else
                {
                    istypeoflb = -1;
                }
            }
            else
            {
                istypeoflb = -1;
            }



            tilex = x + 1;
            tiley = y;
            tilez = z - 1;


            xii = tilex;
            zii = tilez;

            if (xii < 0)
            {
                xii *= -1;
                xii = xii + (maxx - 1);
            }
            if (zii < 0)
            {
                zii *= -1;
                zii = zii + (maxz - 1);
            }
            indextile = xii + somewidth * (y + someheight * zii);

            if (indextile < somewidth * someheight * somedepth)
            {
                if (levelmapsortingtiles[indextile] == 0)
                {
                    istypeofrb = 0;
                }
                else if (levelmapsortingtiles[indextile] == 998)
                {
                    istypeofrb = 1;
                }
                else
                {
                    istypeofrb = -1;
                }
            }
            else
            {
                istypeofrb = -1;
            }


            tilex = x - 1;
            tiley = y;
            tilez = z + 1;


            xii = tilex;
            zii = tilez;

            if (xii < 0)
            {
                xii *= -1;
                xii = xii + (maxx - 1);
            }
            if (zii < 0)
            {
                zii *= -1;
                zii = zii + (maxz - 1);
            }
            indextile = xii + somewidth * (y + someheight * zii);

            if (indextile < somewidth * someheight * somedepth)
            {
                if (levelmapsortingtiles[indextile] == 0)
                {
                    istypeoflt = 0;
                }
                else if (levelmapsortingtiles[indextile] == 998)
                {
                    istypeoflt = 1;
                }
                else
                {
                    istypeoflt = -1;
                }
            }
            else
            {
                istypeoflt = -1;
            }



            tilex = x + 1;
            tiley = y;
            tilez = z + 1;


            xii = tilex;
            zii = tilez;

            if (xii < 0)
            {
                xii *= -1;
                xii = xii + (maxx - 1);
            }
            if (zii < 0)
            {
                zii *= -1;
                zii = zii + (maxz - 1);
            }
            indextile = xii + somewidth * (y + someheight * zii);

            if (indextile < somewidth * someheight * somedepth)
            {
                if (levelmapsortingtiles[indextile] == 0)
                {
                    istypeofrt = 0;
                }
                else if (levelmapsortingtiles[indextile] == 998)
                {
                    istypeofrt = 1;
                }
                else
                {
                    istypeofrt = -1;
                }
            }
            else
            {
                istypeofrt = -1;
            }


            /////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 0 &&  /*/////////////*/ istypeofr == 0 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0)
            {
                //int countofarray = typeoftiles.Count;
                //Console.WriteLine("sometest");
                //if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {

                    int xx = (x);
                    int yy = (y);
                    int zz = (z);

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

                    int indexinarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles
                    
                    if (levelmap[indexinarray] != 1101 &&
                      levelmap[indexinarray] != 1102 &&
                      levelmap[indexinarray] != 1103 &&
                      levelmap[indexinarray] != 1104 &&
                      levelmap[indexinarray] != 1105 &&
                      levelmap[indexinarray] != 1106 &&
                      levelmap[indexinarray] != 1107 &&
                      levelmap[indexinarray] != 1108 &&
                      levelmap[indexinarray] != 1109 &&
                      levelmap[indexinarray] != 1110 &&
                      levelmap[indexinarray] != 1111 &&
                      levelmap[indexinarray] != 1112 &&
                      levelmap[indexinarray] != 0)
                    {
                        levelmap[indexinarray] = 0;
                        levelmapsortingtiles[indexinarray] = 996;
                    }
                    

                    //Console.WriteLine("test");
                    /*typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);*/
                }

                /////////////////////////////////////////////////////////////
                /*if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
                    istypeofl == -1 &&   istypeofr == 0 &&
                    istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0)
                {
                    //int countofarray = typeoftiles.Count;
                    //Console.WriteLine("sometest");
                    //if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                    {

                        int xx = (x);
                        int yy = (y);
                        int zz = (z);

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

                        int indexinarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                        levelmap[indexinarray] = 0;
                        levelmapsortingtiles[indexinarray] = 996;

                        
                    }
                }
                /////////////////////////////////////////////////////////////
                if (istypeoflt == 0 && istypeoft == -1 && istypeofrt == 0 &&
                    istypeofl == 0 &&   istypeofr == 0 &&
                    istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0)
                {
                    //int countofarray = typeoftiles.Count;
                    //Console.WriteLine("sometest");
                    //if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                    {

                        int xx = (x);
                        int yy = (y);
                        int zz = (z);

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

                        int indexinarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                        levelmap[indexinarray] = 0;
                        levelmapsortingtiles[indexinarray] = 996;

                  
                    }
                }

                /////////////////////////////////////////////////////////////
                if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
                    istypeofl == 0 &&   istypeofr == -1 &&
                    istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0)
                {
                    //int countofarray = typeoftiles.Count;
                    //Console.WriteLine("sometest");
                    //if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                    {

                        int xx = (x);
                        int yy = (y);
                        int zz = (z);

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

                        int indexinarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                        levelmap[indexinarray] = 0;
                        levelmapsortingtiles[indexinarray] = 996;

                        
                    }
                }

                /////////////////////////////////////////////////////////////
                if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
                    istypeofl == 0 &&   istypeofr == 0 &&
                    istypeoflb == 0 && istypeofb == -1 && istypeofrb == 0)
                {
                    //int countofarray = typeoftiles.Count;
                    //Console.WriteLine("sometest");
                    //if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                    {

                        int xx = (x);
                        int yy = (y);
                        int zz = (z);

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

                        int indexinarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                        levelmap[indexinarray] = 0;
                        levelmapsortingtiles[indexinarray] = 996;

                      
                    }
                }*/

            }
        }


















        void buildsomewallremains(int x, int y, int z)
        {
            //Console.WriteLine("testing function");
            istypeofl = -2;
            istypeofr = -2;
            istypeoft = -2;
            istypeofb = -2;

            istypeoflt = -2;
            istypeofrt = -2;
            istypeoflb = -2;
            istypeofrb = -2;



            int tilex = x - 1;
            int tiley = y;
            int tilez = z;

            int xii = tilex;
            int zii = tilez;

            if (xii < 0)
            {
                xii *= -1;
                xii = xii + (maxx - 1);
            }
            if (zii < 0)
            {
                zii *= -1;
                zii = zii + (maxz - 1);
            }
            int indextile = xii + somewidth * (y + someheight * zii);

            if (indextile < somewidth * someheight * somedepth)
            {
                if (levelmapsortingtiles[indextile] == 0)
                {
                    istypeofl = 0;
                }
                else if (levelmapsortingtiles[indextile] == 998)
                {
                    istypeofl = 1;
                }
                else
                {
                    istypeofl = -1;
                }
            }
            else
            {
                istypeofl = -1;
            }


            tilex = x + 1;
            tiley = y;
            tilez = z;

            xii = tilex;
            zii = tilez;

            if (xii < 0)
            {
                xii *= -1;
                xii = xii + (maxx - 1);
            }
            if (zii < 0)
            {
                zii *= -1;
                zii = zii + (maxz - 1);
            }
            indextile = xii + somewidth * (y + someheight * zii);

            if (indextile < somewidth * someheight * somedepth)
            {
                if (levelmapsortingtiles[indextile] == 0)
                {
                    istypeofr = 0;
                }
                else if (levelmapsortingtiles[indextile] == 998)
                {
                    istypeofr = 1;
                }
                else
                {
                    istypeofr = -1;
                }
            }
            else
            {
                istypeofr = -1;
            }


            tilex = x;
            tiley = y;
            tilez = z + 1;


            xii = tilex;
            zii = tilez;

            if (xii < 0)
            {
                xii *= -1;
                xii = xii + (maxx - 1);
            }
            if (zii < 0)
            {
                zii *= -1;
                zii = zii + (maxz - 1);
            }
            indextile = xii + somewidth * (y + someheight * zii);

            if (indextile < somewidth * someheight * somedepth)
            {
                if (levelmapsortingtiles[indextile] == 0)
                {
                    istypeoft = 0;
                }
                else if (levelmapsortingtiles[indextile] == 998)
                {
                    istypeoft = 1;
                }
                else
                {
                    istypeoft = -1;
                }
            }
            else
            {
                istypeoft = -1;
            }



            tilex = x;
            tiley = y;
            tilez = z - 1;

            xii = tilex;
            zii = tilez;

            if (xii < 0)
            {
                xii *= -1;
                xii = xii + (maxx - 1);
            }
            if (zii < 0)
            {
                zii *= -1;
                zii = zii + (maxz - 1);
            }

            indextile = xii + somewidth * (y + someheight * zii);

            if (indextile < somewidth * someheight * somedepth)
            {
                if (levelmapsortingtiles[indextile] == 0)
                {
                    istypeofb = 0;
                }
                else if (levelmapsortingtiles[indextile] == 998)
                {
                    istypeofb = 1;
                }
                else
                {
                    istypeofb = -1;
                }
            }
            else
            {
                istypeofb = -1;
            }
            /////LEFT RIGHT FRONT BACK



            tilex = x - 1;
            tiley = y;
            tilez = z - 1;


            xii = tilex;
            zii = tilez;

            if (xii < 0)
            {
                xii *= -1;
                xii = xii + (maxx - 1);
            }
            if (zii < 0)
            {
                zii *= -1;
                zii = zii + (maxz - 1);
            }
            indextile = xii + somewidth * (y + someheight * zii);

            if (indextile < somewidth * someheight * somedepth)
            {
                if (levelmapsortingtiles[indextile] == 0)
                {
                    istypeoflb = 0;
                }
                else if (levelmapsortingtiles[indextile] == 998)
                {
                    istypeoflb = 1;
                }
                else
                {
                    istypeoflb = -1;
                }
            }
            else
            {
                istypeoflb = -1;
            }



            tilex = x + 1;
            tiley = y;
            tilez = z - 1;


            xii = tilex;
            zii = tilez;

            if (xii < 0)
            {
                xii *= -1;
                xii = xii + (maxx - 1);
            }
            if (zii < 0)
            {
                zii *= -1;
                zii = zii + (maxz - 1);
            }
            indextile = xii + somewidth * (y + someheight * zii);

            if (indextile < somewidth * someheight * somedepth)
            {
                if (levelmapsortingtiles[indextile] == 0)
                {
                    istypeofrb = 0;
                }
                else if (levelmapsortingtiles[indextile] == 998)
                {
                    istypeofrb = 1;
                }
                else
                {
                    istypeofrb = -1;
                }
            }
            else
            {
                istypeofrb = -1;
            }


            tilex = x - 1;
            tiley = y;
            tilez = z + 1;


            xii = tilex;
            zii = tilez;

            if (xii < 0)
            {
                xii *= -1;
                xii = xii + (maxx - 1);
            }
            if (zii < 0)
            {
                zii *= -1;
                zii = zii + (maxz - 1);
            }
            indextile = xii + somewidth * (y + someheight * zii);

            if (indextile < somewidth * someheight * somedepth)
            {
                if (levelmapsortingtiles[indextile] == 0)
                {
                    istypeoflt = 0;
                }
                else if (levelmapsortingtiles[indextile] == 998)
                {
                    istypeoflt = 1;
                }
                else
                {
                    istypeoflt = -1;
                }
            }
            else
            {
                istypeoflt = -1;
            }



            tilex = x + 1;
            tiley = y;
            tilez = z + 1;


            xii = tilex;
            zii = tilez;

            if (xii < 0)
            {
                xii *= -1;
                xii = xii + (maxx - 1);
            }
            if (zii < 0)
            {
                zii *= -1;
                zii = zii + (maxz - 1);
            }
            indextile = xii + somewidth * (y + someheight * zii);

            if (indextile < somewidth * someheight * somedepth)
            {
                if (levelmapsortingtiles[indextile] == 0)
                {
                    istypeofrt = 0;
                }
                else if (levelmapsortingtiles[indextile] == 998)
                {
                    istypeofrt = 1;
                }
                else
                {
                    istypeofrt = -1;
                }
            }
            else
            {
                istypeofrt = -1;
            }




            //walls to the right
            /////////////////////////////////////////////////////////////
            if (istypeoflt == -1 || istypeoft == -1 || istypeofrt == -1 ||
                istypeofl == -1 ||  /*/////////////*/ istypeofr == -1 ||
                istypeoflb == -1 || istypeofb == -1 || istypeofrb == -1)
            {
                //int countofarray = typeoftiles.Count;
                //Console.WriteLine("sometest");
                //if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {

                    int xx = (x);
                    int yy = (y);
                    int zz = (z);

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

                    int indexinarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                    levelmapsortingtilesremains[indexinarray] = 998;
                    levelmapsortingtiles[indexinarray] = 998;
                    //Console.WriteLine("test");
                    /*typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);*/
                }
            }



        }










        public void buildWallsRerolled(int x, int y, int z)
        {




            //Console.WriteLine("testing function");
            istypeofl = -2;
            istypeofr = -2;
            istypeoft = -2;
            istypeofb = -2;

            istypeoflt = -2;
            istypeofrt = -2;
            istypeoflb = -2;
            istypeofrb = -2;


            int tilex = x - 1;
            int tiley = y;
            int tilez = z;

            int xii = tilex;
            int zii = tilez;

            if (xii < 0)
            {
                xii *= -1;
                xii = xii + (maxx - 1);
            }
            if (zii < 0)
            {
                zii *= -1;
                zii = zii + (maxz - 1);
            }
            int indextile = xii + somewidth * (y + someheight * zii);

            if (indextile < somewidth * someheight * somedepth)
            {
                if (levelmapsortingtiles[indextile] == 0)
                {
                    istypeofl = 0;
                }
                else if (levelmapsortingtiles[indextile] == 998)
                {
                    istypeofl = 1;
                }
                else
                {
                    istypeofl = -1;
                }
            }
            else
            {
                istypeofl = -1;
            }


            tilex = x + 1;
            tiley = y;
            tilez = z;

            xii = tilex;
            zii = tilez;

            if (xii < 0)
            {
                xii *= -1;
                xii = xii + (maxx - 1);
            }
            if (zii < 0)
            {
                zii *= -1;
                zii = zii + (maxz - 1);
            }
            indextile = xii + somewidth * (y + someheight * zii);

            if (indextile < somewidth * someheight * somedepth)
            {
                if (levelmapsortingtiles[indextile] == 0)
                {
                    istypeofr = 0;
                }
                else if (levelmapsortingtiles[indextile] == 998)
                {
                    istypeofr = 1;
                }
                else
                {
                    istypeofr = -1;
                }
            }
            else
            {
                istypeofr = -1;
            }


            tilex = x;
            tiley = y;
            tilez = z + 1;


            xii = tilex;
            zii = tilez;

            if (xii < 0)
            {
                xii *= -1;
                xii = xii + (maxx - 1);
            }
            if (zii < 0)
            {
                zii *= -1;
                zii = zii + (maxz - 1);
            }
            indextile = xii + somewidth * (y + someheight * zii);

            if (indextile < somewidth * someheight * somedepth)
            {
                if (levelmapsortingtiles[indextile] == 0)
                {
                    istypeoft = 0;
                }
                else if (levelmapsortingtiles[indextile] == 998)
                {
                    istypeoft = 1;
                }
                else
                {
                    istypeoft = -1;
                }
            }
            else
            {
                istypeoft = -1;
            }



            tilex = x;
            tiley = y;
            tilez = z - 1;

            xii = tilex;
            zii = tilez;

            if (xii < 0)
            {
                xii *= -1;
                xii = xii + (maxx - 1);
            }
            if (zii < 0)
            {
                zii *= -1;
                zii = zii + (maxz - 1);
            }

            indextile = xii + somewidth * (y + someheight * zii);

            if (indextile < somewidth * someheight * somedepth)
            {
                if (levelmapsortingtiles[indextile] == 0)
                {
                    istypeofb = 0;
                }
                else if (levelmapsortingtiles[indextile] == 998)
                {
                    istypeofb = 1;
                }
                else
                {
                    istypeofb = -1;
                }
            }
            else
            {
                istypeofb = -1;
            }
            /////LEFT RIGHT FRONT BACK



            tilex = x - 1;
            tiley = y;
            tilez = z - 1;


            xii = tilex;
            zii = tilez;

            if (xii < 0)
            {
                xii *= -1;
                xii = xii + (maxx - 1);
            }
            if (zii < 0)
            {
                zii *= -1;
                zii = zii + (maxz - 1);
            }
            indextile = xii + somewidth * (y + someheight * zii);

            if (indextile < somewidth * someheight * somedepth)
            {
                if (levelmapsortingtiles[indextile] == 0)
                {
                    istypeoflb = 0;
                }
                else if (levelmapsortingtiles[indextile] == 998)
                {
                    istypeoflb = 1;
                }
                else
                {
                    istypeoflb = -1;
                }
            }
            else
            {
                istypeoflb = -1;
            }



            tilex = x + 1;
            tiley = y;
            tilez = z - 1;


            xii = tilex;
            zii = tilez;

            if (xii < 0)
            {
                xii *= -1;
                xii = xii + (maxx - 1);
            }
            if (zii < 0)
            {
                zii *= -1;
                zii = zii + (maxz - 1);
            }
            indextile = xii + somewidth * (y + someheight * zii);

            if (indextile < somewidth * someheight * somedepth)
            {
                if (levelmapsortingtiles[indextile] == 0)
                {
                    istypeofrb = 0;
                }
                else if (levelmapsortingtiles[indextile] == 998)
                {
                    istypeofrb = 1;
                }
                else
                {
                    istypeofrb = -1;
                }
            }
            else
            {
                istypeofrb = -1;
            }


            tilex = x - 1;
            tiley = y;
            tilez = z + 1;


            xii = tilex;
            zii = tilez;

            if (xii < 0)
            {
                xii *= -1;
                xii = xii + (maxx - 1);
            }
            if (zii < 0)
            {
                zii *= -1;
                zii = zii + (maxz - 1);
            }
            indextile = xii + somewidth * (y + someheight * zii);

            if (indextile < somewidth * someheight * somedepth)
            {
                if (levelmapsortingtiles[indextile] == 0)
                {
                    istypeoflt = 0;
                }
                else if (levelmapsortingtiles[indextile] == 998)
                {
                    istypeoflt = 1;
                }
                else
                {
                    istypeoflt = -1;
                }
            }
            else
            {
                istypeoflt = -1;
            }



            tilex = x + 1;
            tiley = y;
            tilez = z + 1;


            xii = tilex;
            zii = tilez;

            if (xii < 0)
            {
                xii *= -1;
                xii = xii + (maxx - 1);
            }
            if (zii < 0)
            {
                zii *= -1;
                zii = zii + (maxz - 1);
            }
            indextile = xii + somewidth * (y + someheight * zii);

            if (indextile < somewidth * someheight * somedepth)
            {
                if (levelmapsortingtiles[indextile] == 0)
                {
                    istypeofrt = 0;
                }
                else if (levelmapsortingtiles[indextile] == 998)
                {
                    istypeofrt = 1;
                }
                else
                {
                    istypeofrt = -1;
                }
            }
            else
            {
                istypeofrt = -1;
            }

            int xx = (x);
            int yy = (y);
            int zz = (z);

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

            int indexinarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles


            /////////BUILD WALL LEFT/////////////////
            if (istypeoflt == -1 && istypeoft == 1 && istypeofrt == 0 &&
                istypeofl == -1 &&/******************/ istypeofr == 0 &&
                istypeoflb == -1 && istypeofb == 1 && istypeofrb == 0 ||

                istypeoflt == -1 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == -1 &&/******************/ istypeofr == 0 &&
                istypeoflb == -1 && istypeofb == 1 && istypeofrb == 0 ||

                istypeoflt == -1 && istypeoft == 1 && istypeofrt == 0 &&
                istypeofl == -1 &&/******************/ istypeofr == 0 &&
                istypeoflb == -1 && istypeofb == 1 && istypeofrb == 1 ||

                istypeoflt == -1 && istypeoft == 1 && istypeofrt == 0 &&
                istypeofl == -1 &&/******************/ istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == 1 && istypeofrb == 0
                
                ||

                istypeoflt == -1 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == -1 &&/******************/ istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == 1 && istypeofrb == 0 ||

                istypeoflt == -1 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == -1 &&/******************/ istypeofr == 0 &&
                istypeoflb == -1 && istypeofb == 1 && istypeofrb == 1 ||

                istypeoflt == -1 && istypeoft == 1 && istypeofrt == 0 &&
                istypeofl == -1 &&/******************/ istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == 1 && istypeofrb == 1
                ||

                istypeoflt == -1 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == -1 &&/******************/ istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == 1 && istypeofrb == 1 ||




                istypeoflt == -1 && istypeoft == 1 && istypeofrt == 0 &&
                istypeofl == -1 &&/******************/ istypeofr == 0 &&
                istypeoflb == 1 && istypeofb == 1 && istypeofrb == 0 ||

                istypeoflt == -1 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == -1 &&/******************/ istypeofr == 0 &&
                istypeoflb == 1 && istypeofb == 1 && istypeofrb == 0 ||

                istypeoflt == -1 && istypeoft == 1 && istypeofrt == 0 &&
                istypeofl == -1 &&/******************/ istypeofr == 0 &&
                istypeoflb == 1 && istypeofb == 1 && istypeofrb == 1 ||

                istypeoflt == -1 && istypeoft == 1 && istypeofrt == 0 &&
                istypeofl == -1 &&/******************/ istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == 1 && istypeofrb == 0

                ||

                istypeoflt == -1 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == -1 &&/******************/ istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == 1 && istypeofrb == 0 ||

                istypeoflt == -1 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == -1 &&/******************/ istypeofr == 0 &&
                istypeoflb == 1 && istypeofb == 1 && istypeofrb == 1 ||

                istypeoflt == -1 && istypeoft == 1 && istypeofrt == 0 &&
                istypeofl == -1 &&/******************/ istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == 1 && istypeofrb == 1
                ||

                istypeoflt == -1 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == -1 &&/******************/ istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == 1 && istypeofrb == 1 ||





                istypeoflt == 1 && istypeoft == 1 && istypeofrt == 0 &&
                istypeofl == -1 &&/******************/ istypeofr == 0 &&
                istypeoflb == -1 && istypeofb == 1 && istypeofrb == 0 ||

                istypeoflt == 1 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == -1 &&/******************/ istypeofr == 0 &&
                istypeoflb == -1 && istypeofb == 1 && istypeofrb == 0 ||

                istypeoflt == 1 && istypeoft == 1 && istypeofrt == 0 &&
                istypeofl == -1 &&/******************/ istypeofr == 0 &&
                istypeoflb == -1 && istypeofb == 1 && istypeofrb == 1 ||

                istypeoflt == 1 && istypeoft == 1 && istypeofrt == 0 &&
                istypeofl == -1 &&/******************/ istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == 1 && istypeofrb == 0

                ||

                istypeoflt == 1 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == -1 &&/******************/ istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == 1 && istypeofrb == 0 ||

                istypeoflt == 1 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == -1 &&/******************/ istypeofr == 0 &&
                istypeoflb == -1 && istypeofb == 1 && istypeofrb == 1 ||

                istypeoflt == 1 && istypeoft == 1 && istypeofrt == 0 &&
                istypeofl == -1 &&/******************/ istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == 1 && istypeofrb == 1
                ||

                istypeoflt == 1 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == -1 &&/******************/ istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == 1 && istypeofrb == 1

                )
            {
                                          
                if (levelmap[indexinarray] != 1101 &&
                    levelmap[indexinarray] != 1102 &&
                    levelmap[indexinarray] != 1103 &&
                    levelmap[indexinarray] != 1104 &&
                    levelmap[indexinarray] != 1105 &&
                    levelmap[indexinarray] != 1106 &&
                    levelmap[indexinarray] != 1107 &&
                    levelmap[indexinarray] != 1108 &&
                    levelmap[indexinarray] != 1109 &&
                    levelmap[indexinarray] != 1110 &&
                    levelmap[indexinarray] != 1111 &&
                    levelmap[indexinarray] != 1112)
                {
                    levelmap[indexinarray] = 1101;
                }
            }


            
            /////////BUILD WALL RIGHT/////////////////
            if (istypeoflt == 0 && istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 0 &&/******************/istypeofr == -1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == -1 ||

                istypeoflt == 1 && istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 0 &&/******************/istypeofr == -1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == -1 ||

                istypeoflt == 0 && istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 0 &&/******************/istypeofr == -1 &&
                istypeoflb == 1 && istypeofb == 1 && istypeofrb == -1 ||

                istypeoflt == 0 && istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 1 &&/******************/istypeofr == -1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == -1

                ||

                istypeoflt == 1 && istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 1 &&/******************/istypeofr == -1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == -1
                ||

                istypeoflt == 1 && istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 0 &&/******************/istypeofr == -1 &&
                istypeoflb == 1 && istypeofb == 1 && istypeofrb == -1
                ||

                istypeoflt == 0 && istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 1 &&/******************/istypeofr == -1 &&
                istypeoflb == 1 && istypeofb == 1 && istypeofrb == -1
                ||

                istypeoflt == 1 && istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 1 &&/******************/istypeofr == -1 &&
                istypeoflb == 1 && istypeofb == 1 && istypeofrb == -1 ||


                istypeoflt == 0 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 0 &&/******************/istypeofr == -1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == -1 ||

                istypeoflt == 1 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 0 &&/******************/istypeofr == -1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == -1 ||

                istypeoflt == 0 && istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 0 &&/******************/istypeofr == -1 &&
                istypeoflb == 1 && istypeofb == 1 && istypeofrb == -1 ||

                istypeoflt == 0 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 1 &&/******************/istypeofr == -1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == -1

                ||

                istypeoflt == 1 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 1 &&/******************/istypeofr == -1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == -1
                ||

                istypeoflt == 1 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 0 &&/******************/istypeofr == -1 &&
                istypeoflb == 1 && istypeofb == 1 && istypeofrb == -1
                ||

                istypeoflt == 0 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 1 &&/******************/istypeofr == -1 &&
                istypeoflb == 1 && istypeofb == 1 && istypeofrb == -1
                ||

                istypeoflt == 1 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 1 &&/******************/istypeofr == -1 &&
                istypeoflb == 1 && istypeofb == 1 && istypeofrb == -1 ||




                istypeoflt == 0 && istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 0 &&/******************/istypeofr == -1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == 1 ||

                istypeoflt == 1 && istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 0 &&/******************/istypeofr == -1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == 1 ||

                istypeoflt == 0 && istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 0 &&/******************/istypeofr == -1 &&
                istypeoflb == 1 && istypeofb == 1 && istypeofrb == 1 ||

                istypeoflt == 0 && istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 1 &&/******************/istypeofr == -1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == 1

                ||

                istypeoflt == 1 && istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 1 &&/******************/istypeofr == -1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == 1
                ||

                istypeoflt == 1 && istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 0 &&/******************/istypeofr == -1 &&
                istypeoflb == 1 && istypeofb == 1 && istypeofrb == 1
                ||

                istypeoflt == 0 && istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 1 &&/******************/istypeofr == -1 &&
                istypeoflb == 1 && istypeofb == 1 && istypeofrb == 1
                ||

                istypeoflt == 1 && istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 1 &&/******************/istypeofr == -1 &&
                istypeoflb == 1 && istypeofb == 1 && istypeofrb == 1

                )
            {
                if (levelmap[indexinarray] != 1101 &&
                    levelmap[indexinarray] != 1102 &&
                    levelmap[indexinarray] != 1103 &&
                    levelmap[indexinarray] != 1104 &&
                    levelmap[indexinarray] != 1105 &&
                    levelmap[indexinarray] != 1106 &&
                    levelmap[indexinarray] != 1107 &&
                    levelmap[indexinarray] != 1108 &&
                    levelmap[indexinarray] != 1109 &&
                    levelmap[indexinarray] != 1110 &&
                    levelmap[indexinarray] != 1111 &&
                    levelmap[indexinarray] != 1112)
                {
                    levelmap[indexinarray] = 1102;
                }
            }





























            /////////BUILD WALL BACK/////////////////
            if (istypeoflt == -1 && istypeoft == -1 && istypeofrt == -1 &&
                istypeofl == 1 &&/******************/istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0 ||

                istypeoflt == -1 && istypeoft == -1 && istypeofrt == -1 &&
                istypeofl == 1 &&/******************/istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == 0 && istypeofrb == 0 ||

                istypeoflt == -1 && istypeoft == -1 && istypeofrt == -1 &&
                istypeofl == 1 &&/******************/istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 1 ||

                istypeoflt == -1 && istypeoft == -1 && istypeofrt == -1 &&
                istypeofl == 1 &&/******************/istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == 0 ||


                istypeoflt == -1 && istypeoft == -1 && istypeofrt == -1 &&
                istypeofl == 1 &&/******************/istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == 1 && istypeofrb == 0 ||

                istypeoflt == -1 && istypeoft == -1 && istypeofrt == -1 &&
                istypeofl == 1 &&/******************/istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == 0 && istypeofrb == 1 ||

                istypeoflt == -1 && istypeoft == -1 && istypeofrt == -1 &&
                istypeofl == 1 &&/******************/istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == 1
                ||

                istypeoflt == -1 && istypeoft == -1 && istypeofrt == -1 &&
                istypeofl == 1 &&/******************/istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == 1 && istypeofrb == 1 ||



                istypeoflt == 1 && istypeoft == -1 && istypeofrt == -1 &&
                istypeofl == 1 &&/******************/istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0 ||

                istypeoflt == 1 && istypeoft == -1 && istypeofrt == -1 &&
                istypeofl == 1 &&/******************/istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == 0 && istypeofrb == 0 ||

                istypeoflt == 1 && istypeoft == -1 && istypeofrt == -1 &&
                istypeofl == 1 &&/******************/istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 1 ||

                istypeoflt == 1 && istypeoft == -1 && istypeofrt == -1 &&
                istypeofl == 1 &&/******************/istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == 0 ||


                istypeoflt == 1 && istypeoft == -1 && istypeofrt == -1 &&
                istypeofl == 1 &&/******************/istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == 1 && istypeofrb == 0 ||

                istypeoflt == 1 && istypeoft == -1 && istypeofrt == -1 &&
                istypeofl == 1 &&/******************/istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == 0 && istypeofrb == 1 ||

                istypeoflt == 1 && istypeoft == -1 && istypeofrt == -1 &&
                istypeofl == 1 &&/******************/istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == 1
                ||

                istypeoflt == 1 && istypeoft == -1 && istypeofrt == -1 &&
                istypeofl == 1 &&/******************/istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == 1 && istypeofrb == 1  ||



                istypeoflt == -1 && istypeoft == -1 && istypeofrt == 1 &&
                istypeofl == 1 &&/******************/istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0 ||

                istypeoflt == -1 && istypeoft == -1 && istypeofrt == 1 &&
                istypeofl == 1 &&/******************/istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == 0 && istypeofrb == 0 ||

                istypeoflt == -1 && istypeoft == -1 && istypeofrt == 1 &&
                istypeofl == 1 &&/******************/istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 1 ||

                istypeoflt == -1 && istypeoft == -1 && istypeofrt == 1 &&
                istypeofl == 1 &&/******************/istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == 0 ||


                istypeoflt == -1 && istypeoft == -1 && istypeofrt == 1 &&
                istypeofl == 1 &&/******************/istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == 1 && istypeofrb == 0 ||

                istypeoflt == -1 && istypeoft == -1 && istypeofrt == 1 &&
                istypeofl == 1 &&/******************/istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == 0 && istypeofrb == 1 ||

                istypeoflt == -1 && istypeoft == -1 && istypeofrt == 1 &&
                istypeofl == 1 &&/******************/istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == 1
                ||

                istypeoflt == -1 && istypeoft == -1 && istypeofrt == 1 &&
                istypeofl == 1 &&/******************/istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == 1 && istypeofrb == 1 



                )
            {
                
                if (levelmap[indexinarray] != 1101 &&
                    levelmap[indexinarray] != 1102 &&
                    levelmap[indexinarray] != 1103 &&
                    levelmap[indexinarray] != 1104 &&
                    levelmap[indexinarray] != 1105 &&
                    levelmap[indexinarray] != 1106 &&
                    levelmap[indexinarray] != 1107 &&
                    levelmap[indexinarray] != 1108 &&
                    levelmap[indexinarray] != 1109 &&
                    levelmap[indexinarray] != 1110 &&
                    levelmap[indexinarray] != 1111 &&
                    levelmap[indexinarray] != 1112)
                {
                    levelmap[indexinarray] = 1104;
                }
            }





            /////////BUILD WALL FRONT/////////////////
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 1 &&/******************/istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == -1 ||

                istypeoflt == 1 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 1 &&/******************/istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == -1 ||

                istypeoflt == 0 && istypeoft == 0 && istypeofrt == 1 &&
                istypeofl == 1 &&/******************/istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == -1 ||

                istypeoflt == 0 && istypeoft == 1 && istypeofrt == 0 &&
                istypeofl == 1 &&/******************/istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == -1
                ||

                istypeoflt == 1 && istypeoft == 1 && istypeofrt == 0 &&
                istypeofl == 1 &&/******************/istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == -1 ||

                istypeoflt == 1 && istypeoft == 0 && istypeofrt == 1 &&
                istypeofl == 1 &&/******************/istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == -1 ||

                istypeoflt == 0 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 1 &&/******************/istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == -1 ||

                istypeoflt == 1 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 1 &&/******************/istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == -1 ||





                istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 1 &&/******************/istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == -1 && istypeofrb == -1 ||

                istypeoflt == 1 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 1 &&/******************/istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == -1 && istypeofrb == -1 ||

                istypeoflt == 0 && istypeoft == 0 && istypeofrt == 1 &&
                istypeofl == 1 &&/******************/istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == -1 && istypeofrb == -1 ||

                istypeoflt == 0 && istypeoft == 1 && istypeofrt == 0 &&
                istypeofl == 1 &&/******************/istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == -1 && istypeofrb == -1
                ||

                istypeoflt == 1 && istypeoft == 1 && istypeofrt == 0 &&
                istypeofl == 1 &&/******************/istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == -1 && istypeofrb == -1 ||

                istypeoflt == 1 && istypeoft == 0 && istypeofrt == 1 &&
                istypeofl == 1 &&/******************/istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == -1 && istypeofrb == -1 ||

                istypeoflt == 0 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 1 &&/******************/istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == -1 && istypeofrb == -1 ||

                istypeoflt == 1 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 1 &&/******************/istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == -1 && istypeofrb == -1 ||




                istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 1 &&/******************/istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == 1 ||

                istypeoflt == 1 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 1 &&/******************/istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == 1 ||

                istypeoflt == 0 && istypeoft == 0 && istypeofrt == 1 &&
                istypeofl == 1 &&/******************/istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == 1 ||

                istypeoflt == 0 && istypeoft == 1 && istypeofrt == 0 &&
                istypeofl == 1 &&/******************/istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == 1
                ||

                istypeoflt == 1 && istypeoft == 1 && istypeofrt == 0 &&
                istypeofl == 1 &&/******************/istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == 1 ||

                istypeoflt == 1 && istypeoft == 0 && istypeofrt == 1 &&
                istypeofl == 1 &&/******************/istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == 1 ||

                istypeoflt == 0 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 1 &&/******************/istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == 1 ||

                istypeoflt == 1 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 1 &&/******************/istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == 1





                )
            {
                
                if (levelmap[indexinarray] != 1101 &&
                    levelmap[indexinarray] != 1102 &&
                    levelmap[indexinarray] != 1103 &&
                    levelmap[indexinarray] != 1104 &&
                    levelmap[indexinarray] != 1105 &&
                    levelmap[indexinarray] != 1106 &&
                    levelmap[indexinarray] != 1107 &&
                    levelmap[indexinarray] != 1108 &&
                    levelmap[indexinarray] != 1109 &&
                    levelmap[indexinarray] != 1110 &&
                    levelmap[indexinarray] != 1111 &&
                    levelmap[indexinarray] != 1112)
                {
                    levelmap[indexinarray] = 1103;
                }
            }


















            /////////BUILD WALL LEFT FRONT INSIDE/////////////////
            if (istypeoft == -1 &&
               istypeofl == -1 && istypeofr == 1 &&
                                  istypeofb == 1)
            {
                
                if (levelmap[indexinarray] != 1101 &&
               levelmap[indexinarray] != 1102 &&
               levelmap[indexinarray] != 1103 &&
               levelmap[indexinarray] != 1104 &&
               levelmap[indexinarray] != 1105 &&
               levelmap[indexinarray] != 1106 &&
               levelmap[indexinarray] != 1107 &&
               levelmap[indexinarray] != 1108 &&
               levelmap[indexinarray] != 1109 &&
               levelmap[indexinarray] != 1110 &&
               levelmap[indexinarray] != 1111 &&
               levelmap[indexinarray] != 1112
               )
                {
                    levelmap[indexinarray] = 1105;
                }
            }

            /////////BUILD WALL RIGHT FRONT INSIDE/////////////////
            if (istypeoft == -1 &&
               istypeofl == 1 && istypeofr == -1 &&
                                  istypeofb == 1)
            {
               
                if (levelmap[indexinarray] != 1101 &&
                levelmap[indexinarray] != 1102 &&
                levelmap[indexinarray] != 1103 &&
                levelmap[indexinarray] != 1104 &&
                levelmap[indexinarray] != 1105 &&
                levelmap[indexinarray] != 1106 &&
                levelmap[indexinarray] != 1107 &&
                levelmap[indexinarray] != 1108 &&
                levelmap[indexinarray] != 1109 &&
                levelmap[indexinarray] != 1110 &&
                levelmap[indexinarray] != 1111 &&
                levelmap[indexinarray] != 1112
                )
                {
                    levelmap[indexinarray] = 1106;
                }
            }


            /////////BUILD WALL LEFT BACK INSIDE/////////////////
            if (istypeoft == 1 &&
               istypeofl == -1 && istypeofr == 1 &&
                                  istypeofb == -1)
            {
                if (levelmap[indexinarray] != 1101 &&
              levelmap[indexinarray] != 1102 &&
              levelmap[indexinarray] != 1103 &&
              levelmap[indexinarray] != 1104 &&
              levelmap[indexinarray] != 1105 &&
              levelmap[indexinarray] != 1106 &&
              levelmap[indexinarray] != 1107 &&
              levelmap[indexinarray] != 1108 &&
              levelmap[indexinarray] != 1109 &&
              levelmap[indexinarray] != 1110 &&
              levelmap[indexinarray] != 1111 &&
              levelmap[indexinarray] != 1112
              )
                {
                    levelmap[indexinarray] = 1107;
                }
            }

            /////////BUILD WALL LEFT BACK INSIDE/////////////////
            if (istypeoft == 1 &&
               istypeofl == 1 && istypeofr == -1 &&
                                  istypeofb == -1)
            {
                if (levelmap[indexinarray] != 1101 &&
              levelmap[indexinarray] != 1102 &&
              levelmap[indexinarray] != 1103 &&
              levelmap[indexinarray] != 1104 &&
              levelmap[indexinarray] != 1105 &&
              levelmap[indexinarray] != 1106 &&
              levelmap[indexinarray] != 1107 &&
              levelmap[indexinarray] != 1108 &&
              levelmap[indexinarray] != 1109 &&
              levelmap[indexinarray] != 1110 &&
              levelmap[indexinarray] != 1111 &&
              levelmap[indexinarray] != 1112
              )
                {
                    levelmap[indexinarray] = 1108;
                }
            }





            /////////BUILD WALL LEFT FRONT OUTSIDE/////////////////
            if (istypeoflt == -1 && istypeoft == 1 &&
               istypeofl == 1 && istypeofr == 0 &&
                                  istypeofb == 0)
            {
                
                if (levelmap[indexinarray] != 1101 &&
              levelmap[indexinarray] != 1102 &&
              levelmap[indexinarray] != 1103 &&
              levelmap[indexinarray] != 1104 &&
              levelmap[indexinarray] != 1105 &&
              levelmap[indexinarray] != 1106 &&
              levelmap[indexinarray] != 1107 &&
              levelmap[indexinarray] != 1108 &&
              levelmap[indexinarray] != 1109 &&
              levelmap[indexinarray] != 1110 &&
              levelmap[indexinarray] != 1111 &&
              levelmap[indexinarray] != 1112
              )
                {
                    levelmap[indexinarray] = 1109;
                }
            }

            /////////BUILD WALL RIGHT FRONT OUTSIDE/////////////////
            if (istypeoft == 1 && istypeofrt == -1 &&
               istypeofl == 0 && istypeofr == 1 &&
                                  istypeofb == 0)
            {
               
                if (levelmap[indexinarray] != 1101 &&
              levelmap[indexinarray] != 1102 &&
              levelmap[indexinarray] != 1103 &&
              levelmap[indexinarray] != 1104 &&
              levelmap[indexinarray] != 1105 &&
              levelmap[indexinarray] != 1106 &&
              levelmap[indexinarray] != 1107 &&
              levelmap[indexinarray] != 1108 &&
              levelmap[indexinarray] != 1109 &&
              levelmap[indexinarray] != 1110 &&
              levelmap[indexinarray] != 1111 &&
              levelmap[indexinarray] != 1112
              )
                {
                    levelmap[indexinarray] = 1110;
                }
            }


            /////////BUILD WALL LEFT BACK OUTSIDE/////////////////
            if (istypeoft == 0 &&
               istypeofl == 1 && istypeofr == 0 &&
                 istypeoflb == -1 && istypeofb == 1)
            {


                //levelmapsortingtiles[indexinarray] = 1111;
                if (levelmap[indexinarray] != 1101 &&
               levelmap[indexinarray] != 1102 &&
               levelmap[indexinarray] != 1103 &&
               levelmap[indexinarray] != 1104 &&
               levelmap[indexinarray] != 1105 &&
               levelmap[indexinarray] != 1106 &&
               levelmap[indexinarray] != 1107 &&
               levelmap[indexinarray] != 1108 &&
               levelmap[indexinarray] != 1109 &&
               levelmap[indexinarray] != 1110 &&
               levelmap[indexinarray] != 1111 &&
               levelmap[indexinarray] != 1112
               )
                {
                    levelmap[indexinarray] = 1111;
                }
            }



            /////////BUILD WALL RIGHT FRONT OUTSIDE/////////////////
            if (istypeoft == 0 &&
               istypeofl == 0 && istypeofr == 1 &&
                                  istypeofb == 1 && istypeofrb == -1)
            {

                if (levelmap[indexinarray] != 1101 &&
                    levelmap[indexinarray] != 1102 &&
                    levelmap[indexinarray] != 1103 &&
                    levelmap[indexinarray] != 1104 &&
                    levelmap[indexinarray] != 1105 &&
                    levelmap[indexinarray] != 1106 &&
                    levelmap[indexinarray] != 1107 &&
                    levelmap[indexinarray] != 1108 &&
                    levelmap[indexinarray] != 1109 &&
                    levelmap[indexinarray] != 1110 &&
                    levelmap[indexinarray] != 1111 &&
                    levelmap[indexinarray] != 1112
                    )
                {
                    levelmap[indexinarray] = 1112;
                }
            }























            //LETTER "E" AS EMPTY TILE
            //LETTER "X" AS THE CURRENT TILE
            //INTERROGATION MARK "?" AS PROBABLE WALL OR FLOOR TILE
            //LETTER "F" AS FLOOR TILE
            //999999999999
            //999999999999
            //9999E?999999
            //9999EXF99999
            //9999E?999999
            //999999999999
            //999999999999

            /*/////////BUILD WALL LEFT/////////////////
            if (istypeoflt == -1 && istypeoft == 1 &&
                istypeofl == -1 &&                  istypeofr == 0 &&
                istypeoflb == -1 && istypeofb == 1)
            {
                int xx = (x);
                int yy = (y);
                int zz = (z);

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

                int indexinarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles
                                                                            //Console.WriteLine("build left wall");
                                                                            //levelmapsortingtiles[indexinarray] = 1101;
                if (levelmap[indexinarray] != 1101 &&
                    levelmap[indexinarray] != 1102 &&
                    levelmap[indexinarray] != 1103 &&
                    levelmap[indexinarray] != 1104 &&
                    levelmap[indexinarray] != 1105 &&
                    levelmap[indexinarray] != 1106 &&
                    levelmap[indexinarray] != 1107 &&
                    levelmap[indexinarray] != 1108 &&
                    levelmap[indexinarray] != 1109 &&
                    levelmap[indexinarray] != 1110 &&
                    levelmap[indexinarray] != 1111 &&
                    levelmap[indexinarray] != 1112)
                {
                    levelmap[indexinarray] = 1101;
                }
              
            }

            if (istypeoflt == 1 && istypeoft == 1 &&
                istypeofl == -1 &&                  istypeofr == 0 &&
                istypeoflb == -1 && istypeofb == 1)
            {
                int xx = (x);
                int yy = (y);
                int zz = (z);

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

                int indexinarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles
                                                                            //Console.WriteLine("build left wall");
                                                                            //levelmapsortingtiles[indexinarray] = 1101;
                if (levelmap[indexinarray] != 1101 &&
                   levelmap[indexinarray] != 1102 &&
                   levelmap[indexinarray] != 1103 &&
                   levelmap[indexinarray] != 1104 &&
                   levelmap[indexinarray] != 1105 &&
                   levelmap[indexinarray] != 1106 &&
                   levelmap[indexinarray] != 1107 &&
                   levelmap[indexinarray] != 1108 &&
                   levelmap[indexinarray] != 1109 &&
                   levelmap[indexinarray] != 1110 &&
                   levelmap[indexinarray] != 1111 &&
                   levelmap[indexinarray] != 1112)
                {
                    levelmap[indexinarray] = 1101;
                }
            }
            if (istypeoflt == -1 && istypeoft == 1 &&
              istypeofl == -1 &&                istypeofr == 0 &&
              istypeoflb == 1 && istypeofb == 1)
            {
                int xx = (x);
                int yy = (y);
                int zz = (z);

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

                int indexinarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles
                                                                            //Console.WriteLine("build left wall");
                                                                            //levelmapsortingtiles[indexinarray] = 1101;
                if (levelmap[indexinarray] != 1101 &&
                   levelmap[indexinarray] != 1102 &&
                   levelmap[indexinarray] != 1103 &&
                   levelmap[indexinarray] != 1104 &&
                   levelmap[indexinarray] != 1105 &&
                   levelmap[indexinarray] != 1106 &&
                   levelmap[indexinarray] != 1107 &&
                   levelmap[indexinarray] != 1108 &&
                   levelmap[indexinarray] != 1109 &&
                   levelmap[indexinarray] != 1110 &&
                   levelmap[indexinarray] != 1111 &&
                   levelmap[indexinarray] != 1112
                   )
                {
                    levelmap[indexinarray] = 1101;
                }
            }
            if (istypeoflt == 1 && istypeoft == 1 &&
                istypeofl == -1 &&              istypeofr == 0 &&
                istypeoflb == 1 && istypeofb == 1)
            {
                int xx = (x);
                int yy = (y);
                int zz = (z);

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

                int indexinarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles
                                                                            //Console.WriteLine("build left wall");
                                                                            //levelmapsortingtiles[indexinarray] = 1101;
                if (levelmap[indexinarray] != 1101 &&
                   levelmap[indexinarray] != 1102 &&
                   levelmap[indexinarray] != 1103 &&
                   levelmap[indexinarray] != 1104 &&
                   levelmap[indexinarray] != 1105 &&
                   levelmap[indexinarray] != 1106 &&
                   levelmap[indexinarray] != 1107 &&
                   levelmap[indexinarray] != 1108 &&
                   levelmap[indexinarray] != 1109 &&
                   levelmap[indexinarray] != 1110 &&
                   levelmap[indexinarray] != 1111 &&
                   levelmap[indexinarray] != 1112
                   )
                {
                    levelmap[indexinarray] = 1101;
                }
            }
            if (istypeoflt == -1 && istypeoft == 1 &&
                istypeofl == -1 &&                  istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == 1)
            {
                int xx = (x);
                int yy = (y);
                int zz = (z);

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

                int indexinarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles
                                                                            //Console.WriteLine("build left wall");
                                                                            //levelmapsortingtiles[indexinarray] = 1101;
                if (levelmap[indexinarray] != 1101 &&
                    levelmap[indexinarray] != 1102 &&
                    levelmap[indexinarray] != 1103 &&
                    levelmap[indexinarray] != 1104 &&
                    levelmap[indexinarray] != 1105 &&
                    levelmap[indexinarray] != 1106 &&
                    levelmap[indexinarray] != 1107 &&
                    levelmap[indexinarray] != 1108 &&
                    levelmap[indexinarray] != 1109 &&
                    levelmap[indexinarray] != 1110 &&
                    levelmap[indexinarray] != 1111 &&
                    levelmap[indexinarray] != 1112)
                {
                    levelmap[indexinarray] = 1101;
                }

            }*/
            /*if (istypeoflt == -1 && istypeoft == 1 &&
                istypeofl == -1 &&                  istypeofr == 0 &&
                istypeoflb == -1 && istypeofb == 0)
            {
                int xx = (x);
                int yy = (y);
                int zz = (z);

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

                int indexinarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles
                                                                            //Console.WriteLine("build left wall");
                                                                            //levelmapsortingtiles[indexinarray] = 1101;
                if (levelmap[indexinarray] != 1101 &&
                   levelmap[indexinarray] != 1102 &&
                   levelmap[indexinarray] != 1103 &&
                   levelmap[indexinarray] != 1104 &&
                   levelmap[indexinarray] != 1105 &&
                   levelmap[indexinarray] != 1106 &&
                   levelmap[indexinarray] != 1107 &&
                   levelmap[indexinarray] != 1108 &&
                   levelmap[indexinarray] != 1109 &&
                   levelmap[indexinarray] != 1110 &&
                   levelmap[indexinarray] != 1111 &&
                   levelmap[indexinarray] != 1112
                   )
                {
                    levelmap[indexinarray] = 1101;
                }
            }*/






            /*
            /////////BUILD WALL RIGHT/////////////////
            if (istypeoft == 1 && istypeofrt == -1 &&
                 istypeofl == 0 && istypeofr == -1 &&
                                 istypeofb == 1 && istypeofrb == -1)
            {
                int xx = (x);
                int yy = (y);
                int zz = (z);

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

                int indexinarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                //levelmapsortingtiles[indexinarray] = 1102;
                if (levelmap[indexinarray] != 1101 &&
                   levelmap[indexinarray] != 1102 &&
                   levelmap[indexinarray] != 1103 &&
                   levelmap[indexinarray] != 1104 &&
                   levelmap[indexinarray] != 1105 &&
                   levelmap[indexinarray] != 1106 &&
                   levelmap[indexinarray] != 1107 &&
                   levelmap[indexinarray] != 1108 &&
                   levelmap[indexinarray] != 1109 &&
                   levelmap[indexinarray] != 1110 &&
                   levelmap[indexinarray] != 1111 &&
                   levelmap[indexinarray] != 1112
                   )
                {
                    levelmap[indexinarray] = 1102;
                }
            }

            if (istypeoft == 1 && istypeofrt == 1 &&
                 istypeofl == 0 && istypeofr == -1 &&
                 istypeofb == 1 && istypeofrb == -1)
            {
                int xx = (x);
                int yy = (y);
                int zz = (z);

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

                int indexinarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                //levelmapsortingtiles[indexinarray] = 1102;
                if (levelmap[indexinarray] != 1101 &&
                 levelmap[indexinarray] != 1102 &&
                 levelmap[indexinarray] != 1103 &&
                 levelmap[indexinarray] != 1104 &&
                 levelmap[indexinarray] != 1105 &&
                 levelmap[indexinarray] != 1106 &&
                 levelmap[indexinarray] != 1107 &&
                 levelmap[indexinarray] != 1108 &&
                 levelmap[indexinarray] != 1109 &&
                 levelmap[indexinarray] != 1110 &&
                 levelmap[indexinarray] != 1111 &&
                 levelmap[indexinarray] != 1112
                 )
                {
                    levelmap[indexinarray] = 1102;
                }
            }

            if (istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 0 && istypeofr == -1 &&
                istypeofb == 1 && istypeofrb == 1)
            {
                int xx = (x);
                int yy = (y);
                int zz = (z);

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

                int indexinarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                //levelmapsortingtiles[indexinarray] = 1102;
                if (levelmap[indexinarray] != 1101 &&
                  levelmap[indexinarray] != 1102 &&
                  levelmap[indexinarray] != 1103 &&
                  levelmap[indexinarray] != 1104 &&
                  levelmap[indexinarray] != 1105 &&
                  levelmap[indexinarray] != 1106 &&
                  levelmap[indexinarray] != 1107 &&
                  levelmap[indexinarray] != 1108 &&
                  levelmap[indexinarray] != 1109 &&
                  levelmap[indexinarray] != 1110 &&
                  levelmap[indexinarray] != 1111 &&
                  levelmap[indexinarray] != 1112
                  )
                {
                    levelmap[indexinarray] = 1102;
                }
            }

            if (istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 0 && istypeofr == -1 &&
                istypeofb == 1 && istypeofrb == 1)
            {
                int xx = (x);
                int yy = (y);
                int zz = (z);

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

                int indexinarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                //levelmapsortingtiles[indexinarray] = 1102;
                if (levelmap[indexinarray] != 1101 &&
                 levelmap[indexinarray] != 1102 &&
                 levelmap[indexinarray] != 1103 &&
                 levelmap[indexinarray] != 1104 &&
                 levelmap[indexinarray] != 1105 &&
                 levelmap[indexinarray] != 1106 &&
                 levelmap[indexinarray] != 1107 &&
                 levelmap[indexinarray] != 1108 &&
                 levelmap[indexinarray] != 1109 &&
                 levelmap[indexinarray] != 1110 &&
                 levelmap[indexinarray] != 1111 &&
                 levelmap[indexinarray] != 1112
                 )
                {
                    levelmap[indexinarray] = 1102;
                }
            }
            if (                    istypeoft == 1 && istypeofrt == -1 &&
                 istypeofl == 1 &&                   istypeofr == -1 &&
                                 istypeofb == 1 && istypeofrb == -1)
            {
                int xx = (x);
                int yy = (y);
                int zz = (z);

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

                int indexinarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                //levelmapsortingtiles[indexinarray] = 1102;
                if (levelmap[indexinarray] != 1101 &&
                   levelmap[indexinarray] != 1102 &&
                   levelmap[indexinarray] != 1103 &&
                   levelmap[indexinarray] != 1104 &&
                   levelmap[indexinarray] != 1105 &&
                   levelmap[indexinarray] != 1106 &&
                   levelmap[indexinarray] != 1107 &&
                   levelmap[indexinarray] != 1108 &&
                   levelmap[indexinarray] != 1109 &&
                   levelmap[indexinarray] != 1110 &&
                   levelmap[indexinarray] != 1111 &&
                   levelmap[indexinarray] != 1112
                   )
                {
                    levelmap[indexinarray] = 1102;
                }
            }
            */
            //////BUILD WALL RIGHT









            /*
            /////////BUILD WALL BACK/////////////////

            if (istypeoflt == -1 && istypeoft == -1 && istypeofrt == -1 &&
               istypeofl == 1 &&                        istypeofr == 1 &&
                                    istypeofb == 0)
            {
                int xx = (x);
                int yy = (y);
                int zz = (z);

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

                int indexinarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                //levelmapsortingtiles[indexinarray] = 1104;
                if (levelmap[indexinarray] != 1101 &&
                 levelmap[indexinarray] != 1102 &&
                 levelmap[indexinarray] != 1103 &&
                 levelmap[indexinarray] != 1104 &&
                 levelmap[indexinarray] != 1105 &&
                 levelmap[indexinarray] != 1106 &&
                 levelmap[indexinarray] != 1107 &&
                 levelmap[indexinarray] != 1108 &&
                 levelmap[indexinarray] != 1109 &&
                 levelmap[indexinarray] != 1110 &&
                 levelmap[indexinarray] != 1111 &&
                 levelmap[indexinarray] != 1112
                 )
                {
                    levelmap[indexinarray] = 1104;
                }
            }
            if (istypeoflt == 1 && istypeoft == -1 && istypeofrt == -1 &&
              istypeofl == 1 && istypeofr == 1 &&
              istypeofb == 0)
            {
                int xx = (x);
                int yy = (y);
                int zz = (z);

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

                int indexinarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                //levelmapsortingtiles[indexinarray] = 1104;
                if (levelmap[indexinarray] != 1101 &&
                levelmap[indexinarray] != 1102 &&
                levelmap[indexinarray] != 1103 &&
                levelmap[indexinarray] != 1104 &&
                levelmap[indexinarray] != 1105 &&
                levelmap[indexinarray] != 1106 &&
                levelmap[indexinarray] != 1107 &&
                levelmap[indexinarray] != 1108 &&
                levelmap[indexinarray] != 1109 &&
                levelmap[indexinarray] != 1110 &&
                levelmap[indexinarray] != 1111 &&
                levelmap[indexinarray] != 1112
                )
                {
                    levelmap[indexinarray] = 1104;
                }
            }

            if (istypeoflt == -1 && istypeoft == -1 && istypeofrt == 1 &&
              istypeofl == 1 && istypeofr == 1 &&
              istypeofb == 0)
            {
                int xx = (x);
                int yy = (y);
                int zz = (z);

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

                int indexinarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                //levelmapsortingtiles[indexinarray] = 1104;
                if (levelmap[indexinarray] != 1101 &&
                levelmap[indexinarray] != 1102 &&
                levelmap[indexinarray] != 1103 &&
                levelmap[indexinarray] != 1104 &&
                levelmap[indexinarray] != 1105 &&
                levelmap[indexinarray] != 1106 &&
                levelmap[indexinarray] != 1107 &&
                levelmap[indexinarray] != 1108 &&
                levelmap[indexinarray] != 1109 &&
                levelmap[indexinarray] != 1110 &&
                levelmap[indexinarray] != 1111 &&
                levelmap[indexinarray] != 1112
                )
                {
                    levelmap[indexinarray] = 1104;
                }
            }
            if (istypeoflt == 1 && istypeoft == -1 && istypeofrt == 1 &&
                   istypeofl == 1 && istypeofr == 1 &&
                   istypeofb == 0)
            {
                int xx = (x);
                int yy = (y);
                int zz = (z);

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

                int indexinarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                //levelmapsortingtiles[indexinarray] = 1104;
                if (levelmap[indexinarray] != 1101 &&
                levelmap[indexinarray] != 1102 &&
                levelmap[indexinarray] != 1103 &&
                levelmap[indexinarray] != 1104 &&
                levelmap[indexinarray] != 1105 &&
                levelmap[indexinarray] != 1106 &&
                levelmap[indexinarray] != 1107 &&
                levelmap[indexinarray] != 1108 &&
                levelmap[indexinarray] != 1109 &&
                levelmap[indexinarray] != 1110 &&
                levelmap[indexinarray] != 1111 &&
                levelmap[indexinarray] != 1112
                )
                {
                    levelmap[indexinarray] = 1104;
                }
            }
            if (istypeoflt == -1 && istypeoft == -1 && istypeofrt == -1 &&
               istypeofl == 1 &&                        istypeofr == 1 &&
                                    istypeofb == 1)
            {
                int xx = (x);
                int yy = (y);
                int zz = (z);

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

                int indexinarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                //levelmapsortingtiles[indexinarray] = 1104;
                if (levelmap[indexinarray] != 1101 &&
                 levelmap[indexinarray] != 1102 &&
                 levelmap[indexinarray] != 1103 &&
                 levelmap[indexinarray] != 1104 &&
                 levelmap[indexinarray] != 1105 &&
                 levelmap[indexinarray] != 1106 &&
                 levelmap[indexinarray] != 1107 &&
                 levelmap[indexinarray] != 1108 &&
                 levelmap[indexinarray] != 1109 &&
                 levelmap[indexinarray] != 1110 &&
                 levelmap[indexinarray] != 1111 &&
                 levelmap[indexinarray] != 1112
                 )
                {
                    levelmap[indexinarray] = 1104;
                }
            }

            /////////BUILD WALL BACK/////////////////
            */



            /////////BUILD WALL FRONT/////////////////
            /*if (                    istypeoft == 0  &&
                 istypeofl == 1                       && istypeofr == 1 &&
                 istypeoflb == -1 && istypeofb == -1 && istypeofrb == -1)
            {
                int xx = (x);
                int yy = (y);
                int zz = (z);

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

                int indexinarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                //levelmapsortingtiles[indexinarray] = 1104;
                if (levelmap[indexinarray] != 1101 &&
                 levelmap[indexinarray] != 1102 &&
                 levelmap[indexinarray] != 1103 &&
                 levelmap[indexinarray] != 1104 &&
                 levelmap[indexinarray] != 1105 &&
                 levelmap[indexinarray] != 1106 &&
                 levelmap[indexinarray] != 1107 &&
                 levelmap[indexinarray] != 1108 &&
                 levelmap[indexinarray] != 1109 &&
                 levelmap[indexinarray] != 1110 &&
                 levelmap[indexinarray] != 1111 &&
                 levelmap[indexinarray] != 1112
                 )
                {
                    levelmap[indexinarray] = 1103;
                }
            }
            if (                 istypeoft == 0     &&
              istypeofl == 1 &&                         istypeofr == 1 &&
              istypeoflb == 1 && istypeofb == -1 && istypeofrb == -1)
            {
                int xx = (x);
                int yy = (y);
                int zz = (z);

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

                int indexinarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                //levelmapsortingtiles[indexinarray] = 1104;
                if (levelmap[indexinarray] != 1101 &&
                levelmap[indexinarray] != 1102 &&
                levelmap[indexinarray] != 1103 &&
                levelmap[indexinarray] != 1104 &&
                levelmap[indexinarray] != 1105 &&
                levelmap[indexinarray] != 1106 &&
                levelmap[indexinarray] != 1107 &&
                levelmap[indexinarray] != 1108 &&
                levelmap[indexinarray] != 1109 &&
                levelmap[indexinarray] != 1110 &&
                levelmap[indexinarray] != 1111 &&
                levelmap[indexinarray] != 1112
                )
                {
                    levelmap[indexinarray] = 1103;
                }
            }

            if (                    istypeoft == 0 &&
              istypeofl == 1 &&                     istypeofr == 1 &&
              istypeoflb == -1 && istypeofb == -1 && istypeofrb == 1)
            {
                int xx = (x);
                int yy = (y);
                int zz = (z);

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

                int indexinarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                //levelmapsortingtiles[indexinarray] = 1104;
                if (levelmap[indexinarray] != 1101 &&
                levelmap[indexinarray] != 1102 &&
                levelmap[indexinarray] != 1103 &&
                levelmap[indexinarray] != 1104 &&
                levelmap[indexinarray] != 1105 &&
                levelmap[indexinarray] != 1106 &&
                levelmap[indexinarray] != 1107 &&
                levelmap[indexinarray] != 1108 &&
                levelmap[indexinarray] != 1109 &&
                levelmap[indexinarray] != 1110 &&
                levelmap[indexinarray] != 1111 &&
                levelmap[indexinarray] != 1112
                )
                {
                    levelmap[indexinarray] = 1103;
                }
            }
            if (                     istypeoft == 0 &&
               istypeofl == 1 &&                       istypeofr == 1 &&
               istypeoflb == 1 && istypeofb == -1 && istypeofrb == 1)
            {
                int xx = (x);
                int yy = (y);
                int zz = (z);

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

                int indexinarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                //levelmapsortingtiles[indexinarray] = 1104;
                if (levelmap[indexinarray] != 1101 &&
                levelmap[indexinarray] != 1102 &&
                levelmap[indexinarray] != 1103 &&
                levelmap[indexinarray] != 1104 &&
                levelmap[indexinarray] != 1105 &&
                levelmap[indexinarray] != 1106 &&
                levelmap[indexinarray] != 1107 &&
                levelmap[indexinarray] != 1108 &&
                levelmap[indexinarray] != 1109 &&
                levelmap[indexinarray] != 1110 &&
                levelmap[indexinarray] != 1111 &&
                levelmap[indexinarray] != 1112
                )
                {
                    levelmap[indexinarray] = 1103;
                }
            }
            if (istypeoft == 1 &&
                 istypeofl == 1 && istypeofr == 1 &&
                 istypeoflb == -1 && istypeofb == -1 && istypeofrb == -1)
            {
                int xx = (x);
                int yy = (y);
                int zz = (z);

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

                int indexinarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                //levelmapsortingtiles[indexinarray] = 1104;
                if (levelmap[indexinarray] != 1101 &&
                 levelmap[indexinarray] != 1102 &&
                 levelmap[indexinarray] != 1103 &&
                 levelmap[indexinarray] != 1104 &&
                 levelmap[indexinarray] != 1105 &&
                 levelmap[indexinarray] != 1106 &&
                 levelmap[indexinarray] != 1107 &&
                 levelmap[indexinarray] != 1108 &&
                 levelmap[indexinarray] != 1109 &&
                 levelmap[indexinarray] != 1110 &&
                 levelmap[indexinarray] != 1111 &&
                 levelmap[indexinarray] != 1112
                 )
                {
                    levelmap[indexinarray] = 1103;
                }
            }*/
            //////BUILD WALL FRONT






            /*
            /////////BUILD WALL LEFT FRONT INSIDE/////////////////
            if (istypeoft == -1 &&
               istypeofl == -1 && istypeofr == 1 &&
                                  istypeofb == 1)
            {
                int xx = (x);
                int yy = (y);
                int zz = (z);

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

                int indexinarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                //levelmapsortingtiles[indexinarray] = 1105;
                if (levelmap[indexinarray] != 1101 &&
               levelmap[indexinarray] != 1102 &&
               levelmap[indexinarray] != 1103 &&
               levelmap[indexinarray] != 1104 &&
               levelmap[indexinarray] != 1105 &&
               levelmap[indexinarray] != 1106 &&
               levelmap[indexinarray] != 1107 &&
               levelmap[indexinarray] != 1108 &&
               levelmap[indexinarray] != 1109 &&
               levelmap[indexinarray] != 1110 &&
               levelmap[indexinarray] != 1111 &&
               levelmap[indexinarray] != 1112
               )
                {
                    levelmap[indexinarray] = 1105;
                }
            }

            /////////BUILD WALL RIGHT FRONT INSIDE/////////////////
            if (istypeoft == -1 &&
               istypeofl == 1 && istypeofr == -1 &&
                                  istypeofb == 1)
            {
                int xx = (x);
                int yy = (y);
                int zz = (z);

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

                int indexinarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                //levelmapsortingtiles[indexinarray] = 1106;
                if (levelmap[indexinarray] != 1101 &&
                levelmap[indexinarray] != 1102 &&
                levelmap[indexinarray] != 1103 &&
                levelmap[indexinarray] != 1104 &&
                levelmap[indexinarray] != 1105 &&
                levelmap[indexinarray] != 1106 &&
                levelmap[indexinarray] != 1107 &&
                levelmap[indexinarray] != 1108 &&
                levelmap[indexinarray] != 1109 &&
                levelmap[indexinarray] != 1110 &&
                levelmap[indexinarray] != 1111 &&
                levelmap[indexinarray] != 1112
                )
                {
                    levelmap[indexinarray] = 1106;
                }
            }


            /////////BUILD WALL LEFT BACK INSIDE/////////////////
            if (istypeoft == 1 &&
               istypeofl == -1 && istypeofr == 1 &&
                                  istypeofb == -1)
            {
                int xx = (x);
                int yy = (y);
                int zz = (z);

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

                int indexinarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                //levelmapsortingtiles[indexinarray] = 1107;
                if (levelmap[indexinarray] != 1101 &&
              levelmap[indexinarray] != 1102 &&
              levelmap[indexinarray] != 1103 &&
              levelmap[indexinarray] != 1104 &&
              levelmap[indexinarray] != 1105 &&
              levelmap[indexinarray] != 1106 &&
              levelmap[indexinarray] != 1107 &&
              levelmap[indexinarray] != 1108 &&
              levelmap[indexinarray] != 1109 &&
              levelmap[indexinarray] != 1110 &&
              levelmap[indexinarray] != 1111 &&
              levelmap[indexinarray] != 1112
              )
                {
                    levelmap[indexinarray] = 1107;
                }
            }

            /////////BUILD WALL LEFT BACK INSIDE/////////////////
            if (istypeoft == 1 &&
               istypeofl == 1 && istypeofr == -1 &&
                                  istypeofb == -1)
            {
                int xx = (x);
                int yy = (y);
                int zz = (z);

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

                int indexinarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                //levelmapsortingtiles[indexinarray] = 1108;
                if (levelmap[indexinarray] != 1101 &&
              levelmap[indexinarray] != 1102 &&
              levelmap[indexinarray] != 1103 &&
              levelmap[indexinarray] != 1104 &&
              levelmap[indexinarray] != 1105 &&
              levelmap[indexinarray] != 1106 &&
              levelmap[indexinarray] != 1107 &&
              levelmap[indexinarray] != 1108 &&
              levelmap[indexinarray] != 1109 &&
              levelmap[indexinarray] != 1110 &&
              levelmap[indexinarray] != 1111 &&
              levelmap[indexinarray] != 1112
              )
                {
                    levelmap[indexinarray] = 1108;
                }
            }





            /////////BUILD WALL LEFT FRONT OUTSIDE/////////////////
            if (istypeoflt == -1 && istypeoft == 1 &&
               istypeofl == 1 && istypeofr == 0 &&
                                  istypeofb == 0)
            {
                int xx = (x);
                int yy = (y);
                int zz = (z);

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

                int indexinarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                //levelmapsortingtiles[indexinarray] = 1109;
                if (levelmap[indexinarray] != 1101 &&
              levelmap[indexinarray] != 1102 &&
              levelmap[indexinarray] != 1103 &&
              levelmap[indexinarray] != 1104 &&
              levelmap[indexinarray] != 1105 &&
              levelmap[indexinarray] != 1106 &&
              levelmap[indexinarray] != 1107 &&
              levelmap[indexinarray] != 1108 &&
              levelmap[indexinarray] != 1109 &&
              levelmap[indexinarray] != 1110 &&
              levelmap[indexinarray] != 1111 &&
              levelmap[indexinarray] != 1112
              )
                {
                    levelmap[indexinarray] = 1109;
                }
            }

            /////////BUILD WALL RIGHT FRONT OUTSIDE/////////////////
            if (istypeoft == 1 && istypeofrt == -1 &&
               istypeofl == 0 && istypeofr == 1 &&
                                  istypeofb == 0)
            {
                int xx = (x);
                int yy = (y);
                int zz = (z);

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

                int indexinarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                //levelmapsortingtiles[indexinarray] = 1110;
                if (levelmap[indexinarray] != 1101 &&
              levelmap[indexinarray] != 1102 &&
              levelmap[indexinarray] != 1103 &&
              levelmap[indexinarray] != 1104 &&
              levelmap[indexinarray] != 1105 &&
              levelmap[indexinarray] != 1106 &&
              levelmap[indexinarray] != 1107 &&
              levelmap[indexinarray] != 1108 &&
              levelmap[indexinarray] != 1109 &&
              levelmap[indexinarray] != 1110 &&
              levelmap[indexinarray] != 1111 &&
              levelmap[indexinarray] != 1112
              )
                {
                    levelmap[indexinarray] = 1110;
                }
            }


            /////////BUILD WALL LEFT BACK OUTSIDE/////////////////
            if (istypeoft == 0 &&
               istypeofl == 1 && istypeofr == 0 &&
                 istypeoflb == -1 && istypeofb == 1)
            {

                int xx = (x);
                int yy = (y);
                int zz = (z);

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

                int indexinarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                //levelmapsortingtiles[indexinarray] = 1111;
                if (levelmap[indexinarray] != 1101 &&
               levelmap[indexinarray] != 1102 &&
               levelmap[indexinarray] != 1103 &&
               levelmap[indexinarray] != 1104 &&
               levelmap[indexinarray] != 1105 &&
               levelmap[indexinarray] != 1106 &&
               levelmap[indexinarray] != 1107 &&
               levelmap[indexinarray] != 1108 &&
               levelmap[indexinarray] != 1109 &&
               levelmap[indexinarray] != 1110 &&
               levelmap[indexinarray] != 1111 &&
               levelmap[indexinarray] != 1112
               )
                {
                    levelmap[indexinarray] = 1111;
                }
            }



            /////////BUILD WALL RIGHT FRONT OUTSIDE/////////////////
            if (istypeoft == 0 &&
               istypeofl == 0 && istypeofr == 1 &&
                                  istypeofb == 1 && istypeofrb == -1)
            {
                int xx = (x);
                int yy = (y);
                int zz = (z);

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

                int indexinarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles
                                                                            //Console.WriteLine("built wall");
                                                                            //levelmapsortingtiles[indexinarray] = 1112;
                if (levelmap[indexinarray] != 1101 &&
                    levelmap[indexinarray] != 1102 &&
                    levelmap[indexinarray] != 1103 &&
                    levelmap[indexinarray] != 1104 &&
                    levelmap[indexinarray] != 1105 &&
                    levelmap[indexinarray] != 1106 &&
                    levelmap[indexinarray] != 1107 &&
                    levelmap[indexinarray] != 1108 &&
                    levelmap[indexinarray] != 1109 &&
                    levelmap[indexinarray] != 1110 &&
                    levelmap[indexinarray] != 1111 &&
                    levelmap[indexinarray] != 1112
                    )
                {
                    levelmap[indexinarray] = 1112;
                }
            }*/
        }
    }
}
