using System.IO;

namespace GridLock.records {

    public class RecordInfo {

        private struct PlayerInfo {
            public static string Name = null!;
            public static int Score;
        }

        private static void SetFromCsv(string csvStr) {
            string[] fields = csvStr.Split(';');
            PlayerInfo.Name = fields[0];
            PlayerInfo.Score = int.Parse(fields[1]);
        }

        private static string GetCsvString() {
            return $"{PlayerInfo.Name};{PlayerInfo.Score}";
        }

        public void WriteCsv(string data) {
            var sw = new StreamWriter("../Record.csv");
            sw.WriteLine(GetCsvString());
            sw.Close();
        }

        public void ReadCsv() {
            var sr = new StreamReader("../Record.csv");
            string? data = sr.ReadLine();
            while (data != null) {
                SetFromCsv(data);
                data = sr.ReadLine();
            }

            sr.Close();
        }
    }
}