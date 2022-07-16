using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Text.Json;
using System.IO;

namespace CollectionUI.Model
{
    public static class FileJson
    {
        private static readonly IFormatProvider formatProvider = CultureInfo.InvariantCulture;
        public static void SaveFileWithTasksXml(this MyTasksCollection tasks, string filePatch) // metoda rozszerzająca
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string strJson = JsonSerializer.Serialize(tasks, options);
                JsonDocument docJson = JsonDocument.Parse(strJson);
                JsonElement root = docJson.RootElement;
                File.WriteAllText("tasks.json", strJson);
            }
            catch (Exception exc)
            {

                throw new Exception("Błąd przy zapisie danych do pliku JSON", exc);
            }
        }

        public static MyTasksCollection LoadFileWithTasks(string filePath)
        {
            try
            {
                string jsonData = File.ReadAllText(filePath);
                List<MyTask> ?tasks = JsonSerializer.Deserialize <List<MyTask>>(jsonData);
                MyTasksCollection MTC = new MyTasksCollection();

                foreach (var item in tasks)
                {
                    MTC.AddTask(item);
                }

                return MTC;
            }
            catch (Exception exc)
            {
                throw new Exception("Błąd przy odczycie danych z pliku JSON", exc);
            }
        }
    }
}
