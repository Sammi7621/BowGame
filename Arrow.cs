using BowGame;
using SplashKitSDK;
using System;

public class Arrow : GameObject
{
    private Vector2D _velocity;
    private double _power;
    private double _angle;
    private bool _isShot;
    private bool _isCollided;

    public Arrow() : base(SplashKit.PointAt(0, 550))
    {
        _velocity = new Vector2D();
        _power = 0;
        _angle = 0;
        _isShot = false;
        _isCollided = false;
    }

    public void Update()
    {
        if (_isShot && !_isCollided)
        {
            // Update the arrow's position based on its velocity
            PositionX += _velocity.X;
            PositionY += _velocity.Y;
        }
    }

    public override void Render()
    {
        // Render the arrow as an image or shape
        Bitmap arrowBitmap = new Bitmap("arrow", "arrow.png");
        arrowBitmap.Draw(PositionX, PositionY);

    }

    public void Shoot(double power, double angle)
    {
        _power = power;
        _angle = angle;

        // Convert power and angle to velocity
        double radians = _angle * Math.PI / 180.0;
        _velocity.X = _power * Math.Cos(radians)*0.01;
        _velocity.Y = -_power * Math.Sin(radians) * 0.01;

        _isShot = true;
    }

    public bool HasCollided(Target target)
    {
        if (_isShot && !_isCollided)
        {
            // Perform collision detection between the arrow and the target
            Bitmap arrowBitmap = new Bitmap("arrow", "arrow.png");
            Bitmap targetBitmap = new Bitmap("target", "target.png");

            bool collided = SplashKit.BitmapCollision(arrowBitmap, PositionX, PositionY, targetBitmap, target.PositionX, target.PositionY);

            if (collided)
            {
                _isCollided = true;
                _isShot = false;  // Stop the arrow from moving
            }

            return collided;
        }
        return false;
    }
}
