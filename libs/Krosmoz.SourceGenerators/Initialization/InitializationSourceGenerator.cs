// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Text;
using Krosmoz.SourceGenerators.Initialization.Generated;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Krosmoz.SourceGenerators.Initialization;

/// <summary>
/// Represents a source generator for initialization services.
/// </summary>
[Generator(LanguageNames.CSharp)]
public sealed partial class InitializationSourceGenerator : IIncrementalGenerator
{
    /// <summary>
    /// Initializes the source generator by registering the necessary steps in the generation pipeline.
    /// </summary>
    /// <param name="context">The context for incremental generator initialization.</param>
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(static ctx =>
        {
            ctx.AddSource(IInitializableService.GlobalName, SourceText.From(IInitializableService.Source, Encoding.UTF8));
            ctx.AddSource(IAsyncInitializableService.GlobalName, SourceText.From(IAsyncInitializableService.Source, Encoding.UTF8));
        });

        var syntaxProvider = context.SyntaxProvider.CreateSyntaxProvider(Predicate, Transform).Collect();

        context.RegisterSourceOutput(syntaxProvider, Generate);
    }
}
