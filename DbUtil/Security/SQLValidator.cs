using System;
using DbUtil.Entities;

namespace DbUtil.Security
{
    public class SQLValidator
    {
        public static bool FieldsValid(QueryParameters parameters)
        {
            var valid = true;

            string[] sqlCheckList = { "--", ";--", ";", "/*", "*/","@@", "@","char","nchar","varchar", "nvarchar","alter","begin", "cast", "create", "cursor","declare", "delete", "drop", "end", "exec", "execute", "fetch", "insert","kill","select","sys","sysobjects","syscolumns","table", "update" };

            //validate the fields to be selected
            for(int i = 0; i < parameters.fields.Length; i++)
            {
                parameters.fields[i] = parameters.fields[i].Replace("'", "''");

                for (int j = 0; j <= sqlCheckList.Length - 1; j++)

                {

                    if ((parameters.fields[i].IndexOf(sqlCheckList[j], StringComparison.OrdinalIgnoreCase) >= 0))
                    {
                        valid = false;
                    }

                }
            }

            //validate the arguments
            for (int i = 0; i < parameters.args.Length; i++)
            {
                parameters.args[i] = parameters.args[i].Replace("'", "''");

                for (int j = 0; j <= sqlCheckList.Length - 1; j++)

                {

                    if ((parameters.args[i].IndexOf(sqlCheckList[j], StringComparison.OrdinalIgnoreCase) >= 0))
                    {
                        valid = false;
                    }

                }
            }

            return valid;
        }
    }
}
