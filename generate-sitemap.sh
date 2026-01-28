#!/usr/bin/env bash

set -euo pipefail

ROOT_DIR="publish/wwwroot"
OUTPUT="$ROOT_DIR/sitemap.xml"
APPSETTINGS="$ROOT_DIR/appsettings.json"

# Extract BaseUrl from appsettings.json
BASE_URL=$(grep -o '"BaseUrl"[[:space:]]*:[[:space:]]*"[^"]*"' "$APPSETTINGS" \
  | sed -E 's/.*"BaseUrl"[[:space:]]*:[[:space:]]*"([^"]*)".*/\1/')

if [[ -z "$BASE_URL" ]]; then
  echo "BaseUrl not found in appsettings.json"
  exit 1
fi

# Normalize base URL (remove trailing slash)
BASE_URL="${BASE_URL%/}"

echo "Using BaseUrl: $BASE_URL"

# Write XML header
cat > "$OUTPUT" <<EOF
<?xml version="1.0" encoding="UTF-8"?>
<urlset xmlns="http://www.sitemaps.org/schemas/sitemap/0.9">
EOF

# Find index.html files
find "$ROOT_DIR" -type f -name "index.html" \
  ! -path "*/_content/*" \
  ! -path "*/_framework/*" \
  ! -path "*/css/*" \
  | while read -r file; do

    # Get directory path relative to wwwroot
    rel_dir=$(dirname "${file#$ROOT_DIR}")

    # Root index.html → "/"
    if [[ "$rel_dir" == "" || "$rel_dir" == "/" ]]; then
      url="$BASE_URL/"
    else
      url="$BASE_URL$rel_dir/"
    fi

    # Get last modified date in ISO 8601
    lastmod=$(date -u -r "$file" +"%Y-%m-%dT%H:%M:%SZ")

    cat >> "$OUTPUT" <<EOF
  <url>
    <loc>$url</loc>
  </url>
EOF
done

# Close XML
cat >> "$OUTPUT" <<EOF
</urlset>
EOF

echo "Sitemap generated at $OUTPUT"
