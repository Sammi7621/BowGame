using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowGame
{
    public abstract class Target : GameObject
    {
        private Circle _targetCircle;
        public Target(Point2D position,double radius) : base(position)
        {
            _targetCircle = SplashKit.CircleAt(position, radius);
        }

        public override abstract void Render();

        public Circle TargetCircle
        {
            get
            {
                return _targetCircle;
            }
        }
        public bool HasBeenHit(Arrow arrow)
        {
            // Perform collision detection between the arrow and the target
            Bitmap arrowBitmap = new Bitmap("arrow", "arrow.png");
            Bitmap targetBitmap = new Bitmap("target", "target.png");

            return SplashKit.BitmapCollision(arrowBitmap, arrow.PositionX, arrow.PositionY, targetBitmap, PositionX, PositionX);
        }

        public virtual int GetPointsEarned(Arrow arrow, double Power)
        {
            return Convert.ToInt32(Power / (TargetCircle.Radius/10));
        }
    }
}
