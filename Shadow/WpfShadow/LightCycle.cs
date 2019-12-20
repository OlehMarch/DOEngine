using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Programmable_PipelineLight;
using System.Windows.Forms;
using OpenTK;
using VMath;

using Timer = System.Threading.Timer;

namespace Shadow
{
    public class LightCycle
    {
        private DirectionalLight light;
        private Timer updatePosition;
        private float angle = 0.6f;

        public LightCycle(DirectionalLight light)
        {
            this.light = light;
            updatePosition = new Timer(new System.Threading.TimerCallback((o) =>
            {
                Matrix4 rot = Matrix4.Identity;
                rot *= Matrix4.CreateRotationY(MathHelper.DegreesToRadians(angle));
                light.Direction = new Vector3(VectorMath.multMatrix(rot, new Vector4(light.Direction, 0.0f)));
            }));

            updatePosition.Change(0, 10);
        }
    }
}
