using System;
using System.Collections.Generic;
using NUnit.Framework;
using ProNet;

namespace Tests
{
    public class DegreesOfSeparationTests
    {
        [Test]
        public void Add_relations_to_should_add_programmers_recommendations()
        {
            var degreesOfSeparation = new DegreesOfSeparation();

            var queue = new Queue<Tuple<int, IProgrammer>>();

            var programmer1 = new Programmer("Programmer1", new string[]{});
            var programmer2 = new Programmer("Programmer2", new string[]{});

            programmer1.Recommends(programmer2);

            degreesOfSeparation.AddRelationsTo(queue, 1, programmer1);

            var expectedQueue = new Queue<Tuple<int, IProgrammer>>();
            expectedQueue.Enqueue(new Tuple<int, IProgrammer>(1, programmer2));

            Assert.That(queue, Is.EquivalentTo(expectedQueue));
        }

        [Test]
        public void Add_relations_to_should_add_programmers_recommended_by()
        {
            var programmer1 = new Programmer("Programmer1", new string[]{});
            var programmer2 = new Programmer("Programmer2", new string[]{});
            programmer1.Recommends(programmer2);

            var degreesOfSeparation = new DegreesOfSeparation();
            var queue = new Queue<Tuple<int, IProgrammer>>();

            degreesOfSeparation.AddRelationsTo(queue, 1, programmer2);

            var expectedQueue = new Queue<Tuple<int, IProgrammer>>();
            expectedQueue.Enqueue(new Tuple<int, IProgrammer>(1, programmer1));

            Assert.That(queue, Is.EquivalentTo(expectedQueue));
        }

        [Test]
        public void Add_relations_to_should_not_add_programmers_already_in_the_queue()
        {
            var programmer1 = new Programmer("Programmer1", new string[]{});
            var programmer2 = new Programmer("Programmer2", new string[]{});
            var programmer3 = new Programmer("Programmer3", new string[]{});
            programmer1.Recommends(programmer2);
            programmer1.Recommends(programmer3);

            var programmerAlreadyInQueue = programmer3;
            var queue = new Queue<Tuple<int, IProgrammer>>();
            queue.Enqueue(new Tuple<int, IProgrammer>(1, programmerAlreadyInQueue));

            var degreesOfSeparation = new DegreesOfSeparation();
            degreesOfSeparation.AddRelationsTo(queue, 2, programmer1);

            var expectedQueue = new Queue<Tuple<int, IProgrammer>>();
            expectedQueue.Enqueue(new Tuple<int, IProgrammer>(1, programmerAlreadyInQueue));
            expectedQueue.Enqueue(new Tuple<int, IProgrammer>(2, programmer2));

            Assert.That(queue, Is.EquivalentTo(expectedQueue));
        }
    }
}