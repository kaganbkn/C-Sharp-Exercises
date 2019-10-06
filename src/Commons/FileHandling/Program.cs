using System;
using System.Collections.Generic;
using System.IO;

namespace FileHandling
{
    class Program
    {
        static void ReadLines()
        {
            using (StreamReader reader = new StreamReader(@"C:\Workspace\C#\C-Sharp-Exercises\Example.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }                
            }

            // reader.Dispose(); --> Using kullanmasaydık bu şekilde dispose edebilirdik.
        }
        static void WriteFile()
        {
            using (StreamWriter writer = new StreamWriter(@"C:\Workspace\C#\C-Sharp-Exercises\Example.txt"))
            {
                writer.WriteLine("Deneme");
            }

        }


        static void Main(string[] args)
        {
            // WriteFile();
            // ReadLines();

            var file = new List<string>();
            // string[] lines=File.ReadAllLines(@"C:\Workspace\C#\C-Sharp-Exercises\Example.txt");

            foreach (var line in File.ReadAllLines(@"C:\Workspace\C#\C-Sharp-Exercises\README.md"))
            {
                file.Add(line);
                Console.WriteLine(line);
            }
            
            string allFile = File.ReadAllText(@"C:\Workspace\C#\C-Sharp-Exercises\README.md");

            File.WriteAllText(@"C:\Workspace\C#\C-Sharp-Exercises\Example.txt","Example");
            ReadLines();

            File.WriteAllLines(@"C:\Workspace\C#\C-Sharp-Exercises\Example.txt",file);
            ReadLines();

            File.AppendAllText(@"C:\Workspace\C#\C-Sharp-Exercises\Example.txt", "Appended Line");
            ReadLines();

            // byte[] img=File.ReadAllBytes(@"C:\Workspace\C#\C-Sharp-Exercises\Example.img");

            Console.Read();
        }
    }
}
