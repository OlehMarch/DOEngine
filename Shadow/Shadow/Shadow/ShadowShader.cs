using OpenTK;
using ShaderPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadow.Shadow
{
    public class ShadowShader : Shader
    {
        private int modelMatrix, viewMatrix, projectionMatrix;

        protected override void getAllUniformLocations()
        {
            modelMatrix = base.getUniformLocation("modelMatrix");
            viewMatrix = base.getUniformLocation("viewMatrix");
            projectionMatrix = base.getUniformLocation("projectionMatrix");
        }

        public void setUniformValues(Matrix4 modelMatrix, Matrix4 viewMatrix, Matrix4 projectionMatrix)
        {
            base.loadMatrix(this.modelMatrix, false, modelMatrix);
            base.loadMatrix(this.viewMatrix, false, viewMatrix);
            base.loadMatrix(this.projectionMatrix, false, projectionMatrix);
        }

        public ShadowShader(string vs, string fs)
            : base(vs, fs)
        {
            if (ShaderLoaded)
            {
                showCompileLogInfo("Shadow");
                showLinkLogInfo("Shadow");
            }
        }
    }
}
