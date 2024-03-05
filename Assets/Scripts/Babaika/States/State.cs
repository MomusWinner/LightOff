using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Babaika.States
{
    public abstract class State
    {
        protected StateConfig StateConfig;
        protected NavMeshAgent _agent;
        protected FieldOfView _fieldOfView;
        protected Babaika _babaika;
        
        public State(StateConfig stateConfig, NavMeshAgent agent, FieldOfView fieldOfView, Babaika babaika)
        {
            StateConfig = stateConfig;
            _agent = agent;
            _fieldOfView = fieldOfView;
            _babaika = babaika;
        }
        
        public virtual void Start()
        {
            _fieldOfView.ViewRadius = StateConfig.PhysicalConfig.ViewRadius;
            _fieldOfView.ViewAngle = StateConfig.PhysicalConfig.ViewAngle;
            _agent.speed = StateConfig.PhysicalConfig.MoveSpeed;

            _fieldOfView.OnChangeVisibleTargets += OnChangeVisibleTargets;
            OnChangeVisibleTargets(_fieldOfView.VisibleTargets);
        }

        public virtual void Dispose()
        {
            _fieldOfView.OnChangeVisibleTargets -= OnChangeVisibleTargets;
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