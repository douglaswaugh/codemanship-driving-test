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

            return FindDegrees(programmer, toProcess);
        }

        private bool AreRelated(Tuple<int, IProgrammer> programmerToProcess, IProgrammer programmer)
        {
            return programmerToProcess.Item2.IsRelatedTo(programmer);
        }

        private int FindDegrees(IProgrammer programmer, List<Tuple<int, IProgrammer>> toProcess)
        {
            foreach (var networkProgrammer in toProcess)
            {
                if (networkProgrammer.Item2.Equals(programmer))
                    return networkProgrammer.Item1;
            }

            throw new ProgrammersNotConnectedException();
        }
    }
}