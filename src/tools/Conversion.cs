// author:    guillaume l. <guillaume@geelweb.org>
// license:   http://opensource.org/licenses/bsd-license.php BSD License
// copyright: copyright (c) 2007-2009, guillaume luchet
// version:   CVS: $Id$
// source:    $Source$

using System;

namespace Tools.Conversion {
    /// <summary>Conversion
    /// <para>Some static methods to units conversions</para>
    /// </summary>
    public class Conversion {
        //Conversion::Conversion() {{{

        /// <summary>Constructor</summary>
        public Conversion() {}

        // }}}
        // Conversion::in2dot() {{{
        
        /// <summary>Convert inches to dots</summary> 
        /// <param name="inches">inche value to convert</param> 
        /// <returns>int</returns> 
        public static double in2dot(double inches) {
            // XXX for 300 or 406 dpi printers, substitute 203 to 300 or 206
            return Math.Ceiling(inches * 203);
        }
        
        // }}}
        // Conversion::mm2in() {{{

        /// <summary>Convert mm to inches</summary> 
        /// <param name="mm">mm value to convert</param> 
        /// <returns>double</returns> 
        public static double mm2in(double mm) {
            return mm / 25.4;
        }

        // }}}
        // Conversion::mm2dot() {{{
        
        /// <summary>Convert mm to dots</summary> 
        /// <param name="mm">mm value to convert</param> 
        /// <returns>double</returns> 
        public static double mm2dot(double mm) {
            return Conversion.in2dot(Conversion.mm2in(mm));
        }
        
        // }}}
        // Conversion::mm2px() {{{
        
        public static double mm2px(double mm) {
            return mm * 100 / 34.29; 
        }
        
        // }}}
        // Conversion::iplFieldRotation2degree() {{{
        
        /// <summary>Convert an ipl field rotation (define with the ipl field
        /// direction command fn) to degree</summary>
        /// <param name="rot">field rotation value</param> 
        /// <returns>int</returns> 
        public static int iplRot2degree(string rot) {
		    switch(rot.ToUpper()) {
		        case "F0":
		            return 0;
		        case "F1":
		            return 90;
		        case "F2":
		            return 180;
		        case "F3":
		            return 270;
		    }
		    return 0;
            //return rot * 90;
        }
        
        // }}}
        // Conversion::dot2px() {{{
        
        /// <summary>Convert dots to pixels.</summary> 
        /// <param name="dots">dots number</param> 
        /// <returns>double</returns> 
        public static double dot2px(double dots) {
            return dots * 25.4 / 203 * 100 / 34.29;
        }
        
        // }}}
        // Conversion::dot2pt() {{{
		
        /// <summary>Convert dot to pt.</summary>
        /// <param name="dots"> a dot value</param>
        /// <returns>double</returns>
		public static double dot2pt(double dots) {
		    return dots * 25.4 / 72;
		}

        // }}}
    }
}
