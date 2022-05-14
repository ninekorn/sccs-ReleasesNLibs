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

        public Dictionary<Vector3, int> createdTiles = new Dictionary<Vector3, int>();

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

        public Dictionary<Vector3, Vector3> tilesCreated = new Dictionary<Vector3, Vector3>();

        Dictionary<Vector3, Vector3> ExtremityFloor = new Dictionary<Vector3, Vector3>();





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
            for (int i = 0; i < tileAmount; i++)
            {

                float dir = (float)sccs.sc_maths.getSomeRandNumThousandDecimal(0, 1, 0.1f, 0, -1);
                //int tile = (int)sccs.sc_maths.getSomeRandNum(0, tiles.Count); //tiles.Length

                //Console.WriteLine(dir);
                //float dir = UnityEngine.Random.Range(0f, 1f);
                //int tile = UnityEngine.Random.Range(0, tiles.Length);


                CreateTile();
                CallMoveGen(dir);
                //yield return new WaitForSeconds(SpawnerMoveWaitTime);

                if (i == tileAmount - 1)
                {
                    //Finish();
                }
            }

            Finish();
            createfinal();
            //yield return 0;
        }


        void CallMoveGen(float ranDir)
        {
            if (ranDir < chanceUp && ranDir > 0)
            {
                MoveGen(0);
            }
            else if (ranDir < chanceRight && ranDir > chanceUp)
            {
                MoveGen(1);
            }
            else if (ranDir < chanceDown && ranDir > chanceRight)
            {
                MoveGen(2);
            }
            else
            {
                MoveGen(3);
            }
        }



        void CreateTile() //int tileIndex
        {
            if (!createdTiles.ContainsKey(currentposition))
            {
                //Instantiate(floorTiles, currentposition, Quaternion.identity);

                //floorTiles.tag = "chunks";
                createdTiles.Add(currentposition, -1);
                tilesCreated.Add(currentposition, currentposition);
            }
            else
            {
                tileAmount++;
            }
        }


        Vector3 currentposition;

        void MoveGen(int dir)
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
                    currentposition = new Vector3(currentposition.X, 0, currentposition.Z + (chunkdepth * planesize));
                    break;
                case 1:
                    currentposition = new Vector3(currentposition.X + (chunkwidth * planesize), 0, currentposition.Z);
                    break;
                case 2:
                    currentposition = new Vector3(currentposition.X, 0, currentposition.Z - (chunkdepth * planesize));
                    break;
                case 3:
                    currentposition = new Vector3(currentposition.X - (chunkwidth * planesize), 0, currentposition.Z);
                    break;

            }
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

            var enumerator0 = createdTiles.GetEnumerator();

            while (enumerator0.MoveNext())
            {
                var currentTuile = enumerator0.Current;
                currentTile = currentTuile.Key;
                checkAllSides(currentTile, currentTuile);
            }
        }


        void checkAllSides(Vector3 currentTilePos, KeyValuePair<Vector3, int> currentTuile)
        {


            /*for (float x = (currentPos.x / planeSize) - viewSize0; x <= (currentPos.x / planeSize) + viewSize0; x += chunkSizeLOWDETAIL)
            {
                for (float y = (currentPos.y / planeSize) - viewSize1; y <= (currentPos.y / planeSize) + viewSize1; y += chunkSizeLOWDETAIL)
                {
                    for (float z = (currentPos.z / planeSize) - viewSize0; z <= (currentPos.z / planeSize) + viewSize0; z += chunkSizeLOWDETAIL)
                    {
                        float chunkX0 = Math.FloorToInt(x / chunkSizeLOWDETAIL) * chunkSizeLOWDETAIL * planeSize;*/

            
            for (var x = -chunkwidth / blockSize; x <= chunkwidth / blockSize; x += chunkwidth)
            {
                for (var z = -chunkdepth / blockSize; z <= chunkdepth / blockSize; z += chunkwidth)
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
                        if (!adjacentWall.Contains(new Vector3(checkX, 0, checkY)) && !createdTiles.ContainsKey(new Vector3(checkX, 0, checkY)))
                        {
                            //MainWindow.MessageBox((IntPtr)0, "test", "scmsg", 0);
                            adjacentWall.Add(new Vector3(checkX, 0, checkY));
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
                    buildWalls(currentTuile);
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

        void buildWalls(Vector3 currentTile)
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
               rightWally0 == true)
            {
                if (!leftWall.Contains(currentTile))
                {
                    leftWall.Add(currentTile);
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
               leftWally0 == false &&
               rightWally0 == false ||

               leftTilly1 == false &&
               rightTilly1 == false &&
               frontWally1 == true &&
               backWally1 == true &&
               leftWally0 == true &&
               rightWally0 == false)
            {
                if (!rightWall.Contains(currentTile))
                {
                    rightWall.Add(currentTile);
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
               backWally2 == false)

            {
                if (!frontWall.Contains(currentTile))
                {
                    frontWall.Add(currentTile);
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
               backWally3 == true)

            {
                if (!backWall.Contains(currentTile))
                {
                    backWall.Add(currentTile);
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
                if (!leftFrontCornerInside.Contains(currentTile))
                {
                    leftFrontCornerInside.Add(currentTile);
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
                if (!rightFrontCornerInside.Contains(currentTile))
                {
                    rightFrontCornerInside.Add(currentTile);
                    buildRightFrontInsideCorner();
                }
            }
            /////////////////////////////////////LEFTBACKINSIDECORNER/////////////////////////////////////////

            bool leftTilly6 = findTiles(currentTile.X - chunkwidth, currentTile.Z);
            //bool rightTilly6 = findTiles(currentTile.X + chunkwidth, currentTile.Z);

            //bool frontTilly6 = findTiles(currentTile.X, currentTile.Z + chunkwidth);
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
               rightWally6 == true)
            {
                if (!leftBackCornerInside.Contains(currentTile))
                {
                    leftBackCornerInside.Add(currentTile);
                    buildLeftBackInsideCorner();
                }
            }

            /////////////////////////////////////RightBACKINSIDECORNER/////////////////////////////////////////

            //bool leftTilly7 = findTiles(currentTile.X - chunkwidth, currentTile.Z);
            bool rightTilly7 = findTiles(currentTile.X + chunkwidth, currentTile.Z);

            //bool frontTilly7 = findTiles(currentTile.X, currentTile.Z + chunkwidth);
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
               rightWally7 == false)
            {
                if (!rightBackCornerInside.Contains(currentTile))
                {
                    rightBackCornerInside.Add(currentTile);
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
                if (!leftFrontCornerOutside.Contains(currentTile))
                {
                    leftFrontCornerOutside.Add(currentTile);
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
                if (!rightFrontCornerOutside.Contains(currentTile))
                {
                    rightFrontCornerOutside.Add(currentTile);
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
                if (!leftBackCornerOutside.Contains(currentTile))
                {
                    leftBackCornerOutside.Add(currentTile);
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
                if (!rightBackCornerOutside.Contains(currentTile))
                {
                    rightBackCornerOutside.Add(currentTile);
                    buildRightBackOutsideCorner();
                    // StopCoroutine("checkForWallLeft");
                }
            }

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




        bool findTiles(float x, float z)
        {
            var enumerator0 = createdTiles.GetEnumerator();
            //Vector3? tls0 = null;     
            while (enumerator0.MoveNext())
            {
                var tls0 = enumerator0.Current;
                var tuile = tls0.Key;

                if ((x < tuile.X) || (z < tuile.Z) || (x >= (tuile.X) + chunkwidth) || (z >= (tuile.Z) + chunkwidth * blockSize))
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
                if (!builtFloorTiles.Contains(toRemove[i]))
                {
                    //Instantiate(floorTiles, toRemove[i], Quaternion.identity);
                    builtFloorTiles.Add(toRemove[i]);
                    //yield return new WaitForSeconds(BuildingWaitTime);
                }
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













        public static newFloorTiles GetChunk(float x, float y, float z)
        {
            var enumerator0 = newFloorTiles.chunkz.GetEnumerator();

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
        }

















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
