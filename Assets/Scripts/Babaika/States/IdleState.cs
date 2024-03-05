using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Babaika.States
{
    public class IdleState: State
    {
        public IdleState(
            Config stateConfig,
            NavMeshAgent agent,
            FieldOfView fieldOfView,
            Babaika babaika
            ) : base(stateConfig, agent, fieldOfView, babaika)
        {
            
        }

        public override void OnChangeVisibleTargets(IReadOnlyList<Transform> targets)
        {
            if(targets.Any()) _babaika.ChangeState(BabaikaStates.CatchingUpState);
        }

        [Serializable]
        public class Config: StateConfig
        {
        }
        
        public class Factory : PlaceholderFactory<IdleState>
        {
        }
    }
}