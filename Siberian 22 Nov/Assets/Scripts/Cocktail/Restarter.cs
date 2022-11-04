using UnityEngine;
using System.Linq;

namespace Cocktails
{
    public delegate void SendEvent();

    public class Restarter : MonoBehaviour
    {
        private event SendEvent _restart;

        private void Awake()
        {
            var resetableObjects = FindObjectsOfType<MonoBehaviour>().OfType<IResetable>().ToArray();
            foreach (var resetable in resetableObjects)
                _restart += resetable.Reset;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
                _restart.Invoke();
        }
    }

    public interface IResetable
    {
        void Reset();
    }
}