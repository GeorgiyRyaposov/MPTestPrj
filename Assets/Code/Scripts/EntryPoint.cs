using Code.Scripts.Components;
using Code.Scripts.Configs;
using Code.Scripts.Data;
using Code.Scripts.Factories;
using Code.Scripts.GameStates;
using Code.Scripts.InputListeners;
using Code.Scripts.Services;
using Code.Scripts.Views;
using Unity.Netcode;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code.Scripts
{
    public class EntryPoint : LifetimeScope
    {
        [SerializeField] private MainMenuPanel _mainMenuPanel;
        [SerializeField] private GameAssets _gameAssets;
        [SerializeField] private BalanceConfig _balanceConfig;
        [SerializeField] private NetworkManager _networkManager;
        [SerializeField] private PlayerCamera _playerCamera;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_gameAssets.PlayerPrefabs);
            builder.RegisterInstance(_gameAssets.StageItems);
            builder.RegisterInstance(_balanceConfig);

            builder.RegisterEntryPoint<GameStateMachine>().As<GameStateMachine>();
            builder.Register<StartMenuState>(Lifetime.Singleton);
            builder.Register<GameplayState>(Lifetime.Singleton);
            
            builder.Register<InputService>(Lifetime.Singleton);
            builder.Register<InputState>(Lifetime.Singleton);
            builder.Register<ViewsState>(Lifetime.Singleton);
            builder.Register<PlayerActionsListener>(Lifetime.Singleton);
            
            builder.Register<StageService>(Lifetime.Singleton);
            builder.Register<ItemsService>(Lifetime.Singleton);
            builder.Register<PlayerService>(Lifetime.Singleton);
            builder.Register<PlayerFactory>(Lifetime.Singleton);
            builder.Register<FirstAidKitFactory>(Lifetime.Singleton);

            builder.RegisterComponent(_networkManager);
            builder.RegisterComponent(_mainMenuPanel);
            builder.RegisterComponent(_playerCamera);
        }
    }
}