using System;
using Newtonsoft.Json;

namespace GoogleMapsSample
{
	[JsonProperty(PropertyName ="GMSAddress")]
	public class Location
	{
		public string AddressLine1{ get; set;}
		public string AddressLine2{ get; set;}
		public string AdministrativeArea{ get; set;}
		public Coordinate Coordinate{ get; set;}
		public string Country{ get; set;}
		public string Locality{ get; set;}
		public int PostalCode{ get; set;}
	}
}

