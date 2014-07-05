using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.CoreLocation;
using Google.Maps;

namespace GoogleMapsSample
{
	public class MapViewController : UIViewController
	{
		MapView mapView;

		public override void LoadView ()
		{
			base.LoadView ();

			CameraPosition camera = CameraPosition.FromCamera (37.797865, -122.402526, -100);

			mapView = MapView.FromCamera (RectangleF.Empty, camera);
			mapView.MyLocationEnabled = true;
			//mapView.MyLocation;
			var xamMarker = new Marker () {
				Title = "Property Pin",
				Snippet = "next best thing since pinterest",
				Position = new CLLocationCoordinate2D (-27.674843,152.921603),
				Map = mapView
			};

			mapView.SelectedMarker = xamMarker;

			View = mapView;
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			mapView.StartRendering ();
		}

		public override void ViewWillDisappear (bool animated)
		{	
			mapView.StopRendering ();
			base.ViewWillDisappear (animated);
		}
	}
}

