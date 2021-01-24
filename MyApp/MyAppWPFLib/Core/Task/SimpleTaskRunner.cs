using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Threading.Tasks;
using System.Threading;

namespace MyAppWPFLib.Core.Task
{
    public interface ISimpleTaskRunnerParams
    {
        public void Run(int iterationNumber);
        public int NumberOfIterations { get; set; }
        public ProgressBar ProgressBar { get; set; }

    }

    /// <summary>
    /// Base class to inherit and override "Run" method. Used as parameter to "SimpleTaskRunner"
    /// </summary>
    public class SimpleTaskRunnerParams : ISimpleTaskRunnerParams
    {
        public int NumberOfIterations { get; set; }
        public ProgressBar ProgressBar { get; set; }
        public virtual void Run(int iterationNumber)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Objective: 
    /// 1) run simple tasks 
    /// 2)Notify mvvm progress bar
    /// </summary>
    public class SimpleTaskRunner
    {
        /// <summary>
        /// Run classes that inherit from "SimpleTaskRunnerParams"
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="simpleTaskRunnerParams"></param>
        /// <returns></returns>
        public async System.Threading.Tasks.Task Run<T>(ISimpleTaskRunnerParams simpleTaskRunnerParams)
        {
            string error = null;

            var progress = new Progress<int>(value => simpleTaskRunnerParams.ProgressBar.Value = value);
            await System.Threading.Tasks.Task.Run(() =>
            {
                int percentage = 0;
                for (int i = 0; i < simpleTaskRunnerParams.NumberOfIterations; i++)
                {
                    percentage = (i * 100 / simpleTaskRunnerParams.NumberOfIterations);
                    ((IProgress<int>)progress).Report(percentage);
                    simpleTaskRunnerParams.Run(i);//run the code
                    //trackSimulator.AddRandomSimulation(out error);
                    if (error != null)
                    {
                        break;
                    }
                    Thread.Sleep(1);
                }
            });
        }

        /// <summary>
        /// Run simulation for testing:
        /// simpleTaskRunnerParams simpleTaskRunnerParams = new SimpleTaskRunnerParams();
        /// simpleTaskRunnerParams.ProgressBar = progressBar;
        /// simpleTaskRunnerParams.NumberOfIterations = 100;
        /// SimpleTaskRunner simpleTaskRunner = new SimpleTaskRunner();
        /// await simpleTaskRunner.Run<Task>(simpleTaskRunnerParams);
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="simpleTaskRunnerParams"></param>
        /// <returns></returns>
        public async System.Threading.Tasks.Task RunSimpleTest<T>(SimpleTaskRunnerParams simpleTaskRunnerParams)
        {
            string error = null;

            var progress = new Progress<int>(value => simpleTaskRunnerParams.ProgressBar.Value = value);
            await System.Threading.Tasks.Task.Run(() =>
            {
                int percentage = 0;
                for (int i = 0; i < simpleTaskRunnerParams.NumberOfIterations; i++)
                {
                    percentage = (i * 100 / simpleTaskRunnerParams.NumberOfIterations);
                    ((IProgress<int>)progress).Report(percentage);
                    //trackSimulator.AddRandomSimulation(out error);
                    if (error != null)
                    {
                        break;
                    }
                    Thread.Sleep(100);
                }
            });
        }
    }
}
