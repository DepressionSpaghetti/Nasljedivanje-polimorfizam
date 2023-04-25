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

            public Cake(string name, double weight, int calories, bool containsGluten, string cakeType) : base(name, weight, calories)
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

            public IceCream(string name, double weight, int calories, string flavour, string color) : base(name, weight, calories)
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
        #region People
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

            public Student(string name, string surname, int age, string studentID, short academicYear) : base(name, surname, age)
            {
                this.studentID = studentID;
                this.academicYear = academicYear;
            }

            public string StudentID { get => studentID; set => studentID = value; }
            public short AcademicYear { get => academicYear; set => academicYear = value; }

            public override bool Equals(object obj)
            {
                if(this==obj) return true;

                if(!(obj is Student)) return false;

                Student other = (Student)obj;
                if (!studentID.Equals(other.studentID)) return false;

                return true;

            }
        }
        class Teacher : Person
        {
            string email, subject;
            double salary;

            public Teacher(string email, string subject, double salary, string name, string surname, int age) : base(name, surname, age)
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
        #endregion
        #region Competition
        class CompetitionEntry
        {
            Teacher teacher;
            Dessert dessert;
            private Student[] students;
            private int[] ratings;
            private int idx;
            public CompetitionEntry(Teacher teacher, Dessert dessert)
            {
                this.teacher = teacher;
                this.dessert = dessert;
            }

            public bool addEntry(Student student, int number)
            {
                if (idx == 3)
                {
                    return false;
                }
                foreach (Student s in students)
                {
                    if (s != null && s.Equals(student))
                        return false;
                }
                Students[idx] = student;
                ratings[idx] = number;
                idx++;
                return true;
            }
            public double getRating()
            {
                if (idx == 0) return 0;

                double sum = 0;
                for (int i = 0; i < idx; i++)
                    sum += ratings[i];

                return sum / idx;
            }

            internal Teacher Teacher { get => teacher; set => teacher = value; }
            internal Dessert Dessert { get => dessert; set => dessert = value; }
            public int[] Ratings { get => ratings; set => ratings = value; }
            public int Idx { get => idx; set => idx = value; }
            public Student[] Students { get => students; set => students = value; }
        }
        class UniMasterChef
        {
            private CompetitionEntry[] entries;
            private int idx = 0;

            public UniMasterChef(int noOfEntries)
            {
                this.entries = new CompetitionEntry[noOfEntries];
            }
            public bool addEntry(CompetitionEntry entry)
            {
                if (idx == this.entries.Length) return false;

                foreach (CompetitionEntry e in entries)
                {
                    if (e != null && e.Equals(entry))
                        return false;
                }
                entries[idx++] = entry;
                return true;
            }
            public Dessert getBestDessert()
            {
                if (idx == 0) return null;

                double max = entries[0].getRating();
                int maxIdx = 0;

                for (int i = 0; i < idx; i++)
                {
                    if (entries[i].getRating() > max)
                    {
                        max = entries[i].getRating();
                        maxIdx = i;
                    }
                }

                return entries[maxIdx].Dessert;
            }
            public static Person[] getInvolvedPeople(CompetitionEntry entry)
            {

                Person[] retVal = new Person[4];

                int idx = 0;

                retVal[idx++] = entry.Teacher;

                foreach (Student s in entry.Students)
                {
                    retVal[idx++] = s;
                }

                return retVal;
            }
        }
        #endregion
        #region Vehicles
        class Vehicle
        {

            string regNo;
            string model;
            int year;
            double price;

            public Vehicle(string regNo, string model, int year, double price)
            {
                this.regNo = regNo;
                this.model = model;
                this.year = year;
                this.price = price;
            }

            public string RegNo { get => regNo; set => regNo = value; }
            public string Model { get=>model; set => model = value; }
            public int Year { get => year ;set => year = value; }
            public double Price { get => price; set => price = value; }

            virtual public double GetPricePerDay()
            {
                return price * 24;
            }

            public double GetPricePerMonth()
            {
                return GetPricePerDay() * 30;
            }

            public override string ToString()
            {
                return "Vehicle [regNo=" + regNo + ", model=" + model + ", year=" + year + ", price=" + price + "]";
            }

            public static Vehicle GetNewestVehicle(params Vehicle[] vehicles)
            {
                if (vehicles.Length == 0)
                {
                    return null;
                }

                Vehicle youngestVehicle = vehicles[0];
                foreach (Vehicle v in vehicles)
                {
                    if (v.Year > youngestVehicle.Year)
                    {
                        youngestVehicle = v;
                    }
                }

                return youngestVehicle;
            }

            public static void PrintAllVehiclesWithCargoSpaceGreaterThan(double cargoSize, params Vehicle[] vehicles)
            {
                Console.WriteLine("Vehicles with cargo space greater than " + cargoSize + " liters:");
                foreach (Vehicle v in vehicles)
                {
                    if (v is Car)
                    {
                        if (((Car)v).CargoSpace > cargoSize)
                        {
                            Console.WriteLine(" - " + v.Model + ": " + ((Car)v).CargoSpace);
                        }
                    }
                    else if (v is CargoVan)
                    {
                        if (((CargoVan)v).MaxLoad > cargoSize)
                        {
                            Console.WriteLine(" - " + v.Model + ": " + ((CargoVan)v).MaxLoad);
                        }
                    }
                }
            }

        }
        class Car : Vehicle
        {
            string carType;
            double cargoSpace;

            public Car(string regNo, string model, int year, double price, string carType, double cargoSpace) : base(regNo, model, year, price)
            {
                this.carType = carType;
                this.cargoSpace = cargoSpace;
            }

            public string CarType { get => carType; set => carType = value; }
            public double CargoSpace { get=>cargoSpace; set => cargoSpace = value; }

            public override double GetPricePerDay()
            {
                return base.GetPricePerDay();
            }

        }
        class Van : Vehicle
        {
            double height;
            short noOfSeats;

            public Van(string regNo, string model, int year, double price, double height, short noOfSeats) : base(regNo, model, year, price)
            {
                this.height = height;
                this.noOfSeats = noOfSeats;
            }

            public double Height { get=> height; set => height = value; }
            public short NoOfSeats { get => noOfSeats; set => noOfSeats = value; }

            public override double GetPricePerDay()
            {
                return base.GetPricePerDay();
            }

        }
        class Limo : Vehicle
        {
            double length;
            bool miniBar;
            bool sunRoof;

            public Limo(string regNo, string model, int year, double price, double length, bool miniBar, bool sunRoof) : base(regNo, model, year, price)
            {
                
                this.length = length;
                this.miniBar = miniBar;
                this.sunRoof = sunRoof;
            }

            public double Length { get => length; set => length = value; }
            public bool MiniBar { get => miniBar; set => miniBar = value; }
            public bool SunRoof { get => sunRoof; set => sunRoof = value; }

            public override double GetPricePerDay()
            {
                return (1.50 * base.GetPricePerDay()) * 24;
            }
        }
        class PassengerVan : Van
        {


            int noOfPassengers;

            public PassengerVan(string regNo, string model, int year, double price, double height, short noOfSeats, int noOfPassengers) : base(regNo, model, year, price, height, noOfSeats)
            {
                this.noOfPassengers = noOfPassengers;
            }

            public int NoOfPassengers { get => noOfPassengers; set => noOfPassengers = value; }

            public override double GetPricePerDay()
            {
                return (0.80 * base.GetPricePerDay()) * 24;
            }
        }
        class CargoVan : Van
        {
            double maxLoad;

            public CargoVan(string regNo, string model, int year, double price, double height, short noOfSeats, double maxLoad) : base(regNo, model, year, price, height, noOfSeats)
            {
                this.maxLoad = maxLoad;
            }

            public double MaxLoad { get => maxLoad; set => maxLoad = value; }

            public override double GetPricePerDay()
            {
                return (1.10 * base.GetPricePerDay()) * 24;
            }

        }
        #endregion

        static void Main(string[] args)
        {
            Dessert genericDessert = new Dessert("Chocolate Mousse", 120, 300);
            Cake cake = new Cake("Raspberry chocolate cake #3", 350.5, 400, false, "birthday");
            Teacher t1 = new Teacher("Dario", "Tušek", 42, "dario.tusek@fer.hr", "OOP", 10000);
            Teacher t2 = new Teacher("Doris", "Bezmalinović", 43, "doris.bezmalinovic@fer.hr", "OOP", 10000);
            Student s1 = new Student("Janko", "Horvat", 18, "0036312123", (short)1);
            Student s2 = new Student("Ana", "Kovač", 19, "0036387656", (short)2);
            Student s3 = new Student("Ivana", "Stanić", 19, "0036392357", (short)1);
            UniMasterChef competition = new UniMasterChef(2);
            CompetitionEntry e1 = new CompetitionEntry(t1, genericDessert);
            competition.addEntry(e1);
            Console.WriteLine("Entry 1 rating: " + e1.getRating());
            e1.addEntry(s1, 4);
            e1.addEntry(s2, 5);
            Console.WriteLine("Entry 1 rating: " + e1.getRating());
            CompetitionEntry e2 = new CompetitionEntry(t2, cake);
            e2.addEntry(s1, 4);
            e2.addEntry(s3, 5);
            e2.addEntry(s2, 5);
            competition.addEntry(e2);
            Console.WriteLine("Entry 2 rating: " + e2.getRating());
            Console.WriteLine("Best dessert is: " + competition.getBestDessert().Name);
            Console.WriteLine("\n\n");
            Vehicle v = new Car("DA1234AA", "Renault Clio", 2001, 20, "coupe", 200);
            Car car = new Car("DA8818BB", "Renault Megane Grandtour", 2007, 25, "caravan", 800);
            Van van1 = new CargoVan("DA0009PO", "Volkswagen Transporter", 2018, 28, 2, (short)3, 4500);
            PassengerVan van2 = new PassengerVan("DA6282EA", "IMV 1600", 1978, 35, 2, (short)3, 0);
            Vehicle limo = new Limo("DA2238AB", "Zastava 750 LE", 1983, 220, 3.2, false, false);
            Console.WriteLine(v.Model + " price per day: " + v.GetPricePerDay());
            Console.WriteLine(van1.Model + " price per day: " + van1.GetPricePerDay());
            Console.WriteLine(van2.Model + " price per month: " + van2.GetPricePerMonth());
            Vehicle newest = Vehicle.GetNewestVehicle(v, car, van1, van2, limo);
            Console.WriteLine("Newest: " + newest.Model + ", " + newest.Year);
            Vehicle.PrintAllVehiclesWithCargoSpaceGreaterThan(500, v, car, van1, van2, limo);

            Console.ReadKey();
        }
    }
}
