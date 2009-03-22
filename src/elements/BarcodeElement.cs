// author:    guillaume l. <guillaume@geelweb.org>
// license:   http://opensource.org/licenses/bsd-license.php BSD License
// copyright: copyright (c) 2007-2009, guillaume luchet

using System;
using Barcode;
using Tools.Conversion;

namespace Ipl.Elements {
    /// <summary>BarcodeElement
    /// <para>Define a barcode</para>
    /// </summary>
    public class BarcodeElement : IplElement {
        // Properties {{{

        /// <summary>Bar dode type.</summary>
        public int symbologie = 0;

        /// <summary>Bar code modifier</summary>
        public int modifiers = 0;

        /// <summary>Bar code ratio</summary>
        public int ratio = 0;
        
        /// <summary>Bar code interpretive</summary>
        public int interpretive = 0;

        public string[] _symbologieMap = {"Code 39", "Code 93",
            "Interleaved 2 of 5", "Code 2 of 5", "Codabar", "Code 11",
            "Code 128", "UPC/EAN Codes", "HIBC Code 39", "Coed 16K", "Code 49",
            "POSTNET", "PDF417", "Code One", "MaxiCode", "JIS-ITF",
            "HIBC Code 128", "Data Matrix", "QR Code", "MicroPDF417"};
        // }}}
        //BarcodeElement::BarcodeElement() {{{

        /// <summary>Constructor</summary>
        /// <param name="index">field index</param>
        /// <param name="fieldOriginX">abscisses field origin</param>
        /// <param name="fieldOriginY">ordinates field origin</param>
        /// <param name="fieldDirection">direction if the field</param>
        /// <param name="width">width</param>
        /// <param name="height">height</param>
        public BarcodeElement(int index, double fieldOriginX, 
                double fieldOriginY, string fieldDirection, double width, 
                double height) : base(index, fieldOriginX, fieldOriginY, 
                    fieldDirection, width, height) {
            this.fieldType = IplElement.BarCode;
        }

        // }}}
    }
}
