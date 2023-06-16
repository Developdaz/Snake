using System; // Подключение пространства имен System, которое содержит основные классы и функции для работы с .NET Framework.
class Program
{ // Объявление класса Program, который содержит точку входа в программу.
    static int widthgame = 40, heightgame = 20;
    static int scrgame1 = 0, scrgame2 = 0;
    static int speed = 80;
    static bool gg1 = false;
    struct Position
    { // Объявление структуры Position, которая содержит два публичных целочисленных поля x и y, и конструктор с двумя параметрами.
        public int x;
        public int y;
        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
    enum Move
    { // Объявление перечисления Move, которое содержит четыре элемента: Up, Left, Down, Right.
        Up,
        Left,
        Down,
        Right
    }
    static void Menu()
    { // Объявление статической функции Menu, который выводит приветствие и предлагает пользователю начать игру.
        while (true)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("HELLO");
            Console.WriteLine("WELCOME TO THE SNAKE!");
            Console.WriteLine();
            Console.WriteLine("ENTER - PLAY");
            ConsoleKeyInfo keyusing = Console.ReadKey();
            if (keyusing.Key == ConsoleKey.Enter)
            {
                break;
            }
        }
    }
    static void Restart()
    { // Объявление статической функции Restart, который сбрасывает значения переменных и вызывает методы OutPutConsole() и StartGame().
        scrgame1 = 0;
        scrgame2 = 0;
        speed = 80;
        gg1 = false;
        OutPutConsole();
        StartGame();
    }
    static void EndGame()
    { // Объявление статической функции EndGame, который выводит результаты игры и предлагает пользователю перезапустить или выйти.
        while (true)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("YOUR SCORE: {0}", scrgame1);
            Console.WriteLine("ROBOT'S SCORE: {0}", scrgame2);
            Console.WriteLine();
            if (scrgame1 > scrgame2)
            {
                Console.WriteLine("YOU WIN!");
            }
            else if (scrgame1 < scrgame2)
            {
                Console.WriteLine("ROBOT WINS!");
            }
            else Console.WriteLine("TIE!");
            Console.WriteLine();
            Console.WriteLine("ENTER - RESTART");
            Console.WriteLine("ESC - EXIT");
            ConsoleKeyInfo keyusing = Console.ReadKey();
            if (keyusing.Key == ConsoleKey.Escape)
            {
                break;
            }
            else if (keyusing.Key == ConsoleKey.Enter)
            {
                Restart();
            }
        }
    }
    static void StartGame()
    { // Объявление статической функции StartGame, который содержит основной игровой цикл.
        int space1 = 16;
        int Length1 = 3;
        int Length2 = 3;
        Position[] snake1 = new Position[100];
        Position[] snake2 = new Position[100];
        snake1[0] = new Position((widthgame / 2), (heightgame / 2));
        snake2[0] = new Position(((widthgame / 2) + 2 + (space1) + widthgame), (heightgame / 2));
        CreateSnake(snake1, Length1, ConsoleColor.White);
        CreateSnake(snake2, Length2, ConsoleColor.White);
        Move movesnake1 = Move.Right;
        Move movesnake2 = Move.Right;
        Position food1 = Food(0 + 1, snake1, Length1);
        Position food2 = Food(((widthgame + 2 + space1) + 1), snake2, Length2);
        CreateFood(food1);
        CreateFood(food2);
        while (!gg1)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.W:
                        if (movesnake1 != Move.Down)
                            movesnake1 = Move.Up;
                        break;
                    case ConsoleKey.A:
                        if (movesnake1 != Move.Right)
                            movesnake1 = Move.Left;
                        break;
                    case ConsoleKey.S:
                        if (movesnake1 != Move.Up)
                            movesnake1 = Move.Down;
                        break;
                    case ConsoleKey.D:
                        if (movesnake1 != Move.Left)
                            movesnake1 = Move.Right;
                        break;
                }
            }
            if (snake2[0].x == widthgame + 2 + 1 + space1)
            {
                if (movesnake2 != Move.Left)
                {
                    if (snake2[0].y == 1)
                    {
                        if (movesnake2 != Move.Up)
                        {
                            movesnake2 = Move.Down;
                        }
                        else
                        {
                            movesnake2 = Move.Right;
                        }
                    }
                    else if (snake2[0].y == (heightgame - 1))
                    {
                        if (movesnake2 != Move.Down)
                        {
                            movesnake2 = Move.Up;
                        }
                        else
                        {
                            movesnake2 = Move.Right;
                        }
                    }
                }
            }
            else if (snake2[0].x == (widthgame + widthgame + 1 + space1))
            {
                if (movesnake2 != Move.Right)
                {
                    if (snake2[0].y == 1)
                    {
                        if (movesnake2 != Move.Up)
                        {
                            movesnake2 = Move.Down;
                        }
                        else
                        {
                            movesnake2 = Move.Left;
                        }
                    }
                    else if (snake2[0].y == (heightgame - 1))
                    {
                        if (movesnake2 != Move.Down)
                        {
                            movesnake2 = Move.Up;
                        }
                        else
                        {
                            movesnake2 = Move.Left;
                        }
                    }
                }
            }
            if ((snake2[0].x >= food2.x) && (movesnake2 == Move.Down || movesnake2 == Move.Up))
            {
                Position nextPosition = new Position(snake2[0].x - 1, snake2[0].y);
                Position nextPosition2 = new Position(snake2[0].x, snake2[0].y + 1);
                if (!SnakeTail(nextPosition, snake2, Length2))
                {
                    if (!SnakeTail(nextPosition2, snake2, Length2))
                    {
                        movesnake2 = Move.Up;
                    }
                    else
                    {
                        movesnake2 = Move.Down;
                    }
                    movesnake2 = Move.Left;
                }
            }
            else if ((snake2[0].x <= food2.x) && (movesnake2 == Move.Down || movesnake2 == Move.Up))
            {
                Position nextPosition = new Position(snake2[0].x + 1, snake2[0].y);
                Position nextPosition2 = new Position(snake2[0].x, snake2[0].y + 1);
                if (!SnakeTail(nextPosition, snake2, Length2))
                {
                    if (!SnakeTail(nextPosition2, snake2, Length2))
                    {
                        movesnake2 = Move.Up;
                    }
                    else
                    {
                        movesnake2 = Move.Down;
                    }
                    movesnake2 = Move.Right;
                }
            }
            else if ((snake2[0].y >= food2.y) && (movesnake2 == Move.Left || movesnake2 == Move.Right))
            {
                Position nextPosition = new Position(snake2[0].x, snake2[0].y - 1);
                Position nextPosition2 = new Position(snake2[0].x + 1, snake2[0].y);
                if (!SnakeTail(nextPosition, snake2, Length2))
                {
                    if (!SnakeTail(nextPosition2, snake2, Length2))
                    {
                        movesnake2 = Move.Left;
                    }
                    else
                    {
                        movesnake2 = Move.Right;
                    }
                    movesnake2 = Move.Up;
                }
            }
            else if ((snake2[0].y <= food2.y) && (movesnake2 == Move.Left || movesnake2 == Move.Right))
            {
                Position nextPosition = new Position(snake2[0].x, snake2[0].y + 1);
                Position nextPosition2 = new Position(snake2[0].x + 1, snake2[0].y);
                if (!SnakeTail(nextPosition, snake2, Length2))
                {
                    if (!SnakeTail(nextPosition2, snake2, Length2))
                    {
                        movesnake2 = Move.Left;
                    }
                    else
                    {
                        movesnake2 = Move.Right;
                    }
                    movesnake2 = Move.Down;
                }
            }
            bool SnakeTail(Position position, Position[] snake, int snakeLength)
            {
                for (int i = 0; (i < snakeLength); i++)
                {
                    if (snake[i].x == position.x && snake[i].y == position.y)
                        return true;
                }
                return false;
            }
            SnakeMoving(snake1, Length1, movesnake1);
            SnakeMoving(snake2, Length2, movesnake2);
            if ((Check(snake1, Length1)) || (Check(snake2, Length2)))
            {
                gg1 = true;
            }
            if ((snake1[0].x == food1.x && snake1[0].y == food1.y) || (snake2[0].x == food2.x && snake2[0].y == food2.y))
            {
                if (speed > 30)
                {
                    speed = (speed - 2);
                }
            }
            if (snake1[0].x == food1.x && snake1[0].y == food1.y)
            {
                scrgame1++;
                Length1++;
                for (int i = 0; (i < Length1); i++)
                {
                    if ((food1.x == snake1[i].x) && (food1.y == snake1[i].y))
                    {
                        food1 = Food(0, snake1, Length1);
                    }
                }
                CreateFood(food1);
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition((widthgame + 2) / 2, heightgame + 2);
                Console.WriteLine(scrgame1);
            }
            if (snake2[0].x == food2.x && snake2[0].y == food2.y)
            {
                scrgame2++;
                Length2++;
                for (int i = 0; (i < Length2); i++)
                {
                    if ((food2.x == snake2[i].x) && (food2.y == snake2[i].y))
                    {
                        food2 = Food(widthgame + space1 + 2, snake2, Length2);
                    }
                    if ((food2.x == snake2[0].x && food2.x == snake2[i].x) || (food2.x == snake2[0].x && food2.x == snake2[i].x))
                    {
                        food2 = Food(widthgame + space1 + 2, snake2, Length2);
                    }
                }
                CreateFood(food2);
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(widthgame + 2 + ((widthgame + 2) / 2) + space1, heightgame + 2);
                Console.WriteLine(scrgame2);
            }
            Thread.Sleep(speed);
        }
    }
    static void CreateSnake(Position[] snake, int snakeLength, ConsoleColor color)
    { // Объявление статической функции CreateSnake, который создает змейку на игровом поле.
        for (int i = 0; (i < snakeLength); i++)
        {
            Console.SetCursorPosition(snake[i].x, snake[i].y);
            Console.ForegroundColor = color;
            if (i == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("■");
            }
            else
                Console.Write("■");
        }
    }
    static void OutPutConsole()
    { // Объявление статической функции OutPutConsole, который отображает игровое поле на консоли.
        int space1 = 16;
        int widthfull = (widthgame + 2);
        Console.Clear();
        Console.SetCursorPosition(0, 0);
        for (int i = 0; (i < widthfull); i++)
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("■");
        }
        Console.WriteLine();
        Console.SetCursorPosition(widthfull, 0);
        for (int i = 0; (i < space1); i++)
        {
            Console.Write(" ");
        }
        Console.WriteLine();
        for (int i = 0; (i < heightgame); i++)
        {
            Console.SetCursorPosition(0, (i + 1));
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("█");
            Console.SetCursorPosition((widthgame + 1), (i + 1));
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("█");
        }
        Console.SetCursorPosition(0, (heightgame + 1));
        for (int i = 0; (i < widthfull); i++)
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("■");
        }
        Console.WriteLine();
        Console.SetCursorPosition((widthfull + space1), 0);
        for (int i = 0; (i <= widthfull); i++)
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("■");
        }
        Console.WriteLine();
        for (int i = 0; (i < heightgame); i++)
        {
            Console.SetCursorPosition((widthfull + space1), (i + 1));
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("█");
            Console.SetCursorPosition((widthfull + widthfull + space1), (i + 1));
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("█");
        }
        Console.SetCursorPosition((widthfull + space1), (heightgame + 1));
        for (int i = 0; (i <= widthfull); i++)
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("■");
        }
    }
    static void SnakeMoving(Position[] snake, int snakeLength, Move move)
    { // Объявление статической функции SnakeMoving, который обрабатывает движение змейки.
        Position[] newSnake = new Position[100];
        newSnake[0] = snake[0];
        for (int i = 1; (i < snakeLength); i++)
        {
            newSnake[i] = snake[i - 1];
        }
        switch (move)
        {
            case Move.Up:
                newSnake[0].y--;
                break;
            case Move.Left:
                newSnake[0].x--;
                break;
            case Move.Down:
                newSnake[0].y++;
                break;
            case Move.Right:
                newSnake[0].x++;
                break;
        }
        CreateSnake(snake, snakeLength, ConsoleColor.Black);
        snakeLength = Math.Min(snakeLength, widthgame * heightgame);
        for (int i = 0; (i < snakeLength); i++)
        {
            snake[i] = newSnake[i];
        }
        CreateSnake(snake, snakeLength, ConsoleColor.White);
    }
    static Position Food(int newoneposfood, Position[] snake, int snakeLength)
    { // Объявление статической функции Food, которая задает координаты для новой еды
        Random random = new Random();
        int x = (random.Next(2, (widthgame - 2)) + newoneposfood);
        int y = random.Next(2, (heightgame - 2));
        for (int i = 1; (i < snakeLength); i++)
        {
            if ((snake[i].x == x) && (snake[i].y == y))
            {
                return Food(newoneposfood, snake, snakeLength);
            }
        }
        return new Position(x, y);
    }
    static bool Check(Position[] snake, int snakeLength)
    { // Объявление статической функции Check, которая проверяет двух змеек на столкновение самих с собой и со стенками игровых полей.
        int space1 = 16;
        if ((snake[0].x == 1) || (snake[0].x == widthgame) || (snake[0].y == 0) || (snake[0].y == heightgame) || (snake[0].x == (widthgame + 2 + 1 + space1)) || (snake[0].x == ((widthgame * 2) + 2 + 1 + space1)))
        {
            gg1 = true;
            return true;
        }
        for (int i = 1; (i < snakeLength); i++)
        {
            if ((snake[i].x == snake[0].x) && (snake[i].y == snake[0].y))
            {
                return true;
            }
        }
        return false;
    }
    static void CreateFood(Position food)
    { // Объявление статической функции CreateFood, которая выводит еду на экран по заданным координатам.
        Console.SetCursorPosition(food.x, food.y);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("■");
    }
    static void Main()
    { // Объявление статической основной функции Main, в ходе которой будет выполняться вся программа.
        int space1 = 16;
        int space2 = 10;
        Console.Clear();
        Console.CursorVisible = false;
        Console.Title = "BUCHKAPOKOTILOV";
        Console.SetWindowSize(((((widthgame * 2) + space1 + 6))), (heightgame + space2));
        Console.SetBufferSize((((widthgame * 2) + space1 + 6)), (heightgame + space2));
        Menu();
        OutPutConsole();
        StartGame();
        EndGame();
    }
}