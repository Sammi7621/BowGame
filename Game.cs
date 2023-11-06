using BowGame;
using SplashKitSDK;
using System;
using static System.Formats.Asn1.AsnWriter;

public class Game
{
    private Window _window;
    private Arrow _arrow;
    private Target _target;
    private bool _arrowShot = false;
    private int pointsEarned = 0;
    private int[] _Highscore;
    private int _ingamehighscore;

    private MainScreen _mainScreen;
    private int _Narrowshot;
    private Random rnd;
    private IButton? DisplayButton;
    private double power;

    public Game()
    {
        _window = new Window("Bow Game", 800, 600);
        _arrow = new Arrow();
        _target = new LargeTarget(SplashKit.PointAt(500, 200), 50);
        _mainScreen = new MainScreen();
        rnd = new Random();
        _Narrowshot = 0;
        _ingamehighscore = 0;
        _Highscore =new int[] {0,0,0};

    }

    public void Run()
    {
        StreamReader reader1 = new StreamReader("C:\\Users\\User\\Desktop\\y2\\oop\\it2\\assign\\BowGame\\resources\\data\\highscore.txt");
        for (int i=0;i<3; i++)
        {
            _Highscore[i] = reader1.ReadInteger();
        }
        reader1.Close();
        SplashKit.ProcessEvents();

        do
        {
            if(_mainScreen.IsRunning)
            {
                SplashKit.ClearScreen();

                Button MainScreenButton;
                 MainScreenButton = new Button(SplashKit.RectangleFrom(300, 250, 200, 50), "Easy Mode");
                _mainScreen.AddButton(MainScreenButton);
                MainScreenButton = new Button(SplashKit.RectangleFrom(300, 325, 200, 50), "Medium Mode");
                _mainScreen.AddButton(MainScreenButton);
                MainScreenButton = new Button(SplashKit.RectangleFrom(300, 400, 200, 50), "Hard Mode");
                _mainScreen.AddButton(MainScreenButton);
                MainScreenButton = new Button(SplashKit.RectangleFrom(300, 475, 200, 50), "Exit");
                _mainScreen.AddButton(MainScreenButton);
                _mainScreen.Run();
                if(_mainScreen.Mode == "Exit")
                {
                    break;
                }
                else if(_mainScreen.Mode == "Easy")
                {
                    _target = new LargeTarget(SplashKit.PointAt(500, 200), 100);
                    _ingamehighscore = _Highscore[0];
                }
                else if (_mainScreen.Mode == "Medium")
                {
                    _target = new MediumTarget(SplashKit.PointAt(500, 200), 80);
                    _ingamehighscore = _Highscore[1];

                }
                else
                {
                    _target = new SmallTarget(SplashKit.PointAt(500, 200), 40);
                    _ingamehighscore = _Highscore[2];

                }
            }
            if(_Narrowshot == 6 )
            {
                DisplayButton = new ButtonDisplay(SplashKit.RectangleFrom(200, 150, 400, 300), ("!NEW HIGH SCORE!"), 50);
                if (pointsEarned > _Highscore[0] && _mainScreen.Mode == "Easy")
                {
                    _Highscore[0] = pointsEarned;
                    ShowResult(DisplayButton);
                    SaveTo(_Highscore);
                }
                if (pointsEarned > _Highscore[1] && _mainScreen.Mode == "Medium")
                {
                    _Highscore[1] = pointsEarned;
                    ShowResult(DisplayButton);
                    SaveTo(_Highscore);
                }
                if (pointsEarned > _Highscore[2] && _mainScreen.Mode == "Hard")
                {
                    _Highscore[2] = pointsEarned;
                    ShowResult(DisplayButton);
                    SaveTo(_Highscore);
                }
                else
                {
                    DisplayButton = new ButtonDisplay(SplashKit.RectangleFrom(200, 150, 400, 300), ("!GAME OVER!"), 50);
                    ShowResult(DisplayButton);
                }
                _ingamehighscore = pointsEarned;
                _Narrowshot = 0;
                pointsEarned = 0;
            }
            if(SplashKit.KeyTyped(KeyCode.BackspaceKey))
            {
                _mainScreen.IsRunning = true;
                _Narrowshot = 0;
                pointsEarned = 0;
            }
            SplashKit.ProcessEvents();
            SplashKit.ClearScreen();
            HandleInput();
            Update();
            Render();
            SplashKit.RefreshScreen();

        }
        while (!_window.CloseRequested);
    }
    private void SaveTo(int[] highscore)
    {
        StreamWriter writer1;
        writer1 = new StreamWriter("C:\\Users\\User\\Desktop\\y2\\oop\\it2\\assign\\BowGame\\resources\\data\\highscore.txt");
        writer1.WriteLine(highscore[0]);
        writer1.WriteLine(highscore[1]);
        writer1.WriteLine(highscore[2]);
        writer1.Close();
    }
    private void ShowResult(IButton DisplayButton)
    {
        SplashKit.ClearScreen();
        DisplayButton.DrawButton();
        while (!SplashKit.KeyTyped(KeyCode.SpaceKey))
        {
            SplashKit.ProcessEvents();
            SplashKit.RefreshScreen();
        }
    }
    private void HandleInput()
    {
        if (!_arrowShot  && SplashKit.MouseDown(MouseButton.LeftButton))
        {
            Point2D startPos = SplashKit.MousePosition();

            while (SplashKit.MouseDown(MouseButton.LeftButton))
            {
                SplashKit.ProcessEvents();
            }

            Point2D endPos = SplashKit.MousePosition();

            double deltaX = endPos.X - startPos.X;
            double deltaY = endPos.Y - startPos.Y;
            while (deltaX == 0 && deltaY == 0)
            {
                if (!_arrowShot && SplashKit.MouseDown(MouseButton.LeftButton))
                {
                     startPos = SplashKit.MousePosition();

                    while (SplashKit.MouseDown(MouseButton.LeftButton))
                    {
                        SplashKit.ProcessEvents();
                    }

                     endPos = SplashKit.MousePosition();

                     deltaX = endPos.X - startPos.X;
                     deltaY = endPos.Y - startPos.Y;
                }
            }
            power = Math.Sqrt(deltaX * deltaX + deltaY * deltaY) * 0.1;
            double angle = Math.Atan2(deltaY, deltaX) * (180 / Math.PI);

            // Invert the angle
            angle = -angle;
            _arrow.Shoot(power, angle);
            _arrowShot = true;  // Set the flag to indicate arrow is shot
        }

        if (!_arrowShot && SplashKit.KeyTyped(KeyCode.RKey))
        {
            // Reset the game
            _arrow = new Arrow();
            _arrowShot = false;  // Reset the flag
            RndTarget();
        }
    }

    private void Update()
    {
        if (_arrowShot)
        {
            _arrow.Update();

            if (_arrow.HasCollided(_target))
            {
                pointsEarned = pointsEarned + _target.GetPointsEarned(_arrow,power);
                Console.WriteLine("Points earned: " + pointsEarned);
                _arrow = new Arrow();
                _arrowShot = false;  // Reset the flag to allow shooting again
                RndTarget();
                _Narrowshot++;

            }

            // Check if arrow is off the screen
            if (_arrow.PositionX < 0 || _arrow.PositionX > _window.Width ||
                _arrow.PositionY < 0 || _arrow.PositionY > _window.Height)
            {
                Console.WriteLine("Arrow missed the target!");
                _arrow = new Arrow();
                _arrowShot = false;  // Reset the flag to allow shooting again
                RndTarget();
                _Narrowshot++;

            }
        }
    }
    private void RndTarget()
    {

        if (_mainScreen.Mode == "Easy")
        {
            _target.PositionX = rnd.Next(50, 700);
            _target.PositionY = rnd.Next(200, 500);
        }
        else if (_mainScreen.Mode == "Medium")
        {
            _target.PositionX = rnd.Next(300, 700);
            _target.PositionY = rnd.Next(150, 400);

        }
        else
        {
            _target.PositionX = rnd.Next(400, 700);
            _target.PositionY = rnd.Next(150, 350);
        }
    }

    private void Render()
    {
        _window.Clear(Color.White);
        DisplayButton = new ButtonDisplay(SplashKit.RectangleFrom(20, 40, 200, 30), (_mainScreen.Mode+" arrowShot: " + _Narrowshot));
        DisplayButton.DrawButton();
        DisplayButton = new ButtonDisplay(SplashKit.RectangleFrom(250, 40, 225, 30), ("Points earned: " + pointsEarned));
        DisplayButton.DrawButton();
        DisplayButton = new ButtonDisplay(SplashKit.RectangleFrom(505, 40, 225, 30), ("Highscore: " + _ingamehighscore));
        DisplayButton.DrawButton();
        DisplayButton = new ButtonDisplay(SplashKit.RectangleFrom(20, 80, 200, 15), (_mainScreen.Mode + " mode "),20);
        DisplayButton.DrawButton();


        _arrow.Render();
        _target.Render();
        _window.Refresh();
    }
}
