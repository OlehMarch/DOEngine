using GpuGraphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics.OpenGL;
using Programmable_PipelineLight;
using TextureLoader;
using Shadow.Shadow;

namespace Shadow
{
    public class Floor
    {
        public VBOArrayF attributes;
        public VAO buffer;
        public EntityShader shader;
        private bool _postConstructor = true;
        private Texture2D texture;
        public Matrix4 ModelMatrix { set; get; }

        public void Render(Camera camera, Matrix4 projectionMatrix, DirectionalLight light, ShadowRenderer shadow)
        {
            postConstructor();

            var shadowMatrix = GetShadowMatrix(shadow.OffsetMatrix, shadow.ProjectionMatrix, shadow.ViewMatrix, ModelMatrix);

            texture.bindTexture2D(TextureUnit.Texture0, texture.TextureID[0]);
            texture.bindTexture2D(TextureUnit.Texture1, texture.TextureID[1]);
            texture.bindTexture2D(TextureUnit.Texture2, shadow.Framebuffer.DepthTexture.TextureID[0]);

            shader.startProgram();
            shader.setUniforms(0, 1, ModelMatrix, camera.ViewMatrix, projectionMatrix,
                new DirectionalLight(light.Direction, light.Ambient, light.Diffuse, light.Specular), camera.Position, true, shadowMatrix, 2);
            VAOManager.renderBuffers(buffer, PrimitiveType.Triangles);
            shader.stopProgram();
        }

        private Matrix4 GetShadowMatrix(Matrix4 offsetMatrix, Matrix4 projectionMatrix, Matrix4 viewMatrix, Matrix4 modelMatrix)
        {
            Matrix4 result = modelMatrix;
            result *= viewMatrix;
            result *= projectionMatrix;
            result *= offsetMatrix;
            return result;
        }

        private void postConstructor()
        {
            if (_postConstructor)
            {
                VAOManager.genVAO(buffer);
                VAOManager.setBufferData(BufferTarget.ArrayBuffer, buffer);
                shader = new EntityShader(Folder.Path + @"/shaders/entityVS.glsl",
                    Folder.Path + @"/shaders/entityFS.glsl");
                texture = TextureLoaderClass.GetCubeTexture();
                ModelMatrix = Matrix4.Identity;
                ModelMatrix *= Matrix4.CreateScale(20);
                _postConstructor = !_postConstructor;
            }
        }

        public Floor()
        {
            attributes = new VBOArrayF(new float[,] { { -1, 0, 1 }, { -1, 0, -1 }, { 1, 0, -1 }, { 1, 0, -1 }, { 1, 0, 1 }, { -1, 0, 1 } },
               new float[,] { { 0, 1, 0 }, { 0, 1, 0 }, { 0, 1, 0 }, { 0, 1, 0 }, { 0, 1, 0 }, { 0, 1, 0 } },
               new float[,] { { 0, 0 }, { 0, 1 }, { 1, 1 }, { 1, 1 }, { 1, 0 }, { 0, 0 } }, true);
            buffer = new VAO(attributes);
        }
    }
}
