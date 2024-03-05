using System;
using Babaika.States;
using UnityEngine;
using Zenject;

[CreateAssetMenu(menuName = "Game/Babaika/Settings")]
public class BabaikaSettingsInstaller : ScriptableObjectInstaller<BabaikaSettingsInstaller>
{
    [Serializable]
    public class StateSettings
    {
        public IdleState.Config IdleStateConfig;
        public CatchingUpState.Config CatchingUpStateConfig;
    }

    [SerializeField] private StateSettings _stateSettings;

    public override void InstallBindings()
    {
        Container.BindInstance(_stateSettings.IdleStateConfig);
        Container.BindInstance(_stateSettings.CatchingUpStateConfig);
    }
}
