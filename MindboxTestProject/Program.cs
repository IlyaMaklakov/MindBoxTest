namespace MindboxTestProject
{
    using System;

    public static class Program
    {
        public static void Main()
        {
            Console.WriteLine("Неупорядоченные карточки:");

            // Получаем список из 20 неупорядоченных карточек и отображаем в консоли
            var tickets = Utilities.GetRandomTickets(20);
            TicketsManager.DisplayTickets(tickets);

            Console.WriteLine(new string('=', 50));

            // Сортируем и отображаем отсортированный список
            Console.WriteLine("Упорядоченные карточки:");
            var orderedTickets = TicketsManager.OrderTickets(tickets);
            TicketsManager.DisplayTickets(orderedTickets);

            Console.ReadKey();
        }
    }
}