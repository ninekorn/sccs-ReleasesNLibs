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
	float4 colorsNFaces : POSITION5;
	float4 mapmatrix0 : POSITION6;
	float4 mapmatrix1 : POSITION7;
	float4 mapmatrix2 : POSITION8;
	float4 mapmatrix3 : POSITION9;
	float4 mapmatrix4 : POSITION10;
	float4 mapmatrix5 : POSITION11;
	float4 mapmatrix6 : POSITION12;
	float4 mapmatrix7 : POSITION13;
	float4 mapmatrix8 : POSITION14;
	float4 mapmatrix9 : POSITION15;
	float4 mapmatrix10 : POSITION16;
	float4 mapmatrix11 : POSITION17;
	float4 mapmatrix12 : POSITION18;
	float4 mapmatrix13 : POSITION19;
	float4 mapmatrix14 : POSITION20;
	float4 mapmatrix15 : POSITION21;
	/*int one : PSIZE3;	
	int oneTwo : PSIZE4;
	int two : PSIZE5;	
	int twoTwo : PSIZE6;	
	int three : PSIZE7;	
	int threeTwo : PSIZE8;	
	int four : PSIZE9;	
	int fourTwo : PSIZE10;*/
	int xindex : PSIZE11;	
	int yindex : PSIZE12;
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
	float4 colorsNFaces : POSITION5;
	float4 mapmatrix0 : POSITION6;
	float4 mapmatrix1 : POSITION7;
	float4 mapmatrix2 : POSITION8;
	float4 mapmatrix3 : POSITION9;
	float4 mapmatrix4 : POSITION10;
	float4 mapmatrix5 : POSITION11;
	float4 mapmatrix6 : POSITION12;
	float4 mapmatrix7 : POSITION13;
	float4 mapmatrix8 : POSITION14;
	float4 mapmatrix9 : POSITION15;
	float4 mapmatrix10 : POSITION16;
	float4 mapmatrix11 : POSITION17;
	float4 mapmatrix12 : POSITION18;
	float4 mapmatrix13 : POSITION19;
	float4 mapmatrix14 : POSITION20;
	float4 mapmatrix15 : POSITION21;
	/*int one : PSIZE3;	
	int oneTwo : PSIZE4;
	int two : PSIZE5;	
	int twoTwo : PSIZE6;	
	int three : PSIZE7;	
	int threeTwo : PSIZE8;	
	int four : PSIZE9;	
	int fourTwo : PSIZE10;*/
	int xindex : PSIZE11;	
	int yindex : PSIZE12;
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

//float planeSize = 0.1f;
//static int mapWidth = 4;
//static int mapHeight = 4;
//static int mapDepth = 4;

static int tinyChunkWidth = 8;
static int tinyChunkHeight = 8;
static int tinyChunkDepth = 8;
static const int maxfloatbytemaparraylength = 8;
static const int maxfloatbytemaparraylengthfull = 9;
static double arrayOfDigits[maxfloatbytemaparraylength];
static double arrayOfDigitsFull[maxfloatbytemaparraylengthfull];

//int iVar[3] = {1,2,3};
//int someoptions[1] = {0};
static int somebytemap[1008];//  = {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};
static int somebytemapswtc[1008];// = {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};

//21 * 6 * 8 = 1008

static double initialmap = 51111.0;

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

	double currentByte = 0.0;

	//IVE SET THE VERTEX UNINSTANCED COLOR TO HOLD THE INDEX LOCATION OF THE BYTE
	int x = int(input.color.x); 
	int y = int(input.color.y); 
	int z = int(input.color.z);
	//IVE SET THE VERTEX UNINSTANCED COLOR TO HOLD THE INDEX LOCATION OF THE BYTE

	int facetype = int(input.color.w);

	int currentMapData;
	
	int currentIndex = x + tinyChunkWidth * (y + (tinyChunkHeight * z));
	int someOtherIndex = currentIndex;


	//0-4-1-5-2-6-3-7
	//8-12-9-13-10-14-11-15
	//16-20-17-21-18-22-19-23
	//24-28-25-29-26-30-27-31
	//32-36-33-37-34-38-35-39
	//40-44-41-45-42-46-43-47
	//48-52-49-53-50-54-51-55
	//56-60-57-61-58-62-59-63  





	
	double selectablevectordouble = 0;
    int index = currentIndex;
    //int maxv = tinyChunkWidth * tinyChunkHeight * tinyChunkDepth;
    int somemaxvecdigit = tinyChunkWidth;
    int somecountermul = 0;
    int somec = 0;

    for (int t = 0; t <= currentIndex; t++) // index == 45 == 11 
    {
        if (somec >= somemaxvecdigit)
        {
            somecountermul++;
            somec = 0;
        }
        somec++;
    }



    
    int somemin = (somecountermul) * somemaxvecdigit; //0
    int somemid = (somemaxvecdigit / 2 ) + somemin; // 4
    int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //8

    if(currentIndex >= somemin && currentIndex <= somemid - 1)
    {
            someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);  // 3 - 0 * 2 = 6 + 1 = 7
    }
    else if (currentIndex >= somemid && currentIndex <= somemax - 1) // 4 // 7
    {
            someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);  // 7 - 4 = 3 * 2 = 6 + 0 = 6
    }



    
	if(somecountermul == 0)
	{  
		currentMapData = input.mapmatrix0.x;
       
        //4
        //0 1
        //2 3

        //8
        //0 3
        //4 7

        //16
        //16 23
        //24 31

        // 61 - 60 = 1 => 1 * 2=> 1 + 2 = 3 // 0 1 2 3 for index 3 which is the 4rth digit
        // 63-62 = 1 * 2 = 2 => 0 + 2 = 2 for index 2 which is the 3rd digit


        int somemin = (somecountermul) * somemaxvecdigit; //0
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 4
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //8

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);  // 3 - 0 * 2 = 6 + 1 = 7
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1) // 4 // 7
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);  // 7 - 4 = 3 * 2 = 6 + 0 = 6
        }

	}
	else if(somecountermul == 1)
	{


		currentMapData =input.mapmatrix0.y;


        int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }


	}
	
    
    else if(somecountermul == 2)
	{
		currentMapData =input.mapmatrix0.z;
				
        int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }


	}
	else if(somecountermul == 3)
	{
		currentMapData =input.mapmatrix0.w;
	     
    
          int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }



	}
	else if(somecountermul == 4)
	{
		currentMapData =input.mapmatrix1.x;
	     
      
          int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }


	}
	else if(somecountermul == 5)
	{
		currentMapData =input.mapmatrix1.y;
	     
    int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }



	}
	else if(somecountermul == 6)
	{
		currentMapData =input.mapmatrix1.z;
	     
     
          int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }


	}
	else if(somecountermul == 7)
	{
		currentMapData =input.mapmatrix1.w;
	     
 
          int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }


	}
	else if(somecountermul == 8)
	{
		currentMapData =input.mapmatrix2.x;
	     
 
         int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }

	}
	else if(somecountermul == 9)
	{
		currentMapData =input.mapmatrix2.y;
	     

          int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }


	}
	else if(somecountermul == 10)
	{
		currentMapData =input.mapmatrix2.z;
	
        
          int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }

	}
	else if(somecountermul == 11)
	{
		currentMapData =input.mapmatrix2.w;
	 
         int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }

	}
	else if(somecountermul == 12)
	{
		currentMapData =input.mapmatrix3.x;
	   
          int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }

	}
	else if(somecountermul == 13)
	{
		currentMapData =input.mapmatrix3.y;
	
         int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }


	}
	else if(somecountermul == 14)
	{
		currentMapData =input.mapmatrix3.z;
	
          int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }


	}
	else if(somecountermul == 15)
	{
		currentMapData =input.mapmatrix3.w;
		
          int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }

	}*/



    /*
	else if(somecountermul == 16)
	{  
		currentMapData =input.mapmatrix4.x;
        int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }



	}
	else if(somecountermul == 17)
	{
		currentMapData =input.mapmatrix4.y;
	    int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }

	}
	else if(somecountermul == 18)
	{
		currentMapData =input.mapmatrix4.z;
    int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }


	}
	else if(somecountermul == 19)
	{
		currentMapData =input.mapmatrix4.w;
	 int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }

	}
	else if(somecountermul == 20)
	{
		currentMapData =input.mapmatrix5.x;
     int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }

	}
	else if(somecountermul == 21)
	{
		currentMapData =input.mapmatrix5.y;
 int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }

	}
	else if(somecountermul == 22)
	{
		currentMapData =input.mapmatrix5.z;
 int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }


	}
	else if(somecountermul == 23)
	{
		currentMapData =input.mapmatrix5.w;
 int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }



	}
	else if(somecountermul == 24)
	{
		currentMapData =input.mapmatrix6.x;
 int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }

	}
	else if(somecountermul == 25)
	{
		currentMapData =input.mapmatrix6.y;
 int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }

	}
	else if(somecountermul == 26)
	{
		currentMapData =input.mapmatrix6.z;
 int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }

	}
	else if(somecountermul == 27)
	{
		currentMapData =input.mapmatrix6.w;
 int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }


	}
	else if(somecountermul == 28)
	{
		currentMapData =input.mapmatrix7.x;
		 int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }

	}
	else if(somecountermul == 29)
	{
		currentMapData =input.mapmatrix7.y;
	    int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }

	}
	else if(somecountermul == 30)
	{
		currentMapData =input.mapmatrix7.z;
	      int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }

	}
	else if(somecountermul == 31)
	{
		currentMapData =input.mapmatrix7.w;
	 int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }


	}









	else if(somecountermul == 32)
	{  
		currentMapData =input.mapmatrix8.x;

		 int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }

	}
	else if(somecountermul == 33)
	{
		currentMapData =input.mapmatrix8.y;
	  int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }


	}
	else if(somecountermul == 34)
	{
		currentMapData =input.mapmatrix8.z;
	  int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }


	}
	else if(somecountermul == 35)
	{
		currentMapData =input.mapmatrix8.w;
 int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }

	}
	else if(somecountermul == 36)
	{
		currentMapData =input.mapmatrix9.x;
	    int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }

	}
	else if(somecountermul == 37)
	{
		currentMapData =input.mapmatrix9.y;
    int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }

	}
	else if(somecountermul == 38)
	{
		currentMapData =input.mapmatrix9.z;
	 int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }

	}
	else if(somecountermul == 39)
	{
		currentMapData =input.mapmatrix9.w;
		 int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }


	}
	else if(somecountermul == 40)
	{
		currentMapData =input.mapmatrix10.x;
	  int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }

	}
	else if(somecountermul == 41)
	{
		currentMapData =input.mapmatrix10.y;
     int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }

	}
	else if(somecountermul == 42)
	{
		currentMapData =input.mapmatrix10.z;
     int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }

	}
	else if(somecountermul == 43)
	{
		currentMapData =input.mapmatrix10.w;
	  int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }

	}
	else if(somecountermul == 44)
	{
		currentMapData =input.mapmatrix11.x;
	  int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }

	}
	else if(somecountermul == 45)
	{
		currentMapData =input.mapmatrix11.y;
	 int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }

	}
	else if(somecountermul == 46)
	{
		currentMapData =input.mapmatrix11.z;
     int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }


	}
	else if(somecountermul == 47)
	{
		currentMapData =input.mapmatrix11.w;
    int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }

	}



	else if(somecountermul == 48)
	{  
		currentMapData =input.mapmatrix12.x;
  int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }

	}
	else if(somecountermul == 49)
	{
		currentMapData =input.mapmatrix12.y;
	 int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }

	}
	else if(somecountermul == 50)
	{
		currentMapData =input.mapmatrix12.z;
	 int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }


	}
	else if(somecountermul == 51)
	{
		currentMapData =input.mapmatrix12.w;
	 int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }


	}
	else if(somecountermul == 52)
	{
		currentMapData =input.mapmatrix13.x;
	 int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }


	}
	else if(somecountermul == 53)
	{
		currentMapData =input.mapmatrix13.y;
	  int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }


	}
	else if(somecountermul == 54)
	{
		currentMapData =input.mapmatrix13.z;
     int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }


	}
	else if(somecountermul == 55)
	{
		currentMapData =input.mapmatrix13.w;
	  int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }

	}
	else if(somecountermul == 56)
	{
		currentMapData =input.mapmatrix14.x;
        int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }

	}
	else if(somecountermul == 57)
	{
		currentMapData =input.mapmatrix14.y;
	    int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }

	}
	else if(somecountermul == 58)
	{
		currentMapData =input.mapmatrix14.z;
	    int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }

	}
	else if(somecountermul == 59)
	{
		currentMapData =input.mapmatrix14.w;
	    int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }

	}
	else if(somecountermul == 60)
	{
		currentMapData =input.mapmatrix15.x;
    int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }

	}
	else if(somecountermul == 61)
	{
		currentMapData =input.mapmatrix15.y;
        int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }

	}
	else if(somecountermul == 62)
	{
		currentMapData =input.mapmatrix15.z;
        int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2);
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }

	}
	else if(somecountermul == 63) // 
	{
		currentMapData =input.mapmatrix15.w;

        int somemin = (somecountermul) * somemaxvecdigit; //16
        int somemid = (somemaxvecdigit / 2 ) + somemin; // 24
        int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //32

        if(currentIndex >= somemin && currentIndex <= somemid - 1)
        {
             someOtherIndex = 1 + (((somemid - 1) - someOtherIndex) * 2); // 
        }
        else if (currentIndex >= somemid && currentIndex <= somemax - 1)
        {
              someOtherIndex = 0 + (((somemax-1) - someOtherIndex) * 2);
        }
       
	}*/












	/*
	if (currentIndex >= 0 && currentIndex <= 3)
    {
        currentMapData =input.mapmatrix0.x;

        if (currentIndex >= 0 && currentIndex <= 1)
        {
            int somemax = 1;
            someOtherIndex = 1 + ((somemax - someOtherIndex) * 2);
        }
        else if (currentIndex >= 2 && currentIndex <= 3)
        {
            int somemax = 3;
            someOtherIndex = 0 + ((somemax - someOtherIndex) * 2);
        }
    }
    else if (currentIndex >= 4 && currentIndex <= 7)
    {
		currentMapData =input.mapmatrix0.y;
        if (currentIndex >= 4 && currentIndex <= 5)
        {
            int somemax = 5;
            someOtherIndex = 1 + ((somemax - someOtherIndex) * 2);
        }
        else if (currentIndex >= 6 && currentIndex <= 7)
        {
            int somemax = 7;
            someOtherIndex = 0 + ((somemax - someOtherIndex) * 2);
        }
    }
    else if (currentIndex >= 8 && currentIndex <= 11)
    {
        currentMapData =input.mapmatrix0.z;

        if (currentIndex >= 8 && currentIndex <= 9)
        {
            int somemax = 9;
            someOtherIndex = 1 + ((somemax - someOtherIndex) * 2);
        }
        else if (currentIndex >= 10 && currentIndex <= 11)
        {
            int somemax = 11;
            someOtherIndex = 0 + ((somemax - someOtherIndex) * 2);
        }
    }

    else if (currentIndex >= 12 && currentIndex <= 15)
    {
      		currentMapData =input.mapmatrix0.w;
        if (currentIndex >= 12 && currentIndex <= 13)
        {
            int somemax = 13;
            someOtherIndex = 1 + ((somemax - someOtherIndex) * 2);
        }
        else if (currentIndex >= 14 && currentIndex <= 15)
        {
            int somemax = 15;
            someOtherIndex = 0 + ((somemax - someOtherIndex) * 2);
        }
    }
    else if (currentIndex >= 16 && currentIndex <= 19)
    {
      		currentMapData =input.mapmatrix1.x;
        if (currentIndex >= 16 && currentIndex <= 17)
        {
            int somemax = 17;
            someOtherIndex = 1 + ((somemax - someOtherIndex) * 2);
        }
        else if (currentIndex >= 18 && currentIndex <= 19)
        {
            int somemax = 19;
            someOtherIndex = 0 + ((somemax - someOtherIndex) * 2);
        }
    }
    else if (currentIndex >= 20 && currentIndex <= 23)
    {
             		currentMapData =input.mapmatrix1.y;
        if (currentIndex >= 20 && currentIndex <= 21)
        {
            int somemax = 21;
            someOtherIndex = 1 + ((somemax - someOtherIndex) * 2);
        }
        else if (currentIndex >= 22 && currentIndex <= 23)
        {
            int somemax = 23;
            someOtherIndex = 0 + ((somemax - someOtherIndex) * 2);
        }
    }
    else if (currentIndex >= 24 && currentIndex <= 27)
    {
        currentMapData =input.mapmatrix1.z;
        if (currentIndex >= 24 && currentIndex <= 25)
        {
            int somemax = 25;
            someOtherIndex = 1 + ((somemax - someOtherIndex) * 2);
        }
        else if (currentIndex >= 26 && currentIndex <= 27)
        {
            int somemax = 27;
            someOtherIndex = 0 + ((somemax - someOtherIndex) * 2);
        }
    }
	else if (currentIndex >= 28 && currentIndex <= 31)
    {
        currentMapData =input.mapmatrix1.w;
        if (currentIndex >= 28 && currentIndex <= 29)
        {
            int somemax = 29;
            someOtherIndex = 1 + ((somemax - someOtherIndex) * 2);
        }
        else if (currentIndex >= 30 && currentIndex <= 31)
        {
            int somemax = 31;
            someOtherIndex = 0 + ((somemax - someOtherIndex) * 2);
        }
    }
    else if (currentIndex >= 32 && currentIndex <= 35)
    {
        currentMapData =input.mapmatrix2.x;

        if (currentIndex >= 32 && currentIndex <= 33)
        {
            int somemax = 33;
            someOtherIndex = 1 + ((somemax - someOtherIndex) * 2);
        }
        else if (currentIndex >= 34 && currentIndex <= 35)
        {
            int somemax = 35;
            someOtherIndex = 0 + ((somemax - someOtherIndex) * 2);
        }
    }
    else if (currentIndex >= 36 && currentIndex <= 39)
    {
        currentMapData =input.mapmatrix2.y;

        if (currentIndex >= 36 && currentIndex <= 37)
        {
            int somemax = 37;
            someOtherIndex = 1 + ((somemax - someOtherIndex) * 2);
        }
        else if (currentIndex >= 38 && currentIndex <= 39)
        {
            int somemax = 39;
            someOtherIndex = 0 + ((somemax - someOtherIndex) * 2);
        }
    }
    else if (currentIndex >= 40 && currentIndex <= 43)
    {
        currentMapData =input.mapmatrix2.z;

        if (currentIndex >= 40 && currentIndex <= 41)
        {
            int somemax = 41;
            someOtherIndex = 1 + ((somemax - someOtherIndex) * 2);
        }
        else if (currentIndex >= 42 && currentIndex <= 43)
        {
            int somemax = 43;
            someOtherIndex = 0 + ((somemax - someOtherIndex) * 2);
        }
    }
    else if (currentIndex >= 44 && currentIndex <= 47)
    {
        currentMapData =input.mapmatrix2.w;

        if (currentIndex >= 44 && currentIndex <= 45)
        {
            int somemax = 45;
            someOtherIndex = 1 + ((somemax - someOtherIndex) * 2);
        }
        else if (currentIndex >= 46 && currentIndex <= 47)
        {
            int somemax = 47;
            someOtherIndex = 0 + ((somemax - someOtherIndex) * 2);
        }
    }
    else if (currentIndex >= 48 && currentIndex <= 51)
    {
       currentMapData =input.mapmatrix3.x;

        if (currentIndex >= 48 && currentIndex <= 49)
        {
            int somemax = 49;
            someOtherIndex = 1 + ((somemax - someOtherIndex) * 2);
        }
        else if (currentIndex >= 50 && currentIndex <= 51)
        {
            int somemax = 51;
            someOtherIndex = 0 + ((somemax - someOtherIndex) * 2);
        }
    }
    else if (currentIndex >= 52 && currentIndex <= 55)
    {
        currentMapData =input.mapmatrix3.y;

        if (currentIndex >= 52 && currentIndex <= 53)
        {
            int somemax = 53;
            someOtherIndex = 1 + ((somemax - someOtherIndex) * 2);
        }
        else if (currentIndex >= 54 && currentIndex <= 55)
        {
            int somemax = 55;
            someOtherIndex = 0 + ((somemax - someOtherIndex) * 2);
        }
    }
    else if (currentIndex >= 56 && currentIndex <= 59)
    {
        currentMapData =input.mapmatrix3.z;

        if (currentIndex >= 56 && currentIndex <= 57)
        {
            int somemax = 57;
            someOtherIndex = 1 + ((somemax - someOtherIndex) * 2);
        }
        else if (currentIndex >= 58 && currentIndex <= 59)
        {
            int somemax = 59;
            someOtherIndex = 0 + ((somemax - someOtherIndex) * 2);
        }
    }
    else if (currentIndex >= 60 && currentIndex <= 63)
    {
        currentMapData =input.mapmatrix3.w;

        if (currentIndex >= 60 && currentIndex <= 61)
        {
            int somemax = 61;
            someOtherIndex = 1 + ((somemax - someOtherIndex) * 2); // 61-60 = 1 *2 = 2 => 1 + 2
        }
        else if (currentIndex >= 62 && currentIndex <= 63)
        {
            int somemax = 63;
            someOtherIndex = 0 + ((somemax - someOtherIndex) * 2); // 63-62 = 1 * 2 = 2 => 0 + 2
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

	//double someremains = (initialmap - currentMapData) * someothermul1; //51111 - 50111 = 1000


	double someothermul0 = 10.0;
	double someothermul1 = 1.0;

    
	/*if (someOtherIndex == 0)
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
	else if (someOtherIndex == 8)
	{
		someothermul0 = 10000000000.0;
		someothermul1 = 0.000000001;
	}
	else if (someOtherIndex == 9)
	{
		someothermul0 = 100000000000.0;
		someothermul1 = 0.0000000001;
	}
	else if (someOtherIndex == 10)
	{
		someothermul0 = 1000000000000.0;
		someothermul1 = 0.00000000001;
	}
	else if (someOtherIndex == 11)
	{
		someothermul0 = 10000000000000.0;
		someothermul1 = 0.000000000001;
	}
	else if (someOtherIndex == 12)
	{
		someothermul0 = 100000000000000.0;
		someothermul1 = 0.0000000000001;
	}
	else if (someOtherIndex == 13)
	{
		someothermul0 = 1000000000000000.0;
		someothermul1 = 0.00000000000001;
	}
	else if (someOtherIndex == 14)
	{
		someothermul0 = 10000000000000000.0;
		someothermul1 = 0.000000000000001;
	}
	else if (someOtherIndex == 15)
	{
		someothermul0 = 100000000000000000.0;
		someothermul1 = 0.0000000000000001;
	}



	//double arrayOfDigits[maxfloatbytemaparraylength-1];// = {0,0,0,0,0,0,0,0,0};

	double somemap = currentMapData;// 51234;//51111 //currentMapData // 51011

	double tempsomemap;
	double someotherbyte;
     
                                                
	for (int i = 0; i < maxfloatbytemaparraylength; i++) //111111111 // 6th digit //someOtherIndex
	{
		tempsomemap = currentMapData;
		somemap = (somemap * 0.1); // 51011 becomes 5101.1

		double somevalue = floor((double)(int)((somemap - (int)somemap) * someothermul0)) * someothermul1; // 5101.1 - 5101 = 0.1 => 0.1 * 
		somevalue = somevalue - (floor(somevalue * 0.1) * 10);

		arrayOfDigits[i] = somevalue;
	}


    /*
    for (int i = 0; i < maxfloatbytemaparraylength; i++) //111111111 // 6th digit //someOtherIndex
	{
        //somemap = currentMapData;
		//tempsomemap = currentMapData;
        tempsomemap = somemap;
		somemap = (somemap * 0.1); // 51011 becomes 5101.1
        //double roundedmap = round(somemap * 0.1) * 10; //51011 becomes 51010
		double somevalue = round((double)(int)((somemap - (int)somemap) * someothermul0)) * someothermul1; // 5101.1 - 5101 = 0.1 => 0.1 * 
		//somevalue = somevalue - (round(somevalue * 0.1) * 10);
        //(somemap - (int)somemap)
		//arrayOfDigits[i] = floor(tempsomemap - roundedmap);  // 51011 - 51010 = 1
       	
        somevalue = somevalue - (floor(somevalue * 0.1) * 10);
		arrayOfDigits[i] = somevalue;
	}*/
    

    
	if(someOtherIndex == 2)
	{
		someOtherIndex = 1;
	}
	else if(someOtherIndex == 1)
	{
		someOtherIndex = 2;
	}
	//double someotherindex1 = 3 - someOtherIndex;













	double somemap1 = currentMapData;// 51234;//51111 //currentMapData

	double tempsomemap1;
	double someotherbyte1;
                                                    
	for (int i = 0; i < maxfloatbytemaparraylengthfull; i++) //111111111 // 6th digit //someOtherIndex
	{
		tempsomemap1 = somemap1;
		somemap1 = (somemap1 * 0.1);

        //double somevalue1 = floor((double)(int)((somemap1 - (int)somemap1) * 1000000.0)) * 0.00001;
		double somevalue1 = floor((double)(int)((somemap1 - (int)somemap1) * 1000000000000.0)) * 0.00000000001;
		somevalue1 = somevalue1 - (floor(somevalue1 * 0.1) * 10);

		arrayOfDigitsFull[i] = somevalue1;
	}

	double somedesiredbyte = 0;





    
	/*double somemap1 = currentMapData;// 51234;//51111 //currentMapData

	double tempsomemap1;
	double someotherbyte1;
                                                    
	for (int i = 0; i < maxfloatbytemaparraylengthfull; i++) //111111111 // 6th digit //someOtherIndex
	{
		tempsomemap1 = somemap1;
		somemap1 = (somemap1 * 0.1);

		double somevalue1 = floor((double)(int)((somemap1 - (int)somemap1) * 1000000.0)) * 0.00001;
		somevalue1 = somevalue1 - (floor(somevalue1 * 0.1) * 10);

		arrayOfDigitsFull[i] = somevalue1;
	}

	double somedesiredbyte = 0;*/




	//5 full chunk cube all 1s for byte breaking when 1s becomes 0s (and for byte adding when byte 0s become 1s WIP)
	//4 full chunk cube all 0s for path tracing with path traced with bytes becoming 1s when the player moves around the invisible chunk.
	//3 full chunk cube all 0s for a way to visualize spatial location of objects in a 3d scene. // not working entirely i think. double checking soon
	//2 full chunk cube all 1s for byte breaking when 1s becomes 0s (and for byte adding when byte 0s become 1s WIP) - using random perlin WIP
	//1 WIP TRANSPARENCY GRID LIKE CHUNK WITH MY UPCOMING CODING CHALLENGE TO LEARN RASTERTEK C# TRANSPARENCY.

	if(round(arrayOfDigitsFull[maxfloatbytemaparraylengthfull -1]) == 5.0)
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
	}


    somedesiredbyte = 1.0;


    /*
    //if(round(input.colorsNFaces.x) == 0.0 && round(input.colorsNFaces.y) == 0.0 && round(input.colorsNFaces.z) == 0.0 ||
    //   round(input.colorsNFaces.x) == 1.0 && round(input.colorsNFaces.y) == 0.0 && round(input.colorsNFaces.z) == 0.0 ) 
	
    if(round(input.colorsNFaces.x) == 0.0 && round(input.colorsNFaces.y) == 0.0 && round(input.colorsNFaces.z) == 0.0 ||
        round(input.colorsNFaces.x) == 1.0 && round(input.colorsNFaces.y) == 0.0 && round(input.colorsNFaces.z) == 0.0 ||
        round(input.colorsNFaces.x) == 2.0 && round(input.colorsNFaces.y) == 0.0 && round(input.colorsNFaces.z) == 0.0 ||
        round(input.colorsNFaces.x) == 3.0 && round(input.colorsNFaces.y) == 0.0 && round(input.colorsNFaces.z) == 0.0 ||
        round(input.colorsNFaces.x) == 0.0 && round(input.colorsNFaces.y) == 0.0 && round(input.colorsNFaces.z) == 0.0 ||
        round(input.colorsNFaces.x) == 0.0 && round(input.colorsNFaces.y) == 0.0 && round(input.colorsNFaces.z) == 1.0 ||
        round(input.colorsNFaces.x) == 0.0 && round(input.colorsNFaces.y) == 0.0 && round(input.colorsNFaces.z) == 2.0 ||
        round(input.colorsNFaces.x) == 0.0 && round(input.colorsNFaces.y) == 0.0 && round(input.colorsNFaces.z) == 3.0) 
    {
        input.position.x = input.instancePosition.x;
		input.position.y = input.instancePosition.y;
		input.position.z = input.instancePosition.z;

		output.position = mul(input.position, world);
		output.position = mul(output.position, view);
		output.position = mul(output.position, proj);
		output.color = float4(0.5f,0.5f,0.5f,1);


    }*/


	if(round(arrayOfDigits[someOtherIndex]) == somedesiredbyte) //1.0 // 0.0
	{	
		input.position.w = 1.0f;

		//input.position.x = input.position.x *-1;

		mod_input_vertex_pos = input.position;

		mod_input_vertex_pos.x += input.instancePosition.x;
		mod_input_vertex_pos.y += input.instancePosition.y;
		mod_input_vertex_pos.z += input.instancePosition.z;
		mod_input_vertex_pos.w = 1.0f;

		forwardDir = float3(input.instanceRadRotFORWARD.x, input.instanceRadRotFORWARD.y, input.instanceRadRotFORWARD.z);
		rightDir = float3(input.instanceRadRotRIGHT.x, input.instanceRadRotRIGHT.y, input.instanceRadRotRIGHT.z); 
		upDir = float3(input.instanceRadRotUP.x, input.instanceRadRotUP.y, input.instanceRadRotUP.z);

		MOVINGPOINT = float3(input.instancePosition.x, input.instancePosition.y, input.instancePosition.z);
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


		/*input.position.x = -input.position.x - input.instancePosition.x;
		input.position.y += input.instancePosition.y;
		input.position.z += input.instancePosition.z;

		output.position = mul(input.position, world);
		output.position = mul(output.position, view);
		output.position = mul(output.position, proj);*/



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


	



	output.paddingvert0 = input.paddingvert0;
	output.paddingvert1 = input.paddingvert1;
	output.paddingvert2 = input.paddingvert2;


	output.tex = input.tex;


	output.normal = input.normal;
	//output.normal = mul(input.normal, world);
	//output.normal = normalize(output.normal);

	output.xindex = input.xindex;
	output.yindex = input.yindex;
	//output.zindex = input.zindex;

	output.colorsNFaces = input.colorsNFaces;

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

