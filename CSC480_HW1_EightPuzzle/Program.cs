using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSC480_HW1_EightPuzzle
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] PuzzleArray = new int[] { 0, 2, 3, 1, 8, 4, 7, 6, 5 };
            int[] Easy = new int[] { 1, 3, 4, 8, 6, 2, 7, 0, 5 };
            int[] Medium = new int[] { 2, 8, 1, 0, 4, 3, 7, 6, 5 };
            int[] Hard = new int[] { 5, 6, 7, 4, 0, 8, 3, 2, 1 };
            int[] GoalState = new int[] { 1, 2, 3, 8, 0, 4, 7, 6, 5 };
            StateNode GoalStateNode = new StateNode();
            int Time = 0;
            int TotalCostOfAllMoves;
            int MaxOfQueue = 0;
            int SizeOfQueue = 0;
            int SizeOfStack = 0;
            int MaxOfStack = 0;
            bool IteritiveDeepeningSolve = false;
            //Max set of Levels to go Down in Iterative Deepening
            //int BigL = 0;

            for (int BigL = 0; BigL < 999999999; BigL++ )
            {

                PuzzleArray = Hard;

                foreach (var item in PuzzleArray)
                {
                    Console.Write(item.ToString() + " ");

                }

                Console.WriteLine();
                Console.WriteLine();

                foreach (var item in PuzzleArray)
                {
                    Console.Write(item.ToString() + " ");

                }
                Console.WriteLine();
                Console.WriteLine("State array");
                Console.WriteLine();

                StateNode node = new StateNode();
                StateNode root = new StateNode(PuzzleArray);

                //foreach (var item in node.StateArray)
                //{

                //    Console.Write(item.ToString() + " ");

                //}

                Console.WriteLine();
                Console.WriteLine("State array Passing PuzzleArray");
                Console.WriteLine();

                foreach (var item in root.StateArray2)
                {

                    Console.Write(item.ToString() + " ");

                }

                //// Create Queue of Nodes
                //Queue<StateNode> q = new Queue<StateNode>();

                // Create Queue that will accept queue returned from Successor function
                Queue<StateNode> qFromSuccessorFunction = new Queue<StateNode>();

                //// Put original puzzle state in queue
                //q.Enqueue(root);
                //SizeOfQueue = 1;
                //MaxOfQueue = MaxOfQueue + 1;


                // Depth First Search - Create a stack
                Stack<StateNode> stack = new Stack<StateNode>();

                // Put original puzzle state on stack
                stack.Push(root);
                SizeOfStack = SizeOfStack + 1;
                MaxOfStack = MaxOfStack + 1;

                // Create two Dictionaries to look up previous states for state checking. Need to check if on the stack or if it was 
                // previously looked at.
                Dictionary<int, int> dictOfNodesOnStack = new Dictionary<int, int>();

                Dictionary<int, int> dictOfStateArraysSeenBefore = new Dictionary<int, int>();

                dictOfNodesOnStack.Add(root.ArrayStateInt, root.ArrayStateInt);




                // Beginning of General Search Loop
                bool keepRunning = true;
                while (keepRunning)
                {
                    if (stack.Count == 0)
                    {
                        Console.WriteLine("stack empty");
                        keepRunning = false;
                    }
                    if (keepRunning == false)
                    {
                        break;
                    }




                    //// Remove nodes from front of queue for Breadth-First Search
                    //StateNode removeFromFront = new StateNode();
                    //removeFromFront = q.Dequeue();
                    //Time = Time + 1;

                    // Remove nodes from top of stack for Depth-First Search
                    StateNode removeFromTopOfStack = new StateNode();
                    removeFromTopOfStack = stack.Pop();
                    Time = Time + 1;

                    // Remove node from Dictionary with list of nodes currently on stack
                    dictOfNodesOnStack.Remove(removeFromTopOfStack.ArrayStateInt);
                    Console.WriteLine();
                    Console.WriteLine("removing this node StateArrayInt: " + removeFromTopOfStack.ArrayStateInt);
                    Console.WriteLine();

                    // add this node to dictionary of nodes we have seen before if we haven't already seen it
                    if (dictOfStateArraysSeenBefore.ContainsKey(removeFromTopOfStack.ArrayStateInt))
                    {
                        //do nothing
                    }
                    else
                    {
                        dictOfStateArraysSeenBefore.Add(removeFromTopOfStack.ArrayStateInt, removeFromTopOfStack.ArrayStateInt);
                    }





                    bool isEqual = Enumerable.SequenceEqual(removeFromTopOfStack.StateArray2, GoalState);
                    if (isEqual)
                    {
                        Console.WriteLine();
                        //Console.WriteLine("Puzzle is Solved!");
                        IteritiveDeepeningSolve = true;
                        GoalStateNode = removeFromTopOfStack;
                        Console.WriteLine();
                        break;
                    }




                    // REturn the Node that matches solution




                    // Check the level of Node in the tree. If Node level is less than BigL, call sussessor.
                    // If == to BigL, can't call successor. We reached limit.

                    if (removeFromTopOfStack.LevelDownInTree < BigL)
                    {
                        qFromSuccessorFunction = StateNode.Successor(removeFromTopOfStack);
                    }



                    //// add nodes from Successor Function into the q for the loop

                    //while (qFromSuccessorFunction.Count != 0)
                    //{
                    //    StateNode removeFromFrontOfSuccessorQueue = new StateNode();
                    //    removeFromFrontOfSuccessorQueue = qFromSuccessorFunction.Dequeue();
                    //    q.Enqueue(removeFromFrontOfSuccessorQueue);
                    //    SizeOfQueue = q.Count;
                    //    if (MaxOfQueue < SizeOfQueue)
                    //    {
                    //        MaxOfQueue = SizeOfQueue;
                    //    }
                    //}

                    //// add nodes from Successor Function into the q for the loop

                    while (qFromSuccessorFunction.Count != 0)
                    {
                        StateNode removeFromFrontOfSuccessorQueue = new StateNode();
                        removeFromFrontOfSuccessorQueue = qFromSuccessorFunction.Dequeue();





                        //If we have already seen this state before or this state is on the stack currently, do nothing. Otherwise put state in both dictionaries
                        if (dictOfNodesOnStack.ContainsKey(removeFromFrontOfSuccessorQueue.ArrayStateInt) || (dictOfStateArraysSeenBefore.ContainsKey(removeFromFrontOfSuccessorQueue.ArrayStateInt)))
                        {
                            // do nothing
                        }
                        else
                        {
                            stack.Push(removeFromFrontOfSuccessorQueue);
                            dictOfNodesOnStack.Add(removeFromFrontOfSuccessorQueue.ArrayStateInt, removeFromFrontOfSuccessorQueue.ArrayStateInt);
                            dictOfStateArraysSeenBefore.Add(removeFromFrontOfSuccessorQueue.ArrayStateInt, removeFromFrontOfSuccessorQueue.ArrayStateInt);
                        }

                        SizeOfStack = stack.Count();
                        if (MaxOfStack < SizeOfStack)
                        {
                            MaxOfStack = SizeOfStack;
                        }

                    }


                }
                
                if (IteritiveDeepeningSolve == true)
                {
                    Console.WriteLine();
                    Console.WriteLine("out of loop");
                    Console.WriteLine();


                    Console.WriteLine();
                    Console.WriteLine("Here were the successful moves to solve the puzzle:");
                    Console.WriteLine();

                    StateNode.PrettyPrintPathToSolvePuzzle(GoalStateNode);

                    Console.WriteLine();
                    Console.WriteLine("Time = " + Time);
                    Console.WriteLine();
                    Console.WriteLine("MaxOfStack " + MaxOfStack);
                    Console.WriteLine();


                    break;
                }


                //Console.ReadLine();
            }
            Console.WriteLine();
            Console.WriteLine("IterativeDeepening Solved at this level : " + GoalStateNode.LevelDownInTree);
            Console.WriteLine();
            Console.ReadLine();
        }

        public static void SwapNums(int[] Array, int position1, int position2)
        {
            int temp = Array[position1];
            Array[position1] = Array[position2];
            Array[position2] = temp;
        }

        
    }
}
