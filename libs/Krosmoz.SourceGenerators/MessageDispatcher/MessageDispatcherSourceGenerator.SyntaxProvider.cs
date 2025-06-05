// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Collections.Immutable;
using Krosmoz.SourceGenerators.MessageDispatcher.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Krosmoz.SourceGenerators.MessageDispatcher;

public sealed partial class MessageDispatcherSourceGenerator
{
    /// <summary>
    /// Determines whether a given syntax node matches the criteria for a message handler method.
    /// </summary>
    /// <param name="node">The syntax node to evaluate.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns><c>true</c> if the node matches the criteria; otherwise, <c>false</c>.</returns>
    private static bool Predicate(SyntaxNode node, CancellationToken cancellationToken)
    {
        return !cancellationToken.IsCancellationRequested &&
               node is ClassDeclarationSyntax classDeclaration &&
               classDeclaration.Members
                   .OfType<MethodDeclarationSyntax>()
                   .SelectMany(static m => m.AttributeLists)
                   .SelectMany(static m => m.Attributes)
                   .Any(static m => m.Name.ToString() is nameof(MessageHandler));
    }

    /// <summary>
    /// Transforms a syntax node into a <see cref="MessageHandler"/> model.
    /// </summary>
    /// <param name="context">The syntax context containing the node and semantic model.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="MessageHandler"/> representing the handler method.</returns>
    private static MessageHandler Transform(GeneratorSyntaxContext context, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var symbol = (INamedTypeSymbol)context.SemanticModel.GetDeclaredSymbol(context.Node)!;

        var handlerName = symbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);

        var handlerVariableName = handlerName.Split('.').Last();

        handlerVariableName = char.ToLowerInvariant(handlerVariableName[0]) + handlerVariableName[1..];

        var methods = symbol.GetMembers()
            .OfType<IMethodSymbol>()
            .Where(static m => m.GetAttributes().Any(static a => a.AttributeClass!.Name is "MessageHandlerAttribute"))
            .Select(static m => new MessageHandlerMethod(m.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat), m.Parameters[1].Type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)))
            .ToImmutableArray();

        return new MessageHandler(handlerName, handlerVariableName, methods);
    }
}
