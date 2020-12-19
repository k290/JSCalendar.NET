using Lib.Models;
using System;
using System.Text.Json;

namespace Lib.Builders
{
    public class JSTaskBuilder
    {
        private JSTask _jsTask;

        public JSTaskBuilder()
        {
            _jsTask = new JSTask();
        }

        public JSTaskBuilder WithUid(string uid) //might want to autogenerate in the future and provide a GetUID method
        {
            _jsTask.uid = uid;
            return this;
        }


        internal JSTask Build()
        {
            return _jsTask;
        }
    }
}
