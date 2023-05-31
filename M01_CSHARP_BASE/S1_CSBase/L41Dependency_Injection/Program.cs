using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace L41Dependency_Injection
{
    interface IClassB
    {
        public void ActionB();
    }
    interface IClassC
    {
        public void ActionC();
    }

    class ClassC : IClassC
    {
        public ClassC() => Console.WriteLine("ClassC is created");
        public void ActionC() => Console.WriteLine("Action in ClassC");
    }

    class ClassC1 : IClassC
    {
        public ClassC1() => Console.WriteLine("ClassC1 is created");
        public void ActionC()
        {
            Console.WriteLine("Action in C1");
        }
    }

    class ClassB : IClassB
    {
        IClassC c_dependency;
        public ClassB(IClassC classc)
        {
            c_dependency = classc;
            Console.WriteLine("ClassB is created");
        }
        public void ActionB()
        {
            Console.WriteLine("Action in ClassB");
            c_dependency.ActionC();
        }
    }

    class ClassB1 : IClassB
    {
        IClassC c_dependency;
        public ClassB1(IClassC classc)
        {
            c_dependency = classc;
            Console.WriteLine("ClassB1 is created");
        }
        public void ActionB()
        {
            Console.WriteLine("Action in B1");
            c_dependency.ActionC();
        }
    }


    class ClassA
    {
        IClassB b_dependency;
        public ClassA(IClassB classb)
        {
            b_dependency = classb;
            Console.WriteLine("ClassA is created");
        }
        public void ActionA()
        {
            Console.WriteLine("Action in ClassA");
            b_dependency.ActionB();
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            // Chức năng của thư viện Dependency Injection
            // 1. DI Container: ServiceCollection
            // - Đăng ký các dịch vụ - service (các lớp)
            // - Get service (ServiceProvider): tự động khởi tạo đối tượng, nếu có dependency
            //   thì tự động khởi tạo dependency và inject vào service (đối tượng)
            //----------------------------------------------------------------

            var services = new ServiceCollection();

            // Đăng ký các dịch vụ
            services.AddSingleton<IClassC, ClassC>();
            services.AddSingleton<IClassB, ClassB>();
            services.AddSingleton<ClassA, ClassA>();


            // --- Options ---
            services.AddSingleton<MyService, MyService>();

            services.Configure<MyServiceOptions>(
                (options) =>
                {
                    options.data1 = "Xin chào các bạn";
                    options.data2 = 1234;
                }
            );


            // --- Options from file ---
            var configBuider = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var configurationRoot = configBuider.Build();

            Console.WriteLine(configurationRoot.GetSection("MyServiceOptions").GetSection("data1").Value);

            Console.WriteLine("------------------------");


            var provider = services.BuildServiceProvider();


            //================
            ClassA a = provider.GetService<ClassA>();
            //a.ActionA();

            Console.WriteLine("------------------------");


            //================ Options ==================
            var myService = provider.GetService<MyService>();
            //myService.PrintData();

        }

    }


    public class MyServiceOptions
    {
        public string data1 { get; set; }
        public int data2 { get; set; }
    }

    public class MyService
    {
        public string data1 { get; set; }
        public int data2 { get; set; }

        public MyService(IOptions<MyServiceOptions> options)
        {
            MyServiceOptions opts = options.Value;
            data1 = opts.data1;
            data2 = opts.data2;
        }
        public void PrintData() => Console.WriteLine($"{data1} / {data2}");
    }
}