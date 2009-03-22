// author:    guillaume l. <guillaume@geelweb.org>
// license:   http://opensource.org/licenses/bsd-license.php BSD License
// copyright: copyright (c) 2007-2009, guillaume luchet
// version:   CVS: $Id$
// source:    $Source$

using System;
using Gtk;
using Ipl.Parser;
using Tools.Preferences;
using IplViewer.Renderers;

namespace IplViewer.Gui {
    
    /// <summary>MainWindow
    /// <para>Extend Gtk.Window to buid the main application window</para>
    /// </summary>
    public class MainWindow : Window {
        // Properties {{{

        /// <summary>Window's Pango.Layout.</summary> 
        public Pango.Layout layout;

        /// <summary>Application's preferences.</summary> 
        public Preferences pref = new Preferences();

        /// <summary>A Gtk.VBox to the window's widgets.</summary> 
        private VBox _vBox;

        /// <summary>IplParser object.</summary> 
        private IplParser _parser = new IplParser();

        // }}}
        //MainWindow::MainWindow() {{{

        /// <summary>Constructor</summary>
        /// <returns>void</returns>  
        public MainWindow() : base("Ipl Viewer") {
            this.Resize(this.pref.mainWindowWidth, this.pref.mainWindowHeight);
            this.WindowPosition = Gtk.WindowPosition.Center;
            this.DeleteEvent += new DeleteEventHandler(onDeleteEvent);

            this._vBox = new VBox(false, 4);
            this._createMenuBar();
            this._createToolBar();

            this.layout = new Pango.Layout(this.PangoContext);

            DrawingArea drawArea = new DrawingArea();
            initDrawingArea(drawArea);
            this._vBox.PackStart(drawArea);

            Statusbar statusbar = new Statusbar();
            this._vBox.PackStart(statusbar, false, true, 0);

            this.Add(this._vBox);
            this.ShowAll();
        }

        // }}}
        // MainWindow::_createMenuBar() {{{
        
        /// <summary>Build main window's menu bar.</summary>  
        /// <returns>void</returns> 
        private void _createMenuBar() {
            MenuBar mBar = new MenuBar();
            AccelGroup group = new AccelGroup();
            this.AddAccelGroup(group);

            // file
            Menu mFile = new Menu();
            MenuItem miFile = new MenuItem("_File");
            miFile.Submenu = mFile;
            mBar.Append(miFile);
            // file->open
            ImageMenuItem miOpen = new ImageMenuItem(Stock.Open, group);
            miOpen.AddAccelerator("activate", group, 
                    new AccelKey(Gdk.Key.o, Gdk.ModifierType.ControlMask, 
                        AccelFlags.Visible));
            miOpen.Activated += new EventHandler(onMenuItemOpenActivate);
            mFile.Append(miOpen);
            // file->quit
            ImageMenuItem miExit  = new ImageMenuItem(Stock.Quit, group);
            miExit.AddAccelerator("activate", group, 
                    new AccelKey(Gdk.Key.q, Gdk.ModifierType.ControlMask, 
                        AccelFlags.Visible));
            miExit.Activated += new EventHandler(onMenuItemExitActivate);
            mFile.Append(miExit);
            // edit
            Menu mEdit = new Menu();
            MenuItem miEdit = new MenuItem("_Edit");
            miEdit.Submenu = mEdit;
            mBar.Append(miEdit);
            // edit->preferences
            ImageMenuItem miPref = new ImageMenuItem(Stock.Preferences, group);
            miPref.AddAccelerator("activate", group, 
                    new AccelKey(Gdk.Key.o, Gdk.ModifierType.ControlMask, 
                        AccelFlags.Visible));
            miPref.Activated += new EventHandler(onMenuItemPrefActivate);
            mEdit.Append(miPref);
            // Help
            Menu mQuestion = new Menu();
            MenuItem miQuestion = new MenuItem("_Help");
            miQuestion.Submenu = mQuestion;
            mBar.Append(miQuestion);
            // Help / About
            MenuItem miAbout = new MenuItem("_About");
            miAbout.Activated += new EventHandler(onMenuItemAboutActivate);
            mQuestion.Append(miAbout);
            
            this._vBox.PackStart(mBar, false, true, 0);
        }
        
        // }}}
        // MainWindow::_createToolBar() {{{
        
        /// <summary>Build main window's tool bar.</summary>
        /// <returns>void</returns> 
        private void _createToolBar() {
            Toolbar tb = new Toolbar();
            tb.ToolbarStyle = ToolbarStyle.Icons;

            ToolButton tbOpen = new ToolButton(Stock.Open);
            tbOpen.Clicked += onMenuItemOpenActivate;
            tb.Insert(tbOpen, 0);

            ToolButton tbPref = new ToolButton(Stock.Preferences);
            tbPref.Clicked += onMenuItemPrefActivate;
            tb.Insert(tbPref, 1);

            this._vBox.PackStart(tb, false, true, 0);
        }
        
        // }}}
        // MainWindow::onDeleteEvent() {{{
        
        /// <summary>Method called on delete event.</summary> 
        /// <param name="sender">object sender</param> 
        /// <param name="args">DeleteEventArgs</param> 
        /// <returns>void</returns> 
        public void onDeleteEvent(object sender, DeleteEventArgs args) {
            Application.Quit();
            args.RetVal = true;
        }
        
        // }}}
        // MainWindow::onExposeEvent() {{{
        
        /// <summary>Method called when the drawing area is exposed.</summary> 
        /// <param name="sender">object sender</param> 
        /// <param name="args">ExposeEventArgs</param> 
        /// <returns>void</returns> 
        public void onExposeEvent(object sender, ExposeEventArgs args) {
            DrawingArea area = (DrawingArea) sender; 
            if(this._parser.fileParsed) {
                new GtkRenderer(area, layout).render(this._parser.elements);
            }
        }
        
        // }}}
        // MainWindow::onMenuItemExitActivate() {{{
        
        /// <summary>Quit the applicatin when menu item Exit is clicked</summary> 
        /// <param name="sender">Object sender</param> 
        /// <param name="args">Event arguments</param> 
        /// <returns>void</returns> 
        public void onMenuItemExitActivate(object sender, EventArgs args) {
            Application.Quit();
        }
        
        // }}}
        // MainWindow::onMenuItemAboutActivate() {{{

        /// <summary>Show the about dialog.</summary>
        /// <param name="sender">The object who call the method</param>
        /// <param name="args">arguments of the event</param>
        /// <returns>void</returns>
        public void onMenuItemAboutActivate(object sender, EventArgs args) {
    	    Gtk.AboutDialog dialog = new Gtk.AboutDialog();
    			
	    	dialog.Authors = new String[] 
                {this.pref.appAuthor + " " + this.pref.appContact};
    		dialog.ProgramName = this.pref.appName;
    		dialog.Version = this.pref.appVersion;
    		dialog.Comments = this.pref.appDescription;
    		dialog.License = this.pref.appLicense + "\n\n" +
                this.pref.appCopyright + "\n\n" + this.pref.appLicenseDetail;
    		dialog.Copyright = this.pref.appCopyright;
    			
    		dialog.Run ();
        }

        // }}}
        // MainWindow::onMenuItemOpenActivate() {{{
		
        /// <summary>Open a file chooser dialog.</summary>
        /// <param name="sender">The object who call the method</param>
        /// <param name="args">arguments of the event</param>
        /// <returns>void</returns>
        public void onMenuItemOpenActivate(object sender, EventArgs args) {
    		FileChooserDialog dialog = new FileChooserDialog(
                    "select a file.",
    				this,
    				FileChooserAction.Open,
                    Stock.Cancel, ResponseType.Cancel,
                    Stock.Ok, ResponseType.Ok);
    			
    		dialog.LocalOnly = true;
    
    		ResponseType response = (ResponseType)dialog.Run();
	    	Uri uri = new Uri(dialog.Uri);
    		dialog.Destroy();
		
            if (response == ResponseType.Ok) {
                Console.WriteLine(uri.AbsolutePath);

                this.loadIPLFile(uri.AbsolutePath);
		    }	
	    }

        // }}}
        // MainWindow::onMenuItemPrefActivate() {{{
        
        /// <summary>Open the preferences window.</summary>
        /// <param name="sender">The object who call the method</param>
        /// <param name="args">arguments of the event</param>
        /// <returns>void</returns>
        public void onMenuItemPrefActivate(object sender, EventArgs args) {
            new PreferencesWindow();
        }
        
        // }}}
        // MainWindow::loadIPLFile() {{{

        /// <summary>Load an IPL file, building an IplParser and render it.</summary>
        /// <param name="file">IPL file</param>
        /// <returns>void</returns>
        public void loadIPLFile(string file) {
            this._parser = new IplParser(file);
        }

        // }}}
        // MainWindow::initDrawingArea() {{{

        /// <summary>Init the drawing area and put some layout's property</summary>
        /// <param name="area">The drawing area</param> 
        /// <returns>void</returns>
        public void initDrawingArea(DrawingArea area) {
            area.SetSizeRequest(this.pref.mainWindowWidth, this.pref.mainWindowHeight);
            area.ExposeEvent += onExposeEvent;
            // define some colors
            Gdk.Color white = new Gdk.Color();
            Gdk.Color black = new Gdk.Color();
            Gdk.Color.Parse("white", ref white);
            Gdk.Color.Parse("black", ref black);
            // define area colors
            area.ModifyBg(StateType.Normal, white);
            area.ModifyBase(StateType.Normal, black);
            // XXX move out ? 
            this.layout.Width = Pango.Units.FromPixels(this.pref.mainWindowWidth);
            this.layout.Wrap = Pango.WrapMode.Word;
            this.layout.Alignment = Pango.Alignment.Left;           
        }

        // }}}
    }
}
