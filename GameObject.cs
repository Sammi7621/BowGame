using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowGame
{
    public abstract class GameObject
    {
        private Point2D _position;
        public GameObject(Point2D position)
        {
            _position = position;
        }
        public double PositionX
        {
            get
            {
                return _position.X;
            }
            set
            {
                _position.X = value;
            }
        }
        public double PositionY
        {
            get
            {
                return _position.Y;
            }
            set
            {
                _position.Y = value;
            }
        }
        public abstract void Render();
    }
}
