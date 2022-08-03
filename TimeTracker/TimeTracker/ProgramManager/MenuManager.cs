using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TimeTracker
{
    public class MenuManager
    {
        public void Menu()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.WriteLine("Zehmet olmasa sechim edin:");
            Thread.Sleep(500);
            Console.WriteLine("1) Bir işçinin məlumatlarının göstərilməsi.");
            Console.WriteLine("2) Bir işçinin məlumatlarının göstərilməsi və həmin ay üzrə iş məlumatların göstərilməsi.");
            Console.WriteLine("3) Müəyyən bir ünvana görə işçilərin siyahısının görüntülənməsi.");
            Console.WriteLine("4) İşə qəbul olunan işçilərin sayının illər üzrə bölgüsünün görüntülənməsi.");
            Console.WriteLine("5) İşə gec gələn işçilərin siyahısı");
            Console.WriteLine("6) Müəyyən bir günün əlavə iş qeydlərinin sadalanması");
            int choise = int.Parse(Console.ReadLine());
            switch (choise)
            {
                case 1:
                    string iscontinue = "hə";
                    do
                    {
                        Console.Write("İşçi nömrəsini qeyd edin: ");
                        int number = Convert.ToInt32(Console.ReadLine());
                        EmployeeManager.EmployeeAdder();
                        EmployeeManager.ReadEmployee(number, null, null, null, null);
                        Console.WriteLine("Davam etmək istəyirsiniz? ");
                        iscontinue = Console.ReadLine().ToLower();
                    } while (iscontinue == "hə");
                    Menu();
                    break;
                case 2:
                    do
                    {
                        Console.Write("İşçi vəzifəsini qeyd edin: ");
                        string position = Console.ReadLine();

                        EmployeeManager.ReadEmployee(null, null, null, null, position);
                        Console.WriteLine("Davam etmək istəyirsiniz? ");
                        iscontinue = Console.ReadLine().ToLower();
                    } while (iscontinue == "hə");
                    Menu();
                    break;
                case 3:
                    do
                    {
                        Console.Write("İl daxil edin 'yyyy': ");
                        int year = Convert.ToInt32(Console.ReadLine());
                        EmployeeManager.ReadEmployee(null, year, null, null, null);
                        Console.WriteLine("Davam etmək istəyirsiniz? ");
                        iscontinue = Console.ReadLine().ToLower();
                    } while (iscontinue == "hə");
                    Menu();
                    break;
                case 4:
                    do
                    {
                        EmployeeManager.ReadEmployee(null, null, true, null, null);
                        Console.WriteLine("Davam etmək istəyirsiniz? ");
                        iscontinue = Console.ReadLine().ToLower();
                    } while (iscontinue == "hə");
                    Menu();
                    break;
                case 5:
                    do
                    {
                        Console.Write("Gün daxil edin: ");
                        int day = Convert.ToInt32(Console.ReadLine());
                        EmployeeManager.ReadEmployee(null, null, null, day, null);
                        Console.WriteLine("Davam etmək istəyirsiniz? ");
                        iscontinue = Console.ReadLine().ToLower();
                    } while (iscontinue == "hə");
                    Menu();
                    break;
            }
        }
    }
}