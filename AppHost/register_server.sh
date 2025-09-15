#!/bin/bash
# Parse the connection string and register the server in pgAdmin
# Example connection string: Host=postgres;Port=5432;Database=postgres;Username=postgres;Password=yourpassword

CONN_STR="$ConnectionStrings__postgres"
HOST=$(echo $CONN_STR | sed -n 's/.*Host=\([^;]*\).*/\1/p')
PORT=$(echo $CONN_STR | sed -n 's/.*Port=\([^;]*\).*/\1/p')
DB=$(echo $CONN_STR | sed -n 's/.*Database=\([^;]*\).*/\1/p')
USER=$(echo $CONN_STR | sed -n 's/.*Username=\([^;]*\).*/\1/p')
PASS=$(echo $CONN_STR | sed -n 's/.*Password=\([^;]*\).*/\1/p')

# Use pgAdmin's CLI or REST API to register the server (see pgAdmin docs for details)
# This step is implementation-specific and may require additional setup.