using System;
using System.Collections.Generic;
using System.IO;

public class FileSearcher {
  private string directory;

  public FileSearcher(string directory) {
    this.directory = directory;
  }

  public List<string> SearchByKeyword(string keyword) {
    List<string> results = new List<string>();

    if (!Directory.Exists(directory)) {
      Console.WriteLine(" Directory does not exist ");
      return results;
    }

    string[] files = Directory.GetFiles(directory, " *.txt ", SearchOption.AllDirectories);
    Console.WriteLine($" Searching for \"{keyword}\" in {files.Length} files... ");

    foreach (string file in files) {
      try {
        string content = File.ReadAllText(file);
        if (content.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0) {
          results.Add(file);
          Console.WriteLine($" Found: {Path.GetFileName(file)} ");
        }
      }
      catch {
      }
    }

    return results;
  }

  public Dictionary<string, List<string>> CreateIndex(List<string> keywords) {
    Dictionary<string, List<string>> index = new Dictionary<string, List<string>>();

    foreach (string keyword in keywords) {
      index[keyword] = new List<string>();
    }

    string[] files = Directory.GetFiles(directory, " *.txt ", SearchOption.AllDirectories);

    foreach (string file in files) {
      try {
        string content = File.ReadAllText(file).ToLower();

        foreach (string keyword in keywords) {
          if (content.Contains(keyword.ToLower())) {
            index[keyword].Add(file);
          }
        }
      }
      catch {
      }
    }

    return index;
  }

  public void DisplayIndex(List<string> keywords) {
    Dictionary<string, List<string>> index = CreateIndex(keywords);

    Console.WriteLine("\n    FILE INDEX    ");
    foreach (KeyValuePair<string, List<string>> pair in index) {
      Console.WriteLine($" \nKeyword: \"{pair.Key}\" - {pair.Value.Count} files ");
      foreach (string file in pair.Value) {
        FileInfo information = new FileInfo(file);
        Console.WriteLine($"  - {file} ({information.Length} bytes) ");
      }
    }
  }
}