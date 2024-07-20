using GridLock.application;

using System.IO;
using System.Reflection;

namespace GridLock.data {

    public class LevelDataService {

        public static Field LoadLevel(int levelNumber) {
            var currentAssembly = Assembly.GetExecutingAssembly();
            string resourceName = $"GridLock.levels.{levelNumber}.json";

            using Stream? stream = currentAssembly.GetManifestResourceStream(resourceName);
            using var reader = new StreamReader(stream!);
            string result = reader.ReadToEnd();

            var parser = new LevelParser();
            return LevelParser.Parse(result);
        }
    }
}