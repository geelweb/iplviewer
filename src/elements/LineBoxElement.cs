// author:    guillaume l. <guillaume@geelweb.org>
// license:   http://opensource.org/licenses/bsd-license.php BSD License
// copyright: copyright (c) 2007-2009, guillaume luchet
// version:   CVS: $Id$
// source:    $Source$

using System;
using Tools.Conversion;

namespace Ipl.Elements {
    /// <summary>LineElement
    /// <para>define an ipl line or box element.</para>
    /// </summary>
    public class LineBoxElement : IplElement {
        // Properties {{{

        /// <summary>Line length.</summary> 
        public double length = 0;

        // }}}
        //LineBoxElement::LineBoxElement() {{{

        /// <summary>Constructor</summary>
        /// <param name="index">field index</param>
        /// <param name="type">type of the field (Line or Box field)</param>
        /// <param name="fieldOriginX">abscisses field origin</param>
        /// <param name="fieldOriginY">ordinates field origin</param>
        /// <param name="fieldDirection">direction if the field</param>
        /// <param name="width">width</param>
        /// <param name="height">height</param>
        public LineBoxElement(int index, int type, double fieldOriginX, 
                double fieldOriginY, string fieldDirection, double width, 
                double height) : base(index, fieldOriginX, fieldOriginY, 
                    fieldDirection, width, height) {
            this.fieldType = type;
        }

        // }}}
    }
}
