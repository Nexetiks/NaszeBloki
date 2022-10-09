using System;
using UnityEngine.Events;

namespace Events
{
    [Serializable]
    public class GameEventListenerResponse<T>
    {
        public GameEvent<T>[] events;
        public UnityEvent<T> response;
    }
}