using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;

//https://www.ok.unsode.com/post/2015/06/23/C-Fast-Memory-Copy-Method-with-x86-Assembly-Usage

namespace SC_skYaRk_VR_V007
{
    internal static class FastMemCopy
    {

        [Flags]
        private enum AllocationTypes : uint
        {
            Commit = 0x1000, Reserve = 0x2000,
            Reset = 0x80000, LargePages = 0x20000000,
            Physical = 0x400000, TopDown = 0x100000,
            WriteWatch = 0x200000
        }
        [Flags]

        private enum MemoryProtections : uint
        {
            Execute = 0x10, ExecuteRead = 0x20,
            ExecuteReadWrite = 0x40, ExecuteWriteCopy = 0x80,
            NoAccess = 0x01, ReadOnly = 0x02,
            ReadWrite = 0x04, WriteCopy = 0x08,
            GuartModifierflag = 0x100, NoCacheModifierflag = 0x200,
            WriteCombineModifierflag = 0x400
        }


        [Flags]
        private enum FreeTypes : uint
        {
            Decommit = 0x4000, Release = 0x8000
        }


        [UnmanagedFunctionPointerAttribute(CallingConvention.Cdecl)]

        private unsafe delegate void FastMemCopyDelegate();

        private static class NativeMethods
        {
            [DllImport("kernel32.dll", SetLastError = true)]
            internal static extern IntPtr VirtualAlloc(
                IntPtr lpAddress,
                UIntPtr dwSize,
                AllocationTypes flAllocationType,
                MemoryProtections flProtect);

            [DllImport("kernel32")]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool VirtualFree(
                IntPtr lpAddress,
                uint dwSize,
                FreeTypes flFreeType);
        }


        public static unsafe void FastMemoryCopy(IntPtr src, IntPtr dst, int nBytes)
        {
            if (IntPtr.Size == 4)
            {
                //we are in 32 bit mode
                //allocate memory for our asm method
                IntPtr p = NativeMethods.VirtualAlloc(IntPtr.Zero, new UIntPtr((uint)x86_FastMemCopy_New.Length), AllocationTypes.Commit | AllocationTypes.Reserve, MemoryProtections.ExecuteReadWrite);

                try
                {
                    //copy our method bytes to allocated memory
                    Marshal.Copy(x86_FastMemCopy_New, 0, p, x86_FastMemCopy_New.Length);
                    //make a delegate to our method
                    FastMemCopyDelegate _fastmemcopy = (FastMemCopyDelegate)Marshal.GetDelegateForFunctionPointer(p, typeof(FastMemCopyDelegate));
                    //offset to the end of our method block
                    p += x86_FastMemCopy_New.Length;
                    //store length param
                    p -= 8;
                    Marshal.Copy(BitConverter.GetBytes((long)nBytes), 0, p, 4);

                    //store destination address param
                    p -= 8;
                    Marshal.Copy(BitConverter.GetBytes((long)dst), 0, p, 4);
                    //store source address param
                    p -= 8;
                    Marshal.Copy(BitConverter.GetBytes((long)src), 0, p, 4);
                    //Start stopwatch
                    //Stopwatch sw = new Stopwatch();
                    //sw.Start();
                    //copy-paste all data 10 times
                    _fastmemcopy();

                    //for (int i = 0; i < 10; i++)
                    //    _fastmemcopy();
                    //stop stopwatch
                    //sw.Stop();

                    //get message with measured time
                    //System.Windows.Forms.MessageBox.Show(sw.ElapsedTicks.ToString());

                }

                catch (Exception ex)
                {
                    //if any exception
                    //System.Windows.Forms.MessageBox.Show(ex.Message);
                }

                finally
                {
                    //free allocated memory
                    NativeMethods.VirtualFree(p, (uint)(x86_FastMemCopy_New.Length), FreeTypes.Release);
                    GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
                }
            }
            else if (IntPtr.Size == 8)
            {
                throw new ApplicationException("x64 is not supported yet!");
            }

        }


        private static byte[] x86_FastMemCopy_New = new byte[]
        {

            0x90, 				//nop do nothing
            0x60, 				//pushad store flag register on stack
            0x95, 				//xchg ebp, eax eax contains memory address of our method
            0x8B, 0xB5, 0x5A, 0x01, 0x00, 0x00, //mov esi,[ebp][00000015A] get source buffer address
            0x89, 0xF0, 			//mov eax,esi
            0x83, 0xE0, 0x0F, 			//and eax,00F will check if it is 16 byte aligned
            0x8B, 0xBD, 0x62, 0x01, 0x00, 0x00, //mov edi,[ebp][000000162] get destination address
            0x89, 0xFB, 			//mov ebx,edi
            0x83, 0xE3, 0x0F, 			//and ebx,00F will check if it is 16 byte aligned
            0x8B, 0x8D, 0x6A, 0x01, 0x00, 0x00, //mov ecx,[ebp][00000016A] get number of bytes to copy
            0xC1, 0xE9, 0x07, 			//shr ecx,7 divide length by 128
            0x85, 0xC9, 			//test ecx,ecx check if zero
            0x0F, 0x84, 0x1C, 0x01, 0x00, 0x00, //jz 000000146 ? copy the rest
            0x0F, 0x18, 0x06, 			//prefetchnta [esi] pre-fetch non-temporal source data for reading
            0x85, 0xC0, 			//test eax,eax check if source address is 16 byte aligned
            0x0F, 0x84, 0x8B, 0x00, 0x00, 0x00, //jz 0000000C0 ? go to copy if aligned
            0x0F, 0x18, 0x86, 0x80, 0x02, 0x00, 0x00, //prefetchnta [esi][000000280] pre-fetch more source data
            0x0F, 0x10, 0x06, 			//movups xmm0,[esi] copy 16 bytes of source data
            0x0F, 0x10, 0x4E, 0x10, 		//movups xmm1,[esi][010] copy more 16 bytes
            0x0F, 0x10, 0x56, 0x20, 		//movups xmm2,[esi][020] copy more
            0x0F, 0x18, 0x86, 0xC0, 0x02, 0x00, 0x00, //prefetchnta [esi][0000002C0] pre-fetch more
            0x0F, 0x10, 0x5E, 0x30, 		//movups xmm3,[esi][030]
            0x0F, 0x10, 0x66, 0x40, 		//movups xmm4,[esi][040]
            0x0F, 0x10, 0x6E, 0x50, 		//movups xmm5,[esi][050]
            0x0F, 0x10, 0x76, 0x60, 		//movups xmm6,[esi][060]
            0x0F, 0x10, 0x7E, 0x70, 		//movups xmm7,[esi][070] we've copied 128 bytes of source data
            0x85, 0xDB, 			//test ebx,ebx check if destination address is 16 byte aligned
            0x74, 0x21, 			//jz 000000087 ? go to past if aligned
            0x0F, 0x11, 0x07, 		//movups [edi],xmm0 past first 16 bytes to non-aligned destination address
            0x0F, 0x11, 0x4F, 0x10, 		//movups [edi][010],xmm1 past more
            0x0F, 0x11, 0x57, 0x20, 		//movups [edi][020],xmm2
            0x0F, 0x11, 0x5F, 0x30, 		//movups [edi][030],xmm3
            0x0F, 0x11, 0x67, 0x40, 		//movups [edi][040],xmm4
            0x0F, 0x11, 0x6F, 0x50, 		//movups [edi][050],xmm5
            0x0F, 0x11, 0x77, 0x60, 		//movups [edi][060],xmm6
            0x0F, 0x11, 0x7F, 0x70, 		//movups [edi][070],xmm7 we've pasted 128 bytes of source data
            0xEB, 0x1F, 			//jmps 0000000A6 ? continue
            0x0F, 0x2B, 0x07, 		//movntps [edi],xmm0 past first 16 bytes to aligned destination address
            0x0F, 0x2B, 0x4F, 0x10, 		//movntps [edi][010],xmm1 past more
            0x0F, 0x2B, 0x57, 0x20, 		//movntps [edi][020],xmm2
            0x0F, 0x2B, 0x5F, 0x30, 		//movntps [edi][030],xmm3
            0x0F, 0x2B, 0x67, 0x40, 		//movntps [edi][040],xmm4
            0x0F, 0x2B, 0x6F, 0x50, 		//movntps [edi][050],xmm5
            0x0F, 0x2B, 0x77, 0x60, 		//movntps [edi][060],xmm6
            0x0F, 0x2B, 0x7F, 0x70, 		//movntps [edi][070],xmm7 we've pasted 128 bytes of source data
            0x81, 0xC6, 0x80, 0x00, 0x00, 0x00, //add esi,000000080 increment source address by 128
            0x81, 0xC7, 0x80, 0x00, 0x00, 0x00, //add edi,000000080 increment destination address by 128
            0x83, 0xE9, 0x01, 			//sub ecx,1 decrement counter
            0x0F, 0x85, 0x7A, 0xFF, 0xFF, 0xFF, //jnz 000000035 ? continue if not zero
            0xE9, 0x86, 0x00, 0x00, 0x00, 	//jmp 000000146 ? go to copy the rest of data
            0x0F, 0x18, 0x86, 0x80, 0x02, 0x00, 0x00, //prefetchnta [esi][000000280] pre-fetch source data
            0x0F, 0x28, 0x06, 			//movaps xmm0,[esi] copy 128 bytes from aligned source address
            0x0F, 0x28, 0x4E, 0x10, 		//movaps xmm1,[esi][010] copy more
            0x0F, 0x28, 0x56, 0x20, 		//movaps xmm2,[esi][020]
            0x0F, 0x18, 0x86, 0xC0, 0x02, 0x00, 0x00, //prefetchnta [esi][0000002C0] pre-fetch more data
            0x0F, 0x28, 0x5E, 0x30, 		//movaps xmm3,[esi][030]
            0x0F, 0x28, 0x66, 0x40, 		//movaps xmm4,[esi][040]
            0x0F, 0x28, 0x6E, 0x50, 		//movaps xmm5,[esi][050]
            0x0F, 0x28, 0x76, 0x60, 		//movaps xmm6,[esi][060]
            0x0F, 0x28, 0x7E, 0x70, 		//movaps xmm7,[esi][070] we've copied 128 bytes of source data
            0x85, 0xDB, 			//test ebx,ebx check if destination address is 16 byte aligned
            0x74, 0x21, 			//jz 000000112 ? go to past if aligned
            0x0F, 0x11, 0x07, 		//movups [edi],xmm0 past 16 bytes to non-aligned destination address
            0x0F, 0x11, 0x4F, 0x10, 		//movups [edi][010],xmm1 past more
            0x0F, 0x11, 0x57, 0x20, 		//movups [edi][020],xmm2
            0x0F, 0x11, 0x5F, 0x30, 		//movups [edi][030],xmm3
            0x0F, 0x11, 0x67, 0x40, 		//movups [edi][040],xmm4
            0x0F, 0x11, 0x6F, 0x50, 		//movups [edi][050],xmm5
            0x0F, 0x11, 0x77, 0x60, 		//movups [edi][060],xmm6
            0x0F, 0x11, 0x7F, 0x70, 		//movups [edi][070],xmm7 we've pasted 128 bytes of data
            0xEB, 0x1F, 			//jmps 000000131 ? continue copy-past
            0x0F, 0x2B, 0x07, 			//movntps [edi],xmm0 past 16 bytes to aligned destination address
            0x0F, 0x2B, 0x4F, 0x10, 		//movntps [edi][010],xmm1 past more
            0x0F, 0x2B, 0x57, 0x20, 		//movntps [edi][020],xmm2
            0x0F, 0x2B, 0x5F, 0x30, 		//movntps [edi][030],xmm3
            0x0F, 0x2B, 0x67, 0x40, 		//movntps [edi][040],xmm4
            0x0F, 0x2B, 0x6F, 0x50, 		//movntps [edi][050],xmm5
            0x0F, 0x2B, 0x77, 0x60, 		//movntps [edi][060],xmm6
            0x0F, 0x2B, 0x7F, 0x70, 		//movntps [edi][070],xmm7 we've pasted 128 bytes of data
            0x81, 0xC6, 0x80, 0x00, 0x00, 0x00, //add esi,000000080 increment source address by 128
            0x81, 0xC7, 0x80, 0x00, 0x00, 0x00, //add edi,000000080 increment destination address by 128
            0x83, 0xE9, 0x01, 			//sub ecx,1 decrement counter
            0x0F, 0x85, 0x7A, 0xFF, 0xFF, 0xFF, //jnz 0000000C0 ? continue copy-past if non-zero
            0x8B, 0x8D, 0x6A, 0x01, 0x00, 0x00, //mov ecx,[ebp][00000016A] get number of bytes to copy
            0x83, 0xE1, 0x7F, 			//and ecx,07F get rest number of bytes
            0x85, 0xC9, 			//test ecx,ecx check if there are bytes
            0x74, 0x02, 			//jz 000000155 ? exit if there are no more bytes
            0xF3, 0xA4, 			//rep movsb copy rest of bytes
            0x0F, 0xAE, 0xF8, //sfence performs a serializing operation on all store-to-memory instructions
            0x61, 				//popad restore flag register
            0xC3, 				//retn return from our method to C#
            0x00, 0x00, 0x00, 0x00, //source buffer address
            0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, //destination buffer address
            0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, //number of bytes to copy-past
            0x00, 0x00, 0x00, 0x00
        };
    }
}
