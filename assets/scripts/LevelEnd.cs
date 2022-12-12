using Godot;
using System;

public partial class LevelEnd : Area2D
{
    public CollisionShape2D CollisionShape;
    [Signal] public delegate void OnLevelEndEventHandler();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        this.Visible = false;
        CollisionShape = GetNode<CollisionShape2D>("CollisionShape2D");
        CollisionShape.Disabled = true;
	}

    public void OnPlayerKeyObtained()
    {
        this.Visible = true;
        CollisionShape.Disabled = false;
        GetNode<AnimationPlayer>("AnimationPlayer").Play("idle");
    }

    public void OnBodyEntered(Player body)
    {
        EmitSignal("OnLevelEnd");
        GD.Print("You Win!");
        //this.QueueFree();
        //GD.Print(body.GetType());
    }
}
