using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;
using Shields.GraphViz.Components;
using Shields.GraphViz.Services;
using Shields.GraphViz.Models;
using NUnit.Framework;

namespace Shields.GraphViz.Tests
{
    [TestFixture]
    public class RendererTests
    {
        private Lazy<IRenderer> renderer;
        private static string GraphVizBin =
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86),
                "Graphviz2.38",
                "bin");

        private IRenderer Renderer
        {
            get { return renderer.Value; }
        }

        [SetUp]
        public void Initialize()
        {
            renderer = new Lazy<IRenderer>(() => new Renderer(GraphVizBin));
        }

        //[Test]
        public async Task DotProducesCorrectPng()
        {
            var graph = Graph.Undirected
                .Add(EdgeStatement.For("a", "b"))
                .Add(EdgeStatement.For("a", "c"));
            using (var outputStream = new MemoryStream())
            {
                await Renderer.RunAsync(graph, outputStream, RendererLayouts.Dot, RendererFormats.Png, CancellationToken.None);
                var output = outputStream.ToArray();
                var expectedOutput = await ReadAllBytesAsync(
                    this.GetType().GetTypeInfo().Assembly.
                    GetManifestResourceStream("Shields.GraphViz.Tests.Resources.Graph1.png"));
                Assert.IsTrue(output.SequenceEqual(expectedOutput));
            }
        }

        [Test]
        public void CancellationWorks()
        {
            var cts = new CancellationTokenSource();
            cts.Cancel();
            var graph = Graph.Undirected
                .Add(EdgeStatement.For("a", "b"))
                .Add(EdgeStatement.For("a", "c"));
            using (var outputStream = new MemoryStream())
            {
                Assert.ThrowsAsync<TaskCanceledException>(
                    () => Renderer.RunAsync(graph, outputStream, RendererLayouts.Dot, RendererFormats.Png, cts.Token));
            }
        }

        private async Task<byte[]> ReadAllBytesAsync(Stream stream)
        {
            using (var memoryStream = new MemoryStream())
            {
                await stream.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
