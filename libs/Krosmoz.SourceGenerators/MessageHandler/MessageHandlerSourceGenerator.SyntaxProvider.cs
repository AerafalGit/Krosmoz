// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using Krosmoz.SourceGenerators.MessageHandler.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Krosmoz.SourceGenerators.MessageHandler;

public sealed partial class MessageHandlerSourceGenerator
{
    /// <summary>
    /// Determines whether the specified syntax node represents a class declaration
    /// that inherits from either <c>AuthMessageHandler&lt;T&gt;</c> or <c>GameMessageHandler&lt;T&gt;</c>.
    /// </summary>
    /// <param name="syntaxNode">The syntax node to evaluate.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// <c>true</c> if the syntax node is a class declaration inheriting from the specified base types; otherwise, <c>false</c>.
    /// </returns>
    private static bool Predicate(SyntaxNode syntaxNode, CancellationToken cancellationToken)
    {
        if (syntaxNode is not ClassDeclarationSyntax classDeclarationSyntax)
            return false;

        if (classDeclarationSyntax.BaseList is null)
            return false;

        return classDeclarationSyntax
            .BaseList
            .Types
            .Select(x => x.Type)
            .Any(x => x.ToString().StartsWith("AuthMessageHandler<") || x.ToString().StartsWith("GameMessageHandler<"));
    }

    /// <summary>
    /// Transforms a syntax node into a <see cref="MessageHandlerSymbol"/> by extracting the handler's fully qualified name
    /// and the associated message type name.
    /// </summary>
    /// <param name="context">The syntax context containing the node and semantic model.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="MessageHandlerSymbol"/> representing the handler and its associated message type.</returns>
    /// <exception cref="Exception">Thrown if the symbol cannot be retrieved from the syntax node.</exception>
    private static MessageHandlerSymbol Transform(GeneratorSyntaxContext context, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (context.SemanticModel.GetDeclaredSymbol(context.Node) is not INamedTypeSymbol symbol)
            throw new Exception("Unable to get symbol from syntax node.");

        var symbolName = symbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        var messageTypeName = symbol.BaseType!.TypeArguments[0].ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);

        return new MessageHandlerSymbol(symbolName, messageTypeName);
    }
}
