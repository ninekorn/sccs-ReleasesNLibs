//MADE BY STEVE CHASSÉ AKA NINEKORN AKA 9


cbuffer MatrixBuffer :register(b0)
{
	float4x4 world;
	float4x4 view;
	float4x4 proj;
};


cbuffer OVRDir :register(b2)
{
	float4 ovrdirf;
	float4 ovrdiru;
	float4 ovrdirr;
	float4 ovrpos;
	
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


static int heightmapw = 4;
static int heightmaph = 4;
static int heightmapd = 1;

float planeSize = 0.05f;
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



static const float PI = 3.1415926535897932384626433832795f;
float DegreeToRadian(float angle)
{
   return PI * angle / 180.0f;
}

float RadianToDegree(float angle)
{
  return angle * (180.0f / PI);
}

//stackoverflow 14607640

float3 rotateveczaxis (float x, float y, float z,float angle)
{
	angle = DegreeToRadian(angle);
	float somenewx = (x * cos(angle)) - (y * sin(angle));
	float somenewy = (x * sin(angle)) + (y * cos(angle));
	float somenewz = z;

	return float3(somenewx,somenewy,somenewz);
}


float3 rotatevecyaxis (float x, float y, float z,float angle)
{	
	angle = DegreeToRadian(angle);
	float somenewx = (x * cos(angle)) + (z * sin(angle));
	float somenewy = y;
	float somenewz = (-x * sin(angle)) + (z * cos(angle));

	return float3(somenewx,somenewy,somenewz);
}



float3 rotatevecxaxis (float x, float y, float z,float angle)
{	
	angle = DegreeToRadian(angle);
	float somenewx = x;
	float somenewy = (y * cos(angle)) - (z * sin(angle));
	float somenewz = (y * sin(angle)) + (z * cos(angle));

	return float3(somenewx,somenewy,somenewz);
}





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
	
	somecountermul = currentIndex;


	int heightmapmul = currentIndex / 1; // 4





    
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







	
	input.position.w = 1.0f;

	//input.position.x = input.position.x * -1;

	mod_input_vertex_pos = input.position;

	mod_input_vertex_pos.x += input.instancePosition.x;
	mod_input_vertex_pos.y += input.instancePosition.y;
	mod_input_vertex_pos.z += input.instancePosition.z;
	mod_input_vertex_pos.w = 1.0f;

	//mod_input_vertex_pos.xyz = mod_input_vertex_pos.xyz + (forwardDir * (someheightmapvalue * 0.000001));
	//mod_input_vertex_pos.xyz = mod_input_vertex_pos.xyz + (float3(0,0,1) * (someheightmapvalue * 0.01));

	/*forwardDir = float3(input.instanceRadRotFORWARD.x, input.instanceRadRotFORWARD.y, input.instanceRadRotFORWARD.z);
	rightDir = float3(input.instanceRadRotRIGHT.x, input.instanceRadRotRIGHT.y, input.instanceRadRotRIGHT.z); 
	upDir = float3(input.instanceRadRotUP.x, input.instanceRadRotUP.y, input.instanceRadRotUP.z);*/

	forwardDir = input.instanceRadRotFORWARD;
	rightDir = input.instanceRadRotRIGHT;
	upDir = input.instanceRadRotUP;

	//mod_input_vertex_pos.z = mod_input_vertex_pos.z + (forwardDir.z * (someheightmapvalue * 0.000001));
	//mod_input_vertex_pos += someheightmapvalue;

	//float2 noise = (frac(sin(dot(float2(input.position.x,input.position.z) ,float2(12.9898,78.233)*2.0)) * 43758.5453));
	//float test = abs(noise.x + noise.y) * 0.5 * 0.001;
	//input.color = float4(input.color.x + (input.position.x*0.1),input.color.y+ (input.position.y*0.1),input.color.z+ (input.position.z*0.1),input.color.w);
   
   


	MOVINGPOINT = float3(input.instancePosition.x, input.instancePosition.y, input.instancePosition.z);
	

	/*double angle = Math.Atan2(modelPosition.X - cameraPosition.X, modelPosition.Z - cameraPosition.Z) * (180.0f / Math.PI);
	// Convert rotation into radians.
	float rotation = (float)angle * 0.0174532925f;
	// Setup the rotation the billboard at the origin using the world matrix.
	Matrix.RotationY(rotation, out worldMatrix);
	// Setup the translation matrix from the billboard model.
	Matrix translationMatrix = Matrix.Translation(modelPosition.X, modelPosition.Y, modelPosition.Z);
	// Finally combine the rotation and translation matrices to create the final world matrix for the billboard model.
	Matrix.Multiply(ref worldMatrix, ref translationMatrix, out worldMatrix);
    */                  
	






	//double angle = atan2(MOVINGPOINT.x - ovrpos.x, MOVINGPOINT.z - ovrpos.z) * (180.0f / PI);


	//double angle = atan2(MOVINGPOINT.x - ovrpos.x, MOVINGPOINT.z - ovrpos.z) * (180.0f / PI);
	//float rotation = (float)angle * 0.0174532925f;
	//float3 zerovec = float3(mod_input_vertex_pos.x,mod_input_vertex_pos.y,mod_input_vertex_pos.z);
	//float3 rotvec = rotatevecyaxis(MOVINGPOINT.x,MOVINGPOINT.y,MOVINGPOINT.z,angle);
	//float3 somepos = mul(rotvec,MOVINGPOINT.xyz);
	//MOVINGPOINT = somepos;
	//double angle = atan2(input.position.x - ovrpos.x, input.position.z - ovrpos.z) * (180.0f / PI);
	//MOVINGPOINT = rotatevecyaxis(MOVINGPOINT.x,MOVINGPOINT.y,MOVINGPOINT.z,angle);
	//MOVINGPOINT = rotatevecxaxis(MOVINGPOINT.x,MOVINGPOINT.y,MOVINGPOINT.z,angle);
	//MOVINGPOINT = rotatevecyaxis(MOVINGPOINT.x,MOVINGPOINT.y,MOVINGPOINT.z,angle);
	//MOVINGPOINT = rotateveczaxis(MOVINGPOINT.x,MOVINGPOINT.y,MOVINGPOINT.z,angle);
	//float angledegyx = ((atan2(rightDir.y,rightDir.x) - atan2(ovrdirr.y,ovrdirr.x))*180)/PI;
	//float angledeg = ((atan2(rightDir.z,rightDir.x) - atan2(ovrdirr.z,ovrdirr.x))*180)/PI;
	//MOVINGPOINT = rotatevecxaxis(MOVINGPOINT.x,MOVINGPOINT.y,MOVINGPOINT.z,angledeg);

	//MOVINGPOINT = MOVINGPOINT + (-ovrdirf.xyz * (1*planeSize* 10));
	//MOVINGPOINT = MOVINGPOINT + (ovrdiru.xyz *  (1*planeSize));
	//MOVINGPOINT = MOVINGPOINT + (ovrdirr.xyz *  (1*planeSize));
	//float3 somepoint = rotatevecxaxis(MOVINGPOINT.x,MOVINGPOINT.y,MOVINGPOINT.z,angle);
	//MOVINGPOINT.xyz = MOVINGPOINT.xyz + (float3(0,0,1) * (someheightmapvalue * 0.001));
	//float3 somepoint = rotatevecxaxis(MOVINGPOINT.x,MOVINGPOINT.y,MOVINGPOINT.z,45);
	//MOVINGPOINT.z = somepoint.z;

	//double angle = atan2(input.position.x - ovrpos.x, input.position.z - ovrpos.z) * (180.0f / PI);
	//float3 somepoint = rotatevecxaxis(MOVINGPOINT.x,MOVINGPOINT.y,MOVINGPOINT.z,angle);
	//MOVINGPOINT.z = somepoint.z;
	//MOVINGPOINT.x = somepoint.x;

	/*
	float4x4 worldviewmatrix = mul(world,view);
	float3 positionvs = MOVINGPOINT + float3(worldviewmatrix._41,worldviewmatrix._42,worldviewmatrix._43);
	float3 finalpos = mul(float4(positionvs,1.0), proj);
	MOVINGPOINT = finalpos;*/



	
	/*double angle = Math.Atan2(modelPosition.X - cameraPosition.X, modelPosition.Z - cameraPosition.Z) * (180.0f / Math.PI);
	// Convert rotation into radians.
	float rotation = (float)angle * 0.0174532925f;
	// Setup the rotation the billboard at the origin using the world matrix.
	Matrix.RotationY(rotation, out worldMatrix);
	// Setup the translation matrix from the billboard model.
	Matrix translationMatrix = Matrix.Translation(modelPosition.X, modelPosition.Y, modelPosition.Z);
	// Finally combine the rotation and translation matrices to create the final world matrix for the billboard model.
	Matrix.Multiply(ref worldMatrix, ref translationMatrix, out worldMatrix);
    */                  

	/*double anglex = atan2(MOVINGPOINT.y - ovrpos.y, MOVINGPOINT.z - ovrpos.z) * (180.0f / PI);
	double angley = atan2(MOVINGPOINT.x - ovrpos.x, MOVINGPOINT.z - ovrpos.z) * (180.0f / PI);
	double anglez = atan2(MOVINGPOINT.z - ovrpos.z, MOVINGPOINT.z - ovrpos.z) * (180.0f / PI);

	float3 somepointx = rotatevecxaxis(MOVINGPOINT.x,MOVINGPOINT.y,MOVINGPOINT.z,anglex);
	float3 somepointy = rotatevecyaxis(MOVINGPOINT.x,MOVINGPOINT.y,MOVINGPOINT.z,angley);
	float3 somepointz = rotateveczaxis(MOVINGPOINT.x,MOVINGPOINT.y,MOVINGPOINT.z,anglez);

	MOVINGPOINT = somepointy;
	*/


	//MOVINGPOINT = somepointx;
	//MOVINGPOINT = somepointz;

	//MOVINGPOINT.x = somepointx.x;
	//MOVINGPOINT.y = somepointy.y;
	//MOVINGPOINT.z = somepointz.z;


	//float rotation = (float)angle * 0.0174532925f;
	//float3 somepoint = rotatevecyaxis(MOVINGPOINT.x,MOVINGPOINT.y,MOVINGPOINT.z,angle);
	//float3 anotherpoint = mul(somepoint,MOVINGPOINT);
	//MOVINGPOINT.x = anotherpoint.x;
	//MOVINGPOINT.z = anotherpoint.z;















	float somemul = currentMapData * 0.01;

	if(facetype == 0) //frontface
	{
		MOVINGPOINT.xyz = MOVINGPOINT.xyz - (float3(0,0,1) * (somemul));
		if(currentMapData > 0)
		{
			
		}
	}
		


	if(facetype == 1) //top
	{
		if(input.color.z - floor(input.color.z) == 0.1f)
		{
				
		}
		else
		{
			MOVINGPOINT.xyz = MOVINGPOINT.xyz - (float3(0,0,1) * (somemul));
			if(currentMapData > 0)
			{
				
			}
		}
	}
	if(facetype == 4) //backface
	{
		MOVINGPOINT.xyz = MOVINGPOINT.xyz - (float3(0,0,1) * (somemul));
		if(currentMapData > 0)
		{
			
		}
	}
		
		
		
		
		
	if(facetype == 2) // left
	{
			
		if(input.color.z - floor(input.color.z) == 0.1f)
		{
				
		}
		else
		{
			MOVINGPOINT.xyz = MOVINGPOINT.xyz - (float3(0,0,1) * (somemul));
			if(currentMapData > 0)
			{
				
			}
		}
	}

	
	if(facetype == 3) //right
	{
			
		if(input.color.z - floor(input.color.z) == 0.1f)
		{
				
		}
		else
		{
			MOVINGPOINT.xyz = MOVINGPOINT.xyz - (float3(0,0,1) * (somemul));
			if(currentMapData > 0)
			{
				
			}
		}
	}


	
	if(facetype == 5) //bottomface
	{
		if(input.color.z - floor(input.color.z) == 0.1f)
		{
				
		}
		else
		{
			MOVINGPOINT.xyz = MOVINGPOINT.xyz - (float3(0,0,1) * (somemul));
			if(currentMapData > 0)
			{
				
			}
		}
	}


	vertPos = float3(mod_input_vertex_pos.x, mod_input_vertex_pos.y, mod_input_vertex_pos.z);	

	diffX = (vertPos.x - (input.instancePosition.x));
	diffY = (vertPos.y - (input.instancePosition.y));
	diffZ = (vertPos.z - (input.instancePosition.z));
		
	//diffX = ((input.instancePosition.x) - vertPos.x);
	//diffY = ((input.instancePosition.y) - vertPos.y);
	//diffZ = ((input.instancePosition.z) - vertPos.z);
		

	//double anglex = atan2(MOVINGPOINT.y - ovrpos.y, MOVINGPOINT.z - ovrpos.z) * (180.0f / PI);
	//double angley = atan2(MOVINGPOINT.x - ovrpos.x, MOVINGPOINT.z - ovrpos.z) * (180.0f / PI);
	//double anglez = atan2(MOVINGPOINT.z - ovrpos.z, MOVINGPOINT.z - ovrpos.z) * (180.0f / PI);

	//float3 somepointx = rotatevecxaxis(MOVINGPOINT.x,MOVINGPOINT.y,MOVINGPOINT.z,anglex);
	//float3 somepointy = rotatevecyaxis(MOVINGPOINT.x,MOVINGPOINT.y,MOVINGPOINT.z,angley);
	//float3 somepointz = rotateveczaxis(MOVINGPOINT.x,MOVINGPOINT.y,MOVINGPOINT.z,anglez);
	//float3 somepos = mul(somepointy,MOVINGPOINT.xyz);
	//MOVINGPOINT = somepos;





	MOVINGPOINT = MOVINGPOINT + (-rightDir * diffX);
	MOVINGPOINT = MOVINGPOINT + (upDir * diffY);
	MOVINGPOINT = MOVINGPOINT + (forwardDir * diffZ);

	//float3 somepos = mul(somepointy,output.position.xyz);
	//MOVINGPOINT = somepos;

	
	//MOVINGPOINT = MOVINGPOINT + (-ovrdirf.xyz * (1*planeSize* 10));
	//MOVINGPOINT = MOVINGPOINT + (ovrdiru.xyz *  (1*planeSize));
	//MOVINGPOINT = MOVINGPOINT + (ovrdirr.xyz *  (1*planeSize));
		




	input.position.x = MOVINGPOINT.x;
	input.position.y = MOVINGPOINT.y;
	input.position.z = MOVINGPOINT.z;


	//output.position = mul(mod_input_vertex_pos, world);
	output.position = mul(input.position, world);
	output.position = mul(output.position, view);
	output.position = mul(output.position, proj);



	//float4x4 worldviewmatrix = mul(world,view);
	//float3 positionvs = input.position.xyz + float3(worldviewmatrix._41,worldviewmatrix._42,worldviewmatrix._43);
	//float3 finalpos = mul(float4(positionvs,1.0), proj);
	//output.position = float4(finalpos,1.0);





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

	/*output.heightmapmat0 = input.heightmapmat0;
	output.heightmapmat1 = input.heightmapmat1;
	output.heightmapmat2 = input.heightmapmat2;
	output.heightmapmat3 = input.heightmapmat3;*/


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

