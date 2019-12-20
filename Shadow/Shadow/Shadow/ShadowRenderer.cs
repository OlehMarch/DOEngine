using OpenTK;
using Programmable_PipelineLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GpuGraphics;

using OpenTK.Graphics.OpenGL;

namespace Shadow.Shadow
{
    public class ShadowRenderer
    {
        public ShadowFramebuffer Framebuffer { private set; get; }
        private ShadowShader shader;
        private Matrix4 viewMatrix, projectionMatrix;
        public Matrix4 ProjectionMatrix { get { return projectionMatrix; } }
        public Matrix4 ViewMatrix { get { return viewMatrix; } }
        private DirectionalLight light;
        bool postConstructor = true;

        public Matrix4 OffsetMatrix { set; get; }

        public const int SHADOW_DISTANCE = 10;


        public void BuildShadows(IEnumerable<object> entities, Camera camera, Matrix4 projectionMatrix, int width, int height)
        {
            if (!postConstructor)
            {
                Framebuffer.renderToFBO(1, Framebuffer.DepthTexture.Rezolution[0].widthRezolution,
                    Framebuffer.DepthTexture.Rezolution[0].heightRezolution);
                SetMatrices(camera);

                // only for closed geometry
                //GL.Enable(EnableCap.CullFace);
                //GL.CullFace(CullFaceMode.Back);
                foreach (object e in entities)
                {
                    RenderEntity r = e as RenderEntity;
                    Floor f = null;
                    if (r == null)
                    {
                        f = e as Floor;
                    }
                    shader.startProgram();
                    shader.setUniformValues(r == null ? f.ModelMatrix : r.ModelMatrix, ViewMatrix, ProjectionMatrix);
                    VAOManager.renderBuffers(r == null ? f.buffer : r.model.Buffer, OpenTK.Graphics.OpenGL.PrimitiveType.Triangles);
                    shader.stopProgram();
                }
                Framebuffer.unbindFramebuffer();
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                GL.Viewport(0, 0, width, height);
                //GL.Disable(EnableCap.CullFace);
            }
            PostConstructor();
        }

        private void PostConstructor()
        {
            if (postConstructor)
            {
                shader = new ShadowShader(Folder.Path + @"/shaders/shadowVS.glsl", Folder.Path + @"/shaders/shadowFS.glsl");
                Framebuffer = new ShadowFramebuffer();

                OffsetMatrix = new Matrix4(
                    0.5f, 0, 0, 0,
                    0, 0.5f, 0, 0,
                    0, 0, 0.5f, 0,
                    0.5f, 0.5f, 0.5f, 1);

                postConstructor = false;
            }
        }

        public ShadowRenderer(DirectionalLight light)
        {
            this.light = light;
        }

        public void SetMatrices(Camera camera)
        {
            projectionMatrix = Matrix4.CreateOrthographicOffCenter(-30, 30, -30, 30, -30, 40);
            viewMatrix = Matrix4.LookAt(light.Direction, new Vector3(0, 0, 0), camera.Up);
        }
    }
}
