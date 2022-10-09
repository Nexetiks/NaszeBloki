using System;
using System.Linq;
using UnityEngine;

namespace Events
{
    public class GameEventListener<T> : MonoBehaviour
    {
        public GameEventListenerResponse<T>[] listenerResponses;

        public void OnEnable()
        {
            foreach (var listenerResponse in listenerResponses)
            {
                Array.ForEach(listenerResponse.events, gameEvent => gameEvent.RegisterListener(this));
            }
        }

        public void OnDisable()
        {
            foreach (var listenerResponse in listenerResponses)
            {
                Array.ForEach(listenerResponse.events, gameEvent => gameEvent.UnregisterListener(this));
            }
        }

        public void OnEventRaised(GameEvent<T> gameEvent, T t)
        {
            foreach (var listenerResponse in listenerResponses.Where(listenerResponse =>
                         listenerResponse.events.Contains(gameEvent)))
            {
                listenerResponse.response.Invoke(t);
            }
        }
    }
}