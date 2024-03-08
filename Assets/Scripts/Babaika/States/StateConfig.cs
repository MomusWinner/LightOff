using UnityEngine;
using System;
using System.Collections.Generic;

namespace Babaika.States
{
    [Serializable]
    public abstract class StateConfig
    {
        [Serializable]
        public class PhysicalSettings
        {
            public float MoveSpeed => _moveSpeed;
            public List<ViewParam> ViewParams => _viewParams;

            [SerializeField] private float _moveSpeed;
            [SerializeField] private List<ViewParam> _viewParams;
        }

        public PhysicalSettings PhysicalConfig => _physicalSettings;
        
        [SerializeField] private PhysicalSettings _physicalSettings;
    }
}