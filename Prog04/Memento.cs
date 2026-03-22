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

public class Caretaker {  // добавила класс "Смотритель"
  private object memento;
  
  public void SaveState(IOriginator originator) {
    memento = originator.GetMemento();
  }

  public void RestoreState(IOriginator originator) {
    originator.SetMemento(memento);
  }
}
