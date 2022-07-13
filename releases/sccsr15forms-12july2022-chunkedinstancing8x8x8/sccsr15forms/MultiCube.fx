// Copyright (c) 2010-2013 SharpDX - Alexandre Mutel
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

/*
[StructLayout(LayoutKind.Explicit)]
public struct DLightBuffer
{
	[FieldOffset(0)]
	public Vector4 ambientColor;
	[FieldOffset(16)]
	public Vector4 diffuseColor;
	[FieldOffset(32)]
	public Vector3 lightDirection;
	[FieldOffset(44)]
	public float padding0; // Added extra padding so structure is a multiple of 16.
	[FieldOffset(48)]
	public Vector3 lightPosition;
	[FieldOffset(60)]
	public float padding1; // Added extra padding so structure is a multiple of 16.
}*/



cbuffer LightBuffer :register(b1)
{
	float4 ambientColor;
	float4 diffuseColor;
	float3 lightDirection;
	float padding0;
	float4 lightPosition;
	float padding1;
};



struct VS_IN
{
	float4 position : POSITION0;
	float4 col : COLOR;
	float4 normal : NORMAL;
};

struct PS_IN
{
	float4 position : SV_POSITION;
	float4 col : COLOR;
	float4 normal : NORMAL;
};

float4x4 WorldViewProj;

PS_IN VS( VS_IN input )
{
	PS_IN output = (PS_IN)0;

	input.position.w = 1.0f;
	
	output.position = mul(input.position, WorldViewProj);
	output.col = input.col;
	output.normal = input.normal;

	//uint sometest = 0x0000;

	return output;
}




static float dstX = 0;
static float dstY = 0;
static float dstZ = 0;
static float dstX_vs_dstZ = 0;
static float dstX_vs_dstY = 0;
static float dstY_vs_dstZ = 0;

float sc_check_distance_node_3d_geometry(float3 nodeA, float3 nodeB, float minx, float miny, float minz, float maxx, float maxy, float maxz)
{
	//STEVE CHASSÉ 3D blueprint for sphere type and a ton more. based on 2d version of Sebastian Lague. but my version is not perfect. i don't know yet what else to put in there.
	//the solution was easier than i thought and it came to me quite fast after fearing for months i'd never be able to quickly get this function written. This function is also a
	//main part of the upgrade that i want to implement to the Jitter physics engine for spatial awareness. In the jitter physics engine, it seems as if every frame that the objects
	//are enabled and non-static, jitter checks ALL bounding boxes for collisions between all of them. So of course at some point, the more objects in the scene, the more bounding
	//boxes it has to check for each individual objects. for instance, lets say there is 1000 objects in the scene, so index 0 to index 999, if object 0 checks against ALL other
	//998 objects, it's a huge waste of performance. But there are collision "islands" in the jitter physics engine but those i believe are chosen only when bounding boxes are 
	//officially declared as colliding. so i want to see if i can use a fast distance checker (which i didnt test yet against Math.Sqrt or the very fast quake sqrt). But all of
	//the things i learned in doing chunks are also going to be needed for when im going to start developing things around and inside of the Jitter Physics Engine. So prior upgrading the physics
	//engine jitter for my engine sccoresystems, i can only have 4000 max objects and it would lag the scene. but the tests are not carved on rock yet because, 
	//1. im not loading the dll how i should maybe
	//2. maybe because multiple instances of the physics engine like i am loading them isn't the proper way to do it? in monogame, to load different scene instances, they use 
	//   Activator.CreateInstance and i was unable to use that anywhere back then. I might try again at some point... But right now i am using an interface. In sccsv10
	//


	dstX = abs((nodeA.x) - (nodeB.x));
	dstY = abs((nodeA.y) - (nodeB.y));
	dstZ = abs((nodeA.z) - (nodeB.z));

	dstX_vs_dstZ = 0;
	dstX_vs_dstY = 0;
	dstY_vs_dstZ = 0;

	if (dstX > dstZ)
	{
		dstX_vs_dstZ = maxx * dstZ + minx * (dstX - dstZ);
	}
	else
	{
		dstX_vs_dstZ = maxx * dstX + minx * (dstZ - dstX);
	}

	if (dstX > dstY)
	{
		dstX_vs_dstY = maxy * dstY + miny * (dstX - dstY);
	}
	else
	{
		dstX_vs_dstY = maxy * dstX + miny * (dstY - dstX);
	}

	if (dstY > dstZ)
	{
		dstY_vs_dstZ = maxz * dstZ + minz * (dstY - dstZ);
	}
	else
	{
		dstY_vs_dstZ = maxz * dstY + minz * (dstZ - dstY);
	}
	return dstX_vs_dstY + dstX_vs_dstZ + dstY_vs_dstZ;
}



float4 PS(PS_IN input) : SV_Target
{
	//input.col = input.col + (input.normal * 0.25f);
	/*float4 textureColor;
	float3 lightDir;
	float lightIntensity;
	float4 color;

	// Sample the pixel color from the texture using the sampler at this texture coordinate location.
	//textureColor = shaderTexture.Sample(SampleType, input.tex);
	textureColor = input.col;
	// Invert the light direction for calculations.
	lightDir = -lightDirection;

	// Calculate the amount of the light on this pixel.
	lightIntensity = saturate(dot(input.normal, lightDir));

	// Determine the final amount of diffuse color based on the diffuse color combined with the light intensity.
	color = saturate(diffuseColor * lightIntensity);

	// Multiply the texture pixel and the final diffuse color to get the final pixel color result.
	color = color * textureColor;*/

	//float4 textureColor;
	float3 lightDir;
	float lightIntensity;
	float4 color;
	float3 reflection;
	float4 specular;

	// Sample the pixel color from the texture using the sampler at this texture coordinate location.
	//textureColor = shaderTexture.Sample(SampleType, input.tex);

	float4 somecolor = float4(0.25f,0.25f,0.25f,1.0f);// float4(0.95f,0.95f,0.95f,1.0f);
	float4 the_color = float4(1.0f, 1.0f, 1.0f, 1.0f);;

	//NORMALS CALCULATIONS
	//NORMALS CALCULATIONS
	//NORMALS CALCULATIONS
	float4 somemoddedinputcolor = the_color;// input.color;
	// WARNING==INPUT.COLOR IS NOT THE COLOR WHEN COMING IN, IT IS THE INDEX POSITION OF EACH BYTES X/Y/Z. BUT IN ORDER TO DISPLAY A COLOR, YOU NEED TO HAVE THIS PIXEL SHADER INPUT.COLOR 
	// SET TO THE DESIRED COLOR WHEN THIS HLSL METHOD FINISHES OTHERWISE YOU WILL GET A MULTICOLOR CHUNK BECAUSE THE INDEXES GO STRAIGHT FROM 0 TO THE WIDTH OF THE CHUNK FROM 0 TO 3 IN THE 
	// X/Y/Z AXIS AND THAT IS THE COLORS.

	float3 inputPos;// = float3();

	inputPos.x = input.position.x;//+ input.instancePosition.x;// + somemoddedinputcolor.x;
	inputPos.y = input.position.y;// + input.instancePosition.y;// + somemoddedinputcolor.y;
	inputPos.z = input.position.z;// + input.instancePosition.z;// + somemoddedinputcolor.z;

	//float3 lightDir;
	//float lightIntensity;
	float4 colorer;

	colorer = ambientColor;

	lightDir = -lightDirection;

	//float distTot = sc_check_distance_node_3d_geometry(lightPosition, inputPos, 0.1f, 0.1f, 0.1f, 1, 1, 1);
	//float distTot = sc_check_distance_node_3d_geometry(lightPosition, inputPos, 9, 9, 9, 9, 9, 9);
	//float distTot = sc_check_distance_node_3d_geometry(lightPosition, inputPos, 3, 3, 3, 3, 3, 3);
	//float distTot = sc_check_distance_node_3d_geometry(lightPosition, inputPos, 1, 1, 1, 1, 1, 1);

	float distTot = sqrt(((lightPosition.x - inputPos.x)*(lightPosition.x - inputPos.x)) + ((lightPosition.y - inputPos.y)*(lightPosition.y - inputPos.y)) + ((lightPosition.z - inputPos.z)*(lightPosition.z - inputPos.z)));

	float3 dirLightToFace = lightPosition - inputPos;
	dirLightToFace /= distTot;

	float someOtherDot = dot(dirLightToFace, lightDir);

	if (someOtherDot >= 0)
	{
		float DOTProdAngleWithHypAndOpp = saturate(dot(input.normal, dirLightToFace));

		//someOtherDot =  (DOTProdAngleWithHypAndOpp + someOtherDot) * 0.5f;

		float someTester = padding0 - distTot;
		float distMod = someTester;
		distMod *= 0.1f;	 //0.01f // 0.1f // 0.5f
		float4 modColor = colorer;

		modColor += (diffuseColor * (DOTProdAngleWithHypAndOpp)) * distMod;
		modColor = saturate(modColor);

		//DOTProdAngleWithHypAndOpp = saturate(dot(input.normal, dirLightToFace));

		somemoddedinputcolor = somecolor + DOTProdAngleWithHypAndOpp * modColor;// * modColor * 0.75f;

		if(input.normal.x == 1.0)
		{
			somemoddedinputcolor *= 0.555f;//float4(0.15,0.95,0.15,1);
		}

		if(input.normal.x == -1.0)
		{
			somemoddedinputcolor *=  0.145f;//float4(0.15,0.95,0.15,1);
		}

		if(input.normal.y == 1.0)
		{
			somemoddedinputcolor*=  0.475f;//float4(0.15,0.95,0.15,1);
		}

		if(input.normal.y == -1.0)
		{
			somemoddedinputcolor*=  0.135f;//float4(0.15,0.95,0.15,1);
		}
		if(input.normal.z == 1.0)
		{
			somemoddedinputcolor *=  0.765f;//float4(0.15,0.95,0.15,1);
		}

		if(input.normal.z == -1.0)
		{
			somemoddedinputcolor *=  0.35f;//float4(0.15,0.95,0.15,1);
		}

		float somedot = dot(input.normal, dirLightToFace);

		if(somedot <= 0)
		{
			// Invert the light direction for calculations.
			lightDir = - lightDirection;

			// Calculate the amount of the light on this pixel.
			lightIntensity = saturate(dot(input.normal, dirLightToFace));

			somemoddedinputcolor *=  1 + (saturate(lightIntensity * somedot * distTot*0.35f));
		}
	}
	else
	{
		float DOTProdAngleWithHypAndOpp = saturate(dot(input.normal, dirLightToFace));

		//someOtherDot =  (DOTProdAngleWithHypAndOpp + someOtherDot) * 0.5f;

		float someTester = padding0 - distTot;
		float distMod = someTester;
		distMod *= 0.1f;	 //0.01f
		float4 modColor = colorer;

		modColor += (diffuseColor * (DOTProdAngleWithHypAndOpp)) * distMod;
		modColor = saturate(modColor);


		//DOTProdAngleWithHypAndOpp = saturate(dot(input.normal, dirLightToFace));


		somemoddedinputcolor = somecolor + DOTProdAngleWithHypAndOpp * modColor;// * modColor * 0.75f;


		if(input.normal.x == 1.0)
		{
			somemoddedinputcolor *= 0.555f;//float4(0.15,0.95,0.15,1);
		}

		if(input.normal.x == -1.0)
		{
			somemoddedinputcolor *=  0.145f;//float4(0.15,0.95,0.15,1);
		}

		if(input.normal.y == 1.0)
		{
			somemoddedinputcolor*=  0.475f;//float4(0.15,0.95,0.15,1);
		}

		if(input.normal.y == -1.0)
		{
			somemoddedinputcolor*=  0.135f;//float4(0.15,0.95,0.15,1);
		}
		if(input.normal.z == 1.0)
		{
			somemoddedinputcolor *=  0.765f;//float4(0.15,0.95,0.15,1);
		}

		if(input.normal.z == -1.0)
		{
			somemoddedinputcolor *=  0.35f;//float4(0.15,0.95,0.15,1);
		}
	}




	//NORMALS CALCULATIONS
	//NORMALS CALCULATIONS
	//NORMALS CALCULATIONS

	/*float DOTProdAngleWithHypAndOpp = saturate(dot(input.normal, dirLightToFace));

	//someOtherDot =  (DOTProdAngleWithHypAndOpp + someOtherDot) * 0.5f;

	float someTester = padding0 - distTot;
	float distMod = someTester;
	distMod *= 0.1f;	 //0.01f // 0.1f // 0.5f
	float4 modColor = colorer;

	modColor += (diffuseColor * (DOTProdAngleWithHypAndOpp)) * distMod;
	modColor = saturate(modColor);

	//DOTProdAngleWithHypAndOpp = saturate(dot(input.normal, dirLightToFace));

	somemoddedinputcolor = somecolor;// +DOTProdAngleWithHypAndOpp * modColor;// * modColor * 0.75f;

	if (input.normal.x == 1.0)
	{
		somemoddedinputcolor *= 0.555f;//float4(0.15,0.95,0.15,1);
	}

	if (input.normal.x == -1.0)
	{
		somemoddedinputcolor *= 0.145f;//float4(0.15,0.95,0.15,1);
	}

	if (input.normal.y == 1.0)
	{
		somemoddedinputcolor *= 0.475f;//float4(0.15,0.95,0.15,1);
	}

	if (input.normal.y == -1.0)
	{
		somemoddedinputcolor *= 0.135f;//float4(0.15,0.95,0.15,1);
	}
	if (input.normal.z == 1.0)
	{
		somemoddedinputcolor *= 0.765f;//float4(0.15,0.95,0.15,1);
	}

	if (input.normal.z == -1.0)
	{
		somemoddedinputcolor *= 0.35f;//float4(0.15,0.95,0.15,1);
	}

	float somedot = dot(input.normal, dirLightToFace);

	if (somedot <= 0)
	{
		
	}
	// Invert the light direction for calculations.
	lightDir = -lightDirection;

	// Calculate the amount of the light on this pixel.
	lightIntensity = saturate(dot(input.normal, dirLightToFace));

	somemoddedinputcolor *= 1 + (saturate(lightIntensity * somedot * distTot * 0.35f));

	

	somemoddedinputcolor = somecolor;// +DOTProdAngleWithHypAndOpp * modColor;// * modColor * 0.75f;

	if (input.normal.x == 1.0)
	{
		somemoddedinputcolor *= 0.555f;//float4(0.15,0.95,0.15,1);
	}

	if (input.normal.x == -1.0)
	{
		somemoddedinputcolor *= 0.145f;//float4(0.15,0.95,0.15,1);
	}

	if (input.normal.y == 1.0)
	{
		somemoddedinputcolor *= 0.475f;//float4(0.15,0.95,0.15,1);
	}

	if (input.normal.y == -1.0)
	{
		somemoddedinputcolor *= 0.135f;//float4(0.15,0.95,0.15,1);
	}
	if (input.normal.z == 1.0)
	{
		somemoddedinputcolor *= 0.765f;//float4(0.15,0.95,0.15,1);
	}

	if (input.normal.z == -1.0)
	{
		somemoddedinputcolor *= 0.35f;//float4(0.15,0.95,0.15,1);
	}*/



	return somemoddedinputcolor;// input.col;
}
