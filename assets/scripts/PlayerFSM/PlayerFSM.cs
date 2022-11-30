using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarioLandPlatformer.Template_Patterns;

namespace WarioLandPlatformer.PlayerFSM
{
    public class PlayerFSM : FiniteStateMachine<int>
    {
        /// <summary>
        /// <c>CardFSM</c>
        /// calls the base constructor, which makes a dictionary of ints
        /// </summary>
        public PlayerFSM() : base() { }

        /// <summary>
        /// <c>Add</c>adds the state to the dictionary of states found in the base class FiniteStateMachine
        /// </summary>
        /// <param name="state">the state we want to add</param>
        public void Add(PlayerFSMState state)
        {
            m_states.Add((int)state.ID, state);
        }

        /// <summary>
        /// <c>GetState</c>Returns the state when given a key in the CardFSMStateType enum.
        /// </summary>
        /// <param name="key">the state listed in the <see cref="CardFSMStateType"/></param>
        /// <returns>The state casted to CardFSMState</returns>
        public PlayerFSMState GetState(PlayerFSMStateType key)
        {
            return (PlayerFSMState)GetState((int)key);
        }

        /// <summary>
        /// <c>SetCurrentState</c>sets the current state in the PlayerFSM to the desired state in the <see cref="PlayerFSMStateType"/>
        /// </summary>
        /// <param name="stateKey">the key in the <see cref="PlayerFSMStateType"/></param>
        public void SetCurrentState(PlayerFSMStateType stateKey)
        {
            State<int> state = m_states[(int)stateKey];
            if (state != null)
            {
                SetCurrentState(state);
            }
        }
    }
}
