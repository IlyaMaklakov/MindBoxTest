namespace Tests
{
    using System.Collections.Generic;
    using System.Diagnostics;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using MindboxTestProject;

    using static MindboxTestProject.Utilities;

    [TestClass]
    public class TicketManagerTests
    {
        [TestMethod]
        public void OrderTicketsTest()
        {
            // ARRANGE
            const int RandomTicketsCount = 0;
            var randomTickets = GetRandomTickets(RandomTicketsCount);

            // ACT
            var watch = Stopwatch.StartNew();
            var orderedTickets = TicketsManager.OrderTickets(randomTickets);
            var elapsedTime = watch.ElapsedMilliseconds;
            var isOrdered = TicketsManager.TicketsIsOrdered(orderedTickets);

            // ASSERT
            Assert.IsTrue(isOrdered);

            Debug.WriteLine("Ellapsed ms: " + elapsedTime);
        }

        [TestMethod]
        public void OrderTicketsWithDictionaryTest()
        {
            // ARRANGE
            const int RandomTicketsCount = 0;
            var randomTickets = GetRandomTickets(RandomTicketsCount);

            // ACT
            var watch = Stopwatch.StartNew();
            var orderedTickets = TicketsManager.OrderTicketsWithDictionary(randomTickets);
            var elapsedTime = watch.ElapsedMilliseconds;
            var isOrdered = TicketsManager.TicketsIsOrdered(orderedTickets);

            // ASSERT
            Assert.IsTrue(isOrdered);

            Debug.WriteLine("Ellapsed ms: " + elapsedTime);
        }

        [TestMethod]
        public void TicketsIsNotOrderedTest()
        {
            // ARRANGE
            var orderedTickets = new LinkedList<Ticket>();
            orderedTickets.AddLast(new Ticket("Мельбурн", "Кельн"));
            orderedTickets.AddLast(new Ticket("Кельн", "Москва"));
            orderedTickets.AddLast(new Ticket("Париж", "Мадрид"));
            orderedTickets.AddLast(new Ticket("Москва", "Париж"));

            // ACT
            var isOrdered = TicketsManager.TicketsIsOrdered(orderedTickets);

            // ASSERT
            Assert.IsFalse(isOrdered);
        }

        [TestMethod]
        public void TicketsIsOrderedTest()
        {
            // ARRANGE
            var orderedTickets = new LinkedList<Ticket>();
            orderedTickets.AddLast(new Ticket("Мельбурн", "Кельн"));
            orderedTickets.AddLast(new Ticket("Кельн", "Москва"));
            orderedTickets.AddLast(new Ticket("Москва", "Париж"));
            orderedTickets.AddLast(new Ticket("Париж", "Мадрид"));

            // ACT
            var isOrdered = TicketsManager.TicketsIsOrdered(orderedTickets);

            // ASSERT
            Assert.IsTrue(isOrdered);
        }
    }
}