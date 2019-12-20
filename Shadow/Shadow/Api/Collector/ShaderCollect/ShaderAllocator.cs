using ShaderPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Shadow.Api.Collector.ShaderCollect
{
    public static class ShaderAllocator
    {
        public static Shader LoadShaderFromFile(string files, ConstructorInfo ctor)
        {
            return (Shader)ctor.Invoke(new object[] { files });
        }

        internal static void ReleaseShader(Shader shader)
        {
            throw new NotImplementedException();
        }
    }
}
