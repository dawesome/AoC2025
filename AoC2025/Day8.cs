namespace AoC2025;

public class Pair
{
    public int Item1;
    public int Item2;
    
    public Pair(int item1, int item2)
    {
        Item1 = item1;
        Item2 = item2;
    }
}

public class JunctionBox
{
    public double x;
    public double y;
    public double z;

    public JunctionBox(double xCoord, double yCoord, double zCoord)
    {
        x = xCoord;
        y = yCoord;
        z = zCoord;
    }
    
    public double Distance(JunctionBox other)
    {
        return Math.Sqrt(Math.Pow(x - other.x, 2) + Math.Pow(y - other.y, 2) + Math.Pow(z - other.z, 2));
    }
}

public class JunctionPair
{
    public int ReferenceJunctionIndex;
    public int ClosestJunctionIndex;
    public double DistanceFromReferenceToClosestJunction;

    public JunctionPair()
    {
        ReferenceJunctionIndex = -1;
        ClosestJunctionIndex = -1;
        DistanceFromReferenceToClosestJunction = double.MaxValue;
    }
    
    public JunctionPair(int reference, int closest, double distance)
    {
        ReferenceJunctionIndex = reference;
        ClosestJunctionIndex = closest;
        DistanceFromReferenceToClosestJunction = distance;
    }
}

class JunctionPairComparer : IEqualityComparer<JunctionPair>
{
    public bool Equals(JunctionPair x, JunctionPair y)
    {

        if (x.DistanceFromReferenceToClosestJunction == y.DistanceFromReferenceToClosestJunction)
        {
            if (x.ReferenceJunctionIndex == y.ReferenceJunctionIndex &&
                x.ClosestJunctionIndex   == y.ClosestJunctionIndex)
            {
                return true;
            } 
            else if (x.ReferenceJunctionIndex == y.ClosestJunctionIndex &&
                     x.ClosestJunctionIndex   == y.ReferenceJunctionIndex)
            {
                return true;
            }
        }

        return false;
    }
    
    public int GetHashCode(JunctionPair obj)
    {
        return obj.ReferenceJunctionIndex.GetHashCode() ^ obj.ClosestJunctionIndex.GetHashCode();
    }
}

public class JunctionPairSorter: IComparer<JunctionPair>
{
    public int Compare(JunctionPair? x, JunctionPair? y)
    {
        return x.DistanceFromReferenceToClosestJunction.CompareTo(y.DistanceFromReferenceToClosestJunction);
    }    
}

public class CircuitListSorter : IComparer<List<int>>
{
    public int Compare(List<int> x, List<int> y)
    {
        return y.Count.CompareTo(x.Count);
    }
}

public class Day8
{
    public JunctionBox[] JunctionBoxes;
    public int[] ClosestJunctionIndexArray;
    public double[] DistanceToClosestJunctionArray;
    public List<JunctionPair> JunctionDistanceList = new();
    

    public void PrintJunctionBoxes()
    {
        foreach (JunctionBox vector in JunctionBoxes)
        {
            Console.WriteLine( "[" + vector.x + ", " + vector.y + ", " + vector.z + "]");
        }
    }

    public void PrintCircuits(List<List<int>> circuits)
    {
        circuits.Sort(new CircuitListSorter());
        
        Console.Write("[ ");
        foreach (List<int> circuit in circuits)
        {
            circuit.Sort();
            Console.Write("[ ");
            foreach (int i in circuit)
            {
                Console.Write(i + ", ");
            }
            Console.Write("], ");
        }
        Console.WriteLine("]");
    }
    
    public void ParseLines(IEnumerable<string> lines)
    {
        JunctionBoxes = new JunctionBox[lines.Count()];
        for (int count = 0; count < lines.Count(); ++count)
        {
            var coordinates = lines.ElementAt(count).Split(',');
            JunctionBoxes[count] = new JunctionBox(double.Parse(coordinates[0]), double.Parse(coordinates[1]), double.Parse(coordinates[2]));
        }
    }

    public int[] CalculateDistancesBetweenJunctionBoxes()
    {
        // Build the list of pair distances
        ClosestJunctionIndexArray = new int[JunctionBoxes.Length];
        DistanceToClosestJunctionArray = new double[JunctionBoxes.Length];
        
        for (int outerIndex = 0; outerIndex < JunctionBoxes.Length; ++outerIndex)
        {
            for (int innerIndex = 0; innerIndex < JunctionBoxes.Length; ++innerIndex)
            {
                if (outerIndex == innerIndex)
                {
                    continue;
                }
                
                JunctionBox outerBox = JunctionBoxes[outerIndex];
                JunctionBox innerBox = JunctionBoxes[innerIndex];
                JunctionPair pairDistance = new(outerIndex, innerIndex, outerBox.Distance(innerBox));
                JunctionDistanceList.Add(pairDistance);
            }
        }
        
        // Remove entries that are duplicate pairs
        JunctionDistanceList = JunctionDistanceList.Distinct(new JunctionPairComparer()).ToList();
        JunctionDistanceList.Sort(new JunctionPairSorter());
        
        return ClosestJunctionIndexArray;
    }
    
    public JunctionPair FindClosestPair(List<int> junctionBoxIndices)
    {
        // TODO: The thing that's wrong here is that I need to take out DIRECTLY CONNECTED pairs when considering distance.
        // So, if 0/19 are a directly connected pair (they have each other as closest?), then no
        //   other boxes should consider them as closest -- other boxes are looking for their closest pair?
        // 

        JunctionPair closestPair = new();
        closestPair.DistanceFromReferenceToClosestJunction = double.MaxValue;

        foreach (int outerJunctionBoxIndex in junctionBoxIndices)
        {
            foreach (int innerJunctionBoxIndex in junctionBoxIndices)
            {
                if (outerJunctionBoxIndex == innerJunctionBoxIndex)
                {
                    continue;
                }
                JunctionBox outerBox = JunctionBoxes[outerJunctionBoxIndex];
                JunctionBox innerBox = JunctionBoxes[innerJunctionBoxIndex];
                double distance = outerBox.Distance(innerBox);
                if (distance < closestPair.DistanceFromReferenceToClosestJunction)
                {
                    closestPair.DistanceFromReferenceToClosestJunction = distance;
                    closestPair.ReferenceJunctionIndex = outerJunctionBoxIndex;
                    closestPair.ClosestJunctionIndex   = innerJunctionBoxIndex;
                }
            }
        }
        
        return closestPair;
    }

    public List<List<int>> ConnectClosestJunctions(long iterations, bool printCircuits = false)
    {
        List<List<int>> circuits = new List<List<int>>();

        // Start with every junction box in its own circut
        for (int index = 0; index < JunctionBoxes.Length; index++)
        {
            circuits.Add(new List<int> { index });
        }
        
        for (int index = 0; index < JunctionDistanceList.Count && iterations > 0; ++index)
        {
            JunctionPair junctionPair = JunctionDistanceList[index];
            
            ConnectJunctionPairInCicuits(circuits, junctionPair, printCircuits);
            --iterations;
        }
        return circuits;
    }

    public void ConnectJunctionPairInCicuits(List<List<int>> circuits, JunctionPair junctionPair, bool printCircuits)
    {
        int circuitIndex = 0;
        Pair junctionToCircuitPair = new Pair(-1, -1);
            
        foreach (List<int> circuit in circuits)
        {
            if (circuit.Contains(junctionPair.ReferenceJunctionIndex))
            {
                junctionToCircuitPair.Item1 = circuitIndex;
            }
            if (circuit.Contains(junctionPair.ClosestJunctionIndex))
            {
                junctionToCircuitPair.Item2 = circuitIndex;
            }

            ++circuitIndex;
        }

        if (junctionToCircuitPair.Item1 != -1 && (junctionToCircuitPair.Item1 == junctionToCircuitPair.Item2))
        {
            // These junctions are already connected
            //--iterations;
            throw new Exception("Tried to connect already connected junctions");
        }
            
        else if (junctionToCircuitPair.Item1 == -1 || junctionToCircuitPair.Item2 == -1)
        {
            // It's a new circuit!
             circuits.Add(new List<int> { junctionPair.ReferenceJunctionIndex, junctionPair.ClosestJunctionIndex });
        }
        else if (((List<int>)circuits[junctionToCircuitPair.Item1]).Count < ((List<int>)circuits[junctionToCircuitPair.Item2]).Count)
        {
            ((List<int>)circuits[junctionToCircuitPair.Item2]).AddRange(((List<int>)circuits[junctionToCircuitPair.Item1]));
            circuits.RemoveAt(junctionToCircuitPair.Item1);
            if (printCircuits)
            {
                PrintCircuits(circuits);
            }
        }
        else
        {
            ((List<int>)circuits[junctionToCircuitPair.Item1]).AddRange(((List<int>)circuits[junctionToCircuitPair.Item2]));
            circuits.RemoveAt(junctionToCircuitPair.Item2);
            if (printCircuits)
            {
                PrintCircuits(circuits);
            }
        }
    }

    public List<List<int>> CreateCircuitsList(long iterations, bool printCircuits = false)
    {
        // Maybe the next algorithm is really:
        //  1. Start with an EMPTY circuit list
        //  2. Find the closest distance pair. Connect them. Remove them from consideration for finding a closest pair.
        //      (but they could still the the closest box to another unconnected box)
        //  3. For every unconnected box, find the closest distance pair.
        List<List<int>> circuits = new List<List<int>>();
        List<int> validJunctionIndices = Enumerable.Range(0, JunctionBoxes.Length).ToList(); 
        
        while (iterations > 0)
        {
            JunctionPair closestPair = FindClosestPair(validJunctionIndices);
            ConnectJunctionPairInCicuits(circuits, closestPair, printCircuits);
            // validJunctionIndices.Remove(closestPair.ReferenceJunctionIndex);
            // validJunctionIndices.Remove(closestPair.ClosestJunctionIndex);
            --iterations;
        }

        return circuits;
    }
    
    public long MultiplyLarestCircuits(List<List<int>> circuits, int numberOfCircuitsToMultiply)
    {
        circuits.Sort(new CircuitListSorter());
        long multiple = 1;
        for (int index = 0; index < numberOfCircuitsToMultiply; ++index)
        {
            multiple *= circuits[index].Count;
        }
        return multiple;
    }
    
    public void Part1()
    {
        var lines = InputReader.GetInputLines("Inputs/Day8Input.txt");
        ParseLines(lines);
    }
    
    public void Part2()
    {
        var lines = InputReader.GetInputLines("Inputs/Day8Input.txt");
        ParseLines(lines);
    }
}