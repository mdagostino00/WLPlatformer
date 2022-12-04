using Godot;
using System;

public partial class OrbTreasure : Treasure
{
    public AnimationPlayer animationPlayer;
    private bool dequeue = false;

    public override void _Ready()
    {
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        animationPlayer.Play("idle");
        base._Ready();
    }

    public override void OnBodyEntered(Node2D body)
    {
        animationPlayer.Play("bounce");
    }

    public void OnAnimationPlayerAnimationFinished(string anim_name)
    {
        this.QueueFree();
    }
}
