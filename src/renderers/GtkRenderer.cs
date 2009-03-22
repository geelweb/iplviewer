// author:    guillaume l. <guillaume@geelweb.org>
// license:   http://opensource.org/licenses/bsd-license.php BSD License
// copyright: copyright (c) 2007-2009, guillaume luchet

using System;
using Gtk;
using Tools.Conversion;
using Tools.Preferences;
using Ipl.Elements;
using Barcode;

namespace IplViewer.Renderers {
    /// <summary>GtkRenderer
    /// <para>Class to render label using Gtk</para>
    /// </summary>
    public class GtkRenderer {
        // Properties {{{
        private DrawingArea _da;
        private Pango.Layout _layout;
        // }}}
        //GtkRenderer::GtkRenderer() {{{

        /// <summary>Constructor</summary>
        public GtkRenderer(DrawingArea da, Pango.Layout layout) {
            this._da = da;
            this._layout = layout;
        }

        // }}}
        // GtkRenderer::render() {{{
        
        /// <summary>render the label</summary>
        public void render(System.Collections.ArrayList iplElms) {
           //Preferences pref = new Preferences();
            // draw the label's borders
            // XXX permettre de choisir si les bords doivent être afficher ou
            // non
            /*this._da.GdkWindow.DrawLine(this._da.Style.BaseGC(StateType.Normal), 
                    (int)Conversion.mm2px(0), (int)Conversion.mm2px(0), 
                    (int)Conversion.mm2px(pref.labelWidth), (int)Conversion.mm2px(0));
            this._da.GdkWindow.DrawLine(this._da.Style.BaseGC(StateType.Normal), 
                    (int)Conversion.mm2px(pref.labelWidth), (int)Conversion.mm2px(0), 
                    (int)Conversion.mm2px(pref.labelWidth), (int)Conversion.mm2px(pref.labelHeight));
            this._da.GdkWindow.DrawLine(this._da.Style.BaseGC(StateType.Normal), 
                    (int)Conversion.mm2px(pref.labelWidth), (int)Conversion.mm2px(pref.labelHeight), 
                    (int)Conversion.mm2px(0), (int)Conversion.mm2px(pref.labelHeight));
            this._da.GdkWindow.DrawLine(this._da.Style.BaseGC(StateType.Normal), 
                    (int)Conversion.mm2px(0), (int)Conversion.mm2px(pref.labelHeight), 
                    (int)Conversion.mm2px(0), (int)Conversion.mm2px(0));
            */
            foreach(IplElement element in iplElms) {
                this.renderElement(element);
            }

        } 
        
        // }}}
        // GtkRenderer::renderElement() {{{

        public void renderElement(IplElement elm) {
            if(elm.fieldType == IplElement.Line) {
                this.renderLine((LineBoxElement)elm);
            } else if(elm.fieldType == IplElement.BarCode) {
                this.renderBarcode((BarcodeElement)elm);
            } else if(elm.fieldType == IplElement.HumanReadable) {
                this.renderHumanReadable((HumanReadableElement)elm);
            } else if(elm.fieldType == IplElement.Box) {
                this.renderBox((LineBoxElement)elm);
            }
        }

        // }}}
        // GtkRenderer::renderLine() {{{

        public void renderLine(LineBoxElement elm) {
            double x1 = elm.fieldOriginX;
            double y1 = elm.fieldOriginY;
            int angle = Conversion.iplRot2degree(elm.fieldDirection);
            switch (angle) {
	            case 0:
	                x1 = elm.fieldOriginX + elm.length;
	                break;
	            case 90:
	                y1 = elm.fieldOriginY - elm.length;
	                break;
	            case 180:
	                x1 = elm.fieldOriginX - elm.length;
	                break;
                case 270:
	                y1 = elm.fieldOriginY + elm.length;
	                break;
	        }
            this._da.GdkWindow.DrawLine(this._da.Style.BaseGC(StateType.Normal), 
                    (int)Conversion.dot2px(elm.fieldOriginX), (int)Conversion.dot2px(elm.fieldOriginY), 
                    (int)Conversion.dot2px(x1), (int)Conversion.dot2px(y1));

        }

        // }}}
        // GtkRenderer::renderHumanReadable() {{{

        public void renderHumanReadable(HumanReadableElement elm) {
            elm.cleanText();


            // font
            Pango.FontDescription font = Pango.FontDescription.FromString(elm.font.name);
            if(elm.font.size > 0) {
                font.Size = elm.font.size * 1024;
            }
            
            this._layout.FontDescription = font;
            this._layout.SetMarkup("<span color=" + (char)34 + "black" + (char)34 +">" + elm.data + "</span>");
        
            // rotate the text
            int angle = Conversion.iplRot2degree(elm.fieldDirection);
            Pango.Matrix m = Pango.Matrix.Identity;
            m.Rotate(angle);
            this._layout.Context.Matrix = m;

            double strWidth = elm.width * elm.data.Length;

            // Calcul the real position of the text using the rotation
            double realX = elm.fieldOriginX;
            double realY = elm.fieldOriginY;
            if(angle == 90) {
                realX = realX - elm.height;
                realY = realY - strWidth;
            } else if(angle == 180) {
                realX = realX - strWidth;
                realY = realY - elm.height;
            } else if(angle == 270) {
                realX = realX - elm.height;
            }
            // draw the text
            this._da.GdkWindow.DrawLayout(this._da.Style.BaseGC(StateType.Normal), 
                    (int)Conversion.dot2px(realX), (int)Conversion.dot2px(realY), this._layout);

        }

        // }}}
        // GtkRenderer::renderBarcode() {{{

        public void renderBarcode(BarcodeElement elm) {
            if(elm.symbologie==0) {
                Barcode39 barcode = new Barcode39();
                //barcode.BarHeight=Conversions.dot2mm(this.heightMagnification);
                barcode.Code = elm.data;
                barcode.BarHeight = (float)elm.height;
                barcode.render((int)elm.fieldOriginX,(int)elm.fieldOriginY, this._da);
            } else {
                Console.WriteLine("Barcode "+elm._symbologieMap[elm.symbologie]+" are not implemented yet");
            }

        }

        // }}}
        // GtkRenderer::renderBox() {{{
        
        public void renderBox(LineBoxElement elm) {
            this._da.GdkWindow.DrawLine(this._da.Style.BaseGC(StateType.Normal), 
                    (int)Conversion.dot2px(elm.fieldOriginX), 
                    (int)Conversion.dot2px(elm.fieldOriginY), 
                    (int)Conversion.dot2px(elm.fieldOriginX + elm.length), 
                    (int)Conversion.dot2px(elm.fieldOriginY));
            this._da.GdkWindow.DrawLine(this._da.Style.BaseGC(StateType.Normal), 
                    (int)Conversion.dot2px(elm.fieldOriginX + elm.length), 
                    (int)Conversion.dot2px(elm.fieldOriginY),
                    (int)Conversion.dot2px(elm.fieldOriginX + elm.length), 
                    (int)Conversion.dot2px(elm.fieldOriginY + elm.height));
            this._da.GdkWindow.DrawLine(this._da.Style.BaseGC(StateType.Normal), 
                    (int)Conversion.dot2px(elm.fieldOriginX + elm.length), 
                    (int)Conversion.dot2px(elm.fieldOriginY + elm.height),
                    (int)Conversion.dot2px(elm.fieldOriginX), 
                    (int)Conversion.dot2px(elm.fieldOriginY + elm.height));
            this._da.GdkWindow.DrawLine(this._da.Style.BaseGC(StateType.Normal), 
                    (int)Conversion.dot2px(elm.fieldOriginX), 
                    (int)Conversion.dot2px(elm.fieldOriginY + elm.height),
                    (int)Conversion.dot2px(elm.fieldOriginX), 
                    (int)Conversion.dot2px(elm.fieldOriginY));

        }
        
        // }}}
    }
}
