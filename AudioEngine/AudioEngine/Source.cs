using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Audio;
using OpenTK.Audio.OpenAL;

namespace AudioEngine
{
    public class Source
    {
        #region Constructors of Source
        public Source()
        {
            _sourceID = AL.GenSource();
        }

        public Source(int buffer)
        {
            _sourceID = AL.GenSource();
            AL.Source(_sourceID, ALSourcei.Buffer, buffer);
        }

        public Source(int buffer, float volume, float pitch)
        {
            _sourceID = AL.GenSource();
            AL.Source(_sourceID, ALSourcei.Buffer, buffer);
            AL.Source(_sourceID, ALSourcef.Gain, volume);
            AL.Source(_sourceID, ALSourcef.Pitch, pitch);
        }

        public Source(int buffer, float xPosition, float yPosition, float zPosition, 
            float xVelocity, float yVelocity, float zVelocity)
        {
            _sourceID = AL.GenSource();
            AL.Source(_sourceID, ALSourcei.Buffer, buffer);
            AL.Source(_sourceID, ALSource3f.Position, xPosition, yPosition, zPosition);
            AL.Source(_sourceID, ALSource3f.Velocity, xVelocity, yVelocity, zVelocity);
        }

        public Source(int buffer, Vector3 position, Vector3 velocity)
        {
            _sourceID = AL.GenSource();
            AL.Source(_sourceID, ALSourcei.Buffer, buffer);
            AL.Source(_sourceID, ALSource3f.Position, position.X, position.Y, position.Z);
            AL.Source(_sourceID, ALSource3f.Velocity, velocity.X, velocity.Y, velocity.Z);
        }

        public Source(int buffer, float rolloffFactor, float referenceDistance, float maxDistance)
        {
            _sourceID = AL.GenSource();
            AL.Source(_sourceID, ALSourcei.Buffer, buffer);
            AL.Source(_sourceID, ALSourcef.RolloffFactor, rolloffFactor);
            AL.Source(_sourceID, ALSourcef.ReferenceDistance, referenceDistance);
            AL.Source(_sourceID, ALSourcef.MaxDistance, maxDistance);
        }

        public Source(int buffer, float volume, float pitch, float xPosition, float yPosition, 
            float zPosition, float xVelocity, float yVelocity, float zVelocity)
        {
            _sourceID = AL.GenSource();
            AL.Source(_sourceID, ALSourcei.Buffer, buffer);
            AL.Source(_sourceID, ALSourcef.Gain, volume);
            AL.Source(_sourceID, ALSourcef.Pitch, pitch);
            AL.Source(_sourceID, ALSource3f.Position, xPosition, yPosition, zPosition);
            AL.Source(_sourceID, ALSource3f.Velocity, xVelocity, yVelocity, zVelocity);
        }

        public Source(int buffer, float volume, float pitch, Vector3 position, Vector3 velocity)
        {
            _sourceID = AL.GenSource();
            AL.Source(_sourceID, ALSourcei.Buffer, buffer);
            AL.Source(_sourceID, ALSourcef.Gain, volume);
            AL.Source(_sourceID, ALSourcef.Pitch, pitch);
            AL.Source(_sourceID, ALSource3f.Position, position.X, position.Y, position.Z);
            AL.Source(_sourceID, ALSource3f.Velocity, velocity.X, velocity.Y, velocity.Z);
        }

        public Source(int buffer, float volume, float pitch, float xPosition, float yPosition,
            float zPosition, float xVelocity, float yVelocity, float zVelocity, float rolloffFactor, 
            float referenceDistance, float maxDistance)
        {
            _sourceID = AL.GenSource();
            AL.Source(_sourceID, ALSourcei.Buffer, buffer);
            AL.Source(_sourceID, ALSourcef.Gain, volume);
            AL.Source(_sourceID, ALSourcef.Pitch, pitch);
            AL.Source(_sourceID, ALSource3f.Position, xPosition, yPosition, zPosition);
            AL.Source(_sourceID, ALSource3f.Velocity, xVelocity, yVelocity, zVelocity);
            AL.Source(_sourceID, ALSourcef.RolloffFactor, rolloffFactor);
            AL.Source(_sourceID, ALSourcef.ReferenceDistance, referenceDistance);
            AL.Source(_sourceID, ALSourcef.MaxDistance, maxDistance);
        }

        public Source(int buffer, float volume, float pitch, Vector3 position, Vector3 velocity, 
            float rolloffFactor, float referenceDistance, float maxDistance)
        {
            _sourceID = AL.GenSource();
            AL.Source(_sourceID, ALSourcei.Buffer, buffer);
            AL.Source(_sourceID, ALSourcef.Gain, volume);
            AL.Source(_sourceID, ALSourcef.Pitch, pitch);
            AL.Source(_sourceID, ALSource3f.Position, position.X, position.Y, position.Z);
            AL.Source(_sourceID, ALSource3f.Velocity, velocity.X, velocity.Y, velocity.Z);
            AL.Source(_sourceID, ALSourcef.RolloffFactor, rolloffFactor);
            AL.Source(_sourceID, ALSourcef.ReferenceDistance, referenceDistance);
            AL.Source(_sourceID, ALSourcef.MaxDistance, maxDistance);
        }
        #endregion


        private int _sourceID;

        #region Init and Delete operations of Source
        public void Init(int buffer)
        {
            if (IsPlaying())
                Stop();

            AL.Source(_sourceID, ALSourcei.Buffer, buffer);
        }

        public void Delete()
        {
            if (IsPlaying())
                Stop();

            AL.DeleteSource(_sourceID);
        }
        #endregion

        #region Multimedia operations of Source
        public void Pause()
        {
            AL.SourcePause(_sourceID);
        }

        public void Play()
        {
            AL.SourcePlay(_sourceID);
        }

        public void Stop()
        {
            AL.SourceStop(_sourceID);
        }
        #endregion

        #region Volume and Pitch settings of Source
        public void SetVolume(float volume)
        {
            AL.Source(_sourceID, ALSourcef.Gain, volume);
        }

        public void SetPitch(float pitch)
        {
            AL.Source(_sourceID, ALSourcef.Pitch, pitch);
        }

        public void SetSourceSettings(float volume, float pitch)
        {
            AL.Source(_sourceID, ALSourcef.Gain, volume);
            AL.Source(_sourceID, ALSourcef.Pitch, pitch);
        }
        #endregion

        #region Position and Velocity settings of Source
        public void SetPosition(float x, float y, float z)
        {
            AL.Source(_sourceID, ALSource3f.Position, x, y, z);
        }

        public void SetPosition(Vector3 position)
        {
            AL.Source(_sourceID, ALSource3f.Position, position.X, position.Y, position.Z);
        }

        public void SetVelocity(float x, float y, float z)
        {
            AL.Source(_sourceID, ALSource3f.Velocity, x, y, z);
        }

        public void SetVelocity(Vector3 velocity)
        {
            AL.Source(_sourceID, ALSource3f.Velocity, velocity.X, velocity.Y, velocity.Z);
        }

        public void SetSourceSettings(float xPosition, float yPosition, float zPosition, float xVelocity, float yVelocity, float zVelocity)
        {
            AL.Source(_sourceID, ALSource3f.Position, xPosition, yPosition, zPosition);
            AL.Source(_sourceID, ALSource3f.Velocity, xVelocity, yVelocity, zVelocity);
        }

        public void SetSourceSettings(Vector3 position, Vector3 velocity)
        {
            AL.Source(_sourceID, ALSource3f.Position, position.X, position.Y, position.Z);
            AL.Source(_sourceID, ALSource3f.Velocity, velocity.X, velocity.Y, velocity.Z);
        }
        #endregion

        #region Distance Attenuation settings of Source
        public void SetRolloffFactor(float rolloffFactor)
        {
            AL.Source(_sourceID, ALSourcef.RolloffFactor, rolloffFactor);
        }

        public void SetReferenceDistance(float referenceDistance)
        {
            AL.Source(_sourceID, ALSourcef.ReferenceDistance, referenceDistance);
        }

        public void SetMaxDistance(float maxDistance)
        {
            AL.Source(_sourceID, ALSourcef.MaxDistance, maxDistance);
        }

        public void SetSourceSettings(float rolloffFactor, float referenceDistance, float maxDistance)
        {
            AL.Source(_sourceID, ALSourcef.RolloffFactor, rolloffFactor);
            AL.Source(_sourceID, ALSourcef.ReferenceDistance, referenceDistance);
            AL.Source(_sourceID, ALSourcef.MaxDistance, maxDistance);
        }
        #endregion

        public void SetBuffer(int buffer)
        {
            AL.Source(_sourceID, ALSourcei.Buffer, buffer);
        }

        public void SetLooping(bool loop)
        {
            AL.Source(_sourceID, ALSourceb.Looping, loop ? true : false);
        }

        public bool IsPlaying()
        {
            return AL.GetSourceState(_sourceID) == ALSourceState.Playing;
        }

    }
}
