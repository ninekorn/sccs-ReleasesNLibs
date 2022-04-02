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
	float4 position : POSITION0;
	float4 color : COLOR0; //byte map index xyz and w for typeofface 0 to 5
	float3 normal : NORMAL0;
	float paddingvert0 : PSIZE0;	//instance width
	float2 tex : TEXCOORD0;
	float paddingvert1 : PSIZE1;	//instance height
	float paddingvert2 : PSIZE2;	//instance depth
	/*float4 instancePosition : POSITION1;
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
	float4 mapmatrix15 : POSITION20;*/

	int xindex : PSIZE3;	
	int yindex : PSIZE4;
};

struct GS_INPUT
{
	float4 position : SV_POSITION;
	float4 color : COLOR0; //byte map index xyz and w for typeofface 0 to 5
	float3 normal : NORMAL0;
	float paddingvert0 : PSIZE0;	//instance width
	float2 tex : TEXCOORD0;
	float paddingvert1 : PSIZE1;	//instance height
	float paddingvert2 : PSIZE2;	//instance depth
	/*float4 instancePosition : POSITION1;
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
	float4 mapmatrix15 : POSITION20;*/

	int xindex : PSIZE3;	
	int yindex : PSIZE4;
};

struct PS_INPUT
{
	float4 position : SV_POSITION;
	float4 color : COLOR0; //byte map index xyz and w for typeofface 0 to 5
	float3 normal : NORMAL0;
	float paddingvert0 : PSIZE0;	//instance width
	float2 tex : TEXCOORD0;
	float paddingvert1 : PSIZE1;	//instance height
	float paddingvert2 : PSIZE2;	//instance depth
	/*float4 instancePosition : POSITION1;
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
	float4 mapmatrix15 : POSITION20;*/

	int xindex : PSIZE3;	
	int yindex : PSIZE4;
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


	output.paddingvert0 = input.paddingvert0;
	output.paddingvert1 = input.paddingvert1;
	output.paddingvert2 = input.paddingvert2;

	output.xindex = input.xindex;
	output.yindex = input.yindex;

    return output;
}

[maxvertexcount(12)]
void GS( triangle GS_INPUT input[3], inout TriangleStream<PS_INPUT> TriStream)
{
	PS_INPUT o;

	float3 edgeA = (input[1].position - input[0].position).xyz;
	float3 edgeB = (input[2].position - input[0].position).xyz;

	float3 crossProd = cross(edgeA, edgeB);
	float3 normalFace = normalize(crossProd);

	/*float4 dismissscreencap = float4(0,0,0,1)
	int swtc0 = 0;
	if(normalFace.x < 0 ||normalFace.y < 0 || normalFace.z < 0  )
	{
		swtc0 = 1;
	}*/


	for (int i = 0; i < 3; i++)
	{
		float x = dot(normalFace, float3(1, 0.25, 0.4));	
		x = x * 0.5 - 0.5;
		o.color = input[i].color;
		float3 color = lerp(float3(o.color.x*0.85, o.color.y*0.85, o.color.z*0.85), float3(o.color.x*0.95, o.color.y*0.95, o.color.z*0.95), x);
		o.color.xyz = color;
		o.color.w = 1.0;
		o.position = input[i].position;
		o.tex = input[i].tex;	

		/*if(swtc0 == 1)
		{
			o.color = dismissscreencap;
		}*/

		o.normal = input[i].normal;
		//o.tex = input[i].tex;


		o.paddingvert0 = input[i].paddingvert0;
		o.paddingvert1 = input[i].paddingvert1;
		o.paddingvert2 = input[i].paddingvert2;

		o.xindex = input[i].xindex;
		o.yindex = input[i].yindex;


		TriStream.Append(o);
	}
	TriStream.RestartStrip();		
}

float4 PS( PS_INPUT input) : SV_Target
{ 
	float4 col = diffuseMap.Sample(textureSampler, input.tex);// * input.Col;
	return col;
}