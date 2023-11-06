using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowGame
{
    internal class LargeTarget: Target
    {
        public LargeTarget(Point2D position, double radius) : base(position, radius)
        {
        }
        public Bitmap Bitmap
        {
            get
            {
                return new Bitmap("target", "target_big.png");
            }
        }
        public override void Render()
        {
            // Render the target circle using the _targetCircle property
            Bitmap targetBitmap = new Bitmap("target", "target_big.png");


        targetBitmap.Draw(PositionX, PositionY);
            // Render any other visuals for the target, such as rings or decorations
        }
        public override int GetPointsEarned(Arrow arrow, double Power)
        {
            return Convert.ToInt32(Power / (TargetCircle.Radius / 8));
        }
    }

}
