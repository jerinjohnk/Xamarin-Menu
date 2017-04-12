﻿using Xamarin.Forms;

namespace MenuForms.Utils
{

    public static class ColorHelper
    {
        public static string ToHexString(this Color c) => $"#{ColorAsInt(c.A):x2}{ColorAsInt(c.R):x2}{ColorAsInt(c.G):x2}{ColorAsInt(c.B):x2}";

        private static int ColorAsInt(double color) => (int)(color * 255);
    }
}
