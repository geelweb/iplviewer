// author:    guillaume l. <guillaume@geelweb.org>
// license:   http://opensource.org/licenses/bsd-license.php BSD License
// copyright: copyright (c) 2007-2009, guillaume luchet
// version:   CVS: $Id$
// source:    $Source$

using System;

namespace Tools.Preferences {

    /// <summary>Define the application's preferences.</summary>
    public class Preferences {
        // application properties {{{

        /// <summary>Application window's width</summary>
        private int _mainWindowWidth = 400;
        
        /// <summary>Application window's height</summary>
        private int _mainWindowHeight = 300;

        // XXX put this in config file. User will can modify the label
        // sizes in a preference window.
        private int _labelWidth = 150;
        private int _labelHeight = 100;

        // }}}
        // getters {{{
        
        /// <summary>The window's width (read only)</summary>
        public int mainWindowWidth { get {return this._mainWindowWidth;} }
        
        /// <summary>The window's height (read only)</summary>
        public int mainWindowHeight { get {return this._mainWindowHeight;} }

        /// <summary>The label's default width (read only)</summary>
        public int labelWidth { get {return this._labelWidth;} }
        
        /// <summary>The label's default height (read only)</summary>
        public int labelHeight { get {return this._labelHeight;} }
        
        // }}}
        // some variables to transform in constants {{{

        private string _appName = "IplViewer";
        private string _appDescription = "\n\n";
        private string _appVersion = "0.0.1";
        private string _appAuthor = "Guillaume Luchet.";
        private string _appContact = "guillaume@geelweb.org";
        private string _appWebPage = "";
        private string _appCopyright = "Copytight © 2007, guillaume luchet.\nAll rights reserved.";
        private string _appLicense = "BSD Licence.";
        private string _appLicenseDetail = "" +
            "Redistribution and use in source and binary forms, with or without\n" +
            "modification, are permitted provided that the following conditions\n" +
            "are met:\n\n" +
            "\t* Redistributions of source code must retain the above copyright\n" +
            "\t  notice, this list of conditions and the following disclaimer.\n\n" +
            "\t* Redistributions in binary form must reproduce the above copyright\n" +
            "\t  notice, this list of conditions and the following disclaimer in the\n" +
            "\t  documentation and/or other materials provided with the distribution.\n\n" +
            "\t* Neither the name of the Xml2Pdf nor the names of its contributors\n" +
            "\t  may be used to endorse or promote products derived from this\n" +
            "\t  software without specific prior written permission.\n\n" +
            "THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND\n" +
            "CONTRIBUTORS \"AS IS\" AND ANY EXPRESS OR IMPLIED WARRANTIES,\n" +
            "INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF\n" +
            "MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE\n" +
            "DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR\n" +
            "CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,\n" + 
            "SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING,\n" +
            "BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR\n" +
            "SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS\n" +
            "INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY,\n" +
            "WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING\n" +
            "NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE\n" +
            "OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH\n" +
            "DAMAGE.";

        // }}}
        // constructor {{{

        /// <summary>Constructor.</summary>
        public Preferences() {
            // XXX load a config file
        }

        // }}}
        // some getters to the constants {{{

        /// <summary>The application's description (read only)</summary>
        public string appDescription { get { return this._appDescription;} }
        
        /// <summary>The application's name (read only)</summary>
        public string appName { get { return this._appName;} }
        
        /// <summary>The application's version (read only)</summary>
        public string appVersion { get { return this._appVersion;} }
        
        /// <summary>The application's authors (read only)</summary>
        public string appAuthor {  get {return this._appAuthor;} }
        
        /// <summary>The contact's informations (read only)</summary>
        public string appContact { get {return this._appContact;} }
        
        /// <summary>The application's web page (read only)</summary>
        public string appWebPage { get {return this._appWebPage;} }
        
        /// <summary>The application's copyright (read only)</summary>
        public string appCopyright { get {return this._appCopyright;} }
        
        /// <summary>The application's license (read only)</summary>
        public string appLicense { get {return this._appLicense;} }
        
        /// <summary>The application's license details (read only)</summary>
        public string appLicenseDetail { get {return this._appLicenseDetail;}}

        // }}}
    }
}
