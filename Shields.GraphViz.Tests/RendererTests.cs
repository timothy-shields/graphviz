using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shields.GraphViz.Components;
using Shields.GraphViz.Services;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Reflection;
using Shields.GraphViz.Models;

namespace Shields.GraphViz.Tests
{
    [TestClass]
    public class RendererTests
    {
        private Lazy<IRenderer> renderer;
        private const string GraphVizBin = @"C:\Program Files (x86)\Graphviz2.38\bin";

        private IRenderer Renderer
        {
            get { return renderer.Value; }
        }

        [TestInitialize]
        public void Initialize()
        {
            renderer = new Lazy<IRenderer>(() => new Renderer(GraphVizBin));
        }

        //[TestMethod]
        public async Task DotProducesCorrectPng()
        {
            var graph = Graph.Undirected
                .Add(EdgeStatement.For("a", "b"))
                .Add(EdgeStatement.For("a", "c"));
            using (var outputStream = new MemoryStream())
            {
                await Renderer.RunAsync(graph, outputStream, RendererLayouts.Dot, RendererFormats.Png, CancellationToken.None);
                var output = outputStream.ToArray();
                var expectedOutput = await ReadAllBytesAsync(Assembly.GetExecutingAssembly().GetManifestResourceStream("Shields.GraphViz.Tests.Resources.Graph1.png"));
                Assert.IsTrue(output.SequenceEqual(expectedOutput));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(OperationCanceledException))]
        public async Task CancellationWorks()
        {
            var cts = new CancellationTokenSource();
            cts.Cancel();
            var graph = Graph.Undirected
                .Add(EdgeStatement.For("a", "b"))
                .Add(EdgeStatement.For("a", "c"));
            using (var outputStream = new MemoryStream())
            {
                await Renderer.RunAsync(graph, outputStream, RendererLayouts.Dot, RendererFormats.Png, cts.Token);
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
