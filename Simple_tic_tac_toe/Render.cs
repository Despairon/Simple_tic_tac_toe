using System.Collections.Generic;
using Tao.FreeGlut;
using Tao.OpenGl;
using Tao.Platform.Windows;

namespace Simple_tic_tac_toe
{
    public static class Render
    {
        public static void init(ref SimpleOpenGlControl canv)
        {
            canvas = canv;
            canvas.InitializeContexts();
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_SINGLE | Glut.GLUT_DEPTH);
            Gl.glClearColor(1, 1, 1, 1);
            Gl.glViewport(0, 0, canvas.Width, canvas.Height);
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Glu.gluOrtho2D(0.0, canvas.Width, canvas.Height, 0.0);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
            Gl.glEnable(Gl.GL_DEPTH_TEST);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);

        }
        private static List<Shape> geometrics;
        private static SimpleOpenGlControl canvas;

        public static void drawAll()
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);

            drawGrid();

            Gl.glFlush();
            canvas.Invalidate(); 
        }     
        
        private static void drawGrid()
        {
            Gl.glColor3f(0.0f, 0.0f, 0.0f);

            int x = 0;
            int x1 = canvas.Width;
            int y = (int)(canvas.Height * 0.33);
            int y1 = y;

            Gl.glLoadIdentity();
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glPushMatrix();
            Gl.glVertex2d(x1, y1 + 3);
            Gl.glVertex2d(x1, y1 - 3);
            Gl.glVertex2d(x, y - 3);
            Gl.glVertex2d(x, y + 3);
            Gl.glPopMatrix();
            Gl.glEnd();

            y = (int)(canvas.Height * 0.66);
            y1 = y;

            Gl.glLoadIdentity();
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glPushMatrix();
            Gl.glVertex2d(x1, y1 + 3);
            Gl.glVertex2d(x1, y1 - 3);
            Gl.glVertex2d(x, y - 3);
            Gl.glVertex2d(x, y + 3);
            Gl.glPopMatrix();
            Gl.glEnd();

            x = (int)(canvas.Width * 0.33);
            x1 = x;
            y = 0;
            y1 = canvas.Height;

            Gl.glLoadIdentity();
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glPushMatrix();
            Gl.glVertex2d(x1 + 3, y1);
            Gl.glVertex2d(x1 - 3, y1);
            Gl.glVertex2d(x - 3, y);
            Gl.glVertex2d(x + 3, y);
            Gl.glPopMatrix();
            Gl.glEnd();

            x = (int)(canvas.Width * 0.66);
            x1 = x;

            Gl.glLoadIdentity();
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glPushMatrix();
            Gl.glVertex2d(x1 + 3, y1);
            Gl.glVertex2d(x1 - 3, y1);
            Gl.glVertex2d(x - 3, y);
            Gl.glVertex2d(x + 3, y);
            Gl.glPopMatrix();
            Gl.glEnd();
        }  
    }

    public abstract class Shape
    {
        public Shape(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public int x { get; }
        public int y { get; }

        public abstract void draw();
    }

    public class X : Shape
    {
        public X(int x, int y) : base (x,y)
        {

        }

        public override void draw()
        {

        }
    }

    public class O : Shape
    {
        public O(int x, int y) : base(x, y)
        {

        }

        public override void draw()
        {

        }
    }

}
