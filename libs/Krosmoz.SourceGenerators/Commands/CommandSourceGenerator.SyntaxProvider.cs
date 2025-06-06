// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Collections.Immutable;
using Krosmoz.SourceGenerators.Commands.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Krosmoz.SourceGenerators.Commands;

public sealed partial class CommandSourceGenerator
{
    /// <summary>
    /// Determines whether a given syntax node matches the criteria for processing.
    /// </summary>
    /// <param name="syntaxNode">The syntax node to evaluate.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// True if the syntax node is a class declaration with at least one attribute and contains the CommandGroup attribute; otherwise, false.
    /// </returns>
    private static bool Predicate(SyntaxNode syntaxNode, CancellationToken cancellationToken)
    {
        return !cancellationToken.IsCancellationRequested &&
               syntaxNode is ClassDeclarationSyntax { AttributeLists.Count: > 0 } declarationSyntax &&
               declarationSyntax.AttributeLists
                   .SelectMany(static x => x.Attributes)
                   .Any(static x => x.Name.ToString() is nameof(CommandGroup));
    }

    /// <summary>
    /// Transforms a syntax node into a CommandGroup object by extracting relevant metadata.
    /// </summary>
    /// <param name="context">The context containing the syntax node and semantic model.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A CommandGroup object representing the extracted metadata.</returns>
    private static CommandGroup Transform(GeneratorSyntaxContext context, CancellationToken cancellationToken)
    {
        var symbol = (INamedTypeSymbol)context.SemanticModel.GetDeclaredSymbol(context.Node, cancellationToken)!;

        var commandGroupTypeName = symbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);

        var commandGroupName = symbol
            .GetAttributes()
            .First(static x => x.AttributeClass?.Name is "CommandGroupAttribute")
            .ConstructorArguments[0].Value!.ToString();

        var methods = symbol.GetMembers()
            .OfType<IMethodSymbol>()
            .Where(static x => x.GetAttributes().Any(static y => y.AttributeClass?.Name is "CommandAttribute"))
            .ToArray();

        var commands = ImmutableArray.CreateBuilder<Command>();

        foreach (var method in methods)
        {
            var attributes = method.GetAttributes();

            var commandName = attributes
                .FirstOrDefault(static x => x.AttributeClass?.Name is "CommandAttribute")?
                .ConstructorArguments.FirstOrDefault().Value?.ToString();

            if (commandName is null)
                continue;

            var commandDescription = attributes
                .FirstOrDefault(static x => x.AttributeClass?.Name is "CommandDescriptionAttribute")?
                .ConstructorArguments.FirstOrDefault().Value?.ToString();

            if (commandDescription is null)
                continue;

            var commandMethodName = method.Name;

            var commandCooldownAttribute = attributes.FirstOrDefault(static x => x.AttributeClass?.Name is "CommandCooldownAttribute");

            var commandCooldown = commandCooldownAttribute?.ConstructorArguments switch
            {
                [ { Value: { } v1 } ] => TimeSpan.FromSeconds((int)v1),
                [ { Value: { } v1 }, { Value: { } v2 } ] => new TimeSpan(0, (int)v1, (int)v2),
                [ { Value: { } v1 }, { Value: { } v2 }, { Value: { } v3 } ] => new TimeSpan(0, (int)v1, (int)v2, (int)v3),
                [ { Value: { } v1 }, { Value: { } v2 }, { Value: { } v3 }, { Value: { } v4 } ] => new TimeSpan((int)v1, (int)v2, (int)v3, (int)v4),
                _ => TimeSpan.Zero
            };

            var commandHierarchy = attributes
                .FirstOrDefault(static x => x.AttributeClass?.Name is "CommandHierarchyAttribute")?
                .ConstructorArguments.FirstOrDefault().Value ?? CommandHierarchies.Player;

            var parameters = ImmutableArray.CreateBuilder<CommandParameter>();

            foreach (var parameter in method.Parameters)
            {
                var parameterName = parameter.Name;
                var parameterType = parameter.Type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);

                if (parameterType is "global::Krosmoz.Servers.GameServer.Network.Transport.DofusConnection")
                    continue;

                var isEnum = parameter.Type.TypeKind is TypeKind.Enum;

                var isPrimitive = parameter.Type.SpecialType is
                    SpecialType.System_Byte or
                    SpecialType.System_SByte or
                    SpecialType.System_Int16 or
                    SpecialType.System_UInt16 or
                    SpecialType.System_Int32 or
                    SpecialType.System_UInt32 or
                    SpecialType.System_Int64 or
                    SpecialType.System_UInt64 or
                    SpecialType.System_Single or
                    SpecialType.System_Double or
                    SpecialType.System_Decimal or
                    SpecialType.System_Boolean or
                    SpecialType.System_Char;

                var isStruct = parameter.Type.IsValueType && !isEnum && !isPrimitive;

                parameters.Add(new CommandParameter(parameterType, parameterName, isPrimitive, isEnum, isStruct));
            }

            var help = $"Usage: {commandGroupName} {commandName}{(parameters.Count is 0 ? string.Empty : " ")}{string.Join(" ", parameters.Select(x => $"<{x.Name}>"))}";

            commands.Add(new Command(commandMethodName, commandName, commandDescription, help, (CommandHierarchies)commandHierarchy, commandCooldown, parameters.ToImmutable()));
        }

        return new CommandGroup(commandGroupTypeName, commandGroupName, commands.ToImmutable());
    }
}
