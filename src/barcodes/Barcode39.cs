// author:    guillaume l. <guillaume@geelweb.org>
// license:   http://opensource.org/licenses/bsd-license.php BSD License
// copyright: copyright (c) 2007-2009, guillaume luchet

using System;
using Gtk;
using Tools.Conversion;

namespace Barcode {
    /// <summary>Barcode39
    /// <para>description</para>
    /// </summary>
    public class Barcode39 : Barcode {
        // Properties {{{
        
        /// <summary>The bars to generate eht code</summary>
        internal static byte[][] BARS = 
        {
            new byte[] {0,0,0,1,1,0,1,0,0},
            new byte[] {1,0,0,1,0,0,0,0,1},
            new byte[] {0,0,1,1,0,0,0,0,1},
            new byte[] {1,0,1,1,0,0,0,0,0},
            new byte[] {0,0,0,1,1,0,0,0,1},
            new byte[] {1,0,0,1,1,0,0,0,0},
            new byte[] {0,0,1,1,1,0,0,0,0},
            new byte[] {0,0,0,1,0,0,1,0,1},
            new byte[] {1,0,0,1,0,0,1,0,0},
            new byte[] {0,0,1,1,0,0,1,0,0},
            new byte[] {1,0,0,0,0,1,0,0,1},
            new byte[] {0,0,1,0,0,1,0,0,1},
            new byte[] {1,0,1,0,0,1,0,0,0},
            new byte[] {0,0,0,0,1,1,0,0,1},
            new byte[] {1,0,0,0,1,1,0,0,0},
            new byte[] {0,0,1,0,1,1,0,0,0},
            new byte[] {0,0,0,0,0,1,1,0,1},
            new byte[] {1,0,0,0,0,1,1,0,0},
            new byte[] {0,0,1,0,0,1,1,0,0},
            new byte[] {0,0,0,0,1,1,1,0,0},
            new byte[] {1,0,0,0,0,0,0,1,1},
            new byte[] {0,0,1,0,0,0,0,1,1},
            new byte[] {1,0,1,0,0,0,0,1,0},
            new byte[] {0,0,0,0,1,0,0,1,1},
            new byte[] {1,0,0,0,1,0,0,1,0},
            new byte[] {0,0,1,0,1,0,0,1,0},
            new byte[] {0,0,0,0,0,0,1,1,1},
            new byte[] {1,0,0,0,0,0,1,1,0},
            new byte[] {0,0,1,0,0,0,1,1,0},
            new byte[] {0,0,0,0,1,0,1,1,0},
            new byte[] {1,1,0,0,0,0,0,0,1},
            new byte[] {0,1,1,0,0,0,0,0,1},
            new byte[] {1,1,1,0,0,0,0,0,0},
            new byte[] {0,1,0,0,1,0,0,0,1},
            new byte[] {1,1,0,0,1,0,0,0,0},
            new byte[] {0,1,1,0,1,0,0,0,0},
            new byte[] {0,1,0,0,0,0,1,0,1},
            new byte[] {1,1,0,0,0,0,1,0,0},
            new byte[] {0,1,1,0,0,0,1,0,0},
            new byte[] {0,1,0,1,0,1,0,0,0},
            new byte[] {0,1,0,1,0,0,0,1,0},
            new byte[] {0,1,0,0,0,1,0,1,0},
            new byte[] {0,0,0,1,0,1,0,1,0},
            new byte[] {0,1,0,0,1,0,1,0,0}
        };
     
        /// <summary>The index chars to <CODE>BARS</CODE>.</summary>
        internal const string CHARS = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ-. $/+%*";
 
        /// <summary>The character combinations to make the code 39 extended.
        /// </summary>
        internal const string EXTENDED = "%U" +
            "$A$B$C$D$E$F$G$H$I$J$K$L$M$N$O$P$Q$R$S$T$U$V$W$X$Y$Z" +
            "%A%B%C%D%E  /A/B/C/D/E/F/G/H/I/J/K/L - ./O" +
            " 0 1 2 3 4 5 6 7 8 9/Z%F%G%H%I%J%V" +
            " A B C D E F G H I J K L M N O P Q R S T U V W X Y Z" +
            "%K%L%M%N%O%W" +
            "+A+B+C+D+E+F+G+H+I+J+K+L+M+N+O+P+Q+R+S+T+U+V+W+X+Y+Z" +
            "%P%Q%R%S%T";

        // }}}
        //Barcode39::Barcode39() {{{

        // <summary>Constructor</summary>
        public Barcode39() {
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
            extended = false;
        }

        // }}}
        // Barcode39::getBarsCode() {{{
        
        /// <summary>Create the bars</summary>
        public override byte[] getBarsCode(string text) {
            text = "*" + text + "*";
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
        // Barcode39::GetCode39Ex() {{{

        /** Converts the extended text into a normal, escaped text,
        * ready to generate bars.
        * @param text the extended text
        * @return the escaped text
        */    
        public static string GetCode39Ex(string text) {
            string ret = "";
            for (int k = 0; k < text.Length; ++k) {
                char c = text[k];
                if (c > 127)
                    throw new ArgumentException("The character '" + c + "' is illegal in code 39 extended.");
                char c1 = EXTENDED[c * 2];
                char c2 = EXTENDED[c * 2 + 1];
                if (c1 != ' ')
                    ret += c1;
                ret += c2;
            }
            return ret;
        }

        // }}}
        // Barcode39::GetChecksum() {{{
        
        /** Calculates the checksum.
        * @param text the text
        * @return the checksum
        */    
        internal static char GetChecksum(string text) {
            int chk = 0;
            for (int k = 0; k < text.Length; ++k) {
                int idx = CHARS.IndexOf(text[k]);
                if (idx < 0)
                    throw new ArgumentException("The character '" + text[k] + "' is illegal in code 39.");
                chk += idx;
            }
            return CHARS[chk % 43];
        }

        // }}}
        // Barcode39::BarcodeSize() {{{
        
        /** Gets the maximum area that the barcode and the text, if
        * any, will occupy. The lower left corner is always (0, 0).
        * @return the size the barcode occupies.
        */    
        public Gdk.Rectangle BarcodeSize {
            get {
                float fontX = 0;
                float fontY = 0;
                if (font != null) {
                    if (baseline > 0)
                        fontY = baseline - font.Size;//GetFontDescriptor(BaseFont.DESCENT, size);
                    else
                        fontY = -baseline + size;
                    string fullCode = code;
                    if (generateChecksum && checksumText)
                        fullCode += GetChecksum(fullCode);
                    if (startStopText)
                        fullCode = "*" + fullCode + "*";
                    fontX = 20;//font.GetWidthPoint(altText != null ? altText : fullCode, size);
                }            
                string fCode = code;
                if (extended)
                    fCode = GetCode39Ex(code);
                int len = fCode.Length + 2;
                if (generateChecksum)
                    ++len;
                float fullWidth = len * (6 * x + 3 * x * n) + (len - 1) * x;
                fullWidth = Math.Max(fullWidth, fontX);
                float fullHeight = barHeight + fontY;
                return new Gdk.Rectangle(0,0,System.Convert.ToInt16(fullWidth), System.Convert.ToInt16(fullHeight));
            }
        }

        // }}}
        // Barcode39::render() {{{

        public void render(int posx, int posy, DrawingArea da) {
            String bCode = code;
            if (extended)
                bCode = GetCode39Ex(code);
            if (generateChecksum)
                bCode += GetChecksum(bCode);

            double len = bCode.Length + 2;
            double nn = (n);
            double fullWidth = len * (6 + 3 * nn) + (len - 1);
            double startx = Conversion.dot2px(posx);
            byte[] bars = getBarsCode(bCode);
            for(int k=0 ; k<bars.Length ; ++k) {
                double w = (bars[k] == 0 ? 1 : nn);
                for (int j = 0; j < w; ++j) {
                    startx++;
                    da.GdkWindow.DrawLine(da.Style.BaseGC(StateType.Normal),
                        (int)(startx+w), (int)Conversion.dot2px(posy), 
                        (int)(startx+w), (int)Conversion.dot2px(posy+barHeight));
                }
            }
        }
        
        // }}}

    }
}
