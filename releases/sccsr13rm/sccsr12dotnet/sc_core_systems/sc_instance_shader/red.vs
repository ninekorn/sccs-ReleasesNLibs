////////////////////////////////////////////////////////////////////////////////
// Filename: texture.vs
////////////////////////////////////////////////////////////////////////////////


/////////////
// GLOBALS //
/////////////
cbuffer MatrixBuffer :register(b0)
{
	//float4x4 worldMatrix;
	//float4x4 viewMatrix;
	//float4x4 projectionMatrix;
	float4x4 worldViewProjection;
}

//cbuffer data :register(b0)
//{
//	float4x4 worldMatrix;
//	float4x4 viewMatrix;
//	float4x4 projectionMatrix;
//}

//float3 instancedPos;



//////////////
// TYPEDEFS //
//////////////
struct VertexInputType
{
    float4 position : POSITION;
    //float2 tex : TEXCOORD0;
	float4 color : COLOR;
	float3 instancePosition : TEXCOORD1;
	//float4 matx : POSITION1;
	//float4 maty : POSITION2;
	///float4 matz : POSITION3;
	//float4 matw : POSITION4;
};

struct PixelInputType
{
    float4 position : SV_POSITION;
    //float2 tex : TEXCOORD0;
	float4 color : COLOR;
};


////////////////////////////////////////////////////////////////////////////////
// Vertex Shader
////////////////////////////////////////////////////////////////////////////////
PixelInputType ColorVertexShader(VertexInputType input)
{
    PixelInputType output;
    
	// Change the position vector to be 4 units for proper matrix calculations.
    input.position.w = 1.0f;

	// Update the position of the vertices based on the data for this particular instance.
    input.position.x += input.instancePosition.x;
    input.position.y += input.instancePosition.y;
    input.position.z += input.instancePosition.z;

	//input.position.x += instancedPos.x;
    //input.position.y += instancedPos.y;
    //input.position.z += instancedPos.z;




	//matrix world = { input.matx, input.maty, input.matz, input.matw };
	//output.position = mul(input.position, world);

	output.position = mul(input.position, worldViewProjection);

	// Calculate the position of the vertex against the world, view, and projection matrices.
    //output.position = mul(input.position, worldMatrix);
    //output.position = mul(output.position, viewMatrix);
    //output.position = mul(output.position, projectionMatrix);
    
	// Store the texture coordinates for the pixel shader.
	//output.tex = input.tex;
    output.color = input.color;

    return output;
}