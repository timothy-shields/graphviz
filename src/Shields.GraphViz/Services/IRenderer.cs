using Shields.GraphViz.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shields.GraphViz.Services
{
    /// <summary>
    /// Provides access to GraphViz applications.
    /// </summary>
    public interface IRenderer
    {
        /// <summary>
        /// Runs GraphViz to render a graph.
        /// </summary>
        /// <param name="graph">The graph to render.</param>
        /// <param name="outputStream">The output is written to the output stream using the specified output format.</param>
        /// <param name="layout">The layout algorithm to use.</param>
        /// <param name="format">The output format.</param>
        /// <param name="cancellationToken">The cancellation token for the asynchronous operation.</param>
        /// <returns>The task that signals completion.</returns>
        Task RunAsync(Graph graph, Stream outputStream, RendererLayouts layout, RendererFormats format, CancellationToken cancellationToken);
    }
}
