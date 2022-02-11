using CollisionExample.Collisions;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArenaOfTimeDemo1.Collisions
{
    public class Hitbox
    {
        public bool Active = true;

        public BoundingRectangle Bounds;

        public Hitbox(float x, float y, float width, float height)
        {
            Bounds = new BoundingRectangle(x, y, width, height);
        }

        public Hitbox(Vector2 position, float width, float height)
        {
            Bounds = new BoundingRectangle(position, width, height);
        }
    }
}
