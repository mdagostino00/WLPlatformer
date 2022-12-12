using Godot;
using System;

public partial class Treasure : Area2D
{
    protected AudioStreamPlayer2D sfx;
    protected Player player;
    [Export] public int TREASURE_VALUE = 500;

    public override void _Ready()
    {
        sfx = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
    }

    public virtual async void OnBodyEntered(Player body)
    {
        this.Visible = false;
        SetCollisionMaskValue(1, false);
        body.AddCoins(TREASURE_VALUE);
        sfx.Play();

        await ToSignal(sfx, "finished");
        this.QueueFree();
    }
}
