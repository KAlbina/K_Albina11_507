namespace Kozlova_Albina;


public class Person
{
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Email { get; set; } = string.Empty;

    public override string ToString() => $"Person {{ Name = {Name}, Age = {Age}, Email = {Email} }}";
}
