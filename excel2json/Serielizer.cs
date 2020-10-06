using System.IO;
using ExcelDataReader;
using System.Text;
using Newtonsoft.Json;
using System.Text.Unicode;

namespace excel2json
{
    public class Serielizer
    {
        private string excelFile;

        public Serielizer(string filename)
        {
            excelFile = filename;
        }

        public MemoryStream Convert()
        {
            var excelwords = new ExcelColumns();

            var memoryStream = new MemoryStream();

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (var inFile = File.Open(excelFile, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(inFile, new ExcelReaderConfiguration() { FallbackEncoding = Encoding.GetEncoding(1252) }))
                using (var streamWriter = new StreamWriter(memoryStream, Encoding.UTF8, 1024, true))
                using (var writer = new JsonTextWriter(streamWriter))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()); // result.Tables[i].TableName
                    writer.Formatting = Formatting.Indented;
                    writer.WriteStartObject();  // first obj
                    int sheetNum = 0;
                    do // for sheets
                    {
                        string sheetName = result.Tables[sheetNum].TableName; // save list name
                        sheetNum++;

                        int Row = reader.RowCount;
                        int Col = reader.FieldCount;

                        writer.WritePropertyName(sheetName);
                        writer.WriteStartArray();
                        for(int i = 0; i < Row; i++)
                        {
                            reader.Read();
                            writer.WriteStartObject();
                            writer.WritePropertyName("List: " + sheetName + " Row " + (i + 1).ToString());

                            writer.WriteStartObject();
                            for(int c = 0; c < reader.FieldCount; c++)
                            {
                                writer.WritePropertyName(excelwords.GetWord(c + 1));
                                
                                if (reader.GetValue(c) != null)
                                {
                                    writer.WriteValue(reader.GetValue(c).ToString());
                                }
                                else
                                {
                                    writer.WriteValue("");
                                }
                                
                            }
                            writer.WriteEndObject();
                            writer.WriteEndObject();

                        }
                        writer.WriteEndArray();


                    } while (reader.NextResult());
                    writer.WriteEndObject(); //first obj
                }
                memoryStream.Seek(0, SeekOrigin.Begin);
                return memoryStream;
            }
        }
    }
}
