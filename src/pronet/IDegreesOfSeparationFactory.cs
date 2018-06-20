using System.Collections.Generic;

namespace ProNet
{
    public interface IDegreesOfSeparationFactory
    {
        DegreesOfSeparation BuildDegreesOfSeparation(IEnumerable<IProgrammer> programmers);
    }
}
