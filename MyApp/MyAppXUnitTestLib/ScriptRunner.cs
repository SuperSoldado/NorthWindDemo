using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace MyAppXUnitTestLib
{
    public class ScriptRunner
    {
        /// <summary>
        /// Run sql commands
        /// </summary>
        /// <param name="fileNameAndPath"></param>
        /// <param name="connectionString"></param>
        /// <param name="hasError"></param>
        /// <param name="log"></param>
        public void TryRunScriptFileWithMultipleCommands(string fileNameAndPath, string connectionString, out string log)
        {
            log = null;
            try
            {
                string script = File.ReadAllText(fileNameAndPath);
                IEnumerable<string> commandStrings = Regex.Split(script, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    foreach (string commandString in commandStrings)
                    {
                        if (commandString.StartsWith("--Script Version:"))
                        {
                            continue;
                        }
                        if (commandString.Trim() != "")
                        {
                            using (var command = new SqlCommand(commandString, connection))
                            {
                                try
                                {
                                    var result = command.ExecuteNonQuery();
                                }
                                catch (Exception ex)
                                {

                                    throw new Exception("Command failed: " + commandString);
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                log = ex.Message;
            }
        }

        public void TryRunScriptSingleCommand(string commandToRun, string connectionString, out bool hasError, out string log)
        {
            log = null;
            try
            {
                IEnumerable<string> commandStrings = Regex.Split(commandToRun, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    foreach (string commandString in commandStrings)
                    {
                        if (commandString.StartsWith("--Script Version:"))
                        {
                            continue;
                        }
                        if (commandString.Trim() != "")
                        {
                            using (var command = new SqlCommand(commandString, connection))
                            {
                                try
                                {
                                    var result = command.ExecuteNonQuery();
                                }
                                catch (Exception ex)
                                {

                                    throw new Exception("Command failed: " + commandString);
                                }
                            }
                        }
                    }
                }

                hasError = false;
            }
            catch (Exception ex)
            {
                hasError = true;
                log = ex.Message;
            }
        }
    }
}
