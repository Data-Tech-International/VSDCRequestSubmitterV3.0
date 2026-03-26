#!/bin/bash
# copilot-pre-tool-check.sh
# Validates Copilot agent tool invocations before execution.
# Reads JSON from stdin, outputs permission decision as JSON.

set -euo pipefail

INPUT=$(cat)

# Extract tool name and arguments using python3 (jq may not be available)
eval "$(echo "$INPUT" | python3 -c "
import sys, json
try:
    data = json.load(sys.stdin)
except Exception:
    data = {}
tool = data.get('tool') or data.get('toolName') or 'unknown'
args = data.get('args') or data.get('arguments') or data.get('input') or ''
if isinstance(args, dict):
    args = json.dumps(args)
# Shell-escape single quotes
tool = tool.replace(\"'\", \"'\\\"'\\\"'\")
args = str(args).replace(\"'\", \"'\\\"'\\\"'\")
print(f\"TOOL='{tool}'\")
print(f\"ARGS='{args}'\")
" 2>/dev/null || echo "TOOL='unknown'; ARGS=''")"

# Protected files that agents must not delete
PROTECTED_FILES="\.sln$|\.csproj$|App\.config$|packages\.config$|\.editorconfig$|global\.json$|\.gitignore$|\.gitattributes$"

# Check for dangerous operations
case "$TOOL" in
  delete_file|remove|rm)
    if echo "$ARGS" | grep -qE "$PROTECTED_FILES"; then
      echo "{\"permissionDecision\":\"deny\",\"reason\":\"Deletion of protected project files is not allowed.\"}"
      exit 0
    fi
    ;;
  shell|bash|terminal|execute)
    # Block rm -rf on project root or .git
    if echo "$ARGS" | grep -qE "rm\s+-rf\s+(\.|/|\.git)"; then
      echo "{\"permissionDecision\":\"deny\",\"reason\":\"Destructive recursive deletion is blocked.\"}"
      exit 0
    fi
    # Block modifications to hooks directory
    if echo "$ARGS" | grep -qE "(edit|write|rm|mv).*\.github/hooks/"; then
      echo "{\"permissionDecision\":\"deny\",\"reason\":\"Modification of Copilot hooks is not allowed.\"}"
      exit 0
    fi
    ;;
  edit|write_file)
    # Block editing hooks config
    if echo "$ARGS" | grep -qE "\.github/hooks/"; then
      echo "{\"permissionDecision\":\"deny\",\"reason\":\"Modification of Copilot hooks is not allowed.\"}"
      exit 0
    fi
    ;;
esac

# Allow by default
echo "{\"permissionDecision\":\"allow\"}"
exit 0
