using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowGame
{
    public class MainScreen
    {
        private List<Button> _buttons;
        private bool _isRunning;
        private string _mode = "Exit";

        public bool IsRunning
        { get { return _isRunning; }set { _isRunning = value; } }
        public string Mode
        {
            get { return _mode; }set { _mode = value; }
        }
        public MainScreen()
        {
            _buttons = new List<Button>();
            _isRunning = true;
        }

        public void AddButton(Button button)
        {
            _buttons.Add(button);
        }

        public void Run()
        {
            while (_isRunning)
            {
                SplashKit.ProcessEvents();
                SplashKit.ClearScreen();

                if (SplashKit.MouseClicked(MouseButton.LeftButton))
                {
                    Point2D mousePosition = SplashKit.MousePosition();
                    foreach (Button button in _buttons)
                    {
                        if (button.IsClicked(mousePosition))
                        {
                            HandleButtonAction(button);
                            break;
                        }
                    }
                }
                SplashKit.DrawText("THE BEST ARROW GAME", Color.Black, SplashKit.FontNamed("C:\\Users\\User\\Desktop\\y2\\oop\\it2\\assign\\BowGame\\resources\\fonts\\DiloWorld-mLJLv.ttf"), 50, 165, 70);
                foreach (Button button in _buttons)
                {
                    button.DrawButton();
                }

                SplashKit.RefreshScreen();
            }
            SplashKit.ClearScreen();

        }

        private void HandleButtonAction(Button button)
        {
            // Handle button actions based on button text or any other logic
            if (button.Text == "Easy Mode")
            {
                // Start the game in easy mode
                _mode = "Easy";

            }
            else if (button.Text == "Medium Mode")
            {
                // Start the game in medium mode
                _mode = "Medium";


            }
            else if (button.Text == "Hard Mode")
            {
                // Start the game in hard mode
                _mode = "Hard";

            }
            else if (button.Text == "Exit")
            {
                _mode = "Exit";
            }
                _isRunning = false;
        }
    }

}
