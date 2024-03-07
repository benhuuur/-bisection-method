using System;
using System.Collections.Generic;
using AulasAI.Collections;
using AulasAI.Search;

// var graph = BuildGraph();
// Console.WriteLine(graph.Nodes[1].Value);
// Console.WriteLine(graph.Nodes[5].Value);
//
// var startNode = new SearchGraphNode<string>(graph.Nodes[1]);
// var endNode = new SearchGraphNode<string>(graph.Nodes[5]);
//
// var found = Search.BFS(startNode, endNode.Node.Value);
// Console.WriteLine(found ? "Goal found!" : "Goal not found.");

var graph = BuildWeightedGraph();
Console.WriteLine(graph.Nodes[1].Value);
Console.WriteLine(graph.Nodes[5].Value);

return;

Graph<string, GraphNode<string>> BuildGraph()
{
    var nodeBaltimore = new GraphNode<string>("Baltimore");
    var nodeBoston = new GraphNode<string>("Boston");
    var nodeBuffalo = new GraphNode<string>("Buffalo");
    var nodeChicago = new GraphNode<string>("Chicago");
    var nodeCleveland = new GraphNode<string>("Cleveland");
    var nodeColumbus = new GraphNode<string>("Columbus");
    var nodeDetroit = new GraphNode<string>("Detroit");
    var nodeIndianapolis = new GraphNode<string>("Indianapolis");
    var nodeNewYork = new GraphNode<string>("New York");
    var nodePhiladelphia = new GraphNode<string>("Philadelphia");
    var nodePittsburgh = new GraphNode<string>("Pittsburgh");
    var nodePortland = new GraphNode<string>("Portland");
    var nodeProvidence = new GraphNode<string>("Providence");
    var nodeSyracuse = new GraphNode<string>("Syracuse");

    nodeBaltimore.AddNode(nodePhiladelphia)
                 .AddNode(nodePittsburgh);

    nodeBoston.AddNode(nodeNewYork)
              .AddNode(nodePortland)
              .AddNode(nodeProvidence)
              .AddNode(nodeSyracuse);

    nodeBuffalo.AddNode(nodeCleveland)
               .AddNode(nodeDetroit)
               .AddNode(nodePittsburgh)
               .AddNode(nodeSyracuse);

    nodeChicago.AddNode(nodeCleveland)
               .AddNode(nodeDetroit)
               .AddNode(nodeIndianapolis);

    nodeCleveland.AddNode(nodeBuffalo)
                 .AddNode(nodeChicago)
                 .AddNode(nodeColumbus)
                 .AddNode(nodeDetroit)
                 .AddNode(nodePittsburgh);

    nodeColumbus.AddNode(nodeCleveland)
                .AddNode(nodeIndianapolis)
                .AddNode(nodePittsburgh);

    nodeDetroit.AddNode(nodeBuffalo)
               .AddNode(nodeChicago)
               .AddNode(nodeCleveland);

    nodeIndianapolis.AddNode(nodeChicago)
                    .AddNode(nodeColumbus);

    nodeNewYork.AddNode(nodeBoston)
               .AddNode(nodePhiladelphia)
               .AddNode(nodeProvidence)
               .AddNode(nodeSyracuse);

    nodePhiladelphia.AddNode(nodeBaltimore)
                    .AddNode(nodeNewYork)
                    .AddNode(nodeSyracuse);

    nodePittsburgh.AddNode(nodeBaltimore)
                  .AddNode(nodeBuffalo)
                  .AddNode(nodeCleveland)
                  .AddNode(nodeColumbus)
                  .AddNode(nodePhiladelphia);

    nodePortland.AddNode(nodeBoston);

    nodeProvidence.AddNode(nodeBoston)
                  .AddNode(nodeNewYork);

    nodeSyracuse.AddNode(nodeBoston)
                .AddNode(nodeBuffalo)
                .AddNode(nodeNewYork)
                .AddNode(nodePhiladelphia);

    var buildGraph = new Graph<string, GraphNode<string>>(new List<GraphNode<string>>
    {
        nodeBaltimore,
        nodeBoston,
        nodeBuffalo,
        nodeChicago,
        nodeCleveland,
        nodeColumbus,
        nodeDetroit,
        nodeIndianapolis,
        nodeNewYork,
        nodePhiladelphia,
        nodePittsburgh,
        nodePortland,
        nodeProvidence,
        nodeSyracuse,
    });

    return buildGraph;
}

Graph<string, WeightedNode<string>> BuildWeightedGraph()
{
    var nodeBaltimore = new WeightedNode<string>("Baltimore");
    var nodeBoston = new WeightedNode<string>("Boston");
    var nodeBuffalo = new WeightedNode<string>("Buffalo");
    var nodeChicago = new WeightedNode<string>("Chicago");
    var nodeCleveland = new WeightedNode<string>("Cleveland");
    var nodeColumbus = new WeightedNode<string>("Columbus");
    var nodeDetroit = new WeightedNode<string>("Detroit");
    var nodeIndianapolis = new WeightedNode<string>("Indianapolis");
    var nodeNewYork = new WeightedNode<string>("New York");
    var nodePhiladelphia = new WeightedNode<string>("Philadelphia");
    var nodePittsburgh = new WeightedNode<string>("Pittsburgh");
    var nodePortland = new WeightedNode<string>("Portland");
    var nodeProvidence = new WeightedNode<string>("Providence");
    var nodeSyracuse = new WeightedNode<string>("Syracuse");

    nodeBaltimore.AddNode(nodePhiladelphia, 101)
                 .AddNode(nodePittsburgh, 247);
    
    nodeBoston.AddNode(nodeNewYork, 215)
              .AddNode(nodePortland, 107)
              .AddNode(nodeProvidence, 50)
              .AddNode(nodeSyracuse, 312);
    
    nodeBuffalo.AddNode(nodeCleveland, 189)
               .AddNode(nodeDetroit, 256)
               .AddNode(nodePittsburgh, 215)
               .AddNode(nodeSyracuse, 150);
    
    nodeChicago.AddNode(nodeCleveland, 345)
               .AddNode(nodeDetroit, 283)
               .AddNode(nodeIndianapolis, 182);
    
    nodeCleveland.AddNode(nodeBuffalo, 189)
                 .AddNode(nodeChicago, 345)
                 .AddNode(nodeColumbus, 144)
                 .AddNode(nodeDetroit, 169)
                 .AddNode(nodePittsburgh, 134);
    
    nodeColumbus.AddNode(nodeCleveland, 144)
                .AddNode(nodeIndianapolis, 176)
                .AddNode(nodePittsburgh, 185);
    
    nodeDetroit.AddNode(nodeBuffalo, 256)
               .AddNode(nodeChicago, 283)
               .AddNode(nodeCleveland, 169);
    
    nodeIndianapolis.AddNode(nodeChicago, 182)
                    .AddNode(nodeColumbus, 176);
    
    nodeNewYork.AddNode(nodeBoston, 215)
               .AddNode(nodePhiladelphia, 97)
               .AddNode(nodeProvidence, 181)
               .AddNode(nodeSyracuse, 254);
    
    nodePhiladelphia.AddNode(nodeBaltimore, 101)
                    .AddNode(nodeNewYork, 97)
                    .AddNode(nodeSyracuse, 253);
    
    nodePittsburgh.AddNode(nodeBaltimore, 247)
                  .AddNode(nodeBuffalo, 215)
                  .AddNode(nodeCleveland, 134)
                  .AddNode(nodeColumbus, 185)
                  .AddNode(nodePhiladelphia, 305);
    
    nodePortland.AddNode(nodeBoston, 107);
    
    nodeProvidence.AddNode(nodeBoston, 50)
                  .AddNode(nodeNewYork, 181);
    
    nodeSyracuse.AddNode(nodeBoston, 312)
                .AddNode(nodeBuffalo, 150)
                .AddNode(nodeNewYork, 254)
                .AddNode(nodePhiladelphia, 253);

    var buildWeightedGraph = new Graph<string, WeightedNode<string>>(new List<WeightedNode<string>>
    {
        nodeBaltimore,
        nodeBoston,
        nodeBuffalo,
        nodeChicago,
        nodeCleveland,
        nodeColumbus,
        nodeDetroit,
        nodeIndianapolis,
        nodeNewYork,
        nodePhiladelphia,
        nodePittsburgh,
        nodePortland,
        nodeProvidence,
        nodeSyracuse,
    });

    return buildWeightedGraph;
}