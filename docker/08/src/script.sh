#!/bin/bash

printenv
pwd
ls -la

for ((count=1; count<=2; count++)); do
    echo "Count: $count"
    sleep 3
done