using System.Xml.Linq;

namespace ConsoleApp27
{
    abstract class Person
    {
        protected Person(string? name, string? surname)
        {
            Name = name;
            Surname = surname;
        }

        private string? Name1 { get; set; }
        private string? Surname1 { get; set; }


        public string? Name
        {
            get => Name1;
            set
            {
                if (string.IsNullOrEmpty(value?.Trim()))
                {
                    throw new ArgumentNullException("Invalid input name..!");
                }
                Name1 = value;
            }
        }
        public string? Surname
        {
            get => Surname1;
            set
            {
                if (string.IsNullOrEmpty(value?.Trim()))
                {
                    throw new ArgumentNullException("Invalid input surname..!");
                }
                Surname1 = value;
            }
        }

    }

    class Doctor : Person
    {

        public Doctor(string? name, string? surname, int age, int workExperience) : base(name, surname)
        {
            SID++;
            ID = SID;
            Age = age;
            WorkExperience = workExperience;
            WorkHour_09_11 = false;
            WorkHour_12_14 = false;
            WorkHour_15_17 = false;
        }

        private static int SID = 0;
        private int Age1 { get; set; }
        private int WorkExperience1 { get; set; }

        public int ID { get; private set; }
        //+ 09:00-11:00
        public bool WorkHour_09_11 { get; set; }

        //+ 12:00-14:00
        public bool WorkHour_12_14 { get; set; }

        //+ 15:00-17:00
        public bool WorkHour_15_17 { get; set; }


        public int Age
        {
            get => Age1;
            set
            {
                if (value < 18 || value > 65)
                {
                    throw new ArgumentNullException("Invalid input age..!");
                }
                Age1 = value;
            }
        }
        public int WorkExperience
        {
            get => WorkExperience1;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentNullException("Invalid input Work Experience..!");
                }
                WorkExperience1 = value;
            }
        }
        public override string ToString()
        {
            string First = (WorkHour_09_11) ? "Rezerv olunub" : "Rezerv olunmayib";
            string Second = (WorkHour_12_14) ? "Rezerv olunub" : "Rezerv olunmayib";
            string Thirdly = (WorkHour_15_17) ? "Rezerv olunub" : "Rezerv olunmayib";

            return $"-------------- Doctor ----------------\nID: {ID}, Name: {Name}, Surname: {Surname}, Age: {Age}\nHekimin is saatlari\n09:00-11:00 - {First}\n12:00-14:00 - {Second}\n15:00-17:00 - {Thirdly}";

        }

        public void RezervTime(string? rezerv_hour, User user)
        {
            if (rezerv_hour == "1")
            {
                Console.WriteLine($"Doctor {Name} {Surname} saat 09-11 arasi {user.Name} {user.Surname} qebul edecek..!");
            }
            else if (rezerv_hour == "2")
            {
                Console.WriteLine($"Doctor {Name} {Surname} saat 12-14 arasi {user.Name} {user.Surname} qebul edecek..!");
            }
            else
            {
                Console.WriteLine($"Doctor {Name} {Surname} saat 15-17 arasi {user.Name} {user.Surname} qebul edecek..!");
            }
        }
    }

    static class Notification
    {
        static event Action? actionSystem;
        public static void Rezerv(Doctor doctor, User user, string? choice_hour)
        {
            if (choice_hour == "1")
            {

                if (!doctor.WorkHour_09_11)
                {
                    if (user.AcceptHour_09_11) { Console.WriteLine("Siz basqa hecime artiq rezerv olunmusunuz.!"); return; }
                    doctor.WorkHour_09_11 = true;
                    user.AcceptHour_09_11 = true;
                    user.Doctors.Add(doctor);
                    actionSystem += () => Console.WriteLine($"--------------------------------------------------\nDoctor {doctor.Name} {doctor.Surname} saat 09-11 arasi {user.Name} {user.Surname} qebul edecek..!\n--------------------------------------------------");
                    Console.WriteLine($"Doctor {doctor.Name} {doctor.Surname} -a saat 09-11 arasi olan rezerv qebul olundu.!");
                    return;

                }
                else
                {
                    foreach (var item in user.Doctors)
                    {
                        if (item.ID == doctor.ID)
                        {
                            doctor.WorkHour_09_11 = false;
                            user.AcceptHour_09_11 = false;
                            actionSystem -= () => Console.WriteLine($"Doctor {doctor.Name} {doctor.Surname} saat 09-11 arasi {user.Name} {user.Surname} qebul edecek..!");
                            Console.WriteLine($"Doctor {doctor.Name} {doctor.Surname} -a saat 09-11 arasi olan rezervasiya legv olundu.!");
                            return;
                        }
                    }
                    Console.WriteLine($"Doctor {doctor.Name} {doctor.Surname} saat 09-11 arasi artiq rezerv olunub.!");
                }
            }
            else if (choice_hour == "2")
            {
                if (!doctor.WorkHour_12_14)
                {
                    if (user.AcceptHour_12_14) { Console.WriteLine("Siz basqa hecime artiq rezerv olunmusunuz.!"); return; }
                    doctor.WorkHour_12_14 = true;
                    user.AcceptHour_12_14 = true;
                    user.Doctors.Add(doctor);
                    actionSystem += () => Console.WriteLine($"--------------------------------------------------\nDoctor {doctor.Name} {doctor.Surname} saat 12-14 arasi {user.Name} {user.Surname} qebul edecek..!\n--------------------------------------------------");
                    Console.WriteLine($"Doctor {doctor.Name} {doctor.Surname} -a saat 12-14 arasi olan rezerv qebul olundu.!");
                    return;

                }
                else
                {
                    foreach (var item in user.Doctors)
                    {
                        if (item.ID == doctor.ID)
                        {
                            doctor.WorkHour_12_14 = false;
                            user.AcceptHour_12_14 = false;
                            actionSystem -= () => Console.WriteLine($"Doctor {doctor.Name} {doctor.Surname} saat 12-14 arasi {user.Name} {user.Surname} qebul edecek..!");
                            Console.WriteLine($"Doctor {doctor.Name} {doctor.Surname} -a saat 12-14 arasi olan rezervasiya legv olundu.!");
                            return;
                        }
                    }
                    Console.WriteLine($"Doctor {doctor.Name} {doctor.Surname} saat 12-14 arasi artiq rezerv olunub.!");
                }
            }
            else if (choice_hour == "3")
            {
                if (!doctor.WorkHour_15_17)
                {
                    if (user.AcceptHour_15_17) { Console.WriteLine("Siz basqa hecime artiq rezerv olunmusunuz.!"); return; }
                    doctor.WorkHour_15_17 = true;
                    user.AcceptHour_15_17 = true;
                    user.Doctors.Add(doctor);
                    actionSystem += () => Console.WriteLine($"--------------------------------------------------\nDoctor {doctor.Name} {doctor.Surname} saat 15-17 arasi {user.Name} {user.Surname} qebul edecek..!\n--------------------------------------------------");
                    Console.WriteLine($"Doctor {doctor.Name} {doctor.Surname} -a saat 15-17 arasi olan rezerv qebul olundu.!");
                    return;

                }
                else
                {
                    foreach (var item in user.Doctors)
                    {
                        if (item.ID == doctor.ID)
                        {
                            doctor.WorkHour_15_17 = false;
                            user.AcceptHour_15_17 = false;
                            actionSystem -= () => Console.WriteLine($"Doctor {doctor.Name} {doctor.Surname} saat 15-17 arasi {user.Name} {user.Surname} qebul edecek..!");
                            Console.WriteLine($"Doctor {doctor.Name} {doctor.Surname} -a saat 15-17 arasi olan rezervasiya legv olundu.!");
                            return;
                        }
                    }
                    Console.WriteLine($"Doctor {doctor.Name} {doctor.Surname} saat 15-17 arasi artiq rezerv olunub.!");
                }
            }
            else { Console.WriteLine("Yalnis secim.!"); }
        }
        public static void ShowAllRezervs()
        {
            Console.WriteLine("Butun rezerv olunan hekimler");
            actionSystem?.Invoke();
        }
    }

    class Hospital
    {
        public Hospital()
        {
            Pediatrics = new List<Doctor>();
            Traumatology = new List<Doctor>();
            Dentistry = new List<Doctor>();
            users = new List<User>();
        }

        public List<Doctor>? Pediatrics { get; set; }
        public List<Doctor>? Traumatology { get; set; }
        public List<Doctor>? Dentistry { get; set; }
        public List<User>? users { get; set; }


        public void AddUser()
        {
            Console.WriteLine("Xestexanadan lutfen qeydiyyatdan kecin.!");
            Console.WriteLine("Adinizi daxil edin.");
            string? name = Console.ReadLine();
            Console.WriteLine("Soyadinizi daxil edin.");
            string? surname = Console.ReadLine();
            Console.WriteLine("Yasinizi daxil edin.");
            int age = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Email unvaninizi daxil edin.");
            string? email = Console.ReadLine();
            Console.WriteLine("Telefon nomrenizi daxil edin.(meselen: 055000000 ve ya +99455000000)");
            string? phone = Console.ReadLine();
            users?.Add(new User(name, surname, age, email, phone));
            Console.WriteLine("Qeydiyyat ugurla basa catdi.");
        }

        public User LoginIsTrue()
        {
            if (users == null || users.Count == 0) { Console.WriteLine("User elave edilmeyib.!"); throw new Exception("User not registration..!"); }
            Console.WriteLine("Giris ucun ilk olaraq Emailinizi ve sonra Telefon nomrenizi daxil etmelisiniz.!");
            Console.WriteLine("Email unvaninizi daxil edin.");
            string? email = Console.ReadLine();
            Console.WriteLine("Telefon nomrenizi daxil edin.");
            string? phone = Console.ReadLine();
            foreach (var item in users)
            {
                if (phone == item.Phone && email == item.Email)
                {
                    return item;
                }
            }
            throw new Exception("Invalid login phone or email..!");
        }
        public void DoctorRandovu()
        {
            User user = LoginIsTrue();
            while (true)
            {

                Console.WriteLine("Hansi sobeye muraciet etmek isteyirsiniz.?");
                Console.WriteLine("1.Pediatriya sobesi\n2.Travmatologiya sobesi\n3.Stomatologiya sobesi\n0.Secimden cixis edin");
                string? choice = Console.ReadLine();
                Console.Clear();
                if (choice == "1")
                {
                    if (Pediatrics == null || Pediatrics.Count == 0) { Console.WriteLine("Pediatriyada hecim yoxdur.!"); continue; }
                    foreach (var item in Pediatrics)
                    {
                        Console.WriteLine("-------------------------------------------------");
                        Console.WriteLine(item);
                        Console.WriteLine("-------------------------------------------------");
                    }
                    Console.WriteLine("Hansi hekimi secmek isteyirsinizse onun ID-ni daxil edin");
                    int id = Convert.ToInt32(Console.ReadLine());
                    foreach (var item in Pediatrics)
                    {
                        if (item.ID == id)
                        {
                            Console.WriteLine("Hekimin hansi is saatini rezerv etmek isteyirsiniz.?");
                            Console.WriteLine("1.Saat 09-11 -e\n2.Saat 12-14 -e\n3.Saat 15-17 -e");
                            string? choice2 = Console.ReadLine();
                            Console.Clear();
                            Notification.Rezerv(item, user, choice2);
                            choice = "";

                        }

                    }
                    if (choice != "") { Console.WriteLine("Pediatriyada bu ID-de hakim yoxdur.!"); }
                }
                else if (choice == "2")
                {
                    if (Traumatology == null || Traumatology.Count == 0) { Console.WriteLine("Travmatologiyada hecim yoxdur.!"); continue; }
                    foreach (var item in Traumatology)
                    {
                        Console.WriteLine("-------------------------------------------------");
                        Console.WriteLine(item);
                        Console.WriteLine("-------------------------------------------------");
                    }
                    Console.WriteLine("Hansi hekimi secmek isteyirsinizse onun ID-ni daxil edin");
                    int id = Convert.ToInt32(Console.ReadLine());
                    foreach (var item in Traumatology)
                    {
                        if (item.ID == id)
                        {
                            Console.WriteLine("Hekimin hansi is saatini rezerv etmek isteyirsiniz.?");
                            Console.WriteLine("1.Saat 09-11 -e\n2.Saat 12-14 -e\n3.Saat 15-17 -e");
                            string? choice2 = Console.ReadLine();
                            Console.Clear();
                            Notification.Rezerv(item, user, choice2);
                            choice = "";

                        }

                    }
                    if (choice != "") { Console.WriteLine("Travmatologiyada bu ID-de hakim yoxdur.!"); }
                }
                else if (choice == "3")
                {
                    if (Dentistry == null || Dentistry.Count == 0) { Console.WriteLine("Stomatoloiyada hecim yoxdur.!"); continue; }
                    foreach (var item in Dentistry)
                    {
                        Console.WriteLine("-------------------------------------------------");
                        Console.WriteLine(item);
                        Console.WriteLine("-------------------------------------------------");
                    }
                    Console.WriteLine("Hansi hekimi secmek isteyirsinizse onun ID-ni daxil edin");
                    int id = Convert.ToInt32(Console.ReadLine());
                    foreach (var item in Dentistry)
                    {
                        if (item.ID == id)
                        {
                            Console.WriteLine("Hekimin hansi is saatini rezerv etmek isteyirsiniz.?");
                            Console.WriteLine("1.Saat 09-11 -e\n2.Saat 12-14 -e\n3.Saat 15-17 -e");
                            string? choice2 = Console.ReadLine();
                            Console.Clear();
                            Notification.Rezerv(item, user, choice2);
                            choice = "";

                        }

                    }
                    if (choice != "") { Console.WriteLine("Stomatologiyada bu ID-de hakim yoxdur.!"); }
                }
                else if (choice == "0")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Yalnis secim.!");
                }
            }



        }

        public void AllRandovu()
        {
            Notification.ShowAllRezervs();
        }

    }

    class User : Person
    {

        public User(string? name, string? surname, int age, string? email, string? phone) : base(name, surname)
        {
            Age = age;
            Email = email;
            Phone = phone;
            AcceptHour_09_11 = false;
            AcceptHour_12_14 = false;
            AcceptHour_15_17 = false;
            Doctors = new List<Doctor>();

        }

        public List<Doctor> Doctors { get; set; }

        //+ 09:00-11:00
        public bool AcceptHour_09_11 { get; set; }

        //+ 12:00-14:00
        public bool AcceptHour_12_14 { get; set; }

        //+ 15:00-17:00
        public bool AcceptHour_15_17 { get; set; }

        private int Age1 { get; set; }
        private string? Email1 { get; set; }
        private string? Phone1 { get; set; }



        public int Age
        {
            get => Age1;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Invalid input age..!");
                }
                Age1 = value;
            }
        }
        public string? Email
        {
            get => Email1;
            set
            {
                if (string.IsNullOrEmpty(value?.Trim()) || !value.EndsWith(".com") && !value.Contains("mail.") || value.Split("@").Length != 2)
                {
                    throw new ArgumentException("Invalid input email..!");
                }
                Email1 = value;
            }
        }

        public string? Phone
        {
            get => Phone1;

            set
            {
                if (string.IsNullOrEmpty(value?.Trim()) || value.Length != 10 && value.Length != 13 || !value.StartsWith("0") && !value.StartsWith("+994"))
                {
                    throw new ArgumentException("Invalid input phone number..!");
                }
                char[] charac = value.ToCharArray();

                int size = (charac.Length - 1);
                int num = 0;

                for (int i = 0; i < 9; i++)
                {
                    if (!char.IsDigit(charac[size]))
                    {
                        throw new ArgumentException("Invalid input phone number..!");
                    }

                    if (char.IsDigit(charac[size--])) { num++; }

                }

                if (num != 9)
                {
                    throw new ArgumentException("Invalid input phone number..!");
                }

                Phone1 = value.Trim(); ;
            }
        }

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Hospital hospital = new Hospital();

            hospital?.Traumatology?.Add(new Doctor("Asif", "Meherremov", 55, 9));
            hospital?.Traumatology?.Add(new Doctor("Aqil", "Hemidov", 55, 9));
            hospital?.Traumatology?.Add(new Doctor("Qalib", "Aliyev", 55, 9));
            hospital?.Traumatology?.Add(new Doctor("Eli", "Musayev", 55, 9));

            hospital?.Pediatrics?.Add(new Doctor("Kazim", "Abitalibov", 55, 9));
            hospital?.Pediatrics?.Add(new Doctor("Kemale", "Merdanova", 55, 9));
            hospital?.Pediatrics?.Add(new Doctor("Samxal", "Babayev", 55, 9));
            hospital?.Pediatrics?.Add(new Doctor("Esmira", "Adigozelova", 55, 9));

            hospital?.Dentistry?.Add(new Doctor("Sireli", "Aydinov", 55, 9));
            hospital?.Dentistry?.Add(new Doctor("Sohrab", "Cavadov", 55, 9));
            hospital?.Dentistry?.Add(new Doctor("Inci", "cemilova", 55, 9));
            hospital?.Dentistry?.Add(new Doctor("Qenire", "Yolcuyeva", 55, 9));

            Console.WriteLine("Ozel xestaxana sistemi");
            bool IsTrue = true;
            while (IsTrue)
            {
                Console.WriteLine("1.Yeni istifadeki kimi qeydiyyatdan cekmek\n2.Movcu istifadeki kimi daxil olmaq\n3.Butun rezerv olunan hekimlere baxmaq\n0.Cixis etmek");
                string? Choice = Console.ReadLine();
                Console.Clear();
                switch (Choice)
                {
                    case "1":
                        try
                        {
                            hospital?.AddUser();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            
                        }
                        break;
                    case "2":
                        try
                        {
                            hospital?.DoctorRandovu();
                        }
                        catch (Exception ex)
                        {

                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case "3":
                        try
                        {
                            hospital?.AllRandovu();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case "0":
                        IsTrue = false;
                        break;
                    default:
                        Console.WriteLine("Yalnis secim.!");
                        break;
                }

            }




        }
    }
}
