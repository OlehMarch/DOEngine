using FramebufferAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextureLoader;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Shadow.Shadow
{
    public class ShadowFramebuffer : Framebuffer
    {
        //public int DepthTexture { get { return depthTexture; } }
        public Texture2D DepthTexture { get { return base.textures; } }
        private int depthTexture;
        private const int SIZE = 1024;

        protected override void setTextures()
        {
            //this.depthTexture = GL.GenTexture();
            //GL.TexStorage2D(TextureTarget2d.Texture2D, 1, SizedInternalFormat.Rgba16ui, SIZE, SIZE);
            //GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)All.Nearest);
            //GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)All.Nearest);
            //GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)All.ClampToBorder);
            //GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)All.ClampToBorder);
            //GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureBorderColor, new float[] { 1.0f, 0, 0, 0 });
            //GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureCompareMode, (int)All.CompareRefToTexture);
            //GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureCompareFunc, (int)All.Less);
            base.textures = new Texture2D();
            textures.genEmptyImg(1, SIZE, SIZE, (int)All.Nearest, PixelInternalFormat.DepthComponent16, PixelFormat.DepthComponent,
                PixelType.Float, TextureWrapMode.ClampToEdge);
        }

        protected override void setFramebuffers()
        {
            base.genFramebuffers(1);
            base.bindFramebuffer(1);
            base.attachTextureToFramebuffer(FramebufferAttachment.DepthAttachment, textures.TextureID[0]);
            GL.DrawBuffer(DrawBufferMode.None);
            base.unbindFramebuffer();
        }

        protected override void setRenderbuffers()
        {

        }

        public ShadowFramebuffer()
            : base()
        {
            
        }

    }
}
