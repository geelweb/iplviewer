// author:    guillaume l. <guillaume@geelweb.org>
// license:   http://opensource.org/licenses/bsd-license.php BSD License
// copyright: copyright (c) 2007-2009, guillaume luchet

using System;
using Tools.Conversion;

namespace Ipl.Elements {
    /// <summary>HumanReadableElement
    /// <para>define an human-readable ipl field.</para>
    /// </summary>
    public class HumanReadableElement : IplElement {
        // Properties {{{
        /// <summary>Font description.</summary> 
        //public int font;

        /// <summary>Font size corresponding to the font description.</summary> 
        //public double _fontSize;

        public double characterRotation = 0;
        public bool pitch = false;
        public bool point = false;
        public string dataOrigin;
        public int dataLength = 30;

        // }}}
        //HumanReadableElement::HumanReadableElement() {{{

        // <summary>Constructor</summary>
        /// <param name="index">field index</param>
        /// <param name="fieldOriginX">abscisses field origin</param>
        /// <param name="fieldOriginY">ordinates field origin</param>
        /// <param name="fieldDirection">direction if the field</param>
        /// <param name="width">width</param>
        /// <param name="height">height</param>
        public HumanReadableElement(int index, double fieldOriginX, 
                double fieldOriginY, string fieldDirection, double width, 
                double height) : base(index, fieldOriginX, fieldOriginY, 
                    fieldDirection, width, height) {
            this.fieldType = IplElement.HumanReadable;
            // set defaults
            //this.fieldOriginX = 0;
            //this.fieldOriginY = 0;
            //this.height = 2;
            //this.font = "7x9 standrad";
        }

        // }}}
        // HumanReadable::_cleanText() {{{

        /// <summary>Clean the text</summary>
        /// <returns>void</returns>
        public void cleanText() {
            this.data = this.data.Replace("<CR>", "");
        }

        // }}}
    }
}
