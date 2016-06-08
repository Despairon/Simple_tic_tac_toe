using System.Windows.Forms;

namespace Simple_tic_tac_toe
{
    public enum shapes
    {
        NONE = 0,
        X = 1,
        O = 2
    }

    public static class Tic_tac_toe
    {
        public static void new_game(int width, int height)
        {
            grid = new int[3, 3];
            gridWidth = width;
            gridHeight = height;
            currMove = (int)shapes.X;
            Render.clear();
        }
        private static int[,] grid;
        private static int currMove;
        private static int gridWidth;
        private static int gridHeight;

        public static void onClick(int x, int y)
        {
            int[] cell = clickedCell(ref x, ref y);
            int cellX = cell[0];
            int cellY = cell[1];
            if (canMove(ref cellX, ref cellY))
                move(ref cellX, ref cellY);
        }

        private static bool canMove(ref int cellX, ref int cellY)
        {
            if (grid[cellX, cellY] == (int)shapes.NONE)
                return true;
            return false;
        } 

        private static int[] clickedCell(ref int x, ref int y)
        { 
            int[] cell = new int[2] { -1 , -1 };
            float dx = (float)x / (float)gridWidth;
            float dy = (float)y / (float)gridHeight;
            if (dx < 0.33f)
                cell[0] = 0;
            else
            if (dx > 0.33f && dx < 0.66f)
                cell[0] = 1;
            else
                cell[0] = 2;

            if (dy < 0.33f)
                cell[1] = 0;
            else
            if (dy > 0.33f && dy < 0.66f)
                cell[1] = 1;
            else
                cell[1] = 2;

            return cell;
        }

        private static void move(ref int cellX, ref int cellY)
        {
            switch (currMove)
            {
                case (int)shapes.X:
                    grid[cellX, cellY] = (int)shapes.X;
                    currMove = (int)shapes.O;
                    break;
                case (int)shapes.O:
                    grid[cellX, cellY] = (int)shapes.O;
                    currMove = (int)shapes.X;
                    break;
            }
        }
    }
}
