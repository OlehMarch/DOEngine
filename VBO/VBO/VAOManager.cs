using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace GpuGraphics
{
    public static class VAOManager
    {
        public static void genVAO(VAO buffers)
        {
            if (buffers.isMapped()) return;    //Проверка ,что генерируем буфферы 1 раз 
            GL.GenVertexArrays(1, buffers.Vao);
            GL.GenBuffers(buffers.getBuffersCount(), buffers.Vbo); //Генерируем заданное количество буферов
            if (buffers.getBufferData().Indices != null)
            {
                GL.GenBuffers(1, buffers.Ibo);
            }
            buffers.mapBuffer();   //Устанавливаем флаг, что буфферы сгенерированы
        }
        public static void setBufferData(BufferTarget bufferType, VAO buffer)
        {
            if (buffer.isDataInserted()) return;    //Проверка ,что заносим информацию в буфферы 1 раз 
            byte bufferCount = 0;
            // Bind VBO's
            GL.BindBuffer(bufferType, buffer.Vbo[bufferCount]);
            GL.BufferData(bufferType, buffer.getBufferData().getVerticesByteSize(),
                buffer.getBufferData().Vertices, BufferUsageHint.StaticDraw);
            bufferCount++;
            if (buffer.getBufferData().Normals != null)
            {
                GL.BindBuffer(bufferType, buffer.Vbo[bufferCount]);
                GL.BufferData(bufferType, buffer.getBufferData().getNormalsByteSize(),
                    buffer.getBufferData().Normals, BufferUsageHint.StaticDraw);
                bufferCount++;
            }
            if (buffer.getBufferData().TextureCoordinates != null)
            {
                GL.BindBuffer(bufferType, buffer.Vbo[bufferCount]);
                GL.BufferData(bufferType, buffer.getBufferData().getTexCoordByteSize(),
                    buffer.getBufferData().TextureCoordinates, BufferUsageHint.StaticDraw);
                bufferCount++;
            }
            if (buffer.getBufferData().Color != null)
            {
                GL.BindBuffer(bufferType, buffer.Vbo[bufferCount]);
                GL.BufferData(bufferType, buffer.getBufferData().getColorByteSize(),
                    buffer.getBufferData().Color, BufferUsageHint.StaticDraw);
                bufferCount++;
            }
            if (buffer.getBufferData().Tangent != null)
            {
                GL.BindBuffer(bufferType, buffer.Vbo[bufferCount]);
                GL.BufferData(bufferType, buffer.getBufferData().getTangentByteSize(),
                    buffer.getBufferData().Tangent, BufferUsageHint.StaticDraw);
                bufferCount++;
            }
            if (buffer.getBufferData().Bitangent != null)
            {
                GL.BindBuffer(bufferType, buffer.Vbo[bufferCount]);
                GL.BufferData(bufferType, buffer.getBufferData().getBitangentByteSize(),
                    buffer.getBufferData().Bitangent, BufferUsageHint.StaticDraw);
                bufferCount++;
            }
            if (buffer.getBufferData().UserAttribValue != null)
            {
                GL.BindBuffer(bufferType, buffer.Vbo[bufferCount]);
                GL.BufferData(bufferType, buffer.getBufferData().getUserAttribValueByteSize(),
                    buffer.getBufferData().UserAttribValue, BufferUsageHint.StaticDraw);
                bufferCount++;
            }
            if (buffer.getBufferData().UserAttribVector2 != null)
            {
                GL.BindBuffer(bufferType, buffer.Vbo[bufferCount]);
                GL.BufferData(bufferType, buffer.getBufferData().getUserAttribVector2ByteSize(),
                    buffer.getBufferData().UserAttribVector2, BufferUsageHint.StaticDraw);
                bufferCount++;
            }
            if (buffer.getBufferData().UserAttribVector3 != null)
            {
                GL.BindBuffer(bufferType, buffer.Vbo[bufferCount]);
                GL.BufferData(bufferType, buffer.getBufferData().getUserAttribVector3ByteSize(),
                    buffer.getBufferData().UserAttribVector3, BufferUsageHint.StaticDraw);
                bufferCount++;
            }
            if (buffer.getBufferData().UserAttribVector4 != null)
            {
                GL.BindBuffer(bufferType, buffer.Vbo[bufferCount]);
                GL.BufferData(bufferType, buffer.getBufferData().getUserAttribVector4ByteSize(),
                    buffer.getBufferData().UserAttribVector4, BufferUsageHint.StaticDraw);
            }
            bufferCount = 0;
            //***************************************Bind VAO***********************//
            GL.BindVertexArray(buffer.Vao[0]);

            if (buffer.getBufferData().Indices != null)
            {
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, buffer.Ibo[0]);  //Bind IBO
                GL.BufferData(BufferTarget.ElementArrayBuffer, buffer.getBufferData().getIndicesByteSize(),
                    buffer.getBufferData().Indices, BufferUsageHint.StaticDraw);
            }
            GL.BindBuffer(bufferType, buffer.Vbo[bufferCount]); //Вершины
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
            bufferCount++;

            if (buffer.getBufferData().Normals != null)
            {
                GL.BindBuffer(bufferType, buffer.Vbo[bufferCount]); //Нормали
                GL.EnableVertexAttribArray(1);
                GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 0, 0);
                bufferCount++;
            }
            if (buffer.getBufferData().TextureCoordinates != null)
            {
                GL.BindBuffer(bufferType, buffer.Vbo[bufferCount]); //Текстурные координаты
                GL.EnableVertexAttribArray(2);
                GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, 0, 0);
                bufferCount++;
            }
            if (buffer.getBufferData().Color != null)
            {
                GL.BindBuffer(bufferType, buffer.Vbo[bufferCount]); //Цвет
                GL.EnableVertexAttribArray(3);
                GL.VertexAttribPointer(3, 3, VertexAttribPointerType.Float, false, 0, 0);
                bufferCount++;
            }
            if (buffer.getBufferData().Tangent != null)
            {
                GL.BindBuffer(bufferType, buffer.Vbo[bufferCount]); //Касательные
                GL.EnableVertexAttribArray(4);
                GL.VertexAttribPointer(4, 3, VertexAttribPointerType.Float, false, 0, 0);
                bufferCount++;
            }
            if (buffer.getBufferData().Bitangent != null)
            {
                GL.BindBuffer(bufferType, buffer.Vbo[bufferCount]); //Бикасательные
                GL.EnableVertexAttribArray(5);
                GL.VertexAttribPointer(5, 3, VertexAttribPointerType.Float, false, 0, 0);
                bufferCount++;
            }
            if (buffer.getBufferData().UserAttribValue != null)
            {
                GL.BindBuffer(bufferType, buffer.Vbo[bufferCount]); //Пользовательские атрибуты 1
                GL.EnableVertexAttribArray(6);
                GL.VertexAttribPointer(6, 1, VertexAttribPointerType.Float, false, 0, 0);
                bufferCount++;
            }
            if (buffer.getBufferData().UserAttribVector2 != null)
            {
                GL.BindBuffer(bufferType, buffer.Vbo[bufferCount]); //Пользовательские атрибуты 2
                GL.EnableVertexAttribArray(7);
                GL.VertexAttribPointer(7, 2, VertexAttribPointerType.Float, false, 0, 0);
                bufferCount++;
            }
            if (buffer.getBufferData().UserAttribVector3 != null)
            {
                GL.BindBuffer(bufferType, buffer.Vbo[bufferCount]); //Пользовательские атрибуты 3
                GL.EnableVertexAttribArray(8);
                GL.VertexAttribPointer(8, 3, VertexAttribPointerType.Float, false, 0, 0);
                bufferCount++;
            }
            if (buffer.getBufferData().UserAttribVector4 != null)
            {
                GL.BindBuffer(bufferType, buffer.Vbo[bufferCount]); //Пользовательские атрибуты 4
                GL.EnableVertexAttribArray(9);
                GL.VertexAttribPointer(9, 4, VertexAttribPointerType.Float, false, 0, 0);
            }

            GL.BindVertexArray(0);      //Отвязываем VAO
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0); //Отвязываем VBO
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);  //Отвязываем IBO

            GL.DisableVertexAttribArray(0);
            if (buffer.getBufferData().Normals != null) { GL.DisableVertexAttribArray(1); }
            if (buffer.getBufferData().TextureCoordinates != null) { GL.DisableVertexAttribArray(2); }
            if (buffer.getBufferData().Color != null) { GL.DisableVertexAttribArray(3); }
            if (buffer.getBufferData().Tangent != null) { GL.DisableVertexAttribArray(4); }
            if (buffer.getBufferData().Bitangent != null) { GL.DisableVertexAttribArray(5); }
            if (buffer.getBufferData().UserAttribValue != null) { GL.DisableVertexAttribArray(6); }
            if (buffer.getBufferData().UserAttribVector2 != null) { GL.DisableVertexAttribArray(7); }
            if (buffer.getBufferData().UserAttribVector3 != null) { GL.DisableVertexAttribArray(8); }
            if (buffer.getBufferData().UserAttribVector4 != null) { GL.DisableVertexAttribArray(9); }

            buffer.dataInserted();
        }

        public static void updateBufferDataPartially(VAO buffer, VBOArrayF bufferData, uint vboIndex,
            int offsetInBytes, int updateSize)
        {
            if (vboIndex == 10)
            {
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, buffer.Ibo[0]);
                GL.BufferSubData(BufferTarget.ElementArrayBuffer, new IntPtr(offsetInBytes),
                    new IntPtr(updateSize), bufferData.Indices);
                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            }
            else
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, vboIndex);
                GL.BufferSubData(BufferTarget.ArrayBuffer, new IntPtr(offsetInBytes), new IntPtr(updateSize), bufferData[vboIndex]);
                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            }
        }
        public static void updateBufferFully(VAO buffer, VBOArrayF bufferData, uint bufferIndex)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, buffer.Vbo[bufferIndex]);
            GL.BufferSubData(BufferTarget.ArrayBuffer, new IntPtr(0),
                bufferData.getAtrributeByteSize(bufferIndex), bufferData[bufferIndex]);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        //public static void changeBufferTypeAt(VBO buffer, uint bufferIndex, BufferTarget bufferType)
        //{
        //    GL.BindBuffer(buffer.getBufferTypeAt(bufferIndex), buffer.VBO[bufferIndex]);
        //    GL.BufferData(bufferType, buffer.getBufferData().getAtrributeByteSize(bufferIndex), buffer.getBufferData()[bufferIndex],
        //        buffer.getBufferUsageAt(bufferIndex));
        //}
        //public static void changeBufferUsageAt(VBO buffer, uint bufferIndex, BufferUsageHint bufferUsage)
        //{
        //    GL.BindBuffer(buffer.getBufferTypeAt(bufferIndex), buffer.VBO[bufferIndex]);
        //    GL.BufferData(buffer.getBufferTypeAt(bufferIndex), buffer.getBufferData().getAtrributeByteSize(bufferIndex),
        //        buffer.getBufferData()[bufferIndex], bufferUsage);
        //}

        public static void renderBuffers(VAO buffer, PrimitiveType privitiveMode)
        {
            GL.BindVertexArray(buffer.Vao[0]);
            if (buffer.getBufferData().Indices != null)
            {
                GL.DrawElements(privitiveMode, buffer.getBufferData().Indices.Length, DrawElementsType.UnsignedInt, 0);
            }
            else
            {
                GL.DrawArrays(privitiveMode, 0, buffer.getBufferData().getCountVertices());
            }
            GL.BindVertexArray(0);
        }

        public static void cleanUp(VAO buffer)
        {
            GL.DeleteBuffers(buffer.getBuffersCount(), buffer.Vbo);
            if (buffer.getBufferData().Indices != null) { GL.DeleteBuffers(1, buffer.Ibo); }
            GL.DeleteVertexArrays(1, buffer.Vao);
        }
    }
}
