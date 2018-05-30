using System.Collections.Generic;
using System.Linq;

namespace ProNet
{
    public class DegreesOfSeparationFactory : IDegreesOfSeparationFactory
    {
        public DegreesOfSeparation BuildDegreesOfSeparation(IEnumerable<IProgrammer> programmers)
        {
            return new DegreesOfSeparation(programmers);
        }
    }
}
