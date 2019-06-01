using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MinoriDemo.RibbonWPF.Modules.VirtualCanvas.Models
{
    /// <include file='doc\ControlPaint.uex' path='docs/doc[@for="ControlPaint.HLSColor"]/*' />
    /// <devdoc>
    ///     Logic copied from Win2K sources to copy the lightening and
    ///     darkening of colors.
    /// </devdoc>
    public struct HlsColor
    {
        private const Int32 ShadowAdj = -333;
        private const Int32 HilightAdj = 500;

        private const Int32 Range = 240;
        private const Int32 HLSMax = Range;
        private const Int32 RGBMax = 255;
        private const Int32 Undefined = HLSMax * 2 / 3;

        /// <include file='doc\ControlPaint.uex' path='docs/doc[@for="ControlPaint.HLSColor.HLSColor"]/*' />
        /// <devdoc>
        /// </devdoc>
        public HlsColor(Color color)
        {
            Int32 r = color.R;
            Int32 g = color.G;
            Int32 b = color.B;
            Int32 max, min;        /* max and min RGB values */
            Int32 sum, dif;
            Int32 Rdelta, Gdelta, Bdelta;  /* intermediate value: % of spread from max */

            /* calculate lightness */
            max = Math.Max(Math.Max(r, g), b);
            min = Math.Min(Math.Min(r, g), b);
            sum = max + min;

            Luminosity = (((sum * HLSMax) + RGBMax) / (2 * RGBMax));

            dif = max - min;
            if (dif == 0)
            {       /* r=g=b --> achromatic case */
                Saturation = 0;                         /* saturation */
                Hue = Undefined;                 /* hue */
            }
            else
            {                           /* chromatic case */
                /* saturation */
                Saturation = Luminosity <= (HLSMax / 2)
                    ? ((dif * HLSMax) + (sum / 2)) / sum
                    : ((dif * HLSMax) + (2 * RGBMax - sum) / 2) / (2 * RGBMax - sum);
                /* hue */
                Rdelta = (((max - r) * (HLSMax / 6)) + (dif / 2)) / dif;
                Gdelta = (((max - g) * (HLSMax / 6)) + (dif / 2)) / dif;
                Bdelta = (((max - b) * (HLSMax / 6)) + (dif / 2)) / dif;

                if (r == max)
                {
                    Hue = Bdelta - Gdelta;
                }
                else
                {
                    Hue = g == max
                        ? (HLSMax / 3) + Rdelta - Bdelta
                        : ((2 * HLSMax) / 3) + Gdelta - Rdelta;
                }

                if (Hue < 0)
                {
                    Hue += HLSMax;
                }

                if (Hue > HLSMax)
                {
                    Hue -= HLSMax;
                }
            }
        }

        /// <include file='doc\ControlPaint.uex' path='docs/doc[@for="ControlPaint.HLSColor.Hue"]/*' />
        /// <devdoc>
        /// </devdoc>
        public Int32 Hue { get; }

        /// <include file='doc\ControlPaint.uex' path='docs/doc[@for="ControlPaint.HLSColor.Luminosity"]/*' />
        /// <devdoc>
        /// </devdoc>
        public Int32 Luminosity { get; }

        /// <include file='doc\ControlPaint.uex' path='docs/doc[@for="ControlPaint.HLSColor.Saturation"]/*' />
        /// <devdoc>
        /// </devdoc>
        public Int32 Saturation { get; }

        public Color Darker(Single percDarker)
        {
            Int32 oneLum = 0;
            Int32 zeroLum = NewLuma(ShadowAdj, true);
            return ColorFromHLS(Hue, zeroLum - (Int32)((zeroLum - oneLum) * percDarker), Saturation);
        }

        public static Boolean operator ==(HlsColor a, HlsColor b) => a.Equals(b);

        public static Boolean operator !=(HlsColor a, HlsColor b) => !a.Equals(b);

        public override Boolean Equals(Object o)
        {
            if (!(o is HlsColor))
            {
                return false;
            }

            HlsColor c = (HlsColor)o;
            return Hue == c.Hue &&
                Saturation == c.Saturation &&
                Luminosity == c.Luminosity;
        }

        public override Int32 GetHashCode() => Hue << 6 | Saturation << 2 | Luminosity;

        public Color Lighter(Single percLighter)
        {
            Int32 zeroLum = Luminosity;
            Int32 oneLum = NewLuma(HilightAdj, true);
            return ColorFromHLS(Hue, zeroLum + (Int32)((oneLum - zeroLum) * percLighter), Saturation);
        }

        private Int32 NewLuma(Int32 n, Boolean scale) => NewLuma(Luminosity, n, scale);

        private static Int32 NewLuma(Int32 luminosity, Int32 n, Boolean scale)
        {
            if (n == 0)
            {
                return luminosity;
            }

            if (scale)
            {
                if (n > 0)
                {
                    return (Int32)((luminosity * (1000 - n) + (Range + 1L) * n) / 1000);
                }
                else
                {
                    return (luminosity * (n + 1000)) / 1000;
                }
            }

            Int32 newLum = luminosity;
            newLum += (Int32)((Int64)n * Range / 1000);

            if (newLum < 0)
            {
                newLum = 0;
            }

            if (newLum > HLSMax)
            {
                newLum = HLSMax;
            }

            return newLum;
        }

        /// <include file='doc\ControlPaint.uex' path='docs/doc[@for="ControlPaint.HLSColor.ColorFromHLS"]/*' />
        /// <devdoc>
        /// </devdoc>
        public static Color ColorFromHLS(Int32 hue, Int32 luminosity, Int32 saturation)
        {
            Byte r, g, b;                      /* RGB component values */
            Int32 magic1, magic2;       /* calculated magic numbers (really!) */

            if (saturation == 0)
            {                /* achromatic case */
                r = g = b = (Byte)((luminosity * RGBMax) / HLSMax);
                if (hue != Undefined)
                {
                    /* ERROR */
                }
            }
            else
            {                         /* chromatic case */
                /* set up magic numbers */
                if (luminosity <= (HLSMax / 2))
                {
                    magic2 = (luminosity * (HLSMax + saturation) + (HLSMax / 2)) / HLSMax;
                }
                else
                {
                    magic2 = luminosity + saturation - ((luminosity * saturation) + HLSMax / 2) / HLSMax;
                }

                magic1 = 2 * luminosity - magic2;

                /* get RGB, change units from HLSMax to RGBMax */
                r = (Byte)((HueToRGB(magic1, magic2, hue + HLSMax / 3) * RGBMax + (HLSMax / 2)) / HLSMax);
                g = (Byte)((HueToRGB(magic1, magic2, hue) * RGBMax + (HLSMax / 2)) / HLSMax);
                b = (Byte)((HueToRGB(magic1, magic2, hue - HLSMax / 3) * RGBMax + (HLSMax / 2)) / HLSMax);
            }
            return Color.FromRgb(r, g, b);
        }

        /// <include file='doc\ControlPaint.uex' path='docs/doc[@for="ControlPaint.HLSColor.HueToRGB"]/*' />
        /// <devdoc>
        /// </devdoc>
        private static Int32 HueToRGB(Int32 n1, Int32 n2, Int32 hue)
        {
            /* range check: note values passed add/subtract thirds of range */

            /* The following is redundant for WORD (unsigned int) */
            if (hue < 0)
            {
                hue += HLSMax;
            }

            if (hue > HLSMax)
            {
                hue -= HLSMax;
            }

            /* return r,g, or b value from this tridrant */
            if (hue < (HLSMax / 6))
            {
                return (n1 + (((n2 - n1) * hue + (HLSMax / 12)) / (HLSMax / 6)));
            }

            if (hue < (HLSMax / 2))
            {
                return (n2);
            }

            if (hue < ((HLSMax * 2) / 3))
            {
                return (n1 + (((n2 - n1) * (((HLSMax * 2) / 3) - hue) + (HLSMax / 12)) / (HLSMax / 6)));
            }
            else
            {
                return (n1);
            }
        }

        public override String ToString() => $"{Hue}, {Luminosity}, {Saturation}";
    }
}
