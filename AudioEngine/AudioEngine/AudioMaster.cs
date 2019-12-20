using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Audio;
using OpenTK.Audio.OpenAL;

using CParser.WAV_Parser;

namespace AudioEngine
{
    public class AudioMaster
    {
        private static List<int> _buffers = new List<int>();
        private static AudioContext _context;

        #region Init of AudioContext
        public static void Init()
        {
            _context = new AudioContext();
        }

        public static void Init(ALDistanceModel distModel)
        {
            _context = new AudioContext();
            AL.DistanceModel(distModel);
        }
        #endregion

        #region Listener Data settings
        public static void SetListenerData(float x, float y, float z)
        {
            AL.Listener(ALListener3f.Position, x, y, z);
            AL.Listener(ALListener3f.Velocity, 0, 0, 0);
        }

        public static void SetListenerData(Vector3 position)
        {
            AL.Listener(ALListener3f.Position, position.X, position.Y, position.Z);
            AL.Listener(ALListener3f.Velocity, 0, 0, 0);
        }
        #endregion

        #region Sound loading
        public static int LoadSound(string file)
        {
            int buffer = AL.GenBuffer();
            _buffers.Add(buffer);
            WaveData waveData = new WaveData(file);
            AL.BufferData(buffer, waveData.SoundFormat, waveData.SoundData, waveData.SoundData.Length, waveData.SampleRate);
            waveData.Dispose();
            return buffer;
        }

        public static int[] LoadSound(string[] files)
        {
            int index = 0;
            int[] buffers = new int[files.Length];
            foreach (string file in files)
            {
                int buffer = AL.GenBuffer();
                buffers[index++] = buffer;
                _buffers.Add(buffer);
                WaveData waveData = new WaveData(file);
                AL.BufferData(buffer, waveData.SoundFormat, waveData.SoundData, waveData.SoundData.Length, waveData.SampleRate);
                waveData.Dispose();
            }
            return buffers;
        }
        #endregion

        public static void CleanUp()
        {
            foreach (int buffer in _buffers)
            {
                AL.DeleteBuffer(buffer);
            }
            _context.Dispose();
        }
    }
}
