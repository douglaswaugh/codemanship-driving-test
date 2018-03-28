using System;
using System.Collections.Generic;
using NUnit.Framework;
using ProNet;

namespace Tests
{
    public class DegreesOfSeparationNetworkTests
    {
        [Test]
        public void Should_build_network_using_programmers_recommendations()
        {
            var degreesOfSeparation = new DegreesOfSeparationNetwork();

            var programmer1 = BuildProgrammer("Programmer1");
            var programmer2 = BuildProgrammer("Programmer2");

            programmer1.Recommends(programmer2);

            var queue = degreesOfSeparation.BuildNetwork(programmer1);

            var expectedQueue = new Queue<Tuple<int, IProgrammer>>();
            expectedQueue.Enqueue(new Tuple<int, IProgrammer>(0, programmer1));
            expectedQueue.Enqueue(new Tuple<int, IProgrammer>(1, programmer2));

            Assert.That(queue, Is.EquivalentTo(expectedQueue));
        }

        [Test]
        public void Should_build_network_using_programmers_recommended_bys()
        {
            var programmer1 = BuildProgrammer("Programmer1");
            var programmer2 = BuildProgrammer("Programmer2");
            programmer1.Recommends(programmer2);

            var degreesOfSeparation = new DegreesOfSeparationNetwork();
            var queue = degreesOfSeparation.BuildNetwork(programmer1);

            var expectedQueue = new Queue<Tuple<int, IProgrammer>>();
            expectedQueue.Enqueue(new Tuple<int, IProgrammer>(0, programmer1));
            expectedQueue.Enqueue(new Tuple<int, IProgrammer>(1, programmer2));

            Assert.That(queue, Is.EquivalentTo(expectedQueue));
        }

        [Test]
        public void Should_only_add_each_programmer_to_the_queue_once()
        {
            var programmer1 = BuildProgrammer("Programmer1");
            var programmer2 = BuildProgrammer("Programmer2");
            var programmer3 = BuildProgrammer("Programmer3");
            programmer1.Recommends(programmer2);
            programmer1.Recommends(programmer3);
            programmer3.Recommends(programmer2);

            var degreesOfSeparation = new DegreesOfSeparationNetwork();
            var queue = degreesOfSeparation.BuildNetwork(programmer1);

            var expectedQueue = new Queue<Tuple<int, IProgrammer>>();
            expectedQueue.Enqueue(new Tuple<int, IProgrammer>(0, programmer1));
            expectedQueue.Enqueue(new Tuple<int, IProgrammer>(1, programmer2));
            expectedQueue.Enqueue(new Tuple<int, IProgrammer>(1, programmer3));

            Assert.That(queue, Is.EquivalentTo(expectedQueue));
        }

        [Test]
        public void Should_build_network_with_one_degree_of_separation()
        {
            var programmer1 = BuildProgrammer("Programmer1");
            var programmer2 = BuildProgrammer("Programmer2");
            var programmer3 = BuildProgrammer("Programmer3");
            programmer1.Recommends(programmer2);
            programmer1.Recommends(programmer3);

            var degreesOfSeparation = new DegreesOfSeparationNetwork();
            var queue = degreesOfSeparation.BuildNetwork(programmer1);

            var expectedQueue = new Queue<Tuple<int, IProgrammer>>();
            expectedQueue.Enqueue(new Tuple<int, IProgrammer>(0, programmer1));
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
            expectedQueue.Enqueue(new Tuple<int, IProgrammer>(0, programmer1));
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
            expectedQueue.Enqueue(new Tuple<int, IProgrammer>(0, programmer1));
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
            expectedQueue.Enqueue(new Tuple<int, IProgrammer>(0, jill));
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
            expectedQueue.Enqueue(BuildTuple(0, jill));
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

        private Programmer BuildProgrammer(string name)
        {
            return new Programmer(name, new string[]{}, new DegreesOfSeparationNetwork());
        }

        private Tuple<int, IProgrammer> BuildTuple(int degreeOfSeparation, IProgrammer programmer)
        {
            return new Tuple<int, IProgrammer>(degreeOfSeparation, programmer);
        }
    }
}