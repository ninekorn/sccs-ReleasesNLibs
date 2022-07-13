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

        int loadmap = 1;

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









            if (loadmap == 0)
            {


                int minw = 10;
                int minh = 9;
                int mind = 10;

                int maxw = 15;
                int maxh = 12;
                int maxd = 15;

                /*int minw = 2;
                int minh = 2;
                int mind = 2;

                int maxw = 3;
                int maxh = 3;
                int maxd = 3;*/

                maxx = (int)sc_maths.getSomeRandNumThousandDecimal(minw, maxw, 1, 0, -1);
                maxy = (int)sc_maths.getSomeRandNumThousandDecimal(minh, maxh, 1, 0, -1);
                maxz = (int)sc_maths.getSomeRandNumThousandDecimal(mind, maxd, 1, 0, -1);

                minx = (int)sc_maths.getSomeRandNumThousandDecimal(minw, maxw, 1, 0, -1);
                miny = 0;// (int)sc_maths.getSomeRandNumThousandDecimal(9, 13, 1, 2, 1);
                minz = (int)sc_maths.getSomeRandNumThousandDecimal(mind, maxd, 1, 0, -1);

                minx *= -1;
                minz *= -1;


                Console.WriteLine("/minx:" + (minx) + "/miny:" + (miny) + "/minz:" + (minz) + "/maxx:" + (maxx) + "/maxy:" + (maxy) + "/maxz:" + (maxz));

                /*minx = (int)sc_maths.getSomeRandNumThousandDecimal(1, 2, 1, 2, 1);
                miny = 0;// (int)sc_maths.getSomeRandNumThousandDecimal(9, 13, 1, 2, 1);
                minz = (int)sc_maths.getSomeRandNumThousandDecimal(1, 2, 1, 2, 1);*/

                /*maxx = minx + somerw;
                maxy = miny + somerh;
                maxz = minz + somerd;*/
                /*
                maxx = somerw;
                maxy = somerh;
                maxz = somerd;*/




                wallheightsize = 10;

                //if minx == -2 and maxx = 0 , somewidth != minx + maxx.... somewidth = minx + maxx + 1... there are 3 indexes, 0 -1 -2 or 0 1 2 so somwidth = 3

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

                //somewidth +=1;
                //someheight += 1;
                //somedepth += 1;

                //Console.WriteLine(minx + "/maxx:" + maxx + "/w:" + somewidth);
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












                //int sometot0 = somewidth * someheight * (-minz + maxz);
                //int sometot1 = somewidth * someheight * somedepth;
                //Console.WriteLine("tot0:" + sometot0  + "/tot1:" + sometot1);


                int outofrange = 0;

                /* for (var i = 0; i < levelmap.Length; i++)
                 {

                     levelmap[i] = -9;
                 }
                 */

                int[] leveltypes = new int[wallheightsize];
                for (int i = 0; i < leveltypes.Length; i++)
                {
                    leveltypes[i] = (i) * -1;
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

                            if (indexinarray < somewidth * someheight * somedepth)
                            {
                                if (y == 0)
                                {
                                    levelmap[indexinarray] = 999;
                                    levelmapsortingtiles[indexinarray] = 999;
                                    levelmapsortingtilesremains[indexinarray] = 999;
                                    adjacenttiles[indexinarray] = 999;
                                }
                                else
                                {
                                    levelmap[indexinarray] = y * -1;
                                    levelmapsortingtiles[indexinarray] = y * -1;
                                    levelmapsortingtilesremains[indexinarray] = y * -1;
                                    adjacenttiles[indexinarray] = y * -1;
                                }

                                /*for (int i = 0; i < leveltypes.Length; i++)
                                {
                                    if (leveltypes[i] * -1 == y)
                                    {
                                        if (y == 0)
                                        {
                                            levelmap[indexinarray] = 999;
                                            levelmapsortingtiles[indexinarray] = 999;
                                            levelmapsortingtilesremains[indexinarray] = 999;
                                            adjacenttiles[indexinarray] = 999;
                                        }
                                        else
                                        {
                                            levelmap[indexinarray] = leveltypes[i];
                                            levelmapsortingtiles[indexinarray] = leveltypes[i];
                                            levelmapsortingtilesremains[indexinarray] = leveltypes[i];
                                            adjacenttiles[indexinarray] = leveltypes[i];
                                        }
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }*/





                                /*
                                if (x > minx + 2 && x < maxx - 2 &&
                                    z > minz + 2 && z < maxz - 2)
                                {
                                    for (int i = 0; i < leveltypes.Length; i++)
                                    {
                                        if (leveltypes[i] * -1 == y)
                                        {
                                            if (y == 0)
                                            {
                                                levelmap[indexinarray] = 999;
                                                levelmapsortingtiles[indexinarray] = 999;
                                                levelmapsortingtilesremains[indexinarray] = 999;
                                                adjacenttiles[indexinarray] = 999;
                                            }
                                            else
                                            {
                                                levelmap[indexinarray] = leveltypes[i];
                                                levelmapsortingtiles[indexinarray] = leveltypes[i];
                                                levelmapsortingtilesremains[indexinarray] = leveltypes[i];
                                                adjacenttiles[indexinarray] = leveltypes[i];
                                            }
                                        }
                                        else
                                        {
                                            continue;
                                        }
                                    }
                                }
                                else
                                {
                                    for (int i = 0; i < leveltypes.Length; i++)
                                    {
                                        if (leveltypes[i] * -1 == y)
                                        {
                                            if (y == 0)
                                            {
                                                levelmap[indexinarray] = 991;
                                                levelmapsortingtiles[indexinarray] = 991;
                                                levelmapsortingtilesremains[indexinarray] = 991;
                                                adjacenttiles[indexinarray] = 991;
                                            }
                                            else
                                            {
                                                levelmap[indexinarray] = leveltypes[i];
                                                levelmapsortingtiles[indexinarray] = leveltypes[i];
                                                levelmapsortingtilesremains[indexinarray] = leveltypes[i];
                                                adjacenttiles[indexinarray] = leveltypes[i];
                                            }
                                        }
                                        else
                                        {
                                            continue;
                                        }
                                    }
                                }*/
                            }
                            else
                            {
                                Console.WriteLine("index is out of range");
                            }
                        }
                    }
                }







                int neighbooraddx = 1;
                int neighbooraddz = 1;







































                int countermaxtile = 0;
                /*for (var x = (minx); x < maxx; x++)
                {
                    for (var y = (miny); y < maxy; y++)
                    {
                        for (var z = (minz); z < maxz; z++)
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

                            if (indexinlevelarray < somewidth * someheight * somedepth)
                            {

                            }
                        }
                    }
                }*/






                for (int t = 0; t < maxtileamount; t++)
                {
                    for (int p = 0; p < somepointermax; p++)
                    {
                        outofrange = 0;
                        for (int xi = -neighbooraddx; xi <= neighbooraddx; xi++)
                        {
                            //for (int yi = y; yi <= y; yi++)
                            {
                                for (int zi = -neighbooraddz; zi <= neighbooraddz; zi++)
                                {

                                    if (countermaxtile >= maxtileamount)
                                    {
                                        Console.WriteLine("reached max0");


                                        break;
                                    }
                                    else
                                    {
                                        int neighboorx = listoftileloc[p][0] + xi;
                                        //int neighboory = y;
                                        int neighboorz = listoftileloc[p][2] + zi;

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

                                        //int indexinarray = xxi + somewidth * (yyi + someheight * zzi); //y is always 0 on floor tiles
                                        int indexinarray = xxi + somewidth * (yyi + someheight * zzi); //y is always 0 on floor tiles

                                        if (indexinarray < somewidth * someheight * somedepth)
                                        {
                                            //levelmap[indexinarray] = 0;

                                            if (levelmap[indexinarray] == 999)
                                            {
                                                //Console.WriteLine(indexinarray);
                                                levelmapsortingtilesremains[indexinarray] = 0;
                                                levelmapsortingtiles[indexinarray] = 0;
                                                //Console.WriteLine(listoftileloc[p][1]);
                                                levelmap[indexinarray] = 0;
                                                adjacenttiles[indexinarray] = 0;
                                                countermaxtile++;
                                            }
                                        }
                                        else
                                        {
                                            /*levelmapsortingtilesremains[indexinlevelarray] = 0;
                                            levelmapsortingtiles[indexinlevelarray] = 0;
                                            //Console.WriteLine(listoftileloc[p][1]);
                                            levelmap[indexinlevelarray] = 0;
                                            adjacenttiles[indexinlevelarray] = 0;*/
                                        }
                                    }
                                }
                            }
                        }



                        int dirlr = (int)Math.Floor(sc_maths.getSomeRandNumThousandDecimal(0, 2, 1, 0, 0));
                        int dirfb = (int)Math.Floor(sc_maths.getSomeRandNumThousandDecimal(0, 2, 1, 0, 0));
                        //float dirldrd = (float)sc_maths.getSomeRandNumThousandDecimal(0, 2, 0.1f, 0, 0);
                        //float dirfdbd = (float)sc_maths.getSomeRandNumThousandDecimal(0, 2, 0.1f, 0, 0);

                        int decidedir = (int)Math.Floor(sc_maths.getSomeRandNumThousandDecimal(0, 2, 1, 0, 0));

                        //Console.WriteLine("0:" + dirlr + "/1:" + dirfb + "/2:" + decidedir);

                        if (decidedir == 0)
                        {
                            if (dirlr == 0)
                            {

                                int[] somevec = new int[3]; //listoftileloc[p];
                                somevec[0] = listoftileloc[p][0] - 1;
                                somevec[1] = listoftileloc[p][1];
                                somevec[2] = listoftileloc[p][2];

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
                                int[] somevec = new int[3]; //listoftileloc[p];
                                somevec[0] = listoftileloc[p][0] + 1;
                                somevec[1] = listoftileloc[p][1];
                                somevec[2] = listoftileloc[p][2];

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

                                int[] somevec = new int[3]; //listoftileloc[p];
                                somevec[0] = listoftileloc[p][0];
                                somevec[1] = listoftileloc[p][1];
                                somevec[2] = listoftileloc[p][2] - 1;

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
                                int[] somevec = new int[3]; //listoftileloc[p];
                                somevec[0] = listoftileloc[p][0];
                                somevec[1] = listoftileloc[p][1];
                                somevec[2] = listoftileloc[p][2] + 1;

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


















                        /*if (outofrange == 0)
                        {

                        }
                        else
                        {
                            int[] somevec = new int[3];
                            somevec[0] = 0;
                            somevec[1] = 0;
                            somevec[2] = 0;

                            listoftileloc[p] = somevec;// Vector3.Zero;
                        }*/
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

                            if (levelmap[indexinlevelarray] == 0)
                            {
                                checkAllSides(x, y, z);
                            }
                        }
                    }
                }

                createfinal();


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
            else
            {

                string pathofrelease0 = Directory.GetCurrentDirectory();
                //Console.WriteLine(pathofrelease);
                string pathofchunkmap0 = pathofrelease0 + @"\chunkmaps\";

                if (!Directory.Exists(pathofchunkmap0))
                {
                    //Console.WriteLine("created directory");
                    Directory.CreateDirectory(pathofchunkmap0);
                }

                //int writetofilecounter = 0;

                System.Globalization.CultureInfo customCulture0 = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
                customCulture0.NumberFormat.NumberDecimalSeparator = ".";
                System.Threading.Thread.CurrentThread.CurrentCulture = customCulture0;




                //LOADING CHUNK BACK INTO MEMORY
                //LOADING CHUNK BACK INTO MEMORY
                string path0 = pathofrelease0 + @"\chunkmaps\" + "levelfloordata.xml" ;

                //https://stackoverflow.com/questions/18891207/how-to-get-value-from-a-specific-child-element-in-xml-using-xmlreader
                //var path = @"C:\Users\steve\Desktop\#chunkmaps\" + "chunkmap" + writetofilecounter + ".xml";
                var reader = new XmlTextReader(path0);


                if (reader.ReadToDescendant("w"))
                {
                    reader.Read();//this moves reader to next node which is text 
                    var result = reader.Value; //this might give value than 

                    //https://stackoverflow.com/questions/2959161/convert-string-to-int-array-using-linq
                    var someres = result.Split(' ').Select(n => Convert.ToInt32(n)).ToArray();

                    somewidth = someres[0];
                    Console.WriteLine(someres[0]);
                    //for (int by = 0; by < ia.Length; by++)
                    //{
                    //    Console.WriteLine(ia[by]);
                    //}
                }
                reader.Close();

                reader = new XmlTextReader(path0);
                if (reader.ReadToDescendant("h"))
                {
                    reader.Read();//this moves reader to next node which is text 
                    var result = reader.Value; //this might give value than 

                    //https://stackoverflow.com/questions/2959161/convert-string-to-int-array-using-linq
                    var someres = result.Split(' ').Select(n => Convert.ToInt32(n)).ToArray();

                    someheight = someres[0];
                    Console.WriteLine(someres[0]);

                    //for (int by = 0; by < ia.Length; by++)
                    //{
                    //    Console.WriteLine(ia[by]);
                    //}
                }
                reader.Close();


                reader = new XmlTextReader(path0);
                if (reader.ReadToDescendant("d"))
                {
                    reader.Read();//this moves reader to next node which is text 
                    var result = reader.Value; //this might give value than 

                    //https://stackoverflow.com/questions/2959161/convert-string-to-int-array-using-linq
                    var someres = result.Split(' ').Select(n => Convert.ToInt32(n)).ToArray();

                    somedepth = someres[0];
                    Console.WriteLine(someres[0]);
                    //for (int by = 0; by < ia.Length; by++)
                    //{
                    //    Console.WriteLine(ia[by]);
                    //}
                }
                reader.Close();


                reader = new XmlTextReader(path0);
                if (reader.ReadToDescendant("minx"))
                {
                    reader.Read();//this moves reader to next node which is text 
                    var result = reader.Value; //this might give value than 

                    //https://stackoverflow.com/questions/2959161/convert-string-to-int-array-using-linq
                    var someres = result.Split(' ').Select(n => Convert.ToInt32(n)).ToArray();

                    minx = someres[0];
                    Console.WriteLine(someres[0]);
                    //for (int by = 0; by < ia.Length; by++)
                    //{
                    //    Console.WriteLine(ia[by]);
                    //}
                }
                reader.Close();

                reader = new XmlTextReader(path0);
                if (reader.ReadToDescendant("maxx"))
                {
                    reader.Read();//this moves reader to next node which is text 
                    var result = reader.Value; //this might give value than 

                    //https://stackoverflow.com/questions/2959161/convert-string-to-int-array-using-linq
                    var someres = result.Split(' ').Select(n => Convert.ToInt32(n)).ToArray();

                    maxx = someres[0];
                    Console.WriteLine(someres[0]);
                    //for (int by = 0; by < ia.Length; by++)
                    //{
                    //    Console.WriteLine(ia[by]);
                    //}
                }
                reader.Close();


                reader = new XmlTextReader(path0);
                if (reader.ReadToDescendant("minz"))
                {
                    reader.Read();//this moves reader to next node which is text 
                    var result = reader.Value; //this might give value than 

                    //https://stackoverflow.com/questions/2959161/convert-string-to-int-array-using-linq
                    var someres = result.Split(' ').Select(n => Convert.ToInt32(n)).ToArray();

                    minz = someres[0];
                    Console.WriteLine(someres[0]);
                    //for (int by = 0; by < ia.Length; by++)
                    //{
                    //    Console.WriteLine(ia[by]);
                    //}
                }
                reader.Close();

                reader = new XmlTextReader(path0);
                if (reader.ReadToDescendant("maxz"))
                {
                    reader.Read();//this moves reader to next node which is text 
                    var result = reader.Value; //this might give value than 

                    //https://stackoverflow.com/questions/2959161/convert-string-to-int-array-using-linq
                    var someres = result.Split(' ').Select(n => Convert.ToInt32(n)).ToArray();

                    maxz = someres[0];
                    Console.WriteLine(someres[0]);
                    //for (int by = 0; by < ia.Length; by++)
                    //{
                    //    Console.WriteLine(ia[by]);
                    //}
                }
                reader.Close();

                reader = new XmlTextReader(path0);
                if (reader.ReadToDescendant("miny"))
                {
                    reader.Read();//this moves reader to next node which is text 
                    var result = reader.Value; //this might give value than 

                    //https://stackoverflow.com/questions/2959161/convert-string-to-int-array-using-linq
                    var someres = result.Split(' ').Select(n => Convert.ToInt32(n)).ToArray();

                    miny = someres[0];
                    //Console.WriteLine(someres[0]);
                    //for (int by = 0; by < ia.Length; by++)
                    //{
                    //    Console.WriteLine(ia[by]);
                    //}
                }
                reader.Close();

                reader = new XmlTextReader(path0);
                if (reader.ReadToDescendant("maxy"))
                {
                    reader.Read();//this moves reader to next node which is text 
                    var result = reader.Value; //this might give value than 

                    //https://stackoverflow.com/questions/2959161/convert-string-to-int-array-using-linq
                    var someres = result.Split(' ').Select(n => Convert.ToInt32(n)).ToArray();

                    maxy = someres[0];
                    Console.WriteLine(someres[0]);
                    //for (int by = 0; by < ia.Length; by++)
                    //{
                    //    Console.WriteLine(ia[by]);
                    //}
                }
                reader.Close();






                reader = new XmlTextReader(path0);
                if (reader.ReadToDescendant("intmap"))
                {
                    reader.Read();//this moves reader to next node which is text 
                    var result = reader.Value; //this might give value than 

                    //https://stackoverflow.com/questions/2959161/convert-string-to-int-array-using-linq
                    levelmap = result.Split(' ').Select(n => Convert.ToInt32(n)).ToArray();

                    /*for (int by = 0; by < levelmap.Length; by++)
                    {
                        Console.WriteLine(levelmap[by]);
                    }*/
                }
                reader.Close();




                adjacenttiles = new int[levelmap.Length];

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
                              levelmap[indexinlevelarray] == -99)
                            {
                                adjacenttiles[indexinlevelarray] = 1001;
                            }
                            else
                            {
                                adjacenttiles[indexinlevelarray] = levelmap[indexinlevelarray];
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

                            if (levelmap[indexinlevelarray] == 0)
                            {
                                checkAllSides(x, y, z);
                            }
                          
                        }
                    }
                }

               
                createfinal();







                //writetofilecounter = 0;
                /*for (int i = 0; i < arrayofchunks.Length; i++)
                {
                   
                }*/
                //LOADING CHUNK BACK INTO MEMORY
                //LOADING CHUNK BACK INTO MEMORY
            }






































        }




        int checky = 0;
        void checkAllSides(int xi, int yi, int zi)
        {
            int xx = xi;
            int yy = yi;
            int zz = zi;

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
            if (levelmap[indexinlevelarray] == 0 && xi == minx && yi == 0 ||
                levelmap[indexinlevelarray] == 0 && zi == minz && yi == 0 ||
                levelmap[indexinlevelarray] == 0 && xi == maxx - 1 && yi == 0 ||
                levelmap[indexinlevelarray] == 0 && zi == maxz - 1 && yi == 0 ||

                levelmap[indexinlevelarray] == 0 && xi == minx && zi == minz && yi == 0 ||
                levelmap[indexinlevelarray] == 0 && zi == minz && zi == maxz-1 && yi == 0 ||
                levelmap[indexinlevelarray] == 0 && xi == maxx - 1 && zi == minz && yi == 0 ||
                levelmap[indexinlevelarray] == 0 && zi == maxz - 1 && zi == maxz-1 && yi == 0 ||

                levelmap[indexinlevelarray] == 999 && xi == minx && yi == 0 ||
                levelmap[indexinlevelarray] == 999 && zi == minz && yi == 0 ||
                levelmap[indexinlevelarray] == 999 && xi == maxx - 1 && yi == 0 ||
                levelmap[indexinlevelarray] == 999 && zi == maxz - 1 && yi == 0 ||

                levelmap[indexinlevelarray] == 999 && xi == minx && zi == minz && yi == 0 ||
                levelmap[indexinlevelarray] == 999 && zi == minz && zi == maxz - 1 && yi == 0 ||
                levelmap[indexinlevelarray] == 999 && xi == maxx - 1 && zi == minz && yi == 0 ||
                levelmap[indexinlevelarray] == 999 && zi == maxz - 1 && zi == maxz - 1 && yi == 0)
            {
                Console.WriteLine("start found bordertile");

                adjacenttiles[indexinlevelarray] = 1001;
                //levelmapsortingtiles[indexinlevelarray] = 998;
            }


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
                                //adjacenttiles[indexinlevelarray] = 1001;
                                //levelmapsortingtiles[indexinlevelarray] = 998;

                                /*
                                ||
                                levelmap[indexinarray0] == 0 && xiii == minx ||
                                levelmap[indexinarray0] == 0 && ziii == minz ||
                                levelmap[indexinarray0] == 0 && xiii == maxx ||
                                levelmap[indexinarray0] == 0 && ziii == maxz ||
                                levelmap[indexinarray0] == 999 && xiii == minx ||
                                levelmap[indexinarray0] == 999 && ziii == minz ||
                                levelmap[indexinarray0] == 999 && xiii == maxx ||
                                levelmap[indexinarray0] == 999 && ziii == maxz*/






                                /*
                                //Console.WriteLine("testing function");
                                istypeofl = -2;
                                istypeofr = -2;
                                istypeoft = -2;
                                istypeofb = -2;

                                istypeoflt = -2;
                                istypeofrt = -2;
                                istypeoflb = -2;
                                istypeofrb = -2;


                                int tilex = checkx - 1;
                                int tiley = yi;
                                int tilez = checkz;

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
                                    if (levelmap[indextile] == 0)
                                    {
                                        istypeofl = 0;
                                    }
                                    else if (levelmap[indextile] == 999)
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
                                    istypeofl = -2;
                                }


                                if (istypeofl == 1 || istypeofl == -2)
                                {
                                    levelmapsortingtiles[indexinlevelarray] = 998;
                                    levelmapsortingtilesremains[indexinlevelarray] = 998;
                                    adjacenttiles[indexinlevelarray] = 1001;
                                }






                                tilex = checkx + 1;
                                tiley = yi;
                                tilez = checkz;

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
                                    if (levelmap[indextile] == 0)
                                    {
                                        istypeofr = 0;
                                    }
                                    else if (levelmap[indextile] == 999)
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
                                    istypeofr = -2;
                                }


                                if (istypeofr == 1 || istypeofr == -2)
                                {
                                    levelmapsortingtiles[indexinlevelarray] = 998;
                                    levelmapsortingtilesremains[indexinlevelarray] = 998;
                                    adjacenttiles[indexinlevelarray] = 1001;
                                }






                                tilex = checkx;
                                tiley = yi;
                                tilez = checkz + 1;


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
                                    if (levelmap[indextile] == 0)
                                    {
                                        istypeoft = 0;
                                    }
                                    else if (levelmap[indextile] == 999)
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
                                    istypeoft = -2;
                                }


                                if (istypeoft == 1 || istypeoft == -2)
                                {
                                    levelmapsortingtiles[indexinlevelarray] = 998;
                                    levelmapsortingtilesremains[indexinlevelarray] = 998;
                                    adjacenttiles[indexinlevelarray] = 1001;
                                }


                                tilex = checkx;
                                tiley = yi;
                                tilez = checkz - 1;

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
                                    if (levelmap[indextile] == 0)
                                    {
                                        istypeofb = 0;
                                    }
                                    else if (levelmap[indextile] == 999)
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
                                    istypeofb = -2;
                                }
                                /////LEFT RIGHT FRONT BACK


                                if (istypeofb == 1 || istypeofb == -2)
                                {
                                    levelmapsortingtiles[indexinlevelarray] = 998;
                                    levelmapsortingtilesremains[indexinlevelarray] = 998;
                                    adjacenttiles[indexinlevelarray] = 1001;
                                }


                                tilex = checkx - 1;
                                tiley = yi;
                                tilez = checkz - 1;


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
                                    if (levelmap[indextile] == 0)
                                    {
                                        istypeoflb = 0;
                                    }
                                    else if (levelmap[indextile] == 999)
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
                                    istypeoflb = -2;
                                }


                                if (istypeoflb == 1 || istypeoflb == -2)
                                {
                                    levelmapsortingtiles[indexinlevelarray] = 998;
                                    levelmapsortingtilesremains[indexinlevelarray] = 998;
                                    adjacenttiles[indexinlevelarray] = 1001;
                                }


                                tilex = checkx + 1;
                                tiley = yi;
                                tilez = checkz - 1;


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
                                    if (levelmap[indextile] == 0)
                                    {
                                        istypeofrb = 0;
                                    }
                                    else if (levelmap[indextile] == 999)
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
                                    istypeofrb = -2;
                                }



                                if (istypeofrb == 1 || istypeofrb == -2)
                                {
                                    levelmapsortingtiles[indexinlevelarray] = 998;
                                    levelmapsortingtilesremains[indexinlevelarray] = 998;
                                    adjacenttiles[indexinlevelarray] = 1001;
                                }








                                tilex = checkx - 1;
                                tiley = yi;
                                tilez = checkz + 1;


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
                                    if (levelmap[indextile] == 0)
                                    {
                                        istypeoflt = 0;
                                    }
                                    else if (levelmap[indextile] == 999)
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
                                    istypeoflt = -2;
                                }


                                if (istypeoflt == 1 || istypeoflt == -2)
                                {
                                    levelmapsortingtiles[indexinlevelarray] = 998;
                                    levelmapsortingtilesremains[indexinlevelarray] = 998;
                                    adjacenttiles[indexinlevelarray] = 1001;
                                }


                                tilex = checkx + 1;
                                tiley = yi;
                                tilez = checkz + 1;


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
                                    if (levelmap[indextile] == 0)
                                    {
                                        istypeofrt = 0;
                                    }
                                    else if (levelmap[indextile] == 999)
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
                                    istypeofrt = -2;
                                }

                                if (istypeofrt == 1 || istypeofrt == -2)
                                {
                                    levelmapsortingtiles[indexinlevelarray] = 998;
                                    levelmapsortingtilesremains[indexinlevelarray] = 998;
                                    adjacenttiles[indexinlevelarray] = 1001;
                                }*/











                                /*//walls to the right
                                /////////////////////////////////////////////////////////////
                                if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
                                    istypeofl == 0 &&   istypeofr == 0 &&
                                    istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0)
                                {

                                    xx = (checkx);
                                    yy = (yi);
                                    zz = (checkz);

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
                                /*else
                                {
                                     xx = (checkx);
                                     yy = (yi);
                                     zz = (checkz);

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

                                    levelmapsortingtiles[indexinarray] = 998; //997
                                }*/
                            }
                            /*else
                            {
                                /*if (levelmap[indexinarray0] != 0)
                                {
                                    //Console.WriteLine("test");

                                    xx = (checkx);
                                    yy = (yi);
                                    zz = (checkz);

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

                                    int indexinarray = xx + somewidth * (yy + someheight * zz);
                                    //
                                    //levelmapsortingtiles[indexinarray] = 998; //997
                                    adjacenttiles[indexinarray] = 1001;
                                }
                            }*/
                        }
                        else
                        {
                            //levelmapsortingtiles[indexinlevelarray] = 998; //997
                            adjacenttiles[indexinlevelarray] = 1001;
                            Console.WriteLine("out of range tile");
                        }
                    }
                }
            }
        }








        public void checkforbordertiles(int x, int y, int z)
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
                if (levelmap[indextile] == 0)
                {
                    istypeofl = 0;
                }
                else if (levelmap[indextile] == 999 )
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
                istypeofl = -2;
            }


            if (istypeofl == 1 || istypeofl == -2)
            {
                levelmapsortingtiles[indexinarray] = 998;
                levelmapsortingtilesremains[indexinarray] = 998;
                adjacenttiles[indexinarray] = 1001;
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
                if (levelmap[indextile] == 0)
                {
                    istypeofr = 0;
                }
                else if (levelmap[indextile] == 999 )
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
                istypeofr = -2;
            }


            if (istypeofr == 1 || istypeofr == -2)
            {
                levelmapsortingtiles[indexinarray] = 998;
                levelmapsortingtilesremains[indexinarray] = 998;
                adjacenttiles[indexinarray] = 1001;
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
                if (levelmap[indextile] == 0)
                {
                    istypeoft = 0;
                }
                else if (levelmap[indextile] == 999 )
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
                istypeoft = -2;
            }


            if (istypeoft == 1 || istypeoft == -2)
            {
                levelmapsortingtiles[indexinarray] = 998;
                levelmapsortingtilesremains[indexinarray] = 998;
                adjacenttiles[indexinarray] = 1001;
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
                if (levelmap[indextile] == 0)
                {
                    istypeofb = 0;
                }
                else if (levelmap[indextile] == 999 )
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
                istypeofb = -2;
            }
            /////LEFT RIGHT FRONT BACK


            if (istypeofb == 1 || istypeofb == -2)
            {
                levelmapsortingtiles[indexinarray] = 998;
                levelmapsortingtilesremains[indexinarray] = 998;
                adjacenttiles[indexinarray] = 1001;
            }


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
                if (levelmap[indextile] == 0)
                {
                    istypeoflb = 0;
                }
                else if (levelmap[indextile] == 999 )
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
                istypeoflb = -2;
            }


            if (istypeoflb == 1 || istypeoflb == -2)
            {
                levelmapsortingtiles[indexinarray] = 998;
                levelmapsortingtilesremains[indexinarray] = 998;
                adjacenttiles[indexinarray] = 1001;
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
                if (levelmap[indextile] == 0)
                {
                    istypeofrb = 0;
                }
                else if (levelmap[indextile] == 999 )
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
                istypeofrb = -2;
            }



            if (istypeofrb == 1 || istypeofrb == -2)
            {
                levelmapsortingtiles[indexinarray] = 998;
                levelmapsortingtilesremains[indexinarray] = 998;
                adjacenttiles[indexinarray] = 1001;
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
                if (levelmap[indextile] == 0)
                {
                    istypeoflt = 0;
                }
                else if (levelmap[indextile] == 999 )
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
                istypeoflt = -2;
            }


            if (istypeoflt == 1 || istypeoflt == -2)
            {
                levelmapsortingtiles[indexinarray] = 998;
                levelmapsortingtilesremains[indexinarray] = 998;
                adjacenttiles[indexinarray] = 1001;
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
                if (levelmap[indextile] == 0)
                {
                    istypeofrt = 0;
                }
                else if (levelmap[indextile] == 999 )
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
                istypeofrt = -2;
            }

            if (istypeofrt == 1 || istypeofrt == -2)
            {
                levelmapsortingtiles[indexinarray] = 998;
                levelmapsortingtilesremains[indexinarray] = 998;
                adjacenttiles[indexinarray] = 1001;
            }


            /*

            //IF INDEX IS OUTOFRANGE
            if (istypeoflt == -2 || istypeoft == -2 || istypeofrt == -2 ||
                istypeofl == -2 ||  istypeofr == -2 ||
                istypeoflb == -2 || istypeofb == -2 || istypeofrb == -2)
            {
                levelmapsortingtiles[indexinarray] = 998;
                levelmapsortingtilesremains[indexinarray] = 998;
                adjacenttiles[indexinarray] = 1001;
                Console.WriteLine("tile is at the border of the size of map " + "/x:" + x + "/y:" + y + "/z:" + z);
            }
            //IF INDEX REVEALS TILES NOT SELECTED BY RANDOM WALKER. 
            else if (istypeoflt == 1 || istypeoft == 1 || istypeofrt == 1 ||
                istypeofl == 1 ||  istypeofr == 1 ||
                istypeoflb == 1 || istypeofb == 1 || istypeofrb == 1)
            {
                levelmapsortingtiles[indexinarray] = 998;
                levelmapsortingtilesremains[indexinarray] = 998;
                adjacenttiles[indexinarray] = 1001;
                Console.WriteLine("tile is at the border of the floor tiles " + "/x:" + x + "/y:" + y + "/z:" + z);
            }
            else
            {
                levelmapsortingtiles[indexinarray] = 0;
                levelmapsortingtilesremains[indexinarray] = 0;
            }*/

        }







        int counter = 1;
        public void createfinal()
        {
            if (counter == 1)
            {

                //int somemaxarray0 = somewidth * someheight * somedepth;
                //int somemaxarray1 = somewidth * someheight * (-minz + maxz);

                /*int somecounter = 0;

                int[] somearray = new int[somewidth * someheight * somedepth];
                //array index test
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

                            if (somearray[indexinlevelarray] != -1)
                            {
                                somearray[indexinlevelarray] = -1;
                            }
                            else
                            {
                                Console.WriteLine("setting same value again");
                            }
                            somecounter++;
                        }
                    }
                }

                int finalcounter = 0;
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

                            if (somearray[indexinlevelarray] != -1)
                            {
                                finalcounter++;
                            }                        
                        }
                    }
                }

                Console.WriteLine("final counter:" + finalcounter + "/countertiles:" + somecounter + "/totaltiles:" + (somewidth * someheight * somedepth));
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

                            if (levelmap[indexinlevelarray] == 0)// || levelmap[indexinlevelarray] == 0 || levelmap[indexinlevelarray] == 999 //adjacenttiles[indexinlevelarray] == 1001
                            {
                                checkforbordertiles(x, y, z);
                            }
                        }
                    }
                }*/
                





                
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
                                zz = zz + (maxz-1);
                            }

                            int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                            //if (levelmapsortingtiles[indexinlevelarray] == 998)
                            //{
                            if (adjacenttiles[indexinlevelarray] == 1001)// || levelmap[indexinlevelarray] == 0 || levelmap[indexinlevelarray] == 999
                            {
                                //levelmap[indexinlevelarray] = 1103;
                                buildWallsRerolled(x, y, z);
                                //buildWallsRerolledTHREE(x, y, z);
                                //buildWallsExceptOnFloor(x, y, z);
                            }
                        }
                    }
                }
                //ADJACENT TILE BUILD WALL TILES LEFT/RIGHT/FRONT/BACK/DIAGLEFT/DIAGRIGHT/DIAGFRONT/DIAGBACK/ OUT OF THAT
                //ADJACENT TILE BUILD WALL TILES LEFT/RIGHT/FRONT/BACK/DIAGLEFT/DIAGRIGHT/DIAGFRONT/DIAGBACK/ OUT OF THAT
                //ADJACENT TILE BUILD WALL TILES LEFT/RIGHT/FRONT/BACK/DIAGLEFT/DIAGRIGHT/DIAGFRONT/DIAGBACK/ OUT OF THAT


                //ADJACENT TILE BUILD WALL TILES LEFT/RIGHT/FRONT/BACK/DIAGLEFT/DIAGRIGHT/DIAGFRONT/DIAGBACK/ OUT OF THAT
                //ADJACENT TILE BUILD WALL TILES LEFT/RIGHT/FRONT/BACK/DIAGLEFT/DIAGRIGHT/DIAGFRONT/DIAGBACK/ OUT OF THAT
                //ADJACENT TILE BUILD WALL TILES LEFT/RIGHT/FRONT/BACK/DIAGLEFT/DIAGRIGHT/DIAGFRONT/DIAGBACK/ OUT OF THAT
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

                            //if (levelmapsortingtiles[indexinlevelarray] == 998)
                            //{
                            if (adjacenttiles[indexinlevelarray] == 1001)// || levelmap[indexinlevelarray] == 0 || levelmap[indexinlevelarray] == 999
                            {
                                //levelmap[indexinlevelarray] = 1103;
                                buildWallsRerolled(x, y, z);
                                //buildWallsRerolledTHREE(x, y, z);
                                //buildWallsExceptOnFloor(x, y, z);
                            }
                        }
                    }
                }*/
                //ADJACENT TILE BUILD WALL TILES LEFT/RIGHT/FRONT/BACK/DIAGLEFT/DIAGRIGHT/DIAGFRONT/DIAGBACK/ OUT OF THAT
                //ADJACENT TILE BUILD WALL TILES LEFT/RIGHT/FRONT/BACK/DIAGLEFT/DIAGRIGHT/DIAGFRONT/DIAGBACK/ OUT OF THAT
                //ADJACENT TILE BUILD WALL TILES LEFT/RIGHT/FRONT/BACK/DIAGLEFT/DIAGRIGHT/DIAGFRONT/DIAGBACK/ OUT OF THAT



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

                            //if (levelmapsortingtiles[indexinlevelarray] == 998)
                            //{
                            if (adjacenttiles[indexinlevelarray] == 1001 && 

                                levelmap[indexinlevelarray] != 1101 &&
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
                                levelmap[indexinlevelarray] = -99;
                            }// || levelmap[indexinlevelarray] == 0 || levelmap[indexinlevelarray] == 999
                                //buildWallsRerolled(x, y, z);
                                //buildWallsRerolledTHREE(x, y, z);
                                //buildWallsExceptOnFloor(x, y, z);
                            
                        }
                    }
                }
                //ADJACENT TILE BUILD WALL TILES LEFT/RIGHT/FRONT/BACK/DIAGLEFT/DIAGRIGHT/DIAGFRONT/DIAGBACK/ OUT OF THAT
                //ADJACENT TILE BUILD WALL TILES LEFT/RIGHT/FRONT/BACK/DIAGLEFT/DIAGRIGHT/DIAGFRONT/DIAGBACK/ OUT OF THAT
                //ADJACENT TILE BUILD WALL TILES LEFT/RIGHT/FRONT/BACK/DIAGLEFT/DIAGRIGHT/DIAGFRONT/DIAGBACK/ OUT OF THAT







              









































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

                /*
                //int somemaxarray0 = somewidth * someheight * somedepth;
                //int somemaxarray1 = somewidth * someheight * (-minz + maxz);
                //int somecounter = 0;
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
                                xx = xx + (maxx-1);
                            }

                            if (yy < 0)
                            {
                                yy *= -1;
                                yy = yy + (maxy-1);
                            }
                            if (zz < 0)
                            {
                                zz *= -1;
                                zz = zz + (maxz-1);
                            }

                            int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                            //Console.WriteLine("index:" + indexinlevelarray);
                            if (adjacenttiles[indexinlevelarray] == 1001) 
                            {
                                //Console.WriteLine("tile adjacent");
                                buildsomefloortiles(x, y, z); //,xx,yy,zz, indexinlevelarray
                                //buildWallsRerolled(x, y, z); //,xx,yy,zz, indexinlevelarray
                            }
                            //somecounter++;
                        }
                    }
                }
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

                                //levelmap[indexinlevelarray] = 1103;
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
                                //levelmap[indexinlevelarray] = 1103;
                                buildWallsRerolled(x, y, z);
                                //buildWallsRerolledTHREE(x, y, z);
                                //buildWallsExceptOnFloor(x, y, z);
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

                            toremove[indexinlevelarray] = adjacenttiles[indexinlevelarray];
                            
                            //if (adjacenttiles[indexinlevelarray] == 1001)
                            //{
                            //    toremove[indexinlevelarray] = adjacenttiles[indexinlevelarray];
                            //}
                            //else
                            //{
                            //   toremove[indexinlevelarray] = 995;
                            //}
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
                                    toremove[indexinlevelarray] = levelmap[indexinlevelarray];// adjacenttiles[indexinlevelarray];
                                }

                            }

                            if (levelmapsortingtilesremains[indexinlevelarray] == -1)
                            {
                                //if (toremove[indexinlevelarray] == 1001)
                                //{
                                    //toremove[indexinlevelarray] = 995;
                                    toremove[indexinlevelarray] = -1;// adjacenttiles[indexinlevelarray];
                                //}
                            }
                        }
                    }
                }
                //FROM TILES ALREADY DISCOVERED FLOOR AND WALL TILES, TAG TOREMOVE ARRAY AS TILES ALREADY DISCOVERED AND DISCARD AS 995
                //FROM TILES ALREADY DISCOVERED FLOOR AND WALL TILES, TAG TOREMOVE ARRAY AS TILES ALREADY DISCOVERED AND DISCARD AS 995



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

                            if (levelmapsortingtilesremains[indexinlevelarray] == -1)
                            {
                                //levelmap[indexinlevelarray] = 1103;
                                buildWallsRerolled(x, y, z);
                                //buildWallsRerolledTHREE(x, y, z);
                                //buildWallsExceptOnFloor(x, y, z);
                            }
                        }
                    }
                }*/
                //ADJACENT TILE BUILD WALL TILES LEFT/RIGHT/FRONT/BACK/DIAGLEFT/DIAGRIGHT/DIAGFRONT/DIAGBACK/ OUT OF THAT
                //ADJACENT TILE BUILD WALL TILES LEFT/RIGHT/FRONT/BACK/DIAGLEFT/DIAGRIGHT/DIAGFRONT/DIAGBACK/ OUT OF THAT
                //ADJACENT TILE BUILD WALL TILES LEFT/RIGHT/FRONT/BACK/DIAGLEFT/DIAGRIGHT/DIAGFRONT/DIAGBACK/ OUT OF THAT






















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
                                    //buildsomefloortiles(x,y,z);
                                }
                            }
                        }
                    }
                }*/























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
                else if (levelmapsortingtiles[indextile] == -1)
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
                else if (levelmapsortingtiles[indextile] == -1)
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
                else if (levelmapsortingtiles[indextile] == -1)
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
                else if (levelmapsortingtiles[indextile] == -1)
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
                else if (levelmapsortingtiles[indextile] == -1)
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
                else if (levelmapsortingtiles[indextile] == -1)
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
                else if (levelmapsortingtiles[indextile] == -1)
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
                else if (levelmapsortingtiles[indextile] == -1)
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
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0
                )
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


                    /*if (levelmap[indexinarray] != 1101 &&
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
                        levelmap[indexinarray] = 0;
                        levelmapsortingtiles[indexinarray] = 0;

                    }*/
                    levelmap[indexinarray] = 0;
                    levelmapsortingtiles[indexinarray] = 995;

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
                else if (levelmapsortingtiles[indextile] == -1)
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
                else if (levelmapsortingtiles[indextile] == -1)
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
                else if (levelmapsortingtiles[indextile] == -1)
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
                else if (levelmapsortingtiles[indextile] == -1)
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
                else if (levelmapsortingtiles[indextile] == -1)
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
                else if (levelmapsortingtiles[indextile] == -1)
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
                else if (levelmapsortingtiles[indextile] == -1)
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
                else if (levelmapsortingtiles[indextile] == -1)
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

                    if (levelmapsortingtiles[indexinarray] != -1)
                    {
                        levelmapsortingtiles[indexinarray] = -1;
                    }


                    levelmapsortingtilesremains[indexinarray] = -1;


                    //levelmap[indexinarray] = 998;

                    //levelmapsortingtiles[indexinarray] = 998;
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

            if (adjacenttiles[indexinarray] == 1001 && x == minx ||
               adjacenttiles[indexinarray] == 1001 && z == minz ||
               adjacenttiles[indexinarray] == 1001 && x == maxx-1 ||
               adjacenttiles[indexinarray] == 1001 && z == maxz-1)
            {
                if (adjacenttiles[indexinarray] == 1001 && x == minx)
                {
                    Console.WriteLine("found bordertile minx");
                }
                else if (adjacenttiles[indexinarray] == 1001 && z == minz)
                {
                    Console.WriteLine("found bordertile minz");
                }
                else if (adjacenttiles[indexinarray] == 1001 && x == maxx - 1)
                {
                    Console.WriteLine("found bordertile maxx");
                }
                else if (adjacenttiles[indexinarray] == 1001 && z == maxz - 1)
                {
                    Console.WriteLine("found bordertile maxz");
                }
            }






            for (int xxi = -1; xxi <= 1; xxi++)
            {
                //int checkx = (((xxi + x)));
                for (int zzi = -1; zzi <= 1; zzi++)
                {
                    //int checkz = (((zzi + z)));

                    int checkx = (((xxi + x)));

                    int checkz = (((zzi + z)));


                    //float checkx = ((currentTilePos.x + x));
                    //float checkz = ((currentTilePos.z + z));

                    if (checkx == x && checkz == z + 1)
                    {
                        //Console.WriteLine("testzneighboorfront");
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

                        int indexinarray0 = xiii + somewidth * (y + someheight * ziii); //y is always 0 on floor tiles

                        if (indexinarray0 < somewidth * someheight * somedepth)
                        {
                            if (adjacenttiles[indexinarray0] == 0)
                            {
                                istypeoft = 0;
                            }
                            else if (adjacenttiles[indexinarray0] == 999)
                            {
                                istypeoft = -1;
                            }
                            else if (adjacenttiles[indexinarray0] == 1001)
                            {
                                istypeoft = 1;
                            }

                            /*
                            if (z+1 == maxz - 1)
                            {
                                istypeoft = -1;
                            }*/


                            if (adjacenttiles[indexinarray] == 0 && z == maxz-1 ||
                                adjacenttiles[indexinarray] == 999 && z == maxz-1 ||
                                adjacenttiles[indexinarray] == 1001 && z == maxz - 1)
                            {
                                //istypeoft = -1;
                            }
                        }
                        else
                        {
                            istypeoft = -1;
                        }
                    }

                    else if (checkx == x && checkz == z - (1))
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

                        int indexinarray0 = xiii + somewidth * (y + someheight * ziii); //y is always 0 on floor tiles

                        if (indexinarray0 < somewidth * someheight * somedepth)
                        {
                            if (adjacenttiles[indexinarray0] == 0)
                            {
                                istypeofb = 0;
                            }
                            else if (adjacenttiles[indexinarray0] == 999)
                            {
                                istypeofb = -1;
                            }
                            else if (adjacenttiles[indexinarray0] == 1001)
                            {
                                istypeofb = 1;
                            }

                            /*
                            if (z-1 == minz)
                            {
                                istypeofb = -1;
                            }
                            */


                            if (adjacenttiles[indexinarray] == 0 && z == minz ||
                               adjacenttiles[indexinarray] == 999 && z == minz ||
                               adjacenttiles[indexinarray] == 1001 && z == minz)
                            {
                                //istypeofb = -1;
                            }
                        }
                        else
                        {
                            istypeofb = -1;
                        }
                    }
                    else if (checkx == x + (1) && checkz == z)
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

                        int indexinarray0 = xiii + somewidth * (y + someheight * ziii); //y is always 0 on floor tiles


                        if (indexinarray0 < somewidth * someheight * somedepth)
                        {
                            if (adjacenttiles[indexinarray0] == 0)
                            {
                                istypeofr = 0;
                            }
                            else if (adjacenttiles[indexinarray0] == 999)
                            {
                                istypeofr = -1;
                            }
                            else if (adjacenttiles[indexinarray0] == 1001)
                            {
                                istypeofr = 1;
                            }



                            /*
                            if (x+1 == maxx-1)
                            {
                                istypeofr = -1;
                            }*/



                            if (adjacenttiles[indexinarray] == 0 && x == maxx-1 ||
                                adjacenttiles[indexinarray] == 999 && x == maxx - 1 ||
                                adjacenttiles[indexinarray] == 1001 && x == maxx - 1)
                            {
                                //istypeofr = -1;
                            }
                        }
                        else
                        {
                            istypeofr = -1;
                        }
                    }
                    else if (checkx == x - (1) && checkz == z)
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

                        int indexinarray0 = xiii + somewidth * (y + someheight * ziii); //y is always 0 on floor tiles

                        if (indexinarray0 < somewidth * someheight * somedepth)
                        {
                            if (adjacenttiles[indexinarray0] == 0)
                            {
                                istypeofl = 0;
                            }
                            else if (adjacenttiles[indexinarray0] == 999)
                            {
                                istypeofl = -1;
                            }
                            else if (adjacenttiles[indexinarray0] == 1001)
                            {
                                istypeofl = 1;
                            }

                            /*
                            if (x-1 == minx)
                            {
                                istypeofl = -1;
                            }
                            */

                            if (adjacenttiles[indexinarray] == 0 && x == minx ||
                               adjacenttiles[indexinarray] == 999 && x == minx ||
                               adjacenttiles[indexinarray] == 1001 && x == minx)
                            {
                                //istypeofl = -1;
                            }
                        }
                        else
                        {
                            istypeofl = -1;
                        }
                    }



                    else if (checkx == x + (1) && checkz == z + (1))
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

                        int indexinarray0 = xiii + somewidth * (y + someheight * ziii); //y is always 0 on floor tiles

                        if (indexinarray0 < somewidth * someheight * somedepth)
                        {
                            if (adjacenttiles[indexinarray0] == 0)
                            {
                                istypeofrt = 0;
                            }
                            else if (adjacenttiles[indexinarray0] == 999)
                            {
                                istypeofrt = -1;
                            }
                            else if (adjacenttiles[indexinarray0] == 1001)
                            {
                                istypeofrt = 1;
                            }
                            /*
                            if (x+1 == maxx -1  && z+1== maxz -1)
                            {
                                istypeofrt = -1;
                            }*/

                            if (adjacenttiles[indexinarray] == 0 && x == maxx - 1 && adjacenttiles[indexinarray] == 0 && z == maxz - 1 ||
                               adjacenttiles[indexinarray] == 999 && x == maxx - 1 && adjacenttiles[indexinarray] == 999 && z == maxz - 1 ||
                               adjacenttiles[indexinarray] == 1001 && x == maxx - 1 && adjacenttiles[indexinarray] == 1001 && z == maxz - 1 ||

                               adjacenttiles[indexinarray] == 0 && x == maxx - 1 && adjacenttiles[indexinarray] == 999 && z == maxz - 1 ||
                               adjacenttiles[indexinarray] == 999 && x == maxx - 1 && adjacenttiles[indexinarray] == 0 && z == maxz - 1 ||
                               adjacenttiles[indexinarray] == 0 && x == maxx - 1 && adjacenttiles[indexinarray] == 1001 && z == maxz - 1 ||
                               adjacenttiles[indexinarray] == 1001 && x == maxx - 1 && adjacenttiles[indexinarray] == 0 && z == maxz - 1 ||

                               adjacenttiles[indexinarray] == 999 && x == maxx - 1 && adjacenttiles[indexinarray] == 1001 && z == maxz - 1 ||
                               adjacenttiles[indexinarray] == 1001 && x == maxx - 1 && adjacenttiles[indexinarray] == 999 && z == maxz - 1)
                            {
                                //istypeofrt = -1;
                            }
                        }
                        else
                        {
                            istypeofrt = -1;
                        }
                    }





                    else if (checkx == x - (1) && checkz == z + (1))
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

                        int indexinarray0 = xiii + somewidth * (y + someheight * ziii); //y is always 0 on floor tiles

                        if (indexinarray0 < somewidth * someheight * somedepth)
                        {
                            if (adjacenttiles[indexinarray0] == 0)
                            {
                                istypeoflt = 0;
                            }
                            else if (adjacenttiles[indexinarray0] == 999)
                            {
                                istypeoflt = -1;
                            }
                            else if (adjacenttiles[indexinarray0] == 1001)
                            {
                                istypeoflt = 1;
                            }


                            /*
                            if (x -1== minx  && z+1 == maxz - 1)
                            {
                                istypeoflt = -1;
                            }*/

                            if (adjacenttiles[indexinarray] == 0 && x == minx && adjacenttiles[indexinarray] == 0 && z == maxz - 1 ||
                               adjacenttiles[indexinarray] == 999 && x == minx && adjacenttiles[indexinarray] == 999 && z == maxz - 1 ||
                               adjacenttiles[indexinarray] == 1001 && x == minx && adjacenttiles[indexinarray] == 1001 && z == maxz - 1 ||

                               adjacenttiles[indexinarray] == 0 && x == minx && adjacenttiles[indexinarray] == 999 && z == maxz - 1 ||
                               adjacenttiles[indexinarray] == 999 && x == minx && adjacenttiles[indexinarray] == 0 && z == maxz - 1 ||
                               adjacenttiles[indexinarray] == 0 && x == minx && adjacenttiles[indexinarray] == 1001 && z == maxz - 1 ||
                               adjacenttiles[indexinarray] == 1001 && x == minx && adjacenttiles[indexinarray] == 0 && z == maxz - 1 ||

                               adjacenttiles[indexinarray] == 999 && x == minx && adjacenttiles[indexinarray] == 1001 && z == maxz - 1 ||
                               adjacenttiles[indexinarray] == 1001 && x == minx && adjacenttiles[indexinarray] == 999 && z == maxz - 1)
                            {
                                //istypeoflt = -1;
                            }
                        }
                        else
                        {
                            istypeoflt = -1;
                        }
                    }


                    else if (checkx == x + (1) && checkz == z - (1))
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

                        int indexinarray0 = xiii + somewidth * (y + someheight * ziii); //y is always 0 on floor tiles

                        if (indexinarray0 < somewidth * someheight * somedepth)
                        {
                            if (adjacenttiles[indexinarray0] == 0)
                            {
                                istypeofrb = 0;
                            }
                            else if (adjacenttiles[indexinarray0] == 999)
                            {
                                istypeofrb = -1;
                            }
                            else if (adjacenttiles[indexinarray0] == 1001)
                            {
                                istypeofrb = 1;
                            }
                            /*

                            if (x+1 == maxx-1 && z-1 == minz)
                            {
                                istypeofrb = -1;
                            }*/

                            if (adjacenttiles[indexinarray] == 0 && x == maxx - 1 && adjacenttiles[indexinarray] == 0 && z == minz ||
                               adjacenttiles[indexinarray] == 999 && x == maxx - 1 && adjacenttiles[indexinarray] == 999 && z == minz ||
                               adjacenttiles[indexinarray] == 1001 && x == maxx - 1 && adjacenttiles[indexinarray] == 1001 && z == minz ||

                               adjacenttiles[indexinarray] == 0 && x == maxx - 1 && adjacenttiles[indexinarray] == 999 && z == minz ||
                               adjacenttiles[indexinarray] == 999 && x == maxx - 1 && adjacenttiles[indexinarray] == 0 && z == minz ||
                               adjacenttiles[indexinarray] == 0 && x == maxx - 1 && adjacenttiles[indexinarray] == 1001 && z == minz ||
                               adjacenttiles[indexinarray] == 1001 && x == maxx - 1 && adjacenttiles[indexinarray] == 0 && z == minz ||

                               adjacenttiles[indexinarray] == 999 && x == maxx - 1 && adjacenttiles[indexinarray] == 1001 && z == minz ||
                               adjacenttiles[indexinarray] == 1001 && x == maxx - 1 && adjacenttiles[indexinarray] == 999 && z == minz)
                            {
                                //istypeofrb = -1;
                            }
                        }
                        else
                        {
                            istypeofrb = -1;
                        }
                    }

                    
                    else if (checkx == x - (1) && checkz == z - (1))
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

                        int indexinarray0 = xiii + somewidth * (y + someheight * ziii); //y is always 0 on floor tiles

                        if (indexinarray0 < somewidth * someheight * somedepth)
                        {
                            if (adjacenttiles[indexinarray0] == 0)
                            {
                                istypeoflb = 0;
                            }
                            else if (adjacenttiles[indexinarray0] == 999)
                            {
                                istypeoflb = -1;
                            }
                            else if (adjacenttiles[indexinarray0] == 1001)
                            {
                                istypeoflb = 1;
                            }
                            /*
                            if (x-1 == minx && z -1== minz)
                            {
                                istypeoflb = -1;
                            }
                            */
                            if (adjacenttiles[indexinarray] == 0 && x == minx && adjacenttiles[indexinarray] == 0 && z == minz ||
                               adjacenttiles[indexinarray] == 999 && x == minx && adjacenttiles[indexinarray] == 999 && z == minz ||
                               adjacenttiles[indexinarray] == 1001 && x == minx && adjacenttiles[indexinarray] == 1001 && z == minz ||

                               adjacenttiles[indexinarray] == 0 && x == minx && adjacenttiles[indexinarray] == 999 && z == minz ||
                               adjacenttiles[indexinarray] == 999 && x == minx && adjacenttiles[indexinarray] == 0 && z == minz ||
                               adjacenttiles[indexinarray] == 0 && x == minx && adjacenttiles[indexinarray] == 1001 && z == minz ||
                               adjacenttiles[indexinarray] == 1001 && x == minx && adjacenttiles[indexinarray] == 0 && z == minz ||

                               adjacenttiles[indexinarray] == 999 && x == minx && adjacenttiles[indexinarray] == 1001 && z == minz ||
                               adjacenttiles[indexinarray] == 1001 && x == minx && adjacenttiles[indexinarray] == 999 && z == minz)
                            {
                                //istypeoflb = -1;
                            }
                        }
                        else
                        {
                            istypeoflb = -1;
                        }
                    }
                }
            }







            /*
            if (x - 1 == minx && z - 1 == minz)
            {
                istypeoflb = -1;
            }

            if (x + 1 == maxx - 1 && z - 1 == minz)
            {
                istypeofrb = -1;
            }

            if (x - 1 == minx && z + 1 == maxz - 1)
            {
                istypeoflt = -1;
            }

            if (x + 1 == maxx - 1 && z + 1 == maxz - 1)
            {
                istypeofrt = -1;
            }

            if (x - 1 == minx)
            {
                istypeofl = -1;
            }

            if (x + 1 == maxx - 1)
            {
                istypeofr = -1;
            }

            if (z - 1 == minz)
            {
                istypeofb = -1;
            }

            if (z + 1 == maxz - 1)
            {
                istypeoft = -1;
            }
            */













            //LEFT WALL
            if (istypeoflt == -1 && istypeoft == 1 &&
                 istypeofl == -1 && istypeofr == 0 &&
                 istypeoflb == -1&& istypeofb == 1 ||

                 istypeoflt == 1 && istypeoft == 1 &&
                 istypeofl == -1 && istypeofr == 0 &&
                 istypeoflb == -1 && istypeofb == 1 ||

                 istypeoflt == -1 && istypeoft == 1 &&
                 istypeofl == -1 && istypeofr == 0 &&
                 istypeoflb == 1 && istypeofb == 1 ||

                 istypeoflt == 1 && istypeoft == 1 &&
                 istypeofl == -1 && istypeofr == 0 &&
                 istypeoflb == 1 && istypeofb == 1)
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


            //RIGHT WALL
            if (istypeoft == 1 && istypeofrt == -1 &&
                 istypeofl == 0 && istypeofr == -1 &&
               istypeofb == 1&& istypeofrb == -1 ||

               istypeoft == 1 && istypeofrt == 1 &&
                      istypeofl == 0 && istypeofr == -1 &&
               istypeofb == 1 && istypeofrb == -1 ||

               istypeoft == 1 && istypeofrt == -1 &&
                   istypeofl == 0 && istypeofr == -1 &&
               istypeofb == 1 && istypeofrb == 1 ||

                istypeoft == 1 && istypeofrt == 1 &&
                    istypeofl == 0 && istypeofr == -1 &&
               istypeofb == 1 && istypeofrb == 1)
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


            //BACK WALL
            if (istypeoflt == -1 && istypeoft == -1 && istypeofrt == -1 &&

                istypeofl == 1 &&                      istypeofr == 1 &&
                                    istypeofb == 0||

                istypeoflt == 1 && istypeoft == -1 && istypeofrt == -1 &&

                istypeofl == 1 && istypeofr == 1 &&
                                    istypeofb == 0 ||

                istypeoflt == -1 && istypeoft == -1 && istypeofrt == 1 &&

                istypeofl == 1 && istypeofr == 1 &&
                                    istypeofb == 0 ||

                  istypeoflt == 1 && istypeoft == -1 && istypeofrt == 1 &&

                istypeofl == 1 && istypeofr == 1 &&
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
                   levelmap[indexinarray] != 1112)
                {
                    levelmap[indexinarray] = 1104;
                }
            }

            //FRONT WALL
            if (                istypeoft == 0 &&
                istypeofl == 1                && istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == -1 ||

                                 istypeoft == 0 &&
                istypeofl == 1 &&                   istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == -1 && istypeofrb == -1 ||

                                     istypeoft == 0 &&
                istypeofl == 1 &&                   istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == 1 ||

                                 istypeoft == 0 &&
                istypeofl == 1 &&                   istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == -1 && istypeofrb == 1)
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












            /*
            //LEFT WALL
            if (istypeoflt == -1 && istypeoft == 1 && istypeofrt == 0 &&
                 istypeofl == -1 && istypeofr == 0 &&
                 istypeoflb == -1 && istypeofb == 1 && istypeofrb == 0 ||

                 istypeoflt == -1 && istypeoft == 1 && istypeofrt == 1 &&
                 istypeofl == -1 && istypeofr == 0 &&
                 istypeoflb == -1 && istypeofb == 1 && istypeofrb == 0 ||

                 istypeoflt == -1 && istypeoft == 1 && istypeofrt == 0 &&
                 istypeofl == -1 && istypeofr == 0 &&
                 istypeoflb == -1 && istypeofb == 1 && istypeofrb == 1 ||


                 istypeoflt == 1 && istypeoft == 1 && istypeofrt == 0 &&
                 istypeofl == -1 &&                     istypeofr == 0 &&
                 istypeoflb == -1 && istypeofb == 1 && istypeofrb == 0 ||

                 istypeoflt == -1 && istypeoft == 1 && istypeofrt == 0 &&
                 istypeofl == -1 &&                      istypeofr == 0 &&
                 istypeoflb == 1 && istypeofb == 1 && istypeofrb == 0 ||



                 istypeoflt == 1 && istypeoft == 1 && istypeofrt == 1 &&
                 istypeofl == -1 && istypeofr == 0 &&
                 istypeoflb == -1 && istypeofb == 1 && istypeofrb == 0 ||

                 istypeoflt == -1 && istypeoft == 1 && istypeofrt == 0 &&
                 istypeofl == -1 && istypeofr == 0 &&
                 istypeoflb == 1 && istypeofb == 1 && istypeofrb == 1 ||


                 istypeoflt == 1 && istypeoft == 1 && istypeofrt == 0 &&
                 istypeofl == -1 && istypeofr == 0 &&
                 istypeoflb == -1 && istypeofb == 1 && istypeofrb == 1 ||

                 istypeoflt == -1 && istypeoft == 1 && istypeofrt == 1 &&
                 istypeofl == -1 && istypeofr == 0 &&
                 istypeoflb == 1 && istypeofb == 1 && istypeofrb == 0


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
                    levelmap[indexinarray] != 1112

                       )
                {
                    levelmap[indexinarray] = 1101;
                }
            }



            //RIGHT WALL


            if (istypeoflt == 0 && istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 0 && istypeofr == -1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == -1 ||

                istypeoflt == 1 && istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 0 && istypeofr == -1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == -1 ||

                istypeoflt == 0 && istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 0 && istypeofr == -1 &&
                istypeoflb == 1 && istypeofb == 1 && istypeofrb == -1 ||


                istypeoflt == 0 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 0 &&                   istypeofr == -1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == -1 ||

                istypeoflt == 0 && istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 0 &&                   istypeofr == -1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == 1 ||




                istypeoflt == 1 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 0 && istypeofr == -1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == -1 ||

                istypeoflt == 0 && istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 0 && istypeofr == -1 &&
                istypeoflb == 1 && istypeofb == 1 && istypeofrb == 1 ||




                istypeoflt == 0 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 0 && istypeofr == -1 &&
                istypeoflb == 1 && istypeofb == 1 && istypeofrb == -1 ||

                istypeoflt == 1 && istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 0 && istypeofr == -1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == 1


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
                    levelmap[indexinarray] != 1112

                       )
                {
                    levelmap[indexinarray] = 1102;

                }
            }





            //BACK WALL
            if (istypeoflt == -1 && istypeoft == -1 && istypeofrt == -1 &&
                istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0 ||

                istypeoflt == -1 && istypeoft == -1 && istypeofrt == -1 &&
                istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == 0 && istypeofrb == 0 ||

                istypeoflt == -1 && istypeoft == -1 && istypeofrt == -1 &&
                istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 1 ||

                istypeoflt == 1 && istypeoft == -1 && istypeofrt == -1 &&
                istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0 ||

                istypeoflt == -1 && istypeoft == -1 && istypeofrt == 1 &&
                istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0 ||


                istypeoflt == 1 && istypeoft == -1 && istypeofrt == -1 &&
                istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == 0 && istypeofrb == 0 ||

                istypeoflt == -1 && istypeoft == -1 && istypeofrt == 1 &&
                istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 1 ||

                istypeoflt == 1 && istypeoft == -1 && istypeofrt == -1 &&
                istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 1 ||

                istypeoflt == -1 && istypeoft == -1 && istypeofrt == 1 &&
                istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == 0 && istypeofrb == 0

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
                    levelmap[indexinarray] != 1112

                       )
                {
                    levelmap[indexinarray] = 1104;
                }
            }



            //FRONT WALL
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == -1 ||

                istypeoflt == 1 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == -1 ||

                istypeoflt == 0 && istypeoft == 0 && istypeofrt == 1 &&
                istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == -1 ||

                istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == -1 && istypeofrb == -1 ||

                istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == 1 ||

                istypeoflt == 1 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == -1 && istypeofrb == -1 ||

                istypeoflt == 0 && istypeoft == 0 && istypeofrt == 1 &&
                istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == 1 ||

                istypeoflt == 0 && istypeoft == 0 && istypeofrt == 1 &&
                istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == -1 && istypeofrb == -1 ||

                istypeoflt == 1 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 1 && istypeofr == 1 &&
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
                    levelmap[indexinarray] != 1112

                       )
                {
                    levelmap[indexinarray] = 1103;
                }
            }*/



















            /*
            //LEFT WALL
            if (istypeoflt == -1 && istypeoft == 1 && istypeofrt == 0 &&
                 istypeofl == -1 &&  istypeofr == 0 &&
                 istypeoflb == -1 && istypeofb == 1 && istypeofrb == 0 ||

                 istypeoflt == -1 && istypeoft == 1 && istypeofrt == 1 &&
                 istypeofl == -1 &&  istypeofr == 0 &&
                 istypeoflb == -1 && istypeofb == 1 && istypeofrb == 0 ||

                 istypeoflt == -1 && istypeoft == 1 && istypeofrt == 0 &&
                 istypeofl == -1 &&  istypeofr == 0 &&
                 istypeoflb == -1 && istypeofb == 1 && istypeofrb == 1 ||


                 istypeoflt == 1 && istypeoft == 1 && istypeofrt == 0 &&
                 istypeofl == -1 &&  istypeofr == 0 &&
                 istypeoflb == -1 && istypeofb == 1 && istypeofrb == 0 ||

                 istypeoflt == 1 && istypeoft == 1 && istypeofrt == 1 &&
                 istypeofl == -1 &&  istypeofr == 0 &&
                 istypeoflb == -1 && istypeofb == 1 && istypeofrb == 0 ||

                 istypeoflt == 1 && istypeoft == 1 && istypeofrt == 0 &&
                 istypeofl == -1 &&  istypeofr == 0 &&
                 istypeoflb == -1 && istypeofb == 1 && istypeofrb == 1 ||



                 istypeoflt == -1 && istypeoft == 1 && istypeofrt == 0 &&
                 istypeofl == -1 &&  istypeofr == 0 &&
                 istypeoflb == 1 && istypeofb == 1 && istypeofrb == 0 ||

                 istypeoflt == -1 && istypeoft == 1 && istypeofrt == 1 &&
                 istypeofl == -1 &&  istypeofr == 0 &&
                 istypeoflb == 1 && istypeofb == 1 && istypeofrb == 0 ||

                 istypeoflt == -1 && istypeoft == 1 && istypeofrt == 0 &&
                 istypeofl == -1 &&  istypeofr == 0 &&
                 istypeoflb == 1 && istypeofb == 1 && istypeofrb == 1 ||



                 istypeoflt == 1 && istypeoft == 1 && istypeofrt == 0 &&
                 istypeofl == -1 &&  istypeofr == 0 &&
                 istypeoflb == 1 && istypeofb == 1 && istypeofrb == 0 ||

                 istypeoflt == 1 && istypeoft == 1 && istypeofrt == 1 &&
                 istypeofl == -1 &&  istypeofr == 0 &&
                 istypeoflb == 1 && istypeofb == 1 && istypeofrb == 0 ||

                 istypeoflt == 1 && istypeoft == 1 && istypeofrt == 0 &&
                 istypeofl == -1 &&  istypeofr == 0 &&
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
                    levelmap[indexinarray] != 1112

                       )
                {
                    levelmap[indexinarray] = 1101;
                }
            }



            //RIGHT WALL


            if (istypeoflt == 0 && istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 0 &&  istypeofr == -1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == -1 ||

                istypeoflt == 1 && istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 0 &&  istypeofr == -1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == -1 ||

                istypeoflt == 0 && istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 0 &&  istypeofr == -1 &&
                istypeoflb == 1 && istypeofb == 1 && istypeofrb == -1 ||


                istypeoflt == 0 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 0 &&  istypeofr == -1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == -1 ||

                istypeoflt == 1 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 0 &&  istypeofr == -1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == -1 ||

                istypeoflt == 0 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 0 &&  istypeofr == -1 &&
                istypeoflb == 1 && istypeofb == 1 && istypeofrb == -1 ||


                istypeoflt == 0 && istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 0 &&  istypeofr == -1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == 1 ||

                istypeoflt == 1 && istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 0 &&  istypeofr == -1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == 1 ||

                istypeoflt == 0 && istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 0 &&  istypeofr == -1 &&
                istypeoflb == 1 && istypeofb == 1 && istypeofrb == 1 ||


                istypeoflt == 0 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 0 &&  istypeofr == -1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == 1 ||

                istypeoflt == 1 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 0 &&  istypeofr == -1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == 1 ||

                istypeoflt == 0 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 0 &&  istypeofr == -1 &&
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
                    levelmap[indexinarray] != 1112

                       )
                {
                    levelmap[indexinarray] = 1102;
                }
            }





            //BACK WALL
            if (istypeoflt == -1 && istypeoft == -1 && istypeofrt == -1 &&
                istypeofl == 1 &&  istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0 ||

                istypeoflt == -1 && istypeoft == -1 && istypeofrt == -1 &&
                istypeofl == 1 &&  istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == 0 && istypeofrb == 0 ||

                istypeoflt == -1 && istypeoft == -1 && istypeofrt == -1 &&
                istypeofl == 1 &&  istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 1 ||


                istypeoflt == 1 && istypeoft == -1 && istypeofrt == -1 &&
                istypeofl == 1 &&  istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0 ||

                istypeoflt == 1 && istypeoft == -1 && istypeofrt == -1 &&
                istypeofl == 1 &&  istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == 0 && istypeofrb == 0 ||

                istypeoflt == 1 && istypeoft == -1 && istypeofrt == -1 &&
                istypeofl == 1 &&  istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 1 ||


                istypeoflt == -1 && istypeoft == -1 && istypeofrt == 1 &&
                istypeofl == 1 &&  istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0 ||

                istypeoflt == -1 && istypeoft == -1 && istypeofrt == 1 &&
                istypeofl == 1 &&  istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == 0 && istypeofrb == 0 ||

                istypeoflt == -1 && istypeoft == -1 && istypeofrt == 1 &&
                istypeofl == 1 &&  istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 1 ||


                istypeoflt == 1 && istypeoft == -1 && istypeofrt == 1 &&
                istypeofl == 1 &&  istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0 ||

                istypeoflt == 1 && istypeoft == -1 && istypeofrt == 1 &&
                istypeofl == 1 &&  istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == 0 && istypeofrb == 0 ||

                istypeoflt == 1 && istypeoft == -1 && istypeofrt == 1 &&
                istypeofl == 1 &&  istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 1

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
                    levelmap[indexinarray] != 1112

                       )
                {
                    levelmap[indexinarray] = 1104;
                }
            }



            //FRONT WALL
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 1 &&  istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == -1 ||

                istypeoflt == 1 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 1 &&  istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == -1 ||

                istypeoflt == 0 && istypeoft == 0 && istypeofrt == 1 &&
                istypeofl == 1 &&  istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == -1 ||


                istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 1 &&  istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == -1 && istypeofrb == -1 ||

                istypeoflt == 1 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 1 &&  istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == -1 && istypeofrb == -1 ||

                istypeoflt == 0 && istypeoft == 0 && istypeofrt == 1 &&
                istypeofl == 1 &&  istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == -1 && istypeofrb == -1 ||


                istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 1 &&  istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == 1 ||

                istypeoflt == 1 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 1 &&  istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == 1 ||

                istypeoflt == 0 && istypeoft == 0 && istypeofrt == 1 &&
                istypeofl == 1 &&  istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == 1||


                istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 1 &&  istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == -1 && istypeofrb == 1 ||

                istypeoflt == 1 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 1 &&  istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == -1 && istypeofrb == 1 ||

                istypeoflt == 0 && istypeoft == 0 && istypeofrt == 1 &&
                istypeofl == 1 &&  istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == -1 && istypeofrb == 1

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
                    levelmap[indexinarray] != 1112

                       )
                {
                    levelmap[indexinarray] = 1103;
                }
            }*/











            //OTHER WALL TYPES


            /*/////////BUILD WALL LEFT/////////////////
            if (istypeoflt == -1 && istypeoft == 1 &&
                istypeofl == -1 && istypeofr == 0 &&
                istypeoflb == -1 && istypeofb == 1)
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
                    levelmap[indexinarray] = 1101;
                }
            }
            if (istypeoflt == 1 && istypeoft == 1 &&
                istypeofl == -1 && istypeofr == 0 &&
                istypeoflb == -1 && istypeofb == 1)
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
                    levelmap[indexinarray] = 1101;
                }
            }
            if (istypeoflt == -1 && istypeoft == 1 &&
              istypeofl == -1 && istypeofr == 0 &&
              istypeoflb == 1 && istypeofb == 1)
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
                    levelmap[indexinarray] = 1101;
                }
            }
            if (istypeoflt == 1 && istypeoft == 1 &&
                istypeofl == -1 && istypeofr == 0 &&
                istypeoflb == 1 && istypeofb == 1)
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
                    levelmap[indexinarray] = 1101;
                }
            }
            if (istypeoflt == -1 && istypeoft == 1 &&
                istypeofl == -1 && istypeofr == 0 &&
                istypeoflb == -1 && istypeofb == 0)
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
                    levelmap[indexinarray] = 1101;
                }
            }





            /////////BUILD WALL RIGHT/////////////////
            if (istypeoft == 1 && istypeofrt == -1 &&
                 istypeofl == 0 && istypeofr == -1 &&
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
                    levelmap[indexinarray] = 1102;
                }
            }

            if (istypeoft == 1 && istypeofrt == 1 &&
                 istypeofl == 0 && istypeofr == -1 &&
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
                    levelmap[indexinarray] = 1102;
                }
            }

            if (istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 0 && istypeofr == -1 &&
                istypeofb == 1 && istypeofrb == 1)
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
                    levelmap[indexinarray] = 1102;
                }
            }

            if (istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 0 && istypeofr == -1 &&
                istypeofb == 1 && istypeofrb == 1)
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
                    levelmap[indexinarray] = 1102;
                }
            }
            //////






            /////////BUILD WALL BACK/////////////////

            if (istypeoflt == -1 && istypeoft == -1 && istypeofrt == -1 &&
               istypeofl == 1 && istypeofr == 1 &&
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
                    levelmap[indexinarray] = 1104;
                }
            }
            if (istypeoflt == 1 && istypeoft == -1 && istypeofrt == -1 &&
              istypeofl == 1 && istypeofr == 1 &&
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
                    levelmap[indexinarray] = 1104;
                }
            }

            if (istypeoflt == -1 && istypeoft == -1 && istypeofrt == 1 &&
              istypeofl == 1 && istypeofr == 1 &&
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
                    levelmap[indexinarray] = 1104;
                }
            }
            if (istypeoflt == 1 && istypeoft == -1 && istypeofrt == 1 &&
                   istypeofl == 1 && istypeofr == 1 &&
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
                    levelmap[indexinarray] = 1104;
                }
            }









            /////////BUILD WALL FRONT/////////////////

            if (istypeoft == 0 &&
               istypeofl == 1 && istypeofr == 1 &&
                 istypeoflb == -1 && istypeofb == -1 && istypeofrb == -1)
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
                    levelmap[indexinarray] = 1103;
                }
            }


            if (istypeoft == 0 &&
              istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == -1 && istypeofrb == -1)
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
                    levelmap[indexinarray] = 1103;
                }
            }


            if (istypeoft == 0 &&
              istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == 1)
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
                    levelmap[indexinarray] = 1103;
                }
            }


            if (istypeoft == 0 &&
              istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == -1 && istypeofrb == 1)
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
                    levelmap[indexinarray] = 1103;
                }
            }

            /////////////////////////////////////////
            */




            /////////BUILD WALL LEFT/////////////////

            /*if (istypeoflt == -1  && istypeofrt == 0 &&
                 istypeofl == -1 && istypeofr == 0 &&
                 istypeoflb == -1  && istypeofrb == 0 ||

                 istypeoflt == -1 && istypeofrt == 1 &&
                 istypeofl == -1 && istypeofr == 0 &&
                 istypeoflb == -1 && istypeofrb == 0 ||

                  istypeoflt == -1 && istypeofrt == 0 &&
                 istypeofl == -1 && istypeofr == 0 &&
                 istypeoflb == -1 && istypeofrb == 1 ||



                 istypeoflt == 1 && istypeofrt == 0 &&
                 istypeofl == -1 && istypeofr == 0 &&
                 istypeoflb == -1 && istypeofrb == 0 ||

                 istypeoflt == 1 && istypeofrt == 1 &&
                 istypeofl == -1 && istypeofr == 0 &&
                 istypeoflb == -1 && istypeofrb == 0 ||

                  istypeoflt == 1 && istypeofrt == 0 &&
                 istypeofl == -1 && istypeofr == 0 &&
                 istypeoflb == -1 && istypeofrb == 1 ||




                 istypeoflt == -1 && istypeofrt == 0 &&
                 istypeofl == -1 && istypeofr == 0 &&
                 istypeoflb == 1 && istypeofrb == 0 ||

                 istypeoflt == -1 && istypeofrt == 1 &&
                 istypeofl == -1 && istypeofr == 0 &&
                 istypeoflb == 1 && istypeofrb == 0 ||

                  istypeoflt == -1 && istypeofrt == 0 &&
                 istypeofl == -1 && istypeofr == 0 &&
                 istypeoflb == 1 && istypeofrb == 1 ||


                 istypeoflt == 1 && istypeofrt == 0 &&
                 istypeofl == -1 && istypeofr == 0 &&
                 istypeoflb == 1 && istypeofrb == 0 ||

                 istypeoflt == 1 && istypeofrt == 1 &&
                 istypeofl == -1 && istypeofr == 0 &&
                 istypeoflb == 1 && istypeofrb == 0 ||

                  istypeoflt == 1 && istypeofrt == 0 &&
                 istypeofl == -1 && istypeofr == 0 &&
                 istypeoflb == 1 && istypeofrb == 1







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
                   levelmap[indexinarray] != 1112

                   )
                {
                    levelmap[indexinarray] = 1101;
                }
            }

            /////////BUILD WALL RIGHT/////////////////

            if (istypeoflt == 0 && istypeofrt == -1 &&
                 istypeofl == 0 && istypeofr == -1 &&
                 istypeoflb == 0 && istypeofrb == -1 ||

                 istypeoflt == 1 && istypeofrt == -1 &&
                 istypeofl == 0 && istypeofr == -1 &&
                 istypeoflb == 0 && istypeofrb == -1 ||

                 istypeoflt == 0 && istypeofrt == -1 &&
                 istypeofl == 0 && istypeofr == -1 &&
                 istypeoflb == 1 && istypeofrb == -1 ||

                 istypeoflt == 0 && istypeofrt == 1 &&
                 istypeofl == 0 && istypeofr == -1 &&
                 istypeoflb == 0 && istypeofrb == -1 ||

                 istypeoflt == 1 && istypeofrt == 1 &&
                 istypeofl == 0 && istypeofr == -1 &&
                 istypeoflb == 0 && istypeofrb == -1 ||

                 istypeoflt == 0 && istypeofrt == 1 &&
                 istypeofl == 0 && istypeofr == -1 &&
                 istypeoflb == 1 && istypeofrb == -1 ||

                 istypeoflt == 0 && istypeofrt == -1 &&
                 istypeofl == 0 && istypeofr == -1 &&
                 istypeoflb == 0 && istypeofrb == 1 ||

                 istypeoflt == 1 && istypeofrt == -1 &&
                 istypeofl == 0 && istypeofr == -1 &&
                 istypeoflb == 0 && istypeofrb == 1 ||

                 istypeoflt == 0 && istypeofrt == -1 &&
                 istypeofl == 0 && istypeofr == -1 &&
                 istypeoflb == 1 && istypeofrb == 1 ||


                 istypeoflt == 0 && istypeofrt == 1 &&
                 istypeofl == 0 && istypeofr == -1 &&
                 istypeoflb == 0 && istypeofrb == 1 ||

                 istypeoflt == 1 && istypeofrt == 1 &&
                 istypeofl == 0 && istypeofr == -1 &&
                 istypeoflb == 0 && istypeofrb == 1 ||

                 istypeoflt == 0 && istypeofrt == 1 &&
                 istypeofl == 0 && istypeofr == -1 &&
                 istypeoflb == 1 && istypeofrb == 1)
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
                    levelmap[indexinarray] = 1102;
                }
            }




            /////////BUILD WALL BACK/////////////////

            if (istypeoflt == -1 && istypeoft == -1 && istypeofrt == -1 &&
                 istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0 ||

                 istypeoflt == -1 && istypeoft == -1 && istypeofrt == -1 &&
                 istypeoflb == 1 && istypeofb == 0 && istypeofrb == 0 ||

                 istypeoflt == -1 && istypeoft == -1 && istypeofrt == -1 &&
                 istypeoflb == 0 && istypeofb == 0 && istypeofrb == 1 ||


                 istypeoflt == 1 && istypeoft == -1 && istypeofrt == -1 &&
                 istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0 ||

                 istypeoflt == 1 && istypeoft == -1 && istypeofrt == -1 &&
                 istypeoflb == 1 && istypeofb == 0 && istypeofrb == 0 ||

                 istypeoflt == 1 && istypeoft == -1 && istypeofrt == -1 &&
                 istypeoflb == 0 && istypeofb == 0 && istypeofrb == 1 ||



                 istypeoflt == -1 && istypeoft == -1 && istypeofrt == 1 &&
                 istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0 ||

                 istypeoflt == -1 && istypeoft == -1 && istypeofrt == 1 &&
                 istypeoflb == 1 && istypeofb == 0 && istypeofrb == 0 ||

                 istypeoflt == -1 && istypeoft == -1 && istypeofrt == 1 &&
                 istypeoflb == 0 && istypeofb == 0 && istypeofrb == 1 ||


                 istypeoflt == 1 && istypeoft == -1 && istypeofrt == 1 &&
                 istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0 ||

                 istypeoflt == 1 && istypeoft == -1 && istypeofrt == 1 &&
                 istypeoflb == 1 && istypeofb == 0 && istypeofrb == 0 ||

                 istypeoflt == 1 && istypeoft == -1 && istypeofrt == 1 &&
                 istypeoflb == 0 && istypeofb == 0 && istypeofrb == 1)
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
                    levelmap[indexinarray] = 1104;
                }
            }

            /////////BUILD WALL FRONT/////////////////

            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
                 istypeoflb == -1 && istypeofb == -1 && istypeofrb == -1 ||

                 istypeoflt == 1 && istypeoft == 0 && istypeofrt == 0 &&
                 istypeoflb == -1 && istypeofb == -1 && istypeofrb == -1 ||

                 istypeoflt == 0 && istypeoft == 0 && istypeofrt == 1 &&
                 istypeoflb == -1 && istypeofb == -1 && istypeofrb == -1 ||


                 istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
                 istypeoflb == 1 && istypeofb == -1 && istypeofrb == -1 ||

                 istypeoflt == 1 && istypeoft == 0 && istypeofrt == 0 &&
                 istypeoflb == 1 && istypeofb == -1 && istypeofrb == -1 ||

                 istypeoflt == 0 && istypeoft == 0 && istypeofrt == 1 &&
                 istypeoflb == 1 && istypeofb == -1 && istypeofrb == -1 ||


                 istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
                 istypeoflb == -1 && istypeofb == -1 && istypeofrb == 1 ||

                 istypeoflt == 1 && istypeoft == 0 && istypeofrt == 0 &&
                 istypeoflb == -1 && istypeofb == -1 && istypeofrb == 1 ||

                 istypeoflt == 0 && istypeoft == 0 && istypeofrt == 1 &&
                 istypeoflb == -1 && istypeofb == -1 && istypeofrb == 1 ||


                 istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
                 istypeoflb == 1 && istypeofb == -1 && istypeofrb == 1 ||

                 istypeoflt == 1 && istypeoft == 0 && istypeofrt == 0 &&
                 istypeoflb == 1 && istypeofb == -1 && istypeofrb == 1 ||

                 istypeoflt == 0 && istypeoft == 0 && istypeofrt == 1 &&
                 istypeoflb == 1 && istypeofb == -1 && istypeofrb == 1)
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
                    levelmap[indexinarray] = 1103;
                }
            }*/























            /////////BUILD WALL LEFT FRONT INSIDE/////////////////
            if (istypeoft == -1 &&
               istypeofl == -1 && istypeofr == 1 &&
                                  istypeofb == 1 ||

                                   istypeoft == -1 &&
               istypeofl == -1 && istypeofr == 0 &&
                                  istypeofb == 1 ||

                                   istypeoft == -1 &&
               istypeofl == -1 && istypeofr == 1 &&
                                  istypeofb == 0 ||

                                   istypeoft == -1 &&
               istypeofl == -1 && istypeofr == 0 &&
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
                    levelmap[indexinarray] = 1105;
                }
            }






            /////////BUILD WALL RIGHT FRONT INSIDE/////////////////
            if (istypeoft == -1 &&
               istypeofl == 1 && istypeofr == -1 &&
                                  istypeofb == 1 ||

                                   istypeoft == -1 &&
               istypeofl == 0 && istypeofr == -1 &&
                                  istypeofb == 1 ||

                                   istypeoft == -1 &&
               istypeofl == 1 && istypeofr == -1 &&
                                  istypeofb == 0 ||

                                  istypeoft == -1 &&
               istypeofl == 0 && istypeofr == -1 &&
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
                    levelmap[indexinarray] = 1106;
                }
            }


            /////////BUILD WALL LEFT BACK INSIDE/////////////////
            if (istypeoft == 1 &&
               istypeofl == -1 && istypeofr == 1 &&
                                  istypeofb == -1 ||

                                    istypeoft == 0 &&
                istypeofl == -1 && istypeofr == 1 &&
                                     istypeofb == -1 ||

                                    istypeoft == 1 &&
               istypeofl == -1 && istypeofr == 0 &&
                                  istypeofb == -1 ||

                                  istypeoft == 0 &&
               istypeofl == -1 && istypeofr == 0 &&
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
                                  istypeofb == -1 ||

                                 istypeoft == 0 &&
               istypeofl == 1 && istypeofr == -1 &&
                                  istypeofb == -1 ||

                                 istypeoft == 1 &&
               istypeofl == 0 && istypeofr == -1 &&
                                  istypeofb == -1 ||

                                  istypeoft == 0 &&
               istypeofl == 0 && istypeofr == -1 &&
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
                istypeofl == 1 &&                    istypeofr == 0 &&
                                  istypeofb == 0 ||

                                  istypeoflt == -1 && istypeoft == 1 &&
                istypeofl == 1 && istypeofr == 1 &&
                                  istypeofb == 0 ||

                                  istypeoflt == -1 && istypeoft == 1 &&
                istypeofl == 1 && istypeofr == 0 &&
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
                    levelmap[indexinarray] = 1109;
                }
            }

            /////////BUILD WALL RIGHT FRONT OUTSIDE/////////////////
            if (istypeoft == 1 && istypeofrt == -1 &&
               istypeofl == 0 && istypeofr == 1 &&
                                  istypeofb == 0 ||

                                  istypeoft == 1 && istypeofrt == -1 &&
               istypeofl == 1 && istypeofr == 1 &&
                                  istypeofb == 0 ||

                                  istypeoft == 1 && istypeofrt == -1 &&
               istypeofl == 0 && istypeofr == 1 &&
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
                    levelmap[indexinarray] = 1110;
                }
            }


            /////////BUILD WALL LEFT BACK OUTSIDE/////////////////
            if (istypeoft == 0 &&
                istypeofl == 1 && istypeofr == 0 &&
                 istypeoflb == -1 && istypeofb == 1 ||

                 istypeoft == 1 &&
                istypeofl == 1 && istypeofr == 0 &&
                 istypeoflb == -1 && istypeofb == 1 ||

                 istypeoft == 0 &&
                istypeofl == 1 && istypeofr == 1 &&
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
                                  istypeofb == 1 && istypeofrb == -1 ||

                                  istypeoft == 1 &&
               istypeofl == 0 && istypeofr == 1 &&
                                  istypeofb == 1 && istypeofrb == -1 ||

                                  istypeoft == 0 &&
               istypeofl == 1 && istypeofr == 1 &&
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











        }

















        public void buildWallsRerolledTWO(int x, int y, int z)
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



































            //OTHER WALL TYPES


            /////////BUILD WALL LEFT/////////////////
            if (istypeoflt == -1 && istypeoft == 1 &&
                istypeofl == -1 && istypeofr == 0 &&
                istypeoflb == -1 && istypeofb == 1)
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
                    levelmap[indexinarray] = 1101;
                }
            }
            if (istypeoflt == 1 && istypeoft == 1 &&
                istypeofl == -1 && istypeofr == 0 &&
                istypeoflb == -1 && istypeofb == 1)
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
                    levelmap[indexinarray] = 1101;
                }
            }
            if (istypeoflt == -1 && istypeoft == 1 &&
              istypeofl == -1 && istypeofr == 0 &&
              istypeoflb == 1 && istypeofb == 1)
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
                    levelmap[indexinarray] = 1101;
                }
            }
            if (istypeoflt == 1 && istypeoft == 1 &&
                istypeofl == -1 && istypeofr == 0 &&
                istypeoflb == 1 && istypeofb == 1)
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
                    levelmap[indexinarray] = 1101;
                }
            }
            if (istypeoflt == -1 && istypeoft == 1 &&
                istypeofl == -1 && istypeofr == 0 &&
                istypeoflb == -1 && istypeofb == 0)
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
                    levelmap[indexinarray] = 1101;
                }
            }





            /////////BUILD WALL RIGHT/////////////////
            if (istypeoft == 1 && istypeofrt == -1 &&
                 istypeofl == 0 && istypeofr == -1 &&
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
                    levelmap[indexinarray] = 1102;
                }
            }

            if (istypeoft == 1 && istypeofrt == 1 &&
                 istypeofl == 0 && istypeofr == -1 &&
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
                    levelmap[indexinarray] = 1102;
                }
            }

            if (istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 0 && istypeofr == -1 &&
                istypeofb == 1 && istypeofrb == 1)
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
                    levelmap[indexinarray] = 1102;
                }
            }

            if (istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 0 && istypeofr == -1 &&
                istypeofb == 1 && istypeofrb == 1)
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
                    levelmap[indexinarray] = 1102;
                }
            }
            //////






            /////////BUILD WALL BACK/////////////////

            if (istypeoflt == -1 && istypeoft == -1 && istypeofrt == -1 &&
               istypeofl == 1 && istypeofr == 1 &&
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
                    levelmap[indexinarray] = 1104;
                }
            }
            if (istypeoflt == 1 && istypeoft == -1 && istypeofrt == -1 &&
              istypeofl == 1 && istypeofr == 1 &&
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
                    levelmap[indexinarray] = 1104;
                }
            }

            if (istypeoflt == -1 && istypeoft == -1 && istypeofrt == 1 &&
              istypeofl == 1 && istypeofr == 1 &&
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
                    levelmap[indexinarray] = 1104;
                }
            }
            if (istypeoflt == 1 && istypeoft == -1 && istypeofrt == 1 &&
                   istypeofl == 1 && istypeofr == 1 &&
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
                    levelmap[indexinarray] = 1104;
                }
            }









            /////////BUILD WALL FRONT/////////////////

            if (istypeoft == 0 &&
               istypeofl == 1 && istypeofr == 1 &&
                 istypeoflb == -1 && istypeofb == -1 && istypeofrb == -1)
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
                    levelmap[indexinarray] = 1103;
                }
            }


            if (istypeoft == 0 &&
              istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == -1 && istypeofrb == -1)
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
                    levelmap[indexinarray] = 1103;
                }
            }


            if (istypeoft == 0 &&
              istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == 1)
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
                    levelmap[indexinarray] = 1103;
                }
            }


            if (istypeoft == 0 &&
              istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == -1 && istypeofrb == 1)
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
                    levelmap[indexinarray] = 1103;
                }
            }

            /////////////////////////////////////////

























            /////////BUILD WALL LEFT FRONT INSIDE/////////////////
            if (istypeoft == -1 &&
               istypeofl == -1 && istypeofr == 1 &&
                                  istypeofb == 1 ||

                                   istypeoft == -1 &&
               istypeofl == -1 && istypeofr == 0 &&
                                  istypeofb == 1 ||

                                   istypeoft == -1 &&
               istypeofl == -1 && istypeofr == 1 &&
                                  istypeofb == 0 ||

                                   istypeoft == -1 &&
               istypeofl == -1 && istypeofr == 0 &&
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
                    levelmap[indexinarray] = 1105;
                }
            }






            /////////BUILD WALL RIGHT FRONT INSIDE/////////////////
            if (istypeoft == -1 &&
               istypeofl == 1 && istypeofr == -1 &&
                                  istypeofb == 1 ||

                                   istypeoft == -1 &&
               istypeofl == 0 && istypeofr == -1 &&
                                  istypeofb == 1 ||

                                   istypeoft == -1 &&
               istypeofl == 1 && istypeofr == -1 &&
                                  istypeofb == 0 ||

                                  istypeoft == -1 &&
               istypeofl == 0 && istypeofr == -1 &&
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
                    levelmap[indexinarray] = 1106;
                }
            }


            /////////BUILD WALL LEFT BACK INSIDE/////////////////
            if (istypeoft == 1 &&
               istypeofl == -1 && istypeofr == 1 &&
                                  istypeofb == -1 ||

                                    istypeoft == 0 &&
                istypeofl == -1 && istypeofr == 1 &&
                                     istypeofb == -1 ||

                                    istypeoft == 1 &&
               istypeofl == -1 && istypeofr == 0 &&
                                  istypeofb == -1 ||

                                  istypeoft == 0 &&
               istypeofl == -1 && istypeofr == 0 &&
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
                                  istypeofb == -1 ||

                                 istypeoft == 0 &&
               istypeofl == 1 && istypeofr == -1 &&
                                  istypeofb == -1 ||

                                 istypeoft == 1 &&
               istypeofl == 0 && istypeofr == -1 &&
                                  istypeofb == -1 ||

                                  istypeoft == 0 &&
               istypeofl == 0 && istypeofr == -1 &&
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











        }



        public void buildWallsRerolledTHREE(int x, int y, int z)
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
            if (istypeoflt == -1 && istypeoft == 1 &&
                istypeofl == -1 &&
                istypeoflb == -1 && istypeofb == 1 ||

                istypeoflt == 1 && istypeoft == 1 &&
                istypeofl == -1 &&
                istypeoflb == -1 && istypeofb == 1 ||

                istypeoflt == -1 && istypeoft == 1 &&
                istypeofl == -1 &&
                istypeoflb == 1 && istypeofb == 1 ||

                istypeoflt == 1 && istypeoft == 1 &&
                istypeofl == -1 &&
                istypeoflb == 1 && istypeofb == 1)
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
                    levelmap[indexinarray] = 1101;
                }
            }

            /////////BUILD WALL RIGHT/////////////////
            if (istypeoft == 1 && istypeofrt == -1 &&
                                                istypeofr == -1 &&
                                istypeofb == 1 && istypeofrb == -1 ||

                                istypeoft == 1 && istypeofrt == 1 &&
                                                istypeofr == -1 &&
                                istypeofb == 1 && istypeofrb == -1 ||

                                istypeoft == 1 && istypeofrt == -1 &&
                                                istypeofr == -1 &&
                                istypeofb == 1 && istypeofrb == 1 ||

                                istypeoft == 1 && istypeofrt == 1 &&
                                                istypeofr == -1 &&
                                istypeofb == 1 && istypeofrb == 1)
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
                    levelmap[indexinarray] = 1102;
                }
            }


            /////////BUILD WALL BACK/////////////////
            if (istypeoflt == -1 && istypeoft == -1 && istypeofrt == -1 &&
                istypeofl == 1 && istypeofr == 1 ||

                istypeoflt == 1 && istypeoft == -1 && istypeofrt == -1 &&
                istypeofl == 1 && istypeofr == 1 ||

                istypeoflt == -1 && istypeoft == -1 && istypeofrt == 1 &&
                istypeofl == 1 && istypeofr == 1 ||

                istypeoflt == 1 && istypeoft == -1 && istypeofrt == 1 &&
                istypeofl == 1 && istypeofr == 1)
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
                    levelmap[indexinarray] = 1104;
                }
            }


            /////////BUILD WALL FRONT/////////////////
            if (
                istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == -1 ||

                istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == -1 && istypeofrb == -1 ||

                istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == 1 ||

                istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == -1 && istypeofrb == 1)
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
                    levelmap[indexinarray] = 1103;
                }
            }





















            /////////BUILD WALL LEFT FRONT INSIDE/////////////////
            if (istypeoft == -1 &&
               istypeofl == -1 && istypeofr == 1 &&
                                  istypeofb == 1 ||

                                   istypeoft == -1 &&
               istypeofl == -1 && istypeofr == 0 &&
                                  istypeofb == 1 ||

                                   istypeoft == -1 &&
               istypeofl == -1 && istypeofr == 1 &&
                                  istypeofb == 0 ||

                                   istypeoft == -1 &&
               istypeofl == -1 && istypeofr == 0 &&
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
                    levelmap[indexinarray] = 1105;
                }
            }






            /////////BUILD WALL RIGHT FRONT INSIDE/////////////////
            if (istypeoft == -1 &&
               istypeofl == 1 && istypeofr == -1 &&
                                  istypeofb == 1 ||

                                   istypeoft == -1 &&
               istypeofl == 0 && istypeofr == -1 &&
                                  istypeofb == 1 ||

                                   istypeoft == -1 &&
               istypeofl == 1 && istypeofr == -1 &&
                                  istypeofb == 0 ||

                                  istypeoft == -1 &&
               istypeofl == 0 && istypeofr == -1 &&
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
                    levelmap[indexinarray] = 1106;
                }
            }


            /////////BUILD WALL LEFT BACK INSIDE/////////////////
            if (istypeoft == 1 &&
               istypeofl == -1 && istypeofr == 1 &&
                                  istypeofb == -1 ||

                                    istypeoft == 0 &&
                istypeofl == -1 && istypeofr == 1 &&
                                     istypeofb == -1 ||

                                    istypeoft == 1 &&
               istypeofl == -1 && istypeofr == 0 &&
                                  istypeofb == -1 ||

                                  istypeoft == 0 &&
               istypeofl == -1 && istypeofr == 0 &&
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
                                  istypeofb == -1 ||

                                 istypeoft == 0 &&
               istypeofl == 1 && istypeofr == -1 &&
                                  istypeofb == -1 ||

                                 istypeoft == 1 &&
               istypeofl == 0 && istypeofr == -1 &&
                                  istypeofb == -1 ||

                                  istypeoft == 0 &&
               istypeofl == 0 && istypeofr == -1 &&
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



        }




























        public void buildWallsExceptOnFloor(int x, int y, int z)
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
            if (istypeoflt == -1 && istypeoft == 1 &&
                istypeofl == -1 &&
                istypeoflb == -1 && istypeofb == 1 ||

                istypeoflt == 1 && istypeoft == 1 &&
                istypeofl == -1 &&
                istypeoflb == -1 && istypeofb == 1 ||

                istypeoflt == -1 && istypeoft == 1 &&
                istypeofl == -1 &&
                istypeoflb == 1 && istypeofb == 1 ||

                istypeoflt == 1 && istypeoft == 1 &&
                istypeofl == -1 &&
                istypeoflb == 1 && istypeofb == 1)
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
                   levelmap[indexinarray] != 1112 &&
                   levelmap[indexinarray] != 0

                   )
                {
                    levelmap[indexinarray] = 1101;
                }
            }

            /////////BUILD WALL RIGHT/////////////////
            if (istypeoft == 1 && istypeofrt == -1 &&
                                                istypeofr == -1 &&
                                istypeofb == 1 && istypeofrb == -1 ||

                                istypeoft == 1 && istypeofrt == 1 &&
                                                istypeofr == -1 &&
                                istypeofb == 1 && istypeofrb == -1 ||

                                istypeoft == 1 && istypeofrt == -1 &&
                                                istypeofr == -1 &&
                                istypeofb == 1 && istypeofrb == 1 ||

                                istypeoft == 1 && istypeofrt == 1 &&
                                                istypeofr == -1 &&
                                istypeofb == 1 && istypeofrb == 1)
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
                   levelmap[indexinarray] != 1112 &&
                   levelmap[indexinarray] != 0

                   )
                {
                    levelmap[indexinarray] = 1102;
                }
            }


            /////////BUILD WALL BACK/////////////////
            if (istypeoflt == -1 && istypeoft == -1 && istypeofrt == -1 &&
                istypeofl == 1 && istypeofr == 1 ||

                istypeoflt == 1 && istypeoft == -1 && istypeofrt == -1 &&
                istypeofl == 1 && istypeofr == 1 ||

                istypeoflt == -1 && istypeoft == -1 && istypeofrt == 1 &&
                istypeofl == 1 && istypeofr == 1 ||

                istypeoflt == 1 && istypeoft == -1 && istypeofrt == 1 &&
                istypeofl == 1 && istypeofr == 1)
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
                   levelmap[indexinarray] != 1112 &&
                   levelmap[indexinarray] != 0

                   )
                {
                    levelmap[indexinarray] = 1104;
                }
            }


            /////////BUILD WALL FRONT/////////////////
            if (
                istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == -1 ||

                istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == -1 && istypeofrb == -1 ||

                istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == 1 ||

                istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == -1 && istypeofrb == 1)
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
                   levelmap[indexinarray] != 1112 &&
                   levelmap[indexinarray] != 0

                   )
                {
                    levelmap[indexinarray] = 1103;
                }
            }





















            /////////BUILD WALL LEFT FRONT INSIDE/////////////////
            if (istypeoft == -1 &&
               istypeofl == -1 && istypeofr == 1 &&
                                  istypeofb == 1 ||

                                   istypeoft == -1 &&
               istypeofl == -1 && istypeofr == 0 &&
                                  istypeofb == 1 ||

                                   istypeoft == -1 &&
               istypeofl == -1 && istypeofr == 1 &&
                                  istypeofb == 0 ||

                                   istypeoft == -1 &&
               istypeofl == -1 && istypeofr == 0 &&
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
                   levelmap[indexinarray] != 1112 &&
                   levelmap[indexinarray] != 0

               )
                {
                    levelmap[indexinarray] = 1105;
                }
            }






            /////////BUILD WALL RIGHT FRONT INSIDE/////////////////
            if (istypeoft == -1 &&
               istypeofl == 1 && istypeofr == -1 &&
                                  istypeofb == 1 ||

                                   istypeoft == -1 &&
               istypeofl == 0 && istypeofr == -1 &&
                                  istypeofb == 1 ||

                                   istypeoft == -1 &&
               istypeofl == 1 && istypeofr == -1 &&
                                  istypeofb == 0 ||

                                  istypeoft == -1 &&
               istypeofl == 0 && istypeofr == -1 &&
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
                   levelmap[indexinarray] != 1112 &&
                   levelmap[indexinarray] != 0

                )
                {
                    levelmap[indexinarray] = 1106;
                }
            }


            /////////BUILD WALL LEFT BACK INSIDE/////////////////
            if (istypeoft == 1 &&
               istypeofl == -1 && istypeofr == 1 &&
                                  istypeofb == -1 ||

                                    istypeoft == 0 &&
                istypeofl == -1 && istypeofr == 1 &&
                                     istypeofb == -1 ||

                                    istypeoft == 1 &&
               istypeofl == -1 && istypeofr == 0 &&
                                  istypeofb == -1 ||

                                  istypeoft == 0 &&
               istypeofl == -1 && istypeofr == 0 &&
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
                   levelmap[indexinarray] != 1112 &&
                   levelmap[indexinarray] != 0

              )
                {
                    levelmap[indexinarray] = 1107;
                }
            }



            /////////BUILD WALL LEFT BACK INSIDE/////////////////
            if (istypeoft == 1 &&
               istypeofl == 1 && istypeofr == -1 &&
                                  istypeofb == -1 ||

                                 istypeoft == 0 &&
               istypeofl == 1 && istypeofr == -1 &&
                                  istypeofb == -1 ||

                                 istypeoft == 1 &&
               istypeofl == 0 && istypeofr == -1 &&
                                  istypeofb == -1 ||

                                  istypeoft == 0 &&
               istypeofl == 0 && istypeofr == -1 &&
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
                   levelmap[indexinarray] != 1112 &&
                   levelmap[indexinarray] != 0

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
                   levelmap[indexinarray] != 1112 &&
                   levelmap[indexinarray] != 0

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
                   levelmap[indexinarray] != 1112 &&
                   levelmap[indexinarray] != 0

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
                   levelmap[indexinarray] != 1112 &&
                   levelmap[indexinarray] != 0

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
                   levelmap[indexinarray] != 1112 &&
                   levelmap[indexinarray] != 0

                    )
                {
                    levelmap[indexinarray] = 1112;
                }
            }



        }
    }
}
