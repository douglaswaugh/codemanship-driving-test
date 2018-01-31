using System;
using System.Collections.Generic;

namespace ProNet
{
    public class DegreesOfSeparation
    {
        public int Calculate(IProgrammer programmerFrom, IProgrammer programmer)
        {
            if (programmerFrom == programmer)
                return 0;

            var toProcess = new Queue<Tuple<int, IProgrammer>>();
            toProcess.Enqueue(new Tuple<int, IProgrammer>(1, programmerFrom));

            while (toProcess.Count > 0)
            {
                var programmerToProcess = toProcess.Dequeue();

                if (HasRecommended(programmerToProcess, programmer))
                    return programmerToProcess.Item1;

                if (IsRecommendedBy(programmerToProcess, programmer))
                    return programmerToProcess.Item1;

                AddRelationsTo(toProcess, programmerToProcess.Item1 + 1, programmerToProcess.Item2);
            }

            throw new ProgrammersNotConnectedException();
        }

        public bool HasRecommended(Tuple<int, IProgrammer> programmerToProcess, IProgrammer programmer)
        {
            return programmerToProcess.Item2.HasRecommended(programmer);
        }

        public bool IsRecommendedBy(Tuple<int, IProgrammer> programmerToProcess, IProgrammer programmer)
        {
            return programmerToProcess.Item2.WasRecommendedBy(programmer);
        }

        public void AddRelationsTo(Queue<Tuple<int, IProgrammer>> queue, int degreeOfSeparation, IProgrammer processed)
        {
            foreach (var relation in processed.Relations)
            {
                if (processed != relation)
                    queue.Enqueue(new Tuple<int, IProgrammer>(degreeOfSeparation, relation));
            }
        }
    }
}