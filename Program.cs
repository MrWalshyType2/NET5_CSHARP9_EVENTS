using System;

namespace Events
{
    class Program
    {
        static void Main(string[] args)
        {
            var pub = new Publisher();
            var sub1 = new Subscriber("sub1", pub);
            var sub2 = new Subscriber("sub2", pub);

            pub.DoSomething();
        }
    }
}
