﻿using System.Collections.Generic;


namespace Puzzle15
{
    public abstract class Solver 
    {
        private List<State> closed = null;

        public LinkedList<State> Solve(State initial)
        {
            closed = new List<State>();
            ClearOpen();
            AddState(initial);

            while (HasElements())
            {
                State s = NextState();


                if (s.IsSolution())
                    return FindPath(s);

                closed.Add(s);

                LinkedList<State> moves = s.GetPossibleMoves();

                foreach (State move in moves)
                    if (!closed.Contains(move))
                        AddState(move);
            }

            return null;
        }

        private LinkedList<State> FindPath(State solution)
        {
            LinkedList<State> path = new LinkedList<State>();

            while (solution != null)
            {
                path.AddFirst(solution);
                solution = solution.Parent;
            }

            return path;
        }

        protected abstract bool HasElements();
        protected abstract State NextState();
        protected abstract void AddState(State state);
        protected abstract void ClearOpen();
    }
}
