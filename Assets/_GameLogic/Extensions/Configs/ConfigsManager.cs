using System;
using System.Collections.Generic;
using System.Linq;
using _GameLogic.Extensions.Patterns;
using UnityEngine;

namespace _GameLogic.Extensions.Configs
{
    public class ConfigsManager : Singleton<ConfigsManager>
    {
        [SerializeField] private List<ScriptableObject> _configs;

        private Dictionary<Type, ScriptableObject> _configDictionary;

        protected override void Init()
        {
            base.Init();

            _configDictionary = _configs.ToDictionary(c => c.GetType(), c => c);
        }

        public static T GetConfig<T>() where T : ScriptableObject => Instance._configDictionary[typeof(T)] as T;
    }
}