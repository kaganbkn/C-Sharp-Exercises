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
            // "Path"
            /*
            string tempPath = Path.GetTempPath();
            string tempName = Path.GetTempFileName();
            Console.WriteLine("Full Path : "+Path.Combine(tempPath,tempName));

            string path = "C:\\stagelist.txt";
            string extension = Path.GetExtension(path);
            string filename = Path.GetFileName(path);
            string filenameNoExtension = Path.GetFileNameWithoutExtension(path);
            string root = Path.GetPathRoot(path);

            Console.WriteLine("{0}\n{1}\n{2}\n{3}",
                extension,
                filename,
                filenameNoExtension,
                root);

            Console.Read();
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
            */
            // byte[] img=File.ReadAllBytes(@"C:\Workspace\C#\C-Sharp-Exercises\Example.img");

            CopyTo(@"C:\Users\kagan.beken\Desktop\New folder (3)\NativeDlls", @"C:\Users\kagan.beken\Desktop\New folder (3)\New folder", true);

            Console.Read();
        }
        public static void CopyTo(string sourceDirectory, string destDirectory, bool recursive)
        {
            var source = new DirectoryInfo(sourceDirectory);
            if (source == null)
                throw new ArgumentNullException("source");
            if (destDirectory == null)
                throw new ArgumentNullException("destDirectory");
            // If the source doesn't exist, we have to throw an exception.
            if (!source.Exists)
                throw new DirectoryNotFoundException(
                    "Source directory not found: " + source.FullName);
            // Compile the target.
            DirectoryInfo target = new DirectoryInfo(destDirectory);
            // If the target doesn't exist, we create it.
            if (!target.Exists)
                target.Create();
            // Get all files and copy them over.
            foreach (FileInfo file in source.GetFiles())
            {
                file.CopyTo(Path.Combine(target.FullName, file.Name), true);
            }
            // Return if no recursive call is required.
            if (!recursive)
                return;
            // Do the same for all sub directories.
            foreach (DirectoryInfo directory in source.GetDirectories())
            {
                // Don't move if directory is empty.
                var newSourceDirectory = new DirectoryInfo(Path.Combine(sourceDirectory, directory.Name));
                if (newSourceDirectory.GetFiles().Length < 1)
                    continue;
                CopyTo(newSourceDirectory.FullName, Path.Combine(target.FullName, directory.Name), recursive);
            }
        }
    }
}
