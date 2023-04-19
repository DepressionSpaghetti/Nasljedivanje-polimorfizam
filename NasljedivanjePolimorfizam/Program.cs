using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasljedivanjePolimorfizam
{
    internal class Program
    {
        #region Dessert
        class Dessert
        {
            string name;
            double weight;
            int calories;


            public Dessert(string name, double weight, int calories)
            {
                this.name = name;
                this.weight = weight;
                this.calories = calories;
            }

            public string Name { get => name; set => name = value; }
            public double Weight { get => weight; set => weight = value; }
            public int Calories { get => calories; set => calories = value; }

            public override string ToString()
            {
                string s = "Name: {0}\nWeight: {1}\nCalories: {2}" + this.name + this.weight + this.calories;

                return s;
            }

            virtual public string getDessertType()
            {
                return "dessert";
            }

        }
        class Cake : Dessert
        {
            bool containsGluten;
            string cakeType;

            Cake(bool containsGluten, string cakeType, string name, double weight, int calories) : base(name, weight, calories)
            {
                this.containsGluten = containsGluten;
                this.cakeType = cakeType;

            }

            public bool ContainsGluten { get => containsGluten; set => containsGluten = value; }
            public string CakeType { get => cakeType; set => cakeType = value; }

            public override string ToString()
            {
                string s = "Name: {0}\nWeight: {1}\nCalories: {2}\nContains gluten: {3}\nType: {4}"
                    + Name + Weight + Calories + this.containsGluten + this.cakeType;

                return s;
            }
            public override string getDessertType()
            {
                return this.cakeType + "cake";
            }

        }
        class IceCream : Dessert
        {
            string flavour, color;

            IceCream(string flavour, string color, string name, double weight, int calories) : base(name, weight, calories)
            {
                this.flavour = flavour;
                this.color = color;

            }

            public string Flavour { get => flavour; set => flavour = value; }
            public string Color { get => color; set => color = value; }

            public override string ToString()
            {
                string s = "Name: {0}\nWeight: {1}\nCalories: {2}\nFlavour:{3}\nColor: {4}"
                    + Name + Weight + Calories + this.flavour + this.color;

                return s;
            }
            public override string getDessertType()
            {
                return "Ice cream";
            }

        }
        #endregion
        class Person
        {
            string name, surname;
            int age;

            public Person(string name, string surname, int age)
            {
                this.name = name;
                this.surname = surname;
                this.age = age;
            }

            public string Name { get => name; set => name = value; }
            public string Surname { get => surname; set => surname = value; }
            public int Age { get => age; set => age = value; }

            public override bool Equals(object obj)
            {
                return obj is Person person &&
                       name == person.name &&
                       surname == person.surname &&
                       age == person.age;
            }

            public override string ToString()
            {
                return this.name + " " + this.surname + " " + this.age;
            }

        }
        class Student : Person
        {
            string studentID;
            short academicYear;

            public Student(string studentID, short academicYear, string name, string surname, int age) : base(name, surname, age)
            {
                this.studentID = studentID;
                this.academicYear = academicYear;
            }

            public string StudentID { get => studentID; set => studentID = value; }
            public short AcademicYear { get => academicYear; set => academicYear = value; }

            public override bool Equals(object obj)
            {
                return obj is Student student &&
                       base.Equals(obj) &&
                       studentID == student.studentID;
            }
        }
        class Teacher : Person
        {
            string email, subject;
            double salary;

            Teacher(string email,string subject, double salary, string name, string surname, int age) : base(name, surname, age)
            {
                this.email = email;
                this.subject = subject;
                this.salary = salary;
            }

            public string Email { get => email; set => email = value; }
            public string Subject { get => subject; set => subject = value; }
            public double Salary { get => salary; set => salary = value; }

            public override bool Equals(object obj)
            {
                return obj is Teacher teacher &&
                       base.Equals(obj) &&
                       email == teacher.email;
            }
            public void incraseSalary(int number)
            {
                this.salary = this.salary * ((number / 100) + 1);

            }
            public static void incraseSalary(int number, params Teacher[] list)
            {
                for (int i = 0; i < list.Length; i++)
                {
                    list[i].incraseSalary(number);
                }
            }
        }

        static void Main(string[] args)
        {
            Person p1 = new Person("Ivo", "Ivic", 20);
            Person p2 = new Person("Ivo", "Ivic", 20);
            Person p3 = new Student("Ivo", "Ivic", 20, "0036312123", (short)3);
            Person p4 = new Student("Marko", "Marić", 25, "0036312123", (short)5);

            Console.WriteLine(p1.Equals(p2));
            Console.WriteLine(p1.Equals(p3));
            Console.WriteLine(p3.Equals(p4));
        }
    }
}
