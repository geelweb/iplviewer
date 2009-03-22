// author:    guillaume l. <guillaume@geelweb.org>
// license:   http://opensource.org/licenses/bsd-license.php BSD License
// copyright: copyright (c) 2007-2009, guillaume luchet

using System;
using Gtk;
using Tools.Conversion;

namespace Barcode {
    /// <summary>Interleaved2of5
    /// <para>Interleaved 2 of 5 barcode</para>
    /// </summary>
    class Interleaved2of5 : Barcode {
        // Properties {{{

        /// <summary>The bars to generate eht code</summary>
        internal static byte[][] BARS = 
        {
            new byte[] {0,0,1,1,0},
            new byte[] {1,0,0,0,1},
            new byte[] {0,1,0,0,1},
            new byte[] {1,1,0,0,0},
            new byte[] {0,0,1,0,1},
            new byte[] {1,0,1,0,0},
            new byte[] {0,1,1,0,0},
            new byte[] {0,0,0,1,1},
            new byte[] {1,0,0,1,0},
            new byte[] {0,1,0,1,0},
            new byte[] {0,0},
            new byte[] {1,0}
        };


        /// <summary>The index chars to <CODE>BARS</CODE>.</summary>
        internal const string CHARS = "0123456789AZ";
        
        // }}}
        //Interleaved2of5::Interleaved2of5() {{{

        // <summary>Constructor</summary>
        public Interleaved2of5() {
            x = 0.8f;// largeur d'une barre fine
            n = 2;   // multiplieur pour les barres épaisses
            //font = BaseFont.CreateFont("Helvetica", "winansi", false);
            font = Pango.FontDescription.FromString("Helvetica winansi 8");
            size = 8;
            baseline = size;
            barHeight = 10;//size * 3;
            //textAlignment = Element.ALIGN_CENTER;
            generateChecksum = false;
            checksumText = false;
            startStopText = true;
            extended = true;//false;
 
            
        }

        public override byte[] getBarsCode(string text) {
            if(text.Length %2 != 0) {
                text = "0" + text;
            }
	        // add start and stop codes
	        text = "AA" + text.ToUpper() + "ZA";
            byte[] bars = new byte[text.Length * 10 -1];
            for(int k=0 ; k<text.Length ; ++k) {
                int idx = CHARS.IndexOf(text[k]);
                if(idx < 0) {
                    throw new ArgumentException("The character '" + text[k] + "' is illegal in code 39");
                }
                Array.Copy(BARS[idx], 0, bars, k * 10, 9);
            }
            return bars;
        }
        // }}}
        // Barcode39::render() {{{

        public void render(int posx, int posy, DrawingArea da) {
            String bCode = code;
            
            int len = bCode.Length + 2;
            int nn = (int)(n);
            int fullWidth = len * (6 + 3 * nn) + (len - 1);
            int startx = (int)Conversion.mm2px(posx);
            byte[] bars = getBarsCode(bCode);
            for(int k=0 ; k<bars.Length ; ++k) {
                int w = (bars[k] == 0 ? 1 : nn);
                for (int j = 0; j < w; ++j) {
                    startx++;
                    da.GdkWindow.DrawLine(da.Style.BaseGC(StateType.Normal),
                        startx+w, (int)Conversion.mm2px((int)posy), 
                        startx+w, (int)Conversion.mm2px((int)(posy+barHeight)));
                }
            }
        }
        
        // }}}


    }
}
