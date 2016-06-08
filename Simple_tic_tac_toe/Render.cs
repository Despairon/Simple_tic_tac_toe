using System;
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

            geometrics = new List<Shape>(); 
        }
        private static List<Shape> geometrics;      
        private static SimpleOpenGlControl canvas;  

        public static void drawAll()
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT); 

            drawGrid();    
            foreach (var shape in geometrics)   
                shape.draw();                   

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

        public static void clear()
        {
            geometrics.Clear();
        }

        public static void addX(ref int x, ref int y)
        {
            geometrics.Add(new X(x, y));
        }

        public static void addO(ref int x, ref int y)
        {
            geometrics.Add(new O(x, y));
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
        public X(int x, int y) : base(x, y) { }

        public override void draw()
        {
            Gl.glColor3f(1.0f, 0.0f, 0.0f);
            Gl.glLoadIdentity();
            Gl.glBegin(Gl.GL_QUADS);            
            Gl.glPushMatrix();

            Gl.glVertex2d(x - 50, y + 55);          
            Gl.glVertex2d(x - 60, y + 55);
            Gl.glVertex2d(x + 50, y - 55);
            Gl.glVertex2d(x + 60, y - 55);

            Gl.glVertex2d(x + 50, y + 55);          
            Gl.glVertex2d(x + 60, y + 55);
            Gl.glVertex2d(x - 50, y - 55);
            Gl.glVertex2d(x - 60, y - 55);

            Gl.glPopMatrix();
            Gl.glEnd();
        }
    }

    public class O : Shape
    {
        public O(int x, int y) : base(x, y) { }

        public override void draw()
        {
            const int OUTTER_R = 60; 
            const int INNER_R = 50; 
            const int SEGMENTS = 100;
            const float PI = 3.1415926f;
            Gl.glLoadIdentity();


            Gl.glColor3d(1.0f, 1.0f, 1.0f);     
            Gl.glBegin(Gl.GL_TRIANGLE_FAN);     
            Gl.glVertex2d(x, y);                
            for (int i = 0; i <= SEGMENTS; i++)     
            {
                float a = (float)i / (float)SEGMENTS * PI * 2.0f;  
                float x = (float)(INNER_R * Math.Cos(a));           
                float y = (float)(INNER_R * Math.Sin(a));
                Gl.glVertex2d(this.x + x, this.y + y);     
            }
            Gl.glEnd();

            Gl.glColor3d(0.0f, 0.0f, 1.0f);     
            Gl.glBegin(Gl.GL_TRIANGLE_FAN);         
            Gl.glVertex2d(x, y);
            for (int i = 0; i <= SEGMENTS; i++)
            {
                float a = (float)i / (float)SEGMENTS * PI * 2.0f;
                float x = (float)(OUTTER_R * Math.Cos(a));
                float y = (float)(OUTTER_R * Math.Sin(a));
                Gl.glVertex2d(this.x + x, this.y + y);
            }
            Gl.glEnd();

        }
    }

}
