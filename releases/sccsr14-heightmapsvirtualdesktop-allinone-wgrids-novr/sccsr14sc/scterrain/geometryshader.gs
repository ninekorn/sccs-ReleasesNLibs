cbuffer MatrixBuffer :register(b0)
{
	float4x4 world;
	float4x4 view;
	float4x4 proj;
};

Texture2D diffuseMap;
SamplerState textureSampler;

struct VS_INPUT
{
    float4 position : POSITION;
	float4 color : COLOR;
	float3 normal : NORMAL;
    float2 tex : TEXCOORD;
	float3 instancePosition : POSITION1;
};

struct GS_INPUT
{
	float4 position : SV_POSITION;
	float4 color : COLOR;
	float3 normal : NORMAL;
    float2 tex : TEXCOORD;
};

struct PS_INPUT
{
    float4 position : SV_POSITION;
	float4 color : COLOR;
	float3 normal : NORMAL;
    float2 tex : TEXCOORD;
};


GS_INPUT VS( VS_INPUT input )
{   
    GS_INPUT output = (GS_INPUT)0;

    output.position = input.position;   
	output.color = input.color;

	//output.position = mul(output.position, worldViewProjection);
	output.position = mul(output.position, world);
	output.position = mul(output.position, view);
	output.position = mul(output.position, proj);

	output.normal = input.normal;   
	output.tex = input.tex;   

	//output.Nor = input.Nor;   


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
		//o.normal = input[i].normal;
		o.normal.xyz = normalFace;


		TriStream.Append(o);
	}
	TriStream.RestartStrip();		
}

float4 PS( PS_INPUT input) : SV_Target
{ 
	float4 col = diffuseMap.Sample(textureSampler, input.tex);// * input.color;
	return col;
}