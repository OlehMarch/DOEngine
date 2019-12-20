using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMath;

namespace Shadow
{
    public class Camera
    {
        public Vector3 Look { set; get; }
        public Vector3 Position { set; get; }
        public Vector3 Up { set; get; }

        public const float ROTATE_MEASURE = 0.5f;

        public Matrix4 ViewMatrix
        {
            get
            {
                return Matrix4.LookAt(Position, Look, Up);
            }
        }

        public void RotateByMouse(int deltaX, int deltaY)
        {
            RotatePosition(deltaX, deltaY);
        }

        public void RotatePosition(int deltaX, int deltaY)
        {
            // rotate pitch
            Vector3 lookDir = Vector3.Normalize(Look - Position);
            Vector3 binormalDir = Vector3.Normalize(Vector3.Cross(lookDir, Up));
            Matrix4 rotatePitch = Matrix4.CreateFromAxisAngle(binormalDir, MathHelper.DegreesToRadians(-deltaY * ROTATE_MEASURE));

            // rotate yaw
            Matrix4 rotateYaw = Matrix4.CreateRotationY(MathHelper.DegreesToRadians(-deltaX * ROTATE_MEASURE));

            Matrix4 rotationMatrix = Matrix4.Identity;
            rotationMatrix *= rotateYaw;
            rotationMatrix *= rotatePitch;

            this.Position = new Vector3(VectorMath.multMatrix(rotationMatrix, new Vector4(this.Position, 1.0f)));
        }

        public Camera(float posX, float posY, float posZ,
            float lookX, float lookY, float lookZ,
            float upX, float upY, float upZ)
        {
            this.Position = new Vector3(posX, posY, posZ);
            this.Look = new Vector3(lookX, lookY, lookZ);
            this.Up = new Vector3(upX, upY, upZ);
        }
    }
}
