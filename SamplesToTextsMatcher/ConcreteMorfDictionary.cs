using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace SamplesToTextsMatcher
{
    public class ConcreteMorfDictionary : AbstractMorfDictionary
    {
        public override List<string> GetSyns(string word, int max = int.MaxValue)
        {
            HashSet<string> res = new HashSet<string>();

            using (var cn = new SqlConnection(_conn))
            using(SqlCommand cmd = new SqlCommand("select Raw from "+ _table + 
                " where NormalFormId = (select NormalFormId from " + _table + " where Raw = '" + word.ToLower() + "')", cn))
            {
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        res.Add(reader.GetString(0));
                    }
                }
                else
                {
                    res.Add(word);
                }

                reader.Close();
            }

            return new List<string>(res);
        }


        public override string[,] GetSyns(string[] words, int max = int.MaxValue)
        {
            throw new NotImplementedException();
        }
    }
}
