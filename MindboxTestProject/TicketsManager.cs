
/*============================================================
*  ���������� ��� ������� ����������. 
* 1-�� ������: ��� ������ ������ ������� �� ��������. ����� �������� ������ ��������, ��������� ���������� ������ 
*              ������������� ��������. �� ���� ���������� �������������� ������ ��� ����� �������������� ������ ��������,
*              ������� �����������. ���� ����������� ���� �� ������� �� ����������.
*              
*  ��������� ������� ��������� : 
*                       - � ������ ������: O(n) 
*                       - � ������ ������: O(n^2) 
*
* 2-�� ������: � ������ ������� ����������� �������. ������ 2 �������: ��������� ������� � ������� ����������. 
*              �������������� ������� ������� �������  �������� ��� ������ ��� ����� ������ �� �������������� ��������.
*              
*  ��������� ������� ��������� : 
*                       - � ������ ������: O(n) 
*                       - � ������ ������: O(n) 
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
        ///     ����������� �������� � �������
        /// </summary>
        /// <param name="tickets">
        ///     ������ �������� ��� �����������
        /// </param>
        public static void DisplayTickets(IEnumerable<Ticket> tickets)
        {
            foreach (var ticket in tickets)
            {
                Console.WriteLine($"{ticket.StartingPoint} -> {ticket.Destination}");
            }
        }

        /// <summary>
        ///     ���������� ��������
        /// </summary>
        /// <param name="randomTickets">
        ///     ��������������� ��������
        /// </param>
        /// <returns>
        ///     ������������� ��������
        /// </returns>
        public static LinkedList<Ticket> OrderTickets(List<Ticket> randomTickets)
        {
            // ������� ������� �� ��������
            var ticketsQueue = new Queue<Ticket>(randomTickets);

            // ������� ���������� ������
            var orderedTickets = new LinkedList<Ticket>();

            // ��� ������, ��������� ������ ��������
            orderedTickets.AddFirst(ticketsQueue.Dequeue());

            // ������� ������ ��������, ��������� � ��������� � �������� ��������� � ���������� ������. 
            // ���� �������� �� ��������, ���������� ��� ������� � �������.
            // �� ����� �������, ����� � ������� �� ��������� ��������.
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
        /// ���������� �������� � ������� ��������
        /// </summary>
        /// <param name="randomTickets">
        ///     ��������������� ��������
        /// </param>
        /// <returns>
        ///     ������������� ��������
        /// </returns>
        public static LinkedList<Ticket> OrderTicketsWithDictionary(List<Ticket> randomTickets)
        {
            // ������� ������� �� ��������
            Dictionary<string, Ticket> startingPointsDictionary = randomTickets.ToDictionary(t => t.StartingPoint, t => t);
            Dictionary<string, Ticket> destinationDictionary = randomTickets.ToDictionary(t => t.Destination, t => t);

            // ������� ���������� ������
            var orderedTickets = new LinkedList<Ticket>();

            orderedTickets.AddFirst(startingPointsDictionary.First().Value);

            for (var i = 1; i < randomTickets.Count; i++)
            {
                var lastTicket = orderedTickets.Last.Value;
                var firstTicket = orderedTickets.First.Value;

                // ������� �������������� �������� �� ������� ��� ����� ������
                if (startingPointsDictionary.ContainsKey(lastTicket.Destination))
                {
                    var ticketForEnd = startingPointsDictionary[lastTicket.Destination];
                    if (ticketForEnd != null)
                    {
                        orderedTickets.AddLast(ticketForEnd);
                    }
                }

                // ������� �������������� �������� �� ������� ��� ������ ������
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
        ///     ����� �������� ��������������� ������ ��������
        /// </summary>
        /// <param name="tickets">
        ///     ������ �������� ��� ��������
        /// </param>
        /// <returns>
        ///     True - ���� �������� �����������, ����� false
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