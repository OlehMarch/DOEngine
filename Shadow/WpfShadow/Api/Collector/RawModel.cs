using GpuGraphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadow.Api.Collector
{
    public class RawModel : IDisposable
    {
        private string Key { set; get; }
        public VAO Buffer { private set; get; }

        public RawModel(string key)
        {
            this.Key = key;
            LoadBuffer();
        }

        private void LoadBuffer()
        {
            this.Buffer = MediaCollector.GetModel(Key);
        }

        public void Dispose()
        {
            MediaCollector.ReleaseModel(Key);
            this.Key = null;
        }
    }
}
