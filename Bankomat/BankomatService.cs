using System.Text.Json;

public class BankomatService
{
    private List<Account> _bankAccounts;
    private const string _jsonFilePath = "C:\\AGU\\bankomat.json";

    public void Init()
    {
        Console.WriteLine("ATM Initializing..... ");
        _bankAccounts = new List<Account>();

        string input = File.ReadAllText(_jsonFilePath);

        _bankAccounts = JsonSerializer.Deserialize<List<Account>>(input);

        Console.WriteLine("ATM ready to use.");
    }
    public void PrintMainMenu()
    {
Start:
        Console.WriteLine("Главное меню");
        string a = InputNumber();
        int p = InputPin();

        Account accounts = _bankAccounts.FirstOrDefault();


        if (a == accounts.CardNumber && p == accounts.Pin)
        {
            Console.Clear();

            Console.Write($"Привет, {accounts.Name}.");
UserMenu:
            int b = UserMenu();

            switch (b)
            {
                case 1:
                    Console.Write("Внесите купюру:");
                    int d = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Успешно выполнено");
                    accounts.Balance += d;
                    Console.ReadKey();
                    Console.Clear() ;
                    goto UserMenu;
                case 2:
                    Console.Write("Введите сумму:");
                    int s = Convert.ToInt32(Console.ReadLine());
                    if (s <= accounts.Balance)
                    {
                        Console.WriteLine("Возьмите деньги");
                        accounts.Balance -= s;
                    }
                    else
                    {
                        Console.WriteLine("Ошибка. Ваш баланс недостаточно");
                    }
                    Console.ReadKey();
                    Console.Clear() ;
                    goto UserMenu;
                case 3:
                    Console.WriteLine($"Ваш баланс: {accounts.Balance}тг");
                    Console.ReadKey();
                    Console.Clear();
                    goto UserMenu;
                case 4:
                    Console.WriteLine("Спасибо за пользование!");
                    UpdateFile();
                    Console.ReadKey();
                    Console.Clear();
                    goto Start;
                default:
                    Console.Clear();
                    goto UserMenu;
            }

        }
        else 
        {
            Console.WriteLine("Данные некорректно");
            Console.ReadKey();
            Console.Clear();
            goto Start;
        }
    }

    private void UpdateFile()
    {
        string output = JsonSerializer.Serialize<List<Account>>(_bankAccounts);
        File.WriteAllText(_jsonFilePath, output);
    }

    public int UserMenu()
    {
        Console.WriteLine("Выберите команду:");
        Console.WriteLine("1)Внести");
        Console.WriteLine("2)Снять");
        Console.WriteLine("3)Проверить баланс");
        Console.WriteLine("4)Выход");

        int b = Convert.ToInt32(Console.ReadLine());
        return b;
    }

    public string InputNumber()
    {
        Console.Write("Введите номер счета:");
        string a = Console.ReadLine();
        return a;
    }

    public int InputPin()
    {
        Console.Write("Введите ПИН-кода:");
        int p = Convert.ToInt32(Console.ReadLine());
        return p;
    }
}