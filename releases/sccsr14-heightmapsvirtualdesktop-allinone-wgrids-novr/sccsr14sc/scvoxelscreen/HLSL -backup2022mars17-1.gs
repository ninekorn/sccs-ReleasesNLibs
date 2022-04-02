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

	float4 instancePosition : POSITION1;
	/*float4 instanceRadRotFORWARD : POSITION2;
	float4 instanceRadRotRIGHT : POSITION3;
	float4 instanceRadRotUP : POSITION4;*/
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
	float4 instancePosition : POSITION1;
	/*float4 instanceRadRotFORWARD : POSITION2;
	float4 instanceRadRotRIGHT : POSITION3;
	float4 instanceRadRotUP : POSITION4;*/
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
	float4 instancePosition : POSITION1;
	/*float4 instanceRadRotFORWARD : POSITION2;
	float4 instanceRadRotRIGHT : POSITION3;
	float4 instanceRadRotUP : POSITION4;*/
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

	/*output.mapmatrix0 = input.mapmatrix0;
	output.mapmatrix1 = input.mapmatrix1;
	output.mapmatrix2 = input.mapmatrix2;
	output.mapmatrix3 = input.mapmatrix3;
	output.mapmatrix4 = input.mapmatrix4;
	output.mapmatrix5 = input.mapmatrix5;
	output.mapmatrix6 = input.mapmatrix6;
	output.mapmatrix7 = input.mapmatrix7;
	output.mapmatrix8 = input.mapmatrix8;
	output.mapmatrix9 = input.mapmatrix9;
	output.mapmatrix10 = input.mapmatrix10;
	output.mapmatrix11 = input.mapmatrix11;
	output.mapmatrix12 = input.mapmatrix12;
	output.mapmatrix13 = input.mapmatrix13;
	output.mapmatrix14 = input.mapmatrix14;
	output.mapmatrix15 = input.mapmatrix15;*/


    return output;
}



static float4 somecolor =  float4(1,0,0,1);
float4 PS( PS_INPUT input) : SV_Target
{ 
	somecolor = diffuseMap.Sample(textureSampler, input.tex);// * input.Col;
	return somecolor;
}


[maxvertexcount(12)]
void GS( triangle GS_INPUT input[3], inout TriangleStream<PS_INPUT> TriStream)
{
	int tinyChunkWidth = 8; // 4 // 8
	int tinyChunkHeight = 8; // 4 // 5
	int tinyChunkDepth = 1; // 4 // 8 // 1


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


	int instancesw = 128; //int(input[0].paddingvert0); // 256 // 240 // 320 // 480 // 128 //120 //160
	int instancesh = 72; //int(input[0].paddingvert1); // 128 // 135 // 180 // 270 // 72 //68 // 90
	int instancesd = 1;  //int(input[0].paddingvert2);

	float screensinw = round(input[0].paddingvert0) * 2.0f;
	float screensinh = round((input[0].paddingvert0 - floor(input[0].paddingvert0)) * 10.0f * 2.0f);

	int screenperw = 2;//int(input[0].paddingvert0); // 256 // 240 // 320 // 480
	int screenperh = 2;//int(input[0].paddingvert1); // 128 // 135 // 180 // 270
	int screenperdepth = 1;//int(input[0].paddingvert2);

	//4 or 8 multiplier
	int oriscreenx = instancesw * 8; //80 when using 20 width instances in SC_GlobalsChunkKeyboard //// 192 <=> 768 
 	int oriscreeny = instancesh * 8; //40 when using 10 height instances in SC_GlobalsChunkKeyboard //// 108 <=> 432
	// mul 2 when more screens in scgraphicssec.cs

	float somemul = 1/oriscreenx;

	float4 textureColor = float4(1,1,1,1);
	


	for (int i = 0; i < 3; i++)
	{
		//float x = dot(normalFace, float3(1, 0.25, 0.4));	
		//x = x * 0.5 - 0.5;
		//o.color = input[i].color;
		//float3 color = lerp(float3(o.color.x*0.85, o.color.y*0.85, o.color.z*0.85), float3(o.color.x*0.95, o.color.y*0.95, o.color.z*0.95), x);
		//o.color.xyz = color;
		//o.color.w = 1.0;
		o.position = input[i].position;
		//o.tex = input[i].tex;	

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

		float2 testinputtex =  input[i].tex;
		
			
		int x = int(input[i].color.x);
		int y = int(input[i].color.y);
		int z = int(input[i].color.z);

		int facetype = int(input[i].color.w);


		testinputtex = (testinputtex/ float2(oriscreenx,oriscreeny));
		float2 testinputtexY = input[i].tex;
		testinputtex.x = (testinputtex.x + (somemul * (input[i].xindex)));
		testinputtex.x = ((testinputtex.x) + (((1.0f/float(oriscreenx)) * 1) * ((input[i].xindex * tinyChunkWidth) + (x))));
		testinputtex.y = ((testinputtex.y) + (((1.0f/float(oriscreeny)) * 1) * ((input[i].yindex * tinyChunkHeight) + (tinyChunkHeight-1- y))));
		//testinputtex.x *=-1;
		o.tex = testinputtex;

		textureColor = diffuseMap.Sample(textureSampler, input[i].tex);

		o.color = somecolor;

		int currentIndex = x + tinyChunkWidth * (y + tinyChunkHeight * z); // xi + tinyChunkWidth * (yi + (tinyChunkHeight * zi));
		int someOtherIndex = currentIndex;
	
		int somecountermul = (currentIndex); //17.25f









    
		if(somecountermul == 0)
		{  
			o.mapmatrix0.x = (textureColor.x*textureColor.y*textureColor.z) / 3;
		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}

		if(somecountermul == 1)
		{
			o.mapmatrix0.y = (textureColor.x*textureColor.y*textureColor.z) / 3;//0.y;
		}    
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}
		if(somecountermul == 2)
		{
			o.mapmatrix0.z = (textureColor.x*textureColor.y*textureColor.z) / 3;//0.z;		
		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}
		if(somecountermul == 3)
		{
			o.mapmatrix0.w = (textureColor.x*textureColor.y*textureColor.z) / 3;//0.w;
		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}



		if(somecountermul == 4)
		{
			o.mapmatrix1.x = (textureColor.x*textureColor.y*textureColor.z) / 3;//1.x;

		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}
		if(somecountermul == 5)
		{
			o.mapmatrix1.y = (textureColor.x*textureColor.y*textureColor.z) / 3;//1.y;
	     
		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}
		if(somecountermul == 6)
		{
			o.mapmatrix1.z = (textureColor.x*textureColor.y*textureColor.z) / 3;//1.z;
	     

		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}


		if(somecountermul == 7)
		{
			o.mapmatrix1.w = (textureColor.x*textureColor.y*textureColor.z) / 3;//1.w;
	     
		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}







		if(somecountermul == 8)
		{
			o.mapmatrix2.x = (textureColor.x*textureColor.y*textureColor.z) / 3;//2.x;
		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}

		if(somecountermul == 9)
		{
			o.mapmatrix2.y = (textureColor.x*textureColor.y*textureColor.z) / 3;//2.y;
		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}

		if(somecountermul == 10)
		{
			o.mapmatrix2.z = (textureColor.x*textureColor.y*textureColor.z) / 3;//2.z;

		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}

		if(somecountermul == 11)
		{
			o.mapmatrix2.w = (textureColor.x*textureColor.y*textureColor.z) / 3;//2.w;
		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}






		if(somecountermul == 12)
		{
			o.mapmatrix3.x = (textureColor.x*textureColor.y*textureColor.z) / 3;//3.x;

		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}

		if(somecountermul == 13)
		{
			o.mapmatrix3.y = (textureColor.x*textureColor.y*textureColor.z) / 3;//3.y;

		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}

		if(somecountermul == 14)
		{
			o.mapmatrix3.z = (textureColor.x*textureColor.y*textureColor.z) / 3;//3.z;
	
		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}

		if(somecountermul == 15)
		{
			o.mapmatrix3.w = (textureColor.x*textureColor.y*textureColor.z) / 3;//3.w;

		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}






		if(somecountermul == 16)
		{  
			o.mapmatrix4.x = (textureColor.x*textureColor.y*textureColor.z) / 3;//4.x;
		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}

		 if(somecountermul == 17)
		{
			o.mapmatrix4.y = (textureColor.x*textureColor.y*textureColor.z) / 3;//4.y;
		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}

		 if(somecountermul == 18)
		{
			o.mapmatrix4.z = (textureColor.x*textureColor.y*textureColor.z) / 3;//4.z;
		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}

		 if(somecountermul == 19)
		{
			o.mapmatrix4.w = (textureColor.x*textureColor.y*textureColor.z) / 3;//4.w;
		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}

		if(somecountermul == 20)
		{
			o.mapmatrix5.x = (textureColor.x*textureColor.y*textureColor.z) / 3;//5.x;
		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}

		if(somecountermul == 21)
		{
			o.mapmatrix5.y = (textureColor.x*textureColor.y*textureColor.z) / 3;//5.y;
		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}

		if(somecountermul == 22)
		{
			o.mapmatrix5.z = (textureColor.x*textureColor.y*textureColor.z) / 3;//5.z;
		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}

		if(somecountermul == 23)
		{
			o.mapmatrix5.w = (textureColor.x*textureColor.y*textureColor.z) / 3;//5.w;
		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}

		if(somecountermul == 24)
		{
			o.mapmatrix6.x = (textureColor.x*textureColor.y*textureColor.z) / 3;//6.x;
		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}

		if(somecountermul == 25)
		{
			o.mapmatrix6.y = (textureColor.x*textureColor.y*textureColor.z) / 3;//6.y;
		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}

		if(somecountermul == 26)
		{
			o.mapmatrix6.z = (textureColor.x*textureColor.y*textureColor.z) / 3;//6.z;
		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}

		if(somecountermul == 27)
		{
			o.mapmatrix6.w = (textureColor.x*textureColor.y*textureColor.z) / 3;//6.w;
		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}

		if(somecountermul == 28)
		{
			o.mapmatrix7.x = (textureColor.x*textureColor.y*textureColor.z) / 3;//7.x;
		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}

		if(somecountermul == 29)
		{
			o.mapmatrix7.y = (textureColor.x*textureColor.y*textureColor.z) / 3;//7.y;
		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}

		if(somecountermul == 30)
		{
			o.mapmatrix7.z = (textureColor.x*textureColor.y*textureColor.z) / 3;//7.z;
		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}

		if(somecountermul == 31)
		{
			o.mapmatrix7.w = (textureColor.x*textureColor.y*textureColor.z) / 3;//7.w;
		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}

		if(somecountermul == 32)
		{  
			o.mapmatrix8.x = (textureColor.x*textureColor.y*textureColor.z) / 3;//8.x;
		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}

		if(somecountermul == 33)
		{
			o.mapmatrix8.y = (textureColor.x*textureColor.y*textureColor.z) / 3;//8.y;
		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}

		if(somecountermul == 34)
		{
			o.mapmatrix8.z = (textureColor.x*textureColor.y*textureColor.z) / 3;//8.z;
		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}

		if(somecountermul == 35)
		{
			o.mapmatrix8.w = (textureColor.x*textureColor.y*textureColor.z) / 3;//8.w;
		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}

		if(somecountermul == 36)
		{
			o.mapmatrix9.x = (textureColor.x*textureColor.y*textureColor.z) / 3;//9.x;
		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}

		if(somecountermul == 37)
		{
			o.mapmatrix9.y = (textureColor.x*textureColor.y*textureColor.z) / 3;//9.y;
		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}

		if(somecountermul == 38)
		{
			o.mapmatrix9.z = (textureColor.x*textureColor.y*textureColor.z) / 3;//9.z;
		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}

		if(somecountermul == 39)
		{
			o.mapmatrix9.w = (textureColor.x*textureColor.y*textureColor.z) / 3;//9.w;
		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}

		if(somecountermul == 40)
		{
			o.mapmatrix10.x = (textureColor.x*textureColor.y*textureColor.z) / 3;//10.x;
		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}

		if(somecountermul == 41)
		{
			o.mapmatrix10.y = (textureColor.x*textureColor.y*textureColor.z) / 3;//10.y;
		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}

		if(somecountermul == 42)
		{
			o.mapmatrix10.z = (textureColor.x*textureColor.y*textureColor.z) / 3;//10.z;
		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}

		if(somecountermul == 43)
		{
			o.mapmatrix10.w = (textureColor.x*textureColor.y*textureColor.z) / 3;//10.w;
		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}

		if(somecountermul == 44)
		{
			o.mapmatrix11.x = (textureColor.x*textureColor.y*textureColor.z) / 3;//11.x;
		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}

		if(somecountermul == 45)
		{
			o.mapmatrix11.y = (textureColor.x*textureColor.y*textureColor.z) / 3;//11.y;
		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}

		if(somecountermul == 46)
		{
			o.mapmatrix11.z = (textureColor.x*textureColor.y*textureColor.z) / 3;//11.z;
		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}

		if(somecountermul == 47)
		{
			o.mapmatrix11.w = (textureColor.x*textureColor.y*textureColor.z) / 3;//11.w;
		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}

		if(somecountermul == 48)
		{  
			o.mapmatrix12.x = (textureColor.x*textureColor.y*textureColor.z) / 3;//12.x;
		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}

		if(somecountermul == 49)
		{
			o.mapmatrix12.y = (textureColor.x*textureColor.y*textureColor.z) / 3;//12.y;
		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}

		if(somecountermul == 50)
		{
			o.mapmatrix12.z = (textureColor.x*textureColor.y*textureColor.z) / 3;//12.z;
		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}

		if(somecountermul == 51)
		{
			o.mapmatrix12.w = (textureColor.x*textureColor.y*textureColor.z) / 3;//12.w;
		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}

		if(somecountermul == 52)
		{
			o.mapmatrix13.x = (textureColor.x*textureColor.y*textureColor.z) / 3;//13.x;
		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}

		if(somecountermul == 53)
		{
			o.mapmatrix13.y = (textureColor.x*textureColor.y*textureColor.z) / 3;//13.y;
		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}

		if(somecountermul == 54)
		{
			o.mapmatrix13.z = (textureColor.x*textureColor.y*textureColor.z) / 3;//13.z;
		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}

		if(somecountermul == 55)
		{
			o.mapmatrix13.w = (textureColor.x*textureColor.y*textureColor.z) / 3;//13.w;
		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}

		if(somecountermul == 56)
		{
			o.mapmatrix14.x = (textureColor.x*textureColor.y*textureColor.z) / 3;//14.x;
		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}

		if(somecountermul == 57)
		{
			o.mapmatrix14.y = (textureColor.x*textureColor.y*textureColor.z) / 3;//14.y;
		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}

		if(somecountermul == 58)
		{
			o.mapmatrix14.z = (textureColor.x*textureColor.y*textureColor.z) / 3;//14.z;
		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}

		if(somecountermul == 59)
		{
			o.mapmatrix14.w = (textureColor.x*textureColor.y*textureColor.z) / 3;//14.w;
		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}



		if(somecountermul == 60)
		{
			o.mapmatrix15.x = (textureColor.x*textureColor.y*textureColor.z) / 3;//15.x;
		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}

		if(somecountermul == 61)
		{
			o.mapmatrix15.y = (textureColor.x*textureColor.y*textureColor.z) / 3; //currentMapData = input.mapmatrix15.y;
		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}

		if(somecountermul == 62)
		{
			o.mapmatrix15.z = (textureColor.x*textureColor.y*textureColor.z) / 3; //currentMapData = input.mapmatrix15.z;
		}
		else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}

		if(somecountermul == 63) // 
		{
			o.mapmatrix15.w = (textureColor.x*textureColor.y*textureColor.z) / 3; //	currentMapData = input.mapmatrix15.w;
		}
	else
		{
			o.mapmatrix0 = input[i].mapmatrix0;
			o.mapmatrix1 = input[i].mapmatrix1;
			o.mapmatrix2 = input[i].mapmatrix2;
			o.mapmatrix3 = input[i].mapmatrix3;
			o.mapmatrix4 = input[i].mapmatrix4;
			o.mapmatrix5 = input[i].mapmatrix5;
			o.mapmatrix6 = input[i].mapmatrix6;
			o.mapmatrix7 = input[i].mapmatrix7;
			o.mapmatrix8 = input[i].mapmatrix8;
			o.mapmatrix9 = input[i].mapmatrix9;
			o.mapmatrix10 = input[i].mapmatrix10;
			o.mapmatrix11 = input[i].mapmatrix11;
			o.mapmatrix12 = input[i].mapmatrix12;
			o.mapmatrix13 = input[i].mapmatrix13;
			o.mapmatrix14 = input[i].mapmatrix14;
			o.mapmatrix15 = input[i].mapmatrix15;
		}









		/*
		if(facetype == 0)
		{
			testinputtex = (testinputtex/ float2(oriscreenx,oriscreeny));
			float2 testinputtexY = input[i].tex;
			testinputtex.x = (testinputtex.x + (somemul * (input[i].xindex)));
			testinputtex.x = ((testinputtex.x) + (((1.0f/float(oriscreenx)) * 1) * ((input[i].xindex * tinyChunkWidth) + (x))));
			testinputtex.y = ((testinputtex.y) + (((1.0f/float(oriscreeny)) * 1) * ((input[i].yindex * tinyChunkHeight) + (tinyChunkHeight-1- y))));
			//testinputtex.x *=-1;
			input[i].tex = testinputtex;

			//textureColor = diffuseMap.Sample(textureSampler, input[i].tex);

			o.color = textureColor;
		}
		else
		{
				
			//DRAW TEXTURES ON SIDES
			testinputtex = (testinputtex/ float2(oriscreenx,oriscreeny));
			float2 testinputtexY = input[i].tex;
			testinputtex.x = (testinputtex.x + (somemul * (input[i].xindex)));
			testinputtex.x = ((testinputtex.x) + (((1.0f/float(oriscreenx)) * 1) * ((input[i].xindex * tinyChunkWidth) + (x))));
			testinputtex.y = ((testinputtex.y) + (((1.0f/float(oriscreeny)) * 1) * ((input[i].yindex * tinyChunkHeight) + (tinyChunkHeight-1- y))));
			//testinputtex.x *=-1;
			input[i].tex = testinputtex;
			//textureColor = diffuseMap.Sample(textureSampler, input[i].tex);


			//DRAW TEXTURES ON SIDES
			
			o.color= textureColor;
			//DONT DRAW TEXTURES ON SIDES
			//textureColor = somemoddedinputcolor;
			//DONT DRAW TEXTURES ON SIDES
		}
		*/

		TriStream.Append(o);
	}
	TriStream.RestartStrip();		
}








/*o.mapmatrix0 = input[i].mapmatrix0;
o.mapmatrix1 = input[i].mapmatrix1;
o.mapmatrix2 = input[i].mapmatrix2;
o.mapmatrix3 = input[i].mapmatrix3;
o.mapmatrix4 = input[i].mapmatrix4;
o.mapmatrix5 = input[i].mapmatrix5;
o.mapmatrix6 = input[i].mapmatrix6;
o.mapmatrix7 = input[i].mapmatrix7;
o.mapmatrix8 = input[i].mapmatrix8;
o.mapmatrix9 = input[i].mapmatrix9;
o.mapmatrix10 = input[i].mapmatrix10;
o.mapmatrix11 = input[i].mapmatrix11;
o.mapmatrix12 = input[i].mapmatrix12;
o.mapmatrix13 = input[i].mapmatrix13;
o.mapmatrix14 = input[i].mapmatrix14;
o.mapmatrix15 = input[i].mapmatrix15;*/

