using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    
    [CreateAssetMenu]
    public class VoidGameEvent : ScriptableObject
    {
        /// <summary>
        /// The list of listeners that this event will notify if it is raised.
        /// </summary>
        private readonly List<VoidGameEventListener> eventListeners = new();

        [SerializeField] private UnityEvent staticResponse;

        public virtual void Raise()
        {
            staticResponse?.Invoke();

            for (var i = eventListeners.Count - 1; i >= 0; i--)
            {
                eventListeners[i].OnEventRaised(this);
            }
        }

        public void RegisterListener(VoidGameEventListener listener)
        {
            if (!eventListeners.Contains(listener))
            {
                eventListeners.Add(listener);
            }
        }

        public void UnregisterListener(VoidGameEventListener listener)
        {
            if (eventListeners.Contains(listener))
            {
                eventListeners.Remove(listener);
            }
        }
    }
}