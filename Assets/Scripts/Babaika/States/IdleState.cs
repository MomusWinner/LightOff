using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Random = UnityEngine.Random;

namespace Babaika.States
{
    public class IdleState: State
    {
        private readonly List<Transform> _bypassPoints;
        private readonly Config _stateConfig;
        private Queue<Transform> _orderedBypassPoints = new ();
        private Transform _target;
        
        public IdleState(
            Config stateConfig,
            NavMeshAgent agent,
            FieldOfView fieldOfView,
            List<Transform> bypassPoints,
            Babaika babaika
            ) : base(stateConfig, agent, fieldOfView, babaika)
        {
            _stateConfig = stateConfig;
            _bypassPoints = bypassPoints;
        }

        public override void Start()
        {
            base.Start();
            Debug.Log("Start");
            SetTarget();
        }

        public override void Update()
        {
            if (Vector3.Distance(_target.position, babaika.transform.position) <= _stateConfig.ByPointDestinationOffset)
                SetTarget();
        }

        private void SetTarget()
        {
            if (!_orderedBypassPoints.Any())
                GenerateOrderedBypassPoints();
            _target = _orderedBypassPoints.Dequeue();
            agent.SetDestination(_target.position);
        }

        private void GenerateOrderedBypassPoints()
        {
            List<Transform> usedTransforms = new();
            List<Transform> positionsPool = new(_bypassPoints);
            while (positionsPool.Any())
            {
                Transform randomTransform =  positionsPool[Random.Range(0, positionsPool.Count)];
                _orderedBypassPoints.Enqueue(randomTransform);
                positionsPool.Remove(randomTransform);
            }
        }
        

        public override void OnChangeVisibleTargets(IReadOnlyList<Transform> targets)
        {
            if(targets.Any()) babaika.ChangeState(BabaikaStates.CatchingUpState);
        }

        [Serializable]
        public class Config: StateConfig
        {
            public float ByPointDestinationOffset => _bypassPointDestinationOffset;

            [SerializeField] private float _bypassPointDestinationOffset = 5f;
        }
        
        public class Factory : PlaceholderFactory<IdleState>
        {
        }
    }
}