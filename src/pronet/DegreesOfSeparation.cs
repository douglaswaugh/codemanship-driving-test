using System;
using System.Collections.Generic;
using System.Linq;

namespace ProNet
{
    public class DegreesOfSeparation
    {
        public int Calculate(IProgrammer programmerFrom, IProgrammer programmer)
        {
            if (programmerFrom == programmer)
                return 0;
            
            var toProcess = InitializeQueue(programmerFrom);

            return ProcessQueue(programmer, toProcess);
        }

        private bool AreRelated(Tuple<int, IProgrammer> programmerToProcess, IProgrammer programmer)
        {
            return programmerToProcess.Item2.IsRelatedTo(programmer);
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

        private int ProcessQueue(IProgrammer programmer, Queue<Tuple<int, IProgrammer>> toProcess)
        {
            var network = new List<Tuple<int, IProgrammer>>();

            while (toProcess.Count > 0)
            {
                var programmerToProcess = toProcess.Dequeue();

                network.Add(programmerToProcess);

                AddRelationsTo(toProcess, programmerToProcess.Item1 + 1, programmerToProcess.Item2, network);
            }

            foreach (var networkProgrammer in network)
            {
                if (AreRelated(networkProgrammer, programmer))
                    return networkProgrammer.Item1;
            }

            throw new ProgrammersNotConnectedException();
        }

        private Queue<Tuple<int, IProgrammer>> InitializeQueue(IProgrammer programmerFrom)
        {
            var toProcess = new Queue<Tuple<int, IProgrammer>>();
            AddRelationsTo(toProcess, 2, programmerFrom, new List<Tuple<int, IProgrammer>>());
            return toProcess;
        }
    }
}