using System;

namespace Events
{
    class Publisher
    {
        // Declare event using `EventHandler<T>`
        public event EventHandler<CustomEventArgs> RaiseCustomEvent;

        public void DoSomething()
        {
            Console.WriteLine("Doing something");
            OnRaiseCustomEvent(new CustomEventArgs("Event triggered"));
        }

        // Wrap event invocations in a protected virtual method
        // which allows derived classes to override event invocation behaviour
        protected virtual void OnRaiseCustomEvent(CustomEventArgs e)
        {
            // tmp copy of event avoids possibility of a race condition if last subscriber unsubscribes
            // immediately after null check but before the event is raised
            EventHandler<CustomEventArgs> raiseEvent = RaiseCustomEvent;

            // If raiseEvent has subscribers
            if (raiseEvent != null)
            {
                e.Message += $" at {DateTime.Now}";
                raiseEvent(this, e);
            }
        }
    }
}