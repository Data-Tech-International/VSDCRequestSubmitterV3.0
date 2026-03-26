#!/bin/bash
# check-secrets.sh - Scan staged files for hardcoded secrets
# Used as a pre-commit hook via Husky.NET
# Exit 0 = clean, Exit 1 = secrets found

set -euo pipefail

RED='\033[0;31m'
YELLOW='\033[1;33m'
NC='\033[0m' # No Color

# Get staged .cs files, or use arguments if provided
if [ $# -gt 0 ]; then
    FILES="$@"
else
    FILES=$(git diff --cached --name-only --diff-filter=ACM -- '*.cs' 2>/dev/null || true)
fi

if [ -z "$FILES" ]; then
    exit 0
fi

FOUND=0

# Filter out Designer.cs files (auto-generated content)
FILTERED_FILES=""
for f in $FILES; do
    if [[ "$f" == *.Designer.cs ]]; then
        continue
    fi
    if [ -f "$f" ]; then
        FILTERED_FILES="$FILTERED_FILES $f"
    fi
done

if [ -z "$FILTERED_FILES" ]; then
    exit 0
fi

check_pattern() {
    local label="$1"
    local pattern="$2"
    shift 2
    local files="$@"

    for f in $files; do
        # Run grep; skip lines containing known placeholder text
        matches=$(grep -nE "$pattern" "$f" 2>/dev/null \
            | grep -ivE "REPLACE WITH|TODO|EXAMPLE|PLACEHOLDER|CHANGEME|YOUR_.*_HERE|xxxx|sample" || true)

        if [ -n "$matches" ]; then
            while IFS= read -r line; do
                echo -e "${RED}[SECRET DETECTED]${NC} ${YELLOW}${label}${NC}"
                echo "  $f:$line"
            done <<< "$matches"
            FOUND=1
        fi
    done
}

echo "Scanning for hardcoded secrets..."

# 1. PAC codes — literal PAC values that look like real codes
check_pattern "Hardcoded PAC code" \
    'PAC\s*=\s*"[A-Za-z0-9]{8,}"' \
    $FILTERED_FILES

# 2. Hardcoded VSDC/TaxCore URLs in .cs source (App.config is fine)
check_pattern "Hardcoded VSDC/TaxCore URL" \
    'https?://[^"]*vsdc|https?://[^"]*taxcore' \
    $FILTERED_FILES

# 3. Certificate / PFX passwords
check_pattern "Certificate password" \
    'new\s+X509Certificate2\s*\(.*".*",\s*"[^"]+"' \
    $FILTERED_FILES

check_pattern "Hardcoded password" \
    'password\s*=\s*"[^"]+"' \
    $FILTERED_FILES

# 4. Connection strings in .cs files
check_pattern "Hardcoded connection string" \
    '(Server\s*=|Data Source\s*=|Initial Catalog\s*=)' \
    $FILTERED_FILES

# 5. Private keys
check_pattern "Embedded private key" \
    '-----BEGIN (RSA |EC )?PRIVATE KEY-----' \
    $FILTERED_FILES

# 6. API keys / secrets / tokens
check_pattern "Hardcoded API key/secret/token" \
    '(api[_-]?key|secret|token)\s*[:=]\s*"[^"]{8,}"' \
    $FILTERED_FILES

if [ "$FOUND" -eq 1 ]; then
    echo ""
    echo -e "${RED}Secrets detected! Commit blocked.${NC}"
    echo "Move sensitive values to App.config, environment variables, or a secrets manager."
    exit 1
fi

echo "No secrets found."
exit 0
