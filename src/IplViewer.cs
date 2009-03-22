// author:    guillaume l. <guillaume@geelweb.org>
// license:   http://opensource.org/licenses/bsd-license.php BSD License
// copyright: copyright (c) 2007-2009, guillaume luchet

using System;
using Gtk;
using IplViewer.Gui;

/// <summary>IplViewer
/// <para>IplViewer main class</para>
/// </summary>
class IplViewerMain {

    /// <summary>Main function</summary>
    /// <param name="args">Ipl file to load</param> 
    /// <returns>void</returns> 
    static void Main(string[] args) {
        Application.Init();
        MainWindow win = new MainWindow();

        if(args.Length > 0) {
            win.loadIPLFile(args[0]);
        }

        Application.Run();
    }
}
