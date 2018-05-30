using System;
using System.Collections.Generic;
using System.Linq;

namespace ProNet
{
    // TODO: This could probably do with being refactored a bit
    public class DegreesOfSeparation
    {
        private readonly IEnumerable<IProgrammer> _programmers;

        public DegreesOfSeparation(IEnumerable<IProgrammer> programmers)
        {
            _programmers = programmers;
        }

        public int Between(IProgrammer programmerFrom, IProgrammer programmerTo)
        {
            if (programmerFrom.Equals(programmerTo))
                return 0;

            var parents = BuildParentPointers(programmerFrom);

            return ShortestPath(programmerFrom, programmerTo, parents);
        }

        private Dictionary<IProgrammer, IProgrammer> BuildParentPointers(IProgrammer programmerFrom)
        {
            var parents = new Dictionary<IProgrammer, IProgrammer>();
            var queue = new Queue<IProgrammer>();

            queue.Enqueue(programmerFrom);

            while (queue.Count() > 0)
            {
                AddAdjacentNodes(parents, queue);
                queue.Dequeue();
            }

            return parents;
        }

        private static void AddAdjacentNodes(Dictionary<IProgrammer, IProgrammer> parents, Queue<IProgrammer> queue)
        {
            var candidate = queue.First();

            foreach (var programmer in candidate.Relations())
            {
                AddNode(parents, queue, candidate, programmer);
            }
        }

        private static void AddNode(Dictionary<IProgrammer, IProgrammer> parents, Queue<IProgrammer> queue, IProgrammer parent, IProgrammer programmer)
        {
            if (!parents.ContainsKey(programmer))
            {
                queue.Enqueue(programmer);
                parents.Add(programmer, parent);
            }
        }

        private static int ShortestPath(IProgrammer programmerFrom, IProgrammer programmerTo, Dictionary<IProgrammer, IProgrammer> parents)
        {
            var degreesOfSeparation = 0;
            IProgrammer parentProgrammer = programmerTo;

            while (!programmerFrom.Equals(parentProgrammer))
            {
                parentProgrammer = parents[parentProgrammer];
                degreesOfSeparation++;
            }

            return degreesOfSeparation;
        }
    }
}