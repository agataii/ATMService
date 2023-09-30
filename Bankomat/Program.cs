internal class Program
{
    private static void Main(string[] args)
    {
        BankomatService service = new BankomatService();
        service.Init();
        service.PrintMainMenu();
    }
}