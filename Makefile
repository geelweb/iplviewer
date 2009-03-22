# Makefile
#
# build iplViewer's libraries and executable.
#
# $Source$
# version:   CVS: $Id$
# author:    guillaume luchet <guillaume@geelweb.org>
# copyright: copyright (c) 2007-2009, guillaume luchet

GTK_SHARP=gtk-sharp-2.0

APP_NAME=iplViewer
APP_VERSION=0.1
APP_STABILITY=alpha

RELEASE=${APP_NAME}_${APP_VERSION}_${APP_STABILITY}.exe

all: init build

build:
    @echo "\n -- compile src..."
    @mcs -pkg:${GTK_SHARP} -recurse:'src/*.cs' -out:bin/${RELEASE} \
        -doc:doc/xml/${APP_NAME}.xml
    
    @echo "\n -- generate xml documentation..."
    @monodocer -path:doc/monodocer -name:${APP_NAME} -pretty \
        -importslashdoc:doc/xml/${APP_NAME}.xml \
        -assembly:bin/${RELEASE}
    
    @echo "\n -- generate html documentation..."
    @monodocs2html --source doc/monodocer/ --dest doc/html/

init:
    @sh init.sh

