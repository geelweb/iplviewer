// author:    guillaume l. <guillaume@geelweb.org>
// license:   http://opensource.org/licenses/bsd-license.php BSD License
// copyright: copyright (c) 2007-2009, guillaume luchet
// version:   CVS: $Id$
// source:    $Source$

using System;
using Gtk;

namespace Barcode {
    /// <summary>Barcode
    /// <para>describe a barcode</para>
    /// </summary>
    public abstract class Barcode  {
   
        // consts {{{
        /** A type of barcode */
        //public const int EAN13 = 1;
        /** A type of barcode */
        //public const int EAN8 = 2;
        /** A type of barcode */
        //public const int UPCA = 3;
        /** A type of barcode */
        //public const int UPCE = 4;
        /** A type of barcode */
        //public const int SUPP2 = 5;
        /** A type of barcode */
        //public const int SUPP5 = 6;
        /** A type of barcode */
        //public const int POSTNET = 7;
        /** A type of barcode */
        //public const int PLANET = 8;
        /** A type of barcode */
        //public const int CODE128 = 9;
        /** A type of barcode */
        //public const int CODE128_UCC = 10;
        /** A type of barcode */
        //public const int CODE128_RAW = 11;
        /** A type of barcode */
        //public const int CODABAR = 12;
        // }}}
        // float x {{{
        
        /// <summary>The minimum bar width.</summary>
        protected float x;    

        /// <summary>The minimum bar width.</summary>
        public float X {
            get { return x; }
            set { this.x = value; }
        }
        
        // }}}
        // float n {{{
        
        /// <summary>The bar multiplier for wide bars or the distance between
        /// bars for Postnet and Planet.</summary>
        protected float n;
    
        /// <summary>Gets the bar multiplier for wide bars.</summary>
        public float N {
            get { return n; }
            set { this.n = value; }
        }
        
        // }}}
        // Pango.FontDescription font {{{

        /// <summary>The text font.</summary>
        protected Pango.FontDescription font;

        /// <summary>Gets the text font. <CODE>null</CODE> if no text.</summary>
        public Pango.FontDescription Font {
            get { return font; }
            set { this.font = value; }
        }

        // }}}
        // float size {{{
    
        /// <summary>The size of the text or the height of the shorter bar
        /// in Postnet.</summary>
        protected float size;
    
        /// <summary>Gets the size of the text.</summary>
        public float Size {
            get { return size; }
            set { this.size = value; }
        }
    
        // }}}
        // float baseline {{{

        /// <summary>If positive, the text distance under the bars. If zero or negative,
        /// the text distance above the bars.</summary>
        protected float baseline;
    
        /// <summary>Gets the text baseline.
        /// If positive, the text distance under the bars. If zero or negative,
        /// the text distance above the bars.</summary>
        public float Baseline {
            get { return baseline; }
            set { this.baseline = value; }
        }

        // }}}
        // float barHeight {{{
    
        /// <summary>Gets the height of the bars.</summary>
        /// <summary>The height of the bars.</summary>
        protected float barHeight;
 
        public float BarHeight {
            get { return barHeight; }
            set { this.barHeight = value; }
        }

        // }}}
        // int textAlignment {{{
        
        /// <summary>The text Element. Can be <CODE>Element.ALIGN_LEFT</CODE>,
        /// <CODE>Element.ALIGN_CENTER</CODE> or 
        /// <CODE>Element.ALIGN_RIGHT</CODE>.</summary>
        protected int textAlignment;
    
        /// <summary>Gets the text Element. Can be 
        /// <CODE>Element.ALIGN_LEFT</CODE>,
        /// <CODE>Element.ALIGN_CENTER</CODE> or 
        /// <CODE>Element.ALIGN_RIGHT</CODE>.</summary>
        public int TextAlignment{
            get { return textAlignment; }
            set { this.textAlignment = value; }
        }

        // }}}
        // bool generateChecksum {{{
    
        /// <summary>The optional checksum generation.</summary>
        protected bool generateChecksum;
    
        /// <summary>The property for the optional checksum
        /// generation.</summary>
        public bool GenerateChecksum {
            set { this.generateChecksum = value; }
            get { return generateChecksum; }
        }

        // }}}
        // bool checksumText {{{
    
        /// <summary>Shows the generated checksum in the the text.</summary>
        protected bool checksumText;
    
        /// <summary>Sets the property to show the generated checksum in the
        /// the text.</summary>
        public bool ChecksumText {
            set { this.checksumText = value; }
            get { return checksumText; }
        }

        // }}}
        // bool startStopText {{{
    
        /// <summary>Show the start and stop character '*' in the text for
        /// the barcode 39 or 'ABCD' for codabar.</summary>
        protected bool startStopText;
    
        /// <summary>Gets the property to show the start and stop character '*'
        /// in the text for the barcode 39.</summary>
        public bool StartStopText {
            set { this.startStopText = value; }
            get { return startStopText; }
        }

        // }}}
        // bool extended {{{
    
        /// <summary>Generates extended barcode 39.</summary>
        protected bool extended;
    
        /// <summary>Sets the property to generate extended barcode
        /// 39.</summary>
        public bool Extended {
            set { this.extended = value; }
            get { return extended; }
        }

        // }}}
        // string code {{{
    
        /// <summary>The code to generate.</summary>
        protected string code = "";
    
        /// <summary>Gets the code to generate.</summary>
        public virtual string Code {
            get { return code; }
            set { this.code = value; }
        }

        // }}}
        // bool guardBars {{{
    
        /// <summary>Show the guard bars for barcode EAN.</summary>
        protected bool guardBars;
    
        /// <summary>Sets the property to show the guard bars for barcode
        /// EAN.</summary>
        public bool GuardBars {
            set { this.guardBars = value; }
            get { return guardBars; }
        }

        // }}}
        // int codeType {{{
    
        /// <summary>The code type.</summary>
        protected int codeType;
    
        /// <summary>Gets the code type.</summary>
        public int CodeType {
            get { return codeType; }
            set { this.codeType = value; }
        }

        // }}}
        // float inkSpreading {{{
    
        /// <summary>The ink spreading.</summary>
        protected float inkSpreading = 0;

        /// <summary></summary>
        public float InkSpreading {
            set { inkSpreading = value; }
            get { return inkSpreading; }
        }

        // }}}
        // String altText {{{

        /// <summary>The alternate text to be used, if present.</summary>
        protected String altText;

        // string altText
        /// <summary>Sets the alternate text. If present, this text will be
        /// used instead of the text derived from the supplied code.</summary>
        public String AltText {
            set { altText = value; }
            get { return altText; }
        }

        // }}}

        // Barcode::getBarsCode() {{{
        
        /// <summary>Return the bars code</summary>
        /// <param name="text">barcode text</param>
        /// <return>byte[]</return>
        public abstract byte[] getBarsCode(string text);

        // }}}
    }
}
