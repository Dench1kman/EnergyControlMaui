using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace EnergyControlMaui.MarkupExtensions
{
    public class EmbeddedImage : IMarkupExtension
    {
        public required string ResourceId { get; set; }
        public object? ProvideValue(IServiceProvider serviceProvider)
        {
            if(string.IsNullOrEmpty(ResourceId))
                return null;

            return ImageSource.FromResource(ResourceId);
        }
    }
}
