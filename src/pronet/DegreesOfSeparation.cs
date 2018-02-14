using System;
using System.Collections.Generic;
using System.Linq;

namespace ProNet
{
    public class DegreesOfSeparation
    {
        DegreesOfSeparationNetwork _degreesOfSeparationNetwork;

        public DegreesOfSeparation()
        {
            _degreesOfSeparationNetwork = new DegreesOfSeparationNetwork();
        }

        public int Calculate(IProgrammer programmerFrom, IProgrammer programmer)
        {
            if (programmerFrom == programmer)
                return 0;
            
            var toProcess = _degreesOfSeparationNetwork.BuildNetwork(programmerFrom);

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

        private int ProcessQueue(IProgrammer programmer, List<Tuple<int, IProgrammer>> toProcess)
        {
            foreach (var networkProgrammer in toProcess)
            {
                if (AreRelated(networkProgrammer, programmer))
                    return networkProgrammer.Item1;
            }

            throw new ProgrammersNotConnectedException();
        }
    }
}