// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Ipc;

/// <summary>
/// Provides a collection of IPC subject strings used for inter-process communication.
/// </summary>
public static class IpcSubjects
{
    /// <summary>
    /// Subject for retrieving an account by its ticket.
    /// </summary>
    public const string AccountByTicket = "boufbowl.ipc.account.byTicket";

    /// <summary>
    /// Subject for retrieving an account by its ID.
    /// </summary>
    public const string AccountById = "boufbowl.ipc.account.byId";

    /// <summary>
    /// Subject for retrieving an account by username and password.
    /// </summary>
    public const string AccountByUsernameAndPassword = "boufbowl.ipc.account.byUsernameAndPassword";

    /// <summary>
    /// Subject for adding a character to an account.
    /// </summary>
    public const string AccountAddCharacter = "boufbowl.ipc.account.addCharacter";

    /// <summary>
    /// Subject for removing a character from an account.
    /// </summary>
    public const string AccountRemoveCharacter = "boufbowl.ipc.account.removeCharacter";

    /// <summary>
    /// Subject for adding a relation to an account.
    /// </summary>
    public const string AccountAddRelation = "boufbowl.ipc.account.addRelation";

    /// <summary>
    /// Subject for removing a relation from an account.
    /// </summary>
    public const string AccountRemoveRelation = "boufbowl.ipc.account.removeRelation";

    /// <summary>
    /// Subject for updating a relation in an account.
    /// </summary>
    public const string AccountUpdateRelation = "boufbowl.ipc.account.updateRelation";

    /// <summary>
    /// Subject for handling heartbeat signals for server keep-alive.
    /// </summary>
    public const string Heartbeat = "boufbowl.ipc.heartbeat";

    /// <summary>
    /// Subject for registering a server.
    /// </summary>
    public const string ServerRegister = "boufbowl.ipc.server.register";

    /// <summary>
    /// Subject for updating the status of a server.
    /// </summary>
    public const string ServerStatusUpdate = "boufbowl.ipc.server.statusUpdate";
}
