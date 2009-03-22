// author:    guillaume l. <guillaume@geelweb.org>
// license:   http://opensource.org/licenses/bsd-license.php BSD License
// copyright: copyright (c) 2007-2009, guillaume luchet
// version:   CVS: $Id$
// source:    $Source$

using System;
//using Gtk;

namespace Ipl.Elements {
    /// <summary>iplFont, describe the Ipl fonts.</summary> 
    public struct iplFont {
        // properties {{{

        /// <summary>font's code</summary> 
        public int code;

        /// <summary>font's size</summary> 
        public int size;

        /// <summary>font's name</summary> 
        public string name;

        // }}}
        // methods {{{
        
        /// <summary>Constructor</summary>
        /// <param name="c">font's code</param> 
        /// <param name="n">font's name</param> 
        /// <param name="s">font's size</param> 
        public iplFont(int c, string n, int s) {code = c; name = n; size = s;}

        // }}}
    }

    /// <summary>IplElement
    /// <para>This class is the base class to define the Ipl elements</para>
    /// </summary>
    public class IplElement {
        // Properties {{{
        
        /// <summary>Array of iplFont structure to describe the fonts.
        /// </summary> 
        public iplFont[] fontDescriptions = {
            new iplFont(0, "7x9 Standard", 0),
            new iplFont(1, "7x11 OCR", 0),
            new iplFont(2, "10x14 Standard", 0),
            new iplFont(7, "5x7 Standard", 0),
            new iplFont(20, "monospace", 8),
            new iplFont(21, "monospace", 12),
            new iplFont(22, "monospace", 20),
            new iplFont(23, "OCR A", 0),
            new iplFont(24, "OCR B size 2", 0),
            new iplFont(25, "Swiss Mono 721 standard outline font", 0),
            new iplFont(26, "Swiss Mono 721 bold outline font", 0),
            new iplFont(28, "Dutch  Roman 801 proportional outline font", 0),
            new iplFont(30, "monospace bold", 6),
            new iplFont(31, "monospace bold", 8),
            new iplFont(32, "monospace standard", 10),
            new iplFont(33, "monospace bold", 10),
            new iplFont(34, "monospace bold", 12),
            new iplFont(35, "monospace standard", 16),
            new iplFont(36, "monospace bold", 16),
            new iplFont(37, "monospace bold", 20),
            new iplFont(38, "monospace standard", 24),
            new iplFont(39, "monospace bold", 24),
            new iplFont(40, "monospace bold", 30),
            new iplFont(41, "monospace bold", 36),
            new iplFont(50, "Kanji outline font", 0),
            new iplFont(51, "Kanji monospace outline font", 0),
            new iplFont(52, "Katakana 12x16 bitmap", 0),
            new iplFont(53, "Katakana 16x24 bitmap", 0),
            new iplFont(54, "Katakana 24x36 bitmap", 0),
            new iplFont(55, "Kanji 16x16 bitmap", 0),
            new iplFont(56, "Kanji 24x24 bitmap", 0),
            new iplFont(61, "Swiss 721", 0),
            new iplFont(62, "Swiss 721 bold", 0),
            new iplFont(63, "Swiss 721 bold condensed", 0),
            new iplFont(64, "Prestige bold", 0),
            new iplFont(65, "Zurich extra condensed", 0),
            new iplFont(66, "Dutch 801 bold", 0),
            new iplFont(67, "Century Schoolbook", 0),
            new iplFont(68, "Futura light", 0),
            new iplFont(69, "Letter Gothic", 0),
            new iplFont(70, "DingDings", 0)
        };

        /// <summary>Constant to define a BarCode element</summary> 
        public const int BarCode = 1;

        /// <summary>Constant to define a Box element</summary> 
        public const int Box = 2;

        /// <summary>Constant to define an HumanReadable element</summary> 
        public const int HumanReadable = 3;

        /// <summary>Constant to define a Line element</summary> 
        public const int Line = 4;

        /// <summary>Constant to define an UDC element</summary> 
        public const int UDC = 5;
        
        /// <summary>Field type.
        /// - 1 BarCode
        /// - 2 Box
        /// - 3 HumanReadable
        /// - 4 Line
        /// - 5 UDC
        /// </summary> 
        public int fieldType = 0;

        /// <summary>Index of the element</summary> 
        public int fieldIndex;

        /// <summary>Upper left corner field abscisse.</summary> 
        public double fieldOriginX = 0;

        /// <summary>Upper left corner field ordinate.</summary> 
        public double fieldOriginY = 0;

        /// <summary>Font.</summary> 
        public iplFont font;

        /// <summary>Field direction.</summary> 
        public string fieldDirection = "f0";

        /// <summary>Field height.</summary> 
        public double height = 0;

        /// <summary>Field width.</summary> 
        public double width = 0;

        /// <summary>Field source.</summary> 
        public int fieldSource = 0;

        /// <summary>Number of characters.</summary> 
        public int numberOfChars = 0;

        /// <summary>True if data should be set. </summary> 
        public bool waitData = false;

        /// <summary>Element data.</summary> 
        public string data = "";

        // }}}
        //IplElement::IplElement() {{{

        /// <summary>Default constructor.</summary> 
        public IplElement() {}

        /// <summary>Constructor</summary>
        /// <param name="index">field index</param>
        /// <param name="fieldOriginX">abscisses field origin</param>
        /// <param name="fieldOriginY">ordinates field origin</param>
        /// <param name="fieldDirection">direction if the field</param>
        /// <param name="width">width</param>
        /// <param name="height">height</param>
        public IplElement(int index, double fieldOriginX, double fieldOriginY,
                string fieldDirection, double width, double height) {
            this.fieldIndex = index;
            this.fieldOriginX = fieldOriginX;
            this.fieldOriginY = fieldOriginY;
            this.fieldDirection = fieldDirection;
            this.height = height;
            this.width = width;
        }

        // }}}
        // IplElement::getFont {{{

        /// <summary>Search in the fontDescriptions the iplFont according to the
        /// code.</summary> 
        /// <param name="code">Code to the font to search</param> 
        /// <returns>struct iplFont</returns> 
        public iplFont getFont(int code) {
            int loop = 0;
            iplFont tmp;
            while(loop < fontDescriptions.Length) {
                tmp = this.fontDescriptions[loop];
                loop ++;
                if(code == tmp.code) {
                    return tmp;
                }
            }
            return this.fontDescriptions[0];
        }

        // }}}
    }
}
