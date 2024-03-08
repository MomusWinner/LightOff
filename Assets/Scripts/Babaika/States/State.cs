using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Babaika.States
{
    public abstract class State
    {
        protected StateConfig baseStateConfig;
        protected NavMeshAgent agent;
        protected FieldOfView fieldOfView;
        protected Babaika babaika;
        
        public State(StateConfig stateConfig, NavMeshAgent agent, FieldOfView fieldOfView, Babaika babaika)
        {
            baseStateConfig = stateConfig;
            this.agent = agent;
            this.fieldOfView = fieldOfView;
            this.babaika = babaika;
        }
        
        public virtual void Start()
        {
            fieldOfView.ViewParams = baseStateConfig.PhysicalConfig.ViewParams;
            agent.speed = baseStateConfig.PhysicalConfig.MoveSpeed;

            fieldOfView.OnChangeVisibleTargets += OnChangeVisibleTargets;
            OnChangeVisibleTargets(fieldOfView.VisibleTargets);
        }

        public virtual void Dispose()
        {
            fieldOfView.OnChangeVisibleTargets -= OnChangeVisibleTargets;
        }

        public virtual void OnTriggerEnter(Collider other)
        {
        }

        public virtual void OnChangeVisibleTargets(IReadOnlyList<Transform> targets)
        {
        }
        
        public virtual void Update()
        {
        }
    }
}