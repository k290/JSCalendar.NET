using Models;
using System;
using System.Text.Json;

namespace Builder
{
    public class JSTaskBuilder
    {
        private JSTask _jsTask;

        public JSTaskBuilder()
        {
            _jsTask = new JSTask();
        }


  
        internal JSTask Build()
        {
            return _jsTask;
        }
    }
}
