// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Microsoft.CodeAnalysis;

namespace Krosmoz.SourceGenerators.Commands;

/// <summary>
/// A source generator that generates command-related code.
/// </summary>
[Generator(LanguageNames.CSharp)]
public sealed partial class CommandSourceGenerator : IIncrementalGenerator
{
    /// <summary>
    /// Initializes the source generator by registering the syntax provider and source output.
    /// </summary>
    /// <param name="context">The context for incremental generator initialization.</param>
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var syntaxProvider = context.SyntaxProvider.CreateSyntaxProvider(Predicate, Transform).Collect();

        context.RegisterSourceOutput(syntaxProvider, Generate);
    }
}
