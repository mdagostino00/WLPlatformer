using Godot;
using System;

public partial class JewelTreasure : Treasure
{
    public AnimationPlayer animationPlayer;

    public override void _Ready()
    {
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        animationPlayer.Play("idle");
        base._Ready();
    }

    public override void OnBodyEntered(Player body)
    {
        animationPlayer.Play("bounce");
        ZIndex = 2;
        SetCollisionMaskValue(1, false); //disable collision box
        player = body;
        body.AddCoins(TREASURE_VALUE);

        sfx.Play();
    }

    public void OnAnimationPlayerAnimationFinished(string anim_name)
    {
        this.QueueFree();
    }
}
