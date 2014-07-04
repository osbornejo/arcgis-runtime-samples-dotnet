﻿using Esri.ArcGISRuntime.Geometry;
using Windows.UI.Xaml.Controls;

namespace ArcGISRuntimeSDKDotNet_StoreSamples.Samples
{
    /// <summary>
    /// Demonstrates setting the extent and spatial reference of a map via the Map.InitialExtent property.
    /// </summary>
    /// <title>Set Spatial Reference</title>
    /// <category>Mapping</category>
	public sealed partial class SetSpatialReference : Page
    {
        public SetSpatialReference()
        {
            this.InitializeComponent();

			// Create initial extend and set it
			var envelopeBuilder = new EnvelopeBuilder(SpatialReference.Create(26777));
			envelopeBuilder.XMin = 661140;
			envelopeBuilder.YMin = -1420246;
			envelopeBuilder.XMax = 3015668;
			envelopeBuilder.YMax = 1594451;

			mapView.Map.InitialExtent = envelopeBuilder.ToGeometry();
        }
    }
}