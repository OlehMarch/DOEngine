﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using GpuGraphics;
using Programmable_PipelineLight;
using VMath;
using Shadow.Skybox;
using Shadow.Shadow;
using Shadow.UI;

namespace Shadow
{
    public class MainWindow : GameWindow
    {
        private Camera camera;

        private List<RenderEntity> entities;

        private Matrix4 projectionMatrix;
        private DirectionalLight light;
        private Floor floor;
        private SkyboxRenderer sky;
        private LightCycle lCycle;
        private ShadowRenderer shadow;
        private UiRenderer ui;

        private bool _postConstructor = true;

        public MainWindow(int width, int height)
            : base(width, height, GraphicsMode.Default, "Shadow")
        {
            
        }

        private void Clear()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.ClearColor(Color4.Blue);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Enable(EnableCap.DepthTest);
            Clear();
            MainLoop();
            this.SwapBuffers();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, this.Width, this.Height);
        }

        protected override void OnMouseMove(OpenTK.Input.MouseMoveEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.Mouse.LeftButton == OpenTK.Input.ButtonState.Pressed)
            {
                camera.RotateByMouse(e.XDelta, e.YDelta);
            }
            
        }

        protected override void OnMouseDown(OpenTK.Input.MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == OpenTK.Input.MouseButton.Right)
            {
                entities.Add(new RenderEntity(Folder.Path + @"\models\City_House_2_BI.obj", TextureLoader.GetHouseTexture(),
                    new Vector3(-5, 5, (15 + entities.Count) % 35), new Vector3(0.5f)));
            }
        }

        private void PostContructor()
        {
            if (_postConstructor)
            {
                projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(75), 16f / 9f, 1, 200);
                camera = new Camera(0, 10, 15, 0, 5, 0, 0, 1, 0);
                light = new DirectionalLight(new Vector3(0.5f, 5, 10), new Vector4(0.2f, 0.2f, 0.2f, 1.0f), new Vector4(0.7f, 0.7f, 0.7f, 1.0f),
                    new Vector4(1f, 1f, 1f, 1.0f));
                lCycle = new LightCycle(light);


                entities = new List<RenderEntity>();
                entities.Add(new RenderEntity(Folder.Path + @"\models\dragon.obj", TextureLoader.GetCubeTexture(),
                    new Vector3(-5, 5, 0), new Vector3(0.5f)));
                entities.Add(new RenderEntity(Folder.Path + @"\models\sphere.obj", TextureLoader.GetCubeTexture(),
                    new Vector3(0, 10, 0), new Vector3(2.5f)));
                entities.Add(new RenderEntity(Folder.Path + @"\models\City_House_2_BI.obj", TextureLoader.GetHouseTexture(),
                     new Vector3(5, 5, 0), new Vector3(1f)));
                entities.Add(new RenderEntity(Folder.Path + @"\models\playerCube.obj", TextureLoader.GetCubeTexture(),
                    new Vector3(10, 5, 10), new Vector3(0.5f)));
                entities.Add(new RenderEntity(Folder.Path + @"\models\playerCube.obj", TextureLoader.GetCubeTexture(),
                    new Vector3(-5, 5, 15), new Vector3(0.5f)));

                floor = new Floor();
                sky = new SkyboxRenderer(ModelLoader.GetCubeModel());
                shadow = new ShadowRenderer(light);
                ui = new UiRenderer(-1, -1, 0.7f, 0.6f);
                _postConstructor = !_postConstructor;
            }
        }

        public void MainLoop()
        {
            PostContructor();

            shadow.BuildShadows(new object[] { entities[0], entities[1], entities[2], entities[3], entities[4], floor }
            , camera, projectionMatrix, this.Width, this.Height);
        
            entities.ForEach(entity =>
            {
                entity.Render(camera, projectionMatrix, light, shadow);
            });

            floor.Render(camera, projectionMatrix, light, shadow);
            sky.Render(camera.ViewMatrix, projectionMatrix);
            ui.Render(shadow.Framebuffer.DepthTexture.TextureID[0]);

            /* debug */
            //Api.Collector.MediaCollector.Equals(null, null);
        }
    }
}
