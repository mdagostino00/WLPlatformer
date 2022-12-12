using Godot;
using System;

public partial class LevelEnd : Area2D
{
    [Signal] public delegate void OnLevelEndEventHandler();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        this.Visible = false;
	}

    public void OnPlayerKeyObtained()
    {
        this.Visible = true;
    }

    public void OnBodyEntered(Player body)
    {
        EmitSignal("OnLevelEnd");
        GD.Print("You Win!");
        //this.QueueFree();
        //GD.Print(body.GetType());
    }
}
