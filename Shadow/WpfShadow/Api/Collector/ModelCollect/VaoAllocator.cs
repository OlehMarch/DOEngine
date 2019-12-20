using CParser.OBJ_Parser;
using GpuGraphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace Shadow.Api.Collector.ModelCollect
{
    public static class VaoAllocator
    {
        public static VAO LoadModelFromFile(string modelPath)
        {
            VAO resultBuffer = null;
            VBOArrayF vbos = LoadVBO(modelPath);
            resultBuffer = LoadVAO(vbos);
            return resultBuffer;
        }

        private static VAO LoadVAO(VBOArrayF vbo)
        {
            VAO modelBuffer = new VAO(vbo);
            VAOManager.genVAO(modelBuffer);
            VAOManager.setBufferData(BufferTarget.ArrayBuffer, modelBuffer);
            return modelBuffer;
        }

        private static VBOArrayF LoadVBO(string modelPath)
        {
            OBJ_ModelLoaderEx model = new OBJ_ModelLoaderEx(true, modelPath);
            return new VBOArrayF(model.Verts, model.N_Verts, model.T_Verts, true);
        }

        public static void ReleaseVAO(VAO modelBuffer)
        {
            VAOManager.cleanUp(modelBuffer);
        }
    }
}
