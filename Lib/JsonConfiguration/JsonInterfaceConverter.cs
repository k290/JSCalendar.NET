using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Lib.JsonConfiguration
{

    //https://github.com/dotnet/runtime/issues/33112#issuecomment-594382795
    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = false)]
	public class JsonInterfaceConverterAttribute : JsonConverterAttribute
	{
		public JsonInterfaceConverterAttribute(Type converterType)
			: base(converterType)
		{
		}
	}
}
