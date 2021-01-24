using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace MyAppXUnitTestLib
{
    public class DabaseReset
    {
        /// <summary>
        /// Kill other users before drop DB
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public string KillUsers(string connectionString)
        {
            ScriptRunner scriptRunner = new ScriptRunner();
            string mycommand = "USE [Northwind] EXEC[dbo].[sp_KillSpidsByDBName]@dbname = Northwind; ";
            string dbName = "";
            bool hasError = false;
            string errorInExecution = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                dbName = connection.Database;
            }

            scriptRunner.TryRunScriptSingleCommand(mycommand, connectionString, out hasError, out errorInExecution);

            return errorInExecution;
        }

        public string ResetDatabank(string connectionString, string dbScriptFile)
        {
            string error = null;
            KillUsers(connectionString);

            try
            {
                string errorInExecution = null;
                ScriptRunner scriptRunner = new ScriptRunner();

                if (File.Exists(dbScriptFile))
                {
                    scriptRunner.TryRunScriptFileWithMultipleCommands(dbScriptFile, connectionString, out errorInExecution);
                    if (errorInExecution != null)
                    {
                        error = errorInExecution;
                    }
                }
                else
                {
                    error = "File not found: " + dbScriptFile;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return error;
        }
    }
}
