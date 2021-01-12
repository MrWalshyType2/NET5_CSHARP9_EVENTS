using System;

namespace Events
{
    // Event subscribe
    class Subscriber
    {
        private readonly string _id;

        public Subscriber(string id, Publisher publisher)
        {
            _id = id;

            // Subscribe to the event
            publisher.RaiseCustomEvent += HandleCustomEvent;
        }

        void HandleCustomEvent(object sender, CustomEventArgs e)
        {
            Console.WriteLine($"{_id} received this message: {e.Message}");
        }
    }
}