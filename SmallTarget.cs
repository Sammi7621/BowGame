using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowGame
{
    public class SmallTarget : Target
    {
        public SmallTarget(Point2D position, double radius):base(position,radius)
        {
        }
        public override void Render()
        {
            // Render the target circle using the _targetCircle property
            Bitmap targetBitmap = new Bitmap("target", "target_small.png");


            targetBitmap.Draw(PositionX, PositionY);
            // Render any other visuals for the target, such as rings or decorations
        }
    }
}
