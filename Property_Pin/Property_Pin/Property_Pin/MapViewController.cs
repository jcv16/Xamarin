using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.CoreLocation;
using Google.Maps;

namespace GoogleMapsSample
{
	public class MapViewController : UIViewController
	{
		private MapView _mapView;
		private int _counter = 1;
		private bool _isZoomedPositionOkToDropPin;
		private const int PINABLE_ZOOM_POSITION = 19;

		public override void LoadView ()
		{
			base.LoadView ();

			var camera = CameraPosition.FromCamera (-33.868, 151.2086, 17);
			_mapView = MapView.FromCamera (RectangleF.Empty, camera);
			_mapView.MyLocationEnabled = true;	

			_mapView.CameraPositionChanged += HandleCameraPositionChanged;
			_mapView.LongPress += HandleLongPress;
			_mapView.TappedMarker = (s, e) => 
			{
				HandleGMSTappedMarker(s,e);
				return true;
			};

			View = _mapView;
		}

		void HandleGMSTappedMarker (MapView mapView, Marker marker)
		{
			// TODO : Handle tapped marker here
			// Cuurently these changes color only
			if (marker.Icon == UIImage.FromBundle ("pink-marker")) 
			{
				marker.Icon = UIImage.FromBundle ("green-marker");
			}
			else if (marker.Icon == UIImage.FromBundle ("blue-marker"))
			{
				marker.Icon = UIImage.FromBundle ("pink-marker");
			}
			else 
				marker.Icon = UIImage.FromBundle ("blue-marker");
		}

		void HandleLongPress (object sender, GMSCoordEventArgs eventArg)
		{
			if (_isZoomedPositionOkToDropPin) 
			{
				var mapView = sender as MapView;

				var xamMarker = new Marker () {
					Title = "Property " + _counter++,
					Snippet = "Where the magic happens.",
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


		void HandleCameraPositionChanged (object sender, GMSCameraEventArgs e)
		{
			if (e.Position.Zoom >= PINABLE_ZOOM_POSITION)
			{
				_isZoomedPositionOkToDropPin = true;
			}
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			_mapView.StartRendering ();
		}

		public override void ViewWillDisappear (bool animated)
		{	
			_mapView.StopRendering ();
			base.ViewWillDisappear (animated);
		}
	}
}

