using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FreeTale.Unity.Toolkit
{

    [Serializable]
    public class StateGuardException : Exception
    {
        public StateGuardException() { }
        public StateGuardException(string message) : base(message) { }
        public StateGuardException(string message, Exception inner) : base(message, inner) { }
        protected StateGuardException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    public interface IStateManagerHook<TState, TName> where TState : IState<TName> where TName : Enum
    {
        void PreExit(IStateManager<TState, TName> manager, IState<TName> from, IState<TName> to);
        void PostExit(IStateManager<TState, TName> manager, IState<TName> from, IState<TName> to);
        void PreEnter(IStateManager<TState, TName> manager, IState<TName> from, IState<TName> to);
        void PostEnter(IStateManager<TState, TName> manager, IState<TName> from, IState<TName> to);
    }
    public interface IStateManager<TState, TName> where TState : IState<TName> where TName : Enum
    {
        bool StateGuard { get; set; }
        List<IStateManagerHook<TState, TName>> Hooks { get; }
        TState CurrentState { get; set; }
        void Tick();
    }

    public interface IState
    {
        void Enter();
        void Tick();
        void Exit();
    }
    public interface IState<TName>: IState where TName : Enum
    {
        TName Name { get; }
    }

    public static class IFSMExtension
    {
        /// <summary>
        /// ensure current state is target state
        /// </summary>
        /// <param name="target"></param>
        /// <returns>target state</returns>
        /// <exception cref="InvalidOperationException">when not in target state</exception>
        public static TState At<TState, TName>(this IStateManager<TState, TName> manager, TState target)
            where TState : IState<TName>
            where TName : Enum
        {
            if (!ReferenceEquals(manager.CurrentState, target))
            {
                throw new InvalidOperationException("state not at target state");
            }
            return target;
        }

        public static void NextState<TState, TName>(this IStateManager<TState, TName> manager, TState to) 
            where TState : IState<TName> 
            where TName : Enum
        {
            if (manager.StateGuard)
            {
                throw new StateGuardException();
            }
            var from = manager.CurrentState;
            manager.StateGuard = true;
            manager.Hooks.ForEach(hook => { hook.PreExit(manager, from, to); });
            from.Exit();
            manager.Hooks.ForEach(hook => { hook.PostExit(manager, from, to); });
            manager.CurrentState = to;
            manager.Hooks.ForEach(hook => { hook.PreEnter(manager, from, to); });
            to.Enter();
            manager.Hooks.ForEach(hook => { hook.PostEnter(manager, from, to); });
            manager.StateGuard = false;
        }
    }
}