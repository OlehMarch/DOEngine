using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace GpuGraphics
{
    public class VAO
    {
        private uint[] _vboID;
        private uint[] _iboID;
        private uint[] _vaoID;
        private BufferTarget[] _buffersType;
        private BufferUsageHint[] _buffersUsage;
        private VBOArrayF _vaoData;
        private bool _maped;
        private bool _dataInserted;

        public void mapBuffer()
        {
            _maped = true;
        }
        public void dataInserted()
        {
            _dataInserted = true;
        }
        public bool isMapped()
        {
            return _maped;
        }
        public bool isDataInserted()
        {
            return _dataInserted;
        }

        public uint[] Vbo { get { return this._vboID; } }
        public uint[] Vao { get { return this._vaoID; } }
        public uint[] Ibo { get { return this._iboID; } }
        public BufferTarget getBufferTypeAt(uint bufferIndex)
        {
            return _buffersType[bufferIndex];
        }
        public BufferUsageHint getBufferUsageAt(uint bufferIndex)
        {
            return _buffersUsage[bufferIndex];
        }
        public int getBuffersCount()
        {
            return _vboID.Length;
        }
        public VBOArrayF getBufferData()
        {
            return this._vaoData;
        }

        public void setBufferTypeAt(uint bufferIndex, BufferTarget bufferType)
        {
            if (_buffersType.Length > bufferIndex)
            {
                _buffersType[bufferIndex] = bufferType;
            }
        }
        public void setBufferUsageAt(uint bufferIndex, BufferUsageHint bufferUsage)
        {
            if (_buffersUsage.Length > bufferIndex)
            {
                _buffersUsage[bufferIndex] = bufferUsage;
            }
        }

        public void changeBufferData(VBOArrayF bufferData)
        {
            _vaoData = bufferData;
        }

        public VAO(VBOArrayF bufferData)
        {
            _maped = false;
            _dataInserted = false;
            this._vaoData = bufferData;
            this._vboID = new uint[bufferData.getActiveAttribsCount()];
            this._vaoID = new uint[1];
            this._iboID = new uint[1];
        }
    }
    
}
