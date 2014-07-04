﻿using Esri.ArcGISRuntime.Controls;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Layers;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ArcGISRuntimeSDKDotNet_StoreSamples.Samples
{
    /// <summary>
    /// Demonstrates adding an ArcGIS dynamic map service layer to a map
    /// </summary>
    /// <title>ArcGIS Dynamic Map Service Layer</title>
    /// <category>Dynamic Service Layers</category>
    public sealed partial class ArcGISDynamicMapServiceLayerSample : Page
    {
        private ArcGISDynamicMapServiceLayer _usaLayer;

        public ArcGISDynamicMapServiceLayerSample()
        {
            this.InitializeComponent();

			// Create initial extend and set it
			var envelopeBuilder = new EnvelopeBuilder(SpatialReference.Create(102009));
			envelopeBuilder.XMin = -3548912;
			envelopeBuilder.YMin = 1847469;
			envelopeBuilder.XMax = 2472012;
			envelopeBuilder.YMax = 1742990;

			mapView.Map.InitialExtent = envelopeBuilder.ToGeometry();

            mapView.LayerLoaded += mapView_LayerLoaded;

            _usaLayer = mapView.Map.Layers["USA"] as ArcGISDynamicMapServiceLayer;
        }

        private void mapView_LayerLoaded(object sender, LayerLoadedEventArgs e)
        {
            if (e.LoadError != null)
            {
                var _ = new MessageDialog(e.LoadError.Message, "Layer Error").ShowAsync();
                return;
            }

            if (e.Layer == _usaLayer)
            {
                if (_usaLayer.DynamicLayerInfos == null)
                    _usaLayer.DynamicLayerInfos = _usaLayer.CreateDynamicLayerInfosFromLayerInfos();

                _usaLayer.VisibleLayers = new ObservableCollection<int>(_usaLayer.DynamicLayerInfos
                    .Where(info => info.DefaultVisibility == true)
                    .Select((info, idx) => idx));

                visibleLayers.ItemsSource = _usaLayer.DynamicLayerInfos
                    .Select((info, idx) => new Tuple<string, int, bool>(info.Name, idx, info.DefaultVisibility));
            }
        }

        private void LayerCheckBox_Click(object sender, RoutedEventArgs e)
        {
            var checkBox = e.OriginalSource as CheckBox;
            if (checkBox != null)
            {
                int layerIndex = ((Tuple<string, int, bool>)checkBox.Tag).Item2;

                if (checkBox.IsChecked == true)
                    _usaLayer.VisibleLayers.Add(layerIndex);
                else
                    _usaLayer.VisibleLayers.Remove(layerIndex);
            }
        }
    }
}