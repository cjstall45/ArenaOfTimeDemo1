using System;
using System.Collections.Generic;
using System.Text;

namespace ArenaOfTimeDemo1
{
    /// <summary>
    /// an enum used to show which animation a sprite is currently playing
    /// </summary>
    public enum AnimationState
    {
        idle,
        walking,
        backingup,
        block,
        attack1,
        hit,
        dead
    }
}
