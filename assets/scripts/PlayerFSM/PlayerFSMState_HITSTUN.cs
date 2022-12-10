using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WarioLandPlatformer.PlayerFSM
{
    internal class PlayerFSMState_HITSTUN : PlayerFSMState
    {
        public PlayerFSMState_HITSTUN(Player player) : base(player)
        {
            _id = PlayerFSMStateType.HITSTUN;
        }

        public override void Enter()
        {
            _player.inputActionable = false;
            _player._animationPlayer.Play("hitstun");

            _player.Modulate = new Color(1, .2f, .4f);
            _player._animationPlayer.Play("hitstun");
            _player.SetCollisionLayerValue(1, false);
            _player.SetCollisionMaskValue(5, false);
            base.Enter();
        }

        public override void Exit()
        {
            _player.inputActionable = true;
            _player.Modulate = new Color(1, 1, 1);
            _player.SetCollisionLayerValue(1, true);
            _player.SetCollisionMaskValue(5, true);
            base.Exit();
        }

        public override void _Process(double delta)
        {
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
