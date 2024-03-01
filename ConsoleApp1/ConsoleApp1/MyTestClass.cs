using System;
using System.IO;
using System.Reflection;
using System.Text;

public class MyTestClass
{

    public void PrintMethodsWithStringParameters(string className)
    {
        Type type = Type.GetType(className);
        if (type != null)
        {
            Console.WriteLine($"Methods in class {className} with string parameters:");
            foreach (var method in type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public))
            {
                foreach (var parameter in method.GetParameters())
                {
                    if (parameter.ParameterType == typeof(string))
                    {
                        Console.WriteLine(method.Name);
                        break;
                    }
                }
            }
        }
        else
        {
            Console.WriteLine($"Class {className} not found.");
        }
    }
    // Метод для вывода всего содержимого класса в текстовый файл
    public void WriteClassContentToFile(string className)
    {
        Type type = Type.GetType(className);
        if (type != null)
        {
            string fileName = $"{className}.txt";
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.WriteLine($"Class: {className}");
                writer.WriteLine("Fields:");
                foreach (var field in type.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public))
                {
                    writer.WriteLine($"{field.FieldType} {field.Name}");
                }

                writer.WriteLine("Methods:");
                foreach (var method in type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public))
                {
                    writer.WriteLine($"{method.ReturnType} {method.Name}({string.Join(", ", Array.ConvertAll(method.GetParameters(), p => $"{p.ParameterType} {p.Name}"))})");
                }
            }
            Console.WriteLine($"Class content has been written to {fileName}");
        }
        else
        {
            Console.WriteLine($"Class {className} not found.");
        }
    }

    // Метод для записи всех членов класса в файл *.cs
    public void WriteClassToFile(string className)
    {
        Type type = Type.GetType(className);
        if (type != null)
        {
            string fileName = $"{className}.cs";
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.WriteLine("using System;");
                writer.WriteLine();
                writer.WriteLine($"public class {className}");
                writer.WriteLine("{");

                // Запись полей
                foreach (var field in type.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public))
                {
                    writer.WriteLine($"    {GetAccessModifier(field)} {field.FieldType} {field.Name};");
                }

                // Запись конструкторов
                foreach (var constructor in type.GetConstructors())
                {
                    writer.Write($"    {className}(");
                    ParameterInfo[] parameters = constructor.GetParameters();
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        writer.Write($"{parameters[i].ParameterType} {parameters[i].Name}");
                        if (i < parameters.Length - 1)
                            writer.Write(", ");
                    }
                    writer.WriteLine(") { }");
                }

                // Запись методов
                foreach (var method in type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public))
                {
                    writer.Write($"    {GetAccessModifier(method)} {method.ReturnType} {method.Name}(");
                    ParameterInfo[] parameters = method.GetParameters();
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        writer.Write($"{parameters[i].ParameterType} {parameters[i].Name}");
                        if (i < parameters.Length - 1)
                            writer.Write(", ");
                    }
                    writer.WriteLine(") { }");
                }

                writer.WriteLine("}");
            }
            // Console.WriteLine($"Class members have been written to {fileName}");
        }
        else
        {
            Console.WriteLine($"Class {className} not found.");
        }
    }

    // Вспомогательный метод для получения модификатора доступа члена
    private string GetAccessModifier(MemberInfo member)
    {
        if (member is FieldInfo field)
        {
            if (field.IsPrivate)
                return "private";
            else if (field.IsPublic)
                return "public";
            else if (field.IsFamily)
                return "protected";
            else if (field.IsAssembly)
                return "internal";
            else
                return "protected internal";
        }
        else if (member is ConstructorInfo constructor)
        {
            if (constructor.IsPrivate)
                return "private";
            else if (constructor.IsPublic)
                return "public";
            else if (constructor.IsFamily)
                return "protected";
            else if (constructor.IsAssembly)
                return "internal";
            else
                return "protected internal";
        }
        else if (member is MethodInfo method)
        {
            if (method.IsPrivate)
                return "private";
            else if (method.IsPublic)
                return "public";
            else if (method.IsFamily)
                return "protected";
            else if (method.IsAssembly)
                return "internal";
            else
                return "protected internal";
        }
        else
        {
            return "";
        }
    }
}
