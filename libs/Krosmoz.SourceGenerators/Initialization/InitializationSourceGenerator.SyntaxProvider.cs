// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.SourceGenerators.Initialization.Generated;
using Krosmoz.SourceGenerators.Initialization.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Krosmoz.SourceGenerators.Initialization;

public sealed partial class InitializationSourceGenerator
{
    /// <summary>
    /// Determines whether the specified syntax node matches the criteria for initialization services.
    /// </summary>
    /// <param name="syntaxNode">The syntax node to evaluate.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// <c>true</c> if the syntax node is a class declaration that implements
    /// <b>IInitializableService</b> or <b>IAsyncInitializableService</b>; otherwise, <c>false</c>.
    /// </returns>
    private static bool Predicate(SyntaxNode syntaxNode, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return syntaxNode is ClassDeclarationSyntax { BaseList: not null } declarationSyntax &&
               declarationSyntax.BaseList.Types.Any(static t => t.Type.ToString() is nameof(IInitializableService) or nameof(IAsyncInitializableService));
    }

    /// <summary>
    /// Transforms a syntax node into an <see cref="InitializableService"/> object.
    /// </summary>
    /// <param name="context">The syntax context containing the node and semantic model.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>An <see cref="InitializableService"/> object representing the transformed syntax node.</returns>
    /// <exception cref="Exception">Thrown if the symbol cannot be retrieved from the syntax node.</exception>
    private static InitializableService Transform(GeneratorSyntaxContext context, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var symbol = (INamedTypeSymbol)context.SemanticModel.GetDeclaredSymbol(context.Node)!;

        var symbolName = symbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);

        var @interface = symbol.Interfaces.First(x => x.Name is nameof(IInitializableService) or nameof(IAsyncInitializableService));

        var isAsync = @interface.Name.Contains("Async");

        return new InitializableService(symbolName, isAsync);
    }
}
