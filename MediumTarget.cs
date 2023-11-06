using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowGame
{
    public class MediumTarget: Target
    {
        public MediumTarget(Point2D position, double radius) : base(position, radius)
        {
        }
        public Bitmap Bitmap
        {
            get
            {
                return new Bitmap("target", "target.png");
            }
        }
        public override void Render()
        {
            // Render the target circle using the _targetCircle property
            Bitmap targetBitmap = new Bitmap("target", "target.png");


            targetBitmap.Draw(PositionX, PositionY);
            // Render any other visuals for the target, such as rings or decorations
        }
    }
}
