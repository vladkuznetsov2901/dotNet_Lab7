using Lab7.DebugPrintApp;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using static Lab7.Form1;

namespace Lab7
{
    public partial class Form1 : Form
    {
        private SampleObject sampleObject;

        public Form1()
        {
            InitializeComponent();
            InitializeSampleObject();
            BindControls();
        }

        private void InitializeSampleObject()
        {
            sampleObject = new SampleObject
            {
                Name = "John Doe",
                Age = 30,
                Description = "This won't be printed"
            };
        }

        private void BindControls()
        {
            textBox1.DataBindings.Add("Text", sampleObject, "Name");
            numericUpDown1.DataBindings.Add("Value", sampleObject, "Age");
            textBox2.DataBindings.Add("Text", sampleObject, "Description");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DebugPrintHelper.PrintToFile(sampleObject, "DebugPrintOutput.txt");
            MessageBox.Show("Debug Print Result saved to file", "Debug Print Result");
        }

        public class SampleObject
        {
            [DebugPrint]
            public string Name { get; set; }

            [DebugPrint("{0}")]
            public int Age { get; set; }
            [DebugPrint]

            public string Description { get; set; }
        }
    }

    namespace DebugPrintApp
    {
        [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
        public class DebugPrintAttribute : Attribute
        {
            public string Format { get; }

            public DebugPrintAttribute(string format = "{0}")
            {
                Format = format;
            }
        }

        public class DebugPrintHelper
        {
            public static void PrintToFile(object obj, string fileName)
            {
                if (obj == null)
                {
                    File.WriteAllText(fileName, "Object is null");
                    return;
                }

                Type type = obj.GetType();
                PropertyInfo[] properties = type.GetProperties();
                FieldInfo[] fields = type.GetFields();

                string result = $"{type.Name} Debug Print:\n";

                foreach (var property in properties)
                {
                    if (property.GetCustomAttribute(typeof(DebugPrintAttribute)) is DebugPrintAttribute attribute)
                    {
                        object value = property.GetValue(obj);
                        result += $"{property.Name} = {string.Format(attribute.Format, value)}\n";
                        result += new string('-', 20) + "\n";
                    }
                }

                foreach (var field in fields)
                {
                    if (field.GetCustomAttribute(typeof(DebugPrintAttribute)) is DebugPrintAttribute attribute)
                    {
                        object value = field.GetValue(obj);
                        result += $"{field.Name} = {string.Format(attribute.Format, value)}\n";
                        result += new string('-', 20) + "\n";
                    }
                }

                File.WriteAllText(fileName, result);
            }
        }
    }
}
