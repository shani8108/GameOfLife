// See https://aka.ms/new-console-template for more information
Console.WriteLine("!!! game of life !!!");

Console.Write("if you want intersing shape press 1 else 0:");
int shape = int.Parse(Console.ReadLine());
int sizeofboard;
if (shape == 1)
    sizeofboard = 18;
else
{
    Console.Write("insert size of board:");
    sizeofboard = int.Parse(Console.ReadLine());
    sizeofboard += 2;
}
Console.Write("insert speed of game in second:");
double speedofrunningD = double.Parse(Console.ReadLine());
speedofrunningD *= 1000;
int speedofrunning = Convert.ToInt32(speedofrunningD);
//set board
int[,,] mainBoards = new int[3, sizeofboard, sizeofboard];
Random rn = new Random();
if (shape == 1)
    intersingShape();
else
{
    Console.Write("if you want a law seed press 1, if you want mideum seed press 2, if you want large seed press 3:");
    int seed = int.Parse(Console.ReadLine());
    switch (seed)
    {
        case 1:
            seedLaw();
            break;
        case 2:
            randomShape();
            break;
        case 3:
            seedLarge();
            break;
        default:
            randomShape();
            break;
    }
    //set the walls active or not active
    Console.Write("if you want to set the walls alive so press 1 if not press 0:");
    int wallsActive = int.Parse(Console.ReadLine());
    for (int i = 0, j = 0; i < sizeofboard; i++)
    {
        mainBoards[0, i, j] = wallsActive;
        mainBoards[0, j, i] = wallsActive;
    }
    for (int j = sizeofboard - 1, i = sizeofboard - 1; j > 0; j--)
    {
        mainBoards[0, i, j] = wallsActive;
        mainBoards[0, j, i] = wallsActive;
    }
}

bool gameOver = false;
while (!gameOver)
{
    Game();
    gameOver = checkGameOver();
}
Console.WriteLine("game over!!!");




void intersingShape()
{
    Console.Write("choose shape to view(1 or 2):");
    if (int.Parse(Console.ReadLine()) == 1)
    {
        for (int i = 0; i < sizeofboard - 1; i++)
        {
            for (int j = 0; j < sizeofboard - 1; j++)
            {
                if ((j == 2 || j == 7 || j == 9 || j == 14) && (i == 4 || i == 5 || i == 6 || i == 10 || i == 11 || i == 12))
                    mainBoards[0, i, j] = 1;
                else
                    if ((i == 2 || i == 7 || i == 9 || i == 14) && (j == 4 || j == 5 || j == 6 || j == 10 || j == 11 || j == 12))
                    mainBoards[0, i, j] = 1;
                else
                    mainBoards[0, i, j] = 0;
            }
        }
    }
    else
    {
        for (int i = 0; i < sizeofboard - 1; i++)
        {
            for (int j = 0; j < sizeofboard - 1; j++)
            {
                if ((i == 2 || i == 7 || i == 10 || i == 15) && (j == 4 || j == 5 || j == 6))
                    mainBoards[0, i, j] = 1;
                else
                    if ((j == 3 || j == 7) && (i == 4 || i == 5 || i == 12 || i == 13))
                    mainBoards[0, i, j] = 1;
                else
                    mainBoards[0, i, j] = 0;
            }
        }
    }

}

void randomShape()
{
    //set the start of board random values.

    for (int i = 1; i < sizeofboard - 1; i++)
    {
        for (int j = 1; j < sizeofboard - 1; j++)
        {
            mainBoards[0, i, j] = rn.Next(0, 2);
        }
    }
}

bool checkGameOver()
{
    for (int i = 1; i < sizeofboard - 1; i++)
    {
        for (int j = 1; j < sizeofboard - 1; j++)
        {
            if (mainBoards[2, i, j] != mainBoards[0, i, j])
            {
                return false;
            }
        }
    }
    return true;
}

void Game()
{
    Console.Clear();
    PrintBoard();
    int[,] NewBoard = new int[sizeofboard, sizeofboard];
    for (int i = 1; i < sizeofboard - 1; i++)
    {
        for (int j = 1; j < sizeofboard - 1; j++)
        {
            int sumLive = GetSumLive(i, j);
            //int sumInActive=GetSumInActive(i, j);
            switch (sumLive)
            {
                case 2:
                    NewBoard[i, j] = mainBoards[0, i, j];
                    break;
                case 3:
                    NewBoard[i, j] = 1;
                    break;
                default:
                    NewBoard[i, j] = 0;
                    break;
            }
        }
    }
    //move the data in the main back
    for (int i = 1; i < sizeofboard - 1; i++)
    {
        for (int j = 1; j < sizeofboard - 1; j++)
        {
            mainBoards[2, i, j] = mainBoards[1, i, j];
            mainBoards[1, i, j] = mainBoards[0, i, j];
            mainBoards[0, i, j] = NewBoard[i, j];
        }
    }
    System.Threading.Thread.Sleep(speedofrunning);
}

int GetSumLive(int v1, int v2)
{
    int sumLive = 0;
    for (int i = v1 - 1; i <= v1 + 1; i++)
    {
        for (int j = v2 - 1; j <= v2 + 1; j++)
        {
            if (!(i == v1 && j == v2))
            {
                sumLive += mainBoards[0, i, j];
            }
            if (sumLive > 3)
            {
                return sumLive;
            }
        }
    }
    return sumLive;
}

void PrintBoard()
{
    Console.WriteLine("!!! game of life !!!");
    Console.WriteLine(" By Shani Shtitzer");
    Console.WriteLine();
    for (int i = 1; i < sizeofboard - 1; i++)
    {
        for (int j = 0; j < sizeofboard; j++)
            if (mainBoards[0, i, j] == 1)
                Console.Write("{0,2}", "*");
            else
                Console.Write("{0,2}", " ");
        Console.WriteLine();
    }
}

void seedLaw()
{
    for (int i = 1; i < sizeofboard - 1; i++)
    {
        for (int j = 1; j < sizeofboard - 1; j++)
        {
            if (rn.Next(0, 2) == 1)
            {
                if (rn.Next(0, 2) == 1)
                    mainBoards[0, i, j] = 1;
                else
                    mainBoards[0, i, j] = 0;
            }
            else
            {
                mainBoards[0, i, j] = 0;
            }
        }
    }
}

void seedLarge()
{
    for (int i = 1; i < sizeofboard - 1; i++)
    {
        for (int j = 1; j < sizeofboard - 1; j++)
        {
            if (rn.Next(0, 2) == 1)
            {
                if (rn.Next(0, 2) == 1)
                    mainBoards[0, i, j] = 0;
                else
                    mainBoards[0, i, j] = 1;
            }
            else
            {
                mainBoards[0, i, j] = 1;
            }
        }
    }
}







