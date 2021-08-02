using System;
using System.Text.Json.Serialization;

namespace Lib.JsonConfiguration
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class JsonAbstractClassConverterAttribute : JsonConverterAttribute
	{
		public JsonAbstractClassConverterAttribute(Type converterType)
			: base(converterType)
		{
		}
	}
}
