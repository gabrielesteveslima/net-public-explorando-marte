using System;
using System.Collections.Generic;
using System.Linq;
using NASA.Explorer.Domain.Aggregate.Mesh;

namespace NASA.Explorer.Console
{
    public class Instructions
    {
        public Instructions(List<string> inputArray)
        {
            InputArray = inputArray;
        }

        public List<string> InputArray { get; }

        public LandMesh GetLandMesh()
        {
            const int landMeshDefinitionIndex = 0;
            const int pointXIndex = 0;
            const int pointYIndex = 1;

            return new LandMesh(Convert.ToInt32(InputArray[landMeshDefinitionIndex].Split(" ")[pointXIndex]),
                Convert.ToInt32(InputArray[landMeshDefinitionIndex].Split(" ")[pointYIndex]));
        }

        public List<List<string>> GetInstructions()
        {
            return InputArray.Skip(1)
                .Select((x, i) => new {Index = i, Value = x})
                .GroupBy(x => x.Index / 2)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }
    }
}