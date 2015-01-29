[GraphViz]: http://www.graphviz.org/
[Shields.GraphViz]: https://www.nuget.org/packages/Shields.GraphViz

[Shields.GraphViz][] is a .NET wrapper for [GraphViz][]. Easily enhance your applications with graph visualizations.

# Example

First define a graph.

```csharp
Graph graph = Graph.Undirected
    .Add(EdgeStatement.For("a", "b"))
    .Add(EdgeStatement.For("a", "c"));
```

Then render the graph to a stream.

```csharp
IRenderer renderer = new Renderer(graphVizBin);
using (Stream file = File.Open("graph.png"))
{
    await renderer.RunAsync(
        graph, file,
        RendererLayouts.Dot,
        RendererFormats.Png,
        CancellationToken.None);
}
```

The output `graph.png` follows.

![graph.png](http://i.imgur.com/NjlQROO.png)
