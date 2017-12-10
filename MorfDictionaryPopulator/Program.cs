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

            Console.WriteLine("Type YES to populate dict");
            string confirm = Console.ReadLine();
            if(confirm == "YES")
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

                writeWordsToDb(part, conn, tableName);
                        part.Clear();
                        count = 0;
            }

            //Console.WriteLine("started joining simalar forms with one normal form id...");

            //joining similar words forms with same normal form id
            //using (var fileStream = File.OpenRead(fileName))
            //using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            //{
            //    String line;
            //    List<KeyValuePair<int, int>> joints = new List<KeyValuePair<int, int>>();

            //    while ((line = streamReader.ReadLine()) != null)
            //    {
            //        if (!line.Trim().StartsWith("<link id"))
            //            continue;

            //        XElement elem = XElement.Parse(line);
            //        int from = int.Parse((string)elem.Attribute("from").Value);
            //        int to = int.Parse((string)elem.Attribute("to").Value);

            //        joints.Add(new KeyValuePair<int, int>(from, to));
            //    }
                
            //    SetSameNormalFormIdForSimalarWordsForms(conn, tableName, joints);
            //}
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

        static void SetSameNormalFormIdForSimalarWordsForms(string conn, string tabName, List<int[]> joints)
        {
            //sql is incorrect. need another table for joints
            string sql = string.Format("update {0} set NormalFormId = @from where Id in " +
                "(select Id from {0} where NormalFormId = @to)", tabName);

            using (SqlConnection connection = new SqlConnection(conn))
            using (SqlCommand cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.Add("@from", SqlDbType.Int);
                cmd.Parameters.Add("@to", SqlDbType.Int);
                cmd.Parameters.Add("@type", SqlDbType.Int);

                connection.Open();

                foreach(var j in joints)
                {
                    cmd.Parameters["@from"].Value = j[0];
                    cmd.Parameters["@to"].Value = j[1];
                    cmd.Parameters["@type"].Value = j[2];

                    cmd.ExecuteNonQuery();
                }

                connection.Close();
            }
        }
    }
}
