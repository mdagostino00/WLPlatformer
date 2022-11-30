using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarioLandPlatformer.Template_Patterns
{
    public class FiniteStateMachine<T>
    {
        // dictionary for all possible states
        protected Dictionary<T, State<T>> m_states;
        // the current state
        protected State<T>? m_currentState;


        /// <summary>
        /// <c>FiniteStateMachine</c>Creates a new dictionary for the FSM
        /// </summary>
        public FiniteStateMachine()
        {
            m_states = new Dictionary<T, State<T>>();
        }

        /// <summary>
        /// <c>Add</c>Add a state to the dictionary
        /// </summary>
        /// <param name="state">the state needed to be added</param>
        public void Add(State<T> state)
        {
            m_states.Add(state.ID, state);
        }

        /// <summary>
        /// <c>Add</c>Overloaded add to dict function
        /// </summary>
        /// <param name="stateID">The ID of the state</param>
        /// <param name="state">The state</param>
        public void Add(T stateID, State<T> state)
        {
            m_states.Add(stateID, state);
        }

        /// <summary>
        /// <c>GetState</c> Returns a state using an id if that state exists in the dict
        /// </summary>
        /// <param name="stateID">the desired state's id</param>
        /// <returns>the state</returns>
        public State<T> GetState(T stateID)
        {
            if (m_states.ContainsKey(stateID))
            {
                return m_states[stateID];
            }
            return null;
        }

        /// <summary>
        /// <c>SetCurrentState</c>set the current state to the desired state using id
        /// </summary>
        /// <param name="stateID">the id of the desired state</param>
        public void SetCurrentState(T stateID)
        {
            State<T> state = m_states[stateID];
            SetCurrentState(state);
        }

        /// <summary>
        /// <c>GetCurrentState</c>Gets the current state
        /// </summary>
        /// <returns>the current state</returns>
        public State<T> GetCurrentState()
        {
            return m_currentState;
        }

        /// <summary>
        /// <c>SetCurrentState</c>Sets the current state using the state.
        /// Has additional checks to call the Enter() and Exit() functions.
        /// </summary>
        /// <param name="state">the state</param>
        public void SetCurrentState(State<T> state)
        {
            //if no state change, don't set anything
            if (m_currentState == state)
            {
                return;
            }

            // if we have left the previous state, trigger Exit()
            if (m_currentState != null)
            {
                m_currentState.Exit();
            }

            // set the current state to the actual current state
            m_currentState = state;

            // now trigger the Enter() function
            if (m_currentState != null)
            {
                m_currentState.Enter();
            }
        }

        /// <summary>
        /// <c><Update/c>calls the current state's Update function
        /// </summary>
        public void _Process()
        {
            if (m_currentState != null)
            {
                m_currentState._Process();
            }
        }

        public void _PhysicsProcess(double delta)
        {
            if (m_currentState != null)
            {
                m_currentState._PhysicsProcess(delta);
            }
        }
    }
}
