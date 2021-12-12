public class Day12
{
    class Node
    {
        public string Value { get; set; }

        public List<Node> Connections { get; set; }

        public bool Big { get; set; }

        public Node(string value)
        {
            Value = value;
            Connections = new List<Node>();
            if (value.ToUpperInvariant().Equals(Value))
            {
                Big = true;
            }
            else
            {
                Big = false;
            }
        }

        public Dictionary<string, Node> GetPossibleNextSteps(HashSet<string> visited)
        {
            var possibleNextSteps = new Dictionary<string, Node>();
            foreach (var connection in Connections)
            {
                if (!visited.Contains(connection.Value))
                {
                    possibleNextSteps.Add(connection.Value, connection);
                }
            }
            return possibleNextSteps;
        }

        public long Explore(HashSet<string> visitedNodes)
        {
            var possibleSteps = GetPossibleNextSteps(visitedNodes);
            if (possibleSteps.Count == 0)
            {
                return 0;
            }
            long total = 0;
            if (possibleSteps.ContainsKey("end"))
            {
                possibleSteps.Remove("end");
                total++;
            }
            foreach (var node in possibleSteps.Values)
            {
                var newVisited = new HashSet<string>(visitedNodes);
                if (!node.Big)
                {
                    newVisited.Add(node.Value);
                }
                total += node.Explore(newVisited);
            }
            return total;
        }
    }

    private static Dictionary<string, Node> ReadInputFile(string filename)
    {
        Dictionary<string, Node> nodes = new Dictionary<string, Node>();
        using System.IO.StreamReader file = new System.IO.StreamReader(filename);
        string? line;
        while ((line = file.ReadLine()) != null)
        {
            string[] nodesIds = line.Split('-');
            var nodeValueStart = nodesIds[0];
            var nodeValueEnd = nodesIds[1];
            if (!nodes.TryGetValue(nodeValueStart, out Node nodeStart))
            {
                nodeStart = new Node(nodeValueStart);
                nodes.Add(nodeValueStart, nodeStart);
            }
            if (!nodes.TryGetValue(nodeValueEnd, out Node nodeEnd))
            {
                nodeEnd = new Node(nodeValueEnd);
                nodes.Add(nodeValueEnd, nodeEnd);
            }
            nodeStart.Connections.Add(nodeEnd);
            nodeEnd.Connections.Add(nodeStart);
        }
        return nodes;
    }

    public static long Sol1()
    {
        var nodesById = ReadInputFile("Inputs/input12.txt");
        Node startNode = nodesById["start"];
        var visitedNodes = new HashSet<string>();
        visitedNodes.Add(startNode.Value);
        return startNode.Explore(visitedNodes);
    }

    public static long Sol2()
    {
        return 0;
    }


}

