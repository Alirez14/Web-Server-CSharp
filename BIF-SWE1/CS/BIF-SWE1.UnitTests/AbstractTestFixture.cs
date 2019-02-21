using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace BIF.SWE1.UnitTests
{
    public abstract class AbstractTestFixture<T>
    {
        private Type _typeToTest;

        #region Setup
        [SetUp]
        public void Setup()
        {
            
        }

        private string _workingDirectory;
        public string WorkingDirectory
        {
            get
            {
                if(_workingDirectory == null)
                {
                    _workingDirectory = TestContext.Parameters["targetpath"] ?? System.Environment.CurrentDirectory;
                }
                return _workingDirectory;
            }
        }

        [OneTimeSetUp]
        public void TestSetup()
        {
            Log("Starting tests in {0}", WorkingDirectory);
            Log("Seaching for a class that implements this interface: {0}", typeof(T).FullName);
            foreach (var file in System.IO.Directory.GetFiles(WorkingDirectory, "*.exe").Concat(System.IO.Directory.GetFiles(WorkingDirectory, "*.dll")))
            {
                Log("Inspecting file {0}", file);
                var assembly = System.Reflection.Assembly.LoadFrom(file);
                Type candidate = null;
                try
                {
                    candidate = assembly.GetTypes().SingleOrDefault(t => t.GetInterfaces().Any(i => i.FullName == typeof(T).FullName));
                }
                catch (System.Reflection.ReflectionTypeLoadException rtlex)
                {
                    Log("ReflectionTypeLoadException while inspecting file: {0}", rtlex.Message);
                    foreach (var le in rtlex.LoaderExceptions)
                    {
                        Log("  {0}", le.Message);
                    }
                }
                catch (Exception ex)
                {
                    Log("WARNING while inspecting file: {0}", ex.Message);
                }
                if (candidate != null)
                {
                    this._typeToTest = candidate;
                    Log("Found a type to test: {0}", candidate);
                    break;
                }
            }

            if (_typeToTest == null)
            {
                throw new InvalidOperationException(string.Format("Unable to find a type that implements {0}", typeof(T).FullName));
            }
        }
        #endregion

        #region Support
        protected T CreateInstance(params object[] parameter)
        {
            Log("Creating instance of type {0} with {1} parameter", _typeToTest.FullName, parameter != null ? parameter.Length : 0);
            return (T)Activator.CreateInstance(_typeToTest, parameter);
        }

        protected void Log(string format, params object[] parameter)
        {
            System.Diagnostics.Trace.WriteLine(string.Format(format, parameter));
            if (System.Console.Out != null)
            {
                System.Console.WriteLine(format, parameter);
            }
        }
        #endregion
    }
}
