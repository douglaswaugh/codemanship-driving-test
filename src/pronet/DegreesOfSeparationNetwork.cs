using System;
using System.Collections.Generic;
using System.Linq;

namespace ProNet
{
    public class DegreesOfSeparationNetwork
    {
        public List<Tuple<int, IProgrammer>> BuildNetwork(IProgrammer programmerFrom)
        {
            var toProcess = new Queue<Tuple<int, IProgrammer>>();
            AddRelationsTo(toProcess, 2, programmerFrom, new List<Tuple<int, IProgrammer>>());

            var network = new List<Tuple<int, IProgrammer>>();

            while (toProcess.Count > 0)
            {
                var programmerToProcess = toProcess.Dequeue();

                network.Add(programmerToProcess);

                AddRelationsTo(toProcess, programmerToProcess.Item1 + 1, programmerToProcess.Item2, network);
            }

            return network;
        }

        public void AddRelationsTo(Queue<Tuple<int, IProgrammer>> queue, int degreeOfSeparation, IProgrammer processed, List<Tuple<int, IProgrammer>> network)
        {
            foreach (var relation in processed.Relations)
            {
                if (processed != relation && !queue.Any(tuple => tuple.Item2.Name == relation.Name) && !network.Any(tuple => tuple.Item2.Name == relation.Name))
                {
                    queue.Enqueue(new Tuple<int, IProgrammer>(degreeOfSeparation, relation));
                }
            }
        }
    }
}