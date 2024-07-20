using GridLock.application;
using Newtonsoft.Json;

namespace GridLock.data {

    public class LevelParser {

        public static Field Parse(string levelDataJson) {
            return JsonConvert.DeserializeObject<Field>(levelDataJson)!;
        }
    }
}