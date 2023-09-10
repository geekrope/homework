public class Triangle
{
    private double a;
    private double b;
    private double c;

    public double Perimeter
    {
        get => a + b + c;
    }

    public Triangle(double a, double b, double c)
    {
        this.a = a;
        this.b = b;
        this.c = c;
    }
}

public class EquilateralTriangle : Triangle
{
    public EquilateralTriangle(double side) : base(side, side, side)
    {

    }
}

public class User
{
    private string name;

    public void SendMessage(User user, string message)
    {

    }
    public void Post(string message)
    {

    }
    public virtual string GetInfo()
    {
        return $"Имя={name}";
    }

    public User(string name)
    {
        this.name = name;
    }
}

public class Person : User
{
    private int age;

    public override string GetInfo()
    {
        return base.GetInfo() + $", Возраст={age}";
    }
    public void Subscribe(User user)
    {

    }

    public Person(string name, int age) : base(name)
    {
        this.age = age;
    }
}

public class Community : User
{
    private string description;

    public override string GetInfo()
    {
        return base.GetInfo() + $", Описание={description}";
    }

    public Community(string name, string description) : base(name)
    {
        this.description = description;
    }
}