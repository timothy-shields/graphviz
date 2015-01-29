[GraphViz]: http://www.graphviz.org/
[Shields.GraphViz]: https://www.nuget.org/packages/Shields.GraphViz

[Shields.GraphViz][] is a .NET wrapper for [GraphViz][]. Easily enhance your applications with graph visualizations.

# Example

```csharp
Graph graph = Graph.Undirected
    .Add(EdgeStatement.For("a", "b"))
    .Add(EdgeStatement.For("a", "c"));
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

![graph.png](http://i.imgur.com/NjlQROO.png)
