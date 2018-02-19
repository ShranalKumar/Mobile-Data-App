using System;
using Android.Graphics;
using MobileApp.Models;

namespace MobileApp.Droid.Converters
{
    public class CoreColorConverter
    {
        public static Color GetColor(CoreColor color)
        {
            var uiColor = Color.White;
            if (color != null)
            {
                uiColor = new Color((byte)color.Red, (byte)color.Green, (byte)color.Blue, (byte)color.Alpha);
            }
            return uiColor;
        }
    }
}