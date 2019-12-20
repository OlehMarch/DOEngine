using OpenTK.Graphics.OpenGL;
using GpuGraphics;
using Programmable_PipelineLight;
using TextureLoader;
using OpenTK;
using Shadow.Shadow;
using Shadow.Api.Collector;

namespace Shadow
{
    public class RenderEntity
    {
        public RawModel model;
        private EntityShader shader;
        private Texture2D texture;
        private bool _postConstructor = true;

        public Matrix4 ModelMatrix { set; get; }

        public void Render(Camera camera, Matrix4 projectionMatrix, DirectionalLight light, ShadowRenderer shadow)
        {
            postConstructor();

            var shadowMatrix = GetShadowMatrix(shadow.OffsetMatrix, shadow.ProjectionMatrix, shadow.ViewMatrix, ModelMatrix);
            
            shader.startProgram();
            texture.bindTexture2D(TextureUnit.Texture0, texture.TextureID[0]);
            texture.bindTexture2D(TextureUnit.Texture1, texture.TextureID[1]);
            texture.bindTexture2D(TextureUnit.Texture2, shadow.Framebuffer.DepthTexture.TextureID[0]);
            shader.setUniforms(0, 1, ModelMatrix, camera.ViewMatrix, projectionMatrix, light, camera.Position, false, shadowMatrix, 2);
            VAOManager.renderBuffers(model.Buffer, PrimitiveType.Triangles);
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
                shader = new EntityShader(Folder.Path + @"/shaders/entityVS.glsl",
                   Folder.Path + @"/shaders/entityFS.glsl");
                _postConstructor = !_postConstructor;
            }
        }

        public RenderEntity(string filePath, Texture2D textures, Vector3 translation, Vector3 scale)
        {
            model = new RawModel(filePath);
            this.texture = textures;
            ModelMatrix = Matrix4.Identity;
            ModelMatrix *= Matrix4.CreateScale(scale);
            ModelMatrix *= Matrix4.CreateTranslation(translation);
        }

        public void CleanUp()
        {
            model.Dispose();
            shader.cleanUp();
            texture.cleanUp();
        }
    }
}
