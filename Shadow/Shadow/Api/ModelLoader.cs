using GpuGraphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CParser;

namespace Shadow
{
    public static class ModelLoader
    {
        public static VBOArrayF GetHouseModel()
        {
            CParser.ModelLoader loader = new CParser.ModelLoader(Folder.Path + @"\models\City_House_2_BI.obj");
            return new VBOArrayF(loader.Verts, loader.N_Verts, loader.T_Verts, true);
        }

        public static VBOArrayF GetCubeModel()
        {
            CParser.ModelLoader loader = new CParser.ModelLoader(Folder.Path + @"\models\playerCube.obj");
            return new VBOArrayF(loader.Verts, loader.N_Verts, loader.T_Verts, true);
        }

        public static VBOArrayF GetDragonModel()
        {
            CParser.ModelLoader loader = new CParser.ModelLoader(Folder.Path + @"\models\dragon.obj");
            return new VBOArrayF(loader.Verts, loader.N_Verts, loader.T_Verts, true);
        }

        public static VBOArrayF GetSphereModel()
        {
            CParser.ModelLoader loader = new CParser.ModelLoader(Folder.Path + @"\models\sphere.obj");
            return new VBOArrayF(loader.Verts, loader.N_Verts, loader.T_Verts, true);
        }
    }
}
