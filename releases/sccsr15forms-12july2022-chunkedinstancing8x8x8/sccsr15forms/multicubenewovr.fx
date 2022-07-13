/////////////////////////////////////////////////////////
//Developed by Steve Chassé aka ninekorn aka nine aka 9//
/////////////////////////////////////////////////////////



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


cbuffer wvp :register(b0)
{
	float4x4 worldmatrix;
	float4x4 viewmatrix;
	float4x4 projectionmatrix;
};


cbuffer LightBuffer :register(b1)
{
	float4 ambientColor;
	float4 diffuseColor;
	float3 lightDirection;
	float padding0;
	float4 lightPosition;
	float padding1;
};
//Texture2D terrain[] : register(t8);
//Texture2D misc[] : register(t0, space1);

/*
cbuffer verticesbuffer :register(b2)
{
	float4 verticesarray[];
};*/

//float4 bufferData = g_Buffer.Load(2);
/*struct Foo
{
	float4 a;
	int2 b;
};
ConstantBuffer<Foo> myCB2 : register(b0, space1);*/

struct VS_IN
{
	float4 position : POSITION0;
	float4 indexpos : POSITION1;
	float4 color : COLOR0; //byte map index xyz and w for typeofface 0 to 5
	float3 normal : NORMAL0;
	float paddingvert0 : PSIZE0;	//instance width
	float2 tex : TEXCOORD0;
	float paddingvert1 : PSIZE1;	//instance height
	float paddingvert2 : PSIZE2;	//instance depth

	float4 instancePosition : POSITION2;

	//containing the bytes where there are faces
	float4 mapmatrix0 : POSITION3;
	float4 mapmatrix1 : POSITION4;
	float4 mapmatrix2 : POSITION5;
	float4 mapmatrix3 : POSITION6;
	//containing the bytes where there are faces

	//containing the dimensions w/h/d of the faces
	float4 vertdimmatrix0 : POSITION7;
	float4 vertdimmatrix1 : POSITION8;
	float4 vertdimmatrix2 : POSITION9;
	float4 vertdimmatrix3 : POSITION10;

	float4 vertdimmatrix4 : POSITION11;
	float4 vertdimmatrix5 : POSITION12;
	float4 vertdimmatrix6 : POSITION13;
	float4 vertdimmatrix7 : POSITION14;
	//containing the dimensions w/h/d of the faces

	//containing the coordinates x/y/z of the faces
	float4 vertfirstloc0 : POSITION15;
	float4 vertfirstloc1 : POSITION16;
	float4 vertfirstloc2 : POSITION17;
	float4 vertfirstloc3 : POSITION18;

	float4 vertfirstloc4 : POSITION19;
	float4 vertfirstloc5 : POSITION20;
	float4 vertfirstloc6 : POSITION21;
	float4 vertfirstloc7 : POSITION22;
	//containing the coordinates x/y/z of the faces
};


struct PS_IN
{
	float4 position : SV_POSITION;
	float4 indexpos : POSITION1;
	float4 color : COLOR0; //byte map index xyz and w for typeofface 0 to 5
	float3 normal : NORMAL0;
	float paddingvert0 : PSIZE0;	//instance width
	float2 tex : TEXCOORD0;
	float paddingvert1 : PSIZE1;	//instance height
	float paddingvert2 : PSIZE2;	//instance depth

	float4 instancePosition : POSITION2;

	/*float4 mapmatrix0 : POSITION3;
	float4 mapmatrix1 : POSITION4;
	float4 mapmatrix2 : POSITION5;
	float4 mapmatrix3 : POSITION6;

	float4 vertdimmatrix0 : POSITION7;
	float4 vertdimmatrix1 : POSITION8;
	float4 vertdimmatrix2 : POSITION9;
	float4 vertdimmatrix3 : POSITION10;

	float4 vertdimmatrix4 : POSITION11;
	float4 vertdimmatrix5 : POSITION12;
	float4 vertdimmatrix6 : POSITION13;
	float4 vertdimmatrix7 : POSITION14;

	float4 vertdimmatrix8 : POSITION15;
	float4 vertdimmatrix9 : POSITION16;
	float4 vertdimmatrix10 : POSITION17;
	float4 vertdimmatrix11 : POSITION18;

	float4 vertdimmatrix12 : POSITION19;
	float4 vertdimmatrix13 : POSITION20;
	float4 vertdimmatrix14 : POSITION21;
	float4 vertdimmatrix15 : POSITION22;*/
};


static int width = 4;
static int height = 4;
static int depth = 4;
static float planesize = 0.006666666666666666666667f;


static const int maxfloatbytemaparraylength = 4; //4 // 8 
static const int maxfloatbytemaparraylengthfull = 5; //5 // 9

static float arrayOfDigits[maxfloatbytemaparraylength];
static float somemaxvecdigit = 4;

PS_IN VS( VS_IN input )
{
	PS_IN output = (PS_IN)0;

	input.position.w = 1.0f;

	//IVE SET THE VERTEX UNINSTANCED COLOR TO HOLD THE INDEX LOCATION OF THE BYTE
	int x = int(input.indexpos.x);
	int y = int(input.indexpos.y);
	int z = int(input.indexpos.z);
	//IVE SET THE VERTEX UNINSTANCED COLOR TO HOLD THE INDEX LOCATION OF THE BYTE

	int vertextype = int(input.indexpos.w);
	//1 is vertex back left.
	//2 is vertex front left.
	//3 is vertex back right.
	//4 is vertex front right.


	//double sometestvar = 0.152f;


	int somemeshwidth = input.paddingvert0;
	int somemeshheight = input.paddingvert1;
	int somemeshdepth = input.paddingvert2;


	double currentfirstvertloc;
	double currentvertdimdata;
	double currentMapData;

	//4*4*4 = 64 voxels per chunk max
	//8*8*8 = 512 voxels per chunk max

	int currentIndex = x + width * (y + height * z); // x + tinyChunkWidth * (y + (tinyChunkHeight * z));
	int someOtherIndex = currentIndex;

	float somecountermul = 0;

	//somecountermul = (currentIndex); //17.25f
	somecountermul = currentIndex / width;

	int somemaxvecdigit = width;

	int swtc0 = 0;
	int swtc1 = 0;

	//int somemin = (somecountermul) * somemaxvecdigit; //63 * 8 == 504
	//int somemid = (somemaxvecdigit / 2 ) + somemin; //504 + 4 = 508
	//int somemax = ((somecountermul + 1 ) * somemaxvecdigit); //8

	int somemin = (somecountermul)*somemaxvecdigit; //0 //8 * 8 = 64 //63 * 8 == 504
	int somemid = round(somemaxvecdigit / 2) + somemin; // 4 //504 + 4 = 508
	int somemax = (somemaxvecdigit)+somemin; //504 + 8 = 512

	//int somemax = ((somecountermul) * somemaxvecdigit); //8
	
	if (currentIndex >= somemin && currentIndex <= somemid - 1) // 0 // 3
	{
		//511010110
		someOtherIndex = 1 + (((somemid - 1) - currentIndex) * 2);
		//index 0 => 1 + (3 - 0) * 2 = 7
		//index 1 => 1 + (3 - 1) * 2 = 5
		//index 2 => 1 + (3 - 2) * 2 = 3
		//index 3 => 1 + (3 - 3) * 2 = 1

		/*if(someOtherIndex == 7)
		{
			someOtherIndex = 0;
		}
		else if(someOtherIndex == 5)
		{
			someOtherIndex = 2;
		}
		else if(someOtherIndex == 3)
		{
			someOtherIndex = 4;
		}
		else if(someOtherIndex == 1)
		{
			someOtherIndex = 6;
		}
		*/
		swtc0 = 1;
	}
	else if (currentIndex >= somemid && currentIndex <= somemax - 1) // 4 // 7
	{
		//511010110

		someOtherIndex = 0 + (((somemax - 1) - currentIndex) * 2);
		//index 4 => 0 + (7 - 4) * 2 = 6
		//index 5 => 0 + (7 - 5) * 2 = 4
		//index 6 => 0 + (7 - 6) * 2 = 2
		//index 7 => 0 + (7 - 7) * 2 = 0

		//index 7 => 0 + (512-1 - 508) * 2 = 0

		/*if(someOtherIndex == 6)
		{
			someOtherIndex = 1;
		}
		else if(someOtherIndex == 4)
		{
			someOtherIndex = 3;
		}
		else if(someOtherIndex == 2)
		{
			someOtherIndex = 5;
		}
		else if(someOtherIndex == 0)
		{
			someOtherIndex = 7;
		}*/
		swtc1 = 1;
	}


	/*
	if (somecountermul == 0)
	{
		currentMapData = input.mapmatrix0.x;
	}
	else if (somecountermul == 1)
	{
		currentMapData = input.mapmatrix0.y;
	}
	else if (somecountermul == 2)
	{
		currentMapData = input.mapmatrix0.z;
	}
	else if (somecountermul == 3)
	{
		currentMapData = input.mapmatrix0.w;
	}
	else if (somecountermul == 4)
	{
		currentMapData = input.mapmatrix1.x;
	}
	else if (somecountermul == 5)
	{
		currentMapData = input.mapmatrix1.y;

	}
	else if (somecountermul == 6)
	{
		currentMapData = input.mapmatrix1.z;

	}
	else if (somecountermul == 7)
	{
		currentMapData = input.mapmatrix1.w;

	}
	else if (somecountermul == 8)
	{
		currentMapData = input.mapmatrix2.x;

	}
	else if (somecountermul == 9)
	{
		currentMapData = input.mapmatrix2.y;
	}
	else if (somecountermul == 10)
	{
		currentMapData = input.mapmatrix2.z;

	}
	else if (somecountermul == 11)
	{
		currentMapData = input.mapmatrix2.w;
	}
	else if (somecountermul == 12)
	{
		currentMapData = input.mapmatrix3.x;

	}
	else if (somecountermul == 13)
	{
		currentMapData = input.mapmatrix3.y;

	}
	else if (somecountermul == 14)
	{
		currentMapData = input.mapmatrix3.z;

	}
	else if (somecountermul == 15)
	{
		currentMapData = input.mapmatrix3.w;
	}*/



	//0 16 32 48
	//4 20 36 52
	//8 24 40 56
	//12 28 44 60
	//1 17 33 49
	//5 21 37 53
	//9 25 41 57
	//13 29 45 61
	//2 18 34 50
	//6 22 38 54 
	//10 26 42 58 
	//14 30 46 62
	//3 19 35 51
	//7 23 39 55
	//11 27 43 59
	//15 31 47 63

	somecountermul = currentIndex;
	int someindexpos = 0;

	if (currentIndex == 0 || currentIndex == 16 || currentIndex == 32 || currentIndex == 48)
	{
		if (currentIndex == 0)
		{
			someindexpos = 3;
		}
		else if (currentIndex == 16)
		{
			someindexpos = 2;
		}
		else if (currentIndex == 32)
		{
			someindexpos = 1;
		}
		else if (currentIndex == 48)
		{
			someindexpos = 0;
		}

		currentMapData = (double)input.mapmatrix0.x;
	}
	else if (currentIndex == 4 || currentIndex == 20 || currentIndex == 36 || currentIndex == 52)
	{
		if (currentIndex == 4)
		{
			someindexpos = 3;
		}
		else if (currentIndex == 20)
		{
			someindexpos = 2;
		}
		else if (currentIndex == 36)
		{
			someindexpos = 1;
		}
		else if (currentIndex == 52)
		{
			someindexpos = 0;
		}
		currentMapData = (double)input.mapmatrix0.y;
	}
	else if (currentIndex == 8 || currentIndex == 24 || currentIndex == 40 || currentIndex == 56)
	{
		if (currentIndex == 8)
		{
			someindexpos = 3;
		}
		else if (currentIndex == 24)
		{
			someindexpos = 2;
		}
		else if (currentIndex == 40)
		{
			someindexpos = 1;
		}
		else if (currentIndex == 56)
		{
			someindexpos = 0;
		}
		currentMapData = (double)input.mapmatrix0.z;
	}
	else if (currentIndex == 12 || currentIndex == 28 || currentIndex == 44 || currentIndex == 60)
	{
		if (currentIndex == 12)
		{
			someindexpos = 3;
		}
		else if (currentIndex == 28)
		{
			someindexpos = 2;
		}
		else if (currentIndex == 44)
		{
			someindexpos = 1;
		}
		else if (currentIndex == 60)
		{
			someindexpos = 0;
		}
		currentMapData = (double)input.mapmatrix0.w;
	}
	else if (currentIndex == 1 || currentIndex == 17 || currentIndex == 33 || currentIndex == 49)
	{
		if (currentIndex == 1)
		{
			someindexpos = 3;
		}
		else if (currentIndex == 17)
		{
			someindexpos = 2;
		}
		else if (currentIndex == 33)
		{
			someindexpos = 1;
		}
		else if (currentIndex == 49)
		{
			someindexpos = 0;
		}
		currentMapData = (double)input.mapmatrix1.x;
	}
	else if (currentIndex == 5 || currentIndex == 21 || currentIndex == 37 || currentIndex == 53)
	{
		if (currentIndex == 5)
		{
			someindexpos = 3;
		}
		else if (currentIndex == 21)
		{
			someindexpos = 2;
		}
		else if (currentIndex == 37)
		{
			someindexpos = 1;
		}
		else if (currentIndex == 53)
		{
			someindexpos = 0;
		}
		currentMapData = (double)input.mapmatrix1.y;
	}
	else if (currentIndex == 9 || currentIndex == 25 || currentIndex == 41 || currentIndex == 57)
	{
		if (currentIndex == 9)
		{
			someindexpos = 3;
		}
		else if (currentIndex == 25)
		{
			someindexpos = 2;
		}
		else if (currentIndex == 41)
		{
			someindexpos = 1;
		}
		else if (currentIndex == 57)
		{
			someindexpos = 0;
		}
		currentMapData = (double)input.mapmatrix1.z;
	}
	else if (currentIndex == 13 || currentIndex == 29 || currentIndex == 45 || currentIndex == 61)
	{
		if (currentIndex == 13)
		{
			someindexpos = 3;
		}
		else if (currentIndex == 29)
		{
			someindexpos = 2;
		}
		else if (currentIndex == 45)
		{
			someindexpos = 1;
		}
		else if (currentIndex == 61)
		{
			someindexpos = 0;
		}
		currentMapData = (double)input.mapmatrix1.w;
	}
	else if (currentIndex == 2 || currentIndex == 18 || currentIndex == 34 || currentIndex == 50)
	{
		if (currentIndex == 2)
		{
			someindexpos = 3;
		}
		else if (currentIndex == 18)
		{
			someindexpos = 2;
		}
		else if (currentIndex == 34)
		{
			someindexpos = 1;
		}
		else if (currentIndex == 50)
		{
			someindexpos = 0;
		}
		currentMapData = (double)input.mapmatrix2.x;
	}
	else if (currentIndex == 6 || currentIndex == 22 || currentIndex == 38 || currentIndex == 54)
	{
		if (currentIndex == 6)
		{
			someindexpos = 3;
		}
		else if (currentIndex == 22)
		{
			someindexpos = 2;
		}
		else if (currentIndex == 38)
		{
			someindexpos = 1;
		}
		else if (currentIndex == 54)
		{
			someindexpos = 0;
		}
		currentMapData = (double)input.mapmatrix2.y;
	}
	else if (currentIndex == 10 || currentIndex == 26 || currentIndex == 42 || currentIndex == 58)
	{
		if (currentIndex == 10)
		{
			someindexpos = 3;
		}
		else if (currentIndex == 26)
		{
			someindexpos = 2;
		}
		else if (currentIndex == 42)
		{
			someindexpos = 1;
		}
		else if (currentIndex == 58)
		{
			someindexpos = 0;
		}
		currentMapData = (double)input.mapmatrix2.z;
	}
	else if (currentIndex == 14 || currentIndex == 30 || currentIndex == 46 || currentIndex == 62)
	{
		if (currentIndex == 14)
		{
			someindexpos = 3;
		}
		else if (currentIndex == 30)
		{
			someindexpos = 2;
		}
		else if (currentIndex == 46)
		{
			someindexpos = 1;
		}
		else if (currentIndex == 62)
		{
			someindexpos = 0;
		}
		currentMapData = (double)input.mapmatrix2.w;
	}
	else if (currentIndex == 3 || currentIndex == 19 || currentIndex == 35 || currentIndex == 51)
	{
		if (currentIndex == 3)
		{
			someindexpos = 3;
		}
		else if (currentIndex == 19)
		{
			someindexpos = 2;
		}
		else if (currentIndex == 35)
		{
			someindexpos = 1;
		}
		else if (currentIndex == 51)
		{
			someindexpos = 0;
		}
		currentMapData = (double)input.mapmatrix3.x;
	}
	else if (currentIndex == 7 || currentIndex == 23 || currentIndex == 39 || currentIndex == 55)
	{
		if (currentIndex == 7)
		{
			someindexpos = 3;
		}
		else if (currentIndex == 23)
		{
			someindexpos = 2;
		}
		else if (currentIndex == 39)
		{
			someindexpos = 1;
		}
		else if (currentIndex == 55)
		{
			someindexpos = 0;
		}
		currentMapData = (double)input.mapmatrix3.y;		
	}
	else if (currentIndex == 11 || currentIndex == 27 || currentIndex == 43 || currentIndex == 59)
	{
		if (currentIndex == 11)
		{
			someindexpos = 3;
		}
		else if (currentIndex == 27)
		{
			someindexpos = 2;
		}
		else if (currentIndex == 43)
		{
			someindexpos = 1;
		}
		else if (currentIndex == 59)
		{
			someindexpos = 0;
		}
		currentMapData = (double)input.mapmatrix3.z;
	}
	else if (currentIndex == 15 || currentIndex == 31 || currentIndex == 47 || currentIndex == 63)
	{
		if (currentIndex == 15)
		{
			someindexpos = 3;
		}
		else if (currentIndex == 31)
		{
			someindexpos = 2;
		}
		else if (currentIndex == 47)
		{
			someindexpos = 1;
		}
		else if (currentIndex == 63)
		{
			someindexpos = 0;
		}
		currentMapData = (double)input.mapmatrix3.w;
	}



	int themul = 100;

	int someextravalue = trunc(currentMapData);
	for (int i = 0; i < (0 + someindexpos); i++)
	{
		someextravalue = someextravalue / themul;
	}
	int someremainsof = someextravalue / themul;
	int theextravalue = (someextravalue - (someremainsof * themul));

	










	int testera = 0;
	int substract = 0;
	int before0 = 0;
	int theByte = 0;
	/*
	if (someOtherIndex == 0) // problem gotta use a different approach for the index 0 and 2
	{

		float someval0 = (float)(int)(currentMapData / 10); // 511111111 / 10 = 51111111.1
		//float someval1 = round(currentMapData / 10); 
		float someval2 = (someval0 * 10); //// 511111111 / 10 = 51111111.1


		//WORKS

		if (someval2 + 1 == currentMapData)
		{
			arrayOfDigits[0] = 1.0;
		}
		else
		{
			arrayOfDigits[0] = 0.0;
		}


	}
	else if (someOtherIndex == 1)
	{
		float someData0 = currentMapData;

		for (int i = 0; i < 4; i++)
		{
			someData0 = int(someData0 * 0.1f);
		}

		before0 = int(trunc(someData0));
		testera = before0 >> 1 << 1;
		theByte = before0 - testera;
		arrayOfDigits[someOtherIndex] = theByte;
	}
	else if (someOtherIndex == 2) // problem gotta use a different approach for the index 0 and 2
	{

		float someval0 = (float)(int)(currentMapData / 100); // 511111111 / 10 = 51111111.1
		//float someval1 = round(currentMapData / 100); 
		float someval2 = (someval0 * 100); //// 511111111 / 10 = 51111111.1


		//WORKS

		if (someval2 + 11 == currentMapData || someval2 + 10 == currentMapData)
		{
			arrayOfDigits[2] = 1.0;
		}
		else
		{
			arrayOfDigits[2] = 0.0;
		}

	}
	else if (someOtherIndex == 3)
	{
		float someData0 = currentMapData;

		for (int i = 0; i < 5; i++)
		{
			someData0 = int(someData0 * 0.1f);
		}

		before0 = int(trunc(someData0));
		testera = before0 >> 1 << 1;
		theByte = before0 - testera;
		arrayOfDigits[someOtherIndex] = theByte;
	}*/




	/*
	if (someindexpos == 0)
	{
		testera = (int)currentMapData >> 1 << 1;
		theByte = (int)currentMapData - testera;
		arrayOfDigits[someindexpos] = theByte;
	}
	else
	{
		float someData0 = currentMapData;

		for (int i = 0; i < someindexpos; i++)
		{
			someData0 = int(someData0 * 0.1f);
		}

		before0 = int(trunc(someData0));
		testera = before0 >> 1 << 1;
		theByte = before0 - testera;
		arrayOfDigits[someindexpos] = theByte;
	}*/

	/*
	int somenewdata = currentMapData;
	for (int i = 0; i < (someindexpos + 1); i++)
	{
		somenewdata = somenewdata / 10;
	}
	//int someres0 = somenewdata / 10;
	float somefinalindex = somenewdata - (someres0 * 10);*/



	
	/*
	
	float somenewmapdata = currentMapData;
	for (int i = 0; i < (somemulfordigit+1); i++)
	{
		somenewmapdata = (somenewmapdata * 0.1f);
	}

	float somenewresult = somenewmapdata - (float)floor(somenewmapdata);
	float somenewmapresult = (float)floor(somenewresult * 10.0f);
	
	*/


	//each float can have max of 3 faces that contain 3 digits each for w/h/d







	/*
	int somemaxdigits = 9;
	
	somecountermul = (((currentIndex * 3) / width));
	*/






	/*int somemaxvecdigitvertdims = 9;

	somemin = (somecountermul)*somemaxvecdigitvertdims; //0 //8 * 8 = 64 //63 * 8 == 504
	somemid = round(somemaxvecdigitvertdims / 2) + somemin; // 4 //504 + 4 = 508
	somemax = (somemaxvecdigitvertdims)+somemin; //504 + 8 = 512*/
	





	/*
	somemin = (somecountermul)*somemaxdigits; //0 //8 * 8 = 64 //63 * 8 == 504
	somemid = round(somemaxvecdigit / 2) + somemin; // 4 //504 + 4 = 508
	somemax = (somemaxvecdigit)+somemin; //504 + 8 = 512

	//int somemax = ((somecountermul) * somemaxvecdigit); //8

	if (currentIndex >= somemin && currentIndex <= somemid - 1) // 0 // 3
	{
		//511010110
		someOtherIndex = 1 + (((somemid - 1) - currentIndex) * 2);
		//index 0 => 1 + (3 - 0) * 2 = 7
		//index 1 => 1 + (3 - 1) * 2 = 5
		//index 2 => 1 + (3 - 2) * 2 = 3
		//index 3 => 1 + (3 - 3) * 2 = 1






		/*if(someOtherIndex == 7)
		{
			someOtherIndex = 0;
		}
		else if(someOtherIndex == 5)
		{
			someOtherIndex = 2;
		}
		else if(someOtherIndex == 3)
		{
			someOtherIndex = 4;
		}
		else if(someOtherIndex == 1)
		{
			someOtherIndex = 6;
		}

		swtc0 = 1;
	}
	else if (currentIndex >= somemid && currentIndex <= somemax - 1) // 4 // 7
	{
		//511010110

		someOtherIndex = 0 + (((somemax - 1) - currentIndex) * 2);
		//index 4 => 0 + (7 - 4) * 2 = 6
		//index 5 => 0 + (7 - 5) * 2 = 4
		//index 6 => 0 + (7 - 6) * 2 = 2
		//index 7 => 0 + (7 - 7) * 2 = 0

		//index 7 => 0 + (512-1 - 508) * 2 = 0

		/*if(someOtherIndex == 6)
		{
			someOtherIndex = 1;
		}
		else if(someOtherIndex == 4)
		{
			someOtherIndex = 3;
		}
		else if(someOtherIndex == 2)
		{
			someOtherIndex = 5;
		}
		else if(someOtherIndex == 0)
		{
			someOtherIndex = 7;
		}
		swtc1 = 1;
	}*/

	/*
	int anotherindex = 0;
	//int someresult = currentIndex % 3;
	int theNumber = 3;
	int remainder = 0;
	int totalTimes = 0;

	for (int i = 0; i < currentIndex; i++)
	{
		if (anotherindex == theNumber)
		{
			anotherindex = 0;
			totalTimes++;
		}
		if (totalTimes * theNumber >= currentIndex) // >=?? why not only >
		{
			break;
		}
		anotherindex++;
	}

	somecountermul = (int)floor((currentIndex) / 3.0f);*/

	//currentvertdimdata = input.vertdimmatrix0.x;

	//0 16 32
	//48 4 20
	//36 52 8
	//24 40 56
	//12 28 44
	//60 1 17
	//33 49 5
	//21 37 53
	//9 25 41
	//57 13 29
	//45 61 2
	//18 34 50
	//6 22 38
	//54 10 26
	//42 58 14
	//30 46 62 
	//3 19 35
	//51 7 23
	//39 55 11
	//27 43 59 
	//15 31 47
	//63

	somecountermul = currentIndex;

	int somemulfordigit = 0;

	/*
	if (somecountermul == 0 || somecountermul == 16 || somecountermul == 32)
	{
		if (somecountermul == 0)
		{
			somemulfordigit = 6;
		}
		else if (somecountermul == 16)
		{
			somemulfordigit = 3;
		}
		else if (somecountermul == 32)
		{
			somemulfordigit = 0;
		}

		currentvertdimdata = input.vertdimmatrix0.x;
	}
	else if (somecountermul == 48 || somecountermul == 4 || somecountermul == 20)
	{
		if (somecountermul == 48)
		{
			somemulfordigit = 6;
		}
		else if (somecountermul == 4)
		{
			somemulfordigit = 3;
		}
		else if (somecountermul == 20)
		{
			somemulfordigit = 0;
		}

		currentvertdimdata = input.vertdimmatrix0.y;
	}
	else if (somecountermul == 36 || somecountermul == 52 || somecountermul == 8)
	{
		if (somecountermul == 36)
		{
			somemulfordigit = 6;
		}
		else if (somecountermul == 52)
		{
			somemulfordigit = 3;
		}
		else if (somecountermul == 8)
		{
			somemulfordigit = 0;
		}
		currentvertdimdata = input.vertdimmatrix0.z;
	}
	else if (somecountermul == 24 || somecountermul == 40 || somecountermul == 56)
	{
		if (somecountermul == 24)
		{
			somemulfordigit = 6;
		}
		else if (somecountermul == 40)
		{
			somemulfordigit = 3;
		}
		else if (somecountermul == 56)
		{
			somemulfordigit = 0;
		}
		currentvertdimdata = input.vertdimmatrix0.w;
	}
	else if (somecountermul == 12 || somecountermul == 28 || somecountermul == 44)
	{
		if (somecountermul == 12)
		{
			somemulfordigit = 6;
		}
		else if (somecountermul == 28)
		{
			somemulfordigit = 3;
		}
		else if (somecountermul == 44)
		{
			somemulfordigit = 0;
		}
		currentvertdimdata = input.vertdimmatrix1.x;
	}
	else if (somecountermul == 60 || somecountermul == 1 || somecountermul == 17)
	{
		if (somecountermul == 60)
		{
			somemulfordigit = 6;
		}
		else if (somecountermul == 1)
		{
			somemulfordigit = 3;
		}
		else if (somecountermul == 17)
		{
			somemulfordigit = 0;
		}
		currentvertdimdata = input.vertdimmatrix1.y;
	}
	else if (somecountermul == 33 || somecountermul == 49 || somecountermul == 5)
	{
		if (somecountermul == 33)
		{
			somemulfordigit = 6;
		}
		else if (somecountermul == 49)
		{
			somemulfordigit = 3;
		}
		else if (somecountermul == 5)
		{
			somemulfordigit = 0;
		}
		currentvertdimdata = input.vertdimmatrix1.z;
	}
	else if (somecountermul == 21 || somecountermul == 37 || somecountermul == 53)
	{
		if (somecountermul == 21)
		{
			somemulfordigit = 6;
		}
		else if (somecountermul == 37)
		{
			somemulfordigit = 3;
		}
		else if (somecountermul == 53)
		{
			somemulfordigit = 0;
		}
		currentvertdimdata = input.vertdimmatrix1.w;
	}
	else if (somecountermul == 9 || somecountermul == 25 || somecountermul == 41)
	{
		if (somecountermul == 9)
		{
			somemulfordigit = 6;
		}
		else if (somecountermul == 25)
		{
			somemulfordigit = 3;
		}
		else if (somecountermul == 41)
		{
			somemulfordigit = 0;
		}
		currentvertdimdata = input.vertdimmatrix2.x;
	}
	else if (somecountermul == 57 || somecountermul == 13 || somecountermul == 29)
	{
		if (somecountermul == 57)
		{
			somemulfordigit = 6;
		}
		else if (somecountermul == 13)
		{
			somemulfordigit = 3;
		}
		else if (somecountermul == 29)
		{
			somemulfordigit = 0;
		}

		currentvertdimdata = input.vertdimmatrix2.y;
	}
	else if (somecountermul == 45 || somecountermul == 61 || somecountermul == 2)
	{
		if (somecountermul == 45)
		{
			somemulfordigit = 6;
		}
		else if (somecountermul == 61)
		{
			somemulfordigit = 3;
		}
		else if (somecountermul == 2)
		{
			somemulfordigit = 0;
		}
		currentvertdimdata = input.vertdimmatrix2.z;
	}
	else if (somecountermul == 18 || somecountermul == 34 || somecountermul == 50)
	{
		if (somecountermul == 18)
		{
			somemulfordigit = 6;
		}
		else if (somecountermul == 34)
		{
			somemulfordigit = 3;
		}
		else if (somecountermul == 50)
		{
			somemulfordigit = 0;
		}

		currentvertdimdata = input.vertdimmatrix2.w;
	}
	else if (somecountermul == 6 || somecountermul == 22 || somecountermul == 38)
	{
		if (somecountermul == 6)
		{
			somemulfordigit = 6;
		}
		else if (somecountermul == 22)
		{
			somemulfordigit = 3;
		}
		else if (somecountermul == 38)
		{
			somemulfordigit = 0;
		}
		currentvertdimdata = input.vertdimmatrix3.x;
	}
	else if (somecountermul == 54 || somecountermul == 10 || somecountermul == 26)
	{
		if (somecountermul == 54)
		{
			somemulfordigit = 6;
		}
		else if (somecountermul == 10)
		{
			somemulfordigit = 3;
		}
		else if (somecountermul == 26)
		{
			somemulfordigit = 0;
		}
		currentvertdimdata = input.vertdimmatrix3.y;
	}
	else if (somecountermul == 42 || somecountermul == 58 || somecountermul == 14)
	{
		if (somecountermul == 42)
		{
			somemulfordigit = 6;
		}
		else if (somecountermul == 58)
		{
			somemulfordigit = 3;
		}
		else if (somecountermul == 14)
		{
			somemulfordigit = 0;
		}
		currentvertdimdata = input.vertdimmatrix3.z;
	}
	else if (somecountermul == 30 || somecountermul == 46 || somecountermul == 62)
	{
		if (somecountermul == 30)
		{
			somemulfordigit = 6;
		}
		else if (somecountermul == 46)
		{
			somemulfordigit = 3;
		}
		else if (somecountermul == 62)
		{
			somemulfordigit = 0;
		}
		currentvertdimdata = input.vertdimmatrix3.w;
	}
	else if (somecountermul == 3 || somecountermul == 19 || somecountermul == 35)
	{
		if (somecountermul == 3)
		{
			somemulfordigit = 6;
		}
		else if (somecountermul == 19)
		{
			somemulfordigit = 3;
		}
		else if (somecountermul == 35)
		{
			somemulfordigit = 0;
		}
		currentvertdimdata = input.vertdimmatrix4.x;
	}
	else if (somecountermul == 51 || somecountermul == 7 || somecountermul == 23)
	{
		if (somecountermul == 51)
		{
			somemulfordigit = 6;
		}
		else if (somecountermul == 7)
		{
			somemulfordigit = 3;
		}
		else if (somecountermul == 23)
		{
			somemulfordigit = 0;
		}
		currentvertdimdata = input.vertdimmatrix4.y;
	}
	else if (somecountermul == 39 || somecountermul == 55 || somecountermul == 11)
	{
		if (somecountermul == 39)
		{
			somemulfordigit = 6;
		}
		else if (somecountermul == 55)
		{
			somemulfordigit = 3;
		}
		else if (somecountermul == 11)
		{
			somemulfordigit = 0;
		}
		currentvertdimdata = input.vertdimmatrix4.z;
	}
	else if (somecountermul == 27 || somecountermul == 43 || somecountermul == 59)
	{
		if (somecountermul == 27)
		{
			somemulfordigit = 6;
		}
		else if (somecountermul == 43)
		{
			somemulfordigit = 3;
		}
		else if (somecountermul == 59)
		{
			somemulfordigit = 0;
		}
		currentvertdimdata = input.vertdimmatrix4.w;
	}
	else if (somecountermul == 15 || somecountermul == 31 || somecountermul == 47)
	{
		if (somecountermul == 15)
		{
			somemulfordigit = 6;
		}
		else if (somecountermul == 31)
		{
			somemulfordigit = 3;
		}
		else if (somecountermul == 47)
		{
			somemulfordigit = 0;
		}
		currentvertdimdata = input.vertdimmatrix5.x;
	}
	else if (somecountermul == 63)
	{
		if (somecountermul == 63)
		{
			somemulfordigit = 6;
		}

		currentvertdimdata = input.vertdimmatrix5.y;
	}*/


	
	
	if (currentIndex == 0 || currentIndex == 16)
	{
		if (currentIndex == 0)
		{
			somemulfordigit = 2;
		}
		else if (currentIndex == 16)
		{
			somemulfordigit = 0;
		}

		currentvertdimdata = (double)input.vertdimmatrix0.x;
		currentfirstvertloc = (double)input.vertfirstloc0.x;
	}
	else if (currentIndex == 32 || currentIndex == 48)
	{
		if (currentIndex == 32)
		{
			somemulfordigit = 2;
		}
		else if (currentIndex == 48)
		{
			somemulfordigit = 0;
		}

		currentvertdimdata = (double)input.vertdimmatrix0.y;
		currentfirstvertloc = (double)input.vertfirstloc0.y;
	}
	else if (currentIndex == 4 || currentIndex == 20)
	{
		if (currentIndex == 4)
		{
			somemulfordigit = 2;
		}
		else if (currentIndex == 20)
		{
			somemulfordigit = 0;
		}

		currentvertdimdata = (double)input.vertdimmatrix0.z;
		currentfirstvertloc = (double)input.vertfirstloc0.z;
	}
	else if (currentIndex == 36 || currentIndex == 52)
	{
		if (currentIndex == 36)
		{
			somemulfordigit = 2;
		}
		else if (currentIndex == 52)
		{
			somemulfordigit = 0;
		}

		currentvertdimdata = (double)input.vertdimmatrix0.w;
		currentfirstvertloc = (double)input.vertfirstloc0.w;
	}
	else if (currentIndex == 8 || currentIndex == 24)
	{
		if (currentIndex == 8)
		{
			somemulfordigit = 2;
		}
		else if (currentIndex == 24)
		{
			somemulfordigit = 0;
		}

		currentvertdimdata = (double)input.vertdimmatrix1.x;
		currentfirstvertloc = (double)input.vertfirstloc1.x;
	}
	else if (currentIndex == 40 || currentIndex == 56)
	{
		if (currentIndex == 40)
		{
			somemulfordigit = 2;
		}
		else if (currentIndex == 56)
		{
			somemulfordigit = 0;
		}

		currentvertdimdata = (double)input.vertdimmatrix1.y;
		currentfirstvertloc = (double)input.vertfirstloc1.y;
	}
	else if (currentIndex == 12 || currentIndex == 28)
	{
		if (currentIndex == 12)
		{
			somemulfordigit = 2;
		}
		else if (currentIndex == 28)
		{
			somemulfordigit = 0;
		}

		currentvertdimdata = (double)input.vertdimmatrix1.z;
		currentfirstvertloc = (double)input.vertfirstloc1.z;
	}
	else if (currentIndex == 44 || currentIndex == 60)
	{
		if (currentIndex == 44)
		{
			somemulfordigit = 2;
		}
		else if (currentIndex == 60)
		{
			somemulfordigit = 0;
		}

		currentvertdimdata = (double)input.vertdimmatrix1.w;
		currentfirstvertloc = (double)input.vertfirstloc1.w;
	}
	else if (currentIndex == 1 || currentIndex == 17)
	{
		if (currentIndex == 1)
		{
			somemulfordigit = 2;
		}
		else if (currentIndex == 17)
		{
			somemulfordigit = 0;
		}

		currentvertdimdata = (double)input.vertdimmatrix2.x;
		currentfirstvertloc = (double)input.vertfirstloc2.x;
	}
	else if (currentIndex == 33 || currentIndex == 49)
	{
		if (currentIndex == 33)
		{
			somemulfordigit = 2;
		}
		else if (currentIndex == 49)
		{
			somemulfordigit = 0;
		}

		currentvertdimdata = (double)input.vertdimmatrix2.y;
		currentfirstvertloc = (double)input.vertfirstloc2.y;
	}
	else if (currentIndex == 5 || currentIndex == 21)
	{
		if (currentIndex == 5)
		{
			somemulfordigit = 2;
		}
		else if (currentIndex == 21)
		{
			somemulfordigit = 0;
		}

		currentvertdimdata = (double)input.vertdimmatrix2.z;
		currentfirstvertloc = (double)input.vertfirstloc2.z;
	}
	else if (currentIndex == 37 || currentIndex == 53)
	{
		if (currentIndex == 37)
		{
			somemulfordigit = 2;
		}
		else if (currentIndex == 53)
		{
			somemulfordigit = 0;
		}

		currentvertdimdata = (double)input.vertdimmatrix2.w;
		currentfirstvertloc = (double)input.vertfirstloc2.w;
	}
	else if (currentIndex == 9 || currentIndex == 25)
	{
		if (currentIndex == 9)
		{
			somemulfordigit = 2;
		}
		else if (currentIndex == 25)
		{
			somemulfordigit = 0;
		}

		currentvertdimdata = (double)input.vertdimmatrix3.x;
		currentfirstvertloc = (double)input.vertfirstloc3.x;
	}
	else if (currentIndex == 41 || currentIndex == 57)
	{
		if (currentIndex == 41)
		{
			somemulfordigit = 2;
		}
		else if (currentIndex == 57)
		{
			somemulfordigit = 0;
		}


		currentvertdimdata = (double)input.vertdimmatrix3.y;
		currentfirstvertloc = (double)input.vertfirstloc3.y;
	}
	else if (currentIndex == 13 || currentIndex == 29)
	{
		if (currentIndex == 13)
		{
			somemulfordigit = 2;
		}
		else if (currentIndex == 29)
		{
			somemulfordigit = 0;
		}


		currentvertdimdata = (double)input.vertdimmatrix3.z;
		currentfirstvertloc = (double)input.vertfirstloc3.z;
	}
	else if (currentIndex == 45 || currentIndex == 61)
	{
		if (currentIndex == 45)
		{
			somemulfordigit = 2;
		}
		else if (currentIndex == 61)
		{
			somemulfordigit = 0;
		}


		currentvertdimdata = (double)input.vertdimmatrix3.w;
		currentfirstvertloc = (double)input.vertfirstloc3.w;
	}
	else if (currentIndex == 2 || currentIndex == 18)
	{
		if (currentIndex == 2)
		{
			somemulfordigit = 2;
		}
		else if (currentIndex == 18)
		{
			somemulfordigit = 0;
		}


		currentvertdimdata = (double)input.vertdimmatrix4.x;
		currentfirstvertloc = (double)input.vertfirstloc4.x;
	}
	else if (currentIndex == 34 || currentIndex == 50)
	{
		if (currentIndex == 34)
		{
			somemulfordigit = 2;
		}
		else if (currentIndex == 50)
		{
			somemulfordigit = 0;
		}

		currentvertdimdata = (double)input.vertdimmatrix4.y;
		currentfirstvertloc = (double)input.vertfirstloc4.y;
	}
	else if (currentIndex == 6 || currentIndex == 22)
	{
		if (currentIndex == 6)
		{
			somemulfordigit = 2;
		}
		else if (currentIndex == 22)
		{
			somemulfordigit = 0;
		}

		currentvertdimdata = (double)input.vertdimmatrix4.z;
		currentfirstvertloc = (double)input.vertfirstloc4.z;
	}
	else if (currentIndex == 38 || currentIndex == 54)
	{
		if (currentIndex == 38)
		{
			somemulfordigit = 2;
		}
		else if (currentIndex == 54)
		{
			somemulfordigit = 0;
		}

		currentvertdimdata = (double)input.vertdimmatrix4.w;
		currentfirstvertloc = (double)input.vertfirstloc4.w;
	}
	else if (currentIndex == 10 || currentIndex == 26)
	{
		if (currentIndex == 10)
		{
			somemulfordigit = 2;
		}
		else if (currentIndex == 26)
		{
			somemulfordigit = 0;
		}

		currentvertdimdata = (double)input.vertdimmatrix5.x;
		currentfirstvertloc = (double)input.vertfirstloc5.x;
	}
	else if (currentIndex == 42 || currentIndex == 58)
	{
		if (currentIndex == 42)
		{
			somemulfordigit = 2;
		}
		else if (currentIndex == 58)
		{
			somemulfordigit = 0;
		}

		currentvertdimdata = (double)input.vertdimmatrix5.y;
		currentfirstvertloc = (double)input.vertfirstloc5.y;
	}
	else if (currentIndex == 14 || currentIndex == 30)
	{
		if (currentIndex == 14)
		{
			somemulfordigit = 2;
		}
		else if (currentIndex == 30)
		{
			somemulfordigit = 0;
		}

		currentvertdimdata = (double)input.vertdimmatrix5.z;
		currentfirstvertloc = (double)input.vertfirstloc5.z;
	}
	else if (currentIndex == 46 || currentIndex == 62)
	{
		if (currentIndex == 46)
		{
			somemulfordigit = 2;
		}
		else if (currentIndex == 62)
		{
			somemulfordigit = 0;
		}

		currentvertdimdata = (double)input.vertdimmatrix5.w;
		currentfirstvertloc = (double)input.vertfirstloc5.w;
	}
	else if (currentIndex == 3 || currentIndex == 19)
	{
		if (currentIndex == 3)
		{
			somemulfordigit = 2;
		}
		else if (currentIndex == 19)
		{
			somemulfordigit = 0;
		}

		currentvertdimdata = (double)input.vertdimmatrix6.x;
		currentfirstvertloc = (double)input.vertfirstloc6.x;
	}
	else if (currentIndex == 35 || currentIndex == 51)
	{
		if (currentIndex == 35)
		{
			somemulfordigit = 2;
		}
		else if (currentIndex == 51)
		{
			somemulfordigit = 0;
		}

		currentvertdimdata = (double)input.vertdimmatrix6.y;
		currentfirstvertloc = (double)input.vertfirstloc6.y;
	}
	else if (currentIndex == 7 || currentIndex == 23)
	{
		if (currentIndex == 7)
		{
			somemulfordigit = 2;
		}
		else if (currentIndex == 23)
		{
			somemulfordigit = 0;
		}

		currentvertdimdata = (double)input.vertdimmatrix6.z;
		currentfirstvertloc = (double)input.vertfirstloc6.z;
	}
	else if (currentIndex == 39 || currentIndex == 55)
	{
		if (currentIndex == 39)
		{
			somemulfordigit = 2;
		}
		else if (currentIndex == 55)
		{
			somemulfordigit = 0;
		}

		currentvertdimdata = (double)input.vertdimmatrix6.w;
		currentfirstvertloc = (double)input.vertfirstloc6.w;
	}
	else if (currentIndex == 11 || currentIndex == 27)
	{
		if (currentIndex == 11)
		{
			somemulfordigit = 2;
		}
		else if (currentIndex == 27)
		{
			somemulfordigit = 0;
		}

		currentvertdimdata = (double)input.vertdimmatrix7.x;
		currentfirstvertloc = (double)input.vertfirstloc7.x;
	}
	else if (currentIndex == 43 || currentIndex == 59)
	{
		if (currentIndex == 43)
		{
			somemulfordigit = 2;
		}
		else if (currentIndex == 59)
		{
			somemulfordigit = 0;
		}

		currentvertdimdata = (double)input.vertdimmatrix7.y;
		currentfirstvertloc = (double)input.vertfirstloc7.y;
	}
	else if (currentIndex == 15 || currentIndex == 31)
	{
		if (currentIndex == 15)
		{
			somemulfordigit = 2;
		}
		else if (currentIndex == 31)
		{
			somemulfordigit = 0;
		}

		currentvertdimdata = (double)input.vertdimmatrix7.z;
		currentfirstvertloc = (double)input.vertfirstloc7.z;
	}
	else if (currentIndex == 47 || currentIndex == 63)
	{
		if (currentIndex == 47)
		{
			somemulfordigit = 2;
		}
		else if (currentIndex == 63)
		{
			somemulfordigit = 0;
		}

		currentvertdimdata = (double)input.vertdimmatrix7.w;
		currentfirstvertloc = (double)input.vertfirstloc7.w;
	}



	//0 16 32
	//48 4 20
	//36 52 8
	//24 40 56
	//12 28 44
	//60 1 17
	//33 49 5
	//21 37 53
	//9 25 41
	//57 13 29
	//45 61 2
	//18 34 50
	//6 22 38
	//54 10 26
	//42 58 14
	//30 46 62 
	//3 19 35
	//51 7 23
	//39 55 11
	//27 43 59 
	//15 31 47
	//63
	/*
	if (somecountermul == 0 || somecountermul == 16 || somecountermul == 32)
	{
		currentfirstvertloc = input.vertfirstloc0.x;
	}
	else if (somecountermul == 48 || somecountermul == 4 || somecountermul == 20)
	{
		currentfirstvertloc = input.vertfirstloc0.y;
	}
	else if (somecountermul == 36 || somecountermul == 52 || somecountermul == 8)
	{
		currentfirstvertloc = input.vertfirstloc0.z;
	}
	else if (somecountermul == 24 || somecountermul == 40 || somecountermul == 56)
	{
		currentfirstvertloc = input.vertfirstloc0.w;
	}
	else if (somecountermul == 12 || somecountermul == 28 || somecountermul == 44)
	{
		currentfirstvertloc = input.vertfirstloc1.x;
	}
	else if (somecountermul == 60 || somecountermul == 1 || somecountermul == 17)
	{
		currentfirstvertloc = input.vertfirstloc1.y;
	}
	else if (somecountermul == 33 || somecountermul == 49 || somecountermul == 5)
	{
		currentfirstvertloc = input.vertfirstloc1.z;
	}
	else if (somecountermul == 21 || somecountermul == 37 || somecountermul == 53)
	{
		currentfirstvertloc = input.vertfirstloc1.w;
	}
	else if (somecountermul == 9 || somecountermul == 25 || somecountermul == 41)
	{
		currentfirstvertloc = input.vertfirstloc2.x;
	}
	else if (somecountermul == 57 || somecountermul == 13 || somecountermul == 29)
	{
		currentfirstvertloc = input.vertfirstloc2.y;
	}
	else if (somecountermul == 45 || somecountermul == 61 || somecountermul == 2)
	{
		currentfirstvertloc = input.vertfirstloc2.z;
	}
	else if (somecountermul == 18 || somecountermul == 34 || somecountermul == 50)
	{
		currentfirstvertloc = input.vertfirstloc2.w;
	}
	else if (somecountermul == 6 || somecountermul == 22 || somecountermul == 38)
	{
		currentfirstvertloc = input.vertfirstloc3.x;
	}
	else if (somecountermul == 54 || somecountermul == 10 || somecountermul == 26)
	{
		currentfirstvertloc = input.vertfirstloc3.y;
	}
	else if (somecountermul == 42 || somecountermul == 58 || somecountermul == 14)
	{
		currentfirstvertloc = input.vertfirstloc3.z;
	}
	else if (somecountermul == 30 || somecountermul == 46 || somecountermul == 62)
	{
		currentfirstvertloc = input.vertfirstloc3.w;
	}
	else if (somecountermul == 3 || somecountermul == 19 || somecountermul == 35)
	{
		currentfirstvertloc = input.vertfirstloc4.x;
	}
	else if (somecountermul == 51 || somecountermul == 7 || somecountermul == 23)
	{
		currentfirstvertloc = input.vertfirstloc4.y;
	}
	else if (somecountermul == 39 || somecountermul == 55 || somecountermul == 11)
	{
		currentfirstvertloc = input.vertfirstloc4.z;
	}
	else if (somecountermul == 27 || somecountermul == 43 || somecountermul == 59)
	{
		currentfirstvertloc = input.vertfirstloc4.w;
	}
	else if (somecountermul == 15 || somecountermul == 31 || somecountermul == 47)
	{
		currentfirstvertloc = input.vertfirstloc5.x;
	}
	else if (somecountermul == 63)
	{
		currentfirstvertloc = input.vertfirstloc5.y;
	}*/
	
	
	
	
	
	/*
	if (somecountermul == 0)
	{
		currentvertdimdata = input.vertdimmatrix0.x;
	}
	else if (somecountermul == 1)
	{
		currentvertdimdata = input.vertdimmatrix0.y;
	}
	else if (somecountermul == 2)
	{
		currentvertdimdata = input.vertdimmatrix0.z;
	}
	else if (somecountermul == 3)
	{
		currentvertdimdata = input.vertdimmatrix0.w;
	}
	else if (somecountermul == 4)
	{
		currentvertdimdata = input.vertdimmatrix1.x;
	}
	else if (somecountermul == 5)
	{
		currentvertdimdata = input.vertdimmatrix1.y;

	}
	else if (somecountermul == 6)
	{
		currentvertdimdata = input.vertdimmatrix1.z;

	}
	else if (somecountermul == 7)
	{
		currentvertdimdata = input.vertdimmatrix1.w;

	}
	else if (somecountermul == 8)
	{
		currentvertdimdata = input.vertdimmatrix2.x;

	}
	else if (somecountermul == 9)
	{
		currentvertdimdata = input.vertdimmatrix2.y;
	}
	else if (somecountermul == 10)
	{
		currentvertdimdata = input.vertdimmatrix2.z;

	}
	else if (somecountermul == 11)
	{
		currentvertdimdata = input.vertdimmatrix2.w;
	}
	else if (somecountermul == 12)
	{
		currentvertdimdata = input.vertdimmatrix3.x;

	}
	else if (somecountermul == 13)
	{
		currentvertdimdata = input.vertdimmatrix3.y;

	}
	else if (somecountermul == 14)
	{
		currentvertdimdata = input.vertdimmatrix3.z;

	}
	else if (somecountermul == 15)
	{
		currentvertdimdata = input.vertdimmatrix3.w;
	}
	else if (somecountermul == 16)
	{
		currentvertdimdata = input.vertdimmatrix4.x;
	}
	else if (somecountermul == 17)
	{
		currentvertdimdata = input.vertdimmatrix4.y;
	}
	else if (somecountermul == 18)
	{
		currentvertdimdata = input.vertdimmatrix4.z;
	}
	else if (somecountermul == 19)
	{
		currentvertdimdata = input.vertdimmatrix4.w;
	}
	else if (somecountermul == 20)
	{
		currentvertdimdata = input.vertdimmatrix5.x;
	}
	else if (somecountermul == 21)
	{
		currentvertdimdata = input.vertdimmatrix5.y;
	}
	else if (somecountermul == 22)
	{
		currentvertdimdata = input.vertdimmatrix5.z;
	}
	else if (somecountermul == 23)
	{
		currentvertdimdata = input.vertdimmatrix5.w;
	}
	else if (somecountermul == 24)
	{
		currentvertdimdata = input.vertdimmatrix6.x;
	}
	else if (somecountermul == 25)
	{
		currentvertdimdata = input.vertdimmatrix6.y;
	}
	else if (somecountermul == 26)
	{
		currentvertdimdata = input.vertdimmatrix6.z;
	}
	else if (somecountermul == 27)
	{
		currentvertdimdata = input.vertdimmatrix6.w;
	}
	else if (somecountermul == 28)
	{
		currentvertdimdata = input.vertdimmatrix7.x;
	}
	else if (somecountermul == 29)
	{
		currentvertdimdata = input.vertdimmatrix7.y;
	}
	else if (somecountermul == 30)
	{
		currentvertdimdata = input.vertdimmatrix7.z;
	}
	else if (somecountermul == 31)
	{
		currentvertdimdata = input.vertdimmatrix7.w;
	}*/






	
/*
	if (somecountermul == 0)
	{
		currentfirstvertloc = input.vertfirstloc0.x;
	}
	else if (somecountermul == 1)
	{
		currentfirstvertloc = input.vertfirstloc0.y;
	}
	else if (somecountermul == 2)
	{
		currentfirstvertloc = input.vertfirstloc0.z;
	}
	else if (somecountermul == 3)
	{
		currentfirstvertloc = input.vertfirstloc0.w;
	}
	else if (somecountermul == 4)
	{
		currentfirstvertloc = input.vertfirstloc1.x;
	}
	else if (somecountermul == 5)
	{
		currentfirstvertloc = input.vertfirstloc1.y;

	}
	else if (somecountermul == 6)
	{
		currentfirstvertloc = input.vertfirstloc1.z;

	}
	else if (somecountermul == 7)
	{
		currentfirstvertloc = input.vertfirstloc1.w;

	}
	else if (somecountermul == 8)
	{
		currentfirstvertloc = input.vertfirstloc2.x;

	}
	else if (somecountermul == 9)
	{
		currentfirstvertloc = input.vertfirstloc2.y;
	}
	else if (somecountermul == 10)
	{
		currentfirstvertloc = input.vertfirstloc2.z;

	}
	else if (somecountermul == 11)
	{
		currentfirstvertloc = input.vertfirstloc2.w;
	}
	else if (somecountermul == 12)
	{
		currentfirstvertloc = input.vertfirstloc3.x;

	}
	else if (somecountermul == 13)
	{
		currentfirstvertloc = input.vertfirstloc3.y;

	}
	else if (somecountermul == 14)
	{
		currentfirstvertloc = input.vertfirstloc3.z;

	}
	else if (somecountermul == 15)
	{
		currentfirstvertloc = input.vertfirstloc3.w;
	}
	else if (somecountermul == 16)
	{
		currentfirstvertloc = input.vertfirstloc4.x;
	}
	else if (somecountermul == 17)
	{
		currentfirstvertloc = input.vertfirstloc4.y;
	}
	else if (somecountermul == 18)
	{
		currentfirstvertloc = input.vertfirstloc4.z;
	}
	else if (somecountermul == 19)
	{
		currentfirstvertloc = input.vertfirstloc4.w;
	}
	else if (somecountermul == 20)
	{
		currentfirstvertloc = input.vertfirstloc5.x;
	}
	else if (somecountermul == 21)
	{
		currentfirstvertloc = input.vertfirstloc5.y;
	}
	else if (somecountermul == 22)
	{
		currentfirstvertloc = input.vertfirstloc5.z;
	}
	else if (somecountermul == 23)
	{
		currentfirstvertloc = input.vertfirstloc5.w;
	}
	else if (somecountermul == 24)
	{
		currentfirstvertloc = input.vertfirstloc6.x;
	}
	else if (somecountermul == 25)
	{
		currentfirstvertloc = input.vertfirstloc6.y;
	}
	else if (somecountermul == 26)
	{
		currentfirstvertloc = input.vertfirstloc6.z;
	}
	else if (somecountermul == 27)
	{
		currentfirstvertloc = input.vertfirstloc6.w;
	}
	else if (somecountermul == 28)
	{
		currentfirstvertloc = input.vertfirstloc7.x;
	}
	else if (somecountermul == 29)
	{
		currentfirstvertloc = input.vertfirstloc7.y;
	}
	else if (somecountermul == 30)
	{
		currentfirstvertloc = input.vertfirstloc7.z;
	}
	else if (somecountermul == 31)
	{
		currentfirstvertloc = input.vertfirstloc7.w;
	}

	*/








	/*else if (somecountermul == 32)
	{
		currentvertdimdata = input.vertdimmatrix8.x;
	}
	else if (somecountermul == 33)
	{
		currentvertdimdata = input.vertdimmatrix8.y;
	}
	else if (somecountermul == 34)
	{
		currentvertdimdata = input.vertdimmatrix8.z;
	}
	else if (somecountermul == 35)
	{
		currentvertdimdata = input.vertdimmatrix8.w;
	}
	else if (somecountermul == 36)
	{
		currentvertdimdata = input.vertdimmatrix9.x;
	}
	else if (somecountermul == 37)
	{
		currentvertdimdata = input.vertdimmatrix9.y;
	}
	else if (somecountermul == 38)
	{
		currentvertdimdata = input.vertdimmatrix9.z;
	}
	else if (somecountermul == 39)
	{
		currentvertdimdata = input.vertdimmatrix9.w;
	}
	else if (somecountermul == 40)
	{
		currentvertdimdata = input.vertdimmatrix10.x;
	}
	else if (somecountermul == 41)
	{
		currentvertdimdata = input.vertdimmatrix10.y;
	}
	else if (somecountermul == 42)
	{
		currentvertdimdata = input.vertdimmatrix10.z;
	}
	else if (somecountermul == 43)
	{
		currentvertdimdata = input.vertdimmatrix10.w;
	}
	else if (somecountermul == 44)
	{
		currentvertdimdata = input.vertdimmatrix11.x;
	}
	else if (somecountermul == 45)
	{
		currentvertdimdata = input.vertdimmatrix11.y;
	}
	else if (somecountermul == 46)
	{
		currentvertdimdata = input.vertdimmatrix11.z;
	}
	else if (somecountermul == 47)
	{
		currentvertdimdata = input.vertdimmatrix11.w;
	}
	else if (somecountermul == 48)
	{
		currentvertdimdata = input.vertdimmatrix12.x;
	}
	else if (somecountermul == 49)
	{
		currentvertdimdata = input.vertdimmatrix12.y;
	}
	else if (somecountermul == 50)
	{
		currentvertdimdata = input.vertdimmatrix12.z;
	}
	else if (somecountermul == 51)
	{
		currentvertdimdata = input.vertdimmatrix12.w;
	}
	else if (somecountermul == 52)
	{
		currentvertdimdata = input.vertdimmatrix13.x;
	}
	else if (somecountermul == 53)
	{
		currentvertdimdata = input.vertdimmatrix13.y;
	}
	else if (somecountermul == 54)
	{
		currentvertdimdata = input.vertdimmatrix13.z;
	}
	else if (somecountermul == 55)
	{
		currentvertdimdata = input.vertdimmatrix13.w;
	}
	else if (somecountermul == 56)
	{
		currentvertdimdata = input.vertdimmatrix14.x;
	}
	else if (somecountermul == 57)
	{
		currentvertdimdata = input.vertdimmatrix14.y;
	}
	else if (somecountermul == 58)
	{
		currentvertdimdata = input.vertdimmatrix14.z;
	}
	else if (somecountermul == 59)
	{
		currentvertdimdata = input.vertdimmatrix14.w;
	}
	else if (somecountermul == 60)
	{
		currentvertdimdata = input.vertdimmatrix15.x;
	}
	else if (somecountermul == 61)
	{
		currentvertdimdata = input.vertdimmatrix15.y;
	}
	else if (somecountermul == 62)
	{
		currentvertdimdata = input.vertdimmatrix15.z;
	}
	else if (somecountermul == 63) // 
	{
		currentvertdimdata = input.vertdimmatrix15.w;
	}*/
	

	//retrieve the byte out of the intmap. check if it is a zero or 1.
	//retrieve the width and depth of the vertmap.


	





	/*
	//int someindex = 7;
	float someData0 = currentMapData;
	for (int i = 0; i < someOtherIndex; i++)
	{
		someData0 = (someData0 * 0.1f);
	}
	float someres = someData0 - (float)floor(someData0);
	arrayOfDigits[someOtherIndex] = (float)floor(someres * 10.0f);
	*/




	/*
	float someothermul0 = 10.0;
	float someothermul1 = 1.0;	
	if (someOtherIndex == 0)
	{
		someothermul0 = 100.0;
		someothermul1 = 0.1;
	}
	else if (someOtherIndex == 1)
	{
		someothermul0 = 1000.0;
		someothermul1 = 0.01;
	}
	else if (someOtherIndex == 2)
	{
		someothermul0 = 10000.0;
		someothermul1 = 0.001;
	}
	else if (someOtherIndex == 3)
	{
		someothermul0 = 100000.0;
		someothermul1 = 0.0001;
	}

	float tempsomemap;
	float somemap = currentMapData;

	for (int i = 0; i < maxfloatbytemaparraylength; i++)
	{
		tempsomemap = somemap;
		somemap = (somemap * 0.1);
		float somevalue = floor((float)(int)((somemap - (int)somemap) * someothermul0)) * someothermul1;
		somevalue = somevalue - (floor(somevalue * 0.1) * 10);
		arrayOfDigits[i] = somevalue;
	}*/




/*
	if (someOtherIndex == 0) // problem gotta use a different approach for the index 0 and 2
	{
		
	}
	else if (someOtherIndex == 1) // problem gotta use a different approach for the index 0 and 2
	{
	
	}
	else if (someOtherIndex == 2) // problem gotta use a different approach for the index 0 and 2
	{
		
	}*/


	//currentvertdimdata

	//int someindex = 7;
	/*float somevertdata = currentvertdimdata;

	for (int i = 0; i < (someOtherIndex); i++)
	{
		somevertdata = (somevertdata * 0.01f);
	}

	float someresult = somevertdata - (float)floor(somevertdata);
	int somedepth = (float)floor(someresult * 10.0f);



	somevertdata = currentvertdimdata;

	for (int i = 0; i < (someOtherIndex) - 2; i++)
	{
		somevertdata = (somevertdata * 0.01f);
	}

	someresult = somevertdata - (float)floor(somevertdata);
	int somewidth = (float)floor(someresult * 10.0f);
	

	somevertdata = currentvertdimdata;

	for (int i = 0; i < (someOtherIndex) - 1; i++)
	{
		somevertdata = (somevertdata * 0.01f);
	}

	someresult = somevertdata - (float)floor(somevertdata);
	int someheight = (float)floor(someresult * 10.0f);

	*/




	/*
	int somemulfordigit = 0;
	
	if (anotherindex == 0)
	{
		somemulfordigit = 6;
	}
	else if (anotherindex == 1)
	{
		somemulfordigit = 3;
	}
	else if (anotherindex == 2)
	{
		somemulfordigit = 0;
	}
	*/

	/*float somevertdata = currentvertdimdata;
	for (int i = 0; i < (somemulfordigit+2); i++)
	{
		somevertdata = (somevertdata * 0.1f);
	}

	float someresult = somevertdata - (float)floor(somevertdata);
	float somedepth = (float)floor(someresult * 10.0f) ; // to readd*/


	int firstvertlocx = 0;
	int firstvertlocy = 0;
	int firstvertlocz = 0;

	int somewidth = 0;
	int someheight = 0;
	int somedepth = 0;



	/*

	if (somemulfordigit == 0)
	{


		int somevertdata = trunc(currentvertdimdata);
		for (int i = 0; i < (somemulfordigit + 0); i++)
		{
			somevertdata = somevertdata / 10;
		}
		int someres = somevertdata / 10;
		 somedepth = (somevertdata - (someres * 10));

		somevertdata = trunc(currentvertdimdata);
		for (int i = 0; i < (somemulfordigit + 1); i++)
		{
			somevertdata = somevertdata / 10;
		}
		someres = somevertdata / 10;
		 someheight = (somevertdata - (someres * 10));

		somevertdata = trunc(currentvertdimdata);
		for (int i = 0; i < (somemulfordigit + 2); i++)
		{
			somevertdata = somevertdata / 10;
		}
		someres = somevertdata / 10;
		 somewidth = (somevertdata - (someres * 10));




		somevertdata = trunc(currentfirstvertloc);
		for (int i = 0; i < (somemulfordigit + 0); i++)
		{
			somevertdata = somevertdata / 10;
		}
		someres = somevertdata / 10;
		 firstvertlocz = somevertdata - (someres * 10);


		somevertdata = trunc(currentfirstvertloc);
		for (int i = 0; i < (somemulfordigit + 1); i++)
		{
			somevertdata = somevertdata / 10;
		}
		someres = somevertdata / 10;
		 firstvertlocy = somevertdata - (someres * 10);


		somevertdata = trunc(currentfirstvertloc);
		for (int i = 0; i < (somemulfordigit + 2); i++)
		{
			somevertdata = somevertdata / 10;
		}
		someres = somevertdata / 10;
		firstvertlocx = somevertdata - (someres * 10);
	}
	else if (somemulfordigit == 3)
	{
		
		somemulfordigit = 0;

		double somefloattodouble = (double)((currentvertdimdata - trunc(currentvertdimdata)));
		
		int powBy = 3;
		somefloattodouble *= (double)pow(10, powBy);
		somefloattodouble = (round((double)somefloattodouble * 10) / 10);

		
		double somevertdata = somefloattodouble;

		for (int i = 0; i < (somemulfordigit + 0); i++)
		{
			somevertdata = somevertdata / 10;
		}
		int someres = (int)(somevertdata / 10);
		somedepth = (int)(somevertdata - (someres * 10));



		somefloattodouble = (double)((currentvertdimdata - trunc(currentvertdimdata)));
		 powBy = 3;
		somefloattodouble *= (double)pow(10, powBy);
		somefloattodouble = (round((double)somefloattodouble * 10) / 10);


		somevertdata = somefloattodouble;
		for (int i = 0; i < (somemulfordigit + 1); i++)
		{
			somevertdata = somevertdata / 10;
		}
		someres = (int)(somevertdata / 10);
		someheight = (int)(somevertdata - (someres * 10));



		somefloattodouble = (double)((currentvertdimdata - trunc(currentvertdimdata)));
		powBy = 3;
		somefloattodouble *= (double)pow(10, powBy);
		somefloattodouble = (round((double)somefloattodouble * 10) / 10);

		somevertdata = somefloattodouble;
		for (int i = 0; i < (somemulfordigit + 2); i++)
		{
			somevertdata = somevertdata / 10;
		}
		someres = (int)(somevertdata / 10);
		somewidth = (int)(somevertdata - (someres * 10));

















		somemulfordigit = 0;

		somefloattodouble = (double)((currentfirstvertloc - trunc(currentfirstvertloc)));
		 powBy = 3;
		somefloattodouble *= (double)pow(10, powBy);
		somefloattodouble = (round((double)somefloattodouble * 10) / 10);
		somevertdata = somefloattodouble;
		for (int i = 0; i < (somemulfordigit + 0); i++)
		{
			somevertdata = somevertdata / 10;
		}
		someres = (int)(somevertdata / 10);
		firstvertlocz = (int)(somevertdata - (someres * 10));




		somefloattodouble = (double)((currentfirstvertloc - trunc(currentfirstvertloc)));
		 powBy = 3;
		somefloattodouble *= (double)pow(10, powBy);
		somefloattodouble = (round((double)somefloattodouble * 10) / 10);
		somevertdata = somefloattodouble;
		for (int i = 0; i < (somemulfordigit + 1); i++)
		{
			somevertdata = somevertdata / 10;
		}
		someres = (int)(somevertdata / 10);
		firstvertlocy = (int)(somevertdata - (someres * 10));




		somefloattodouble = (double)((currentfirstvertloc - trunc(currentfirstvertloc)));
		 powBy = 3;
		somefloattodouble *= (double)pow(10, powBy);
		somefloattodouble = (round((double)somefloattodouble * 10) / 10);

		somevertdata = somefloattodouble;
		for (int i = 0; i < (somemulfordigit + 2); i++)
		{
			somevertdata = somevertdata / 10;
		}
		someres = (int)(somevertdata / 10);
		firstvertlocx = (int)(somevertdata - (someres * 10));




		//FETCH FIRST VERTEX OF FACE
		//FETCH FIRST VERTEX OF FACE
		//FETCH FIRST VERTEX OF FACE
		/*
		//somevalueinshader = floor((double)currentfirstvertloc * 1000000) / 1000000;
		somefloattodouble = (double)((currentfirstvertloc - trunc(currentfirstvertloc)));
		somevertdata = (round((double)somefloattodouble * 1000000) / 1000000) ;

		powBy = 6;
		somevertdata *= (double)pow(10, powBy);
		//somevertdata = trunc(currentfirstvertloc);
		for (int i = 0; i < (somemulfordigit + 0); i++)
		{
			somevertdata = somevertdata / 100;
		}
		someres = somevertdata / 100;
		firstvertlocz = somevertdata - (someres * 100);

		//somevalueinshader = floor((double)currentfirstvertloc * 1000000) / 1000000;
		somefloattodouble = (double)((currentfirstvertloc - trunc(currentfirstvertloc)));
		somevertdata = (round((double)somefloattodouble * 1000000) / 1000000) ;
		powBy = 6;
		somevertdata *= (double)pow(10, powBy);

		somevertdata = trunc(currentfirstvertloc);
		for (int i = 0; i < (somemulfordigit + 1); i++)
		{
			somevertdata = somevertdata / 100;
		}
		someres = somevertdata / 100;
		firstvertlocy = somevertdata - (someres * 100);

		//somevalueinshader = floor((double)currentfirstvertloc * 1000000) / 1000000;
		somefloattodouble = (double)((currentfirstvertloc - trunc(currentfirstvertloc)));
		somevertdata = (round((double)somefloattodouble * 1000000) / 1000000) ;

		powBy = 6;
		somevertdata *= (double)pow(10, powBy);

		somevertdata = trunc(currentfirstvertloc);
		for (int i = 0; i < (somemulfordigit + 2); i++)
		{
			somevertdata = somevertdata / 100;
		}
		someres = somevertdata / 100;
		firstvertlocx = somevertdata - (someres * 100);
		//FETCH FIRST VERTEX OF FACE
		//FETCH FIRST VERTEX OF FACE
		//FETCH FIRST VERTEX OF FACE
	}*/




	

	int somevertdata = 0;
	int someres = 0;


	/*
	somevertdata = trunc(currentvertdimdata);
	for (int i = 0; i < (somemulfordigit + 0); i++)
	{
		somevertdata = somevertdata / 10;
	}
	 someres = somevertdata / 10;
	somedepth = (somevertdata - (someres * 10));

	somevertdata = trunc(currentvertdimdata);
	for (int i = 0; i < (somemulfordigit + 1); i++)
	{
		somevertdata = somevertdata / 10;
	}
	someres = somevertdata / 10;
	someheight = (somevertdata - (someres * 10));

	somevertdata = trunc(currentvertdimdata);
	for (int i = 0; i < (somemulfordigit + 2); i++)
	{
		somevertdata = somevertdata / 10;
	}
	someres = somevertdata / 10;
	somewidth = (somevertdata - (someres * 10));


	somevertdata = trunc(currentfirstvertloc);
	for (int i = 0; i < (somemulfordigit + 0); i++)
	{
		somevertdata = somevertdata / 10;
	}
	someres = somevertdata / 10;
	firstvertlocz = somevertdata - (someres * 10);


	somevertdata = trunc(currentfirstvertloc);
	for (int i = 0; i < (somemulfordigit + 1); i++)
	{
		somevertdata = somevertdata / 10;
	}
	someres = somevertdata / 10;
	firstvertlocy = somevertdata - (someres * 10);


	somevertdata = trunc(currentfirstvertloc);
	for (int i = 0; i < (somemulfordigit + 2); i++)
	{
		somevertdata = somevertdata / 10;
	}
	someres = somevertdata / 10;
	firstvertlocx = somevertdata - (someres * 10);*/




	if (vertextype == 1 || vertextype == 2 || vertextype == 3 || vertextype == 4) 
	{
		somevertdata = trunc(currentvertdimdata);
		for (int i = 0; i < (somemulfordigit + 0); i++)
		{
			somevertdata = somevertdata / themul;
		}
		someres = somevertdata / themul;
		somedepth = (somevertdata - (someres * themul));

		/*somevertdata = trunc(currentvertdimdata);
		for (int i = 0; i < (somemulfordigit + 1); i++)
		{
			somevertdata = somevertdata / themul;
		}
		someres = somevertdata / themul;
		someheight = (somevertdata - (someres * themul));*/

		somevertdata = trunc(currentvertdimdata);
		for (int i = 0; i < (somemulfordigit + 1); i++)
		{
			somevertdata = somevertdata / themul;
		}
		someres = somevertdata / themul;
		somewidth = (somevertdata - (someres * themul));






		somevertdata = trunc(currentfirstvertloc);
		for (int i = 0; i < (somemulfordigit + 0); i++)
		{
			somevertdata = somevertdata / themul;
		}
		someres = somevertdata / themul;
		firstvertlocz = somevertdata - (someres * themul);

		/*
		somevertdata = trunc(currentfirstvertloc);
		for (int i = 0; i < (somemulfordigit + 1); i++)
		{
			somevertdata = somevertdata / themul;
		}
		someres = somevertdata / themul;
		firstvertlocy = somevertdata - (someres * themul);*/


		somevertdata = trunc(currentfirstvertloc);
		for (int i = 0; i < (somemulfordigit + 1); i++)
		{
			somevertdata = somevertdata / themul;
		}
		someres = somevertdata / themul;
		firstvertlocx = somevertdata - (someres * themul);
	}

	else if (vertextype == 5 || vertextype == 6 || vertextype == 7 || vertextype == 8)
	{
		somevertdata = trunc(currentvertdimdata);
		for (int i = 0; i < (somemulfordigit + 0); i++)
		{
			somevertdata = somevertdata / themul;
		}
		someres = somevertdata / themul;
		somedepth = (somevertdata - (someres * themul));

		somevertdata = trunc(currentvertdimdata);
		for (int i = 0; i < (somemulfordigit + 1); i++)
		{
			somevertdata = somevertdata / themul;
		}
		someres = somevertdata / themul;
		someheight = (somevertdata - (someres * themul));

		/*somevertdata = trunc(currentvertdimdata);
		for (int i = 0; i < (somemulfordigit + 2); i++)
		{
			somevertdata = somevertdata / themul;
		}
		someres = somevertdata / themul;
		somewidth = (somevertdata - (someres * themul));*/


		somevertdata = trunc(currentfirstvertloc);
		for (int i = 0; i < (somemulfordigit + 0); i++)
		{
			somevertdata = somevertdata / themul;
		}
		someres = somevertdata / themul;
		firstvertlocz = somevertdata - (someres * themul);


		somevertdata = trunc(currentfirstvertloc);
		for (int i = 0; i < (somemulfordigit + 1); i++)
		{
			somevertdata = somevertdata / themul;
		}
		someres = somevertdata / themul;
		firstvertlocy = somevertdata - (someres * themul);


		/*somevertdata = trunc(currentfirstvertloc);
		for (int i = 0; i < (somemulfordigit + 2); i++)
		{
			somevertdata = somevertdata / themul;
		}
		someres = somevertdata / themul;
		firstvertlocx = somevertdata - (someres * themul);*/
	}
	else if (vertextype == 9 || vertextype == 10 || vertextype == 11 || vertextype == 12)
	{
		somevertdata = trunc(currentvertdimdata);
		for (int i = 0; i < (somemulfordigit + 0); i++)
		{
			somevertdata = somevertdata / themul;
		}
		someres = somevertdata / themul;
		somedepth = (somevertdata - (someres * themul));

		somevertdata = trunc(currentvertdimdata);
		for (int i = 0; i < (somemulfordigit + 1); i++)
		{
			somevertdata = somevertdata / themul;
		}
		someres = somevertdata / themul;
		someheight = (somevertdata - (someres * themul));

		/*somevertdata = trunc(currentvertdimdata);
		for (int i = 0; i < (somemulfordigit + 2); i++)
		{
			somevertdata = somevertdata / themul;
		}
		someres = somevertdata / themul;
		somewidth = (somevertdata - (someres * themul));*/


		somevertdata = trunc(currentfirstvertloc);
		for (int i = 0; i < (somemulfordigit + 0); i++)
		{
			somevertdata = somevertdata / themul;
		}
		someres = somevertdata / themul;
		firstvertlocz = somevertdata - (someres * themul);


		somevertdata = trunc(currentfirstvertloc);
		for (int i = 0; i < (somemulfordigit + 1); i++)
		{
			somevertdata = somevertdata / themul;
		}
		someres = somevertdata / themul;
		firstvertlocy = somevertdata - (someres * themul);


		/*somevertdata = trunc(currentfirstvertloc);
		for (int i = 0; i < (somemulfordigit + 2); i++)
		{
			somevertdata = somevertdata / themul;
		}
		someres = somevertdata / themul;
		firstvertlocx = somevertdata - (someres * themul);*/
	}
	else if (vertextype == 13 || vertextype == 14 || vertextype == 15 || vertextype == 16)
	{
		/*somevertdata = trunc(currentvertdimdata);
		for (int i = 0; i < (somemulfordigit + 1); i++)
		{
			somevertdata = somevertdata / themul;
		}
		someres = somevertdata / themul;
		somedepth = (somevertdata - (someres * themul));*/

		somevertdata = trunc(currentvertdimdata);
		for (int i = 0; i < (somemulfordigit + 1); i++)
		{
			somevertdata = somevertdata / themul;
		}
		someres = somevertdata / themul;
		someheight = (somevertdata - (someres * themul));

		somevertdata = trunc(currentvertdimdata);
		for (int i = 0; i < (somemulfordigit + 0); i++)
		{
			somevertdata = somevertdata / themul;
		}
		someres = somevertdata / themul;
		somewidth = (somevertdata - (someres * themul));




		/*
		somevertdata = trunc(currentfirstvertloc);
		for (int i = 0; i < (somemulfordigit + 1); i++)
		{
			somevertdata = somevertdata / themul;
		}
		someres = somevertdata / themul;
		firstvertlocz = somevertdata - (someres * themul);*/


		somevertdata = trunc(currentfirstvertloc);
		for (int i = 0; i < (somemulfordigit + 1); i++)
		{
			somevertdata = somevertdata / themul;
		}
		someres = somevertdata / themul;
		firstvertlocy = somevertdata - (someres * themul);


		somevertdata = trunc(currentfirstvertloc);
		for (int i = 0; i < (somemulfordigit + 0); i++)
		{
			somevertdata = somevertdata / themul;
		}
		someres = somevertdata / themul;
		firstvertlocx = somevertdata - (someres * themul);
	}
	else if (vertextype == 17 || vertextype == 18 || vertextype == 19 || vertextype == 20)
	{
		/*somevertdata = trunc(currentvertdimdata);
		for (int i = 0; i < (somemulfordigit + 1); i++)
		{
			somevertdata = somevertdata / themul;
		}
		someres = somevertdata / themul;
		somedepth = (somevertdata - (someres * themul));*/

		somevertdata = trunc(currentvertdimdata);
		for (int i = 0; i < (somemulfordigit + 1); i++)
		{
			somevertdata = somevertdata / themul;
		}
		someres = somevertdata / themul;
		someheight = (somevertdata - (someres * themul));

		somevertdata = trunc(currentvertdimdata);
		for (int i = 0; i < (somemulfordigit + 0); i++)
		{
			somevertdata = somevertdata / themul;
		}
		someres = somevertdata / themul;
		somewidth = (somevertdata - (someres * themul));




		/*
		somevertdata = trunc(currentfirstvertloc);
		for (int i = 0; i < (somemulfordigit + 1); i++)
		{
			somevertdata = somevertdata / themul;
		}
		someres = somevertdata / themul;
		firstvertlocz = somevertdata - (someres * themul);*/


		somevertdata = trunc(currentfirstvertloc);
		for (int i = 0; i < (somemulfordigit + 1); i++)
		{
			somevertdata = somevertdata / themul;
		}
		someres = somevertdata / themul;
		firstvertlocy = somevertdata - (someres * themul);


		somevertdata = trunc(currentfirstvertloc);
		for (int i = 0; i < (somemulfordigit + 0); i++)
		{
			somevertdata = somevertdata / themul;
		}
		someres = somevertdata / themul;
		firstvertlocx = somevertdata - (someres * themul);
	}
	else if (vertextype == 21 || vertextype == 22 || vertextype == 23 || vertextype == 24)
	{
		somevertdata = trunc(currentvertdimdata);
		for (int i = 0; i < (somemulfordigit + 0); i++)
		{
			somevertdata = somevertdata / themul;
		}
		someres = somevertdata / themul;
		somedepth = (somevertdata - (someres * themul));

		/*somevertdata = trunc(currentvertdimdata);
		for (int i = 0; i < (somemulfordigit + 1); i++)
		{
			somevertdata = somevertdata / themul;
		}
		someres = somevertdata / themul;
		someheight = (somevertdata - (someres * themul));*/

		somevertdata = trunc(currentvertdimdata);
		for (int i = 0; i < (somemulfordigit + 1); i++)
		{
			somevertdata = somevertdata / themul;
		}
		someres = somevertdata / themul;
		somewidth = (somevertdata - (someres * themul));






		somevertdata = trunc(currentfirstvertloc);
		for (int i = 0; i < (somemulfordigit + 0); i++)
		{
			somevertdata = somevertdata / themul;
		}
		someres = somevertdata / themul;
		firstvertlocz = somevertdata - (someres * themul);

		/*
		somevertdata = trunc(currentfirstvertloc);
		for (int i = 0; i < (somemulfordigit + 1); i++)
		{
			somevertdata = somevertdata / themul;
		}
		someres = somevertdata / themul;
		firstvertlocy = somevertdata - (someres * themul);*/


		somevertdata = trunc(currentfirstvertloc);
		for (int i = 0; i < (somemulfordigit + 1); i++)
		{
			somevertdata = somevertdata / themul;
		}
		someres = somevertdata / themul;
		firstvertlocx = somevertdata - (someres * themul);
	}





















	//for 4 by 4 by 4 and placing 8 digits in a float.
	//0-4-1-5-2-6-3-7
	//8-12-9-13-10-14-11-15
	//16-20-17-21-18-22-19-23
	//24-28-25-29-26-30-27-31
	//32-36-33-37-34-38-35-39
	//40-44-41-45-42-46-43-47
	//48-52-49-53-50-54-51-55
	//56-60-57-61-58-62-59-63

	//0 16 32 48
	//4 20 36 52
	//8 24 40 56
	//12 28 44 60
	//1 17 33 49
	//5 21 37 53
	//9 25 41 57
	//13 29 45 61
	//2 18 34 50
	//6 22 38 54 
	//10 26 42 58 
	//14 30 46 62
	//3 19 35 51
	//7 23 39 55
	//11 27 43 59
	//15 31 47 63
	

	//0 16 32
	//48 4 20
	//36 52 8
	//24 40 56
	//12 28 44
	//60 1 17
	//33 49 5
	//21 37 53
	//9 25 41
	//57 13 29
	//45 61 2
	//18 34 50
	//6 22 38
	//54 10 26
	//42 58 14
	//30 46 62 
	//3 19 35
	//51 7 23
	//39 55 11
	//27 43 59 
	//15 31 47
	//63










	/*
	if (somewidth!= 0 && somedepth!= 0) 
	{
		float4 mod_input_vertex_pos = input.position;

		mod_input_vertex_pos.x;// += (x * planesize);
		mod_input_vertex_pos.y;// += (y * planesize);
		mod_input_vertex_pos.z;// += (z * planesize);

		mod_input_vertex_pos.x += input.instancePosition.x;
		mod_input_vertex_pos.y += input.instancePosition.y;
		mod_input_vertex_pos.z += input.instancePosition.z;
		mod_input_vertex_pos.w = 1.0f;

		//output.position = mul(mod_input_vertex_pos, worldviewproj);

		output.color = input.color;
		output.normal = input.normal;
	}*/



	/*
	float4 mod_input_vertex_pos = input.position;

	mod_input_vertex_pos.x;// += (x * planesize);
	mod_input_vertex_pos.y += (someheight * planesize);// += (y * planesize);
	mod_input_vertex_pos.z;// += (z * planesize);

	mod_input_vertex_pos.x += input.instancePosition.x;
	mod_input_vertex_pos.y += input.instancePosition.y;
	mod_input_vertex_pos.z += input.instancePosition.z;
	mod_input_vertex_pos.w = 1.0f;

	//output.position = mul(mod_input_vertex_pos, worldviewproj);

	output.color = input.color;
	output.normal = input.normal;*/

	float4 somefirstvertlocation = input.position; // float4(firstvertlocx, firstvertlocy, firstvertlocz, 1.0f);

	somefirstvertlocation.w = 1.0f;

	//somefirstvertlocation.x *= planesize;
	//somefirstvertlocation.y *= planesize;
	//somefirstvertlocation.z *= planesize;
	
	/*
	if (somemeshwidth == 1 && somemeshheight == 1 && somemeshdepth == 1)
	{
		//4 vertex => 2 triangles (1 face mesh)
		if (x == 0 && y == 0 && z == 0)
		{

			if (vertextype == 1)
			{
				float4 mod_input_vertex_pos = input.position;

				mod_input_vertex_pos.x;// += (x * planesize);
				mod_input_vertex_pos.y += (someheight * planesize);// += (y * planesize);
				mod_input_vertex_pos.z;// += (z * planesize);

				mod_input_vertex_pos.x += input.instancePosition.x;
				mod_input_vertex_pos.y += input.instancePosition.y;
				mod_input_vertex_pos.z += input.instancePosition.z;
				mod_input_vertex_pos.w = 1.0f;

				//output.position = mul(mod_input_vertex_pos, worldviewproj);

				output.color = input.color;
				output.normal = input.normal;
			}
			else if (vertextype == 2)
			{
				float4 mod_input_vertex_pos = input.position;

				mod_input_vertex_pos.x;// += (x * planesize);
				mod_input_vertex_pos.y += (someheight * planesize);//// += (y * planesize);
				mod_input_vertex_pos.z += (somedepth * planesize);// += (z * planesize);

				mod_input_vertex_pos.x += input.instancePosition.x;
				mod_input_vertex_pos.y += input.instancePosition.y;
				mod_input_vertex_pos.z += input.instancePosition.z;
				mod_input_vertex_pos.w = 1.0f;

				//output.position = mul(mod_input_vertex_pos, worldviewproj);

				output.color = input.color;
				output.normal = input.normal;
			}
			else if (vertextype == 3)
			{
				float4 mod_input_vertex_pos = input.position;

				mod_input_vertex_pos.x += (somewidth * planesize);// += (x * planesize);
				mod_input_vertex_pos.y += (someheight * planesize);//// += (y * planesize);
				mod_input_vertex_pos.z;// += (z * planesize);

				mod_input_vertex_pos.x += input.instancePosition.x;
				mod_input_vertex_pos.y += input.instancePosition.y;
				mod_input_vertex_pos.z += input.instancePosition.z;
				mod_input_vertex_pos.w = 1.0f;

				//output.position = mul(mod_input_vertex_pos, worldviewproj);

				output.color = input.color;
				output.normal = input.normal;
			}
			else if (vertextype == 4)
			{
				float4 mod_input_vertex_pos = input.position;

				mod_input_vertex_pos.x += (somewidth * planesize);// += (x * planesize);
				mod_input_vertex_pos.y += (someheight * planesize);//// += (y * planesize);
				mod_input_vertex_pos.z += (somedepth * planesize);// += (z * planesize);

				mod_input_vertex_pos.x += input.instancePosition.x;
				mod_input_vertex_pos.y += input.instancePosition.y;
				mod_input_vertex_pos.z += input.instancePosition.z;
				mod_input_vertex_pos.w = 1.0f;

				//output.position = mul(mod_input_vertex_pos, worldviewproj);

				output.color = input.color;
				output.normal = input.normal;
			}
		}
	}*/



	/*
	||
		round(arrayOfDigits[someOtherIndex]) == 2.0 ||
		round(arrayOfDigits[someOtherIndex]) == 3.0 ||
		round(arrayOfDigits[someOtherIndex]) == 4.0
	*/


	//if (somemeshwidth == 1 && somemeshheight == 1 && somemeshdepth == 1)
	{


		/*
		if (vertextype == 1)
		{
			float4 mod_input_vertex_pos = somefirstvertlocation;//  input.position;

			mod_input_vertex_pos.x += firstvertlocx;// += (x * planesize);
			mod_input_vertex_pos.y += firstvertlocy;// += (y * planesize);
			mod_input_vertex_pos.z += firstvertlocz;// += (z * planesize);

			mod_input_vertex_pos.x *= planesize;// += (x * planesize);
			mod_input_vertex_pos.y *= planesize;// += (y * planesize);
			mod_input_vertex_pos.z *= planesize;// += (z * planesize);


			mod_input_vertex_pos.x += input.instancePosition.x;
			mod_input_vertex_pos.y += input.instancePosition.y;
			mod_input_vertex_pos.z += input.instancePosition.z;
			mod_input_vertex_pos.w = 1.0f;

			//output.position = mul(mod_input_vertex_pos, worldviewproj);

			output.color = input.color;
			output.normal = input.normal;
		}
		else if (vertextype == 2)
		{
			float4 mod_input_vertex_pos = somefirstvertlocation;// input.position;

			mod_input_vertex_pos.x += firstvertlocx;// += (x * planesize);
			mod_input_vertex_pos.y += firstvertlocy;//// += (y * planesize);
			mod_input_vertex_pos.z += firstvertlocz + (somedepth );// += (z * planesize);

			mod_input_vertex_pos.x *= planesize;// += (x * planesize);
			mod_input_vertex_pos.y *= planesize;// += (y * planesize);
			mod_input_vertex_pos.z *= planesize;// += (z * planesize);

			mod_input_vertex_pos.x += input.instancePosition.x;
			mod_input_vertex_pos.y += input.instancePosition.y;
			mod_input_vertex_pos.z += input.instancePosition.z;
			mod_input_vertex_pos.w = 1.0f;

			//output.position = mul(mod_input_vertex_pos, worldviewproj);

			output.color = input.color;
			output.normal = input.normal;
		}
		else if (vertextype == 3)
		{
			float4 mod_input_vertex_pos = somefirstvertlocation;// input.position;

			mod_input_vertex_pos.x += firstvertlocx + (somewidth );// += (x * planesize);
			mod_input_vertex_pos.y += firstvertlocy;//// += (y * planesize);
			mod_input_vertex_pos.z += firstvertlocz;// += (z * planesize);


			mod_input_vertex_pos.x *= planesize;// += (x * planesize);
			mod_input_vertex_pos.y *= planesize;// += (y * planesize);
			mod_input_vertex_pos.z *= planesize;// += (z * planesize);


			mod_input_vertex_pos.x += input.instancePosition.x;
			mod_input_vertex_pos.y += input.instancePosition.y;
			mod_input_vertex_pos.z += input.instancePosition.z;
			mod_input_vertex_pos.w = 1.0f;

			//output.position = mul(mod_input_vertex_pos, worldviewproj);

			output.color = input.color;
			output.normal = input.normal;
		}
		else if (vertextype == 4)
		{
			float4 mod_input_vertex_pos = somefirstvertlocation;//  input.position;

			mod_input_vertex_pos.x += firstvertlocx + (somewidth );// += (x * planesize);
			mod_input_vertex_pos.y += firstvertlocy;//// += (y * planesize);
			mod_input_vertex_pos.z += firstvertlocz + (somedepth );// += (z * planesize);


			mod_input_vertex_pos.x *= planesize;// += (x * planesize);
			mod_input_vertex_pos.y *= planesize;// += (y * planesize);
			mod_input_vertex_pos.z *= planesize;// += (z * planesize);



			mod_input_vertex_pos.x += input.instancePosition.x;
			mod_input_vertex_pos.y += input.instancePosition.y;
			mod_input_vertex_pos.z += input.instancePosition.z;
			mod_input_vertex_pos.w = 1.0f;

			//output.position = mul(mod_input_vertex_pos, worldviewproj);

			output.color = input.color;
			output.normal = input.normal;
		}*/
	}
	
	/*
	if (round(somenewmapresult) == 1.0) 
	{
		
	}*/
	
	
	






	double somedesiredbyte = 1.0;


	/*
	||
		x == 0 && y == 0 && z == 1 ||
		x == 0 && y == 0 && z == 2 ||
		x == 0 && y == 0 && z == 3*/

	//if (vertextype == 0)
	//if(x == 0 && y == 0 && z == 0)
	//   x == 0 && y == 0 && z == 1 ||
	//	x == 0 && y == 0 && z == 2 ||
	//	x == 0 && y == 0 && z == 3) //somemeshwidth == 1 && somemeshheight == 1 && somemeshdepth == 1)//if (input.instancePosition.x == 0 ) //somemeshwidth == 1 && somemeshheight == 1 && somemeshdepth == 1)
	{
		//round(somenewmapresult) == 1.0) //
		//if (round(arrayOfDigits[someindexpos]) == 1.0) //1.0 // 0.0 //someOtherIndex >= 0 && someOtherIndex <= 7 ) // //theByte == somedesiredbyte) //  //round(theByte) == round(somedesiredbyte)) // 
		{


			/*if (firstvertlocy == 0)
			{

				
			}*/
			
			/*
			float4 mod_input_vertex_pos = somefirstvertlocation;//  input.position;

			//mod_input_vertex_pos.x = (firstvertlocx * planesize);// += (x * planesize);
			//mod_input_vertex_pos.y += 0;// (firstvertlocy * planesize);// += (y * planesize);
			//mod_input_vertex_pos.z = (firstvertlocz * planesize);// += (z * planesize);

			mod_input_vertex_pos.x += input.instancePosition.x;
			mod_input_vertex_pos.y += input.instancePosition.y;
			mod_input_vertex_pos.z += input.instancePosition.z;
			mod_input_vertex_pos.w = 1.0f;

			//output.position = mul(mod_input_vertex_pos, worldviewproj);

			output.color = input.color;
			output.normal = input.normal;*/

			//firstvertlocx -= 1;
			//firstvertlocy -= 1;
			//firstvertlocz -= 1;

			//somewidth -= 1;
			//someheight -= 1;
			//somedepth -= 1;

			//TOP FACE
			//TOP FACE
			if (vertextype == 1)
			{
				float4 mod_input_vertex_pos = somefirstvertlocation;//  input.position;

				mod_input_vertex_pos.x = (firstvertlocx * planesize);// += (x * planesize);
				mod_input_vertex_pos.y = theextravalue * planesize;// (firstvertlocy* planesize);//// += (y * planesize);
				mod_input_vertex_pos.z = (firstvertlocz * planesize);// += (z * planesize);

				mod_input_vertex_pos.x += input.instancePosition.x;
				mod_input_vertex_pos.y += input.instancePosition.y;
				mod_input_vertex_pos.z += input.instancePosition.z;
				//mod_input_vertex_pos.w = 1.0f;

				//output.position = mul(mod_input_vertex_pos, worldviewproj);
				output.position = mul(mod_input_vertex_pos, worldmatrix );
				output.position = mul(output.position, viewmatrix );
				output.position = mul(output.position, projectionmatrix);
				

				output.color = input.color;
				output.normal = input.normal;
			}
			else if (vertextype == 2)
			{
				float4 mod_input_vertex_pos = somefirstvertlocation;// input.position;

				mod_input_vertex_pos.x = (firstvertlocx * planesize);// += (x * planesize);
				mod_input_vertex_pos.y = theextravalue * planesize;//(firstvertlocy * planesize);//// += (y * planesize);
				mod_input_vertex_pos.z = (firstvertlocz * planesize);// += (z * planesize);

				mod_input_vertex_pos.z += (somedepth * planesize);

				//mod_input_vertex_pos.x *= planesize;// += (x * planesize);
				//mod_input_vertex_pos.y *= planesize;// += (y * planesize);
				//mod_input_vertex_pos.z *= planesize;// += (z * planesize);

				mod_input_vertex_pos.x += input.instancePosition.x;
				mod_input_vertex_pos.y += input.instancePosition.y;
				mod_input_vertex_pos.z += input.instancePosition.z;
				//mod_input_vertex_pos.w = 1.0f;

				//output.position = mul(mod_input_vertex_pos, worldviewproj);
				output.position = mul(mod_input_vertex_pos, worldmatrix );
				output.position = mul(output.position, viewmatrix );
				output.position = mul(output.position, projectionmatrix);

				output.color = input.color;
				output.normal = input.normal;
			}
			else if (vertextype == 3)
			{
				float4 mod_input_vertex_pos = somefirstvertlocation;// input.position;


				mod_input_vertex_pos.x = (firstvertlocx * planesize);// += (x * planesize);
				mod_input_vertex_pos.y = theextravalue * planesize;//(firstvertlocy * planesize);//// += (y * planesize);
				mod_input_vertex_pos.z = (firstvertlocz * planesize);// += (z * planesize);

				mod_input_vertex_pos.x += (somewidth * planesize);


				mod_input_vertex_pos.x += input.instancePosition.x;
				mod_input_vertex_pos.y += input.instancePosition.y;
				mod_input_vertex_pos.z += input.instancePosition.z;
				//mod_input_vertex_pos.w = 1.0f;


				//output.position = mul(mod_input_vertex_pos, worldviewproj);
				output.position = mul(mod_input_vertex_pos, worldmatrix );
				output.position = mul(output.position, viewmatrix );
				output.position = mul(output.position, projectionmatrix);

				output.color = input.color;
				output.normal = input.normal;
			}
			else if (vertextype == 4)
			{
				float4 mod_input_vertex_pos = somefirstvertlocation;//  input.position;

				mod_input_vertex_pos.x = (firstvertlocx * planesize);// += (x * planesize);
				mod_input_vertex_pos.y = theextravalue * planesize;//(firstvertlocy * planesize);//// += (y * planesize);
				mod_input_vertex_pos.z = (firstvertlocz * planesize);// += (z * planesize);

				mod_input_vertex_pos.x += (somewidth * planesize);
				mod_input_vertex_pos.z += (somedepth * planesize);

				mod_input_vertex_pos.x += input.instancePosition.x;
				mod_input_vertex_pos.y += input.instancePosition.y;
				mod_input_vertex_pos.z += input.instancePosition.z;

				///mod_input_vertex_pos.w = 1.0f;

				////output.position = mul(mod_input_vertex_pos, worldviewproj);

				//output.position = mul(mod_input_vertex_pos, worldviewproj);
				output.position = mul(mod_input_vertex_pos, worldmatrix );
				output.position = mul(output.position, viewmatrix );
				output.position = mul(output.position, projectionmatrix);


				output.color = input.color;
				output.normal = input.normal;
			}
			//TOP FACE
			//TOP FACE


			//LEFT FACE
			//LEFT FACE
			else if (vertextype == 5)
			{
				float4 mod_input_vertex_pos = somefirstvertlocation;//  input.position;

				


				mod_input_vertex_pos.x = theextravalue * planesize;//(firstvertlocx * planesize);// += (x * planesize);
				mod_input_vertex_pos.y = (firstvertlocy * planesize);//// += (y * planesize);
				mod_input_vertex_pos.z = (firstvertlocz * planesize);// += (z * planesize);

				//mod_input_vertex_pos.z -= planesize;


				mod_input_vertex_pos.x += input.instancePosition.x;
				mod_input_vertex_pos.y += input.instancePosition.y;
				mod_input_vertex_pos.z += input.instancePosition.z;
				//mod_input_vertex_pos.w = 1.0f;


				//output.position = mul(mod_input_vertex_pos, worldviewproj);
				output.position = mul(mod_input_vertex_pos, worldmatrix );
				output.position = mul(output.position, viewmatrix );
				output.position = mul(output.position, projectionmatrix);


				output.color = input.color;
				output.normal = input.normal;
			}
			else if (vertextype == 6)
			{
				float4 mod_input_vertex_pos = somefirstvertlocation;// input.position;
				//firstvertlocz -= planesize;

				mod_input_vertex_pos.x = theextravalue * planesize;//(firstvertlocx * planesize);// += (x * planesize);
				mod_input_vertex_pos.y = (firstvertlocy * planesize);//// += (y * planesize);
				mod_input_vertex_pos.z = (firstvertlocz * planesize);// += (z * planesize);

				//mod_input_vertex_pos.z -= planesize;

				mod_input_vertex_pos.z += (somedepth * planesize);

				//mod_input_vertex_pos.x *= planesize;// += (x * planesize);
				//mod_input_vertex_pos.y *= planesize;// += (y * planesize);
				//mod_input_vertex_pos.z *= planesize;// += (z * planesize);

				mod_input_vertex_pos.x += input.instancePosition.x;
				mod_input_vertex_pos.y += input.instancePosition.y;
				mod_input_vertex_pos.z += input.instancePosition.z;
				//mod_input_vertex_pos.w = 1.0f;

				////output.position = mul(mod_input_vertex_pos, worldviewproj);

				//output.position = mul(mod_input_vertex_pos, worldviewproj);
				output.position = mul(mod_input_vertex_pos, worldmatrix );
				output.position = mul(output.position, viewmatrix );
				output.position = mul(output.position, projectionmatrix);

				output.color = input.color;
				output.normal = input.normal;
			}
			else if (vertextype == 7)
			{
				float4 mod_input_vertex_pos = somefirstvertlocation;// input.position;

				//firstvertlocz -= planesize;

				mod_input_vertex_pos.x = theextravalue * planesize;//(firstvertlocx * planesize);// += (x * planesize);
				mod_input_vertex_pos.y = (firstvertlocy * planesize);//// += (y * planesize);
				mod_input_vertex_pos.z = (firstvertlocz * planesize);// += (z * planesize);
				//mod_input_vertex_pos.z -= planesize;
				//mod_input_vertex_pos.x += (somewidth * planesize);
				mod_input_vertex_pos.y += (someheight * planesize);

				mod_input_vertex_pos.x += input.instancePosition.x;
				mod_input_vertex_pos.y += input.instancePosition.y;
				mod_input_vertex_pos.z += input.instancePosition.z;
				//mod_input_vertex_pos.w = 1.0f;

				////output.position = mul(mod_input_vertex_pos, worldviewproj);


				//output.position = mul(mod_input_vertex_pos, worldviewproj);
				output.position = mul(mod_input_vertex_pos, worldmatrix );
				output.position = mul(output.position, viewmatrix );
				output.position = mul(output.position, projectionmatrix);

				output.color = input.color;
				output.normal = input.normal;
			}
			else if (vertextype == 8)
			{
				float4 mod_input_vertex_pos = somefirstvertlocation;//  input.position;
				//firstvertlocz -= planesize;

				mod_input_vertex_pos.x = theextravalue * planesize;// (firstvertlocx * planesize);// += (x * planesize);
				mod_input_vertex_pos.y = (firstvertlocy * planesize);//// += (y * planesize);
				mod_input_vertex_pos.z = (firstvertlocz * planesize);// += (z * planesize);
				//mod_input_vertex_pos.z -= planesize;

				mod_input_vertex_pos.y += (someheight * planesize);
				//mod_input_vertex_pos.x += (somewidth * planesize);
				mod_input_vertex_pos.z += (somedepth * planesize);

				mod_input_vertex_pos.x += input.instancePosition.x;
				mod_input_vertex_pos.y += input.instancePosition.y;
				mod_input_vertex_pos.z += input.instancePosition.z;

				///mod_input_vertex_pos.w = 1.0f;

				//output.position = mul(mod_input_vertex_pos, worldviewproj);
				output.position = mul(mod_input_vertex_pos, worldmatrix );
				output.position = mul(output.position, viewmatrix );
				output.position = mul(output.position, projectionmatrix);

				output.color = input.color;
				output.normal = input.normal;
			}
			//LEFT FACE
			//LEFT FACE


			

			
			//RIGHT FACE
			//RIGHT FACE
			else if (vertextype == 9)
			{
				float4 mod_input_vertex_pos = somefirstvertlocation;//  input.position;

				mod_input_vertex_pos.x = theextravalue * planesize;//(firstvertlocx * planesize);// += (x * planesize);
				mod_input_vertex_pos.y = (firstvertlocy * planesize);//// += (y * planesize);
				mod_input_vertex_pos.z = (firstvertlocz * planesize);// += (z * planesize);

				//mod_input_vertex_pos.y += planesize;
				//mod_input_vertex_pos.x -= planesize;
				mod_input_vertex_pos.x += input.instancePosition.x;
				mod_input_vertex_pos.y += input.instancePosition.y;
				mod_input_vertex_pos.z += input.instancePosition.z;
				//mod_input_vertex_pos.w = 1.0f;


				//output.position = mul(mod_input_vertex_pos, worldviewproj);
				output.position = mul(mod_input_vertex_pos, worldmatrix );
				output.position = mul(output.position, viewmatrix );
				output.position = mul(output.position, projectionmatrix);

				output.color = input.color;
				output.normal = input.normal;
			}
			else if (vertextype == 10)
			{
				float4 mod_input_vertex_pos = somefirstvertlocation;// input.position;
				//firstvertlocz -= planesize;

				mod_input_vertex_pos.x = theextravalue * planesize;//(firstvertlocx * planesize);// += (x * planesize);
				mod_input_vertex_pos.y = (firstvertlocy * planesize);//// += (y * planesize);
				mod_input_vertex_pos.z = (firstvertlocz * planesize);// += (z * planesize);

				//mod_input_vertex_pos.y += planesize;
				//mod_input_vertex_pos.x -= planesize;
				mod_input_vertex_pos.z += (somedepth * planesize);

				//mod_input_vertex_pos.x *= planesize;// += (x * planesize);
				//mod_input_vertex_pos.y *= planesize;// += (y * planesize);
				//mod_input_vertex_pos.z *= planesize;// += (z * planesize);

				mod_input_vertex_pos.x += input.instancePosition.x;
				mod_input_vertex_pos.y += input.instancePosition.y;
				mod_input_vertex_pos.z += input.instancePosition.z;
				//mod_input_vertex_pos.w = 1.0f;


				//output.position = mul(mod_input_vertex_pos, worldviewproj);
				output.position = mul(mod_input_vertex_pos, worldmatrix );
				output.position = mul(output.position, viewmatrix );
				output.position = mul(output.position, projectionmatrix);

				output.color = input.color;
				output.normal = input.normal;
			}
			else if (vertextype == 11)
			{
				float4 mod_input_vertex_pos = somefirstvertlocation;// input.position;

				//firstvertlocz -= planesize;

				mod_input_vertex_pos.x = theextravalue * planesize;//(firstvertlocx * planesize);// += (x * planesize);
				mod_input_vertex_pos.y = (firstvertlocy * planesize);//// += (y * planesize);
				mod_input_vertex_pos.z = (firstvertlocz * planesize);// += (z * planesize);

				//mod_input_vertex_pos.y += planesize;
				//mod_input_vertex_pos.x -= planesize;
				//mod_input_vertex_pos.x += (somewidth * planesize);
				mod_input_vertex_pos.y += (someheight * planesize);

				mod_input_vertex_pos.x += input.instancePosition.x;
				mod_input_vertex_pos.y += input.instancePosition.y;
				mod_input_vertex_pos.z += input.instancePosition.z;
				//mod_input_vertex_pos.w = 1.0f;


				//output.position = mul(mod_input_vertex_pos, worldviewproj);
				output.position = mul(mod_input_vertex_pos, worldmatrix );
				output.position = mul(output.position, viewmatrix );
				output.position = mul(output.position, projectionmatrix);

				output.color = input.color;
				output.normal = input.normal;
			}
			else if (vertextype == 12)
			{
				float4 mod_input_vertex_pos = somefirstvertlocation;//  input.position;
				//firstvertlocz -= planesize;

				mod_input_vertex_pos.x = theextravalue * planesize;//(firstvertlocx * planesize);// += (x * planesize);
				mod_input_vertex_pos.y = (firstvertlocy * planesize);//// += (y * planesize);
				mod_input_vertex_pos.z = (firstvertlocz * planesize);// += (z * planesize);

				//mod_input_vertex_pos.y += planesize;
				//mod_input_vertex_pos.x -= planesize;

				mod_input_vertex_pos.y += (someheight * planesize);
				//mod_input_vertex_pos.x += (somewidth * planesize);
				mod_input_vertex_pos.z += (somedepth * planesize);

				mod_input_vertex_pos.x += input.instancePosition.x;
				mod_input_vertex_pos.y += input.instancePosition.y;
				mod_input_vertex_pos.z += input.instancePosition.z;

				///mod_input_vertex_pos.w = 1.0f;


				//output.position = mul(mod_input_vertex_pos, worldviewproj);
				output.position = mul(mod_input_vertex_pos, worldmatrix );
				output.position = mul(output.position, viewmatrix );
				output.position = mul(output.position, projectionmatrix);

				output.color = input.color;
				output.normal = input.normal;
			}
			//RIGHT FACE
			//RIGHT FACE




			
			//FRONT FACE
			//FRONT FACE
			else if (vertextype == 13)
			{
			float4 mod_input_vertex_pos = somefirstvertlocation;//  input.position;

			mod_input_vertex_pos.x = (firstvertlocx * planesize);// += (x * planesize);
			mod_input_vertex_pos.y = (firstvertlocy * planesize);//// += (y * planesize);
			mod_input_vertex_pos.z = theextravalue * planesize;//(firstvertlocz * planesize);// += (z * planesize);


			//mod_input_vertex_pos.y += planesize;
			//mod_input_vertex_pos.x -= planesize;

			mod_input_vertex_pos.x += input.instancePosition.x;
			mod_input_vertex_pos.y += input.instancePosition.y;
			mod_input_vertex_pos.z += input.instancePosition.z;
			//mod_input_vertex_pos.w = 1.0f;


			//output.position = mul(mod_input_vertex_pos, worldviewproj);
			output.position = mul(mod_input_vertex_pos, worldmatrix );
			output.position = mul(output.position, viewmatrix );
			output.position = mul(output.position, projectionmatrix);

			output.color = input.color;
			output.normal = input.normal;
			}
			else if (vertextype == 14)
			{
			float4 mod_input_vertex_pos = somefirstvertlocation;// input.position;

			mod_input_vertex_pos.x = (firstvertlocx * planesize);// += (x * planesize);
			mod_input_vertex_pos.y = (firstvertlocy * planesize);//// += (y * planesize);
			mod_input_vertex_pos.z = theextravalue * planesize;//(firstvertlocz * planesize);// += (z * planesize);

			//mod_input_vertex_pos.y += planesize;
			//mod_input_vertex_pos.x -= planesize;

			mod_input_vertex_pos.x += (somewidth * planesize);

			//mod_input_vertex_pos.x *= planesize;// += (x * planesize);
			//mod_input_vertex_pos.y *= planesize;// += (y * planesize);
			//mod_input_vertex_pos.z *= planesize;// += (z * planesize);

			mod_input_vertex_pos.x += input.instancePosition.x;
			mod_input_vertex_pos.y += input.instancePosition.y;
			mod_input_vertex_pos.z += input.instancePosition.z;
			//mod_input_vertex_pos.w = 1.0f;

			//output.position = mul(mod_input_vertex_pos, worldviewproj);
			output.position = mul(mod_input_vertex_pos, worldmatrix );
			output.position = mul(output.position, viewmatrix );
			output.position = mul(output.position, projectionmatrix);

			output.color = input.color;
			output.normal = input.normal;
			}
			else if (vertextype == 15)
			{
			float4 mod_input_vertex_pos = somefirstvertlocation;// input.position;


			mod_input_vertex_pos.x = (firstvertlocx * planesize);// += (x * planesize);
			mod_input_vertex_pos.y = (firstvertlocy * planesize);//// += (y * planesize);
			mod_input_vertex_pos.z = theextravalue * planesize;// (firstvertlocz * planesize);// += (z * planesize);

			//mod_input_vertex_pos.y += planesize;
			//mod_input_vertex_pos.x -= planesize;

			//mod_input_vertex_pos.x += (somewidth * planesize);
			mod_input_vertex_pos.y += (someheight * planesize);

			mod_input_vertex_pos.x += input.instancePosition.x;
			mod_input_vertex_pos.y += input.instancePosition.y;
			mod_input_vertex_pos.z += input.instancePosition.z;
			//mod_input_vertex_pos.w = 1.0f;


			//output.position = mul(mod_input_vertex_pos, worldviewproj);
			output.position = mul(mod_input_vertex_pos, worldmatrix );
			output.position = mul(output.position, viewmatrix );
			output.position = mul(output.position, projectionmatrix);
			output.color = input.color;
			output.normal = input.normal;
			}
			else if (vertextype == 16)
			{
			float4 mod_input_vertex_pos = somefirstvertlocation;//  input.position;

			mod_input_vertex_pos.x = (firstvertlocx * planesize);// += (x * planesize);
			mod_input_vertex_pos.y = (firstvertlocy * planesize);//// += (y * planesize);
			mod_input_vertex_pos.z = theextravalue * planesize;//(firstvertlocz * planesize);// += (z * planesize);

			//mod_input_vertex_pos.y += planesize;
			//mod_input_vertex_pos.x -= planesize;

			mod_input_vertex_pos.y += (someheight * planesize);
			//mod_input_vertex_pos.x += (somewidth * planesize);
			mod_input_vertex_pos.x += (somewidth * planesize);

			mod_input_vertex_pos.x += input.instancePosition.x;
			mod_input_vertex_pos.y += input.instancePosition.y;
			mod_input_vertex_pos.z += input.instancePosition.z;

			///mod_input_vertex_pos.w = 1.0f;

			//output.position = mul(mod_input_vertex_pos, worldviewproj);
			output.position = mul(mod_input_vertex_pos, worldmatrix );
			output.position = mul(output.position, viewmatrix );
			output.position = mul(output.position, projectionmatrix);
			output.color = input.color;
			output.normal = input.normal;
			}
			//FRONT FACE
			//FRONT FACE



			
			//BACK FACE
			//BACK FACE
			else if (vertextype == 17)
			{
				float4 mod_input_vertex_pos = somefirstvertlocation;//  input.position;

				mod_input_vertex_pos.x = (firstvertlocx * planesize);// += (x * planesize);
				mod_input_vertex_pos.y = (firstvertlocy * planesize);//// += (y * planesize);
				mod_input_vertex_pos.z = theextravalue * planesize;//(firstvertlocz * planesize);// += (z * planesize);


				//mod_input_vertex_pos.z -= planesize;
				//mod_input_vertex_pos.y += planesize;

				mod_input_vertex_pos.x += input.instancePosition.x;
				mod_input_vertex_pos.y += input.instancePosition.y;
				mod_input_vertex_pos.z += input.instancePosition.z;
				//mod_input_vertex_pos.w = 1.0f;

				//output.position = mul(mod_input_vertex_pos, worldviewproj);
				output.position = mul(mod_input_vertex_pos, worldmatrix );
				output.position = mul(output.position, viewmatrix );
				output.position = mul(output.position, projectionmatrix);
				output.color = input.color;
				output.normal = input.normal;
			}
			else if (vertextype == 18)
			{
				float4 mod_input_vertex_pos = somefirstvertlocation;// input.position;

				mod_input_vertex_pos.x = (firstvertlocx * planesize);// += (x * planesize);
				mod_input_vertex_pos.y = (firstvertlocy * planesize);//// += (y * planesize);
				mod_input_vertex_pos.z = theextravalue * planesize;//(firstvertlocz * planesize);// += (z * planesize);

				//mod_input_vertex_pos.z -= planesize;
				//mod_input_vertex_pos.y += planesize;

				mod_input_vertex_pos.x += (somewidth * planesize);

				//mod_input_vertex_pos.x *= planesize;// += (x * planesize);
				//mod_input_vertex_pos.y *= planesize;// += (y * planesize);
				//mod_input_vertex_pos.z *= planesize;// += (z * planesize);

				mod_input_vertex_pos.x += input.instancePosition.x;
				mod_input_vertex_pos.y += input.instancePosition.y;
				mod_input_vertex_pos.z += input.instancePosition.z;
				//mod_input_vertex_pos.w = 1.0f;


				//output.position = mul(mod_input_vertex_pos, worldviewproj);
				output.position = mul(mod_input_vertex_pos, worldmatrix );
				output.position = mul(output.position, viewmatrix );
				output.position = mul(output.position, projectionmatrix);
				output.color = input.color;
				output.normal = input.normal;
			}
			else if (vertextype == 19)
			{
				float4 mod_input_vertex_pos = somefirstvertlocation;// input.position;


				mod_input_vertex_pos.x = (firstvertlocx * planesize);// += (x * planesize);
				mod_input_vertex_pos.y = (firstvertlocy * planesize);//// += (y * planesize);
				mod_input_vertex_pos.z = theextravalue * planesize;//(firstvertlocz * planesize);// += (z * planesize);

				//mod_input_vertex_pos.z -= planesize;
				//mod_input_vertex_pos.y += planesize;

				//mod_input_vertex_pos.x += (somewidth * planesize);
				mod_input_vertex_pos.y += (someheight * planesize);

				mod_input_vertex_pos.x += input.instancePosition.x;
				mod_input_vertex_pos.y += input.instancePosition.y;
				mod_input_vertex_pos.z += input.instancePosition.z;
				//mod_input_vertex_pos.w = 1.0f;


				//output.position = mul(mod_input_vertex_pos, worldviewproj);
				output.position = mul(mod_input_vertex_pos, worldmatrix );
				output.position = mul(output.position, viewmatrix );
				output.position = mul(output.position, projectionmatrix);
				output.color = input.color;
				output.normal = input.normal;
			}
			else if (vertextype == 20)
			{
				float4 mod_input_vertex_pos = somefirstvertlocation;//  input.position;

				mod_input_vertex_pos.x = (firstvertlocx * planesize);// += (x * planesize);
				mod_input_vertex_pos.y = (firstvertlocy * planesize);//// += (y * planesize);
				mod_input_vertex_pos.z = theextravalue * planesize;//(firstvertlocz * planesize);// += (z * planesize);

				//mod_input_vertex_pos.z -= planesize;
				//mod_input_vertex_pos.y += planesize;

				mod_input_vertex_pos.y += (someheight * planesize);
				//mod_input_vertex_pos.x += (somewidth * planesize);
				mod_input_vertex_pos.x += (somewidth * planesize);

				mod_input_vertex_pos.x += input.instancePosition.x;
				mod_input_vertex_pos.y += input.instancePosition.y;
				mod_input_vertex_pos.z += input.instancePosition.z;

				///mod_input_vertex_pos.w = 1.0f;

				//output.position = mul(mod_input_vertex_pos, worldviewproj);
				output.position = mul(mod_input_vertex_pos, worldmatrix );
				output.position = mul(output.position, viewmatrix );
				output.position = mul(output.position, projectionmatrix);
				output.color = input.color;
				output.normal = input.normal;
			}
			//BACK FACE
			//BACK FACE


			
			//BOTTOM FACE
			//BOTTOM FACE
			if (vertextype == 21)
			{
				float4 mod_input_vertex_pos = somefirstvertlocation;//  input.position;

				mod_input_vertex_pos.x = (firstvertlocx * planesize);// += (x * planesize);
				mod_input_vertex_pos.y = theextravalue * planesize;// (firstvertlocy * planesize);//// += (y * planesize);
				mod_input_vertex_pos.z = (firstvertlocz * planesize);// += (z * planesize);

				//mod_input_vertex_pos.y += planesize;

				mod_input_vertex_pos.x += input.instancePosition.x;
				mod_input_vertex_pos.y += input.instancePosition.y;
				mod_input_vertex_pos.z += input.instancePosition.z;
				//mod_input_vertex_pos.w = 1.0f;

				//output.position = mul(mod_input_vertex_pos, worldviewproj);
				output.position = mul(mod_input_vertex_pos, worldmatrix );
				output.position = mul(output.position, viewmatrix );
				output.position = mul(output.position, projectionmatrix);
				output.color = input.color;
				output.normal = input.normal;
			}
			else if (vertextype == 22)
			{
				float4 mod_input_vertex_pos = somefirstvertlocation;// input.position;

				mod_input_vertex_pos.x = (firstvertlocx * planesize);// += (x * planesize);
				mod_input_vertex_pos.y = theextravalue * planesize;//(firstvertlocy * planesize);//// += (y * planesize);
				mod_input_vertex_pos.z = (firstvertlocz * planesize);// += (z * planesize);

				//mod_input_vertex_pos.y += planesize;

				mod_input_vertex_pos.z += (somedepth * planesize);

				//mod_input_vertex_pos.x *= planesize;// += (x * planesize);
				//mod_input_vertex_pos.y *= planesize;// += (y * planesize);
				//mod_input_vertex_pos.z *= planesize;// += (z * planesize);

				mod_input_vertex_pos.x += input.instancePosition.x;
				mod_input_vertex_pos.y += input.instancePosition.y;
				mod_input_vertex_pos.z += input.instancePosition.z;
				//mod_input_vertex_pos.w = 1.0f;


				//output.position = mul(mod_input_vertex_pos, worldviewproj);
				output.position = mul(mod_input_vertex_pos, worldmatrix );
				output.position = mul(output.position, viewmatrix );
				output.position = mul(output.position, projectionmatrix);

				output.color = input.color;
				output.normal = input.normal;
			}
			else if (vertextype == 23)
			{
				float4 mod_input_vertex_pos = somefirstvertlocation;// input.position;


				mod_input_vertex_pos.x = (firstvertlocx * planesize);// += (x * planesize);
				mod_input_vertex_pos.y = theextravalue * planesize;// (firstvertlocy * planesize);//// += (y * planesize);
				mod_input_vertex_pos.z = (firstvertlocz * planesize);// += (z * planesize);
				
				//mod_input_vertex_pos.y += planesize;

				mod_input_vertex_pos.x += (somewidth * planesize);


				mod_input_vertex_pos.x += input.instancePosition.x;
				mod_input_vertex_pos.y += input.instancePosition.y;
				mod_input_vertex_pos.z += input.instancePosition.z;
				//mod_input_vertex_pos.w = 1.0f;


				//output.position = mul(mod_input_vertex_pos, worldviewproj);
				output.position = mul(mod_input_vertex_pos, worldmatrix );
				output.position = mul(output.position, viewmatrix );
				output.position = mul(output.position, projectionmatrix);
				output.color = input.color;
				output.normal = input.normal;
			}
			else if (vertextype == 24)
			{
				float4 mod_input_vertex_pos = somefirstvertlocation;//  input.position;

				mod_input_vertex_pos.x = (firstvertlocx * planesize);// += (x * planesize);
				mod_input_vertex_pos.y = theextravalue * planesize;//(firstvertlocy * planesize);//// += (y * planesize);
				mod_input_vertex_pos.z = (firstvertlocz * planesize);// += (z * planesize);
				
				//mod_input_vertex_pos.y += planesize;

				mod_input_vertex_pos.x += (somewidth * planesize);
				mod_input_vertex_pos.z += (somedepth * planesize);

				mod_input_vertex_pos.x += input.instancePosition.x;
				mod_input_vertex_pos.y += input.instancePosition.y;
				mod_input_vertex_pos.z += input.instancePosition.z;

				///mod_input_vertex_pos.w = 1.0f;

				//output.position = mul(mod_input_vertex_pos, worldviewproj);
				output.position = mul(mod_input_vertex_pos, worldmatrix );
				output.position = mul(output.position, viewmatrix );
				output.position = mul(output.position, projectionmatrix);
				output.color = input.color;
				output.normal = input.normal;
			}
			//BOTTOM FACE
			//BOTTOM FACE
			




			/*
			if (vertextype == 1)
			{
				float4 mod_input_vertex_pos = somefirstvertlocation;//  input.position;

				mod_input_vertex_pos.x = (firstvertlocx * planesize);// += (x * planesize);
				mod_input_vertex_pos.y = (firstvertlocy * planesize);//// += (y * planesize);
				mod_input_vertex_pos.z = (firstvertlocz * planesize);// += (z * planesize);


				mod_input_vertex_pos.x += input.instancePosition.x;
				mod_input_vertex_pos.y += input.instancePosition.y;
				mod_input_vertex_pos.z += input.instancePosition.z;
				//mod_input_vertex_pos.w = 1.0f;

				//output.position = mul(mod_input_vertex_pos, worldviewproj);

				output.color = input.color;
				output.normal = input.normal;
			}
			else if (vertextype == 2)
			{
				float4 mod_input_vertex_pos = somefirstvertlocation;// input.position;

				mod_input_vertex_pos.x = (firstvertlocx * planesize);// += (x * planesize);
				mod_input_vertex_pos.y = (firstvertlocy * planesize);//// += (y * planesize);
				mod_input_vertex_pos.z = (firstvertlocz * planesize);// += (z * planesize);
				mod_input_vertex_pos.z += (somedepth * planesize);

				//mod_input_vertex_pos.x *= planesize;// += (x * planesize);
				//mod_input_vertex_pos.y *= planesize;// += (y * planesize);
				//mod_input_vertex_pos.z *= planesize;// += (z * planesize);

				mod_input_vertex_pos.x += input.instancePosition.x;
				mod_input_vertex_pos.y += input.instancePosition.y;
				mod_input_vertex_pos.z += input.instancePosition.z;
				//mod_input_vertex_pos.w = 1.0f;

				//output.position = mul(mod_input_vertex_pos, worldviewproj);

				output.color = input.color;
				output.normal = input.normal;
			}
			else if (vertextype == 3)
			{
				float4 mod_input_vertex_pos = somefirstvertlocation;// input.position;


				mod_input_vertex_pos.x = (firstvertlocx * planesize);// += (x * planesize);
				mod_input_vertex_pos.y = (firstvertlocy * planesize);//// += (y * planesize);
				mod_input_vertex_pos.z = (firstvertlocz * planesize);// += (z * planesize);

				mod_input_vertex_pos.x += (somewidth * planesize);


				mod_input_vertex_pos.x += input.instancePosition.x;
				mod_input_vertex_pos.y += input.instancePosition.y;
				mod_input_vertex_pos.z += input.instancePosition.z;
				//mod_input_vertex_pos.w = 1.0f;

				//output.position = mul(mod_input_vertex_pos, worldviewproj);

				output.color = input.color;
				output.normal = input.normal;
			}
			else if (vertextype == 4)
			{
				float4 mod_input_vertex_pos = somefirstvertlocation;//  input.position;

			

				mod_input_vertex_pos.x = (firstvertlocx * planesize);// += (x * planesize);
				mod_input_vertex_pos.y = (firstvertlocy * planesize);//// += (y * planesize);
				mod_input_vertex_pos.z = (firstvertlocz * planesize);// += (z * planesize);
				mod_input_vertex_pos.x += (somewidth * planesize);
				mod_input_vertex_pos.z += (somedepth * planesize);


				mod_input_vertex_pos.x += input.instancePosition.x;
				mod_input_vertex_pos.y += input.instancePosition.y;
				mod_input_vertex_pos.z += input.instancePosition.z;

				///mod_input_vertex_pos.w = 1.0f;

				//output.position = mul(mod_input_vertex_pos, worldviewproj);

				output.color = input.color;
				output.normal = input.normal;
			}*/

		}
	}




























	//else 
	{
		/*float4 mod_input_vertex_pos = input.position;

		mod_input_vertex_pos.x;// += (x * planesize);
		mod_input_vertex_pos.y;// += (y * planesize);
		mod_input_vertex_pos.z;// += (z * planesize);

		mod_input_vertex_pos.x += input.instancePosition.x;
		mod_input_vertex_pos.y += input.instancePosition.y;
		mod_input_vertex_pos.z += input.instancePosition.z;
		mod_input_vertex_pos.w = 1.0f;

		//output.position = mul(mod_input_vertex_pos, worldviewproj);

		output.color = input.color;
		output.normal = input.normal;*/

	}
	


	/*float4 mod_input_vertex_pos = input.position;

	mod_input_vertex_pos.x += (x * planesize);
	mod_input_vertex_pos.y += (y * planesize);
	mod_input_vertex_pos.z += (z * planesize);

	mod_input_vertex_pos.x += input.instancePosition.x;
	mod_input_vertex_pos.y += input.instancePosition.y;
	mod_input_vertex_pos.z += input.instancePosition.z;
	mod_input_vertex_pos.w = 1.0f;

	//output.position = mul(mod_input_vertex_pos, worldviewproj);

	output.color = input.color;
	output.normal = input.normal;*/

	//input.position.x = input.position.x * -1;






	//uint sometest = 0x0000;

	return output;
}






float4 PS(PS_IN input) : SV_Target
{

	/*int facetype = int(input.indexpos.w);
	float4 somecolor = float4(0.25f, 0.25f, 0.25f, 1.0f);

	if (facetype == 1 || facetype == 2 || facetype == 3 || facetype == 4)
	{
		somecolor = float4(0.25f, 0.25f, 0.25f, 1.0f);
		//somecolor *= 1.15f;
	}
	else if (facetype == 5 || facetype == 6 || facetype == 7 || facetype == 8)
	{
		somecolor = float4(0.25f, 0.75f, 0.25f, 1.0f);
		//somecolor *= 1.25f;
	}
	input.color = somecolor;*/

	return  input.color;//input.color;// input.color;// input.col;
}
