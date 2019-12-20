using ShaderPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Shadow.Api.Collector.ShaderCollect
{
    public class ShaderCollector
    {
        private ShaderCollection shaderCollection;

        public ShaderCollector()
        {
            shaderCollection = new ShaderCollection();
        }

        public Shader GetShader(string key, ConstructorInfo ctor)
        {
            return shaderCollection.RetrieveShader(key, ctor);
        }

        public void ReleaseShader(string key)
        {
            this.shaderCollection.ReleaseShader(key);
        }
    }
}
