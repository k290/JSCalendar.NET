using Lib.Models;

namespace Lib.Builders
{
    public abstract class JSCommonBuilder<E, B> where B: JSCommonBuilder<E, B> where E : JSCommon
    {
        protected abstract E _jsCalendarObject { get; set;  }
        public B WithUid(string uid)
        {
            _jsCalendarObject.uid = uid;
            return this as B;
        }
    }
}