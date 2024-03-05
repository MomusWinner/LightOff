using ModestTree;

namespace Babaika.States
{
    public enum BabaikaStates
    {
        IdleState,
        CatchingUpState
    }
    
    public class BabaikaStateFactory
    {
        private readonly IdleState.Factory _idleFactory;
        private readonly CatchingUpState.Factory _catchingUpFactory;

        public BabaikaStateFactory(IdleState.Factory idleFactory, CatchingUpState.Factory catchingUpFactory)
        {
            _idleFactory = idleFactory;
            _catchingUpFactory = catchingUpFactory;
        }

        public State CreateState(BabaikaStates state)
        {
            switch (state)
            {
                case BabaikaStates.IdleState:
                {
                    return _idleFactory.Create();
                }
                case BabaikaStates.CatchingUpState:
                {
                    return _catchingUpFactory.Create();
                }
            }
            
            throw Assert.CreateException();
        }
    }
}