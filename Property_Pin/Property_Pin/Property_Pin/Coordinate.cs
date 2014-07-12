using System;
using Newtonsoft.Json;

namespace GoogleMapsSample
{
	public class Location
	{
		[JsonProperty(PropertyName = "lat")]
		public double Latitude{ get; set;}

		[JsonProperty(PropertyName = "lng")]
		public double Longitude{ get; set;} 
	}
}

