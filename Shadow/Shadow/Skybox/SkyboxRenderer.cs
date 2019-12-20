using GpuGraphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextureLoader;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Shadow.Skybox
{
    public class SkyboxRenderer
    {
        VBOArrayF attributes;
        VAO buffer;
        SkyboxShader shader;
        CubemapTexture texture;
        bool postContructor;

        public void Render(Matrix4 viewMatrix, Matrix4 projectionMatrix)
        {
            PostConstructor();

            texture.bindCubemapTexture(TextureUnit.Texture0);
            shader.startProgram();
            shader.setUniforms(0, viewMatrix, projectionMatrix);
            VAOManager.renderBuffers(buffer, PrimitiveType.Triangles);
            shader.stopProgram();
        }

        private void PostConstructor()
        {
            if (postContructor)
            {
                buffer = new VAO(attributes);
                VAOManager.genVAO(buffer);
                VAOManager.setBufferData(BufferTarget.ArrayBuffer, buffer);
                shader = new SkyboxShader(Folder.Path + @"\shaders\skyboxVS.glsl", Folder.Path + @"\shaders\skyboxFS.glsl");
                postContructor = false;
            }
        }

        public SkyboxRenderer(VBOArrayF attributes)
        {
            this.attributes = attributes;
            this.texture = TextureLoader.GetSkyboxTexture();
            postContructor = true;
        }
        

    }
}
