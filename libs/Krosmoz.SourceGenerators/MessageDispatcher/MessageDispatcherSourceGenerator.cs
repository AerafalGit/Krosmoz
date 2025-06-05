// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Microsoft.CodeAnalysis;

namespace Krosmoz.SourceGenerators.MessageDispatcher;

/// <summary>
/// A source generator for creating message dispatchers based on annotated methods.
/// </summary>
[Generator(LanguageNames.CSharp)]
public sealed partial class MessageDispatcherSourceGenerator : IIncrementalGenerator
{
    /// <summary>
    /// Initializes the source generator by registering the necessary syntax providers and source outputs.
    /// </summary>
    /// <param name="context">The context for incremental generator initialization.</param>
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var assemblyName = context.CompilationProvider.Select(static (compilation, _) => compilation.Assembly.Name);

        var syntaxProvider = context.SyntaxProvider
            .CreateSyntaxProvider(Predicate, Transform)
            .Collect()
            .Combine(assemblyName);

        context.RegisterSourceOutput(syntaxProvider, static (spc, pair) => Generate(spc, pair.Left, pair.Right));
    }
}
