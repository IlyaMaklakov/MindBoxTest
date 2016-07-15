namespace MindboxTestProject
{
    using System;

    /// <summary>
    ///     Карточка путешествия
    /// </summary>
    public class Ticket
    {
        /// <summary>
        ///     Конструктор карточки
        /// </summary>
        /// <param name="startingPoint">
        ///     Пункт назначения
        /// </param>
        /// <param name="destination">
        ///     Пункт отправления
        /// </param>
        public Ticket(string startingPoint, string destination)
        {
            if (string.IsNullOrWhiteSpace(startingPoint))
            {
                throw new ArgumentNullException(nameof(startingPoint));
            }

            if (string.IsNullOrWhiteSpace(destination))
            {
                throw new ArgumentNullException(nameof(destination));
            }

            StartingPoint = startingPoint;
            Destination = destination;
        }

        /// <summary>
        ///     Пункт назначения
        /// </summary>
        public string Destination { get; private set; }

        /// <summary>
        ///     Пункт отправления
        /// </summary>
        public string StartingPoint { get; private set; }
    }
}