
cbuffer MatrixBuffer :register(b0)
{
	float4x4 world;
	float4x4 view;
	float4x4 proj;
};


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

static int tinyChunkWidth = 4;
static int tinyChunkHeight = 4;
static int tinyChunkDepth = 4;
static const int maxfloatbytemaparraylength = 4;
static const int maxfloatbytemaparraylengthfull = 5;
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
	

	int currentIndex = x + (tinyChunkWidth * (y + (tinyChunkHeight * z)));
	int someOtherIndex = currentIndex;

    //somechunkmap.M31 = somechunkkeyboardpriminstanceindex_;
    //somechunkmap.M32 = chunkprimindex_;
    //somechunkmap.M33 = chunkinstindex;

	//int somemainclassindex = input.mapmatrix2.x;
	//int somemainmeshindex = input.mapmatrix2.y;
	//int somemaininstanceindex = input.mapmatrix2.z;
	



	/*int indexmax = 8;
	int indexmul = somemainclassindex * somemainmeshindex * somemaininstanceindex;

	int indexForShaderMemory = (indexmul * indexmax) + currentIndex;

	if(somebytemapswtc[indexForShaderMemory] == 0) // the index of the byte within a chunk. can be from between 0 to tinyChunkWidth * tinyChunkHeight * tinyChunkDepth
	{
		//currentByte = nthbit(currentMapData,someOtherIndex);

		somebytemap[indexForShaderMemory]  = 1;
		somebytemapswtc[indexForShaderMemory] = 1;
	}*/



	//somemainclassindex * somemainmeshindex * somemaininstanceindex;


	//0-4-1-5-2-6-3-7
	//8-12-9-13-10-14-11-15
	//16-20-17-21-18-22-19-23
	//24-28-25-29-26-30-27-31
	//32-36-33-37-34-38-35-39
	//40-44-41-45-42-46-43-47
	//48-52-49-53-50-54-51-55
	//56-60-57-61-58-62-59-63  

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
            someOtherIndex = 1 + ((somemax - someOtherIndex) * 2);
        }
        else if (currentIndex >= 62 && currentIndex <= 63)
        {
            int somemax = 63;
            someOtherIndex = 0 + ((somemax - someOtherIndex) * 2);
        }
    }

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



	//double arrayOfDigits[maxfloatbytemaparraylength-1];// = {0,0,0,0,0,0,0,0,0};

	double somemap = currentMapData;// 51234;//51111 //currentMapData

	double tempsomemap;
	double someotherbyte;
                                                    
	for (int i = 0; i < maxfloatbytemaparraylength; i++) //111111111 // 6th digit //someOtherIndex
	{
		tempsomemap = somemap;
		somemap = (somemap * 0.1);

		double somevalue = floor((double)(int)((somemap - (int)somemap) * someothermul0)) * someothermul1;
		somevalue = somevalue - (floor(somevalue * 0.1) * 10);

		arrayOfDigits[i] = somevalue;
	}

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

		double somevalue1 = floor((double)(int)((somemap1 - (int)somemap1) * 1000000.0)) * 0.00001;
		somevalue1 = somevalue1 - (floor(somevalue1 * 0.1) * 10);

		arrayOfDigitsFull[i] = somevalue1;
	}

	double somedesiredbyte = 0;



	//5 full chunk cube all 1s for byte breaking when 1s becomes 0s (and for byte adding when byte 0s become 1s WIP)
	//4 full chunk cube all 0s for path tracing with path traced with bytes becoming 1s when the player moves around the invisible chunk.
	//3 full chunk cube all 0s for a way to visualize spatial location of objects in a 3d scene.
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




	if(round(arrayOfDigits[someOtherIndex]) == somedesiredbyte) //1.0 // 0.0
	{
		//PixelInputType output;
    
		input.position.w = 1.0f;

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

		diffX = vertPos.x - input.instancePosition.x;
		diffY = vertPos.y - input.instancePosition.y;
		diffZ = vertPos.z - input.instancePosition.z;

		//diffX = vertPos.x - input.position.x;
		//diffY = vertPos.y - input.position.y;
		//diffZ = vertPos.z - input.position.z;

		MOVINGPOINT = MOVINGPOINT + -(rightDir * diffX);
		MOVINGPOINT = MOVINGPOINT + -(upDir * diffY);
		MOVINGPOINT = MOVINGPOINT + -(forwardDir * diffZ);

		input.position.x = MOVINGPOINT.x;
		input.position.y = MOVINGPOINT.y;
		input.position.z = MOVINGPOINT.z;

		//output.color = input.color;	

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




		/*input.position.x += input.instancePosition.x;
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
		output.color = float4(0.35f,0.95f,0.35f,1.0f) * input.colorsNFaces;
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

/*technique Test
{
    pass pass0 //pass1
    {
        VertexShader = compile vs_5_0 TextureVertexShader();
        //PixelShader  = compile ps_5_0 TexturePixelShader();
    }
}*/



















   /*switch (someOtherIndex)
    {
        case 0:
            somemul = 0.1f;
            break;

        case 1:
            somemul = 0.01f;
            break;

        case 2:
            somemul = 0.001f;
            break;

        case 3:
            somemul = 0.0001f;
            break;

        case 4:
            somemul = 0.00001f;
            break;

        case 5:
            somemul = 0.000001f;
            break;

        case 6:
            somemul = 0.0000001f;
            break;

        case 7:
            somemul = 0.00000001f;
            break;

        case 8:
            somemul = 0.000000001f;
            break;

        case 9:
            somemul = 0.0000000001f;
            break;

        case 10:
            somemul = 0.00000000001f;
            break;

        case 11:
            somemul = 0.000000000001f;
            break;

        case 12:
            somemul = 0.0000000000001f;
            break;

        case 13:
            somemul = 0.00000000000001f;
            break;
        case 14:
            somemul = 0.000000000000001f;
            break;
        case 15:
            somemul = 0.0000000000000001f;
            break;
        case 16:
            somemul = 0.00000000000000001f;
            break;
        case 17:
            somemul = 0.000000000000000001f;
            break;
        case 18:
            somemul = 0.0000000000000000001f;
            break;
        case 19:
            somemul = 0.00000000000000000001f;
            break;
        case 20:
            somemul = 0.000000000000000000001f;
            break;
        case 21:
            somemul = 0.0000000000000000000001f;
            break;
        case 22:
            somemul = 0.00000000000000000000001f;
            break;
        case 23:
            somemul = 0.000000000000000000000001f;
            break;
        case 24:
            somemul = 0.0000000000000000000000001f;
            break;
        case 25:
            somemul = 0.00000000000000000000000001f;
            break;
        case 26:
            somemul = 0.000000000000000000000000001f;
            break;
        case 27:
            somemul = 0.0000000000000000000000000001f;
            break;
        case 28:
            somemul = 0.00000000000000000000000000001f;
            break;
        case 29:
            somemul = 0.000000000000000000000000000001f;
            break;
        case 30:
            somemul = 0.0000000000000000000000000000001f;
            break;
        case 31:
            somemul = 0.00000000000000000000000000000001f;
            break;

    }*/







	/*int index = currentIndex;
    float somemul = 0;
	int chosenmap = 0;

    if (index >= 0 && index <= 31)
    {
        currentMapData = (float)input.mapmatrix0.x;
        chosenmap = 0;
    }
    else if (index >= 32 && index <= 63)
    {
        currentMapData =  (float)input.mapmatrix0.y;
        chosenmap = 1;
    }
    else if (index >= 64 && index <= 95)
    {
        currentMapData = (float)input.mapmatrix0.z; 
        chosenmap = 2;
    }
    else if (index >= 96 && index <= 127)
    {
        currentMapData =  (float)input.mapmatrix0.w; 
        chosenmap = 3;
    }

    else if (index >= 128 && index <= 159)
    {
        currentMapData = (float)input.mapmatrix1.x; 
        chosenmap = 4;
    }
    else if (index >= 160 && index <= 191)
    {
        currentMapData = (float)input.mapmatrix1.y;
        chosenmap = 5;
    }
    else if (index >= 192 && index <= 223)
    {
        currentMapData =  (float)input.mapmatrix1.z;
        chosenmap = 6;
    }
    else if (index >= 224 && index <= 255)
    {
        currentMapData =  (float)input.mapmatrix1.w;
        chosenmap = 7;
    }
    else if (index >= 256 && index <= 287)
    {
        currentMapData =  (float)input.mapmatrix2.x;
        chosenmap = 8;
    }
    else if (index >= 288 && index <= 319)
    {
        currentMapData =  (float)input.mapmatrix2.y;
        chosenmap = 9;
    }
    else if (index >= 320 && index <= 351)
    {
        currentMapData =  (float)input.mapmatrix2.z;
        chosenmap = 10;
    }
    else if (index >= 352 && index <= 383)
    {
        currentMapData =  (float)input.mapmatrix2.w;
        chosenmap = 11;
    }
    else if (index >= 384 && index <= 415)
    {
        currentMapData =  (float)input.mapmatrix3.x;
        chosenmap = 12;
    }
    else if (index >= 416 && index <= 447)
    {
        currentMapData =  (float)input.mapmatrix3.y;
        chosenmap = 13;
    }
    else if (index >= 448 && index <= 479)
    {
        currentMapData =  (float)input.mapmatrix3.z;
        chosenmap = 14;
    }
    else if (index >= 480 && index <= 511)
    {
        currentMapData =  (float)input.mapmatrix3.w;
        chosenmap = 15;
    }*/



	

	/*
	if(currentIndex >= 0 && currentIndex <= 7)
	{	
		currentMapData = input.mapmatrix0.x;//input.one;
		if(currentIndex >= 0 && currentIndex <= 3)
		{
			int somemax = 3;
			someOtherIndex =  1 + ((somemax - someOtherIndex)*2);
		}
		else if(currentIndex >= 4 && currentIndex <= 7)
		{
			int somemax = 7;
			someOtherIndex =  0 + ((somemax - someOtherIndex)*2);
		}
	}	
	else if(currentIndex >= 8 && currentIndex <= 15)
	{
		currentMapData = input.mapmatrix0.y;//input.oneTwo;
		if(currentIndex >= 8 && currentIndex <= 11)
		{
			int somemax = 11;
			someOtherIndex =  1 + ((somemax - someOtherIndex)*2);
		}
		else if(currentIndex >= 12 && currentIndex <= 15)
		{
			int somemax = 15;
			someOtherIndex =  0 + ((somemax - someOtherIndex)*2);
		}
	}
	else if(currentIndex >= 16 && currentIndex <= 23)
	{
		currentMapData = input.mapmatrix0.z;//input.two;
		if(currentIndex >= 16 && currentIndex <= 19)
		{
			int somemax = 19;
			someOtherIndex =  1 + ((somemax - someOtherIndex)*2);
		}
		else if(currentIndex >= 20 && currentIndex <= 23)
		{
			int somemax = 23;
			someOtherIndex =  0 + ((somemax - someOtherIndex)*2);
		}
	}

	else if(currentIndex >= 24 && currentIndex <= 31)
	{
		currentMapData = input.mapmatrix0.w;//input.twoTwo;
		if(currentIndex >= 24 && currentIndex <= 27)
		{
			int somemax = 27;
			someOtherIndex =  1 + ((somemax - someOtherIndex)*2);
		}
		else if(currentIndex >= 28 && currentIndex <= 31)
		{
			int somemax = 31;
			someOtherIndex =  0 + ((somemax - someOtherIndex)*2);
		}
	}
	else if(currentIndex >= 32 && currentIndex <= 39)
	{
		currentMapData = input.mapmatrix1.x;//input.three;
		if(currentIndex >= 32 && currentIndex <= 35)
		{
			int somemax = 35;
			someOtherIndex =  1 + ((somemax - someOtherIndex)*2);
		}
		else if(currentIndex >= 36 && currentIndex <= 39)
		{
			int somemax = 39;
			someOtherIndex =  0 + ((somemax - someOtherIndex)*2);
		}
	}
	else if(currentIndex >= 40 && currentIndex <= 47)
	{
		currentMapData = input.mapmatrix1.y;//input.threeTwo;
		if(currentIndex >= 40 && currentIndex <= 43)
		{
			int somemax = 43;
			someOtherIndex =  1 + ((somemax - someOtherIndex)*2);
		}
		else if(currentIndex >= 44 && currentIndex <= 47)
		{
			int somemax = 47;
			someOtherIndex =  0 + ((somemax - someOtherIndex)*2);
		}
	}
	else if(currentIndex >= 48 && currentIndex <= 55)
	{
		currentMapData = input.mapmatrix1.z;//input.four;
		if(currentIndex >= 48 && currentIndex <= 51)
		{
			int somemax = 51;
			someOtherIndex =  1 + ((somemax - someOtherIndex)*2);
		}
		else if(currentIndex >= 52 && currentIndex <= 55)
		{
			int somemax = 55;
			someOtherIndex =  0 + ((somemax - someOtherIndex)*2);
		}
	}
	else if(currentIndex >= 56 && currentIndex <= 63)
	{
		currentMapData = input.mapmatrix1.w;//input.fourTwo;
		if(currentIndex >= 56 && currentIndex <= 59)
		{
			int somemax = 59;
			someOtherIndex =  1 + ((somemax - someOtherIndex)*2);
		}
		else if(currentIndex >= 60 && currentIndex <= 63)
		{
			int somemax = 63;
			someOtherIndex =  0 + ((somemax - someOtherIndex)*2);
		}
	}*/





	
	/*
	int theNumber = tinyChunkWidth;
	int remainder = 0;
	int totalTimes = 0;

	for (int i = 0;i <= currentIndex; i++)
	{           
		if (remainder == theNumber)
		{
			remainder = 0;
			totalTimes++;
		}
		if (totalTimes * theNumber >= currentIndex) // >=?? why not only >
		{
			break;
		}
		remainder++;
	}

	int arrayIndex = int(floor(totalTimes *0.5));


	int somemul = 0;

	switch(arrayIndex)
	{
		case 0:
			currentMapData = int(input.mapmatrix0.x);//input.one;
			somemul = 1.0;
			break;
		case 1:
			currentMapData = int(input.mapmatrix0.y);//input.oneTwo;
			somemul = 0.1;
			break;
		case 2:
			currentMapData = int(input.mapmatrix0.z);//input.two;
			somemul = 0.01;
			break;
		case 3:
			currentMapData = int(input.mapmatrix0.w);//input.twoTwo;
			somemul = 0.001;
			break;
		case 4:
			currentMapData = int(input.mapmatrix1.x);// input.three;
			somemul = 0.0001;
			break;
		case 5:
			currentMapData = int(input.mapmatrix1.y);// input.threeTwo;
			somemul = 0.00001;
			break;
		case 6:
			currentMapData = int(input.mapmatrix1.z);// input.four;
			somemul = 0.000001;
			break;
		case 7:
			currentMapData = int(input.mapmatrix1.w);// input.fourTwo;
			somemul = 0.0000001;
			break;
	}*/

	

	//currentByte = nthbit(currentMapData,someOtherIndex);
	/*int someremains = 0;
	int truncateopsval = 0;

    if (someOtherIndex == 0)
    {
        someremains = currentMapData >> 1 << 1;
        currentByte = currentMapData - someremains;
		//currentByte = nthbit(currentMapData,someOtherIndex);
    }
    else
    {
		float someData0 = currentMapData;

		for (int i = 0; i < someOtherIndex; i++)
		{
			someData0 = int(someData0 * 0.1f);
		}
        truncateopsval = int(trunc(someData0));
		//https://stackoverflow.com/questions/46312893/how-do-you-use-bit-shift-operators-to-find-out-a-certain-digit-of-a-number-in-ba
        someremains = truncateopsval >> 1 << 1;
        currentByte = truncateopsval - someremains;
		//currentByte = nthbit(currentMapData,someOtherIndex);
    }*/



	




	/*
	int theNumber = tinyChunkWidth;
	int remainder = 0;
	int totalTimes = 0;

	for (int ii = 0;ii <= currentIndex; ii++)
	{           
		if (remainder == theNumber)
		{
			remainder = 0;
			totalTimes++;
		}
		if (totalTimes * theNumber >= currentIndex) // >=?? why not only >
		{
			break;
		}
		remainder++;
	}

	int arrayIndex = int(floor(totalTimes *0.5));
		
	int baser = totalTimes;

    int someAdder = totalTimes % 2;

    int someOtherIndexOne = ((tinyChunkWidth*2)-1) - (((someOtherIndex - (tinyChunkWidth * baser)) * 2) + someAdder);

    //int somemapremains = 0;
    //int substract = 0;
    //int beforeshift = 0;*/





	/*int sometotal = tinyChunkWidth * tinyChunkHeight * tinyChunkDepth;
	int somestartindex = arrayIndex * (tinyChunkWidth + tinyChunkWidth);
	int someremainsindextwo = (sometotal) - somestartindex;

	baser = totalTimes;
	someAdder = totalTimes % 2;
	int someremainsindexthree = 3 - (((someOtherIndex - (tinyChunkWidth * baser))*2)+someAdder);

	//currentByte = nthbit(currentMapData,someOtherIndex);*/



	
	//modded version of https://stackoverflow.com/questions/13038482/get-the-decimal-part-from-a-double/13038524
    //TO ROLL BYTEMAP PELLICULE TO THE LEFT OF THE DOT TO GET THE DIGIT AT THE INDEX OF MUL
	/*float somemul = 1.0f;
	for(int ir = 1;ir < someOtherIndex;ir++)
	{
		somemul  *=0.1;
	}
    float multiplierleft = somemul;
    double doublevalueleft = currentMapData;
    double doubleresultleft = (double)floor((doublevalueleft - (int)doublevalueleft) * multiplierleft);
    double beforeremainsleft = (double)floor(doubleresultleft * 0.1f) * 10;
    currentByte = int(doubleresultleft - beforeremainsleft);*/
    //TO ROLL BYTEMAP PELLICULE TO THE LEFT OF THE DOT TO GET THE DIGIT AT THE INDEX OF MUL



	/*
	int arrayOfDigits[9];
	//int arrayOfDigits[9] = {0,0,0,0,0,0,0,0,0};

	int somemap = currentMapData;

	int tempsomemap;
	for (int i = 0; i < 9; i++) //111111111 // 6th digit //someOtherIndex
	{
		tempsomemap = somemap;
		somemap = (int)(floor(somemap * 0.1f));
		arrayOfDigits[i] = (int)(tempsomemap - (somemap * 10));
	}

	currentByte = arrayOfDigits[someOtherIndex];*/


    /*double arrayOfDigits[4];// = {0,0,0,0,0,0,0,0,0};

    double somemap = currentMapData;

    double tempsomemap;
    for (int i = 0; i < 4; i++) //111111111 // 6th digit //someOtherIndex
    {
        tempsomemap = somemap;
        somemap = (int)(floor(somemap * 0.1f));
        arrayOfDigits[i] = (int)(tempsomemap - (somemap * 10));
    }

    currentByte = arrayOfDigits[someOtherIndex];*/









	/*int someremains = 0;
	int truncateopsval = 0;

    if (someOtherIndex == 0)
    {
        someremains = currentMapData >> 1 << 1;
        currentByte = currentMapData - someremains;
		//currentByte = nthbit(currentMapData,someOtherIndex);
    }
    else
    {
		float someData0 = currentMapData;

		for (int i = 0; i < someOtherIndex; i++)
		{
			someData0 = int(someData0 * 0.1f);
		}
        truncateopsval = int(trunc(someData0));
		//https://stackoverflow.com/questions/46312893/how-do-you-use-bit-shift-operators-to-find-out-a-certain-digit-of-a-number-in-ba
        someremains = truncateopsval >> 1 << 1;
        currentByte = truncateopsval - someremains;
		//currentByte = nthbit(currentMapData,someOtherIndex);
    }*/

	/*//double someotherbyte = (currentMapData * someothermul0) - (Math.Round(currentMapData) * someothermul0); //51.111 - 50.0 => 1.111 => 1.0
    double multiplierleft = someothermul0;
    double doublevalueleft = currentMapData; //5911111111111111111111111111111111.1111111111111111111111111111111135f;
    double doubleresultleft = (double)floor((doublevalueleft - floor(doublevalueleft)) * multiplierleft);
    double beforeremainsleft = (double)floor(doubleresultleft * 0.1) * 10;
    currentByte = doubleresultleft - beforeremainsleft;*/





	/*double arrayOfDigits[4];// = {0,0,0,0,0,0,0,0,0};

    double somemap = currentMapData;

    double tempsomemap;
    for (int i = 0; i < 4; i++) //111111111 // 6th digit //someOtherIndex
    {
        tempsomemap = somemap;
        somemap = (floor(somemap * 0.1));
        arrayOfDigits[i] = (tempsomemap - (somemap * 10));
    }

    currentByte = arrayOfDigits[someOtherIndex];*/


	



	



	
    /*double someothermul0 = 10.0;
    double someothermul1 = 1.0;

    if (someOtherIndex == 0)
    {
        someothermul0 = 10.0;
        someothermul1 = 0.1f;
    }
    else if (someOtherIndex == 1)
    {
        someothermul0 = 100.0;
        someothermul1 = 0.01;
    }
    else if (someOtherIndex == 2)
    {
        someothermul0 = 1000.0;
        someothermul1 = 0.001;
    }
    else if (someOtherIndex == 3)
    {
        someothermul0 = 10000.0;
        someothermul1 = 0.0001;
    }*/


    //currentMapData = 51110.0;
    /*currentMapData *= 0.0001f;
    currentMapData = round(currentMapData * 100000) * 0.00001; // 5.1111
    double double_value = currentMapData;
    currentByte = (int)((double_value - (int)double_value) * someothermul0) - (int)((floor((int)((double_value - (int)double_value) * someothermul0) * 0.1)*10));// == 0.345f
	*/




	/*int someremains = 0;
	int truncateopsval = 0;

    if (someOtherIndex == 0)
    {
        someremains = currentMapData >> 1 << 1;
        currentByte = currentMapData - someremains;
		//currentByte = nthbit(currentMapData,someOtherIndex);
    }
    else
    {
		float someData0 = currentMapData;

		for (int i = 0; i < someOtherIndex; i++)
		{
			someData0 = int(someData0 * 0.1f);
		}
        truncateopsval = int(trunc(someData0));
		//https://stackoverflow.com/questions/46312893/how-do-you-use-bit-shift-operators-to-find-out-a-certain-digit-of-a-number-in-ba
        someremains = truncateopsval >> 1 << 1;
        currentByte = truncateopsval - someremains;
		//currentByte = nthbit(currentMapData,someOtherIndex);
    }*/

	//double someotherbyte = (currentMapData * someothermul0) - (Math.Round(currentMapData) * someothermul0); //51.111 - 50.0 => 1.111 => 1.0
    /*double multiplierleft = someothermul0;
    double doublevalueleft = currentMapData; //5911111111111111111111111111111111.1111111111111111111111111111111135f;
    double doubleresultleft = (double)floor((doublevalueleft - floor(doublevalueleft)) * multiplierleft);
    double beforeremainsleft = (double)floor(doubleresultleft * 0.1) * 10;
    currentByte = doubleresultleft - beforeremainsleft;*/

	//currentByte = nthbit(currentMapData,someOtherIndex);

	/*
	currentMapData *= 0.0001f;
    currentMapData = round(currentMapData * 100000) * 0.00001; // 5.1111
    double double_value = currentMapData;
    currentByte = ((double_value - trunc(double_value)) * someothermul0) - trunc((trunc(trunc((double_value -trunc(double_value)) * someothermul0) * 0.1f) * 10));// == 0.345f
	*/

	/*double someothermul0 = 10.0;
    double someothermul1 = 1.0;

	if (someOtherIndex == 0)
    {
        someothermul0 = 10.0;
        someothermul1 = 1.0;
    }
    else if (someOtherIndex == 1)
    {
        someothermul0 = 100.0;
        someothermul1 = 0.1;
    }
    else if (someOtherIndex == 2)
    {
        someothermul0 = 1000.0;
        someothermul1 = 0.01;
    }
    else if (someOtherIndex == 3)
    {
        someothermul0 = 10000.0;
        someothermul1 = 0.001;
    }

    currentMapData = 51111.0;
    currentMapData *= 0.0001f;
    currentMapData = round(currentMapData * 100000) * 0.00001; // 5.1111
    double double_value = currentMapData;
    currentByte = ((double_value - floor(double_value)) * someothermul0) - floor((floor(floor((double_value - floor(double_value)) * someothermul0) * 0.1f) * 10));// == 0.345f*/




	/*double arrayOfDigits[4];// = {0,0,0,0,0,0,0,0,0};

    double somemap = currentMapData;

    double tempsomemap;
    for (int i = 0; i < 4; i++) //111111111 // 6th digit //someOtherIndex
    {
        tempsomemap = somemap;
        somemap = (floor(somemap * 0.1));
		somemap= (tempsomemap - (somemap * 10));
        arrayOfDigits[i] = somemap;
    }

    currentByte = floor(arrayOfDigits[someOtherIndex]);*/










	
	/*double arrayOfDigits[4];
	double somemap = currentMapData;
	double tempsomemap;

    for (int i = 0; i < 4; i++) //111111111 // 6th digit //someOtherIndex
    {
        tempsomemap = somemap;
        somemap = (somemap * 0.1);
        double somevalue = floor((double)(int)((somemap - (int)somemap) * someothermul0)) * someothermul1;
        somevalue = somevalue - (floor(somevalue * 0.1) * 10);
        arrayOfDigits[i] = somevalue;
    }

    double someotherindex1 = 4 - someOtherIndex;*/















	/*
	double someothermul0 = 10.0;
	double someothermul1 = 1.0;

	if (someOtherIndex == 0)
	{
		someothermul0 = 10.0;
		someothermul1 = 0.1;
		currentByte = (currentMapData) - (floor(currentMapData * someothermul1)*10);

	}
	else if (someOtherIndex == 1)
	{
		someothermul0 = 100.0;
		someothermul1 = 0.01;
		currentByte = (currentMapData*0.1) - (floor(currentMapData * someothermul1)*10);
	}
	else if (someOtherIndex == 2)
	{
		someothermul0 = 1000.0;
		someothermul1 = 0.001;
		currentByte = (currentMapData*0.01) - (floor(currentMapData * someothermul1)*10); // 51111.0 * 0.001 = 51.111
	}
	else if (someOtherIndex == 3)
	{
		someothermul0 = 10000.0;
		someothermul1 = 0.0001;
		currentByte = (currentMapData*0.001) - (floor(currentMapData * someothermul1)*10);
	}*/












	
    /*if (someOtherIndex == 0)
    {
        someothermul0 = 10.0;
        someothermul1 = 1.0;
    }
    else if (someOtherIndex == 1)
    {
        someothermul0 = 100.0;
        someothermul1 = 0.1;
    }
    else if (someOtherIndex == 2)
    {
        someothermul0 = 1000.0;
        someothermul1 = 0.01;
    }
    else if (someOtherIndex == 3)
    {
        someothermul0 = 10000.0;
        someothermul1 = 0.001;
    }
    //currentMapData = 51234.0;
    currentMapData *= 0.0001f;
    currentMapData = round(currentMapData * 100000) * 0.00001; // 5.1111
    double double_value = currentMapData;
	currentByte = ((double_value - floor(double_value)) * someothermul0) - floor((floor(floor((double_value - floor(double_value)) * someothermul0) * 0.1f) * 10));// == 0.345f
	*/

















	/*
	someOtherIndex = 4 - someOtherIndex;


	double someothermul0 = 10.0;
	double someothermul1 = 1.0;

	if (someOtherIndex == 0)
	{
		someothermul0 = 10.0;
		someothermul1 = 1.0;
		//currentByte = (currentMapData) - (floor(currentMapData * someothermul1)*10);

	}
	else if (someOtherIndex == 1)
	{
		someothermul0 = 100.0;
		someothermul1 = 0.1;
		//currentByte = (currentMapData*0.1) - (floor(currentMapData * someothermul1)*10);
	}
	else if (someOtherIndex == 2)
	{
		someothermul0 = 1000.0;
		someothermul1 = 0.01;
		//currentByte = (currentMapData*0.01) - (floor(currentMapData * someothermul1)*10); // 51111.0 * 0.001 = 51.111
	}
	else if (someOtherIndex == 3)
	{
		someothermul0 = 10000.0;
		someothermul1 = 0.001;
		//currentByte = (currentMapData*0.001) - (floor(currentMapData * someothermul1)*10);
	}*/


	



	/*if(arrayOfDigits[someOtherIndex] == 1.0) // && input.indexPos.w == 0
	{	
		input.position.x += input.instancePosition.x;
		input.position.y += input.instancePosition.y;
		input.position.z += input.instancePosition.z;

		output.position = mul(input.position, world);
		output.position = mul(output.position, view);
		output.position = mul(output.position, proj);
		output.color = input.color;
	}
	else if(arrayOfDigits[someOtherIndex] == 0)
	{
		input.position.x = input.instancePosition.x;
		input.position.y = input.instancePosition.y;
		input.position.z = input.instancePosition.z;

		output.position = mul(input.position, world);
		output.position = mul(output.position, view);
		output.position = mul(output.position, proj);
		output.color = float4(0.35f,0.95f,0.35f,1.0f) * input.colorsNFaces;

		//if(facetype == 0)
		//{
		//	output.color = float4(0.15f,0.15f,0.95f,1.0f);
		//}
		//else
		//{
		//}

		//TO READD
		//input.color.y *= trunc(input.colorsNFaces.w)*somemul;
		//output.color = 	input.color;
		//TO READD
	}
	else
	{
		input.position.x += input.instancePosition.x;
		input.position.y += input.instancePosition.y;
		input.position.z += input.instancePosition.z;

		output.position = mul(input.position, world);
		output.position = mul(output.position, view);
		output.position = mul(output.position, proj);
		output.color = float4(0.05f,0.05f,0.05f,1.0f);

		//if(facetype == 0)
		//{
		//	output.color = float4(0.15f,0.15f,0.95f,1.0f);
		//}
		//else
		//{
		//}

		//TO READD
		//input.color.y *= trunc(input.colorsNFaces.w)*somemul;
		//output.color = 	input.color;
		//TO READD
	}*/





	