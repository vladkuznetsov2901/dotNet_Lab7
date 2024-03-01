using System;



    public class MyClass
    {
        // Поля различных типов и различным уровнем доступа
        private int privateField;
        public string publicField;
        protected double protectedField;
        internal static bool internalField;

        // Перегрузка конструкторов
        public MyClass()
        {
            // Инициализация полей по умолчанию
        }

        public MyClass(int privateFieldValue, string publicFieldValue, double protectedFieldValue)
        {
            privateField = privateFieldValue;
            publicField = publicFieldValue;
            protectedField = protectedFieldValue;
        }

        // Методы с различным набором аргументов и различным типом возвращаемого значения
        public void Method1()
        {
            Console.WriteLine("Method1 has been called.");
        }

        public int Method2(int x, int y)
        {
            return x + y;
        }

        protected string Method3(string input)
        {
            return "Hello, " + input;
        }

        internal void Method4(bool flag)
        {
            if (flag)
                Console.WriteLine("Method4 has been called with true.");
            else
                Console.WriteLine("Method4 has been called with false.");
        }

        // Реализация интерфейса IInterface1
        public void Method1Interface1()
        {
            Console.WriteLine("Method1Interface1 has been called.");
        }

        public void Method2Interface1()
        {
            Console.WriteLine("Method2Interface1 has been called.");
        }

        // Реализация интерфейса IInterface2
        public void Method1Interface2()
        {
            Console.WriteLine("Method1Interface2 has been called.");
        }

        public void Method2Interface2()
        {
            Console.WriteLine("Method2Interface2 has been called.");
        }
    }

