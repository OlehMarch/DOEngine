using GpuGraphics;
using ShaderPattern;
using Shadow.Api.Collector.ModelCollect;
using Shadow.Api.Collector.ShaderCollect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TextureLoader;

namespace Shadow.Api.Collector
{
    public static class MediaCollector
    {
        private static ModelCollector modelCollector;
        private static ShaderCollector shaderCollector; 
        //texture collector
        //sound collector

        static MediaCollector()
        {
            modelCollector = new ModelCollector();
        }

        public static void ReleaseModel(string key)
        {
            modelCollector.ReleaseModel(key);
        }

        public static VAO GetModel(string key)
        {
            return modelCollector.GetModel(key);
        }

        public static SingleTexture2D GetTexture(string key)
        {
            return null;
        }

        public static Shader GetShaderProgram(string key, ConstructorInfo ctor)
        {
            return shaderCollector.GetShader(key, ctor);
        }
    }
}
