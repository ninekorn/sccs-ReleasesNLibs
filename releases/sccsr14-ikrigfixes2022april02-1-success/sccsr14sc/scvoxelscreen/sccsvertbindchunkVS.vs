//MADE BY STEVE CHASSÉ AKA NINEKORN AKA 9


cbuffer MatrixBuffer :register(b0)
{
	float4x4 world;
	float4x4 view;
	float4x4 proj;
};

//cbuffer MatrixBuffer :register(b1)
//{
//	int mapper[][]
//};

struct VertexInputType
{   
	float4 position : POSITION0;
	float4 color : COLOR0; //byte map index xyz and w for typeofface 0 to 5
	float3 normal : NORMAL0;
	float paddingvert0 : PSIZE0;	//instance width
	float2 tex : TEXCOORD0;
	float paddingvert1 : PSIZE1;	//instance height
	float paddingvert2 : PSIZE2;	//instance depth
	float4 instancePosition : POSITION1;
	float4 instanceRadRotFORWARD : POSITION2;
	float4 instanceRadRotRIGHT : POSITION3;
	float4 instanceRadRotUP : POSITION4;
	float4 mapmatrix0 : POSITION5;
	float4 mapmatrix1 : POSITION6;
	float4 mapmatrix2 : POSITION7;
	float4 mapmatrix3 : POSITION8;
	float4 mapmatrix4 : POSITION9;
	float4 mapmatrix5 : POSITION10;
	float4 mapmatrix6 : POSITION11;
	float4 mapmatrix7 : POSITION12;
	float4 mapmatrix8 : POSITION13;
	float4 mapmatrix9 : POSITION14;
	float4 mapmatrix10 : POSITION15;
	float4 mapmatrix11 : POSITION16;
	float4 mapmatrix12 : POSITION17;
	float4 mapmatrix13 : POSITION18;
	float4 mapmatrix14 : POSITION19;
	float4 mapmatrix15 : POSITION20;

	/*float4 heightmapmat0 : POSITION21;
	float4 heightmapmat1 : POSITION22;
	float4 heightmapmat2 : POSITION23;
	float4 heightmapmat3 : POSITION24;*/

	/*int one : PSIZE3;	
	int oneTwo : PSIZE4;
	int two : PSIZE5;	
	int twoTwo : PSIZE6;	
	int three : PSIZE7;	
	int threeTwo : PSIZE8;	
	int four : PSIZE9;	
	int fourTwo : PSIZE10;*/
	int xindex : PSIZE3;	
	int yindex : PSIZE4;
};

struct PixelInputType
{
    float4 position : SV_POSITION;
	float4 color : COLOR0; //byte map index xyz and w for typeofface 0 to 5
	float3 normal : NORMAL0;
	float paddingvert0 : PSIZE0;	//instance width
	float2 tex : TEXCOORD0;
	float paddingvert1 : PSIZE1;	//instance height
	float paddingvert2 : PSIZE2;	//instance depth
	float4 instancePosition : POSITION1;
	float4 instanceRadRotFORWARD : POSITION2;
	float4 instanceRadRotRIGHT : POSITION3;
	float4 instanceRadRotUP : POSITION4;
	float4 mapmatrix0 : POSITION5;
	float4 mapmatrix1 : POSITION6;
	float4 mapmatrix2 : POSITION7;
	float4 mapmatrix3 : POSITION8;
	float4 mapmatrix4 : POSITION9;
	float4 mapmatrix5 : POSITION10;
	float4 mapmatrix6 : POSITION11;
	float4 mapmatrix7 : POSITION12;
	float4 mapmatrix8 : POSITION13;
	float4 mapmatrix9 : POSITION14;
	float4 mapmatrix10 : POSITION15;
	float4 mapmatrix11 : POSITION16;
	float4 mapmatrix12 : POSITION17;
	float4 mapmatrix13 : POSITION18;
	float4 mapmatrix14 : POSITION19;
	float4 mapmatrix15 : POSITION20;

	/*float4 heightmapmat0 : POSITION21;
	float4 heightmapmat1 : POSITION22;
	float4 heightmapmat2 : POSITION23;
	float4 heightmapmat3 : POSITION24;*/

	/*int one : PSIZE3;	
	int oneTwo : PSIZE4;
	int two : PSIZE5;	
	int twoTwo : PSIZE6;	
	int three : PSIZE7;	
	int threeTwo : PSIZE8;	
	int four : PSIZE9;	
	int fourTwo : PSIZE10;*/
	int xindex : PSIZE3;	
	int yindex : PSIZE4;
};

int lastbit(int number)
{
    number = number - (number >> 1 << 1);
    return number;
}

int nthbit(int number,int position)
{
    return lastbit(number >> position);
}


static int heightmapw = 8;
static int heightmaph = 8;
static int heightmapd = 1;

//float planeSize = 0.1f;
//static int mapWidth = 4;
//static int mapHeight = 4;
//static int mapDepth = 4;

static int tinyChunkWidth = 8; // 4 // 8
static int tinyChunkHeight = 8; // 4 // 5
static int tinyChunkDepth = 1; // 4 // 8 // 1
static const int maxfloatbytemaparraylength = 8; //4 // 8 
static const int maxfloatbytemaparraylengthfull = 9; //5 // 9
static float arrayOfDigits[maxfloatbytemaparraylength];
static float arrayOfDigitsFull[maxfloatbytemaparraylengthfull];

//int iVar[3] = {1,2,3};
//int someoptions[1] = {0};
static int somebytemap[1008];//  = {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};
static int somebytemapswtc[1008];// = {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};

//21 * 6 * 8 = 1008

static float initialmap = 51111.0;

static float4 mod_input_vertex_pos;

static float3 forwardDir;
static float3 rightDir;
static float3 upDir;

static float3 MOVINGPOINT;
static float3 vertPos;
static float diffX;
static float diffY;
static float diffZ;

//public int IsTransparent(int x, int y, int z)
//{
//    if ((x < 0) || (y < 0) || (z < 0) || (x >= tinyChunkWidth) || (y >= tinyChunkHeight) || (z >= tinyChunkDepth)) return 1;
//    {
//		if(map[x + tinyChunkWidth * (y + tinyChunkHeight * z)] == 0)
//		{
//
//		}
//        return map[x + tinyChunkWidth * (y + tinyChunkHeight * z)] == 0;
//    }
//}




//[maxvertexcount(96)] 
PixelInputType TextureVertexShader(VertexInputType input)
{ 
	PixelInputType output;
    input.position.w = 1.0f;

	float currentByte = 0.0;

	//IVE SET THE VERTEX UNINSTANCED COLOR TO HOLD THE INDEX LOCATION OF THE BYTE
	int x = int(input.color.x); 
	int y = int(input.color.y); 
	int z = int(input.color.z);
	//IVE SET THE VERTEX UNINSTANCED COLOR TO HOLD THE INDEX LOCATION OF THE BYTE

	int facetype = int(input.color.w);

	int currentMapData;
	
	//4*4*4 = 64 voxels per chunk max
	//8*8*8 = 512 voxels per chunk max

	int currentIndex = x + tinyChunkWidth * (y + tinyChunkHeight * z); // x + tinyChunkWidth * (y + (tinyChunkHeight * z));
	int someOtherIndex = currentIndex;

	//0-4-1-5-2-6-3-7
	//8-12-9-13-10-14-11-15
	//16-20-17-21-18-22-19-23
	//24-28-25-29-26-30-27-31
	//32-36-33-37-34-38-35-39
	//40-44-41-45-42-46-43-47
	//48-52-49-53-50-54-51-55
	//56-60-57-61-58-62-59-63  
	
	float selectablevectorfloat = 0;
    int index = currentIndex;
    int maxv = tinyChunkWidth * tinyChunkHeight * tinyChunkDepth;
    int somemaxvecdigit = tinyChunkWidth;
    int somecountermul = 0;
    int somec = 0;
	

	/*
    for (int t = 0; t <= currentIndex; t++) //
    {
        if (somec == somemaxvecdigit)
        {
            somecountermul++;
            somec = 0;
        }
		//if (somecountermul * somemaxvecdigit >= maxv) // >=?? why not only >
		//{
		//	break;
		//}

        somec++;
    }*/

	somecountermul = currentIndex / tinyChunkWidth;


	int heightmapmul = currentIndex / 1; // 4





	// 3 - 0 * 2 = 6 + 1 = 7
    // 7 - 4 = 3 * 2 = 6 + 0 = 6

	int swtc0 = 0;
	int swtc1 = 0;

    //int somemin = (somecountermul) * somemaxvecdigit; //63 * 8 == 504
    //int somemid = (somemaxvecdigit / 2 ) + somemin; //504 + 4 = 508
    //int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //8

	int somemin = (somecountermul) * somemaxvecdigit; //0 //8 * 8 = 64 //63 * 8 == 504
    int somemid = round(somemaxvecdigit / 2 ) + somemin; // 4 //504 + 4 = 508
	int somemax = (somemaxvecdigit) + somemin; //504 + 8 = 512

    //int somemax = ((somecountermul) * somemaxvecdigit); //8

    if(currentIndex >= somemin && currentIndex <= somemid - 1) // 0 // 3
    {
		//511010110
        someOtherIndex = 1 + (((somemid - 1) - currentIndex) * 2);
		//index 0 => 1 + (3 - 0) * 2 = 7
		//index 1 => 1 + (3 - 1) * 2 = 5
		//index 2 => 1 + (3 - 2) * 2 = 3
		//index 3 => 1 + (3 - 3) * 2 = 1

		/*if(someOtherIndex == 7)
		{
			someOtherIndex = 0;
		}			
		else if(someOtherIndex == 5)
		{
			someOtherIndex = 2;
		}			
		else if(someOtherIndex == 3)
		{
			someOtherIndex = 4;
		}	
		else if(someOtherIndex == 1)
		{
			someOtherIndex = 6;
		}*/

		swtc0 = 1;
    }
    else if (currentIndex >= somemid && currentIndex <= somemax - 1) // 4 // 7
    {
		//511010110

        someOtherIndex = 0 + (((somemax - 1) - currentIndex) * 2);  
		//index 4 => 0 + (7 - 4) * 2 = 6
		//index 5 => 0 + (7 - 5) * 2 = 4
		//index 6 => 0 + (7 - 6) * 2 = 2
		//index 7 => 0 + (7 - 7) * 2 = 0

		//index 7 => 0 + (512-1 - 508) * 2 = 0

		/*if(someOtherIndex == 6)
		{
			someOtherIndex = 1;
		}			
		else if(someOtherIndex == 4)
		{
			someOtherIndex = 3;
		}			
		else if(someOtherIndex == 2)
		{
			someOtherIndex = 5;
		}	
		else if(someOtherIndex == 0)
		{
			someOtherIndex = 7;
		}*/
		swtc1 = 1;
    }




	float4 someheightmapvalue;

	if(heightmapmul == 0)
	{
		someheightmapvalue = input.heightmapmat0.x;
	}
	else if(heightmapmul == 1)
	{
		someheightmapvalue = input.heightmapmat0.y;
	}
	else if(heightmapmul == 2)
	{
		someheightmapvalue = input.heightmapmat0.z;
	}
	else if(heightmapmul == 3)
	{	
		someheightmapvalue = input.heightmapmat0.w;
	}
	else if(heightmapmul == 4)
	{	
		someheightmapvalue = input.heightmapmat1.x;
	}
	else if(heightmapmul == 5)
	{	
		someheightmapvalue = input.heightmapmat1.y;
	}
	else if(heightmapmul == 6)
	{	
		someheightmapvalue = input.heightmapmat1.z;
	}
	else if(heightmapmul == 7)
	{	
		someheightmapvalue = input.heightmapmat1.w;
	}
	else if(heightmapmul == 8)
	{	
		someheightmapvalue = input.heightmapmat2.x;
	}
	else if(heightmapmul == 9)
	{	
		someheightmapvalue = input.heightmapmat2.y;
	}
	else if(heightmapmul == 10)
	{	
		someheightmapvalue = input.heightmapmat2.z;
	}
	else if(heightmapmul == 11)
	{	
		someheightmapvalue = input.heightmapmat2.w;
	}
	else if(heightmapmul == 12)
	{	
		someheightmapvalue = input.heightmapmat3.x;
	}
	else if(heightmapmul == 13)
	{	
		someheightmapvalue = input.heightmapmat3.y;
	}
	else if(heightmapmul == 14)
	{	
		someheightmapvalue = input.heightmapmat3.z;
	}
	else if(heightmapmul == 15)
	{	
		someheightmapvalue = input.heightmapmat3.w;
	}



	/*
	if(heightmapmul == 0)
	{
		someheightmapvalue = input.heightmapmat0.x;
	}
	else if(heightmapmul == 1)
	{
		someheightmapvalue = input.heightmapmat1.y;
	}
	else if(heightmapmul == 2)
	{
		someheightmapvalue = input.heightmapmat2.z;
	}
	else if(heightmapmul == 3)
	{	
		someheightmapvalue = input.heightmapmat3.w;
	}
	else if(heightmapmul == 4)
	{	
		someheightmapvalue = input.heightmapmat1.x;
	}
	else if(heightmapmul == 5)
	{	
		someheightmapvalue = input.heightmapmat0.y;
	}
	else if(heightmapmul == 6)
	{	
		someheightmapvalue = input.heightmapmat3.z;
	}
	else if(heightmapmul == 7)
	{	
		someheightmapvalue = input.heightmapmat2.w;
	}
	else if(heightmapmul == 8)
	{	
		someheightmapvalue = input.heightmapmat3.x;
	}
	else if(heightmapmul == 9)
	{	
		someheightmapvalue = input.heightmapmat2.y;
	}
	else if(heightmapmul == 10)
	{	
		someheightmapvalue = input.heightmapmat0.z;
	}
	else if(heightmapmul == 11)
	{	
		someheightmapvalue = input.heightmapmat1.w;
	}
	else if(heightmapmul == 12)
	{	
		someheightmapvalue = input.heightmapmat2.x;
	}
	else if(heightmapmul == 13)
	{	
		someheightmapvalue = input.heightmapmat3.y;
	}
	else if(heightmapmul == 14)
	{	
		someheightmapvalue = input.heightmapmat1.z;
	}
	else if(heightmapmul == 15)
	{	
		someheightmapvalue = input.heightmapmat0.w;
	}*/














	else if(heightmapmul == 16)
	{
		someheightmapvalue = input.heightmapmat0.x;
	}
	else if(heightmapmul == 17)
	{
		someheightmapvalue = input.heightmapmat0.y;
	}
	else if(heightmapmul == 18)
	{
		someheightmapvalue = input.heightmapmat0.z;
	}
	else if(heightmapmul == 19)
	{	
		someheightmapvalue = input.heightmapmat0.w;
	}
	else if(heightmapmul == 20)
	{	
		someheightmapvalue = input.heightmapmat1.x;
	}
	else if(heightmapmul == 21)
	{	
		someheightmapvalue = input.heightmapmat1.y;
	}
	else if(heightmapmul == 22)
	{	
		someheightmapvalue = input.heightmapmat1.z;
	}
	else if(heightmapmul == 23)
	{	
		someheightmapvalue = input.heightmapmat1.w;
	}
	else if(heightmapmul == 24)
	{	
		someheightmapvalue = input.heightmapmat2.x;
	}
	else if(heightmapmul == 25)
	{	
		someheightmapvalue = input.heightmapmat2.y;
	}
	else if(heightmapmul == 26)
	{	
		someheightmapvalue = input.heightmapmat2.z;
	}
	else if(heightmapmul == 27)
	{	
		someheightmapvalue = input.heightmapmat2.w;
	}
	else if(heightmapmul == 28)
	{	
		someheightmapvalue = input.heightmapmat3.x;
	}
	else if(heightmapmul == 29)
	{	
		someheightmapvalue = input.heightmapmat3.y;
	}
	else if(heightmapmul == 30)
	{	
		someheightmapvalue = input.heightmapmat3.z;
	}
	else if(heightmapmul == 31)
	{	
		someheightmapvalue = input.heightmapmat3.w;
	}


	else if(heightmapmul == 32)
	{
		someheightmapvalue = input.heightmapmat0.x;
	}
	else if(heightmapmul == 33)
	{
		someheightmapvalue = input.heightmapmat0.y;
	}
	else if(heightmapmul == 34)
	{
		someheightmapvalue = input.heightmapmat0.z;
	}
	else if(heightmapmul == 35)
	{	
		someheightmapvalue = input.heightmapmat0.w;
	}
	else if(heightmapmul == 36)
	{	
		someheightmapvalue = input.heightmapmat1.x;
	}
	else if(heightmapmul == 37)
	{	
		someheightmapvalue = input.heightmapmat1.y;
	}
	else if(heightmapmul == 38)
	{	
		someheightmapvalue = input.heightmapmat1.z;
	}
	else if(heightmapmul == 39)
	{	
		someheightmapvalue = input.heightmapmat1.w;
	}
	else if(heightmapmul == 40)
	{	
		someheightmapvalue = input.heightmapmat2.x;
	}
	else if(heightmapmul == 41)
	{	
		someheightmapvalue = input.heightmapmat2.y;
	}
	else if(heightmapmul == 42)
	{	
		someheightmapvalue = input.heightmapmat2.z;
	}
	else if(heightmapmul == 43)
	{	
		someheightmapvalue = input.heightmapmat2.w;
	}
	else if(heightmapmul == 44)
	{	
		someheightmapvalue = input.heightmapmat3.x;
	}
	else if(heightmapmul == 45)
	{	
		someheightmapvalue = input.heightmapmat3.y;
	}
	else if(heightmapmul == 46)
	{	
		someheightmapvalue = input.heightmapmat3.z;
	}
	else if(heightmapmul == 47)
	{	
		someheightmapvalue = input.heightmapmat3.w;
	}



	else if(heightmapmul == 48)
	{
		someheightmapvalue = input.heightmapmat0.x;
	}
	else if(heightmapmul == 49)
	{
		someheightmapvalue = input.heightmapmat0.y;
	}
	else if(heightmapmul == 50)
	{
		someheightmapvalue = input.heightmapmat0.z;
	}
	else if(heightmapmul == 51)
	{	
		someheightmapvalue = input.heightmapmat0.w;
	}
	else if(heightmapmul == 52)
	{	
		someheightmapvalue = input.heightmapmat1.x;
	}
	else if(heightmapmul == 53)
	{	
		someheightmapvalue = input.heightmapmat1.y;
	}
	else if(heightmapmul == 54)
	{	
		someheightmapvalue = input.heightmapmat1.z;
	}
	else if(heightmapmul == 55)
	{	
		someheightmapvalue = input.heightmapmat1.w;
	}
	else if(heightmapmul == 56)
	{	
		someheightmapvalue = input.heightmapmat2.x;
	}
	else if(heightmapmul == 57)
	{	
		someheightmapvalue = input.heightmapmat2.y;
	}
	else if(heightmapmul == 58)
	{	
		someheightmapvalue = input.heightmapmat2.z;
	}
	else if(heightmapmul == 59)
	{	
		someheightmapvalue = input.heightmapmat2.w;
	}
	else if(heightmapmul == 60)
	{	
		someheightmapvalue = input.heightmapmat3.x;
	}
	else if(heightmapmul == 61)
	{	
		someheightmapvalue = input.heightmapmat3.y;
	}
	else if(heightmapmul == 62)
	{	
		someheightmapvalue = input.heightmapmat3.z;
	}
	else if(heightmapmul == 63)
	{	
		someheightmapvalue = input.heightmapmat3.w;
	}







	/*
	if (currentIndex >= 0 && currentIndex <= 3)
    {

        currentMapData =input.mapmatrix0.x;

        if (currentIndex >= 0 && currentIndex <= 1)
        {
			//1 + ((1 - 0) * 2) = 3
			//1 + ((1 - 1) * 2) = 1

            int somemax = 1;
            someOtherIndex = 1 + ((somemax - someOtherIndex) * 2);
        }
        else if (currentIndex >= 2 && currentIndex <= 3)
        {
			//0 + ((3 - 2) * 2) = 2
			//0 + ((3 - 3) * 2) = 0

            int somemax = 3;
            someOtherIndex = 0 + ((somemax - someOtherIndex) * 2);
        }
    }*/






	//someOtherIndex = currentIndex % 8;









    
	if(somecountermul == 0)
	{  
		currentMapData = input.mapmatrix0.x;
	}
	else if(somecountermul == 1)
	{
		currentMapData =input.mapmatrix0.y;
	}    
    else if(somecountermul == 2)
	{
		currentMapData =input.mapmatrix0.z;		
	}
	else if(somecountermul == 3)
	{
		currentMapData =input.mapmatrix0.w;
	}
	else if(somecountermul == 4)
	{
		currentMapData =input.mapmatrix1.x;
	}
	else if(somecountermul == 5)
	{
		currentMapData =input.mapmatrix1.y;
	     
	}
	else if(somecountermul == 6)
	{
		currentMapData =input.mapmatrix1.z;
	     
	}
	else if(somecountermul == 7)
	{
		currentMapData =input.mapmatrix1.w;
	     
	}
	else if(somecountermul == 8)
	{
		currentMapData =input.mapmatrix2.x;
	     
	}
	else if(somecountermul == 9)
	{
		currentMapData =input.mapmatrix2.y;
	}
	else if(somecountermul == 10)
	{
		currentMapData =input.mapmatrix2.z;

	}
	else if(somecountermul == 11)
	{
		currentMapData =input.mapmatrix2.w;
	}
	else if(somecountermul == 12)
	{
		currentMapData =input.mapmatrix3.x;

	}
	else if(somecountermul == 13)
	{
		currentMapData =input.mapmatrix3.y;

	}
	else if(somecountermul == 14)
	{
		currentMapData =input.mapmatrix3.z;
	
	}
	else if(somecountermul == 15)
	{
		currentMapData =input.mapmatrix3.w;

	}
	else if(somecountermul == 16)
	{  
		currentMapData =input.mapmatrix4.x;
	}
	else if(somecountermul == 17)
	{
		currentMapData =input.mapmatrix4.y;
	}
	else if(somecountermul == 18)
	{
		currentMapData =input.mapmatrix4.z;
	}
	else if(somecountermul == 19)
	{
		currentMapData =input.mapmatrix4.w;
	}
	else if(somecountermul == 20)
	{
		currentMapData =input.mapmatrix5.x;
	}
	else if(somecountermul == 21)
	{
		currentMapData =input.mapmatrix5.y;
	}
	else if(somecountermul == 22)
	{
		currentMapData =input.mapmatrix5.z;
	}
	else if(somecountermul == 23)
	{
		currentMapData =input.mapmatrix5.w;
	}
	else if(somecountermul == 24)
	{
		currentMapData =input.mapmatrix6.x;
	}
	else if(somecountermul == 25)
	{
		currentMapData =input.mapmatrix6.y;
	}
	else if(somecountermul == 26)
	{
		currentMapData =input.mapmatrix6.z;
	}
	else if(somecountermul == 27)
	{
		currentMapData =input.mapmatrix6.w;
	}
	else if(somecountermul == 28)
	{
		currentMapData =input.mapmatrix7.x;
	}
	else if(somecountermul == 29)
	{
		currentMapData =input.mapmatrix7.y;
	}
	else if(somecountermul == 30)
	{
		currentMapData =input.mapmatrix7.z;
	}
	else if(somecountermul == 31)
	{
		currentMapData =input.mapmatrix7.w;
	}
	else if(somecountermul == 32)
	{  
		currentMapData =input.mapmatrix8.x;
	}
	else if(somecountermul == 33)
	{
		currentMapData =input.mapmatrix8.y;
	}
	else if(somecountermul == 34)
	{
		currentMapData =input.mapmatrix8.z;
	}
	else if(somecountermul == 35)
	{
		currentMapData =input.mapmatrix8.w;
	}
	else if(somecountermul == 36)
	{
		currentMapData =input.mapmatrix9.x;
	}
	else if(somecountermul == 37)
	{
		currentMapData =input.mapmatrix9.y;
	}
	else if(somecountermul == 38)
	{
		currentMapData =input.mapmatrix9.z;
	}
	else if(somecountermul == 39)
	{
		currentMapData =input.mapmatrix9.w;
	}
	else if(somecountermul == 40)
	{
		currentMapData =input.mapmatrix10.x;
	}
	else if(somecountermul == 41)
	{
		currentMapData =input.mapmatrix10.y;
	}
	else if(somecountermul == 42)
	{
		currentMapData =input.mapmatrix10.z;
	}
	else if(somecountermul == 43)
	{
		currentMapData =input.mapmatrix10.w;
	}
	else if(somecountermul == 44)
	{
		currentMapData =input.mapmatrix11.x;
	}
	else if(somecountermul == 45)
	{
		currentMapData =input.mapmatrix11.y;
	}
	else if(somecountermul == 46)
	{
		currentMapData =input.mapmatrix11.z;
	}
	else if(somecountermul == 47)
	{
		currentMapData =input.mapmatrix11.w;
	}
	else if(somecountermul == 48)
	{  
		currentMapData =input.mapmatrix12.x;
	}
	else if(somecountermul == 49)
	{
		currentMapData =input.mapmatrix12.y;
	}
	else if(somecountermul == 50)
	{
		currentMapData =input.mapmatrix12.z;
	}
	else if(somecountermul == 51)
	{
		currentMapData =input.mapmatrix12.w;
	}
	else if(somecountermul == 52)
	{
		currentMapData =input.mapmatrix13.x;
	}
	else if(somecountermul == 53)
	{
		currentMapData =input.mapmatrix13.y;
	}
	else if(somecountermul == 54)
	{
		currentMapData =input.mapmatrix13.z;
	}
	else if(somecountermul == 55)
	{
		currentMapData =input.mapmatrix13.w;
	}
	else if(somecountermul == 56)
	{
		currentMapData =input.mapmatrix14.x;
	}
	else if(somecountermul == 57)
	{
		currentMapData =input.mapmatrix14.y;
	}
	else if(somecountermul == 58)
	{
		currentMapData =input.mapmatrix14.z;
	}
	else if(somecountermul == 59)
	{
		currentMapData =input.mapmatrix14.w;
	}
	else if(somecountermul == 60)
	{
		currentMapData =input.mapmatrix15.x;
	}
	else if(somecountermul == 61)
	{
		currentMapData =input.mapmatrix15.y;
	}
	else if(somecountermul == 62)
	{
		currentMapData =input.mapmatrix15.z;
	}
	else if(somecountermul == 63) // 
	{
		currentMapData = input.mapmatrix15.w;
	}


		
	//x-5-3-1-x-x-2-0
	//7-5-3-1-6-4-2-0
	//arrayOfDigits[5] = 0.0;

	//int someotherindexinverted = someOtherIndex; //(tinyChunkWidth -1) - someOtherIndex;


	//0-4-1-5-2-6-3-7
	//8-12-9-13-10-14-11-15
	//16-20-17-21-18-22-19-23
	//24-28-25-29-26-30-27-31
	//32-36-33-37-34-38-35-39
	//40-44-41-45-42-46-43-47
	//48-52-49-53-50-54-51-55
	//56-60-57-61-58-62-59-63  

	//float someremains = (initialmap - currentMapData) * someothermul1; //51111 - 50111 = 1000

	float someothermul0 = 10.0;
	float someothermul1 = 1.0;
    
	/*if (someotherindexinverted == 0)
	{
		someothermul0 = 100.0;
		someothermul1 = 0.1;
	}
	else if (someotherindexinverted == 1)
	{
		someothermul0 = 1000.0;
		someothermul1 = 0.01;
	}
	else if (someotherindexinverted == 2)
	{
		someothermul0 = 10000.0;
		someothermul1 = 0.001;
	}
	else if (someotherindexinverted == 3)
	{
		someothermul0 = 100000.0;
		someothermul1 = 0.0001;
	}
	else if (someotherindexinverted == 4)
	{
		someothermul0 = 1000000.0;
		someothermul1 = 0.00001;
	}
	else if (someotherindexinverted == 5)
	{
		someothermul0 = 10000000.0;
		someothermul1 = 0.000001;
	}
	else if (someotherindexinverted == 6)
	{
		someothermul0 = 100000000.0;
		someothermul1 = 0.0000001;
	}
	else if (someotherindexinverted == 7)
	{
		someothermul0 = 1000000000.0;
		someothermul1 = 0.00000001;
	}*/



	/*else if (someotherindexinverted == 8)
	{
		someothermul0 = 10000000000.0;
		someothermul1 = 0.000000001;
	}
	else if (someotherindexinverted == 9)
	{
		someothermul0 = 100000000000.0;
		someothermul1 = 0.0000000001;
	}
	else if (someotherindexinverted == 10)
	{
		someothermul0 = 1000000000000.0;
		someothermul1 = 0.00000000001;
	}
	else if (someotherindexinverted == 11)
	{
		someothermul0 = 10000000000000.0;
		someothermul1 = 0.000000000001;
	}
	else if (someotherindexinverted == 12)
	{
		someothermul0 = 100000000000000.0;
		someothermul1 = 0.0000000000001;
	}
	else if (someotherindexinverted == 13)
	{
		someothermul0 = 1000000000000000.0;
		someothermul1 = 0.00000000000001;
	}
	else if (someotherindexinverted == 14)
	{
		someothermul0 = 10000000000000000.0;
		someothermul1 = 0.000000000000001;
	}
	else if (someotherindexinverted == 15)
	{
		someothermul0 = 100000000000000000.0;
		someothermul1 = 0.0000000000000001;
	}*/


	if (someOtherIndex == 0)
	{
		someothermul0 = 100.0;
		someothermul1 = 0.1;
	}
	else if (someOtherIndex == 1)
	{
		someothermul0 = 1000.0;
		someothermul1 = 0.01;
	}
	else if (someOtherIndex == 2)
	{
		someothermul0 = 10000.0;
		someothermul1 = 0.001;
	}
	else if (someOtherIndex == 3)
	{
		someothermul0 = 100000.0;
		someothermul1 = 0.0001;
	}
	else if (someOtherIndex == 4)
	{
		someothermul0 = 1000000.0;
		someothermul1 = 0.00001;
	}
	else if (someOtherIndex == 5)
	{
		someothermul0 = 10000000.0;
		someothermul1 = 0.000001;
	}
	else if (someOtherIndex == 6)
	{
		someothermul0 = 100000000.0;
		someothermul1 = 0.0000001;
	}
	else if (someOtherIndex == 7)
	{
		someothermul0 = 1000000000.0;
		someothermul1 = 0.00000001;
	}




	float somemap = currentMapData;
	float tempsomemap;



	//CHUNK 4X4X4
	//0-4-1-5-2-6-3-7
	//8-12-9-13-10-14-11-15
	//16-20-17-21-18-22-19-23
	//24-28-25-29-26-30-27-31
	//32-36-33-37-34-38-35-39
	//40-44-41-45-42-46-43-47
	//48-52-49-53-50-54-51-55
	//56-60-57-61-58-62-59-63  
		
	//x-5-3-1-x-x-2-0
	//7-5-3-1-6-4-2-0
	//arrayOfDigits[5] = 0.0;








	/*
	if(someOtherIndex == 0 || someOtherIndex == 1)
	{

		if(someOtherIndex == 0 )
		{
			float someval0 = (currentMapData / 10);
			float someval1 = round(currentMapData / 10);
			float someval2 = (someval0 * 10);

			if(someval2 + 1 == currentMapData)
			{
				arrayOfDigits[0] = 1.0;
			}
			else
			{
				arrayOfDigits[0] = 0.0;
			}

			float someval00 = (currentMapData/100); //5111111.11
			float someval11 = round(currentMapData/100); ////511111100.0
			float someval22 = (someval00 * 100); // 511111110.0

			//int somevalint = asint(currentMapData/100)*100;

			if(someval22 + 11 == currentMapData || someval22 + 10 == currentMapData)
			{
				arrayOfDigits[1] = 1.0;
			}
			else
			{
				arrayOfDigits[1] = 0.0;
			}
			for (int i = 0; i < maxfloatbytemaparraylength; i++)
			{
				tempsomemap = somemap;
				somemap = (somemap * 0.1);

				if(i >= 2)
				{
					float somevalue = floor((float)(int)((somemap - (int)somemap) * someothermul0)) * someothermul1;
					somevalue = somevalue - (floor(somevalue * 0.1) * 10);
					arrayOfDigits[i] = somevalue;
				}
			}
		}
		else if(someOtherIndex == 1)
		{

			float someval0 = (currentMapData / 10);
			float someval1 = round(currentMapData / 10);
			float someval2 = (someval0 * 10);

			if(someval2 + 1 == currentMapData)
			{
				arrayOfDigits[0] = 1.0;
			}
			else
			{
				arrayOfDigits[0] = 0.0;
			}

			float someval00 = (currentMapData/100); //5111111.11
			float someval11 = round(currentMapData/100); ////511111100.0
			float someval22 = (someval00 * 100); // 511111110.0

			//int somevalint = asint(currentMapData/100)*100;

			if(someval22 + 11 == currentMapData || someval22 + 10 == currentMapData)
			{
				arrayOfDigits[1] = 1.0;
			}
			else
			{
				arrayOfDigits[1] = 0.0;
			}


			for (int i = 0; i < maxfloatbytemaparraylength; i++)
			{
				tempsomemap = somemap;
				somemap = (somemap * 0.1);

				if(i >= 2)
				{
					float somevalue = floor((float)(int)((somemap - (int)somemap) * someothermul0)) * someothermul1;
					somevalue = somevalue - (floor(somevalue * 0.1) * 10);
					arrayOfDigits[i] = somevalue;
				}
		
			}
		}
	}
	else
	{
		for (int i = 0; i < maxfloatbytemaparraylength; i++)
		{
			tempsomemap = somemap;
			somemap = (somemap * 0.1);
			float somevalue = floor((float)(int)((somemap - (int)somemap) * someothermul0)) * someothermul1;
			somevalue = somevalue - (floor(somevalue * 0.1) * 10);
			arrayOfDigits[i] = somevalue;
		}
	}*/


	//x-5-3-1-x-x-2-0
	//7-5-3-1-6-4-2-0



	
	if(tinyChunkWidth == 8) // && tinyChunkHeight == 8 && tinyChunkDepth == 8
	{
		//my old method
		int testera = 0;
		int substract = 0;
		int before0 = 0;
		int theByte = 0;
	
		if(someOtherIndex == 0) // problem gotta use a different approach for the index 0 and 2
		{

			float someval0 = (float)(int)(currentMapData / 10); // 511111111 / 10 = 51111111.1
			//float someval1 = round(currentMapData / 10); 
			float someval2 = (someval0 * 10); //// 511111111 / 10 = 51111111.1


			//WORKS
			
			if(someval2 + 1 == currentMapData)
			{
				arrayOfDigits[0] = 1.0;
			}
			else
			{
				arrayOfDigits[0] = 0.0;
			}


		}
		else if(someOtherIndex == 1)
		{
			float someData0 = currentMapData;
		
			for(int i = 0;i < 4;i++)
			{
				someData0 = int(someData0 * 0.1f);
			}

			before0 = int(trunc(someData0));
			testera = before0 >> 1 << 1;
			theByte = before0 - testera;
			arrayOfDigits[someOtherIndex] = theByte;
		}
		else if(someOtherIndex == 2) // problem gotta use a different approach for the index 0 and 2
		{	
		
			float someval0 = (float)(int)(currentMapData / 100); // 511111111 / 10 = 51111111.1
			//float someval1 = round(currentMapData / 100); 
			float someval2 = (someval0 * 100); //// 511111111 / 10 = 51111111.1


			//WORKS
			
			if(someval2 + 11 == currentMapData || someval2 + 10 == currentMapData)
			{
				arrayOfDigits[2] = 1.0;
			}
			else
			{
				arrayOfDigits[2] = 0.0;
			}

		}
		else if(someOtherIndex == 3)
		{
			float someData0 = currentMapData;
		
			for(int i = 0;i < 5;i++)
			{
				someData0 = int(someData0 * 0.1f);
			}

			before0 = int(trunc(someData0));
			testera = before0 >> 1 << 1;
			theByte = before0 - testera;
			arrayOfDigits[someOtherIndex] = theByte;
		}
		else if(someOtherIndex == 4)
		{
			float someData0 = currentMapData;
		
			for(int i = 0;i < 2;i++)
			{
				someData0 = int(someData0 * 0.1f);
			}

			before0 = int(trunc(someData0));
			testera = before0 >> 1 << 1;
			theByte = before0 - testera;
			arrayOfDigits[someOtherIndex] = theByte;
		}
		else if(someOtherIndex == 5 )
		{
			float someData0 = currentMapData;
		
			for(int i = 0;i < 6;i++)
			{
				someData0 = int(someData0 * 0.1f);
			}

			before0 = int(trunc(someData0));
			testera = before0 >> 1 << 1;
			theByte = before0 - testera;
			arrayOfDigits[someOtherIndex] = theByte;
		}
		else if(someOtherIndex == 6)
		{
			float someData0 = currentMapData;
		
			for(int i = 0;i < 3;i++)
			{
				someData0 = int(someData0 * 0.1f);
			}

			before0 = int(trunc(someData0));
			testera = before0 >> 1 << 1;
			theByte = before0 - testera;
			arrayOfDigits[someOtherIndex] = theByte;
		}
		else if(someOtherIndex == 7)
		{
			//7-5-3-1-6-4-2-0
			float someData0 = currentMapData;
		
			for(int i = 0;i < 7;i++)
			{
				someData0 = int(someData0 * 0.1f);
			}

			before0 = int(trunc(someData0));
			testera = before0 >> 1 << 1;
			theByte = before0 - testera;
			arrayOfDigits[someOtherIndex] = theByte;
		}

	}
	else if(tinyChunkWidth == 4) // && tinyChunkHeight == 4 && tinyChunkDepth == 4
	{
		for (int i = 0; i < maxfloatbytemaparraylength; i++)
		{
			tempsomemap = somemap;
			somemap = (somemap * 0.1);
			float somevalue = floor((float)(int)((somemap - (int)somemap) * someothermul0)) * someothermul1;
			somevalue = somevalue - (floor(somevalue * 0.1) * 10);
			arrayOfDigits[i] = somevalue;
		}
	}
	else if(tinyChunkWidth == 16) // && tinyChunkHeight == 4 && tinyChunkDepth == 4
	{
		//my old method
		int testera = 0;
		int substract = 0;
		int before0 = 0;
		int theByte = 0;
	
		if(someOtherIndex == 0) // problem gotta use a different approach for the index 0 and 2
		{

			float someval0 = (float)(int)(currentMapData / 10); // 511111111 / 10 = 51111111.1
			//float someval1 = round(currentMapData / 10); 
			float someval2 = (someval0 * 10); //// 511111111 / 10 = 51111111.1


			//WORKS
			
			if(someval2 + 1 == currentMapData)
			{
				arrayOfDigits[0] = 1.0;
			}
			else
			{
				arrayOfDigits[0] = 0.0;
			}


		}
		else if(someOtherIndex == 1)
		{
			float someData0 = currentMapData;
		
			for(int i = 0;i < 4;i++)
			{
				someData0 = int(someData0 * 0.1f);
			}

			before0 = int(trunc(someData0));
			testera = before0 >> 1 << 1;
			theByte = before0 - testera;
			arrayOfDigits[someOtherIndex] = theByte;
		}
		else if(someOtherIndex == 2) // problem gotta use a different approach for the index 0 and 2
		{	
		
			float someval0 = (float)(int)(currentMapData / 100); // 511111111 / 10 = 51111111.1
			//float someval1 = round(currentMapData / 100); 
			float someval2 = (someval0 * 100); //// 511111111 / 10 = 51111111.1


			//WORKS
			
			if(someval2 + 11 == currentMapData || someval2 + 10 == currentMapData)
			{
				arrayOfDigits[2] = 1.0;
			}
			else
			{
				arrayOfDigits[2] = 0.0;
			}

		}
		else if(someOtherIndex == 3)
		{
			float someData0 = currentMapData;
		
			for(int i = 0;i < 5;i++)
			{
				someData0 = int(someData0 * 0.1f);
			}

			before0 = int(trunc(someData0));
			testera = before0 >> 1 << 1;
			theByte = before0 - testera;
			arrayOfDigits[someOtherIndex] = theByte;
		}
		else if(someOtherIndex == 4)
		{
			float someData0 = currentMapData;
		
			for(int i = 0;i < 2;i++)
			{
				someData0 = int(someData0 * 0.1f);
			}

			before0 = int(trunc(someData0));
			testera = before0 >> 1 << 1;
			theByte = before0 - testera;
			arrayOfDigits[someOtherIndex] = theByte;
		}
		else if(someOtherIndex == 5 )
		{
			float someData0 = currentMapData;
		
			for(int i = 0;i < 6;i++)
			{
				someData0 = int(someData0 * 0.1f);
			}

			before0 = int(trunc(someData0));
			testera = before0 >> 1 << 1;
			theByte = before0 - testera;
			arrayOfDigits[someOtherIndex] = theByte;
		}
		else if(someOtherIndex == 6)
		{
			float someData0 = currentMapData;
		
			for(int i = 0;i < 3;i++)
			{
				someData0 = int(someData0 * 0.1f);
			}

			before0 = int(trunc(someData0));
			testera = before0 >> 1 << 1;
			theByte = before0 - testera;
			arrayOfDigits[someOtherIndex] = theByte;
		}
		else if(someOtherIndex == 7)
		{
			//7-5-3-1-6-4-2-0
			float someData0 = currentMapData;
		
			for(int i = 0;i < 7;i++)
			{
				someData0 = int(someData0 * 0.1f);
			}

			before0 = int(trunc(someData0));
			testera = before0 >> 1 << 1;
			theByte = before0 - testera;
			arrayOfDigits[someOtherIndex] = theByte;
		}
	}



	/*else if(someOtherIndex == 8)
	{
		//7-5-3-1-6-4-2-0
		float someData0 = currentMapData;
		
		for(int i = 0;i < 8;i++)
		{
			someData0 = int(someData0 * 0.1f);
		}

		before0 = int(trunc(someData0));
		testera = before0 >> 1 << 1;
		theByte = before0 - testera;
		arrayOfDigits[someOtherIndex] = theByte;
	}
	else if(someOtherIndex == 9)
	{
		//7-5-3-1-6-4-2-0
		float someData0 = currentMapData;
		
		for(int i = 0;i < 9;i++)
		{
			someData0 = int(someData0 * 0.1f);
		}

		before0 = int(trunc(someData0));
		testera = before0 >> 1 << 1;
		theByte = before0 - testera;
		arrayOfDigits[someOtherIndex] = theByte;
	}*/



	/*
	if(someOtherIndex == 8)
	{
		arrayOfDigits[0] = 1.0;
	}*/
	/*else if(someOtherIndex == 9)
	{
		arrayOfDigits[someOtherIndex] = 1.0;
	}*/









	/*
	//my old method
	int testera = 0;
	int substract = 0;
	int before0 = 0;
	int theByte = 0;

	if(someOtherIndex == 0)
	{
		testera = currentMapData >> 1 << 1;
		theByte = currentMapData - testera;
		arrayOfDigits[someOtherIndex] = theByte;
	}
	else
	{
		float someData0 = currentMapData;
		
		for(int i = 0;i < someOtherIndex;i++)
		{
			someData0 = int(someData0 * 0.1f);
		}

		before0 = int(trunc(someData0));
		testera = before0 >> 1 << 1;
		theByte = before0 - testera;
		arrayOfDigits[someOtherIndex] = theByte;
	}*/
	

















	/*
	//my old method
	int testera = 0;
	int substract = 0;
	int before0 = 0;
	int theByte = 0;

	if(someOtherIndex == 0)
	{
		testera = currentMapData >> 1 << 1;
		theByte = currentMapData - testera;
		arrayOfDigits[someOtherIndex] = theByte;
	}
	else
	{
		float someData0 = currentMapData;
		
		for(int i = 0;i < someOtherIndex;i++)
		{
			someData0 = int(someData0 * 0.1f);
		}

		before0 = int(trunc(someData0));
		testera = before0 >> 1 << 1;
		theByte = before0 - testera;
		arrayOfDigits[someOtherIndex] = theByte;
	}
	*/

	//THE WAY THAT THE RENDERED CHUNK'S VOXEL INDEXES ARE BEING DESTROYED.
	//x-5-3-1-x-x-2-0
	//7-5-3-1-6-4-2-0
	








	/*for (int i = 0; i < maxfloatbytemaparraylength; i++)
	{
		tempsomemap = somemap;
		somemap = (somemap * 0.1);
		float somevalue = floor((float)(int)((somemap - (int)somemap) * someothermul0)) * someothermul1;
		somevalue = somevalue - (floor(somevalue * 0.1) * 10);
		arrayOfDigits[i] = somevalue;
	}*/

	/*
	for (int i = 0; i < maxfloatbytemaparraylength; i++)
	{
		if(i == 0)
		{
			tempsomemap = somemap;
			//somemap = (somemap * 0.1);
			float somevalue = floor((float)(int)((somemap - (int)somemap) * someothermul0)) * someothermul1;
			somevalue = somevalue - (floor(somevalue * 0.1) * 10);
			arrayOfDigits[i] = somevalue;
		}
		else {
			tempsomemap = somemap;
			somemap = (somemap * 0.1);
			float somevalue = floor((float)(int)((somemap - (int)somemap) * someothermul0)) * someothermul1;
			somevalue = somevalue - (floor(somevalue * 0.1) * 10);
			arrayOfDigits[i] = somevalue;
		}
	}*/

	




	//0-4-1-5-2-6-3-7
	//8-12-9-13-10-14-11-15
	//16-20-17-21-18-22-19-23
	//24-28-25-29-26-30-27-31
	//32-36-33-37-34-38-35-39
	//40-44-41-45-42-46-43-47
	//48-52-49-53-50-54-51-55
	//56-60-57-61-58-62-59-63	

	//0-1-2-3-4-5-6-7
	//indexplacement 0 => 1 + (3 - 0) * 2 = 7
	//indexplacement 1 => 1 + (3 - 1) * 2 = 5
	//indexplacement 2 => 1 + (3 - 2) * 2 = 3
	//indexplacement 3 => 1 + (3 - 3) * 2 = 1

	//indexplacement 4 => 0 + (7 - 4) * 2 = 6
	//indexplacement 5 => 0 + (7 - 5) * 2 = 4
	//indexplacement 6 => 0 + (7 - 6) * 2 = 2
	//indexplacement 7 => 0 + (7 - 7) * 2 = 0

















	


	if(swtc0 == 1)
	{
		
	}

	if(swtc1 == 1)
	{
		
	}
















	//float someotherindex1 = 3 - someOtherIndex;
	/*
	
	float somemap1 = currentMapData;// 51234;//51111 //currentMapData

	float tempsomemap1;
	float someotherbyte1;
                                                    
	for (int i = 0; i < maxfloatbytemaparraylengthfull; i++) //111111111 // 6th digit //someOtherIndex
	{
		tempsomemap1 = somemap1;
		somemap1 = (somemap1 * 0.1);

        //float somevalue1 = floor((float)(int)((somemap1 - (int)somemap1) * 1000000.0)) * 0.00001;
		float somevalue1 = floor((float)(int)((somemap1 - (int)somemap1) * 1000000000000.0)) * 0.00000000001;
		somevalue1 = somevalue1 - (floor(somevalue1 * 0.1) * 10);

		arrayOfDigitsFull[i] = somevalue1;
	}*/





	double somedesiredbyte = 0;





    
	/*float somemap1 = currentMapData;// 51234;//51111 //currentMapData

	float tempsomemap1;
	float someotherbyte1;
                                                    
	for (int i = 0; i < maxfloatbytemaparraylengthfull; i++) //111111111 // 6th digit //someOtherIndex
	{
		tempsomemap1 = somemap1;
		somemap1 = (somemap1 * 0.1);

		float somevalue1 = floor((float)(int)((somemap1 - (int)somemap1) * 1000000.0)) * 0.00001;
		somevalue1 = somevalue1 - (floor(somevalue1 * 0.1) * 10);

		arrayOfDigitsFull[i] = somevalue1;
	}

	float somedesiredbyte = 0;*/

	//5 full chunk cube all 1s for byte breaking when 1s becomes 0s (and for byte adding when byte 0s become 1s WIP)
	//4 full chunk cube all 0s for path tracing with path traced with bytes becoming 1s when the player moves around the invisible chunk.
	//3 full chunk cube all 0s for a way to visualize spatial location of objects in a 3d scene. // not working entirely i think. float checking soon
	//2 full chunk cube all 1s for byte breaking when 1s becomes 0s (and for byte adding when byte 0s become 1s WIP) - using random perlin WIP
	//1 WIP TRANSPARENCY GRID LIKE CHUNK WITH MY UPCOMING CODING CHALLENGE TO LEARN RASTERTEK C# TRANSPARENCY.

	/*if(round(arrayOfDigitsFull[maxfloatbytemaparraylengthfull -1]) == 5.0)
	{
		somedesiredbyte = 1.0;
	}
	else if(round(arrayOfDigitsFull[maxfloatbytemaparraylengthfull -1]) == 4.0)
	{
		somedesiredbyte = 0.0;
	}
	else if(round(arrayOfDigitsFull[maxfloatbytemaparraylengthfull -1]) == 3.0)
	{
		somedesiredbyte = 1.0;
	}
	else if(round(arrayOfDigitsFull[maxfloatbytemaparraylengthfull -1]) == 2.0)
	{
		somedesiredbyte = 1.0;
	}
	else if(round(arrayOfDigitsFull[maxfloatbytemaparraylengthfull -1]) == 1.0)
	{
		somedesiredbyte = 1.0;
	}*/

    somedesiredbyte = 1.0;

 

	//someOtherIndex = 7 - someOtherIndex;
	//theByte == int(somedesiredbyte)) // 



	
	//currentMapData == 511111111.0) //
	if(round(arrayOfDigits[someOtherIndex]) == somedesiredbyte) //1.0 // 0.0 //someOtherIndex >= 0 && someOtherIndex <= 7 ) // //theByte == somedesiredbyte) //  //round(theByte) == round(somedesiredbyte)) // 
	{
		input.position.w = 1.0f;

		//input.position.x = input.position.x * -1;

		mod_input_vertex_pos = input.position;

		mod_input_vertex_pos.x += input.instancePosition.x;
		mod_input_vertex_pos.y += input.instancePosition.y;
		mod_input_vertex_pos.z += input.instancePosition.z;
		mod_input_vertex_pos.w = 1.0f;

		//mod_input_vertex_pos.xyz = mod_input_vertex_pos.xyz + (forwardDir * (someheightmapvalue * 0.000001));
		//mod_input_vertex_pos.xyz = mod_input_vertex_pos.xyz + (float3(0,0,1) * (someheightmapvalue * 0.01));

		forwardDir = float3(input.instanceRadRotFORWARD.x, input.instanceRadRotFORWARD.y, input.instanceRadRotFORWARD.z);
		rightDir = float3(input.instanceRadRotRIGHT.x, input.instanceRadRotRIGHT.y, input.instanceRadRotRIGHT.z); 
		upDir = float3(input.instanceRadRotUP.x, input.instanceRadRotUP.y, input.instanceRadRotUP.z);

		//mod_input_vertex_pos.z = mod_input_vertex_pos.z + (forwardDir.z * (someheightmapvalue * 0.000001));
		//mod_input_vertex_pos += someheightmapvalue;

		//float2 noise = (frac(sin(dot(float2(input.position.x,input.position.z) ,float2(12.9898,78.233)*2.0)) * 43758.5453));
		//float test = abs(noise.x + noise.y) * 0.5 * 0.001;
		//input.color = float4(input.color.x + (input.position.x*0.1),input.color.y+ (input.position.y*0.1),input.color.z+ (input.position.z*0.1),input.color.w);
   
		


		MOVINGPOINT = float3(input.instancePosition.x, input.instancePosition.y, input.instancePosition.z);
		//MOVINGPOINT.xyz = MOVINGPOINT.xyz + (float3(0,0,1) * (someheightmapvalue * 0.001));
		

		vertPos = float3(mod_input_vertex_pos.x, mod_input_vertex_pos.y, mod_input_vertex_pos.z);	

		diffX = (vertPos.x - (input.instancePosition.x));
		diffY = (vertPos.y - (input.instancePosition.y));
		diffZ = (vertPos.z - (input.instancePosition.z));
		
		//diffX = ((input.instancePosition.x) - vertPos.x);
		//diffY = ((input.instancePosition.y) - vertPos.y);
		//diffZ = ((input.instancePosition.z) - vertPos.z);
		


		MOVINGPOINT = MOVINGPOINT + (-rightDir * diffX);
		MOVINGPOINT = MOVINGPOINT + (upDir * diffY);
		MOVINGPOINT = MOVINGPOINT + (forwardDir * diffZ);


		



		input.position.x = MOVINGPOINT.x;
		input.position.y = MOVINGPOINT.y;
		input.position.z = MOVINGPOINT.z;


		//output.position = mul(mod_input_vertex_pos, world);
		output.position = mul(input.position, world);
		output.position = mul(output.position, view);
		output.position = mul(output.position, proj);

		output.instancePosition.x = input.instancePosition.x;
		output.instancePosition.y = input.instancePosition.y;
		output.instancePosition.z = input.instancePosition.z;

		output.instanceRadRotFORWARD.x = input.instanceRadRotFORWARD.x;
		output.instanceRadRotFORWARD.y = input.instanceRadRotFORWARD.y;
		output.instanceRadRotFORWARD.z = input.instanceRadRotFORWARD.z;

		output.instanceRadRotRIGHT.x = input.instanceRadRotRIGHT.x;
		output.instanceRadRotRIGHT.y = input.instanceRadRotRIGHT.y;
		output.instanceRadRotRIGHT.z = input.instanceRadRotRIGHT.z;

		output.instanceRadRotUP.x = input.instanceRadRotUP.x;
		output.instanceRadRotUP.y = input.instanceRadRotUP.y;
		output.instanceRadRotUP.z = input.instanceRadRotUP.z;

		output.color = input.color;
	}
	else
	{

		input.position.x = input.instancePosition.x;
		input.position.y = input.instancePosition.y;
		input.position.z = input.instancePosition.z;

		output.position = mul(input.position, world);
		output.position = mul(output.position, view);
		output.position = mul(output.position, proj);
		output.color = float4(0.5f,0.5f,0.5f,1);

		output.color = input.color;
	}
	

	



	/*output.paddingvert0 = input.paddingvert0;
	output.paddingvert1 = input.paddingvert1;
	output.paddingvert2 = input.paddingvert2;
	*/

	output.tex = input.tex;


	output.normal = input.normal;
	//output.normal = mul(input.normal, world);
	//output.normal = normalize(output.normal);

	output.xindex = input.xindex;
	output.yindex = input.yindex;
	//output.zindex = input.zindex;

	output.heightmapmat0 = input.heightmapmat0;
	output.heightmapmat1 = input.heightmapmat1;
	output.heightmapmat2 = input.heightmapmat2;
	output.heightmapmat3 = input.heightmapmat3;


	return output;
}




/*
technique Test
{
    pass pass0 //pass1
    {
        VertexShader = compile vs_5_0 TextureVertexShader();
        PixelShader  = compile ps_5_0 TexturePixelShader();
    }
}*/


		/*mod_input_vertex_pos.x += input.instancePosition.x;// +  (4*0.01f);//diffX;
		mod_input_vertex_pos.y += input.instancePosition.y ;// +  (4*0.01f);//diffY;
		mod_input_vertex_pos.z += input.instancePosition.z ;// +  (4*0.01f);//diffZ;
		mod_input_vertex_pos.w = 1.0f;

		forwardDir = float3(input.instanceRadRotFORWARD.x, input.instanceRadRotFORWARD.y, input.instanceRadRotFORWARD.z);
		rightDir = float3(input.instanceRadRotRIGHT.x, input.instanceRadRotRIGHT.y, input.instanceRadRotRIGHT.z); 

		//cross(world_up,float3(input.instanceRadRotFORWARD.x, input.instanceRadRotFORWARD.y, input.instanceRadRotFORWARD.z));
		//float3(input.instanceRadRotFORWARD.x, input.instanceRadRotFORWARD.y, input.instanceRadRotFORWARD.z); //world_forward;//

		upDir = float3(input.instanceRadRotUP.x, input.instanceRadRotUP.y, input.instanceRadRotUP.z);

		MOVINGPOINT = float3(input.instancePosition.x, input.instancePosition.y, input.instancePosition.z);
		vertPos = float3(mod_input_vertex_pos.x, mod_input_vertex_pos.y, mod_input_vertex_pos.z);	

		diffX = vertPos.x - input.instancePosition.x;
		diffY = vertPos.y - input.instancePosition.y;
		diffZ = vertPos.z - input.instancePosition.z;


		//MOVINGPOINT = MOVINGPOINT + -(rightDir * diffX *  (4*4*0.01f*2));
		//MOVINGPOINT = MOVINGPOINT + -(upDir * diffY *  (4*4*0.01f*2));
		//MOVINGPOINT = MOVINGPOINT + -(forwardDir * diffZ *  (4*4*0.01f*2));*/









		
		/*input.position.x += input.instancePosition.x;
		input.position.y += input.instancePosition.y;
		input.position.z += input.instancePosition.z;

		output.position = mul(input.position, world);
		output.position = mul(output.position, view);
		output.position = mul(output.position, proj);*/



		
		//diffX = (vertPos.x - (input.instancePosition.x + (4* input.paddingvert0 * 4 * 0.01f * 0.5f)));
		//diffY = (vertPos.y - (input.instancePosition.y+ (4* input.paddingvert1 * 4 * 0.01f * 0.5f)));
		//diffZ = (vertPos.z - (input.instancePosition.z+ (4* input.paddingvert2 * 4 * 0.01f * 0.5f)));
		//MOVINGPOINT.x += diffX;
		//MOVINGPOINT.y += diffY;
		//MOVINGPOINT.z += diffZ;
		//MOVINGPOINT = MOVINGPOINT + -(rightDir * (diffX+(4* input.paddingvert0 * 4 * 0.01f * 0.5f)));
		//MOVINGPOINT = MOVINGPOINT + -(upDir * (diffY+(4* input.paddingvert1 * 4 * 0.01f * 0.5f)));
		//MOVINGPOINT = MOVINGPOINT + -(forwardDir * (diffZ+(4* input.paddingvert2 * 4 * 0.01f * 0.5f)));
		//input.position.x = MOVINGPOINT.x + (4* input.paddingvert0 * 4 * 0.01f * 0.5f);//+ (4*4*0.01f*0.5f);
		//input.position.y = MOVINGPOINT.y + (4* input.paddingvert1 * 4 * 0.01f * 0.5f);//;// + (4*4*0.01f*0.5f);
		//input.position.z = MOVINGPOINT.z + (4* input.paddingvert2 * 4 * 0.01f * 0.5f);//;//+ (4*4*0.01f*0.5f);
		//input.position.x += diffX;
		//input.position.y += diffY;
		//input.position.z += diffZ;

