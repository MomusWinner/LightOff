using UnityEngine;
using System;

namespace Babaika.States
{
    [Serializable]
    public abstract class StateConfig
    {
        [Serializable]
        public class PhysicalSettings
        {
            public float MoveSpeed => moveSpeed;
            public float ViewRadius => viewRadius;
            public float ViewAngle => viewAngle;

            [SerializeField] private float viewAngle;
            [SerializeField] private float viewRadius;
            [SerializeField] private float moveSpeed;
        }

        public PhysicalSettings PhysicalConfig => _physicalSettings;
        
        [SerializeField] private PhysicalSettings _physicalSettings;
    }
}