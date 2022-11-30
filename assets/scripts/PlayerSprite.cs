using Godot;
using System;

public partial class PlayerSprite : AnimatedSprite2D
{
    public void SetWalking(Vector2 velocity)
    {
        this.Play("walking");
        this.FlipH = velocity.x < 0;
    }

    public void SetJumping(Vector2 velocity)
    {
        if (velocity.y < 0)
            this.Play("jumping_up");
        else
            this.Play("jumping_down");
    }

    public void SetIdle()
    {
        this.Play("idle");
    }

}
