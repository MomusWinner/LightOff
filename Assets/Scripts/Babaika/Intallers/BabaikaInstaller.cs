using System.Collections.Generic;
using Babaika.States;
using UnityEngine;
using Zenject;

namespace Babaika
{
    public class BabaikaInstaller: MonoInstaller
    {
        [SerializeField] private List<Transform> _bypassPoints;
        
        public override void InstallBindings()
        {
            Container.Bind<BabaikaStateFactory>().AsSingle();
            Container.Bind<List<Transform>>().FromInstance(_bypassPoints);
            Container.BindFactory<IdleState, IdleState.Factory>().WhenInjectedInto<BabaikaStateFactory>();
            Container.BindFactory<CatchingUpState, CatchingUpState.Factory>().WhenInjectedInto<BabaikaStateFactory>();
        }
    }
}