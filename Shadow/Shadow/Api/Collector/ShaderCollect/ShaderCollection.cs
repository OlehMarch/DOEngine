using ShaderPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Shadow.Api.Collector.ShaderCollect
{
    public class ShaderCollection
    {
        private Dictionary<string, Shader> shaderDictionary;
        private Dictionary<string, int> referenceCount;

        public ShaderCollection()
        {
            shaderDictionary = new Dictionary<string, Shader>();
            referenceCount = new Dictionary<string, int>();
        }

        public Shader RetrieveShader(string key, ConstructorInfo ctor)
        {
            return TryGetShader(key, ctor);
        }

        private Shader TryGetShader(string key, ConstructorInfo ctor)
        {
            Shader result = null;
            bool exist = shaderDictionary.TryGetValue(key, out result);
            if (!exist)
            {
                result = ShaderAllocator.LoadShaderFromFile(key, ctor);
                shaderDictionary.Add(key, result);
            }
            IncreaseRefCounter(key, exist);
            return result;
        }

        private void IncreaseRefCounter(string key, bool exist)
        {
            if (exist)
            {
                referenceCount[key]++;
            }
            else
            {
                referenceCount.Add(key, 1);
            }
        }

        public void ReleaseShader(string key)
        {
            bool exist = false;
            exist = shaderDictionary.Any( value => value.Key == key);
            if (exist)
            {
                DecrementReference(key);
            }
        }

        private void DecrementReference(string key)
        {
            referenceCount[key]--;
            if (referenceCount[key] == 0)
            {
                ShaderAllocator.ReleaseShader(shaderDictionary[key]);
                shaderDictionary.Remove(key);
                referenceCount.Remove(key);
            }
        }
    }
}
