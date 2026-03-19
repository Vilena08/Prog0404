using System;
using System.Collections.Generic;
using System.IO;

public class TextEditor : IOriginator {
  private TextFile textFile;
  private object savedState;
  private string filePath;

  public TextEditor(string path) {
    this.filePath = path;
    this.textFile = new TextFile(path);
    Console.WriteLine($" Editor opened. File: {path} ");
  }

  public void AddLine(string text) {
    if (string.IsNullOrWhiteSpace(text)) {
      Console.WriteLine(" Text cannot be empty ");
      return;
    }

    textFile.Content.Add(text);
    Console.WriteLine($" Line added: \"{text}\" ");
  }

  public void DisplayContent() {
    textFile.Print();
  }

  public void SaveState() {
    savedState = GetMemento();
    Console.WriteLine(" State saved ");
  }

  public void Undo() {
    if (savedState != null) {
      SetMemento(savedState);
      Console.WriteLine(" State restored ");
      DisplayContent();
    }
    else {
      Console.WriteLine(" No saved state ");
    }
  }

  public void SaveToFile() {
    textFile.Save();
  }

  public void SaveAndExit() {
    SaveToFile();
    Console.WriteLine(" Editor closed ");
  }

  public object GetMemento() {
    string content = string.Join(Environment.NewLine, textFile.Content);
    return new TextFileMemento(content);
  }

  public void SetMemento(object memento) {
    if (memento is TextFileMemento) {
      TextFileMemento memory = (TextFileMemento)memento;
      textFile.Content.Clear();
      textFile.Content.AddRange(memory.FileContent.Split(new[] { Environment.NewLine }, StringSplitOptions.None));
      textFile.LastChange = memory.Timestamp;
    }
  }

  public void BinaryBackup() {
    string backupPath = $" backup_{DateTime.Now:yyyyMMdd_HHmmss}.bin ";
    textFile.BinarySerialize(backupPath);
  }

  public void XmlBackup() {
    string backupPath = $" backup_{DateTime.Now:yyyyMMdd_HHmmss}.xml ";
    textFile.XmlSerialize(backupPath);
  }

  public void RestoreFromBackup(string backupFile) {
    try {
      TextFile restored = null;

      if (backupFile.EndsWith(" .bin "))
        restored = TextFile.BinaryDeserialize(backupFile);
      else if (backupFile.EndsWith(" .xml "))
        restored = TextFile.XmlDeserialize(backupFile);
      else {
        Console.WriteLine(" Unknown file format ");
        return;
      }

      if (restored != null) {
        textFile = restored;
        Console.WriteLine(" Restored from backup ");
        DisplayContent();
      }
    }
    catch (Exception ex) {
      Console.WriteLine($" Restore error: {ex.Message} ");
    }
  }
}