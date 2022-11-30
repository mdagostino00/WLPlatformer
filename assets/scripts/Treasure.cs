using Godot;
using System;

public partial class Treasure : Area2D
{
    [Export] int TREASURE_VALUE = 500;

    public void OnBodyEntered(Node2D body)
    {
        this.QueueFree();
    }
}
