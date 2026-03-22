using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class TextFile {
  public string FullPath { get; set; }
  public List<string> Content { get; set; }
  public DateTime LastChange { get; set; }

  public TextFile() {
    Content = new List<string>();
  }

  public TextFile(string path) {
    FullPath = path;
    Content = new List<string>();

    if (File.Exists(path)) {
      Content.AddRange(File.ReadAllLines(path));
      LastChange = File.GetLastWriteTime(path);
    }
    else {
      LastChange = DateTime.Now;
    }
  }

  public void BinarySerialize(string filePath) {
    using (FileStream stream = new FileStream(filePath, FileMode.Create)) {
      BinaryFormatter formatter = new BinaryFormatter();
      formatter.Serialize(stream, this);
      stream.Flush();
      Console.WriteLine($" Binary Serialization: {filePath} ");
    }
  }

  public static TextFile BinaryDeserialize(string filePath) {
    using (FileStream stream = new FileStream(filePath, FileMode.Open)) {
      BinaryFormatter formatter = new BinaryFormatter();
      TextFile deserialized = (TextFile)formatter.Deserialize(stream);
      Console.WriteLine($" Binary Deserialization: {filePath} ");
      return deserialized;
    }
  }

  public void XmlSerialize(string filePath) {
    XmlSerializer serializer = new XmlSerializer(typeof(TextFile));
    using (FileStream stream = new FileStream(filePath, FileMode.Create)) {
      serializer.Serialize(stream, this);
      Console.WriteLine($" XML Serialization: {filePath} ");
    }
  }

  public static TextFile XmlDeserialize(string filePath) {
    XmlSerializer serializer = new XmlSerializer(typeof(TextFile));
    using (FileStream stream = new FileStream(filePath, FileMode.Open)) {
      return (TextFile)serializer.Deserialize(stream);
    }
  }

  public void Save() {
    File.WriteAllLines(FullPath, Content);
    LastChange = DateTime.Now;
    Console.WriteLine($" File saved: {FullPath} ");
  }

  public void Print() {
    Console.WriteLine($" \n--- File: {FullPath} --- ");
    Console.WriteLine($" Change: {LastChange} ");
    Console.WriteLine($" Count of lines: {Content.Count} ");

    if (Content.Count == 0) {
      Console.WriteLine(" File is empty ");
    }
    else {
      for (int indexText = 0; indexText < Content.Count; ++indexText) {
        Console.WriteLine($"{indexText + 1}: {Content[indexText]}");
      }
    }
  }
}
