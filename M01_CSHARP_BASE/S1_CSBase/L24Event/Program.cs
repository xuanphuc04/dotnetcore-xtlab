namespace L24Event
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Publisher p = new Publisher();
            ClassA classA = new ClassA();
            ClassB classB = new ClassB();

            classA.Sub(p);
            classB.Sub(p);

            p.Send();
        }
    }

    // delegate cho event mac dinh trong .NET:
    // public delegate void EventHandler(object sender?, EventArgs e);
    // public delegate void EventHandler<TEventArgs>(object sender?, TEventArgs e);

    public class MyEventArgs : EventArgs {

        private string data;

        public MyEventArgs(string data) {
            this.data = data;
        }

        public string Data { get => data; }
    }

    public class Publisher
    {
        public event EventHandler<MyEventArgs> news_event;

        public void Send()
        {
            news_event?.Invoke(this, new MyEventArgs("Co tin moi ABC..."));
        }
    }

    public class ClassA
    {
        public void Sub(Publisher p)
        {
            p.news_event += ReceiverFromPublisher;
        }

        private void ReceiverFromPublisher(object sender, MyEventArgs e)
        {
            Console.WriteLine("ClassA: " + e.Data);
        }
    }

    public class ClassB
    {
        public void Sub(Publisher p)
        {
            p.news_event += ReceiverFromPublisher;
        }

        private void ReceiverFromPublisher(object sender, MyEventArgs e)
        {
            Console.WriteLine("ClassB: " + e.Data);
        }
    }
}