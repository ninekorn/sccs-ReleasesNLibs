[![Github All Releases](https://img.shields.io/github/downloads/ninekorn/sccs/total.svg)]() <== added icon 04thapril2022 and it doesn't work yet. But it will work when i make a proper release. This year, this repository generated approx 3000++ clones. and about 3000 of those in two/three weeks around the time that i uploaded my virtual desktop voxel heightmap directx solution.

[![Github All Releases](https://img.shields.io/github/license/ninekorn/sccs)]()

Discord channel invite: https://discord.gg/jVhSa3tt

# sccs-ReleasesNLibs
same as repo sccs but only releases and libs and tests

WORKING WITH THE OCULUS RIFT CV1 AND WITHOUT VR (sccsr14forms) FOR MY VIRTUAL DESKTOP SIMILI OVERLAY FOR PLAYING NON COMPETITIVE VIDEO GAMES DUE TO A GRID LIKE VOXEL THAT ACTS AS AN OVERLAY AND IT CAN BE CONSIDERED AN ADVANTAGE FOR PLAYERS USING MY PROGRAM WHEN PLAYING COMPETITIVE GAMES AND YOU MIGHT BE BANNED ON THOSE GAMES. OTHERWISE, IT'S A WORK IN PROGRESS FOR VR AND NON-VR VIRTUAL DESKTOP AND VOXEL WORLD. 

developped by steve chassé

i updated my solutions program to only use the ab3d.OculusWrap DLL only.

regards,
steve chassé

OCULUS RIFT CV1 only - NOT WORKING YET FOR AS AN OVERLAY IN ELITE DANGEROUS AS I HAVE A HARD TIME MAKING THE STEAMVR OVERLAY WORK - 

as per advertised here: 
https://forums.frontier.co.uk/threads/virtual-desktop-program-with-embedded-physics-engine-at-the-press-of-a-button-coming-in-2020.542577/#post-8514927 .
https://twitter.com/sccoresystems1
https://www.reddit.com/r/virtualreality/comments/p2cwb8/look_at_what_i_made_in_c_using_the_shardpx_and/
https://www.facebook.com/sccoresystems

currently the keyboard is not working and the microsoft windows voice recognition stopped working. I have coded this program and used as reference 
many sources from the internet and normally i try to reference it all when i program.

the username is "9" and the password is "std" for standard. 

List of programs:

sccsv101 - 1st version - using instancing but i ended up discarding that program because there was some stuff that wasn't working as expected. I wanted to build some voxel covid19 cloth objects but they ended up looking a bit more like snowflakes/minesweeper/covid19cells. https://youtu.be/GKVrsqPHaMQ

<img WIDTH=500 src="https://i.ibb.co/LJP0C2W/sccsv10.png" alt="sccsv10" border="0">

sccsv11 - 2nd version - decided to modify a couple of things compared to sccsv101 but again, i thought my program architecture was lame.

<img WIDTH=500 src="https://i.ibb.co/c66WLyn/sccsv11.png" alt="sccsv11" border="0">

sccsr12dotnet and sccsr12wpf - 3rd version. console dotnet directx virtual reality.

<img WIDTH=500 src="https://i.ibb.co/chZnvY2/sccs-VD4-ED.png" alt="sccs-VD4-ED" border="0">

sccsr12dotnet and sccsr12wpf - 3rd version. showing the instancing capabilities. but in this screenshot i wasn't done with the vr human ik so the above screenshot was my later revision of sccsr12dotnet.

<img WIDTH=500 src="https://i.ibb.co/jkbRf86/sccs-humanvrikvoxelrig.webp" alt="sccs-humanvrikvoxelrig" border="0">

sccsr13dotnet and sccsr13wpf - 4rth version. breakable voxel virtual desktop (my normal iteration of the virtual desktop is interactive with the oculus touch but not my breakable virtual desktop iteration)

<img WIDTH=500 src="https://i.ibb.co/khpmr7g/sccoresystems-25august2021-04.png" alt="sccoresystems-25august2021-04" border="0">

sccsr14forms - 5th revision - virtual desktop in vr and without vr, with physical mesh voxel keyboard and keyboard keys changing color when pressed for VR only. Without VR there isn't a physical keyboard voxel mesh yet but i think i will add one in the future for using my software when streaming and for viewers of the stream to visually see where the user of my software clicks on the keyboard, when doing programming and editing tutorials this is a very good way to let viewers know what keys are being pressed when we are working fast to show them which keyboard shortcuts are being used. I added my vertex/triangle reducer technique to the human vr ik rig, it finally works. Also, i added fingers to the hands and used the oculus touch index sensors for the index pose and the hand trigger to grasp.

<img WIDTH=500 src="https://i.ibb.co/dfn3KxZ/tablevoxeldesktop-2022-02-22-185240.png" alt="tablevoxeldesktop-2022-02-22-185240" border="0">
<img WIDTH = 500 src="https://i.ibb.co/5WRHCMC/Capture-d-cran-2022-04-03-005256.png" alt="Capture-d-cran-2022-04-03-005256" border="0"><img WIDTH = 500 src="https://i.ibb.co/Nn3cLmf/Capture-d-cran-2022-04-03-014034.png" alt="Capture-d-cran-2022-04-03-014034" border="0"><img WIDTH=500 src="https://i.ibb.co/cK925KF/Capture-d-cran-2022-04-06-200255.png" alt="Capture-d-cran-2022-04-06-200255" border="0">

May 2022 release - I have developped a better version of my level generator with enclosed walls and using my vertex/triangle reducer technique. I have attempted to use inverse kinematics with floor collision using byte index location but it isn't working that great yet. I am not using physics yet either. Also the voxels are not breakable currently but it is my plan for the future. This is a demo.  I also was able to develop the logic for voxels level of detail and a 2d dot frustrum culling. Also i developed a defered rendering example without VR using my voxel level generator. I developed a completely new voxel level generator and it is super fast now, i stopped using check for contain of lists and dictionaries while building my level generator and used integer index check within an integer map to decide if to build a wall/floor/ceiling.

<img WIDTH=500 src="https://i.ibb.co/CKBRYPp/Capture-d-cran-2022-05-13-222134.png" alt="Capture-d-cran-2022-05-13-222134" border="0"><img WIDTH=500 src="https://i.ibb.co/x5jKk9P/Capture-d-cran-2022-05-17-002056.png" alt="Capture-d-cran-2022-05-17-002056" border="0"><img WIDTH=500 src="https://i.ibb.co/BgBKp4G/Capture-d-cran-2022-05-17-002233.png" alt="Capture-d-cran-2022-05-17-002233" border="0">

<img WIDTH=500 src="https://i.ibb.co/jDfwXL0/Photo-GIF-5-22-2022-9-41-11-PM.gif" alt="Photo-GIF-5-22-2022-9-41-11-PM" border="0"><img WIDTH=500 src="https://i.ibb.co/109dL2f/Photo-GIF-5-22-2022-9-45-35-PM.gif" alt="Photo-GIF-5-22-2022-9-45-35-PM" border="0"><img WIDTH=500 src="https://i.ibb.co/BwC9SNh/Photo-GIF-5-21-2022-3-03-54-AM.gif" alt="Photo-GIF-5-21-2022-3-03-54-AM" border="0"><img WIDTH=500 src="https://i.ibb.co/J2cVs8J/Capture-d-cran-2022-06-05-085053.png" alt="Capture-d-cran-2022-06-05-085053" border="0"><img WIDTH=500 src="https://i.ibb.co/ncmMfsh/Capture-d-cran-2022-06-05-155250.png" alt="Capture-d-cran-2022-06-05-155250" border="0">
In the examples above, please do not take into account the FPS as it isn't the real rendering thread fps. The rendering thread fps was as low as 20 fps when rendering individual meshes and a big level depending on how far i was making the level of detail 1 to display the most detailed voxel chunks. My idea was to make 5 levels of details to test the performance, and even with those, i couldn't get a real stable 60-90fps in virtual reality, even when including my 2d dot frustrum culling and distance culling. That's why i decided to finally develop my idea of instancing voxels. I succeeded in June 2022 to make breakable voxel instances in a most basic form but it lags. 

June 2022 Uploads: Chunked Voxel instancing with breakable adjacent chunks in VR using the Oculus Rift CV1. Using EVGA GTX 960 and 8gb ram. Somehow my RX570 doesn't display the Chunked Voxel instances... Laggy demo for a basic example of how to make a chunked voxel instanced Random Level with breakable adjacent chunks. This demo only has breakable chunked voxel instancing with adjacent chunks also breaking and a voxel human ik rig. There are no Virtual desktops yet and there is no deferred rendering either. I create 64 types of meshes for each type of face (front/back/top/bottom/left/right) with the 1st one being 4 vertex and the 64rth is 256 vertex. Each faces are drawn separately as they have their own sets of instances. Also the chunks dimensions cannot be changed for the moment and are stuck at 4x4x4. I didn't knew up until the month of june that what i was attempting to develop was actually called "Chunked Voxel Instancing". I also use my vertex/triangle reducer technique for my Chunked Voxel Instances in order to make larger faces that cover more area and reduces the vertex/triangle number further... But spawning 1000 instances of a mesh that includes only 4 vertex for each instances and having to create a new buffer when destroying a face to remove 1 instance only is a very costly operation, hence why i am refering to it as my "Chunked Voxel Instancing in it's most basic form" and you can find my development in the month of June where i succeeded... 

<img WIDTH=500 src="https://i.ibb.co/h9xzjpL/Capture-d-cran-2022-06-23-193841.png" alt="Capture-d-cran-2022-06-23-193841" border="0"><img WIDTH=500 src="https://i.ibb.co/djrtkNV/Capture-d-cran-2022-06-19-221226.png" alt="Capture-d-cran-2022-06-19-221226" border="0"><img WIDTH=500 src="https://i.ibb.co/zPhWzy2/Capture-d-cran-2022-06-23-104938.png" alt="Capture-d-cran-2022-06-23-104938" border="0">

July 2022 uploads: This last night, i uploaded my next version of chunked voxel instancing. My main goal was to use my development on my earlier version using chunks of size 4x4x4 and implement chunks of size 8x8x8. But just changing the values to 8 in width and 8 in height and 8 in depth wasn't going to work. For the reason that i was and still am using 2 worldmatrices to store the first vertex location of each face and 2 worldmatrices to store the dimensions of each faces. So i am inserting the first vertex location xyz in digits in the worldmatrix and using currently a maximum of 6 digits (2 vertex) per coordinates, casting them as integer once inside of the shader. 2 Worldmatrices using 4 vectors each and 4 coordinates per vector. When i use 2 faces per digits and having 32 total floats that i can insert data into, i can build a maximum of 64 faces, which is the strict minimum to be able to build a chunk instanced voxel with all bytes of the chunk able to be built as a face and not forget any faces for 4x4x4. So since i was limited by my vertex bindings, of using 2 worldmatrices for the face dimensions and locations, i had to find another way. I developed a way to extend the vertices up to 8 in width and 8 in height and 8 in depth while keeping my chunks original at 4x4x4 but fetching their map data of a map of 8x8x8. Of course, the first chunk in a bundle of eight chunks of 4x4x4, the one at 0x0y0z, can have it's vertex extended up to 8 in width and 8 in height and 8 in depth, the rest of the chunks in that bundle are max of 4 in width and 4 in height and 4 in depth. So i didn't change the creation of the chunks from 4x4x4, instead, i only changed the extent to which my vertex/triangle reducer is working in order to reach up to 8 in width and height and depth. So i am building a bundle chunk of eight chunks of 4x4x4 to make one chunk of 8x8x8 (512 bytes), and the first chunk of 4x4x4 at location 0x0y0z in the bundle, is extending from 0 to 7 in width and height and depth to cover the most possible area for a face. The rest of the bytes in the bundle, are simply building faces starting from their index up to the extent of the chunk 8x8x8. The principle is working, because all bytes are accounted for. All bytes have a possibility to have a first vertex/face built there, but only the first chunk of 4x4x4 in the bundle of 8 chunks can extend from 1 of width/height/depth to 8 of width/height/depth. That's why i am calling this "simulating" a chunk of 8x8x8 from eight chunks of 4x4x4. It was very difficult to develop. But my draft template is done. Now will be the time to work on optimizations. The looks are pretty much the same as my june 2022 releases but i can spawn double the amount of voxels so they look smaller. Although the size of the voxels seem smaller than my june 2022 release, in actuality i made the level bigger and decreased the height of the ceiling of the level. So more voxels, but it lags breaking voxels because the arrays/buffers are huge... In my june 2022 i made so that it's possible to divide the level in multiple parts so that the arrays/buffers are smaller in size, but doing that increases the SharpDX.Direct3D11.Buffers and classes which makes dividing the level in multiple fractions not the right way to go for trying to decrease the ram usage and cpu usage. I will have to develop a way to write to buffer in small portions or an altogether other approach to make sure the fps isn't dropping. To give an idea, for a level 100 in width and 100 in depth and 8 in height, with a voxel size of 0.05f and 2 voxel chunks per unit of the level, without dividing the level in fractions, i get about 45fps to 60 fps, but when dividing the level it drops to 35 fps. If i would use my june 2022 release with distance and frustrum culling and level of detail (LOD) it was as low as 20 fps but breaking bytes was better since all chunks were individual meshes of 10x10x10, and now they are 4x4x4 simulating a chunk of 8x8x8 but are instances of the same mesh.

<img WIDTH=500 src="https://i.ibb.co/Jprc4Wb/Capture-d-cran-2022-07-07-225847.png" alt="Capture-d-cran-2022-07-07-225847" border="0">

Edit-2022-July-12: I developed a way to increase the chunks size from 4x4x4 to simulating chunks up to 100x100x100x in size. But it doesn't help the performance. Although i will keep this new way to make the chunks bigger for my next developments, i am a bit chocked that it doesn't help the performance when rendering as i made sure that it uses my vertex/triangle reducer. I will investigate. Edit: precision loss after 16x16x16. can only change chunks to dimensions of 4x4x4 or 8x8x8 or 16x16x16. I left notes in the script updateprim.cs

<img WIDTH=500 src="https://i.ibb.co/XZmpM24/Capture-d-cran-2022-07-12-230527.png" alt="Capture-d-cran-2022-07-12-230527" border="0">

--------------------------------------------------------------------------




I have learned to create my own direct3D c# programs and the main help from being able to get out of unity3d and be independant from unity3D was to code my own programs in C#. It was a sad choice, but back in unity 2017, adding the framework in order to make a virtual desktop from sharpdx was a tough thing to do and i couldn't make it happen. i then chose to learn by myself. But i am not giving up on unity3d yet, neither on the ab3d.dxengine. I've just got to be at a further point of stability in my c# barebone program to then make a complete copy of my engine in unity3d/ab3d.dxengine/xenko. Gotta find the time too.

So for developping my softwares, i took this as a reference:
https://github.com/Dan6040/SharpDX-Rastertek-Tutorials
which is based on:
https://www.rastertek.com/tutindex.html
Also, I have implemented in my own way the inverse kinematics using the page here for the upper part of the virtual reality 1st person "controller":
https://mathworld.wolfram.com/Circle-CircleIntersection.html
Website for the jitter physics engine:
https://code.google.com/archive/p/jitterphysics/
Website for the windows input simulator:
https://archive.codeplex.com/?p=inputsimulator
Website for the Ab3d.OculusWrap
https://github.com/ab4d/Ab3d.OculusWrapiso


You can currently support my work on Patreon here: https://www.patreon.com/join/SteveChasse/checkout?ru=undefined 

and also donate here https://www.paypal.com/donate?business=LKL9AGGDNNDN2&no_recurring=0&item_name=development+of+my+patented+Virtual+Reality+Virtual+Desktop+Voxel+instancing&currency_code=CAD

Or use the same donation link as above but with a QRCode here:

![QR Code](https://user-images.githubusercontent.com/31090600/136681210-a6031168-3de9-46c4-b899-de57e339c023.png)

My satoshi wallet BTC deposit address:
3FkKAeynsgQWhREbkeLcNU2txHkKqcTDkP

![Warning2020-2 fw](https://user-images.githubusercontent.com/31090600/136682673-83b3d969-7e62-497e-8d20-53e07c1b10b4.jpg)

