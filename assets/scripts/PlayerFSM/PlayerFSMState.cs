using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarioLandPlatformer.Template_Patterns;

namespace WarioLandPlatformer.PlayerFSM
{
    public class PlayerFSMState : State<int>
    {
        public new PlayerFSMStateType ID { get { return _id; } }
        protected Player _player = null;
        protected PlayerFSMStateType _id;

        public PlayerFSMState(FiniteStateMachine<int> fsm, Player player) : base(fsm)
        {
            _player = player;
        }

        public PlayerFSMState(Player player) : base()
        {
            _player = player;
            m_fsm = _player.playerFSM;
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
