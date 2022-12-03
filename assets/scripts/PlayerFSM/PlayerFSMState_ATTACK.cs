using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarioLandPlatformer.PlayerFSM
{
    public class PlayerFSMState_ATTACK : PlayerFSMState
    {
        public PlayerFSMState_ATTACK(Player player) : base(player)
        {
            _id = PlayerFSMStateType.ATTACK;
        }

        public override void Enter()
        {
            _player._animationPlayer.Stop();
            _player._animationPlayer.Play("attack");
            _player.animationActable = false;
            base.Enter();
        }

        public override void Exit()
        {
            _player.SetAnimationActable();
            base.Exit();
        }

        public override void _Process(double delta)
        {
            //_player.SetAnimationActable();
            //_player._animationPlayer.Play("attack");

            if (_player.animationActable == false)
            {
                return;
            }
            if (Input.IsActionJustPressed("attack"))
            {
                Enter();
                return;
            }
            if (Input.IsActionPressed("move_left") ||
                Input.IsActionPressed("move_right") ||
                Input.IsActionJustPressed("jump")
                )
            {
                _player.playerFSM.SetCurrentState(PlayerFSMStateType.MOVEMENT);
                return;
            }

            /*
            if (_player.animationActable == true && Input.IsActionJustPressed("attack"))
            {                
                _player.playerFSM.SetCurrentState(PlayerFSMStateType.ATTACK);
                //Enter();

            }
            */
            base._Process(delta);
        }

        public override void _PhysicsProcess(double delta)
        {
            Vector2 velocity = _player.Velocity;
            // Add the gravity.
            if (!_player.IsOnFloor())
                velocity.y += _player.gravity * (float)delta;

            velocity.x = Mathf.MoveToward(_player.Velocity.x, 0, Player.Acceleration);
            _player.Velocity = velocity;
            base._PhysicsProcess(delta);
        }
    }
}
