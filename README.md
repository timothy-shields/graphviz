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
using (Stream file = File.Create("graph.png"))
{
    await renderer.RunAsync(
        graph, file,
        RendererLayouts.Dot,
        RendererFormats.Png,
        CancellationToken.None);
}
```

> The location of your GraphViz installation, which is typically something like `C:\Program Files\Graphviz\bin`, should be supplied as the value of `graphVizBin`.

The output `graph.png` follows.

![graph.png](http://i.imgur.com/NjlQROO.png)

# Features

- A fluent API for defining graphs
- Graphs and their components are immutable
- Ready for dependency injection via the `IRenderer` interface
