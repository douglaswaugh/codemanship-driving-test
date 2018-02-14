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

        public void AddRelationsTo(Queue<Tuple<int, IProgrammer>> queue, int degreeOfSeparation, IProgrammer processed)
        {
            foreach (var relation in processed.Relations)
            {
                if (processed != relation && !queue.Any(tuple => tuple.Item2.Name == relation.Name))
                {
                    queue.Enqueue(new Tuple<int, IProgrammer>(degreeOfSeparation, relation));
                }
            }
        }

        private int ProcessQueue(IProgrammer programmer, Queue<Tuple<int, IProgrammer>> toProcess)
        {
            while (toProcess.Count > 0)
            {
                var programmerToProcess = toProcess.Dequeue();

                if (AreRelated(programmerToProcess, programmer))
                    return programmerToProcess.Item1;

                AddRelationsTo(toProcess, programmerToProcess.Item1 + 1, programmerToProcess.Item2);
            }

            throw new ProgrammersNotConnectedException();
        }

        private Queue<Tuple<int, IProgrammer>> InitializeQueue(IProgrammer programmerFrom)
        {
            var toProcess = new Queue<Tuple<int, IProgrammer>>();
            AddRelationsTo(toProcess, 2, programmerFrom);
            return toProcess;
        }
    }
}