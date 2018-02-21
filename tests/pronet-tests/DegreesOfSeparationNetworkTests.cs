using System;
using System.Collections.Generic;
using NUnit.Framework;
using ProNet;

namespace Tests
{
    public class DegreesOfSeparationNetworkTests
    {
        [Test]
        public void Add_relations_to_should_add_programmers_recommendations()
        {
            var degreesOfSeparation = new DegreesOfSeparationNetwork();

            var queue = new Queue<Tuple<int, IProgrammer>>();

            var programmer1 = new Programmer("Programmer1", new string[]{});
            var programmer2 = new Programmer("Programmer2", new string[]{});

            programmer1.Recommends(programmer2);

            degreesOfSeparation.AddRelationsTo(programmer1, queue, 1, programmer1, new List<Tuple<int, IProgrammer>>());

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

            var degreesOfSeparation = new DegreesOfSeparationNetwork();
            var queue = new Queue<Tuple<int, IProgrammer>>();

            degreesOfSeparation.AddRelationsTo(programmer2, queue, 1, programmer2, new List<Tuple<int, IProgrammer>>());

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

            var degreesOfSeparation = new DegreesOfSeparationNetwork();
            degreesOfSeparation.AddRelationsTo(programmer1, queue, 2, programmer1 , new List<Tuple<int, IProgrammer>>());

            var expectedQueue = new Queue<Tuple<int, IProgrammer>>();
            expectedQueue.Enqueue(new Tuple<int, IProgrammer>(1, programmerAlreadyInQueue));
            expectedQueue.Enqueue(new Tuple<int, IProgrammer>(2, programmer2));

            Assert.That(queue, Is.EquivalentTo(expectedQueue));
        }

        [Test]
        public void Should_build_network_with_one_degree_of_separation()
        {
            var programmer1 = new Programmer("Programmer1", new string[]{});
            var programmer2 = new Programmer("Programmer2", new string[]{});
            var programmer3 = new Programmer("Programmer3", new string[]{});
            programmer1.Recommends(programmer2);
            programmer1.Recommends(programmer3);

            var degreesOfSeparation = new DegreesOfSeparationNetwork();
            var queue = degreesOfSeparation.BuildNetwork(programmer1);

            var expectedQueue = new Queue<Tuple<int, IProgrammer>>();
            expectedQueue.Enqueue(new Tuple<int, IProgrammer>(1, programmer2));
            expectedQueue.Enqueue(new Tuple<int, IProgrammer>(1, programmer3));

            Assert.That(queue, Is.EquivalentTo(expectedQueue));
        }

        [Test]
        public void Should_build_network_with_two_degrees_of_separation()
        {
            var programmer1 = BuildProgrammer("Programmer1");
            var programmer2 = BuildProgrammer("Programmer2");
            var programmer3 = BuildProgrammer("Programmer3");

            programmer1.Recommends(programmer2);
            programmer2.Recommends(programmer3);

            var degreesOfSeparation = new DegreesOfSeparationNetwork();
            var queue = degreesOfSeparation.BuildNetwork(programmer1);

            var expectedQueue = new Queue<Tuple<int, IProgrammer>>();
            expectedQueue.Enqueue(new Tuple<int, IProgrammer>(1, programmer2));
            expectedQueue.Enqueue(new Tuple<int, IProgrammer>(2, programmer3));

            Assert.That(queue, Is.EquivalentTo(expectedQueue));
        }

        [Test]
        public void Should_build_network_with_three_degrees_of_separation()
        {
            var programmer1 = BuildProgrammer("Programmer1");
            var programmer2 = BuildProgrammer("Programmer2");
            var programmer3 = BuildProgrammer("Programmer3");
            var programmer4 = BuildProgrammer("Programmer4");

            programmer1.Recommends(programmer2);
            programmer2.Recommends(programmer3);
            programmer3.Recommends(programmer4);

            var degreesOfSeparation = new DegreesOfSeparationNetwork();
            var queue = degreesOfSeparation.BuildNetwork(programmer1);

            var expectedQueue = new Queue<Tuple<int, IProgrammer>>();
            expectedQueue.Enqueue(new Tuple<int, IProgrammer>(1, programmer2));
            expectedQueue.Enqueue(new Tuple<int, IProgrammer>(2, programmer3));
            expectedQueue.Enqueue(new Tuple<int, IProgrammer>(3, programmer4));

            Assert.That(queue, Is.EquivalentTo(expectedQueue));
        }

        [Test]
        public void Should_build_network_with_three_degrees_of_separation_and_recommended_by_connections()
        {
            var jill = BuildProgrammer("Jill");
            var bill = BuildProgrammer("Bill");
            var ed = BuildProgrammer("Ed");
            var rick = BuildProgrammer("Rick");

            bill.Recommends(jill);
            ed.Recommends(bill);
            ed.Recommends(rick);
            rick.Recommends(ed);

            var degreesOfSeparation = new DegreesOfSeparationNetwork();
            var queue = degreesOfSeparation.BuildNetwork(jill);

            var expectedQueue = new Queue<Tuple<int, IProgrammer>>();
            expectedQueue.Enqueue(new Tuple<int, IProgrammer>(1, bill));
            expectedQueue.Enqueue(new Tuple<int, IProgrammer>(2, ed));
            expectedQueue.Enqueue(new Tuple<int, IProgrammer>(3, rick));

            Assert.That(queue, Is.EquivalentTo(expectedQueue));
        }

        [Test]
        public void Should_build_network_provided_in_pronet_xml()
        {
            var jill = BuildProgrammer("Jill");
            var bill = BuildProgrammer("Bill");
            var ed = BuildProgrammer("Ed");
            var rick = BuildProgrammer("Rick");
            var nick = BuildProgrammer("Nick");
            var dave = BuildProgrammer("Dave");
            var stu = BuildProgrammer("Stu");
            var frank = BuildProgrammer("Frank");
            var liz = BuildProgrammer("Liz");
            var jason = BuildProgrammer("Jason");

            bill.Recommends(jason);
            bill.Recommends(jill);
            bill.Recommends(nick);
            bill.Recommends(stu);
            dave.Recommends(jill);
            ed.Recommends(liz);
            ed.Recommends(rick);
            ed.Recommends(bill);
            frank.Recommends(nick);
            jason.Recommends(dave);
            jason.Recommends(liz);
            jill.Recommends(nick);
            liz.Recommends(bill);
            nick.Recommends(stu);
            rick.Recommends(ed);
            stu.Recommends(frank);

            var degreesOfSeparation = new DegreesOfSeparationNetwork();
            var queue = degreesOfSeparation.BuildNetwork(jill);

            var expectedQueue = new Queue<Tuple<int, IProgrammer>>();
            expectedQueue.Enqueue(BuildTuple(1, nick));
            expectedQueue.Enqueue(BuildTuple(1, dave));
            expectedQueue.Enqueue(BuildTuple(1, bill));
            expectedQueue.Enqueue(BuildTuple(2, stu));
            expectedQueue.Enqueue(BuildTuple(2, frank));
            expectedQueue.Enqueue(BuildTuple(2, jason));
            expectedQueue.Enqueue(BuildTuple(2, liz));
            expectedQueue.Enqueue(BuildTuple(2, ed));
            expectedQueue.Enqueue(BuildTuple(3, rick));

            Assert.That(queue, Is.EquivalentTo(expectedQueue));
        }

        private IProgrammer BuildProgrammer(string name)
        {
            return new Programmer(name, new string[]{});
        }

        private Tuple<int, IProgrammer> BuildTuple(int degreeOfSeparation, IProgrammer programmer)
        {
            return new Tuple<int, IProgrammer>(degreeOfSeparation, programmer);
        }
    }
}