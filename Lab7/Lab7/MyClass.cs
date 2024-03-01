using System;
using System.IO;
using System.Reflection;

public class MyClass
{
    private int privateField;
    public string PublicField;

    public MyClass()
    {
        privateField = 0;
        PublicField = "";
    }

    public int GetPrivateField()
    {
        return privateField;
    }

    public void SetPrivateField(int value)
    {
        privateField = value;
    }

    public string MethodWithNoParameters()
    {
        return "Method with no parameters";
    }

    public int MethodWithParameters(int a, int b)
    {
        return a + b;
    }

    public string MethodWithStrings(string str1, string str2)
    {
        return str1 + " " + str2;
    }
}

public class MyTestClass
{
    public void PrintStringParameterMethods(string className)
    {
        Type type = Type.GetType(className);
        if (type != null)
        {
            Console.WriteLine($"Methods in class {className} with string parameters:");

            foreach (var method in type.GetMethods())
            {
                var parameters = method.GetParameters();
                foreach (var parameter in parameters)
                {
                    if (parameter.ParameterType == typeof(string))
                    {
                        Console.WriteLine($"- {method.Name}");
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

    public void InvokeMethodFromFile(string className, string methodName)
    {
        Type type = Type.GetType(className);
        if (type != null)
        {
            MethodInfo method = type.GetMethod(methodName);
            if (method != null)
            {
                object instance = Activator.CreateInstance(type);

                // Assuming the values for method parameters are stored in a text file
                string fileName = $"{className}_{methodName}_Parameters.txt";
                if (File.Exists(fileName))
                {
                    string[] parameterValues = File.ReadAllLines(fileName);
                    object[] parameters = new object[parameterValues.Length];

                    for (int i = 0; i < parameterValues.Length; i++)
                    {
                        // Convert parameter values to the appropriate type
                        parameters[i] = Convert.ChangeType(parameterValues[i], method.GetParameters()[i].ParameterType);
                    }

                    // Invoke the method with the provided parameters
                    object result = method.Invoke(instance, parameters);
                    Console.WriteLine($"Result of {methodName}: {result}");
                }
                else
                {
                    Console.WriteLine($"File {fileName} not found.");
                }
            }
            else
            {
                Console.WriteLine($"Method {methodName} not found in class {className}.");
            }
        }
        else
        {
            Console.WriteLine($"Class {className} not found.");
        }
    }

    public void WriteParametersToFile(string className, string methodName, string[] parameters)
    {
        // Write parameters to a text file
        string fileName = $"{className}_{methodName}_Parameters.txt";
        File.WriteAllLines(fileName, parameters);
        Console.WriteLine($"Parameters written to {fileName}");
    }
}


