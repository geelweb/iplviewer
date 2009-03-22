// author:    guillaume l. <guillaume@geelweb.org>
// license:   http://opensource.org/licenses/bsd-license.php BSD License
// copyright: copyright (c) 2007-2009, guillaume luchet
// version:   CVS: $Id$
// source:    $Source$

using System;
//using Gtk;
using System.IO;
using System.Text.RegularExpressions;
using Ipl.Elements;
using Tools.Preferences;
using Tools.Conversion;

namespace Ipl.Parser {
    /// <summary>IplParser
    /// <para>This class parse an Ipl content.</para>
    /// <example>
    /// <code>
    /// IplParser parser = new IplParser('/path/to/ipl/file');
    /// </code>
    /// </example>
    /// </summary>
    public class IplParser {
        // Properties {{{

        /// <summary>Program mode constan</summary> 
        public const int programMode = 1;

        /// <summary>Print mode constant</summary> 
        public const int printMode = 2;

        /// <summary>Test And Service mode constant</summary> 
        public const int TASMode = 3;

        /// <summary>Configuration to print mode constant</summary> 
        public const int confMode = 4;

        /// <summary>Immediate mode constant</summary> 
        public const int immediateMode  =5;

        /// <summary>Ipl content.</summary> 
        private string _iplCode;

        /// <summary>Array of elements define in Ipl code.</summary> 
        private System.Collections.ArrayList _elements = new System.Collections.ArrayList();

        /// <summary>Label's elements</summary> 
        public System.Collections.ArrayList elements { get {return this._elements;} }

        /// <summary>Define the current command type.
        /// Allowed command types are:
        ///  - 1 Print to print mode
        ///  - 2 Program to program mode
        ///  - 3 TestAndService to test and service mode
        ///  - 4 Configuration to print mode
        ///  - 5 Immediate to any operating mode
        /// (p2 of IPL Programmer's Reference Manual)</summary> 
        private int _commandType = 0;

        /// <summary>true when the file has been parsed.</summary> 
        public bool fileParsed = false;

        // field type representation:
        // H : Human-readable (class HumanReadableElement)
        // B : Bar code (class BarcodeElement)
        // L : Line (class LineElement)
        // W : Box (class BoxElement)
        // U : User-defines characters (UDCs) or graphics (class UDCsElement)

        // }}}
        //IplParser::IplParser() {{{

        /// <summary>Constructor</summary>
        public IplParser() {}

        /// <summary>Constructor</summary> 
        /// <param name="file">path to an Ipl file</param> 
        /// <returns>void</returns> 
        public IplParser(string file) {
            StreamReader stream = File.OpenText(file);
            this._iplCode = stream.ReadToEnd();
            stream.Close();
            this._parse();
        }

        // }}}
        // IplParser::_parse() {{{
        
        /// <summary>Parse an Ipl content.</summary>  
        /// <returns>void</returns> 
        private void _parse() {
            Regex reg = new Regex("<STX>(.+)<ETX>");
            MatchCollection matches = reg.Matches(this._iplCode);
            for(int i=0 ; i<matches.Count ; i++) {
                this._parseInstruction(matches[i].Groups[1].Value);
            }
            this.fileParsed = true;
        }
        
        // }}}
        // IplParser::_parseInstruction() {{{
        
        /// <summary>Parse an Ipl instruction</summary> 
        /// <param name="code">Ipl instruction to parsed</param> 
        /// <returns>boolean</returns> 
        private bool _parseInstruction(string code) {
            Regex reg;
            MatchCollection matches;

            reg = new Regex("^[BHLUW]");
            matches = reg.Matches(code);
            if(matches.Count == 1) {
                return this._parseElementDefinition(code);
            }
            // Format Editing Commands {{{
            // B: Bar code field, create or edit
            // D: field, delete
            // H: Human-readable field, create or edit
            // I: Interpretive field, edit
            // L: Line field, create or edit
            // U: User-Defined character field, create or edit
            // W: Box field, create or edit
            // }}}
            // Page editing commands {{{
            // e: data source for format in a page, define
            // M: format position in a page, assign
            // m: format position from page, delete
            // O: format offset within a page, define
            // q: format direction in a page, define
            // }}}
            // Programming Commands {{{
            // A: Format, Create or edit
            // E: Format, erase
            // F: Format, create or edit
            // G: User-defined characte, clear or create
            // J: Outline font, clear or create
            // N: Current edit session, save
            // R: Program mode, exit
            reg = new Regex("R;|R");
            matches = reg.Matches(code);
            if(matches.Count==1) {
                this._commandType = IplParser.printMode;
                return true;
            }
            // S: page, create or edit
            // s: page delete
            // T: Bitmap userèdefined font, clear or define
            // }}}
            // UDC editing commands {{{
            // u: graphix or UDC, define
            // x: bitmap cell width for graphic or UDF, define
            // y: bitmap cell height for graphic or UDF, define
            // j: Outline font, download
            // J: outline font, clear or create
            // }}}
            // print mode commands {{{
            // <SI>a: audible alarm, enable or disable
            // <SI>A: control panel access permission, set
            // <SI>b: takeup motor torque, increase
            // <SI>c: cutter, enable or disable
            // <SI>C: emulation or advanced mode on power-up
            // <SI>d: Dark adjust, set
            // <SI>D: end-of-print skip distance, set
            // <SI>e: media fault recovery mode, set
            // <SI>f: label rest point, adjust
            // <SI>F: top of form, set
            // <SI>g: media sensitivity, select
            // <SI>h: printhead pressure, set
            // <SI>H: printhead pressure, set
            // <SI>i: IBM language translation, enable or disable
            // <SI>I: number of image bands, set
            // <SI>l: printer language, select
            // <SI>L: maximum label length, set
            // <SI>N: amount of storage, define
            // <SI>O: online or offline on power-up
            // <SI>p: pin 11/20 protocol, set
            // <SI>r: label retract distance, set
            // <SI>R: label retract, enable or disable
            // <SI>s: interlabel ribbon save, enable or disable
            // <SI>S: print sped, set
            // <SI>t: self-strip, enable or disable
            // <SI>T: label stock type, select
            // <SI>U: printhead test parameters, set
            // <SI>W: label width, set
            // <SI>z: slash zero, enable or disable
            // <SI>Z: ribbon save zones, set
            // }}}
            // print commands {{{
            // <ACK>: first data entry field, select
            // <BS>: warm boot
            // <CAN>: clear all data
            // <CR>: next data entry field, select
            // <DEL>: clear data from current field
            // <ESC>c: emulation mode, select
            // <ESC>C: advances mode, select
            // <ESC>D: field decrement, set
            // <ESC>E: format, select
            // <ESC>F: field, select
            // <ESC>g: direct graphics mode, select
            // <ESC>G: page, select
            // <ESC>H: printhead parameters, transmit
            // <ESC>I: field increment, set
            // <ESC>m: memory usage, transmit
            // <ESC>M: program number, transmit
            // <ESC>N: increment and decrement, disable
            // <ESC>O: options selected, transmit
            // <ESC>p: configuration parameters, transmit
            // <ESC>P: program mode, enter
            if(code == "<ESC>P") {
                this._commandType = IplParser.programMode;
                return true;
            }
            // <ESC><SP>: start and stop codes (code 39), print
            // <ESC>T: test and service mode, enter
            // <ESC>u: user-defines characters, transmit
            // <ESC>v: font, transmit
            // <ESC>x: format, transmit
            // <ESC>y: page, transmit
            // <ESC>Z: user-defined tables, transmit
            // <ETB>: print
            if(code == "<ETB>") {
                return false;
            }
            // <FF>: form feed
            // <FS>: numeric field separator
            // <GS>: alphanumeric field separator
            // <LF>: command terminator 2
            // <NUL>: command terminator 1
            // <RS>: quantity count, set
            // <SO>: cut
            // <SUB>: data shift-international characters
            // <US>: batch count, set
            // }}}
            // protocol modification commands {{{
            // <EOT>: postamble, set
            // <ESC>d: auto-transmit 2, enable
            // <ESC>e: auto-transmit 3, enable
            // <ESC>j: auto transmit 1, enable
            // <ESC>k: auto transmit 1, 2 and 3, disable
            // <SI>p: pin 11/20 protocol, set
            // <SI>P: communication port configuration, set
            // <ESC><SYN>: message delay, set
            // <SOH>: preamble, set
            // <SYN>: intercharacter delay, set
            // }}}
            // Test and service commands {{{
            // ;: command terminator
            // A: ambient temperature, transmit
            // B: printhead resistance test, begin
            // C: pitch label, print
            // D: factory defaults, reset
            // f: formats, print
            // g: user-defined characters and graphics, print
            // G: transmissive sensor value, transmit
            // h: hardware configuration label, print
            // K: dark adjust
            // L: label path open sensor value, transmit
            // M: reflective sensor value, transmit
            // p: pages, print
            // P: printhead temperature sensor value, transmit
            // Q: print quality label, print
            // R: test and service mode, exit
            // s: software configuratin label, print
            // S: printhead resistance values, transmit
            // t: user-defined fints, print
            // T: label taken sensor value, transmit
            // U: 12 volt supply value, transmit
            // V: printhead volt supply value, transmit
            // }}}

            return false;
        }
        
        // }}}
        // IplParser::_parseElementDefinition() {{{
        
        /// <summary>Parse the code witch define an element.</summary> 
        /// <param name="code">An Ipl instruction</param> 
        /// <returns>boolean</returns> 
        private bool _parseElementDefinition(string code) {
            Match result;
            Regex reg;
            int index = 0;
            char elementType = ' ';

            reg = new Regex(@"([HBLUW])(\d+),(.+)[;]");
            result = reg.Match(code);

            if(result.Success) {
                elementType = System.Convert.ToChar(result.Groups[1].Value);
                index = System.Convert.ToInt32(result.Groups[2].Value);
            } else {
                reg = new Regex(@"([HBLUW])(\d+)[;]");
                result = reg.Match(code);
                if(result.Success) {
                    elementType = System.Convert.ToChar(result.Groups[1].Value);
                    index = System.Convert.ToInt32(result.Groups[2].Value);
                }
            }

            if(this._commandType == IplParser.programMode) {
                double fieldOriginX = 0;
                double fieldOriginY = 0;
                string fieldDirection = "";
                double height = 0;
                double width = 0;
                // o: Field Origin, Define {{{
                reg = new Regex(@"[o](\d+),(\d+)[;]");
                result = reg.Match(code);
                if(result.Success) {
                    fieldOriginX = System.Convert.ToDouble(result.Groups[1].Value);
                    fieldOriginY = System.Convert.ToDouble(result.Groups[2].Value);
                }
                // }}}
                // f: field direction, define {{{
                reg = new Regex(@"([f]\d)[;]");
                result = reg.Match(code);
                if(result.Success) {
                    fieldDirection = System.Convert.ToString(result.Groups[1].Value);
                }
                // }}}
                // h: Height Magnification of bar,Box, or UDC, Define {{{
                reg = new Regex(@"[h](\d+)[;]");
                result = reg.Match(code);
                if(result.Success) {
                    height = System.Convert.ToDouble(result.Groups[1].Value);
                }
                // }}}
                // w: Width of Line, Box, Bar, or Character, define {{{
                reg = new Regex(@"[w](\d+)[;]");
                result = reg.Match(code);
                if(result.Success) {
                    width = System.Convert.ToDouble(result.Groups[1].Value);
                }
                // }}}

                if(elementType == 'B') {
                    // barcode element {{{
                    BarcodeElement element = new BarcodeElement(index, fieldOriginX, 
                            fieldOriginY, fieldDirection, width, height);
                    // c: Bar Code, Select Type
                    // bar code type : cn[,m1][,m2][,m3]
                    // n is the symbologie and m1, m2 and m3 are modifiers for that
                    // symbologie - cf page 157 spec ipl.
                    reg = new Regex(@"[c](\d+)");
                    result = reg.Match(code);
                    if(result.Success) {
                        element.symbologie = System.Convert.ToInt16(result.Groups[1].Value);
                    }
                    // modifiers : m
                    reg = new Regex(@"[m](\d+)");
                    result = reg.Match(code);
                    if(result.Success) {
                        element.modifiers = System.Convert.ToInt16(result.Groups[1].Value);
                    }
                    // d: Field Data, Define Source
                    reg = new Regex(@"[d](\d+),(.+)[;]");
                    result = reg.Match(code);
                    if(result.Success) {
                        if(result.Groups[1].Value == "3") {
                            // XXX trouver la regex qui évite le split
                            element.data = result.Groups[2].Value.Split(';')[0];
                        } else if(result.Groups[1].Value == "2") {
                            element.data = result.Groups[2].Value.Split(';')[0].Split(',')[0];
                        }
                    } else {
                        element.waitData = true;
                    }
                    // i: Interpretive Field, Enable or Disable
                    reg = new Regex(@"[i](\d+)");
                    result = reg.Match(code);
                    if(result.Success) {
                        element.interpretive = System.Convert.ToInt16(result.Groups[1].Value);
                    }
                    // p: Code39 Prefic Character, Define
                    // r: Character Rotation or Bar Code Ratio, define
                    reg = new Regex(@"[r](\d+)");
                    result = reg.Match(code);
                    if(result.Success) {
                        element.ratio = System.Convert.ToInt16(result.Groups[1].Value);
                    }
                    this._elements.Add(element);
                    // }}}
                } else if(elementType == 'H') {
                    // HumanReadable element {{{
                    HumanReadableElement element = new HumanReadableElement(
                            index, fieldOriginX, fieldOriginY, fieldDirection, 
                            width, height);
                    // b: Border around human-readable text, define
                    // c: Font type, select (cn,m) m est l'espace entre les
                    // chars
                    reg = new Regex(@"[c](\d+)[;]");
                    result = reg.Match(code);
                    if(result.Success) {
                        element.font = element.getFont(System.Convert.ToInt32(result.Groups[1].Value));
                    }
                    // d: field data, define source
                    reg = new Regex(@"[d](\d+),(.+)[;]");
                    result = reg.Match(code);
                    if(result.Success) {
                        if(result.Groups[1].Value == "3") {
                            // XXX trouver la regex qui évite le split
                            element.data = result.Groups[2].Value.Split(';')[0];
                        } else if(result.Groups[1].Value == "2") {
                            element.data = result.Groups[2].Value.Split(';')[0].Split(',')[0];
                        }
                    } else {
                        element.waitData = true;
                    }
                    // g: pitch size, set
                    // k: point size, set
                    // r: character rotation or bar code ratio, define
                    reg = new Regex(@"[r](\d+)");
                    result = reg.Match(code);
                    if(result.Success) {
                        element.characterRotation = System.Convert.ToInt16(result.Groups[1].Value);
                    }
                    this._elements.Add(element);
                    // }}}
                } else if(elementType == 'L') {
                    // Line element {{{
                    LineBoxElement element = new LineBoxElement(index, 
                            IplElement.Line, fieldOriginX, fieldOriginY, 
                            fieldDirection, width, height);
                    // l: length of line or box field, define
                    reg = new Regex(@"[l](\d+)");
                    result = reg.Match(code);
                    if(result.Success) {
                        element.length = System.Convert.ToDouble(result.Groups[1].Value);
                    }
                    this._elements.Add(element);
                    // }}}
                } else if(elementType == 'U') {
                    // UDCs element {{{
                    // XXX not yet implemented
                    //UDCsElement element = new UDCsElement(index);
                    // c: Graphic, select
                    // }}}
                } else if(elementType == 'W') {
                    // Box element {{{
                    LineBoxElement element = new LineBoxElement(index, 
                            IplElement.Box, fieldOriginX, fieldOriginY, 
                            fieldDirection, width, height);
                    // l: length of line or box field, define
                    reg = new Regex(@"[l](\d+)");
                    result = reg.Match(code);
                    if(result.Success) {
                        element.length = System.Convert.ToDouble(result.Groups[1].Value);
                    }
                    this._elements.Add(element);

                    // XXX not yet implemented
                    //BoxElement element = new BoxElement(index);
                    // l: Length of line or box field, Define
                    // }}}
                }
                // Bitmap User-Defined Font Editing Commands
                // t: User-Defined Font Character, Create
                // u: Graphic or UDC, Define
                // X: Character Bitmap Origin Offset, Define
                // x: Bitmap Cell Width for Graphic or UDF, Define
                // y: Bitmap Cell Height for Graphic or UDF, Define
                // Z: Font Character Width, Define
                // z: Intercharacter Space for UDF, Define
            
                // Interpretive fields editing commands 
                // b: border around human-readable text, define
                // c: font type, select
                // g: pitch size, set
                // k: point size, set
            } else if(this._commandType == IplParser.printMode) {
            }
            return true;
        }
        
        // }}}
    }
}
