#!/bin/bash
#
# Create the bin and doc directories.
#
# $Source$
# version:   CVS: $Id$
# author:    guillaume luchet <guillaume@geelweb.org>
# copyright: copyright (c) 2007-2009, guillaume luchet

LIST="bin doc doc/xml"
for dir in ${LIST} ;
do
    if [ ! -d ${dir} ] ; then
        mkdir ${dir}
    fi
done

