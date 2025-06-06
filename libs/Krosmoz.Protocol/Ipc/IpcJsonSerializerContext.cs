// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

using System.Text.Json;
using System.Text.Json.Serialization;
using Krosmoz.Core.IO.Text.Json;
using Krosmoz.Protocol.Enums;
using Krosmoz.Protocol.Enums.Custom;
using Krosmoz.Protocol.Ipc.Messages.Accounts;
using Krosmoz.Protocol.Ipc.Messages.Heartbeat;
using Krosmoz.Protocol.Ipc.Messages.Servers;

namespace Krosmoz.Protocol.Ipc;

/// <summary>
/// A JSON serializer context for IPC (Inter-Process Communication) messages.
/// This context is used to generate source code for serializing and deserializing
/// specific IPC message types.
/// </summary>
[JsonSerializable(typeof(IpcAccountByIdRequest))]
[JsonSerializable(typeof(IpcAccountByIdResponse))]
[JsonSerializable(typeof(IpcAccountByTicketRequest))]
[JsonSerializable(typeof(IpcAccountByTicketResponse))]
[JsonSerializable(typeof(IpcAccountByUsernameAndPasswordRequest))]
[JsonSerializable(typeof(IpcAccountByUsernameAndPasswordResponse))]
[JsonSerializable(typeof(IpcAccountAddCharacterRequest))]
[JsonSerializable(typeof(IpcAccountAddCharacterResponse))]
[JsonSerializable(typeof(IpcAccountRemoveCharacterRequest))]
[JsonSerializable(typeof(IpcAccountRemoveCharacterResponse))]
[JsonSerializable(typeof(IpcAccountAddRelationRequest))]
[JsonSerializable(typeof(IpcAccountAddRelationResponse))]
[JsonSerializable(typeof(IpcAccountRemoveRelationRequest))]
[JsonSerializable(typeof(IpcAccountRemoveRelationResponse))]
[JsonSerializable(typeof(IpcAccountUpdateRelationRequest))]
[JsonSerializable(typeof(IpcAccountUpdateRelationResponse))]
[JsonSerializable(typeof(IpcHeartbeatRequest))]
[JsonSerializable(typeof(IpcServerRegisterRequest))]
[JsonSerializable(typeof(IpcServerRegisterResponse))]
[JsonSerializable(typeof(IpcServerStatusUpdateEvent))]
public sealed partial class IpcJsonSerializerContext : JsonSerializerContext
{
    private static JsonSerializerOptions? s_options;

    /// <summary>
    /// Gets an instance of <see cref="IpcJsonSerializerContext"/> configured with custom converters.
    /// </summary>
    public static IpcJsonSerializerContext WithCustomConverters
    {
        get
        {
            if (s_options is null)
            {
                var options = new JsonSerializerOptions();
                options.Converters.Add(new IpAddressConverter());
                options.Converters.Add(new PhysicalAddressConverter());
                options.Converters.Add(new JsonStringEnumConverter<GameHierarchies>());
                options.Converters.Add(new JsonStringEnumConverter<SocialRelationTypeIds>());
                options.Converters.Add(new JsonStringEnumConverter<ServerStatuses>());
                s_options = options;
            }

            return new IpcJsonSerializerContext(s_options);
        }
    }
}
