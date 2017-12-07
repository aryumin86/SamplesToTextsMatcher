using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace SamplesToTextsMatcher
{
    /// <summary>
    /// Abstract morfological dictionary.
    /// </summary>
    public abstract class AbstractMorfDictionary
    {
        /// <summary>
        /// Connection string.
        /// </summary>
        protected string _conn;

        /// <summary>
        /// Sql table with dictionary.
        /// </summary>
        protected string _table;

        public AbstractMorfDictionary()
        {
            IConfigurationRoot Configuration;
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            _conn = Configuration["main_conn"];
            _table = Configuration["morf_table"];
        }

        public abstract List<string> GetSyns(string word, int max = Int32.MaxValue);
        public abstract string[,] GetSyns(string[] words, int max = int.MaxValue);
    }
}
