using System.Runtime.Serialization;

namespace FreeTale.Unity.Toolkit
{

    [System.Serializable]
    public class StateGuardException : System.Exception
    {
        public StateGuardException() { }
        public StateGuardException(string message) : base(message) { }
        public StateGuardException(string message, System.Exception inner) : base(message, inner) { }
        protected StateGuardException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    public interface IManager<T> where T : IState
    {
        bool StateGuard { get; set; }
        T CurrentState { get; set; }
        void Tick();
    }

    public interface IState
    {
        void Enter();
        void Tick();
        void Exit();
    }

    public static class IFSMExtension
    {
        public static void NextState<T>(this IManager<T> manager, T state) where T : IState
        {
            if (manager.StateGuard)
            {
                throw new StateGuardException();
            }
            manager.StateGuard = true;
            manager.CurrentState.Exit();
            manager.CurrentState = state;
            manager.CurrentState.Enter();
            manager.StateGuard = false;
        }
    }
}