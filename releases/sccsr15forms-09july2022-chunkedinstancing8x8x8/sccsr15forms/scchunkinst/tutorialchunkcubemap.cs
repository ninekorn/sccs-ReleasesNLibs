using SharpDX;
using System;

using System.Collections;
using System.Collections.Generic;

using SimplexNoise;


namespace sccsr15forms
{
    public class tutorialchunkcubemap
    {

        /*
        public tutorialcubeaschunkinst.instancetype instancetypelist;//=  tutorialcubeaschunkinst.instancetype>();

        public tutorialcubeaschunkinst.scinstanceintmaps mapints;//= tutorialcubeaschunkinst.scinstanceintmaps>();

        public tutorialcubeaschunkinst.scinstancevertdimensions dimensionsmapsa;// = tutorialcubeaschunkinst.scinstancevertdimensions;
        public tutorialcubeaschunkinst.scinstancevertdimensions dimensionsmapsb;//= tutorialcubeaschunkinst.scinstancevertdimensions;

        public tutorialcubeaschunkinst.scinstancevertdimensions firstvertloca;// =  tutorialcubeaschunkinst.scinstancevertdimensions;
        public tutorialcubeaschunkinst.scinstancevertdimensions firstvertlocb;// =  tutorialcubeaschunkinst.scinstancevertdimensions;
        */



        public int indexinmainarray = 0;
        public int vertexcountermemory = 0;

        FastNoise fastNoise = new FastNoise();

        int somecountertopface = 0;
        public tutorialchunkcubemap(int x, int y, int z, float[] newchunkpos_)
        {
            chunkPos = new int[3];

            chunkPos[0] = x;
            chunkPos[1] = y;
            chunkPos[2] = z;

            newchunkpos = newchunkpos_;
        }

        public float[] newchunkpos;
        public int[] chunkPos;

        public int[] mapvertindexfordims;

        public int[] widthdimtop;
        public int[] heightdimtop;
        public int[] depthdimtop;
        public int[] mapfirstvertxtop;
        public int[] mapfirstvertytop;
        public int[] mapfirstvertztop;

        /*public int[] widthdimleft;
        public int[] heightdimleft;
        public int[] depthdimleft;
        public int[] mapfirstvertxleft;
        public int[] mapfirstvertyleft;
        public int[] mapfirstvertzleft;

        public int[] widthdimright;
        public int[] heightdimright;
        public int[] depthdimright;
        public int[] mapfirstvertxright;
        public int[] mapfirstvertyright;
        public int[] mapfirstvertzright;

        public int[] widthdimfront;
        public int[] heightdimfront;
        public int[] depthdimfront;
        public int[] mapfirstvertxfront;
        public int[] mapfirstvertyfront;
        public int[] mapfirstvertzfront;

        public int[] widthdimback;
        public int[] heightdimback;
        public int[] depthdimback;
        public int[] mapfirstvertxback;
        public int[] mapfirstvertyback;
        public int[] mapfirstvertzback;

        public int[] widthdimbottom;
        public int[] heightdimbottom;
        public int[] depthdimbottom;
        public int[] mapfirstvertxbottom;
        public int[] mapfirstvertybottom;
        public int[] mapfirstvertzbottom;*/











        int seed = 3420;
        int total = 0;
        public int[] map;

        public int width = 8;
        public int height = 8;
        public int depth = 8;
        public int maxveclength = 4;


        public int typeofterraintiles;

        public int levelofdetail = 1;

        tutorialcubeaschunkinst componentparent;

        public float levelofdetailmul = 1.0f;

        float levellimitroofy = 5.0f;
        float levellimitfloory = 0.0f;
        float staticPlaneSize;
        float alternateStaticPlaneSize;

        float levelgenmapsplanesize;


        public void buildchunkmaps(int swtc,int typeofterraintiles_, tutorialcubeaschunkinst componentparent_, int levelofdetail_)// , int somechunkkeyboardpriminstanceindex_, int chunkprimindex_, int chunkinstindex_
        {

            typeofterraintiles = typeofterraintiles_;


            componentparent = componentparent_;


            levelofdetail = levelofdetail_;
            //chunkz.Add(this, _chunkPos);


            levellimitfloory = 0;

            /*if (levelofdetail == 1)
            {
                width = componentparent.somelevelgenprimglobals.widthlod0;
                height = componentparent.somelevelgenprimglobals.heightlod0;
                depth = componentparent.somelevelgenprimglobals.depthlod0;

                //width = 10;
                //height = 10;
                //depth = 10;
            }
            else if (levelofdetail == 2)
            {
                width = componentparent.somelevelgenprimglobals.widthlod1;
                height = componentparent.somelevelgenprimglobals.heightlod1;
                depth = componentparent.somelevelgenprimglobals.depthlod1;

                //width = 5;
                //height = 5;
                //depth = 5;
            }

            else if (levelofdetail == 3)
            {
                width = componentparent.somelevelgenprimglobals.widthlod2;
                height = componentparent.somelevelgenprimglobals.heightlod2;
                depth = componentparent.somelevelgenprimglobals.depthlod2;
                //width = 3;
                //height = 3;
                //depth = 3;
            }
            else if (levelofdetail == 4)
            {
                width = componentparent.somelevelgenprimglobals.widthlod3;
                height = componentparent.somelevelgenprimglobals.heightlod3;
                depth = componentparent.somelevelgenprimglobals.depthlod3;

                //width = 2;
                //height = 2;
                //depth = 2;
            }
            else if (levelofdetail == 5)
            {
                width = componentparent.somelevelgenprimglobals.widthlod4;
                height = componentparent.somelevelgenprimglobals.heightlod4;
                depth = componentparent.somelevelgenprimglobals.depthlod4;

                //width = 2;
                //height = 2;
                //depth = 2;
            }*/


            width = 8;
            height = 8;
            depth = 8;






            /*

            if (levelofdetail == 1)
            {
                width = 10;
                height = 10;
                depth = 10;
            }
            else if (levelofdetail == 2)
            {
                width = 5;
                height = 5;
                depth = 5;
            }

            else if (levelofdetail == 3)
            {
                width = 3;
                height = 3;
                depth = 3;
            }
            else if (levelofdetail == 4)
            {
                width = 2;
                height = 2;
                depth = 2;
            }

            */

            /*
            if (levelofdetail == 1)
            {
                width = 6;
                height = 6;
                depth = 6;
            }
            else if (levelofdetail == 2)
            {
                width = 3;
                height = 3;
                depth = 3;
            }

            else if (levelofdetail == 3)
            {
                width = 2;
                height = 2;
                depth = 2;
            }
            else if (levelofdetail == 4)
            {
                width = 1;
                height = 1;
                depth = 1;
            }*/

            /*
            if (levelofdetail == 1)
            {
                width = 20;
                height = 20;
                depth = 20;
            }
            else if (levelofdetail == 2)
            {
                width = 10;
                height = 10;
                depth = 10;
            }

            else if (levelofdetail == 3)
            {
                width = 6;
                height = 6;
                depth = 6;
            }
            else if (levelofdetail == 4)
            {
                width = 5;
                height = 5;
                depth = 5;
            }*/




            /*
            width = width_ / levelofdetail_;
            height = height_ / levelofdetail_;
            depth = depth_ / levelofdetail_;*/



            //chunkPos = _chunkPos;//

            /*
            chunkPos.X *= 2f;
            chunkPos.Y *= 2f;
            chunkPos.Z *= 2f;*/










            //xChunkPos = _chunkPos.X;
            //yChunkPos = _chunkPos.Y;
            //zChunkPos = _chunkPos.Z;


            //floorHeight = height;



            float standardwidth = 10.0f;
            float currentratio = standardwidth / width;

            ////Console.WriteLine(currentratio);
            levelgenmapsplanesize = 0.1f * currentratio; //0.05f when 20w20h20d



            staticPlaneSize = levelgenmapsplanesize;

            if (staticPlaneSize == 1)
            {
                staticPlaneSize = levelgenmapsplanesize * 0.1f;
                alternateStaticPlaneSize = levelgenmapsplanesize * 0.1f;
            }
            else if (staticPlaneSize == 0.1f)
            {
                staticPlaneSize = levelgenmapsplanesize;
                alternateStaticPlaneSize = levelgenmapsplanesize * 10;
            }
            else if (staticPlaneSize == 0.01f)
            {
                staticPlaneSize = levelgenmapsplanesize;
                alternateStaticPlaneSize = levelgenmapsplanesize * 1000;
            }
            else if (staticPlaneSize == 0.2f)
            {
                staticPlaneSize = levelgenmapsplanesize;
                alternateStaticPlaneSize = levelgenmapsplanesize * 10;
            }
            else if (staticPlaneSize == 0.02f)
            {
                staticPlaneSize = levelgenmapsplanesize;
                alternateStaticPlaneSize = levelgenmapsplanesize * 1000;
            }







            // new Vector3(currentPosition.X, currentPosition.Y, currentPosition.Z);


            //planeSize = planeSize;
            //realplanetwidth = 4;
            //width = width;
            //height = height;
            //depth = depth;









            //componentparent = componentparent_;
            //addfracturedcubeonimpact = addfracturedcubeonimpact_;
            //UnityTutorialGameObjectPool = UnityTutorialGameObjectPool_;

            // this.GameObject.position;

            /*
            this.gameObject.tag = "collisionObject";
            this.gameObject.layer = 8; //"collisionObject"
            UnityTutorialGameObjectPool = this.GameObject.GetComponent<NewObjectPoolerScript>();

            parentObject = this.GameObject.parent;
            //componentparent = parentObject.gameObject.GetComponent<sccsproceduralplanetbuilderGen2>().currentplanetbuilder;

            mesh = new Mesh();
            this.gameObject.GetComponent<MeshFilter>().mesh = mesh;
            this.gameObject.GetComponent<MeshFilter>().sharedMesh = mesh;
            */




            ////Console.WriteLine("w" + width + "/h:" + height + "/d:" + depth);



            map = new int[width * height * depth];
            
            
            /*mapvertindexfordims = new int[width * height * depth];
            widthdimtop = new int[width * height * depth];
            heightdimtop = new int[width * height * depth];
            depthdimtop = new int[width * height * depth];
            mapfirstvertxtop = new int[width * height * depth];
            mapfirstvertytop = new int[width * height * depth];
            mapfirstvertztop = new int[width * height * depth];*/


            mapvertindexfordims = new int[4 * 4 * 4];

            widthdimtop = new int[4 * 4 * 4];
            heightdimtop = new int[4 * 4 * 4];
            depthdimtop = new int[4 * 4 * 4];
            mapfirstvertxtop = new int[4 * 4 * 4];
            mapfirstvertytop = new int[4 * 4 * 4];
            mapfirstvertztop = new int[4 * 4 * 4];



            /*
            widthdimleft = new int[width * height * depth];
            heightdimleft = new int[width * height * depth];
            depthdimleft = new int[width * height * depth];
            mapfirstvertxleft = new int[width * height * depth];
            mapfirstvertyleft = new int[width * height * depth];
            mapfirstvertzleft = new int[width * height * depth];

            widthdimright = new int[width * height * depth];
            heightdimright = new int[width * height * depth];
            depthdimright = new int[width * height * depth];
            mapfirstvertxright = new int[width * height * depth];
            mapfirstvertyright = new int[width * height * depth];
            mapfirstvertzright = new int[width * height * depth];

            widthdimfront = new int[width * height * depth];
            heightdimfront = new int[width * height * depth];
            depthdimfront = new int[width * height * depth];
            mapfirstvertxfront = new int[width * height * depth];
            mapfirstvertyfront = new int[width * height * depth];
            mapfirstvertzfront = new int[width * height * depth];

            widthdimback = new int[width * height * depth];
            heightdimback = new int[width * height * depth];
            depthdimback = new int[width * height * depth];
            mapfirstvertxback = new int[width * height * depth];
            mapfirstvertyback = new int[width * height * depth];
            mapfirstvertzback = new int[width * height * depth];

            widthdimbottom = new int[width * height * depth];
            heightdimbottom = new int[width * height * depth];
            depthdimbottom = new int[width * height * depth];
            mapfirstvertxbottom = new int[width * height * depth];
            mapfirstvertybottom = new int[width * height * depth];
            mapfirstvertzbottom = new int[width * height * depth];*/









            //normalslist = new List<Vector3>();
            //colorslist = new List<Vector4>();
            //indexPoslist = new List<Vector4>();
            //textureslist = new List<Vector2>();


            if (swtc == 0)
            {


                //STALAGMITE CAVE SYSTEM VALUES FOR CEILING 
                //DETAILSCALE = 10 // 20 // 75.0fgood
                //HEIGHTSCALE = 1.25f //1.25f //4.55fgood

                float _detailScaleceiling = 200.0f;//200
                float _heightScaleceiling = 2.0f;//5

                float _detailScale = 200;//200
                float _heightScale = 5;//5

                float somenoisevalue = 10.0f;
                float someothernoisevalue = 20.0f;


                //realplanetwidth = planeSize * width;

                //map = new int[width * height * depth];

                float somenoiseval0 = 200; //200
                float somenoiseval1 = 5; //5

                var seed0 = 3420;


                if (typeofterraintiles == 0)
                {
                    for (int x = 0; x < width; x++)
                    {
                        for (int y = 0; y < height; y++)
                        {
                            for (int z = 0; z < depth; z++)
                            {

                                //map[x + width * (y + height * z)] = 1;
                                //map[x + width * (y + height * z)] = 1;


                                /*float noiseXZ = 20;

                                noiseXZ *= fastNoise.GetNoise((((x * staticPlaneSize) + (currentPosition.X * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((y * staticPlaneSize) + (currentPosition.Y * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((z * staticPlaneSize) + (currentPosition.Z * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale);
                                //Console.WriteLine(noiseXZ);
                                if (noiseXZ >= 0.2f)
                                {
                                    map[x + width * (y + height * z)] = 1;
                                }
                                else if (y == 0 && currentPosition.Y == 0)
                                {
                                    map[x + width * (y + height * z)] = 1;
                                }
                                else
                                {
                                    map[x + width * (y + height * z)] = 0;
                                }*/

                                float noiseXZ = somenoisevalue;
                                //noiseXZ *= OriginalSimplexNoise.Noise((((x * levelgenmapsplanesize) + chunkPos[0] + seed) / _detailScale) * _heightScale, (((z* levelgenmapsplanesize) + chunkPos[2] + seed) / _detailScale) * _heightScale);
                                noiseXZ *= OriginalSimplexNoise.SeamlessNoise((((x * levelgenmapsplanesize) + chunkPos[0] + seed) / _detailScale) * _heightScale, (((z * levelgenmapsplanesize) + chunkPos[2] + seed) / _detailScale) * _heightScale, 15, 15, 0);


                                if (chunkPos[1] == sccslevelgen.wallheightsize - 1)
                                {
                                    ////Console.WriteLine("noiseXZ" + noiseXZ);
                                    if ((int)Math.Round(noiseXZ) >= y) //|| (int)Math.Round(noiseXZ) < -y
                                    {
                                        map[x + width * ((height - 1 - y) + height * z)] = 1;
                                    }
                                    /*else if (y == height - 1)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }
                                    else
                                    {
                                        map[x + width * ((height - 1 - y) + height * z)] = 0;
                                    }*/
                                }

                                float size0 = (1 / levelgenmapsplanesize) * chunkPos[1];
                                noiseXZ -= size0;
                                //noise = (noise + 1.0f) * 0.5f;
                                //float noiser1 = OriginalSimplexNoise.Noise(x, y);

                                //float size0 = (1 / levelgenmapsplanesize) * chunkPos[1];
                                //noise -= size0;
                                ////Console.WriteLine(noiseXZ + " y: " + y);

                                if ((int)Math.Round(noiseXZ) >= y) //|| (int)Math.Round(noiseXZ) < -y
                                {
                                    map[x + width * (y + height * z)] = 1;
                                }

                                if (y == 0 && chunkPos[1] == 0)
                                {
                                    map[x + width * (y + height * z)] = 1;
                                }
                                if (y == height - 1 && chunkPos[1] == sccslevelgen.wallheightsize - 1)
                                {
                                    map[x + width * (y + height * z)] = 1;
                                }

                                /*else
                                {
                                    map[x + width * (y + height * z)] = 0;
                                }*/

                                //map[x + width * (y + height * z)] = 1;
                            }
                        }
                    }
                }



                if (typeofterraintiles != 1115 && typeofterraintiles != 0 && chunkPos[1] != 0)
                {
                    for (int x = 0; x < width; x++)
                    {
                        for (int y = 0; y < height; y++)
                        {
                            for (int z = 0; z < depth; z++)
                            {

                                //map[x + width * (y + height * z)] = 1;
                                //map[x + width * (y + height * z)] = 1;


                                /*float noiseXZ = 20;

                                noiseXZ *= fastNoise.GetNoise((((x * staticPlaneSize) + (currentPosition.X * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((y * staticPlaneSize) + (currentPosition.Y * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((z * staticPlaneSize) + (currentPosition.Z * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale);
                                //Console.WriteLine(noiseXZ);
                                if (noiseXZ >= 0.2f)
                                {
                                    map[x + width * (y + height * z)] = 1;
                                }
                                else if (y == 0 && currentPosition.Y == 0)
                                {
                                    map[x + width * (y + height * z)] = 1;
                                }
                                else
                                {
                                    map[x + width * (y + height * z)] = 0;
                                }*/

                                float noiseXZ = somenoisevalue;
                                //noiseXZ *= OriginalSimplexNoise.Noise((((x * levelgenmapsplanesize) + chunkPos[0] + seed) / _detailScale) * _heightScale, (((z* levelgenmapsplanesize) + chunkPos[2] + seed) / _detailScale) * _heightScale);
                                noiseXZ *= OriginalSimplexNoise.SeamlessNoise((((x * levelgenmapsplanesize) + chunkPos[0] + seed) / _detailScaleceiling) * _heightScaleceiling, (((z * levelgenmapsplanesize) + chunkPos[2] + seed) / _detailScaleceiling) * _heightScaleceiling, 15, 15, 0);


                                if (chunkPos[1] == sccslevelgen.wallheightsize - 1)
                                {
                                    ////Console.WriteLine("noiseXZ" + noiseXZ);
                                    if ((int)Math.Round(noiseXZ) >= y) //|| (int)Math.Round(noiseXZ) < -y
                                    {
                                        map[x + width * ((height - 1 - y) + height * z)] = 1;
                                    }
                                    /*else if (y == height - 1)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }
                                    else
                                    {
                                        map[x + width * ((height - 1 - y) + height * z)] = 0;
                                    }*/
                                }

                                float size0 = (1 / levelgenmapsplanesize) * chunkPos[1];
                                noiseXZ -= size0;
                                //noise = (noise + 1.0f) * 0.5f;
                                //float noiser1 = OriginalSimplexNoise.Noise(x, y);

                                //float size0 = (1 / levelgenmapsplanesize) * chunkPos[1];
                                //noise -= size0;
                                ////Console.WriteLine(noiseXZ + " y: " + y);

                                if ((int)Math.Round(noiseXZ) >= y) //|| (int)Math.Round(noiseXZ) < -y
                                {
                                    map[x + width * (y + height * z)] = 1;
                                }

                                if (y == 0 && chunkPos[1] == 0)
                                {
                                    map[x + width * (y + height * z)] = 1;
                                }
                                if (y == height - 1 && chunkPos[1] == sccslevelgen.wallheightsize - 1)
                                {
                                    map[x + width * (y + height * z)] = 1;
                                }

                                /*else
                                {
                                    map[x + width * (y + height * z)] = 0;
                                }*/

                                //map[x + width * (y + height * z)] = 1;
                            }
                        }
                    }
                }


                if (typeofterraintiles != 1115 && typeofterraintiles != 0 && chunkPos[1] == 0)
                {
                    for (int x = 0; x < width; x++)
                    {
                        for (int y = 0; y < height; y++)
                        {
                            for (int z = 0; z < depth; z++)
                            {

                                //map[x + width * (y + height * z)] = 1;
                                //map[x + width * (y + height * z)] = 1;


                                /*float noiseXZ = 20;

                                noiseXZ *= fastNoise.GetNoise((((x * staticPlaneSize) + (currentPosition.X * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((y * staticPlaneSize) + (currentPosition.Y * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((z * staticPlaneSize) + (currentPosition.Z * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale);
                                //Console.WriteLine(noiseXZ);
                                if (noiseXZ >= 0.2f)
                                {
                                    map[x + width * (y + height * z)] = 1;
                                }
                                else if (y == 0 && currentPosition.Y == 0)
                                {
                                    map[x + width * (y + height * z)] = 1;
                                }
                                else
                                {
                                    map[x + width * (y + height * z)] = 0;
                                }*/

                                float noiseXZ = somenoisevalue;
                                //noiseXZ *= OriginalSimplexNoise.Noise((((x * levelgenmapsplanesize) + chunkPos[0] + seed) / _detailScale) * _heightScale, (((z* levelgenmapsplanesize) + chunkPos[2] + seed) / _detailScale) * _heightScale);
                                noiseXZ *= OriginalSimplexNoise.SeamlessNoise((((x * levelgenmapsplanesize) + chunkPos[0] + seed) / _detailScale) * _heightScale, (((z * levelgenmapsplanesize) + chunkPos[2] + seed) / _detailScale) * _heightScale, 15, 15, 0);


                                if (chunkPos[1] == sccslevelgen.wallheightsize - 1)
                                {
                                    ////Console.WriteLine("noiseXZ" + noiseXZ);
                                    if ((int)Math.Round(noiseXZ) >= y) //|| (int)Math.Round(noiseXZ) < -y
                                    {
                                        map[x + width * ((height - 1 - y) + height * z)] = 1;
                                    }
                                    /*else if (y == height - 1)
                                    {
                                        map[x + width * (y + height * z)] = 1;
                                    }
                                    else
                                    {
                                        map[x + width * ((height - 1 - y) + height * z)] = 0;
                                    }*/
                                }

                                float size0 = (1 / levelgenmapsplanesize) * chunkPos[1];
                                noiseXZ -= size0;
                                //noise = (noise + 1.0f) * 0.5f;
                                //float noiser1 = OriginalSimplexNoise.Noise(x, y);

                                //float size0 = (1 / levelgenmapsplanesize) * chunkPos[1];
                                //noise -= size0;
                                ////Console.WriteLine(noiseXZ + " y: " + y);

                                if ((int)Math.Round(noiseXZ) >= y) //|| (int)Math.Round(noiseXZ) < -y
                                {
                                    map[x + width * (y + height * z)] = 1;
                                }

                                if (y == 0 && chunkPos[1] == 0)
                                {
                                    map[x + width * (y + height * z)] = 1;
                                }
                                if (y == height - 1 && chunkPos[1] == sccslevelgen.wallheightsize - 1)
                                {
                                    map[x + width * (y + height * z)] = 1;
                                }

                                /*else
                                {
                                    map[x + width * (y + height * z)] = 0;
                                }*/

                                //map[x + width * (y + height * z)] = 1;
                            }
                        }
                    }
                }

                if (typeofterraintiles == 1115)
                {
                    int[] fakepos = chunkPos;
                    fakepos[1] = 0;

                    for (int x = 0; x < width; x++)
                    {
                        for (int y = 0; y < height; y++)
                        {
                            for (int z = 0; z < depth; z++)
                            {
                                float noiseXZ = somenoisevalue;
                                //noiseXZ *= OriginalSimplexNoise.Noise((((x * levelgenmapsplanesize) + fakepos.X + seed) / _detailScale) * _heightScale, (((z* levelgenmapsplanesize) + fakepos.Z + seed) / _detailScale) * _heightScale);
                                noiseXZ *= OriginalSimplexNoise.SeamlessNoise((((x * levelgenmapsplanesize) + fakepos[0] + seed) / _detailScaleceiling) * _heightScaleceiling, (((z * levelgenmapsplanesize) + fakepos[2] + seed) / _detailScaleceiling) * _heightScaleceiling, 15, 15, 0);

                                float size0 = (1 / levelgenmapsplanesize) * fakepos[1];
                                noiseXZ -= size0;
                                //noise = (noise + 1.0f) * 0.5f;
                                //float noiser1 = OriginalSimplexNoise.Noise(x, y);

                                //float size0 = (1 / levelgenmapsplanesize) * fakepos.Y;
                                //noise -= size0;
                                ////Console.WriteLine(noiseXZ + " y: " + y);

                                if ((int)Math.Round(noiseXZ) >= y) //|| (int)Math.Round(noiseXZ) < -y
                                {
                                    map[x + width * ((height - 1 - y) + height * z)] = 1;
                                }
                                else if (y == height - 1 && fakepos[1] == 0)
                                {
                                    map[x + width * (y + height * z)] = 1;
                                }

                                else
                                {
                                    map[x + width * ((height - 1 - y) + height * z)] = 0;
                                }
                            }
                        }
                    }
                }










                if (typeofterraintiles == -99) //typeofterraintiles == -2
                {
                    for (int x = 0; x < width; x++)
                    {
                        for (int y = 0; y < height; y++)
                        {
                            for (int z = 0; z < depth; z++)
                            {
                                //map[x + width * (y + height * z)] = 1;

                                /*float noiseXZ = somenoisevalue;
                                //noiseXZ *= OriginalSimplexNoise.Noise((((x * levelgenmapsplanesize) + chunkPos[0] + seed) / _detailScale) * _heightScale, (((z* levelgenmapsplanesize) + chunkPos[2] + seed) / _detailScale) * _heightScale);
                                noiseXZ *= OriginalSimplexNoise.SeamlessNoise((((x * levelgenmapsplanesize) + chunkPos[0] + seed) / _detailScale) * _heightScale, (((z* levelgenmapsplanesize) + chunkPos[2] + seed) / _detailScale) * _heightScale, 15, 15, 0);

                                float size0 = (1 / levelgenmapsplanesize) * chunkPos[1];
                                noiseXZ -= size0;
                                //noise = (noise + 1.0f) * 0.5f;
                                //float noiser1 = OriginalSimplexNoise.Noise(x, y);

                                //float size0 = (1 / levelgenmapsplanesize) * chunkPos[1];
                                //noise -= size0;
                                ////Console.WriteLine(noiseXZ + " y: " + y);

                                if ((int)Math.Round(noiseXZ) >= y) //|| (int)Math.Round(noiseXZ) < -y
                                {
                                    map[x + width * (y + height * z)] = 1;
                                }
                                else if (y == 0 && chunkPos[1] == 0)
                                {
                                    map[x + width * (y + height * z)] = 1;
                                }
                                else
                                {
                                    map[x + width * (y + height * z)] = 0;
                                }*/
                                /* if (y < width /1.15f)
                                 {
                                     map[x + width * (y + height * z)] = 1;
                                 }*/
                                map[x + width * (y + height * z)] = 1;
                            }
                        }
                    }
                }














                if (typeofterraintiles == -99) ////typeofterraintiles == -3
                {
                    for (int x = 0; x < width; x++)
                    {
                        for (int y = 0; y < height; y++)
                        {
                            for (int z = 0; z < depth; z++)
                            {
                                //map[x + width * (y + height * z)] = 1;

                                /*float noiseXZ = somenoisevalue;
                                //noiseXZ *= OriginalSimplexNoise.Noise((((x * levelgenmapsplanesize) + chunkPos[0] + seed) / _detailScale) * _heightScale, (((z* levelgenmapsplanesize) + chunkPos[2] + seed) / _detailScale) * _heightScale);
                                noiseXZ *= OriginalSimplexNoise.SeamlessNoise((((x * levelgenmapsplanesize) + chunkPos[0] + seed) / _detailScale) * _heightScale, (((z* levelgenmapsplanesize) + chunkPos[2] + seed) / _detailScale) * _heightScale, 15, 15, 0);

                                float size0 = (1 / levelgenmapsplanesize) * chunkPos[1];
                                noiseXZ -= size0;
                                //noise = (noise + 1.0f) * 0.5f;
                                //float noiser1 = OriginalSimplexNoise.Noise(x, y);

                                //float size0 = (1 / levelgenmapsplanesize) * chunkPos[1];
                                //noise -= size0;
                                ////Console.WriteLine(noiseXZ + " y: " + y);

                                if ((int)Math.Round(noiseXZ) >= y) //|| (int)Math.Round(noiseXZ) < -y
                                {
                                    map[x + width * (y + height * z)] = 1;
                                }
                                else if (y == 0 && chunkPos[1] == 0)
                                {
                                    map[x + width * (y + height * z)] = 1;
                                }
                                else
                                {
                                    map[x + width * (y + height * z)] = 0;
                                }*/
                                if (y < width / 1.05f)
                                {
                                    map[x + width * (y + height * z)] = 1;
                                }

                            }
                        }
                    }
                }



                //TERRAIN INSIDE TILES. DOES NOT INCLUDE THE WALLS BOTTOM FLOOR THAT IS PART OF WALLS.
                //TERRAIN INSIDE TILES. DOES NOT INCLUDE THE WALLS BOTTOM FLOOR THAT IS PART OF WALLS.
                /*if (typeofterraintiles == 0)
                {
                    for (int x = 0; x < width; x++)
                    {
                        for (int y = 0; y < height; y++)
                        {
                            for (int z = 0; z < depth; z++)
                            {
                                //map[x + width * (y + height * z)] = 1;


                                /*float noiseXZ = 20;

                                noiseXZ *= fastNoise.GetNoise((((x * staticPlaneSize) + (chunkPos[0] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((y * staticPlaneSize) + (chunkPos[1] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((z * staticPlaneSize) + (chunkPos[2] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale);
                                //Console.WriteLine(noiseXZ);
                                if (noiseXZ >= 0.2f)
                                {
                                    map[x + width * (y + height * z)] = 1;
                                }
                                else if (y == 0 && chunkPos[1] == 0)
                                {
                                    map[x + width * (y + height * z)] = 1;
                                }
                                else
                                {
                                    map[x + width * (y + height * z)] = 0;
                                }

                                float noiseXZ = somenoisevalue;
                                //noiseXZ *= OriginalSimplexNoise.Noise((((x * levelgenmapsplanesize) + chunkPos[0] + seed) / _detailScale) * _heightScale, (((z* levelgenmapsplanesize) + chunkPos[2] + seed) / _detailScale) * _heightScale);
                                noiseXZ *= OriginalSimplexNoise.SeamlessNoise((((x * levelgenmapsplanesize) + chunkPos[0] + seed) / _detailScale) * _heightScale, (((z * levelgenmapsplanesize) + chunkPos[2] + seed) / _detailScale) * _heightScale, 15, 15, 0);

                                float size0 = (1 / levelgenmapsplanesize) * chunkPos[1];
                                noiseXZ -= size0;
                                //noise = (noise + 1.0f) * 0.5f;
                                //float noiser1 = OriginalSimplexNoise.Noise(x, y);

                                //float size0 = (1 / levelgenmapsplanesize) * chunkPos[1];
                                //noise -= size0;
                                ////Console.WriteLine(noiseXZ + " y: " + y);

                                if ((int)Math.Round(noiseXZ) >= y) //|| (int)Math.Round(noiseXZ) < -y
                                {
                                    map[x + width * (y + height * z)] = 1;
                                }
                                else if (y == 0 && chunkPos[1] == 0)
                                {
                                    map[x + width * (y + height * z)] = 1;
                                }
                                else
                                {
                                    map[x + width * (y + height * z)] = 0;
                                }
                                //map[x + width * (y + height * z)] = 1;
                            }
                        }
                    }
                }*/
                //TERRAIN INSIDE TILES. DOES NOT INCLUDE THE WALLS BOTTOM FLOOR THAT IS PART OF WALLS.
                //TERRAIN INSIDE TILES. DOES NOT INCLUDE THE WALLS BOTTOM FLOOR THAT IS PART OF WALLS.








                //LEFT WALL
                if (typeofterraintiles == 1101)
                {
                    //for (int j = 0; j < levelgen.leftWall.Count; j++)
                    {
                        //if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == levelgen.leftWall[j])
                        {
                            for (int x = 0; x < width; x++)
                            {
                                //float noiseX = Math.Abs((float)(x * levelgenmapsplanesize + chunkPos[0] + seed0) / somenoiseval0);
                                float noiseX2 = Math.Abs((float)(x * levelgenmapsplanesize + chunkPos[0] + seed0) / somenoiseval1);
                                for (int y = 0; y < height; y++)
                                {
                                    //float noiseY = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) / somenoiseval0);
                                    float noiseY2 = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) / somenoiseval1);
                                    for (int z = 0; z < width; z++)
                                    {
                                        //float noiseZ = Math.Abs((float)(z * levelgenmapsplanesize + chunkPos[2] + seed0) / somenoiseval0);
                                        float noiseZ2 = Math.Abs((float)(z * levelgenmapsplanesize + chunkPos[2] + seed0) / somenoiseval1);

                                        //float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);
                                        float noiseValue = someothernoisevalue;
                                        noiseValue *= fastNoise.GetNoise((((x * staticPlaneSize) + (chunkPos[0] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((y * staticPlaneSize) + (chunkPos[1] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((z * staticPlaneSize) + (chunkPos[2] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale);

                                        /*if ((int)Math.Round(noiseValue) >= y) //|| (int)Math.Round(noiseXZ) < -y
                                        {
                                            map[x + width * (y + height * z)] = 1;
                                        }
                                        else if (y == 0 && chunkPos[1] == 0)
                                        {
                                            map[x + width * (y + height * z)] = 1;
                                        }
                                        else
                                        {
                                            map[x + width * (y + height * z)] = 0;
                                        }*/
                                        //noiseValue += (10 - (float)y) / somenoisevalue;
                                        //noiseValue /= (float)y / 5;

                                        if (noiseValue > 0.2f && y < sccslevelgen.wallheightsize - 1)
                                        {
                                            map[x + width * (y + height * z)] = 1;
                                        }

                                        float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);

                                        float noiseValue1i = noiseValue2;

                                        noiseValue1i += (5 - (float)x) / 5;
                                        noiseValue1i /= (float)x / 5;


                                        if (noiseValue1i > 0.2f)
                                        {
                                            map[x + width * (y + height * z)] = 1;
                                            //leftExtremity[x + width * (y + height * z)] = map[x + width * (y + height * z)];
                                        }

                                        if (x == 0)
                                        {
                                            map[x + width * (y + height * z)] = 1;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                //LEFT WALL



                //RIGHT WALL
                if (typeofterraintiles == 1102)
                {
                    // for (int j = 0; j < levelgen.rightWall.Count; j++)
                    {
                        //if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == levelgen.rightWall[j])
                        {
                            for (int x = 0; x < width; x++)
                            {
                                //float noiseX = Math.Abs((float)(x * levelgenmapsplanesize + chunkPos[0] + seed0) /somenoiseval0);
                                float noiseX2 = Math.Abs((float)(x * levelgenmapsplanesize + chunkPos[0] + seed0) / somenoiseval1);
                                for (int y = 0; y < height; y++)
                                {
                                    //float noiseY = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) /somenoiseval0);
                                    float noiseY2 = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) / somenoiseval1);
                                    for (int z = 0; z < depth; z++)
                                    {
                                        //float noiseZ = Math.Abs((float)(z * levelgenmapsplanesize + chunkPos[2] + seed0) /somenoiseval0);
                                        float noiseZ2 = Math.Abs((float)(z * levelgenmapsplanesize + chunkPos[2] + seed0) / somenoiseval1);

                                        //float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);
                                        float noiseValue = someothernoisevalue;
                                        noiseValue *= fastNoise.GetNoise((((x * staticPlaneSize) + (chunkPos[0] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((y * staticPlaneSize) + (chunkPos[1] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((z * staticPlaneSize) + (chunkPos[2] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale);

                                        float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);

                                        //noiseValue += (10 - (float)y) / somenoisevalue;
                                        //noiseValue /= (float)y / 5;

                                        /*if ((int)Math.Round(noiseValue) >= y) //|| (int)Math.Round(noiseXZ) < -y
                                        {
                                            map[x + width * (y + height * z)] = 1;
                                        }
                                        else if (y == 0 && chunkPos[1] == 0)
                                        {
                                            map[x + width * (y + height * z)] = 1;
                                        }
                                        else
                                        {
                                            map[x + width * (y + height * z)] = 0;
                                        }*/
                                        if (noiseValue > 0.2f && y < sccslevelgen.wallheightsize - 1)
                                        {
                                            map[x + width * (y + height * z)] = 1;
                                        }

                                        float noiseValue3i = noiseValue2;

                                        noiseValue3i += (5 - (float)x) / 5;
                                        noiseValue3i /= (float)x / 5;

                                        if (noiseValue3i > 0.2f)
                                        {
                                            map[(width - 1 - x) + width * (y + height * z)] = 1;
                                            //rightExtremity[x + width * (y + height * z)] = map[x + width * (y + height * z)];
                                        }
                                        /*
                                        if (x == width-1)
                                        {
                                            map[x + width * (y + height * z)] = 1;
                                        }*/
                                    }
                                }
                            }
                        }
                    }
                }
                //RIGHT WALL




                /////////////////////////////////////FRONT WALL/////////////////////////////////
                if (typeofterraintiles == 1103)
                {
                    //for (int j = 0; j < levelgen.frontWall.Count; j++)
                    {
                        //if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == levelgen.frontWall[j])
                        {
                            for (int x = 0; x < width; x++)
                            {
                                //float noiseX = Math.Abs((float)(x * levelgenmapsplanesize + chunkPos[0] + seed0) /somenoiseval0);
                                float noiseX5 = Math.Abs((float)(x * levelgenmapsplanesize + chunkPos[0] + seed0) / somenoiseval1);
                                for (int y = 0; y < height; y++)
                                {
                                    //float noiseY = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) /somenoiseval0);6
                                    float noiseY5 = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) / somenoiseval1);
                                    for (int z = 0; z < width; z++)
                                    {
                                        //float noiseZ = Math.Abs((float)(z * levelgenmapsplanesize + chunkPos[2] + seed0) /somenoiseval0);
                                        float noiseZ5 = Math.Abs((float)(z * levelgenmapsplanesize + chunkPos[2] + seed0) / somenoiseval1);

                                        //float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);

                                        float noiseValue5 = Noise.Generate(noiseX5, noiseZ5, noiseY5);

                                        //noiseValue += (10 - (float)y) / somenoisevalue;
                                        //noiseValue /= (float)y / 5;
                                        float noiseValue = someothernoisevalue;
                                        noiseValue *= fastNoise.GetNoise((((x * staticPlaneSize) + (chunkPos[0] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((y * staticPlaneSize) + (chunkPos[1] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((z * staticPlaneSize) + (chunkPos[2] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale);

                                        /*if ((int)Math.Round(noiseValue) >= y) //|| (int)Math.Round(noiseXZ) < -y
                                        {
                                            map[x + width * (y + height * z)] = 1;
                                        }
                                        else if (y == 0 && chunkPos[1] == 0)
                                        {
                                            map[x + width * (y + height * z)] = 1;
                                        }
                                        else
                                        {
                                            map[x + width * (y + height * z)] = 0;
                                        }*/

                                        if (noiseValue > 0.2f && y < sccslevelgen.wallheightsize - 1)
                                        {
                                            map[x + width * (y + height * z)] = 1;
                                        }

                                        float noiseValue6i = noiseValue5;

                                        noiseValue6i += (5 - (float)z) / 5;
                                        noiseValue6i /= (float)z / 5;

                                        if (noiseValue6i > 0.2f)
                                        {
                                            map[x + width * (y + height * z)] = 1;
                                            //frontExtremity[x + width * (y + height * z)] = map[x + width * (y + height * z)];
                                        }

                                        if (z == 0)
                                        {
                                            map[x + width * (y + height * z)] = 1;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }





                /////////////////////////////////////BACK WALL////////////////////////////////
                if (typeofterraintiles == 1104)
                {
                    //for (int j = 0; j < levelgen.backWall.Count; j++)
                    {
                        //if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == levelgen.backWall[j])
                        {
                            for (int x = 0; x < width; x++)
                            {
                                //float noiseX = Math.Abs((float)(x * levelgenmapsplanesize + chunkPos[0] + seed0) /somenoiseval0);
                                float noiseX5 = Math.Abs((float)(x * levelgenmapsplanesize + chunkPos[0] + seed0) / somenoiseval1);
                                for (int y = 0; y < height; y++)
                                {
                                    //float noiseY = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) /somenoiseval0);
                                    float noiseY5 = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) / somenoiseval1);
                                    for (int z = 0; z < width; z++)
                                    {
                                        //float noiseZ = Math.Abs((float)(z * levelgenmapsplanesize + chunkPos[2] + seed0) /somenoiseval0);
                                        float noiseZ5 = Math.Abs((float)(z * levelgenmapsplanesize + chunkPos[2] + seed0) / somenoiseval1);

                                        //float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);
                                        float noiseValue = someothernoisevalue;
                                        noiseValue *= fastNoise.GetNoise((((x * staticPlaneSize) + (chunkPos[0] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((y * staticPlaneSize) + (chunkPos[1] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((z * staticPlaneSize) + (chunkPos[2] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale);

                                        float noiseValue5 = Noise.Generate(noiseX5, noiseZ5, noiseY5);

                                        /*if ((int)Math.Round(noiseValue) >= y) //|| (int)Math.Round(noiseXZ) < -y
                                        {
                                            map[x + width * (y + height * z)] = 1;
                                        }
                                        else if (y == 0 && chunkPos[1] == 0)
                                        {
                                            map[x + width * (y + height * z)] = 1;
                                        }
                                        else
                                        {
                                            map[x + width * (y + height * z)] = 0;
                                        }*/
                                        //noiseValue += (10 - (float)y) / somenoisevalue;
                                        //noiseValue /= (float)y / 5;

                                        if (noiseValue > 0.2f && y < sccslevelgen.wallheightsize - 1)
                                        {
                                            map[x + width * (y + height * z)] = 1;
                                        }

                                        float noiseValue4i = noiseValue5;

                                        noiseValue4i += (5 - (float)z) / 5;
                                        noiseValue4i /= (float)z / 5;


                                        if (noiseValue4i > 0.2f)
                                        {
                                            map[x + width * (y + height * (depth - 1 - z))] = 1;
                                            //backExtremity[x + width * (y + height * z)] = map[x + width * (y + height * z)];
                                        }

                                        if (z == depth - 1)
                                        {
                                            map[x + width * (y + height * z)] = 1;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }




                /////////////////////////////////////LEFT BACK INSIDE CORNER////////////////////////////////
                if (typeofterraintiles == 1105)
                {
                    //for (int j = 0; j < levelgen.builtLeftFrontInsideCorner.Count; j++)
                    {
                        //if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == levelgen.builtLeftFrontInsideCorner[j])
                        {
                            for (int x = 0; x < width; x++)
                            {
                                //float noiseX = Math.Abs((float)(x * levelgenmapsplanesize + chunkPos[0] + seed0) /somenoiseval0);
                                float noiseX2 = Math.Abs((float)(x * levelgenmapsplanesize + chunkPos[0] + seed0) / somenoiseval1);
                                float noiseX5 = Math.Abs((float)(x * levelgenmapsplanesize + chunkPos[0] + seed0) / somenoiseval1);
                                for (int y = 0; y < height; y++)
                                {
                                    //float noiseY = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) /somenoiseval0);
                                    float noiseY2 = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) / somenoiseval1);
                                    float noiseY5 = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) / somenoiseval1);
                                    for (int z = 0; z < width; z++)
                                    {
                                        //float noiseZ = Math.Abs((float)(z * levelgenmapsplanesize + chunkPos[2] + seed0) /somenoiseval0);
                                        float noiseZ2 = Math.Abs((float)((depth - 1 - z) * levelgenmapsplanesize + chunkPos[2] + seed0) / somenoiseval1);
                                        float noiseZ5 = Math.Abs((float)((depth - 1 - z) * levelgenmapsplanesize + chunkPos[2] + seed0) / somenoiseval1);

                                        //float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);

                                        float noiseValue = someothernoisevalue;
                                        noiseValue *= fastNoise.GetNoise((((x * staticPlaneSize) + (chunkPos[0] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((y * staticPlaneSize) + (chunkPos[1] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((z * staticPlaneSize) + (chunkPos[2] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale);

                                        float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);
                                        float noiseValue5 = Noise.Generate(noiseX5, noiseZ5, noiseY5);

                                        if (noiseValue > 0.2f && y < sccslevelgen.wallheightsize - 1)
                                        {
                                            map[x + width * (y + height * z)] = 1;
                                        }

                                        float noiseValue9i = noiseValue2;

                                        noiseValue9i += (5 - (float)x) / 5;
                                        noiseValue9i /= (float)x / 5;

                                        float noiseValue10i = noiseValue5;
                                        noiseValue10i += (5 - (float)z) / 5;
                                        noiseValue10i /= (float)z / 5;



                                        if (noiseValue9i > 0.2f || noiseValue10i > 0.2f)
                                        {
                                            map[x + width * (y + height * (depth - 1 - z))] = 1;
                                            //backInsideCornerExtremity[x + width * (y + height * z)] = map[x + width * (y + height * z)];
                                        }

                                        /*if (x == 0 || z == 0)
                                        {
                                            map[x + width * (y + height * z)] = 1;
                                        }

                                        map[x + width * (y + height * z)] = 1;*/

                                    }
                                }
                            }
                        }
                    }
                }





                /////////////////////////////////////RIGHT BACK INSIDE CORNER////////////////////////////////
                if (typeofterraintiles == 1106)
                {
                    //for (int j = 0; j < levelgen.builtRightFrontInsideCorner.Count; j++)
                    {
                        //if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == levelgen.builtRightFrontInsideCorner[j])
                        {
                            for (int x = 0; x < width; x++)
                            {
                                //float noiseX = Math.Abs((float)(x * levelgenmapsplanesize + chunkPos[0] + seed0) /somenoiseval0);
                                float noiseX2 = Math.Abs((float)((width - 1 - x) * levelgenmapsplanesize + chunkPos[0] + seed0) / somenoiseval1);
                                float noiseX5 = Math.Abs((float)((width - 1 - x) * levelgenmapsplanesize + chunkPos[0] + seed0) / somenoiseval1);
                                for (int y = 0; y < height; y++)
                                {
                                    //float noiseY = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) /somenoiseval0);
                                    float noiseY2 = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) / somenoiseval1);
                                    float noiseY5 = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) / somenoiseval1);
                                    for (int z = 0; z < width; z++)
                                    {
                                        //float noiseZ = Math.Abs((float)(z * levelgenmapsplanesize + chunkPos[2] + seed0) /somenoiseval0);
                                        float noiseZ2 = Math.Abs((float)((depth - 1 - z) * levelgenmapsplanesize + chunkPos[2] + seed0) / somenoiseval1);
                                        float noiseZ5 = Math.Abs((float)((depth - 1 - z) * levelgenmapsplanesize + chunkPos[2] + seed0) / somenoiseval1);

                                        //float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);

                                        float noiseValue = someothernoisevalue;
                                        noiseValue *= fastNoise.GetNoise(((((width - 1 - x) * staticPlaneSize) + (chunkPos[0] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((y * staticPlaneSize) + (chunkPos[1] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((z * staticPlaneSize) + (chunkPos[2] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale);

                                        /*float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);
                                        float noiseValue5 = Noise.Generate(noiseX5, noiseZ5, noiseY5);


                                        if (noiseValue > 0.2f && y < sccslevelgen.wallheightsize -1)
                                        {
                                            map[x + width * (y + height * z)] = 1;
                                        }

                                        float noiseValue11i = noiseValue5;
                                        noiseValue11i += (5 - (float)z) / 5;
                                        noiseValue11i /= (float)z / 5;

                                        float noiseValue12i = noiseValue2;

                                        noiseValue12i += (5 - (float)x) / 5;
                                        noiseValue12i /= (float)x / 5;


                                        if (noiseValue11i > 0.2f || noiseValue12i < 0.2f)
                                        {
                                            map[x + width * (y + height * z)] = 1;
                                            //frontInsideCornerExtremity[x + width * (y + height * z)] = map[x + width * (y + height * z)];
                                        }*/

                                        float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);
                                        float noiseValue5 = Noise.Generate(noiseX5, noiseZ5, noiseY5);

                                        if (noiseValue > 0.2f && y < sccslevelgen.wallheightsize - 1)
                                        {
                                            map[x + width * (y + height * z)] = 1;
                                        }

                                        float noiseValue9i = noiseValue2;

                                        noiseValue9i += (5 - (float)x) / 5;
                                        noiseValue9i /= (float)x / 5;

                                        float noiseValue10i = noiseValue5;
                                        noiseValue10i += (5 - (float)z) / 5;
                                        noiseValue10i /= (float)z / 5;



                                        if (noiseValue9i > 0.2f || noiseValue10i > 0.2f)
                                        {
                                            map[(width - 1 - x) + width * (y + height * (depth - 1 - z))] = 1;
                                            //backInsideCornerExtremity[x + width * (y + height * z)] = map[x + width * (y + height * z)];
                                        }
                                        /*
                                        if (x == 0 || z == 0)
                                        {
                                            map[x + width * (y + height * z)] = 1;
                                        }

                                        map[x + width * (y + height * z)] = 1;*/

                                    }
                                }
                            }
                        }
                    }
                }








                /////////////////////////////////////LEFT FRONT INSIDE CORNER////////////////////////////////
                if (typeofterraintiles == 1107)
                {
                    //for (int j = 0; j < levelgen.builtLeftBackInsideCorner.Count; j++)
                    {
                        //if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == levelgen.builtLeftBackInsideCorner[j])
                        {
                            for (int x = 0; x < width; x++)
                            {
                                //float noiseX = Math.Abs((float)(x * levelgenmapsplanesize + chunkPos[0] + seed0) /somenoiseval0);
                                float noiseX2 = Math.Abs((float)(x * levelgenmapsplanesize + chunkPos[0] + seed0) / somenoiseval1);
                                float noiseX5 = Math.Abs((float)(x * levelgenmapsplanesize + chunkPos[0] + seed0) / somenoiseval1);
                                for (int y = 0; y < height; y++)
                                {
                                    //float noiseY = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) /somenoiseval0);
                                    float noiseY2 = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) / somenoiseval1);
                                    float noiseY5 = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) / somenoiseval1);
                                    for (int z = 0; z < width; z++)
                                    {
                                        //float noiseZ = Math.Abs((float)(z * levelgenmapsplanesize + chunkPos[2] + seed0) /somenoiseval0);
                                        float noiseZ2 = Math.Abs((float)(z * levelgenmapsplanesize + chunkPos[2] + seed0) / somenoiseval1);
                                        float noiseZ5 = Math.Abs((float)(z * levelgenmapsplanesize + chunkPos[2] + seed0) / somenoiseval1);

                                        //float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);

                                        float noiseValue = someothernoisevalue;
                                        noiseValue *= fastNoise.GetNoise((((x * staticPlaneSize) + (chunkPos[0] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((y * staticPlaneSize) + (chunkPos[1] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((z * staticPlaneSize) + (chunkPos[2] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale);

                                        float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);
                                        float noiseValue5 = Noise.Generate(noiseX5, noiseZ5, noiseY5);

                                        if (noiseValue > 0.2f && y < sccslevelgen.wallheightsize - 1)
                                        {
                                            map[x + width * (y + height * z)] = 1;
                                        }

                                        float noiseValue9i = noiseValue2;

                                        noiseValue9i += (5 - (float)x) / 5;
                                        noiseValue9i /= (float)x / 5;

                                        float noiseValue10i = noiseValue5;
                                        noiseValue10i += (5 - (float)z) / 5;
                                        noiseValue10i /= (float)z / 5;



                                        if (noiseValue9i > 0.2f || noiseValue10i > 0.2f)
                                        {
                                            map[x + width * (y + height * z)] = 1;
                                            //backInsideCornerExtremity[x + width * (y + height * z)] = map[x + width * (y + height * z)];
                                        }

                                        /*if (x == 0 || z == 0)
                                        {
                                            map[x + width * (y + height * z)] = 1;
                                        }

                                        map[x + width * (y + height * z)] = 1;*/

                                    }
                                }
                            }
                        }
                    }
                }




                /////////////////////////////////////RIGHT FRONT INSIDE CORNER////////////////////////////////
                if (typeofterraintiles == 1108)
                {
                    //for (int j = 0; j < levelgen.builtRightBackInsideCorner.Count; j++)
                    {
                        //if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == levelgen.builtRightBackInsideCorner[j])
                        {
                            for (int x = 0; x < width; x++)
                            {
                                //float noiseX = Math.Abs((float)(x * levelgenmapsplanesize + chunkPos[0] + seed0) /somenoiseval0);
                                float noiseX2 = Math.Abs((float)((width - 1 - x) * levelgenmapsplanesize + chunkPos[0] + seed0) / somenoiseval1);
                                float noiseX5 = Math.Abs((float)((width - 1 - x) * levelgenmapsplanesize + chunkPos[0] + seed0) / somenoiseval1);
                                for (int y = 0; y < height; y++)
                                {
                                    //float noiseY = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) /somenoiseval0);
                                    float noiseY2 = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) / somenoiseval1);
                                    float noiseY5 = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) / somenoiseval1);
                                    for (int z = 0; z < width; z++)
                                    {
                                        //float noiseZ = Math.Abs((float)(z * levelgenmapsplanesize + chunkPos[2] + seed0) /somenoiseval0);
                                        float noiseZ2 = Math.Abs((float)(z * levelgenmapsplanesize + chunkPos[2] + seed0) / somenoiseval1);
                                        float noiseZ5 = Math.Abs((float)(z * levelgenmapsplanesize + chunkPos[2] + seed0) / somenoiseval1);

                                        //float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);

                                        float noiseValue = someothernoisevalue;
                                        noiseValue *= fastNoise.GetNoise(((((width - 1 - x) * staticPlaneSize) + (chunkPos[0] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((y * staticPlaneSize) + (chunkPos[1] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((z * staticPlaneSize) + (chunkPos[2] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale);

                                        /*float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);
                                        float noiseValue5 = Noise.Generate(noiseX5, noiseZ5, noiseY5);


                                        if (noiseValue > 0.2f && y < sccslevelgen.wallheightsize -1)
                                        {
                                            map[x + width * (y + height * z)] = 1;
                                        }

                                        float noiseValue11i = noiseValue5;
                                        noiseValue11i += (5 - (float)z) / 5;
                                        noiseValue11i /= (float)z / 5;

                                        float noiseValue12i = noiseValue2;

                                        noiseValue12i += (5 - (float)x) / 5;
                                        noiseValue12i /= (float)x / 5;


                                        if (noiseValue11i > 0.2f || noiseValue12i < 0.2f)
                                        {
                                            map[x + width * (y + height * z)] = 1;
                                            //frontInsideCornerExtremity[x + width * (y + height * z)] = map[x + width * (y + height * z)];
                                        }*/

                                        float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);
                                        float noiseValue5 = Noise.Generate(noiseX5, noiseZ5, noiseY5);

                                        if (noiseValue > 0.2f && y < sccslevelgen.wallheightsize - 1)
                                        {
                                            map[x + width * (y + height * z)] = 1;
                                        }

                                        float noiseValue9i = noiseValue2;

                                        noiseValue9i += (5 - (float)x) / 5;
                                        noiseValue9i /= (float)x / 5;

                                        float noiseValue10i = noiseValue5;
                                        noiseValue10i += (5 - (float)z) / 5;
                                        noiseValue10i /= (float)z / 5;



                                        if (noiseValue9i > 0.2f || noiseValue10i > 0.2f)
                                        {
                                            map[(width - 1 - x) + width * (y + height * z)] = 1;
                                            //backInsideCornerExtremity[x + width * (y + height * z)] = map[x + width * (y + height * z)];
                                        }
                                        /*
                                        if (x == 0 || z == 0)
                                        {
                                            map[x + width * (y + height * z)] = 1;
                                        }

                                        map[x + width * (y + height * z)] = 1;*/

                                    }
                                }
                            }
                        }
                    }
                }




                /////////////////////////////////////LEFT BACK OUTSIDE CORNER////////////////////////////////
                if (typeofterraintiles == 1109)
                {
                    // for (int j = 0; j < levelgen.builtLeftFrontOutsideCorner.Count; j++)
                    {
                        //if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == levelgen.builtLeftFrontOutsideCorner[j])
                        {
                            for (int x = 0; x < width; x++)
                            {
                                //float noiseX = Math.Abs((float)(x * levelgenmapsplanesize + chunkPos[0] + seed0) /somenoiseval0);
                                float noiseX2 = Math.Abs((float)(x * levelgenmapsplanesize + chunkPos[0] + seed0) / somenoiseval1);
                                float noiseX5 = Math.Abs((float)(x * levelgenmapsplanesize + chunkPos[0] + seed0) / somenoiseval1);
                                for (int y = 0; y < height; y++)
                                {
                                    //float noiseY = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) /somenoiseval0);
                                    float noiseY2 = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) / somenoiseval1);
                                    float noiseY5 = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) / somenoiseval1);
                                    for (int z = 0; z < width; z++)
                                    {
                                        //float noiseZ = Math.Abs((float)(z * levelgenmapsplanesize + chunkPos[2] + seed0) /somenoiseval0);
                                        float noiseZ2 = Math.Abs((float)((depth - 1 - z) * levelgenmapsplanesize + chunkPos[2] + seed0) / somenoiseval1);
                                        float noiseZ5 = Math.Abs((float)((depth - 1 - z) * levelgenmapsplanesize + chunkPos[2] + seed0) / somenoiseval1);

                                        //float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);
                                        float noiseValue = someothernoisevalue;
                                        noiseValue *= fastNoise.GetNoise((((x * staticPlaneSize) + (chunkPos[0] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((y * staticPlaneSize) + (chunkPos[1] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((z * staticPlaneSize) + (chunkPos[2] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale);


                                        float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);
                                        float noiseValue5 = Noise.Generate(noiseX5, noiseZ5, noiseY5);

                                        //noiseValue += (10 - (float)y) / somenoisevalue;
                                        //noiseValue /= (float)y / 5;

                                        if (noiseValue > 0.2f && y < sccslevelgen.wallheightsize - 1)
                                        {
                                            map[x + width * (y + height * z)] = 1;
                                        }

                                        float noiseValue17i = noiseValue2;

                                        noiseValue17i += (5 - (float)x) / 5;
                                        noiseValue17i /= (float)x / 5;

                                        float noiseValue18i = noiseValue5;

                                        noiseValue18i += (5 - (float)z) / 5;
                                        noiseValue18i /= (float)z / 5;

                                        if (noiseValue17i > 0.2f && noiseValue18i > 0.2f)
                                        {
                                            map[x + width * (y + height * (depth - 1 - z))] = 1;
                                            //backOutsideCornerExtremity[x + width * (y + height * z)] = map[x + width * (y + height * z)];
                                        }

                                        //map[x + width * (y + height * z)] = 1;
                                    }
                                }
                            }
                        }
                    }
                }



                /////////////////////////////////////RIGHT BACK OUTSIDE CORNER////////////////////////////////
                if (typeofterraintiles == 1110)
                {
                    //for (int j = 0; j < levelgen.builtRightFrontOutsideCorner.Count; j++)
                    {
                        //if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == levelgen.builtRightFrontOutsideCorner[j])
                        {
                            for (int x = 0; x < width; x++)
                            {
                                //float noiseX = Math.Abs((float)(x * levelgenmapsplanesize + chunkPos[0] + seed0) /somenoiseval0);
                                float noiseX2 = Math.Abs((float)((width - 1 - x) * levelgenmapsplanesize + chunkPos[0] + seed0) / somenoiseval1);
                                float noiseX5 = Math.Abs((float)((width - 1 - x) * levelgenmapsplanesize + chunkPos[0] + seed0) / somenoiseval1);
                                for (int y = 0; y < height; y++)
                                {
                                    //float noiseY = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) /somenoiseval0);
                                    float noiseY2 = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) / somenoiseval1);
                                    float noiseY5 = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) / somenoiseval1);
                                    for (int z = 0; z < width; z++)
                                    {
                                        //float noiseZ = Math.Abs((float)(z * levelgenmapsplanesize + chunkPos[2] + seed0) /somenoiseval0);
                                        float noiseZ2 = Math.Abs((float)((depth - 1 - z) * levelgenmapsplanesize + chunkPos[2] + seed0) / somenoiseval1);
                                        float noiseZ5 = Math.Abs((float)((depth - 1 - z) * levelgenmapsplanesize + chunkPos[2] + seed0) / somenoiseval1);

                                        //float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);
                                        float noiseValue = someothernoisevalue;
                                        noiseValue *= fastNoise.GetNoise((((x * staticPlaneSize) + (chunkPos[0] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((y * staticPlaneSize) + (chunkPos[1] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((z * staticPlaneSize) + (chunkPos[2] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale);


                                        float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);
                                        float noiseValue5 = Noise.Generate(noiseX5, noiseZ5, noiseY5);

                                        //noiseValue += (10 - (float)y) / somenoisevalue;
                                        //noiseValue /= (float)y / 5;

                                        if (noiseValue > 0.2f && y < sccslevelgen.wallheightsize - 1)
                                        {
                                            map[x + width * (y + height * z)] = 1;
                                        }

                                        float noiseValue17i = noiseValue2;

                                        noiseValue17i += (5 - (float)x) / 5;
                                        noiseValue17i /= (float)x / 5;

                                        float noiseValue18i = noiseValue5;

                                        noiseValue18i += (5 - (float)z) / 5;
                                        noiseValue18i /= (float)z / 5;

                                        if (noiseValue17i > 0.2f && noiseValue18i > 0.2f)
                                        {
                                            map[(width - 1 - x) + width * (y + height * (depth - 1 - z))] = 1;
                                            //backOutsideCornerExtremity[x + width * (y + height * z)] = map[x + width * (y + height * z)];
                                        }

                                        //map[x + width * (y + height * z)] = 1;
                                    }
                                }
                            }
                        }
                    }
                }


                /////////////////////////////////////LEFT FRONT OUTSIDE CORNER////////////////////////////////

                if (typeofterraintiles == 1111)
                {
                    //for (int j = 0; j < levelgen.builtLeftBackOutsideCorner.Count; j++)
                    {
                        //if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == levelgen.builtLeftBackOutsideCorner[j])
                        {
                            for (int x = 0; x < width; x++)
                            {
                                //float noiseX = Math.Abs((float)(x * levelgenmapsplanesize + chunkPos[0] + seed0) /somenoiseval0);
                                float noiseX2 = Math.Abs((float)(x * levelgenmapsplanesize + chunkPos[0] + seed0) / somenoiseval1);
                                float noiseX5 = Math.Abs((float)(x * levelgenmapsplanesize + chunkPos[0] + seed0) / somenoiseval1);
                                for (int y = 0; y < height; y++)
                                {
                                    //float noiseY = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) /somenoiseval0);
                                    float noiseY2 = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) / somenoiseval1);
                                    float noiseY5 = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) / somenoiseval1);
                                    for (int z = 0; z < width; z++)
                                    {
                                        //float noiseZ = Math.Abs((float)(z * levelgenmapsplanesize + chunkPos[2] + seed0) /somenoiseval0);
                                        float noiseZ2 = Math.Abs((float)(z * levelgenmapsplanesize + chunkPos[2] + seed0) / somenoiseval1);
                                        float noiseZ5 = Math.Abs((float)(z * levelgenmapsplanesize + chunkPos[2] + seed0) / somenoiseval1);

                                        //float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);
                                        float noiseValue = someothernoisevalue;
                                        noiseValue *= fastNoise.GetNoise((((x * staticPlaneSize) + (chunkPos[0] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((y * staticPlaneSize) + (chunkPos[1] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((z * staticPlaneSize) + (chunkPos[2] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale);


                                        float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);
                                        float noiseValue5 = Noise.Generate(noiseX5, noiseZ5, noiseY5);

                                        //noiseValue += (10 - (float)y) / somenoisevalue;
                                        //noiseValue /= (float)y / 5;

                                        if (noiseValue > 0.2f && y < sccslevelgen.wallheightsize - 1)
                                        {
                                            map[x + width * (y + height * z)] = 1;
                                        }

                                        float noiseValue17i = noiseValue2;

                                        noiseValue17i += (5 - (float)x) / 5;
                                        noiseValue17i /= (float)x / 5;

                                        float noiseValue18i = noiseValue5;

                                        noiseValue18i += (5 - (float)z) / 5;
                                        noiseValue18i /= (float)z / 5;

                                        if (noiseValue17i > 0.2f && noiseValue18i > 0.2f)
                                        {
                                            map[x + width * (y + height * z)] = 1;
                                            //backOutsideCornerExtremity[x + width * (y + height * z)] = map[x + width * (y + height * z)];
                                        }

                                        //map[x + width * (y + height * z)] = 1;
                                    }
                                }
                            }
                        }
                    }

                }


                /////////////////////////////////////RIGHT FRONT OUTSIDE CORNER////////////////////////////////
                if (typeofterraintiles == 1112)
                {
                    //for (int j = 0; j < levelgen.builtRightBackOutsideCorner.Count; j++)
                    {
                        //if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == levelgen.builtRightBackOutsideCorner[j])
                        {
                            for (int x = 0; x < width; x++)
                            {
                                //float noiseX = Math.Abs((float)(x * levelgenmapsplanesize + chunkPos[0] + seed0) /somenoiseval0);
                                float noiseX2 = Math.Abs((float)((width - 1 - x) * levelgenmapsplanesize + chunkPos[0] + seed0) / somenoiseval1);
                                float noiseX5 = Math.Abs((float)((width - 1 - x) * levelgenmapsplanesize + chunkPos[0] + seed0) / somenoiseval1);
                                for (int y = 0; y < height; y++)
                                {
                                    //float noiseY = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) /somenoiseval0);
                                    float noiseY2 = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) / somenoiseval1);
                                    float noiseY5 = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) / somenoiseval1);
                                    for (int z = 0; z < width; z++)
                                    {
                                        //float noiseZ = Math.Abs((float)(z * levelgenmapsplanesize + chunkPos[2] + seed0) /somenoiseval0);
                                        float noiseZ2 = Math.Abs((float)(z * levelgenmapsplanesize + chunkPos[2] + seed0) / somenoiseval1);
                                        float noiseZ5 = Math.Abs((float)(z * levelgenmapsplanesize + chunkPos[2] + seed0) / somenoiseval1);

                                        //float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);
                                        float noiseValue = someothernoisevalue;
                                        noiseValue *= fastNoise.GetNoise((((x * staticPlaneSize) + (chunkPos[0] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((y * staticPlaneSize) + (chunkPos[1] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((z * staticPlaneSize) + (chunkPos[2] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale);


                                        float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);
                                        float noiseValue5 = Noise.Generate(noiseX5, noiseZ5, noiseY5);

                                        //noiseValue += (10 - (float)y) / somenoisevalue;
                                        //noiseValue /= (float)y / 5;

                                        if (noiseValue > 0.2f && y < sccslevelgen.wallheightsize - 1)
                                        {
                                            map[x + width * (y + height * z)] = 1;
                                        }

                                        float noiseValue17i = noiseValue2;

                                        noiseValue17i += (5 - (float)x) / 5;
                                        noiseValue17i /= (float)x / 5;

                                        float noiseValue18i = noiseValue5;

                                        noiseValue18i += (5 - (float)z) / 5;
                                        noiseValue18i /= (float)z / 5;

                                        if (noiseValue17i > 0.2f && noiseValue18i > 0.2f)
                                        {
                                            map[(width - 1 - x) + width * (y + height * z)] = 1;
                                            //backOutsideCornerExtremity[x + width * (y + height * z)] = map[x + width * (y + height * z)];
                                        }

                                        //map[x + width * (y + height * z)] = 1;
                                    }
                                }
                            }
                        }
                    }
                }






























                /////////////////////////////////////LEFT FRONT OUTSIDE CORNER////////////////////////////////
                if (typeofterraintiles == 13)
                {
                    // for (int j = 0; j < levelgen.builtLeftFrontOutsideCorner.Count; j++)
                    {
                        //if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == levelgen.builtLeftFrontOutsideCorner[j])
                        {
                            for (int x = 0; x < width; x++)
                            {
                                //float noiseX = Math.Abs((float)(x * levelgenmapsplanesize + chunkPos[0] + seed0) /somenoiseval0);
                                float noiseX2 = Math.Abs((float)(x * levelgenmapsplanesize + chunkPos[0] + seed0) / somenoiseval1);
                                float noiseX5 = Math.Abs((float)(x * levelgenmapsplanesize + chunkPos[0] + seed0) / somenoiseval1);
                                for (int y = 0; y < height; y++)
                                {
                                    //float noiseY = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) /somenoiseval0);
                                    float noiseY2 = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) / somenoiseval1);
                                    float noiseY5 = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) / somenoiseval1);
                                    for (int z = 0; z < width; z++)
                                    {
                                        //float noiseZ = Math.Abs((float)(z * levelgenmapsplanesize + chunkPos[2] + seed0) /somenoiseval0);
                                        float noiseZ2 = Math.Abs((float)(z * levelgenmapsplanesize + chunkPos[2] + seed0) / somenoiseval1);
                                        float noiseZ5 = Math.Abs((float)(z * levelgenmapsplanesize + chunkPos[2] + seed0) / somenoiseval1);

                                        //float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);
                                        float noiseValue = someothernoisevalue;
                                        noiseValue *= fastNoise.GetNoise((((x * staticPlaneSize) + (chunkPos[0] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((y * staticPlaneSize) + (chunkPos[1] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((z * staticPlaneSize) + (chunkPos[2] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale);

                                        /*if ((int)Math.Round(noiseValue) >= y) //|| (int)Math.Round(noiseXZ) < -y
                                        {
                                            map[x + width * (y + height * z)] = 1;
                                        }
                                        else if (y == 0 && chunkPos[1] == 0)
                                        {
                                            map[x + width * (y + height * z)] = 1;
                                        }
                                        else
                                        {
                                            map[x + width * (y + height * z)] = 0;
                                        }*/

                                        float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);
                                        float noiseValue5 = Noise.Generate(noiseX5, noiseZ5, noiseY5);

                                        //noiseValue += (10 - (float)y) / somenoisevalue;
                                        //noiseValue /= (float)y / 5;

                                        if (noiseValue > 0.2f && y < sccslevelgen.wallheightsize - 1)
                                        {
                                            map[x + width * (y + height * z)] = 1;
                                        }

                                        float noiseValue13i = noiseValue2;

                                        noiseValue13i += (5 - (float)x) / 5;
                                        noiseValue13i /= (float)x / 5;

                                        float noiseValue14i = noiseValue5;

                                        noiseValue14i += (5 - (float)z) / 5;
                                        noiseValue14i /= (float)z / 5;


                                        if (noiseValue13i > 0.2f && noiseValue14i < 0.2f)
                                        {
                                            map[x + width * (y + height * z)] = 1;
                                            //leftOutsideCornerExtremity[x + width * (y + height * z)] = map[x + width * (y + height * z)];
                                        }

                                        if (noiseValue > 0.2f && y < sccslevelgen.wallheightsize - 1)
                                        {
                                            map[x + width * (y + height * z)] = 1;
                                        }

                                        float noiseValue19i = noiseValue5;
                                        noiseValue19i += (5 - (float)z) / 5;
                                        noiseValue19i /= (float)z / 5;

                                        float noiseValue20i = noiseValue2;
                                        noiseValue20i += (5 - (float)x) / 5;
                                        noiseValue20i /= (float)x / 5;


                                        if (noiseValue19i > 0.2f && noiseValue20i < 0.2f)
                                        {
                                            map[x + width * (y + height * z)] = 1;
                                            //frontOutsideCornerExtremity[x + width * (y + height * z)] = map[x + width * (y + height * z)];
                                        }
                                    }
                                }
                            }
                        }
                    }
                }









                /////////////////////////////////////RIGHT FRONT OUTSIDE CORNER////////////////////////////////
                if (typeofterraintiles == 14)
                {
                    //for (int j = 0; j < levelgen.builtRightFrontOutsideCorner.Count; j++)
                    {
                        //if (new Vector3(xChunkPos, yChunkPos, zChunkPos) == levelgen.builtRightFrontOutsideCorner[j])
                        {
                            for (int x = 0; x < width; x++)
                            {
                                //float noiseX = Math.Abs((float)(x * levelgenmapsplanesize + chunkPos[0] + seed0) /somenoiseval0);
                                float noiseX2 = Math.Abs((float)(x * levelgenmapsplanesize + chunkPos[0] + seed0) / somenoiseval1);
                                float noiseX5 = Math.Abs((float)(x * levelgenmapsplanesize + chunkPos[0] + seed0) / somenoiseval1);
                                for (int y = 0; y < height; y++)
                                {
                                    //float noiseY = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) /somenoiseval0);
                                    float noiseY2 = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) / somenoiseval1);
                                    float noiseY5 = Math.Abs((float)(y * levelgenmapsplanesize + chunkPos[1] + seed0) / somenoiseval1);
                                    for (int z = 0; z < width; z++)
                                    {
                                        //float noiseZ = Math.Abs((float)(z * levelgenmapsplanesize + chunkPos[2] + seed0) /somenoiseval0);
                                        float noiseZ2 = Math.Abs((float)(z * levelgenmapsplanesize + chunkPos[2] + seed0) / somenoiseval1);
                                        float noiseZ5 = Math.Abs((float)(z * levelgenmapsplanesize + chunkPos[2] + seed0) / somenoiseval1);

                                        //float noiseValue = Noise.Generate(noiseX, noiseY, noiseZ);
                                        float noiseValue = someothernoisevalue;
                                        noiseValue *= fastNoise.GetNoise((((x * staticPlaneSize) + (chunkPos[0] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((y * staticPlaneSize) + (chunkPos[1] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale, (((z * staticPlaneSize) + (chunkPos[2] * alternateStaticPlaneSize) + seed) / _detailScale) * _heightScale);


                                        float noiseValue2 = Noise.Generate(noiseY2, noiseX2, noiseZ2);
                                        float noiseValue5 = Noise.Generate(noiseX5, noiseZ5, noiseY5);

                                        //noiseValue += (10 - (float)y) / somenoisevalue;
                                        //noiseValue /= (float)y / 5;

                                        /*if ((int)Math.Round(noiseValue) >= y) //|| (int)Math.Round(noiseXZ) < -y
                                        {
                                            map[x + width * (y + height * z)] = 1;
                                        }
                                        else if (y == 0 && chunkPos[1] == 0)
                                        {
                                            map[x + width * (y + height * z)] = 1;
                                        }
                                        else
                                        {
                                            map[x + width * (y + height * z)] = 0;
                                        }*/


                                        if (noiseValue > 0.2f && y < sccslevelgen.wallheightsize - 1)
                                        {
                                            map[x + width * (y + height * z)] = 1;
                                        }

                                        float noiseValue15i = noiseValue2;

                                        noiseValue15i += (5 - (float)x) / 5;
                                        noiseValue15i /= (float)x / 5;

                                        float noiseValue16i = noiseValue5;

                                        noiseValue16i += (5 - (float)z) / 5;
                                        noiseValue16i /= (float)z / 5;


                                        if (noiseValue15i < 0.2f && noiseValue16i < 0.2f)
                                        {
                                            map[x + width * (y + height * z)] = 1;
                                            //rightOutsideCornerExtremity[x + width * (y + height * z)] = map[x + width * (y + height * z)];
                                        }

                                        float noiseValue17i = noiseValue2;

                                        noiseValue17i += (5 - (float)x) / 5;
                                        noiseValue17i /= (float)x / 5;

                                        float noiseValue18i = noiseValue5;

                                        noiseValue18i += (5 - (float)z) / 5;
                                        noiseValue18i /= (float)z / 5;

                                        if (noiseValue17i > 0.2f && noiseValue18i > 0.2f)
                                        {
                                            map[x + width * (y + height * z)] = 1;
                                            //backOutsideCornerExtremity[x + width * (y + height * z)] = map[x + width * (y + height * z)];
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                for (int x = 0; x < width; x++)
                {

                    for (int y = 0; y < height; y++)
                    {
                        for (int z = 0; z < width; z++)
                        {
                            map[x + width * (y + height * z)] = 1;
                        }
                    }
                }
            }


            //fastNoise = null;
        }



        public void setmapforchunks(out double m11, out double m12, out double m13, out double m14,
            out double m21, out double m22, out double m23, out double m24, out double m31, out double m32, out double m33, out double m34, out double m41, out double m42, out double m43, out double m44)
        {
            //int minx,int miny, int minz, int maxx, int maxy, int maxz , 



            maxveclength = 4;
            /*for (int i = 0; i < map.Length;i++)
            {
                if (map[i] == 1)
                {
                    mapvertindexfordims[i] = 0;
                }
                else if (map[i] == 2)
                {
                    mapvertindexfordims[i] = 1;
                }
            }*/


            //for 4 by 4 by 4 and placing 8 digits in a float.
            //0-4-1-5-2-6-3-7
            //8-12-9-13-10-14-11-15
            //16-20-17-21-18-22-19-23
            //24-28-25-29-26-30-27-31
            //32-36-33-37-34-38-35-39
            //40-44-41-45-42-46-43-47
            //48-52-49-53-50-54-51-55
            //56-60-57-61-58-62-59-63

            //0 16 32 48
            //4 20 36 52
            //8 24 40 56
            //12 28 44 60
            //1 17 33 49
            //5 21 37 53
            //9 25 41 57
            //13 29 45 61
            //2 18 34 50
            //6 22 38 54 
            //10 26 42 58 
            //14 30 46 62
            //3 19 35 51
            //7 23 39 55
            //11 27 43 59
            //15 31 47 63


            //0 16 32
            //48 4 20
            //36 52 8
            //24 40 56
            //12 28 44
            //60 1 17
            //33 49 5
            //21 37 53
            //9 25 41
            //57 13 29
            //45 61 2
            //18 34 50
            //6 22 38
            //54 10 26
            //42 58 14
            //30 46 62 
            //3 19 35
            //51 7 23
            //39 55 11
            //27 43 59 
            //15 31 47
            //63

            /*
            somenewmap = new int[mapvertindexfordims.Length];

            List<int> somedata = new List<int>();
            int somecounter = 0;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        int index = x + width * (y + height * z);

                        if (mapvertindexfordims[index] == 1)
                        {
                            somedata.Add(index);
                            somecounter++;
                        }
                    }
                }
            }



            int someothercounter = 0;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        int index = x + width * (y + height * z);

                        ////Console.WriteLine(index);
                        if (someothercounter < somedata.Count)
                        {
                            somenewmap[index] = mapvertindexfordims[somedata[someothercounter]];
                        }
                        someothercounter++;
                    }
                }
            }
            */
























            /*
            //List<int>
            somenewmap = new int[mapvertindexfordims.Length];
            int somecounter = 0;
            for (int i = 0; i < mapvertindexfordims.Length; i++)
            {
                if (mapvertindexfordims[i] == 1)
                {
                    somenewmap[somecounter] = 1;
                    somecounter++;
                }
            }*/

            /*
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < depth; z++)
                    {

                    }
                }
            }*/
            /*
            somenewmap = new int[mapvertindexfordims.Length];
            List<int> somedata = new List<int>();
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        int index = x + width * (y + height * z);

                        if (mapvertindexfordims[index] == 1)
                        {
                            //somenewdimswidth[somecounter] = widthdim[i];
                            //somenewdimsheight[somecounter] = heightdim[i];
                            //somenewdimsdepth[somecounter] = depthdim[i];
                            somedata.Add(index);
                            somecounter++;
                        }
                    }
                }
            }

            int someothercounter = 0;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        int index = x + width * (y + height * z);

                        if (someothercounter < somedata.Count)
                        {
                            somenewmap[index] = mapvertindexfordims[somedata[someothercounter]];
                        }
                        someothercounter++;
                    }
                }
            }
            */









            //int someindex = 0 + width * (3 + height * 0);

            ////Console.WriteLine("someindex: " + someindex);

            //0 + width * (0 + height * 0) == 0
            //0 + width * (0 + height * 1) == 16
            //0 + width * (0 + height * 2) == 32
            //0 + width * (0 + height * 3) == 48

            //0 + width * (0 + height * 0) == 0
            //1 + width * (0 + height * 0) == 1
            //2 + width * (0 + height * 0) == 2
            //3 + width * (0 + height * 0) == 3

            //0 + width * (0 + height * 0) == 0
            //0 + width * (1 + height * 0) == 4
            //0 + width * (2 + height * 0) == 8
            //0 + width * (3 + height * 0) == 12

            double arrayofbytemaprowm11a = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm12a = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm13a = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm14a = Program.paddingformaps;// 1; //111111111111111111111111111

            double arrayofbytemaprowm21a = Program.paddingformaps;// 1; //111111111111111111111111111 
            double arrayofbytemaprowm22a = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm23a = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm24a = Program.paddingformaps;// 1; //111111111111111111111111111

            double arrayofbytemaprowm31a = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm32a = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm33a = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm34a = Program.paddingformaps;// 1; //111111111111111111111111111

            double arrayofbytemaprowm41a = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm42a = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm43a = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm44a = Program.paddingformaps;// 1; //111111111111111111111111111

            total = width * height * depth;

            int switchXX = 0;
            int switchYY = 0;

            double selectablevectordouble = 0;
            int maxv = width * height * depth;

            int m11adder = 0;
            int m12adder = 0;
            int m13adder = 0;
            int m14adder = 0;

            int m21adder = 0;
            int m22adder = 0;
            int m23adder = 0;
            int m24adder = 0;

            int m31adder = 0;
            int m32adder = 0;
            int m33adder = 0;
            int m34adder = 0;

            int m41adder = 0;
            int m42adder = 0;
            int m43adder = 0;
            int m44adder = 0;



            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        //int index = x + (width * (y + (height * z)));
                        ////Console.WriteLine("index:" + index);
                        //int currentByte = map[index];

                        //10*4*4

                        int index = x + width * (y + height * z);//; //x + width * (y + height * z);// max of 64
                        int currentByte = mapvertindexfordims[index];

                        //Console.Write(" " + index);

                        int somemaxvecdigit = maxveclength;
                        int somecountermul = 0;
                        int somec = 0;

                        //3 

                        for (int t = 0; t <= index; t++) // index == 45 == 11 
                        {
                            if (somec == somemaxvecdigit)
                            {
                                somecountermul++;
                                somec = 0;
                            }
                            somec++;
                        }


                        //for 4 by 4 by 4 and placing 8 digits in a float.
                        //0-4-1-5-2-6-3-7
                        //8-12-9-13-10-14-11-15
                        //16-20-17-21-18-22-19-23
                        //24-28-25-29-26-30-27-31
                        //32-36-33-37-34-38-35-39
                        //40-44-41-45-42-46-43-47
                        //48-52-49-53-50-54-51-55
                        //56-60-57-61-58-62-59-63

                        //0 16 32 48
                        //4 20 36 52
                        //8 24 40 56
                        //12 28 44 60
                        //1 17 33 49
                        //5 21 37 53
                        //9 25 41 57
                        //13 29 45 61
                        //2 18 34 50
                        //6 22 38 54 
                        //10 26 42 58 
                        //14 30 46 62
                        //3 19 35 51
                        //7 23 39 55
                        //11 27 43 59
                        //15 31 47 63

                        if (index == 0 || index == 16 || index == 32 || index == 48)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm11a = (arrayofbytemaprowm11a * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm11a = (arrayofbytemaprowm11a * 10) + currentByte;
                            }
                        }
                        else if (index == 4 || index == 20 || index == 36 || index == 52)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm12a = (arrayofbytemaprowm12a * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm12a = (arrayofbytemaprowm12a * 10) + currentByte;
                            }
                        }
                        else if (index == 8 || index == 24 || index == 40 || index == 56)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm13a = (arrayofbytemaprowm13a * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm13a = (arrayofbytemaprowm13a * 10) + currentByte;
                            }
                        }
                        else if (index == 12 || index == 28 || index == 44 || index == 60)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm14a = (arrayofbytemaprowm14a * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm14a = (arrayofbytemaprowm14a * 10) + currentByte;
                            }
                        }
                        else if (index == 1 || index == 17 || index == 33 || index == 49)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm21a = (arrayofbytemaprowm21a * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm21a = (arrayofbytemaprowm21a * 10) + currentByte;
                            }
                        }
                        else if (index == 5 || index == 21 || index == 37 || index == 53)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm22a = (arrayofbytemaprowm22a * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm22a = (arrayofbytemaprowm22a * 10) + currentByte;
                            }
                        }
                        else if (index == 9 || index == 25 || index == 41 || index == 57)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm23a = (arrayofbytemaprowm23a * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm23a = (arrayofbytemaprowm23a * 10) + currentByte;
                            }
                        }
                        else if (index == 13 || index == 29 || index == 45 || index == 61)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm24a = (arrayofbytemaprowm24a * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm24a = (arrayofbytemaprowm24a * 10) + currentByte;
                            }
                        }
                        else if (index == 2 || index == 18 || index == 34 || index == 50)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm31a = (arrayofbytemaprowm31a * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm31a = (arrayofbytemaprowm31a * 10) + currentByte;
                            }
                        }
                        else if (index == 6 || index == 22 || index == 38 || index == 54)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm32a = (arrayofbytemaprowm32a * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm32a = (arrayofbytemaprowm32a * 10) + currentByte;
                            }
                        }
                        else if (index == 10 || index == 26 || index == 42 || index == 58)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm33a = (arrayofbytemaprowm33a * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm33a = (arrayofbytemaprowm33a * 10) + currentByte;
                            }
                        }
                        else if (index == 14 || index == 30 || index == 46 || index == 62)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm34a = (arrayofbytemaprowm34a * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm34a = (arrayofbytemaprowm34a * 10) + currentByte;
                            }
                        }
                        else if (index == 3 || index == 19 || index == 35 || index == 51)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm41a = (arrayofbytemaprowm41a * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm41a = (arrayofbytemaprowm41a * 10) + currentByte;
                            }
                        }
                        else if (index == 7 || index == 23 || index == 39 || index == 55)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm42a = (arrayofbytemaprowm42a * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm42a = (arrayofbytemaprowm42a * 10) + currentByte;
                            }
                        }
                        else if (index == 11 || index == 27 || index == 43 || index == 59)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm43a = (arrayofbytemaprowm43a * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm43a = (arrayofbytemaprowm43a * 10) + currentByte;
                            }
                        }
                        else if (index == 15 || index == 31 || index == 47 || index == 63)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm44a = (arrayofbytemaprowm44a * 10);
                            }
                            else
                            {
                                arrayofbytemaprowm44a = (arrayofbytemaprowm44a * 10) + currentByte;
                            }
                        }













                        /*
                        switch (somecountermul)
                        {
                            case 0:
                                //selectablevectordouble = arrayofbytemaprowm11a;


                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm11a = (arrayofbytemaprowm11a * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm11a = (arrayofbytemaprowm11a * 10) + currentByte;
                                }

                                m11adder++;
                                break;
                            case 1:
                                //selectablevectordouble = arrayofbytemaprowm12a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm12a = (arrayofbytemaprowm12a * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm12a = (arrayofbytemaprowm12a * 10) + currentByte;
                                }
                                m12adder++;
                                break;
                            case 2:
                                //selectablevectordouble = arrayofbytemaprowm13a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm13a = (arrayofbytemaprowm13a * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm13a = (arrayofbytemaprowm13a * 10) + currentByte;
                                }
                                m13adder++;
                                break;
                            case 3:
                                //selectablevectordouble = arrayofbytemaprowm14a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm14a = (arrayofbytemaprowm14a * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm14a = (arrayofbytemaprowm14a * 10) + currentByte;
                                }
                                m14adder++;
                                break;
                            case 4:
                                //selectablevectordouble = arrayofbytemaprowm21a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm21a = (arrayofbytemaprowm21a * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm21a = (arrayofbytemaprowm21a * 10) + currentByte;
                                }
                                m21adder++;
                                break;
                            case 5:
                                //selectablevectordouble = arrayofbytemaprowm22a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm22a = (arrayofbytemaprowm22a * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm22a = (arrayofbytemaprowm22a * 10) + currentByte;
                                }
                                m22adder++;
                                break;
                            case 6:
                                //selectablevectordouble = arrayofbytemaprowm23a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm23a = (arrayofbytemaprowm23a * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm23a = (arrayofbytemaprowm23a * 10) + currentByte;
                                }
                                m23adder++;
                                break;
                            case 7:
                                //selectablevectordouble = arrayofbytemaprowm24a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm24a = (arrayofbytemaprowm24a * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm24a = (arrayofbytemaprowm24a * 10) + currentByte;
                                }
                                m24adder++;
                                break;
                            case 8:
                                //selectablevectordouble = arrayofbytemaprowm31a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm31a = (arrayofbytemaprowm31a * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm31a = (arrayofbytemaprowm31a * 10) + currentByte;
                                }
                                m31adder++;
                                break;
                            case 9:
                                //selectablevectordouble = arrayofbytemaprowm32a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm32a = (arrayofbytemaprowm32a * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm32a = (arrayofbytemaprowm32a * 10) + currentByte;
                                }
                                m32adder++;

                                break;
                            case 10:
                                //selectablevectordouble = arrayofbytemaprowm33a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm33a = (arrayofbytemaprowm33a * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm33a = (arrayofbytemaprowm33a * 10) + currentByte;
                                }
                                m33adder++;
                                break;
                            case 11:
                                //selectablevectordouble = arrayofbytemaprowm34a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm34a = (arrayofbytemaprowm34a * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm34a = (arrayofbytemaprowm34a * 10) + currentByte;
                                }
                                m34adder++;
                                break;
                            case 12:
                                //selectablevectordouble = arrayofbytemaprowm41a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm41a = (arrayofbytemaprowm41a * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm41a = (arrayofbytemaprowm41a * 10) + currentByte;
                                }
                                m41adder++;
                                break;
                            case 13:
                                //selectablevectordouble = arrayofbytemaprowm42a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm42a = (arrayofbytemaprowm42a * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm42a = (arrayofbytemaprowm42a * 10) + currentByte;
                                }
                                m42adder++;
                                break;
                            case 14:
                                //selectablevectordouble = arrayofbytemaprowm43a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm43a = (arrayofbytemaprowm43a * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm43a = (arrayofbytemaprowm43a * 10) + currentByte;
                                }
                                m43adder++;
                                break;
                            case 15:
                                //selectablevectordouble = arrayofbytemaprowm44a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm44a = (arrayofbytemaprowm44a * 10);
                                }
                                else
                                {
                                    arrayofbytemaprowm44a = (arrayofbytemaprowm44a * 10) + currentByte;
                                }
                                m44adder++;
                                break;
                        };*/
                    }
                }
            }



            m11 = arrayofbytemaprowm11a;
            m12 = arrayofbytemaprowm12a;
            m13 = arrayofbytemaprowm13a;
            m14 = arrayofbytemaprowm14a;
            m21 = arrayofbytemaprowm21a;
            m22 = arrayofbytemaprowm22a;
            m23 = arrayofbytemaprowm23a;
            m24 = arrayofbytemaprowm24a;
            m31 = arrayofbytemaprowm31a;
            m32 = arrayofbytemaprowm32a;
            m33 = arrayofbytemaprowm33a;
            m34 = arrayofbytemaprowm34a;
            m41 = arrayofbytemaprowm41a;
            m42 = arrayofbytemaprowm42a;
            m43 = arrayofbytemaprowm43a;
            m44 = arrayofbytemaprowm44a;
        }



        /*int[] mapdimsx;
        int[] mapdimsy;
        int[] mapdimsz;

        int[] mapvertx;
        int[] mapverty;
        int[] mapvertz;*/

        /*int[] somenewfirstvertx;
        int[] somenewfirstverty;
        int[] somenewfirstvertz;*/

        //int maxveclengthalt;

        int vertminx = 0;
        int vertminy = 0;
        int vertminz = 0;
        int vertmaxx = 64;
        int vertmaxy = 64;
        int vertmaxz = 64;

        int vertrealx = 0;
        int vertrealy = 0;
        int vertrealz = 0;

        public void setmapforfirstverts(int minx, int miny, int minz, int maxx, int maxy, int maxz, int typeofterraintiles_, tutorialcubeaschunkinst componentparent_, int levelofdetail_, int maxveclengthalt, int facetype,
           out double m11, out double m12, out double m13, out double m14,
           out double m21, out double m22, out double m23, out double m24, out double m31, out double m32, out double m33, out double m34, out double m41, out double m42, out double m43, out double m44,

           out double m11b, out double m12b, out double m13b, out double m14b,
           out double m21b, out double m22b, out double m23b, out double m24b, out double m31b, out double m32b, out double m33b, out double m34b, out double m41b, out double m42b, out double m43b, out double m44b)// , int somechunkkeyboardpriminstanceindex_, int chunkprimindex_, int chunkinstindex_
        {


            int[] mapvertx = null;
            int[] mapverty = null;
            int[] mapvertz = null;

            mapvertx = mapfirstvertxtop;
            mapverty = mapfirstvertytop;
            mapvertz = mapfirstvertztop;

            /*
            if (facetype == 0)
            {
                mapvertx = mapfirstvertxtop;
                mapverty = mapfirstvertytop;
                mapvertz = mapfirstvertztop;

            }
            else if (facetype == 1)
            {
                mapvertx = mapfirstvertxleft;
                mapverty = mapfirstvertyleft;
                mapvertz = mapfirstvertzleft;
            }
            else if (facetype == 2)
            {
                mapvertx = mapfirstvertxright;
                mapverty = mapfirstvertyright;
                mapvertz = mapfirstvertzright;
            }
            else if (facetype == 3)
            {
                mapvertx = mapfirstvertxfront;
                mapverty = mapfirstvertyfront;
                mapvertz = mapfirstvertzfront;
            }
            else if (facetype == 4)
            {
                mapvertx = mapfirstvertxback;
                mapverty = mapfirstvertyback;
                mapvertz = mapfirstvertzback;
            }
            else if (facetype == 5)
            {
                mapvertx = mapfirstvertxbottom;
                mapverty = mapfirstvertybottom;
                mapvertz = mapfirstvertzbottom;
            }*/







            /*
            List<int> somedata = new List<int>();
            somenewfirstvertx = new int[mapvertindexfordims.Length];
            somenewfirstverty = new int[mapvertindexfordims.Length];
            somenewfirstvertz = new int[mapvertindexfordims.Length];
            */
            int somecounter = 0;
            /*for (int i = 0; i < mapvertindexfordims.Length; i++)
            {
                if (mapvertindexfordims[i] == 1)
                {
                    somenewfirstvertx[somecounter] = mapfirstvertx[i];
                    somenewfirstverty[somecounter] = mapfirstverty[i];
                    somenewfirstvertz[somecounter] = mapfirstvertz[i];
                    somedata.Add(i);
                    somecounter++;
                }
            }*/









            /*
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        int index = x + width * (y + height * z);

                        if (mapvertindexfordims[index] != 0)
                        {
                            //somenewdimswidth[somecounter] = widthdim[i];
                            //somenewdimsheight[somecounter] = heightdim[i];
                            //somenewdimsdepth[somecounter] = depthdim[i];
                            somedata.Add(index);
                            somecounter++;
                        }
                    }
                }
            }
            
            int someothercounter = 0;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        int index = x + width * (y + height * z);

                        if (someothercounter < somedata.Count)
                        {
                            somenewfirstvertx[index] = mapfirstvertx[somedata[someothercounter]];
                            somenewfirstverty[index] = mapfirstverty[somedata[someothercounter]];
                            somenewfirstvertz[index] = mapfirstvertz[somedata[someothercounter]];

                        }
                        someothercounter++;
                    }
                }
            }
            */


            //somedata = null;














            double arrayofbytemaprowm11a = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm12a = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm13a = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm14a = Program.paddingformaps;// 1; //111111111111111111111111111

            double arrayofbytemaprowm21a = Program.paddingformaps;// 1; //111111111111111111111111111 
            double arrayofbytemaprowm22a = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm23a = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm24a = Program.paddingformaps;// 1; //111111111111111111111111111

            double arrayofbytemaprowm31a = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm32a = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm33a = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm34a = Program.paddingformaps;// 1; //111111111111111111111111111

            double arrayofbytemaprowm41a = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm42a = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm43a = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm44a = Program.paddingformaps;// 1; //111111111111111111111111111

            double arrayofbytemaprowm11b = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm12b = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm13b = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm14b = Program.paddingformaps;// 1; //111111111111111111111111111

            double arrayofbytemaprowm21b = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm22b = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm23b = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm24b = Program.paddingformaps;// 1; //111111111111111111111111111

            double arrayofbytemaprowm31b = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm32b = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm33b = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm34b = Program.paddingformaps;// 1; //111111111111111111111111111

            double arrayofbytemaprowm41b = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm42b = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm43b = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm44b = Program.paddingformaps;// 1; //111111111111111111111111111

            double arrayofbytemaprowm11c = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm12c = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm13c = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm14c = Program.paddingformaps;// 1; //111111111111111111111111111

            double arrayofbytemaprowm21c = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm22c = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm23c = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm24c = Program.paddingformaps;// 1; //111111111111111111111111111

            double arrayofbytemaprowm31c = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm32c = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm33c = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm34c = Program.paddingformaps;// 1; //111111111111111111111111111

            double arrayofbytemaprowm41c = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm42c = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm43c = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm44c = Program.paddingformaps;// 1; //111111111111111111111111111

            double arrayofbytemaprowm11d = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm12d = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm13d = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm14d = Program.paddingformaps;// 1; //111111111111111111111111111

            double arrayofbytemaprowm21d = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm22d = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm23d = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm24d = Program.paddingformaps;// 1; //111111111111111111111111111

            double arrayofbytemaprowm31d = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm32d = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm33d = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm34d = Program.paddingformaps;// 1; //111111111111111111111111111

            double arrayofbytemaprowm41d = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm42d = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm43d = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm44d = Program.paddingformaps;// 1; //111111111111111111111111111



            int maxv = width * height * depth;



            int m11adder = 0;
            int m12adder = 0;
            int m13adder = 0;
            int m14adder = 0;

            int m21adder = 0;
            int m22adder = 0;
            int m23adder = 0;
            int m24adder = 0;

            int m31adder = 0;
            int m32adder = 0;
            int m33adder = 0;
            int m34adder = 0;

            int m41adder = 0;
            int m42adder = 0;
            int m43adder = 0;
            int m44adder = 0;


            int m11badder = 0;
            int m12badder = 0;
            int m13badder = 0;
            int m14badder = 0;

            int m21badder = 0;
            int m22badder = 0;
            int m23badder = 0;
            int m24badder = 0;

            int m31badder = 0;
            int m32badder = 0;
            int m33badder = 0;
            int m34badder = 0;

            int m41badder = 0;
            int m42badder = 0;
            int m43badder = 0;
            int m44badder = 0;



            int m11cadder = 0;
            int m12cadder = 0;
            int m13cadder = 0;
            int m14cadder = 0;

            int m21cadder = 0;
            int m22cadder = 0;
            int m23cadder = 0;
            int m24cadder = 0;

            int m31cadder = 0;
            int m32cadder = 0;
            int m33cadder = 0;
            int m34cadder = 0;

            int m41cadder = 0;
            int m42cadder = 0;
            int m43cadder = 0;
            int m44cadder = 0;


            int m11dadder = 0;
            int m12dadder = 0;
            int m13dadder = 0;
            int m14dadder = 0;

            int m21dadder = 0;
            int m22dadder = 0;
            int m23dadder = 0;
            int m24dadder = 0;

            int m31dadder = 0;
            int m32dadder = 0;
            int m33dadder = 0;
            int m34dadder = 0;

            int m41dadder = 0;
            int m42dadder = 0;
            int m43dadder = 0;
            int m44dadder = 0;





            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    for (int z = 0; z < 4; z++)
                    {
                        //int index = x + (width * (y + (height * z)));
                        ////Console.WriteLine("index:" + index);
                        //int currentByte = map[index];

                        //10*4*4


                        int index = x + 4 * (y + 4 * z);//; //x + width * (y + height * z);//
                        int currentByte;// = map[index];

                        int somevertx = mapvertx[index];// widthdim[index];
                        int someverty = mapverty[index];
                        int somevertz = mapvertz[index];// depthdim[index];

                        currentByte = (((somevertx * 10) + (someverty)) * 10) + somevertz;

                        ////Console.WriteLine(currentByte);


                        //Console.Write(" " + index);

                        /*
                        int somemaxvecdigit = maxveclengthalt / 2;
                        int somecountermul = 0;
                        int somec = 0;

                        //3 

                        for (int t = 0; t <= index; t++) // index == 45 == 11 
                        {
                            if (somec == somemaxvecdigit)
                            {
                                somecountermul++;
                                somec = 0;
                            }
                            somec++;
                        }*/


                        if (index == 0 || index == 16)
                        {
                            if (index == 0)
                            {
                                arrayofbytemaprowm11a = currentByte;
                            }
                            else if (index == 16)
                            {
                                arrayofbytemaprowm11a = (arrayofbytemaprowm11a * 1000) + currentByte;
                            }
                        }
                        else if (index == 32 || index == 48)
                        {
                            if (index == 32)
                            {
                                arrayofbytemaprowm12a = currentByte;
                            }
                            else if (index == 48)
                            {
                                arrayofbytemaprowm12a = (arrayofbytemaprowm12a * 1000) + currentByte;
                            }
                        }
                        else if (index == 4 || index == 20)
                        {
                            if (index == 4)
                            {
                                arrayofbytemaprowm13a = currentByte;
                            }
                            else if (index == 20)
                            {
                                arrayofbytemaprowm13a = (arrayofbytemaprowm13a * 1000) + currentByte;
                            }
                        }
                        else if (index == 36 || index == 52)
                        {
                            if (index == 36)
                            {
                                arrayofbytemaprowm14a = currentByte;
                            }
                            else if (index == 52)
                            {
                                arrayofbytemaprowm14a = (arrayofbytemaprowm14a * 1000) + currentByte;
                            }
                        }
                        else if (index == 8 || index == 24)
                        {
                            if (index == 8)
                            {
                                arrayofbytemaprowm21a = currentByte;
                            }
                            else if (index == 24)
                            {
                                arrayofbytemaprowm21a = (arrayofbytemaprowm21a * 1000) + currentByte;
                            }
                        }
                        else if (index == 40 || index == 56)
                        {
                            if (index == 40)
                            {
                                arrayofbytemaprowm22a = currentByte;
                            }
                            else if (index == 56)
                            {
                                arrayofbytemaprowm22a = (arrayofbytemaprowm22a * 1000) + currentByte;
                            }
                        }
                        else if (index == 12 || index == 28)
                        {
                            if (index == 12)
                            {
                                arrayofbytemaprowm23a = currentByte;
                            }
                            else if (index == 28)
                            {
                                arrayofbytemaprowm23a = (arrayofbytemaprowm23a * 1000) + currentByte;
                            }
                        }
                        else if (index == 44 || index == 60)
                        {
                            if (index == 44)
                            {
                                arrayofbytemaprowm24a = currentByte;
                            }
                            else if (index == 60)
                            {
                                arrayofbytemaprowm24a = (arrayofbytemaprowm24a * 1000) + currentByte;
                            }
                        }
                        else if (index == 1 || index == 17)
                        {
                            if (index == 1)
                            {
                                arrayofbytemaprowm31a = currentByte;
                            }
                            else if (index == 17)
                            {
                                arrayofbytemaprowm31a = (arrayofbytemaprowm31a * 1000) + currentByte;
                            }
                        }
                        else if (index == 33 || index == 49)
                        {
                            if (index == 33)
                            {
                                arrayofbytemaprowm32a = currentByte;
                            }
                            else if (index == 49)
                            {
                                arrayofbytemaprowm32a = (arrayofbytemaprowm32a * 1000) + currentByte;
                            }
                        }
                        else if (index == 5 || index == 21)
                        {
                            if (index == 5)
                            {
                                arrayofbytemaprowm33a = currentByte;
                            }
                            else if (index == 21)
                            {
                                arrayofbytemaprowm33a = (arrayofbytemaprowm33a * 1000) + currentByte;
                            }
                        }
                        else if (index == 37 || index == 53)
                        {
                            if (index == 37)
                            {
                                arrayofbytemaprowm34a = currentByte;
                            }
                            else if (index == 53)
                            {
                                arrayofbytemaprowm34a = (arrayofbytemaprowm34a * 1000) + currentByte;
                            }
                        }
                        else if (index == 9 || index == 25)
                        {
                            if (index == 9)
                            {
                                arrayofbytemaprowm41a = currentByte;
                            }
                            else if (index == 25)
                            {
                                arrayofbytemaprowm41a = (arrayofbytemaprowm41a * 1000) + currentByte;
                            }
                        }
                        else if (index == 41 || index == 57)
                        {
                            if (index == 41)
                            {
                                arrayofbytemaprowm42a = currentByte;
                            }
                            else if (index == 57)
                            {
                                arrayofbytemaprowm42a = (arrayofbytemaprowm42a * 1000) + currentByte;
                            }
                        }
                        else if (index == 13 || index == 29)
                        {
                            if (index == 13)
                            {
                                arrayofbytemaprowm43a = currentByte;
                            }
                            else if (index == 29)
                            {
                                arrayofbytemaprowm43a = (arrayofbytemaprowm43a * 1000) + currentByte;
                            }
                        }
                        else if (index == 45 || index == 61)
                        {
                            if (index == 45)
                            {
                                arrayofbytemaprowm44a = currentByte;
                            }
                            else if (index == 61)
                            {
                                arrayofbytemaprowm44a = (arrayofbytemaprowm44a * 1000) + currentByte;
                            }
                        }
                        else if (index == 2 || index == 18)
                        {
                            if (index == 2)
                            {
                                arrayofbytemaprowm11b = currentByte;
                            }
                            else if (index == 18)
                            {
                                arrayofbytemaprowm11b = (arrayofbytemaprowm11b * 1000) + currentByte;
                            }
                        }
                        else if (index == 34 || index == 50)
                        {
                            if (index == 34)
                            {
                                arrayofbytemaprowm12b = currentByte;
                            }
                            else if (index == 50)
                            {
                                arrayofbytemaprowm12b = (arrayofbytemaprowm12b * 1000) + currentByte;
                            }
                        }
                        else if (index == 6 || index == 22)
                        {
                            if (index == 6)
                            {
                                arrayofbytemaprowm13b = currentByte;
                            }
                            else if (index == 22)
                            {
                                arrayofbytemaprowm13b = (arrayofbytemaprowm13b * 1000) + currentByte;
                            }
                        }
                        else if (index == 38 || index == 54)
                        {
                            if (index == 38)
                            {
                                arrayofbytemaprowm14b = currentByte;
                            }
                            else if (index == 54)
                            {
                                arrayofbytemaprowm14b = (arrayofbytemaprowm14b * 1000) + currentByte;
                            }
                        }
                        else if (index == 10 || index == 26)
                        {
                            if (index == 10)
                            {
                                arrayofbytemaprowm21b = currentByte;
                            }
                            else if (index == 26)
                            {
                                arrayofbytemaprowm21b = (arrayofbytemaprowm21b * 1000) + currentByte;
                            }
                        }
                        else if (index == 42 || index == 58)
                        {
                            if (index == 42)
                            {
                                arrayofbytemaprowm22b = currentByte;
                            }
                            else if (index == 58)
                            {
                                arrayofbytemaprowm22b = (arrayofbytemaprowm22b * 1000) + currentByte;
                            }
                        }
                        else if (index == 14 || index == 30)
                        {
                            if (index == 14)
                            {
                                arrayofbytemaprowm23b = currentByte;
                            }
                            else if (index == 30)
                            {
                                arrayofbytemaprowm23b = (arrayofbytemaprowm23b * 1000) + currentByte;
                            }
                        }
                        else if (index == 46 || index == 62)
                        {
                            if (index == 46)
                            {
                                arrayofbytemaprowm24b = currentByte;
                            }
                            else if (index == 62)
                            {
                                arrayofbytemaprowm24b = (arrayofbytemaprowm24b * 1000) + currentByte;
                            }
                        }
                        else if (index == 3 || index == 19)
                        {
                            if (index == 3)
                            {
                                arrayofbytemaprowm31b = currentByte;
                            }
                            else if (index == 19)
                            {
                                arrayofbytemaprowm31b = (arrayofbytemaprowm31b * 1000) + currentByte;
                            }
                        }
                        else if (index == 35 || index == 51)
                        {
                            if (index == 35)
                            {
                                arrayofbytemaprowm32b = currentByte;
                            }
                            else if (index == 51)
                            {
                                arrayofbytemaprowm32b = (arrayofbytemaprowm32b * 1000) + currentByte;
                            }
                        }
                        else if (index == 7 || index == 23)
                        {
                            if (index == 7)
                            {
                                arrayofbytemaprowm33b = currentByte;
                            }
                            else if (index == 23)
                            {
                                arrayofbytemaprowm33b = (arrayofbytemaprowm33b * 1000) + currentByte;
                            }
                        }
                        else if (index == 39 || index == 55)
                        {
                            if (index == 39)
                            {
                                arrayofbytemaprowm34b = currentByte;
                            }
                            else if (index == 55)
                            {
                                arrayofbytemaprowm34b = (arrayofbytemaprowm34b * 1000) + currentByte;
                            }
                        }
                        else if (index == 11 || index == 27)
                        {
                            if (index == 11)
                            {
                                arrayofbytemaprowm41b = currentByte;
                            }
                            else if (index == 27)
                            {
                                arrayofbytemaprowm41b = (arrayofbytemaprowm41b * 1000) + currentByte;
                            }
                        }
                        else if (index == 43 || index == 59)
                        {
                            if (index == 43)
                            {
                                arrayofbytemaprowm42b = currentByte;
                            }
                            else if (index == 59)
                            {
                                arrayofbytemaprowm42b = (arrayofbytemaprowm42b * 1000) + currentByte;
                            }
                        }
                        else if (index == 15 || index == 31)
                        {
                            if (index == 15)
                            {
                                arrayofbytemaprowm43b = currentByte;
                            }
                            else if (index == 31)
                            {
                                arrayofbytemaprowm43b = (arrayofbytemaprowm43b * 1000) + currentByte;
                            }
                        }
                        else if (index == 47 || index == 63)
                        {
                            if (index == 47)
                            {
                                arrayofbytemaprowm44b = currentByte;
                            }
                            else if (index == 63)
                            {
                                arrayofbytemaprowm44b = (arrayofbytemaprowm44b * 1000) + currentByte;
                            }
                        }




                        /*
                        if (index == 0 || index == 16)
                        {
                            arrayofbytemaprowm11a = (arrayofbytemaprowm11a * 1000) + currentByte;
                        }
                        else if (index == 32 || index == 48)
                        {
                            arrayofbytemaprowm12a = (arrayofbytemaprowm12a * 1000) + currentByte;
                        }
                        else if (index == 4 || index == 20 )
                        {
                            arrayofbytemaprowm13a = (arrayofbytemaprowm13a * 1000) + currentByte;
                        }
                        else if (index == 36 || index == 52 )
                        {
                            arrayofbytemaprowm14a = (arrayofbytemaprowm14a * 1000) + currentByte;
                        }
                        else if (index == 8 || index == 24 )
                        {
                            arrayofbytemaprowm21a = (arrayofbytemaprowm21a * 1000) + currentByte;
                        }
                        else if (index == 40 || index == 56)
                        {
                            arrayofbytemaprowm22a = (arrayofbytemaprowm22a * 1000) + currentByte;
                        }
                        else if (index == 12 || index == 28 )
                        {
                            arrayofbytemaprowm23a = (arrayofbytemaprowm23a * 1000) + currentByte;
                        }
                        else if ( index == 44 || index == 60 )
                        {
                            arrayofbytemaprowm24a = (arrayofbytemaprowm24a * 1000) + currentByte;
                        }
                        else if (index == 1 || index == 17 )
                        {
                            arrayofbytemaprowm31a = (arrayofbytemaprowm31a * 1000) + currentByte;
                        }
                        else if (index == 33 || index == 49 )
                        {
                            arrayofbytemaprowm32a = (arrayofbytemaprowm32a * 1000) + currentByte;

                        }
                        else if (index == 5 || index == 21 )
                        {
                            arrayofbytemaprowm33a = (arrayofbytemaprowm33a * 1000) + currentByte;
                        }
                        else if (index == 37 || index == 53 )
                        {
                            arrayofbytemaprowm34a = (arrayofbytemaprowm34a * 1000) + currentByte;
                        }
                        else if (index == 9 || index == 25 )
                        {
                            arrayofbytemaprowm41a = (arrayofbytemaprowm41a * 1000) + currentByte;
                        }
                        else if ( index == 41 || index == 57 )
                        {
                            arrayofbytemaprowm42a = (arrayofbytemaprowm42a * 1000) + currentByte;
                        }
                        else if (index == 13 || index == 29)
                        {
                            arrayofbytemaprowm43a = (arrayofbytemaprowm43a * 1000) + currentByte;
                        }
                        else if (index == 45 || index == 61 )
                        {
                            arrayofbytemaprowm44a = (arrayofbytemaprowm44a * 1000) + currentByte;

                        }
                        else if (index == 2 || index == 18 )
                        {
                            arrayofbytemaprowm11b = (arrayofbytemaprowm11b * 1000) + currentByte;

                        }
                        else if (index == 34 || index == 50)
                        {
                            arrayofbytemaprowm12b = (arrayofbytemaprowm12b * 1000) + currentByte;

                        }
                        else if (index == 6 || index == 22 )
                        {
                            arrayofbytemaprowm13b = (arrayofbytemaprowm13b * 1000) + currentByte;

                        }
                        else if (index == 38 || index == 54 )
                        {
                            arrayofbytemaprowm14b = (arrayofbytemaprowm14b * 1000) + currentByte;

                        }
                        else if (index == 10 || index == 26 )
                        {
                            arrayofbytemaprowm21b = (arrayofbytemaprowm21b * 1000) + currentByte;

                        }
                        else if (index == 42 || index == 58 )
                        {
                            arrayofbytemaprowm22b = (arrayofbytemaprowm22b * 1000) + currentByte;

                        }
                        else if (index == 14 || index == 30 )
                        {
                            arrayofbytemaprowm23b = (arrayofbytemaprowm23b * 1000) + currentByte;

                        }
                        else if (index == 46 || index == 62 )
                        {
                            arrayofbytemaprowm24b = (arrayofbytemaprowm24b * 1000) + currentByte;

                        }
                        else if (index == 3 || index == 19)
                        {
                            arrayofbytemaprowm31b = (arrayofbytemaprowm31b * 1000) + currentByte;

                        }
                        else if ( index == 35 || index == 51 )
                        {
                            arrayofbytemaprowm32b = (arrayofbytemaprowm32b * 1000) + currentByte;

                        }
                        else if (index == 7 || index == 23 )
                        {
                            arrayofbytemaprowm33b = (arrayofbytemaprowm33b * 1000) + currentByte;

                        }
                        else if (index == 39 || index == 55 )
                        {
                            arrayofbytemaprowm34b = (arrayofbytemaprowm34b * 1000) + currentByte;

                        }
                        else if (index == 11 || index == 27 )
                        {
                            arrayofbytemaprowm41b = (arrayofbytemaprowm41b * 1000) + currentByte;

                        }
                        else if (index == 43 || index == 59 )
                        {
                            arrayofbytemaprowm42b = (arrayofbytemaprowm42b * 1000) + currentByte;

                        }
                        else if (index == 15 || index == 31)
                        {
                            arrayofbytemaprowm43b = (arrayofbytemaprowm43b * 1000) + currentByte;

                        }
                        else if (index == 47 || index == 63)
                        {
                            arrayofbytemaprowm44b = (arrayofbytemaprowm44b * 1000) + currentByte;

                        }*/

                        /*
                        switch (somecountermul)
                        {
                            case 0:
                                //selectablevectordouble = arrayofbytemaprowm11a;


                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm11a = (arrayofbytemaprowm11a * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm11a = (arrayofbytemaprowm11a * 1000) + currentByte;
                                }

                                m11adder++;
                                break;
                            case 1:
                                //selectablevectordouble = arrayofbytemaprowm12a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm12a = (arrayofbytemaprowm12a * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm12a = (arrayofbytemaprowm12a * 1000) + currentByte;
                                }
                                m12adder++;
                                break;
                            case 2:
                                //selectablevectordouble = arrayofbytemaprowm13a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm13a = (arrayofbytemaprowm13a * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm13a = (arrayofbytemaprowm13a * 1000) + currentByte;
                                }
                                m13adder++;
                                break;
                            case 3:
                                //selectablevectordouble = arrayofbytemaprowm14a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm14a = (arrayofbytemaprowm14a * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm14a = (arrayofbytemaprowm14a * 1000) + currentByte;
                                }
                                m14adder++;
                                break;
                            case 4:
                                //selectablevectordouble = arrayofbytemaprowm21a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm21a = (arrayofbytemaprowm21a * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm21a = (arrayofbytemaprowm21a * 1000) + currentByte;
                                }
                                m21adder++;
                                break;
                            case 5:
                                //selectablevectordouble = arrayofbytemaprowm22a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm22a = (arrayofbytemaprowm22a * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm22a = (arrayofbytemaprowm22a * 1000) + currentByte;
                                }
                                m22adder++;
                                break;
                            case 6:
                                //selectablevectordouble = arrayofbytemaprowm23a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm23a = (arrayofbytemaprowm23a * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm23a = (arrayofbytemaprowm23a * 1000) + currentByte;
                                }
                                m23adder++;
                                break;
                            case 7:
                                //selectablevectordouble = arrayofbytemaprowm24a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm24a = (arrayofbytemaprowm24a * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm24a = (arrayofbytemaprowm24a * 1000) + currentByte;
                                }
                                m24adder++;
                                break;
                            case 8:
                                //selectablevectordouble = arrayofbytemaprowm31a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm31a = (arrayofbytemaprowm31a * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm31a = (arrayofbytemaprowm31a * 1000) + currentByte;
                                }
                                m31adder++;
                                break;
                            case 9:
                                //selectablevectordouble = arrayofbytemaprowm32a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm32a = (arrayofbytemaprowm32a * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm32a = (arrayofbytemaprowm32a * 1000) + currentByte;
                                }
                                m32adder++;

                                break;
                            case 10:
                                //selectablevectordouble = arrayofbytemaprowm33a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm33a = (arrayofbytemaprowm33a * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm33a = (arrayofbytemaprowm33a * 1000) + currentByte;
                                }
                                m33adder++;
                                break;
                            case 11:
                                //selectablevectordouble = arrayofbytemaprowm34a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm34a = (arrayofbytemaprowm34a * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm34a = (arrayofbytemaprowm34a * 1000) + currentByte;
                                }
                                m34adder++;
                                break;
                            case 12:
                                //selectablevectordouble = arrayofbytemaprowm41a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm41a = (arrayofbytemaprowm41a * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm41a = (arrayofbytemaprowm41a * 1000) + currentByte;
                                }
                                m41adder++;
                                break;
                            case 13:
                                //selectablevectordouble = arrayofbytemaprowm42a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm42a = (arrayofbytemaprowm42a * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm42a = (arrayofbytemaprowm42a * 1000) + currentByte;
                                }
                                m42adder++;
                                break;
                            case 14:
                                //selectablevectordouble = arrayofbytemaprowm43a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm43a = (arrayofbytemaprowm43a * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm43a = (arrayofbytemaprowm43a * 1000) + currentByte;
                                }
                                m43adder++;
                                break;
                            case 15:
                                //selectablevectordouble = arrayofbytemaprowm44a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm44a = (arrayofbytemaprowm44a * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm44a = (arrayofbytemaprowm44a * 1000) + currentByte;
                                }
                                m44adder++;
                                break;




                            case 16:
                                //selectablevectorbouble = arrayofbytemaprowm11a;


                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm11b = (arrayofbytemaprowm11b * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm11b = (arrayofbytemaprowm11b * 1000) + currentByte;
                                }
                                m11badder++;
                                break;
                            case 17:
                                //selectablevectorbouble = arrayofbytemaprowm12a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm12b = (arrayofbytemaprowm12b * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm12b = (arrayofbytemaprowm12b * 1000) + currentByte;
                                }
                                m12badder++;
                                break;
                            case 18:
                                //selectablevectorbouble = arrayofbytemaprowm13a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm13b = (arrayofbytemaprowm13b * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm13b = (arrayofbytemaprowm13b * 1000) + currentByte;
                                }
                                m13badder++;
                                break;
                            case 19:
                                //selectablevectorbouble = arrayofbytemaprowm14a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm14b = (arrayofbytemaprowm14b * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm14b = (arrayofbytemaprowm14b * 1000) + currentByte;
                                }
                                m14badder++;
                                break;
                            case 20:
                                //selectablevectorbouble = arrayofbytemaprowm21a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm21b = (arrayofbytemaprowm21b * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm21b = (arrayofbytemaprowm21b * 1000) + currentByte;
                                }
                                m21badder++;
                                break;
                            case 21:
                                //selectablevectorbouble = arrayofbytemaprowm22a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm22b = (arrayofbytemaprowm22b * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm22b = (arrayofbytemaprowm22b * 1000) + currentByte;
                                }
                                m22badder++;
                                break;
                            case 22:
                                //selectablevectorbouble = arrayofbytemaprowm23a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm23b = (arrayofbytemaprowm23b * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm23b = (arrayofbytemaprowm23b * 1000) + currentByte;
                                }
                                m23badder++;
                                break;
                            case 23:
                                //selectablevectorbouble = arrayofbytemaprowm24a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm24b = (arrayofbytemaprowm24b * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm24b = (arrayofbytemaprowm24b * 1000) + currentByte;
                                }
                                m24badder++;
                                break;
                            case 24:
                                //selectablevectorbouble = arrayofbytemaprowm31a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm31b = (arrayofbytemaprowm31b * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm31b = (arrayofbytemaprowm31b * 1000) + currentByte;
                                }
                                m31badder++;
                                break;
                            case 25:
                                //selectablevectorbouble = arrayofbytemaprowm32a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm32b = (arrayofbytemaprowm32b * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm32b = (arrayofbytemaprowm32b * 1000) + currentByte;
                                }
                                m32badder++;
                                break;
                            case 26:
                                //selectablevectorbouble = arrayofbytemaprowm33a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm33b = (arrayofbytemaprowm33b * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm33b = (arrayofbytemaprowm33b * 1000) + currentByte;
                                }
                                m33badder++;
                                break;
                            case 27:
                                //selectablevectorbouble = arrayofbytemaprowm34a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm34b = (arrayofbytemaprowm34b * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm34b = (arrayofbytemaprowm34b * 1000) + currentByte;
                                }
                                m34badder++;
                                break;
                            case 28:
                                //selectablevectorbouble = arrayofbytemaprowm41a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm41b = (arrayofbytemaprowm41b * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm41b = (arrayofbytemaprowm41b * 1000) + currentByte;
                                }
                                m41badder++;
                                break;
                            case 29:
                                //selectablevectorbouble = arrayofbytemaprowm42a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm42b = (arrayofbytemaprowm42b * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm42b = (arrayofbytemaprowm42b * 1000) + currentByte;
                                }
                                m42badder++;
                                break;
                            case 30:
                                //selectablevectorbouble = arrayofbytemaprowm43a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm43b = (arrayofbytemaprowm43b * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm43b = (arrayofbytemaprowm43b * 1000) + currentByte;
                                }
                                m43badder++;
                                break;
                            case 31:
                                //selectablevectorbouble = arrayofbytemaprowm44a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm44b = (arrayofbytemaprowm44b * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm44b = (arrayofbytemaprowm44b * 1000) + currentByte;
                                }
                                m44badder++;
                                break;








                            case 32:
                                //selectablevectorcouble = arrayofbytemaprowm11a;


                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm11c = (arrayofbytemaprowm11c * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm11c = (arrayofbytemaprowm11c * 1000) + currentByte;
                                }
                                m11cadder++;
                                break;
                            case 33:
                                //selectablevectorcouble = arrayofbytemaprowm12a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm12c = (arrayofbytemaprowm12c * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm12c = (arrayofbytemaprowm12c * 1000) + currentByte;
                                }
                                m12cadder++;
                                break;
                            case 34:
                                //selectablevectorcouble = arrayofbytemaprowm13a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm13c = (arrayofbytemaprowm13c * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm13c = (arrayofbytemaprowm13c * 1000) + currentByte;
                                }
                                m13cadder++;
                                break;
                            case 35:
                                //selectablevectorcouble = arrayofbytemaprowm14a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm14c = (arrayofbytemaprowm14c * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm14c = (arrayofbytemaprowm14c * 1000) + currentByte;
                                }
                                m14cadder++;
                                break;
                            case 36:
                                //selectablevectorcouble = arrayofbytemaprowm21a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm21c = (arrayofbytemaprowm21c * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm21c = (arrayofbytemaprowm21c * 1000) + currentByte;
                                }
                                m21cadder++;
                                break;
                            case 37:
                                //selectablevectorcouble = arrayofbytemaprowm22a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm22c = (arrayofbytemaprowm22c * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm22c = (arrayofbytemaprowm22c * 1000) + currentByte;
                                }
                                m22cadder++;
                                break;
                            case 38:
                                //selectablevectorcouble = arrayofbytemaprowm23a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm23c = (arrayofbytemaprowm23c * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm23c = (arrayofbytemaprowm23c * 1000) + currentByte;
                                }
                                m23cadder++;
                                break;
                            case 39:
                                //selectablevectorcouble = arrayofbytemaprowm24a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm24c = (arrayofbytemaprowm24c * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm24c = (arrayofbytemaprowm24c * 1000) + currentByte;
                                }
                                m24cadder++;
                                break;
                            case 40:
                                //selectablevectorcouble = arrayofbytemaprowm31a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm31c = (arrayofbytemaprowm31c * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm31c = (arrayofbytemaprowm31c * 1000) + currentByte;
                                }
                                m31cadder++;
                                break;
                            case 41:
                                //selectablevectorcouble = arrayofbytemaprowm32a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm32c = (arrayofbytemaprowm32c * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm32c = (arrayofbytemaprowm32c * 1000) + currentByte;
                                }
                                m32cadder++;
                                break;
                            case 42:
                                //selectablevectorcouble = arrayofbytemaprowm33a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm33c = (arrayofbytemaprowm33c * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm33c = (arrayofbytemaprowm33c * 1000) + currentByte;
                                }
                                m33cadder++;
                                break;
                            case 43:
                                //selectablevectorcouble = arrayofbytemaprowm34a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm34c = (arrayofbytemaprowm34c * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm34c = (arrayofbytemaprowm34c * 1000) + currentByte;
                                }
                                m34cadder++;
                                break;
                            case 44:
                                //selectablevectorcouble = arrayofbytemaprowm41a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm41c = (arrayofbytemaprowm41c * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm41c = (arrayofbytemaprowm41c * 1000) + currentByte;
                                }
                                m41cadder++;
                                break;
                            case 45:
                                //selectablevectorcouble = arrayofbytemaprowm42a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm42c = (arrayofbytemaprowm42c * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm42c = (arrayofbytemaprowm42c * 1000) + currentByte;
                                }
                                m42cadder++;
                                break;
                            case 46:
                                //selectablevectorcouble = arrayofbytemaprowm43a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm43c = (arrayofbytemaprowm43c * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm43c = (arrayofbytemaprowm43c * 1000) + currentByte;
                                }
                                m43cadder++;
                                break;
                            case 47:
                                //selectablevectorcouble = arrayofbytemaprowm44a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm44c = (arrayofbytemaprowm44c * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm44c = (arrayofbytemaprowm44c * 1000) + currentByte;
                                }
                                m44cadder++;
                                break;







                            case 48:
                                //selectablevectordouble = arrayofbytemaprowm11a;


                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm11d = (arrayofbytemaprowm11d * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm11d = (arrayofbytemaprowm11d * 1000) + currentByte;
                                }
                                m11dadder++;
                                break;
                            case 49:
                                //selectablevectordouble = arrayofbytemaprowm12a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm12d = (arrayofbytemaprowm12d * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm12d = (arrayofbytemaprowm12d * 1000) + currentByte;
                                }
                                m12dadder++;
                                break;
                            case 50:
                                //selectablevectordouble = arrayofbytemaprowm13a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm13d = (arrayofbytemaprowm13d * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm13d = (arrayofbytemaprowm13d * 1000) + currentByte;
                                }
                                m13dadder++;
                                break;
                            case 51:
                                //selectablevectordouble = arrayofbytemaprowm14a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm14d = (arrayofbytemaprowm14d * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm14d = (arrayofbytemaprowm14d * 1000) + currentByte;
                                }
                                m14dadder++;
                                break;
                            case 52:
                                //selectablevectordouble = arrayofbytemaprowm21a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm21d = (arrayofbytemaprowm21d * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm21d = (arrayofbytemaprowm21d * 1000) + currentByte;
                                }
                                m21dadder++;
                                break;
                            case 53:
                                //selectablevectordouble = arrayofbytemaprowm22a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm22d = (arrayofbytemaprowm22d * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm22d = (arrayofbytemaprowm22d * 1000) + currentByte;
                                }
                                m22dadder++;
                                break;
                            case 54:
                                //selectablevectordouble = arrayofbytemaprowm23a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm23d = (arrayofbytemaprowm23d * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm23d = (arrayofbytemaprowm23d * 1000) + currentByte;
                                }
                                m23dadder++;

                                break;
                            case 55:
                                //selectablevectordouble = arrayofbytemaprowm24a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm24d = (arrayofbytemaprowm24d * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm24d = (arrayofbytemaprowm24d * 1000) + currentByte;
                                }
                                m24dadder++;
                                break;
                            case 56:
                                //selectablevectordouble = arrayofbytemaprowm31a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm31d = (arrayofbytemaprowm31d * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm31d = (arrayofbytemaprowm31d * 1000) + currentByte;
                                }
                                m31dadder++;
                                break;
                            case 57:
                                //selectablevectordouble = arrayofbytemaprowm32a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm32d = (arrayofbytemaprowm32d * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm32d = (arrayofbytemaprowm32d * 1000) + currentByte;
                                }
                                m32dadder++;
                                break;
                            case 58:
                                //selectablevectordouble = arrayofbytemaprowm33a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm33d = (arrayofbytemaprowm33d * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm33d = (arrayofbytemaprowm33d * 1000) + currentByte;
                                }
                                m33dadder++;
                                break;
                            case 59:
                                //selectablevectordouble = arrayofbytemaprowm34a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm34d = (arrayofbytemaprowm34d * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm34d = (arrayofbytemaprowm34d * 1000) + currentByte;
                                }
                                m34dadder++;
                                break;
                            case 60:
                                //selectablevectordouble = arrayofbytemaprowm41a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm41d = (arrayofbytemaprowm41d * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm41d = (arrayofbytemaprowm41d * 1000) + currentByte;
                                }
                                m41dadder++;
                                break;
                            case 61:
                                //selectablevectordouble = arrayofbytemaprowm42a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm42d = (arrayofbytemaprowm42d * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm42d = (arrayofbytemaprowm42d * 1000) + currentByte;
                                }
                                m42dadder++;
                                break;
                            case 62:
                                //selectablevectordouble = arrayofbytemaprowm43a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm43d = (arrayofbytemaprowm43d * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm43d = (arrayofbytemaprowm43d * 1000) + currentByte;
                                }
                                m43dadder++;
                                break;
                            case 63:
                                //selectablevectordouble = arrayofbytemaprowm44a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm44d = (arrayofbytemaprowm44d * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm44d = (arrayofbytemaprowm44d * 1000) + currentByte;
                                }
                                m44dadder++;
                                break;
                        };*/
                    }
                }
            }







            m11 = arrayofbytemaprowm11a;
            m12 = arrayofbytemaprowm12a;
            m13 = arrayofbytemaprowm13a;
            m14 = arrayofbytemaprowm14a;
            m21 = arrayofbytemaprowm21a;
            m22 = arrayofbytemaprowm22a;
            m23 = arrayofbytemaprowm23a;
            m24 = arrayofbytemaprowm24a;
            m31 = arrayofbytemaprowm31a;
            m32 = arrayofbytemaprowm32a;
            m33 = arrayofbytemaprowm33a;
            m34 = arrayofbytemaprowm34a;
            m41 = arrayofbytemaprowm41a;
            m42 = arrayofbytemaprowm42a;
            m43 = arrayofbytemaprowm43a;
            m44 = arrayofbytemaprowm44a;


            m11b = arrayofbytemaprowm11b;
            m12b = arrayofbytemaprowm12b;
            m13b = arrayofbytemaprowm13b;
            m14b = arrayofbytemaprowm14b;

            m21b = arrayofbytemaprowm21b;
            m22b = arrayofbytemaprowm22b;
            m23b = arrayofbytemaprowm23b;
            m24b = arrayofbytemaprowm24b;

            m31b = arrayofbytemaprowm31b;
            m32b = arrayofbytemaprowm32b;
            m33b = arrayofbytemaprowm33b;
            m34b = arrayofbytemaprowm34b;

            m41b = arrayofbytemaprowm41b;
            m42b = arrayofbytemaprowm42b;
            m43b = arrayofbytemaprowm43b;
            m44b = arrayofbytemaprowm44b;

            /*
            m11c = arrayofbytemaprowm11c;
            m12c = arrayofbytemaprowm12c;
            m13c = arrayofbytemaprowm13c;
            m14c = arrayofbytemaprowm14c;

            m21c = arrayofbytemaprowm21c;
            m22c = arrayofbytemaprowm22c;
            m23c = arrayofbytemaprowm23c;
            m24c = arrayofbytemaprowm24c;

            m31c = arrayofbytemaprowm31c;
            m32c = arrayofbytemaprowm32c;
            m33c = arrayofbytemaprowm33c;
            m34c = arrayofbytemaprowm34c;

            m41c = arrayofbytemaprowm41c;
            m42c = arrayofbytemaprowm42c;
            m43c = arrayofbytemaprowm43c;
            m44c = arrayofbytemaprowm44c;

            m11d = arrayofbytemaprowm11d;
            m12d = arrayofbytemaprowm12d;
            m13d = arrayofbytemaprowm13d;
            m14d = arrayofbytemaprowm14d;

            m21d = arrayofbytemaprowm21d;
            m22d = arrayofbytemaprowm22d;
            m23d = arrayofbytemaprowm23d;
            m24d = arrayofbytemaprowm24d;

            m31d = arrayofbytemaprowm31d;
            m32d = arrayofbytemaprowm32d;
            m33d = arrayofbytemaprowm33d;
            m34d = arrayofbytemaprowm34d;

            m41d = arrayofbytemaprowm41d;
            m42d = arrayofbytemaprowm42d;
            m43d = arrayofbytemaprowm43d;
            m44d = arrayofbytemaprowm44d;*/




            /*
            if (arrayofbytemaprowm11a != 1000000000.0)
            {
                //Console.WriteLine(arrayofbytemaprowm11a);
            }
            */


            mapvertx = null;
            mapverty = null;
            mapvertz = null;

            vertminz++;
            if (vertminz >= 2)
            {
                vertminy++;
                vertminz = 0;
            }

            if (vertminy >= 2)
            {
                vertminx++;
                vertminy = 0;
            }

            if (vertminx >= 2)
            {
                vertminx = 0;
            }

        }





        //int[] somenewdimswidth;
        //int[] somenewdimsheight;
        //int[] somenewdimsdepth;

        int dimsminx = 0;
        int dimsminy = 0;
        int dimsminz = 0;
        int dimsmaxx = 64;
        int dimsmaxy = 64;
        int dimsmaxz = 64;

        int dimsrealx = 0;
        int dimsrealy = 0;
        int dimsrealz = 0;

        //int maxveclengthalt;
        public void insertdimensionsinint(int minx, int miny, int minz, int maxx, int maxy, int maxz,int typeofterraintiles_, tutorialcubeaschunkinst componentparent_, int levelofdetail_, int maxveclengthalt, int facetype,
           out double m11, out double m12, out double m13, out double m14,
           out double m21, out double m22, out double m23, out double m24, out double m31, out double m32, out double m33, out double m34, out double m41, out double m42, out double m43, out double m44,

           out double m11b, out double m12b, out double m13b, out double m14b,
           out double m21b, out double m22b, out double m23b, out double m24b, out double m31b, out double m32b, out double m33b, out double m34b, out double m41b, out double m42b, out double m43b, out double m44b)// , int somechunkkeyboardpriminstanceindex_, int chunkprimindex_, int chunkinstindex_
        {

            int[] mapdimsx = null;
            int[] mapdimsy = null;
            int[] mapdimsz = null;



            mapdimsx = widthdimtop;
            mapdimsy = heightdimtop;
            mapdimsz = depthdimtop;

            /* if (facetype == 0)
             {
                 mapdimsx = widthdimtop;
                 mapdimsy = heightdimtop;
                 mapdimsz = depthdimtop;

             }
             else if (facetype == 1)
             {
                 mapdimsx = widthdimleft;
                 mapdimsy = heightdimleft;
                 mapdimsz = depthdimleft;
             }
             else if (facetype == 2)
             {
                 mapdimsx = widthdimright;
                 mapdimsy = heightdimright;
                 mapdimsz = depthdimright;
             }
             else if (facetype == 3)
             {
                 mapdimsx = widthdimfront;
                 mapdimsy = heightdimfront;
                 mapdimsz = depthdimfront;
             }
             else if (facetype == 4)
             {
                 mapdimsx = widthdimback;
                 mapdimsy = heightdimback;
                 mapdimsz = depthdimback;
             }
             else if (facetype == 5)
             {
                 mapdimsx = widthdimbottom;
                 mapdimsy = heightdimbottom;
                 mapdimsz = depthdimbottom;
             }
            */



            /*List<int> somedata = new List<int>();

            somenewdimswidth = new int[map.Length];
            somenewdimsheight = new int[map.Length];
            somenewdimsdepth = new int[map.Length];
            */
            int somecounter = 0;
            /*for (int i = 0; i < mapvertindexfordims.Length; i++)
            {
                if (mapvertindexfordims[i] == 1)
                {
                    somenewdimswidth[somecounter] = widthdim[i];
                    somenewdimsheight[somecounter] = heightdim[i];
                    somenewdimsdepth[somecounter] = depthdim[i];

                    somedata.Add(i);
                    somecounter++;
                }
            }*/
            /*
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        int index = x + width * (y + height * z);

                        if (mapvertindexfordims[index] != 0)
                        {
                            //somenewdimswidth[somecounter] = widthdim[i];
                            //somenewdimsheight[somecounter] = heightdim[i];
                            //somenewdimsdepth[somecounter] = depthdim[i];
                            somedata.Add(index);
                            somecounter++;
                        }
                    }
                }
            }

            int someothercounter = 0;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        int index = x + width * (y + height * z);

                        ////Console.WriteLine(index);
                        if (someothercounter < somedata.Count)
                        {
                            somenewdimswidth[index] = widthdim[somedata[someothercounter]];
                            somenewdimsheight[index] = heightdim[somedata[someothercounter]];
                            somenewdimsdepth[index] = depthdim[somedata[someothercounter]];
                        }
                        someothercounter++;
                    }
                }
            }*/






            //for 4 by 4 by 4 and placing 8 digits in a float.
            //0-4-1-5-2-6-3-7
            //8-12-9-13-10-14-11-15
            //16-20-17-21-18-22-19-23
            //24-28-25-29-26-30-27-31
            //32-36-33-37-34-38-35-39
            //40-44-41-45-42-46-43-47
            //48-52-49-53-50-54-51-55
            //56-60-57-61-58-62-59-63

            //0 16 32 48
            //4 20 36 52
            //8 24 40 56
            //12 28 44 60
            //1 17 33 49
            //5 21 37 53
            //9 25 41 57
            //13 29 45 61
            //2 18 34 50
            //6 22 38 54 
            //10 26 42 58 
            //14 30 46 62
            //3 19 35 51
            //7 23 39 55
            //11 27 43 59
            //15 31 47 63



            //somedata = null;







            double arrayofbytemaprowm11a = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm12a = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm13a = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm14a = Program.paddingformaps;// 1; //111111111111111111111111111

            double arrayofbytemaprowm21a = Program.paddingformaps;// 1; //111111111111111111111111111 
            double arrayofbytemaprowm22a = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm23a = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm24a = Program.paddingformaps;// 1; //111111111111111111111111111

            double arrayofbytemaprowm31a = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm32a = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm33a = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm34a = Program.paddingformaps;// 1; //111111111111111111111111111

            double arrayofbytemaprowm41a = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm42a = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm43a = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm44a = Program.paddingformaps;// 1; //111111111111111111111111111

            double arrayofbytemaprowm11b = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm12b = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm13b = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm14b = Program.paddingformaps;// 1; //111111111111111111111111111

            double arrayofbytemaprowm21b = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm22b = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm23b = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm24b = Program.paddingformaps;// 1; //111111111111111111111111111

            double arrayofbytemaprowm31b = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm32b = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm33b = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm34b = Program.paddingformaps;// 1; //111111111111111111111111111

            double arrayofbytemaprowm41b = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm42b = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm43b = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm44b = Program.paddingformaps;// 1; //111111111111111111111111111

            double arrayofbytemaprowm11c = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm12c = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm13c = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm14c = Program.paddingformaps;// 1; //111111111111111111111111111

            double arrayofbytemaprowm21c = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm22c = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm23c = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm24c = Program.paddingformaps;// 1; //111111111111111111111111111

            double arrayofbytemaprowm31c = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm32c = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm33c = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm34c = Program.paddingformaps;// 1; //111111111111111111111111111

            double arrayofbytemaprowm41c = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm42c = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm43c = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm44c = Program.paddingformaps;// 1; //111111111111111111111111111

            double arrayofbytemaprowm11d = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm12d = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm13d = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm14d = Program.paddingformaps;// 1; //111111111111111111111111111

            double arrayofbytemaprowm21d = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm22d = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm23d = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm24d = Program.paddingformaps;// 1; //111111111111111111111111111

            double arrayofbytemaprowm31d = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm32d = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm33d = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm34d = Program.paddingformaps;// 1; //111111111111111111111111111

            double arrayofbytemaprowm41d = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm42d = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm43d = Program.paddingformaps;// 1; //111111111111111111111111111
            double arrayofbytemaprowm44d = Program.paddingformaps;// 1; //111111111111111111111111111



            int maxv = width * height * depth;



            int m11adder = 0;
            int m12adder = 0;
            int m13adder = 0;
            int m14adder = 0;

            int m21adder = 0;
            int m22adder = 0;
            int m23adder = 0;
            int m24adder = 0;

            int m31adder = 0;
            int m32adder = 0;
            int m33adder = 0;
            int m34adder = 0;

            int m41adder = 0;
            int m42adder = 0;
            int m43adder = 0;
            int m44adder = 0;


            int m11badder = 0;
            int m12badder = 0;
            int m13badder = 0;
            int m14badder = 0;

            int m21badder = 0;
            int m22badder = 0;
            int m23badder = 0;
            int m24badder = 0;

            int m31badder = 0;
            int m32badder = 0;
            int m33badder = 0;
            int m34badder = 0;

            int m41badder = 0;
            int m42badder = 0;
            int m43badder = 0;
            int m44badder = 0;



            int m11cadder = 0;
            int m12cadder = 0;
            int m13cadder = 0;
            int m14cadder = 0;

            int m21cadder = 0;
            int m22cadder = 0;
            int m23cadder = 0;
            int m24cadder = 0;

            int m31cadder = 0;
            int m32cadder = 0;
            int m33cadder = 0;
            int m34cadder = 0;

            int m41cadder = 0;
            int m42cadder = 0;
            int m43cadder = 0;
            int m44cadder = 0;


            int m11dadder = 0;
            int m12dadder = 0;
            int m13dadder = 0;
            int m14dadder = 0;

            int m21dadder = 0;
            int m22dadder = 0;
            int m23dadder = 0;
            int m24dadder = 0;

            int m31dadder = 0;
            int m32dadder = 0;
            int m33dadder = 0;
            int m34dadder = 0;

            int m41dadder = 0;
            int m42dadder = 0;
            int m43dadder = 0;
            int m44dadder = 0;




            //512 nope... 0 to 64 when using map of 
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    for (int z = 0; z < 4; z++)
                    {
                        //int index = x + (width * (y + (height * z)));
                        ////Console.WriteLine("index:" + index);
                        //int currentByte = map[index];

                        //10*4*4


                        int index = x + 4 * (y + 4 * z);//; //x + width * (y + height * z);//
                        int currentByte;// = map[index];

                        int somewidth = mapdimsx[index];// widthdim[index];
                        int someheight = mapdimsy[index];
                        int somedepth = mapdimsz[index];// depthdim[index];

                        currentByte = (((somewidth * 10) + (someheight)) * 10) + somedepth;

                        ////Console.WriteLine(currentByte);


                        //Console.Write(" " + index);

                        /*
                        int somemaxvecdigit = maxveclengthalt /2;
                        int somecountermul = 0;
                        int somec = 0;

                        //3 

                        for (int t = 0; t <= index; t++) // index == 45 == 11 
                        {
                            if (somec == somemaxvecdigit)
                            {
                                somecountermul++;
                                somec = 0;
                            }
                            somec++;
                        }*/





                        if (index == 0 || index == 16)
                        {
                            if (index == 0)
                            {
                                arrayofbytemaprowm11a = currentByte;
                            }
                            else if (index == 16)
                            {
                                arrayofbytemaprowm11a = (arrayofbytemaprowm11a * 1000) + currentByte;
                                ////Console.WriteLine(arrayofbytemaprowm11a);
                            }
                        }
                        else if (index == 32 || index == 48)
                        {
                            if (index == 32)
                            {
                                arrayofbytemaprowm12a = currentByte;
                            }
                            else if (index == 48)
                            {
                                arrayofbytemaprowm12a = (arrayofbytemaprowm12a * 1000) + currentByte;
                            }
                        }
                        else if (index == 4 || index == 20)
                        {
                            if (index == 4)
                            {
                                arrayofbytemaprowm13a = currentByte;
                            }
                            else if (index == 20)
                            {
                                arrayofbytemaprowm13a = (arrayofbytemaprowm13a * 1000) + currentByte;
                            }
                        }
                        else if (index == 36 || index == 52)
                        {
                            if (index == 36)
                            {
                                arrayofbytemaprowm14a = currentByte;
                            }
                            else if (index == 52)
                            {
                                arrayofbytemaprowm14a = (arrayofbytemaprowm14a * 1000) + currentByte;
                            }
                        }
                        else if (index == 8 || index == 24)
                        {
                            if (index == 8)
                            {
                                arrayofbytemaprowm21a = currentByte;
                            }
                            else if (index == 24)
                            {
                                arrayofbytemaprowm21a = (arrayofbytemaprowm21a * 1000) + currentByte;
                            }
                        }
                        else if (index == 40 || index == 56)
                        {
                            if (index == 40)
                            {
                                arrayofbytemaprowm22a = currentByte;
                            }
                            else if (index == 56)
                            {
                                arrayofbytemaprowm22a = (arrayofbytemaprowm22a * 1000) + currentByte;
                            }
                        }
                        else if (index == 12 || index == 28)
                        {
                            if (index == 12)
                            {
                                arrayofbytemaprowm23a = currentByte;
                            }
                            else if (index == 28)
                            {
                                arrayofbytemaprowm23a = (arrayofbytemaprowm23a * 1000) + currentByte;
                            }
                        }
                        else if (index == 44 || index == 60)
                        {
                            if (index == 44)
                            {
                                arrayofbytemaprowm24a = currentByte;
                            }
                            else if (index == 60)
                            {
                                arrayofbytemaprowm24a = (arrayofbytemaprowm24a * 1000) + currentByte;
                            }
                        }
                        else if (index == 1 || index == 17)
                        {
                            if (index == 1)
                            {
                                arrayofbytemaprowm31a = currentByte;
                            }
                            else if (index == 17)
                            {
                                arrayofbytemaprowm31a = (arrayofbytemaprowm31a * 1000) + currentByte;
                            }
                        }
                        else if (index == 33 || index == 49)
                        {
                            if (index == 33)
                            {
                                arrayofbytemaprowm32a = currentByte;
                            }
                            else if (index == 49)
                            {
                                arrayofbytemaprowm32a = (arrayofbytemaprowm32a * 1000) + currentByte;
                            }
                        }
                        else if (index == 5 || index == 21)
                        {
                            if (index == 5)
                            {
                                arrayofbytemaprowm33a = currentByte;
                            }
                            else if (index == 21)
                            {
                                arrayofbytemaprowm33a = (arrayofbytemaprowm33a * 1000) + currentByte;
                            }
                        }
                        else if (index == 37 || index == 53)
                        {
                            if (index == 37)
                            {
                                arrayofbytemaprowm34a = currentByte;
                            }
                            else if (index == 53)
                            {
                                arrayofbytemaprowm34a = (arrayofbytemaprowm34a * 1000) + currentByte;
                            }
                        }
                        else if (index == 9 || index == 25)
                        {
                            if (index == 9)
                            {
                                arrayofbytemaprowm41a = currentByte;
                            }
                            else if (index == 25)
                            {
                                arrayofbytemaprowm41a = (arrayofbytemaprowm41a * 1000) + currentByte;
                            }
                        }
                        else if (index == 41 || index == 57)
                        {
                            if (index == 41)
                            {
                                arrayofbytemaprowm42a = currentByte;
                            }
                            else if (index == 57)
                            {
                                arrayofbytemaprowm42a = (arrayofbytemaprowm42a * 1000) + currentByte;
                            }
                        }
                        else if (index == 13 || index == 29)
                        {
                            if (index == 13)
                            {
                                arrayofbytemaprowm43a = currentByte;
                            }
                            else if (index == 29)
                            {
                                arrayofbytemaprowm43a = (arrayofbytemaprowm43a * 1000) + currentByte;
                            }
                        }
                        else if (index == 45 || index == 61)
                        {
                            if (index == 45)
                            {
                                arrayofbytemaprowm44a = currentByte;
                            }
                            else if (index == 61)
                            {
                                arrayofbytemaprowm44a = (arrayofbytemaprowm44a * 1000) + currentByte;
                            }
                        }
                        else if (index == 2 || index == 18)
                        {
                            if (index == 2)
                            {
                                arrayofbytemaprowm11b = currentByte;
                            }
                            else if (index == 18)
                            {
                                arrayofbytemaprowm11b = (arrayofbytemaprowm11b * 1000) + currentByte;
                            }
                        }
                        else if (index == 34 || index == 50)
                        {
                            if (index == 34)
                            {
                                arrayofbytemaprowm12b = currentByte;
                            }
                            else if (index == 50)
                            {
                                arrayofbytemaprowm12b = (arrayofbytemaprowm12b * 1000) + currentByte;
                            }
                        }
                        else if (index == 6 || index == 22)
                        {
                            if (index == 6)
                            {
                                arrayofbytemaprowm13b = currentByte;
                            }
                            else if (index == 22)
                            {
                                arrayofbytemaprowm13b = (arrayofbytemaprowm13b * 1000) + currentByte;
                            }
                        }
                        else if (index == 38 || index == 54)
                        {
                            if (index == 38)
                            {
                                arrayofbytemaprowm14b = currentByte;
                            }
                            else if (index == 54)
                            {
                                arrayofbytemaprowm14b = (arrayofbytemaprowm14b * 1000) + currentByte;
                            }
                        }
                        else if (index == 10 || index == 26)
                        {
                            if (index == 10)
                            {
                                arrayofbytemaprowm21b = currentByte;
                            }
                            else if (index == 26)
                            {
                                arrayofbytemaprowm21b = (arrayofbytemaprowm21b * 1000) + currentByte;
                            }
                        }
                        else if (index == 42 || index == 58)
                        {
                            if (index == 42)
                            {
                                arrayofbytemaprowm22b = currentByte;
                            }
                            else if (index == 58)
                            {
                                arrayofbytemaprowm22b = (arrayofbytemaprowm22b * 1000) + currentByte;
                            }
                        }
                        else if (index == 14 || index == 30)
                        {
                            if (index == 14)
                            {
                                arrayofbytemaprowm23b = currentByte;
                            }
                            else if (index == 30)
                            {
                                arrayofbytemaprowm23b = (arrayofbytemaprowm23b * 1000) + currentByte;
                            }
                        }
                        else if (index == 46 || index == 62)
                        {
                            if (index == 46)
                            {
                                arrayofbytemaprowm24b = currentByte;
                            }
                            else if (index == 62)
                            {
                                arrayofbytemaprowm24b = (arrayofbytemaprowm24b * 1000) + currentByte;
                            }
                        }
                        else if (index == 3 || index == 19)
                        {
                            if (index == 3)
                            {
                                arrayofbytemaprowm31b = currentByte;
                            }
                            else if (index == 19)
                            {
                                arrayofbytemaprowm31b = (arrayofbytemaprowm31b * 1000) + currentByte;
                            }
                        }
                        else if (index == 35 || index == 51)
                        {
                            if (index == 35)
                            {
                                arrayofbytemaprowm32b = currentByte;
                            }
                            else if (index == 51)
                            {
                                arrayofbytemaprowm32b = (arrayofbytemaprowm32b * 1000) + currentByte;
                            }
                        }
                        else if (index == 7 || index == 23)
                        {
                            if (index == 7)
                            {
                                arrayofbytemaprowm33b = currentByte;
                            }
                            else if (index == 23)
                            {
                                arrayofbytemaprowm33b = (arrayofbytemaprowm33b * 1000) + currentByte;
                            }
                        }
                        else if (index == 39 || index == 55)
                        {
                            if (index == 39)
                            {
                                arrayofbytemaprowm34b = currentByte;
                            }
                            else if (index == 55)
                            {
                                arrayofbytemaprowm34b = (arrayofbytemaprowm34b * 1000) + currentByte;
                            }
                        }
                        else if (index == 11 || index == 27)
                        {
                            if (index == 11)
                            {
                                arrayofbytemaprowm41b = currentByte;
                            }
                            else if (index == 27)
                            {
                                arrayofbytemaprowm41b = (arrayofbytemaprowm41b * 1000) + currentByte;
                            }
                        }
                        else if (index == 43 || index == 59)
                        {
                            if (index == 43)
                            {
                                arrayofbytemaprowm42b = currentByte;
                            }
                            else if (index == 59)
                            {
                                arrayofbytemaprowm42b = (arrayofbytemaprowm42b * 1000) + currentByte;
                            }
                        }
                        else if (index == 15 || index == 31)
                        {
                            if (index == 15)
                            {
                                arrayofbytemaprowm43b = currentByte;
                            }
                            else if (index == 31)
                            {
                                arrayofbytemaprowm43b = (arrayofbytemaprowm43b * 1000) + currentByte;
                            }
                        }
                        else if (index == 47 || index == 63)
                        {
                            if (index == 47)
                            {
                                arrayofbytemaprowm44b = currentByte;
                            }
                            else if (index == 63)
                            {
                                arrayofbytemaprowm44b = (arrayofbytemaprowm44b * 1000) + currentByte;
                            }
                        }









                        /*
                        if (index == 0 || index == 16)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm11a = (arrayofbytemaprowm11a * 1000);
                            }
                            else
                            {
                                arrayofbytemaprowm11a = (arrayofbytemaprowm11a * 1000) + currentByte;
                            }
                        }
                        else if (index == 32 || index == 48)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm12a = (arrayofbytemaprowm12a * 1000);
                            }
                            else
                            {
                                arrayofbytemaprowm12a = (arrayofbytemaprowm12a * 1000) + currentByte;
                            }
                        }
                        else if (index == 4 || index == 20)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm13a = (arrayofbytemaprowm13a * 1000);
                            }
                            else
                            {
                                arrayofbytemaprowm13a = (arrayofbytemaprowm13a * 1000) + currentByte;
                            }
                        }
                        else if (index == 36 || index == 52)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm14a = (arrayofbytemaprowm14a * 1000);
                            }
                            else
                            {
                                arrayofbytemaprowm14a = (arrayofbytemaprowm14a * 1000) + currentByte;
                            }
                        }
                        else if (index == 8 || index == 24)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm21a = (arrayofbytemaprowm21a * 1000);
                            }
                            else
                            {
                                arrayofbytemaprowm21a = (arrayofbytemaprowm21a * 1000) + currentByte;
                            }
                        }
                        else if (index == 40 || index == 56)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm22a = (arrayofbytemaprowm22a * 1000);
                            }
                            else
                            {
                                arrayofbytemaprowm22a = (arrayofbytemaprowm22a * 1000) + currentByte;
                            }
                        }
                        else if (index == 12 || index == 28)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm23a = (arrayofbytemaprowm23a * 1000);
                            }
                            else
                            {
                                arrayofbytemaprowm23a = (arrayofbytemaprowm23a * 1000) + currentByte;
                            }
                        }
                        else if (index == 44 || index == 60)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm24a = (arrayofbytemaprowm24a * 1000);
                            }
                            else
                            {
                                arrayofbytemaprowm24a = (arrayofbytemaprowm24a * 1000) + currentByte;
                            }
                        }
                        else if (index == 1 || index == 17)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm31a = (arrayofbytemaprowm31a * 1000);
                            }
                            else
                            {
                                arrayofbytemaprowm31a = (arrayofbytemaprowm31a * 1000) + currentByte;
                            }
                        }
                        else if (index == 33 || index == 49)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm32a = (arrayofbytemaprowm32a * 1000);
                            }
                            else
                            {
                                arrayofbytemaprowm32a = (arrayofbytemaprowm32a * 1000) + currentByte;
                            }
                        }
                        else if (index == 5 || index == 21)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm33a = (arrayofbytemaprowm33a * 1000);
                            }
                            else
                            {
                                arrayofbytemaprowm33a = (arrayofbytemaprowm33a * 1000) + currentByte;
                            }
                        }
                        else if (index == 37 || index == 53)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm34a = (arrayofbytemaprowm34a * 1000);
                            }
                            else
                            {
                                arrayofbytemaprowm34a = (arrayofbytemaprowm34a * 1000) + currentByte;
                            }
                        }
                        else if (index == 9 || index == 25)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm41a = (arrayofbytemaprowm41a * 1000);
                            }
                            else
                            {
                                arrayofbytemaprowm41a = (arrayofbytemaprowm41a * 1000) + currentByte;
                            }
                        }
                        else if (index == 41 || index == 57)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm42a = (arrayofbytemaprowm42a * 1000);
                            }
                            else
                            {
                                arrayofbytemaprowm42a = (arrayofbytemaprowm42a * 1000) + currentByte;
                            }
                        }
                        else if (index == 13 || index == 29)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm43a = (arrayofbytemaprowm43a * 1000);
                            }
                            else
                            {
                                arrayofbytemaprowm43a = (arrayofbytemaprowm43a * 1000) + currentByte;
                            }
                        }
                        else if (index == 45 || index == 61)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm44a = (arrayofbytemaprowm44a * 1000);
                            }
                            else
                            {
                                arrayofbytemaprowm44a = (arrayofbytemaprowm44a * 1000) + currentByte;
                            }
                        }
                        else if (index == 2 || index == 18)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm11b = (arrayofbytemaprowm11b * 1000);
                            }
                            else
                            {
                                arrayofbytemaprowm11b = (arrayofbytemaprowm11b * 1000) + currentByte;
                            }
                        }
                        else if (index == 34 || index == 50)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm12b = (arrayofbytemaprowm12b * 1000);
                            }
                            else
                            {
                                arrayofbytemaprowm12b = (arrayofbytemaprowm12b * 1000) + currentByte;
                            }
                        }
                        else if (index == 6 || index == 22)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm13b = (arrayofbytemaprowm13b * 1000);
                            }
                            else
                            {
                                arrayofbytemaprowm13b = (arrayofbytemaprowm13b * 1000) + currentByte;
                            }
                        }
                        else if (index == 38 || index == 54)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm14b = (arrayofbytemaprowm14b * 1000);
                            }
                            else
                            {
                                arrayofbytemaprowm14b = (arrayofbytemaprowm14b * 1000) + currentByte;
                            }
                        }
                        else if (index == 10 || index == 26)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm21b = (arrayofbytemaprowm21b * 1000);
                            }
                            else
                            {
                                arrayofbytemaprowm21b = (arrayofbytemaprowm21b * 1000) + currentByte;
                            }
                        }
                        else if (index == 42 || index == 58)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm22b = (arrayofbytemaprowm22b * 1000);
                            }
                            else
                            {
                                arrayofbytemaprowm22b = (arrayofbytemaprowm22b * 1000) + currentByte;
                            }
                        }
                        else if (index == 14 || index == 30)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm23b = (arrayofbytemaprowm23b * 1000);
                            }
                            else
                            {
                                arrayofbytemaprowm23b = (arrayofbytemaprowm23b * 1000) + currentByte;
                            }
                        }
                        else if (index == 46 || index == 62)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm24b = (arrayofbytemaprowm24b * 1000);
                            }
                            else
                            {
                                arrayofbytemaprowm24b = (arrayofbytemaprowm24b * 1000) + currentByte;
                            }
                        }
                        else if (index == 3 || index == 19)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm31b = (arrayofbytemaprowm31b * 1000);
                            }
                            else
                            {
                                arrayofbytemaprowm31b = (arrayofbytemaprowm31b * 1000) + currentByte;
                            }
                        }
                        else if (index == 35 || index == 51)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm32b = (arrayofbytemaprowm32b * 1000);
                            }
                            else
                            {
                                arrayofbytemaprowm32b = (arrayofbytemaprowm32b * 1000) + currentByte;
                            }
                        }
                        else if (index == 7 || index == 23)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm33b = (arrayofbytemaprowm33b * 1000);
                            }
                            else
                            {
                                arrayofbytemaprowm33b = (arrayofbytemaprowm33b * 1000) + currentByte;
                            }
                        }
                        else if (index == 39 || index == 55)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm34b = (arrayofbytemaprowm34b * 1000);
                            }
                            else
                            {
                                arrayofbytemaprowm34b = (arrayofbytemaprowm34b * 1000) + currentByte;
                            }
                        }
                        else if (index == 11 || index == 27)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm41b = (arrayofbytemaprowm41b * 1000);
                            }
                            else
                            {
                                arrayofbytemaprowm41b = (arrayofbytemaprowm41b * 1000) + currentByte;
                            }
                        }
                        else if (index == 43 || index == 59)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm42b = (arrayofbytemaprowm42b * 1000);
                            }
                            else
                            {
                                arrayofbytemaprowm42b = (arrayofbytemaprowm42b * 1000) + currentByte;
                            }
                        }
                        else if (index == 15 || index == 31)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm43b = (arrayofbytemaprowm43b * 1000);
                            }
                            else
                            {
                                arrayofbytemaprowm43b = (arrayofbytemaprowm43b * 1000) + currentByte;
                            }
                        }
                        else if (index == 47 || index == 63)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm44b = (arrayofbytemaprowm44b * 1000);
                            }
                            else
                            {
                                arrayofbytemaprowm44b = (arrayofbytemaprowm44b * 1000) + currentByte;
                            }
                        }
                        */














                        /*
                        if (index == 0 || index == 16 || index == 32)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm11a = (arrayofbytemaprowm11a * 1000);
                            }
                            else
                            {
                                arrayofbytemaprowm11a = (arrayofbytemaprowm11a * 1000) + currentByte;
                            }
                        }
                        else if (index == 48 || index == 4 || index == 20)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm12a = (arrayofbytemaprowm12a * 1000);
                            }
                            else
                            {
                                arrayofbytemaprowm12a = (arrayofbytemaprowm12a * 1000) + currentByte;
                            }
                        }
                        else if (index == 36 || index == 52 || index == 8)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm13a = (arrayofbytemaprowm13a * 1000);
                            }
                            else
                            {
                                arrayofbytemaprowm13a = (arrayofbytemaprowm13a * 1000) + currentByte;
                            }
                        }
                        else if (index == 24 || index == 40 || index == 56)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm14a = (arrayofbytemaprowm14a * 1000);
                            }
                            else
                            {
                                arrayofbytemaprowm14a = (arrayofbytemaprowm14a * 1000) + currentByte;
                            }
                        }
                        else if (index == 12 || index == 28 || index == 44)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm21a = (arrayofbytemaprowm21a * 1000);
                            }
                            else
                            {
                                arrayofbytemaprowm21a = (arrayofbytemaprowm21a * 1000) + currentByte;
                            }
                        }
                        else if (index == 60 || index == 1 || index == 17)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm22a = (arrayofbytemaprowm22a * 1000);
                            }
                            else
                            {
                                arrayofbytemaprowm22a = (arrayofbytemaprowm22a * 1000) + currentByte;
                            }
                        }
                        else if (index == 33 || index == 49 || index == 5)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm23a = (arrayofbytemaprowm23a * 1000);
                            }
                            else
                            {
                                arrayofbytemaprowm23a = (arrayofbytemaprowm23a * 1000) + currentByte;
                            }
                        }
                        else if (index == 21 || index == 37 || index == 53)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm24a = (arrayofbytemaprowm24a * 1000);
                            }
                            else
                            {
                                arrayofbytemaprowm24a = (arrayofbytemaprowm24a * 1000) + currentByte;
                            }
                        }
                        else if (index == 9 || index == 25 || index == 41)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm31a = (arrayofbytemaprowm31a * 1000);
                            }
                            else
                            {
                                arrayofbytemaprowm31a = (arrayofbytemaprowm31a * 1000) + currentByte;
                            }
                        }
                        else if (index == 57 || index == 13 || index == 29)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm32a = (arrayofbytemaprowm32a * 1000);
                            }
                            else
                            {
                                arrayofbytemaprowm32a = (arrayofbytemaprowm32a * 1000) + currentByte;
                            }
                        }
                        else if (index == 45 || index == 61 || index == 2)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm33a = (arrayofbytemaprowm33a * 1000);
                            }
                            else
                            {
                                arrayofbytemaprowm33a = (arrayofbytemaprowm33a * 1000) + currentByte;
                            }
                        }
                        else if (index == 18 || index == 34 || index == 50)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm34a = (arrayofbytemaprowm34a * 1000);
                            }
                            else
                            {
                                arrayofbytemaprowm34a = (arrayofbytemaprowm34a * 1000) + currentByte;
                            }
                        }
                        else if (index == 6 || index == 22 || index == 38)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm41a = (arrayofbytemaprowm41a * 1000);
                            }
                            else
                            {
                                arrayofbytemaprowm41a = (arrayofbytemaprowm41a * 1000) + currentByte;
                            }
                        }
                        else if (index == 54 || index == 10 || index == 26)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm42a = (arrayofbytemaprowm42a * 1000);
                            }
                            else
                            {
                                arrayofbytemaprowm42a = (arrayofbytemaprowm42a * 1000) + currentByte;
                            }
                        }
                        else if (index == 42 || index == 58 || index == 14)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm43a = (arrayofbytemaprowm43a * 1000);
                            }
                            else
                            {
                                arrayofbytemaprowm43a = (arrayofbytemaprowm43a * 1000) + currentByte;
                            }
                        }
                        else if (index == 30 || index == 46 || index == 62)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm44a = (arrayofbytemaprowm44a * 1000);
                            }
                            else
                            {
                                arrayofbytemaprowm44a = (arrayofbytemaprowm44a * 1000) + currentByte;
                            }
                        }
                        else if (index == 3 || index == 19 || index == 35)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm11b = (arrayofbytemaprowm11b * 1000);
                            }
                            else
                            {
                                arrayofbytemaprowm11b = (arrayofbytemaprowm11b * 1000) + currentByte;
                            }
                        }
                        else if (index == 51 || index == 7 || index == 23)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm12b = (arrayofbytemaprowm12b * 1000);
                            }
                            else
                            {
                                arrayofbytemaprowm12b = (arrayofbytemaprowm12b * 1000) + currentByte;
                            }
                        }
                        else if (index == 39 || index == 55 || index == 11)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm13b = (arrayofbytemaprowm13b * 1000);
                            }
                            else
                            {
                                arrayofbytemaprowm13b = (arrayofbytemaprowm13b * 1000) + currentByte;
                            }
                        }
                        else if (index == 27 || index == 43 || index == 59)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm14b = (arrayofbytemaprowm14b * 1000);
                            }
                            else
                            {
                                arrayofbytemaprowm14b = (arrayofbytemaprowm14b * 1000) + currentByte;
                            }
                        }
                        else if (index == 15 || index == 31 || index == 47)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm21b = (arrayofbytemaprowm21b * 1000);
                            }
                            else
                            {
                                arrayofbytemaprowm21b = (arrayofbytemaprowm21b * 1000) + currentByte;
                            }
                        }
                        else if (index == 63)
                        {
                            if (currentByte == 0)
                            {
                                arrayofbytemaprowm22b = (arrayofbytemaprowm22b * 1000);
                            }
                            else
                            {
                                arrayofbytemaprowm22b = (arrayofbytemaprowm22b * 1000) + currentByte;
                            }
                        }*/










                        /*
                        switch (somecountermul)
                        {
                            case 0:
                                //selectablevectordouble = arrayofbytemaprowm11a;


                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm11a = (arrayofbytemaprowm11a * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm11a = (arrayofbytemaprowm11a * 1000) + currentByte;
                                }

                                m11adder++;
                                break;
                            case 1:
                                //selectablevectordouble = arrayofbytemaprowm12a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm12a = (arrayofbytemaprowm12a * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm12a = (arrayofbytemaprowm12a * 1000) + currentByte;
                                }
                                m12adder++;
                                break;
                            case 2:
                                //selectablevectordouble = arrayofbytemaprowm13a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm13a = (arrayofbytemaprowm13a * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm13a = (arrayofbytemaprowm13a * 1000) + currentByte;
                                }
                                m13adder++;
                                break;
                            case 3:
                                //selectablevectordouble = arrayofbytemaprowm14a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm14a = (arrayofbytemaprowm14a * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm14a = (arrayofbytemaprowm14a * 1000) + currentByte;
                                }
                                m14adder++;
                                break;
                            case 4:
                                //selectablevectordouble = arrayofbytemaprowm21a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm21a = (arrayofbytemaprowm21a * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm21a = (arrayofbytemaprowm21a * 1000) + currentByte;
                                }
                                m21adder++;
                                break;
                            case 5:
                                //selectablevectordouble = arrayofbytemaprowm22a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm22a = (arrayofbytemaprowm22a * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm22a = (arrayofbytemaprowm22a * 1000) + currentByte;
                                }
                                m22adder++;
                                break;
                            case 6:
                                //selectablevectordouble = arrayofbytemaprowm23a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm23a = (arrayofbytemaprowm23a * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm23a = (arrayofbytemaprowm23a * 1000) + currentByte;
                                }
                                m23adder++;
                                break;
                            case 7:
                                //selectablevectordouble = arrayofbytemaprowm24a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm24a = (arrayofbytemaprowm24a * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm24a = (arrayofbytemaprowm24a * 1000) + currentByte;
                                }
                                m24adder++;
                                break;
                            case 8:
                                //selectablevectordouble = arrayofbytemaprowm31a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm31a = (arrayofbytemaprowm31a * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm31a = (arrayofbytemaprowm31a * 1000) + currentByte;
                                }
                                m31adder++;
                                break;
                            case 9:
                                //selectablevectordouble = arrayofbytemaprowm32a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm32a = (arrayofbytemaprowm32a * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm32a = (arrayofbytemaprowm32a * 1000) + currentByte;
                                }
                                m32adder++;

                                break;
                            case 10:
                                //selectablevectordouble = arrayofbytemaprowm33a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm33a = (arrayofbytemaprowm33a * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm33a = (arrayofbytemaprowm33a * 1000) + currentByte;
                                }
                                m33adder++;
                                break;
                            case 11:
                                //selectablevectordouble = arrayofbytemaprowm34a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm34a = (arrayofbytemaprowm34a * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm34a = (arrayofbytemaprowm34a * 1000) + currentByte;
                                }
                                m34adder++;
                                break;
                            case 12:
                                //selectablevectordouble = arrayofbytemaprowm41a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm41a = (arrayofbytemaprowm41a * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm41a = (arrayofbytemaprowm41a * 1000) + currentByte;
                                }
                                m41adder++;
                                break;
                            case 13:
                                //selectablevectordouble = arrayofbytemaprowm42a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm42a = (arrayofbytemaprowm42a * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm42a = (arrayofbytemaprowm42a * 1000) + currentByte;
                                }
                                m42adder++;
                                break;
                            case 14:
                                //selectablevectordouble = arrayofbytemaprowm43a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm43a = (arrayofbytemaprowm43a * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm43a = (arrayofbytemaprowm43a * 1000) + currentByte;
                                }
                                m43adder++;
                                break;
                            case 15:
                                //selectablevectordouble = arrayofbytemaprowm44a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm44a = (arrayofbytemaprowm44a * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm44a = (arrayofbytemaprowm44a * 1000) + currentByte;
                                }
                                m44adder++;
                                break;




                            case 16:
                                //selectablevectorbouble = arrayofbytemaprowm11a;


                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm11b = (arrayofbytemaprowm11b * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm11b = (arrayofbytemaprowm11b * 1000) + currentByte;
                                }
                                m11badder++;
                                break;
                            case 17:
                                //selectablevectorbouble = arrayofbytemaprowm12a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm12b = (arrayofbytemaprowm12b * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm12b = (arrayofbytemaprowm12b * 1000) + currentByte;
                                }
                                m12badder++;
                                break;
                            case 18:
                                //selectablevectorbouble = arrayofbytemaprowm13a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm13b = (arrayofbytemaprowm13b * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm13b = (arrayofbytemaprowm13b * 1000) + currentByte;
                                }
                                m13badder++;
                                break;
                            case 19:
                                //selectablevectorbouble = arrayofbytemaprowm14a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm14b = (arrayofbytemaprowm14b * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm14b = (arrayofbytemaprowm14b * 1000) + currentByte;
                                }
                                m14badder++;
                                break;
                            case 20:
                                //selectablevectorbouble = arrayofbytemaprowm21a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm21b = (arrayofbytemaprowm21b * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm21b = (arrayofbytemaprowm21b * 1000) + currentByte;
                                }
                                m21badder++;
                                break;
                            case 21:
                                //selectablevectorbouble = arrayofbytemaprowm22a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm22b = (arrayofbytemaprowm22b * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm22b = (arrayofbytemaprowm22b * 1000) + currentByte;
                                }
                                m22badder++;
                                break;
                            case 22:
                                //selectablevectorbouble = arrayofbytemaprowm23a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm23b = (arrayofbytemaprowm23b * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm23b = (arrayofbytemaprowm23b * 1000) + currentByte;
                                }
                                m23badder++;
                                break;
                            case 23:
                                //selectablevectorbouble = arrayofbytemaprowm24a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm24b = (arrayofbytemaprowm24b * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm24b = (arrayofbytemaprowm24b * 1000) + currentByte;
                                }
                                m24badder++;
                                break;
                            case 24:
                                //selectablevectorbouble = arrayofbytemaprowm31a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm31b = (arrayofbytemaprowm31b * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm31b = (arrayofbytemaprowm31b * 1000) + currentByte;
                                }
                                m31badder++;
                                break;
                            case 25:
                                //selectablevectorbouble = arrayofbytemaprowm32a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm32b = (arrayofbytemaprowm32b * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm32b = (arrayofbytemaprowm32b * 1000) + currentByte;
                                }
                                m32badder++;
                                break;
                            case 26:
                                //selectablevectorbouble = arrayofbytemaprowm33a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm33b = (arrayofbytemaprowm33b * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm33b = (arrayofbytemaprowm33b * 1000) + currentByte;
                                }
                                m33badder++;
                                break;
                            case 27:
                                //selectablevectorbouble = arrayofbytemaprowm34a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm34b = (arrayofbytemaprowm34b * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm34b = (arrayofbytemaprowm34b * 1000) + currentByte;
                                }
                                m34badder++;
                                break;
                            case 28:
                                //selectablevectorbouble = arrayofbytemaprowm41a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm41b = (arrayofbytemaprowm41b * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm41b = (arrayofbytemaprowm41b * 1000) + currentByte;
                                }
                                m41badder++;
                                break;
                            case 29:
                                //selectablevectorbouble = arrayofbytemaprowm42a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm42b = (arrayofbytemaprowm42b * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm42b = (arrayofbytemaprowm42b * 1000) + currentByte;
                                }
                                m42badder++;
                                break;
                            case 30:
                                //selectablevectorbouble = arrayofbytemaprowm43a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm43b = (arrayofbytemaprowm43b * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm43b = (arrayofbytemaprowm43b * 1000) + currentByte;
                                }
                                m43badder++;
                                break;
                            case 31:
                                //selectablevectorbouble = arrayofbytemaprowm44a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm44b = (arrayofbytemaprowm44b * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm44b = (arrayofbytemaprowm44b * 1000) + currentByte;
                                }
                                m44badder++;
                                break;








                            case 32:
                                //selectablevectorcouble = arrayofbytemaprowm11a;


                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm11c = (arrayofbytemaprowm11c * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm11c = (arrayofbytemaprowm11c * 1000) + currentByte;
                                }
                                m11cadder++;
                                break;
                            case 33:
                                //selectablevectorcouble = arrayofbytemaprowm12a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm12c = (arrayofbytemaprowm12c * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm12c = (arrayofbytemaprowm12c * 1000) + currentByte;
                                }
                                m12cadder++;
                                break;
                            case 34:
                                //selectablevectorcouble = arrayofbytemaprowm13a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm13c = (arrayofbytemaprowm13c * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm13c = (arrayofbytemaprowm13c * 1000) + currentByte;
                                }
                                m13cadder++;
                                break;
                            case 35:
                                //selectablevectorcouble = arrayofbytemaprowm14a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm14c = (arrayofbytemaprowm14c * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm14c = (arrayofbytemaprowm14c * 1000) + currentByte;
                                }
                                m14cadder++;
                                break;
                            case 36:
                                //selectablevectorcouble = arrayofbytemaprowm21a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm21c = (arrayofbytemaprowm21c * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm21c = (arrayofbytemaprowm21c * 1000) + currentByte;
                                }
                                m21cadder++;
                                break;
                            case 37:
                                //selectablevectorcouble = arrayofbytemaprowm22a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm22c = (arrayofbytemaprowm22c * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm22c = (arrayofbytemaprowm22c * 1000) + currentByte;
                                }
                                m22cadder++;
                                break;
                            case 38:
                                //selectablevectorcouble = arrayofbytemaprowm23a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm23c = (arrayofbytemaprowm23c * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm23c = (arrayofbytemaprowm23c * 1000) + currentByte;
                                }
                                m23cadder++;
                                break;
                            case 39:
                                //selectablevectorcouble = arrayofbytemaprowm24a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm24c = (arrayofbytemaprowm24c * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm24c = (arrayofbytemaprowm24c * 1000) + currentByte;
                                }
                                m24cadder++;
                                break;
                            case 40:
                                //selectablevectorcouble = arrayofbytemaprowm31a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm31c = (arrayofbytemaprowm31c * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm31c = (arrayofbytemaprowm31c * 1000) + currentByte;
                                }
                                m31cadder++;
                                break;
                            case 41:
                                //selectablevectorcouble = arrayofbytemaprowm32a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm32c = (arrayofbytemaprowm32c * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm32c = (arrayofbytemaprowm32c * 1000) + currentByte;
                                }
                                m32cadder++;
                                break;
                            case 42:
                                //selectablevectorcouble = arrayofbytemaprowm33a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm33c = (arrayofbytemaprowm33c * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm33c = (arrayofbytemaprowm33c * 1000) + currentByte;
                                }
                                m33cadder++;
                                break;
                            case 43:
                                //selectablevectorcouble = arrayofbytemaprowm34a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm34c = (arrayofbytemaprowm34c * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm34c = (arrayofbytemaprowm34c * 1000) + currentByte;
                                }
                                m34cadder++;
                                break;
                            case 44:
                                //selectablevectorcouble = arrayofbytemaprowm41a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm41c = (arrayofbytemaprowm41c * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm41c = (arrayofbytemaprowm41c * 1000) + currentByte;
                                }
                                m41cadder++;
                                break;
                            case 45:
                                //selectablevectorcouble = arrayofbytemaprowm42a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm42c = (arrayofbytemaprowm42c * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm42c = (arrayofbytemaprowm42c * 1000) + currentByte;
                                }
                                m42cadder++;
                                break;
                            case 46:
                                //selectablevectorcouble = arrayofbytemaprowm43a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm43c = (arrayofbytemaprowm43c * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm43c = (arrayofbytemaprowm43c * 1000) + currentByte;
                                }
                                m43cadder++;
                                break;
                            case 47:
                                //selectablevectorcouble = arrayofbytemaprowm44a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm44c = (arrayofbytemaprowm44c * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm44c = (arrayofbytemaprowm44c * 1000) + currentByte;
                                }
                                m44cadder++;
                                break;







                            case 48:
                                //selectablevectordouble = arrayofbytemaprowm11a;


                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm11d = (arrayofbytemaprowm11d * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm11d = (arrayofbytemaprowm11d * 1000) + currentByte;
                                }
                                m11dadder++;
                                break;
                            case 49:
                                //selectablevectordouble = arrayofbytemaprowm12a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm12d = (arrayofbytemaprowm12d * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm12d = (arrayofbytemaprowm12d * 1000) + currentByte;
                                }
                                m12dadder++;
                                break;
                            case 50:
                                //selectablevectordouble = arrayofbytemaprowm13a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm13d = (arrayofbytemaprowm13d * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm13d = (arrayofbytemaprowm13d * 1000) + currentByte;
                                }
                                m13dadder++;
                                break;
                            case 51:
                                //selectablevectordouble = arrayofbytemaprowm14a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm14d = (arrayofbytemaprowm14d * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm14d = (arrayofbytemaprowm14d * 1000) + currentByte;
                                }
                                m14dadder++;
                                break;
                            case 52:
                                //selectablevectordouble = arrayofbytemaprowm21a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm21d = (arrayofbytemaprowm21d * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm21d = (arrayofbytemaprowm21d * 1000) + currentByte;
                                }
                                m21dadder++;
                                break;
                            case 53:
                                //selectablevectordouble = arrayofbytemaprowm22a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm22d = (arrayofbytemaprowm22d * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm22d = (arrayofbytemaprowm22d * 1000) + currentByte;
                                }
                                m22dadder++;
                                break;
                            case 54:
                                //selectablevectordouble = arrayofbytemaprowm23a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm23d = (arrayofbytemaprowm23d * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm23d = (arrayofbytemaprowm23d * 1000) + currentByte;
                                }
                                m23dadder++;

                                break;
                            case 55:
                                //selectablevectordouble = arrayofbytemaprowm24a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm24d = (arrayofbytemaprowm24d * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm24d = (arrayofbytemaprowm24d * 1000) + currentByte;
                                }
                                m24dadder++;
                                break;
                            case 56:
                                //selectablevectordouble = arrayofbytemaprowm31a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm31d = (arrayofbytemaprowm31d * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm31d = (arrayofbytemaprowm31d * 1000) + currentByte;
                                }
                                m31dadder++;
                                break;
                            case 57:
                                //selectablevectordouble = arrayofbytemaprowm32a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm32d = (arrayofbytemaprowm32d * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm32d = (arrayofbytemaprowm32d * 1000) + currentByte;
                                }
                                m32dadder++;
                                break;
                            case 58:
                                //selectablevectordouble = arrayofbytemaprowm33a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm33d = (arrayofbytemaprowm33d * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm33d = (arrayofbytemaprowm33d * 1000) + currentByte;
                                }
                                m33dadder++;
                                break;
                            case 59:
                                //selectablevectordouble = arrayofbytemaprowm34a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm34d = (arrayofbytemaprowm34d * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm34d = (arrayofbytemaprowm34d * 1000) + currentByte;
                                }
                                m34dadder++;
                                break;
                            case 60:
                                //selectablevectordouble = arrayofbytemaprowm41a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm41d = (arrayofbytemaprowm41d * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm41d = (arrayofbytemaprowm41d * 1000) + currentByte;
                                }
                                m41dadder++;
                                break;
                            case 61:
                                //selectablevectordouble = arrayofbytemaprowm42a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm42d = (arrayofbytemaprowm42d * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm42d = (arrayofbytemaprowm42d * 1000) + currentByte;
                                }
                                m42dadder++;
                                break;
                            case 62:
                                //selectablevectordouble = arrayofbytemaprowm43a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm43d = (arrayofbytemaprowm43d * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm43d = (arrayofbytemaprowm43d * 1000) + currentByte;
                                }
                                m43dadder++;
                                break;
                            case 63:
                                //selectablevectordouble = arrayofbytemaprowm44a;

                                if (currentByte == 0)
                                {
                                    arrayofbytemaprowm44d = (arrayofbytemaprowm44d * 1000);
                                }
                                else
                                {
                                    arrayofbytemaprowm44d = (arrayofbytemaprowm44d * 1000) + currentByte;
                                }
                                m44dadder++;
                                break;
                        };*/
                    }
                }
            }







            m11 = arrayofbytemaprowm11a;
            m12 = arrayofbytemaprowm12a;
            m13 = arrayofbytemaprowm13a;
            m14 = arrayofbytemaprowm14a;
            m21 = arrayofbytemaprowm21a;
            m22 = arrayofbytemaprowm22a;
            m23 = arrayofbytemaprowm23a;
            m24 = arrayofbytemaprowm24a;
            m31 = arrayofbytemaprowm31a;
            m32 = arrayofbytemaprowm32a;
            m33 = arrayofbytemaprowm33a;
            m34 = arrayofbytemaprowm34a;
            m41 = arrayofbytemaprowm41a;
            m42 = arrayofbytemaprowm42a;
            m43 = arrayofbytemaprowm43a;
            m44 = arrayofbytemaprowm44a;


            m11b = arrayofbytemaprowm11b;
            m12b = arrayofbytemaprowm12b;
            m13b = arrayofbytemaprowm13b;
            m14b = arrayofbytemaprowm14b;

            m21b = arrayofbytemaprowm21b;
            m22b = arrayofbytemaprowm22b;
            m23b = arrayofbytemaprowm23b;
            m24b = arrayofbytemaprowm24b;

            m31b = arrayofbytemaprowm31b;
            m32b = arrayofbytemaprowm32b;
            m33b = arrayofbytemaprowm33b;
            m34b = arrayofbytemaprowm34b;

            m41b = arrayofbytemaprowm41b;
            m42b = arrayofbytemaprowm42b;
            m43b = arrayofbytemaprowm43b;
            m44b = arrayofbytemaprowm44b;

            /*
            m11c = arrayofbytemaprowm11c;
            m12c = arrayofbytemaprowm12c;
            m13c = arrayofbytemaprowm13c;
            m14c = arrayofbytemaprowm14c;

            m21c = arrayofbytemaprowm21c;
            m22c = arrayofbytemaprowm22c;
            m23c = arrayofbytemaprowm23c;
            m24c = arrayofbytemaprowm24c;

            m31c = arrayofbytemaprowm31c;
            m32c = arrayofbytemaprowm32c;
            m33c = arrayofbytemaprowm33c;
            m34c = arrayofbytemaprowm34c;

            m41c = arrayofbytemaprowm41c;
            m42c = arrayofbytemaprowm42c;
            m43c = arrayofbytemaprowm43c;
            m44c = arrayofbytemaprowm44c;

            m11d = arrayofbytemaprowm11d;
            m12d = arrayofbytemaprowm12d;
            m13d = arrayofbytemaprowm13d;
            m14d = arrayofbytemaprowm14d;

            m21d = arrayofbytemaprowm21d;
            m22d = arrayofbytemaprowm22d;
            m23d = arrayofbytemaprowm23d;
            m24d = arrayofbytemaprowm24d;

            m31d = arrayofbytemaprowm31d;
            m32d = arrayofbytemaprowm32d;
            m33d = arrayofbytemaprowm33d;
            m34d = arrayofbytemaprowm34d;

            m41d = arrayofbytemaprowm41d;
            m42d = arrayofbytemaprowm42d;
            m43d = arrayofbytemaprowm43d;
            m44d = arrayofbytemaprowm44d;*/





            /*
            if (arrayofbytemaprowm11a != 1000000000.0)
            {
                Console.WriteLine("dim:" + arrayofbytemaprowm11a);
            }*/



            mapdimsx = null;
            mapdimsy = null;
            mapdimsz = null;




            dimsminz++;
            if (dimsminz >= 2)
            {
                dimsminy++;
                dimsminz = 0;
            }

            if (dimsminy >= 2)
            {
                dimsminx++;
                dimsminy = 0;
            }

            if (dimsminx >= 2)
            {
                dimsminx = 0;
            }
        }
















        float padding0 = 0.0f;
        float padding1 = 0.0f;
        float padding2 = 0.0f;

        int index;
        public tutorialcubeaschunkinst.DVertex[] arrayofvertstop;


        /*public int[] arrayoftrigstop;        
        public tutorialcubeaschunkinst.DVertex[] arrayofvertsbottom;
        public int[] arrayoftrigsbottom;
        public tutorialcubeaschunkinst.DVertex[] arrayofvertsleft;
        public int[] arrayoftrigsleft;
        public tutorialcubeaschunkinst.DVertex[] arrayofvertsright;
        public int[] arrayoftrigsright;
        public tutorialcubeaschunkinst.DVertex[] arrayofvertsfront;
        public int[] arrayoftrigsfront;
        public tutorialcubeaschunkinst.DVertex[] arrayofvertsback;
        public int[] arrayoftrigsback;*/


        int[][] somearrayofcoords = new int[6][];
        int[][] somearrayofcoordsfloor = new int[6][];

        //public List<int> triangles;
        //public int[] _chunkArray;
        public int[] _tempChunkArray;


        /*public int[] _tempChunkArrayRightFace;
        public int[] _tempChunkArrayLeftFace;
        public int[] _tempChunkArrayFrontFace;
        public int[] _tempChunkArrayBackFace;
        public int[] _tempChunkArrayBottomFace;
        */

        public int[] _chunkVertexArray0;
        /*public int[] _chunkVertexArray1;
        public int[] _chunkVertexArray2;
        public int[] _chunkVertexArray3;
        public int[] _chunkVertexArray4;
        public int[] _chunkVertexArray5;*/


        public int[] _testVertexArray0;
        /*public int[] _testVertexArray1;
        public int[] _testVertexArray2;
        public int[] _testVertexArray3;
        public int[] _testVertexArray4;
        public int[] _testVertexArray5;*/

        float planeSize;
        int facetype;
        int vertexlistWidth;
        int vertexlistHeight;
        int vertexlistDepth;
        Vector4 chunkoriginpos;
        int totalints;
        private Vector4 topfacecolor = new Vector4(0, 1, 1, 1);
        private Vector4 leftfacecolor = new Vector4(1, 0, 0, 1);
        private Vector4 rightfacecolor = new Vector4(0, 1, 0, 1);
        private Vector4 frontfacecolor = new Vector4(0, 0, 1, 1);
        private Vector4 backfacecolor = new Vector4(1, 1, 0, 1);
        private Vector4 bottomfacecolor = new Vector4(1, 0, 1, 1);

        public int X;
        public int Y;
        public int Z;

        int _maxWidth;// = 0;
        int _maxHeight;
        int _maxDepth;// = 0;

        int rowIterateX;// = 0;
        int rowIterateY;
        int rowIterateZ;// = 0;

        bool foundVertOne;// = false;
        bool foundVertTwo;// = false;
        bool foundVertThree;// = false;
        bool foundVertFour;// = false;


        //public List<sclevelgenclass.DVertex> vertexlist = new List<sclevelgenclass.DVertex>(); //listOfVerts
        public List<tutorialcubeaschunkinst.DVertex> vertexlisttop = new List<tutorialcubeaschunkinst.DVertex>(); //listOfVerts

        /*public List<int> listOfTriangleIndicestop = new List<int>();

        public List<tutorialcubeaschunkinst.DVertex> vertexlistright = new List<tutorialcubeaschunkinst.DVertex>(); //listOfVerts
        public List<int> listOfTriangleIndicesright = new List<int>();

        public List<tutorialcubeaschunkinst.DVertex> vertexlistleft = new List<tutorialcubeaschunkinst.DVertex>(); //listOfVerts
        public List<int> listOfTriangleIndicesleft = new List<int>();

        public List<tutorialcubeaschunkinst.DVertex> vertexlistbottom = new List<tutorialcubeaschunkinst.DVertex>(); //listOfVerts
        public List<int> listOfTriangleIndicesbottom = new List<int>();

        public List<tutorialcubeaschunkinst.DVertex> vertexlistfront = new List<tutorialcubeaschunkinst.DVertex>(); //listOfVerts
        public List<int> listOfTriangleIndicesfront = new List<int>();

        public List<tutorialcubeaschunkinst.DVertex> vertexlistback = new List<tutorialcubeaschunkinst.DVertex>(); //listOfVerts
        public List<int> listOfTriangleIndicesback = new List<int>();*/




        tutorialchunkcubemap[] listofchunksadjacent = new tutorialchunkcubemap[6];
        tutorialchunkcubemap[] listofchunksadjacentfloor = new tutorialchunkcubemap[6];

        int _index0;// = 0;
        int _index1;// = 0;
        int _index2;// = 0;
        int _index3;// = 0;
        int _newVertzCounter;// = 0;

        int oneVertIndexX;// = 0;
        int oneVertIndexY;// = 0;
        int oneVertIndexZ;// = 0;

        int twoVertIndexX;// = 0;
        int twoVertIndexY;// = 0;
        int twoVertIndexZ;// = 0;

        int threeVertIndexX;// = 0;
        int threeVertIndexY;//= 0;
        int threeVertIndexZ;// = 0;

        int fourVertIndexX;// = 0;
        int fourVertIndexY;// = 0;
        int fourVertIndexZ;// = 0;
        int block;

        public int[] startBuildingArray(int facetype_, tutorialcubeaschunkinst componentparent_, int levelofdetail_, int minx, int miny, int minz, int maxx, int maxy, int maxz) //, out int vertexNum, out int indicesNum
        //public void startBuildingArray(Vector4 currentPosition, out sclevelgenclass.DVertex[] vertexArray, out int[] triangleArray, out int[] mapper, int padding0_, int padding1_, int padding2_, int numberOfObjectInWidth_, int numberOfObjectInHeight_, int numberOfObjectInDepth_, int numberOfInstancesPerObjectInWidth_, int numberOfInstancesPerObjectInHeight_, int numberOfInstancesPerObjectInDepth_, int width_, int height_, int depth_, float planeSize_, sclevelgenclassPrim componentparentprim_, sclevelgenclass componentparentinstance_, sclevelgenclass_instances componentparentthischunk_, int fullface_, int voxeltype_)
        {
            //realpos = newchunkpos_;
            levelofdetail = levelofdetail_;
            // TOFIX
            // TOFIX
            // TOFIX
            //levelofdetail
            //width = 10;
            //height = 10;
            //depth = 10;
            // TOFIX
            // TOFIX
            // TOFIX
            //newchunkpos = newchunkpos_;

            componentparent = componentparent_;

            //map = map_;

            facetype = facetype_;

            //width = 4;
            //height = 4;
            //depth = 4;

            ////Console.WriteLine(chunkPos);


            if (levelofdetail == 3)
            {
                levelofdetailmul = 1.15f;
            }
            if (levelofdetail == 4)
            {
                levelofdetailmul = 1.25f;
            }
            if (levelofdetail == 5)
            {
                levelofdetailmul = 2.0f;
            }



            /*
            if (levelofdetail == 1)
            {
                width = componentparent.somelevelgenprimglobals.widthlod0;
                height = componentparent.somelevelgenprimglobals.heightlod0;
                depth = componentparent.somelevelgenprimglobals.depthlod0;

                //width = 10;
                //height = 10;
                //depth = 10;
            }
            else if (levelofdetail == 2)
            {
                width = componentparent.somelevelgenprimglobals.widthlod1;
                height = componentparent.somelevelgenprimglobals.heightlod1;
                depth = componentparent.somelevelgenprimglobals.depthlod1;

                //width = 5;
                //height = 5;
                //depth = 5;
            }

            else if (levelofdetail == 3)
            {
                width = componentparent.somelevelgenprimglobals.widthlod2;
                height = componentparent.somelevelgenprimglobals.heightlod2;
                depth = componentparent.somelevelgenprimglobals.depthlod2;
                //width = 3;
                //height = 3;
                //depth = 3;
            }
            else if (levelofdetail == 4)
            {
                width = componentparent.somelevelgenprimglobals.widthlod3;
                height = componentparent.somelevelgenprimglobals.heightlod3;
                depth = componentparent.somelevelgenprimglobals.depthlod3;

                //width = 2;
                //height = 2;
                //depth = 2;
            }
            else if (levelofdetail == 5)
            {
                width = componentparent.somelevelgenprimglobals.widthlod4;
                height = componentparent.somelevelgenprimglobals.heightlod4;
                depth = componentparent.somelevelgenprimglobals.depthlod4;

                //width = 2;
                //height = 2;
                //depth = 2;
            }
            */


            width = 8;
            height = 8;
            depth = 8;



            planeSize = 0.01f;//  componentparent.somelevelgenprimglobals.planeSize;












            /*
            planeSize = 0.1f;
            staticPlaneSize = planeSize;

            if (staticPlaneSize == 1)
            {
                staticPlaneSize = planeSize * 0.1f;
                alternateStaticPlaneSize = planeSize * 0.1f;
            }
            else if (staticPlaneSize == 0.1f)
            {
                staticPlaneSize = planeSize;
                alternateStaticPlaneSize = planeSize * 10;
            }
            else if (staticPlaneSize == 0.01f)
            {
                staticPlaneSize = planeSize;
                alternateStaticPlaneSize = planeSize * 1000;
            }*/
            //width = 10;
            seed = 3420;


            chunkoriginpos = new Vector4(newchunkpos[0], newchunkpos[1], newchunkpos[2], 0.0f);

            //floorHeight = height;


            total = width * height * depth;
            totalints = width * height * depth;

            vertexlistWidth = width + 1;
            vertexlistHeight = height + 1;
            vertexlistDepth = depth + 1;
            //map = new int[width * height * depth];

            /*_tempChunkArrayBottomFace = new int[width * height * depth];
            _tempChunkArrayBackFace = new int[width * height * depth];
            _tempChunkArrayFrontFace = new int[width * height * depth];
            _tempChunkArrayLeftFace = new int[width * height * depth];
            _tempChunkArrayRightFace = new int[width * height * depth];*/
            _tempChunkArray = new int[width * height * depth];

            /*_chunkArray = new int[width * height * depth];*/

            _chunkVertexArray0 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            /*_chunkVertexArray1 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _chunkVertexArray2 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _chunkVertexArray3 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _chunkVertexArray4 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _chunkVertexArray5 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];*/

            _testVertexArray0 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            /*_testVertexArray1 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _testVertexArray2 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _testVertexArray3 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _testVertexArray4 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _testVertexArray5 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];*/

            //vertexlist = new List<Vector3>();

            vertexlisttop = new List<tutorialcubeaschunkinst.DVertex>();
            /*listOfTriangleIndicestop = new List<int>();

            vertexlistback = new List<tutorialcubeaschunkinst.DVertex>();
            listOfTriangleIndicesback = new List<int>();


            vertexlistleft = new List<tutorialcubeaschunkinst.DVertex>();
            listOfTriangleIndicesleft = new List<int>();


            vertexlistright = new List<tutorialcubeaschunkinst.DVertex>();
            listOfTriangleIndicesright = new List<int>();


            vertexlistbottom = new List<tutorialcubeaschunkinst.DVertex>();
            listOfTriangleIndicesbottom = new List<int>();

            vertexlistfront = new List<tutorialcubeaschunkinst.DVertex>();
            listOfTriangleIndicesfront = new List<int>();*/





            //normalslist = new List<Vector3>();
            //colorslist = new List<Vector4>();
            //indexPoslist = new List<Vector4>();
            //textureslist = new List<Vector2>();

            ////Console.WriteLine(width + " " + width);

            /*
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        float noiseXZ = 20;

                        noiseXZ *= fastNoise.GetNoise((((x * staticPlaneSize) + (currentPosition.X * alternateStaticPlaneSize) + seed) / detailScale) * HeightScale, (((y * staticPlaneSize) + (currentPosition.Y * alternateStaticPlaneSize) + seed) / detailScale) * HeightScale, (((z * staticPlaneSize) + (currentPosition.Z * alternateStaticPlaneSize) + seed) / detailScale) * HeightScale);

                        ////Console.WriteLine(noiseXZ);

                        if (noiseXZ >= 0.1f)
                        {
                            map[x + width * (y + height * z)] = 1;
                        }
                        else if (y == 0 && currentPosition.Y == 0)
                        {
                            map[x + width * (y + height * z)] = 1;
                        }
                        else
                        {
                            map[x + width * (y + height * z)] = 0;
                        }

                        //map[x + width * (y + height * z)] = 1;
                    }
                }
            }*/

            mapvertindexfordims = new int[4 * 4 * 4];

            widthdimtop = new int[4 * 4 * 4];
            heightdimtop = new int[4 * 4 * 4];
            depthdimtop = new int[4 * 4 * 4];
            mapfirstvertxtop = new int[4 * 4 * 4];
            mapfirstvertytop = new int[4 * 4 * 4];
            mapfirstvertztop = new int[4 * 4 * 4];








            for (int i = 0; i < somearrayofcoords.Length; i++)
            {
                somearrayofcoords[i] = new int[3];
            }



            somearrayofcoords[0][0] = X - 1;
            somearrayofcoords[0][1] = Y;
            somearrayofcoords[0][2] = Z;

            somearrayofcoords[1][0] = X + 1;
            somearrayofcoords[1][1] = Y;
            somearrayofcoords[1][2] = Z;

            somearrayofcoords[2][0] = X;
            somearrayofcoords[2][1] = Y;
            somearrayofcoords[2][2] = Z - 1;

            somearrayofcoords[3][0] = X;
            somearrayofcoords[3][1] = Y;
            somearrayofcoords[3][2] = Z + 1;

            somearrayofcoords[4][0] = X;
            somearrayofcoords[4][1] = Y - 1;
            somearrayofcoords[4][2] = Z;

            somearrayofcoords[5][0] = X;
            somearrayofcoords[5][1] = Y + 1;
            somearrayofcoords[5][2] = Z;


            // //Console.WriteLine(chunkPos);

            //float somemul = 1.0f;

            //List<Vector3> listofvecs = new List<Vector3>();

            ////Console.WriteLine(chunkPos);

            //LEFT CHUNK
            /*Vector3 somechunkpos = chunkPos;
            somechunkpos.X -= 1.0f;
            listofvecs.Add(somechunkpos);

            //RIGHT CHUNK
            somechunkpos = chunkPos;
            somechunkpos.X += 1.0f;
            listofvecs.Add(somechunkpos);

            //BACK CHUNK
            somechunkpos = chunkPos;
            somechunkpos.Z -= 1.0f;
            listofvecs.Add(somechunkpos);

            //FRONT CHUNK
            somechunkpos = chunkPos;
            somechunkpos.Z += 1.0f;
            listofvecs.Add(somechunkpos);

            //BOTTOM CHUNK
            somechunkpos = chunkPos;
            somechunkpos.Y -= 1.0f;
            listofvecs.Add(somechunkpos);

            //TOP CHUNK
            somechunkpos = chunkPos;
            somechunkpos.Y += 1.0f;
            listofvecs.Add(somechunkpos);*/




            int arrayindex;



            /*for (int i = 0; i < listofvecs.Count; i++)
            {
                listofchunksadjacent[i] = componentparent_.getChunklod0((int)Math.Round(listofvecs[i].X), (int)Math.Round(listofvecs[i].Y), (int)Math.Round(listofvecs[i].Z), out arrayindex); //(int)listofvecs[i].Y
                //listofchunksadjacent[i] = componentparent_.getchunkinlevelgenmap((int)Math.Round(listofvecs[i].X), (int)Math.Round(listofvecs[i].Y), (int)Math.Round(listofvecs[i].Z), levelofdetail); //(int)listofvecs[i].Y

                if (listofchunksadjacent[i] != null)
                {
                    ////Console.WriteLine(listofchunksadjacent[i].chunkPos);
                }
            }*/




            ////Console.WriteLine("LOD:" + levelofdetail);

            for (int i = 0; i < somearrayofcoords.Length; i++)
            {
                //if (levelofdetail == 1)
                {
                    //listofchunksadjacent[i] = (sclevelgenvert)componentparent.getChunklod0(somearrayofcoords[i][0], somearrayofcoords[i][1], somearrayofcoords[i][2], out arrayindex);
                    listofchunksadjacent[i] = (tutorialchunkcubemap)sccslevelgen.currentlevelgen.getchunkinlevelgenmap(somearrayofcoords[i][0], somearrayofcoords[i][1], somearrayofcoords[i][2], levelofdetail, out arrayindex);
                }
                /* else if (levelofdetail == 2)
                 {
                     listofchunksadjacent[i] = (sclevelgenvert)componentparent.getChunklod1(listofvecs[i].X, listofvecs[i].Y, listofvecs[i].Z, out arrayindex);


                 }
                 else if (levelofdetail == 3)
                 {
                     listofchunksadjacent[i] = (sclevelgenvert)componentparent.getChunklod2(listofvecs[i].X, listofvecs[i].Y, listofvecs[i].Z, out arrayindex);


                 }
                 else if (levelofdetail == 4)
                 {
                     listofchunksadjacent[i] = (sclevelgenvert)componentparent.getChunklod3(listofvecs[i].X, listofvecs[i].Y, listofvecs[i].Z, out arrayindex);

                 }
                 else if (levelofdetail == 5)
                 {
                     listofchunksadjacent[i] = (sclevelgenvert)componentparent.getChunklod4(listofvecs[i].X, listofvecs[i].Y, listofvecs[i].Z, out arrayindex);

                 }*/
            }








            /*
            if (levelofdetail == 1)
            {
              

            }
            else if (levelofdetail == 2)
            {
                for (int i = 0; i < listofvecs.Count; i++)
                {
                    listofchunksadjacent[i] = (sclevelgenvert)componentparent.getChunklod1(listofvecs[i].X, listofvecs[i].Y, listofvecs[i].Z, out arrayindex);
                }

            }
            else if (levelofdetail == 3)
            {
                for (int i = 0; i < listofvecs.Count; i++)
                {
                    listofchunksadjacent[i] = (sclevelgenvert)componentparent.getChunklod2(listofvecs[i].X, listofvecs[i].Y, listofvecs[i].Z, out arrayindex);
                }

            }
            else if (levelofdetail == 4)
            {
                for (int i = 0; i < listofvecs.Count; i++)
                {
                    listofchunksadjacent[i] = (sclevelgenvert)componentparent.getChunklod3(listofvecs[i].X, listofvecs[i].Y, listofvecs[i].Z, out arrayindex);
                }
            }*/








            /*List<Vector3> listofvecsadjacentfloor = new List<Vector3>();

            //LEFT CHUNK
            somechunkpos = chunkPos;
            somechunkpos.X -= 1.0f;
            listofvecsadjacentfloor.Add(somechunkpos);

            //RIGHT CHUNK
            somechunkpos = chunkPos;
            somechunkpos.X += 1.0f;
            listofvecsadjacentfloor.Add(somechunkpos);

            //BACK CHUNK
            somechunkpos = chunkPos;
            somechunkpos.Z -= 1.0f;
            listofvecsadjacentfloor.Add(somechunkpos);

            //FRONT CHUNK
            somechunkpos = chunkPos;
            somechunkpos.Z += 1.0f;
            listofvecsadjacentfloor.Add(somechunkpos);

            //BOTTOM CHUNK
            somechunkpos = chunkPos;
            somechunkpos.Y -= 1.0f;
            listofvecsadjacentfloor.Add(somechunkpos);

            //TOP CHUNK
            somechunkpos = chunkPos;
            somechunkpos.Y += 1.0f;
            listofvecsadjacentfloor.Add(somechunkpos);*/

            for (int i = 0; i < somearrayofcoordsfloor.Length; i++)
            {
                somearrayofcoordsfloor[i] = new int[3];
            }



            somearrayofcoordsfloor[0][0] = X - 1;
            somearrayofcoordsfloor[0][1] = 0;
            somearrayofcoordsfloor[0][2] = Z;

            somearrayofcoordsfloor[1][0] = X + 1;
            somearrayofcoordsfloor[1][1] = 0;
            somearrayofcoordsfloor[1][2] = Z;

            somearrayofcoordsfloor[2][0] = X;
            somearrayofcoordsfloor[2][1] = 0;
            somearrayofcoordsfloor[2][2] = Z - 1;

            somearrayofcoordsfloor[3][0] = X;
            somearrayofcoordsfloor[3][1] = 0;
            somearrayofcoordsfloor[3][2] = Z + 1;

            somearrayofcoordsfloor[4][0] = X;
            somearrayofcoordsfloor[4][1] = 0;
            somearrayofcoordsfloor[4][2] = Z;

            somearrayofcoordsfloor[5][0] = X;
            somearrayofcoordsfloor[5][1] = 0;
            somearrayofcoordsfloor[5][2] = Z;


            for (int i = 0; i < somearrayofcoordsfloor.Length; i++)
            {
                //if (levelofdetail == 1)
                {
                    //listofchunksadjacentfloor[i] = (sclevelgenvert)componentparent.getChunklod0(somearrayofcoordsfloor[i][0], somearrayofcoordsfloor[i][1], somearrayofcoordsfloor[i][2], out arrayindex);
                    listofchunksadjacentfloor[i] = (tutorialchunkcubemap)sccslevelgen.currentlevelgen.getchunkinlevelgenmap(somearrayofcoordsfloor[i][0], 0, somearrayofcoordsfloor[i][2], levelofdetail, out arrayindex);
                }
                /*else if (levelofdetail == 2)
                {
                    listofchunksadjacentfloor[i] = (sclevelgenvert)componentparent.getChunklod1(listofvecsadjacentfloor[i].X, 0, listofvecsadjacentfloor[i].Z, out arrayindex);


                }
                else if (levelofdetail == 3)
                {
                    listofchunksadjacentfloor[i] = (sclevelgenvert)componentparent.getChunklod2(listofvecsadjacentfloor[i].X, 0, listofvecsadjacentfloor[i].Z, out arrayindex);


                }
                else if (levelofdetail == 4)
                {
                    listofchunksadjacentfloor[i] = (sclevelgenvert)componentparent.getChunklod3(listofvecsadjacentfloor[i].X, 0, listofvecsadjacentfloor[i].Z, out arrayindex);

                }
                else if (levelofdetail == 5)
                {
                    listofchunksadjacentfloor[i] = (sclevelgenvert)componentparent.getChunklod4(listofvecsadjacentfloor[i].X, 0, listofvecsadjacentfloor[i].Z, out arrayindex);

                }*/
            }



            /*
            for (int i = 0; i < listofvecsadjacentfloor.Count; i++)
            {
                ////Console.WriteLine("x:" + listofvecsadjacentfloor[i].X + "/y:" + listofvecsadjacentfloor[i].Y + "/z:" + listofvecsadjacentfloor[i].Z);
                //listofchunksadjacentfloor[i] = componentparent_.getChunklod0((int)Math.Round(listofvecsadjacentfloor[i].X), 0, (int)Math.Round(listofvecsadjacentfloor[i].Z) , out arrayindex);
                listofchunksadjacentfloor[i] = componentparent_.getChunklod0((int)Math.Round(listofvecsadjacentfloor[i].X), 0, (int)Math.Round(listofvecsadjacentfloor[i].Z), out arrayindex); //(int)listofvecs[i].Y
                //listofchunksadjacentfloor[i] = componentparent_.getchunkinlevelgenmap((int)Math.Round(listofvecsadjacentfloor[i].X), (int)Math.Round(listofvecsadjacentfloor[i].Y), (int)Math.Round(listofvecsadjacentfloor[i].Z), levelofdetail); //(int)listofvecs[i].Y



                if (listofchunksadjacentfloor[i] != null)
                {
                    ////Console.WriteLine(listofchunksadjacentfloor[i].chunkPos);
                }

            }*/





            //cannot be zero otherwise each vertex will be at zero
            //levelofdetail += 1;
            //cannot be zero otherwise each vertex will be at zero



            dimsminx = 0;
            dimsminy = 0;
            dimsminz = 0;

            dimsmaxx = 0;
            dimsmaxy = 0;
            dimsmaxz = 0;

            vertminx = 0;
            vertminy = 0;
            vertminz = 0;

            vertmaxx = 0;
            vertmaxy = 0;
            vertmaxz = 0;

            sccsSetMap();
            Regenerate(facetype,  minx,  miny,  minz,  maxx,  maxy,  maxz); //currentPosition



            //vertexArray = vertexlist.ToArray();
            //triangleArray = listOfTriangleIndices.ToArray();






            arrayofvertstop = vertexlisttop.ToArray();
            //arrayoftrigstop = listOfTriangleIndicestop.ToArray();
            vertexcountermemory = vertexlisttop.Count;


            /*if (facetype == 0)
            {
                arrayofvertstop = vertexlisttop.ToArray();
                //arrayoftrigstop = listOfTriangleIndicestop.ToArray();
                vertexcountermemory = vertexlisttop.Count;
            }
            else if (facetype == 1)
            {
                arrayofvertsleft = vertexlistleft.ToArray();
                arrayoftrigsleft = listOfTriangleIndicesleft.ToArray();

                vertexcountermemory = vertexlistleft.Count;
            }
            else if (facetype == 2)
            {
                arrayofvertsright = vertexlistright.ToArray();
                arrayoftrigsright = listOfTriangleIndicesright.ToArray();

                vertexcountermemory = vertexlistright.Count;
            }
            else if (facetype == 3)
            {

                arrayofvertsfront = vertexlistfront.ToArray();
                arrayoftrigsfront = listOfTriangleIndicesfront.ToArray();
                vertexcountermemory = vertexlistfront.Count;
            }
            else if (facetype == 4)
            {
                arrayofvertsback = vertexlistback.ToArray();
                arrayoftrigsback = listOfTriangleIndicesback.ToArray();

                vertexcountermemory = vertexlistback.Count;
            }
            else if (facetype == 5)
            {
                arrayofvertsbottom = vertexlistbottom.ToArray();
                arrayoftrigsbottom = listOfTriangleIndicesbottom.ToArray();
                vertexcountermemory = vertexlistbottom.Count;
            }*/


            /*
            
            if (facetype == 0)
            {
                //vertexArray = vertexlist.ToArray();
                //triangleArray = listOfTriangleIndices.ToArray();
                arrayofvertstop = vertexlisttop.ToArray();
                arrayoftrigstop = listOfTriangleIndicestop.ToArray();
            }
            else if (facetype == 1)
            {
                arrayofvertsleft = vertexlistleft.ToArray();
                arrayoftrigsleft = listOfTriangleIndicesleft.ToArray();

            }
            else if (facetype == 2)
            {
                arrayofvertsright = vertexlistright.ToArray();
                arrayoftrigsright = listOfTriangleIndicesright.ToArray();
            }
            else if (facetype == 3)
            {
                arrayofvertsfront = vertexlistfront.ToArray();
                arrayoftrigsfront = listOfTriangleIndicesfront.ToArray();
            }
            else if (facetype == 4)
            {
                arrayofvertsback = vertexlistback.ToArray();
                arrayoftrigsback = listOfTriangleIndicesback.ToArray();

            }
            else if (facetype == 5)
            {
                arrayofvertsbottom = vertexlistbottom.ToArray();
                arrayoftrigsbottom = listOfTriangleIndicesbottom.ToArray();
            }*/



            /*
            vertexArray = new Vector4[vertexlist.Count];

            for (int i = 0; i < vertexArray.Length; i++)
            {
                vertexArray[i] = vertexlist[i].position;
            }*/

            /*
            norms = new Vector3[vertexlist.Count];

            for (int i = 0; i < vertexArray.Length; i++)
            {
                norms[i] = vertexlist[i].normal;
            }

            tex = new Vector2[vertexlist.Count];

            for (int i = 0; i < vertexArray.Length; i++)
            {
                tex[i] = vertexlist[i].tex;
            }
            */







            /*_tempChunkArrayBottomFace = null;// new int[width * height * depth];
            _tempChunkArrayBackFace = null;// new int[width * height * depth];
            _tempChunkArrayFrontFace = null;// new int[width * height * depth];
            _tempChunkArrayLeftFace = null;//new int[width * height * depth];
            _tempChunkArrayRightFace = null;//new int[width * height * depth];*/
            //_tempChunkArray = null;// new int[width * height * depth];

            //_chunkArray = null;//new int[width * height * depth];

            //_chunkVertexArray0 = null;// new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            /*_chunkVertexArray1 = null;// new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _chunkVertexArray2 = null;// new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _chunkVertexArray3 = null;//new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _chunkVertexArray4 = null;//new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _chunkVertexArray5 = null;// new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];*/

            //_testVertexArray0 = null;//new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            /*_testVertexArray1 = null;//new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _testVertexArray2 = null;//new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _testVertexArray3 = null;//new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _testVertexArray4 = null;//new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _testVertexArray5 = null;// new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];*/

            /*vertexlisttop = null;
            listOfTriangleIndicestop = null;

            vertexlistbottom = null;
            listOfTriangleIndicesbottom = null;

            vertexlistleft = null;
            listOfTriangleIndicesleft = null;

            vertexlistright = null;
            listOfTriangleIndicesright = null;

            vertexlistfront = null;
            listOfTriangleIndicesfront = null;

            vertexlistback = null;
            listOfTriangleIndicesback = null;*/


            /*
            widthdim = null;
            heightdim = null;
            depthdim = null;
            mapvertindexfordims = null;
            mapfirstvertx = null;
            mapfirstverty = null;
            mapfirstvertz = null;

            somenewdimswidth = null;
            somenewdimsheight = null;
            somenewdimsdepth = null;

            somenewfirstvertx = null;
            somenewfirstverty = null;
            somenewfirstvertz = null;*/


            //map = null;

            //listofchunksadjacent = null;
            //listofchunksadjacentfloor = null;

            return map;
        }

        public void newregenerate(int facetype, int minx, int miny, int minz, int maxx, int maxy, int maxz)
        {


            //vertexcount = 0;

            dimsminx = 0;
            dimsminy = 0;
            dimsminz = 0;

            dimsmaxx = 0;
            dimsmaxy = 0;
            dimsmaxz = 0;

            vertminx = 0;
            vertminy = 0;
            vertminz = 0;

            vertmaxx = 0;
            vertmaxy = 0;
            vertmaxz = 0;

            if (vertexlisttop!= null)
            {

                vertexlisttop.Clear();
            }

            vertexcountermemory = 0;
            _newVertzCounter = 0;



            mapvertindexfordims = new int[4 * 4 * 4];
            widthdimtop = new int[4 * 4 * 4];
            heightdimtop = new int[4 * 4 * 4];
            depthdimtop = new int[4 * 4 * 4];
            mapfirstvertxtop = new int[4 * 4 * 4];
            mapfirstvertytop = new int[4 * 4 * 4];
            mapfirstvertztop = new int[4 * 4 * 4];

            sccsSetMap();
            Regenerate(facetype, minx, miny, minz, maxx, maxy, maxz); //currentPosition



            //vertexArray = vertexlist.ToArray();
            //triangleArray = listOfTriangleIndices.ToArray();


            arrayofvertstop = vertexlisttop.ToArray();
            //arrayoftrigstop = listOfTriangleIndicestop.ToArray();
            vertexcountermemory = vertexlisttop.Count;
        }







        public void cleararrays()
        {

            /*_tempChunkArrayBottomFace = null;// new int[width * height * depth];
            _tempChunkArrayBackFace = null;// new int[width * height * depth];
            _tempChunkArrayFrontFace = null;// new int[width * height * depth];
            _tempChunkArrayLeftFace = null;//new int[width * height * depth];
            _tempChunkArrayRightFace = null;//new int[width * height * depth];*/
            _tempChunkArray = null;// new int[width * height * depth];

            //_chunkArray = null;//new int[width * height * depth];

            _chunkVertexArray0 = null;// new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            /*_chunkVertexArray1 = null;// new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _chunkVertexArray2 = null;// new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _chunkVertexArray3 = null;//new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _chunkVertexArray4 = null;//new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _chunkVertexArray5 = null;// new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];*/

            _testVertexArray0 = null;//new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            /*_testVertexArray1 = null;//new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _testVertexArray2 = null;//new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _testVertexArray3 = null;//new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _testVertexArray4 = null;//new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _testVertexArray5 = null;// new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];*/


            vertexlisttop = null;
            /*listOfTriangleIndicestop = null;
            
            vertexlistbottom = null;
            listOfTriangleIndicesbottom = null;

            vertexlistleft = null;
            listOfTriangleIndicesleft = null;

            vertexlistright = null;
            listOfTriangleIndicesright = null;

            vertexlistfront = null;
            listOfTriangleIndicesfront = null;

            vertexlistback = null;
            listOfTriangleIndicesback = null;*/






            mapvertindexfordims = null;
            
            widthdimtop = null;
            heightdimtop = null;
            depthdimtop = null;
            mapfirstvertxtop = null;
            mapfirstvertytop = null;
            mapfirstvertztop = null;


            /*
            widthdimleft = null;
            heightdimleft = null;
            depthdimleft = null;
            mapfirstvertxleft = null;
            mapfirstvertyleft = null;
            mapfirstvertzleft = null;

            widthdimright = null;
            heightdimright = null;
            depthdimright = null;
            mapfirstvertxright = null;
            mapfirstvertyright = null;
            mapfirstvertzright = null;

            widthdimfront = null;
            heightdimfront = null;
            depthdimfront = null;
            mapfirstvertxfront = null;
            mapfirstvertyfront = null;
            mapfirstvertzfront = null;

            widthdimback = null;
            heightdimback = null;
            depthdimback = null;
            mapfirstvertxback = null;
            mapfirstvertyback = null;
            mapfirstvertzback = null;


            widthdimbottom = null;
            heightdimbottom = null;
            depthdimbottom = null;
            mapfirstvertxbottom = null;
            mapfirstvertybottom = null;
            mapfirstvertzbottom = null;*/

            /*
            mapdimsx = null;
            mapdimsy = null;
            mapdimsz = null;

            mapvertx = null;
            mapverty = null;
            mapvertz = null;*/

            //map = null;


            /*somenewdimswidth = null;
            somenewdimsheight = null;
            somenewdimsdepth = null;

            somenewfirstvertx = null;
            somenewfirstverty = null;
            somenewfirstvertz = null;*/

        }



        public void clearsecarrays()
        {

            /*_tempChunkArrayBottomFace = null;// new int[width * height * depth];
            _tempChunkArrayBackFace = null;// new int[width * height * depth];
            _tempChunkArrayFrontFace = null;// new int[width * height * depth];
            _tempChunkArrayLeftFace = null;//new int[width * height * depth];
            _tempChunkArrayRightFace = null;//new int[width * height * depth];*/
            _tempChunkArray = null;// new int[width * height * depth];

            //_chunkArray = null;//new int[width * height * depth];

            _chunkVertexArray0 = null;// new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            /*_chunkVertexArray1 = null;// new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _chunkVertexArray2 = null;// new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _chunkVertexArray3 = null;//new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _chunkVertexArray4 = null;//new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _chunkVertexArray5 = null;// new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];*/

            _testVertexArray0 = null;//new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            /*_testVertexArray1 = null;//new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _testVertexArray2 = null;//new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _testVertexArray3 = null;//new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _testVertexArray4 = null;//new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _testVertexArray5 = null;// new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];*/


            vertexlisttop = null;
            /*listOfTriangleIndicestop = null;
            
            vertexlistbottom = null;
            listOfTriangleIndicesbottom = null;

            vertexlistleft = null;
            listOfTriangleIndicesleft = null;

            vertexlistright = null;
            listOfTriangleIndicesright = null;

            vertexlistfront = null;
            listOfTriangleIndicesfront = null;

            vertexlistback = null;
            listOfTriangleIndicesback = null;*/






            mapvertindexfordims = null;

            widthdimtop = null;
            heightdimtop = null;
            depthdimtop = null;
            mapfirstvertxtop = null;
            mapfirstvertytop = null;
            mapfirstvertztop = null;


            /*
            widthdimleft = null;
            heightdimleft = null;
            depthdimleft = null;
            mapfirstvertxleft = null;
            mapfirstvertyleft = null;
            mapfirstvertzleft = null;

            widthdimright = null;
            heightdimright = null;
            depthdimright = null;
            mapfirstvertxright = null;
            mapfirstvertyright = null;
            mapfirstvertzright = null;

            widthdimfront = null;
            heightdimfront = null;
            depthdimfront = null;
            mapfirstvertxfront = null;
            mapfirstvertyfront = null;
            mapfirstvertzfront = null;

            widthdimback = null;
            heightdimback = null;
            depthdimback = null;
            mapfirstvertxback = null;
            mapfirstvertyback = null;
            mapfirstvertzback = null;


            widthdimbottom = null;
            heightdimbottom = null;
            depthdimbottom = null;
            mapfirstvertxbottom = null;
            mapfirstvertybottom = null;
            mapfirstvertzbottom = null;*/

            /*
            mapdimsx = null;
            mapdimsy = null;
            mapdimsz = null;

            mapvertx = null;
            mapverty = null;
            mapvertz = null;*/

            //map = null;


            /*somenewdimswidth = null;
            somenewdimsheight = null;
            somenewdimsdepth = null;

            somenewfirstvertx = null;
            somenewfirstverty = null;
            somenewfirstvertz = null;*/

        }



        public void sccsSetMap()
        {
            //_newVertzCounter = 0;
            /*_tempChunkArrayBottomFace = new int[width * height * depth];
            _tempChunkArrayBackFace = new int[width * height * depth];
            _tempChunkArrayFrontFace = new int[width * height * depth];
            _tempChunkArrayLeftFace = new int[width * height * depth];
            _tempChunkArrayRightFace = new int[width * height * depth];
            _tempChunkArray = new int[width * height * depth];

            _chunkArray = new int[width * height * depth];

            _chunkVertexArray = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _testVertexArray = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];

            vertexlist = new List<Vector3>();
            triangles = new List<int>();*/

            if (vertexlisttop == null)
            {
                vertexlisttop = new List<tutorialcubeaschunkinst.DVertex>();
            }
            else
            {
                vertexlisttop.Clear();
            }

            /*if (listOfTriangleIndicestop == null)
            {
                listOfTriangleIndicestop = new List<int>();
            }
            else
            {
                listOfTriangleIndicestop.Clear();
            }*/

            /*if (vertexlistbottom == null)
            {
                vertexlistbottom = new List<tutorialcubeaschunkinst.DVertex>();
            }
            else
            {
                vertexlistbottom.Clear();
            }

            if (listOfTriangleIndicesbottom == null)
            {
                listOfTriangleIndicesbottom = new List<int>();
            }
            else
            {
                listOfTriangleIndicesbottom.Clear();
            }



            if (vertexlistleft == null)
            {
                vertexlistleft = new List<tutorialcubeaschunkinst.DVertex>();
            }
            else
            {
                vertexlistleft.Clear();
            }

            if (listOfTriangleIndicesleft == null)
            {
                listOfTriangleIndicesleft = new List<int>();
            }
            else
            {
                listOfTriangleIndicesleft.Clear();
            }




            if (vertexlistright == null)
            {
                vertexlistright = new List<tutorialcubeaschunkinst.DVertex>();
            }
            else
            {
                vertexlistright.Clear();
            }

            if (listOfTriangleIndicesright == null)
            {
                listOfTriangleIndicesright = new List<int>();
            }
            else
            {
                listOfTriangleIndicesright.Clear();
            }

            if (vertexlistfront == null)
            {
                vertexlistfront = new List<tutorialcubeaschunkinst.DVertex>();
            }
            else
            {
                vertexlistfront.Clear();
            }

            if (listOfTriangleIndicesfront == null)
            {
                listOfTriangleIndicesfront = new List<int>();
            }
            else
            {
                listOfTriangleIndicesfront.Clear();
            }

            if (vertexlistback == null)
            {
                vertexlistback = new List<tutorialcubeaschunkinst.DVertex>();
            }
            else
            {
                vertexlistback.Clear();
            }

            if (listOfTriangleIndicesback == null)
            {
                listOfTriangleIndicesback = new List<int>();
            }
            else
            {
                listOfTriangleIndicesback.Clear();
            }*/





            dimsminx = 0;
            dimsminy = 0;
            dimsminz = 0;

            dimsmaxx = 0;
            dimsmaxy = 0;
            dimsmaxz = 0;

            vertminx = 0;
            vertminy = 0;
            vertminz = 0;

            vertmaxx = 0;
            vertmaxy = 0;
            vertmaxz = 0;
            _newVertzCounter = 0;
            //vertexlist.Clear();
            //listOfTriangleIndices.Clear();



            /*
            _tempChunkArrayBottomFace = new int[width * height * depth];
            _tempChunkArrayBackFace = new int[width * height * depth];
            _tempChunkArrayFrontFace = new int[width * height * depth];
            _tempChunkArrayLeftFace = new int[width * height * depth];
            _tempChunkArrayRightFace = new int[width * height * depth];*/
            _tempChunkArray = new int[width * height * depth];

            //_chunkArray = new int[width * height * depth];

            _chunkVertexArray0 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            /*_chunkVertexArray1 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _chunkVertexArray2 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _chunkVertexArray3 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _chunkVertexArray4 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _chunkVertexArray5 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];*/

            _testVertexArray0 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            /*_testVertexArray1 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _testVertexArray2 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _testVertexArray3 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _testVertexArray4 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _testVertexArray5 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];*/




            mapvertindexfordims = new int[4 * 4 * 4];

            widthdimtop = new int[4 * 4 * 4];
            heightdimtop = new int[4 * 4 * 4];
            depthdimtop = new int[4 * 4 * 4];
            mapfirstvertxtop = new int[4 * 4 * 4];
            mapfirstvertytop = new int[4 * 4 * 4];
            mapfirstvertztop = new int[4 * 4 * 4];

            /*widthdimleft = new int[width * height * depth];
            heightdimleft = new int[width * height * depth];
            depthdimleft = new int[width * height * depth];
            mapfirstvertxleft = new int[width * height * depth];
            mapfirstvertyleft = new int[width * height * depth];
            mapfirstvertzleft = new int[width * height * depth];

            widthdimright = new int[width * height * depth];
            heightdimright = new int[width * height * depth];
            depthdimright = new int[width * height * depth];
            mapfirstvertxright = new int[width * height * depth];
            mapfirstvertyright = new int[width * height * depth];
            mapfirstvertzright = new int[width * height * depth];

            widthdimfront = new int[width * height * depth];
            heightdimfront = new int[width * height * depth];
            depthdimfront = new int[width * height * depth];
            mapfirstvertxfront = new int[width * height * depth];
            mapfirstvertyfront = new int[width * height * depth];
            mapfirstvertzfront = new int[width * height * depth];

            widthdimback = new int[width * height * depth];
            heightdimback = new int[width * height * depth];
            depthdimback = new int[width * height * depth];
            mapfirstvertxback = new int[width * height * depth];
            mapfirstvertyback = new int[width * height * depth];
            mapfirstvertzback = new int[width * height * depth];

            widthdimbottom = new int[width * height * depth];
            heightdimbottom = new int[width * height * depth];
            depthdimbottom = new int[width * height * depth];
            mapfirstvertxbottom = new int[width * height * depth];
            mapfirstvertybottom = new int[width * height * depth];
            mapfirstvertzbottom = new int[width * height * depth];*/


            someixtop = 0;
            someiytop = 0;
            someiztop = 0;

            someixleft = 0;
            someiyleft = 0;
            someizleft = 0;

            someixright = 0;
            someiyright = 0;
            someizright = 0;

            someixfront = 0;
            someiyfront = 0;
            someizfront = 0;

            someixback = 0;
            someiyback = 0;
            someizback = 0;

            someixbottom = 0;
            someiybottom = 0;
            someizbottom = 0;



            //vertexlist = new List<Vector3>();
            /*
            vertexlisttop = new List<tutorialcubeaschunkinst.DVertex>();
            listOfTriangleIndicestop = new List<int>();

            vertexlistback = new List<tutorialcubeaschunkinst.DVertex>();
            listOfTriangleIndicesback = new List<int>();


            vertexlistleft = new List<tutorialcubeaschunkinst.DVertex>();
            listOfTriangleIndicesleft = new List<int>();


            vertexlistright = new List<tutorialcubeaschunkinst.DVertex>();
            listOfTriangleIndicesright = new List<int>();


            vertexlistbottom = new List<tutorialcubeaschunkinst.DVertex>();
            listOfTriangleIndicesbottom = new List<int>();

            vertexlistfront = new List<tutorialcubeaschunkinst.DVertex>();
            listOfTriangleIndicesfront = new List<int>();
            */

            /*somenewdimswidth = new int[width * height * depth];
            somenewdimsheight = new int[width * height * depth];
            somenewdimsdepth = new int[width * height * depth];

            somenewfirstvertx = new int[width * height * depth];
            somenewfirstverty = new int[width * height * depth];
            somenewfirstvertz = new int[width * height * depth];
            */






            for (int t = 0; t < vertexlistWidth * vertexlistHeight * vertexlistDepth; t++) //total
            {
                if (t < total)
                {
                    if (map[t] == 1 || map[t] == 3)
                    {
                        //_chunkArray[t] = 1;

                        _tempChunkArray[t] = map[t]; //map[t]
                        /*_tempChunkArrayRightFace[t] = 1;
                        _tempChunkArrayLeftFace[t] = 1;

                        _tempChunkArrayBottomFace[t] = 1;
                        _tempChunkArrayBackFace[t] = 1;
                        _tempChunkArrayFrontFace[t] = 1;*/
                    }
                    else
                    {
                        //_chunkArray[t] = 0;

                        _tempChunkArray[t] = 0;
                        /*_tempChunkArrayRightFace[t] = 0;
                        _tempChunkArrayLeftFace[t] = 0;

                        _tempChunkArrayBottomFace[t] = 0;
                        _tempChunkArrayBackFace[t] = 0;
                        _tempChunkArrayFrontFace[t] = 0;*/

                    }
                }

                if (t < vertexlistWidth * vertexlistHeight * vertexlistDepth)
                {
                    _chunkVertexArray0[t] = 0;
                    /*_chunkVertexArray1[t] = 0;
                    _chunkVertexArray2[t] = 0;
                    _chunkVertexArray3[t] = 0;
                    _chunkVertexArray4[t] = 0;
                    _chunkVertexArray5[t] = 0;*/

                    _testVertexArray0[t] = 0;
                    /*_testVertexArray1[t] = 0;
                    _testVertexArray2[t] = 0;
                    _testVertexArray3[t] = 0;
                    _testVertexArray4[t] = 0;
                    _testVertexArray5[t] = 0;*/
                }
            }
        }












        public void setvertex(int facetype)
        {

            //vertexArray = vertexlist.ToArray();
            //triangleArray = listOfTriangleIndices.ToArray();
            /*arrayofvertstop = vertexlisttop.ToArray();
            arrayoftrigstop = listOfTriangleIndicestop.ToArray();

            arrayofvertsbottom = vertexlistbottom.ToArray();
            arrayoftrigsbottom = listOfTriangleIndicesbottom.ToArray();

            arrayofvertsfront = vertexlistfront.ToArray();
            arrayoftrigsfront = listOfTriangleIndicesfront.ToArray();

            arrayofvertsback = vertexlistback.ToArray();
            arrayoftrigsback = listOfTriangleIndicesback.ToArray();

            arrayofvertsleft = vertexlistleft.ToArray();
            arrayoftrigsleft = listOfTriangleIndicesleft.ToArray();

            arrayofvertsright = vertexlistright.ToArray();
            arrayoftrigsright = listOfTriangleIndicesright.ToArray();


            if (facetype == 0)
            {
                vertexcountermemory = vertexlisttop.Count;
            }
            else if (facetype == 1)
            {
                vertexcountermemory = vertexlistleft.Count;
            }
            else if (facetype == 2)
            {
                vertexcountermemory = vertexlistright.Count;
            }
            else if (facetype == 3)
            {
                vertexcountermemory = vertexlistfront.Count;
            }
            else if (facetype == 4)
            {
                vertexcountermemory = vertexlistback.Count;
            }
            else if (facetype == 5)
            {
                vertexcountermemory = vertexlistbottom.Count;
            }*/



            arrayofvertstop = vertexlisttop.ToArray();
            //arrayoftrigstop = listOfTriangleIndicestop.ToArray();
            vertexcountermemory = vertexlisttop.Count;

            /*if (facetype == 0)
            {
                arrayofvertstop = vertexlisttop.ToArray();
                arrayoftrigstop = listOfTriangleIndicestop.ToArray();
                vertexcountermemory = vertexlisttop.Count;
            }
            else if (facetype == 1)
            {
                arrayofvertsleft = vertexlistleft.ToArray();
                arrayoftrigsleft = listOfTriangleIndicesleft.ToArray();

                vertexcountermemory = vertexlistleft.Count;
            }
            else if (facetype == 2)
            {
                arrayofvertsright = vertexlistright.ToArray();
                arrayoftrigsright = listOfTriangleIndicesright.ToArray();

                vertexcountermemory = vertexlistright.Count;
            }
            else if (facetype == 3)
            {

                arrayofvertsfront = vertexlistfront.ToArray();
                arrayoftrigsfront = listOfTriangleIndicesfront.ToArray();
                vertexcountermemory = vertexlistfront.Count;
            }
            else if (facetype == 4)
            {
                arrayofvertsback = vertexlistback.ToArray();
                arrayoftrigsback = listOfTriangleIndicesback.ToArray();

                vertexcountermemory = vertexlistback.Count;
            }
            else if (facetype == 5)
            {
                arrayofvertsbottom = vertexlistbottom.ToArray();
                arrayoftrigsbottom = listOfTriangleIndicesbottom.ToArray();
                vertexcountermemory = vertexlistbottom.Count;
            }*/


        }





        public void Regenerate(int facetype, int minx, int miny, int minz, int maxx, int maxy, int maxz)
        {





            //vertexlist.Clear();
            //listOfTriangleIndices.Clear();

            /*_tempChunkArrayBottomFace = new int[width * height * depth];
            _tempChunkArrayBackFace = new int[width * height * depth];
            _tempChunkArrayFrontFace = new int[width * height * depth];
            _tempChunkArrayLeftFace = new int[width * height * depth];
            _tempChunkArrayRightFace = new int[width * height * depth];
            _tempChunkArray = new int[width * height * depth];

            _chunkArray = new int[width * height * depth];

            _chunkVertexArray0 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _chunkVertexArray1 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _chunkVertexArray2 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _chunkVertexArray3 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _chunkVertexArray4 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _chunkVertexArray5 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];

            _testVertexArray0 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _testVertexArray1 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _testVertexArray2 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _testVertexArray3 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _testVertexArray4 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
            _testVertexArray5 = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];*/
            //normalslist.Clear();
            //colorslist.Clear();
            //indexPoslist.Clear();
            //textureslist.Clear();

            /*
            int xi = 0;
            int yi = 0;
            int zi = 0;*/


            //CalculateintsForFaces();
            //CreateChunkFaces();

            int someswitchforbuildtype = 1;

            //0 to 512 when using map of 8x8x8...
            for (int x = minx * 4; x < maxx * 4; x++)
            {
                var xi = x;
                for (int y = miny * 4; y < maxy * 4; y++)
                {
                    var yi = y;
                    for (int z = minz * 4; z < maxz * 4; z++)
                    {
                        var zi = z;





                        /*
                        buildTopFace(xi, yi, zi, 1.0f);
                        buildTopLeft(xi, yi, zi, 1.0f);
                        buildTopRight(xi, yi, zi, 1.0f);
                        //buildFrontFace(xi, yi, zi, 1.0f);
                        buildBackFace(xi, yi, zi, 1.0f);
                        buildBottomFace(xi, yi, zi, 1.0f);*/








                        if (someswitchforbuildtype == 0)
                        {
                            if (facetype == 0)
                            {
                                buildTopFace(xi, yi, zi, 1.0f);
                            }
                            else if (facetype == 1)
                            {
                                buildTopLeft(xi, yi, zi, 1.0f);
                            }
                            else if (facetype == 2)
                            {
                                buildTopRight(xi, yi, zi, 1.0f);
                            }
                            else if (facetype == 3)
                            {
                                buildFrontFace(xi, yi, zi, 1.0f);
                            }
                            else if (facetype == 4)
                            {
                                buildBackFace(xi, yi, zi, 1.0f);
                            }
                            else if (facetype == 5)
                            {
                                buildBottomFace(xi, yi, zi, 1.0f);
                            }
                        }





                        index = xi + (width) * (yi + (height) * zi);
                        block = map[index];
                        if (block == 1 )
                        {








                            if (someswitchforbuildtype == 1)
                            {


                                if (facetype == 1)
                                {
                                    if (IsTransparent(xi - 1, yi, zi))
                                    {
                                        if (xi != width - 1 && xi != 0)
                                        {
                                            buildTopLeft(xi, yi, zi, 1.0f);
                                        }
                                        else if (xi == width - 1)
                                        {
                                            buildTopLeft(xi, yi, zi, 1.0f);
                                        }
                                        else if (xi == 0)
                                        {
                                            //Vector3 somechunkpos = chunkPos;
                                            //somechunkpos.X -= 1.0f;
                                            //sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunklod0(somechunkpos.X, somechunkPos[1], somechunkpos.Z, out arrayindex);

                                            if (listofchunksadjacent[0] != null && levelofdetail == 1) // && levelofdetail == 1  && levelofdetail == 1
                                            {
                                                ////Console.WriteLine(chunkPos + " " + someChunk.chunkPos);

                                                if (listofchunksadjacent[0].map != null)
                                                {

                                                    ////Console.WriteLine("mapl:" + listofchunksadjacent[0].map.Length);
                                                    if (listofchunksadjacent[0].GetByte(width - 1, yi, zi) == 0) //someChunk.IsTransparent(width - 1, yi, zi)) //
                                                    {

                                                        /*Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize,1) + chunkoriginpos;
                                                        var offset1 = back * planeSize;
                                                        var offset2 = down * planeSize;
                                                        createleftFace(start + up * planeSize + forward * planeSize, offset1, offset2);
                                                        */

                                                        /*if (typeofterraintiles != 0)
                                                        {
                                                            Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize,1) + chunkoriginpos;
                                                            var offset1 = back * planeSize;
                                                            var offset2 = down * planeSize;
                                                            createleftFace(start + up * planeSize + forward * planeSize, offset1, offset2);
                                                        }
                                                        else
                                                        {
                                                            buildTopLeft(xi, yi, zi, 1.0f);
                                                        }*/

                                                        buildTopLeft(xi, yi, zi, 1.0f);
                                                    }
                                                    else
                                                    {
                                                        //buildTopLeft(xi, yi, zi, 1.0f);
                                                        //buildTopLeft(xi, yi, zi, 1.0f);
                                                    }
                                                }
                                                else
                                                {
                                                    //buildTopLeft(xi, yi, zi, 1.0f);
                                                }
                                            }
                                            else
                                            {
                                                //var somechunkpos = chunkPos;
                                                //somechunkpos.X -= 1.0f;
                                                //var someChunk = (sclevelgenvert)componentparent.getChunklod0(somechunkpos.X, 0, somechunkpos.Z);

                                                if (listofchunksadjacentfloor[0] != null)
                                                {
                                                    buildTopLeft(xi, yi, zi, 1.0f);
                                                }

                                                if (levelofdetail != 1)
                                                {
                                                    if (listofchunksadjacent[0] != null)
                                                    {
                                                        buildTopLeft(xi, yi, zi, 1.0f);
                                                    }

                                                }



                                                /*
                                                if (listofchunksadjacent[0] == null && levelofdetail == 1 && chunkPos[1] == levellimitfloory) //&& levelofdetail == 1
                                                {
                                                    buildTopLeft(xi, yi, zi, 1.0f);
                                                }
                                                else if (listofchunksadjacent[0] == null && levelofdetail == 1 && chunkPos[1] == sclevelgen.wallheightsize)
                                                {
                                                    //dont build
                                                }
                                                else
                                                {
                                                    if (listofchunksadjacentfloor[0] != null && chunkPos[1] != sclevelgen.wallheightsize)
                                                    {
                                                        buildTopLeft(xi, yi, zi, 1.0f);
                                                    }
                                                }
                                                */


                                                //buildTopLeft(xi, yi, zi, 1.0f);
                                            }
                                        }
                                    }
                                }

                                if (facetype == 2)
                                {


                                    if (IsTransparent(xi + 1, yi, zi))
                                    {
                                        if (xi != width - 1 && xi != 0)
                                        {
                                            buildTopRight(xi, yi, zi, 1.0f);
                                        }
                                        else if (xi == 0)
                                        {
                                            buildTopRight(xi, yi, zi, 1.0f);
                                        }
                                        else if (xi == width - 1)
                                        {

                                            //Vector3 somechunkpos = chunkPos;
                                            //somechunkpos.X += 1.0f;
                                            //sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunklod0(somechunkpos.X, somechunkPos[1], somechunkpos.Z);


                                            if (listofchunksadjacent[1] != null && levelofdetail == 1)//&& levelofdetail == 1
                                            {
                                                ////Console.WriteLine(chunkPos + " " + someChunk.chunkPos);

                                                if (listofchunksadjacent[1].map != null)
                                                {
                                                    if (listofchunksadjacent[1].GetByte(0, yi, zi) == 0) //someChunk.IsTransparent(width - 1, yi, zi)) //
                                                    {
                                                        /*Vector4 start = new Vector4(xi * planeSize * levelofdetail* levelofdetailmul, yi * planeSize * levelofdetail * levelofdetailmul, zi * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos;
                                                        var offset1 = up * planeSize;
                                                        var offset2 = forward * planeSize;
                                                        createRightFace(start + right * planeSize, offset1, offset2);*/
                                                        /*
                                                        if (typeofterraintiles != 0)
                                                        {
                                                            Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize,1) + chunkoriginpos;
                                                            var offset1 = up * planeSize;
                                                            var offset2 = forward * planeSize;
                                                            createRightFace(start + right * planeSize, offset1, offset2);
                                                        }
                                                        else
                                                        {
                                                            buildTopRight(xi, yi, zi, 1.0f);
                                                        }
                                                        */

                                                        buildTopRight(xi, yi, zi, 1.0f);
                                                    }
                                                    else
                                                    {

                                                    }
                                                }
                                                else
                                                {
                                                    //buildTopRight(xi, yi, zi, 1.0f);
                                                }
                                            }
                                            else
                                            {
                                                //var somechunkpos = chunkPos;
                                                //somechunkpos.X += 1.0f;
                                                //var someChunk = (sclevelgenvert)componentparent.getChunklod0(somechunkpos.X, 0, somechunkpos.Z);

                                                if (listofchunksadjacentfloor[1] != null)
                                                {

                                                    buildTopRight(xi, yi, zi, 1.0f);
                                                }

                                                if (levelofdetail != 1)
                                                {
                                                    if (listofchunksadjacent[1] != null)
                                                    {
                                                        buildTopRight(xi, yi, zi, 1.0f);
                                                    }

                                                }
                                                //buildTopRight(xi, yi, zi, 1.0f);
                                            }
                                        }
                                    }



                                }



                                if (facetype == 5)
                                {


                                    if (IsTransparent(xi, yi - 1, zi))
                                    {
                                        if (yi != height - 1 && yi != 0)
                                        {
                                            buildBottomFace(xi, yi, zi, 1.0f);
                                        }
                                        else if (yi == height - 1)
                                        {
                                            buildBottomFace(xi, yi, zi, 1.0f);
                                        }
                                        else if (yi == 0)
                                        {

                                            //Vector3 somechunkpos = chunkPos;
                                            //somechunkPos[1] -= 1.0f;
                                            //sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunklod0(somechunkpos.X, somechunkPos[1], somechunkpos.Z);

                                            if (listofchunksadjacent[4] != null && levelofdetail == 1 && chunkPos[1] != levellimitfloory) //&& levelofdetail == 1 // && chunkPos[1] != levellimitfloory
                                            {
                                                ////Console.WriteLine(chunkPos + " " + someChunk.chunkPos);

                                                if (listofchunksadjacent[4].map != null)
                                                {
                                                    if (listofchunksadjacent[4].GetByte(xi, height - 1, zi) == 0) //someChunk.IsTransparent(width - 1, yi, zi)) //
                                                    {
                                                        /*Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize,1) + chunkoriginpos;
                                                        var offset1 = right * planeSize;
                                                        var offset2 = forward * planeSize;
                                                        createBottomFace(start, offset1, offset2);*/
                                                        /*if (typeofterraintiles != 0)
                                                        {
                                                            Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize,1) + chunkoriginpos;
                                                            var offset1 = right * planeSize;
                                                            var offset2 = forward * planeSize;
                                                            createBottomFace(start, offset1, offset2);
                                                        }
                                                        else
                                                        {
                                                            buildBottomFace(xi, yi, zi, 1.0f);
                                                        }*/
                                                        buildBottomFace(xi, yi, zi, 1.0f);
                                                    }
                                                    else
                                                    {
                                                        //buildBottomFace(xi, yi, zi, 1.0f);
                                                    }
                                                }
                                                else
                                                {
                                                    //buildBottomFace(xi, yi, zi, 1.0f);
                                                }
                                            }
                                            else
                                            {





                                                if (listofchunksadjacent[4] != null && chunkPos[1] != levellimitfloory && typeofterraintiles == 1101 ||
                                listofchunksadjacent[4] != null && chunkPos[1] != levellimitfloory && typeofterraintiles == 1102 ||
                                listofchunksadjacent[4] != null && chunkPos[1] != levellimitfloory && typeofterraintiles == 1103 ||
                                listofchunksadjacent[4] != null && chunkPos[1] != levellimitfloory && typeofterraintiles == 1104 ||
                                listofchunksadjacent[4] != null && chunkPos[1] != levellimitfloory && typeofterraintiles == 1105 ||
                                listofchunksadjacent[4] != null && chunkPos[1] != levellimitfloory && typeofterraintiles == 1106 ||
                                listofchunksadjacent[4] != null && chunkPos[1] != levellimitfloory && typeofterraintiles == 1107 ||
                                listofchunksadjacent[4] != null && chunkPos[1] != levellimitfloory && typeofterraintiles == 1108 ||
                                listofchunksadjacent[4] != null && chunkPos[1] != levellimitfloory && typeofterraintiles == 1109 ||
                                listofchunksadjacent[4] != null && chunkPos[1] != levellimitfloory && typeofterraintiles == 1110 ||
                                listofchunksadjacent[4] != null && chunkPos[1] != levellimitfloory && typeofterraintiles == 1111 ||
                                listofchunksadjacent[4] != null && chunkPos[1] != levellimitfloory && typeofterraintiles == 1112 ||
                                typeofterraintiles == 1115)
                                                {
                                                    buildBottomFace(xi, yi, zi, 1.0f);
                                                }



                                                if (listofchunksadjacent[4] != null && chunkPos[1] == levellimitfloory && typeofterraintiles == 1101 && yi != 0 ||
                                                   listofchunksadjacent[4] != null && chunkPos[1] == levellimitfloory && typeofterraintiles == 1102 && yi != 0 ||
                                                   listofchunksadjacent[4] != null && chunkPos[1] == levellimitfloory && typeofterraintiles == 1103 && yi != 0 ||
                                                   listofchunksadjacent[4] != null && chunkPos[1] == levellimitfloory && typeofterraintiles == 1104 && yi != 0 ||
                                                   listofchunksadjacent[4] != null && chunkPos[1] == levellimitfloory && typeofterraintiles == 1105 && yi != 0 ||
                                                   listofchunksadjacent[4] != null && chunkPos[1] == levellimitfloory && typeofterraintiles == 1106 && yi != 0 ||
                                                   listofchunksadjacent[4] != null && chunkPos[1] == levellimitfloory && typeofterraintiles == 1107 && yi != 0 ||
                                                   listofchunksadjacent[4] != null && chunkPos[1] == levellimitfloory && typeofterraintiles == 1108 && yi != 0 ||
                                                   listofchunksadjacent[4] != null && chunkPos[1] == levellimitfloory && typeofterraintiles == 1109 && yi != 0 ||
                                                   listofchunksadjacent[4] != null && chunkPos[1] == levellimitfloory && typeofterraintiles == 1110 && yi != 0 ||
                                                   listofchunksadjacent[4] != null && chunkPos[1] == levellimitfloory && typeofterraintiles == 1111 && yi != 0 ||
                                                   listofchunksadjacent[4] != null && chunkPos[1] == levellimitfloory && typeofterraintiles == 1112 && yi != 0)
                                                {
                                                    buildBottomFace(xi, yi, zi, 1.0f);
                                                }


                                                if (typeofterraintiles == 0 && yi != 0)
                                                {
                                                    buildBottomFace(xi, yi, zi, 1.0f);
                                                }












                                                /*

                                                if (listofchunksadjacent[4] == null && chunkPos[1] == 0.0f)
                                                {
                                                    buildBottomFace(xi, yi, zi, 1.0f);
                                                }
                                                else if (listofchunksadjacent[4] != null && chunkPos[1] != sclevelgen.wallheightsize - 1)
                                                {
                                                    buildBottomFace(xi, yi, zi, 1.0f);
                                                }

                                                */


                                                /*if (listofchunksadjacent[4] != null && chunkPos[1] == sclevelgen.wallheightsize - 1)
                                                {
                                                    buildBottomFace(xi, yi, zi, 1.0f);
                                                }*/
                                                /*else if (listofchunksadjacent[4] == null && chunkPos[1] != levellimitfloory)
                                                {
                                                    buildBottomFace(xi, yi, zi, 1.0f);
                                                }*/











                                                /*
                                                if (listofchunksadjacent[4] == null && levelofdetail != 1 && levelofdetail != 5 && chunkPos[1] == sclevelgen.wallheightsize - 1)
                                                {
                                                    buildBottomFace(xi, yi, zi, 1.0f);
                                                }
                                                else if (listofchunksadjacent[4] == null && levelofdetail == 5 && chunkPos[1] == sclevelgen.wallheightsize - 1 && yi == 0)
                                                {
                                                    //buildTopFace(xi, yi, zi, 1.0f);
                                                }

                                                if (listofchunksadjacent[4] != null && levelofdetail != 1 && chunkPos[1] != sclevelgen.wallheightsize - 1)
                                                {
                                                    //buildTopFace(xi, yi, zi, 1.0f);
                                                }
                                                if (listofchunksadjacent[4] == null && levelofdetail != 1 && chunkPos[1] != sclevelgen.wallheightsize - 1)
                                                {
                                                    buildBottomFace(xi, yi, zi, 1.0f);
                                                }*/








                                                /*if (listofchunksadjacent[5] == null && levelofdetail != 1 && levelofdetail != 5 && chunkPos[1] == levellimitfloory && yi != 0)
                                                {
                                                    buildBottomFace(xi, yi, zi, 1.0f);
                                                }
                                                else if (listofchunksadjacent[5] == null && levelofdetail == 5 && chunkPos[1] == levellimitfloory && yi == 0)
                                                {
                                                    //buildTopFace(xi, yi, zi, 1.0f);
                                                }

                                                if (listofchunksadjacent[5] != null && levelofdetail != 1 && chunkPos[1] != levellimitfloory)
                                                {
                                                    //buildTopFace(xi, yi, zi, 1.0f);
                                                }
                                                if (listofchunksadjacent[5] == null && levelofdetail != 1 && chunkPos[1] != levellimitfloory)
                                                {
                                                    buildBottomFace(xi, yi, zi, 1.0f);
                                                }*/

                                                /*
                                                if (listofchunksadjacent[4] == null && chunkPos[1] == levellimitfloory && yi == 0)
                                                {
                                                    //buildBottomFace(xi, yi, zi, 1.0f);
                                                }
                                                else //if()
                                                {
                                                    buildBottomFace(xi, yi, zi, 1.0f);
                                                }*/




                                                /*
                                                if (listofchunksadjacentfloor[4] != null && chunkPos[1] != levellimitfloory)
                                                {

                                                    buildBottomFace(xi, yi, zi, 1.0f);
                                                }
                                                else if (listofchunksadjacentfloor[4] != null && chunkPos[1] == levellimitfloory && yi != 0)
                                                {
                                                    buildBottomFace(xi, yi, zi, 1.0f);
                                                }
                                                else if (listofchunksadjacent[4] != null && levelofdetail == 1 && chunkPos[1] == levellimitfloory && yi != 0)
                                                {
                                                    buildBottomFace(xi, yi, zi, 1.0f);
                                                }

                                                if (levelofdetail != 1)
                                                {
                                                    if (listofchunksadjacent[4] == null && chunkPos[1] == levellimitfloory)
                                                    {
                                                        buildBottomFace(xi, yi, zi, 1.0f);
                                                    }

                                                }


                                                if (levelofdetail == 1)
                                                {
                                                    if (chunkPos[1] == sclevelgen.wallheightsize - 1 && yi == 0)
                                                    {
                                                        buildBottomFace(xi, yi, zi, 1.0f);
                                                    }

                                                }*/


                                                //buildBottomFace(xi, yi, zi, 1.0f);


                                                /*if (listofchunksadjacent[4] == null && chunkPos[1] == sclevelgen.wallheightsize) //&& levelofdetail == 1 // && levelofdetail == 1 
                                                {
                                                    buildBottomFace(xi, yi, zi, 1.0f);
                                                }
                                                else if (listofchunksadjacent[4] == null && levelofdetail == 1 && chunkPos[1] == levellimitfloory)
                                                {
                                                    //dont build
                                                }
                                                else
                                                {
                                                    if (listofchunksadjacentfloor[4] != null && chunkPos[1] != levellimitfloory)
                                                    {
                                                        buildBottomFace(xi, yi, zi, 1.0f);
                                                    }
                                                }*/

                                                //var somechunkpos = chunkPos;
                                                //somechunkPos[1] -= 1.0f;
                                                //var someChunk = (sclevelgenvert)componentparent.getChunklod0(somechunkpos.X, 0, somechunkpos.Z);

                                                /*if (listofchunksadjacentfloor[4] != null && chunkPos[1] != levellimitfloory)
                                                {
                                                    buildBottomFace(xi, yi, zi, 1.0f);
                                                }



                                                if (levelofdetail != 1)
                                                {
                                                    if (listofchunksadjacent[4] != null)
                                                    {
                                                        buildBottomFace(xi, yi, zi, 1.0f);
                                                    }
                                                }*/
                                                //buildBottomFace(xi, yi, zi, 1.0f);
                                            }
                                        }
                                    }
                                }
                                /*
                                if (IsTransparent(xi, yi + 1, zi))
                                {
                                    Vector4 start = new Vector4(xi, yi, zi, 1);
                                    var offset1 = Vector4.UnitZ;
                                    var offset2 = Vector4.UnitX;
                                    createTopFace(start + Vector4.UnitY, offset1, offset2, xi,yi,zi);
                                }
                                */

                                /*
                                if (IsTransparent(xi, yi + 1, zi))
                                {
                                    buildTopFace(xi, yi, zi, 1.0f);
                                }*/





                                if (facetype == 0)
                                {


                                    if (IsTransparent(xi, yi + 1, zi))
                                    {
                                        if (yi != height - 1 && yi != 0)
                                        {
                                            buildTopFace(xi, yi, zi, 1.0f);
                                        }
                                        else if (yi == 0)
                                        {
                                            buildTopFace(xi, yi, zi, 1.0f);
                                        }
                                        else if (yi == height - 1)
                                        {
                                            //buildTopFace(xi, yi, zi, 1.0f);
                                            //Vector3 somechunkpos = chunkPos;
                                            //somechunkPos[1] += 1.0f;
                                            //sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunklod0(somechunkpos.X, somechunkPos[1], somechunkpos.Z);


                                            if (listofchunksadjacent[5] != null && levelofdetail == 1 && chunkPos[1] != sccslevelgen.wallheightsize - 1) //&& levelofdetail == 1
                                            {
                                                ////Console.WriteLine(chunkPos + " " + someChunk.chunkPos);

                                                if (listofchunksadjacent[5].map != null)
                                                {
                                                    if (listofchunksadjacent[5].GetByte(xi, 0, zi) == 0) //someChunk.IsTransparent(width - 1, yi, zi)) //
                                                    {
                                                        /*Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize,1) + chunkoriginpos;
                                                        var offset1 = forward * planeSize;
                                                        var offset2 = right * planeSize;
                                                        createTopFace(start + up * planeSize, offset1, offset2);*/

                                                        /*if (typeofterraintiles != 0)
                                                        {
                                                            Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize,1) + chunkoriginpos;
                                                            var offset1 = forward * planeSize;
                                                            var offset2 = right * planeSize;
                                                            createTopFace(start + up * planeSize, offset1, offset2);
                                                        }
                                                        else
                                                        {
                                                            buildTopFace(xi, yi, zi, 1.0f);
                                                        }*/
                                                        buildTopFace(xi, yi, zi, 1.0f);
                                                    }
                                                    else
                                                    {
                                                        /*Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize,1) + chunkoriginpos;
                                                        var offset1 = forward * planeSize;
                                                        var offset2 = right * planeSize;
                                                        createTopFace(start + up * planeSize, offset1, offset2);*/
                                                        //buildTopFace(xi, yi, zi, 1.0f);
                                                    }
                                                }
                                                else
                                                {
                                                    //buildTopFace(xi, yi, zi, 1.0f);
                                                }
                                            }
                                            else
                                            {

                                                /*if (listofchunksadjacent[5] == null && levelofdetail != 1 && levelofdetail != 5 && chunkPos[1] == sclevelgen.wallheightsize - 1 && yi != height - 1)
                                                {
                                                    buildTopFace(xi, yi, zi, 1.0f);
                                                }
                                                else if (listofchunksadjacent[5] == null && levelofdetail == 5 && chunkPos[1] == sclevelgen.wallheightsize - 1 && yi == height - 1)
                                                {
                                                    //buildTopFace(xi, yi, zi, 1.0f);
                                                }

                                                if (listofchunksadjacent[5] != null && levelofdetail != 1 && chunkPos[1] != sclevelgen.wallheightsize - 1)
                                                {
                                                    //buildTopFace(xi, yi, zi, 1.0f);
                                                }
                                                if (listofchunksadjacent[5] == null && levelofdetail != 1 && chunkPos[1] != sclevelgen.wallheightsize - 1)
                                                {
                                                    buildTopFace(xi, yi, zi, 1.0f);
                                                }*/




                                                /*if (listofchunksadjacent[5] == null && chunkPos[1] != sclevelgen.wallheightsize - 1)
                                                {
                                                    buildTopFace(xi, yi, zi, 1.0f);
                                                }
                                                else if (listofchunksadjacent[5] != null && chunkPos[1] != levellimitfloory)
                                                {
                                                    buildTopFace(xi, yi, zi, 1.0f);
                                                }

                                                if (listofchunksadjacent[5] == null && chunkPos[1] == levellimitfloory)
                                                {
                                                    buildTopFace(xi, yi, zi, 1.0f);
                                                }*/

                                                if (listofchunksadjacent[5] != null && chunkPos[1] != sccslevelgen.wallheightsize - 1 && typeofterraintiles == 1101 ||
                                                    listofchunksadjacent[5] != null && chunkPos[1] != sccslevelgen.wallheightsize - 1 && typeofterraintiles == 1102 ||
                                                    listofchunksadjacent[5] != null && chunkPos[1] != sccslevelgen.wallheightsize - 1 && typeofterraintiles == 1103 ||
                                                    listofchunksadjacent[5] != null && chunkPos[1] != sccslevelgen.wallheightsize - 1 && typeofterraintiles == 1104 ||
                                                    listofchunksadjacent[5] != null && chunkPos[1] != sccslevelgen.wallheightsize - 1 && typeofterraintiles == 1105 ||
                                                    listofchunksadjacent[5] != null && chunkPos[1] != sccslevelgen.wallheightsize - 1 && typeofterraintiles == 1106 ||
                                                    listofchunksadjacent[5] != null && chunkPos[1] != sccslevelgen.wallheightsize - 1 && typeofterraintiles == 1107 ||
                                                    listofchunksadjacent[5] != null && chunkPos[1] != sccslevelgen.wallheightsize - 1 && typeofterraintiles == 1108 ||
                                                    listofchunksadjacent[5] != null && chunkPos[1] != sccslevelgen.wallheightsize - 1 && typeofterraintiles == 1109 ||
                                                    listofchunksadjacent[5] != null && chunkPos[1] != sccslevelgen.wallheightsize - 1 && typeofterraintiles == 1110 ||
                                                    listofchunksadjacent[5] != null && chunkPos[1] != sccslevelgen.wallheightsize - 1 && typeofterraintiles == 1111 ||
                                                    listofchunksadjacent[5] != null && chunkPos[1] != sccslevelgen.wallheightsize - 1 && typeofterraintiles == 1112 ||
                                                    typeofterraintiles == 0)
                                                {
                                                    buildTopFace(xi, yi, zi, 1.0f);
                                                }



                                                if (listofchunksadjacent[5] != null && chunkPos[1] == sccslevelgen.wallheightsize - 1 && typeofterraintiles == 1101 && yi != height - 1 ||
                                                   listofchunksadjacent[5] != null && chunkPos[1] == sccslevelgen.wallheightsize - 1 && typeofterraintiles == 1102 && yi != height - 1 ||
                                                   listofchunksadjacent[5] != null && chunkPos[1] == sccslevelgen.wallheightsize - 1 && typeofterraintiles == 1103 && yi != height - 1 ||
                                                   listofchunksadjacent[5] != null && chunkPos[1] == sccslevelgen.wallheightsize - 1 && typeofterraintiles == 1104 && yi != height - 1 ||
                                                   listofchunksadjacent[5] != null && chunkPos[1] == sccslevelgen.wallheightsize - 1 && typeofterraintiles == 1105 && yi != height - 1 ||
                                                   listofchunksadjacent[5] != null && chunkPos[1] == sccslevelgen.wallheightsize - 1 && typeofterraintiles == 1106 && yi != height - 1 ||
                                                   listofchunksadjacent[5] != null && chunkPos[1] == sccslevelgen.wallheightsize - 1 && typeofterraintiles == 1107 && yi != height - 1 ||
                                                   listofchunksadjacent[5] != null && chunkPos[1] == sccslevelgen.wallheightsize - 1 && typeofterraintiles == 1108 && yi != height - 1 ||
                                                   listofchunksadjacent[5] != null && chunkPos[1] == sccslevelgen.wallheightsize - 1 && typeofterraintiles == 1109 && yi != height - 1 ||
                                                   listofchunksadjacent[5] != null && chunkPos[1] == sccslevelgen.wallheightsize - 1 && typeofterraintiles == 1110 && yi != height - 1 ||
                                                   listofchunksadjacent[5] != null && chunkPos[1] == sccslevelgen.wallheightsize - 1 && typeofterraintiles == 1111 && yi != height - 1 ||
                                                   listofchunksadjacent[5] != null && chunkPos[1] == sccslevelgen.wallheightsize - 1 && typeofterraintiles == 1112 && yi != height - 1)
                                                {
                                                    buildTopFace(xi, yi, zi, 1.0f);
                                                }


                                                if (typeofterraintiles == 1115 && yi != height - 1)
                                                {
                                                    buildTopFace(xi, yi, zi, 1.0f);
                                                }


                                                /*

                                                if (listofchunksadjacent[5] == null && chunkPos[1] == levellimitfloory)
                                                {
                                                    buildTopFace(xi, yi, zi, 1.0f);
                                                }

                                                if (listofchunksadjacent[5] != null && chunkPos[1] == levellimitfloory && typeofterraintiles == 1112)
                                                {

                                                }
                                             */


                                                /*
                                                if (listofchunksadjacent[5] != null && typeofterraintiles == 1101 ||
                                                    listofchunksadjacent[5] != null && typeofterraintiles == 1102 ||
                                                    listofchunksadjacent[5] != null && typeofterraintiles == 1103 ||
                                                    listofchunksadjacent[5] != null && typeofterraintiles == 1104 ||
                                                    listofchunksadjacent[5] != null && typeofterraintiles == 1105 ||
                                                    listofchunksadjacent[5] != null && typeofterraintiles == 1106 ||
                                                    listofchunksadjacent[5] != null && typeofterraintiles == 1107 ||
                                                    listofchunksadjacent[5] != null && typeofterraintiles == 1108 ||
                                                    listofchunksadjacent[5] != null && typeofterraintiles == 1109 ||
                                                    listofchunksadjacent[5] != null && typeofterraintiles == 1110 ||
                                                    listofchunksadjacent[5] != null && typeofterraintiles == 1111 ||
                                                    listofchunksadjacent[5] != null && typeofterraintiles == 1112 ||
                                                    listofchunksadjacent[5] != null && typeofterraintiles == 1115)
                                                {
                                                    buildTopFace(xi, yi, zi, 1.0f);
                                                }*/








                                                //var somechunkpos = chunkPos;
                                                //somechunkPos[1] += (height) * planeSize;
                                                //var someChunk = (sclevelgenvert)componentparent.getChunklod0(somechunkpos.X, 0, somechunkpos.Z);

                                                /*if (listofchunksadjacent[5] == null && levelofdetail == 1 && chunkPos[1] == levellimitfloory) //&& levelofdetail == 1
                                                {
                                                    buildTopFace(xi, yi, zi, 1.0f);
                                                }
                                                else if (listofchunksadjacent[5] == null && levelofdetail == 1 && chunkPos[1] == sclevelgen.wallheightsize)
                                                {
                                                    //dont build
                                                }
                                                else
                                                {
                                                    if (listofchunksadjacentfloor[5] != null && chunkPos[1] != sclevelgen.wallheightsize)
                                                    {
                                                        buildTopFace(xi, yi, zi, 1.0f);
                                                    }
                                                }*/
                                                /*
                                                if (listofchunksadjacentfloor[5] != null && chunkPos[1] != sclevelgen.wallheightsize - 1)
                                                {

                                                    buildTopFace(xi, yi, zi, 1.0f);
                                                }*/
                                                /*else if (listofchunksadjacentfloor[5] == null && chunkPos[1] == sclevelgen.wallheightsize - 1 && yi != height-1)
                                                {
                                                    buildTopFace(xi, yi, zi, 1.0f);
                                                }*/



                                                /*
                                                if (listofchunksadjacent[5] == null && chunkPos[1] == sclevelgen.wallheightsize - 1 && yi != height - 1)
                                                {
                                                    buildTopFace(xi, yi, zi, 1.0f);
                                                }

                                                if (listofchunksadjacent[5] != null && levelofdetail == 1 && chunkPos[1] == sclevelgen.wallheightsize - 1 && yi != height - 1)
                                                {
                                                    buildTopFace(xi, yi, zi, 1.0f);
                                                }
                                                */
                                                /*if (levelofdetail != 1)
                                                {
                                                    if (listofchunksadjacent[5] != null && chunkPos[1] == sclevelgen.wallheightsize-1)
                                                    {
                                                        buildTopFace(xi, yi, zi, 1.0f);
                                                    }

                                                }*/


                                                //buildTopFace(xi, yi, zi, 1.0f);
                                            }
                                        }
                                    }
                                }







                                /*
                                if (IsTransparent(xi, yi, zi + 1))
                                {
                                    if (zi != depth - 1 && zi != 0)
                                    {
                                        buildFrontFace(xi, yi, zi, 1.0f);
                                    }
                                    else if (zi == 0)
                                    {
                                        buildFrontFace(xi, yi, zi, 1.0f);
                                    }
                                    else if (zi == depth - 1)
                                    {
                                        if (listofchunksadjacent[3] != null) // && levelofdetail == 1
                                        {
                                            ////Console.WriteLine(chunkPos + " " + someChunk.chunkPos);

                                            if (listofchunksadjacent[3].map != null)
                                            {
                                                if (listofchunksadjacent[3].GetByte(xi, yi, 0) == 0) //someChunk.IsTransparent(width - 1, yi, zi)) //
                                                {
                                                    /*Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize, 1) + chunkoriginpos;
                                                    var offset1 = right * planeSize;
                                                    var offset2 = up * planeSize;
                                                    createFrontFace(start + forward * planeSize, offset1, offset2);
                                                    buildFrontFace(xi, yi, zi, 1.0f);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (listofchunksadjacentfloor[3] != null)
                                            {
                                                buildFrontFace(xi, yi, zi, 1.0f);
                                            }

                                            if (levelofdetail != 1)
                                            {
                                                if (listofchunksadjacent[3] != null)
                                                {
                                                    buildFrontFace(xi, yi, zi, 1.0f);
                                                }
                                            }
                                        }
                                    }
                                }

                                */


                                if (facetype == 3)
                                {


                                    if (IsTransparent(xi, yi, zi + 1))
                                    {
                                        if (zi != depth - 1 && zi != 0)
                                        {
                                            buildFrontFace(xi, yi, zi, 1.0f);
                                        }
                                        else if (zi == 0)
                                        {
                                            buildFrontFace(xi, yi, zi, 1.0f);
                                        }
                                        else if (zi == depth - 1)
                                        {
                                            //Vector3 somechunkpos = chunkPos;
                                            //somechunkpos.Z += 1.0f;
                                            //sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunklod0(somechunkpos.X, somechunkpos.Y, somechunkpos.Z);

                                            if (listofchunksadjacent[3] != null && levelofdetail == 1) // && levelofdetail == 1
                                            {
                                                ////Console.WriteLine(chunkPos + " " + someChunk.chunkPos);

                                                if (listofchunksadjacent[3].map != null)
                                                {
                                                    if (listofchunksadjacent[3].GetByte(xi, yi, 0) == 0) //someChunk.IsTransparent(width - 1, yi, zi)) //
                                                    {
                                                        /*Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize,1) + chunkoriginpos;
                                                        var offset1 = right * planeSize;
                                                        var offset2 = up * planeSize;
                                                        createFrontFace(start + forward * planeSize, offset1, offset2);*/

                                                        /*if (typeofterraintiles != 0)
                                                        {
                                                            Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize,1) + chunkoriginpos;
                                                            var offset1 = right * planeSize;
                                                            var offset2 = up * planeSize;
                                                            createFrontFace(start + forward * planeSize, offset1, offset2);
                                                        }
                                                        else
                                                        {
                                                            buildFrontFace(xi, yi, zi, 1.0f);
                                                        }*/
                                                        /*offset1 = left * planeSize;
                                                        offset2 = up * planeSize;
                                                        createFrontFace(start + right * planeSize, offset1, offset2);*/
                                                        buildFrontFace(xi, yi, zi, 1.0f);
                                                    }
                                                    else
                                                    {

                                                    }
                                                }
                                                else
                                                {
                                                    //buildFrontFace(xi, yi, zi, 1.0f);
                                                }
                                            }
                                            else
                                            {
                                                //buildFrontFace(xi, yi, zi, 1.0f);

                                                //var somechunkpos = chunkPos;
                                                //somechunkpos.Z += 1.0f;
                                                //var someChunk = (sclevelgenvert)componentparent.getChunklod0(somechunkpos.X, 0, somechunkpos.Z);

                                                if (listofchunksadjacentfloor[3] != null)
                                                {
                                                    buildFrontFace(xi, yi, zi, 1.0f);
                                                }

                                                if (levelofdetail != 1)
                                                {
                                                    if (listofchunksadjacent[3] != null)
                                                    {
                                                        buildFrontFace(xi, yi, zi, 1.0f);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }




                                if (facetype == 4)
                                {


                                    if (IsTransparent(xi, yi, zi - 1))
                                    {
                                        if (zi != depth - 1 && zi != 0)
                                        {
                                            buildBackFace(xi, yi, zi, 1.0f);
                                        }
                                        else if (zi == depth - 1)
                                        {
                                            buildBackFace(xi, yi, zi, 1.0f);
                                        }
                                        else if (zi == 0)
                                        {

                                            //Vector3 somechunkpos = chunkPos;
                                            //somechunkpos.Z -= 1.0f;
                                            //sclevelgenvert someChunk = (sclevelgenvert)componentparent.getChunklod0(somechunkpos.X, somechunkpos.Y, somechunkpos.Z);

                                            if (listofchunksadjacent[2] != null && levelofdetail == 1) //
                                            {
                                                ////Console.WriteLine(chunkPos + " " + someChunk.chunkPos);

                                                if (listofchunksadjacent[2].map != null)
                                                {

                                                    ////Console.WriteLine("x:" + xi + "/y:" + yi + "/z:" + zi + "depth:" + depth);
                                                    if (listofchunksadjacent[2].GetByte(xi, yi, depth - 1) == 0) //someChunk.IsTransparent(width - 1, yi, zi)) //
                                                    {

                                                        /*Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize,1) + chunkoriginpos;

                                                        var offset1 = left * planeSize;
                                                        var offset2 = up * planeSize;
                                                        createBackFace(start + right * planeSize, offset1, offset2);*/
                                                        /*if (typeofterraintiles != 0)
                                                        {
                                                            Vector4 start = new Vector4(xi * planeSize, yi * planeSize, zi * planeSize,1) + chunkoriginpos;

                                                            var offset1 = left * planeSize;
                                                            var offset2 = up * planeSize;
                                                            createBackFace(start + right * planeSize, offset1, offset2);
                                                        }
                                                        else
                                                        {
                                                            buildBackFace(xi, yi, zi, 1.0f);
                                                        }*/
                                                        buildBackFace(xi, yi, zi, 1.0f);
                                                    }
                                                    else
                                                    {

                                                    }
                                                }
                                                else
                                                {
                                                    //buildBackFace(xi, yi, zi, 1.0f);
                                                }
                                            }
                                            else
                                            {
                                                //var somechunkpos = chunkPos;
                                                //somechunkpos.Z -= 1.0f;
                                                //var someChunk = (sclevelgenvert)componentparent.getChunklod0(somechunkpos.X, 0, somechunkpos.Z);

                                                if (listofchunksadjacentfloor[2] != null)
                                                {
                                                    buildBackFace(xi, yi, zi, 1.0f);
                                                }


                                                if (levelofdetail != 1)
                                                {
                                                    if (listofchunksadjacent[2] != null)
                                                    {
                                                        buildBackFace(xi, yi, zi, 1.0f);
                                                    }

                                                }
                                                //buildBackFace(xi, yi, zi, 1.0f);
                                            }
                                        }
                                        else
                                        {
                                            ////Console.WriteLine("test");
                                        }
                                    }
                                }

                            }





                        }





                        //CalculateintsForFaces();
                        /*if (swtchz == 0)
                        {
                            CreateChunkFaces();
                            //CalculateintsForFaces();


                        }
                        else
                        {
                            break;
                        }*/
                    }
                }
            }

            //Console.WriteLine("vertexcountermemory:" + vertexcountermemory);
        }





        int firstvertx = 0;
        int firstverty = 0;
        int firstvertz = 0;


        int secondvertx = 0;
        int secondverty = 0;
        int secondvertz = 0;



        int thirdvertx = 0;
        int thirdverty = 0;
        int thirdvertz = 0;



        int fourthvertx = 0;
        int fourthverty = 0;
        int fourthvertz = 0;



        int someixtop = 0;
        int someiytop = 0;
        int someiztop = 0;
        int someindextop = 0;


        int someixleft = 0;
        int someiyleft = 0;
        int someizleft = 0;
        int someindexleft = 0;


        int someixright = 0;
        int someiyright = 0;
        int someizright = 0;
        int someindexright = 0;


        int someixfront = 0;
        int someiyfront = 0;
        int someizfront = 0;
        int someindexfront = 0;


        int someixback = 0;
        int someiyback = 0;
        int someizback = 0;
        int someindexback = 0;


        int someixbottom = 0;
        int someiybottom = 0;
        int someizbottom = 0;
        int someindexbottom = 0;






        //UnityEngine.Debug.Log("_xx: " + _xx + " _zz: " + _zz + " _maxWidth: " + _maxWidth + " _maxDepth: " + _maxDepth + " rowIterateX: " + rowIterateX + " rowIterateZ: " + rowIterateZ);
        void buildTopFace(int xi, int yi, int zi, float block) //int _x, int _y, int _z, Vector3 chunkPos
        {
            _maxWidth = width;
            _maxDepth = depth;
            _maxHeight = height;
            foundVertOne = false;
            foundVertTwo = false;
            foundVertThree = false;
            foundVertFour = false;
            //TOPFACE

            block = _tempChunkArray[xi + width * (yi + height * zi)];

            if (block == 1) //|| block == 2
            {

                //if (IsTransparent(xi, yi + 1, zi))
                {
                    for (int _xx = 0; _xx < _maxWidth; _xx++)
                    {
                        rowIterateX = xi + _xx;
                        for (int _zz = 0; _zz < _maxDepth; _zz++)
                        {
                            rowIterateZ = zi + _zz;

                            if (rowIterateX < width && rowIterateZ < depth)
                            {

                                //if (someswtc == 1)
                                {
                                    if (_xx == 0 && _zz == 0)
                                    {
                                        oneVertIndexX = rowIterateX;
                                        oneVertIndexY = yi + 1;
                                        oneVertIndexZ = rowIterateZ;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);
                                        foundVertOne = true;




                                        //each time a first corner vertex is found, tag the byte as a face byte;
                                        firstvertx = rowIterateX;
                                        firstverty = yi;
                                        firstvertz = rowIterateZ;





                                        if (blockExistsInArray(rowIterateX + 1, yi, rowIterateZ))
                                        {
                                            block = _tempChunkArray[(rowIterateX + 1) + width * ((yi) + height * (rowIterateZ))];

                                            thirdvertx = rowIterateX + 1;
                                            thirdverty = yi;
                                            thirdvertz = rowIterateZ;



                                            if (block == 0)
                                            {
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = yi + 1;
                                                threeVertIndexZ = rowIterateZ;
                                                _maxWidth = _xx;
                                                foundVertThree = true;



                                                ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y+1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);

                                            }
                                            else if (block == 1 || block == 2)
                                            {
                                                if (blockExistsInArray(rowIterateX + 1, yi + 1, rowIterateZ))
                                                {
                                                    block = _tempChunkArray[(rowIterateX + 1) + width * ((yi + 1) + height * (rowIterateZ))];

                                                    thirdvertx = rowIterateX + 1;
                                                    thirdverty = yi + 1;
                                                    thirdvertz = rowIterateZ;

                                                    //thirdvertx = rowIterateX + 1;
                                                    //thirdverty = yi;
                                                    //thirdvertz = rowIterateZ;

                                                    if (block == 1 || block == 2)
                                                    {
                                                        threeVertIndexX = rowIterateX + 1;
                                                        threeVertIndexY = yi + 1;
                                                        threeVertIndexZ = rowIterateZ;
                                                        _maxWidth = _xx;
                                                        foundVertThree = true;


                                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            threeVertIndexX = rowIterateX + 1;
                                            threeVertIndexY = yi + 1;
                                            threeVertIndexZ = rowIterateZ;

                                            _maxWidth = _xx;
                                            foundVertThree = true;


                                            thirdvertx = rowIterateX + 1;
                                            thirdverty = yi + 1;
                                            thirdvertz = rowIterateZ;


                                            /*thirdvertx = rowIterateX + 1;
                                            thirdverty = yi;
                                            thirdvertz = rowIterateZ;*/
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi + 1;
                                                fourVertIndexZ = twoVertIndexZ;


                                                fourthvertx = rowIterateX;
                                                fourthverty = yi;
                                                fourthvertz = rowIterateZ;

                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }

                                        if (blockExistsInArray(rowIterateX, yi, rowIterateZ + 1))
                                        {
                                            block = _tempChunkArray[(rowIterateX) + width * ((yi) + height * (rowIterateZ + 1))];


                                            secondvertx = rowIterateX;
                                            secondverty = yi;
                                            secondvertz = rowIterateZ + 1;


                                            if (block == 0)
                                            {
                                                twoVertIndexX = rowIterateX;
                                                twoVertIndexY = yi + 1;
                                                twoVertIndexZ = rowIterateZ + 1;
                                                _maxDepth = _zz + 1;
                                                foundVertTwo = true;
                                                ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = yi + 1;
                                                    fourVertIndexZ = twoVertIndexZ;

                                                    fourthvertx = rowIterateX;
                                                    fourthverty = yi;
                                                    fourthvertz = rowIterateZ;

                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                }
                                            }
                                            else if (block == 1 || block == 2) //block == 1||
                                            {
                                                if (block == 1)
                                                {
                                                    if (blockExistsInArray(rowIterateX, yi + 1, rowIterateZ + 1))
                                                    {
                                                        block = _tempChunkArray[(rowIterateX) + width * ((yi + 1) + height * (rowIterateZ + 1))];


                                                        if (block == 1 || block == 2)
                                                        {
                                                            twoVertIndexX = rowIterateX;
                                                            twoVertIndexY = yi + 1;
                                                            twoVertIndexZ = rowIterateZ + 1;
                                                            _maxDepth = _zz + 1;
                                                            foundVertTwo = true;

                                                            secondvertx = rowIterateX;
                                                            secondverty = yi + 1;
                                                            secondvertz = rowIterateZ + 1;

                                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                            {
                                                                fourVertIndexX = threeVertIndexX;
                                                                fourVertIndexY = yi + 1;
                                                                fourVertIndexZ = twoVertIndexZ;

                                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (block == 2)
                                                {
                                                    twoVertIndexX = rowIterateX;
                                                    twoVertIndexY = yi + 1;
                                                    twoVertIndexZ = rowIterateZ + 1;
                                                    _maxDepth = _zz + 1;
                                                    foundVertTwo = true;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = yi + 1;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            twoVertIndexX = rowIterateX;
                                            twoVertIndexY = yi + 1;
                                            twoVertIndexZ = rowIterateZ + 1;
                                            _maxDepth = _zz + 1;
                                            foundVertTwo = true;
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi + 1;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }

                                    else if (_xx == 0 && _zz > 0)
                                    {
                                        if (blockExistsInArray(rowIterateX, yi, rowIterateZ + 1))
                                        {
                                            block = _tempChunkArray[(rowIterateX) + width * ((yi) + height * (rowIterateZ + 1))];

                                            if (block == 0)
                                            {
                                                twoVertIndexX = rowIterateX;
                                                twoVertIndexY = yi + 1;
                                                twoVertIndexZ = rowIterateZ + 1;
                                                _maxDepth = _zz + 1;
                                                foundVertTwo = true;
                                                ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = yi + 1;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                }


                                            }
                                            else if (block == 1 || block == 2) //block == 1||
                                            {
                                                if (block == 1)
                                                {
                                                    if (blockExistsInArray(rowIterateX, yi + 1, rowIterateZ + 1))
                                                    {
                                                        block = _tempChunkArray[(rowIterateX) + width * ((yi + 1) + height * (rowIterateZ + 1))];
                                                        if (block == 1 || block == 2)
                                                        {
                                                            twoVertIndexX = rowIterateX;
                                                            twoVertIndexY = yi + 1;
                                                            twoVertIndexZ = rowIterateZ + 1;
                                                            _maxDepth = _zz + 1;
                                                            foundVertTwo = true;
                                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                            {
                                                                fourVertIndexX = threeVertIndexX;
                                                                fourVertIndexY = yi + 1;
                                                                fourVertIndexZ = twoVertIndexZ;
                                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                            }
                                                        }
                                                    }
                                                    else //continue??
                                                    {

                                                    }
                                                }
                                                else if (block == 2)
                                                {
                                                    twoVertIndexX = rowIterateX;
                                                    twoVertIndexY = yi + 1;
                                                    twoVertIndexZ = rowIterateZ + 1;
                                                    _maxDepth = _zz + 1;
                                                    foundVertTwo = true;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = yi + 1;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            twoVertIndexX = rowIterateX;
                                            twoVertIndexY = yi + 1;
                                            twoVertIndexZ = rowIterateZ + 1;
                                            _maxDepth = _zz + 1;
                                            foundVertTwo = true;

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi + 1;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);
                                        }

                                        if (blockExistsInArray(rowIterateX + 1, yi, rowIterateZ))
                                        {
                                            block = _tempChunkArray[(rowIterateX + 1) + width * ((yi) + height * (rowIterateZ))];

                                            if (block == 0)
                                            {
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = yi + 1;
                                                threeVertIndexZ = rowIterateZ - _zz;
                                                _maxWidth = _xx;
                                                foundVertThree = true;


                                                thirdvertx = rowIterateX + 1;
                                                thirdverty = yi + 1;
                                                thirdvertz = rowIterateZ - _zz;


                                                ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = yi + 1;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                }
                                            }
                                            else if (block == 1 || block == 2)
                                            {
                                                //********************************************************
                                                if (blockExistsInArray(rowIterateX + 1, yi + 1, rowIterateZ))
                                                {
                                                    block = _tempChunkArray[(rowIterateX + 1) + width * ((yi + 1) + height * (rowIterateZ))];
                                                    if (block == 1 || block == 2)
                                                    {
                                                        threeVertIndexX = rowIterateX + 1;
                                                        threeVertIndexY = yi + 1;
                                                        threeVertIndexZ = rowIterateZ - _zz;
                                                        _maxWidth = _xx;
                                                        foundVertThree = true;
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                        {
                                                            fourVertIndexX = threeVertIndexX;
                                                            fourVertIndexY = yi + 1;
                                                            fourVertIndexZ = twoVertIndexZ;
                                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                        }
                                                    }
                                                }
                                                //************************************************************
                                            }
                                        }
                                        else
                                        {
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi + 1;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                    else if (_xx > 0 && _zz == 0)
                                    {
                                        if (blockExistsInArray(rowIterateX + 1, yi, rowIterateZ))
                                        {
                                            block = _tempChunkArray[(rowIterateX + 1) + width * ((yi) + height * (rowIterateZ))];

                                            if (block == 0)
                                            {
                                                //UnityEngine.Debug.Log("test");
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = yi + 1;
                                                threeVertIndexZ = rowIterateZ - _zz;
                                                _maxWidth = _xx;
                                                foundVertThree = true;
                                                ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                                if (foundVertTwo)
                                                {
                                                    if (foundVertThree)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = yi + 1;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                            else if (block == 1 || block == 2)
                                            {
                                                if (blockExistsInArray(rowIterateX + 1, yi + 1, rowIterateZ))
                                                {
                                                    block = _tempChunkArray[(rowIterateX + 1) + width * ((yi + 1) + height * (rowIterateZ))];
                                                    if (block == 1 || block == 2)
                                                    {
                                                        threeVertIndexX = rowIterateX + 1;
                                                        threeVertIndexY = yi + 1;
                                                        threeVertIndexZ = rowIterateZ - _zz;
                                                        _maxWidth = _xx;
                                                        foundVertThree = true;
                                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = yi + 1;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            threeVertIndexX = rowIterateX + 1;
                                            threeVertIndexY = yi + 1;
                                            threeVertIndexZ = rowIterateZ - _zz;
                                            _maxWidth = _xx;
                                            foundVertThree = true;
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi + 1;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }

                                        if (blockExistsInArray(rowIterateX, yi, rowIterateZ + 1))
                                        {
                                            block = _tempChunkArray[(rowIterateX) + width * ((yi) + height * (rowIterateZ + 1))];

                                            if (block == 1 || block == 2)
                                            {
                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = yi + 1;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                }
                                            }

                                            if (blockExistsInArray(rowIterateX, yi + 1, rowIterateZ + 1))
                                            {
                                                //*****************************************************************************
                                                block = _tempChunkArray[(rowIterateX) + width * ((yi + 1) + height * (rowIterateZ + 1))];
                                                if (block == 1 || block == 2)
                                                {
                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = yi + 1;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                    }
                                                }
                                                //*****************************************************************************
                                            }
                                        }
                                        else
                                        {
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi + 1;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }

                                    else if (_xx > 0 && _zz > 0)
                                    {
                                        if (blockExistsInArray(rowIterateX + 1, yi, rowIterateZ))
                                        {
                                            block = _tempChunkArray[(rowIterateX + 1) + width * ((yi) + height * (rowIterateZ))];

                                            if (block == 0)
                                            {
                                                //UnityEngine.Debug.Log("test");
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = yi + 1;
                                                threeVertIndexZ = rowIterateZ - _zz;
                                                _maxWidth = _xx;
                                                foundVertThree = true;
                                                ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi + 1;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                            else if (block == 1 || block == 2)
                                            {
                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = yi + 1;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColorOrange, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                }

                                                //***********************************************************
                                                if (blockExistsInArray(rowIterateX + 1, yi + 1, rowIterateZ))
                                                {
                                                    block = _tempChunkArray[(rowIterateX + 1) + width * ((yi + 1) + height * (rowIterateZ))];
                                                    if (block == 1 || block == 2)
                                                    {
                                                        threeVertIndexX = rowIterateX + 1;
                                                        threeVertIndexY = yi + 1;
                                                        threeVertIndexZ = rowIterateZ - _zz;
                                                        _maxWidth = _xx;

                                                        foundVertThree = true;
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                        {
                                                            fourVertIndexX = threeVertIndexX;
                                                            fourVertIndexY = yi + 1;
                                                            fourVertIndexZ = twoVertIndexZ;
                                                            ////////Instantiate(_sphereVisualOtherColorOrange, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                        }
                                                    }
                                                }
                                                //*******************************************************
                                            }
                                        }
                                        else
                                        {
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi + 1;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }

                                        if (!blockExistsInArray(rowIterateX, yi, rowIterateZ + 1))
                                        {
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi + 1;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                            }

                            if (blockExistsInArray(rowIterateX, yi, rowIterateZ))
                            {
                                _tempChunkArray[(rowIterateX) + width * (yi + height * (rowIterateZ))] = 2;
                                ////////Instantiate(blockZero, new Vector3(rowIterateX + 0.5f, y, rowIterateZ + 0.5f) * planeSize + chunkPos, Quaternion.identity);
                            }
                        }
                    }






                    if (getChunklod0Vertexint0(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                    {
                        _chunkVertexArray0[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = 1;
                    }

                    if (getChunklod0Vertexint0(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                    {
                        _chunkVertexArray0[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = 1;
                    }

                    if (getChunklod0Vertexint0(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                    {
                        _chunkVertexArray0[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = 1;
                    }

                    if (getChunklod0Vertexint0(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                    {
                        _chunkVertexArray0[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = 1;
                    }

                    if (getChunklod0Vertexint0(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 1 && getChunklod0Vertexint0(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 1 && getChunklod0Vertexint0(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 1 && getChunklod0Vertexint0(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 1)//
                    {




                      






                        //var indexofmapvertindexfordimsone = firstvertx + width * ((firstverty) + height * (firstvertz));
                        //var indexofmapvertindexfordimsone = oneVertIndexX + width * ((oneVertIndexY) + height * (oneVertIndexZ));
                        someindextop = someixtop + 4 * ((someiytop) + 4 * (someiztop));

                        ////Console.WriteLine("index:" + someindex);

                        mapvertindexfordims[someindextop] = 1;

                        vertexlisttop.Add(new tutorialcubeaschunkinst.DVertex()
                        {
                            position = new Vector4(oneVertIndexX * planeSize * levelofdetail * levelofdetailmul, oneVertIndexY * planeSize * levelofdetail * levelofdetailmul, oneVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos,
                            indexPos = new Vector4(xi, yi, zi, 1),
                            color = topfacecolor,
                            normal = new Vector3(0, 1, 0),
                            padding0 = padding0,
                            tex = new Vector2(1, 1),
                            padding1 = padding1,
                            padding2 = padding2,
                        });
                        _testVertexArray0[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;

                        //var indexofmapvertindexfordimstwo = (twoVertIndexX) + width * ((twoVertIndexY - 1) + height * (twoVertIndexZ - 1));
                        //mapvertindexfordims[indexofmapvertindexfordimstwo] = 2;
                        vertexlisttop.Add(new tutorialcubeaschunkinst.DVertex()
                        {
                            position = new Vector4(twoVertIndexX * planeSize * levelofdetail * levelofdetailmul, twoVertIndexY * planeSize * levelofdetail * levelofdetailmul, twoVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos,
                            indexPos = new Vector4(xi, yi, zi, 2),
                            color = topfacecolor,
                            normal = new Vector3(0, 1, 0),
                            padding0 = padding0,
                            tex = new Vector2(1, 1),
                            padding1 = padding1,
                            padding2 = padding2,
                        });

                        _testVertexArray0[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;

                        //var indexofmapvertindexfordimsthree = (threeVertIndexX - 1) + width * ((threeVertIndexY - 1) + height * (threeVertIndexZ));
                        //mapvertindexfordims[indexofmapvertindexfordimsthree] = 3;
                        vertexlisttop.Add(new tutorialcubeaschunkinst.DVertex()
                        {
                            position = new Vector4(threeVertIndexX * planeSize * levelofdetail * levelofdetailmul, threeVertIndexY * planeSize * levelofdetail * levelofdetailmul, threeVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos,
                            indexPos = new Vector4(xi, yi, zi, 3),
                            color = topfacecolor,
                            normal = new Vector3(0, 1, 0),
                            padding0 = padding0,
                            tex = new Vector2(1, 1),
                            padding1 = padding1,
                            padding2 = padding2,
                        });
                        _testVertexArray0[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;

                        //var indexofmapvertindexfordimsfour = (fourVertIndexX - 1) + width * ((fourVertIndexY - 1) + height * (fourVertIndexZ - 1));
                        //mapvertindexfordims[indexofmapvertindexfordimsfour] = 4;
                        vertexlisttop.Add(new tutorialcubeaschunkinst.DVertex()
                        {
                            position = new Vector4(fourVertIndexX * planeSize * levelofdetail * levelofdetailmul, fourVertIndexY * planeSize * levelofdetail * levelofdetailmul, fourVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos,
                            indexPos = new Vector4(xi, yi, zi, 4),
                            color = topfacecolor,
                            normal = new Vector3(0, 1, 0),
                            padding0 = padding0,
                            tex = new Vector2(1, 1),
                            padding1 = padding1,
                            padding2 = padding2,
                        });
                        _testVertexArray0[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;

                        _index0 = _testVertexArray0[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)];
                        _index1 = _testVertexArray0[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)];
                        _index2 = _testVertexArray0[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)];
                        _index3 = _testVertexArray0[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)];

                        var vert0 = new Vector4(oneVertIndexX, oneVertIndexY, oneVertIndexZ, 1);
                        var vert1 = new Vector4(twoVertIndexX, twoVertIndexY, twoVertIndexZ, 1);
                        var vert2 = new Vector4(threeVertIndexX, threeVertIndexY, threeVertIndexZ, 1);
                        var vert3 = new Vector4(fourVertIndexX, fourVertIndexY, fourVertIndexZ, 1);

                       //Console.WriteLine("/x:" + vert2.X + "/y:" + vert2.Y  + "/z:" + vert2.Z);


                        //Vector3 firstvert = new Vector4(oneVertIndexX, oneVertIndexY, oneVertIndexZ, 1));
                        mapfirstvertxtop[someindextop] = oneVertIndexX;
                        mapfirstvertytop[someindextop] = oneVertIndexY;
                        mapfirstvertztop[someindextop] = oneVertIndexZ;

                        //widthdimtop[someindextop] = (int)Math.Abs(vert0.X - vert2.X); //0  3 == 3 
                        //heightdimtop[someindextop] = (int)Math.Abs(vert0.Y);
                        //depthdimtop[someindextop] = (int)Math.Abs(vert0.Z - vert1.Z);

                        widthdimtop[someindextop] = (int)Math.Abs(Math.Abs(vert0.X) - Math.Abs(vert2.X)); //0  3 == 3 
                        heightdimtop[someindextop] = (int)Math.Abs(Math.Abs(vert0.Y) - Math.Abs(vert2.Y));
                        depthdimtop[someindextop] = (int)Math.Abs(Math.Abs(vert0.Z) - Math.Abs(vert1.Z));


                        //MAKING THE BYTES UNAVAILABLE TO BE BUILT AS FACES FOR THE NEXT ITERATIONS.
                        for (int x = 0; x < widthdimtop[someindextop]; x++)
                        {
                            for (int z = 0; z < depthdimtop[someindextop]; z++)
                            {
                                int theindex = (oneVertIndexX + x) + 8 * ((oneVertIndexY - 1) + 8 * (oneVertIndexZ + z));
                                _tempChunkArray[theindex] = 3;
                            }
                        }




                        //MAKING THE BYTES UNAVAILABLE TO BE BUILT AS FACES FOR THE NEXT ITERATIONS.

                        /*
                        listOfTriangleIndicestop.Add(_index0);
                        listOfTriangleIndicestop.Add(_index1);
                        listOfTriangleIndicestop.Add(_index2);
                        listOfTriangleIndicestop.Add(_index3);
                        listOfTriangleIndicestop.Add(_index2);
                        listOfTriangleIndicestop.Add(_index1);*/

                        someiztop++;
                        if (someiztop == 4)
                        {
                            someiytop++;
                            someiztop = 0;
                        }
                        if (someiytop == 4)
                        {
                            someixtop++;
                            someiytop = 0;
                        }
                        if (someixtop == 4)
                        {
                            someixtop = 0;
                        }


                    }
                }
            }

            /*//_mesh = new Mesh();
            _mesh.vertices = vertexlist.ToArray();
            _mesh.listOfTriangleIndices = listOfTriangleIndices.ToArray();

            _testChunk.GetComponent<MeshFilter>().mesh = _mesh;

            _meshRend = _testChunk.GetComponent<MeshRenderer>();
            _meshRend.material = _mat;*/

        }

        /*
        private void createleftFace(Vector4 start, Vector4 offset1, Vector4 offset2)
        {
            //offset1 = back * planeSize;
            //offset2 = down * planeSize;

            //positions[0 + vertzIndex] = start; //(x,y+1,z+1)
            //positions[1 + vertzIndex] = start + offset1;//(x,y+1,z)
            //positions[2 + vertzIndex] = start + offset2; //(x,y,z+1)
            //positions[3 + vertzIndex] = start + offset1 + offset2;//(x,y,z)



            int index0 = vertexlist.Count;// _newVertzCounter;
            //vertexlist.Add(start);
            //vertexlist.Add(leftfacecolor);
            vertexlist.Add(new tutorialcubeaschunkinst.DVertex()
            {
                position = start,
                color = leftfacecolor,
                //normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            int index1 = vertexlist.Count;//_newVertzCounter;
            //vertexlist.Add(start + offset1);
            //vertexlist.Add(leftfacecolor);
            vertexlist.Add(new tutorialcubeaschunkinst.DVertex()
            {
                position = start + offset1,
                color = leftfacecolor,
                //normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            int index2 = vertexlist.Count;//_newVertzCounter;
            //vertexlist.Add(start + offset2);
            //vertexlist.Add(leftfacecolor);
            vertexlist.Add(new tutorialcubeaschunkinst.DVertex()
            {
                position = start + offset2,
                color = leftfacecolor,
                //normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            int index3 = vertexlist.Count;// _newVertzCounter;
            //vertexlist.Add(start + offset1 + offset2);
            //vertexlist.Add(leftfacecolor);
            vertexlist.Add(new tutorialcubeaschunkinst.DVertex()
            {
                position = start + offset1 + offset2,
                color = leftfacecolor,
                //normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            listOfTriangleIndices.Add(index0);
            listOfTriangleIndices.Add(index1);
            listOfTriangleIndices.Add(index2);
            listOfTriangleIndices.Add(index3);
            listOfTriangleIndices.Add(index2);
            listOfTriangleIndices.Add(index1);
        }



        private void createRightFace(Vector4 start, Vector4 offset1, Vector4 offset2)
        {


            int index0 = vertexlist.Count;// _newVertzCounter;
            //vertexlist.Add(start);
            //vertexlist.Add(leftfacecolor);
            vertexlist.Add(new tutorialcubeaschunkinst.DVertex()
            {
                position = start,
                color = rightfacecolor,
                //normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            int index1 = vertexlist.Count;//_newVertzCounter;
            //vertexlist.Add(start + offset1);
            //vertexlist.Add(leftfacecolor);
            vertexlist.Add(new tutorialcubeaschunkinst.DVertex()
            {
                position = start + offset1,
                color = rightfacecolor,
                //normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            int index2 = vertexlist.Count;//_newVertzCounter;
            //vertexlist.Add(start + offset2);
            //vertexlist.Add(leftfacecolor);
            vertexlist.Add(new tutorialcubeaschunkinst.DVertex()
            {
                position = start + offset2,
                color = rightfacecolor,
                //normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            int index3 = vertexlist.Count;// _newVertzCounter;
            //vertexlist.Add(start + offset1 + offset2);
            //vertexlist.Add(leftfacecolor);
            vertexlist.Add(new tutorialcubeaschunkinst.DVertex()
            {
                position = start + offset1 + offset2,
                color = rightfacecolor,
                //normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            listOfTriangleIndices.Add(index0);
            listOfTriangleIndices.Add(index1);
            listOfTriangleIndices.Add(index2);
            listOfTriangleIndices.Add(index3);
            listOfTriangleIndices.Add(index2);
            listOfTriangleIndices.Add(index1);
        }
        */


        /*
        private void createTopFace(Vector4 start, Vector4 offset1, Vector4 offset2, int xi, int yi, int zi)
        {

            someindex = someix + width * ((someiy) + height * (someiz));

            mapvertindexfordims[someindex] = 1;

            

            int index0 = vertexlisttop.Count;
            vertexlisttop.Add(new tutorialcubeaschunkinst.DVertex()
            {
                position = start* planeSize, //new Vector4(oneVertIndexX * planeSize * levelofdetail * levelofdetailmul, oneVertIndexY * planeSize * levelofdetail * levelofdetailmul, oneVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos,
                indexPos = new Vector4(xi, yi, zi, 1),
                color = topfacecolor,
                normal = new Vector3(0, 1, 0),
                padding0 = padding0,
                tex = new Vector2(1, 1),
                padding1 = padding1,
                padding2 = padding2,
            });

            int index1 = vertexlisttop.Count;
            //var indexofmapvertindexfordimstwo = (twoVertIndexX) + width * ((twoVertIndexY - 1) + height * (twoVertIndexZ - 1));
            //mapvertindexfordims[indexofmapvertindexfordimstwo] = 2;
            vertexlisttop.Add(new tutorialcubeaschunkinst.DVertex()
            {
                position = (start * planeSize) + (offset1 * planeSize), // new Vector4(twoVertIndexX * planeSize * levelofdetail * levelofdetailmul, twoVertIndexY * planeSize * levelofdetail * levelofdetailmul, twoVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos,
                indexPos = new Vector4(xi, yi, zi, 2),
                color = topfacecolor,
                normal = new Vector3(0, 1, 0),
                padding0 = padding0,
                tex = new Vector2(1, 1),
                padding1 = padding1,
                padding2 = padding2,
            });

            int index2 = vertexlisttop.Count;
            //var indexofmapvertindexfordimsthree = (threeVertIndexX - 1) + width * ((threeVertIndexY - 1) + height * (threeVertIndexZ));
            //mapvertindexfordims[indexofmapvertindexfordimsthree] = 3;
            vertexlisttop.Add(new tutorialcubeaschunkinst.DVertex()
            {
                position = (start * planeSize) + (offset2 * planeSize), //new Vector4(threeVertIndexX * planeSize * levelofdetail * levelofdetailmul, threeVertIndexY * planeSize * levelofdetail * levelofdetailmul, threeVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos,
                indexPos = new Vector4(xi, yi, zi, 3),
                color = topfacecolor,
                normal = new Vector3(0, 1, 0),
                padding0 = padding0,
                tex = new Vector2(1, 1),
                padding1 = padding1,
                padding2 = padding2,
            });

            int index3 = vertexlisttop.Count;
            //var indexofmapvertindexfordimsfour = (fourVertIndexX - 1) + width * ((fourVertIndexY - 1) + height * (fourVertIndexZ - 1));
            //mapvertindexfordims[indexofmapvertindexfordimsfour] = 4;
            vertexlisttop.Add(new tutorialcubeaschunkinst.DVertex()
            {
                position = (start * planeSize) + (offset1 * planeSize) + (offset2 * planeSize), // new Vector4(fourVertIndexX * planeSize * levelofdetail * levelofdetailmul, fourVertIndexY * planeSize * levelofdetail * levelofdetailmul, fourVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos,
                indexPos = new Vector4(xi, yi, zi, 4),
                color = topfacecolor,
                normal = new Vector3(0, 1, 0),
                padding0 = padding0,
                tex = new Vector2(1, 1),
                padding1 = padding1,
                padding2 = padding2,
            });

            var vert0 = start;// new Vector4(start.X, start.Y, start.Z, 1);
            var vert1 = start + offset1;// new Vector4(twoVertIndexX, twoVertIndexY, twoVertIndexZ, 1);
            var vert2 = start + offset2;// new Vector4(threeVertIndexX, threeVertIndexY, threeVertIndexZ, 1);
            var vert3 = start + offset1 + offset2;// new Vector4(fourVertIndexX, fourVertIndexY, fourVertIndexZ, 1);

            //Vector3 firstvert = new Vector4(oneVertIndexX, oneVertIndexY, oneVertIndexZ, 1));
            mapfirstvertx[someindex] = xi;
            mapfirstverty[someindex] = yi;// oneVertIndexY;
            mapfirstvertz[someindex] = zi;// oneVertIndexZ;

            widthdim[someindex] = (int)Math.Abs(vert0.X - vert2.X); //0  3 == 3 
            heightdim[someindex] = (int)Math.Abs(vert0.Y);
            depthdim[someindex] = (int)Math.Abs(vert0.Z - vert1.Z);


            listOfTriangleIndicestop.Add(index0);
            listOfTriangleIndicestop.Add(index1);
            listOfTriangleIndicestop.Add(index2);
            listOfTriangleIndicestop.Add(index3);
            listOfTriangleIndicestop.Add(index2);
            listOfTriangleIndicestop.Add(index1);

            someiz++;
            if (someiz == depth)
            {
                someiy++;
                someiz = 0;
            }
            if (someiy == height)
            {
                someix++;
                someiy = 0;
            }
            if (someix == width)
            {
                someix = 0;
            }
        }
        */

        /*
        private void createBottomFace(Vector4 start, Vector4 offset1, Vector4 offset2)
        {


            int index0 = vertexlist.Count;// _newVertzCounter;
            //vertexlist.Add(start);
            //vertexlist.Add(leftfacecolor);
            vertexlist.Add(new tutorialcubeaschunkinst.DVertex()
            {
                position = start,
                color = bottomfacecolor,
                //normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            int index1 = vertexlist.Count;//_newVertzCounter;
            //vertexlist.Add(start + offset1);
            //vertexlist.Add(leftfacecolor);
            vertexlist.Add(new tutorialcubeaschunkinst.DVertex()
            {
                position = start + offset1,
                color = bottomfacecolor,
                //normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            int index2 = vertexlist.Count;//_newVertzCounter;
            //vertexlist.Add(start + offset2);
            //vertexlist.Add(leftfacecolor);
            vertexlist.Add(new tutorialcubeaschunkinst.DVertex()
            {
                position = start + offset2,
                color = bottomfacecolor,
                //normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            int index3 = vertexlist.Count;// _newVertzCounter;
            //vertexlist.Add(start + offset1 + offset2);
            //vertexlist.Add(leftfacecolor);
            vertexlist.Add(new tutorialcubeaschunkinst.DVertex()
            {
                position = start + offset1 + offset2,
                color = bottomfacecolor,
                //normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0, 0),

            });
            _newVertzCounter++;


            listOfTriangleIndices.Add(index2);
            listOfTriangleIndices.Add(index1);
            listOfTriangleIndices.Add(index0);
            listOfTriangleIndices.Add(index1);
            listOfTriangleIndices.Add(index2);
            listOfTriangleIndices.Add(index3);
        }

        private void createFrontFace(Vector4 start, Vector4 offset1, Vector4 offset2)
        {


            int index0 = vertexlist.Count;// _newVertzCounter;
            //vertexlist.Add(start);
            //vertexlist.Add(leftfacecolor);
            vertexlist.Add(new tutorialcubeaschunkinst.DVertex()
            {
                position = start,
                color = frontfacecolor,
                //normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            int index1 = vertexlist.Count;//_newVertzCounter;
            //vertexlist.Add(start + offset1);
            //vertexlist.Add(leftfacecolor);
            vertexlist.Add(new tutorialcubeaschunkinst.DVertex()
            {
                position = start + offset1,
                color = frontfacecolor,
                //normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            int index2 = vertexlist.Count;//_newVertzCounter;
            //vertexlist.Add(start + offset2);
            //vertexlist.Add(leftfacecolor);
            vertexlist.Add(new tutorialcubeaschunkinst.DVertex()
            {
                position = start + offset2,
                color = frontfacecolor,
                //normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            int index3 = vertexlist.Count;// _newVertzCounter;
            //vertexlist.Add(start + offset1 + offset2);
            //vertexlist.Add(leftfacecolor);
            vertexlist.Add(new tutorialcubeaschunkinst.DVertex()
            {
                position = start + offset1 + offset2,
                color = frontfacecolor,
                //normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0, 0),

            });
            _newVertzCounter++;


            listOfTriangleIndices.Add(index0);
            listOfTriangleIndices.Add(index1);
            listOfTriangleIndices.Add(index2);
            listOfTriangleIndices.Add(index3);
            listOfTriangleIndices.Add(index2);
            listOfTriangleIndices.Add(index1);
        }



        private void createBackFace(Vector4 start, Vector4 offset1, Vector4 offset2)
        {


            int index0 = vertexlist.Count;// _newVertzCounter;
            //vertexlist.Add(start);
            //vertexlist.Add(leftfacecolor);
            vertexlist.Add(new tutorialcubeaschunkinst.DVertex()
            {
                position = start,
                color = backfacecolor,
                //normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            int index1 = vertexlist.Count;//_newVertzCounter;
            //vertexlist.Add(start + offset1);
            //vertexlist.Add(leftfacecolor);
            vertexlist.Add(new tutorialcubeaschunkinst.DVertex()
            {
                position = start + offset1,
                color = backfacecolor,
                //normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            int index2 = vertexlist.Count;//_newVertzCounter;
            //vertexlist.Add(start + offset2);
            //vertexlist.Add(leftfacecolor);
            vertexlist.Add(new tutorialcubeaschunkinst.DVertex()
            {
                position = start + offset2,
                color = backfacecolor,
                //normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0, 0),

            });
            _newVertzCounter++;

            int index3 = vertexlist.Count;// _newVertzCounter;
            //vertexlist.Add(start + offset1 + offset2);
            //vertexlist.Add(leftfacecolor);
            vertexlist.Add(new tutorialcubeaschunkinst.DVertex()
            {
                position = start + offset1 + offset2,
                color = backfacecolor,
                //normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0, 0),

            });
            _newVertzCounter++;


            listOfTriangleIndices.Add(index0);
            listOfTriangleIndices.Add(index1);
            listOfTriangleIndices.Add(index2);
            listOfTriangleIndices.Add(index3);
            listOfTriangleIndices.Add(index2);
            listOfTriangleIndices.Add(index1);
        }*/


        void buildTopLeft(int xi, int yi, int zi, float block) //int _x, int _y, int _z, Vector3 chunkPos
        {
            //Vector3 somechunkpos = chunkPos;
            //somechunkpos.X -= (1.0f * width) * planeSize;
            //sccstriglevelchunk someChunk = (sccstriglevelchunk)componentparent.getChunklod0(somechunkpos.X, 0, somechunkpos.Z);


            ////Console.WriteLine(planeSize);

            _maxWidth = width;
            _maxDepth = depth;
            _maxHeight = height;
            foundVertOne = false;
            foundVertTwo = false;
            foundVertThree = false;
            foundVertFour = false;
            //TOPFACE

            block = _tempChunkArray[xi + width * (yi + height * zi)];

            if (block == 1) //|| block == 2
            {
                //if (someChunk != null)
                {
                    //if (someChunk.map != null)
                    {
                        //if (someChunk.IsTransparent(width - 1, yi, zi))
                        {

                            if (IsTransparent(xi - 1, yi, zi))// && someChunk.IsTransparent(width - 1, yi, zi))
                            {
                                for (int _yy = 0; _yy < _maxHeight; _yy++)
                                {
                                    rowIterateY = yi + _yy;

                                    for (int _zz = 0; _zz < _maxDepth; _zz++)//int _zz = _maxDepth-1; _zz >= 0; _zz--) //int _zz = 0; _zz < _maxDepth; _zz++
                                    {
                                        rowIterateZ = zi + _zz;

                                        //if (someChunk != null)
                                        {
                                            //if (someChunk.map != null)
                                            {
                                                //if (someChunk.IsTransparent(width - 1, rowIterateY, rowIterateZ))
                                                {



                                                    if (rowIterateY < height && rowIterateZ < depth)
                                                    {
                                                        if (_yy == 0 && _zz == 0)
                                                        {

                                                            oneVertIndexX = xi;
                                                            oneVertIndexY = rowIterateY;
                                                            oneVertIndexZ = rowIterateZ;
                                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);
                                                            foundVertOne = true;

                                                            if (blockExistsInArray(xi, rowIterateY + 1, rowIterateZ))
                                                            {
                                                                block = _tempChunkArray[(xi) + width * ((rowIterateY + 1) + height * (rowIterateZ))];

                                                                if (block == 0)
                                                                {
                                                                    threeVertIndexX = xi;
                                                                    threeVertIndexY = rowIterateY + 1;
                                                                    threeVertIndexZ = rowIterateZ;
                                                                    _maxHeight = _yy;
                                                                    foundVertThree = true;
                                                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y+1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);

                                                                }
                                                                else if (block == 1 || block == 2)
                                                                {
                                                                    if (blockExistsInArray(xi - 1, rowIterateY + 1, rowIterateZ))
                                                                    {
                                                                        block = _tempChunkArray[(xi - 1) + width * ((rowIterateY + 1) + height * (rowIterateZ))];

                                                                        if (block == 1 || block == 2)
                                                                        {
                                                                            threeVertIndexX = xi;
                                                                            threeVertIndexY = rowIterateY + 1;
                                                                            threeVertIndexZ = rowIterateZ;
                                                                            _maxHeight = _yy;
                                                                            foundVertThree = true;
                                                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                threeVertIndexX = xi;
                                                                threeVertIndexY = rowIterateY + 1;
                                                                threeVertIndexZ = rowIterateZ;
                                                                _maxHeight = _yy;
                                                                foundVertThree = true;
                                                                ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);

                                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                                {
                                                                    fourVertIndexX = xi;
                                                                    fourVertIndexY = threeVertIndexY;
                                                                    fourVertIndexZ = twoVertIndexZ;
                                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                                }
                                                            }

                                                            if (blockExistsInArray(xi, rowIterateY, rowIterateZ + 1))
                                                            {
                                                                block = _tempChunkArray[(xi) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                                                if (block == 0)
                                                                {
                                                                    twoVertIndexX = xi;
                                                                    twoVertIndexY = rowIterateY;
                                                                    twoVertIndexZ = rowIterateZ + 1;
                                                                    _maxDepth = _zz + 1;
                                                                    foundVertTwo = true;
                                                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);
                                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                                    {
                                                                        fourVertIndexX = xi;
                                                                        fourVertIndexY = threeVertIndexY;
                                                                        fourVertIndexZ = twoVertIndexZ;
                                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                                    }
                                                                }
                                                                else if (block == 1 || block == 2) //block == 1||
                                                                {
                                                                    if (block == 1)
                                                                    {
                                                                        if (blockExistsInArray(xi - 1, rowIterateY, rowIterateZ + 1))
                                                                        {
                                                                            block = _tempChunkArray[(xi - 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                                                            if (block == 1 || block == 2)
                                                                            {
                                                                                twoVertIndexX = xi;
                                                                                twoVertIndexY = rowIterateY;
                                                                                twoVertIndexZ = rowIterateZ + 1;
                                                                                _maxDepth = _zz + 1;
                                                                                foundVertTwo = true;
                                                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                                                {
                                                                                    fourVertIndexX = xi;
                                                                                    fourVertIndexY = threeVertIndexY;
                                                                                    fourVertIndexZ = twoVertIndexZ;
                                                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                    else if (block == 2)
                                                                    {
                                                                        twoVertIndexX = xi;
                                                                        twoVertIndexY = rowIterateY;
                                                                        twoVertIndexZ = rowIterateZ + 1;
                                                                        _maxDepth = _zz + 1;
                                                                        foundVertTwo = true;
                                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                                        {
                                                                            fourVertIndexX = xi;
                                                                            fourVertIndexY = threeVertIndexY;
                                                                            fourVertIndexZ = twoVertIndexZ;
                                                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                twoVertIndexX = xi;
                                                                twoVertIndexY = rowIterateY;
                                                                twoVertIndexZ = rowIterateZ + 1;
                                                                _maxDepth = _zz + 1;
                                                                foundVertTwo = true;
                                                                ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                                {
                                                                    fourVertIndexX = xi;
                                                                    fourVertIndexY = threeVertIndexY;
                                                                    fourVertIndexZ = twoVertIndexZ;
                                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                                }
                                                            }
                                                        }

                                                        else if (_yy == 0 && _zz > 0)
                                                        {
                                                            if (blockExistsInArray(xi, rowIterateY, rowIterateZ + 1))
                                                            {
                                                                block = _tempChunkArray[(xi) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                                                if (block == 0)
                                                                {
                                                                    twoVertIndexX = xi;
                                                                    twoVertIndexY = rowIterateY;
                                                                    twoVertIndexZ = rowIterateZ + 1;
                                                                    _maxDepth = _zz + 1;
                                                                    foundVertTwo = true;
                                                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                                    {
                                                                        fourVertIndexX = xi;
                                                                        fourVertIndexY = threeVertIndexY;
                                                                        fourVertIndexZ = twoVertIndexZ;
                                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                                    }


                                                                }
                                                                else if (block == 1 || block == 2) //block == 1||
                                                                {
                                                                    if (block == 1)
                                                                    {
                                                                        if (blockExistsInArray(xi - 1, rowIterateY, rowIterateZ + 1))
                                                                        {
                                                                            block = _tempChunkArray[(xi - 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];
                                                                            if (block == 1 || block == 2)
                                                                            {
                                                                                twoVertIndexX = xi;
                                                                                twoVertIndexY = rowIterateY;
                                                                                twoVertIndexZ = rowIterateZ + 1;
                                                                                _maxDepth = _zz + 1;
                                                                                foundVertTwo = true;
                                                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                                                {
                                                                                    fourVertIndexX = xi;
                                                                                    fourVertIndexY = threeVertIndexY;
                                                                                    fourVertIndexZ = twoVertIndexZ;
                                                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                                                }
                                                                            }
                                                                        }
                                                                        else //continue??
                                                                        {

                                                                        }
                                                                    }
                                                                    else if (block == 2)
                                                                    {
                                                                        twoVertIndexX = xi;
                                                                        twoVertIndexY = rowIterateY;
                                                                        twoVertIndexZ = rowIterateZ + 1;
                                                                        _maxDepth = _zz + 1;
                                                                        foundVertTwo = true;
                                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);
                                                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                                        {
                                                                            fourVertIndexX = xi;
                                                                            fourVertIndexY = threeVertIndexY;
                                                                            fourVertIndexZ = twoVertIndexZ;
                                                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                twoVertIndexX = xi;
                                                                twoVertIndexY = rowIterateY;
                                                                twoVertIndexZ = rowIterateZ + 1;
                                                                _maxDepth = _zz + 1;
                                                                foundVertTwo = true;

                                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                                {
                                                                    fourVertIndexX = xi;
                                                                    fourVertIndexY = threeVertIndexY;
                                                                    fourVertIndexZ = twoVertIndexZ;
                                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                                }
                                                                ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);
                                                            }

                                                            if (blockExistsInArray(xi, rowIterateY + 1, rowIterateZ))
                                                            {
                                                                block = _tempChunkArray[(xi) + width * ((rowIterateY + 1) + height * (rowIterateZ))];

                                                                if (block == 0)
                                                                {
                                                                    threeVertIndexX = xi;
                                                                    threeVertIndexY = rowIterateY + 1;
                                                                    threeVertIndexZ = rowIterateZ - _zz;
                                                                    _maxHeight = _yy;
                                                                    foundVertThree = true;
                                                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);
                                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                                    {
                                                                        fourVertIndexX = xi;
                                                                        fourVertIndexY = threeVertIndexY;
                                                                        fourVertIndexZ = twoVertIndexZ;
                                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                                    }
                                                                }
                                                                else if (block == 1 || block == 2)
                                                                {
                                                                    //********************************************************
                                                                    if (blockExistsInArray(xi - 1, rowIterateY + 1, rowIterateZ))
                                                                    {
                                                                        block = _tempChunkArray[(xi - 1) + width * ((rowIterateY + 1) + height * (rowIterateZ))];
                                                                        if (block == 1 || block == 2)
                                                                        {
                                                                            threeVertIndexX = xi;
                                                                            threeVertIndexY = rowIterateY + 1;
                                                                            threeVertIndexZ = rowIterateZ - _zz;
                                                                            _maxHeight = _yy;
                                                                            foundVertThree = true;
                                                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);
                                                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                                            {
                                                                                fourVertIndexX = xi;
                                                                                fourVertIndexY = threeVertIndexY;
                                                                                fourVertIndexZ = twoVertIndexZ;
                                                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                                            }
                                                                        }
                                                                    }
                                                                    //************************************************************
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                                {
                                                                    fourVertIndexX = xi;
                                                                    fourVertIndexY = threeVertIndexY;
                                                                    fourVertIndexZ = twoVertIndexZ;
                                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                                }
                                                            }
                                                        }
                                                        else if (_yy > 0 && _zz == 0)
                                                        {
                                                            if (blockExistsInArray(xi, rowIterateY + 1, rowIterateZ))
                                                            {
                                                                block = _tempChunkArray[(xi) + width * ((rowIterateY + 1) + height * (rowIterateZ))];

                                                                if (block == 0)
                                                                {
                                                                    //UnityEngine.Debug.Log("test");
                                                                    threeVertIndexX = xi;
                                                                    threeVertIndexY = rowIterateY + 1;
                                                                    threeVertIndexZ = rowIterateZ - _zz;
                                                                    _maxHeight = _yy;
                                                                    foundVertThree = true;
                                                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                                                    if (foundVertTwo)
                                                                    {
                                                                        if (foundVertThree)
                                                                        {
                                                                            fourVertIndexX = xi;
                                                                            fourVertIndexY = threeVertIndexY;
                                                                            fourVertIndexZ = twoVertIndexZ;
                                                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                                        }
                                                                    }
                                                                }
                                                                else if (block == 1 || block == 2)
                                                                {
                                                                    if (blockExistsInArray(xi - 1, rowIterateY + 1, rowIterateZ))
                                                                    {
                                                                        block = _tempChunkArray[(xi - 1) + width * ((rowIterateY + 1) + height * (rowIterateZ))];
                                                                        if (block == 1 || block == 2)
                                                                        {
                                                                            threeVertIndexX = xi;
                                                                            threeVertIndexY = rowIterateY + 1;
                                                                            threeVertIndexZ = rowIterateZ - _zz;
                                                                            _maxHeight = _yy;
                                                                            foundVertThree = true;
                                                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                                                            fourVertIndexX = xi;
                                                                            fourVertIndexY = threeVertIndexY;
                                                                            fourVertIndexZ = twoVertIndexZ;
                                                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                threeVertIndexX = xi;
                                                                threeVertIndexY = rowIterateY + 1;
                                                                threeVertIndexZ = rowIterateZ - _zz;
                                                                _maxHeight = _yy;
                                                                foundVertThree = true;
                                                                ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                                {
                                                                    fourVertIndexX = xi;
                                                                    fourVertIndexY = threeVertIndexY;
                                                                    fourVertIndexZ = twoVertIndexZ;
                                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                                }
                                                            }

                                                            if (blockExistsInArray(xi, rowIterateY, rowIterateZ + 1))
                                                            {
                                                                block = _tempChunkArray[(xi) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                                                if (block == 1 || block == 2)
                                                                {
                                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                                    {
                                                                        fourVertIndexX = xi;
                                                                        fourVertIndexY = threeVertIndexY;
                                                                        fourVertIndexZ = twoVertIndexZ;
                                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                                    }
                                                                }

                                                                if (blockExistsInArray(xi - 1, rowIterateY, rowIterateZ + 1))
                                                                {
                                                                    //*****************************************************************************
                                                                    block = _tempChunkArray[(xi - 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];
                                                                    if (block == 1 || block == 2)
                                                                    {
                                                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                                        {
                                                                            fourVertIndexX = xi;
                                                                            fourVertIndexY = threeVertIndexY;
                                                                            fourVertIndexZ = twoVertIndexZ;
                                                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                                        }
                                                                    }
                                                                    //*****************************************************************************
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                                {
                                                                    fourVertIndexX = xi;
                                                                    fourVertIndexY = threeVertIndexY;
                                                                    fourVertIndexZ = twoVertIndexZ;
                                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                                }
                                                            }
                                                        }

                                                        else if (_yy > 0 && _zz > 0)
                                                        {
                                                            if (blockExistsInArray(xi, rowIterateY + 1, rowIterateZ))
                                                            {
                                                                block = _tempChunkArray[(xi) + width * ((rowIterateY + 1) + height * (rowIterateZ))];

                                                                if (block == 0)
                                                                {
                                                                    //UnityEngine.Debug.Log("test");
                                                                    threeVertIndexX = xi;
                                                                    threeVertIndexY = rowIterateY + 1;
                                                                    threeVertIndexZ = rowIterateZ - _zz;
                                                                    _maxHeight = _yy;
                                                                    foundVertThree = true;
                                                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                                                    fourVertIndexX = xi;
                                                                    fourVertIndexY = threeVertIndexY;
                                                                    fourVertIndexZ = twoVertIndexZ;
                                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                                }
                                                                else if (block == 1 || block == 2)
                                                                {
                                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                                    {
                                                                        fourVertIndexX = xi;
                                                                        fourVertIndexY = threeVertIndexY;
                                                                        fourVertIndexZ = twoVertIndexZ;
                                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                                    }

                                                                    //***********************************************************
                                                                    if (blockExistsInArray(xi - 1, rowIterateY + 1, rowIterateZ))
                                                                    {
                                                                        block = _tempChunkArray[(xi - 1) + width * ((rowIterateY + 1) + height * (rowIterateZ))];
                                                                        if (block == 1 || block == 2)
                                                                        {
                                                                            threeVertIndexX = xi;
                                                                            threeVertIndexY = rowIterateY + 1;
                                                                            threeVertIndexZ = rowIterateZ - _zz;
                                                                            _maxHeight = _yy;

                                                                            foundVertThree = true;
                                                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                                            {
                                                                                fourVertIndexX = xi;
                                                                                fourVertIndexY = threeVertIndexY;
                                                                                fourVertIndexZ = twoVertIndexZ;
                                                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                                            }
                                                                        }
                                                                    }
                                                                    //*******************************************************
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                                {
                                                                    fourVertIndexX = xi;
                                                                    fourVertIndexY = threeVertIndexY;
                                                                    fourVertIndexZ = twoVertIndexZ;
                                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                                }
                                                            }

                                                            if (!blockExistsInArray(xi, rowIterateY, rowIterateZ + 1))
                                                            {
                                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                                {
                                                                    fourVertIndexX = xi;
                                                                    fourVertIndexY = threeVertIndexY;
                                                                    fourVertIndexZ = twoVertIndexZ;
                                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                                }
                                                            }
                                                        }
                                                    }

                                                    if (blockExistsInArray(xi, rowIterateY, rowIterateZ))
                                                    {
                                                        _tempChunkArray[(xi) + width * (rowIterateY + height * (rowIterateZ))] = 2;
                                                        ////////Instantiate(blockZero, new Vector3(rowIterateX + 0.5f, y, rowIterateZ + 0.5f) * planeSize + chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                        }

                                    }
                                }






                                if (getChunklod0Vertexint0(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                                {
                                    _chunkVertexArray0[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = 1;
                                }

                                if (getChunklod0Vertexint0(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                                {
                                    _chunkVertexArray0[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = 1;
                                }

                                if (getChunklod0Vertexint0(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                                {
                                    _chunkVertexArray0[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = 1;
                                }

                                if (getChunklod0Vertexint0(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                                {
                                    _chunkVertexArray0[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = 1;
                                }

                                if (getChunklod0Vertexint0(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 1 && getChunklod0Vertexint0(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 1 && getChunklod0Vertexint0(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 1 && getChunklod0Vertexint0(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 1)//
                                {
                                    //var indexofmapvertindexfordimsone = firstvertx + width * ((firstverty) + height * (firstvertz));
                                    //var indexofmapvertindexfordimsone = oneVertIndexX + width * ((oneVertIndexY) + height * (oneVertIndexZ));
                                    someindexleft = someixleft + 4 * ((someiyleft) + 4 * (someizleft));

                                    ////Console.WriteLine("index:" + someindex);

                                    mapvertindexfordims[someindexleft] = 1;

                                    vertexlisttop.Add(new tutorialcubeaschunkinst.DVertex()
                                    {
                                        position = new Vector4(oneVertIndexX * planeSize * levelofdetail * levelofdetailmul, oneVertIndexY * planeSize * levelofdetail * levelofdetailmul, oneVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos,
                                        indexPos = new Vector4(xi, yi, zi, 1),
                                        color = leftfacecolor,
                                        normal = new Vector3(0, 1, 0),
                                        padding0 = padding0,
                                        tex = new Vector2(1, 1),
                                        padding1 = padding1,
                                        padding2 = padding2,
                                    });
                                    _testVertexArray0[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = _newVertzCounter;
                                    _newVertzCounter++;

                                    //var indexofmapvertindexfordimstwo = (twoVertIndexX) + width * ((twoVertIndexY - 1) + height * (twoVertIndexZ - 1));
                                    //mapvertindexfordims[indexofmapvertindexfordimstwo] = 2;
                                    vertexlisttop.Add(new tutorialcubeaschunkinst.DVertex()
                                    {
                                        position = new Vector4(twoVertIndexX * planeSize * levelofdetail * levelofdetailmul, twoVertIndexY * planeSize * levelofdetail * levelofdetailmul, twoVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos,
                                        indexPos = new Vector4(xi, yi, zi, 2),
                                        color = leftfacecolor,
                                        normal = new Vector3(0, 1, 0),
                                        padding0 = padding0,
                                        tex = new Vector2(1, 1),
                                        padding1 = padding1,
                                        padding2 = padding2,
                                    });
                                    _testVertexArray0[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = _newVertzCounter;
                                    _newVertzCounter++;

                                    //var indexofmapvertindexfordimsthree = (threeVertIndexX - 1) + width * ((threeVertIndexY - 1) + height * (threeVertIndexZ));
                                    //mapvertindexfordims[indexofmapvertindexfordimsthree] = 3;
                                    vertexlisttop.Add(new tutorialcubeaschunkinst.DVertex()
                                    {
                                        position = new Vector4(threeVertIndexX * planeSize * levelofdetail * levelofdetailmul, threeVertIndexY * planeSize * levelofdetail * levelofdetailmul, threeVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos,
                                        indexPos = new Vector4(xi, yi, zi, 3),
                                        color = leftfacecolor,
                                        normal = new Vector3(0, 1, 0),
                                        padding0 = padding0,
                                        tex = new Vector2(1, 1),
                                        padding1 = padding1,
                                        padding2 = padding2,
                                    });
                                    _testVertexArray0[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = _newVertzCounter;
                                    _newVertzCounter++;

                                    //var indexofmapvertindexfordimsfour = (fourVertIndexX - 1) + width * ((fourVertIndexY - 1) + height * (fourVertIndexZ - 1));
                                    //mapvertindexfordims[indexofmapvertindexfordimsfour] = 4;
                                    vertexlisttop.Add(new tutorialcubeaschunkinst.DVertex()
                                    {
                                        position = new Vector4(fourVertIndexX * planeSize * levelofdetail * levelofdetailmul, fourVertIndexY * planeSize * levelofdetail * levelofdetailmul, fourVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos,
                                        indexPos = new Vector4(xi, yi, zi, 4),
                                        color = leftfacecolor,
                                        normal = new Vector3(0, 1, 0),
                                        padding0 = padding0,
                                        tex = new Vector2(1, 1),
                                        padding1 = padding1,
                                        padding2 = padding2,
                                    });
                                    _testVertexArray0[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = _newVertzCounter;
                                    _newVertzCounter++;

                                    _index0 = _testVertexArray0[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)];
                                    _index1 = _testVertexArray0[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)];
                                    _index2 = _testVertexArray0[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)];
                                    _index3 = _testVertexArray0[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)];

                                    var vert0 = new Vector4(oneVertIndexX, oneVertIndexY, oneVertIndexZ, 1);
                                    var vert1 = new Vector4(twoVertIndexX, twoVertIndexY, twoVertIndexZ, 1);
                                    var vert2 = new Vector4(threeVertIndexX, threeVertIndexY, threeVertIndexZ, 1);
                                    var vert3 = new Vector4(fourVertIndexX, fourVertIndexY, fourVertIndexZ, 1);

                                    //Vector3 firstvert = new Vector4(oneVertIndexX, oneVertIndexY, oneVertIndexZ, 1));
                                    mapfirstvertxtop[someindexleft] = oneVertIndexX;
                                    mapfirstvertytop[someindexleft] = oneVertIndexY;
                                    mapfirstvertztop[someindexleft] = oneVertIndexZ;

                                    ////Console.WriteLine("/v1d:" + vert0.Z + "/v2d:" + vert1.Z);
                                    ////Console.WriteLine("/v1h:" + vert0.Y + "/v2h:" + vert2.Y);

                                    widthdimtop[someindexleft] = (int)Math.Abs(Math.Abs(vert0.X) - Math.Abs(vert2.X)); //0  3 == 3 
                                    heightdimtop[someindexleft] = (int)Math.Abs(Math.Abs(vert0.Y) - Math.Abs(vert2.Y));
                                    depthdimtop[someindexleft] = (int)Math.Abs(Math.Abs(vert0.Z) - Math.Abs(vert1.Z));


                                    /*
                                    for (int y = 0; y < heightdimtop[someindexleft]; y++)
                                    {
                                        for (int z = 0; z < depthdimtop[someindexleft]; z++)
                                        {
                                            int theindex = (oneVertIndexX) + 8 * (((oneVertIndexY + y)) + 8 * (oneVertIndexZ + z));
                                            _tempChunkArray[theindex] = 3;
                                        }
                                    }
                                    */



                                    /*listOfTriangleIndicesleft.Add(_index0);
                                    listOfTriangleIndicesleft.Add(_index1);
                                    listOfTriangleIndicesleft.Add(_index2);
                                    listOfTriangleIndicesleft.Add(_index3);
                                    listOfTriangleIndicesleft.Add(_index2);
                                    listOfTriangleIndicesleft.Add(_index1);*/

                                    someizleft++;
                                    if (someizleft == 4)
                                    {
                                        someiyleft++;
                                        someizleft = 0;
                                    }
                                    if (someiyleft == 4)
                                    {
                                        someixleft++;
                                        someiyleft = 0;
                                    }
                                    if (someixleft == 4)
                                    {
                                        someixleft = 0;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            /*//_mesh = new Mesh();
            _mesh.vertices = vertexlist.ToArray();
            _mesh.listOfTriangleIndices = _trigz.ToArray();

            _testChunk.GetComponent<MeshFilter>().mesh = _mesh;

            _meshRend = _testChunk.GetComponent<MeshRenderer>();
            _meshRend.material = _mat;*/

            /*_mesh.vertices = vertexlist.ToArray();
            _mesh.listOfTriangleIndices = listOfTriangleIndices.ToArray();

            _testChunk.GetComponent<MeshFilter>().mesh = _mesh;*/
            //_testChunk.GetComponent<MeshRenderer>().material = _mat;
        }

        void buildTopRight(int xi, int yi, int zi, float block) //int xi, int _y, int _z, Vector3 chunkPos
        {
            _maxWidth = width;
            _maxDepth = depth;
            _maxHeight = height;
            foundVertOne = false;
            foundVertTwo = false;
            foundVertThree = false;
            foundVertFour = false;
            //TOPFACE

            block = _tempChunkArray[xi + width * (yi + height * zi)];

            if (block == 1) //|| block == 2
            {
                if (IsTransparent(xi + 1, yi, zi))
                {
                    for (int _yy = 0; _yy < _maxHeight; _yy++)
                    {
                        rowIterateY = yi + _yy;
                        for (int _zz = 0; _zz < _maxDepth; _zz++)
                        {
                            rowIterateZ = zi + _zz;

                            if (rowIterateY < height && rowIterateZ < depth)
                            {
                                if (_yy == 0 && _zz == 0)
                                {
                                    oneVertIndexX = xi + 1;
                                    oneVertIndexY = rowIterateY;
                                    oneVertIndexZ = rowIterateZ;
                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ) * planeSize  + chunkPos, Quaternion.identity);
                                    foundVertOne = true;

                                    if (blockExistsInArray(xi, rowIterateY + 1, rowIterateZ))
                                    {
                                        block = _tempChunkArray[(xi) + width * ((rowIterateY + 1) + height * (rowIterateZ))];

                                        if (block == 0)
                                        {
                                            threeVertIndexX = xi + 1;
                                            threeVertIndexY = rowIterateY + 1;
                                            threeVertIndexZ = rowIterateZ;
                                            _maxHeight = _yy;
                                            foundVertThree = true;
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y+1, rowIterateZ) * planeSize  + chunkPos, Quaternion.identity);

                                        }
                                        else if (block == 1 || block == 2)
                                        {
                                            if (blockExistsInArray(xi + 1, rowIterateY + 1, rowIterateZ))
                                            {
                                                block = _tempChunkArray[(xi + 1) + width * ((rowIterateY + 1) + height * (rowIterateZ))];

                                                if (block == 1 || block == 2)
                                                {
                                                    threeVertIndexX = xi + 1;
                                                    threeVertIndexY = rowIterateY + 1;
                                                    threeVertIndexZ = rowIterateZ;
                                                    _maxHeight = _yy;
                                                    foundVertThree = true;
                                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize  + chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        threeVertIndexX = xi + 1;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = rowIterateZ;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize  + chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi + 1;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (blockExistsInArray(xi, rowIterateY, rowIterateZ + 1))
                                    {
                                        block = _tempChunkArray[(xi) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                        if (block == 0)
                                        {
                                            twoVertIndexX = xi + 1;
                                            twoVertIndexY = rowIterateY;
                                            twoVertIndexZ = rowIterateZ + 1;
                                            _maxDepth = _zz + 1;
                                            foundVertTwo = true;
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize  + chunkPos, Quaternion.identity);
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = xi + 1;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
                                            }
                                        }
                                        else if (block == 1 || block == 2) //block == 1||
                                        {
                                            if (block == 1)
                                            {
                                                if (blockExistsInArray(xi + 1, rowIterateY, rowIterateZ + 1))
                                                {
                                                    block = _tempChunkArray[(xi + 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                                    if (block == 1 || block == 2)
                                                    {
                                                        twoVertIndexX = xi + 1;
                                                        twoVertIndexY = rowIterateY;
                                                        twoVertIndexZ = rowIterateZ + 1;
                                                        _maxDepth = _zz + 1;
                                                        foundVertTwo = true;
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize  + chunkPos, Quaternion.identity);

                                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                        {
                                                            fourVertIndexX = xi + 1;
                                                            fourVertIndexY = threeVertIndexY;
                                                            fourVertIndexZ = twoVertIndexZ;
                                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
                                                        }
                                                    }
                                                }
                                            }
                                            else if (block == 2)
                                            {
                                                twoVertIndexX = xi + 1;
                                                twoVertIndexY = rowIterateY;
                                                twoVertIndexZ = rowIterateZ + 1;
                                                _maxDepth = _zz + 1;
                                                foundVertTwo = true;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize  + chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = xi + 1;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        twoVertIndexX = xi + 1;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz + 1;
                                        foundVertTwo = true;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize  + chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi + 1;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
                                        }
                                    }
                                }

                                else if (_yy == 0 && _zz > 0)
                                {
                                    if (blockExistsInArray(xi, rowIterateY, rowIterateZ + 1))
                                    {
                                        block = _tempChunkArray[(xi) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                        if (block == 0)
                                        {
                                            twoVertIndexX = xi + 1;
                                            twoVertIndexY = rowIterateY;
                                            twoVertIndexZ = rowIterateZ + 1;
                                            _maxDepth = _zz + 1;
                                            foundVertTwo = true;
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize  + chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = xi + 1;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
                                            }


                                        }
                                        else if (block == 1 || block == 2) //block == 1||
                                        {
                                            if (block == 1)
                                            {
                                                if (blockExistsInArray(xi + 1, rowIterateY, rowIterateZ + 1))
                                                {
                                                    block = _tempChunkArray[(xi + 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];
                                                    if (block == 1 || block == 2)
                                                    {
                                                        twoVertIndexX = xi + 1;
                                                        twoVertIndexY = rowIterateY;
                                                        twoVertIndexZ = rowIterateZ + 1;
                                                        _maxDepth = _zz + 1;
                                                        foundVertTwo = true;
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize  + chunkPos, Quaternion.identity);

                                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                        {
                                                            fourVertIndexX = xi + 1;
                                                            fourVertIndexY = threeVertIndexY;
                                                            fourVertIndexZ = twoVertIndexZ;
                                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
                                                        }
                                                    }
                                                }
                                                else //continue??
                                                {

                                                }
                                            }
                                            else if (block == 2)
                                            {
                                                twoVertIndexX = xi + 1;
                                                twoVertIndexY = rowIterateY;
                                                twoVertIndexZ = rowIterateZ + 1;
                                                _maxDepth = _zz + 1;
                                                foundVertTwo = true;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize  + chunkPos, Quaternion.identity);
                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = xi + 1;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        twoVertIndexX = xi + 1;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz + 1;
                                        foundVertTwo = true;

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi + 1;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
                                        }
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize  + chunkPos, Quaternion.identity);
                                    }

                                    if (blockExistsInArray(xi, rowIterateY + 1, rowIterateZ))
                                    {
                                        block = _tempChunkArray[(xi) + width * ((rowIterateY + 1) + height * (rowIterateZ))];

                                        if (block == 0)
                                        {
                                            threeVertIndexX = xi + 1;
                                            threeVertIndexY = rowIterateY + 1;
                                            threeVertIndexZ = rowIterateZ - _zz;
                                            _maxHeight = _yy;
                                            foundVertThree = true;
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize  + chunkPos, Quaternion.identity);
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = xi + 1;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
                                            }
                                        }
                                        else if (block == 1 || block == 2)
                                        {
                                            //********************************************************
                                            if (blockExistsInArray(xi + 1, rowIterateY + 1, rowIterateZ))
                                            {
                                                block = _tempChunkArray[(xi + 1) + width * ((rowIterateY + 1) + height * (rowIterateZ))];
                                                if (block == 1 || block == 2)
                                                {
                                                    threeVertIndexX = xi + 1;
                                                    threeVertIndexY = rowIterateY + 1;
                                                    threeVertIndexZ = rowIterateZ - _zz;
                                                    _maxHeight = _yy;
                                                    foundVertThree = true;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize  + chunkPos, Quaternion.identity);
                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = xi + 1;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                            //************************************************************
                                        }
                                    }
                                    else
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi + 1;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
                                        }
                                    }
                                }
                                else if (_yy > 0 && _zz == 0)
                                {
                                    if (blockExistsInArray(xi, rowIterateY + 1, rowIterateZ))
                                    {
                                        block = _tempChunkArray[(xi) + width * ((rowIterateY + 1) + height * (rowIterateZ))];

                                        if (block == 0)
                                        {
                                            //UnityEngine.Debug.Log("test");
                                            threeVertIndexX = xi + 1;
                                            threeVertIndexY = rowIterateY + 1;
                                            threeVertIndexZ = rowIterateZ - _zz;
                                            _maxHeight = _yy;
                                            foundVertThree = true;
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize  + chunkPos, Quaternion.identity);

                                            if (foundVertTwo)
                                            {
                                                if (foundVertThree)
                                                {
                                                    fourVertIndexX = xi + 1;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                        else if (block == 1 || block == 2)
                                        {
                                            if (blockExistsInArray(xi + 1, rowIterateY + 1, rowIterateZ))
                                            {
                                                block = _tempChunkArray[(xi + 1) + width * ((rowIterateY + 1) + height * (rowIterateZ))];
                                                if (block == 1 || block == 2)
                                                {
                                                    threeVertIndexX = xi + 1;
                                                    threeVertIndexY = rowIterateY + 1;
                                                    threeVertIndexZ = rowIterateZ - _zz;
                                                    _maxHeight = _yy;
                                                    foundVertThree = true;
                                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize  + chunkPos, Quaternion.identity);

                                                    fourVertIndexX = xi + 1;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        threeVertIndexX = xi + 1;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = rowIterateZ - _zz;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize  + chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi + 1;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (blockExistsInArray(xi, rowIterateY, rowIterateZ + 1))
                                    {
                                        block = _tempChunkArray[(xi) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                        if (block == 1 || block == 2)
                                        {
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = xi + 1;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
                                            }
                                        }

                                        if (blockExistsInArray(xi + 1, rowIterateY, rowIterateZ + 1))
                                        {
                                            //*****************************************************************************
                                            block = _tempChunkArray[(xi + 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];
                                            if (block == 1 || block == 2)
                                            {
                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = xi + 1;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
                                                }
                                            }
                                            //*****************************************************************************
                                        }
                                    }
                                    else
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi + 1;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
                                        }
                                    }
                                }

                                else if (_yy > 0 && _zz > 0)
                                {
                                    if (blockExistsInArray(xi, rowIterateY + 1, rowIterateZ))
                                    {
                                        block = _tempChunkArray[(xi) + width * ((rowIterateY + 1) + height * (rowIterateZ))];

                                        if (block == 0)
                                        {
                                            //UnityEngine.Debug.Log("test");
                                            threeVertIndexX = xi + 1;
                                            threeVertIndexY = rowIterateY + 1;
                                            threeVertIndexZ = rowIterateZ - _zz;
                                            _maxHeight = _yy;
                                            foundVertThree = true;
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y + 1, rowIterateZ - _zz) * planeSize  + chunkPos, Quaternion.identity);

                                            fourVertIndexX = xi + 1;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
                                        }
                                        else if (block == 1 || block == 2)
                                        {
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = xi + 1;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
                                            }

                                            //***********************************************************
                                            if (blockExistsInArray(xi + 1, rowIterateY + 1, rowIterateZ))
                                            {
                                                block = _tempChunkArray[(xi + 1) + width * ((rowIterateY + 1) + height * (rowIterateZ))];
                                                if (block == 1 || block == 2)
                                                {
                                                    threeVertIndexX = xi + 1;
                                                    threeVertIndexY = rowIterateY + 1;
                                                    threeVertIndexZ = rowIterateZ - _zz;
                                                    _maxHeight = _yy;

                                                    foundVertThree = true;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize  + chunkPos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = xi + 1;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                            //*******************************************************
                                        }
                                    }
                                    else
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi + 1;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (!blockExistsInArray(xi, rowIterateY, rowIterateZ + 1))
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi + 1;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
                                        }
                                    }
                                }
                            }

                            if (blockExistsInArray(xi, rowIterateY, rowIterateZ))
                            {
                                _tempChunkArray[(xi) + width * (rowIterateY + height * (rowIterateZ))] = 2;
                                ////////Instantiate(blockZero, new Vector3(rowIterateX + 0.5f, y, rowIterateZ + 0.5f) * planeSize  + chunkPos, Quaternion.identity);
                            }
                        }
                    }






                    if (getChunklod0Vertexint0(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                    {
                        _chunkVertexArray0[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = 1;
                    }

                    if (getChunklod0Vertexint0(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                    {
                        _chunkVertexArray0[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = 1;
                    }

                    if (getChunklod0Vertexint0(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                    {
                        _chunkVertexArray0[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = 1;
                    }

                    if (getChunklod0Vertexint0(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                    {
                        _chunkVertexArray0[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = 1;
                    }

                    if (getChunklod0Vertexint0(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 1 && getChunklod0Vertexint0(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 1 && getChunklod0Vertexint0(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 1 && getChunklod0Vertexint0(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 1)//
                    {
                        //var indexofmapvertindexfordimsone = firstvertx + width * ((firstverty) + height * (firstvertz));
                        //var indexofmapvertindexfordimsone = oneVertIndexX + width * ((oneVertIndexY) + height * (oneVertIndexZ));
                        someindexright = someixright + 4 * ((someiyright) + 4 * (someizright));

                        ////Console.WriteLine("index:" + someindex);

                        mapvertindexfordims[someindexright] = 1;

                        vertexlisttop.Add(new tutorialcubeaschunkinst.DVertex()
                        {
                            position = new Vector4(oneVertIndexX * planeSize * levelofdetail * levelofdetailmul, oneVertIndexY * planeSize * levelofdetail * levelofdetailmul, oneVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos,
                            indexPos = new Vector4(xi, yi, zi, 1),
                            color = rightfacecolor,
                            normal = new Vector3(0, 1, 0),
                            padding0 = padding0,
                            tex = new Vector2(1, 1),
                            padding1 = padding1,
                            padding2 = padding2,
                        });
                        _testVertexArray0[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;

                        //var indexofmapvertindexfordimstwo = (twoVertIndexX) + width * ((twoVertIndexY - 1) + height * (twoVertIndexZ - 1));
                        //mapvertindexfordims[indexofmapvertindexfordimstwo] = 2;
                        vertexlisttop.Add(new tutorialcubeaschunkinst.DVertex()
                        {
                            position = new Vector4(twoVertIndexX * planeSize * levelofdetail * levelofdetailmul, twoVertIndexY * planeSize * levelofdetail * levelofdetailmul, twoVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos,
                            indexPos = new Vector4(xi, yi, zi, 2),
                            color = rightfacecolor,
                            normal = new Vector3(0, 1, 0),
                            padding0 = padding0,
                            tex = new Vector2(1, 1),
                            padding1 = padding1,
                            padding2 = padding2,
                        });

                        _testVertexArray0[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;

                        //var indexofmapvertindexfordimsthree = (threeVertIndexX - 1) + width * ((threeVertIndexY - 1) + height * (threeVertIndexZ));
                        //mapvertindexfordims[indexofmapvertindexfordimsthree] = 3;
                        vertexlisttop.Add(new tutorialcubeaschunkinst.DVertex()
                        {
                            position = new Vector4(threeVertIndexX * planeSize * levelofdetail * levelofdetailmul, threeVertIndexY * planeSize * levelofdetail * levelofdetailmul, threeVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos,
                            indexPos = new Vector4(xi, yi, zi, 3),
                            color = rightfacecolor,
                            normal = new Vector3(0, 1, 0),
                            padding0 = padding0,
                            tex = new Vector2(1, 1),
                            padding1 = padding1,
                            padding2 = padding2,
                        });
                        _testVertexArray0[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;

                        //var indexofmapvertindexfordimsfour = (fourVertIndexX - 1) + width * ((fourVertIndexY - 1) + height * (fourVertIndexZ - 1));
                        //mapvertindexfordims[indexofmapvertindexfordimsfour] = 4;
                        vertexlisttop.Add(new tutorialcubeaschunkinst.DVertex()
                        {
                            position = new Vector4(fourVertIndexX * planeSize * levelofdetail * levelofdetailmul, fourVertIndexY * planeSize * levelofdetail * levelofdetailmul, fourVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos,
                            indexPos = new Vector4(xi, yi, zi, 4),
                            color = rightfacecolor,
                            normal = new Vector3(0, 1, 0),
                            padding0 = padding0,
                            tex = new Vector2(1, 1),
                            padding1 = padding1,
                            padding2 = padding2,
                        });
                        _testVertexArray0[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;

                        _index0 = _testVertexArray0[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)];
                        _index1 = _testVertexArray0[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)];
                        _index2 = _testVertexArray0[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)];
                        _index3 = _testVertexArray0[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)];

                        var vert0 = new Vector4(oneVertIndexX, oneVertIndexY, oneVertIndexZ, 1);
                        var vert1 = new Vector4(twoVertIndexX, twoVertIndexY, twoVertIndexZ, 1);
                        var vert2 = new Vector4(threeVertIndexX, threeVertIndexY, threeVertIndexZ, 1);
                        var vert3 = new Vector4(fourVertIndexX, fourVertIndexY, fourVertIndexZ, 1);

                        //Vector3 firstvert = new Vector4(oneVertIndexX, oneVertIndexY, oneVertIndexZ, 1));
                        mapfirstvertxtop[someindexright] = oneVertIndexX;
                        mapfirstvertytop[someindexright] = oneVertIndexY;
                        mapfirstvertztop[someindexright] = oneVertIndexZ;

                        // widthdimright[someindexright] = (int)Math.Abs(vert0.X - vert2.X); //0  3 == 3 
                        // heightdimright[someindexright] = (int)Math.Abs(vert0.Y - vert2.Y);
                        //depthdimright[someindexright] = (int)Math.Abs(vert0.Z - vert1.Z);
                        widthdimtop[someindexright] = (int)Math.Abs(Math.Abs(vert0.X) - Math.Abs(vert2.X)); //0  3 == 3 
                        heightdimtop[someindexright] = (int)Math.Abs(Math.Abs(vert0.Y) - Math.Abs(vert2.Y));
                        depthdimtop[someindexright] = (int)Math.Abs(Math.Abs(vert0.Z) - Math.Abs(vert1.Z));


                        /*
                        for (int y = 0; y < heightdimtop[someindexright]; y++)
                        {
                            for (int z = 0; z < depthdimtop[someindexright]; z++)
                            {
                                int theindex = (oneVertIndexX - 1) + 8 * (((oneVertIndexY + y)) + 8 * (oneVertIndexZ + z));
                                _tempChunkArray[theindex] = 3;
                            }
                        }
                        */

                        /*listOfTriangleIndicesright.Add(_index0);
                        listOfTriangleIndicesright.Add(_index1);
                        listOfTriangleIndicesright.Add(_index2);
                        listOfTriangleIndicesright.Add(_index3);
                        listOfTriangleIndicesright.Add(_index2);
                        listOfTriangleIndicesright.Add(_index1);*/

                        someizright++;
                        if (someizright == 4)
                        {
                            someiyright++;
                            someizright = 0;
                        }
                        if (someiyright == 4)
                        {
                            someixright++;
                            someiyright = 0;
                        }
                        if (someixright == 4)
                        {
                            someixright = 0;
                        }


                    }


                }
            }
            /*//_mesh = new Mesh();
            _mesh.vertices = vertexlist.ToArray();
            _mesh.listOfTriangleIndices = listOfTriangleIndices.ToArray();

            _testChunk.GetComponent<MeshFilter>().mesh = _mesh;

            _meshRend = _testChunk.GetComponent<MeshRenderer>();
            _meshRend.material = _mat;*/
        }




        void buildFrontFace(int xi, int yi, int zi, float block) // int _x, int _y, int _z, Vector3 chunkPos
        {

            _maxWidth = width;
            _maxDepth = depth;
            _maxHeight = height;
            foundVertOne = false;
            foundVertTwo = false;
            foundVertThree = false;
            foundVertFour = false;
            //TOPFACE

            block = _tempChunkArray[xi + width * (yi + height * zi)];

            if (block == 1) //|| block == 2
            {
                if (IsTransparent(xi, yi, zi + 1))
                {
                    for (int _yy = 0; _yy < _maxHeight; _yy++)
                    {
                        rowIterateY = yi + _yy;
                        for (int _xx = 0; _xx < _maxWidth; _xx++)
                        {
                            rowIterateX = xi + _xx;

                            if (rowIterateY < height && rowIterateX < width)
                            {
                                if (_yy == 0 && _xx == 0)
                                {
                                    oneVertIndexX = rowIterateX;
                                    oneVertIndexY = rowIterateY;
                                    oneVertIndexZ = zi + 1;
                                    //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ) * planeSize  + _chunkPos, Quaternion.identity);

                                    foundVertOne = true;

                                    if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi))
                                    {
                                        block = _tempChunkArray[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi))];

                                        if (block == 0)
                                        {
                                            threeVertIndexX = rowIterateX;
                                            threeVertIndexY = rowIterateY + 1;
                                            threeVertIndexZ = zi + 1;
                                            _maxHeight = _yy;
                                            foundVertThree = true;
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y+1, rowIterateZ) * planeSize  + _chunkPos, Quaternion.identity);

                                        }
                                        else if (block == 1 || block == 2)
                                        {
                                            if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi + 1))
                                            {
                                                block = _tempChunkArray[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi + 1))];

                                                if (block == 1 || block == 2)
                                                {
                                                    threeVertIndexX = rowIterateX;
                                                    threeVertIndexY = rowIterateY + 1;
                                                    threeVertIndexZ = zi + 1;
                                                    _maxHeight = _yy;
                                                    foundVertThree = true;
                                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize  + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        threeVertIndexX = rowIterateX;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = zi + 1;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize  + _chunkPos, Quaternion.identity);

                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi + 1;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (blockExistsInArray(rowIterateX + 1, rowIterateY, zi))
                                    {
                                        block = _tempChunkArray[(rowIterateX + 1) + width * ((rowIterateY) + height * (zi))];

                                        if (block == 0)
                                        {
                                            twoVertIndexX = rowIterateX + 1;
                                            twoVertIndexY = rowIterateY;
                                            twoVertIndexZ = zi + 1;
                                            _maxWidth = _xx + 1;
                                            foundVertTwo = true;
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize  + _chunkPos, Quaternion.identity);


                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi + 1;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                        else if (block == 1 || block == 2) //block == 1||
                                        {
                                            if (block == 1)
                                            {
                                                if (blockExistsInArray(rowIterateX + 1, rowIterateY, zi + 1))
                                                {
                                                    block = _tempChunkArray[(rowIterateX + 1) + width * ((rowIterateY) + height * (zi + 1))];

                                                    if (block == 1 || block == 2)
                                                    {
                                                        twoVertIndexX = rowIterateX + 1;
                                                        twoVertIndexY = rowIterateY;
                                                        twoVertIndexZ = zi + 1;
                                                        _maxWidth = _xx + 1;
                                                        foundVertTwo = true;
                                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize  + _chunkPos, Quaternion.identity);


                                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                        {
                                                            fourVertIndexX = twoVertIndexX;
                                                            fourVertIndexY = threeVertIndexY;
                                                            fourVertIndexZ = zi + 1;
                                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                                        }
                                                    }
                                                }
                                            }
                                            else if (block == 2)
                                            {
                                                twoVertIndexX = rowIterateX + 1;
                                                twoVertIndexY = rowIterateY;
                                                twoVertIndexZ = zi + 1;
                                                _maxWidth = _xx + 1;
                                                foundVertTwo = true;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize  + _chunkPos, Quaternion.identity);


                                                if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = zi + 1;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        twoVertIndexX = rowIterateX + 1;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = zi + 1;
                                        _maxWidth = _xx + 1;
                                        foundVertTwo = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize  + _chunkPos, Quaternion.identity);


                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi + 1;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                }

                                else if (_yy == 0 && _xx > 0)
                                {
                                    if (blockExistsInArray(rowIterateX + 1, rowIterateY, zi))
                                    {
                                        block = _tempChunkArray[(rowIterateX + 1) + width * ((rowIterateY) + height * (zi))];

                                        if (block == 0)
                                        {
                                            twoVertIndexX = rowIterateX + 1;
                                            twoVertIndexY = rowIterateY;
                                            twoVertIndexZ = zi + 1;
                                            _maxWidth = _xx + 1;
                                            foundVertTwo = true;
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize  + _chunkPos, Quaternion.identity);


                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi + 1;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                            }


                                        }
                                        else if (block == 1 || block == 2) //block == 1||
                                        {
                                            if (block == 1)
                                            {
                                                if (blockExistsInArray(rowIterateX + 1, rowIterateY, zi + 1))
                                                {
                                                    block = _tempChunkArray[(rowIterateX + 1) + width * ((rowIterateY) + height * (zi + 1))];
                                                    if (block == 1 || block == 2)
                                                    {
                                                        twoVertIndexX = rowIterateX + 1;
                                                        twoVertIndexY = rowIterateY;
                                                        twoVertIndexZ = zi + 1;
                                                        _maxWidth = _xx + 1;
                                                        foundVertTwo = true;
                                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize  + _chunkPos, Quaternion.identity);


                                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                        {
                                                            fourVertIndexX = twoVertIndexX;
                                                            fourVertIndexY = threeVertIndexY;
                                                            fourVertIndexZ = zi + 1;
                                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                                        }
                                                    }
                                                }
                                                else //continue??
                                                {

                                                }
                                            }
                                            else if (block == 2)
                                            {
                                                twoVertIndexX = rowIterateX + 1;
                                                twoVertIndexY = rowIterateY;
                                                twoVertIndexZ = zi + 1;
                                                _maxWidth = _xx + 1;
                                                foundVertTwo = true;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize  + _chunkPos, Quaternion.identity);


                                                if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = zi + 1;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        twoVertIndexX = rowIterateX + 1;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = zi + 1;
                                        _maxWidth = _xx + 1;
                                        foundVertTwo = true;


                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi + 1;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                        }
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize  + _chunkPos, Quaternion.identity);
                                    }

                                    if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi))
                                    {
                                        block = _tempChunkArray[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi))];

                                        if (block == 0)
                                        {
                                            threeVertIndexX = rowIterateX - _xx;
                                            threeVertIndexY = rowIterateY + 1;
                                            threeVertIndexZ = zi + 1;
                                            _maxHeight = _yy;
                                            foundVertThree = true;
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize  + _chunkPos, Quaternion.identity);


                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi + 1;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                        else if (block == 1 || block == 2)
                                        {
                                            //********************************************************
                                            if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi + 1))
                                            {
                                                block = _tempChunkArray[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi + 1))];
                                                if (block == 1 || block == 2)
                                                {
                                                    threeVertIndexX = rowIterateX - _xx;
                                                    threeVertIndexY = rowIterateY + 1;
                                                    threeVertIndexZ = zi + 1;
                                                    _maxHeight = _yy;
                                                    foundVertThree = true;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize  + _chunkPos, Quaternion.identity);

                                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = twoVertIndexX;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = zi + 1;
                                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                            //************************************************************
                                        }
                                    }
                                    else
                                    {

                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi + 1;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                }
                                else if (_yy > 0 && _xx == 0)
                                {
                                    if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi))
                                    {
                                        block = _tempChunkArray[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi))];

                                        if (block == 0)
                                        {
                                            //UnityEngine.Debug.Log("test");
                                            threeVertIndexX = rowIterateX - _xx;
                                            threeVertIndexY = rowIterateY + 1;
                                            threeVertIndexZ = zi + 1;
                                            _maxHeight = _yy;
                                            foundVertThree = true;
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize  + _chunkPos, Quaternion.identity);

                                            if (foundVertTwo)
                                            {
                                                if (foundVertThree)
                                                {
                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = zi + 1;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                        else if (block == 1 || block == 2)
                                        {
                                            if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi + 1))
                                            {
                                                block = _tempChunkArray[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi + 1))];
                                                if (block == 1 || block == 2)
                                                {
                                                    threeVertIndexX = rowIterateX - _xx;
                                                    threeVertIndexY = rowIterateY + 1;
                                                    threeVertIndexZ = zi + 1;
                                                    _maxHeight = _yy;
                                                    foundVertThree = true;
                                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize  + _chunkPos, Quaternion.identity);

                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = zi + 1;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        threeVertIndexX = rowIterateX - _xx;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = zi + 1;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize  + _chunkPos, Quaternion.identity);

                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi + 1;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (blockExistsInArray(rowIterateX + 1, rowIterateY, zi))
                                    {
                                        block = _tempChunkArray[(rowIterateX + 1) + width * ((rowIterateY) + height * (zi))];

                                        if (block == 1 || block == 2)
                                        {
                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi + 1;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                            }
                                        }

                                        if (blockExistsInArray(rowIterateX + 1, rowIterateY, zi + 1))
                                        {
                                            //*****************************************************************************
                                            block = _tempChunkArray[(rowIterateX + 1) + width * ((rowIterateY) + height * (zi + 1))];
                                            if (block == 1 || block == 2)
                                            {
                                                if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = zi + 1;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                            //*****************************************************************************
                                        }
                                    }
                                    else
                                    {
                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi + 1;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                }

                                else if (_yy > 0 && _xx > 0)
                                {
                                    if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi))
                                    {
                                        block = _tempChunkArray[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi))];

                                        if (block == 0)
                                        {
                                            //UnityEngine.Debug.Log("test");
                                            threeVertIndexX = rowIterateX - _xx;
                                            threeVertIndexY = rowIterateY + 1;
                                            threeVertIndexZ = zi + 1;
                                            _maxHeight = _yy;
                                            foundVertThree = true;
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y + 1, rowIterateZ - ziz) * planeSize  + _chunkPos, Quaternion.identity);

                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi + 1;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                        }
                                        else if (block == 1 || block == 2)
                                        {
                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi + 1;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                            }

                                            //***********************************************************
                                            if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi + 1))
                                            {
                                                block = _tempChunkArray[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi + 1))];
                                                if (block == 1 || block == 2)
                                                {
                                                    threeVertIndexX = rowIterateX - _xx;
                                                    threeVertIndexY = rowIterateY + 1;
                                                    threeVertIndexZ = zi + 1;
                                                    _maxHeight = _yy;

                                                    foundVertThree = true;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize  + _chunkPos, Quaternion.identity);

                                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = twoVertIndexX;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = zi + 1;
                                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                            //*******************************************************
                                        }
                                    }
                                    else
                                    {
                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi + 1;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (!blockExistsInArray(rowIterateX + 1, rowIterateY, zi))
                                    {
                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi + 1;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                }
                            }

                            if (blockExistsInArray(rowIterateX, rowIterateY, zi))
                            {
                                _tempChunkArray[(rowIterateX) + width * (rowIterateY + height * (zi))] = 2;
                                //////Instantiate(blockZero, new Vector3(rowIterateX + 0.5f, y, rowIterateZ + 0.5f) * planeSize  + _chunkPos, Quaternion.identity);
                            }
                        }
                    }




                    if (getChunklod0Vertexint0(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                    {
                        _chunkVertexArray0[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = 1;
                    }

                    if (getChunklod0Vertexint0(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                    {
                        _chunkVertexArray0[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = 1;
                    }

                    if (getChunklod0Vertexint0(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                    {
                        _chunkVertexArray0[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = 1;
                    }

                    if (getChunklod0Vertexint0(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                    {
                        _chunkVertexArray0[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = 1;
                    }

                    if (getChunklod0Vertexint0(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 1 && getChunklod0Vertexint0(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 1 && getChunklod0Vertexint0(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 1 && getChunklod0Vertexint0(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 1)//
                    {
                        //var indexofmapvertindexfordimsone = firstvertx + width * ((firstverty) + height * (firstvertz));
                        //var indexofmapvertindexfordimsone = oneVertIndexX + width * ((oneVertIndexY) + height * (oneVertIndexZ));
                        someindexfront = someixfront + 4 * ((someiyfront) + 4 * (someizfront));

                        ////Console.WriteLine("index:" + someindex);

                        mapvertindexfordims[someindexfront] = 1;

                        vertexlisttop.Add(new tutorialcubeaschunkinst.DVertex()
                        {
                            position = new Vector4(oneVertIndexX * planeSize * levelofdetail * levelofdetailmul, oneVertIndexY * planeSize * levelofdetail * levelofdetailmul, oneVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos,
                            indexPos = new Vector4(xi, yi, zi, 1),
                            color = frontfacecolor,
                            normal = new Vector3(0, 1, 0),
                            padding0 = padding0,
                            tex = new Vector2(1, 1),
                            padding1 = padding1,
                            padding2 = padding2,
                        });
                        _testVertexArray0[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;

                        //var indexofmapvertindexfordimstwo = (twoVertIndexX) + width * ((twoVertIndexY - 1) + height * (twoVertIndexZ - 1));
                        //mapvertindexfordims[indexofmapvertindexfordimstwo] = 2;
                        vertexlisttop.Add(new tutorialcubeaschunkinst.DVertex()
                        {
                            position = new Vector4(twoVertIndexX * planeSize * levelofdetail * levelofdetailmul, twoVertIndexY * planeSize * levelofdetail * levelofdetailmul, twoVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos,
                            indexPos = new Vector4(xi, yi, zi, 2),
                            color = frontfacecolor,
                            normal = new Vector3(0, 1, 0),
                            padding0 = padding0,
                            tex = new Vector2(1, 1),
                            padding1 = padding1,
                            padding2 = padding2,
                        });

                        _testVertexArray0[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;

                        //var indexofmapvertindexfordimsthree = (threeVertIndexX - 1) + width * ((threeVertIndexY - 1) + height * (threeVertIndexZ));
                        //mapvertindexfordims[indexofmapvertindexfordimsthree] = 3;
                        vertexlisttop.Add(new tutorialcubeaschunkinst.DVertex()
                        {
                            position = new Vector4(threeVertIndexX * planeSize * levelofdetail * levelofdetailmul, threeVertIndexY * planeSize * levelofdetail * levelofdetailmul, threeVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos,
                            indexPos = new Vector4(xi, yi, zi, 3),
                            color = frontfacecolor,
                            normal = new Vector3(0, 1, 0),
                            padding0 = padding0,
                            tex = new Vector2(1, 1),
                            padding1 = padding1,
                            padding2 = padding2,
                        });
                        _testVertexArray0[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;

                        //var indexofmapvertindexfordimsfour = (fourVertIndexX - 1) + width * ((fourVertIndexY - 1) + height * (fourVertIndexZ - 1));
                        //mapvertindexfordims[indexofmapvertindexfordimsfour] = 4;
                        vertexlisttop.Add(new tutorialcubeaschunkinst.DVertex()
                        {
                            position = new Vector4(fourVertIndexX * planeSize * levelofdetail * levelofdetailmul, fourVertIndexY * planeSize * levelofdetail * levelofdetailmul, fourVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos,
                            indexPos = new Vector4(xi, yi, zi, 4),
                            color = frontfacecolor,
                            normal = new Vector3(0, 1, 0),
                            padding0 = padding0,
                            tex = new Vector2(1, 1),
                            padding1 = padding1,
                            padding2 = padding2,
                        });
                        _testVertexArray0[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;

                        _index0 = _testVertexArray0[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)];
                        _index1 = _testVertexArray0[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)];
                        _index2 = _testVertexArray0[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)];
                        _index3 = _testVertexArray0[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)];

                        var vert0 = new Vector4(oneVertIndexX, oneVertIndexY, oneVertIndexZ, 1);
                        var vert1 = new Vector4(twoVertIndexX, twoVertIndexY, twoVertIndexZ, 1);
                        var vert2 = new Vector4(threeVertIndexX, threeVertIndexY, threeVertIndexZ, 1);
                        var vert3 = new Vector4(fourVertIndexX, fourVertIndexY, fourVertIndexZ, 1);

                        //Vector3 firstvert = new Vector4(oneVertIndexX, oneVertIndexY, oneVertIndexZ, 1));
                        mapfirstvertxtop[someindexfront] = oneVertIndexX;
                        mapfirstvertytop[someindexfront] = oneVertIndexY;
                        mapfirstvertztop[someindexfront] = oneVertIndexZ;

                        //widthdimfront[someindexfront] = (int)Math.Abs(vert0.X - vert1.X); //0  3 == 3 
                        //heightdimfront[someindexfront] = (int)Math.Abs(vert0.Y - vert2.Y);
                        //depthdimfront[someindexfront] = (int)Math.Abs(vert0.Z - vert1.Z);


                        widthdimtop[someindexfront] = (int)Math.Abs(Math.Abs(vert0.X) - Math.Abs(vert1.X)); //0  3 == 3 
                        heightdimtop[someindexfront] = (int)Math.Abs(Math.Abs(vert0.Y) - Math.Abs(vert2.Y));
                        depthdimtop[someindexfront] = (int)Math.Abs(Math.Abs(vert0.Z) - Math.Abs(vert1.Z));




                        /*
                        for (int y = 0; y < heightdimtop[someindexfront]; y++)
                        {
                            for (int x = 0; x < widthdimtop[someindexfront]; x++)
                            {
                                int theindex = (oneVertIndexX + x) + 8 * (((oneVertIndexY + y)) + 8 * (oneVertIndexZ-1));
                                _tempChunkArray[theindex] = 3;
                            }
                        }*/



                        /*
                        listOfTriangleIndicesfront.Add(_index0);
                        listOfTriangleIndicesfront.Add(_index1);
                        listOfTriangleIndicesfront.Add(_index2);
                        listOfTriangleIndicesfront.Add(_index3);
                        listOfTriangleIndicesfront.Add(_index2);
                        listOfTriangleIndicesfront.Add(_index1);*/

                        someizfront++;
                        if (someizfront == 4)
                        {
                            someiyfront++;
                            someizfront = 0;
                        }
                        if (someiyfront == 4)
                        {
                            someixfront++;
                            someiyfront = 0;
                        }
                        if (someixfront == 4)
                        {
                            someixfront = 0;
                        }


                    }

                }
            }
            /*//_mesh = new Mesh();
            _mesh.vertices = vertexlist.ToArray();
            _mesh.listOfTriangleIndices = _trigz.ToArray();

            _testChunk.GetComponent<MeshFilter>().mesh = _mesh;

            _meshRend = _testChunk.GetComponent<MeshRenderer>();
            _meshRend.material = _mat;*/
        }


        void buildBackFace(int xi, int yi, int zi, float block) //int _x, int _y, int zi, Vector3 chunkPos
        {
            _maxWidth = width;
            _maxDepth = depth;
            _maxHeight = height;
            foundVertOne = false;
            foundVertTwo = false;
            foundVertThree = false;
            foundVertFour = false;
            //TOPFACE

            block = _tempChunkArray[xi + width * (yi + height * zi)];
            if (block == 1) //|| block == 2
            {
                if (IsTransparent(xi, yi, zi - 1))
                {





                    for (int _yy = 0; _yy < _maxHeight; _yy++)
                    {
                        rowIterateY = yi + _yy;
                        for (int _xx = 0; _xx < _maxWidth; _xx++)
                        {
                            rowIterateX = xi + _xx;

                            if (rowIterateY < height && rowIterateX < width)
                            {
                                if (_yy == 0 && _xx == 0)
                                {
                                    oneVertIndexX = rowIterateX;
                                    oneVertIndexY = rowIterateY;
                                    oneVertIndexZ = zi;
                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ) * planeSize  + _chunkPos, Quaternion.identity);
                                    foundVertOne = true;

                                    if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi))
                                    {
                                        block = _tempChunkArray[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi))];

                                        if (block == 0)
                                        {
                                            threeVertIndexX = rowIterateX;
                                            threeVertIndexY = rowIterateY + 1;
                                            threeVertIndexZ = zi;
                                            _maxHeight = _yy;
                                            foundVertThree = true;
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y+1, rowIterateZ) * planeSize  + _chunkPos, Quaternion.identity);

                                        }
                                        else if (block == 1 || block == 2)
                                        {
                                            if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi - 1))
                                            {
                                                block = _tempChunkArray[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi - 1))];

                                                if (block == 1 || block == 2)
                                                {
                                                    threeVertIndexX = rowIterateX;
                                                    threeVertIndexY = rowIterateY + 1;
                                                    threeVertIndexZ = zi;
                                                    _maxHeight = _yy;
                                                    foundVertThree = true;
                                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize  + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        threeVertIndexX = rowIterateX;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = zi;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize  + _chunkPos, Quaternion.identity);

                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (blockExistsInArray(rowIterateX + 1, rowIterateY, zi))
                                    {
                                        block = _tempChunkArray[(rowIterateX + 1) + width * ((rowIterateY) + height * (zi))];

                                        if (block == 0)
                                        {
                                            twoVertIndexX = rowIterateX + 1;
                                            twoVertIndexY = rowIterateY;
                                            twoVertIndexZ = zi;
                                            _maxWidth = _xx + 1;
                                            foundVertTwo = true;
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize  + _chunkPos, Quaternion.identity);


                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                        else if (block == 1 || block == 2) //block == 1||
                                        {
                                            if (block == 1)
                                            {
                                                if (blockExistsInArray(rowIterateX + 1, rowIterateY, zi - 1))
                                                {
                                                    block = _tempChunkArray[(rowIterateX + 1) + width * ((rowIterateY) + height * (zi - 1))];

                                                    if (block == 1 || block == 2)
                                                    {
                                                        twoVertIndexX = rowIterateX + 1;
                                                        twoVertIndexY = rowIterateY;
                                                        twoVertIndexZ = zi;
                                                        _maxWidth = _xx + 1;
                                                        foundVertTwo = true;
                                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize  + _chunkPos, Quaternion.identity);


                                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                        {
                                                            fourVertIndexX = twoVertIndexX;
                                                            fourVertIndexY = threeVertIndexY;
                                                            fourVertIndexZ = zi;
                                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                                        }
                                                    }
                                                }
                                            }
                                            else if (block == 2)
                                            {
                                                twoVertIndexX = rowIterateX + 1;
                                                twoVertIndexY = rowIterateY;
                                                twoVertIndexZ = zi;
                                                _maxWidth = _xx + 1;
                                                foundVertTwo = true;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize  + _chunkPos, Quaternion.identity);


                                                if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = zi;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        twoVertIndexX = rowIterateX + 1;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = zi;
                                        _maxWidth = _xx + 1;
                                        foundVertTwo = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize  + _chunkPos, Quaternion.identity);


                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                }

                                else if (_yy == 0 && _xx > 0)
                                {
                                    if (blockExistsInArray(rowIterateX + 1, rowIterateY, zi))
                                    {
                                        block = _tempChunkArray[(rowIterateX + 1) + width * ((rowIterateY) + height * (zi))];

                                        if (block == 0)
                                        {
                                            twoVertIndexX = rowIterateX + 1;
                                            twoVertIndexY = rowIterateY;
                                            twoVertIndexZ = zi;
                                            _maxWidth = _xx + 1;
                                            foundVertTwo = true;
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize  + _chunkPos, Quaternion.identity);


                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                            }


                                        }
                                        else if (block == 1 || block == 2) //block == 1||
                                        {
                                            if (block == 1)
                                            {
                                                if (blockExistsInArray(rowIterateX + 1, rowIterateY, zi - 1))
                                                {
                                                    block = _tempChunkArray[(rowIterateX + 1) + width * ((rowIterateY) + height * (zi - 1))];
                                                    if (block == 1 || block == 2)
                                                    {
                                                        twoVertIndexX = rowIterateX + 1;
                                                        twoVertIndexY = rowIterateY;
                                                        twoVertIndexZ = zi;
                                                        _maxWidth = _xx + 1;
                                                        foundVertTwo = true;
                                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize  + _chunkPos, Quaternion.identity);


                                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                        {
                                                            fourVertIndexX = twoVertIndexX;
                                                            fourVertIndexY = threeVertIndexY;
                                                            fourVertIndexZ = zi;
                                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                                        }
                                                    }
                                                }
                                                else //continue??
                                                {

                                                }
                                            }
                                            else if (block == 2)
                                            {
                                                twoVertIndexX = rowIterateX + 1;
                                                twoVertIndexY = rowIterateY;
                                                twoVertIndexZ = zi;
                                                _maxWidth = _xx + 1;
                                                foundVertTwo = true;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize  + _chunkPos, Quaternion.identity);


                                                if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = zi;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        twoVertIndexX = rowIterateX + 1;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = zi;
                                        _maxWidth = _xx + 1;
                                        foundVertTwo = true;


                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                        }
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize  + _chunkPos, Quaternion.identity);
                                    }

                                    if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi))
                                    {
                                        block = _tempChunkArray[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi))];

                                        if (block == 0)
                                        {
                                            threeVertIndexX = rowIterateX - _xx;
                                            threeVertIndexY = rowIterateY + 1;
                                            threeVertIndexZ = zi;
                                            _maxHeight = _yy;
                                            foundVertThree = true;
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize  + _chunkPos, Quaternion.identity);


                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                        else if (block == 1 || block == 2)
                                        {
                                            //********************************************************
                                            if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi - 1))
                                            {
                                                block = _tempChunkArray[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi - 1))];
                                                if (block == 1 || block == 2)
                                                {
                                                    threeVertIndexX = rowIterateX - _xx;
                                                    threeVertIndexY = rowIterateY + 1;
                                                    threeVertIndexZ = zi;
                                                    _maxHeight = _yy;
                                                    foundVertThree = true;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize  + _chunkPos, Quaternion.identity);

                                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = twoVertIndexX;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = zi;
                                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                            //************************************************************
                                        }
                                    }
                                    else
                                    {

                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                }
                                else if (_yy > 0 && _xx == 0)
                                {
                                    if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi))
                                    {
                                        block = _tempChunkArray[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi))];

                                        if (block == 0)
                                        {
                                            //UnityEngine.Debug.Log("test");
                                            threeVertIndexX = rowIterateX - _xx;
                                            threeVertIndexY = rowIterateY + 1;
                                            threeVertIndexZ = zi;
                                            _maxHeight = _yy;
                                            foundVertThree = true;
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize  + _chunkPos, Quaternion.identity);

                                            if (foundVertTwo)
                                            {
                                                if (foundVertThree)
                                                {
                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = zi;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                        else if (block == 1 || block == 2)
                                        {
                                            if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi - 1))
                                            {
                                                block = _tempChunkArray[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi - 1))];
                                                if (block == 1 || block == 2)
                                                {
                                                    threeVertIndexX = rowIterateX - _xx;
                                                    threeVertIndexY = rowIterateY + 1;
                                                    threeVertIndexZ = zi;
                                                    _maxHeight = _yy;
                                                    foundVertThree = true;
                                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize  + _chunkPos, Quaternion.identity);

                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = zi;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        threeVertIndexX = rowIterateX - _xx;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = zi;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize  + _chunkPos, Quaternion.identity);

                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (blockExistsInArray(rowIterateX + 1, rowIterateY, zi))
                                    {
                                        block = _tempChunkArray[(rowIterateX + 1) + width * ((rowIterateY) + height * (zi))];

                                        if (block == 1 || block == 2)
                                        {
                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                            }
                                        }

                                        if (blockExistsInArray(rowIterateX + 1, rowIterateY, zi - 1))
                                        {
                                            //*****************************************************************************
                                            block = _tempChunkArray[(rowIterateX + 1) + width * ((rowIterateY) + height * (zi - 1))];
                                            if (block == 1 || block == 2)
                                            {
                                                if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = zi;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                            //*****************************************************************************
                                        }
                                    }
                                    else
                                    {
                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                }

                                else if (_yy > 0 && _xx > 0)
                                {
                                    if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi))
                                    {
                                        block = _tempChunkArray[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi))];

                                        if (block == 0)
                                        {
                                            //UnityEngine.Debug.Log("test");
                                            threeVertIndexX = rowIterateX - _xx;
                                            threeVertIndexY = rowIterateY + 1;
                                            threeVertIndexZ = zi;
                                            _maxHeight = _yy;
                                            foundVertThree = true;
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y + 1, rowIterateZ - ziz) * planeSize  + _chunkPos, Quaternion.identity);

                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                        }
                                        else if (block == 1 || block == 2)
                                        {
                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                            }

                                            //***********************************************************
                                            if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi - 1))
                                            {
                                                block = _tempChunkArray[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi - 1))];
                                                if (block == 1 || block == 2)
                                                {
                                                    threeVertIndexX = rowIterateX - _xx;
                                                    threeVertIndexY = rowIterateY + 1;
                                                    threeVertIndexZ = zi;
                                                    _maxHeight = _yy;

                                                    foundVertThree = true;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize  + _chunkPos, Quaternion.identity);

                                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = twoVertIndexX;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = zi;
                                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                            //*******************************************************
                                        }
                                    }
                                    else
                                    {
                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (!blockExistsInArray(rowIterateX + 1, rowIterateY, zi))
                                    {
                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize  + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                }
                            }

                            if (blockExistsInArray(rowIterateX, rowIterateY, zi))
                            {
                                _tempChunkArray[(rowIterateX) + width * (rowIterateY + height * (zi))] = 2;
                                //////Instantiate(blockZero, new Vector3(rowIterateX + 0.5f, y, rowIterateZ + 0.5f) * planeSize  + _chunkPos, Quaternion.identity);
                            }
                        }
                    }





                    if (getChunklod0Vertexint0(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                    {
                        _chunkVertexArray0[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = 1;
                    }

                    if (getChunklod0Vertexint0(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                    {
                        _chunkVertexArray0[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = 1;
                    }

                    if (getChunklod0Vertexint0(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                    {
                        _chunkVertexArray0[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = 1;
                    }

                    if (getChunklod0Vertexint0(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                    {
                        _chunkVertexArray0[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = 1;
                    }

                    if (getChunklod0Vertexint0(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 1 && getChunklod0Vertexint0(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 1 && getChunklod0Vertexint0(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 1 && getChunklod0Vertexint0(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 1)//
                    {
                        //var indexofmapvertindexfordimsone = firstvertx + width * ((firstverty) + height * (firstvertz));
                        //var indexofmapvertindexfordimsone = oneVertIndexX + width * ((oneVertIndexY) + height * (oneVertIndexZ));
                        someindexback = someixback + 4 * ((someiyback) + 4 * (someizback));

                        ////Console.WriteLine("index:" + someindex);

                        mapvertindexfordims[someindexback] = 1;

                        vertexlisttop.Add(new tutorialcubeaschunkinst.DVertex()
                        {
                            position = new Vector4(oneVertIndexX * planeSize * levelofdetail * levelofdetailmul, oneVertIndexY * planeSize * levelofdetail * levelofdetailmul, oneVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos,
                            indexPos = new Vector4(xi, yi, zi, 1),
                            color = backfacecolor,
                            normal = new Vector3(0, 1, 0),
                            padding0 = padding0,
                            tex = new Vector2(1, 1),
                            padding1 = padding1,
                            padding2 = padding2,
                        });
                        _testVertexArray0[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;

                        //var indexofmapvertindexfordimstwo = (twoVertIndexX) + width * ((twoVertIndexY - 1) + height * (twoVertIndexZ - 1));
                        //mapvertindexfordims[indexofmapvertindexfordimstwo] = 2;
                        vertexlisttop.Add(new tutorialcubeaschunkinst.DVertex()
                        {
                            position = new Vector4(twoVertIndexX * planeSize * levelofdetail * levelofdetailmul, twoVertIndexY * planeSize * levelofdetail * levelofdetailmul, twoVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos,
                            indexPos = new Vector4(xi, yi, zi, 2),
                            color = backfacecolor,
                            normal = new Vector3(0, 1, 0),
                            padding0 = padding0,
                            tex = new Vector2(1, 1),
                            padding1 = padding1,
                            padding2 = padding2,
                        });

                        _testVertexArray0[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;

                        //var indexofmapvertindexfordimsthree = (threeVertIndexX - 1) + width * ((threeVertIndexY - 1) + height * (threeVertIndexZ));
                        //mapvertindexfordims[indexofmapvertindexfordimsthree] = 3;
                        vertexlisttop.Add(new tutorialcubeaschunkinst.DVertex()
                        {
                            position = new Vector4(threeVertIndexX * planeSize * levelofdetail * levelofdetailmul, threeVertIndexY * planeSize * levelofdetail * levelofdetailmul, threeVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos,
                            indexPos = new Vector4(xi, yi, zi, 3),
                            color = backfacecolor,
                            normal = new Vector3(0, 1, 0),
                            padding0 = padding0,
                            tex = new Vector2(1, 1),
                            padding1 = padding1,
                            padding2 = padding2,
                        });
                        _testVertexArray0[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;

                        //var indexofmapvertindexfordimsfour = (fourVertIndexX - 1) + width * ((fourVertIndexY - 1) + height * (fourVertIndexZ - 1));
                        //mapvertindexfordims[indexofmapvertindexfordimsfour] = 4;
                        vertexlisttop.Add(new tutorialcubeaschunkinst.DVertex()
                        {
                            position = new Vector4(fourVertIndexX * planeSize * levelofdetail * levelofdetailmul, fourVertIndexY * planeSize * levelofdetail * levelofdetailmul, fourVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos,
                            indexPos = new Vector4(xi, yi, zi, 4),
                            color = backfacecolor,
                            normal = new Vector3(0, 1, 0),
                            padding0 = padding0,
                            tex = new Vector2(1, 1),
                            padding1 = padding1,
                            padding2 = padding2,
                        });
                        _testVertexArray0[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;

                        _index0 = _testVertexArray0[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)];
                        _index1 = _testVertexArray0[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)];
                        _index2 = _testVertexArray0[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)];
                        _index3 = _testVertexArray0[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)];

                        var vert0 = new Vector4(oneVertIndexX, oneVertIndexY, oneVertIndexZ, 1);
                        var vert1 = new Vector4(twoVertIndexX, twoVertIndexY, twoVertIndexZ, 1);
                        var vert2 = new Vector4(threeVertIndexX, threeVertIndexY, threeVertIndexZ, 1);
                        var vert3 = new Vector4(fourVertIndexX, fourVertIndexY, fourVertIndexZ, 1);

                        //Vector3 firstvert = new Vector4(oneVertIndexX, oneVertIndexY, oneVertIndexZ, 1));
                        mapfirstvertxtop[someindexback] = oneVertIndexX;
                        mapfirstvertytop[someindexback] = oneVertIndexY;
                        mapfirstvertztop[someindexback] = oneVertIndexZ;

                        //widthdimback[someindexback] = (int)Math.Abs(vert0.X - vert1.X); //0  3 == 3 
                        //heightdimback[someindexback] = (int)Math.Abs(vert0.Y);
                        //depthdimback[someindexback] = (int)Math.Abs(vert0.Z - vert1.Z);


                        widthdimtop[someindexback] = (int)Math.Abs(Math.Abs(vert0.X) - Math.Abs(vert1.X)); //0  3 == 3 
                        heightdimtop[someindexback] = (int)Math.Abs(Math.Abs(vert0.Y) - Math.Abs(vert2.Y));
                        depthdimtop[someindexback] = (int)Math.Abs(Math.Abs(vert0.Z) - Math.Abs(vert1.Z));



                        /*
                        for (int y = 0; y < heightdimtop[someindexback]; y++)
                        {
                            for (int x = 0; x < widthdimtop[someindexback]; x++)
                            {
                                int theindex = (oneVertIndexX + x) + 8 * (((oneVertIndexY + y)) + 8 * (oneVertIndexZ));
                                _tempChunkArray[theindex] = 3;
                            }
                        }*/



                        /*
                        listOfTriangleIndicesback.Add(_index0);
                        listOfTriangleIndicesback.Add(_index1);
                        listOfTriangleIndicesback.Add(_index2);
                        listOfTriangleIndicesback.Add(_index3);
                        listOfTriangleIndicesback.Add(_index2);
                        listOfTriangleIndicesback.Add(_index1);*/

                        someizback++;
                        if (someizback == 4)
                        {
                            someiyback++;
                            someizback = 0;
                        }
                        if (someiyback == 4)
                        {
                            someixback++;
                            someiyback = 0;
                        }
                        if (someixback == 4)
                        {
                            someixback = 0;
                        }


                    }


                }
            }
        }




        void buildBottomFace(int xi, int yi, int zi, float block) //int _x, int _y, int _z, Vector3 chunkPos
        {
            _maxWidth = width;
            _maxDepth = depth;
            _maxHeight = height;
            foundVertOne = false;
            foundVertTwo = false;
            foundVertThree = false;
            foundVertFour = false;
            //TOPFACE

            block = _tempChunkArray[xi + width * (yi + height * zi)];
            if (block == 1) //|| block == 2
            {
                if (IsTransparent(xi, yi - 1, zi))
                {
                    for (int _xx = 0; _xx < _maxWidth; _xx++)
                    {
                        rowIterateX = xi + _xx;
                        for (int _zz = 0; _zz < _maxDepth; _zz++)
                        {
                            rowIterateZ = zi + _zz;

                            if (rowIterateX < width && rowIterateZ < depth)
                            {
                                if (_xx == 0 && _zz == 0)
                                {
                                    oneVertIndexX = rowIterateX;
                                    oneVertIndexY = yi;
                                    oneVertIndexZ = rowIterateZ;
                                    //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, yi + 1, rowIterateZ) * planeSize  + chunkPos, Quaternion.identity);
                                    foundVertOne = true;

                                    if (blockExistsInArray(rowIterateX + 1, yi, rowIterateZ))
                                    {
                                        block = _tempChunkArray[(rowIterateX + 1) + width * ((yi) + height * (rowIterateZ))];

                                        if (block == 0)
                                        {
                                            threeVertIndexX = rowIterateX + 1;
                                            threeVertIndexY = yi;
                                            threeVertIndexZ = rowIterateZ;
                                            _maxWidth = _xx;
                                            foundVertThree = true;
                                            //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ) * planeSize  + chunkPos, Quaternion.identity);

                                        }
                                        else if (block == 1 || block == 2)
                                        {
                                            if (blockExistsInArray(rowIterateX + 1, yi - 1, rowIterateZ))
                                            {
                                                block = _tempChunkArray[(rowIterateX + 1) + width * ((yi - 1) + height * (rowIterateZ))];

                                                if (block == 1 || block == 2)
                                                {
                                                    threeVertIndexX = rowIterateX + 1;
                                                    threeVertIndexY = yi;
                                                    threeVertIndexZ = rowIterateZ;
                                                    _maxWidth = _xx;
                                                    foundVertThree = true;
                                                    //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ) * planeSize  + chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = yi;
                                        threeVertIndexZ = rowIterateZ;
                                        _maxWidth = _xx;
                                        foundVertThree = true;
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ) * planeSize  + chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi;
                                            fourVertIndexZ = twoVertIndexZ;
                                            //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (blockExistsInArray(rowIterateX, yi, rowIterateZ + 1))
                                    {
                                        block = _tempChunkArray[(rowIterateX) + width * ((yi) + height * (rowIterateZ + 1))];

                                        if (block == 0)
                                        {
                                            twoVertIndexX = rowIterateX;
                                            twoVertIndexY = yi;
                                            twoVertIndexZ = rowIterateZ + 1;
                                            _maxDepth = _zz + 1;
                                            foundVertTwo = true;
                                            //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * planeSize  + chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi;
                                                fourVertIndexZ = twoVertIndexZ;
                                                //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
                                            }
                                        }
                                        else if (block == 1 || block == 2) //block == 1||
                                        {
                                            if (block == 1)
                                            {
                                                if (blockExistsInArray(rowIterateX, yi - 1, rowIterateZ + 1))
                                                {
                                                    block = _tempChunkArray[(rowIterateX) + width * ((yi - 1) + height * (rowIterateZ + 1))];

                                                    if (block == 1 || block == 2)
                                                    {
                                                        twoVertIndexX = rowIterateX;
                                                        twoVertIndexY = yi;
                                                        twoVertIndexZ = rowIterateZ + 1;
                                                        _maxDepth = _zz + 1;
                                                        foundVertTwo = true;
                                                        //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * planeSize  + chunkPos, Quaternion.identity);

                                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                        {
                                                            fourVertIndexX = threeVertIndexX;
                                                            fourVertIndexY = yi;
                                                            fourVertIndexZ = twoVertIndexZ;
                                                            //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
                                                        }
                                                    }
                                                }
                                            }
                                            else if (block == 2)
                                            {
                                                twoVertIndexX = rowIterateX;
                                                twoVertIndexY = yi;
                                                twoVertIndexZ = rowIterateZ + 1;
                                                _maxDepth = _zz + 1;
                                                foundVertTwo = true;
                                                //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * planeSize  + chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = yi;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        twoVertIndexX = rowIterateX;
                                        twoVertIndexY = yi;
                                        twoVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz + 1;
                                        foundVertTwo = true;
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * planeSize  + chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi;
                                            fourVertIndexZ = twoVertIndexZ;
                                            //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
                                        }
                                    }
                                }

                                else if (_xx == 0 && _zz > 0)
                                {
                                    if (blockExistsInArray(rowIterateX, yi, rowIterateZ + 1))
                                    {
                                        block = _tempChunkArray[(rowIterateX) + width * ((yi) + height * (rowIterateZ + 1))];

                                        if (block == 0)
                                        {
                                            twoVertIndexX = rowIterateX;
                                            twoVertIndexY = yi;
                                            twoVertIndexZ = rowIterateZ + 1;
                                            _maxDepth = _zz + 1;
                                            foundVertTwo = true;
                                            //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * planeSize  + chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi;
                                                fourVertIndexZ = twoVertIndexZ;
                                                //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
                                            }


                                        }
                                        else if (block == 1 || block == 2) //block == 1||
                                        {
                                            if (block == 1)
                                            {
                                                if (blockExistsInArray(rowIterateX, yi - 1, rowIterateZ + 1))
                                                {
                                                    block = _tempChunkArray[(rowIterateX) + width * ((yi - 1) + height * (rowIterateZ + 1))];
                                                    if (block == 1 || block == 2)
                                                    {
                                                        twoVertIndexX = rowIterateX;
                                                        twoVertIndexY = yi;
                                                        twoVertIndexZ = rowIterateZ + 1;
                                                        _maxDepth = _zz + 1;
                                                        foundVertTwo = true;
                                                        //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * planeSize  + chunkPos, Quaternion.identity);

                                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                        {
                                                            fourVertIndexX = threeVertIndexX;
                                                            fourVertIndexY = yi;
                                                            fourVertIndexZ = twoVertIndexZ;
                                                            //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
                                                        }
                                                    }
                                                }
                                                else //continue??
                                                {

                                                }
                                            }
                                            else if (block == 2)
                                            {
                                                twoVertIndexX = rowIterateX;
                                                twoVertIndexY = yi;
                                                twoVertIndexZ = rowIterateZ + 1;
                                                _maxDepth = _zz + 1;
                                                foundVertTwo = true;
                                                //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * planeSize  + chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = yi;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        twoVertIndexX = rowIterateX;
                                        twoVertIndexY = yi;
                                        twoVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz + 1;
                                        foundVertTwo = true;

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi;
                                            fourVertIndexZ = twoVertIndexZ;
                                            //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
                                        }
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * planeSize  + chunkPos, Quaternion.identity);
                                    }

                                    if (blockExistsInArray(rowIterateX + 1, yi, rowIterateZ))
                                    {
                                        block = _tempChunkArray[(rowIterateX + 1) + width * ((yi) + height * (rowIterateZ))];

                                        if (block == 0)
                                        {
                                            threeVertIndexX = rowIterateX + 1;
                                            threeVertIndexY = yi;
                                            threeVertIndexZ = rowIterateZ - _zz;
                                            _maxWidth = _xx;
                                            foundVertThree = true;
                                            //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ - _zz) * planeSize  + chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi;
                                                fourVertIndexZ = twoVertIndexZ;
                                                //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
                                            }
                                        }
                                        else if (block == 1 || block == 2)
                                        {
                                            //********************************************************
                                            if (blockExistsInArray(rowIterateX + 1, yi - 1, rowIterateZ))
                                            {
                                                block = _tempChunkArray[(rowIterateX + 1) + width * ((yi - 1) + height * (rowIterateZ))];
                                                if (block == 1 || block == 2)
                                                {
                                                    threeVertIndexX = rowIterateX + 1;
                                                    threeVertIndexY = yi;
                                                    threeVertIndexZ = rowIterateZ - _zz;
                                                    _maxWidth = _xx;
                                                    foundVertThree = true;
                                                    //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ - _zz) * planeSize  + chunkPos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = yi;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                            //************************************************************
                                        }
                                    }
                                    else
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi;
                                            fourVertIndexZ = twoVertIndexZ;
                                            //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
                                        }
                                    }
                                }
                                else if (_xx > 0 && _zz == 0)
                                {
                                    if (blockExistsInArray(rowIterateX + 1, yi, rowIterateZ))
                                    {
                                        block = _tempChunkArray[(rowIterateX + 1) + width * ((yi) + height * (rowIterateZ))];

                                        if (block == 0)
                                        {
                                            //UnityEngine.Debug.Log("test");
                                            threeVertIndexX = rowIterateX + 1;
                                            threeVertIndexY = yi;
                                            threeVertIndexZ = rowIterateZ - _zz;
                                            _maxWidth = _xx;
                                            foundVertThree = true;
                                            ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ - _zz) * planeSize  + chunkPos, Quaternion.identity);

                                            if (foundVertTwo)
                                            {
                                                if (foundVertThree)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = yi;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                        else if (block == 1 || block == 2)
                                        {
                                            if (blockExistsInArray(rowIterateX + 1, yi - 1, rowIterateZ))
                                            {
                                                block = _tempChunkArray[(rowIterateX + 1) + width * ((yi - 1) + height * (rowIterateZ))];
                                                if (block == 1 || block == 2)
                                                {
                                                    threeVertIndexX = rowIterateX + 1;
                                                    threeVertIndexY = yi;
                                                    threeVertIndexZ = rowIterateZ - _zz;
                                                    _maxWidth = _xx;
                                                    foundVertThree = true;
                                                    ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ - _zz) * planeSize  + chunkPos, Quaternion.identity);

                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = yi;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = yi;
                                        threeVertIndexZ = rowIterateZ - _zz;
                                        _maxWidth = _xx;
                                        foundVertThree = true;
                                        ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ - _zz) * planeSize  + chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (blockExistsInArray(rowIterateX, yi, rowIterateZ + 1))
                                    {
                                        block = _tempChunkArray[(rowIterateX) + width * ((yi) + height * (rowIterateZ + 1))];

                                        if (block == 1 || block == 2)
                                        {
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
                                            }
                                        }

                                        if (blockExistsInArray(rowIterateX, yi - 1, rowIterateZ + 1))
                                        {
                                            //*****************************************************************************
                                            block = _tempChunkArray[(rowIterateX) + width * ((yi - 1) + height * (rowIterateZ + 1))];
                                            if (block == 1 || block == 2)
                                            {
                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = yi;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
                                                }
                                            }
                                            //*****************************************************************************
                                        }
                                    }
                                    else
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
                                        }
                                    }
                                }

                                else if (_xx > 0 && _zz > 0)
                                {
                                    if (blockExistsInArray(rowIterateX + 1, yi, rowIterateZ))
                                    {
                                        block = _tempChunkArray[(rowIterateX + 1) + width * ((yi) + height * (rowIterateZ))];

                                        if (block == 0)
                                        {
                                            //UnityEngine.Debug.Log("test");
                                            threeVertIndexX = rowIterateX + 1;
                                            threeVertIndexY = yi;
                                            threeVertIndexZ = rowIterateZ - _zz;
                                            _maxWidth = _xx;
                                            foundVertThree = true;
                                            ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, yi + 1, rowIterateZ - _zz) * planeSize  + chunkPos, Quaternion.identity);

                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
                                        }
                                        else if (block == 1 || block == 2)
                                        {
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////Instantiate(_sphereVisualOtherColorOrange, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
                                            }

                                            //***********************************************************
                                            if (blockExistsInArray(rowIterateX + 1, yi - 1, rowIterateZ))
                                            {
                                                block = _tempChunkArray[(rowIterateX + 1) + width * ((yi - 1) + height * (rowIterateZ))];
                                                if (block == 1 || block == 2)
                                                {
                                                    threeVertIndexX = rowIterateX + 1;
                                                    threeVertIndexY = yi;
                                                    threeVertIndexZ = rowIterateZ - _zz;
                                                    _maxWidth = _xx;

                                                    foundVertThree = true;
                                                    ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ - _zz) * planeSize  + chunkPos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = yi;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        ////Instantiate(_sphereVisualOtherColorOrange, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                            //*******************************************************
                                        }
                                    }
                                    else
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (!blockExistsInArray(rowIterateX, yi, rowIterateZ + 1))
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize  + chunkPos, Quaternion.identity);
                                        }
                                    }
                                }
                            }

                            if (blockExistsInArray(rowIterateX, yi, rowIterateZ))
                            {
                                _tempChunkArray[(rowIterateX) + width * (yi + height * (rowIterateZ))] = 2;
                                //////Instantiate(blockZero, new Vector3(rowIterateX + 0.5f, y, rowIterateZ + 0.5f) * planeSize  + chunkPos, Quaternion.identity);
                            }
                        }
                    }






                    if (getChunklod0Vertexint0(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                    {
                        _chunkVertexArray0[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = 1;
                    }

                    if (getChunklod0Vertexint0(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                    {
                        _chunkVertexArray0[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = 1;
                    }

                    if (getChunklod0Vertexint0(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                    {
                        _chunkVertexArray0[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = 1;
                    }

                    if (getChunklod0Vertexint0(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                    {
                        _chunkVertexArray0[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = 1;
                    }

                    if (getChunklod0Vertexint0(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 1 && getChunklod0Vertexint0(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 1 && getChunklod0Vertexint0(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 1 && getChunklod0Vertexint0(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 1)//
                    {
                        //var indexofmapvertindexfordimsone = firstvertx + width * ((firstverty) + height * (firstvertz));
                        //var indexofmapvertindexfordimsone = oneVertIndexX + width * ((oneVertIndexY) + height * (oneVertIndexZ));
                        someindexbottom = someixbottom + 4 * ((someiybottom) + 4 * (someizbottom));

                        ////Console.WriteLine("index:" + someindex);

                        mapvertindexfordims[someindexbottom] = 1;

                        vertexlisttop.Add(new tutorialcubeaschunkinst.DVertex()
                        {
                            position = new Vector4(oneVertIndexX * planeSize * levelofdetail * levelofdetailmul, oneVertIndexY * planeSize * levelofdetail * levelofdetailmul, oneVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos,
                            indexPos = new Vector4(xi, yi, zi, 1),
                            color = bottomfacecolor,
                            normal = new Vector3(0, 1, 0),
                            padding0 = padding0,
                            tex = new Vector2(1, 1),
                            padding1 = padding1,
                            padding2 = padding2,
                        });
                        _testVertexArray0[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;

                        //var indexofmapvertindexfordimstwo = (twoVertIndexX) + width * ((twoVertIndexY - 1) + height * (twoVertIndexZ - 1));
                        //mapvertindexfordims[indexofmapvertindexfordimstwo] = 2;
                        vertexlisttop.Add(new tutorialcubeaschunkinst.DVertex()
                        {
                            position = new Vector4(twoVertIndexX * planeSize * levelofdetail * levelofdetailmul, twoVertIndexY * planeSize * levelofdetail * levelofdetailmul, twoVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos,
                            indexPos = new Vector4(xi, yi, zi, 2),
                            color = bottomfacecolor,
                            normal = new Vector3(0, 1, 0),
                            padding0 = padding0,
                            tex = new Vector2(1, 1),
                            padding1 = padding1,
                            padding2 = padding2,
                        });

                        _testVertexArray0[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;

                        //var indexofmapvertindexfordimsthree = (threeVertIndexX - 1) + width * ((threeVertIndexY - 1) + height * (threeVertIndexZ));
                        //mapvertindexfordims[indexofmapvertindexfordimsthree] = 3;
                        vertexlisttop.Add(new tutorialcubeaschunkinst.DVertex()
                        {
                            position = new Vector4(threeVertIndexX * planeSize * levelofdetail * levelofdetailmul, threeVertIndexY * planeSize * levelofdetail * levelofdetailmul, threeVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos,
                            indexPos = new Vector4(xi, yi, zi, 3),
                            color = bottomfacecolor,
                            normal = new Vector3(0, 1, 0),
                            padding0 = padding0,
                            tex = new Vector2(1, 1),
                            padding1 = padding1,
                            padding2 = padding2,
                        });
                        _testVertexArray0[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;

                        //var indexofmapvertindexfordimsfour = (fourVertIndexX - 1) + width * ((fourVertIndexY - 1) + height * (fourVertIndexZ - 1));
                        //mapvertindexfordims[indexofmapvertindexfordimsfour] = 4;
                        vertexlisttop.Add(new tutorialcubeaschunkinst.DVertex()
                        {
                            position = new Vector4(fourVertIndexX * planeSize * levelofdetail * levelofdetailmul, fourVertIndexY * planeSize * levelofdetail * levelofdetailmul, fourVertIndexZ * planeSize * levelofdetail * levelofdetailmul, 1) + chunkoriginpos,
                            indexPos = new Vector4(xi, yi, zi, 4),
                            color = bottomfacecolor,
                            normal = new Vector3(0, 1, 0),
                            padding0 = padding0,
                            tex = new Vector2(1, 1),
                            padding1 = padding1,
                            padding2 = padding2,
                        });
                        _testVertexArray0[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = _newVertzCounter;
                        _newVertzCounter++;

                        _index0 = _testVertexArray0[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)];
                        _index1 = _testVertexArray0[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)];
                        _index2 = _testVertexArray0[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)];
                        _index3 = _testVertexArray0[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)];

                        var vert0 = new Vector4(oneVertIndexX, oneVertIndexY, oneVertIndexZ, 1);
                        var vert1 = new Vector4(twoVertIndexX, twoVertIndexY, twoVertIndexZ, 1);
                        var vert2 = new Vector4(threeVertIndexX, threeVertIndexY, threeVertIndexZ, 1);
                        var vert3 = new Vector4(fourVertIndexX, fourVertIndexY, fourVertIndexZ, 1);

                        //Vector3 firstvert = new Vector4(oneVertIndexX, oneVertIndexY, oneVertIndexZ, 1));
                        mapfirstvertxtop[someindexbottom] = oneVertIndexX;
                        mapfirstvertytop[someindexbottom] = oneVertIndexY;
                        mapfirstvertztop[someindexbottom] = oneVertIndexZ;

                        //widthdimbottom[someindexbottom] = (int)Math.Abs(vert0.X - vert2.X); //0  3 == 3 
                        //heightdimbottom[someindexbottom] = (int)Math.Abs(vert0.Y - vert2.Y);
                        //depthdimbottom[someindexbottom] = (int)Math.Abs(vert0.Z - vert1.Z);

                        widthdimtop[someindexbottom] = (int)Math.Abs(Math.Abs(vert0.X) - Math.Abs(vert2.X)); //0  3 == 3 
                        heightdimtop[someindexbottom] = (int)Math.Abs(Math.Abs(vert0.Y) - Math.Abs(vert2.Y));
                        depthdimtop[someindexbottom] = (int)Math.Abs(Math.Abs(vert0.Z) - Math.Abs(vert1.Z));


                        /*
                        for (int x = 0; x < widthdimtop[someindexbottom]; x++)
                        {
                            for (int z = 0; z < depthdimtop[someindexbottom]; z++)
                            {
                                int theindex = (oneVertIndexX + x) + 8 * (((oneVertIndexY)) + 8 * (oneVertIndexZ + z));
                                _tempChunkArray[theindex] = 3;
                            }
                        }
                        */



                        /*
                        listOfTriangleIndicesbottom.Add(_index0);
                        listOfTriangleIndicesbottom.Add(_index1);
                        listOfTriangleIndicesbottom.Add(_index2);
                        listOfTriangleIndicesbottom.Add(_index3);
                        listOfTriangleIndicesbottom.Add(_index2);
                        listOfTriangleIndicesbottom.Add(_index1);*/

                        someizbottom++;
                        if (someizbottom == 4)
                        {
                            someiybottom++;
                            someizbottom = 0;
                        }
                        if (someiybottom == 4)
                        {
                            someixbottom++;
                            someiybottom = 0;
                        }
                        if (someixbottom == 4)
                        {
                            someixbottom = 0;
                        }


                    }

                }
            }
            /*//_mesh = new Mesh();
            _mesh.vertices = vertexlist.ToArray();
            _mesh.listOfTriangleIndices = listOfTriangleIndices.ToArray();

            _testChunk.GetComponent<MeshFilter>().mesh = _mesh;

            _meshRend = _testChunk.GetComponent<MeshRenderer>();
            _meshRend.material = _mat;*/
        }














        public void setAdjacentChunks(Vector3 pos, int indexx, int indexy, int indexz)
        {
            /*if (indexx == 0)
            {
                if (listofchunksadjacent)
                {

                }
            }*/
        }






        /*
        public void setAdjacentChunks(Vector3 pos, int indexx, int indexy, int indexz)
        {
            //int width = currentChunk.sclevelgenclass.width;
            //int height = currentChunk.sclevelgenclass.height;
            //int depth = currentChunk.sclevelgenclass.depth;

            //////Debug.Log("x: " + (indexx) + " y: " + (indexy) + " z: " + (indexz));

            int useonlyunitOneForNeighboorIndexPlease = 1;

            if (indexx == 0)
            {
                if (componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z);

                    if (adjacentChunk.map != null)
                    {


                        if (adjacentChunk.Getint((int)width - 1, (int)indexy, (int)indexz) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)width - 1, (int)indexy, (int)indexz, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
            }

            if (indexx == width - 1)
            {
                if (componentparent.getChunklod0((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)0, (int)indexy, (int)indexz) == 1)
                        {
                            //////Debug.Log("adjacent chunk right exists");
                            adjacentChunk.Setint((int)0, (int)indexy, (int)indexz, activeBlockType, pos);
                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
            }

            if (indexy == 0)
            {
                if (componentparent.getChunklod0((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)indexx, (int)height - 1, (int)indexz) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)indexx, (int)height - 1, (int)indexz, activeBlockType, pos);
                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
            }

            if (indexy == height - 1)
            {
                if (componentparent.getChunklod0((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)indexx, (int)0, (int)indexz) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)indexx, (int)0, (int)indexz, activeBlockType, pos);
                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
            }

            if (indexz == 0)
            {
                if (componentparent.getChunklod0((int)pos.X, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)indexx, (int)indexy, (int)depth - 1) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)indexx, (int)indexy, (int)depth - 1, activeBlockType, pos);
                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
            }

            if (indexz == depth - 1)
            {
                if (componentparent.getChunklod0((int)pos.X, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)indexx, (int)indexy, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)indexx, (int)indexy, (int)0, activeBlockType, pos);
                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
            }
















            //neighboorTiles
            if (indexx == 0 && indexy == 0 && indexz > 0 && indexz < depth - 1)
            {
                //already checked
                /*if (componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z);

                    if (adjacentChunk.Getint((int)width - 1, (int)indexy, (int)indexz) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.Setint((int)width - 1, (int)indexy, (int)indexz, activeBlockType, pos);

                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        adjacentChunk.chunkbuildingswtc = 1;
                        adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                    }
                }

                if (componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)width - 1, (int)height - 1, (int)indexz) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)width - 1, (int)height - 1, (int)indexz, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }

                /*if (componentparent.getChunklod0((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z);

                    if (adjacentChunk.Getint((int)indexx, (int)height - 1, (int)indexz) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.Setint((int)indexx, (int)height - 1, (int)indexz, activeBlockType, pos);

                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        adjacentChunk.chunkbuildingswtc = 1;
                        adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                    }
                }
            }
            if (indexx == 0 && indexy == 0 && indexz == 0)
            {
                /*if (componentparent.getChunklod0((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X , (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z);

                    if (adjacentChunk.Getint((int)width-1, (int)height - 1, (int)depth-1) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.Setint((int)width - 1, (int)height - 1, (int)depth - 1, activeBlockType, pos);

                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        adjacentChunk.chunkbuildingswtc = 1;
                        adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                    }
                }


                if (componentparent.getChunklod0((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)width - 1, (int)height - 1, (int)depth - 1) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)width - 1, (int)height - 1, (int)depth - 1, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
                /*
                if (componentparent.getChunklod0((int)pos.X, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);

                    if (adjacentChunk.Getint((int)width - 1, (int)height - 1, (int)depth - 1) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.Setint((int)width - 1, (int)height - 1, (int)depth - 1, activeBlockType, pos);

                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        adjacentChunk.chunkbuildingswtc = 1;
                        adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                    }
                }

                if (componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)width - 1, (int)height - 1, (int)depth - 1) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)width - 1, (int)height - 1, (int)depth - 1, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }

                if (componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)width - 1, (int)height - 1, (int)depth - 1) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)width - 1, (int)height - 1, (int)depth - 1, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
            }










            if (indexx == 0 && indexy == 0 && indexz == depth - 1)
            {
                /*if (componentparent.getChunklod0((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z);

                    if (adjacentChunk.Getint((int)indexx, (int)height - 1, (int)0) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.Setint((int)indexx, (int)height - 1, (int)0, activeBlockType, pos);

                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        adjacentChunk.chunkbuildingswtc = 1;
                        adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                    }
                }

                if (componentparent.getChunklod0((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)width - 1, (int)height - 1, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)width - 1, (int)height - 1, (int)0, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
                /*
                if (componentparent.getChunklod0((int)pos.X, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);

                    if (adjacentChunk.Getint((int)width - 1, (int)height - 1, (int)0) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.Setint((int)width - 1, (int)height - 1, (int)0, activeBlockType, pos);
                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        adjacentChunk.chunkbuildingswtc = 1;
                        adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                    }
                }

                if (componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)width - 1, (int)height - 1, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)width - 1, (int)height - 1, (int)0, activeBlockType, pos);
                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }

                if (componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)width - 1, (int)height - 1, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)width - 1, (int)height - 1, (int)0, activeBlockType, pos);
                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
            }




            if (indexx == 0 && indexz == 0 && indexy > 0 && indexy < height - 1)
            {
                /*if (componentparent.getChunklod0((int)pos.X, (int)pos.Y, (int)pos.Z- useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);

                    if (adjacentChunk.Getint((int)width - 1, (int)indexz, (int)depth-1) == 1)
                    {
                        //////Debug.Log("adjacent chunk left exists");
                        adjacentChunk.Setint((int)width - 1, (int)indexz, (int)depth - 1, activeBlockType, pos);

                        adjacentChunk.sccsSetMap();
                        adjacentChunk.Regenerate();
                        adjacentChunk.chunkbuildingswtc = 1;
                        adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                    }
                }

                if (componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)width - 1, (int)indexy, (int)depth - 1) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)width - 1, (int)indexy, (int)depth - 1, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
            }
            /*if (indexx == 0 && indexz == 0 && indexy == 0)
            {

            }
            if (indexx == 0 && indexz == 0 && indexy == height - 1)
            {
                if (componentparent.getChunklod0((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)width - 1, (int)0, (int)depth - 1) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)width - 1, (int)0, (int)depth - 1, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }

                if (componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)width - 1, (int)0, (int)depth - 1) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)width - 1, (int)0, (int)depth - 1, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }

                if (componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)width - 1, (int)0, (int)depth - 1) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)width - 1, (int)0, (int)depth - 1, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
            }

            if (indexz == 0 && indexy == 0 && indexx > 0 && indexx < width - 1)
            {

                if (componentparent.getChunklod0((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)indexx, (int)height - 1, (int)depth - 1) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)indexx, (int)height - 1, (int)depth - 1, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }

            }
            /*if (indexz == 0 && indexy == 0 && indexx == 0)
            {

            }
            if (indexz == 0 && indexy == 0 && indexx == width - 1)
            {
                if (componentparent.getChunklod0((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)0, (int)height - 1, (int)depth - 1) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)0, (int)height - 1, (int)depth - 1, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }

                if (componentparent.getChunklod0((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)0, (int)height - 1, (int)depth - 1) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)0, (int)height - 1, (int)depth - 1, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }



                if (componentparent.getChunklod0((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)0, (int)height - 1, (int)depth - 1) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)0, (int)height - 1, (int)depth - 1, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
            }

            if (indexx == width - 1 && indexy == 0 && indexz > 0 && indexz < depth - 1)
            {
                if (componentparent.getChunklod0((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)0, (int)height - 1, (int)indexz) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)0, (int)height - 1, (int)indexz, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
            }
            /*if (indexx == width - 1 && indexy == 0 && indexz == 0)
            {

            }
            if (indexx == width - 1 && indexy == 0 && indexz == depth - 1)
            {
                if (componentparent.getChunklod0((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)0, (int)height - 1, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)0, (int)height - 1, (int)0, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }

                if (componentparent.getChunklod0((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)0, (int)height - 1, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)0, (int)height - 1, (int)0, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }


                if (componentparent.getChunklod0((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)0, (int)height - 1, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)0, (int)height - 1, (int)0, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
            }

            if (indexx == 0 && indexz == depth - 1 && indexy > 0 && indexy < height - 1)
            {

                if (componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)width - 1, (int)indexy, (int)depth - 1) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)width - 1, (int)indexy, (int)depth - 1, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
            }
            if (indexx == 0 && indexz == depth - 1 && indexy == 0)
            {
                if (componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)width - 1, (int)height - 1, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)width - 1, (int)height - 1, (int)0, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
                if (componentparent.getChunklod0((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)width - 1, (int)height - 1, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)width - 1, (int)height - 1, (int)0, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
                if (componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)width - 1, (int)height - 1, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)width - 1, (int)height - 1, (int)0, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }




            }
            if (indexx == 0 && indexz == depth - 1 && indexy == height - 1)
            {
                if (componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)width - 1, (int)0, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)width - 1, (int)0, (int)0, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
                if (componentparent.getChunklod0((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)width - 1, (int)0, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)width - 1, (int)0, (int)0, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
                if (componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)width - 1, (int)0, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)width - 1, (int)0, (int)0, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }

            }
            if (indexz == 0 && indexy == height - 1 && indexx > 0 && indexx < width - 1)
            {
                if (componentparent.getChunklod0((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)indexx, (int)0, (int)depth - 1) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)indexx, (int)0, (int)depth - 1, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
            }
            if (indexz == 0 && indexy == height - 1 && indexx == 0)
            {
                if (componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)width - 1, (int)0, (int)depth - 1) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)width - 1, (int)0, (int)depth - 1, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
                if (componentparent.getChunklod0((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)width - 1, (int)0, (int)depth - 1) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)width - 1, (int)0, (int)depth - 1, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
                if (componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X - useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)width - 1, (int)0, (int)depth - 1) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)width - 1, (int)0, (int)depth - 1, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
            }
            if (indexz == 0 && indexy == height - 1 && indexx == width - 1)
            {
                if (componentparent.getChunklod0((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)0, (int)0, (int)depth - 1) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)0, (int)0, (int)depth - 1, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
                if (componentparent.getChunklod0((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)0, (int)0, (int)depth - 1) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)0, (int)0, (int)depth - 1, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
                if (componentparent.getChunklod0((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z - useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)0, (int)0, (int)depth - 1) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)0, (int)0, (int)depth - 1, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
            }

            if (indexx == width - 1 && indexy == height - 1 && indexz > 0 && indexz < depth - 1)
            {
                if (componentparent.getChunklod0((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)0, (int)0, (int)indexz) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)0, (int)0, (int)indexz, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
            }
            /*if (indexx == width - 1 && indexy == height - 1 && indexz == 0)
            {

            }
            if (indexx == width - 1 && indexy == height - 1 && indexz == depth - 1)
            {
                if (componentparent.getChunklod0((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)0, (int)0, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)0, (int)0, (int)0, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }

                if (componentparent.getChunklod0((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)0, (int)0, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)0, (int)0, (int)0, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }

                if (componentparent.getChunklod0((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)0, (int)0, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)0, (int)0, (int)0, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
            }


            if (indexx == width - 1 && indexz == depth - 1 && indexy > 0 && indexy < height - 1)
            {
                if (componentparent.getChunklod0((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)0, (int)indexy, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)0, (int)indexy, (int)0, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
            }
            if (indexx == width - 1 && indexz == depth - 1 && indexy == 0)
            {
                if (componentparent.getChunklod0((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)0, (int)height - 1, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)0, (int)height - 1, (int)0, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }

                if (componentparent.getChunklod0((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)0, (int)height - 1, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)0, (int)height - 1, (int)0, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }

                if (componentparent.getChunklod0((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X + useonlyunitOneForNeighboorIndexPlease, (int)pos.Y - useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)0, (int)height - 1, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)0, (int)height - 1, (int)0, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
            }
            /*if (indexx == width - 1 && indexz == depth - 1 && indexy == height - 1)
            {

            }


            if (indexz == depth - 1 && indexy == height - 1 && indexx > 0 && indexx < width - 1)
            {
                if (componentparent.getChunklod0((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease) != null)
                {
                    sclevelgenclass adjacentChunk = (sclevelgenclass)componentparent.getChunklod0((int)pos.X, (int)pos.Y + useonlyunitOneForNeighboorIndexPlease, (int)pos.Z + useonlyunitOneForNeighboorIndexPlease);
                    if (adjacentChunk.map != null)
                    {

                        if (adjacentChunk.Getint((int)indexx, (int)0, (int)0) == 1)
                        {
                            //////Debug.Log("adjacent chunk left exists");
                            adjacentChunk.Setint((int)indexx, (int)0, (int)0, activeBlockType, pos);

                            adjacentChunk.sccsSetMap();
                            adjacentChunk.Regenerate();
                            adjacentChunk.chunkbuildingswtc = 1;
                            if (adjacentChunk.vertexlist.Count > 0)
                            {
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = adjacentChunk.vertexlist.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = adjacentChunk.triangles.ToArray();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                                adjacentChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

                                adjacentChunk.planetchunk.transform.GetComponent<MeshRenderer>().material = hitmaterial;
                            }
                        }
                    }
                }
            }

            /*if (indexz == depth - 1 && indexy == height - 1 && indexx == 0)
            {

            }*/
        /*if (indexz == depth - 1 && indexy == height - 1 && indexx == width - 1)
        {

        }*/

        /*for (int x = -1; x < 1; x++)
        {
            for (int y = -1; y < 1; y++)
            {
                for (int z = -1; z < 1; z++)
                {

                }
            }
        }
    }*/

        public int GetByte(int x, int y, int z)
        {
            if ((x < 0) || (y < 0) || (z < 0) || (x >= width) || (y >= height) || (z >= depth))
            {
                ////Console.WriteLine("out of range");
                return 0;
            }
            ////Console.WriteLine("index:" + (x + width * (y + height * z)) + "/mapl:" + map.Length + "/x:" + x + "/y:" + y + "/z:" + z + "/w:" + width + "/h:" + height + "/d:" + depth);
            return map[x + width * (y + height * z)];
        }

        //using as reference from Craig Perko's first minecraft tutorial on youtube. //https://www.youtube.com/watch?v=YpHQ-Kykp_s&t=18s&ab_channel=CraigPerko
        public bool IsTransparent(int _x, int _y, int _z)
        {
            if ((_x < 0) || (_y < 0) || (_z < 0) || (_x >= width) || (_y >= height) || (_z >= depth)) return true;
            return map[_x + width * (_y + height * _z)] == 0; //_chunkArray
        }

        int getChunklod0int(int _x, int _y, int _z)
        {
            if (_x >= 0 && _y >= 0 && _z >= 0 && _x < width && _y < height && _z < depth)
            {
                return map[_x + width * (_y + height * _z)]; //_chunkArray
            }
            return 0;
        }


        int getTempArrayint(int _x, int _y, int _z)
        {
            if (_x >= 0 && _y >= 0 && _z >= 0 && _x < width && _y < height && _z < depth)
            {
                return _tempChunkArray[_x + width * (_y + height * _z)];
            }
            return 0;
        }



        int getChunklod0Vertexint0(int _x, int _y, int _z)
        {
            if (_x >= 0 && _y >= 0 && _z >= 0 && _x < vertexlistWidth && _y < vertexlistHeight && _z < vertexlistDepth)
            {
                return _chunkVertexArray0[_x + vertexlistWidth * (_y + vertexlistHeight * _z)];
            }
            return 0;
        }



        /*
        int getChunklod0Vertexint1(int _x, int _y, int _z)
        {
            if (_x >= 0 && _y >= 0 && _z >= 0 && _x < vertexlistWidth && _y < vertexlistHeight && _z < vertexlistDepth)
            {
                return _chunkVertexArray1[_x + vertexlistWidth * (_y + vertexlistHeight * _z)];
            }
            return 0;
        }




        int getChunklod0Vertexint2(int _x, int _y, int _z)
        {
            if (_x >= 0 && _y >= 0 && _z >= 0 && _x < vertexlistWidth && _y < vertexlistHeight && _z < vertexlistDepth)
            {
                return _chunkVertexArray2[_x + vertexlistWidth * (_y + vertexlistHeight * _z)];
            }
            return 0;
        }




        int getChunklod0Vertexint0(int _x, int _y, int _z)
        {
            if (_x >= 0 && _y >= 0 && _z >= 0 && _x < vertexlistWidth && _y < vertexlistHeight && _z < vertexlistDepth)
            {
                return _chunkVertexArray3[_x + vertexlistWidth * (_y + vertexlistHeight * _z)];
            }
            return 0;
        }




        int getChunklod0Vertexint4(int _x, int _y, int _z)
        {
            if (_x >= 0 && _y >= 0 && _z >= 0 && _x < vertexlistWidth && _y < vertexlistHeight && _z < vertexlistDepth)
            {
                return _chunkVertexArray4[_x + vertexlistWidth * (_y + vertexlistHeight * _z)];
            }
            return 0;
        }




        int getChunklod0Vertexint0(int _x, int _y, int _z)
        {
            if (_x >= 0 && _y >= 0 && _z >= 0 && _x < vertexlistWidth && _y < vertexlistHeight && _z < vertexlistDepth)
            {
                return _chunkVertexArray5[_x + vertexlistWidth * (_y + vertexlistHeight * _z)];
            }
            return 0;
        }

        */









        public bool blockExistsInArray(int _x, int _y, int _z)
        {
            if ((_x < 0) || (_y < 0) || (_z < 0) || (_x >= width) || (_y >= height) || (_z >= depth))
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        public void Setint(int x, int y, int z, int block, Vector3 chunkintpos_)
        {
            /*if (addfracturedcubeonimpact == 1)
            {
                //var unityTutorialObjectPool = this.GameObject.GetComponent<NewObjectPoolerScript>();
                var UnityTutorialPooledObject = UnityTutorialGameObjectPool.GetPooledObject();
                UnityTutorialPooledObject.transform.position = chunkintpos_;
                UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                UnityTutorialPooledObject.SetActive(true);
            }*/

            if ((x < 0) || (y < 0) || (z < 0) || (y >= width) || (x >= height) || (z >= depth))
            {
                //Debug.Log("out of range");
                return;
            }

            int indexOf = x + width * (y + depth * z);
            map[indexOf] = block;
        }


        public int Getint(int x, int y, int z)
        {
            if ((x < 0) || (y < 0) || (z < 0) || (y >= width) || (x >= height) || (z >= depth))
            {
                return 0;
            }

            int indexOf = x + width * (y + depth * z);
            return map[indexOf];
            //return map[x + width * (y + depth * z)];
        }




















        /*
        public void Regenerate(Vector4 currentPosition)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < depth; z++)
                    {

                        block = map[x + width * (y + height * z)];

                        DrawBrick(x, y, z, currentPosition, block);
                    }
                }
            }
        }

        public void DrawBrick(int x, int y, int z, Vector4 currentPosition, int block)
        {
            Vector4 start = new Vector4(x * planeSize, y * planeSize, z * planeSize,1) + chunkoriginpos;
            Vector4 offset1, offset2;

            offset1 = forward * planeSize;
            offset2 = right * planeSize;
            createTopFace(start + up * planeSize, offset1, offset2, currentPosition, x, y, z, 1);

            offset1 = back * planeSize;
            offset2 = down * planeSize;
            createleftFace(start + up * planeSize + forward * planeSize, offset1, offset2, currentPosition, x, y, z, 1);

            offset1 = up * planeSize;
            offset2 = forward * planeSize;
            createRightFace(start + right * planeSize, offset1, offset2, currentPosition, x, y, z, 1);

            offset1 = left * planeSize;
            offset2 = up * planeSize;
            createFrontFace(start + right * planeSize, offset1, offset2, currentPosition, x, y, z, 1);

            offset1 = right * planeSize;
            offset2 = up * planeSize;
            createBackFace(start + forward * planeSize, offset1, offset2, currentPosition, x, y, z, 1);

            offset1 = right * planeSize;
            offset2 = forward * planeSize;
            createBottomFace(start, offset1, offset2, currentPosition, x, y, z, 1);
        }


        private void createTopFace(Vector4 start, Vector4 offset1, Vector4 offset2, Vector4 currentPosition, int x, int y, int z, int block)
        {
            int index = listOfVerts.Count;

            listOfVerts.Add(new sclevelgenclass.DVertex()
            {
                position = start,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, 0),
                tex = new Vector2(0, 0),
            });

            listOfVerts.Add(new sclevelgenclass.DVertex()
            {
                position = start + offset1,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, 0),
                tex = new Vector2(0, 1),
            });


            listOfVerts.Add(new sclevelgenclass.DVertex()
            {
                position = start + offset2,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, 0),
                tex = new Vector2(1, 0),
            });


            listOfVerts.Add(new sclevelgenclass.DVertex()
            {
                position = start + offset1 + offset2,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, 0),
                tex = new Vector2(1f, 1),
            });

            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 0);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 3);
        }



        private void createBottomFace(Vector4 start, Vector4 offset1, Vector4 offset2, Vector4 currentPosition, int x, int y, int z, int block)
        {
            int index = listOfVerts.Count;
            listOfVerts.Add(new sclevelgenclass.DVertex()
            {
                position = start,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(0, 1, -1),
                tex = new Vector2(0f, 0),
            });

            listOfVerts.Add(new sclevelgenclass.DVertex()
            {
                position = start + offset1,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(0, 1, -1),
                tex = new Vector2(0f, 1f),
            });


            listOfVerts.Add(new sclevelgenclass.DVertex()
            {
                position = start + offset2,
                indexPos = new Vector4(x, y, z, block),
                normal = new Vector3(0, 1, -1),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                tex = new Vector2(1, 0),

            });


            listOfVerts.Add(new sclevelgenclass.DVertex()
            {
                position = start + offset1 + offset2,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(0, 1, -1),
                tex = new Vector2(1, 1f),
            });

            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 0);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 3);

        }


        private void createFrontFace(Vector4 start, Vector4 offset1, Vector4 offset2, Vector4 currentPosition, int x, int y, int z, int block)
        {
            int index = listOfVerts.Count;

            listOfVerts.Add(new sclevelgenclass.DVertex()
            {
                position = start,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 0, 0),
                tex = new Vector2(0, 0),
            });

            listOfVerts.Add(new sclevelgenclass.DVertex()
            {
                position = start + offset1,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 0, 0),
                tex = new Vector2(0, 1f),
            });


            listOfVerts.Add(new sclevelgenclass.DVertex()
            {
                position = start + offset2,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 0, 0),
                tex = new Vector2(1, 0),
            });


            listOfVerts.Add(new sclevelgenclass.DVertex()
            {
                position = start + offset1 + offset2,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 0, 0),
                tex = new Vector2(1, 1f),
            });

            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 0);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 3);

        }
        private void createBackFace(Vector4 start, Vector4 offset1, Vector4 offset2, Vector4 currentPosition, int x, int y, int z, int block)
        {
            int index = listOfVerts.Count;

            listOfVerts.Add(new sclevelgenclass.DVertex()
            {
                position = start,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(0, 0, -1),
                tex = new Vector2(0, 0),
            });

            listOfVerts.Add(new sclevelgenclass.DVertex()
            {
                position = start + offset1,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(0, 0, -1),
                tex = new Vector2(0, 1),
            });


            listOfVerts.Add(new sclevelgenclass.DVertex()
            {
                position = start + offset2,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(0, 0, -1),
                tex = new Vector2(1, 0),
            });


            listOfVerts.Add(new sclevelgenclass.DVertex()
            {
                position = start + offset1 + offset2,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(0, 0, -1),
                tex = new Vector2(1, 1f),
            });

            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 0);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 3);
        }

        private void createRightFace(Vector4 start, Vector4 offset1, Vector4 offset2, Vector4 currentPosition, int x, int y, int z, int block)
        {
            int index = listOfVerts.Count;

            listOfVerts.Add(new sclevelgenclass.DVertex()
            {
                position = start,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 0, -1),
                tex = new Vector2(0, 0),
            });

            listOfVerts.Add(new sclevelgenclass.DVertex()
            {
                position = start + offset1,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 0, -1),
                tex = new Vector2(0, 1),
            });


            listOfVerts.Add(new sclevelgenclass.DVertex()
            {
                position = start + offset2,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 0, -1),
                tex = new Vector2(1, 0),
            });


            listOfVerts.Add(new sclevelgenclass.DVertex()
            {
                position = start + offset1 + offset2,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 0, -1),
                tex = new Vector2(1, 1f),
            });

            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 0);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 3);
        }

        private void createleftFace(Vector4 start, Vector4 offset1, Vector4 offset2, Vector4 currentPosition, int x, int y, int z, int block)
        {
            int index = listOfVerts.Count;
            listOfVerts.Add(new sclevelgenclass.DVertex()
            {
                position = start,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0, 0),
            });

            listOfVerts.Add(new sclevelgenclass.DVertex()
            {
                position = start + offset1,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, -1),
                tex = new Vector2(0, 1),
            });


            listOfVerts.Add(new sclevelgenclass.DVertex()
            {
                position = start + offset2,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, -1),
                tex = new Vector2(1, 0),
            });


            listOfVerts.Add(new sclevelgenclass.DVertex()
            {
                position = start + offset1 + offset2,
                indexPos = new Vector4(x, y, z, block),
                color = new Vector4(0.25f, 0.25f, 0.25f, 1),
                normal = new Vector3(-1, 1, -1),
                tex = new Vector2(1, 1),
            });

            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 0);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 3);

        }
        public bool IsTransparent(int x, int y, int z)
        {
            if ((x < 0) || (y < 0) || (z < 0) || (x >= width) || (y >= height) || (z >= depth)) return true;
            {
                return map[x + width * (y + height * z)] == 0;
            }
        }*/
    }
}
