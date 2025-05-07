using Krosmoz.Core.Extensions;
using Krosmoz.Servers.GameServer.Database.Repositories.Datacenter;
using Krosmoz.Tools.Protocol.Converters;
using Krosmoz.Tools.Protocol.Generators;
using Krosmoz.Tools.Protocol.Models;
using Krosmoz.Tools.Protocol.Parsers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

await Host.CreateDefaultBuilder(args)
    .UseSerilogLogging()
    .ConfigureServices(static services =>
    {
        services
            .AddSingleton<IDatacenterRepository, DatacenterRepository>()
            .AddSingleton<IParser<EnumSymbol>, EnumParser>()
            .AddSingleton<IConverter<EnumSymbol>, EnumConverter>()
            .AddHostedService<ProtocolGenerator>();
    })
    .RunConsoleAsync();
