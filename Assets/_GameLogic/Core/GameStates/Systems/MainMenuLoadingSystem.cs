﻿using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using Unity.IL2CPP.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _GameLogic.Core.GameStates.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [CreateAssetMenu(menuName = "ECS/Systems/Core/GameStates/" + nameof(MainMenuLoadingSystem))]
    public class MainMenuLoadingSystem : UpdateSystem
    {
        private FilterBuilder _filterBuilder;
        private readonly float _loadingDuration = 1;

        public override void OnAwake()
        {
            _filterBuilder = World.Filter.With<StateMachine>().With<MainMenuState>().With<LoadingState>();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filterBuilder.Build())
            {
                ref var loadingState = ref entity.GetComponent<LoadingState>();
                var progress = math.clamp(loadingState.LoadingTime / _loadingDuration, 0, 1);
                loadingState.Progress = progress;
                
                if (!loadingState.SceneIsLoaded)
                {
                    var loadingSceneLoadingOperation = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
                    loadingSceneLoadingOperation.completed += _ => { };
                    loadingState.SceneIsLoaded = true;
                }

                if (loadingState.LoadingTime < _loadingDuration)
                {
                    loadingState.LoadingTime += deltaTime;
                }
                else
                {
                    var operation = SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
                    operation.completed += _ =>
                    {
                    };
                    entity.RemoveComponent<LoadingState>();
                    
                    if (SceneManager.GetSceneByBuildIndex(1).isLoaded)
                    {
                        var loadingSceneUnloadingOperation = SceneManager.UnloadSceneAsync(1);
                        loadingSceneUnloadingOperation.completed += _ => { };
                    }

                    ref var mainMenuState = ref entity.GetComponent<MainMenuState>();
                    mainMenuState.SceneIsLoaded = true;
                }
            }
        }
    }
}