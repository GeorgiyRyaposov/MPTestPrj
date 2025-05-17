using System;
using UnityEngine;
using VContainer;

namespace Code.Scripts.Services
{
    public class NetworkItemsInjector : MonoBehaviour
    {
        private IObjectResolver _resolver;
        private static NetworkItemsInjector _instance;

        private void Awake()
        {
            _instance = this;
        }

        [Inject]
        private void Construct(IObjectResolver resolver)
        {
            _resolver = resolver;
        }

        public static void Inject(MonoBehaviour monoBehaviour)
        {
            _instance._resolver.Inject(monoBehaviour);
        }
    }
}