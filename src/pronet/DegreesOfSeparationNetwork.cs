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
            return BuildNetwork(InitialiseQueue(programmerFrom), InitialiseNetwork());
        }

        private List<Tuple<int, IProgrammer>> BuildNetwork(Queue<Tuple<int, IProgrammer>> toProcess, List<Tuple<int, IProgrammer>> network)
        {
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

        private List<Tuple<int, IProgrammer>> InitialiseNetwork()
        {
            return new List<Tuple<int, IProgrammer>>();
        }

        private Queue<Tuple<int, IProgrammer>> InitialiseQueue(IProgrammer programmerFrom)
        {
            var toProcess = new Queue<Tuple<int, IProgrammer>>();
            toProcess.Enqueue(new Tuple<int, IProgrammer>(0, programmerFrom));
            return toProcess;
        }

        private bool NetworkContainsProgrammer(List<Tuple<int, IProgrammer>> network, Tuple<int, IProgrammer> programmerToProcess)
        {
            return network.Any(tuple => tuple.Item2.Equals(programmerToProcess.Item2));
        }
    }
}