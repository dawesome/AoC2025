using System.Collections;
using System.Numerics;

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

public class Day8
{
    public Vector3[] junctionBoxes;
    public long[] closestJunctionIndexArray;
    public float[] distanceToClosestJunctionArray;
    public SortedList<float, Tuple<long, long>> distanceToJunctionsList = new();
    

    public void PrintJunctionBoxes()
    {
        foreach (Vector3 vector in junctionBoxes)
        {
            Console.WriteLine(vector);
        }
    }
    
    public void ParseLines(IEnumerable<string> lines)
    {
        junctionBoxes = new Vector3[lines.Count()];
        for (int count = 0; count < lines.Count(); ++count)
        {
            var coordinates = lines.ElementAt(count).Split(',');
            junctionBoxes[count] = new Vector3(long.Parse(coordinates[0]), long.Parse(coordinates[1]), long.Parse(coordinates[2]));
        }
    }

    public long[] CalculateDistancesBetweenJunctionBoxes()
    {
        long closestDistance = long.MaxValue;
        closestJunctionIndexArray = new long[junctionBoxes.Length];
        distanceToClosestJunctionArray = new float[junctionBoxes.Length];
        
        for (long outerIndex = 0; outerIndex < junctionBoxes.Length; ++outerIndex)
        {
            closestDistance = long.MaxValue;
            for (long innerIndex = 0; innerIndex < junctionBoxes.Length; ++innerIndex)
            {
                if (outerIndex == innerIndex)
                {
                    continue;
                }
                Vector3 outerVector = junctionBoxes[outerIndex];
                Vector3 innerVector = junctionBoxes[innerIndex];
                long distance = (long)Vector3.Distance(outerVector, innerVector);
                if (distance < closestDistance)
                {
                    closestDistance = (long)distance;
                    distanceToClosestJunctionArray[outerIndex] = distance;
                    distanceToClosestJunctionArray[innerIndex] = distance;
                    closestJunctionIndexArray[outerIndex] = innerIndex;
                    closestJunctionIndexArray[innerIndex] = outerIndex;
                }
            }

            if (distanceToJunctionsList.ContainsKey(closestDistance))
            {
                // Make sure it's just the same tuple. Really should find a way to skip it but whatever
                if (distanceToJunctionsList[closestDistance].Item1 == outerIndex || distanceToJunctionsList[closestDistance].Item2 == outerIndex)
                {
                    continue;
                }
                else
                {
                    // panic
                    throw new Exception("Duplicate distance");
                }
            }
            distanceToJunctionsList.Add(closestDistance, Tuple.Create(outerIndex, closestJunctionIndexArray[outerIndex]));
        }
        
        return closestJunctionIndexArray;
    }

    public ArrayList ConnectClosestJunctions(long iterations)
    {
        ArrayList circuits = new ArrayList();
        // Start with every junction box in its own circut
        for (long index = 0; index < junctionBoxes.Length; index++)
        {
            circuits.Add(new List<long> { index });
        }
        
        // TODO: start from the last index into distanceToJunctionsList we added something to
        for (int index = 0; index < distanceToJunctionsList.Count && iterations > 0; ++index)
        {
            Tuple<long, long> junctionPair = distanceToJunctionsList.GetValueAtIndex(index);
            Pair junctionToCircuitPair = new Pair(-1, -1);

            int circuitIndex = 0;
            foreach (List<long> circuit in circuits)
            {
                if (circuit.Contains(junctionPair.Item1))
                {
                    junctionToCircuitPair.Item1 = circuitIndex;
                }
                if (circuit.Contains(junctionPair.Item2))
                {
                    junctionToCircuitPair.Item2 = circuitIndex;
                }

                ++circuitIndex;
            }

            if (junctionToCircuitPair.Item1 == -1 || junctionToCircuitPair.Item2 == -1)
            {
                // panic again
                throw new Exception("Junction not in any circut");
            }
            
            if (junctionToCircuitPair.Item1 == junctionToCircuitPair.Item2)
            {
                // These junctions are already connected
                --iterations;
            }
            
            else if (((List<long>)circuits[junctionToCircuitPair.Item1]).Count < ((List<long>)circuits[junctionToCircuitPair.Item2]).Count)
            {
                ((List<long>)circuits[junctionToCircuitPair.Item2]).AddRange(((List<long>)circuits[junctionToCircuitPair.Item1]));
                circuits.RemoveAt(junctionToCircuitPair.Item1);
                --iterations;
            }
            else
            {
                ((List<long>)circuits[junctionToCircuitPair.Item1]).AddRange(((List<long>)circuits[junctionToCircuitPair.Item2]));
                circuits.RemoveAt(junctionToCircuitPair.Item2);
                --iterations;
            }
        }
        return circuits;
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