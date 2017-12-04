using SamplesToTextsMatcher.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Xml.Linq;
using System.Linq;
using System.Data;

namespace MorfDictionaryPopulator
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfigurationRoot Configuration;
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            string fileName = Configuration["dict_file"];
            string conn = Configuration["main_conn"];
            string tableName = Configuration["morf_table"];

            WriteRusDictToDb(fileName, conn, tableName);

            Console.WriteLine("Done...");
            Console.ReadLine();
        }

        static void WriteRusDictToDb(string fileName, string conn, string tableName)
        {
            List<TheWord> part = new List<TheWord>(10000);

            const Int32 BufferSize = 128;            
            using (var fileStream = File.OpenRead(fileName))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                String line;
                int count = 0;
                while ((line = streamReader.ReadLine()) != null)
                {
                    if (!line.Trim().StartsWith("<lemma id"))
                        continue;

                    count++;

                    XElement elem = XElement.Parse(line);

                    TheWord lemma = new TheWord()
                    {
                        Lang = SamplesToTextsMatcher.Enums.Lang.RUS,
                        Raw = (string)elem.Element("l").Attribute("t").Value,
                        IsNormalForm = true,
                        NormalFormId = int.Parse((string)elem.Attribute("id").Value)
                    };

                    part.Add(lemma);

                    var ff = elem.Elements("f").Select(e => e.Attribute("t").Value).Distinct();

                    foreach(var f in ff)
                    {
                        if (f == lemma.Raw)
                            continue;

                        part.Add(new TheWord()
                        {
                            Raw = f,
                            IsNormalForm = false,
                            Lang = SamplesToTextsMatcher.Enums.Lang.RUS,
                            NormalFormId = lemma.NormalFormId
                        });
                    }

                    if(count >= 10000)
                    {
                        writeWordsToDb(part, conn, tableName);
                        part.Clear();
                        count = 0;
                    }
                }
            }

        }


        static void writeWordsToDb(List<TheWord> words, string conn, string tabName)
        {
            string saveStaff = "INSERT into " + tabName + " (NormalFormId,Raw,Lang,IsNormalForm) VALUES (@NormalFormId,@Raw,@Lang,@IsNormalForm)";

            using (SqlConnection connection = new SqlConnection(conn))
            using (SqlCommand cmd = new SqlCommand(saveStaff, connection))
            {
                cmd.Parameters.Add("@NormalFormId", SqlDbType.Int);
                cmd.Parameters.Add("@Raw", SqlDbType.NVarChar);
                cmd.Parameters.Add("@Lang", SqlDbType.SmallInt);
                cmd.Parameters.Add("@IsNormalForm", SqlDbType.Bit);

                connection.Open();

                foreach (var w in words)
                {
                    cmd.Parameters["@NormalFormId"].Value = w.NormalFormId;
                    cmd.Parameters["@Raw"].Value = w.Raw;
                    cmd.Parameters["@Lang"].Value = w.Lang;
                    cmd.Parameters["@IsNormalForm"].Value = w.IsNormalForm;

                    cmd.ExecuteNonQuery();
                }

                Console.WriteLine(DateTime.Now + " Written to db " + words.Count + " words...");
                connection.Close();
            }
        }
    }
}
