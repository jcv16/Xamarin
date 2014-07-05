using System;
using MonoTouch.UIKit;
using System.Drawing;
using Google.Maps;

namespace GoogleMapsAdvSample
{
	public class BasicMapViewController : UIViewController
	{
		public BasicMapViewController () : base ()
		{
			Title = "Basic Map";
		}

		private bool _isZoomedPositionOkToDropPin;
		private const int PINABLE_ZOOM_POSITION = 19;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			var camera = CameraPosition.FromCamera (-33.868, 151.2086, 17);
			var mapView = MapView.FromCamera (RectangleF.Empty, camera);	// Take note of the concept here, controller has View property

			mapView.CameraPositionChanged += HandleCameraPositionChanged;
			//mapView.
			View = mapView;

		}

		private int _counter = 1;
		void HandleCameraPositionChanged (object sender, GMSCameraEventArgs e)
		{
			if (e.Position.Zoom >= PINABLE_ZOOM_POSITION)
			{
				_isZoomedPositionOkToDropPin = true;

				var mapView = sender as MapView;

				var xamMarker = new Marker () {
					Title = "Xamarin HQ " + _counter++,
					Snippet = "Where the magic happens.",
					Position =e.Position.Target,
					Map = mapView
				};

				mapView.SelectedMarker = xamMarker;

			}
		}
	
	}
}

