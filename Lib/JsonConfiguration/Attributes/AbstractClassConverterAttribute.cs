using System;
using System.Text.Json.Serialization;

namespace Lib.JsonConfiguration
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class AbstractClassConverterAttribute : JsonConverterAttribute
	{
		public AbstractClassConverterAttribute(Type converterType)
			: base(converterType)
		{
		}
	}
}
