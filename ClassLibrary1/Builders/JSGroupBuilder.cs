using Lib.Models;
using System;
using System.Text.Json;

namespace Lib.Builders
{
    public class JSGroupBuilder
    {
        private JSGroup _jsGroup;

        public JSGroupBuilder()
        {
            _jsGroup = new JSGroup();
        }

 
        public JSGroupBuilder WithEvent(Action<JSEventBuilder> eventBuilderAction)
        {
            var eventBuilder = new JSEventBuilder();
            eventBuilderAction(eventBuilder);
            _jsGroup.AddEntry(eventBuilder.Build());
            return this;
        }

        public JSGroupBuilder WithTask(Action<JSTaskBuilder> taskBuilderAction)
        {
            var taskBuilder = new JSTaskBuilder();
            taskBuilderAction(taskBuilder);
            _jsGroup.AddEntry(taskBuilder.Build());
            return this;
        }

        public JSGroupBuilder WithUid(string uid) //might want to autogenerate in the future and provide a GetUID method
        {
            _jsGroup.uid = uid;
            return this;
        }

        public JSGroup Build()
        {
            return _jsGroup;
        }
    }
}
