using System.Collections;
using System.Collections.Generic;
//using UnityEngine;
using System.Linq;
using System;

using SharpDX;

/*
public class chunk : IEnumerable
{
    private Vector3[] _people;

    Vector3 position;
    Vector3 chunkyPos;


    public chunk(Vector3[] pArray)//Vector3 pos, Vector3 chunkPos)
    {
        //position = pos;
        //chunkyPos = chunkPos;

        _people = new Vector3[pArray.Length];

        for (int i = 0; i < pArray.Length; i++)
        {
            _people[i] = pArray[i];
        }
    }

    // Implementation for the GetEnumerator method.
    IEnumerator IEnumerable.GetEnumerator()
    {
        return (IEnumerator)GetEnumerator();
    }

    public chunkEnum GetEnumerator()
    {
        return new chunkEnum(_people);
    }
}*/

/*
public class chunkEnum : IEnumerator
{
    public Vector3[] _people;
    int position = -1;
    float timer = 10000;
    int counter = 0;
    int counter1 = 0;
    public int one = 50;
    public int two = 50;

    


    public chunkEnum(Vector3[] list)
    {
        _people = list;
    }

    public bool MoveNext()
    {
        TimeSpan timeout;       
        position++;
        return (position < _people.Length);
    }

    public void Reset()
    {
        position = -1;
    }

    object IEnumerator.Current
    {     
        get
        {
            return Current;
        }
    }

    public Vector3 Current
    {
        get
        {
            if (timer > 0 && counter == 0)
            {
                waitsomeTime();
            }
            try
            {
                return _people[position];
            }
            catch (IndexOutOfRangeException)
            {
                throw new InvalidOperationException();
            }
        }

    }

    void waitsomeTime()
    {

        if (two > 0)
        {
            two -= 1;
        }
        if (two == 40)
        {
            Debug.Log("ANUS");
        }
        if (two == 0)
        {
            Debug.Log("Finished");
        }


        while (timer> 0)
        {
            //Debug.Log("yo");
            timer -= Time.deltaTime;
            Debug.Log(timer);
        }

    }

    public void MyDelay (float seconds)
    {
      
        do
        {
            timer -= Time.deltaTime;
        } while (timer > 0);
        if (timer == 0 || timer < 0)
        {
            Debug.Log(timer);
            return;
        }

    }


}*/




/*public class chunk : IEnumerable<newFloorTiles>
{
    private Dictionary<Vector3, Vector3> chunky = new Dictionary<Vector3, Vector3>();

    Vector3 pos;
    newFloorTiles currentChunk;
    Vector3 chunkPos;


    public chunk(Vector3 position, Vector3 currentChunkPos)
    {
        pos = position;
        chunkPos = currentChunkPos;
    }

    public IEnumerator<newFloorTiles> GetEnumerator()
    {
        return chunky.Values.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)chunky.Values).GetEnumerator();
    }

}*/
namespace sccs
{
    public class LevelGenerator4
    {
        //public static List<Vector3> chunks = new List<Vector3>();
        //public static Dictionary<Vector3, Vector3> chunkz = new Dictionary<Vector3, Vector3>();

        public float planesize = 1f;

        public static LevelGenerator4 currentLevelGen;

        public List<Vector3> tiles;
        //public GameObject[] tiles;
        //public GameObject wall;

        public int tileAmount;
        public float chunkwidth;
        public float chunkheight;
        public float chunkdepth;

        //public List<Vector3> createdTiles;

        //public Dictionary<Vector3, int> createdTiles = new Dictionary<Vector3, int>();

        public float chanceUp;
        public float chanceRight;
        public float chanceDown;

        public float SpawnerMoveWaitTime;
        public float BuildingWaitTime;

        float minY = 0;
        float maxY = 0;
        float minX = 0;
        float maxX = 0;

        public float xAmount;

        public float yAmount;

        public List<Vector3> adjacentWall = new List<Vector3>();

        public List<Vector3> createdWall = new List<Vector3>();

        //public Dictionary<Vector3, Vector3> adjacentWall = new Dictionary<Vector3, Vector3>();

        //public Dictionary<Vector3, Vector3> tilesCreated = new Dictionary<Vector3, Vector3>();

        Dictionary<Vector3, Vector3> ExtremityFloor = new Dictionary<Vector3, Vector3>();

        public Dictionary<Vector3, int> typeoftiles = new Dictionary<Vector3, int>();
        public Dictionary<Vector3, int> forsortingtiles = new Dictionary<Vector3, int>();

        public List<Vector3> leftFrontCornerInside = new List<Vector3>();

        public List<Vector3> rightFrontCornerInside = new List<Vector3>();

        public List<Vector3> leftBackCornerInside = new List<Vector3>();

        public List<Vector3> rightBackCornerInside = new List<Vector3>();


        public List<Vector3> leftWall = new List<Vector3>();

        public List<Vector3> rightWall = new List<Vector3>();

        public List<Vector3> frontWall = new List<Vector3>();

        public List<Vector3> backWall = new List<Vector3>();


        public List<Vector3> builtLeftWall = new List<Vector3>();

        public List<Vector3> builtRightWall = new List<Vector3>();

        public List<Vector3> builtFrontWall = new List<Vector3>();

        public List<Vector3> builtBackWall = new List<Vector3>();



        public List<Vector3> builtLeftFrontInsideCorner = new List<Vector3>();

        public List<Vector3> builtRightFrontInsideCorner = new List<Vector3>();

        public List<Vector3> builtLeftBackInsideCorner = new List<Vector3>();

        public List<Vector3> builtRightBackInsideCorner = new List<Vector3>();


        public List<Vector3> builtLeftFrontOutsideCorner = new List<Vector3>();

        public List<Vector3> builtRightFrontOutsideCorner = new List<Vector3>();

        public List<Vector3> builtLeftBackOutsideCorner = new List<Vector3>();

        public List<Vector3> builtRightBackOutsideCorner = new List<Vector3>();


        public List<Vector3> leftFrontCornerOutside = new List<Vector3>();

        public List<Vector3> rightFrontCornerOutside = new List<Vector3>();

        public List<Vector3> leftBackCornerOutside = new List<Vector3>();

        public List<Vector3> rightBackCornerOutside = new List<Vector3>();


        public List<Vector3> threeWayWallLeft = new List<Vector3>();

        public List<Vector3> threeWayWallRight = new List<Vector3>();

        public List<Vector3> threeWayWallFront = new List<Vector3>();

        public List<Vector3> threeWayWallBack = new List<Vector3>();

        //public GameObject sphere;
        //public GameObject sphere1;

        //public float chunkwidth = 10;

        //public GameObject tileSpawner;


        List<Vector3> toRemove = new List<Vector3>();
        //public Dictionary<Vector3, Vector3> toRemove = new Dictionary<Vector3, Vector3>();

        List<Vector3> floorTilesList = new List<Vector3>();

        /*public GameObject leftWallz;
        public GameObject rightWallz;
        public GameObject frontWallz;
        public GameObject backWallz;

        public GameObject leftFrontInsideCornerWall;
        public GameObject RightFrontInsideCornerWall;
        public GameObject leftBackInsideCornerWall;
        public GameObject RightBackInsideCornerWall;

        public GameObject leftFrontOutsideCornerWall;
        public GameObject RightFrontOutsideCornerWall;
        public GameObject leftBackOutsideCornerWall;
        public GameObject RightBackOutsideCornerWall;*/

        /*GameObject leftWallz;
        GameObject rightWallz;
        GameObject frontWallz;
        GameObject backWallz;

        GameObject leftFrontInsideCornerWall;
        GameObject RightFrontInsideCornerWall;
        GameObject leftBackInsideCornerWall;
        GameObject RightBackInsideCornerWall;

        GameObject leftFrontOutsideCornerWall;
        GameObject RightFrontOutsideCornerWall;
        GameObject leftBackOutsideCornerWall;
        GameObject RightBackOutsideCornerWall;

        public GameObject floorTiles;*/

        int counter = 0;
        Vector3 currentTile;

        /*bool isTileLeft;
        bool isTileRight;
        bool isWallFront;
        bool isWallBack;*/

        bool isSurrounded = false;

        int counter999 = 0;
        public float blockSize;


        //chunk chunker;

        List<Vector3> currentChunkPos = new List<Vector3>();


        public List<Vector3> builtFloorTiles = new List<Vector3>();


        public void StartGeneratingVoxelLevel()
        {
            /*leftWallz = floorTiles;
            rightWallz = floorTiles;
            frontWallz = floorTiles;
            backWallz = floorTiles;

            leftFrontInsideCornerWall = floorTiles;
            RightFrontInsideCornerWall = floorTiles;
            leftBackInsideCornerWall = floorTiles;
            RightBackInsideCornerWall = floorTiles;

            leftFrontOutsideCornerWall = floorTiles;
            RightFrontOutsideCornerWall = floorTiles;
            leftBackOutsideCornerWall = floorTiles;
            RightBackOutsideCornerWall = floorTiles;*/


            //chunkwidth = chunkwidth * 0.25f;
            //tileSize = tileSize * 0.25f;
            currentLevelGen = this;
            GenerateLevel();
        }

        void GenerateLevel()
        {

            Vector3 curpos0 = Vector3.Zero;

            List<Vector3> listoftileloc = new List<Vector3>();

            int somepointermax = tileAmount / 10;

            for (int t = 0; t < somepointermax; t++)
            {
                listoftileloc.Add(curpos0);
            }

            int neighbooraddx = 2;
            int neighbooraddz = 2;

            int i = 0;
            for (; i < tileAmount;)
            {
                float dir = (float)sccs.sc_maths.getSomeRandNumThousandDecimal(0, 1, 0.1f, 0, -1);
                //int tile = (int)sccs.sc_maths.getSomeRandNum(0, tiles.Count); //tiles.Length

                //Console.WriteLine(dir);
                //float dir = UnityEngine.Random.Range(0f, 1f);
                //int tile = UnityEngine.Random.Range(0, tiles.Length);

                /*for (int t = 0; t < somepointermax; t++)
                {
                    for (var x = -neighbooraddx / blockSize; x <= neighbooraddx / blockSize; x ++)
                    {
                        for (var z = -neighbooraddz / blockSize; z <= neighbooraddz / blockSize; z ++)
                        {

                            float checkX = (int)Math.Floor(((listoftileloc[t].X + x) / chunkwidth)) * chunkwidth;
                            float checkZ = (int)Math.Floor(((listoftileloc[t].Z + z) / chunkwidth)) * chunkwidth;

                            CreateTile(new Vector3(checkX, 0, checkZ));
                            
                            i++;
                        }
                    }

                    listoftileloc[t] = CallMoveGen(dir, listoftileloc[t]);
                }*/
                /*for (int t = 0; t < somepointermax; t++)
                {
                    CreateTile(new Vector3(listoftileloc[t].X, 0, listoftileloc[t].Z));
                    listoftileloc[t] = CallMoveGen(dir, listoftileloc[t]);
                    i++;
                }*/
                CreateTile(new Vector3(listoftileloc[0].X, 0, listoftileloc[0].Z));
                listoftileloc[0] = CallMoveGen(dir, listoftileloc[0]);
                i++;


                /*dir = (float)sccs.sc_maths.getSomeRandNumThousandDecimal(0, 1, 0.1f, 0, -1);
                //int tile = (int)sccs.sc_maths.getSomeRandNum(0, tiles.Count); //tiles.Length

                //Console.WriteLine(dir);
                //float dir = UnityEngine.Random.Range(0f, 1f);
                //int tile = UnityEngine.Random.Range(0, tiles.Length);
                CreateTile(listoftileloc[1]);
                listoftileloc[1] = CallMoveGen(dir, listoftileloc[1]);
                i++;*/

                /*
                dir = (float)sccs.sc_maths.getSomeRandNumThousandDecimal(0, 1, 0.1f, 0, -1);
                //int tile = (int)sccs.sc_maths.getSomeRandNum(0, tiles.Count); //tiles.Length

                //Console.WriteLine(dir);
                //float dir = UnityEngine.Random.Range(0f, 1f);
                //int tile = UnityEngine.Random.Range(0, tiles.Length);
                CreateTile(curpos1);
                curpos1 = CallMoveGen(dir, curpos1);*/

                /*
                dir = (float)sccs.sc_maths.getSomeRandNumThousandDecimal(0, 1, 0.1f, 0, -1);
                //int tile = (int)sccs.sc_maths.getSomeRandNum(0, tiles.Count); //tiles.Length

                //Console.WriteLine(dir);
                //float dir = UnityEngine.Random.Range(0f, 1f);
                //int tile = UnityEngine.Random.Range(0, tiles.Length);
                CreateTile(curpos2);
                curpos2 = CallMoveGen(dir, curpos2);


                dir = (float)sccs.sc_maths.getSomeRandNumThousandDecimal(0, 1, 0.1f, 0, -1);
                //int tile = (int)sccs.sc_maths.getSomeRandNum(0, tiles.Count); //tiles.Length

                //Console.WriteLine(dir);
                //float dir = UnityEngine.Random.Range(0f, 1f);
                //int tile = UnityEngine.Random.Range(0, tiles.Length);
                CreateTile(curpos3);
                curpos3 = CallMoveGen(dir, curpos3);
                */


                //yield return new WaitForSeconds(SpawnerMoveWaitTime);

                if (i == tileAmount - 1)
                {
                    //Finish();
                }
            }


            Console.WriteLine("finished creating locations of tiles!");
            Finish();
            createfinal();
            //yield return 0;
        }


        Vector3 CallMoveGen(float ranDir, Vector3 curpos)
        {
            if (ranDir < chanceUp && ranDir > 0)
            {
                curpos = MoveGen(0, curpos);
            }
            else if (ranDir < chanceRight && ranDir > chanceUp)
            {
                curpos = MoveGen(1, curpos);
            }
            else if (ranDir < chanceDown && ranDir > chanceRight)
            {
                curpos = MoveGen(2, curpos);
            }
            else
            {
                curpos = MoveGen(3, curpos);
            }

            return curpos;
        }



        void CreateTile(Vector3 curpos) //int tileIndex
        {

            if (!typeoftiles.ContainsKey(curpos))
            {
                //Instantiate(floorTiles, currentposition, Quaternion.identity);

                //floorTiles.tag = "chunks";
                //createdTiles.Add(curpos, -1);
                typeoftiles.Add(curpos, 0);
                forsortingtiles.Add(curpos, 0);
                // tilesCreated.Add(curpos, curpos);
            }
            else
            {
                tileAmount++;
            }




        }


        //Vector3 currentposition;

        Vector3 MoveGen(int dir, Vector3 curpos)
        {
            switch (dir)
            {
                /*case 0:
                    currentposition = new Vector3(currentposition.X, 0, currentposition.Z + (tileSize*blockSize));
                    break;
                case 1:
                    currentposition = new Vector3(currentposition.X + (tileSize ), 0, currentposition.Z);
                    break;
                case 2:
                    currentposition = new Vector3(currentposition.X, 0, currentposition.Z - (tileSize ));
                    break;
                case 3:
                    currentposition = new Vector3(currentposition.X - (tileSize ), 0, currentposition.Z);
                    break;*/


                case 0:
                    curpos = new Vector3(curpos.X, 0, curpos.Z + (chunkdepth * planesize));
                    break;
                case 1:
                    curpos = new Vector3(curpos.X + (chunkwidth * planesize), 0, curpos.Z);
                    break;
                case 2:
                    curpos = new Vector3(curpos.X, 0, curpos.Z - (chunkdepth * planesize));
                    break;
                case 3:
                    curpos = new Vector3(curpos.X - (chunkwidth * planesize), 0, curpos.Z);
                    break;

            }
            return curpos;
        }

        void Finish()
        {
            /*var enumerator1 = createdTiles.GetEnumerator();

            while (enumerator1.MoveNext())
            {
                var currentTuile = enumerator1.Current;
                currentTile = currentTuile.Key;
                Instantiate(floorTiles, currentTile, Quaternion.identity);
            }*/

            var enumerator0 = typeoftiles.GetEnumerator();

            while (enumerator0.MoveNext())
            {
                var currentTuile = enumerator0.Current;
                currentTile = currentTuile.Key;
                checkAllSides(currentTile);
            }
        }


        void checkAllSides(Vector3 currentTilePos)
        {


            /*for (float x = (currentPos.x / planeSize) - viewSize0; x <= (currentPos.x / planeSize) + viewSize0; x += chunkSizeLOWDETAIL)
            {
                for (float y = (currentPos.y / planeSize) - viewSize1; y <= (currentPos.y / planeSize) + viewSize1; y += chunkSizeLOWDETAIL)
                {
                    for (float z = (currentPos.z / planeSize) - viewSize0; z <= (currentPos.z / planeSize) + viewSize0; z += chunkSizeLOWDETAIL)
                    {
                        float chunkX0 = Math.FloorToInt(x / chunkSizeLOWDETAIL) * chunkSizeLOWDETAIL * planeSize;*/


            for (var x = -chunkwidth / blockSize; x <= chunkwidth / blockSize; x++)
            {
                for (var z = -chunkdepth / blockSize; z <= chunkdepth / blockSize; z++)
                {

                    float checkX = (int)Math.Floor(((currentTilePos.X + x) / chunkwidth)) * chunkwidth;
                    float checkY = (int)Math.Floor(((currentTilePos.Z + z) / chunkwidth)) * chunkwidth;


                    //float checkX = ((currentTilePos.x + x));
                    //float checkY = ((currentTilePos.z + z));


                    if (checkX == currentTilePos.X && checkY == currentTilePos.Z + (chunkdepth * 1) ||
                        checkX == currentTilePos.X && checkY == currentTilePos.Z - (chunkdepth * 1) ||
                        checkX == currentTilePos.X + (chunkwidth * 1) && checkY == currentTilePos.Z ||
                        checkX == currentTilePos.X - (chunkwidth * 1) && checkY == currentTilePos.Z ||

                        checkX == currentTilePos.X + (chunkwidth * 1) && checkY == currentTilePos.Z + (chunkdepth * 1) ||
                        checkX == currentTilePos.X - (chunkwidth * 1) && checkY == currentTilePos.Z + (chunkdepth * 1) ||
                        checkX == currentTilePos.X + (chunkwidth * 1) && checkY == currentTilePos.Z - (chunkdepth * 1) ||
                        checkX == currentTilePos.X - (chunkwidth * 1) && checkY == currentTilePos.Z - (chunkdepth * 1))
                    {
                        //Instantiate(sphere, new Vector3(checkX, 0, checkY), Quaternion.identity);
                        if (!adjacentWall.Contains(new Vector3(checkX, 0, checkY)) && !typeoftiles.ContainsKey(new Vector3(checkX, 0, checkY)))
                        {
                            //MainWindow.MessageBox((IntPtr)0, "test", "scmsg", 0);
                            adjacentWall.Add(new Vector3(checkX, 0, checkY));
                            
                            
                            forsortingtiles.Add(new Vector3(checkX, 0, checkY), -1);



                            /*istypeofl = -2;
                            istypeofr = -2;
                            istypeoft = -2;
                            istypeofb = -2;

                            istypeoflt = -2;
                            istypeofrt = -2;
                            istypeoflb = -2;
                            istypeofrb = -2;

                            var somevalueindict = typeoftiles.Where(x => x.Key == currentTile).ToArray();

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
                            /*Vector3 currentTile = new Vector3(checkX, 0, checkY);

                            Vector3 tempvec = currentTile;//.X - chunkwidth;
                            tempvec.X -= chunkwidth;
                            var leftTile = forsortingtiles.Where(x => x.Key == tempvec).ToArray();//  findTiles(currentTile.X - chunkwidth, currentTile.Z);
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


                            tempvec = currentTile;//.X - chunkwidth;
                            tempvec.X += chunkwidth;
                            var rightTile = forsortingtiles.Where(x => x.Key == tempvec).ToArray();//  findTiles(currentTile.X - chunkwidth, currentTile.Z);
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

                            tempvec = currentTile;//.X - chunkwidth;
                            tempvec.Z += chunkwidth;
                            var topTile = forsortingtiles.Where(x => x.Key == tempvec).ToArray();//  findTiles(currentTile.X - chunkwidth, currentTile.Z);
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

                            tempvec = currentTile;//.X - chunkwidth;
                            tempvec.Z -= chunkwidth;
                            var backTile = forsortingtiles.Where(x => x.Key == tempvec).ToArray();//  findTiles(currentTile.X - chunkwidth, currentTile.Z);
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









                            tempvec = currentTile;//.X - chunkwidth;
                            tempvec.X -= chunkwidth;
                            tempvec.Z += chunkwidth;
                            var topTilel = forsortingtiles.Where(x => x.Key == tempvec).ToArray();//  findTiles(currentTile.X - chunkwidth, currentTile.Z);
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

                            tempvec = currentTile;//.X - chunkwidth;
                            tempvec.X -= chunkwidth;
                            tempvec.Z -= chunkwidth;
                            var backTilel = forsortingtiles.Where(x => x.Key == tempvec).ToArray();//  findTiles(currentTile.X - chunkwidth, currentTile.Z);
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



                            tempvec = currentTile;//.X - chunkwidth;
                            tempvec.X += chunkwidth;
                            tempvec.Z += chunkwidth;
                            var topTiler = forsortingtiles.Where(x => x.Key == tempvec).ToArray();//  findTiles(currentTile.X - chunkwidth, currentTile.Z);
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


                            tempvec = currentTile;//.X - chunkwidth;
                            tempvec.X += chunkwidth;
                            tempvec.Z -= chunkwidth;
                            var backTiler = forsortingtiles.Where(x => x.Key == tempvec).ToArray();//  findTiles(currentTile.X - chunkwidth, currentTile.Z);
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


                            if (istypeoflt == -1 && istypeoft == 1 &&
                                 istypeofl == -1 &&                 istypeofr == 0 &&
                                istypeoflb == -1 && istypeofb == 1)
                            {
                                
                            }*/








                        }
                    }
                }
            }


            //planesize


            /*for (int x = 0; x < chunkwidth; x++)
            {
                for (int y = 0; y < chunkheight; y++)
                {
                    for (int z = 0; z < chunkdepth; z++)
                    {
                        var xx = x;
                        var yy = y;// (mapHeight - 1) - y;
                        var zz = z;

                        var position = new Vector3(x, y, z);
                        //newChunker = new chunk();

                        //position.X = position.X + (_chunkPos.X ); //*1.05f
                        //position.Y = position.Y + (_chunkPos.Y );
                        //position.Z = position.Z + (_chunkPos.Z );

                        position.X *= ((chunkwidth * planesize));
                        position.Y *= ((chunkheight * planesize));
                        position.Z *= ((chunkdepth * planesize));

                        //Console.WriteLine(_chunkPos.X);

                        position.X = position.X + (currentTilePos.X); //*1.05f
                        position.Y = position.Y + (currentTilePos.Y);
                        position.Z = position.Z + (currentTilePos.Z);
                        
                        
                        float checkX = (int)Math.Floor(((currentTilePos.X + x) / chunkwidth)) * chunkwidth;
                        float checkY = (int)Math.Floor(((currentTilePos.Z + z) / chunkwidth)) * chunkwidth;


                        if (checkX == currentTilePos.X && checkY == currentTilePos.Z + (chunkdepth * 1) ||
                        checkX == currentTilePos.X && checkY == currentTilePos.Z - (chunkdepth * 1) ||
                        checkX == currentTilePos.X + (chunkwidth * 1) && checkY == currentTilePos.Z ||
                        checkX == currentTilePos.X - (chunkwidth * 1) && checkY == currentTilePos.Z ||

                        checkX == currentTilePos.X + (chunkwidth * 1) && checkY == currentTilePos.Z + (chunkdepth * 1) ||
                        checkX == currentTilePos.X - (chunkwidth * 1) && checkY == currentTilePos.Z + (chunkdepth * 1) ||
                        checkX == currentTilePos.X + (chunkwidth * 1) && checkY == currentTilePos.Z - (chunkdepth * 1) ||
                        checkX == currentTilePos.X - (chunkwidth * 1) && checkY == currentTilePos.Z - (chunkdepth * 1))
                        {
                            //Instantiate(sphere, new Vector3(checkX, 0, checkY), Quaternion.identity);
                            if (!adjacentWall.Contains(new Vector3(checkX, 0, checkY)) && !createdTiles.ContainsKey(new Vector3(checkX, 0, checkY)))
                            {
                                adjacentWall.Add(new Vector3(checkX, 0, checkY));
                            }
                        }
                    }
                }
            }*/



            /*var enumerator2 = createdTiles.GetEnumerator();

            while (enumerator2.MoveNext())
            {
                var tls0 = enumerator2.Current;
                Instantiate(sphere, new Vector3(tls0.Key.x, tls0.Key.y, tls0.Key.z), Quaternion.identity);
            }

          var enumerator1 = adjacentWall.GetEnumerator();

           while (enumerator1.MoveNext())
           {
               var tls0 = enumerator1.Current;
               Instantiate(sphere1, new Vector3(tls0.Key.x, tls0.Key.y, tls0.Key.z), Quaternion.identity);
           }*/

            if (counter == 0)
            {
                counter = 1;
            }
        }



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




                for (int i = 0; i < adjacentWall.Count; i++)
                {
                    var currentTuile = adjacentWall[i];
                    buildsomefloortiles(currentTuile);
                    //adjacentWall.Remove(adjacentWall[i]);
                }


                for (int i = 0; i < adjacentWall.Count; i++)
                {
                    var currentTuile = adjacentWall[i];
                    buildWallsRerolled(currentTuile);
                    //adjacentWall.Remove(adjacentWall[i]);
                }







                toRemove = adjacentWall;

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

                /*
                for (int i = 0; i < toRemove.Count; i++)
                {
                    var currentTuile = toRemove[i];
                    buildWallsRerolled(currentTuile);
                }*/



















                if (counter999 == 0)
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
                }


                //singleChunk.GetComponent<newFloorTiles>().Regenerate();

                //GetComponent<startGeneratingFaces>().BuildFaces();


                counter = 2;
            }




            if (counter == 2)
            {
                //Debug.Log("total: " + totalTiles + " corout: " + countingCoroutines);

                if (countingCoroutinesStart == countingCoroutinesEnd)
                {
                    //BuildFaces();

                    counter = 3;
                }
            }

        }

        int totalTiles = 0;
        int countingCoroutinesStart = 0;
        int countingCoroutinesEnd = 0;

        //List<GameObject> chunks;
        //GameObject[] chunkz;

        //List<Vector3> createdTiles = new List<Vector3>();
        //public Dictionary<Vector3, Vector3> createdTiles = new Dictionary<Vector3, Vector3>();
        List<Vector3> leftWalls = new List<Vector3>();
        //public GameObject chunk;


        public void BuildFaces()
        {
            //chunks = new List<GameObject>();
            //chunkz = GameObject.FindGameObjectsWithTag("chunks");
            buildFaces();
        }

        //WaitForSeconds waiting = new WaitForSeconds(0.5f);
        void buildFaces()
        {
            /*for (int i = 0; i < chunkz.Length; i++)
            {
                GameObject singleChunk = chunkz[i];
                singleChunk.GetComponent<newFloorTiles>().Regenerate();
                //yield return new WaitForSeconds(0f);
            }*/
            //yield return new WaitForSeconds(0f);
        }


        //public Dictionary<Vector3, int> typeoftiles = new Dictionary<Vector3, int>();

        public List<Vector3> someWall = new List<Vector3>();

        void buildWallsNotSet(Vector3 currentTile)
        {
            if (!someWall.Contains(currentTile))
            {
                someWall.Add(currentTile);
                //leftWall.Add(currentTile);
                //buildWallLeft();
            }
        }

        /*void buildWallsSet(Vector3 currentTile)
        {
            if (!someWall.Contains(currentTile))
            {
                someWall.Add(currentTile);
                //leftWall.Add(currentTile);
                //buildWallLeft();
            }
        }*/




        int istypeofl = -2;
        int istypeofr = -2;
        int istypeoft = -2;
        int istypeofb = -2;
        //int istile = -1;

        int istypeoflt = -2;
        int istypeofrt = -2;
        int istypeoflb = -2;
        int istypeofrb = -2;



        void buildsomefloortiles(Vector3 currentTile)
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

            Vector3 tempvec = currentTile;//.X - chunkwidth;
            tempvec.X -= chunkwidth;
            var leftTile = forsortingtiles.Where(x => x.Key == tempvec).ToArray();//  findTiles(currentTile.X - chunkwidth, currentTile.Z);
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


            tempvec = currentTile;//.X - chunkwidth;
            tempvec.X += chunkwidth;
            var rightTile = forsortingtiles.Where(x => x.Key == tempvec).ToArray();//  findTiles(currentTile.X - chunkwidth, currentTile.Z);
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

            tempvec = currentTile;//.X - chunkwidth;
            tempvec.Z += chunkwidth;
            var topTile = forsortingtiles.Where(x => x.Key == tempvec).ToArray();//  findTiles(currentTile.X - chunkwidth, currentTile.Z);
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

            tempvec = currentTile;//.X - chunkwidth;
            tempvec.Z -= chunkwidth;
            var backTile = forsortingtiles.Where(x => x.Key == tempvec).ToArray();//  findTiles(currentTile.X - chunkwidth, currentTile.Z);
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









            tempvec = currentTile;//.X - chunkwidth;
            tempvec.X -= chunkwidth;
            tempvec.Z += chunkwidth;
            var topTilel = forsortingtiles.Where(x => x.Key == tempvec).ToArray();//  findTiles(currentTile.X - chunkwidth, currentTile.Z);
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

            tempvec = currentTile;//.X - chunkwidth;
            tempvec.X -= chunkwidth;
            tempvec.Z -= chunkwidth;
            var backTilel = forsortingtiles.Where(x => x.Key == tempvec).ToArray();//  findTiles(currentTile.X - chunkwidth, currentTile.Z);
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



            tempvec = currentTile;//.X - chunkwidth;
            tempvec.X += chunkwidth;
            tempvec.Z += chunkwidth;
            var topTiler = forsortingtiles.Where(x => x.Key == tempvec).ToArray();//  findTiles(currentTile.X - chunkwidth, currentTile.Z);
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


            tempvec = currentTile;//.X - chunkwidth;
            tempvec.X += chunkwidth;
            tempvec.Z -= chunkwidth;
            var backTiler = forsortingtiles.Where(x => x.Key == tempvec).ToArray();//  findTiles(currentTile.X - chunkwidth, currentTile.Z);
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
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            /////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 1 &&
                istypeofl == 0 &&  /*/////////////*/ istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            /////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 0 &&  /*/////////////*/ istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            /////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 1 &&
                istypeofl == 0 &&  /*/////////////*/ istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            /////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 0 &&  /*/////////////*/ istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            /////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 1 &&
                istypeofl == 0 &&  /*/////////////*/ istypeofr == 0 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            /////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 0 &&  /*/////////////*/ istypeofr == 0 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            /////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 1 &&
                istypeofl == 0 &&  /*/////////////*/ istypeofr == 0 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            ////////////////////////////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 1 && istypeofrt == 0 &&
                istypeofl == 0 &&  /*/////////////*/ istypeofr == 0 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            /////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 0 &&  /*/////////////*/ istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            /////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 1 && istypeofrt == 0 &&
                istypeofl == 0 &&  /*/////////////*/ istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            /////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 0 &&  /*/////////////*/ istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            /////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 1 && istypeofrt == 0 &&
                istypeofl == 0 &&  /*/////////////*/ istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            /////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 0 &&  /*/////////////*/ istypeofr == 0 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            /////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 1 && istypeofrt == 0 &&
                istypeofl == 0 &&  /*/////////////*/ istypeofr == 0 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            /////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 0 &&  /*/////////////*/ istypeofr == 0 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            ////////////////////////////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 0 &&  /*/////////////*/ istypeofr == 0 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            /////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 1 &&
                istypeofl == 0 &&  /*/////////////*/ istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            /////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 0 &&  /*/////////////*/ istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            /////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 1 &&
                istypeofl == 0 &&  /*/////////////*/ istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            /////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 0 &&  /*/////////////*/ istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            /////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 1 &&
                istypeofl == 0 &&  /*/////////////*/ istypeofr == 0 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            /////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 0 &&  /*/////////////*/ istypeofr == 0 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            /////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 1 &&
                istypeofl == 0 &&  /*/////////////*/ istypeofr == 0 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            ////////////////////////////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 1 && istypeofrt == 0 &&
                istypeofl == 0 &&  /*/////////////*/ istypeofr == 0 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            /////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 0 &&  /*/////////////*/ istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            /////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 1 && istypeofrt == 0 &&
                istypeofl == 0 &&  /*/////////////*/ istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            /////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 0 &&  /*/////////////*/ istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            /////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 1 && istypeofrt == 0 &&
                istypeofl == 0 &&  /*/////////////*/ istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            /////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 0 &&  /*/////////////*/ istypeofr == 0 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            /////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 1 && istypeofrt == 0 &&
                istypeofl == 0 &&  /*/////////////*/ istypeofr == 0 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            /////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 0 &&  /*/////////////*/ istypeofr == 0 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }



























            /////////////////////////////////////////////////////////////
            if (istypeoflt == 1 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 1 &&  /*/////////////*/ istypeofr == 0 &&
                istypeoflb == 1 && istypeofb == 0 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
             istypeofl == 1 &&  /*/////////////*/ istypeofr == 0 &&
             istypeoflb == 1 && istypeofb == 0 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            if (istypeoflt == 1 && istypeoft == 0 && istypeofrt == 0 &&
             istypeofl == 1 &&  /*/////////////*/ istypeofr == 0 &&
             istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
             istypeofl == 1 &&  /*/////////////*/ istypeofr == 0 &&
             istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            if (istypeoflt == 1 && istypeoft == 0 && istypeofrt == 0 &&
             istypeofl == 0 &&  /*/////////////*/ istypeofr == 0 &&
             istypeoflb == 1 && istypeofb == 0 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
             istypeofl == 0 &&  /*/////////////*/ istypeofr == 0 &&
             istypeoflb == 1 && istypeofb == 0 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            if (istypeoflt == 1 && istypeoft == 0 && istypeofrt == 0 &&
             istypeofl == 0 &&  /*/////////////*/ istypeofr == 0 &&
             istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            /////////////////////////////////////////////////////////////////////////////////
            if (istypeoflt == 1 && istypeoft == 1 && istypeofrt == 1 &&
             istypeofl == 0 &&  /*/////////////*/ istypeofr == 0 &&
             istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            if (istypeoflt == 1 && istypeoft == 0 && istypeofrt == 1 &&
          istypeofl == 0 &&  /*/////////////*/ istypeofr == 0 &&
          istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            if (istypeoflt == 1 && istypeoft == 1 && istypeofrt == 0 &&
          istypeofl == 0 &&  /*/////////////*/ istypeofr == 0 &&
          istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            if (istypeoflt == 0 && istypeoft == 1 && istypeofrt == 1 &&
          istypeofl == 0 &&  /*/////////////*/ istypeofr == 0 &&
          istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            if (istypeoflt == 1 && istypeoft == 0 && istypeofrt == 0 &&
          istypeofl == 0 &&  /*/////////////*/ istypeofr == 0 &&
          istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            if (istypeoflt == 0 && istypeoft == 1 && istypeofrt == 0 &&
          istypeofl == 0 &&  /*/////////////*/ istypeofr == 0 &&
          istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 1 &&
          istypeofl == 0 &&  /*/////////////*/ istypeofr == 0 &&
          istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            ///////////////////////////////////////////////////////////////////////
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 0 &&  /*/////////////*/ istypeofr == 0 &&
                istypeoflb == 1 && istypeofb == 1 && istypeofrb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
            istypeofl == 0 &&  /*/////////////*/ istypeofr == 0 &&
            istypeoflb == 1 && istypeofb == 1 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
            istypeofl == 0 &&  /*/////////////*/ istypeofr == 0 &&
            istypeoflb == 0 && istypeofb == 1 && istypeofrb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
            istypeofl == 0 &&  /*/////////////*/ istypeofr == 0 &&
            istypeoflb == 1 && istypeofb == 0 && istypeofrb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
            istypeofl == 0 &&  /*/////////////*/ istypeofr == 0 &&
            istypeoflb == 1 && istypeofb == 0 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
            istypeofl == 0 &&  /*/////////////*/ istypeofr == 0 &&
            istypeoflb == 0 && istypeofb == 1 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
            istypeofl == 0 &&  /*/////////////*/ istypeofr == 0 &&
            istypeoflb == 0 && istypeofb == 0 && istypeofrb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 0);
                    //leftWall.Add(currentTile);
                    //buildWallLeft();
                    forsortingtiles.Remove(currentTile);
                }
            }







        }






        void buildWallsRerolled(Vector3 currentTile)
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

            Vector3 tempvec = currentTile;//.X - chunkwidth;
            tempvec.X -= chunkwidth;
            var leftTile = forsortingtiles.Where(x => x.Key == tempvec).ToArray();//  findTiles(currentTile.X - chunkwidth, currentTile.Z);
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


            tempvec = currentTile;//.X - chunkwidth;
            tempvec.X += chunkwidth;
            var rightTile = forsortingtiles.Where(x => x.Key == tempvec).ToArray();//  findTiles(currentTile.X - chunkwidth, currentTile.Z);
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

            tempvec = currentTile;//.X - chunkwidth;
            tempvec.Z += chunkwidth;
            var topTile = forsortingtiles.Where(x => x.Key == tempvec).ToArray();//  findTiles(currentTile.X - chunkwidth, currentTile.Z);
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

            tempvec = currentTile;//.X - chunkwidth;
            tempvec.Z -= chunkwidth;
            var backTile = forsortingtiles.Where(x => x.Key == tempvec).ToArray();//  findTiles(currentTile.X - chunkwidth, currentTile.Z);
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









            tempvec = currentTile;//.X - chunkwidth;
            tempvec.X -= chunkwidth;
            tempvec.Z += chunkwidth;
            var topTilel = forsortingtiles.Where(x => x.Key == tempvec).ToArray();//  findTiles(currentTile.X - chunkwidth, currentTile.Z);
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

            tempvec = currentTile;//.X - chunkwidth;
            tempvec.X -= chunkwidth;
            tempvec.Z -= chunkwidth;
            var backTilel = forsortingtiles.Where(x => x.Key == tempvec).ToArray();//  findTiles(currentTile.X - chunkwidth, currentTile.Z);
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



            tempvec = currentTile;//.X - chunkwidth;
            tempvec.X += chunkwidth;
            tempvec.Z += chunkwidth;
            var topTiler = forsortingtiles.Where(x => x.Key == tempvec).ToArray();//  findTiles(currentTile.X - chunkwidth, currentTile.Z);
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


            tempvec = currentTile;//.X - chunkwidth;
            tempvec.X += chunkwidth;
            tempvec.Z -= chunkwidth;
            var backTiler = forsortingtiles.Where(x => x.Key == tempvec).ToArray();//  findTiles(currentTile.X - chunkwidth, currentTile.Z);
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
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 1);
                    //leftWall.Add(currentTile);
                    buildWallLeft();
                }
            }

            /////////BUILD WALL RIGHT/////////////////


            if (istypeoft == 1 && istypeofrt == -1 &&
                 istypeofl == 0 && istypeofr == -1 &&
                                 istypeofb == 1 && istypeofrb == -1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 2);
                    //leftWall.Add(currentTile);
                    buildWallRight();
                }
            }
            /////////BUILD WALL BACK/////////////////

            if (istypeoflt == -1 && istypeoft == -1 && istypeofrt == -1 &&
               istypeofl == 1 && istypeofr == 1 &&
                                 istypeofb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 4);
                    //leftWall.Add(currentTile);
                    buildWallBack();
                }
            }

            /////////BUILD WALL FRONT/////////////////

            if (istypeoft == 0 &&
               istypeofl == 1 && istypeofr == 1 &&
                 istypeoflb == -1 && istypeofb == -1 && istypeofrb == -1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 3);
                    //leftWall.Add(currentTile);
                    buildWallFront();
                }
            }




            /////////BUILD WALL LEFT FRONT INSIDE/////////////////
            if (istypeoft == -1 &&
               istypeofl == -1 && istypeofr == 1 &&
                                  istypeofb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
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
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
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
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
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
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 8);
                    //leftWall.Add(currentTile);
                    buildLeftBackInsideCorner();
                }
            }



            /////////////////////////////////////////////////////
            /////////BUILD WALL LEFT FRONT OUTSIDE/////////////////
            if (istypeoflt == -1 && istypeoft == 1 &&
               istypeofl == 1 && istypeofr == 0 &&
                                  istypeofb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 9);
                    //leftWall.Add(currentTile);
                    buildLeftFrontInsideCorner();
                }
            }
            if (istypeoflt == -1 && istypeoft == 1 &&
             istypeofl == 1 && istypeofr == 1 &&
                                istypeofb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 9);
                    //leftWall.Add(currentTile);
                    buildLeftFrontInsideCorner();
                }
            }
            if (istypeoflt == -1 && istypeoft == 1 &&
                istypeofl == 1 && istypeofr == 0 &&
                                istypeofb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 9);
                    //leftWall.Add(currentTile);
                    buildLeftFrontInsideCorner();
                }
            }
            if (istypeoflt == -1 && istypeoft == 1 &&
                istypeofl == 1 && istypeofr == 1 &&
                             istypeofb == 1)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
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
                if (!typeoftiles.ContainsKey(currentTile))
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
                if (!typeoftiles.ContainsKey(currentTile))
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
                if (!typeoftiles.ContainsKey(currentTile))
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
                if (!typeoftiles.ContainsKey(currentTile))
                {
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
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
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
                if (!typeoftiles.ContainsKey(currentTile))
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
                if (!typeoftiles.ContainsKey(currentTile))
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
                if (!typeoftiles.ContainsKey(currentTile))
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
                if (!typeoftiles.ContainsKey(currentTile))
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
                if (!typeoftiles.ContainsKey(currentTile))
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
                if (!typeoftiles.ContainsKey(currentTile))
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
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 12);
                    //leftWall.Add(currentTile);
                    buildRightFrontOutsideCorner();
                }
            }




















            /*
            /////////////////////////////////////////////////////////////
            if (istypeoflt == -1 && istypeoft == 1 && istypeofrt == 0 &&
                istypeofl  == -1          &&           istypeofr == 0 &&
                istypeoflb == -1 && istypeofb == 1 && istypeofrb == 0)
            {
                //Console.WriteLine("sometest");
                if (!typeoftiles.ContainsKey(currentTile))
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
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 5);
                    //leftWall.Add(currentTile);
                    buildLeftFrontInsideCorner();
                }
            }*/








        }







































        void buildWallsNEW(Vector3 currentTile)
        {
            bool leftTile = findToSortTiles(currentTile.X - chunkwidth, currentTile.Z);
            bool rightTile = findToSortTiles(currentTile.X + chunkwidth, currentTile.Z);
            bool topTile = findToSortTiles(currentTile.X, currentTile.Z + chunkwidth);
            bool backTile = findToSortTiles(currentTile.X, currentTile.Z - chunkwidth);

            bool lefttopdiagTile = findToSortTiles(currentTile.X - chunkwidth, currentTile.Z + chunkwidth);
            bool righttopdiagTile = findToSortTiles(currentTile.X + chunkwidth, currentTile.Z + chunkwidth);
            bool backbottomleftdiagTile = findToSortTiles(currentTile.X - chunkwidth, currentTile.Z - chunkwidth);
            bool backbottomrightdiagTile = findToSortTiles(currentTile.X + chunkwidth, currentTile.Z - chunkwidth);

            if (leftTile == false &&
                rightTile == true &&
                topTile == true &&
                backTile == true &&
                lefttopdiagTile == false &&
                righttopdiagTile == true &&
                backbottomleftdiagTile == false &&
                backbottomrightdiagTile == true

                ||

                leftTile == false &&
                rightTile == true &&
                topTile == true &&
                backTile == true &&
                lefttopdiagTile == false &&
                righttopdiagTile == true &&
                backbottomleftdiagTile == true &&
                backbottomrightdiagTile == true ||

                leftTile == false &&
                rightTile == true &&
                topTile == true &&
                backTile == true &&
                lefttopdiagTile == true &&
                righttopdiagTile == true &&
                backbottomleftdiagTile == false &&
                backbottomrightdiagTile == true
                 ||

                leftTile == false &&
                rightTile == true &&
                topTile == true &&
                backTile == true &&
                lefttopdiagTile == true &&
                righttopdiagTile == true &&
                backbottomleftdiagTile == true &&
                backbottomrightdiagTile == true)
            {
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 1);
                    //leftWall.Add(currentTile);
                    buildWallLeft();
                }
            }

            if (leftTile == true &&
                rightTile == false &&
                topTile == true &&
                backTile == true &&
                lefttopdiagTile == true &&
                righttopdiagTile == false &&
                backbottomleftdiagTile == true &&
                backbottomrightdiagTile == false ||

                leftTile == true &&
                rightTile == false &&
                topTile == true &&
                backTile == true &&
                lefttopdiagTile == true &&
                righttopdiagTile == false &&
                backbottomleftdiagTile == true &&
                backbottomrightdiagTile == true ||

                leftTile == true &&
                rightTile == false &&
                topTile == true &&
                backTile == true &&
                lefttopdiagTile == true &&
                righttopdiagTile == true &&
                backbottomleftdiagTile == true &&
                backbottomrightdiagTile == false ||

                leftTile == true &&
                rightTile == false &&
                topTile == true &&
                backTile == true &&
                lefttopdiagTile == true &&
                righttopdiagTile == true &&
                backbottomleftdiagTile == true &&
                backbottomrightdiagTile == true)
            {
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 2);
                    //leftWall.Add(currentTile);
                    buildWallRight();
                }
            }

            if (leftTile == true &&
                 rightTile == true &&
                 topTile == true &&
                 backTile == false &&
                lefttopdiagTile == true &&
                righttopdiagTile == true &&
                backbottomleftdiagTile == false &&
                backbottomrightdiagTile == false ||

                leftTile == true &&
                 rightTile == true &&
                 topTile == true &&
                 backTile == false &&
                lefttopdiagTile == true &&
                righttopdiagTile == true &&
                backbottomleftdiagTile == false &&
                backbottomrightdiagTile == true ||

                leftTile == true &&
                 rightTile == true &&
                 topTile == true &&
                 backTile == false &&
                lefttopdiagTile == true &&
                righttopdiagTile == true &&
                backbottomleftdiagTile == true &&
                backbottomrightdiagTile == false ||

                leftTile == true &&
                 rightTile == true &&
                 topTile == true &&
                 backTile == false &&
                lefttopdiagTile == true &&
                righttopdiagTile == true &&
                backbottomleftdiagTile == true &&
                backbottomrightdiagTile == true)
            {
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 3);
                    //leftWall.Add(currentTile);
                    buildWallBack();
                }
            }

            if (leftTile == true &&
                rightTile == true &&
                topTile == false &&
                backTile == true &&
                lefttopdiagTile == false &&
                righttopdiagTile == false &&
                backbottomleftdiagTile == true &&
                backbottomrightdiagTile == true ||

                leftTile == true &&
                rightTile == true &&
                topTile == false &&
                backTile == true &&
                lefttopdiagTile == false &&
                righttopdiagTile == true &&
                backbottomleftdiagTile == true &&
                backbottomrightdiagTile == true ||

                leftTile == true &&
                rightTile == true &&
                topTile == false &&
                backTile == true &&
                lefttopdiagTile == true &&
                righttopdiagTile == false &&
                backbottomleftdiagTile == true &&
                backbottomrightdiagTile == true ||

                leftTile == true &&
                rightTile == true &&
                topTile == false &&
                backTile == true &&
                lefttopdiagTile == true &&
                righttopdiagTile == true &&
                backbottomleftdiagTile == true &&
                backbottomrightdiagTile == true)
            {
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 4);
                    //leftWall.Add(currentTile);
                    buildWallFront();
                }
            }














            if (leftTile == false &&
                rightTile == true &&
                topTile == false &&
                backTile == true

                ||

                leftTile == true &&
                rightTile == true &&
                topTile == true &&
                backTile == false &&
                lefttopdiagTile == false &&
                righttopdiagTile == true &&
                backbottomleftdiagTile == true &&
                backbottomrightdiagTile == false ||

                leftTile == true &&
                rightTile == true &&
                topTile == false &&
                backTile == true &&
                lefttopdiagTile == true &&
                righttopdiagTile == false &&
                backbottomleftdiagTile == false &&
                backbottomrightdiagTile == true
                )
            {
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 5);
                    //leftWall.Add(currentTile);
                    buildLeftBackInsideCorner();
                }
            }

            if (leftTile == true &&
                rightTile == false &&
                topTile == false &&
                backTile == true
              ||

                leftTile == true &&
                rightTile == true &&
                topTile == true &&
                backTile == false &&
                lefttopdiagTile == true &&
                righttopdiagTile == false &&
                backbottomleftdiagTile == false &&
                backbottomrightdiagTile == true ||

                leftTile == true &&
                rightTile == true &&
                topTile == false &&
                backTile == true &&
                lefttopdiagTile == false &&
                righttopdiagTile == true &&
                backbottomleftdiagTile == true &&
                backbottomrightdiagTile == false)
            {
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 6);
                    //leftWall.Add(currentTile);
                    buildRightBackInsideCorner();
                }
            }

            if (leftTile == false &&
                rightTile == true &&
                topTile == true &&
                backTile == false

                ||

                leftTile == true &&
                rightTile == true &&
                topTile == false &&
                backTile == true &&
                lefttopdiagTile == true &&
                righttopdiagTile == false &&
                backbottomleftdiagTile == false &&
                backbottomrightdiagTile == true ||

                leftTile == true &&
                rightTile == true &&
                topTile == true &&
                backTile == false &&
                lefttopdiagTile == false &&
                righttopdiagTile == true &&
                backbottomleftdiagTile == true &&
                backbottomrightdiagTile == false)
            {
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 7);
                    //leftWall.Add(currentTile);
                    buildLeftFrontInsideCorner();
                }
            }

            if (leftTile == true &&
                rightTile == false &&
                topTile == true &&
                backTile == false

                ||

                leftTile == true &&
                rightTile == true &&
                topTile == false &&
                backTile == true &&
                lefttopdiagTile == false &&
                righttopdiagTile == true &&
                backbottomleftdiagTile == true &&
                backbottomrightdiagTile == false ||

                leftTile == true &&
                rightTile == true &&
                topTile == true &&
                backTile == false &&
                lefttopdiagTile == true &&
                righttopdiagTile == false &&
                backbottomleftdiagTile == false &&
                backbottomrightdiagTile == true)
            {
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 8);
                    //leftWall.Add(currentTile);
                    buildRightFrontInsideCorner();
                }
            }












            if (leftTile == true &&
                rightTile == true &&
                topTile == true &&
                backTile == true &&
                lefttopdiagTile == false &&
                righttopdiagTile == true &&
                backbottomleftdiagTile == true &&
                backbottomrightdiagTile == true)
            {
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 9);
                    //leftWall.Add(currentTile);
                    buildRightFrontOutsideCorner();
                }
            }



            if (leftTile == true &&
                rightTile == true &&
                topTile == true &&
                backTile == true &&
                lefttopdiagTile == true &&
                righttopdiagTile == false &&
                backbottomleftdiagTile == true &&
                backbottomrightdiagTile == true)
            {
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 10);
                    //leftWall.Add(currentTile);
                    buildLeftFrontOutsideCorner();
                }
            }

            if (leftTile == true &&
                rightTile == true &&
                topTile == true &&
                backTile == true &&
                lefttopdiagTile == true &&
                righttopdiagTile == true &&
                backbottomleftdiagTile == false &&
                backbottomrightdiagTile == true)
            {
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 11);
                    //leftWall.Add(currentTile);
                    buildLeftBackOutsideCorner();
                }
            }


            if (leftTile == true &&
                rightTile == true &&
                topTile == true &&
                backTile == true &&
                lefttopdiagTile == true &&
                righttopdiagTile == true &&
                backbottomleftdiagTile == true &&
                backbottomrightdiagTile == false)
            {
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 12);
                    //leftWall.Add(currentTile);
                    buildRightBackOutsideCorner();
                }
            }


























            /*
            if (lefttopdiagTile == false &&
                righttopdiagTile == true &&
                backbottomleftdiagTile == true &&
                backbottomrightdiagTile == false)
            {
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 13);
                    //leftWall.Add(currentTile);
                    buildRightBackOutsideCorner();
                }
            }



            if (lefttopdiagTile == true &&
                righttopdiagTile == false &&
                backbottomleftdiagTile == false &&
                backbottomrightdiagTile == true)
            {
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 14);
                    //leftWall.Add(currentTile);
                    buildRightBackOutsideCorner();
                }
            }*/


            /*if (lefttopdiagTile == true &&
                righttopdiagTile == false &&
                backbottomleftdiagTile == false &&
                backbottomrightdiagTile == true)
            {
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 15);
                    //leftWall.Add(currentTile);
                    buildRightBackOutsideCorner();
                }
            }



            if (lefttopdiagTile == false &&
                righttopdiagTile == true &&
                backbottomleftdiagTile == true &&
                backbottomrightdiagTile == false)
            {
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 16);
                    //leftWall.Add(currentTile);
                    buildRightBackOutsideCorner();
                }
            }*/






















            /*



            if (lefttopdiagTile == false &&
                righttopdiagTile == true &&
                backbottomleftdiagTile == true &&
                backbottomrightdiagTile == true)
            {
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 9);
                    //leftWall.Add(currentTile);
                    buildRightFrontOutsideCorner();
                }
            }



            if (lefttopdiagTile == true &&
                righttopdiagTile == false &&
                backbottomleftdiagTile == true &&
                backbottomrightdiagTile == true)
            {
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 10);
                    //leftWall.Add(currentTile);
                    buildLeftFrontOutsideCorner();
                }
            }

            if (lefttopdiagTile == true &&
                righttopdiagTile == true &&
                backbottomleftdiagTile == false &&
                backbottomrightdiagTile == true)
            {
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 11);
                    //leftWall.Add(currentTile);
                    buildLeftBackOutsideCorner();
                }
            }


            if (lefttopdiagTile == true &&
                righttopdiagTile == true &&
                backbottomleftdiagTile == true &&
                backbottomrightdiagTile == false)
            {
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 12);
                    //leftWall.Add(currentTile);
                    buildRightBackOutsideCorner();
                }
            }

            */





















            /////////////////////////////////////LEFTWALL/////////////////////////////////////////
            /*bool leftTile = findTiles(currentTile.X - chunkwidth, currentTile.Z);
            bool rightTile = findTiles(currentTile.X + chunkwidth, currentTile.Z);
            bool topTile = findTiles(currentTile.X, currentTile.Z + chunkwidth);
            bool backTile = findTiles(currentTile.X, currentTile.Z - chunkwidth);

            bool lefttopdiagTile = findTiles(currentTile.X - chunkwidth, currentTile.Z + chunkwidth);
            bool righttopdiagTile = findTiles(currentTile.X + chunkwidth, currentTile.Z + chunkwidth);
            bool backbottomleftdiagTile = findTiles(currentTile.X - chunkwidth, currentTile.Z - chunkwidth);
            bool backbottomrightdiagTile = findTiles(currentTile.X + chunkwidth, currentTile.Z - chunkwidth);

            bool leftWall = findWalls(currentTile.X - chunkwidth, currentTile.Z);
            bool rightWall = findWalls(currentTile.X + chunkwidth, currentTile.Z);
            bool topWall = findWalls(currentTile.X, currentTile.Z + chunkwidth);
            bool backWall = findWalls(currentTile.X, currentTile.Z - chunkwidth);

            bool lefttopdiagWall = findWalls(currentTile.X - chunkwidth, currentTile.Z + chunkwidth);
            bool righttopdiagWall = findWalls(currentTile.X + chunkwidth, currentTile.Z + chunkwidth);
            bool backbottomleftdiagWall = findWalls(currentTile.X - chunkwidth, currentTile.Z - chunkwidth);
            bool backbottomrightdiagWall = findWalls(currentTile.X + chunkwidth, currentTile.Z - chunkwidth);

            if (leftWall == false &&
                rightWall == false &&
                topWall == true &&
                backWall == true &&
                rightTile == true &&
                leftTile == false ||

                leftWall == false &&
                rightWall == true &&
                topWall == true &&
                backWall == true &&
                rightTile == false &&
                leftTile == false)
            {
                if (!forsortingtiles.ContainsKey(currentTile))
                {
                    forsortingtiles.Add(currentTile, 1);
                    //leftWall.Add(currentTile);
                    buildWallLeft();
                }
            }


            if (leftWall == false &&
                rightWall == false &&
                topWall == true &&
                backWall == true &&
                rightTile == false &&
                leftTile == true ||

                leftWall == true &&
                rightWall == false &&
                topWall == true &&
                backWall == true &&
                rightTile == false &&
                leftTile == false)
            {
                if (!forsortingtiles.ContainsKey(currentTile))
                {
                    forsortingtiles.Add(currentTile, 2);
                    //leftWall.Add(currentTile);
                    buildWallRight();
                }
            }

            if (leftWall == true &&
              rightWall == true &&
              topWall == false &&
              backWall == true &&
              topTile == false &&
              backTile == false ||

              leftWall == true &&
              rightWall == true &&
              topWall == false &&
              backWall == false &&
              topTile == false &&
              backTile == true)
            {
                if (!forsortingtiles.ContainsKey(currentTile))
                {
                    forsortingtiles.Add(currentTile, 4);
                    //leftWall.Add(currentTile);
                    buildWallFront();
                }
            }

            if (leftWall == true &&
            rightWall == true &&
            topWall == true &&
            backWall == false &&
            topTile == false &&
            backTile == false ||

            leftWall == true &&
            rightWall == true &&
            topWall == false &&
            backWall == false &&
            topTile == true &&
            backTile == false)
            {
                if (!forsortingtiles.ContainsKey(currentTile))
                {
                    forsortingtiles.Add(currentTile, 3);
                    //leftWall.Add(currentTile);
                    buildWallBack();
                }
            }




            if (topTile == false &&
                topWall == false &&
                leftTile == false&&
                leftWall == false&& 
                rightWall == true &&
                backWall == true &&
                backbottomrightdiagTile == true)
            {
                if (!forsortingtiles.ContainsKey(currentTile))
                {
                    forsortingtiles.Add(currentTile, 5);
                    //leftWall.Add(currentTile);
                    buildLeftFrontInsideCorner();
                }
            }




            
            if (topTile == false &&
                topWall == false &&
                rightTile == false &&
                rightWall == false && 
                leftWall == true &&
                backWall == true &&
                backbottomleftdiagTile == true)
            {
                if (!forsortingtiles.ContainsKey(currentTile))
                {
                    forsortingtiles.Add(currentTile, 6);
                    //leftWall.Add(currentTile);
                    buildRightFrontInsideCorner();
                }
            }


            if (leftTile == false &&
                leftWall == false &&
                backTile == false&& 
                backWall == false &&
                topWall == true &&
                rightWall == true &&
                righttopdiagTile == true)
            {
                if (!forsortingtiles.ContainsKey(currentTile))
                {
                    forsortingtiles.Add(currentTile, 7);
                    //leftWall.Add(currentTile);
                    buildLeftBackInsideCorner();
                }
            }


            if (rightTile == false &&
                rightWall == false &&
                backTile == false &&
                backWall == false && 
                topWall == true &&
                leftWall == true &&
                lefttopdiagTile == true)
            {
                if (!forsortingtiles.ContainsKey(currentTile))
                {
                    forsortingtiles.Add(currentTile, 8);
                    //leftWall.Add(currentTile);
                    buildRightBackInsideCorner();
                }
            }
            


            //0wt
            //wwt
            //ttt

     
            if (topTile == true &&
                topWall == false &&
                leftTile == true &&
                leftWall == false &&
                rightTile == false &&
                rightWall == true &&
                backTile == false &&
                backWall == true &&
                backbottomrightdiagTile == false &&
                backbottomrightdiagWall == false &&
                backbottomleftdiagTile == true &&
                backbottomleftdiagWall == false &&
                lefttopdiagTile == true &&
                lefttopdiagWall == false &&
                righttopdiagTile == true &&
                righttopdiagWall == false

                ||

                topTile == false &&
                topWall == true &&
                leftTile == true &&
                leftWall == false &&
                rightTile == false &&
                rightWall == true &&
                backTile == false &&
                backWall == true &&
                backbottomrightdiagTile == false &&
                backbottomrightdiagWall == false &&
                backbottomleftdiagTile == true &&
                backbottomleftdiagWall == false &&
                lefttopdiagTile == true &&
                lefttopdiagWall == false &&
                righttopdiagTile == true &&
                righttopdiagWall == false
               )
            {
                if (!forsortingtiles.ContainsKey(currentTile))
                {
                    forsortingtiles.Add(currentTile, 12);
                    //leftWall.Add(currentTile);
                    buildLeftFrontOutsideCorner();
                }
            }*/




            /*
            topTile == true &&
                topWall == false &&
                leftTile == true &&
                leftWall == false &&
                rightTile == false &&
                rightWall == true &&
                backTile == false &&
                backWall == true &&
                lefttopdiagTile == true &&
                lefttopdiagWall == false &&
                backbottomrightdiagTile == false &&
                backbottomrightdiagWall == false*/






            /*if (topTile == true &&
               topWall == false &&
               leftTile == true &&
               leftWall == false &&
               rightTile == false &&
               rightWall == true &&
               backTile == false &&
               backWall == true &&
               lefttopdiagTile == true &&
               lefttopdiagWall == false &&
               backbottomrightdiagTile == false &&
               backbottomrightdiagWall == false



               )
            {
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 12);
                    //leftWall.Add(currentTile);
                    buildLeftFrontOutsideCorner();
                }
            }*/


















            /*
            if (topTile == true &&
                topWall == false &&
                leftTile == true &&
                leftWall == false &&
                rightTile == false &&
                rightWall == true &&
                backWall == true &&
                lefttopdiagTile == true &&
                lefttopdiagWall == false &&
                backbottomrightdiagTile == false &&
                backbottomrightdiagWall == false ||

                topTile == true &&
                topWall == false &&
                leftTile == true &&
                leftWall == false &&
                rightTile == false &&
                rightWall == true &&
                backWall == true &&
                lefttopdiagTile == false &&
                lefttopdiagWall == true &&
                backbottomrightdiagTile == false &&
                backbottomrightdiagWall == false ||

                topTile == true &&
                topWall == false &&
                leftTile == true &&
                leftWall == false &&
                rightTile == true &&
                rightWall == false &&
                backWall == true &&
                lefttopdiagTile == true &&
                lefttopdiagWall == false &&
                backbottomrightdiagTile == false &&
                backbottomrightdiagWall == false ||

                topTile == true &&
                topWall == false &&
                leftTile == true &&
                leftWall == false &&
                rightTile == true &&
                rightWall == false &&
                backWall == true &&
                lefttopdiagTile == false &&
                lefttopdiagWall == true &&
                backbottomrightdiagTile == false &&
                backbottomrightdiagWall == false ||

                topTile == true &&
                topWall == false &&
                leftTile == true &&
                leftWall == false &&
                rightTile == true &&
                rightWall == false &&
                backTile == true &&
                backWall == false &&
                lefttopdiagTile == true &&
                lefttopdiagWall == false &&
                backbottomrightdiagTile == false &&
                backbottomrightdiagWall == false ||

                topTile == true &&
                topWall == false &&
                leftTile == true &&
                leftWall == false &&
                rightTile == true &&
                rightWall == false &&
                backTile == true &&
                backWall == false &&
                lefttopdiagTile == false &&
                lefttopdiagWall == true &&
                backbottomrightdiagTile == false &&
                backbottomrightdiagWall == false





                
                )
            {
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 12);
                    //leftWall.Add(currentTile);
                    buildLeftFrontOutsideCorner();
                }
            }*/








            /*
             * 
             * topTile == false &&
                topWall == true &&
                leftTile == true &&
                leftWall == false &&
                rightTile == false &&
                rightWall == true &&
                backWall == true &&
                lefttopdiagTile == true &&
                lefttopdiagWall == false &&
                backbottomrightdiagTile == false &&
                backbottomrightdiagWall == false ||

                topTile == false &&
                topWall == true &&
                leftTile == true &&
                leftWall == false &&
                rightTile == false &&
                rightWall == true &&
                backWall == true &&
                lefttopdiagTile == false &&
                lefttopdiagWall == true &&
                backbottomrightdiagTile == false &&
                backbottomrightdiagWall == false ||

                topTile == false &&
                topWall == true &&
                leftTile == true &&
                leftWall == false &&
                rightTile == true &&
                rightWall == false &&
                backWall == true &&
                lefttopdiagTile == true &&
                lefttopdiagWall == false &&
                backbottomrightdiagTile == false &&
                backbottomrightdiagWall == false ||

                topTile == false &&
                topWall == true &&
                leftTile == true &&
                leftWall == false &&
                rightTile == true &&
                rightWall == false &&
                backWall == true &&
                lefttopdiagTile == false &&
                lefttopdiagWall == true &&
                backbottomrightdiagTile == false &&
                backbottomrightdiagWall == false ||

                topTile == false &&
                topWall == true &&
                leftTile == true &&
                leftWall == false &&
                rightTile == true &&
                rightWall == false &&
                backTile == true &&
                backWall == false &&
                lefttopdiagTile == true &&
                lefttopdiagWall == false &&
                backbottomrightdiagTile == false &&
                backbottomrightdiagWall == false ||

                topTile == false &&
                topWall == true &&
                leftTile == true &&
                leftWall == false &&
                rightTile == true &&
                rightWall == false &&
                backTile == true &&
                backWall == false &&
                lefttopdiagTile == false &&
                lefttopdiagWall == true &&
                backbottomrightdiagTile == false &&
                backbottomrightdiagWall == false
            */







            /*
                   if (topTile == true &&
                      topWall == false &&
                      leftTile == true &&
                      leftWall == false &&
                      rightTile == false &&
                      rightWall == true &&
                      backWall == true &&
                      lefttopdiagTile == true &&
                      backbottomrightdiagTile == false &&
                      backbottomrightdiagWall == false ||

                      topTile == false &&
                      topWall == true &&
                      leftTile == true &&
                      leftWall == false &&
                      rightTile == false &&
                      rightWall == true &&
                      backWall == true &&
                      lefttopdiagTile == true &&
                      backbottomrightdiagTile == false &&
                      backbottomrightdiagWall == false ||

                      topTile == true &&
                      topWall == false &&
                      leftTile == false &&
                      leftWall == true &&
                      rightTile == false &&
                      rightWall == true &&
                      backWall == true &&
                      lefttopdiagTile == true &&
                      backbottomrightdiagTile == false &&
                      backbottomrightdiagWall == false ||

                      topTile == true &&
                      topWall == false &&
                      leftTile == true &&
                      leftWall == false &&
                      rightTile == false &&
                      rightWall == true &&
                      backWall == true &&
                      lefttopdiagTile == false &&
                      lefttopdiagWall == true &&
                      backbottomrightdiagTile == false &&
                      backbottomrightdiagWall == false ||

                      topTile == false &&
                      topWall == true &&
                      leftTile == true &&
                      leftWall == false &&
                      rightTile == false &&
                      rightWall == true &&
                      backWall == true &&
                      lefttopdiagTile == false &&
                      lefttopdiagWall == true &&
                      backbottomrightdiagTile == false &&
                      backbottomrightdiagWall == false ||

                      topTile == true &&
                      topWall == false &&
                      leftTile == false &&
                      leftWall == true &&
                      rightTile == false &&
                      rightWall == true &&
                      backWall == true &&
                      lefttopdiagTile == false &&
                      lefttopdiagWall == true &&
                      backbottomrightdiagTile == false &&
                      backbottomrightdiagWall == false
                      )
                   {
                       if (!typeoftiles.ContainsKey(currentTile))
                       {
                           typeoftiles.Add(currentTile, 12);
                           //leftWall.Add(currentTile);
                           buildLeftFrontOutsideCorner();
                       }
                   }
            */






















            /*
            if (rightTile == false &&
                rightWall == true &&
                backTile == false &&
                backWall == true &&
                topWall == false &&
                topTile == true &&
                leftTile == true &&
                leftWall == false &&
                lefttopdiagTile == true &&
                lefttopdiagWall == true &&
                backbottomrightdiagTile == false &&
                backbottomrightdiagWall == false)
            {
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 12);
                    //leftWall.Add(currentTile);
                    buildRightBackOutsideCorner();
                }
            }*/


            /*
            if (topTile == false &&
                 topWall == true &&
                 leftTile == true &&
                 leftWall == false &&
                 rightWall == true &&
                 rightTile == false &&
                 backWall == false &&
                 backTile == true &&
                 backbottomleftdiagTile == true &&
                 righttopdiagTile == false &&
                 righttopdiagWall == false)
            {
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 10);
                    //leftWall.Add(currentTile);
                    buildRightFrontOutsideCorner();
                }
            }




            if (topTile == true &&
                 topWall == false &&
                 leftTile == false &&
                 leftWall == true &&
                 rightWall == false &&
                 rightTile == true &&
                 backWall == true &&
                 backTile == false &&
                 righttopdiagTile == true &&
                 backbottomleftdiagTile == false &&
                 backbottomleftdiagWall == false)
            {
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 11);
                    //leftWall.Add(currentTile);
                    buildLeftBackOutsideCorner();
                }
            }



            if (topTile == true &&
                 topWall == false &&
                 leftTile == true &&
                 leftWall == false &&
                 rightWall == true &&
                 rightTile == false &&
                 backWall == true &&
                 backTile == false &&
                 lefttopdiagTile == true &&
                 backbottomrightdiagTile == false &&
                 backbottomrightdiagWall == false)
            {
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 12);
                    //leftWall.Add(currentTile);
                    buildRightBackOutsideCorner();
                }
            }*/





            /*
            if (topTile == false &&
                topWall == true &&
                leftTile == false &&
                leftWall == true &&
                rightWall == false &&
                rightTile == true &&
                backWall == false &&
                backTile == true &&
                backbottomrightdiagTile == true)
            {
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 9);
                    //leftWall.Add(currentTile);
                    buildLeftFrontOutsideCorner();
                }
            }



            if (topTile == false &&
                topWall == true &&
                leftTile == false &&
                leftWall == false &&
                rightWall == true &&
                rightTile == false &&
                backWall == false &&
                backTile == true &&
                backbottomleftdiagTile == true)
            {
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 10);
                    //leftWall.Add(currentTile);
                    buildRightFrontOutsideCorner();
                }
            }




            if (topTile == true &&
                topWall == false &&
                leftTile == false &&
                leftWall == true &&
                rightWall == false &&
                rightTile == true &&
                backWall == true &&
                backTile == false &&
                righttopdiagTile == true)
            {
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 11);
                    //leftWall.Add(currentTile);
                    buildLeftBackOutsideCorner();
                }
            }



            if (topTile == true &&
                topWall == false &&
                leftTile == true &&
                leftWall == false &&
                rightWall == true &&
                rightTile == false &&
                backWall == true &&
                backTile == false &&
                lefttopdiagTile == true)
            {
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 12);
                    //leftWall.Add(currentTile);
                    buildRightBackOutsideCorner();
                }
            }*/




        }








        void buildWallsOLD(Vector3 currentTile)
        {

            /////////////////////////////////////LEFTWALL/////////////////////////////////////////
            bool leftTilly0 = findTiles(currentTile.X - chunkwidth * blockSize, currentTile.Z);
            bool rightTilly0 = findTiles(currentTile.X + chunkwidth, currentTile.Z);

            bool frontWally0 = findWalls(currentTile.X, currentTile.Z + chunkwidth);
            bool backWally0 = findWalls(currentTile.X, currentTile.Z - chunkwidth);

            bool leftWally0 = findWalls(currentTile.X - chunkwidth, currentTile.Z);
            bool rightWally0 = findWalls(currentTile.X + chunkwidth, currentTile.Z);

            if (leftTilly0 == false &&
               rightTilly0 == true &&
               frontWally0 == true &&
               backWally0 == true &&
               leftWally0 == false &&
               rightWally0 == false ||

               leftTilly0 == false &&
               rightTilly0 == false &&
               frontWally0 == true &&
               backWally0 == true &&
               leftWally0 == false &&
               rightWally0 == true ||

               leftTilly0 == false &&
               rightTilly0 == false &&
               frontWally0 == true &&
               backWally0 == true &&
               leftWally0 == false &&
               rightWally0 == true)
            {
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 1);
                    //leftWall.Add(currentTile);
                    buildWallLeft();
                }
            }
            /////////////////////////////////////RIGHTWALL/////////////////////////////////////////

            bool leftTilly1 = findTiles(currentTile.X - chunkwidth, currentTile.Z);
            bool rightTilly1 = findTiles(currentTile.X + chunkwidth, currentTile.Z);
            bool frontWally1 = findWalls(currentTile.X, currentTile.Z + chunkwidth);
            bool backWally1 = findWalls(currentTile.X, currentTile.Z - chunkwidth);

            bool leftWally1 = findWalls(currentTile.X - chunkwidth, currentTile.Z);
            bool rightWally1 = findWalls(currentTile.X + chunkwidth, currentTile.Z);

            if (leftTilly1 == true &&
               rightTilly1 == false &&
               frontWally1 == true &&
               backWally1 == true &&
               leftWally1 == false &&
               rightWally1 == false ||

               leftTilly1 == false &&
               rightTilly1 == false &&
               frontWally1 == true &&
               backWally1 == true &&
               leftWally1 == true &&
               rightWally1 == false ||

               leftTilly1 == false &&
               rightTilly1 == false &&
               frontWally1 == true &&
               backWally1 == true &&
               leftWally1 == true &&
               rightWally1 == false)
            {
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 2);
                    //rightWall.Add(currentTile);
                    buildWallRight();
                }
            }

            /////////////////////////////////////FRONTWALL/////////////////////////////////////////

            bool leftTilly2 = findWalls(currentTile.X - chunkwidth, currentTile.Z);
            bool rightTilly2 = findWalls(currentTile.X + chunkwidth, currentTile.Z);
            bool frontTilly2 = findTiles(currentTile.X, currentTile.Z + chunkwidth);
            bool backTilly2 = findTiles(currentTile.X, currentTile.Z - chunkwidth);

            bool frontWally2 = findWalls(currentTile.X, currentTile.Z + chunkwidth);
            bool backWally2 = findWalls(currentTile.X, currentTile.Z - chunkwidth);

            if (leftTilly2 == true &&
               rightTilly2 == true &&
               frontTilly2 == true &&
               backTilly2 == false &&
               frontWally2 == false &&
               backWally2 == false ||

               leftTilly2 == true &&
               rightTilly2 == true &&
               frontTilly2 == false &&
               backTilly2 == false &&
               frontWally2 == true &&
               backWally2 == false ||

               leftTilly2 == true &&
               rightTilly2 == true &&
               frontTilly2 == false &&
               backTilly2 == false &&
               frontWally2 == true &&
               backWally2 == false)

            {
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 3);
                    //frontWall.Add(currentTile);
                    buildWallFront();
                }
            }

            /////////////////////////////////////BACKWALL/////////////////////////////////////////

            bool leftTilly3 = findWalls(currentTile.X - chunkwidth, currentTile.Z);
            bool rightTilly3 = findWalls(currentTile.X + chunkwidth, currentTile.Z);
            bool frontTilly3 = findTiles(currentTile.X, currentTile.Z + chunkwidth);
            bool backTilly3 = findTiles(currentTile.X, currentTile.Z - chunkwidth);

            bool frontWally3 = findWalls(currentTile.X, currentTile.Z + chunkwidth);
            bool backWally3 = findWalls(currentTile.X, currentTile.Z - chunkwidth);

            if (leftTilly3 == true &&
               rightTilly3 == true &&
               frontTilly3 == false &&
               backTilly3 == true &&
               frontWally3 == false &&
               backWally3 == false ||

               leftTilly3 == true &&
               rightTilly3 == true &&
               frontTilly3 == false &&
               backTilly3 == false &&
               frontWally3 == false &&
               backWally3 == true ||

               leftTilly2 == true &&
               rightTilly2 == true &&
               frontTilly2 == false &&
               backTilly2 == false &&
               frontWally2 == false &&
               backWally2 == true)

            {
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 4);
                    //backWall.Add(currentTile);
                    buildWallBack();
                }
            }

            /////////////////////////////////////LEFTFRONTINSIDECORNER/////////////////////////////////////////

            bool leftTilly4 = findTiles(currentTile.X - chunkwidth, currentTile.Z);
            //bool rightTilly4 = findTiles(currentTile.X + chunkwidth, currentTile.Z);

            bool frontTilly4 = findTiles(currentTile.X, currentTile.Z + chunkwidth);
            bool frontWally4 = findWalls(currentTile.X, currentTile.Z + chunkwidth);

            bool backWally4 = findWalls(currentTile.X, currentTile.Z - chunkwidth);

            bool leftWally4 = findWalls(currentTile.X - chunkwidth, currentTile.Z);
            bool rightWally4 = findWalls(currentTile.X + chunkwidth, currentTile.Z);

            if (leftTilly4 == false &&
               frontTilly4 == false &&
               frontWally4 == false &&
               backWally4 == true &&
               leftWally4 == false &&
               rightWally4 == true)
            {
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 5);
                    //leftFrontCornerInside.Add(currentTile);
                    buildLeftFrontInsideCorner();
                }
            }


            /////////////////////////////////////RightFRONTINSIDECORNER/////////////////////////////////////////

            //bool leftTilly5 = findTiles(currentTile.X - chunkwidth, currentTile.Z);
            bool rightTilly5 = findTiles(currentTile.X + chunkwidth, currentTile.Z);

            bool frontTilly5 = findTiles(currentTile.X, currentTile.Z + chunkwidth);
            bool frontWally5 = findWalls(currentTile.X, currentTile.Z + chunkwidth);

            bool backWally5 = findWalls(currentTile.X, currentTile.Z - chunkwidth);

            bool leftWally5 = findWalls(currentTile.X - chunkwidth, currentTile.Z);
            bool rightWally5 = findWalls(currentTile.X + chunkwidth, currentTile.Z);

            if (rightTilly5 == false &&
               frontTilly5 == false &&
               frontWally5 == false &&
               backWally5 == true &&
               leftWally5 == true &&
               rightWally5 == false)
            {
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 6);
                    //rightFrontCornerInside.Add(currentTile);
                    buildRightFrontInsideCorner();
                }
            }
            /////////////////////////////////////LEFTBACKINSIDECORNER/////////////////////////////////////////

            bool leftTilly6 = findTiles(currentTile.X - chunkwidth, currentTile.Z);
            bool rightTilly6 = findTiles(currentTile.X + chunkwidth, currentTile.Z);

            bool frontTilly6 = findTiles(currentTile.X, currentTile.Z + chunkwidth);
            bool frontWally6 = findWalls(currentTile.X, currentTile.Z + chunkwidth);

            bool backWally6 = findWalls(currentTile.X, currentTile.Z - chunkwidth);
            bool backTilly6 = findTiles(currentTile.X, currentTile.Z - chunkwidth);

            bool leftWally6 = findWalls(currentTile.X - chunkwidth, currentTile.Z);
            bool rightWally6 = findWalls(currentTile.X + chunkwidth, currentTile.Z);

            if (leftTilly6 == false &&
               //frontTilly6 == false &&
               frontWally6 == true &&
               backWally6 == false &&
               backTilly6 == false &&
               leftWally6 == false &&
               rightWally6 == true ||

               rightTilly6 == false &&
               frontTilly6 == false &&
               frontWally6 == true &&
               backWally6 == false &&
               backTilly6 == false &&
               leftWally6 == true &&
               rightWally6 == true)
            {
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 7);
                    //leftBackCornerInside.Add(currentTile);
                    buildLeftBackInsideCorner();
                }
            }

            /////////////////////////////////////RightBACKINSIDECORNER/////////////////////////////////////////

            bool leftTilly7 = findTiles(currentTile.X - chunkwidth, currentTile.Z);
            bool rightTilly7 = findTiles(currentTile.X + chunkwidth, currentTile.Z);

            bool frontTilly7 = findTiles(currentTile.X, currentTile.Z + chunkwidth);
            bool frontWally7 = findWalls(currentTile.X, currentTile.Z + chunkwidth);

            bool backWally7 = findWalls(currentTile.X, currentTile.Z - chunkwidth);
            bool backTilly7 = findTiles(currentTile.X, currentTile.Z - chunkwidth);

            bool leftWally7 = findWalls(currentTile.X - chunkwidth, currentTile.Z);
            bool rightWally7 = findWalls(currentTile.X + chunkwidth, currentTile.Z);

            if (rightTilly7 == false &&
               //frontTilly7 == false &&
               frontWally7 == true &&
               backWally7 == false &&
               backTilly7 == false &&
               leftWally7 == true &&
               rightWally7 == false ||

               rightTilly7 == false &&
               frontTilly7 == false &&
               frontWally7 == true &&
               backWally7 == false &&
               backTilly7 == false &&
               leftWally7 == true &&
               rightWally7 == true)
            {
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 8);
                    //rightBackCornerInside.Add(currentTile);
                    buildRightBackInsideCorner();
                }
            }


            /////////////////////////////////////LEFTFRONTOUTSIDECORNER/////////////////////////////////////////

            //bool leftTilly8 = findTiles(currentTile.X - chunkwidth, currentTile.Z);
            bool rightTilly8 = findTiles(currentTile.X + chunkwidth, currentTile.Z);

            //bool frontTilly8 = findTiles(currentTile.X, currentTile.Z + chunkwidth);
            bool frontWally8 = findWalls(currentTile.X, currentTile.Z + chunkwidth);

            bool backWally8 = findWalls(currentTile.X, currentTile.Z - chunkwidth);
            bool backTilly8 = findTiles(currentTile.X, currentTile.Z - chunkwidth);

            bool leftWally8 = findWalls(currentTile.X - chunkwidth, currentTile.Z);
            bool rightWally8 = findWalls(currentTile.X + chunkwidth, currentTile.Z);

            bool leftFrontTilly8 = findTiles(currentTile.X - chunkwidth, currentTile.Z + chunkwidth);
            bool leftFrontWally8 = findWalls(currentTile.X - chunkwidth, currentTile.Z + chunkwidth);

            bool rightBackTilly8 = findTiles(currentTile.X + chunkwidth, currentTile.Z - chunkwidth);
            bool rightBackWally8 = findWalls(currentTile.X + chunkwidth, currentTile.Z - chunkwidth);

            if (frontWally8 == true &&
                 backTilly8 == true &&
                 leftWally8 == true &&
                 rightTilly8 == true &&
                 leftFrontTilly8 == false &&
                 leftFrontWally8 == false &&
                 rightBackTilly8 == true ||

                 frontWally8 == true &&
                 backTilly8 == true &&
                 leftWally8 == true &&
                 rightWally8 == true &&
                 leftFrontTilly8 == false &&
                 leftFrontWally8 == false &&
                 rightBackTilly8 == true ||

                 frontWally8 == true &&
                 backWally8 == true &&
                 leftWally8 == true &&
                 rightTilly8 == true &&
                 leftFrontTilly8 == false &&
                 leftFrontWally8 == false &&
                 rightBackTilly8 == true ||

                 frontWally8 == true &&
                 backWally8 == true &&
                 leftWally8 == true &&
                 rightWally8 == true &&
                 leftFrontTilly8 == false &&
                 leftFrontWally8 == false &&
                 rightBackTilly8 == true ||

                 frontWally8 == true &&
                 backTilly8 == true &&
                 leftWally8 == true &&
                 rightTilly8 == true &&
                 leftFrontTilly8 == false &&
                 leftFrontWally8 == false &&
                 rightBackWally8 == true ||

                  frontWally8 == true &&
                  backTilly8 == true &&
                  leftWally8 == true &&
                  rightWally8 == true &&
                  leftFrontTilly8 == false &&
                  leftFrontWally8 == false &&
                  rightBackWally8 == true ||

                  frontWally8 == true &&
                  backWally8 == true &&
                  leftWally8 == true &&
                  rightTilly8 == true &&
                  leftFrontTilly8 == false &&
                  leftFrontWally8 == false &&
                  rightBackWally8 == true ||

                  frontWally8 == true &&
                  backWally8 == true &&
                  leftWally8 == true &&
                  rightWally8 == true &&
                  leftFrontTilly8 == false &&
                  leftFrontWally8 == false &&
                  rightBackWally8 == true)
            {
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 9);
                    //leftFrontCornerOutside.Add(currentTile);
                    buildLeftFrontOutsideCorner();
                }
            }


            /////////////////////////////////////RIGHTFRONTOUTSIDECORNER/////////////////////////////////////////

            bool leftWally9 = findWalls(currentTile.X - chunkwidth, currentTile.Z);
            bool rightTilly9 = findTiles(currentTile.X + chunkwidth, currentTile.Z);

            //bool frontTilly9 = findTiles(currentTile.X, currentTile.Z + chunkwidth);
            bool frontWally9 = findWalls(currentTile.X, currentTile.Z + chunkwidth);

            bool backWally9 = findWalls(currentTile.X, currentTile.Z - chunkwidth);
            bool backTilly9 = findTiles(currentTile.X, currentTile.Z - chunkwidth);

            bool leftTilly9 = findTiles(currentTile.X - chunkwidth, currentTile.Z);
            bool rightWally9 = findWalls(currentTile.X + chunkwidth, currentTile.Z);

            bool leftBackTilly9 = findTiles(currentTile.X - chunkwidth, currentTile.Z - chunkwidth);
            bool leftBackWally9 = findWalls(currentTile.X - chunkwidth, currentTile.Z - chunkwidth);

            bool rightFrontWally9 = findWalls(currentTile.X + chunkwidth, currentTile.Z + chunkwidth);
            bool rightFrontTilly9 = findTiles(currentTile.X + chunkwidth, currentTile.Z + chunkwidth);



            if (frontWally9 == true &&
               backTilly9 == true &&
               leftTilly9 == true &&
               rightWally9 == true &&
               leftBackTilly9 == true &&
               rightFrontWally9 == false &&
               rightFrontTilly9 == false ||

               frontWally9 == true &&
               backTilly9 == true &&
               leftWally9 == true &&
               rightWally9 == true &&
               leftBackTilly9 == true &&
               rightFrontWally9 == false &&
               rightFrontTilly9 == false ||

               frontWally9 == true &&
               backWally9 == true &&
               leftTilly9 == true &&
               rightWally9 == true &&
               leftBackTilly9 == true &&
               rightFrontWally9 == false &&
               rightFrontTilly9 == false ||

               frontWally9 == true &&
               backWally9 == true &&
               leftWally9 == true &&
               rightWally9 == true &&
               leftBackTilly9 == true &&
               rightFrontWally9 == false &&
               rightFrontTilly9 == false ||


               frontWally9 == true &&
               backTilly9 == true &&
               leftTilly9 == true &&
               rightWally9 == true &&
               leftBackWally9 == true &&
               rightFrontWally9 == false &&
               rightFrontTilly9 == false ||


               frontWally9 == true &&
               backTilly9 == true &&
               leftWally9 == true &&
               rightWally9 == true &&
               leftBackWally9 == true &&
               rightFrontWally9 == false &&
               rightFrontTilly9 == false ||

               frontWally9 == true &&
               backWally9 == true &&
               leftTilly9 == true &&
               rightWally9 == true &&
               leftBackWally9 == true &&
               rightFrontWally9 == false &&
               rightFrontTilly9 == false ||

               frontWally9 == true &&
               backWally9 == true &&
               leftWally9 == true &&
               rightWally9 == true &&
               leftBackWally9 == true &&
               rightFrontWally9 == false &&
               rightFrontTilly9 == false)
            {
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 10);
                    //rightFrontCornerOutside.Add(currentTile);
                    buildRightFrontOutsideCorner();
                }
            }


            /////////////////////////////////////LEFTBACKOUTSIDECORNER/////////////////////////////////////////

            bool leftTilly10 = findTiles(currentTile.X - chunkwidth, currentTile.Z);
            bool rightTilly10 = findTiles(currentTile.X + chunkwidth, currentTile.Z);

            bool frontTilly10 = findTiles(currentTile.X, currentTile.Z + chunkwidth);
            bool frontWally10 = findWalls(currentTile.X, currentTile.Z + chunkwidth);

            bool backWally10 = findWalls(currentTile.X, currentTile.Z - chunkwidth);

            bool leftWally10 = findWalls(currentTile.X - chunkwidth, currentTile.Z);
            bool rightWally10 = findWalls(currentTile.X + chunkwidth, currentTile.Z);

            bool leftBackTilly10 = findTiles(currentTile.X - chunkwidth, currentTile.Z - chunkwidth);
            bool leftBackWally10 = findWalls(currentTile.X - chunkwidth, currentTile.Z - chunkwidth);

            bool rightFrontWally10 = findWalls(currentTile.X + chunkwidth, currentTile.Z + chunkwidth);
            bool rightFrontTilly10 = findTiles(currentTile.X + chunkwidth, currentTile.Z + chunkwidth);



            if (leftWally10 == true &&
               frontTilly10 == true &&
               backWally10 == true &&
               rightTilly10 == true &&
               leftBackTilly10 == false &&
               leftBackWally10 == false &&
               rightFrontTilly10 == true ||

               leftWally10 == true &&
               frontWally10 == true &&
               backWally10 == true &&
               rightTilly10 == true &&
               leftBackTilly10 == false &&
               leftBackWally10 == false &&
               rightFrontTilly10 == true ||

               leftWally10 == true &&
               frontTilly10 == true &&
               backWally10 == true &&
               rightWally10 == true &&
               leftBackTilly10 == false &&
               leftBackWally10 == false &&
               rightFrontTilly10 == true ||

               leftWally10 == true &&
               frontWally10 == true &&
               backWally10 == true &&
               rightWally10 == true &&
               leftBackTilly10 == false &&
               leftBackWally10 == false &&
               rightFrontTilly10 == true ||

               leftWally10 == true &&
               frontTilly10 == true &&
               backWally10 == true &&
               rightTilly10 == true &&
               leftBackTilly10 == false &&
               leftBackWally10 == false &&
               rightFrontWally10 == true ||

               leftWally10 == true &&
               frontWally10 == true &&
               backWally10 == true &&
               rightTilly10 == true &&
               leftBackTilly10 == false &&
               leftBackWally10 == false &&
               rightFrontWally10 == true ||

               leftWally10 == true &&
               frontTilly10 == true &&
               backWally10 == true &&
               rightWally10 == true &&
               leftBackTilly10 == false &&
               leftBackWally10 == false &&
               rightFrontWally10 == true ||

               leftWally10 == true &&
               frontWally10 == true &&
               backWally10 == true &&
               rightWally10 == true &&
               leftBackTilly10 == false &&
               leftBackWally10 == false &&
               rightFrontWally10 == true)
            {
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 11);
                    //leftBackCornerOutside.Add(currentTile);
                    buildLeftBackOutsideCorner();
                }
            }
            /////////////////////////////////////RightBACKOUTSIDECORNER/////////////////////////////////////////


            bool leftTilly11 = findTiles(currentTile.X - chunkwidth, currentTile.Z);
            bool rightTilly11 = findTiles(currentTile.X + chunkwidth, currentTile.Z);

            bool frontTilly11 = findTiles(currentTile.X, currentTile.Z + chunkwidth);
            bool frontWally11 = findWalls(currentTile.X, currentTile.Z + chunkwidth);

            bool backWally11 = findWalls(currentTile.X, currentTile.Z - chunkwidth);

            bool leftWally11 = findWalls(currentTile.X - chunkwidth, currentTile.Z);
            bool rightWally11 = findWalls(currentTile.X + chunkwidth, currentTile.Z);

            bool leftFrontTilly11 = findTiles(currentTile.X - chunkwidth, currentTile.Z + chunkwidth);
            bool leftFrontWally11 = findWalls(currentTile.X - chunkwidth, currentTile.Z + chunkwidth);

            bool rightBackTilly11 = findTiles(currentTile.X + chunkwidth, currentTile.Z - chunkwidth);
            bool rightBackWally11 = findWalls(currentTile.X + chunkwidth, currentTile.Z - chunkwidth);

            if (leftTilly11 == true &&
               frontTilly11 == true &&
               backWally11 == true &&
               rightWally11 == true &&
               rightBackWally11 == false &&
               rightBackTilly11 == false &&
               leftFrontTilly11 == true ||

               leftWally11 == true &&
               frontTilly11 == true &&
               backWally11 == true &&
               rightWally11 == true &&
               rightBackWally11 == false &&
               rightBackTilly11 == false &&
               leftFrontTilly11 == true ||

               leftTilly11 == true &&
               frontWally11 == true &&
               backWally11 == true &&
               rightWally11 == true &&
               rightBackWally11 == false &&
               rightBackTilly11 == false &&
               leftFrontTilly11 == true ||

               leftWally11 == true &&
               frontWally11 == true &&
               backWally11 == true &&
               rightWally11 == true &&
               rightBackWally11 == false &&
               rightBackTilly11 == false &&
               leftFrontTilly11 == true ||

               leftTilly11 == true &&
               frontTilly11 == true &&
               backWally11 == true &&
               rightWally11 == true &&
               rightBackWally11 == false &&
               rightBackTilly11 == false &&
               leftFrontWally11 == true ||

               leftWally11 == true &&
               frontTilly11 == true &&
               backWally11 == true &&
               rightWally11 == true &&
               rightBackWally11 == false &&
               rightBackTilly11 == false &&
               leftFrontWally11 == true ||

               leftTilly11 == true &&
               frontWally11 == true &&
               backWally11 == true &&
               rightWally11 == true &&
               rightBackWally11 == false &&
               rightBackTilly11 == false &&
               leftFrontWally11 == true ||

               leftWally11 == true &&
               frontWally11 == true &&
               backWally11 == true &&
               rightWally11 == true &&
               rightBackWally11 == false &&
               rightBackTilly11 == false &&
               leftFrontWally11 == true)

            {
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 12);
                    //rightBackCornerOutside.Add(currentTile);
                    buildRightBackOutsideCorner();
                    // StopCoroutine("checkForWallLeft");
                }
            }



            ///ADDED MAY 3RD 2022 - TO SORT WITH PROPER TYLE TYPE
            bool leftTilly12 = findTiles(currentTile.X - chunkwidth, currentTile.Z);
            bool rightTilly12 = findTiles(currentTile.X + chunkwidth, currentTile.Z);
            bool frontTilly12 = findTiles(currentTile.X, currentTile.Z + chunkwidth);
            bool backTilly12 = findTiles(currentTile.X, currentTile.Z - chunkwidth);

            bool leftWally12 = findWalls(currentTile.X - chunkwidth, currentTile.Z);
            bool rightWally12 = findWalls(currentTile.X + chunkwidth, currentTile.Z);

            bool backWally12 = findWalls(currentTile.X, currentTile.Z - chunkwidth);
            bool frontWally12 = findWalls(currentTile.X, currentTile.Z + chunkwidth);

            bool leftFrontTilly12 = findTiles(currentTile.X - chunkwidth, currentTile.Z + chunkwidth);
            bool leftFrontWally12 = findWalls(currentTile.X - chunkwidth, currentTile.Z + chunkwidth);

            bool leftBackTilly12 = findTiles(currentTile.X - chunkwidth, currentTile.Z + chunkwidth);
            bool leftBackWally12 = findWalls(currentTile.X - chunkwidth, currentTile.Z + chunkwidth);

            bool rightBackTilly12 = findTiles(currentTile.X + chunkwidth, currentTile.Z - chunkwidth);
            bool rightBackWally12 = findWalls(currentTile.X + chunkwidth, currentTile.Z - chunkwidth);


            bool rightFrontTilly12 = findTiles(currentTile.X + chunkwidth, currentTile.Z + chunkwidth);
            bool rightFrontWally12 = findWalls(currentTile.X + chunkwidth, currentTile.Z + chunkwidth);


            if (leftWally12 == true &&
                rightWally12 == false &&
                backWally12 == true &&
                frontWally12 == true &&
                leftTilly12 == false &&
                rightTilly12 == false &&
                frontTilly12 == false &&
                backTilly12 == false &&
                leftFrontTilly12 == true ||

                leftWally12 == false &&
                rightWally12 == true &&
                backWally12 == true &&
                frontWally12 == true &&
                leftTilly12 == false &&
                rightTilly12 == false &&
                frontTilly12 == false &&
                backTilly12 == false &&
                rightFrontTilly12 == true ||

                leftWally12 == false &&
                rightWally12 == true &&
                backWally12 == true &&
                frontWally12 == true &&
                leftTilly12 == false &&
                rightTilly12 == false &&
                frontTilly12 == false &&
                backTilly12 == false &&
                rightBackTilly12 == true ||

                leftWally12 == true &&
                rightWally12 == false &&
                backWally12 == true &&
                frontWally12 == true &&
                leftTilly12 == false &&
                rightTilly12 == false &&
                frontTilly12 == false &&
                backTilly12 == false &&
                leftBackTilly12 == true ||

                leftWally12 == true &&
                rightWally12 == true &&
                backWally12 == true &&
                frontWally12 == true &&
                leftTilly12 == false &&
                rightTilly12 == false &&
                frontTilly12 == false &&
                backTilly12 == false &&
                leftBackTilly12 == true &&
                rightFrontTilly12 == true
                ||

                leftWally12 == true &&
                rightWally12 == true &&
                backWally12 == true &&
                frontWally12 == true &&
                leftTilly12 == false &&
                rightTilly12 == false &&
                frontTilly12 == false &&
                backTilly12 == false &&
                leftFrontTilly12 == true &&
                rightBackTilly12 == true)
            {
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, -1);
                    //leftFrontCornerOutside.Add(currentTile);
                    buildLeftFrontOutsideCorner();
                }
            }



            /*bool leftTilly12 = findTiles(currentTile.X - chunkwidth, currentTile.Z);
            bool rightTilly12 = findTiles(currentTile.X + chunkwidth, currentTile.Z);

            bool frontTilly12 = findTiles(currentTile.X, currentTile.Z + chunkwidth);
            bool frontWally12 = findWalls(currentTile.X, currentTile.Z + chunkwidth);

            bool backWally12 = findWalls(currentTile.X, currentTile.Z - chunkwidth);
            bool backTilly12 = findTiles(currentTile.X, currentTile.Z - chunkwidth);

            bool leftWally12 = findWalls(currentTile.X - chunkwidth, currentTile.Z);
            bool rightWally12 = findWalls(currentTile.X + chunkwidth, currentTile.Z);

            bool leftFrontTilly12 = findTiles(currentTile.X - chunkwidth, currentTile.Z + chunkwidth);
            bool leftFrontWally12 = findWalls(currentTile.X - chunkwidth, currentTile.Z + chunkwidth);

            bool rightBackTilly12 = findTiles(currentTile.X + chunkwidth, currentTile.Z - chunkwidth);
            bool rightBackWally12 = findWalls(currentTile.X + chunkwidth, currentTile.Z - chunkwidth);

            if (frontWally12 == true &&
               backTilly12 == false &&
               leftWally12 == true &&
               rightTilly12 == true &&
               leftFrontTilly12 == false &&
               leftFrontWally12 == true &&
               rightBackTilly12 == false && )
            {
                if (!typeoftiles.ContainsKey(currentTile))
                {
                    typeoftiles.Add(currentTile, 9);
                    //leftFrontCornerOutside.Add(currentTile);
                    buildLeftFrontOutsideCorner();
                }
            }*/










            // //yield return new WaitForSeconds(BuildingWaitTime);


            /*if (counter==2)
            {
                BuildFaces();
                counter = 3;
            }

            Debug.Log("done");*/

            // Instantiate(sphere, currentTile, Quaternion.identity);
        }


        bool findWalls(float x, float z)
        {
            /*for (int i = 0; i < someWall.Count; i++)
            {
                if ((x < someWall[i].X) || (z < someWall[i].Z) || (x >= (someWall[i].X) + chunkwidth) || (z >= (someWall[i].Z) + chunkwidth))
                {
                    continue;
                }
                return true;
            }

            return false;*/


            var enumerator0 = adjacentWall.GetEnumerator();
            //Vector3? tls0 = null;     
            while (enumerator0.MoveNext())
            {
                var tls0 = enumerator0.Current;
                var tuile = tls0;

                if ((x < tuile.X) || (z < tuile.Z) || (x >= (tuile.X) + chunkwidth) || (z >= (tuile.Z) + chunkwidth))
                {
                    continue;
                }
                return true;
            }
            return false;
        }








        /*int findToSortTiles(float x, float z)
        {
            if (typeoftiles.ContainsKey(new Vector3(x, 0, z)))
            {
                //typeoftiles.index
                //return typeoftiles.
            }


            /*var enumerator0 = forsortingtiles.GetEnumerator();
            //Vector3? tls0 = null;     
            while (enumerator0.MoveNext())
            {
                var tls0 = enumerator0.Current;
                var tuile = tls0.Key;

                if ((x < tuile.X) || (z < tuile.Z) || (x >= (tuile.X) + chunkwidth) || (z >= (tuile.Z) + chunkwidth))
                {
                    continue;
                }
                return true;
            }
            return false;
        }*/





        bool findTiles(float x, float z)
        {
            var enumerator0 = typeoftiles.GetEnumerator();
            //Vector3? tls0 = null;     
            while (enumerator0.MoveNext())
            {
                var tls0 = enumerator0.Current;
                var tuile = tls0.Key;

                if ((x < tuile.X) || (z < tuile.Z) || (x >= (tuile.X) + chunkwidth) || (z >= (tuile.Z) + chunkwidth))
                {
                    continue;
                }
                return true;
            }
            return false;
        }


        bool findToSortTiles(float x, float z)
        {
            var enumerator0 = forsortingtiles.GetEnumerator();
            //Vector3? tls0 = null;     
            while (enumerator0.MoveNext())
            {
                var tls0 = enumerator0.Current;
                var tuile = tls0.Key;

                if ((x < tuile.X) || (z < tuile.Z) || (x >= (tuile.X) + chunkwidth) || (z >= (tuile.Z) + chunkwidth))
                {
                    continue;
                }
                return true;
            }
            return false;
        }




        /*bool findWalls(float x, float z)
        {
            var enumerator0 = adjacentWall.GetEnumerator();
            //Vector3? tls0 = null;     
            while (enumerator0.MoveNext())
            {
                var tls0 = enumerator0.Current;
                var tuile = tls0.Key;

                if ((x < tuile.x + blockSize) || (z < tuile.z + blockSize) || (x >= (tuile.x + blockSize) + chunkwidth ) || (z >= (tuile.z + blockSize) + chunkwidth ))
                {
                    continue;
                }
                return true;
            }
            return false;
        }




        bool findTiles(float x, float z)
        {
            var enumerator0 = createdTiles.GetEnumerator();
            //Vector3? tls0 = null;     
            while (enumerator0.MoveNext())
            {
                var tls0 = enumerator0.Current;
                var tuile = tls0.Key;

                if ((x < tuile.x + blockSize) || (z < tuile.z + blockSize) || (x >= (tuile.x + blockSize) + chunkwidth ) || (z >= (tuile.z + blockSize) + chunkwidth ))
                {
                    Debug.Log("anus");
                    continue;
                }
                return true;
            }
            return false;
        }*/





        void buildWallLeft()
        {
            for (int i = 0; i < leftWall.Count; i++)
            {
                if (!builtLeftWall.Contains(leftWall[i]))
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
                if (!builtRightWall.Contains(rightWall[i]))
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
                if (!builtFrontWall.Contains(frontWall[i]))
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
                if (!builtBackWall.Contains(backWall[i]))
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
                if (!builtLeftFrontInsideCorner.Contains(leftFrontCornerInside[i]))
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
                if (!builtRightFrontInsideCorner.Contains(rightFrontCornerInside[i]))
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
                if (!builtLeftBackInsideCorner.Contains(leftBackCornerInside[i]))
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
                if (!builtRightBackInsideCorner.Contains(rightBackCornerInside[i]))
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
                if (!builtLeftFrontOutsideCorner.Contains(leftFrontCornerOutside[i]))
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
                if (!builtRightFrontOutsideCorner.Contains(rightFrontCornerOutside[i]))
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
                if (!builtLeftBackOutsideCorner.Contains(leftBackCornerOutside[i]))
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
                if (!builtRightBackOutsideCorner.Contains(rightBackCornerOutside[i]))
                {
                    //Instantiate(RightBackOutsideCornerWall, rightBackCornerOutside[i], Quaternion.identity);
                    builtRightBackOutsideCorner.Add(rightBackCornerOutside[i]);
                }
            }
            // //yield return new WaitForSeconds(BuildingWaitTime);
        }














        void buildFloorTiles()
        {
            countingCoroutinesStart++;

            //yield return new WaitForSeconds(BuildingWaitTime);

            for (int i = 0; i < toRemove.Count; i++)
            {
                if (!typeoftiles.ContainsKey(toRemove[i]))
                {
                    typeoftiles.Add(toRemove[i], 0);
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

            countingCoroutinesEnd++;

        }











        //WaitForSeconds slowdown = new WaitForSeconds(2f);

        /*IEnumerator waitFunction()
        {
            yield return slowdown;
            //StartCoroutine("checkForWallLeft");
        }*/



        /*public static bool FindChunk(Vector3 pos)
        {
            for (int a = 0; a < chunks.Count; a++)
            {
                Vector3 cpos = chunks[a].currentposition;

                if ((pos.x < cpos.x) || (pos.z < cpos.z) || (pos.x >= cpos.x + width - 2) || (pos.z >= cpos.z + width - 2)) continue;
                //return chunks[a];
                return true;

            }
            return false;
        }*/



        /*
        public sccstriglevelchunk getChunk(float x, float y, float z) //, Vector3 chunkPos
        { 
            float x0 = (float)(Math.Floor(x * 10) / 10);
            float y0 = (float)(Math.Floor(y * 10) / 10);
            float z0 = (float)(Math.Floor(z * 10) / 10);



            //var enumerator0 = sclevelgenchunk.arrayofchunks.GetEnumerator();
            for (int i = 0; i < sclevelgenchunk.arrayofchunks.Length; i++)
            {

                float x1 = (float)(Math.Floor(sclevelgenchunk.arrayofchunks[i].chunkPos.X * 10) / 10);
                float y1 = (float)(Math.Floor(sclevelgenchunk.arrayofchunks[i].chunkPos.Y * 10) / 10);
                float z1 = (float)(Math.Floor(sclevelgenchunk.arrayofchunks[i].chunkPos.Z * 10) / 10);


                if (x0 == x1 && y0 == y1 && z0 == z1)
                {
                    return sclevelgenchunk.arrayofchunks[i];
                }
                /*
                if (x >= 0 && y >= 0 && z >= 0 && x < mapWidth && y < mapHeight && z < mapDepth)
                {
                    
                }

            }

            return null;

            /*while (enumerator0.MoveNext())
            {
                var tls0 = enumerator0.Current;

                if ((x < tls0.Value.X) || y < tls0.Value.Y || (z < tls0.Value.Z) || (x >= (tls0.Value.X) + 10) || (y >= (tls0.Value.Y) + 10) || (z >= tls0.Value.Z + 10))
                {
                    continue;
                }
                return tls0.Key;
            }
            return null;
        }*/






        /*
        public sccstriglevelchunk getChunk(float x, float y, float z)
        {
            var enumerator0 = createdTiles.GetEnumerator();

            float x0 = (float)(Math.Round(x * 10) / 10);
            float y0 = (float)(Math.Round(y * 10) / 10);
            float z0 = (float)(Math.Round(z * 10) / 10);


            while (enumerator0.MoveNext())
            {
                var tls0 = enumerator0.Current;

                /*if ((x < tls0.Value.X) || y < tls0.Value.Y || (z < tls0.Value.Z) || (x >= (tls0.Value.X) + 10) || (y >= (tls0.Value.Y) + 10) || (z >= tls0.Value.Z + 10))
                {
                    continue;
                }


                float x1 = (float)(Math.Round(tls0.Value.X * 10) / 10);
                float y1 = (float)(Math.Round(tls0.Value.Y * 10) / 10);
                float z1 = (float)(Math.Round(tls0.Value.Z * 10) / 10);


                if (x0 == x1 && y0 == y1 && z0 == z1)
                {
                    return tls0.Key;
                }
            }
            return null;
        }*/






        public sccstriglevelchunk getChunk(float x, float y, float z)
        {
            var enumerator0 = sccstriglevelchunk.chunkz.GetEnumerator();

            float x0 = (float)(Math.Round(x * 10) / 10);
            float y0 = (float)(Math.Round(y * 10) / 10);
            float z0 = (float)(Math.Round(z * 10) / 10);


            while (enumerator0.MoveNext())
            {
                var tls0 = enumerator0.Current;

                //if ((x < tls0.Value.X) || y < tls0.Value.Y || (z < tls0.Value.Z) || (x >= (tls0.Value.X) + 10) || (y >= (tls0.Value.Y) + 10) || (z >= tls0.Value.Z + 10))
                //{
                //    continue;
                //}


                float x1 = (float)(Math.Round(tls0.Value.X * 10) / 10);
                float y1 = (float)(Math.Round(tls0.Value.Y * 10) / 10);
                float z1 = (float)(Math.Round(tls0.Value.Z * 10) / 10);


                if (x0 == x1 && y0 == y1 && z0 == z1)
                {
                    return tls0.Key;
                }
                else
                {
                    continue;
                }
            }
            return null;
        }


        /*
        public sccstriglevelchunk getChunk(float x, float y, float z)
        {
            var enumerator0 = sccstriglevelchunk.chunkz.GetEnumerator();

            float x0 = (float)(Math.Round(x * 10) / 10);
            float y0 = (float)(Math.Round(y * 10) / 10);
            float z0 = (float)(Math.Round(z * 10) / 10);


            while (enumerator0.MoveNext())
            {
                var tls0 = enumerator0.Current;

                /*if ((x < tls0.Value.X) || y < tls0.Value.Y || (z < tls0.Value.Z) || (x >= (tls0.Value.X) + 10) || (y >= (tls0.Value.Y) + 10) || (z >= tls0.Value.Z + 10))
                {
                    continue;
                }


                float x1 = (float)(Math.Round(tls0.Value.X * 10) / 10);
                float y1 = (float)(Math.Round(tls0.Value.Y * 10) / 10);
                float z1 = (float)(Math.Round(tls0.Value.Z * 10) / 10);


                if (x0 == x1 && y0 == y1 && z0 == z1)
                {
                    return tls0.Key;
                }
            }
            return null;
        }

        */


        /*
        public sccstriglevelchunk getChunk(float x, float y, float z)
        {
            var enumerator0 = sccstriglevelchunk.chunkz.GetEnumerator();

            while (enumerator0.MoveNext())
            {
                var tls0 = enumerator0.Current;

                if ((x < tls0.Value.X) || y < tls0.Value.Y || (z < tls0.Value.Z) || (x >= (tls0.Value.X) + 10) || (y >= (tls0.Value.Y) + 10) || (z >= tls0.Value.Z + 10))
                {
                    continue;
                }
                return tls0.Key;
            }
            return null;
        }*/



        /*
        public sccstriglevelchunk getChunk(float x, float y, float z)
        {
            /*var enumerator0 = sccstriglevelchunk.chunkz.GetEnumerator();

            while (enumerator0.MoveNext())
            {
                var tls0 = enumerator0.Current;

                if ((x < tls0.Value.X) || y < tls0.Value.Y || (z < tls0.Value.Z) || (x >= (tls0.Value.X) + 10) || (y >= (tls0.Value.Y) + 10) || (z >= tls0.Value.Z + 10))
                {
                    continue;
                }
                return tls0.Key;
            }
            return null;

            for (int i = 0;i < sclevelgenchunk.arr)
            {

            }

        }*/












        /*bool findTiles(float x, float z)
        {
            var enumerator0 = tilesCreated.GetEnumerator();
            //Vector3? tls0 = null;     
            while (enumerator0.MoveNext())
            {
                var tls0 = enumerator0.Current;
                var tuile = tls0.Key;

                if ((x < tuile.x) || (z < tuile.z) || (x >= (tuile.x) + chunkwidth) || (z >= tuile.z + chunkwidth))
                {
                    continue;
                }
                return true;
            }
            return false;
        }*/


        /*bool findWalls(float x, float z)
        {
            var enumerator0 = adjacentWall.GetEnumerator();
            //Vector3? tls0 = null;     
            while (enumerator0.MoveNext())
            {
                var tls0 = enumerator0.Current;
                var tuile = tls0.Key;

                if ((x < tuile.x) || (z < tuile.z) || (x >= (tuile.x) + chunkwidth) || (z >= tuile.z + chunkwidth))
                {
                    continue;
                }
                return true;
            }
            return false;
        }*/

    }

}




/* StartCoroutine(DelayedCallback((float x, float z) =>
               {

               }));     
   public IEnumerator DelayedCallback(System.Action<float,float> callBack)
   {

       int counter999 = 1;
       yield return new WaitForSeconds(1f);
       yield return counter999;
       Debug.Log("yo");

   }*/







/*IEnumerator TryToSleep()
{
    float x = currentTile.X;
    float z = currentTile.Z;

    var request = CountSheep();
    yield return StartCoroutine(request);
    int? result = request.Current as int?;
    Debug.Log(result);

}*/

/*IEnumerator CountSheep()
{
    int count = 0;
    while (count <99)
    {
        Debug.Log(count);
        yield return new WaitForSeconds(0.05f);
        count++;
    }      
    yield return count;
}*/
