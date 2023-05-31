namespace L23Lamda_Delegate
{
    delegate void AnonymousFunc(string a, string b);
    internal class Program
    {
        
        static void Main(string[] args)
        {
            Logs.showResult();
            Logs.TestShowLog();

            // Anonymous function + Lamda expression
            AnonymousFunc test = (a, b) =>
            {
                Console.WriteLine(a + " | " + b);
            };

            test("Test 1", "Test 2");


            MyDelegateTest dlgTest = new MyDelegateTest();
            dlgTest.Run();
        }
    }

    delegate int Calculator(int a, int b);
    class Logs
    {
        static int sum (int a, int b)
        {
            return a + b;
        }

        public static void showResult()
        {
            Action<int> log = (int a) => {
                Console.WriteLine(a);
            };
            Calculator calSum = sum;
            int result = calSum(2, 3);
            log(result);
        }

        // Test callback
        public delegate void ShowLog(string str);
        public static void Warning(string message, ShowLog log)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            log(message);
            Console.ResetColor();
        }

        public static void TestShowLog()
        {
            Warning("He thong sap qua tai!", (string log) => { 
                Console.WriteLine(log); 
            });
        }
    }

    // Multiple delegate: nếu delegate có trả về giá trị
    // khi tham chiếu đến nhiều hàm thì giá trị trả về
    // là giá trị của hàm được tham chiếu cuối cùng
    public class MyDelegateTest
    {
        private delegate int Add(int x, int y);

        public void Run()
        {
            var firstAdd = new Add(FirstAdd);
            var secondAdd = new Add(SecondAdd);
            var thirdAdd = new Add(ThirdAdd);

            var add1 = firstAdd + secondAdd;
            var rs = add1(10, 20);
            Console.WriteLine(rs);  //  Output: -10

            var add2 = secondAdd + firstAdd;
            rs = add2(10, 20);
            Console.WriteLine(rs);  //  Output: 30

            var add3 = firstAdd + secondAdd - thirdAdd;
            rs = add3(10, 20);
            Console.WriteLine(rs);  //  Output: -10
        }

        private int ThirdAdd(int x, int y)
        {
            return 2 * x + 2 * y;
        }

        private int SecondAdd(int x, int y)
        {
            return x - y;
        }

        private int FirstAdd(int x, int y)
        {
            return x + y;
        }
    }
}