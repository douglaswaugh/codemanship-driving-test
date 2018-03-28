using System;
using System.Collections.Generic;
using System.Linq;

namespace ProNet
{
    // TODO: This could probably do with being refactored a bit
    public class DegreesOfSeparationNetwork
    {
        public List<Tuple<int, IProgrammer>> BuildNetwork(IProgrammer programmerFrom)
        {
            var toProcess = new Queue<Tuple<int, IProgrammer>>();
            toProcess.Enqueue(new Tuple<int, IProgrammer>(0, programmerFrom));

            var network = new List<Tuple<int, IProgrammer>>();

            while (toProcess.Count > 0)
            {
                var programmerToProcess = toProcess.Dequeue();

                network.Add(programmerToProcess);

                AddRelationsTo(programmerToProcess.Item2, toProcess, network, programmerToProcess.Item1 + 1);
            }

            return network;
        }

        private void AddRelationsTo(IProgrammer processed, Queue<Tuple<int, IProgrammer>> queue, List<Tuple<int, IProgrammer>> network, int degreeOfSeparation)
        {
            foreach (var relation in processed.Relations)
            {
                if (!queue.Any(tuple => tuple.Item2.Equals(relation)) && !network.Any(tuple => tuple.Item2.Equals(relation)))
                {
                    queue.Enqueue(new Tuple<int, IProgrammer>(degreeOfSeparation, relation));
                }
            }
        }
    }
}