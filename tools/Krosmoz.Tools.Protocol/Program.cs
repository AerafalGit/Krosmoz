﻿using Krosmoz.Core.Extensions;
using Krosmoz.Protocol.Datacenter;
using Krosmoz.Serialization.D2O.Abstractions;
using Krosmoz.Servers.GameServer.Database.Repositories.Datacenter;
using Krosmoz.Tools.Protocol.Converters;
using Krosmoz.Tools.Protocol.Generators;
using Krosmoz.Tools.Protocol.Models;
using Krosmoz.Tools.Protocol.Parsers;
using Krosmoz.Tools.Protocol.Renderers;
using Krosmoz.Tools.Protocol.Storages.Symbols;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

await Host.CreateDefaultBuilder(args)
    .UseSerilogLogging()
    .ConfigureServices(static services =>
    {
        services
            .AddSingleton<IDatacenterObjectFactory, DatacenterObjectFactory>()
            .AddSingleton<IDatacenterRepository, DatacenterRepository>()
            .AddSingleton<ISymbolStorage, SymbolStorage>()
            .AddSingleton<IParser<EnumSymbol>, EnumParser>()
            .AddSingleton<IConverter<EnumSymbol>, EnumConverter>()
            .AddSingleton<IRenderer<EnumSymbol>, EnumRenderer>()
            .AddSingleton<IParser<ClassSymbol>, ClassParser>()
            .AddSingleton<IConverter<ClassSymbol>, ClassConverter>()
            .AddSingleton<IRenderer<ClassSymbol>, ClassRenderer>()
            .AddKeyedSingleton<IRenderer<ClassSymbol[]>, MessageFactoryRenderer>(nameof(MessageFactoryRenderer))
            .AddKeyedSingleton<IRenderer<ClassSymbol[]>, TypeFactoryRenderer>(nameof(TypeFactoryRenderer))
            .AddKeyedSingleton<IRenderer<DatacenterSymbol>, DatacenterRenderer>(nameof(DatacenterRenderer))
            .AddKeyedSingleton<IRenderer<DatacenterSymbol>, DatacenterObjectFactoryRenderer>(nameof(DatacenterObjectFactoryRenderer))
            .AddHostedService<DatacenterGenerator>()
            .AddHostedService<DatacenterEnumGenerator>()
            .AddHostedService<ProtocolGenerator>();
    })
    .RunConsoleAsync();
