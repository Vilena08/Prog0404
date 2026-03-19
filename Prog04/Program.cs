using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program {
  static void Main(string[] arguments) {
    Console.WriteLine("   TEXT EDITOR    \n");

    string workingDirectory = "Documents";
    if (!Directory.Exists(workingDirectory))
      Directory.CreateDirectory(workingDirectory);

    string filePath = Path.Combine(workingDirectory, " document.txt ");
    TextEditor editor = new TextEditor(filePath);
    FileSearcher searcher = new FileSearcher(workingDirectory);

    while (true) {
      Console.WriteLine(" \n    MENU     ");
      Console.WriteLine(" 1. Add line ");
      Console.WriteLine(" 2. Show content ");
      Console.WriteLine(" 3. Undo ");
      Console.WriteLine(" 4. Save ");
      Console.WriteLine(" 5. Binary backup ");
      Console.WriteLine(" 6. XML backup ");
      Console.WriteLine(" 7. Restore backup ");
      Console.WriteLine(" 8. Search files ");
      Console.WriteLine(" 9. File index ");
      Console.WriteLine(" 0. Save and exit ");
      Console.Write(" Select action: ");

      string choice = Console.ReadLine();

      switch (choice) {
        case "1":
          Console.Write(" Enter text: ");
          string text = Console.ReadLine();
          editor.AddLine(text);
          break;

        case "2":
          editor.DisplayContent();
          break;

        case "3":
          editor.Undo();
          break;

        case "4":
          editor.SaveToFile();
          break;

        case "5":
          editor.BinaryBackup();
          break;

        case "6":
          editor.XmlBackup();
          break;

        case "7":
          Console.Write(" Enter the backup file name: ");
          string backupFile = Console.ReadLine();
          if (File.Exists(backupFile))
            editor.RestoreFromBackup(backupFile);
          else
            Console.WriteLine(" File not found ");
          break;

        case "8":
          Console.Write(" Enter keyword: ");
          string keyword = Console.ReadLine();
          List<string> results = searcher.SearchByKeyword(keyword);
          Console.WriteLine($" \nFiles found: {results.Count} ");
          break;

        case "9":
          Console.Write(" Enter keywords separated by commas: ");
          string input = Console.ReadLine();
          List<string> keywords = input.Split(',').Select(key => key.Trim()).ToList();
          searcher.DisplayIndex(keywords);
          break;

        case "0":
          editor.SaveAndExit();
          return;

        default:
          Console.WriteLine(" Incorrect selection. Try again. ");
          break;
      }
    }
  }
}