using Godot;
using System;

public partial class Key : Treasure
{
    public AnimationPlayer animationPlayer;
    [Signal] public delegate void KeyObtainedEventHandler();

    public override void _Ready()
	{
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        animationPlayer.Play("idle");
        base._Ready();
    }

    public override void OnBodyEntered(Player body)
    {
        sfx.Play();
        GetTree().Paused = true;
        animationPlayer.Play("bounce");
        ZIndex = 2;
        SetCollisionMaskValue(1, false); //disable collision box
        player = body;
    }

    public void OnAnimationPlayerAnimationFinished(string anim_name)
    {
        EmitSignal("KeyObtained");
        GetTree().Paused = false;
        this.QueueFree();
    }
}
