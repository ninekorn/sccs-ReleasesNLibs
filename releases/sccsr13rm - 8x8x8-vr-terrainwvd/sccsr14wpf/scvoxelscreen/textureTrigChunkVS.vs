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
	float4 indexPos : POSITION1;
	float4 color : COLOR0;
	float3 normal : NORMAL0;
	float2 tex : TEXCOORD0;
	int one : PSIZE0;	
	int two : PSIZE1;	
	int three : PSIZE2;	
	int four : PSIZE3;	
	int oneTwo : PSIZE4;	
	int twoTwo : PSIZE5;	
	int threeTwo : PSIZE6;	
	int fourTwo : PSIZE7;	
	float4 instancePosition1 : POSITION2; 
	int xindex : PSIZE8;	
	int yindex : PSIZE9;
	float4 instanceRadRotFORWARD : POSITION3;
	float4 instanceRadRotRIGHT : POSITION4;
	float4 instanceRadRotUP : POSITION5;
};

//row_major matrix instancePosition1 : WORLD;

struct PixelInputType
{ 
	//float4 position : SV_POSITION;
	//float4 color : COLOR0;
	//float3 normal : NORMAL0;
	//float2 tex : TEXCOORD0;

	float4 position : SV_POSITION;
	float4 indexPos : POSITION1;
	float4 color : COLOR0;
	float3 normal : NORMAL0;
	float2 tex : TEXCOORD0;
	int one : PSIZE0;	
	int two : PSIZE1;	
	int three : PSIZE2;	
	int four : PSIZE3;	
	int oneTwo : PSIZE4;	
	int twoTwo : PSIZE5;
	int threeTwo : PSIZE6;	
	int fourTwo : PSIZE7;	
	float4 instancePosition1 : POSITION2;
	int xindex : PSIZE8;	
	int yindex : PSIZE9;
	float4 instanceRadRotFORWARD : POSITION3;
	float4 instanceRadRotRIGHT : POSITION4;
	float4 instanceRadRotUP : POSITION5;
};

static float4 mod_input_vertex_pos;

static float3 forwardDir;
static float3 rightDir;
static float3 upDir;

static float3 MOVINGPOINT;
static float3 vertPos;
static float diffX;
static float diffY;
static float diffZ;

//float planeSize = 0.1f;

//static int mapWidth = 4;
//static int mapHeight = 4;
//static int mapDepth = 4;

static int tinyChunkWidth = 2;
static int tinyChunkHeight = 2;
static int tinyChunkDepth = 1;

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

int BitShiftIndex(int x, int y, int z, VertexInputType input)
{
	int currentMapData= 0;
	int currentByte= 0;
	float someData0= 0;
	int currentIndex = 0;
	int someOtherIndex = 0;
	int theNumber = 0;
	int remainder = 0;
	int totalTimes = 0;
	int baser = 0;
	int someAdder = 0;
	int testera = 0;
	int substract = 0;
	int before0 = 0;
	int arrayIndex= 0;

	currentIndex = x + (tinyChunkWidth * (y + (tinyChunkHeight * z)));
	someOtherIndex = currentIndex;

	theNumber = tinyChunkWidth;

	remainder = 0;
	totalTimes = 0;

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

	arrayIndex = int(floor(totalTimes *0.5));

	switch(arrayIndex)
	{
		case 0:
			currentMapData = input.one;
			break;
		case 1:
			currentMapData = input.oneTwo;
			break;
		case 2:
			currentMapData = input.two;
			break;
		case 3:
			currentMapData = input.twoTwo;
			break;
		case 4:
			currentMapData = input.three;
			break;
		case 5:
			currentMapData = input.threeTwo;
			break;
		case 6:
			currentMapData = input.four;
			break;
		case 7:
			currentMapData = input.fourTwo;
			break;
	}

	//0-4-1-5-2-6-3-7
	//8-12-9-13-10-14-11-15
	//16-20-17-21-18-22-19-23
	//24-28-25-29-26-30-27-31
	//32-36-33-37-34-38-35-39
	//40-44-41-45-42-46-43-47
	//48-52-49-53-50-54-51-55
	//56-60-57-61-58-62-59-63

	baser = totalTimes;

	someAdder = totalTimes % 2;

	someOtherIndex = 7 - (((someOtherIndex - (tinyChunkWidth * baser)) * 2) + someAdder);

		testera = 0;
		substract = 0;
		before0 = 0;

	if (someOtherIndex == 0)
	{
		testera = currentMapData >> 1 << 1;
		currentByte = currentMapData - testera;
	}
	else
	{
		someData0 = currentMapData;

		for (int i = 0; i < someOtherIndex; i++)
		{
			someData0 = int(someData0 * 0.1f);
		}

		before0 = int(trunc(someData0));
		//https://stackoverflow.com/questions/46312893/how-do-you-use-bit-shift-operators-to-find-out-a-certain-digit-of-a-number-in-ba
		testera = before0 >> 1 << 1;
		currentByte = before0 - testera;
	}

	return currentByte;
}






//[maxvertexcount(96)] 
PixelInputType TextureVertexShader(VertexInputType input)
{ 
	int arrayRow = 0;
	int x = 0;
	int y = 0;
	int z = 0;
	int currentMapData= 0;
	int currentByte= 0;
	float someData0= 0;
	int currentIndex = 0;
	int someOtherIndex = 0;
	int theNumber = 0;
	int remainder = 0;
	int totalTimes = 0;
	int baser = 0;
	int someAdder = 0;
	int testera = 0;
	int substract = 0;
	int before0 = 0;
	int arrayIndex= 0;

	PixelInputType output;
    input.position.w = 1.0f;

	 /*x = input.indexPos.x; 
	 y = input.indexPos.y; 
	 z = input.indexPos.z;

	 currentIndex = x + (tinyChunkWidth * (y + (tinyChunkHeight * z)));
	 someOtherIndex = currentIndex;

	 theNumber = tinyChunkWidth;
	 remainder = 0;
	 totalTimes = 0;

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
	
	arrayIndex = int(floor(totalTimes *0.5));


	switch(arrayIndex)
	{
		case 0:
			currentMapData = input.one;
			arrayRow = 0;
			break;
		case 1:
			currentMapData = input.oneTwo;
			arrayRow = 1;
			break;
		case 2:
			currentMapData = input.two;
			arrayRow = 2;
			break;
		case 3:
			currentMapData = input.twoTwo;
			arrayRow = 3;
			break;
		case 4:
			currentMapData = input.three;
			arrayRow = 4;
			break;
		case 5:
			currentMapData = input.threeTwo;
			arrayRow = 5;
			break;
		case 6:
			currentMapData = input.four;
			arrayRow = 6;
			break;
		case 7:
			currentMapData = input.fourTwo;
			arrayRow = 7;
			break;
	}

	//0-4-1-5-2-6-3-7
	//8-12-9-13-10-14-11-15
	//16-20-17-21-18-22-19-23
	//24-28-25-29-26-30-27-31
	//32-36-33-37-34-38-35-39
	//40-44-41-45-42-46-43-47
	//48-52-49-53-50-54-51-55
	//56-60-57-61-58-62-59-63

	 baser = totalTimes;

	 someAdder = totalTimes % 2;

	someOtherIndex = 7- (((someOtherIndex - (tinyChunkWidth * baser))*2)+someAdder);

	 testera = 0;
	 substract = 0;
	 before0 = 0;

    if (someOtherIndex == 0)
    {
        testera = currentMapData >> 1 << 1;
        currentByte = currentMapData - testera;
    }
    else
    {
		someData0 = currentMapData;

		for (int i = 0; i < someOtherIndex; i++)
		{
			someData0 = int(someData0 * 0.1f);
		}
        before0 = int(trunc(someData0));
		//https://stackoverflow.com/questions/46312893/how-do-you-use-bit-shift-operators-to-find-out-a-certain-digit-of-a-number-in-ba
        testera = before0 >> 1 << 1;
        currentByte = before0 - testera;
    }

	//currentByte = 1;

	if(currentByte == 1)
	{	
		input.position.x += input.instancePosition1.x;
		input.position.y += input.instancePosition1.y;
		input.position.z += input.instancePosition1.z;

		output.position = mul(input.position, world);
		output.position = mul(output.position, view);
		output.position = mul(output.position, proj);
		output.color = input.color;
	}
	else
	{
		input.position.x = input.instancePosition1.x;
		input.position.y = input.instancePosition1.y;
		input.position.z = input.instancePosition1.z;

		output.position = mul(input.position, world);
		output.position = mul(output.position, view);
		output.position = mul(output.position, proj);
		output.color = float4(0.5f,0.5f,0.5f,1);




		/*if(BitShiftIndex(x + 1, y, z, input) == 1)
		{
			input.position.x += input.instancePosition1.x;
			input.position.y += input.instancePosition1.y;
			input.position.z += input.instancePosition1.z;

			output.position = mul(input.position, world);
			output.position = mul(output.position, view);
			output.position = mul(output.position, proj);

			output.color = input.color;
		}
		else
		{
			input.position.x = input.instancePosition1.x;
			input.position.y = input.instancePosition1.y;
			input.position.z = input.instancePosition1.z;

			output.position = mul(input.position, world);
			output.position = mul(output.position, view);
			output.position = mul(output.position, proj);
			output.color = float4(0.5f,0.5f,0.5f,1);
		}*/


		/*if(BitShiftIndex(x + 1, y, z, input) == 1)
		{
			input.position.x = input.instancePosition1.x;
			input.position.y = input.instancePosition1.y;
			input.position.z = input.instancePosition1.z;

			output.position = mul(input.position, world);
			output.position = mul(output.position, view);
			output.position = mul(output.position, proj);
			output.color = input.color * float4(0.5f,0.5f,0.5f,1);
		}*/

		/*
		if(BitShiftIndex(x - 1, y, z, input) == 1)
		{
			input.position.x = input.instancePosition1.x;
			input.position.y = input.instancePosition1.y;
			input.position.z = input.instancePosition1.z;

			output.position = mul(input.position, world);
			output.position = mul(output.position, view);
			output.position = mul(output.position, proj);
			output.color = input.color * float4(0.5f,0.5f,0.5f,1);
		}
		
		if(BitShiftIndex(x , y + 1, z, input) == 1)
		{
			input.position.x = input.instancePosition1.x;
			input.position.y = input.instancePosition1.y;
			input.position.z = input.instancePosition1.z;

			output.position = mul(input.position, world);
			output.position = mul(output.position, view);
			output.position = mul(output.position, proj);
			output.color = input.color * float4(0.5f,0.5f,0.5f,1);
		}

		if(BitShiftIndex(x , y - 1, z, input) == 1)
		{
			input.position.x = input.instancePosition1.x;
			input.position.y = input.instancePosition1.y;
			input.position.z = input.instancePosition1.z;

			output.position = mul(input.position, world);
			output.position = mul(output.position, view);
			output.position = mul(output.position, proj);
			output.color = input.color * float4(0.5f,0.5f,0.5f,1);
		}*/


		//var indexToCheckForRightFace = input.instancePosition1.x + 1;

		/*if (IsTransparent(input.instancePosition1.x + 1, input.instancePosition1.y, input.instancePosition1.z))
        {
			input.position.x += input.instancePosition1.x;
			input.position.y += input.instancePosition1.y;
			input.position.z += input.instancePosition1.z;

			output.position = mul(input.position, world);
			output.position = mul(output.position, view);
			output.position = mul(output.position, proj);
			output.color = input.color;

            //map[x + SC_Globals.tinyChunkWidth * (y + SC_Globals.tinyChunkHeight * z)] = 1;
            //MainWindow.MessageBox((IntPtr)0, "++++", "sccoresystems message", 0);

            //offset1 = up * SC_Globals.planeSize;
            //offset2 = forward * SC_Globals.planeSize;
            //createRightFace(start + right * SC_Globals.planeSize, offset1, offset2, currentPosition, x, y, z, 1);
            //MainWindow.MessageBox((IntPtr)0, "++++", "sccoresystems messahge", 0);
        }*/

		/*input.position.x += input.instancePosition1.x;
		input.position.y += input.instancePosition1.y;
		input.position.z += input.instancePosition1.z;

		output.position = mul(input.position, world);
		output.position = mul(output.position, view);
		output.position = mul(output.position, proj);
		output.color = input.color;*/

		/*input.position.x = input.instancePosition1.x;
		input.position.y = input.instancePosition1.y;
		input.position.z = input.instancePosition1.z;

		output.position = mul(input.position, world);
		output.position = mul(output.position, view);
		output.position = mul(output.position, proj);
		output.color = input.color * float4(0.5f,0.5f,0.5f,1);
	}*/


	
	/*//PixelInputType output;
    
	input.position.w = 1.0f;

	mod_input_vertex_pos = input.position;

	mod_input_vertex_pos.x += input.instancePosition1.x;
	mod_input_vertex_pos.y += input.instancePosition1.y;
	mod_input_vertex_pos.z += input.instancePosition1.z;
	mod_input_vertex_pos.w = 1.0f;

	forwardDir = float3(input.instanceRadRotFORWARD.x, input.instanceRadRotFORWARD.y, input.instanceRadRotFORWARD.z);
	rightDir = float3(input.instanceRadRotRIGHT.x, input.instanceRadRotRIGHT.y, input.instanceRadRotRIGHT.z); 
	upDir = float3(input.instanceRadRotUP.x, input.instanceRadRotUP.y, input.instanceRadRotUP.z);

	//cross(world_up,float3(input.instanceRadRotFORWARD.x, input.instanceRadRotFORWARD.y, input.instanceRadRotFORWARD.z));
	//float3(input.instanceRadRotFORWARD.x, input.instanceRadRotFORWARD.y, input.instanceRadRotFORWARD.z); //world_forward;//

	MOVINGPOINT = float3(input.instancePosition1.x, input.instancePosition1.y, input.instancePosition1.z);
	vertPos = float3(mod_input_vertex_pos.x, mod_input_vertex_pos.y, mod_input_vertex_pos.z);

	diffX = vertPos.x - input.instancePosition1.x;
	diffY = vertPos.y - input.instancePosition1.y;
	diffZ = vertPos.z - input.instancePosition1.z;

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

	output.instancePosition1.x = input.instancePosition1.x;
	output.instancePosition1.y = input.instancePosition1.y;
	output.instancePosition1.z = input.instancePosition1.z;

	output.instanceRadRotFORWARD.x = input.instanceRadRotFORWARD.x;
	output.instanceRadRotFORWARD.y = input.instanceRadRotFORWARD.y;
	output.instanceRadRotFORWARD.z = input.instanceRadRotFORWARD.z;
	
	output.instanceRadRotRIGHT.x = input.instanceRadRotRIGHT.x;
	output.instanceRadRotRIGHT.y = input.instanceRadRotRIGHT.y;
	output.instanceRadRotRIGHT.z = input.instanceRadRotRIGHT.z;
	
	output.instanceRadRotUP.x = input.instanceRadRotUP.x;
	output.instanceRadRotUP.y = input.instanceRadRotUP.y;
	output.instanceRadRotUP.z = input.instanceRadRotUP.z;









	//float xer = dot(output.normal, float3(1, 0.25, 0.4)); //float3(0.75, 0.50, 0.25));  //float3(1, 0.25, 0.4));   //float3(0.35, 0.15, 0.25));	
	//xer *= 10.25;
	//float xnormvalue = dot(input.normal,input.instanceRadRotUP); // input.instanceRadRotUP);	
	//xnormvalue = xnormvalue * 0.5 - 0.5;	
	//if(xnormvalue < 0.95)
	//{
	//	xnormvalue = 0.95;
	//}
	//float x0 = input.color.x;
	//float y0 = input.color.y;
	//float z0 = input.color.z;
	//float w0 = input.color.w;
	//float xx = xer;
	//float3 color = lerp(float3(input.color.x*0.90*xx, input.color.y*0.90*xx, input.color.z*0.90*xx), float3(input.color.x*0.95*xx, input.color.y*0.95*xx, input.color.z*0.95*xx), xnormvalue);
	//input.color.xyz = color;
	//input.color.w = 1.0;
	//output.color = input.color;
	//output.tex = output.tex / float2(10,10);
	//float2 test = output.tex;
	//test.x = test.x + (2 *(1920/10));
	//test.y = test.y + (2 *(1080/10));
	//output.tex =test;
	//output.one = input.one;
	//output.two = input.two;
    //return output;










	input.position.x += input.instancePosition1.x;
	input.position.y += input.instancePosition1.y;
	input.position.z += input.instancePosition1.z;

	output.position = mul(input.position, world);
	output.position = mul(output.position, view);
	output.position = mul(output.position, proj);









	output.color = input.color;

	output.one = input.one;
	output.two = input.two;
	output.three = input.three;
	output.four = input.four;
	output.oneTwo = input.oneTwo;
	output.twoTwo = input.twoTwo;
	output.threeTwo = input.threeTwo;
	output.fourTwo = input.fourTwo;
	output.instancePosition1 = input.instancePosition1;

	output.xindex = input.xindex;
	output.yindex = input.yindex;

	output.tex = input.tex;


		//output.normal = input.normal;
	//output.normal = mul(input.normal, world);
	//output.normal = normalize(output.normal);





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