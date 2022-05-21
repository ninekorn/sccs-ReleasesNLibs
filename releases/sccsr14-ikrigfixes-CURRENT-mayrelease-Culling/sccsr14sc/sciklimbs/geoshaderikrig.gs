cbuffer MatrixBuffer :register(b0)
{
	float4x4 worldMatrix;
	float4x4 viewMatrix;
	float4x4 projectionMatrix;
	//float4x4 worldViewProjection;
}

Texture2D diffuseMap;
SamplerState textureSampler;

struct VS_INPUT
{
    float4 position : POSITION;
	float4 color : COLOR;
	float2 tex: TEXCOORD;
	float3 normal : NORMAL;
	//float4 instancePosition : POSITION1;
	//float4 instanceRadRot : POSITION2;
	//float4 instanceRadRotRIGHT : POSITION3;
	//float4 instanceRadRotUP : POSITION4;
};

struct GS_INPUT
{
	float4 position : SV_POSITION;
	float4 color : COLOR;
	float2 tex: TEXCOORD;
	float3 normal : NORMAL;
	//float4 instancePosition : POSITION1;
	//float4 instanceRadRot : POSITION2;
	//float4 instanceRadRotRIGHT : POSITION3;
	//float4 instanceRadRotUP : POSITION4;
};

struct PS_INPUT
{
    float4 position : SV_POSITION;
	float4 color : COLOR;
    float2 tex: TEXCOORD;
	float3 normal : NORMAL;
	//float4 instancePosition : POSITION1;
	//float4 instanceRadRot : POSITION2;
	//float4 instanceRadRotRIGHT : POSITION3;
	//float4 instanceRadRotUP : POSITION4;
};


GS_INPUT VS( VS_INPUT input )
{   
    GS_INPUT output = (GS_INPUT)0;

    output.position = input.position;   
	output.color = input.color;

	//output.position = mul(output.position, worldViewProjection);
	output.position = mul(output.position, worldMatrix);
	output.position = mul(output.position, viewMatrix);
	output.position = mul(output.position, projectionMatrix);

	output.normal = input.normal;   
	output.tex = input.tex;  

    return output;
}

[maxvertexcount(12)]
void GS( triangle GS_INPUT input[3], inout TriangleStream<PS_INPUT> TriStream)
{
	/*PS_INPUT o;

	float3 edgeA = (input[1].Pos - input[0].Pos).xyz;
	float3 edgeB = (input[2].Pos - input[0].Pos).xyz;

	float3 crossProd = cross(edgeA, edgeB);
	float3 normalFace = normalize(crossProd);

	//float4 dismissscreencap = float4(0,0,0,1)
	//int swtc0 = 0;
	//if(normalFace.x < 0 ||normalFace.y < 0 || normalFace.z < 0  )
	//{
	//	swtc0 = 1;
	//}


	for (int i = 0; i < 3; i++)
	{
		float x = dot(normalFace, float3(1, 0.25, 0.4));	
		x = x * 0.5 - 0.5;
		o.Col = input[i].Col;
		float3 color = lerp(float3(o.Col.x*0.85, o.Col.y*0.85, o.Col.z*0.85), float3(o.Col.x*0.95, o.Col.y*0.95, o.Col.z*0.95), x);
		o.Col.xyz = color;
		o.Col.w = 1.0;
		o.Pos = input[i].Pos;
		o.tex = input[i].tex;	

		//if(swtc0 == 1)
		//{
		//	o.Col = dismissscreencap;
		//}

		TriStream.Append(o);
	}
	TriStream.RestartStrip();*/
	


	PS_INPUT output;

	float3 edgeA = (input[1].position - input[0].position).xyz;
	float3 edgeB = (input[2].position - input[0].position).xyz;

	float3 crossProd = cross(edgeA, edgeB);
	float3 normalFace = normalize(crossProd);

	//output.normal = input.normal;   

	for (int i = 0; i < 3; i++)
	{
		float x = dot(normalFace, float3(1, 0.25, 0.4));	
		x = x * 0.5 - 0.5;
		output.color = input[i].color;
		float3 color = lerp(float3(output.color.x*0.85, output.color.y*0.85, output.color.z*0.85), float3(output.color.x*0.95, output.color.y*0.95, output.color.z*0.95), x);
		output.color.xyz = color;
		output.color.w = 1.0;
		output.position = input[i].position;
		output.tex = input[i].tex;	
		output.normal = input[i].normal;   

		//output.indexPos = input[i].indexPos;   

		TriStream.Append(output);
	}


	TriStream.RestartStrip();	
}

float4 PS( PS_INPUT input) : SV_Target
{ 
	float4 col = input.color;// diffuseMap.Sample(textureSampler, input.tex);// * input.Col;
	return col;
}