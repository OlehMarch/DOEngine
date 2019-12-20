using ShaderPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadow.UI
{
    public class UiShader : Shader
    {
        private int texImage;

        protected override void getAllUniformLocations()
        {
            texImage = base.getUniformLocation("texImage");
        }

        public void setUniformValues(int texImage)
        {
            base.loadInteger(this.texImage, texImage);
        }

        public UiShader(string vs, string fs)
            : base(vs, fs)
        {
            if (ShaderLoaded)
            {
                base.showCompileLogInfo("UI");
                base.showLinkLogInfo("UI");
            }
        }

    }
}
