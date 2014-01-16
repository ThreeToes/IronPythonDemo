using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PythonDemo.Scripting
{
    public class CallbackManager
    {
        private Dictionary<string, List<IHookListener>> _callbacks;

        public CallbackManager()
        {
            _callbacks = new Dictionary<string, List<IHookListener>>();
        }

        /// <summary>
        /// Register a callback for events
        /// </summary>
        /// <param name="eventId">String ID for event</param>
        /// <param name="listener">Listener object</param>
        public void RegisterCallback(string eventId, IHookListener listener)
        {
            if(!_callbacks.ContainsKey(eventId))
            {
                _callbacks[eventId] = new List<IHookListener>();
            }
            _callbacks[eventId].Add(listener);
        }

        /// <summary>
        /// Fire an event
        /// </summary>
        /// <param name="eventId">Event ID</param>
        /// <param name="payload">Payload</param>
        public void FireEvent(string eventId, object payload)
        {
            //If nothing is registered, 
            if (!_callbacks.ContainsKey(eventId)) return;
            
            foreach (var callback in _callbacks[eventId])
            {
                //Run callback
                callback.Callback(payload);   
            }
        }
    }
}
