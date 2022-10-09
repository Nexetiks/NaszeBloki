using System;
using System.Linq;
using UnityEngine;

namespace Events
{
    public class VoidGameEventListener : MonoBehaviour
    {
        public VoidGameEventListenerResponse[] listenerResponses;

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

        public void OnEventRaised(VoidGameEvent gameEvent)
        {
            foreach (var listenerResponse in listenerResponses.Where(listenerResponse =>
                         listenerResponse.events.Contains(gameEvent)))
            {
                listenerResponse.response.Invoke();
            }
        }
    }
}