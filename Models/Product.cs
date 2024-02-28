using moduleADO.Models.Observer;

namespace moduleADO.Models;

public class Product {
    private int id;
    private string code;
    private string name;
    private string email;

    private List<IProductObserver> observers = new();

    public Product(int id, string code, string name, string email) {
        this.id = id;
        this.code = code;
        this.name = name;
        this.email = email;
    }
    public Product(string code, string name, string email) {
        this.code = code;
        this.name = name;
        this.email = email;
    }
    public void Attach(IProductObserver observer) {
        observers.Add(observer);
    }

    public void Detach(IProductObserver observer) {
        observers.Remove(observer);
    }

    private void Notify(object updatedValue) {
        foreach (var observer in observers) {
            observer.Update(this, updatedValue);
        }
    }
    private object _updatedValue;
    public int Id {
        get { return id; }
        set {
            if (id != value) _updatedValue = id;
            id = value; Notify(_updatedValue);
        }
    }

    public string Code {
        get { return code; }
        set {
            if (code != value) _updatedValue = code;
            code = value; Notify(_updatedValue);
        }
    }

    public string Name {
        get { return name; }
        set {
            if (name != value) _updatedValue = name;
            name = value; Notify(_updatedValue);
        }
    }

    public string Email {
        get { return email; }
        set {
            if (email != value) _updatedValue = email;
            email = value; Notify(_updatedValue);
        }
    }
}
