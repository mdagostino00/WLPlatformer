using Godot;
using System;

public partial class PlayerAnimationPlayer : AnimationPlayer
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public void SetWalking(Vector2 velocity)
    {
        this.Play("walking");
        //this.FlipH = velocity.x < 0;
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
