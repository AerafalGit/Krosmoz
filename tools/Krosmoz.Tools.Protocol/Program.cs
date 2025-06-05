using Krosmoz.Tools.Protocol.Converters;
using Krosmoz.Tools.Protocol.Generators;
using Krosmoz.Tools.Protocol.Models;
using Krosmoz.Tools.Protocol.Parsers;
using Krosmoz.Tools.Protocol.Renderers;
using Krosmoz.Tools.Protocol.Storages.Symbols;
using Krosmoz.Core.Extensions;
using Krosmoz.Protocol.Datacenter;
using Krosmoz.Serialization;
using Krosmoz.Serialization.D2I;
using Krosmoz.Serialization.D2O;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

await Host.CreateDefaultBuilder(args)
    .ConfigureLogging(static logging => logging.UseSerilog())
    .ConfigureServices(static services =>
    {
        services
            .AddSingleton(static _ =>
            {
                var d2IFile = new D2IFile();
                d2IFile.Load();

                var d2OFile = new D2OFile(new DatacenterObjectFactory(), d2IFile);

                foreach (var filePath in Directory.EnumerateFiles(PathConstants.Directories.CommonPath).Where(static x => x.EndsWith(".d2o") || x.EndsWith(".d2os")))
                    d2OFile.Load(filePath);

                return d2OFile;
            })
            .AddSingleton<ISymbolStorage, SymbolStorage>()
            .AddSingleton<IConverter<DatacenterSymbol>, DatacenterConverter>()
            .AddSingleton<IRenderer<DatacenterSymbol>, DatacenterRenderer>()
            .AddSingleton<IRenderer<Dictionary<string, Dictionary<int, D2OClass>>>, DatacenterObjectFactoryRenderer>()
            .AddSingleton<IParser<EnumSymbol>, EnumParser>()
            .AddSingleton<IConverter<EnumSymbol>, EnumConverter>()
            .AddSingleton<IRenderer<EnumSymbol>, EnumRenderer>()
            .AddSingleton<IParser<ClassSymbol>, ClassParser>()
            .AddSingleton<IConverter<ClassSymbol>, ClassConverter>()
            .AddSingleton<IRenderer<ClassSymbol>, ClassRenderer>()
            .AddKeyedSingleton<IRenderer<ClassSymbol[]>, TypeFactoryRenderer>(nameof(TypeFactoryRenderer))
            .AddKeyedSingleton<IRenderer<ClassSymbol[]>, MessageFactoryRenderer>(nameof(MessageFactoryRenderer))
            .AddHostedService<DatacenterGenerator>()
            .AddHostedService<DatacenterEnumGenerator>()
            .AddHostedService<ProtocolGenerator>();
    })
    .RunConsoleAsync();
