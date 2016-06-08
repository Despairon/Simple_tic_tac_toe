using System;
using System.Collections.Generic;
using Tao.FreeGlut;
using Tao.OpenGl;
using Tao.Platform.Windows;

namespace Simple_tic_tac_toe
{
    /*** статический класс "отображение". Статический, потому что нам нужен только один его экземпляр,
     *   и доступность полей во всех других классах (по-крутому в программировании это называется
     *   "синглтон", если я правильно понимаю значение этого слова :) 
     *   см. "шаблоны проектирования")
     ***/
    public static class Render
    {
        /*** в этой функции мы инициализируем всякие разности для работы с openGL
         *   Входной параметр canv - ссылка на элемент управления SimpleOpenGlControl, который
         *   расположен на форме и выполняет отображающую функцию 
         ***/
        public static void init(ref SimpleOpenGlControl canv)
        {
            canvas = canv;
            canvas.InitializeContexts();  // инициализация канвы
            Glut.glutInit();              // инициализация библиотеки Glut
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_SINGLE | Glut.GLUT_DEPTH);  // выбираем режим отображения
            Gl.glClearColor(1, 1, 1, 1);      // определяем цвет очистки канвы
            Gl.glViewport(0, 0, canvas.Width, canvas.Height);  // определяем пространство видимости. В нашем случае - это полностью вся канва
            Gl.glMatrixMode(Gl.GL_PROJECTION);  // ставим режим матрицы - проекция
            Gl.glLoadIdentity();    // загружаем единичную матрицу
            Glu.gluOrtho2D(0.0, canvas.Width, canvas.Height, 0.0);  // настраиваем проекцию
            Gl.glMatrixMode(Gl.GL_MODELVIEW);   // ставим режим матрицы - модельно-видовая
            Gl.glLoadIdentity();  // загружаем единичную
            Gl.glEnable(Gl.GL_DEPTH_TEST);  // включаем тест глубинного буфера
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);  // очищаем буферы

            geometrics = new List<Shape>(); // инициализируем список фигур
        }
        private static List<Shape> geometrics;      // список фигур
        private static SimpleOpenGlControl canvas;  // указатель на канву

        /*** функция рисования всех фигур ***/
        public static void drawAll()
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);  // очищаем буферы

            drawGrid();    // рисуем клетки
            foreach (var shape in geometrics)   // для каждой фигуры в списке
                shape.draw();                   // рисуем её

            Gl.glFlush();                       // ждем завершения процесса рисования
            canvas.Invalidate();                // перерисовываем кадр
        }     
        
        /*** функция рисования клеток ***/
        private static void drawGrid()
        {
            Gl.glColor3f(0.0f, 0.0f, 0.0f);     // ставим черный цвет

            /*** координаты х,х1,у,у1 - координаты вершин прямоугольника.
             *   Клетки рисуются путем рисования линий (прямоугольников) от края до края канвы.
             *   Координаты считаются таким образом, чтобы разделить канву на 3 части 
             *   (умножение на 0.33, 0.66) и на каждой части рисуется линия
             ***/
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

        /*** функция удаления фигур ***/
        public static void clear()
        {
            geometrics.Clear();
        }

        /*** функция рисует крестик по указанным координатам ***/
        public static void addX(ref int x, ref int y)
        {
            geometrics.Add(new X(x, y));
        }

        /*** функция рисует нолик по указанным координатам ***/
        public static void addO(ref int x, ref int y)
        {
            geometrics.Add(new O(x, y));
        }
    }

    /*** абстрактный класс "фигура". Проявление полиморфизма : может быть ТОЛЬКО либо
     *   крестиком, либо ноликом. фигуру саму по себе создать нельзя - только одну из её проявлений. 
     ***/
    public abstract class Shape     
    {
        public Shape(int x, int y)  // точка (х,у) - центральная точка фигуры. От нее мы отталкиваемся когда пишем координаты в методе draw каждой фигуры
        {
            this.x = x;
            this.y = y;
        }
        public int x { get; }
        public int y { get; }

        public abstract void draw();
    }

    /*** класс "крестик", который является фигурой ***/
    public class X : Shape
    {
        public X(int x, int y) : base(x, y) { }

        public override void draw()
        {
            Gl.glColor3f(1.0f, 0.0f, 0.0f);
            Gl.glLoadIdentity();
            Gl.glBegin(Gl.GL_QUADS);            // режим рисования - квадраты
            Gl.glPushMatrix();

            Gl.glVertex2d(x - 50, y + 55);          // рисуем правый слеш
            Gl.glVertex2d(x - 60, y + 55);
            Gl.glVertex2d(x + 50, y - 55);
            Gl.glVertex2d(x + 60, y - 55);

            Gl.glVertex2d(x + 50, y + 55);          // рисуем левый слеш
            Gl.glVertex2d(x + 60, y + 55);
            Gl.glVertex2d(x - 50, y - 55);
            Gl.glVertex2d(x - 60, y - 55);

            Gl.glPopMatrix();
            Gl.glEnd();
        }
    }

    /*** класс "нолик", который является фигурой ***/
    public class O : Shape
    {
        public O(int x, int y) : base(x, y) { }

        /***  Нолик рисуется таким образом, что вначале рисуется внутренний круг, закрашенный белым
         *    цветом, а затем внешний, закрашенный синим. Таким образом получается не просто полностью
         *    закрашенный круг, а то что нам нужно - нолик (кольцо, окружность) 
         ***/
        public override void draw()
        {
            const int OUTTER_R = 60; // радиус внешнего круга
            const int INNER_R = 50;  // радиус внутреннего круга
            const int SEGMENTS = 100; // количество сегментов (круги рисуются треугольниками, сегмент - 1 треугольник)
            const float PI = 3.1415926f;
            Gl.glLoadIdentity();

            /*** рисуем белый внутренний круг... ***/

            Gl.glColor3d(1.0f, 1.0f, 1.0f);     // белый цвет
            Gl.glBegin(Gl.GL_TRIANGLE_FAN);     // режим рисования - "веер" из треугольников
            Gl.glVertex2d(x, y);                // начинаем из точки в центре круга
            for (int i = 0; i <= SEGMENTS; i++)     // алгоритм взят с интернета... цикл по кол-ву сегментов
            {
                float a = (float)i / (float)SEGMENTS * PI * 2.0f;  // высчитывается угол
                float x = (float)(INNER_R * Math.Cos(a));           // считаются коорды след. точки
                float y = (float)(INNER_R * Math.Sin(a));
                Gl.glVertex2d(this.x + x, this.y + y);      // точки соединяются по шаблону (веер из треугольников)
            }
            Gl.glEnd();

            /*** рисуем синий внешний круг... ***/
            Gl.glColor3d(0.0f, 0.0f, 1.0f);     // синий цвет
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
