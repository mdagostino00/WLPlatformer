using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarioLandPlatformer.Template_Patterns
{
    public class State<T>
    {
        protected FiniteStateMachine<T> m_fsm;

        // name of state
        public string Name { get; set; }
        // ID of state
        public T ID { get; private set; }

        /// <summary>
        /// <c>State</c>Default constructor for State
        /// </summary>
        public State()
        {
            //Debug.Log("I think something went wrong in the parameterless State() constructor.");
        }

        /// <summary>
        /// <c>State</c>constructor for state that takes a FiniteStateMachine object
        /// </summary>
        /// <param name="fsm">the FiniteStateMachine object</param>
        public State(FiniteStateMachine<T> fsm)
        {
            m_fsm = fsm;
        }

        /// <summary>
        /// <c>State</c>constructor that takes the id of the state
        /// </summary>
        /// <param name="id">the id</param>
        public State(T id)
        {
            ID = id;
        }

        /// <summary>
        /// <c>State</c>constructor that takes the id and name of a state
        /// </summary>
        /// <param name="id">the id number of the state</param>
        /// <param name="name">the name of the state</param>
        public State(T id, string name) : this(id)
        {
            Name = name;
        }

        // delegates are basically C# function pointers
        // a delegate is a reference type variable that holds the reference to a method.
        public delegate void DelegateNoArg();

        public DelegateNoArg? OnEnter;
        public DelegateNoArg? OnExit;
        public DelegateNoArg? On_Process;
        public DelegateNoArg? On_PhysicsProcess;

        /// <summary>
        /// <c>State</c>constructor that takes multiple delegate arguments for its functions
        /// </summary>
        /// <param name="id">the id</param>
        /// <param name="onEnter">onEnter function pointer</param>
        /// <param name="onExit">on Exit function pointer</param>
        /// <param name="on_Process">on_Process function pointer</param>
        /// /// <param name="on_PhysicsProcess">on_PhysicsProcess function pointer</param>
        public State(T id,
            DelegateNoArg onEnter,
            DelegateNoArg? onExit = null,
            DelegateNoArg? on_Process = null,
            DelegateNoArg? on_PhysicsProcess = null) : this(id)
        {
            OnEnter = onEnter;
            OnExit = onExit;
            On_Process = on_Process;
            On_PhysicsProcess = on_PhysicsProcess;
        }

        /// <summary>
        /// <c>State</c>constructor that takes multiple delegate arguments for its functions
        /// </summary>
        /// <param name="id">the id</param>
        /// <param name="name">the name of the state</param>
        /// <param name="onEnter">onEnter function pointer</param>
        /// <param name="onExit">on Exit function pointer</param>
        /// <param name="on_Process">onUpdate function pointer</param>
        /// <param name="on_PhysicsProcess">onRender function pointer</param>
        public State(T id,
            string name,
            DelegateNoArg? onEnter,
            DelegateNoArg? onExit = null,
            DelegateNoArg? on_Process = null,
            DelegateNoArg? on_PhysicsProcess = null) : this(id, name)
        {
            OnEnter = onEnter;
            OnExit = onExit;
            On_Process = on_Process;
            On_PhysicsProcess = on_PhysicsProcess;
        }

        /// <summary>
        /// <c>Enter</c>The Enter function created in FiniteStateMachine
        /// </summary>
        public virtual void Enter()
        {
            OnEnter?.Invoke();
        }

        /// <summary>
        /// <c>Exit</c>The Exit function created in FiniteStateMachine
        /// </summary>
        public virtual void Exit()
        {
            OnExit?.Invoke();
        }

        /// <summary>
        /// <c>Update</c>Unity's update function
        /// </summary>
        public virtual void _Process()
        {
            On_Process?.Invoke();
        }

        public virtual void _PhysicsProcess(double delta)
        {
            On_PhysicsProcess?.Invoke();
        }
    }
}
