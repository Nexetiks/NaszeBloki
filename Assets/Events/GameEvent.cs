using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    
    public class GameEvent<T> : ScriptableObject
    {
        /// <summary>
        /// The list of listeners that this event will notify if it is raised.
        /// </summary>
        private readonly List<GameEventListener<T>> eventListeners = new();

        [SerializeField] private UnityEvent<T> staticResponse;

        public virtual void Raise(T t)
        {
            staticResponse?.Invoke(t);

            for (var i = eventListeners.Count - 1; i >= 0; i--)
            {
                eventListeners[i].OnEventRaised(this, t);
            }
        }

        public void RegisterListener(GameEventListener<T> listener)
        {
            if (eventListeners != null && !eventListeners.Contains(listener))
            {
                eventListeners.Add(listener);
            }
        }

        public void UnregisterListener(GameEventListener<T> listener)
        {
            if (eventListeners != null && eventListeners.Contains(listener))
            {
                eventListeners.Remove(listener);
            }
        }
    }
}