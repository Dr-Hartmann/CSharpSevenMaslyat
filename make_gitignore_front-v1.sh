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

# игнорирование корневых файлов и каталогов:
/MainProject/
/TestSection/Backend/
/TestSection/Database/
/DataBase/
/LICENSE
/README.md
EOF