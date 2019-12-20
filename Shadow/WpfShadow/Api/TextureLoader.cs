using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextureLoader;

namespace Shadow
{
    public static class TextureLoaderClass
    {
        public static Texture2D GetHouseTexture()
        {
            return new Texture2D(new String[] { Folder.Path + @"\textures\city_house_2_Col.jpg",
                Folder.Path + @"\textures\city_house_2_Nor.jpg" });
        }

        public static Texture2D GetCubeTexture()
        {
            return new Texture2D(new String[] { Folder.Path + @"\textures\b.png",
                Folder.Path + @"\textures\brick_nm_high.png" });
        }

        public static CubemapTexture GetSkyboxTexture()
        {
            return new CubemapTexture(new String[] { 
                Folder.Path + @"\textures\Day\Right.bmp",
                Folder.Path + @"\textures\Day\Left.bmp",
                Folder.Path + @"\textures\Day\top.bmp",
                Folder.Path + @"\textures\Day\bottom.bmp",
                Folder.Path + @"\textures\Day\Back.bmp",
                Folder.Path + @"\textures\Day\Front.bmp"
            });
        }
    }
}
