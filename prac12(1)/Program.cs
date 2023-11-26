using System;

public delegate void PropertyEventHandler(object sender, PropertyEventArgs e);

// Аргументы события для передачи имени измененного свойства
public class PropertyEventArgs : EventArgs
{
    public string PropertyName { get; }

    public PropertyEventArgs(string propertyName)
    {
        PropertyName = propertyName;
    }
}

// Интерфейс, уведомляющий о изменении свойства
public interface IPropertyChanged
{
    event PropertyEventHandler PropertyChanged;
}

// Класс, реализующий интерфейс и содержащий свойство
public class MyClass : IPropertyChanged
{
    private string _myProperty;

    public string MyProperty
    {
        get { return _myProperty; }
        set
        {
            if (_myProperty != value)
            {
                _myProperty = value;
                OnMyPropertyChanged();
            }
        }
    }

    // Реализация события из интерфейса
    public event PropertyEventHandler PropertyChanged;

    // Метод для вызова события при изменении свойства
    protected virtual void OnMyPropertyChanged()
    {
        PropertyChanged?.Invoke(this, new PropertyEventArgs(nameof(MyProperty)));
    }
}

class Program
{
    static void Main()
    {
        MyClass myObject = new MyClass();

        // Подписка на событие изменения свойства
        myObject.PropertyChanged += (sender, e) =>
        {
            Console.WriteLine($"Свойство '{e.PropertyName}' было изменено.");
        };

        // Изменение свойства
        myObject.MyProperty = "Новое значение";
    }
}
