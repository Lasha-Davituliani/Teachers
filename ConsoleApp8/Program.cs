using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

public class Teacher
{
    [Key]
    public int TeacherId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Gender { get; set; }
    public string Subject { get; set; }

    public virtual ICollection<Pupil> Pupils { get; set; }
}

public class Pupil
{
    [Key]
    public int PupilId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Gender { get; set; }
    public string Class { get; set; }

    public virtual ICollection<Teacher> Teachers { get; set; }
}

public class SchoolContext : DbContext
{
    public SchoolContext() : base("Server=DESKTOP-SPJL6RF;Database=NETII;Trusted_Connection=True;")
    {
    }

    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Pupil> Pupils { get; set; }
}

public class TeacherService
{
    public Teacher[] GetAllTeachersByStudent(string studentName)
    {
        using (var context = new SchoolContext())
        {
            return context.Teachers
                .Where(t => t.Pupils.Any(p => p.FirstName == studentName))
                .ToArray();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        var teacherService = new TeacherService();
        var teachersTeachingGeorge = teacherService.GetAllTeachersByStudent("George");

        Console.WriteLine("Teachers who teach a student named George:");
        foreach (var teacher in teachersTeachingGeorge)
        {
            Console.WriteLine($"{teacher.FirstName} {teacher.LastName}, Subject: {teacher.Subject}");
        }
    }
}
