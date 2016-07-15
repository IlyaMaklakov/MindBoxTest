
/*============================================================
*  Реализовал два способа сортировки. 
* 1-ый способ: Для начала создал очередь из карточек. Путем перебора каждой карточки, наращиваю двусвязный список 
*              упорядоченных карточек. По мере нахождения соответсвующих началу или концу упорядоченного списка карточек,
*              очередь уменьшается. Цикл повторяется пока не очередь не закончится.
*              
*  Сложность данного алгоритма : 
*                       - в лучшем случае: O(n) 
*                       - в худшем случае: O(n^2) 
*
* 2-ый способ: В данном способе использовал словари. Создал 2 словаря: начальных пунктов и пунктов назначения. 
*              Упорядочивание ведется простым поиском  карточек для начала или конца списка из соответсвующих словарей.
*              
*  Сложность данного алгоритма : 
*                       - в лучшем случае: O(n) 
*                       - в худшем случае: O(n) 
*
===========================================================*/

namespace MindboxTestProject
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    public static class TicketsManager
    {
        /// <summary>
        ///     Отображение карточек в консоли
        /// </summary>
        /// <param name="tickets">
        ///     Список карточек для отображения
        /// </param>
        public static void DisplayTickets(IEnumerable<Ticket> tickets)
        {
            foreach (var ticket in tickets)
            {
                Console.WriteLine($"{ticket.StartingPoint} -> {ticket.Destination}");
            }
        }

        /// <summary>
        ///     Сортировка карточек
        /// </summary>
        /// <param name="randomTickets">
        ///     Неупорядоченные карточки
        /// </param>
        /// <returns>
        ///     Упорядоченные карточки
        /// </returns>
        public static LinkedList<Ticket> OrderTickets(List<Ticket> randomTickets)
        {
            // Создаем очередь из карточек
            var ticketsQueue = new Queue<Ticket>(randomTickets);

            // Создаем двусвязный список
            var orderedTickets = new LinkedList<Ticket>();

            // Для начала, добавляем первую карточку
            orderedTickets.AddFirst(ticketsQueue.Dequeue());

            // Смотрим каждую карточку, сравнивая с начальной и конечной карточкой в двусвязном списке. 
            // Если карточка не подходит, возвращаем его обратно в очередь.
            // Из цикла выходим, когда в очереди не останется карточек.
            while (ticketsQueue.Count != 0)
            {
                var current = ticketsQueue.Dequeue();
                var firstInChain = orderedTickets.First.Value;
                var lastInChain = orderedTickets.Last.Value;

                if (current.StartingPoint.Equals(lastInChain.Destination, StringComparison.InvariantCultureIgnoreCase))
                {
                    orderedTickets.AddLast(current);
                }
                else if (current.Destination.Equals(
                    firstInChain.StartingPoint,
                    StringComparison.InvariantCultureIgnoreCase))
                {
                    orderedTickets.AddFirst(current);
                }
                else
                {
                    ticketsQueue.Enqueue(current);
                }
            }

            return orderedTickets;
        }

        /// <summary>
        /// Сортировка карточек с помощью словарей
        /// </summary>
        /// <param name="randomTickets">
        ///     Неупорядоченные карточки
        /// </param>
        /// <returns>
        ///     Упорядоченные карточки
        /// </returns>
        public static LinkedList<Ticket> OrderTicketsWithDictionary(List<Ticket> randomTickets)
        {
            // Создаем словарь из карточек
            Dictionary<string, Ticket> startingPointsDictionary = randomTickets.ToDictionary(t => t.StartingPoint, t => t);
            Dictionary<string, Ticket> destinationDictionary = randomTickets.ToDictionary(t => t.Destination, t => t);

            // Создаем двусвязный список
            var orderedTickets = new LinkedList<Ticket>();

            orderedTickets.AddFirst(startingPointsDictionary.First().Value);

            for (var i = 1; i < randomTickets.Count; i++)
            {
                var lastTicket = orderedTickets.Last.Value;
                var firstTicket = orderedTickets.First.Value;

                // Находим соответсвующую карточку из словаря для конца списка
                if (startingPointsDictionary.ContainsKey(lastTicket.Destination))
                {
                    var ticketForEnd = startingPointsDictionary[lastTicket.Destination];
                    if (ticketForEnd != null)
                    {
                        orderedTickets.AddLast(ticketForEnd);
                    }
                }

                // Находим соответсвующую карточку из словаря для начала списка
                if (destinationDictionary.ContainsKey(firstTicket.StartingPoint))
                {
                    var ticketForStart = destinationDictionary[firstTicket.StartingPoint];
                    if (ticketForStart != null)
                    {
                        orderedTickets.AddFirst(ticketForStart);
                    }
                }
            }

            return orderedTickets;
        }

        /// <summary>
        ///     Метод проверки упорядоченности списка карточек
        /// </summary>
        /// <param name="tickets">
        ///     Список карточек для проверки
        /// </param>
        /// <returns>
        ///     True - если карточки упорядочены, иначе false
        /// </returns>
        public static bool TicketsIsOrdered(LinkedList<Ticket> tickets)
        {
            var ordered = false;
            try
            {
                var ticketsList = tickets.ToList();
                for (var i = 1; i < tickets.Count; i++)
                {
                    if (
                        !ticketsList[i - 1].Destination.Equals(
                            ticketsList[i].StartingPoint,
                            StringComparison.InvariantCultureIgnoreCase))
                    {
                        throw new Exception("Tickets is not ordered");
                    }
                }

                ordered = true;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("Error in tickets list order checking." + exception.Message);
            }
            return ordered;
        }
    }
}