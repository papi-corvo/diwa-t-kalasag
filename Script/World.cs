using Godot;
using System;

public partial class World : Node2D
{
    [Export] private Label FPS;

    public override void _Process(double delta)
    {
        // Update the FPS label with the current frames per second
        FPS.Text = $"FPS: {Engine.GetFramesPerSecond()}";
    }
}
