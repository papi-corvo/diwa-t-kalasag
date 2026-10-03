using Godot;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Security;

public partial class PLayerMovement : CharacterBody2D
{
    [Export] private float Speed = 50f;
    [Export] private float Sprint = 85f;
    [Export] private AnimatedSprite2D playerAnimations;
    public override void _PhysicsProcess(double delta)
    {
        // Get the current velocity and apply gravity if not on the floor
        Vector2 velocity = Velocity;

        if (!IsOnFloor())
        {
            velocity.Y += GetGravity().Y * (float)delta;
        }
        else
        {
            if (velocity.Y > 0)
            {
                velocity.Y = 0;
            }
        }

        // Get the input direction and determine the current speed based on whether the sprint action is pressed
        float inputDir = Input.GetAxis("Left", "Right");
        float currentSpeed = Speed;
        if (Input.IsActionPressed("Sprint"))
        {
            currentSpeed = Sprint;
        }

        // Set the horizontal velocity based on the input direction and current speed
        velocity.X = inputDir * currentSpeed;

        if (velocity.X > 0)
        {
            playerAnimations.FlipH = false;
        }
        else if (velocity.X < 0)
        {
            playerAnimations.FlipH = true;
        }

        // Play the appropriate animation based on the input direction
        if (inputDir == 0)
        {
            playerAnimations.Play("idle");
        }
        else
        {
            playerAnimations.Play("walk");
        }

        // Quick exit the game when the "QuickExit" action is just pressed
        if (Input.IsActionJustPressed("QuickExit"))
        {
            GetTree().Quit();
        }

        // Move the character using the calculated velocity
        Velocity = velocity;
        MoveAndSlide();
    }

}
