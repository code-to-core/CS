#!/bin/sh

while read a
do
   echo $a | nc -q 1 127.0.0.1 12003
done
