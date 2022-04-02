cbuffer MatrixBuffer : register(b0)
{
	matrix worldMatrix;
	matrix viewMatrix;
	matrix projectionMatrix;
};

struct VertexInputType
{
    float4 position : POSITION;
    float2 tex : TEXCOORD0;
	float4 color : COLOR;
	float3 normal : NORMAL;
	float4 instancePosition : POSITION1;
	float4 instanceRadRot : POSITION2;
	float4 instanceRadRotRIGHT : POSITION3;
	float4 instanceRadRotUP : POSITION4;
};

struct PixelInputType
{
    float4 position : SV_POSITION;
    float2 tex : TEXCOORD0;
	float4 color : COLOR;
	float3 normal : NORMAL;
	float4 instancePosition : POSITION1;
	float4 instanceRadRot : POSITION2;
	float4 instanceRadRotRIGHT : POSITION3;
	float4 instanceRadRotUP : POSITION4;
};

const float PI = 3.1415926535897932384626433832795f;

float DegreeToRadian(float angle)
{
   return PI * angle / 180.0f;
}

float RadianToDegree(float angle)
{
  return angle * (180.0f / PI);
}

float distance(float2 a,float2 b) 
{
    return sqrt((a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y));
}

PixelInputType TextureVertexShader(VertexInputType input)
{ 
	float4 mod_input_vertex_pos;
	float3 forwardDir;
	float3 rightDir;
	float3 upDir;

	float3 MOVINGPOINT;
	float3 vertPos;
	float diffX;
	float diffY;
	float diffZ;

	PixelInputType output;
    
	input.position.w = 1.0f;

	mod_input_vertex_pos = input.position;

	mod_input_vertex_pos.x += input.instancePosition.x;
	mod_input_vertex_pos.y += input.instancePosition.y;
	mod_input_vertex_pos.z += input.instancePosition.z;
	mod_input_vertex_pos.w = 1.0f;

	forwardDir = float3(input.instanceRadRot.x, input.instanceRadRot.y, input.instanceRadRot.z);
	rightDir = float3(input.instanceRadRotRIGHT.x, input.instanceRadRotRIGHT.y, input.instanceRadRotRIGHT.z);
	upDir = float3(input.instanceRadRotUP.x, input.instanceRadRotUP.y, input.instanceRadRotUP.z);

	MOVINGPOINT = float3(input.instancePosition.x, input.instancePosition.y, input.instancePosition.z);
	vertPos = float3(mod_input_vertex_pos.x,mod_input_vertex_pos.y,mod_input_vertex_pos.z);

	diffX = vertPos.x - input.instancePosition.x;
	diffY = vertPos.y - input.instancePosition.y;
	diffZ = vertPos.z - input.instancePosition.z;

	MOVINGPOINT = MOVINGPOINT + -(rightDir * diffX);
	MOVINGPOINT = MOVINGPOINT + -(upDir * diffY);
	MOVINGPOINT = MOVINGPOINT + -(forwardDir * diffZ);		

	input.position.x = MOVINGPOINT.x;
	input.position.y = MOVINGPOINT.y;
	input.position.z = MOVINGPOINT.z;


	output.position = mul(input.position, worldMatrix);
    output.position = mul(output.position, viewMatrix);
    output.position = mul(output.position, projectionMatrix);
   
	output.instancePosition.x = input.instancePosition.x;
	output.instancePosition.y = input.instancePosition.y;
	output.instancePosition.z = input.instancePosition.z;

	   
	output.instanceRadRot.x = input.instanceRadRot.x;
	output.instanceRadRot.y = input.instanceRadRot.y;
	output.instanceRadRot.z = input.instanceRadRot.z;

	//output.tex = input.tex;
    //output.color = input.color;	


	output.normal = input.normal;
	float xer = dot(output.normal, float3(1, 0.25, 0.4));
	//float3(0.75, 0.50, 0.25));  
	//float3(1, 0.25, 0.4));   
	//float3(0.35, 0.15, 0.25));
	
	//xer *= 1.5f;
	//xer *= 10.25;

	float x = dot(input.normal,input.instanceRadRotUP);

	x = x* 0.5 - 0.5;
	
	//x*=10;

	float x0 = input.color.x;
	float y0 = input.color.y;
	float z0 = input.color.z;
	float w0 = input.color.w;


	float3 color = lerp(float3(input.color.x*0.90*xer, input.color.y*0.90*xer, input.color.z*0.90*xer), float3(input.color.x*0.95*xer, input.color.y*0.95*xer, input.color.z*0.95*xer), x);
	input.color.xyz = color;
	input.color.w = 1.0;
	output.color = input.color;	


    return output;
}