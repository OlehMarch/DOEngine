using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ShaderPattern;
using OpenTK;
using Programmable_PipelineLight;

namespace Shadow
{
    public class EntityShader : Shader
    {
        private int modelMatrix, viewMatrix, projectionMatrix, frameTexture,
            lightDir, lightAmbient, lightDiffuse, lightSpecular, normalMap, cameraPos, multCoords,
            shadowMatrix, shadowmap;

        protected override void getAllUniformLocations()
        {
            modelMatrix = base.getUniformLocation("modelMatrix");
            viewMatrix = base.getUniformLocation("viewMatrix");
            projectionMatrix = base.getUniformLocation("projectionMatrix");
            frameTexture = base.getUniformLocation("frameTexture");
            lightDir = base.getUniformLocation("lightDir");
            lightAmbient = base.getUniformLocation("ambient");
            lightDiffuse = base.getUniformLocation("diffuse");
            lightSpecular = base.getUniformLocation("specular");
            normalMap = base.getUniformLocation("normalMap");
            cameraPos = base.getUniformLocation("cameraPos");
            multCoords = base.getUniformLocation("multCoords");
            shadowMatrix = base.getUniformLocation("shadowMatrix");
            shadowmap = base.getUniformLocation("shadowmap");
        }

        public void setUniforms(int frameTexture, int normalMap, Matrix4 modelMatrix,
            Matrix4 viewMatrix, Matrix4 projectionMatrix, DirectionalLight light, Vector3 cameraPos, bool multCoords,
            Matrix4 shadowMatrix, int shadowmap)
        {
            base.loadMatrix(this.modelMatrix, false, modelMatrix);
            base.loadMatrix(this.viewMatrix, false, viewMatrix);
            base.loadMatrix(this.projectionMatrix, false, projectionMatrix);
            base.loadInteger(this.frameTexture, frameTexture);
            base.loadInteger(this.normalMap, normalMap);
            base.loadVector(this.lightDir, light.Direction);
            base.loadVector(this.lightAmbient, light.Ambient.Xyz);
            base.loadVector(this.lightDiffuse, light.Diffuse.Xyz);
            base.loadVector(this.lightSpecular, light.Specular.Xyz);
            base.loadVector(this.cameraPos, cameraPos);
            base.loadBool(this.multCoords, multCoords);
            base.loadMatrix(this.shadowMatrix, false, shadowMatrix);
            base.loadInteger(this.shadowmap, shadowmap);
        }

        public EntityShader(string vs, string fs)
            : base(vs, fs)
        {
            if (base.ShaderLoaded)
            {
                base.showCompileLogInfo("Enitity Shader");
                base.showLinkLogInfo("Enitity Shader");
            }
        }
    }
}
