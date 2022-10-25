using Godot;
using System;

public partial class Treasure : Area2D
{
    public void OnBodyEntered(Node2D body)
    {
        this.QueueFree();
    }
}
