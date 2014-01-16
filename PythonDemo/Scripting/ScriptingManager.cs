using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using IronPython.Hosting;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;

namespace PythonDemo.Scripting
{
    class ScriptingManager
    {
        private readonly ScriptEngine _engine;
        private readonly CallbackManager _callbackManager;

        public ScriptingManager(CallbackManager manager)
        {
            _callbackManager = manager;
            _engine = Python.CreateEngine();
            var searchPaths = new List<string>(_engine.GetSearchPaths());
            searchPaths.Add(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PythonLib"));
            _engine.SetSearchPaths(searchPaths);
            _engine.Runtime.Globals.SetVariable("callbackManager", _callbackManager);
        }

        public void RunScript(string script)
        {
            var scope = _engine.CreateScope();
            var source = _engine.CreateScriptSourceFromString(script, SourceCodeKind.Statements);
            var compiled = source.Compile();
            try
            {
                var result = compiled.Execute(scope);
            }catch(Exception e)
            {
                MessageBox.Show("Script failed to run: " + e.Message);
            }
        }

        public void RunFile(string path)
        {
            var script = File.ReadAllText(path);
            RunScript(script);
        }
    }
}
