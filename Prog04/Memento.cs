using System;

public class TextFileMemento {
  public string FileContent { get; set; }
  public DateTime Timestamp { get; set; }

  public TextFileMemento(string content) {
    FileContent = content;
    Timestamp = DateTime.Now;
  }
}

public interface IOriginator {
  object GetMemento();
  void SetMemento(object memento);
}