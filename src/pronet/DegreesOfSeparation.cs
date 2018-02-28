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

        public int Calculate(IProgrammer programmerFrom, IProgrammer programmerTo)
        {
            if (programmerFrom == programmerTo)
                return 0;
            
            var network = _degreesOfSeparationNetwork.BuildNetwork(programmerFrom);

            return FindDegrees(programmerTo, network);
        }

        private int FindDegrees(IProgrammer programmerTo, List<Tuple<int, IProgrammer>> network)
        {
            foreach (var networkProgrammer in network)
            {
                if (networkProgrammer.Item2.Equals(programmerTo))
                    return networkProgrammer.Item1;
            }

            throw new ProgrammersNotConnectedException();
        }
    }
}