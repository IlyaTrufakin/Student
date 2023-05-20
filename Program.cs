using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Розробити клас, який описує студента.
//Передбачити в ньому наступні властивості: прізвище, ім'я, по батькові, група, вік, масив оцінок з програмування, адміністрування та дизайну.
//А також додати методи роботи з перерахованими даними:
//можливість встановлення/отримання оцінки,
//отримання середнього балу по заданому предмету,
//роздрук даних про студента.

namespace Student_Class
{
    public enum Subject : int
    {
        Programming = 0, Administration = 1, Design = 2
    }


    class Student
    {
        public string LastName { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Group { get; set; }
        public int Age { get; set; }
        private readonly List<int>[] Grades = new List<int>[3]; // массив оценок по 3 предметам

        // конструкторы и методы ----------------------------------------------------------------------------------------------------------------------
        public Student(string _lastname, string _name, string _patronymic, int _age, string _group, List<int> _gradesProgramming, List<int> _gradesAdministration, List<int> _gradesDesign)
        {
            LastName = _lastname;
            Name = _name;
            Patronymic = _patronymic;
            Age = _age;
            Group = _group;

            Grades[(int)Subject.Programming] = new List<int>();
            Grades[(int)Subject.Administration] = new List<int>();
            Grades[(int)Subject.Design] = new List<int>();



            if (_gradesProgramming != null)
            {
                Grades[(int)Subject.Programming].AddRange(_gradesProgramming);
            }


            if (_gradesAdministration != null)
            {
                Grades[(int)Subject.Administration].AddRange(_gradesAdministration);
            }


            if (_gradesDesign != null)
            {
                Grades[(int)Subject.Design].AddRange(_gradesDesign);
            }

        }

        public void SetGrade(Subject _subject, int grade) // добавляем оценку в список для данного предмета
        {
            Grades[(int)_subject].Add(grade);
        }


        public List<int> GetGrade(Subject _subject) => Grades[(int)_subject]; // возвращаем оценки для данного предмета


        public float GetAverageGrade(Subject _subject) // определение среднего бала по предмету
        {
            List<int> subjectGrades = Grades[(int)_subject];
            if (subjectGrades.Count == 0)
            {
                return 0;
            }

            float sum = 0;
            foreach (float grade in subjectGrades)
            {
                sum += grade;
            }

            return sum / subjectGrades.Count;
        }



        public void PrintStudentInfo()
        {
            Console.WriteLine($"\n_____________________________________________________________________");
            Console.WriteLine($"Full name: {LastName} {Name} {Patronymic}");
            Console.WriteLine($"Group: {Group}");
            Console.WriteLine($"Age: {Age}");

            Console.WriteLine("Grades:");
            Console.WriteLine($"\nProgramming: {string.Join(", ", Grades[0])}");
            Console.WriteLine($"Average grade: {GetAverageGrade(Subject.Programming):0.##}");

            Console.WriteLine($"\nAdministration: {string.Join(", ", Grades[1])}");
            Console.WriteLine($"Average grade: {GetAverageGrade(Subject.Administration):0.##}");

            Console.WriteLine($"\nDesign: {string.Join(", ", Grades[2])}");
            Console.WriteLine($"Average grade: {GetAverageGrade(Subject.Design):0.##}");
        }



    }
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Student> students = new List<Student>();
            students.Add(new Student("Иванов", "Иван", "Иванович", 25, "СПУ221",
                    new List<int> { 11, 9, 12, 10 },
                    new List<int> { 7 },
                    new List<int> { 10, 8, 9, 12, 7 }));
            students.Add(new Student("Петров", "Петр", "Петрович", 32, "СБУ221",
                    new List<int> { 12, 11, 7, 8 },
                    new List<int> { },
                    new List<int> { 7, 12, 12, 8, 10 }));

            foreach (Student student in students)
            {
                student.PrintStudentInfo();
            }

            Console.WriteLine("\n------------------------------------------------------------------------");
            Console.WriteLine("------------------------------------------------------------------------");
            Console.WriteLine("\nAdding grades to students: ");
            // установка оценки студенту по фамилии
            var theStudent = students.Where(s => s.LastName == "Иванов").FirstOrDefault();
            if (theStudent != null) // Студент с фамилией "Иванов" найден, ставим оценку
            {
                theStudent.SetGrade(Subject.Administration, 5);
            }
            else // Студент с фамилией "Иванов" не найден, выводим сообщение об отсутствии студента с заданной фамилией
            {
                Console.WriteLine("\nthis student is not on the list...");
            }

            students[1].SetGrade(Subject.Administration, 10); // установка оценки конкретному студенту по конкретному предмету
            Console.WriteLine("\n------------------------------------------------------------------------");
            Console.WriteLine("------------------------------------------------------------------------");
            Console.WriteLine("\nAfter grades change: ");
            students[0].PrintStudentInfo();
            students[1].PrintStudentInfo();

            // получение оценок по конкретному предмету, всех студентов
            Console.WriteLine("\n------------------------------------------------------------------------");
            Console.WriteLine("------------------------------------------------------------------------");
            foreach (Student student in students)
            {
                Console.WriteLine("\nCtudents LastName: " + student.LastName + "\t Grades in the subject \"design\": " + string.Join("; ", student.GetGrade(Subject.Design)));
            }

        }
    }
}
