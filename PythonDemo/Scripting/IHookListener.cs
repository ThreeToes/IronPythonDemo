using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PythonDemo.Scripting
{
    /// <summary>
    /// Basic plugin primitive
    /// </summary>
    public interface IHookListener
    {
        void Callback(object payload);
    }
}
