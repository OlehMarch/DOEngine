using GpuGraphics;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK.Graphics.OpenGL;

namespace Shadow.UI
{
    public class UiRenderer
    {
        private VBOArrayF attributes;
        private VAO buffer;
        private UiShader shader;
        private bool postConstructor = true;

        private void PostConstructor()
        {
            if (postConstructor)
            {
                shader = new UiShader(Folder.Path + @"/shaders/uiVS.glsl", Folder.Path + @"/shaders/uiFS.glsl");
                VAOManager.genVAO(buffer);
                VAOManager.setBufferData(BufferTarget.ArrayBuffer, buffer);
                postConstructor = false;
            }
        }

        public void Render(uint texImage)
        {
            PostConstructor();

            shader.startProgram();
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, texImage);
            shader.setUniformValues(0);
            VAOManager.renderBuffers(buffer, PrimitiveType.Triangles);
            shader.stopProgram();
        }

        public UiRenderer(float x, float y, float width, float height)
        {
            Vector2 p1 = new Vector2(x, y), p2 = new Vector2(x + width, y), p3 = new Vector2(x + width, y + height),
                p4 = p3, p5 = new Vector2(x, y + height), p6 = p1;

            this.attributes = new VBOArrayF(new float[6, 3] { { p1.X, p1.Y, 0.0f }, { p2.X, p2.Y, 0.0f }, { p3.X, p3.Y, 0.0f },
             { p4.X, p4.Y, 0.0f }, { p5.X, p5.Y, 0.0f }, { p6.X, p6.Y, 0.0f} },
            new float[6, 2] { { 0, 1 }, { 1, 1 }, { 1, 0 }, { 1, 0 }, { 0, 0 }, { 0, 1 } }, null);
            buffer = new VAO(attributes);
        }
    }
}
