cbuffer data :register(b0)
{
	float4x4 worldMatrix;
	float4x4 viewMatrix;
	float4x4 projectionMatrix;
}

struct VertexInputType
{
	float4 position : POSITION;
	float4 color : COLOR;
	float2 tex: TEXCOORD;
};

struct PixelInputType
{
	float4 position : SV_POSITION;
	float4 color : COLOR;
	float2 tex: TEXCOORD;
};

PixelInputType ColorVertexShader(VertexInputType input)
{
	PixelInputType output;

	input.position.w = 1.0f;

	output.position = mul(input.position, worldMatrix);
	output.position = mul(output.position, viewMatrix);
	output.position = mul(output.position, projectionMatrix);


	output.tex = input.tex;
	output.color = input.color;	
	//input.tex.y = 1-input.tex.y;

	return output;
}