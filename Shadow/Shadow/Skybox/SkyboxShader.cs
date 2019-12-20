using OpenTK;
using ShaderPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadow.Skybox
{
    public class SkyboxShader : Shader
    {
        private int cubeTex, viewMatrix, projectionMatrix;

        protected override void getAllUniformLocations()
        {
            cubeTex = base.getUniformLocation("cubeTex");
            viewMatrix = base.getUniformLocation("viewMatrix");
            projectionMatrix = base.getUniformLocation("projectionMatrix");
        }

        public void setUniforms(int cubeSampler, Matrix4 viewMatrix, Matrix4 projectionMatrix)
        {
            Matrix4 createScale = Matrix4.CreateScale(100);
            viewMatrix *= createScale;
            viewMatrix[3, 0] = 0;
            viewMatrix[3, 1] = 0;
            viewMatrix[3, 2] = 0;

            base.loadInteger(cubeTex, cubeSampler);
            base.loadMatrix(this.viewMatrix, false, viewMatrix);
            base.loadMatrix(this.projectionMatrix, false,  projectionMatrix);
        }

        public SkyboxShader(string vs, string fs)
            : base(vs, fs)
        {
            if (ShaderLoaded)
            {
                base.showCompileLogInfo("Skybox");
                base.showLinkLogInfo("Skybox");
            }
        }
    }
}
