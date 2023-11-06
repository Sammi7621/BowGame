using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowGame
{
    public class Button: IButton
    {
        private Rectangle _bounds;
        private string _text;

        public Button(Rectangle bounds, string text)
        {
            _bounds = bounds;
            _text = text;
        }
        public string Text
        {
            get
            { 
                return _text;
            } 
            set
            {
                _text = value;
            }
        }

        public void DrawButton()
        {
            // Draw button shape
            SplashKit.FillRectangle(Color.Gray, _bounds);

            // Calculate text position
            float textX = (float)(_bounds.X + _bounds.Width / 2 - SplashKit.TextWidth(_text, SplashKit.FontNamed("C:\\Users\\User\\Desktop\\y2\\oop\\it2\\assign\\BowGame\\resources\\fonts\\DiloWorld-mLJLv.ttf"), 20) / 2);
            float textY = (float)(_bounds.Y + _bounds.Height / 2 - SplashKit.TextHeight(_text, SplashKit.FontNamed("C:\\Users\\User\\Desktop\\y2\\oop\\it2\\assign\\BowGame\\resources\\fonts\\DiloWorld-mLJLv.ttf"), 20) / 2);

            // Draw button text
            SplashKit.DrawText(_text, Color.Black, SplashKit.FontNamed("C:\\Users\\User\\Desktop\\y2\\oop\\it2\\assign\\BowGame\\resources\\fonts\\DiloWorld-mLJLv.ttf"), 20, textX, textY);
        }

        public bool IsClicked(Point2D mousePosition)
        {
            if (SplashKit.PointInRectangle(mousePosition, _bounds))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}
