using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NASA.Explorer.Console.Utils;
using NASA.Explorer.Domain.Aggregate.Mesh;
using NASA.Explorer.Domain.Aggregate.Probe;
using Serilog;
using static System.Console;

namespace NASA.Explorer.Console
{
    public class Worker : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public Worker(IConfiguration configuration, ILogger logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
                try
                {
                    var fileInput = await File.ReadAllLinesAsync(_configuration["EntryFilePath"], stoppingToken);

                    _logger.Information("GETTING INSTRUCTIONS FROM {@FileFrom}", _configuration["EntryFilePath"]);

                    var input = new Instructions(fileInput.ToList());
                    var landMash = input.GetLandMesh();
                    var spaceProbeInstructions = input.GetInstructions();

                    foreach (var instruction in spaceProbeInstructions)
                    {
                        var initialPoint = instruction[0].Split(" ");

                        var spaceProbe =
                            new SpaceProbe(new Point(initialPoint[0].ConvertToInt(), initialPoint[1].ConvertToInt()),
                                initialPoint[2].ConvertToEnum<Orientation>());

                        var movimentsInstructions = instruction.LastOrDefault().ToCharArray();

                        foreach (var moviment in movimentsInstructions)
                            if (moviment == 'M')
                                spaceProbe.TryMove(landMash);
                            else
                                spaceProbe.Rotate(moviment.ToString().ConvertToEnum<Direction>());

                        WriteLine(
                            $"{spaceProbe.Position.X} {spaceProbe.Position.Y} {spaceProbe.Orientation.ToString()}");
                    }

                    _logger.Information("SCAN FINISHED, KEEPING IN LAND MESH {@LandMesh}", landMash);

                    ReadKey();
                }
                catch (Exception e)
                {
                    _logger.Error(e, e.Message);
                }
        }
    }
}