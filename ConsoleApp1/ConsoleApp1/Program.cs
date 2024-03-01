using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;


    class Program
    {
        static void Main(string[] args)
        {
            // Создание экземпляра класса MyClass
            MyClass myClassInstance = new MyClass();

            // Создание экземпляра класса MyTestClass
            MyTestClass testClass = new MyTestClass();

            // Задание 1: вывод методов с строковыми параметрами из класса MyClass
            testClass.PrintMethodsWithStringParameters("MyClass");

            // Задание 2: запись содержимого класса MyClass в файл
            testClass.WriteClassContentToFile("MyClass");

            // Задание 3: запись всех членов класса MyClass в файл *.cs
            testClass.WriteClassToFile("MyClass");

            Console.ReadLine(); // Пауза перед закрытием консоли (для удобства просмотра вывода)
        }
    }

