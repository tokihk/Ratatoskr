set CD_OLD=%CD%

cd /d %~dp0
gplex /out:"ExpressionLexer.cs" "ExpressionLexer.l"
gppg /gplex /no-lines "ExpressionParser.y" > "ExpressionParser.cs"
cd /d %CD_OLD%
