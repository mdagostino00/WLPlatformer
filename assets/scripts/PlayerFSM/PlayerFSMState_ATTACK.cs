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
            base._PhysicsProcess(delta);
        }
    }
}
