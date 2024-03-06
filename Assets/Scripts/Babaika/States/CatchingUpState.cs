using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Babaika.States
{
    public class CatchingUpState : State
    {
        public CatchingUpState(
            Config stateConfig, 
            NavMeshAgent agent, 
            FieldOfView fieldOfView,
            Babaika babaika
            ) : base(stateConfig, agent, fieldOfView, babaika)
        {
        }
        
        [Serializable]
        public class Config : StateConfig
        {
        }

        public override void OnChangeVisibleTargets(IReadOnlyList<Transform> targets)
        {
            if(!targets.Any()) return;
            agent.SetDestination(targets[0].position);
        }

        public class Factory : PlaceholderFactory<CatchingUpState>
        {
        }
    }
}