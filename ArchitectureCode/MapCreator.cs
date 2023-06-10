using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Drawing;

namespace Elementaria
{
    public static class MapCreator
    {
        private static readonly Dictionary<string, Func<ICreature>> factory = new Dictionary<string, Func<ICreature>>();

        public static ICreature[,] CreateMap(string map, string splitter = "\r\n")
        {
            var rows = map.Split(new[] { splitter }, StringSplitOptions.RemoveEmptyEntries);
            var result = new ICreature[rows[0].Length, rows.Length];
            for (var x = 0; x < rows[0].Length; x++)
                for (var y = 0; y < rows.Length; y++)
                    result[x, y] = CreateCreatureBySymbol(rows[y][x]);
            return result;
        }

        private static ICreature CreateCreatureByTypeName(string name)
        {
            if (!factory.ContainsKey(name))
            {
                var type = Assembly
                    .GetExecutingAssembly()
                    .GetTypes()
                    .FirstOrDefault(z => z.Name == name);
                if (type == null)
                    throw new Exception($"Can't find type '{name}'");
                factory[name] = () => (ICreature)Activator.CreateInstance(type);
            }
            return factory[name]();
        }

        private static ICreature CreateCreatureBySymbol(char c)
        {
            switch (c)
            {
                case 'P':
                    return CreateCreatureByTypeName("Player");
                case 'S':
                    return CreateCreatureByTypeName("Stone");
                case 'B':
                    return CreateCreatureByTypeName("Box");
                case 'C':
                    return CreateCreatureByTypeName("Chest");
                case ' ':
                    return null;
                default:
                    throw new Exception($"wrong character for Create");
            }
        }
    }
}