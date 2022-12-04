using Godot;
using System;

public partial class Enemy : CharacterBody2D
{ 
    public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	protected AnimationPlayer _animationPlayer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		_animationPlayer.Play("walk");
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override void _PhysicsProcess(double delta)
	{
        Vector2 velocity = this.Velocity;
		if (!IsOnFloor()) 
			velocity.y += gravity * (float)delta;

        Velocity = velocity;
        MoveAndSlide();
    }
}
