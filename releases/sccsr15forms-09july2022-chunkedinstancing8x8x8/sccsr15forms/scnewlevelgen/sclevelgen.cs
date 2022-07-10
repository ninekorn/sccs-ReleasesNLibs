//DEVELOPED BY STEVE CHASSÉ AKA NINEKORN AKA NINE AKA 9

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using SharpDX;
using System.Collections;

namespace sccsr15forms
{
    public class sclevelgen
    {
        int[] arrayofcoords = new int[3];

        //https://stackoverflow.com/questions/63129018/c-sharp-check-if-dictionary-contains-key-which-is-a-reference-type
        public class SequenceEqualityComparer<T> : IEqualityComparer<IEnumerable<T>>
        {
            public bool Equals(IEnumerable<T> x, IEnumerable<T> y)
            {
                if (ReferenceEquals(x, y))
                    return true;
                else if (null == x || null == y)
                    return false;

                return Enumerable.SequenceEqual(x, y, EqualityComparer<T>.Default);
            }

            public int GetHashCode(IEnumerable<T> obj) =>
              obj == null ? 0 : obj.FirstOrDefault()?.GetHashCode() ?? 0;
        }


        public Dictionary<int[], int> typeoftiles = new Dictionary<int[], int>(new SequenceEqualityComparer<int>());// {{a, "Some Value"}};


        public List<int[]> adjacentWall = new List<int[]>();
        public Dictionary<int[], int> forsortingtiles = new Dictionary<int[], int>();

        public List<int[]> listoftiles = new List<int[]>();
        public List<int> listoftilesvalues = new List<int>();

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

        public int somerw;
        public int somerh;
        public int somerd;

        public int[] levelmap;

        public int[][] levelmapfloor;

        public int maxtileamount = 750;




        //public Dictionary<int[], int> typeoftiles = new Dictionary<int[], int>();

        ///public Dictionary<Vector3, int> typeoftiles = new Dictionary<Vector3, int>();
        //List<Vector3> floortileslist = new List<Vector3>();

        public int maxx;
        public int maxy;
        public int maxz;

        public static int wallheightsize = 10;

        public List<int[]> leftWall = new List<int[]>();
        public List<int[]> rightWall = new List<int[]>();
        public List<int[]> frontWall = new List<int[]>();
        public List<int[]> backWall = new List<int[]>();
        List<int[]> toRemove = new List<int[]>();
        List<int[]> listofremainingwalls = new List<int[]>();
        public List<int[]> builtLeftFrontInsideCorner = new List<int[]>();
        public List<int[]> builtRightFrontInsideCorner = new List<int[]>();
        public List<int[]> builtLeftBackInsideCorner = new List<int[]>();
        public List<int[]> builtRightBackInsideCorner = new List<int[]>();
        public List<int[]> builtLeftFrontOutsideCorner = new List<int[]>();
        public List<int[]> builtRightFrontOutsideCorner = new List<int[]>();
        public List<int[]> builtLeftBackOutsideCorner = new List<int[]>();
        public List<int[]> builtRightBackOutsideCorner = new List<int[]>();
        public List<int[]> leftFrontCornerOutside = new List<int[]>();
        public List<int[]> rightFrontCornerOutside = new List<int[]>();
        public List<int[]> leftBackCornerOutside = new List<int[]>();
        public List<int[]> rightBackCornerOutside = new List<int[]>();
        public List<int[]> builtLeftWall = new List<int[]>();
        public List<int[]> builtRightWall = new List<int[]>();
        public List<int[]> builtFrontWall = new List<int[]>();
        public List<int[]> builtBackWall = new List<int[]>();
        public List<int[]> leftFrontCornerInside = new List<int[]>();
        public List<int[]> rightFrontCornerInside = new List<int[]>();
        public List<int[]> leftBackCornerInside = new List<int[]>();
        public List<int[]> rightBackCornerInside = new List<int[]>();

        int minx = 0;
        int miny = 0;
        int minz = 0;


        public void StartGeneratingVoxelLevel()
        {

            try
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

                var minx = (int)sc_maths.getSomeRandNumThousandDecimal(5, 10, 1, 2, 1);
                var miny = 0;// (int)sc_maths.getSomeRandNumThousandDecimal(9, 13, 1, 2, 1);
                var minz = (int)sc_maths.getSomeRandNumThousandDecimal(5, 10, 1, 2, 1);




                maxx = minx + somerw;
                maxy = miny + somerh;
                maxz = minz + somerd;

                /*
                minx = -6;
                miny = -6;
                minz = -6;

                maxx = 6;
                maxy = 6;
                maxz = 6;
                */

                //wallheightsize = maxy-1;
                wallheightsize = 10;// maxy - 1;

                /*int typeoftilesinlevelgen = 14;
                somelevelgentypeoftiles.tilestypeof = new List<Dictionary<Vector3, int>>();

                for (int i = 0; i < typeoftilesinlevelgen; i++)
                {
                    somelevelgentypeoftiles.tilestypeof.Add(new Dictionary<Vector3, int>());
                }
                */

                //somelevelgentypeoftiles.tilestypeof = new List<Dictionary<Vector3, int>>(wallheightsize);
                //Console.WriteLine("main list count:" + somelevelgentypeoftiles.tilestypeof.Count);
                /*for (int i = 0;i < somelevelgentypeoftiles.tilestypeof.Count;i++)
                {
                    somelevelgentypeoftiles.tilestypeof[i] = new Dictionary<Vector3, int>();
                }*/





                //somewidth = 12; //(int)(maxx - minx);
                //someheight = 12;// (int)(maxy - miny);
                //somedepth = 12;// (int)(maxz - minz);

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

                maxtileamount = (somewidth * somedepth);
                //maxtileamount = 6 * 6 * 6;







                Console.WriteLine("mw:" + minx + "/mh:" + miny + "/md:" + minz + "/mxw:" + maxx + "/mxh:" + maxy + "/mxd:" + maxz);



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

                            int indexinarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                            if (indexinarray < somewidth * someheight * somedepth)
                            {

                            }
                            else
                            {
                                Console.WriteLine("OUT OF RANGE");
                            }
                        }
                    }
                }*/



                levelmap = new int[somewidth * someheight * somedepth];


                /*
                levelmapfloor = new int[somewidth][];

                for (int x = 0; x < somewidth; x++)
                {
                    levelmapfloor[x] = new int[somedepth];

                    /*for (int z = 0; z < somedepth; z++)
                    {
                        //if (x == 0 && z == 0)
                        //{
                        //    levelmapfloor[x][z] = 1;
                        //}

                        //var tilefloorornot = (int)sc_maths.getSomeRandNumThousandDecimal(0, 2, 1, 0, 1);

                        //levelmapfloor[x][z] = tilefloorornot;

                        //Console.WriteLine(tilefloorornot);
                    }
                }*/



                List<int[]> listoftileloc = new List<int[]>();

                int somepointermax = 1;// maxtileamount / 10; //maxtileamount / 10

                for (int x = 0; x < somepointermax; x++)
                {
                    /*float randx = (float)sc_maths.getSomeRandNumThousandDecimal(2, somewidth - 2, 1, 0, 0);
                    float randy = (float)sc_maths.getSomeRandNumThousandDecimal(2, someheight - 2, 1, 0, 0);
                    float randz = (float)sc_maths.getSomeRandNumThousandDecimal(2, somedepth - 2, 1, 0, 0);

                    float posx = minx + randx;
                    float posy = miny + randy;
                    float posz = minz + randz;

                    listoftileloc.Add(new Vector3(posx, posy, posz));*/

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
                int startingtileindex = 0;
                //levelmap[0] = 999;
                int countingtiletries = 0;


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


                            for (int p = 0; p < somepointermax; p++)
                            {

                                /*int neighboorx = (int)Math.Round(((listoftileloc[p].X)));
                                int neighboorz = (int)Math.Round(((listoftileloc[p].Z)));

                                Vector3 tilepos = new Vector3(neighboorx, 0, neighboorz);

                                int countofarray = typeoftiles.Count;
                                if (!typeoftiles.ContainsKey(tilepos))
                                {
                                    int indexinarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                                    if (indexinarray < somewidth * someheight * somedepth)
                                    {
                                        if (countofarray == 0)
                                        {
                                            Console.WriteLine("found starting tile");
                                            //levelmap[indexinarray] = 999;
                                            startingtile = countofarray;
                                            //Console.WriteLine(countofarray);
                                        }
                                        else
                                        {
                                            //levelmap[indexinarray] = countofarray;
                                        }
                                        //levelmap[indexinarray] = 0;

                                        //levelmapfloor[xi][zi] = 1;
                                        typeoftiles.Add(tilepos, 0);
                                        forsortingtiles.Add(tilepos, 0);
                                        countingtiletries++;
                                    }
                                    else
                                    {
                                        Console.WriteLine("OUT OF RANGE");
                                        outofrange = 1;
                                    }
                                }*/

                                outofrange = 0;
                                for (int xi = -neighbooraddx; xi <= neighbooraddx; xi++)
                                {
                                    for (int zi = -neighbooraddz; zi <= neighbooraddz; zi++)
                                    {
                                        int neighboorx = listoftileloc[p][0] + xi;// (int)Math.Round(listoftileloc[p].X) + xi;
                                        int neighboorz = listoftileloc[p][2] + zi;// (int)Math.Round(listoftileloc[p].Z) + zi;

                                        Vector3 tilepos = new Vector3(neighboorx, 0, neighboorz);
                                        
                                        arrayofcoords = new int[3];
                                        arrayofcoords[0] = (int)Math.Round(tilepos.X);
                                        arrayofcoords[1] = (int)Math.Round(tilepos.Y);
                                        arrayofcoords[2] = (int)Math.Round(tilepos.Z);

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
                                            int countofarray = typeoftiles.Count;

                                            /*if (levelmap[indexinarray] == 0)
                                            {
                                                levelmap[indexinarray] = -1;
                                            }*/

                                            if(!typeoftiles.Keys.Any(key => key.SequenceEqual(arrayofcoords)))
                                            //if (!typeoftiles.ContainsKey(tilepos))
                                            {

                                                if (countofarray == 0)
                                                {
                                                    levelmap[indexinarray] = 999;
                                                    startingtileindex = indexinarray;
                                                }

                                                levelmap[indexinarray] = countofarray;
                                                //levelmap[indexinarray] = countofarray;

                                                //levelmap[indexinarray] = 0;

                                                //levelmapfloor[xi][zi] = 1;
                                                typeoftiles.Add(arrayofcoords, 0);
                                                forsortingtiles.Add(arrayofcoords, 0);
                                            }

                                        }
                                        else
                                        {
                                            //Console.WriteLine("OUT OF RANGE");
                                            outofrange = 1;
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

                                                listoftileloc[p] = somevec;// Vector3.Zero;
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

                                                listoftileloc[p] = somevec;// Vector3.Zero;
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

                                                listoftileloc[p] = somevec;// Vector3.Zero;
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

                                                listoftileloc[p] = somevec;// Vector3.Zero;
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

                Console.WriteLine("main floor count:" + typeoftiles.Count);


                var enumerator0 = typeoftiles.GetEnumerator();
                //int startingtile = 0;
                while (enumerator0.MoveNext())
                {
                    var currentTuile = enumerator0.Current;
                    var currentTile = currentTuile.Key;
                    var currentTileType = currentTuile.Value;

                    listoftiles.Add(currentTile);
                    listoftilesvalues.Add(currentTileType);
                }



                for (int i = 0; i < levelmap.Length; i++)
                {
                    if (levelmap[i] == 0)
                    {
                        levelmap[i] = -1;
                    }
                }

                int somecounter = 0;

                for (var x = minx-3; x < maxx+3; x++)
                {
                    for (var y = miny-3; y < maxy+3; y++)
                    {
                        for (var z = minz-3; z < maxz+3; z++)
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
                                if (levelmap[indexinarray] == 0)
                                {
                                    levelmap[indexinarray] = -1;
                                }

                                for (int i = 0; i < listoftiles.Count; i++)
                                {
                                    int xi = (listoftiles[i][0]);
                                    int yi = (listoftiles[i][1]);
                                    int zi = (listoftiles[i][2]);

                                    if (xi == x && yi == y && zi == z)
                                    {
                                        levelmap[indexinarray] = i;

                                        if (i == 0)
                                        {
                                            //levelmap[indexinarray] = 999;

                                        }
                                        else
                                        {
                                            //
                                        }
                                        somecounter++;
                                    }
                                }
                            }
                        }
                    }
                }

                Console.WriteLine("c:" + somecounter + "/length:" + levelmap.Length + "/typeoftileslength:" + typeoftiles.Count);

                
                levelmap[startingtileindex] = 0;


































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














                /*
                var enumerator0 = typeoftiles.GetEnumerator();

                while (enumerator0.MoveNext())
                {
                    var currentTuile = enumerator0.Current;
                    var currentTile = currentTuile.Key;
                    checkAllSides(currentTile);
                }*/

                //Console.WriteLine("mw:" + minx + "/mh:" + miny + "/md:" + minz + "/mxw:" + maxx + "/mxh:" + maxy + "/mxd:" + maxz);

                /*

                Console.WriteLine("w:" + somewidth + "/h:" + someheight + "/d:" + somedepth);

                somewidth = (int)(maxx.X - minx.X);
                someheight = (int)(maxy.Y - miny.Y);
                somedepth = (int)(maxz.Z - minz.Z);

                levelmap = new int[width * height * depth];*/

                /*
                //unLOADING CHUNK to XML
                //unLOADING CHUNK to XML
                pathofrelease = Directory.GetCurrentDirectory();
                //Console.WriteLine(pathofrelease);
                pathofchunkmap = pathofrelease + @"\chunkmaps\";

                if (!Directory.Exists(pathofchunkmap))
                {
                    //Console.WriteLine("created directory");
                    Directory.CreateDirectory(pathofchunkmap);
                }

                //int writetofilecounter = 0;

                customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
                customCulture.NumberFormat.NumberDecimalSeparator = ".";
                System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

                path = pathofchunkmap + @"\levelfloordataneighboors" + ".xml";

                writer = new XmlTextWriter(path, System.Text.Encoding.UTF8);

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
                writer.WriteEndElement(); //open 4 //"\r\n" + 

                writer.WriteStartElement("intmap"); //open 4 //"\r\n" + 
                writer.WriteValue("\r\n");
                for (int x = 0; x < levelmapfloor.Length; x++)
                {
                    writer.WriteValue(levelmapfloor[x]);
                    writer.WriteValue("\r\n");
                }
                writer.WriteEndElement();

                writer.WriteEndElement(); //close 2
                writer.Close();




                Console.WriteLine("generated new level w neighboors");*/

            }
            catch (Exception ex)
            {
                Program.MessageBox((IntPtr)0, "" + ex.ToString(), "scmsg", 0);
            }
        }



        void checkAllSides(int[] currentTilePos)
        {

            for (int x = -1; x <= 1; x++)
            {
                for (int z = -1; z <= 1; z++)
                {
                    int checkx = (((currentTilePos[0] + x)));
                    int checkz = (((currentTilePos[2] + z)));

                    //float checkx = ((currentTilePos.x + x));
                    //float checkz = ((currentTilePos.z + z));

                    if (checkx == currentTilePos[0] && checkz == currentTilePos[2] + (1) ||
                        checkx == currentTilePos[0] && checkz == currentTilePos[2] - (1) ||
                        checkx == currentTilePos[0] + (1) && checkz == currentTilePos[2] ||
                        checkx == currentTilePos[0] - (1) && checkz == currentTilePos[2] ||

                        checkx == currentTilePos[0] + (1) && checkz == currentTilePos[2] + (1) ||
                        checkx == currentTilePos[0] - (1) && checkz == currentTilePos[2] + (1) ||
                        checkx == currentTilePos[0] + (1) && checkz == currentTilePos[2] - (1) ||
                        checkx == currentTilePos[0] - (1) && checkz == currentTilePos[2] - (1))
                    {


                        int xi = checkx;
                        int zi = checkz;

                        if (xi < 0)
                        {
                            xi *= -1;
                            xi = xi + (maxx - 1);
                        }
                        if (zi < 0)
                        {
                            zi *= -1;
                            zi = zi + (maxz - 1);
                        }

                        // Console.WriteLine("test0");

                        //if (xi < somewidth && somedepth < zi)
                        {
                            //Console.WriteLine("test1");
                            //Instantiate(sphere, new Vector3(checkx, 0, checkz), Quaternion.identity);

                            arrayofcoords = new int[3];
                            arrayofcoords[0] = (checkx);
                            arrayofcoords[1] = (0);
                            arrayofcoords[2] = (checkz);

                            int countofarray = typeoftiles.Count;
                            if (!adjacentWall.Any(key => key.SequenceEqual(arrayofcoords)) && !typeoftiles.Any(x => Enumerable.SequenceEqual(arrayofcoords, x.Key))) //typeoftiles.Keys.Any(key => key.SequenceEqual(arrayofcoords))) //!!typeoftiles.Keys.Any(key => key.SequenceEqual(arrayofcoords)))
                            {

                                //Console.WriteLine("new tile neighboor");

                                int xx = (checkx);
                                int yy = 0;// (int)Math.Round(0);
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


                                if (indexinarray < somewidth * someheight * somedepth)
                                {
                                    levelmap[indexinarray] = countofarray;


                                    //Program.MessageBox((IntPtr)0, "test0", "scmsg", 0);
                                    adjacentWall.Add(arrayofcoords);
                                    //typeoftiles.Add(new Vector3(checkx, 0, checkz),0);

                                    //levelmapfloor[xi][zi] = 1;

























                                    istypeofl = -2;
                                    istypeofr = -2;
                                    istypeoft = -2;
                                    istypeofb = -2;

                                    istypeoflt = -2;
                                    istypeofrt = -2;
                                    istypeoflb = -2;
                                    istypeofrb = -2;

                                    /*arrayofcoords = new int[3];
                                    arrayofcoords[0] = (checkx);
                                    arrayofcoords[1] = (0);
                                    arrayofcoords[2] = (checkz);*/

                                    //Vector3 currentTile = new Vector3(checkx, 0, checkz);
                                    //Vector3 tempvec = currentTile;//.X - 1;
                                    var tempvec = arrayofcoords;
                                    tempvec[0] -= 1;


                                    var leftTile = forsortingtiles.Where(x => x.Key == arrayofcoords).ToArray();//  findTiles(currentTile.X - 1, currentTile.Z);
                                    if (leftTile != null)
                                    {
                                        if (leftTile.Length > 0)
                                        {
                                            if (leftTile[0].Value == 0)
                                            {
                                                istypeofl = 0;
                                            }
                                            else if (leftTile[0].Value == -1)
                                            {
                                                istypeofl = 1;
                                            }
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


                                    tempvec = arrayofcoords;//.X - 1;
                                    tempvec[0] += 1;
                                    var rightTile = forsortingtiles.Where(x => x.Key == tempvec).ToArray();//  findTiles(currentTile.X - 1, currentTile.Z);
                                    if (rightTile != null)
                                    {
                                        if (rightTile.Length > 0)
                                        {
                                            if (rightTile[0].Value == 0)
                                            {
                                                istypeofr = 0;
                                            }
                                            else if (rightTile[0].Value == -1)
                                            {
                                                istypeofr = 1;
                                            }
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

                                    tempvec = arrayofcoords;//.X - 1;
                                    tempvec[2] += 1;
                                    var topTile = forsortingtiles.Where(x => x.Key == tempvec).ToArray();//  findTiles(currentTile.X - 1, currentTile.Z);
                                    if (topTile != null)
                                    {
                                        if (topTile.Length > 0)
                                        {
                                            if (topTile[0].Value == 0)
                                            {
                                                istypeoft = 0;
                                            }
                                            else if (topTile[0].Value == -1)
                                            {
                                                istypeoft = 1;
                                            }
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

                                    tempvec = arrayofcoords;//.X - 1;
                                    tempvec[2] -= 1;
                                    var backTile = forsortingtiles.Where(x => x.Key == tempvec).ToArray();//  findTiles(currentTile.X - 1, currentTile.Z);
                                    if (backTile != null)
                                    {
                                        if (backTile.Length > 0)
                                        {
                                            if (backTile[0].Value == 0)
                                            {
                                                istypeofb = 0;
                                            }
                                            else if (backTile[0].Value == -1)
                                            {
                                                istypeofb = 1;
                                            }
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









                                    tempvec = arrayofcoords;//.X - 1;
                                    tempvec[0] -= 1;
                                    tempvec[2] += 1;
                                    var topTilel = forsortingtiles.Where(x => x.Key == tempvec).ToArray();//  findTiles(currentTile.X - 1, currentTile.Z);
                                    if (topTilel != null)
                                    {
                                        if (topTilel.Length > 0)
                                        {
                                            if (topTilel[0].Value == 0)
                                            {
                                                //Console.WriteLine("found0");
                                                istypeoflt = 0;
                                            }
                                            else if (topTilel[0].Value == -1)
                                            {
                                                //Console.WriteLine("found1");
                                                istypeoflt = 1;
                                            }
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

                                    tempvec = arrayofcoords;//.X - 1;
                                    tempvec[0] -= 1;
                                    tempvec[2] -= 1;
                                    var backTilel = forsortingtiles.Where(x => x.Key == tempvec).ToArray();//  findTiles(currentTile.X - 1, currentTile.Z);
                                    if (backTilel != null)
                                    {
                                        if (backTilel.Length > 0)
                                        {
                                            if (backTilel[0].Value == 0)
                                            {
                                                istypeoflb = 0;
                                            }
                                            else if (backTilel[0].Value == -1)
                                            {
                                                istypeoflb = 1;
                                            }

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



                                    tempvec = arrayofcoords;//.X - 1;
                                    tempvec[0] += 1;
                                    tempvec[2] += 1;
                                    var topTiler = forsortingtiles.Where(x => x.Key == tempvec).ToArray();//  findTiles(currentTile.X - 1, currentTile.Z);
                                    if (topTiler != null)
                                    {
                                        if (topTiler.Length > 0)
                                        {
                                            if (topTiler[0].Value == 0)
                                            {
                                                istypeofrt = 0;
                                            }
                                            else if (topTiler[0].Value == -1)
                                            {
                                                istypeofrt = 1;
                                            }
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


                                    tempvec = arrayofcoords;//.X - 1;
                                    tempvec[0] += 1;
                                    tempvec[2] -= 1;
                                    var backTiler = forsortingtiles.Where(x => x.Key == tempvec).ToArray();//  findTiles(currentTile.X - 1, currentTile.Z);
                                    if (backTiler != null)
                                    {
                                        if (backTiler.Length > 0)
                                        {
                                            if (backTiler[0].Value == 0)
                                            {
                                                istypeofrb = 0;
                                            }
                                            else if (backTiler[0].Value == -1)
                                            {
                                                istypeofrb = 1;
                                            }
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


                                    //IS THIS A TILE OR A WALL
                                    /////////////////////////////////////////////////////////////
                                    if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
                                        istypeofl == 0 &&                    istypeofr == 0 &&
                                        istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0)
                                    {

                                        forsortingtiles.Add(arrayofcoords, 0);


                                    }
                                    else
                                    {
                                        forsortingtiles.Add(arrayofcoords, -1);
                                    }
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

                /*var enumerator0 = adjacentWall.GetEnumerator();
                while (enumerator0.MoveNext())
                {
                    var currentTuile = enumerator0.Current;
                    //currentTile = currentTuile;
                    StartCoroutine(buildWalls(currentTuile));
                }*/





                //REMOVING THE EASILY SORTABLE TILES FROM THE ADJACENTWALL LIST AND ADDING THEM TO THE typeoftiles LIST
                //REMOVING THE EASILY SORTABLE TILES FROM THE ADJACENTWALL LIST AND ADDING THEM TO THE typeoftiles LIST
                //REMOVING THE EASILY SORTABLE TILES FROM THE ADJACENTWALL LIST AND ADDING THEM TO THE typeoftiles LIST
                for (int i = 0; i < adjacentWall.Count; i++)
                {
                    var currentTuile = adjacentWall[i];
                    buildsomefloortiles(currentTuile);
                    //adjacentWall.Remove(adjacentWall[i]);
                }

                //REMOVING THE EASILY SORTABLE TILES FROM THE ADJACENTWALL LIST AND ADDING THEM TO THE typeoftiles LIST
                //REMOVING THE EASILY SORTABLE TILES FROM THE ADJACENTWALL LIST AND ADDING THEM TO THE typeoftiles LIST
                //REMOVING THE EASILY SORTABLE TILES FROM THE ADJACENTWALL LIST AND ADDING THEM TO THE typeoftiles LIST
                for (int i = 0; i < adjacentWall.Count; i++)
                {
                    var currentTuile = adjacentWall[i];
                    //currentTuile.Y = 1.0f;
                    buildsomewallremains(currentTuile);
                    //adjacentWall.Remove(adjacentWall[i]);
                }

                for (int i = 0; i < adjacentWall.Count; i++)
                {
                    var currentTuile = adjacentWall[i];
                    //currentTuile.Y = 1.0f;
                    buildWallsRerolled(currentTuile);
                    //adjacentWall.Remove(adjacentWall[i]);
                }

                /*for (int i = 0; i < adjacentWall.Count; i++)
                {
                    var currentTuile = adjacentWall[i];
                    buildWallsRerolled(currentTuile);
                    //adjacentWall.Remove(adjacentWall[i]);
                }*/



                toRemove = adjacentWall;

                for (int i = 0; i < backWall.Count; i++)//1101
                {
                    toRemove.Remove(backWall[i]);//1102
                }
                for (int i = 0; i < frontWall.Count; i++)
                {
                    toRemove.Remove(frontWall[i]);//1103
                }
                for (int i = 0; i < rightWall.Count; i++)
                {
                    toRemove.Remove(rightWall[i]);
                }
                for (int i = 0; i < leftWall.Count; i++)
                {
                    toRemove.Remove(leftWall[i]);
                }
                for (int i = 0; i < builtLeftFrontInsideCorner.Count; i++)
                {
                    toRemove.Remove(builtLeftFrontInsideCorner[i]);
                }
                for (int i = 0; i < builtRightFrontInsideCorner.Count; i++)
                {
                    toRemove.Remove(builtRightFrontInsideCorner[i]);
                }
                for (int i = 0; i < builtLeftBackInsideCorner.Count; i++)
                {
                    toRemove.Remove(builtLeftBackInsideCorner[i]);
                }
                for (int i = 0; i < builtRightBackInsideCorner.Count; i++)
                {
                    toRemove.Remove(builtRightBackInsideCorner[i]);
                }
                for (int i = 0; i < builtLeftFrontOutsideCorner.Count; i++)
                {
                    toRemove.Remove(builtLeftFrontOutsideCorner[i]);
                }
                for (int i = 0; i < builtRightFrontOutsideCorner.Count; i++)
                {
                    toRemove.Remove(builtRightFrontOutsideCorner[i]);
                }
                for (int i = 0; i < builtLeftBackOutsideCorner.Count; i++)
                {
                    toRemove.Remove(builtLeftBackOutsideCorner[i]);
                }
                for (int i = 0; i < builtRightBackOutsideCorner.Count; i++)
                {
                    toRemove.Remove(builtRightBackOutsideCorner[i]);
                }
                for (int i = 0; i < listofremainingwalls.Count; i++)
                {
                    toRemove.Remove(listofremainingwalls[i]);
                }








                buildFloorTiles();

                for (int i = 0; i < listofremainingwalls.Count; i++)
                {
                    var currentTuile = listofremainingwalls[i];
                    //currentTuile.Y = 1.0f;
                    buildWallsRerolled(currentTuile);
                    //adjacentWall.Remove(adjacentWall[i]);

                }

                toRemove = listofremainingwalls;

                for (int i = 0; i < backWall.Count; i++)
                {
                    toRemove.Remove(backWall[i]);
                }
                for (int i = 0; i < frontWall.Count; i++)
                {
                    toRemove.Remove(frontWall[i]);
                }
                for (int i = 0; i < rightWall.Count; i++)
                {
                    toRemove.Remove(rightWall[i]);
                }
                for (int i = 0; i < leftWall.Count; i++)
                {
                    toRemove.Remove(leftWall[i]);
                }
                for (int i = 0; i < builtLeftFrontInsideCorner.Count; i++)
                {
                    toRemove.Remove(builtLeftFrontInsideCorner[i]);
                }
                for (int i = 0; i < builtRightFrontInsideCorner.Count; i++)
                {
                    toRemove.Remove(builtRightFrontInsideCorner[i]);
                }
                for (int i = 0; i < builtLeftBackInsideCorner.Count; i++)
                {
                    toRemove.Remove(builtLeftBackInsideCorner[i]);
                }
                for (int i = 0; i < builtRightBackInsideCorner.Count; i++)
                {
                    toRemove.Remove(builtRightBackInsideCorner[i]);
                }
                for (int i = 0; i < builtLeftFrontOutsideCorner.Count; i++)
                {
                    toRemove.Remove(builtLeftFrontOutsideCorner[i]);
                }
                for (int i = 0; i < builtRightFrontOutsideCorner.Count; i++)
                {
                    toRemove.Remove(builtRightFrontOutsideCorner[i]);
                }
                for (int i = 0; i < builtLeftBackOutsideCorner.Count; i++)
                {
                    toRemove.Remove(builtLeftBackOutsideCorner[i]);
                }
                for (int i = 0; i < builtRightBackOutsideCorner.Count; i++)
                {
                    toRemove.Remove(builtRightBackOutsideCorner[i]);
                }




                for (int i = 0; i < toRemove.Count; i++)
                {
                    var currentTuile = toRemove[i];
                    //buildWallsRerolled(currentTuile);

                    arrayofcoords = new int[3];
                    arrayofcoords[0] = (currentTuile[0]);
                    arrayofcoords[1] = (currentTuile[1]);
                    arrayofcoords[2] = (currentTuile[2]);

                    int countofarray = typeoftiles.Count;
                    if (!!typeoftiles.Keys.Any(key => key.SequenceEqual(arrayofcoords)))
                    {

                        int xx = arrayofcoords[0];
                        int yy = arrayofcoords[1];
                        int zz = arrayofcoords[2];

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
                        typeoftiles.Add(currentTuile, -2);
                        //leftWall.Add(currentTile);
                        //buildWallLeft();
                        //forsortingtiles.Remove(currentTile);
                    }
                }




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
                        for (int tw = 1; tw <= wallheightsize-1; tw++)
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
                        arrayofcoords[1] = (somekey[1]) + wallheightsize-1;
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

















                /*
                Dictionary<Vector3, int> firstlayerwallisfloor = new Dictionary<Vector3, int>();

                var enumerator00 = typeoftiles.GetEnumerator();
                //Vector3? tls0 = null;     
                while (enumerator00.MoveNext())
                {
                    var tls0 = enumerator00.Current;
                    var somekey = tls0.Key;
                    var someval = tls0.Value;

                    if (someval != 0)
                    {
                        if (!firstlayerwallisfloor.ContainsKey(new Vector3(somekey.X, somekey.Y - 1.0f, somekey.Z)))
                        {
                            firstlayerwallisfloor.Add(new Vector3(somekey.X, somekey.Y - 1.0f, somekey.Z), someval);
                        }

                    }
                }

                var enumerator11 = firstlayerwallisfloor.GetEnumerator();
                //Vector3? tls0 = null;     
                while (enumerator11.MoveNext())
                {
                    var tls0 = enumerator11.Current;
                    var somekey = tls0.Key;
                    var someval = tls0.Value;

                    if (!typeoftiles.Keys.Any(key => key.SequenceEqual(arrayofcoords))
                    {
                        typeoftiles.Add(somekey, 0);
                        //leftWall.Add(currentTile);
                        //buildWallLeft();
                        //forsortingtiles.Remove(currentTile);
                    }
                }*/


                /*
                Dictionary<Vector3, int> toplayerwallisfloor = new Dictionary<Vector3, int>();

                var enumerator000 = typeoftiles.GetEnumerator();
                //Vector3? tls0 = null;     
                while (enumerator000.MoveNext())
                {
                    var tls0 = enumerator000.Current;
                    var somekey = tls0.Key;
                    var someval = tls0.Value;

                    if (someval != 0)
                    {
                        /*if (!firstlayerwallisfloor.ContainsKey(new Vector3(somekey.X, somekey.Y - 1.0f, somekey.Z)))
                        {
                            firstlayerwallisfloor.Add(new Vector3(somekey.X, somekey.Y - 1.0f, somekey.Z), someval);
                        }
                        
                       if (!toplayerwallisfloor.ContainsKey(new Vector3(somekey.X, somekey.Y + 4.0f, somekey.Z)))
                        {
                            toplayerwallisfloor.Add(new Vector3(somekey.X, somekey.Y + 4.0f, somekey.Z), someval);
                        }
                        /*if (!topwalllayer.ContainsKey(new Vector3(somekey.X, somekey.Y + 1, somekey.Z)))
                        {
                            topwalllayer.Add(new Vector3(somekey.X, somekey.Y + 1, somekey.Z), someval);
                        }
                    }
                }

                var enumerator111 = toplayerwallisfloor.GetEnumerator();
                //Vector3? tls0 = null;     
                while (enumerator111.MoveNext())
                {
                    var tls0 = enumerator111.Current;
                    var somekey = tls0.Key;
                    var someval = tls0.Value;

                    if (!typeoftiles.Keys.Any(key => key.SequenceEqual(arrayofcoords))
                    {
                        typeoftiles.Add(somekey, 16);
                        //leftWall.Add(currentTile);
                        //buildWallLeft();
                        //forsortingtiles.Remove(currentTile);
                    }
                }*/

























                /*
                for (int i = 0; i < typeoftiles.Count; i++)
                {

                    var currentTuile = adjacentWall[i];
                    buildWallsRerolled(currentTuile);
                }*/















                /*
                for (int i = 0; i < toRemove.Count; i++)
                {
                    var currentTuile = toRemove[i];
                    buildWallsRerolled(currentTuile);
                }*/


                /* 

                 for (int i = 0; i < toRemove.Count; i++)
                 {
                     var currentTuile = toRemove[i];
                     buildWallsRerolled(currentTuile);
                     //adjacentWall.Remove(adjacentWall[i]);
                 }*/




                /*
                for (int i = 0; i < adjacentWall.Count; i++)
                {
                    var currentTuile = adjacentWall[i];
                    buildsomewallremains(currentTuile);
                    //adjacentWall.Remove(adjacentWall[i]);
                }*/






















                /*if (counter999 == 0)
                {
                    totalTiles = toRemove.Count;

                    for (int i = 0; i < toRemove.Count; i++)
                    {
                        var currentTile = toRemove[i];
                        buildFloorTiles();
                    }



                    //chunks = new List<GameObject>();
                    //chunkz = GameObject.FindGameObjectsWithTag("chunks");
                    //StartCoroutine(buildFaces());

                    counter999 = 1;
                }*/


                //singleChunk.GetComponent<newFloorTiles>().Regenerate();

                //GetComponent<startGeneratingFaces>().BuildFaces();


                counter = 2;
            }



            /*
            if (counter == 2)
            {
                //Debug.Log("total: " + totalTiles + " corout: " + countingCoroutines);

                if (countingCoroutinesStart == countingCoroutinesEnd)
                {
                    //BuildFaces();

                    counter = 3;
                }
            }*/

        }


        void buildFloorTiles()
        {
            //countingCoroutinesStart++;

            //yield return new WaitForSeconds(BuildingWaitTime);

            for (int i = 0; i < toRemove.Count; i++)
            {
                int countofarray = typeoftiles.Count;
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(toRemove[i])))
                {

                    int xx = (toRemove[i][0]);
                    int yy = (toRemove[i][1]);
                    int zz = (toRemove[i][2]);

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

                    typeoftiles.Add(toRemove[i], 0);
                    forsortingtiles.Remove(toRemove[i]);
                    forsortingtiles.Add(toRemove[i], 0);
                }
                /*if (!builtFloorTiles.Contains(toRemove[i]))
                {
                    //Instantiate(floorTiles, toRemove[i], Quaternion.identity);
                    builtFloorTiles.Add(toRemove[i]);
                    //yield return new WaitForSeconds(BuildingWaitTime);
                }*/
                //yield return new WaitForSeconds(BuildingWaitTime);
            }
            //yield return new WaitForSeconds(BuildingWaitTime);

            //countingCoroutinesEnd++;

        }






        void buildWallsRerolled(int[] currentTile)
        {

            istypeofl = -2;
            istypeofr = -2;
            istypeoft = -2;
            istypeofb = -2;

            istypeoflt = -2;
            istypeofrt = -2;
            istypeoflb = -2;
            istypeofrb = -2;

            /*var somevalueindict = typeoftiles.Where(x => x.Key == currentTile).ToArray();

            if (somevalueindict!= null)
            {
                if (somevalueindict[0].Value == 0)
                {
                    istypeof = 0;
                }
                else if (somevalueindict[0].Value == -1)
                {
                    istypeof = -1;
                }
            }*/

            int[] tempvec = currentTile;//.X - 1;
            tempvec[0] -= 1;
            var leftTile = forsortingtiles.Where(x => x.Key == tempvec).ToArray();//  findTiles(currentTile.X - 1, currentTile.Z);
            if (leftTile != null)
            {
                if (leftTile.Length > 0)
                {
                    if (leftTile[0].Value == 0)
                    {
                        istypeofl = 0;
                    }
                    else if (leftTile[0].Value == -1)
                    {
                        istypeofl = 1;
                    }
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


            tempvec = currentTile;//.X - 1;
            tempvec[0] += 1;
            var rightTile = forsortingtiles.Where(x => x.Key == tempvec).ToArray();//  findTiles(currentTile.X - 1, currentTile.Z);
            if (rightTile != null)
            {
                if (rightTile.Length > 0)
                {
                    if (rightTile[0].Value == 0)
                    {
                        istypeofr = 0;
                    }
                    else if (rightTile[0].Value == -1)
                    {
                        istypeofr = 1;
                    }
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

            tempvec = currentTile;//.X - 1;
            tempvec[2] += 1;
            var topTile = forsortingtiles.Where(x => x.Key == tempvec).ToArray();//  findTiles(currentTile.X - 1, currentTile.Z);
            if (topTile != null)
            {
                if (topTile.Length > 0)
                {
                    if (topTile[0].Value == 0)
                    {
                        istypeoft = 0;
                    }
                    else if (topTile[0].Value == -1)
                    {
                        istypeoft = 1;
                    }
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

            tempvec = currentTile;//.X - 1;
            tempvec[2] -= 1;
            var backTile = forsortingtiles.Where(x => x.Key == tempvec).ToArray();//  findTiles(currentTile.X - 1, currentTile.Z);
            if (backTile != null)
            {
                if (backTile.Length > 0)
                {
                    if (backTile[0].Value == 0)
                    {
                        istypeofb = 0;
                    }
                    else if (backTile[0].Value == -1)
                    {
                        istypeofb = 1;
                    }
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









            tempvec = currentTile;//.X - 1;
            tempvec[0] -= 1;
            tempvec[2] += 1;
            var topTilel = forsortingtiles.Where(x => x.Key == tempvec).ToArray();//  findTiles(currentTile.X - 1, currentTile.Z);
            if (topTilel != null)
            {
                if (topTilel.Length > 0)
                {
                    if (topTilel[0].Value == 0)
                    {
                        //Console.WriteLine("found0");
                        istypeoflt = 0;
                    }
                    else if (topTilel[0].Value == -1)
                    {
                        //Console.WriteLine("found1");
                        istypeoflt = 1;
                    }
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

            tempvec = currentTile;//.X - 1;
            tempvec[0] -= 1;
            tempvec[2] -= 1;
            var backTilel = forsortingtiles.Where(x => x.Key == tempvec).ToArray();//  findTiles(currentTile.X - 1, currentTile.Z);
            if (backTilel != null)
            {
                if (backTilel.Length > 0)
                {
                    if (backTilel[0].Value == 0)
                    {
                        istypeoflb = 0;
                    }
                    else if (backTilel[0].Value == -1)
                    {
                        istypeoflb = 1;
                    }

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



            tempvec = currentTile;//.X - 1;
            tempvec[0] += 1;
            tempvec[2] += 1;
            var topTiler = forsortingtiles.Where(x => x.Key == tempvec).ToArray();//  findTiles(currentTile.X - 1, currentTile.Z);
            if (topTiler != null)
            {
                if (topTiler.Length > 0)
                {
                    if (topTiler[0].Value == 0)
                    {
                        istypeofrt = 0;
                    }
                    else if (topTiler[0].Value == -1)
                    {
                        istypeofrt = 1;
                    }
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


            tempvec = currentTile;//.X - 1;
            tempvec[0] += 1;
            tempvec[2] -= 1;
            var backTiler = forsortingtiles.Where(x => x.Key == tempvec).ToArray();//  findTiles(currentTile.X - 1, currentTile.Z);
            if (backTiler != null)
            {
                if (backTiler.Length > 0)
                {
                    if (backTiler[0].Value == 0)
                    {
                        istypeofrb = 0;
                    }
                    else if (backTiler[0].Value == -1)
                    {
                        istypeofrb = 1;
                    }
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


            //Console.WriteLine(istypeoflt + "_" + istypeoft + "_" + istypeofrt + "_" + istypeofl + "_" + istypeofr + "_" + istypeoflb + "_" + istypeofb + "_" + istypeofrb);






            /////////BUILD WALL LEFT/////////////////
            if (istypeoflt == -1 && istypeoft == 1 &&
                istypeofl == -1 && istypeofr == 0 &&
                istypeoflb == -1 && istypeofb == 1)
            {
                //Console.WriteLine("sometest");
                int countofarray = typeoftiles.Count;
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    int xx = (currentTile[0]);
                    int yy = (currentTile[1]);
                    int zz = (currentTile[2]);

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
                    typeoftiles.Add(currentTile, 1);
                    //leftWall.Add(currentTile);
                    buildWallLeft();
                }
            }
            if (istypeoflt == 1 && istypeoft == 1 &&
                istypeofl == -1 && istypeofr == 0 &&
                istypeoflb == -1 && istypeofb == 1)
            {
                int countofarray = typeoftiles.Count;
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    int xx = (currentTile[0]);
                    int yy = (currentTile[1]);
                    int zz = (currentTile[2]);

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

                    typeoftiles.Add(currentTile, 1);
                    //leftWall.Add(currentTile);
                    buildWallLeft();
                }
            }
            if (istypeoflt == -1 && istypeoft == 1 &&
              istypeofl == -1 && istypeofr == 0 &&
              istypeoflb == 1 && istypeofb == 1)
            {
                int countofarray = typeoftiles.Count;
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    int xx = (currentTile[0]);
                    int yy = (currentTile[1]);
                    int zz = (currentTile[2]);

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


                    typeoftiles.Add(currentTile, 1);
                    //leftWall.Add(currentTile);
                    buildWallLeft();
                }
            }
            if (istypeoflt == 1 && istypeoft == 1 &&
                istypeofl == -1 && istypeofr == 0 &&
                istypeoflb == 1 && istypeofb == 1)
            {
                int countofarray = typeoftiles.Count;
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    int xx = (currentTile[0]);
                    int yy = (currentTile[1]);
                    int zz = (currentTile[2]);

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


                    typeoftiles.Add(currentTile, 1);
                    //leftWall.Add(currentTile);
                    buildWallLeft();
                }
            }
            /*////
            if (istypeoflt == -1 && istypeoft == 1 &&
                istypeofl == -1 && istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 1);
                    //leftWall.Add(currentTile);
                    buildWallLeft();
                }
            }
            if (istypeoflt == 1 && istypeoft == 1 &&
                istypeofl == -1 && istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 1);
                    //leftWall.Add(currentTile);
                    buildWallLeft();
                }
            }
            if (istypeoflt == -1 && istypeoft == 1 &&
              istypeofl == -1 && istypeofr == 1 &&
              istypeoflb == 1 && istypeofb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 1);
                    //leftWall.Add(currentTile);
                    buildWallLeft();
                }
            }
            if (istypeoflt == 1 && istypeoft == 1 &&
                istypeofl == -1 && istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 1);
                    //leftWall.Add(currentTile);
                    buildWallLeft();
                }
            }
            */



            /////////BUILD WALL RIGHT/////////////////
            if (istypeoft == 1 &&                   istypeofrt == -1 &&
                 istypeofl == 0 &&                  istypeofr == -1 &&
                                 istypeofb == 1 && istypeofrb == -1)
            {
                int countofarray = typeoftiles.Count;
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    int xx = (currentTile[0]);
                    int yy = (currentTile[1]);
                    int zz = (currentTile[2]);

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

                    typeoftiles.Add(currentTile, 2);
                    //leftWall.Add(currentTile);
                    buildWallRight();
                }
            }

            if (istypeoft == 1 && istypeofrt == 1 &&
                 istypeofl == 0 && istypeofr == -1 &&
                 istypeofb == 1 && istypeofrb == -1)
            {
                int countofarray = typeoftiles.Count;
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    int xx = (currentTile[0]);
                    int yy = (currentTile[1]);
                    int zz = (currentTile[2]);

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

                    typeoftiles.Add(currentTile, 2);
                    //leftWall.Add(currentTile);
                    buildWallRight();
                }
            }

            if (istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 0 && istypeofr == -1 &&
                istypeofb == 1 && istypeofrb == 1)
            {
                int countofarray = typeoftiles.Count;
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    int xx = (currentTile[0]);
                    int yy = (currentTile[1]);
                    int zz = (currentTile[2]);

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

                    typeoftiles.Add(currentTile, 2);
                    //leftWall.Add(currentTile);
                    buildWallRight();
                }
            }

            if (istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 0 && istypeofr == -1 &&
                istypeofb == 1 && istypeofrb == 1)
            {
                int countofarray = typeoftiles.Count;
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    int xx = (currentTile[0]);
                    int yy = (currentTile[1]);
                    int zz = (currentTile[2]);

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

                    typeoftiles.Add(currentTile, 2);
                    //leftWall.Add(currentTile);
                    buildWallRight();
                }
            }
            //////
            /*if (istypeoft == 1 && istypeofrt == -1 &&
                 istypeofl == 1 && istypeofr == -1 &&
                                 istypeofb == 1 && istypeofrb == -1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 2);
                    //leftWall.Add(currentTile);
                    buildWallRight();
                }
            }

            if (istypeoft == 1 && istypeofrt == 1 &&
                 istypeofl == 1 && istypeofr == -1 &&
                 istypeofb == 1 && istypeofrb == -1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 2);
                    //leftWall.Add(currentTile);
                    buildWallRight();
                }
            }

            if (istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 1 && istypeofr == -1 &&
                istypeofb == 1 && istypeofrb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 2);
                    //leftWall.Add(currentTile);
                    buildWallRight();
                }
            }

            if (istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 1 && istypeofr == -1 &&
                istypeofb == 1 && istypeofrb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 2);
                    //leftWall.Add(currentTile);
                    buildWallRight();
                }
            }*/



            /////////BUILD WALL BACK/////////////////

            if (istypeoflt == -1 && istypeoft == -1 && istypeofrt == -1 &&
               istypeofl == 1 && istypeofr == 1 &&
               istypeofb == 0)
            {
                int countofarray = typeoftiles.Count;
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    int xx = (currentTile[0]);
                    int yy = (currentTile[1]);
                    int zz = (currentTile[2]);

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

                    typeoftiles.Add(currentTile, 4);
                    //leftWall.Add(currentTile);
                    buildWallBack();
                }
            }
            if (istypeoflt == 1 && istypeoft == -1 && istypeofrt == -1 &&
              istypeofl == 1 && istypeofr == 1 &&
              istypeofb == 0)
            {
                int countofarray = typeoftiles.Count;
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    int xx = (currentTile[0]);
                    int yy = (currentTile[1]);
                    int zz = (currentTile[2]);

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

                    typeoftiles.Add(currentTile, 4);
                    //leftWall.Add(currentTile);
                    buildWallBack();
                }
            }
            if (istypeoflt == -1 && istypeoft == -1 && istypeofrt == 1 &&
              istypeofl == 1 && istypeofr == 1 &&
              istypeofb == 0)
            {
                int countofarray = typeoftiles.Count;
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    int xx = (currentTile[0]);
                    int yy = (currentTile[1]);
                    int zz = (currentTile[2]);

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

                    typeoftiles.Add(currentTile, 4);
                    //leftWall.Add(currentTile);
                    buildWallBack();
                }
            }
            if (istypeoflt == 1 && istypeoft == -1 && istypeofrt == 1 &&
                   istypeofl == 1 && istypeofr == 1 &&
                   istypeofb == 0)
            {
                int countofarray = typeoftiles.Count;
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    int xx = (currentTile[0]);
                    int yy = (currentTile[1]);
                    int zz = (currentTile[2]);

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

                    typeoftiles.Add(currentTile, 4);
                    //leftWall.Add(currentTile);
                    buildWallBack();
                }
            }
            //////
            /*if (istypeoflt == -1 && istypeoft == -1 && istypeofrt == -1 &&
               istypeofl == 1 && istypeofr == 1 &&
               istypeofb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 4);
                    //leftWall.Add(currentTile);
                    buildWallBack();
                }
            }
            if (istypeoflt == 1 && istypeoft == -1 && istypeofrt == -1 &&
              istypeofl == 1 && istypeofr == 1 &&
              istypeofb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 4);
                    //leftWall.Add(currentTile);
                    buildWallBack();
                }
            }
            if (istypeoflt == -1 && istypeoft == -1 && istypeofrt == 1 &&
              istypeofl == 1 && istypeofr == 1 &&
              istypeofb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 4);
                    //leftWall.Add(currentTile);
                    buildWallBack();
                }
            }
            if (istypeoflt == 1 && istypeoft == -1 && istypeofrt == 1 &&
                   istypeofl == 1 && istypeofr == 1 &&
                   istypeofb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 4);
                    //leftWall.Add(currentTile);
                    buildWallBack();
                }
            }
            */



            /////////BUILD WALL FRONT/////////////////

            if (istypeoft == 0 &&
               istypeofl == 1 && istypeofr == 1 &&
                 istypeoflb == -1 && istypeofb == -1 && istypeofrb == -1)
            {
                int countofarray = typeoftiles.Count;
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    int xx = (currentTile[0]);
                    int yy = (currentTile[1]);
                    int zz = (currentTile[2]);

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

                    typeoftiles.Add(currentTile, 3);
                    //leftWall.Add(currentTile);
                    buildWallFront();
                }
            }
            if (istypeoft == 0 &&
              istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == -1 && istypeofrb == -1)
            {
                int countofarray = typeoftiles.Count;
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    int xx = (currentTile[0]);
                    int yy = (currentTile[1]);
                    int zz = (currentTile[2]);

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

                    typeoftiles.Add(currentTile, 3);
                    //leftWall.Add(currentTile);
                    buildWallFront();
                }
            }
            if (istypeoft == 0 &&
              istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == 1)
            {
                int countofarray = typeoftiles.Count;
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    int xx = (currentTile[0]);
                    int yy = (currentTile[1]);
                    int zz = (currentTile[2]);

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

                    typeoftiles.Add(currentTile, 3);
                    //leftWall.Add(currentTile);
                    buildWallFront();
                }
            }
            if (istypeoft == 0 &&
              istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == -1 && istypeofrb == 1)
            {
                int countofarray = typeoftiles.Count;
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    int xx = (currentTile[0]);
                    int yy = (currentTile[1]);
                    int zz = (currentTile[2]);

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

                    typeoftiles.Add(currentTile, 3);
                    //leftWall.Add(currentTile);
                    buildWallFront();
                }
            }
            //////
            /*if (istypeoft == 1 &&
               istypeofl == 1 && istypeofr == 1 &&
                 istypeoflb == -1 && istypeofb == -1 && istypeofrb == -1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 3);
                    //leftWall.Add(currentTile);
                    buildWallFront();
                }
            }
            if (istypeoft == 1 &&
              istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == -1 && istypeofrb == -1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 3);
                    //leftWall.Add(currentTile);
                    buildWallFront();
                }
            }
            if (istypeoft == 1 &&
              istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 3);
                    //leftWall.Add(currentTile);
                    buildWallFront();
                }
            }
            if (istypeoft == 1 &&
              istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == -1 && istypeofrb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 3);
                    //leftWall.Add(currentTile);
                    buildWallFront();
                }
            }
            */






            /////////BUILD WALL LEFT FRONT INSIDE/////////////////
            if (istypeoft == -1 &&
               istypeofl == -1 && istypeofr == 1 &&
                                  istypeofb == 1)
            {
                int countofarray = typeoftiles.Count;
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    int xx = (currentTile[0]);
                    int yy = (currentTile[1]);
                    int zz = (currentTile[2]);

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

                    typeoftiles.Add(currentTile, 5);
                    //leftWall.Add(currentTile);
                    buildLeftFrontInsideCorner();
                }
            }

            /////////BUILD WALL RIGHT FRONT INSIDE/////////////////
            if (istypeoft == -1 &&
               istypeofl == 1 && istypeofr == -1 &&
                                  istypeofb == 1)
            {
                int countofarray = typeoftiles.Count;
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    int xx = (currentTile[0]);
                    int yy = (currentTile[1]);
                    int zz = (currentTile[2]);

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


                    typeoftiles.Add(currentTile, 6);
                    //leftWall.Add(currentTile);
                    buildRightFrontInsideCorner();
                }
            }
            /////////BUILD WALL LEFT BACK INSIDE/////////////////
            if (istypeoft == 1 &&
               istypeofl == -1 && istypeofr == 1 &&
                                  istypeofb == -1)
            {
                int countofarray = typeoftiles.Count;
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    int xx = (currentTile[0]);
                    int yy = (currentTile[1]);
                    int zz = (currentTile[2]);

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


                    typeoftiles.Add(currentTile, 7);
                    //leftWall.Add(currentTile);
                    buildRightBackInsideCorner();
                }
            }

            /////////BUILD WALL LEFT BACK INSIDE/////////////////
            if (istypeoft == 1 &&
               istypeofl == 1 && istypeofr == -1 &&
                                  istypeofb == -1)
            {
                int countofarray = typeoftiles.Count;
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    int xx = (currentTile[0]);
                    int yy = (currentTile[1]);
                    int zz = (currentTile[2]);

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


                    typeoftiles.Add(currentTile, 8);
                    //leftWall.Add(currentTile);
                    buildLeftBackInsideCorner();
                }
            }





            /////////BUILD WALL LEFT FRONT OUTSIDE/////////////////
            if (istypeoflt == -1 && istypeoft == 1 &&
               istypeofl == 1 && istypeofr == 0 &&
                                  istypeofb == 0)
            {
                int countofarray = typeoftiles.Count;
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    int xx = (currentTile[0]);
                    int yy = (currentTile[1]);
                    int zz = (currentTile[2]);

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


                    typeoftiles.Add(currentTile, 9);
                    //leftWall.Add(currentTile);
                    buildLeftFrontOutsideCorner();
                }
            }

            /////////BUILD WALL RIGHT FRONT OUTSIDE/////////////////
            if (istypeoft == 1 && istypeofrt == -1 &&
               istypeofl == 0 && istypeofr == 1 &&
                                  istypeofb == 0)
            {
                int countofarray = typeoftiles.Count;
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    int xx = (currentTile[0]);
                    int yy = (currentTile[1]);
                    int zz = (currentTile[2]);

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


                    typeoftiles.Add(currentTile, 10);
                    //leftWall.Add(currentTile);
                    buildRightFrontOutsideCorner();
                }
            }
            /////////BUILD WALL LEFT BACK OUTSIDE/////////////////
            if (istypeoft == 0 &&
               istypeofl == 1 && istypeofr == 0 &&
                 istypeoflb == -1 && istypeofb == 1)
            {

                int countofarray = typeoftiles.Count;
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    int xx = (currentTile[0]);
                    int yy = (currentTile[1]);
                    int zz = (currentTile[2]);

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


                    typeoftiles.Add(currentTile, 11);
                    //leftWall.Add(currentTile);
                    buildLeftBackOutsideCorner();
                }
            }
            /////////BUILD WALL RIGHT FRONT OUTSIDE/////////////////
            if (istypeoft == 0 &&
               istypeofl == 0 && istypeofr == 1 &&
                                  istypeofb == 1 && istypeofrb == -1)
            {
                int countofarray = typeoftiles.Count;
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    int xx = (currentTile[0]);
                    int yy = (currentTile[1]);
                    int zz = (currentTile[2]);

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


                    typeoftiles.Add(currentTile, 12);
                    //leftWall.Add(currentTile);
                    buildRightBackOutsideCorner();
                }
            }





            /////////////////////////////////////////////////////
            /////////BUILD WALL LEFT FRONT OUTSIDE/////////////////
            /*if (istypeoflt == -1 && istypeoft == 1 &&
               istypeofl == 1 &&                istypeofr == 0 &&
                                  istypeofb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 9);
                    //leftWall.Add(currentTile);
                    buildLeftFrontInsideCorner();
                }
            }
            if (istypeoflt == -1 && istypeoft == 1 &&
             istypeofl == 1 &&                  istypeofr == 1 &&
                                istypeofb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 9);
                    //leftWall.Add(currentTile);
                    buildLeftFrontInsideCorner();
                }
            }
            if (istypeoflt == -1 && istypeoft == 1 &&
                istypeofl == 1 &&               istypeofr == 0 &&
                                istypeofb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 9);
                    //leftWall.Add(currentTile);
                    buildLeftFrontInsideCorner();
                }
            }
            if (istypeoflt == -1 && istypeoft == 1 &&
                istypeofl == 1 &&               istypeofr == 1 &&
                             istypeofb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 9);
                    //leftWall.Add(currentTile);
                    buildLeftFrontInsideCorner();
                }
            }





            /////////BUILD WALL RIGHT FRONT OUTSIDE/////////////////
            if (istypeoft == 1 && istypeofrt == -1 &&
              istypeofl == 0 && istypeofr == 1 &&
                                 istypeofb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 10);
                    //leftWall.Add(currentTile);
                    buildRightFrontOutsideCorner();
                }
            }
            if (istypeoft == 1 && istypeofrt == -1 &&
             istypeofl == 1 && istypeofr == 1 &&
                                istypeofb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 10);
                    //leftWall.Add(currentTile);
                    buildRightFrontOutsideCorner();
                }
            }
            if (istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 0 && istypeofr == 1 &&
                                istypeofb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 10);
                    //leftWall.Add(currentTile);
                    buildRightFrontOutsideCorner();
                }
            }
            if (istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 1 && istypeofr == 1 &&
                             istypeofb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 10);
                    //leftWall.Add(currentTile);
                    buildRightFrontOutsideCorner();
                }
            }

            /////////BUILD WALL LEFT BACK OUTSIDE/////////////////
            if (                istypeoft == 0 &&
               istypeofl == 1 &&                 istypeofr == 0 &&
               istypeoflb == -1 &&               istypeofb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 11);
                    //leftWall.Add(currentTile);
                    buildLeftFrontInsideCorner();
                }
            }
            if (istypeoft == 0 &&
             istypeofl == 1 && istypeofr == 1 &&
                 istypeoflb == -1 && istypeofb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 11);
                    //leftWall.Add(currentTile);
                    buildLeftFrontInsideCorner();
                }
            }
            if (istypeoft == 1 &&
                istypeofl == 1 && istypeofr == 0 &&
                 istypeoflb == -1 && istypeofb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 11);
                    //leftWall.Add(currentTile);
                    buildLeftFrontInsideCorner();
                }
            }
            if (istypeoft == 1 &&
                istypeofl == 1 && istypeofr == 1 &&
                 istypeoflb == -1 && istypeofb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 11);
                    //leftWall.Add(currentTile);
                    buildLeftFrontInsideCorner();
                }
            }
            /////////BUILD WALL RIGHT BACK OUTSIDE/////////////////
            if (istypeoft == 0 &&
              istypeofl == 0 && istypeofr == 1 &&
                                 istypeofb == 1 && istypeofrb == -1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 12);
                    //leftWall.Add(currentTile);
                    buildRightFrontOutsideCorner();
                }
            }
            if (istypeoft == 0 &&
             istypeofl == 1 && istypeofr == 1 &&
                                istypeofb == 1 && istypeofrb == -1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 12);
                    //leftWall.Add(currentTile);
                    buildRightFrontOutsideCorner();
                }
            }
            if (istypeoft == 1 &&
                istypeofl == 0 && istypeofr == 1 &&
                                istypeofb == 1 && istypeofrb == -1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 12);
                    //leftWall.Add(currentTile);
                    buildRightFrontOutsideCorner();
                }
            }
            if (istypeoft == 1 &&
                istypeofl == 1 && istypeofr == 1 &&
                                 istypeofb == 1 && istypeofrb == -1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 12);
                    //leftWall.Add(currentTile);
                    buildRightFrontOutsideCorner();
                }
            }*/




















            /*
            /////////////////////////////////////////////////////////////
            if (istypeoflt == -1 && istypeoft == 1 && istypeofrt == 0 &&
                istypeofl  == -1          &&           istypeofr == 0 &&
                istypeoflb == -1 && istypeofb == 1 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 1);
                    //leftWall.Add(currentTile);
                    buildWallLeft();
                }
            }*/






            //00w inside corners 
            //www
            //tww

            //0wt inside corners 
            //wwt
            //www

            //0wt inside corners 
            //0ww
            //ww0

            //0ww inside corners 
            //ww0
            //tww







            //frontleftinside
            //1 -1 -1
            //1 1 1
            //1 1 0

            //frontrightinside
            //-1 -1 1
            //1 1 1
            //0 1 1

            //backleftinside
            //1 1 0
            //1 1 1
            //1 -1 -1

            //backrightinside
            //0 1 1
            //1 1 1
            //-1 -1 1


            //frontleftinside
            //-1 1 0
            //1 1 0
            //1 1 1

            //frontrightinside
            //0 1 -1
            //0 1 1
            //1 1 1

            //backleftinside
            //1 1 1
            //1 1 0
            //-1 1 0

            //backrightinside
            //1 1 1
            //0 1 1
            //0 1 -1




            //frontleftoutside
            //-1 1 0
            //1 1 0
            //000

            //frontrightoutside
            //0 1 -1
            //0 1 1
            //0 0 0

            //leftbackoutside
            //0 0 0
            //1 1 0
            //-1 1 0

            //rightbackoutside
            //0 0 0
            //0 1 1
            //0 1 -1



            //backleftoutside
            //-1 1 0
            //-1 1 1
            //1 1 -1

            //frontrightoutside
            //0 1 -1
            //1 1 -1
            //-1 1 1

            //leftbackoutside
            //1 1 -1
            //-1 1 1
            //-1 1 0

            //rightbackoutside
            //-1 1 1
            //1 1 -1
            //0 1 -1



            //frontleftinside
            //111
            //110
            //-110

            //frontrightinside
            //111
            //011
            //01-1

            //leftbackinside
            //-110
            //110
            //111

            //rightbackinside
            //01-1
            //011
            //111

            //frontleftinside
            //111
            //-110
            //-110

            //frontrightinside
            //111
            //01-1
            //01-1

            //leftbackinside
            //-110
            //-110
            //111

            //rightbackinside
            //01-1
            //01-1
            //111





            //frontleftinside
            // 1 -1  1
            // 1  1  0
            //-1  1  0


            //frontrightinside
            // 1 -1  1
            // 0  1  1
            //0  1  -1

            //leftbackinside
            // -1 1 0
            // 1  1  0
            //1 -1 1

            //rightbackinside
            // 0 1  -1
            // 0  1  1
            // 1  -1  1


            //-1 1 0
            //-1 1 1
            //1 1 0

            //-1 1 1
            //1 1 -1
            //0 1 0

            //-1 1 0
            //1 1-1
            //0 1 0



            //-1-11
            //111
            //011






            /////////////////INSIDE CORNERS//////////////////
            ///////////////////////LEFT FRONT INSIDE CORNER///////////////
            /*if (//istypeoflt == -1 && istypeoft == -1 && istypeofrt == -1 &&
                /*istypeofl == -1 &&                     istypeofr == 1 &&
                /*istypeoflb == -1 && istypeofb == 1 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 5);
                    //leftWall.Add(currentTile);
                    buildLeftFrontInsideCorner();
                }
            }*/








        }









        void buildsomewallremains(int[] currentTile)
        {
            istypeofl = -2;
            istypeofr = -2;
            istypeoft = -2;
            istypeofb = -2;

            istypeoflt = -2;
            istypeofrt = -2;
            istypeoflb = -2;
            istypeofrb = -2;

            /*var somevalueindict = typeoftiles.Where(x => x.Key == currentTile).ToArray();

            if (somevalueindict!= null)
            {
                if (somevalueindict[0].Value == 0)
                {
                    istypeof = 0;
                }
                else if (somevalueindict[0].Value == -1)
                {
                    istypeof = -1;
                }
            }*/

            int[] tempvec = currentTile;//.X - 1;
            tempvec[0] -= 1;
            var leftTile = forsortingtiles.Where(x => x.Key == tempvec).ToArray();//  findTiles(currentTile.X - 1, currentTile.Z);
            if (leftTile != null)
            {
                if (leftTile.Length > 0)
                {
                    if (leftTile[0].Value == 0)
                    {
                        istypeofl = 0;
                    }
                    else if (leftTile[0].Value == -1)
                    {
                        istypeofl = 1;
                    }
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


            tempvec = currentTile;//.X - 1;
            tempvec[0] += 1;
            var rightTile = forsortingtiles.Where(x => x.Key == tempvec).ToArray();//  findTiles(currentTile.X - 1, currentTile.Z);
            if (rightTile != null)
            {
                if (rightTile.Length > 0)
                {
                    if (rightTile[0].Value == 0)
                    {
                        istypeofr = 0;
                    }
                    else if (rightTile[0].Value == -1)
                    {
                        istypeofr = 1;
                    }
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

            tempvec = currentTile;//.X - 1;
            tempvec[2] += 1;
            var topTile = forsortingtiles.Where(x => x.Key == tempvec).ToArray();//  findTiles(currentTile.X - 1, currentTile.Z);
            if (topTile != null)
            {
                if (topTile.Length > 0)
                {
                    if (topTile[0].Value == 0)
                    {
                        istypeoft = 0;
                    }
                    else if (topTile[0].Value == -1)
                    {
                        istypeoft = 1;
                    }
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

            tempvec = currentTile;//.X - 1;
            tempvec[2] -= 1;
            var backTile = forsortingtiles.Where(x => x.Key == tempvec).ToArray();//  findTiles(currentTile.X - 1, currentTile.Z);
            if (backTile != null)
            {
                if (backTile.Length > 0)
                {
                    if (backTile[0].Value == 0)
                    {
                        istypeofb = 0;
                    }
                    else if (backTile[0].Value == -1)
                    {
                        istypeofb = 1;
                    }
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









            tempvec = currentTile;//.X - 1;
            tempvec[0] -= 1;
            tempvec[2] += 1;
            var topTilel = forsortingtiles.Where(x => x.Key == tempvec).ToArray();//  findTiles(currentTile.X - 1, currentTile.Z);
            if (topTilel != null)
            {
                if (topTilel.Length > 0)
                {
                    if (topTilel[0].Value == 0)
                    {
                        //Console.WriteLine("found0");
                        istypeoflt = 0;
                    }
                    else if (topTilel[0].Value == -1)
                    {
                        //Console.WriteLine("found1");
                        istypeoflt = 1;
                    }
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

            tempvec = currentTile;//.X - 1;
            tempvec[0] -= 1;
            tempvec[2] -= 1;
            var backTilel = forsortingtiles.Where(x => x.Key == tempvec).ToArray();//  findTiles(currentTile.X - 1, currentTile.Z);
            if (backTilel != null)
            {
                if (backTilel.Length > 0)
                {
                    if (backTilel[0].Value == 0)
                    {
                        istypeoflb = 0;
                    }
                    else if (backTilel[0].Value == -1)
                    {
                        istypeoflb = 1;
                    }

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



            tempvec = currentTile;//.X - 1;
            tempvec[0] += 1;
            tempvec[2] += 1;
            var topTiler = forsortingtiles.Where(x => x.Key == tempvec).ToArray();//  findTiles(currentTile.X - 1, currentTile.Z);
            if (topTiler != null)
            {
                if (topTiler.Length > 0)
                {
                    if (topTiler[0].Value == 0)
                    {
                        istypeofrt = 0;
                    }
                    else if (topTiler[0].Value == -1)
                    {
                        istypeofrt = 1;
                    }
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


            tempvec = currentTile;//.X - 1;
            tempvec[0] += 1;
            tempvec[2] -= 1;
            var backTiler = forsortingtiles.Where(x => x.Key == tempvec).ToArray();//  findTiles(currentTile.X - 1, currentTile.Z);
            if (backTiler != null)
            {
                if (backTiler.Length > 0)
                {
                    if (backTiler[0].Value == 0)
                    {
                        istypeofrb = 0;
                    }
                    else if (backTiler[0].Value == -1)
                    {
                        istypeofrb = 1;
                    }
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

            //walls to the right
            /////////////////////////////////////////////////////////////
            if (istypeoflt == -1 || istypeoft == -1 || istypeofrt == -1 ||
                istypeofl == -1 ||  /*/////////////*/ istypeofr == -1 ||
                istypeoflb == -1 || istypeofb == -1 || istypeofrb == -1)
            {
                //Console.WriteLine("sometest");
                /*if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, -2);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    //forsortingtiles.Remove(currentTile);
                }*/


                if (!listofremainingwalls.Any(key => key.SequenceEqual(currentTile)))
                {
                    listofremainingwalls.Add(currentTile);
                }



                if (forsortingtiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    forsortingtiles.Remove(currentTile);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Add(currentTile, -1);
                }
            }
        }




















        void buildsomefloortiles(int[] currentTile)
        {
            istypeofl = -2;
            istypeofr = -2;
            istypeoft = -2;
            istypeofb = -2;

            istypeoflt = -2;
            istypeofrt = -2;
            istypeoflb = -2;
            istypeofrb = -2;

            /*var somevalueindict = typeoftiles.Where(x => x.Key == currentTile).ToArray();

            if (somevalueindict!= null)
            {
                if (somevalueindict[0].Value == 0)
                {
                    istypeof = 0;
                }
                else if (somevalueindict[0].Value == -1)
                {
                    istypeof = -1;
                }
            }*/

            int[] tempvec = currentTile;//.X - 1;
            tempvec[0] -= 1;
            var leftTile = forsortingtiles.Where(x => x.Key == tempvec).ToArray();//  findTiles(currentTile.X - 1, currentTile.Z);
            if (leftTile != null)
            {
                if (leftTile.Length > 0)
                {
                    if (leftTile[0].Value == 0)
                    {
                        istypeofl = 0;
                    }
                    else if (leftTile[0].Value == -1)
                    {
                        istypeofl = 1;
                    }
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


            tempvec = currentTile;//.X - 1;
            tempvec[0] += 1;
            var rightTile = forsortingtiles.Where(x => x.Key == tempvec).ToArray();//  findTiles(currentTile.X - 1, currentTile.Z);
            if (rightTile != null)
            {
                if (rightTile.Length > 0)
                {
                    if (rightTile[0].Value == 0)
                    {
                        istypeofr = 0;
                    }
                    else if (rightTile[0].Value == -1)
                    {
                        istypeofr = 1;
                    }
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

            tempvec = currentTile;//.X - 1;
            tempvec[2] += 1;
            var topTile = forsortingtiles.Where(x => x.Key == tempvec).ToArray();//  findTiles(currentTile.X - 1, currentTile.Z);
            if (topTile != null)
            {
                if (topTile.Length > 0)
                {
                    if (topTile[0].Value == 0)
                    {
                        istypeoft = 0;
                    }
                    else if (topTile[0].Value == -1)
                    {
                        istypeoft = 1;
                    }
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

            tempvec = currentTile;//.X - 1;
            tempvec[2] -= 1;
            var backTile = forsortingtiles.Where(x => x.Key == tempvec).ToArray();//  findTiles(currentTile.X - 1, currentTile.Z);
            if (backTile != null)
            {
                if (backTile.Length > 0)
                {
                    if (backTile[0].Value == 0)
                    {
                        istypeofb = 0;
                    }
                    else if (backTile[0].Value == -1)
                    {
                        istypeofb = 1;
                    }
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









            tempvec = currentTile;//.X - 1;
            tempvec[0] -= 1;
            tempvec[2] += 1;
            var topTilel = forsortingtiles.Where(x => x.Key == tempvec).ToArray();//  findTiles(currentTile.X - 1, currentTile.Z);
            if (topTilel != null)
            {
                if (topTilel.Length > 0)
                {
                    if (topTilel[0].Value == 0)
                    {
                        //Console.WriteLine("found0");
                        istypeoflt = 0;
                    }
                    else if (topTilel[0].Value == -1)
                    {
                        //Console.WriteLine("found1");
                        istypeoflt = 1;
                    }
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

            tempvec = currentTile;//.X - 1;
            tempvec[0] -= 1;
            tempvec[2] -= 1;
            var backTilel = forsortingtiles.Where(x => x.Key == tempvec).ToArray();//  findTiles(currentTile.X - 1, currentTile.Z);
            if (backTilel != null)
            {
                if (backTilel.Length > 0)
                {
                    if (backTilel[0].Value == 0)
                    {
                        istypeoflb = 0;
                    }
                    else if (backTilel[0].Value == -1)
                    {
                        istypeoflb = 1;
                    }

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



            tempvec = currentTile;//.X - 1;
            tempvec[0] += 1;
            tempvec[2] += 1;
            var topTiler = forsortingtiles.Where(x => x.Key == tempvec).ToArray();//  findTiles(currentTile.X - 1, currentTile.Z);
            if (topTiler != null)
            {
                if (topTiler.Length > 0)
                {
                    if (topTiler[0].Value == 0)
                    {
                        istypeofrt = 0;
                    }
                    else if (topTiler[0].Value == -1)
                    {
                        istypeofrt = 1;
                    }
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


            tempvec = currentTile;//.X - 1;
            tempvec[0] += 1;
            tempvec[2] -= 1;
            var backTiler = forsortingtiles.Where(x => x.Key == tempvec).ToArray();//  findTiles(currentTile.X - 1, currentTile.Z);
            if (backTiler != null)
            {
                if (backTiler.Length > 0)
                {
                    if (backTiler[0].Value == 0)
                    {
                        istypeofrb = 0;
                    }
                    else if (backTiler[0].Value == -1)
                    {
                        istypeofrb = 1;
                    }
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

            //walls to the right
            /////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 0 &&  /*/////////////*/ istypeofr == 0 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0)
            {
                int countofarray = typeoftiles.Count;
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {

                    int xx = (currentTile[0]);
                    int yy = (currentTile[1]);
                    int zz = (currentTile[2]);

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

                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }


            /*
            /////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 1 &&
                istypeofl == 0 &&   istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            /////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 0 &&   istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            /////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 1 &&
                istypeofl == 0 &&   istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            /////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 0 &&   istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            /////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 1 &&
                istypeofl == 0 &&   istypeofr == 0 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            /////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 0 &&   istypeofr == 0 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            /////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 1 &&
                istypeofl == 0 &&   istypeofr == 0 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            ////////////////////////////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 1 && istypeofrt == 0 &&
                istypeofl == 0 &&   istypeofr == 0 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            /////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 0 &&   istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            /////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 1 && istypeofrt == 0 &&
                istypeofl == 0 &&   istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            /////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 0 &&   istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            /////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 1 && istypeofrt == 0 &&
                istypeofl == 0 &&   istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            /////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 0 &&   istypeofr == 0 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            /////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 1 && istypeofrt == 0 &&
                istypeofl == 0 &&   istypeofr == 0 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            /////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 0 &&   istypeofr == 0 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            ////////////////////////////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 0 &&   istypeofr == 0 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            /////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 1 &&
                istypeofl == 0 &&   istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            /////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 0 &&   istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            /////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 1 &&
                istypeofl == 0 &&   istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            /////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 0 &&   istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            /////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 1 &&
                istypeofl == 0 &&   istypeofr == 0 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            /////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 0 &&   istypeofr == 0 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            /////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 1 &&
                istypeofl == 0 &&   istypeofr == 0 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            ////////////////////////////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 1 && istypeofrt == 0 &&
                istypeofl == 0 &&   istypeofr == 0 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            /////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 0 &&   istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            /////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 1 && istypeofrt == 0 &&
                istypeofl == 0 &&   istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            /////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 0 &&   istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            /////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 1 && istypeofrt == 0 &&
                istypeofl == 0 &&   istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            /////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 0 &&   istypeofr == 0 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            /////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 1 && istypeofrt == 0 &&
                istypeofl == 0 &&   istypeofr == 0 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            /////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 0 &&   istypeofr == 0 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }



























            /////////////////////////////////////////////////////////////
            if (istypeoflt == 1 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 1 &&   istypeofr == 0 &&
                istypeoflb == 1 && istypeofb == 0 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
             istypeofl == 1 &&   istypeofr == 0 &&
             istypeoflb == 1 && istypeofb == 0 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            if (istypeoflt == 1 && istypeoft == 0 && istypeofrt == 0 &&
             istypeofl == 1 &&   istypeofr == 0 &&
             istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
             istypeofl == 1 &&   istypeofr == 0 &&
             istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            if (istypeoflt == 1 && istypeoft == 0 && istypeofrt == 0 &&
             istypeofl == 0 &&   istypeofr == 0 &&
             istypeoflb == 1 && istypeofb == 0 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
             istypeofl == 0 &&   istypeofr == 0 &&
             istypeoflb == 1 && istypeofb == 0 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            if (istypeoflt == 1 && istypeoft == 0 && istypeofrt == 0 &&
             istypeofl == 0 &&   istypeofr == 0 &&
             istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            /////////////////////////////////////////////////////////////////////////////////
            if (istypeoflt == 1 && istypeoft == 1 && istypeofrt == 1 &&
             istypeofl == 0 &&   istypeofr == 0 &&
             istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            if (istypeoflt == 1 && istypeoft == 0 && istypeofrt == 1 &&
          istypeofl == 0 &&   istypeofr == 0 &&
          istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            if (istypeoflt == 1 && istypeoft == 1 && istypeofrt == 0 &&
          istypeofl == 0 &&   istypeofr == 0 &&
          istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            if (istypeoflt == 0 && istypeoft == 1 && istypeofrt == 1 &&
          istypeofl == 0 &&   istypeofr == 0 &&
          istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            if (istypeoflt == 1 && istypeoft == 0 && istypeofrt == 0 &&
          istypeofl == 0 &&   istypeofr == 0 &&
          istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            if (istypeoflt == 0 && istypeoft == 1 && istypeofrt == 0 &&
          istypeofl == 0 &&   istypeofr == 0 &&
          istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 1 &&
          istypeofl == 0 &&   istypeofr == 0 &&
          istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            ///////////////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 0 &&   istypeofr == 0 &&
                istypeoflb == 1 && istypeofb == 1 && istypeofrb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
            istypeofl == 0 &&   istypeofr == 0 &&
            istypeoflb == 1 && istypeofb == 1 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
            istypeofl == 0 &&   istypeofr == 0 &&
            istypeoflb == 0 && istypeofb == 1 && istypeofrb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
            istypeofl == 0 &&   istypeofr == 0 &&
            istypeoflb == 1 && istypeofb == 0 && istypeofrb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
            istypeofl == 0 &&   istypeofr == 0 &&
            istypeoflb == 1 && istypeofb == 0 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
            istypeofl == 0 &&   istypeofr == 0 &&
            istypeoflb == 0 && istypeofb == 1 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
            istypeofl == 0 &&   istypeofr == 0 &&
            istypeoflb == 0 && istypeofb == 0 && istypeofrb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.Keys.Any(key => key.SequenceEqual(currentTile)))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }*/
        }



        void buildWallLeft()
        {
            for (int i = 0; i < leftWall.Count; i++)
            {
                if (!builtLeftWall.Any(key => key.SequenceEqual(leftWall[i])))
                {
                    //Instantiate(leftWallz, leftWall[i], Quaternion.identity);
                    builtLeftWall.Add(leftWall[i]);
                }
            }
            // //yield return new WaitForSeconds(BuildingWaitTime);


        }


        void buildWallRight()
        {
            for (int i = 0; i < rightWall.Count; i++)
            {
                if (!builtRightWall.Any(key => key.SequenceEqual(rightWall[i])))
                {
                    //Instantiate(rightWallz, rightWall[i], Quaternion.identity);
                    builtRightWall.Add(rightWall[i]);
                }
            }
            ////yield return new WaitForSeconds(BuildingWaitTime);       
        }



        void buildWallFront()
        {
            for (int i = 0; i < frontWall.Count; i++)
            {
                if (!builtFrontWall.Any(key => key.SequenceEqual(frontWall[i])))
                {
                    //Instantiate(frontWallz, frontWall[i], Quaternion.identity);
                    builtFrontWall.Add(frontWall[i]);
                }
            }
            //yield return new WaitForSeconds(BuildingWaitTime);
        }



        void buildWallBack()
        {
            for (int i = 0; i < backWall.Count; i++)
            {
                if (!builtBackWall.Any(key => key.SequenceEqual(backWall[i])))
                {
                    //Instantiate(backWallz, backWall[i], Quaternion.identity);
                    builtBackWall.Add(backWall[i]);
                }
            }
            //yield return new WaitForSeconds(BuildingWaitTime);
        }


        void buildLeftFrontInsideCorner()
        {
            for (int i = 0; i < leftFrontCornerInside.Count; i++)
            {
                if (!builtLeftFrontInsideCorner.Any(key => key.SequenceEqual(leftFrontCornerInside[i])))
                {
                    //Instantiate(leftFrontInsideCornerWall, leftFrontCornerInside[i], Quaternion.identity);
                    builtLeftFrontInsideCorner.Add(leftFrontCornerInside[i]);
                }
            }
            //yield return new WaitForSeconds(BuildingWaitTime);
        }


        void buildRightFrontInsideCorner()
        {
            for (int i = 0; i < rightFrontCornerInside.Count; i++)
            {
                if (!builtRightFrontInsideCorner.Any(key => key.SequenceEqual(rightFrontCornerInside[i])))
                {
                    //Instantiate(RightFrontInsideCornerWall, rightFrontCornerInside[i], Quaternion.identity);
                    builtRightFrontInsideCorner.Add(rightFrontCornerInside[i]);
                }
            }
            //yield return new WaitForSeconds(BuildingWaitTime);
        }


        void buildLeftBackInsideCorner()
        {
            for (int i = 0; i < leftBackCornerInside.Count; i++)
            {
                if (!builtLeftBackInsideCorner.Any(key => key.SequenceEqual(leftBackCornerInside[i])))
                {
                    //Instantiate(leftBackInsideCornerWall, leftBackCornerInside[i], Quaternion.identity);
                    builtLeftBackInsideCorner.Add(leftBackCornerInside[i]);
                }
            }
            //yield return new WaitForSeconds(BuildingWaitTime);
        }





        void buildRightBackInsideCorner()
        {
            for (int i = 0; i < rightBackCornerInside.Count; i++)
            {
                if (!builtRightBackInsideCorner.Any(key => key.SequenceEqual(rightBackCornerInside[i])))
                {
                    //Instantiate(RightBackInsideCornerWall, rightBackCornerInside[i], Quaternion.identity);
                    builtRightBackInsideCorner.Add(rightBackCornerInside[i]);
                }
            }
            //yield return new WaitForSeconds(BuildingWaitTime);
        }




        void buildLeftFrontOutsideCorner()
        {
            for (int i = 0; i < leftFrontCornerOutside.Count; i++)
            {
                if (!builtLeftFrontOutsideCorner.Any(key => key.SequenceEqual(leftFrontCornerOutside[i])))
                {
                    //Instantiate(leftFrontOutsideCornerWall, leftFrontCornerOutside[i], Quaternion.identity);
                    builtLeftFrontOutsideCorner.Add(leftFrontCornerOutside[i]);
                }
            }
            //yield return new WaitForSeconds(BuildingWaitTime);
        }




        void buildRightFrontOutsideCorner()
        {
            for (int i = 0; i < rightFrontCornerOutside.Count; i++)
            {
                if (!builtRightFrontOutsideCorner.Any(key => key.SequenceEqual(rightFrontCornerOutside[i])))
                {
                    //Instantiate(RightFrontOutsideCornerWall, rightFrontCornerOutside[i], Quaternion.identity);
                    builtRightFrontOutsideCorner.Add(rightFrontCornerOutside[i]);
                }
            }
            //yield return new WaitForSeconds(BuildingWaitTime);
        }




        void buildLeftBackOutsideCorner()
        {
            for (int i = 0; i < leftBackCornerOutside.Count; i++)
            {
                if (!builtLeftBackOutsideCorner.Any(key => key.SequenceEqual(leftBackCornerOutside[i])))
                {
                    //Instantiate(leftBackOutsideCornerWall, leftBackCornerOutside[i], Quaternion.identity);
                    builtLeftBackOutsideCorner.Add(leftBackCornerOutside[i]);
                }
            }
            //yield return new WaitForSeconds(BuildingWaitTime);
        }




        void buildRightBackOutsideCorner()
        {
            for (int i = 0; i < rightBackCornerOutside.Count; i++)
            {
                if (!builtRightBackOutsideCorner.Any(key => key.SequenceEqual(rightBackCornerOutside[i])))
                {
                    //Instantiate(RightBackOutsideCornerWall, rightBackCornerOutside[i], Quaternion.identity);
                    builtRightBackOutsideCorner.Add(rightBackCornerOutside[i]);
                }
            }
            // //yield return new WaitForSeconds(BuildingWaitTime);
        }

    }
}
