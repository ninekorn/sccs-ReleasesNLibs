cbuffer data :register(b0)
{
	float4x4 worldMatrix;
	float4x4 viewMatrix;
	float4x4 projMatrix;
}

Texture2D diffuseMap;
SamplerState textureSampler;

struct VS_INPUT
{
    float4 Pos : POSITION;
	//float4 Nor : NORMAL;
	float4 Col : COLOR;
	float2 tex: TEXCOORD;

};

struct GS_INPUT
{
	float4 Pos : SV_POSITION;
	//float4 Nor : NORMAL;
	float4 Col : COLOR;
	float2 tex: TEXCOORD;
};

struct PS_INPUT
{
    float4 Pos : SV_POSITION;
	//float4 Nor : NORMAL;
	float4 Col : COLOR;
    float2 tex: TEXCOORD;
};


GS_INPUT VS( VS_INPUT input )
{   
    GS_INPUT output = (GS_INPUT)0;

    output.Pos = input.Pos;   
	output.Col = input.Col;

	output.Pos = mul(output.Pos, worldMatrix);
	output.Pos = mul(output.Pos, viewMatrix);
	output.Pos = mul(output.Pos, projMatrix);

	//output.Nor = input.Nor;   


    return output;
}

[maxvertexcount(3)]
void GS( triangle GS_INPUT input[3], inout TriangleStream<PS_INPUT> TriStream)
{
	PS_INPUT o;

	float3 edgeA = (input[1].Pos - input[0].Pos).xyz;
	float3 edgeB = (input[2].Pos - input[0].Pos).xyz;
	float3 crossProd = cross(edgeA, edgeB);
	float3 normalFace = normalize(crossProd);


	for (int i = 0; i < 3; i++)
	{
		float x = dot(normalFace, float3(1, 0.25, 0.4));
		
		//float x = (xx < 0.0) ? xx: -xx;


		x = x * 0.5 - 0.5;

		o.Col = input[2-i].Col;
		float3 color = lerp(float3(o.Col.x*0.55, o.Col.y*0.55, o.Col.z*0.55), float3(o.Col.x*0.85, o.Col.y*0.85, o.Col.z*0.85), x);
		o.Col.xyz = color;
		o.Col.w = 1.0;


		o.Pos = input[2-i].Pos;
		o.tex = input[2-i].tex;

		//o.Pos.x *=-1;
	
		TriStream.Append(o);
	}
	TriStream.RestartStrip();		
}

float4 PS( PS_INPUT input) : SV_Target
{ 
	float4 col = diffuseMap.Sample(textureSampler, input.tex) * input.Col;
	return col;
}