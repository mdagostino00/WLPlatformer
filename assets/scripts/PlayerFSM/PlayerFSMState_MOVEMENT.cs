﻿using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarioLandPlatformer.PlayerFSM
{
    public class PlayerFSMState_MOVEMENT : PlayerFSMState
    {
        public PlayerFSMState_MOVEMENT(Player player) : base(player)
        {
            _id = PlayerFSMStateType.MOVEMENT;
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void _Process()
        {
            base._Process();
        }

        public override void _PhysicsProcess(double delta)
        {
            Vector2 velocity = _player.Velocity;

            // Add the gravity.
            if (!_player.IsOnFloor())
                velocity.y += _player.gravity * (float)delta;

            // Handle Jump.
            if (Input.IsActionJustPressed("jump") && _player.IsOnFloor())
                velocity.y = Player.JumpVelocity;
            if (!Input.IsActionPressed("jump") && velocity.y < -50.0f && velocity.y > -250.0f)
                velocity.y = -50.0f;

            // Get the input direction and handle the movement/deceleration.
            // As good practice, you should replace UI actions with custom gameplay actions.
            Vector2 direction = Input.GetVector(
                "move_left", 
                "move_right", 
                "move_up", 
                "move_down"
                );

            if (direction.x != 0)
            {
                // if the player is holding the sprint button, add sprint speed
                if (Input.IsActionPressed("sprint"))
                {
                    velocity.x += direction.x * Player.Acceleration;
                    velocity.x = Mathf.Clamp(velocity.x, 
                        -Player.SprintSpeed, 
                        Player.SprintSpeed
                        );
                }
                else // add the normal walk speed
                {
                    velocity.x += direction.x * Player.Acceleration;
                    velocity.x = Mathf.Clamp(velocity.x, 
                        -Player.MaxSpeed, 
                        Player.MaxSpeed
                        );
                }

            }
            else
            {
                velocity.x = Mathf.MoveToward(_player.Velocity.x, 0, Player.Acceleration);
            }

            _player.Velocity = velocity;
            _player.SetAnimation(_player.Velocity);
            //base._PhysicsProcess(delta);
        }
    }
}
