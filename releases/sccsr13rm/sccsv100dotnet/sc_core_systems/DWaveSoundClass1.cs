﻿using SharpDX;
using SharpDX.DirectSound;
using SharpDX.Multimedia;
using System;
using System.IO;

namespace _sc_core_systems.sound
{
    public class DWaveSound : DSound                    // 131 lines
    {
        // Variables
        private string chunkId;
        private int chunkSize;
        private string format;
        private string subChunkId;
        private int subChunkSize;
        private WaveFormatEncoding audioFormat;
        private short numChannels;
        private int sampleRate;
        private int bytesPerSecond;
        private short blockAlign;
        private short bitsPerSample;
        private string dataChunkId;
        private int dataSize;

        // Properties
        public SecondarySoundBuffer _SecondarySoundBuffer { get; set; }

        // Constructor
        public DWaveSound(string fileName) : base(fileName)
        { }

        // Virtual Methods
        protected override bool LoadAudioFile(string audioFile, DirectSound directSound)
        {
            try
            {
                // Open the wave file in binary. 
                BinaryReader reader = new BinaryReader(File.OpenRead(audioFile)); //DSystemConfiguration.DataFilePath +

                // Read in the wave file header.
                chunkId = new string(reader.ReadChars(4));
                chunkSize = reader.ReadInt32();
                format = new string(reader.ReadChars(4));
                subChunkId = new string(reader.ReadChars(4));
                subChunkSize = reader.ReadInt32();
                audioFormat = (WaveFormatEncoding)reader.ReadInt16();
                numChannels = reader.ReadInt16();
                sampleRate = reader.ReadInt32();
                bytesPerSecond = reader.ReadInt32();
                blockAlign = reader.ReadInt16();
                bitsPerSample = reader.ReadInt16();
                dataChunkId = new string(reader.ReadChars(4));
                dataSize = reader.ReadInt32();

                // Check that the chunk ID is the RIFF format
                // and the file format is the WAVE format
                // and sub chunk ID is the fmt format
                // and the audio format is PCM
                // and the wave file was recorded in stereo format
                // and at a sample rate of 44.1 KHz
                // and at 16 bit format
                // and there is the data chunk header.
                // Otherwise return false.
                if (chunkId != "RIFF" || format != "WAVE" || subChunkId.Trim() != "fmt" || audioFormat != WaveFormatEncoding.Pcm || numChannels != 2 || sampleRate != 44100 || bitsPerSample != 16 || dataChunkId != "data")
                    return false;

                // Set the buffer description of the secondary sound buffer that the wave file will be loaded onto and the wave format.
                SoundBufferDescription secondaryBufferDesc = new SoundBufferDescription()
                {
                    Flags = BufferFlags.ControlVolume,
                    BufferBytes = dataSize,
                    Format = new WaveFormat(44100, 16, 2),
                    AlgorithmFor3D = Guid.Empty
                };

                // Create a temporary sound buffer with the specific buffer settings.
                _SecondarySoundBuffer = new SecondarySoundBuffer(directSound, secondaryBufferDesc);

                // Read in the wave file data into the temporary buffer.
                byte[] waveData = reader.ReadBytes(dataSize);

                // Close the reader
                reader.Close();

                // Lock the secondary buffer to write wave data into it.
                DataStream waveBufferData2;
                DataStream waveBufferData1 = _SecondarySoundBuffer.Lock(0, dataSize, LockFlags.None, out waveBufferData2);

                // Copy the wave data into the buffer.
                waveBufferData1.Write(waveData, 0, dataSize);

                // Unlock the secondary buffer after the data has been written to it.
                // var result = _SecondaryBuffer.Unlock(waveBufferData1, waveBufferData2);
                _SecondarySoundBuffer.Unlock(waveBufferData1, waveBufferData2);
            }
            catch
            {
                return false;
            }

            return true;
        }
        protected override bool PlayAudioFile(int volume)
        {
            try
            {
                // Set the position at the beginning of the sound buffer.
                _SecondarySoundBuffer.CurrentPosition = 0;

                // Set volume of the buffer to 100%
                _SecondarySoundBuffer.Volume = volume;

                // Play the contents of the secondary sound buffer with a continuous loop.
                _SecondarySoundBuffer.Play(0, PlayFlags.Looping);
            }
            catch
            {
                return false;
            }

            return true;
        }
        protected override void ShutdownAudioFile()
        {
            _SecondarySoundBuffer?.Dispose();
            _SecondarySoundBuffer = null;
        }
    }
}