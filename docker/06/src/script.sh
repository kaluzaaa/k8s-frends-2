#!/bin/bash

pwd
ls -la

for ((count=1; count<=10; count++)); do
    echo "Count: $count"
    sleep 3
done