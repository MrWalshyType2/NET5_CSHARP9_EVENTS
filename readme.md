# Events
Events enable classes or objects to notify other classes or objects of some event occuring. The class that sends/*raises* the event is called
the *publisher* while the class that receives/*handles* the event are called subscribers.

- Publisher determines when events are raised; subscribers determine the action taken in response.
- Events can have multiple subscribers, subscribers can handle multiple events from multiple publishers.
- Events with no subscribers are never raised.
- Events are typically used to signal user actions.
- When events have multiple subscribers, event handlers are invoked synchronously when an event is raised.
- Events in the .NET class library are based on the `EventHandler` delegate and the `EventArgs` base class.

## How to subscribe to and unsubscribe from events
Subscribing to an event is done when you want to write custom code in response to an event published by a different class.

### Visual Studio IDE Subscriber
1. Properties Window (If unavailable, right-click the form or control in design view for which to create an event handler and select 'Properties')
2. Click the 'Events' icon
3. Double-click the event to create.

### Programmatic Subscription
1. Define an event handler method with a signature matching the delegate signature for the event. If the event was based on the `EventHandler` delegate
type, the method stub would look like:
```
void HandleCustomEvent(object sender, CustomEventArgs a)
{
	...
}
```
2. Use the addition-assigment operator to attach event handlers to the event. Assume the following example has an object named `publisher` and an 
event named `RaiseCustomEvent`:
```
publisher.RaiseCustomEvent += HandleCustomEvent;
// C# 2.0 equivalent to C# 1.0
publisher.RaiseCustomEvent += new CustomEventHandler(HandleCustomEvent);
```
Lambda expressions can also be used to specify an event handler:
```
public Form1()
{
	InitializeComponent();
	this.Click += (s, e) =>
		{
			...
		}
}
```

### Unsubscribing
Before disposing of a subscriber object, the object should be unsubscribed to prevent resource leaks.

Use the subtraction-assigment operator to unsubscribe:
```
publisher.RaiseCustomEvent -= HandleCustomEvent;
```

## How to publish .NET conforming events
All events in the .NET class library are based on the `EventHandler` delegate, defined as follows:
```
public delegate void EventHandler(object sender, EventArgs e);
```

.NET 2.0 introduced a generic version, `EventHandler<TEventArgs>`. Microsoft recommends the .NET pattern for basing events.

The `EventHandler` delegate doesn't actually handle events, they instead delegate to a method or lambda expression event handler whose signature matches
the delegates definition.

1. Skip to 3a if not sending custom data with the event. Declare a custom data class with a scope visible to the publisher and subscriber classes. 
Add the required members to hold custom event data:
```
public class CustomEventArgs : EventArgs
{
	public CustomEventArgs(string message)
	{
		Message = message;
	}

	public string Message { get; set; }
}
```

2. Skip if using the generic version of `EventHandler<TEventArgs>`. Declare a delegate in the publishing class, give it a name ending with 
'EventHandler'. The second parameter specifies the custom `EventArgs` type.
```
public delegate void CustomEventHandler(object sender, CustomEventArgs args);
```

3. Declare the event in the publishing class:
    a. NON-GENERIC `EventHandler`
	Delegate does not need to be declared as it is already declared in the `System` namespace included when a C# project is created,
	add this code:
	```
	public event EventHandler RaiseCustomEvent;
	```

	b. NON-GENERIC `EventHandler`, USING CUSTOM `EventArgs`
	Declare event inside the publishing class using the delegate from step 2 as the type:
	```
	public event CustomEventHandler RaiseCustomEvent;
	```

	c. GENERIC `EventHandler`
	No custom delegate is needed. Instead, in the publishing class, specify the event type as `EventHandler<T>` substituting 'T' with the custom `EventArgs` class.
	```
	pbulic event EventHandler<CustomEventArgs> RaiseCustomEvent;
	```