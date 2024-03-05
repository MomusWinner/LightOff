using System;
using Babaika.States;
using UnityEngine;
using Zenject;

namespace Babaika 
{
    public class Babaika : MonoBehaviour
    {
        private BabaikaStateFactory _stateFactory;
        private State _currentState;
        
        [Inject]
        public void Construct(BabaikaStateFactory stateFactory)
        {
            _stateFactory = stateFactory;
        }

        private void Start()
        {
            ChangeState(BabaikaStates.IdleState);
        }

        public void ChangeState(BabaikaStates state)
        {
            _currentState?.Dispose();
            _currentState = _stateFactory.CreateState(state);
            _currentState.Start();
        }

        private void Update()
        {
            _currentState?.Update();
        }

        private void OnTriggerEnter(Collider other)
        {
            _currentState?.OnTriggerEnter(other);
        }
    }
}