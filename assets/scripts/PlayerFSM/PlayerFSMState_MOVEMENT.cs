using Godot;
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
            //_player._animationPlayer.Play("idle");
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void _Process(double delta)
        {
            Vector2 velocity = _player.Velocity;
            var animatedSprite = _player.GetNode<Sprite2D>("PlayerSpriteSheet");

            if (_player.IsOnFloor() && velocity.x != 0)

            {
                _player._animationPlayer.Play("walking");
            }
            else if (!_player.IsOnFloor())
            {
                if (velocity.y < 0)
                    _player._animationPlayer.Play("jumping_up");
                else
                    _player._animationPlayer.Play("jumping_down");
            }
            else
            {
                //_player._animationPlayer.Stop();
                _player._animationPlayer.Play("idle");
            }


            if (velocity.x != 0)
            {
                if (animatedSprite.FlipH = velocity.x < 0)
                {
                    animatedSprite.Offset = new Vector2(-32.0f, 0.0f);
                }
                else
                {
                    animatedSprite.Offset = new Vector2(0.0f, 0.0f);
                }
            }

            base._Process(delta);
        }

        public override void _PhysicsProcess(double delta)
        {
            Vector2 velocity = _player.Velocity;
            // Add the gravity.
            if (!_player.IsOnFloor())
                velocity.y += _player.gravity * (float)delta;

            // Handle Jump.
            if (Input.IsActionPressed("jump") && _player.IsOnFloor() && _player.inputActionable)
            {
                velocity.y = Player.JumpVelocity;
                _player._jumpsfx.Play();
            }
            if (!Input.IsActionPressed("jump") && velocity.y < -50.0f && velocity.y > -250.0f)
                velocity.y = -50.0f;

            // Get the input direction and handle the movement/deceleration.
            // As good practice, you should replace UI actions with custom gameplay actions.
            Vector2 direction = _player.GetInputDirection();

            if (Input.IsActionJustPressed("attack") && _player.inputActionable)
            {
                _player.playerFSM.SetCurrentState(PlayerFSMStateType.ATTACK);
            }

            if (direction.x != 0)
            {
                // if the player is holding the sprint button, add sprint speed
                // && _player.IsOnFloor()
                if (Input.IsActionPressed("sprint"))
                {
                    if (velocity.x > 100.0f)
                    {
                        velocity.x += direction.x * Player.Acceleration / 4;
                    }
                    else
                    {
                        velocity.x += direction.x * Player.Acceleration;
                    }
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
        }
    }
}
