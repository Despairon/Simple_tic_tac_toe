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
            {
                move(ref cellX, ref cellY);
                if (checkForWin())
                    new_game(gridWidth,gridHeight);
            }
        }

        /*** в принципе, функцию можно было сделать намного проще, но я сделал универсальный вариант,
         *   подходящий для игры любой сложности (не только 3х3) 
         ***/
        private static bool checkForWin()
        {
            bool xWins = false;
            bool oWins = false;

            /*** проверяем столбики ***/
            for (int i = 0; i < 3; i++)  
            {
                xWins = true;  
                oWins = true;
                for (int j = 0; j < 3; j++)
                {
                    if (grid[i, j] == (int)shapes.NONE)
                    {
                        xWins = false;
                        oWins = false;
                        break;
                    }
                    if (grid[i, j] == (int)shapes.X)
                    {
                        oWins = false;
                        continue;
                    }
                    else
                    {
                        xWins = false;
                        continue;
                    }
                }
                if (xWins)
                {
                    MessageBox.Show("X wins!");
                    return true;
                }
                if (oWins)
                {
                    MessageBox.Show("O wins!");
                    return true;
                }
            }

            /*** проверяем строки ***/
            for (int i = 0; i < 3; i++)
            {
                xWins = true;
                oWins = true;
                for (int j = 0; j < 3; j++)
                {
                    if (grid[j, i] == (int)shapes.NONE)
                    {
                        xWins = false;
                        oWins = false;
                        break;
                    }
                    if (grid[j, i] == (int)shapes.X)
                    {
                        oWins = false;
                        continue;
                    }
                    else
                    {
                        xWins = false;
                        continue;
                    }
                }
                if (xWins)
                {
                    MessageBox.Show("X wins!");
                    return true;
                }
                if (oWins)
                {
                    MessageBox.Show("O wins!");
                    return true;
                }
            }

            /*** проверяем главную диагональ ***/
            xWins = true;
            oWins = true;
            for (int i = 0; i < 3; i++)
            {
                if (grid[i, i] == (int)shapes.NONE)
                {
                    xWins = false;
                    oWins = false;
                    break;
                }
                if (grid[i, i] == (int)shapes.X)
                {
                    oWins = false;
                    continue;
                }
                else
                {
                    xWins = false;
                    continue;
                }
            }
            if (xWins)
            {
                MessageBox.Show("X wins!");
                return true;
            }
            if (oWins)
            {
                MessageBox.Show("O wins!");
                return true;
            }

            /*** проверяем побочную диагональ ***/
            xWins = true;
            oWins = true;
            for (int i = 2, j = 0; i >= 0; i--, j++)
            {
                if (grid[i, j] == (int)shapes.NONE)
                {
                    xWins = false;
                    oWins = false;
                    break;
                }
                if (grid[i, j] == (int)shapes.X)
                {
                    oWins = false;
                    continue;
                }
                else
                {
                    xWins = false;
                    continue;
                }
            }
            if (xWins)
            {
                MessageBox.Show("X wins!");
                return true;
            }
            if (oWins)
            {
                MessageBox.Show("O wins!");
                return true;
            }

            return false;
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
            int x;
            int y;
            translateCellToCoords(ref cellX, ref cellY, out x, out y);
            switch (currMove)
            {
                case (int)shapes.X:
                    Render.addX(ref x, ref y);
                    grid[cellX, cellY] = (int)shapes.X;
                    currMove = (int)shapes.O;
                break;
                case (int)shapes.O:
                    Render.addO(ref x, ref y);
                    grid[cellX, cellY] = (int)shapes.O;
                    currMove = (int)shapes.X;
                break;
            }
        }

        private static void translateCellToCoords(ref int cellX, ref int cellY, out int x, out int y)
        {
            switch (cellX)
            {
                case 0:
                    x = (int)((0.33f / 2) * gridWidth);
                break;
                case 1:
                    x = (int)(( (0.33f + 0.66f) / 2) * gridWidth);
                break;
                case 2:
                    x = (int)(( (0.66f + 0.99f) / 2) * gridWidth);
                break;
                default:
                    x = -1;
                break;
            }

            switch (cellY)
            {
                case 0:
                    y = (int)((0.33f / 2) * gridHeight);
                    break;
                case 1:
                    y = (int)(((0.33f + 0.66f) / 2) * gridHeight);
                    break;
                case 2:
                    y = (int)(((0.66f + 0.99f) / 2) * gridHeight);
                    break;
                default:
                    y = -1;
                break;
            }
        }

    }
}
