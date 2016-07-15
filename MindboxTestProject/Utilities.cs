/*============================================================
* 
*  Создал класс, в котором формируется неупорядоченный список карточек путем считвания CSV файла с городами России.
*
===========================================================*/

namespace MindboxTestProject
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Вспомогательные методы
    /// </summary>
    public static class Utilities
    {
        /// <summary>
        /// Получение указанного количества неупорядоченных карточек 
        /// </summary>
        /// <param name="count">
        /// Количество карточек
        /// </param>
        /// <returns>
        /// Список неупорядоченных карточек
        /// </returns>
        public static List<Ticket> GetRandomTickets(int count)
        {
            var tickets = new List<Ticket>();
            var pathFile = Path.Combine(
                Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName,
                "MindboxTestProject",
                "city.csv");
            if (!File.Exists(pathFile))
            {
                return null;
            }

            var lines = File.ReadAllLines(pathFile, Encoding.UTF8);
            var citiesNamesList = lines.Select(l => l.Split(';')[3]).Distinct().ToList();
            citiesNamesList.RemoveAt(0);

            if (count == 0)
            {
                count = citiesNamesList.Count - 1;
            }

            for (var i = 1; i <= count; i++)
            {
                tickets.Add(new Ticket(citiesNamesList[i - 1], citiesNamesList[i]));
            }

            Shuffle(tickets);

            return tickets;
        }

        /// <summary>
        /// Перемешивание карточек
        /// </summary>
        /// <param name="tickets">
        /// Список карточек
        /// </param>
        private static void Shuffle(IList<Ticket> tickets)
        {
            var rng = new Random();
            var n = tickets.Count;
            while (n > 1)
            {
                n--;
                var k = rng.Next(n + 1);
                var temp = tickets[k];
                tickets[k] = tickets[n];
                tickets[n] = temp;
            }
        }
    }
}