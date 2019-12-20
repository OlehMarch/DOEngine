using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using Programmable_PipelineLight;
using Shadow.Skybox;
using Shadow.Shadow;
using Shadow.UI;
using Shadow;

using Point = System.Drawing.Point;

namespace WpfShadow
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
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

        private GLControl glControl;

        private Point prevMousePoint;
        private bool _postConstructor = true;

        public MainWindow()
        {
            InitializeComponent();
            glControl = new GLControl();
            glControl.Paint += GlControl_Paint;
            glControl.MouseMove += GlControl_MouseMove;
            glControl.MouseDown += GlControl_MouseDown;
            this.SizeChanged += MainWindow_SizeChanged;
            WFHost.Child = glControl;
            this.Width = 800;
            this.Height = 600;
            prevMousePoint = new Point(-1, -1);
        }

        private void GlControl_Paint(object sender, PaintEventArgs e)
        {
            GL.Enable(EnableCap.DepthTest);
            Clear();
            MainLoop();
            glControl.SwapBuffers();

            glControl.Invalidate();
        }

        private void GlControl_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                camera.RotateByMouse(e.X - prevMousePoint.X, e.Y - prevMousePoint.Y);
            }

            prevMousePoint = e.Location;
        }

        private void GlControl_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                entities.Add(new RenderEntity(Folder.Path + @"\models\City_House_2_BI.obj", 
                    TextureLoaderClass.GetHouseTexture(),
                    new Vector3(-5, 5, (15 + entities.Count) % 35), new Vector3(0.5f)));
            }
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            glControl.Invalidate();
            //GL.Viewport(0, 0, this.Width, this.Height);
        }

        private void Clear()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.ClearColor(Color4.Blue);
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
                entities.Add(new RenderEntity(Folder.Path + @"\models\dragon.obj",
                    TextureLoaderClass.GetCubeTexture(),
                    new Vector3(-5, 5, 0), new Vector3(0.5f)));
                entities.Add(new RenderEntity(Folder.Path + @"\models\sphere.obj",
                    TextureLoaderClass.GetCubeTexture(),
                    new Vector3(0, 10, 0), new Vector3(2.5f)));
                entities.Add(new RenderEntity(Folder.Path + @"\models\City_House_2_BI.obj",
                    TextureLoaderClass.GetHouseTexture(),
                     new Vector3(5, 5, 0), new Vector3(1f)));
                entities.Add(new RenderEntity(Folder.Path + @"\models\playerCube.obj",
                    TextureLoaderClass.GetCubeTexture(),
                    new Vector3(10, 5, 10), new Vector3(0.5f)));
                entities.Add(new RenderEntity(Folder.Path + @"\models\playerCube.obj",
                    TextureLoaderClass.GetCubeTexture(),
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
            , camera, projectionMatrix, Convert.ToInt32(this.Width), Convert.ToInt32(this.Height));

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
