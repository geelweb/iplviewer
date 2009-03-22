// author:    guillaume l. <guillaume@geelweb.org>
// license:   http://opensource.org/licenses/bsd-license.php BSD License
// copyright: copyright (c) 2007-2009, guillaume luchet
// version:   CVS: $Id$
// source:    $Source$

using System;
using Gtk;

namespace IplViewer.Gui {
    /// <summary>PreferencesWindow
    /// <para>Window to set application preferences</para>
    /// </summary>
    public class PreferencesWindow : Window {
        // Properties {{{

        // }}}
        //PreferencesWindow::PreferencesWindow() {{{

        // <summary>Constructor</summary>
        public PreferencesWindow() : base("Preferences") {
            
        }

        // }}}
        // PreferencesWindow::savePreferences() {{{
        
        /// <summary>Save preferences in local file.</summary> 
        /// <returns>boolean</returns> 
        public bool savePreferences() {
            return false;
        }
        
        // }}}
    }
}
