#!/bin/bash
cat <<EOF >.gitignore
# игнорирование всех файлов расширений:
*.log 
*.tmp
*.cache
*.csproj
*~

# игнорирование всех каталогов:
.obsidian/
.vs/
.vscode/
Debug/
EOF
