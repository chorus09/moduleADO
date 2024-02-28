using moduleADO.Models.Observer;

namespace moduleADO.Models;

public class Client {

    private int id;
    private string firstName;
    private string lastName;
    private string middleName;
    private string email;
    private string phone;

    private List<IClientObserver> observers = new();

    public Client(int id, string firstName, string lastName, string middleName, string email, string phone) {
        this.id = id;
        this.firstName = firstName;
        this.lastName = lastName;
        this.middleName = middleName;
        this.email = email;
        this.phone = phone;
    }
    public Client(string firstName, string lastName, string middleName, string email, string phone) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.middleName = middleName;
        this.email = email;
        this.phone = phone;
    }

    public void Attach(IClientObserver observer) {
        observers.Add(observer);
    }

    public void Detach(IClientObserver observer) {
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

    public string FirstName {
        get { return firstName; }
        set {
            if (firstName != value) _updatedValue = firstName;
            firstName = value; Notify(_updatedValue); 
        }
    }

    public string LastName {
        get { return lastName; }
        set {
            if (lastName != value) _updatedValue = lastName;
            lastName = value; Notify(_updatedValue);
        }
    }

    public string MiddleName {
        get { return middleName; }
        set {
            if (middleName != value) _updatedValue = middleName;
            middleName = value; Notify(_updatedValue);
        }
    }

    public string Email {
        get { return email; }
        set {
            if (email != value) _updatedValue = email;
            email = value; Notify(_updatedValue);
        }
    }

    public string Phone {
        get { return phone; }
        set {
            if (phone != value) _updatedValue = phone;
            phone = value; Notify(_updatedValue);
        }
    }
}

