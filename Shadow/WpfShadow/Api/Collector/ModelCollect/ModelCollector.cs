using GpuGraphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadow.Api.Collector.ModelCollect
{
    public class ModelCollector
    {
        private ModelCollection modelCollection;

        public ModelCollector()
        {
            modelCollection = new ModelCollection();
        }
        
        public VAO GetModel(string key)
        {
            return modelCollection.RetrieveModel(key);
        }

        public void ReleaseModel(string key)
        {
            this.modelCollection.ReleaseModel(key);
        }
    }
}
