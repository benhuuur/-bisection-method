using System.Runtime.CompilerServices;

namespace MazeAI;

public class Solver
{
    public int Option { get; set; }
    public Maze Maze { get; set; } = null!;

    public string Algorithm
    {
        get
        {
            return (Option % 4) switch
            {
                0 => "DFS",
                1 => "BFS",
                2 => "dijkstra",
                _ => "aStar"
            };
        }
    }

    public void Solve()
    {
        var goal = Maze.Spaces.FirstOrDefault(s => s.Exit);

        if (Maze.Root is null || goal is null)
            return;

        switch (Option % 4)
        {
            case 0:
                DFS(Maze.Root, goal);
                break;
            case 1:
                BFS(Maze.Root, goal);
                break;
            case 2:
                Dijkstra(Maze.Root, goal);
                break;
            case 3:
                AStar(Maze.Root, goal);
                break;
        }
    }

    private static bool DFS(Space space, Space goal)
    {
        if (space.Visited || space is null)
            return false;

        space.Visited = true;

        if (space == goal)
        {
            space.IsSolution = true;
            return true;
        }

        var currNeighbours = new List<Space>();
        foreach (var neighbour in space.Neighbours())
        {
            if (neighbour is null)
                continue;
            currNeighbours.Add(neighbour);
        }

        var clause = currNeighbours.Any(neighbour => !neighbour.Visited && DFS(neighbour, goal));
        if (clause)
            space.IsSolution = true;
        return clause;
    }

    private static bool BFS(Space start, Space goal)
    {
        Dictionary<Space, Space> prev = new();
        Queue<Space> queue = new();
        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            var currNode = queue.Dequeue();

            if (currNode.Visited)
                continue;

            currNode.Visited = true;

            if (currNode == goal)
                break;

            foreach (var neighbour in currNode.Neighbours())
            {
                if (neighbour is null)
                    continue;
                if (!prev.ContainsKey(neighbour))
                    prev[neighbour] = currNode;
                queue.Enqueue(neighbour);
            }
        }

        var attempt = goal;
        while (attempt != start)
        {
            if (!prev.ContainsKey(attempt))
                return false;
            attempt.IsSolution = true;
            attempt = prev[attempt];
        }

        attempt.IsSolution = true;
        return true;
    }

    private static bool Dijkstra(Space start, Space goal)
    {
        var queue = new PriorityQueue<Space, float>();
        var dist = new Dictionary<Space, float>();
        var prev = new Dictionary<Space, Space>();

        queue.Enqueue(start, 0.0f);
        dist[start] = 0.0f;

        while (queue.Count > 0)
        {
            var currNode = queue.Dequeue();
            currNode.Visited = true;

            if (currNode == goal)
                break;

            foreach (var neighbour in currNode.Neighbours())
            {
                if (neighbour is null)
                    continue;

                var newWeight = dist[currNode] + 1;
                if (!dist.ContainsKey(neighbour))
                {
                    dist[neighbour] = float.PositiveInfinity;
                    prev[neighbour] = null!;
                }
                if (newWeight < dist[neighbour])
                {
                    dist[neighbour] = newWeight;
                    prev[neighbour] = currNode;
                    queue.Enqueue(neighbour, newWeight);
                }
            }
        }

        var attempt = goal;
        while (attempt != start)
        {
            if (!prev.ContainsKey(attempt))
                return false;
            attempt.IsSolution = true;
            attempt = prev[attempt];
        }

        attempt.IsSolution = true;
        return true;
    }

    private static bool AStar(Space start, Space goal)
    {
        var queue = new PriorityQueue<Space, float>();
        var dist = new Dictionary<Space, float>();
        var prev = new Dictionary<Space, Space>();

        queue.Enqueue(start, 0.0f);
        dist[start] = 0.0f;

        while (queue.Count > 0)
        {
            var currNode = queue.Dequeue();
            currNode.Visited = true;

            if (currNode == goal)
                break;

            foreach (var neighbour in currNode.Neighbours())
            {
                if (neighbour is null)
                    continue;

                if (!dist.ContainsKey(neighbour))
                {
                    dist[neighbour] = float.PositiveInfinity;
                    prev[neighbour] = null!;
                }

                var newWeight = dist[currNode] + 1;

                if (newWeight < dist[neighbour])
                {
                    var dx = neighbour.X - goal.X;
                    var dy = neighbour.Y - goal.Y;

                    var penalty = MathF.Sqrt(dx * dx + dy * dy);

                    dist[neighbour] = newWeight;
                    prev[neighbour] = currNode;
                    queue.Enqueue(neighbour, newWeight + penalty);
                }
            }
        }

        var attempt = goal;
        while (attempt != start)
        {
            if (!prev.ContainsKey(attempt))
                return false;
            attempt.IsSolution = true;
            attempt = prev[attempt];
        }

        attempt.IsSolution = true;
        return true;
    }
}
