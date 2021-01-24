using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace MyAppDB
{
    /// <summary>
    /// only for csv files
    /// </summary>
    public class CVSParams
    {
        /// <summary>
        /// if true, create sql files
        /// </summary>
        public bool GenerateSQLFileAndDontRunCVS { get; set; }

        /// <summary>
        /// if true, trea as cvs
        /// </summary>
        public bool IsCVS { get; set; }
        public string CVSTableName { get; set; }
    }

    public class ScriptRunnerParams
    {
        public bool IncludeDBMessages { get; set; }

        public CVSParams CVSParams { get; set; }
    }

    public class ScriptRunner
    {
        public async Task Test<T>(ObservableCollection<T> SqlScriptLog, string propertyName)
        {
            for (int i = 0; i < 10; i++)
            {
                T newObject = (T)Activator.CreateInstance(typeof(T));
                Type t = typeof(T);
                await Task.Delay(1000);
                var targetProperty = t.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (targetProperty == null)
                {
                    continue;
                }

                targetProperty.SetValue(newObject, i.ToString(), null);
                SqlScriptLog.Add(newObject);
            }
        }

        private async Task<T> GetLog<T>(string propertyName, string loggMessage)
        {
            T newObject = (T)Activator.CreateInstance(typeof(T));
            Type t = typeof(T);
            var targetProperty = t.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            targetProperty.SetValue(newObject, loggMessage.ToString(), null);
            return await Task.FromResult<T>(newObject);
            //return Task<T> targetProperty;
        }

        private void myConnection_InfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            DbOutput = e.Message;
        }

        private string DbOutput { get; set; }

        public async Task TryRunScriptFileWithMultipleCommandsAsync<T>(bool includeDBMessages, string fileNameAndPath, string connectionString, ObservableCollection<T> SqlScriptLog, string propertyName)
        {
            try
            {
                string script = File.ReadAllText(fileNameAndPath);
                IEnumerable<string> commandStrings = Regex.Split(script, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    if (includeDBMessages)
                    {
                        connection.InfoMessage += new SqlInfoMessageEventHandler(myConnection_InfoMessage);
                    }

                    SqlScriptLog.Insert(0, await GetLog<T>(propertyName, "Try Open Connection"));
                    connection.Open();
                    SqlScriptLog.Insert(0, await GetLog<T>(propertyName, "Connection opened"));
                    int commandNumber = 0;

                    //optimization: if list contains 1000, update the list for each 100 itens.
                    int LogCommandReleaseNumber = 1;
                    if (commandStrings.Count() > 1000)
                    {
                        LogCommandReleaseNumber = 100;
                    }

                    int LogCommandReleaseNumberCounter = 0;
                    foreach (string commandString in commandStrings)
                    {
                        LogCommandReleaseNumberCounter++;
                        commandNumber++;
                        if (commandString.StartsWith("--Script Version:"))
                        {
                            SqlScriptLog.Insert(0, await GetLog<T>(propertyName, commandString));
                            continue;
                        }
                        if (commandString.Trim() != "")
                        {
                            using (var command = new SqlCommand(commandString, connection))
                            {
                                SqlScriptLog.Insert(0, await GetLog<T>(propertyName, commandNumber + " Try to run..."));
                                try
                                {
                                    DbOutput = null;
                                    var result = command.ExecuteNonQuery();
                                    if ((includeDBMessages) && (DbOutput != null))
                                    {
                                        SqlScriptLog.Insert(0, await GetLog<T>(propertyName, DbOutput));
                                    }
                                }
                                catch (Exception ex)
                                {
                                    SqlScriptLog.Insert(0, await GetLog<T>(propertyName, commandNumber + " Failed. Aborting script."));
                                    SqlScriptLog.Insert(0, await GetLog<T>(propertyName, commandNumber + "Command is: " + commandString));
                                    SqlScriptLog.Insert(0, await GetLog<T>(propertyName, commandNumber + "Error: " + ex.Message));
                                    break;
                                }
                                SqlScriptLog.Insert(0, await GetLog<T>(propertyName, commandNumber + " OK"));

                                //if counter reaches it's limit, update the screen
                                if (LogCommandReleaseNumberCounter == LogCommandReleaseNumber)
                                {
                                    LogCommandReleaseNumberCounter = 0;

                                    int itensToMaintainInList = 50;
                                    if (SqlScriptLog.Count >= itensToMaintainInList)
                                    {
                                        int itensToRemove = SqlScriptLog.Count;
                                        if (itensToRemove > itensToMaintainInList)
                                        {
                                            //removing dump from log. 
                                            itensToRemove = itensToRemove - itensToMaintainInList;
                                        }

                                        for (int i = 0; i < itensToRemove; i++)
                                        {
                                            SqlScriptLog.RemoveAt(SqlScriptLog.Count - 1);
                                        }
                                    }

                                    await Task.Delay(1);
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                var log = await GetLog<T>(propertyName, ex.Message);
                SqlScriptLog.Insert(0, log);
            }
            SqlScriptLog.Insert(0, await GetLog<T>(propertyName, "Finished without errors. You may have a cofee"));
        }

        public async Task TryRunScriptFileWithMultipleCommandsAsyncV2<T>(ScriptRunnerParams scriptRunnerParams, string fileNameAndPath, string connectionString, ObservableCollection<T> SqlScriptLog, string propertyName)
        {
            try
            {
                //Clean data from log to optimize data
                bool trimmData = true;
                string script = File.ReadAllText(fileNameAndPath);
                IEnumerable<string> commandStrings = Regex.Split(script, Environment.NewLine, RegexOptions.Multiline | RegexOptions.IgnoreCase);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    if (scriptRunnerParams.IncludeDBMessages)
                    {
                        connection.InfoMessage += new SqlInfoMessageEventHandler(myConnection_InfoMessage);
                    }

                    SqlScriptLog.Insert(0, await GetLog<T>(propertyName, "Try Open Connection"));
                    connection.Open();
                    SqlScriptLog.Insert(0, await GetLog<T>(propertyName, "Connection opened"));
                    int commandNumber = 0;
                    string cvsColumns = null;

                    List<string> sqlInsertCommandsFromCSV = new List<string>();
                    //optimization: if list contains 1000, update the list for each 100 itens.
                    int LogCommandReleaseNumber = 1;
                    if (commandStrings.Count() > 1000)
                    {
                        LogCommandReleaseNumber = 100;
                    }

                    int LogCommandReleaseNumberCounter = 0;
                    foreach (string commandString in commandStrings)
                    {
                        LogCommandReleaseNumberCounter++;
                        commandNumber++;

                        if (commandNumber == 1)
                        {
                            if (scriptRunnerParams.CVSParams.IsCVS)
                            {
                                cvsColumns = commandString.Replace("\"", "'");
                                continue;
                            }
                        }

                        if (commandString.StartsWith("--Script Version:"))
                        {
                            SqlScriptLog.Insert(0, await GetLog<T>(propertyName, commandString));
                            continue;
                        }
                        if (commandString.Trim() != "")
                        {
                            string commandToRun = null;
                            string cvsTo_InsertInto = null;
                            if (scriptRunnerParams.CVSParams.IsCVS)
                            {
                                string cvsData = commandString.Replace("'", "").Replace("\"", "'");
                                cvsTo_InsertInto = "insert into " + scriptRunnerParams.CVSParams.CVSTableName + " values (" + cvsData + ")";
                                commandToRun = cvsTo_InsertInto;
                            }
                            else
                            {
                                commandToRun = commandString;
                            }

                            if (scriptRunnerParams.CVSParams.GenerateSQLFileAndDontRunCVS)
                            {
                                commandToRun += Environment.NewLine + " GO ";
                                sqlInsertCommandsFromCSV.Add(commandToRun);
                                SqlScriptLog.Insert(0, await GetLog<T>(propertyName, commandNumber + " Generating..."));
                            }
                            else
                            {
                                using (var command = new SqlCommand(commandToRun, connection))
                                {
                                    SqlScriptLog.Insert(0, await GetLog<T>(propertyName, commandNumber + " Try to run..."));
                                    try
                                    {
                                        DbOutput = null;
                                        var result = command.ExecuteNonQuery();
                                        if ((scriptRunnerParams.IncludeDBMessages) && (DbOutput != null))
                                        {
                                            SqlScriptLog.Insert(0, await GetLog<T>(propertyName, DbOutput));
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        SqlScriptLog.Insert(0, await GetLog<T>(propertyName, commandNumber + " Failed. Aborting script."));
                                        SqlScriptLog.Insert(0, await GetLog<T>(propertyName, commandNumber + "Command is: " + commandString));
                                        SqlScriptLog.Insert(0, await GetLog<T>(propertyName, commandNumber + "Error: " + ex.Message));
                                        break;
                                    }
                                    SqlScriptLog.Insert(0, await GetLog<T>(propertyName, commandNumber + " OK"));
                                }
                            }
                            //if counter reaches it's limit, update the screen
                            if (LogCommandReleaseNumberCounter == LogCommandReleaseNumber)
                            {
                                LogCommandReleaseNumberCounter = 0;

                                int itensToMaintainInList = 20;
                                if (SqlScriptLog.Count >= itensToMaintainInList)
                                {
                                    int itensToRemove = SqlScriptLog.Count;
                                    if (itensToRemove > itensToMaintainInList)
                                    {
                                        //removing dump from log. 
                                        itensToRemove = itensToRemove - itensToMaintainInList;
                                    }

                                    for (int i = 0; i < itensToRemove; i++)
                                    {
                                        SqlScriptLog.RemoveAt(SqlScriptLog.Count - 1);
                                    }
                                }

                                await Task.Delay(1);
                            }
                        }

                    }
                    if (scriptRunnerParams.CVSParams.GenerateSQLFileAndDontRunCVS)
                    {
                        System.IO.File.WriteAllLines(fileNameAndPath + ".sql", sqlInsertCommandsFromCSV);
                    }
                }

            }
            catch (Exception ex)
            {
                var log = await GetLog<T>(propertyName, ex.Message);
                SqlScriptLog.Insert(0, log);
            }
            SqlScriptLog.Insert(0, await GetLog<T>(propertyName, "Finished without errors. You may have a cofee"));
        }

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
