using System;
using System.Collections.Generic;

namespace ProNet
{
    public interface IProgrammer
    { 
        ProgrammerDto Details { get; }

        bool IsNamed(string name);
        void UpdateRank();
        void Recommends(Programmer programmer);
        List<Tuple<int, IProgrammer>> BuildNetwork();
        void AddRelationsTo(Queue<Tuple<int, IProgrammer>> queue, int degreeOfSeparation);
    }
}