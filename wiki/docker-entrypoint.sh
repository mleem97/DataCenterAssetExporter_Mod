#!/bin/sh
set -e
# Prefer wiki subfolder when the repo root is mounted at /app (root also has package.json).
# Otherwise use /app when only ./wiki is bound there (compose: ./wiki:/app).
if [ -f /app/wiki/package.json ]; then
  cd /app/wiki
elif [ -f /app/package.json ]; then
  cd /app
else
  echo "docker-entrypoint: no package.json under /app or /app/wiki." >&2
  echo "Run docker compose from the repository root so ./wiki is mounted, then: docker compose build --no-cache docs" >&2
  exit 1
fi
exec "$@"
