using Krosmoz.Core.Extensions;
using Krosmoz.Core.Network.Factory;
using Krosmoz.Core.Network.Framing;
using Krosmoz.Core.Network.Protocol.Dofus;
using Krosmoz.Core.Network.Transport;
using Krosmoz.Protocol.Messages;
using Krosmoz.Servers.AuthServer.Database;
using Krosmoz.Servers.AuthServer.Database.Repositories.Accounts;
using Krosmoz.Servers.AuthServer.Extensions;
using Krosmoz.Servers.AuthServer.Network.Transport;
using Krosmoz.Servers.AuthServer.Services.Connection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

await Host.CreateDefaultBuilder(args)
    .UseSerilogLogging()
    .ConfigureServices(static (context, services) =>
    {
        services
            .Configure<TcpServerOptions>(static options =>
            {
                options.Host = "127.0.0.1";
                options.Port = 446;
                options.MaxConnections = 100;
                options.MaxConnectionsPerIp = 8;
            })
            .AddDbContext<AuthDbContext>(context.Configuration.GetConnectionString("Auth"))
            .AddTransient<IMessageDecoder<DofusMessage>, DofusMessageDecoder>()
            .AddTransient<IMessageEncoder<DofusMessage>, DofusMessageEncoder>()
            .AddSingleton<IMessageFactory<DofusMessage>, MessageFactory>()
            .AddSingleton<IAccountRepository, AccountRepository>()
            .AddSingleton<IConnectionService, ConnectionService>()
            .AddHostedServiceAsSingleton<AuthServer>()
            .AddMessageHandlers();
    })
    .RunConsoleAsync();
