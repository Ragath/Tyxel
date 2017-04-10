using System;
using System.IO;
using System.Linq;
using Tyxel.Model;

namespace Tyxel
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!args.Any())
            {
                Console.WriteLine("Please use this program to open a <project>.json file.");
                Console.WriteLine("Drag & drop the *.json file onto the .exe is one way of doing so.");
                return;
            }

            var input = string.Join(" ", args);
            if (!File.Exists(input))
                throw new FileNotFoundException();


            ProjectConfig project;
            if (Path.GetExtension(input).Equals(".pyxel", StringComparison.InvariantCultureIgnoreCase))
                project = ProjectManager.CreateProject(input);
            else
                project = ProjectManager.LoadProject(input);


            var pyxelPath = project.Pyxel;
            Console.WriteLine($"Observing: {pyxelPath}");
            using (var watcher = new Watcher(pyxelPath, filename => PyxelProcessing.ProcessFile(project)))
            {
                while (true)
                    watcher.WaitForChange();
            }
        }       

    }
}
