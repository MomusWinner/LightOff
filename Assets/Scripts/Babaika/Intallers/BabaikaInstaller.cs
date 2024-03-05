using Babaika.States;
using Zenject;

namespace Babaika
{
    public class BabaikaInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<BabaikaStateFactory>().AsSingle();
            Container.BindFactory<IdleState, IdleState.Factory>().WhenInjectedInto<BabaikaStateFactory>();
            Container.BindFactory<CatchingUpState, CatchingUpState.Factory>().WhenInjectedInto<BabaikaStateFactory>();
        }
    }
}