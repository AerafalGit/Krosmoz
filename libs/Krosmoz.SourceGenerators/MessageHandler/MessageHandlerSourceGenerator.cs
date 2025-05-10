// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Microsoft.CodeAnalysis;

namespace Krosmoz.SourceGenerators.MessageHandler;

/// <summary>
/// Represents a source generator for creating message handler-related code.
/// </summary>
/// <remarks>
/// This generator is implemented as an incremental generator and is designed to work with C# code.
/// </remarks>
[Generator(LanguageNames.CSharp)]
public sealed partial class MessageHandlerSourceGenerator : IIncrementalGenerator
{
    /// <summary>
    /// Initializes the incremental source generator by setting up the syntax provider and
    /// registering the source output generation logic.
    /// </summary>
    /// <param name="context">
    /// The <see cref="IncrementalGeneratorInitializationContext"/> used to configure the generator.
    /// </param>
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var isAuthAssembly = context.CompilationProvider
            .Select(static (compilation, cancellationToken) =>
            {
                cancellationToken.ThrowIfCancellationRequested();
                return compilation.Assembly.Name is "Krosmoz.Servers.AuthServer";
            });

        var syntaxProvider = context.SyntaxProvider
            .CreateSyntaxProvider(Predicate, Transform)
            .Collect()
            .Combine(isAuthAssembly);

        context.RegisterSourceOutput(syntaxProvider, static (spc, pair) => Generate(spc, pair.Left, pair.Right));
    }
}
