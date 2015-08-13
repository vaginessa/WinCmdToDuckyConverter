namespace BatToDuckyConverter
{
    using System.Collections.Generic;
    using System.IO;

    class Program
    {
        static void Main(string[] args)
        {
            var batchFilePath = args[0];
            var txtFilePath = args[1];

            var converter = new DuckyConverter();
            converter.ConvertBatchToDucky(batchFilePath, txtFilePath);
        }
    }

    class DuckyConverter
    {
        public void ConvertBatchToDucky(string batPath, string txtPath)
        {
            string[] commandsToStartCommandLineShell = { "DELAY 3000", "GUI R", "DELAY 500", "STRING cmd", "ENTER", "DELAY 500" };
            string[] commandsToExitCommandLineShell = { "STRING exit", "ENTER" };

            var linesFromBATFile = File.ReadAllLines(batPath);
            var newDuckyLines = new List<string>();

            foreach (var lineFromBAT in linesFromBATFile)
            {
                newDuckyLines.Add("STRING " + lineFromBAT);
                newDuckyLines.Add("ENTER");
                newDuckyLines.Add("DELAY 500");
            }

            var duckyLinesMadeFromBAT = newDuckyLines.ToArray();           

            // Combine all 3 arrays into one big array
            var finalArraySize = 
                  commandsToStartCommandLineShell.Length 
                + duckyLinesMadeFromBAT.Length 
                + commandsToExitCommandLineShell.Length;

            var finalArray = new string[finalArraySize];
            
            commandsToStartCommandLineShell.CopyTo(finalArray, 0);
            duckyLinesMadeFromBAT.CopyTo(finalArray, commandsToStartCommandLineShell.Length);
            commandsToExitCommandLineShell.CopyTo(finalArray, commandsToStartCommandLineShell.Length + duckyLinesMadeFromBAT.Length);

            File.WriteAllLines(txtPath, finalArray);
        }
    }
}
