using System;
using UnityEngine.Events;

namespace Events
{
    [Serializable]
    public class VoidGameEventListenerResponse
    {
        public VoidGameEvent[] events;
        public UnityEvent response;
    }
}