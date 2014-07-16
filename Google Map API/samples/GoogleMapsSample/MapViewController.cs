using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.CoreLocation;
using Google.Maps;
using System.Net;
using System.IO;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;


namespace GoogleMapsSample
{
    public class MapViewController : UIViewController
    {
        private MapView _mapView;

        public override void LoadView()
        {
            base.LoadView();

            var camera = CameraPosition.FromCamera(-33.868, 151.2086, 17);
            _mapView = MapView.FromCamera(RectangleF.Empty, camera);
            _mapView.MyLocationEnabled = true;

            _mapView.LongPress += HandleLongPress;

            _mapView.Settings.MyLocationButton = true; ;
            _mapView.Settings.CompassButton = true;
            View = _mapView;
        }

        // TODO: Map coloring will be added in info window instead
        void HandleGMSTappedMarker(MapView mapView, Marker marker)
        {
            // TODO : Handle tapped marker here
            // Curently these changes color only
            if (marker.Icon == UIImage.FromBundle("pink-marker"))
            {
                marker.Icon = UIImage.FromBundle("green-marker");
            }
            else if (marker.Icon == UIImage.FromBundle("blue-marker"))
            {
                marker.Icon = UIImage.FromBundle("pink-marker");
            }
            else
                marker.Icon = UIImage.FromBundle("blue-marker");
        }


        void HandleLongPress(object sender, GMSCoordEventArgs eventArg)
        {
            var mapView = sender as MapView;
            GMSAddress address = null;
            new Geocoder().ReverseGeocodeCord(eventArg.Coordinate, (response, error) =>
                {
                    if (response.Results != null)
                    {
                        var result = response.FirstResult as Google.Maps.Address;
                        var serializedCoord = Newtonsoft.Json.JsonConvert.SerializeObject(result.Coordinate);
                        address = new GMSAddress() 
                                { 
                                    Lines = result.Lines, 
                                    Locality = result.Locality, 
                                    Country = result.Country, 
                                    Coordinate = Newtonsoft.Json.JsonConvert.DeserializeObject<Coordinate>(serializedCoord) 
                                };

                        // Serialize/ deserialize codes
                        //var serializedAddress = Newtonsoft.Json.JsonConvert.SerializeObject(address);
                        //GMSAddress add = Newtonsoft.Json.JsonConvert.DeserializeObject<GMSAddress>(serializedAddress);

                        if (address != null)
                        {
                            string infoAddress = "";
                            for (int i = 0; i < address.Lines.Length; i++)
                            {
                                infoAddress += address.Lines[i] + ", ";
                            }

                            var xamMarker = new Marker()
                            {
                                Title = infoAddress,
                                Snippet = address.Country,
                                Position = eventArg.Coordinate,
                                Icon = UIImage.FromBundle("pink-marker"),
                                Map = mapView,
                                AppearAnimation = MarkerAnimation.Pop,
                                Draggable = true,
                            };

                            xamMarker.Tappable = true;
                            mapView.SelectedMarker = xamMarker;
                        }
                    }
                }
            );
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
        }
    }
}

