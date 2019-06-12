﻿using System.Collections.Generic;

namespace Puzzle15
{
    public abstract class State 
    {
        private State parent = null;
        private double distance = 0;

        public State Parent
        {
            get
            {
                return parent;
            }

            set
            {
                parent = value;
            }
        }

        public double Distance
        {
            get
            {
                return distance;
            }

            set
            {
                distance = value;
            }
        }

        public State() { }
        public State(State parent)
        {
            this.parent = parent;
            this.distance = parent.distance + 1;
        }

        public abstract LinkedList<State> GetPossibleMoves();
        public abstract bool IsSolution();
        public abstract double GetHeuristic();
    }
}
