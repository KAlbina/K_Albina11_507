namespace StudentsJson;

public class Student
{
    public string Name { get; set; } = "";
    public int Age { get; set; }
    public string Group { get; set; } = "";

    public override string ToString() => $"{Name}, {Age} лет, группа {Group}";
}
