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

                if (!NetworkContainsProgrammer(network, programmerToProcess))
                {
                    network.Add(programmerToProcess);
                    programmerToProcess.Item2.AddRelationsTo(toProcess, programmerToProcess.Item1 + 1);
                }
            }

            return network;
        }

        private bool NetworkContainsProgrammer(List<Tuple<int, IProgrammer>> network, Tuple<int, IProgrammer> programmerToProcess)
        {
            return network.Any(tuple => tuple.Item2.Equals(programmerToProcess.Item2));
        }
    }
}