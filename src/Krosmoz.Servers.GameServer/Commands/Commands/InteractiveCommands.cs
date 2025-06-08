// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Drawing;
using Krosmoz.Protocol.Enums;
using Krosmoz.Protocol.Messages.Debug;
using Krosmoz.Servers.GameServer.Commands.Attributes;
using Krosmoz.Servers.GameServer.Network.Transport;
using Krosmoz.Servers.GameServer.Services.Chat;

namespace Krosmoz.Servers.GameServer.Commands.Commands;

/// <summary>
/// Represents a group of commands related to interactives.
/// </summary>
[CommandGroup("interactives")]
public sealed class InteractiveCommands
{
    /// <summary>
    /// Predefined set of colors used for highlighting interactives on the map.
    /// </summary>
    private static readonly Color[] s_colors = new[]
    {
        0xFFFF0000, 0xFF00FF00, 0xFF0000FF, 0xFFFFFF00, 0xFFFF00FF,
        0xFF00FFFF, 0xFF000000,
        0xFF800000, 0xFF008000, 0xFF000080, 0xFF808000, 0xFF800080,
        0xFF008080, 0xFF808080,
        0xFFC00000, 0xFF00C000, 0xFF0000C0, 0xFFC0C000, 0xFFC000C0,
        0xFF00C0C0, 0xFFC0C0C0,
        0xFF400000, 0xFF004000, 0xFF000040, 0xFF404000, 0xFF400040,
        0xFF004040, 0xFF404040,
        0xFF200000, 0xFF002000, 0xFF000020, 0xFF202000, 0xFF200020,
        0xFF002020, 0xFF202020,
        0xFF600000, 0xFF006000, 0xFF000060, 0xFF606000, 0xFF600060,
        0xFF006060, 0xFF606060,
        0xFFA00000, 0xFF00A000, 0xFF0000A0, 0xFFA0A000, 0xFFA000A0,
        0xFF00A0A0, 0xFFA0A0A0,
        0xFFE00000, 0xFF00E000, 0xFF0000E0, 0xFFE0E000, 0xFFE000E0,
        0xFF00E0E0, 0xFFE0E0E0,
    }.Select(static x => Color.FromArgb((int)x)).ToArray();

    private readonly IChatService _chatService;

    /// <summary>
    /// Initializes a new instance of the <see cref="InteractiveCommands"/> class.
    /// </summary>
    /// <param name="chatService">The chat service used for sending messages to players.</param>
    public InteractiveCommands(IChatService chatService)
    {
        _chatService = chatService;
    }

    /// <summary>
    /// Displays the interactives present on the map by highlighting their cells and sending details to the chat.
    /// </summary>
    /// <param name="connection">The connection associated with the player.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [Command("show")]
    [CommandDescription("Display the interactives present on the map.")]
    [CommandHierarchy(GameHierarchies.Moderator)]
    public async Task ShowInteractivesAsync(DofusConnection connection)
    {
        var colorIndex = 0;

        foreach (var interactive in connection.Heroes.Master.Map.GetInteractives())
        {
            var color = s_colors[colorIndex];

            await connection.SendAsync(new DebugHighlightCellsMessage { Color = color.ToArgb(), Cells = [(ushort)interactive.CellId] });

            await _chatService.SendServerMessageAsync(
                connection,
                "<font color=\"#{0:X}\">Id: {1} - CellId: {2} - ElementId: {3} - Gfx: {4}</font>",
                color.ToArgb(),
                interactive.ElementId,
                interactive.CellId,
                interactive.ElementSkillId,
                interactive.GfxId);

            colorIndex = (colorIndex + 1) % s_colors.Length;
        }
    }
}
