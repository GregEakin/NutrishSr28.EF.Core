#!/bin/bash
set -e

# Parse the connection string from environment variable
CONN_STR="${ConnectionStrings__postgres:-$CONNECTIONSTRINGS__POSTGRES}"
if [ -z "$CONN_STR" ]; then
  echo "ConnectionStrings__postgres environment variable not set."
  exit 1
fi

HOST=$(echo "$CONN_STR" | sed -n 's/.*Host=\([^;]*\).*/\1/p')
PORT=$(echo "$CONN_STR" | sed -n 's/.*Port=\([^;]*\).*/\1/p')
DB=$(echo "$CONN_STR" | sed -n 's/.*Database=\([^;]*\).*/\1/p')
USER=$(echo "$CONN_STR" | sed -n 's/.*Username=\([^;]*\).*/\1/p')
PASS=$(echo "$CONN_STR" | sed -n 's/.*Password=\([^;]*\).*/\1/p')

# Fallbacks
PORT=${PORT:-5432}
DB=${DB:-postgres}

# Create servers.json for pgAdmin import
mkdir -p /pgadmin4/servers
cat >/pgadmin4/servers/servers.json <<EOF
{
  "Servers": {
    "Postgres": {
      "Name": "Postgres",
      "Group": "Servers",
      "Host": "$HOST",
      "Port": $PORT,
      "MaintenanceDB": "$DB",
      "Username": "$USER",
      "SSLMode": "prefer",
      "PassFile": "/pgadmin4/servers/passfile"
    }
  }
}
EOF

# Create passfile for passwordless login in pgAdmin
cat >/pgadmin4/servers/passfile <<EOF
*:*:*:$USER:$PASS
EOF
chmod 600 /pgadmin4/servers/passfile

echo "pgAdmin server registration file created."