// http://www.quut.com/c/ANSI-C-grammar-y.html
// http://www.quut.com/c/ANSI-C-grammar-l-2011.html

%using System.Diagnostics;

%visibility internal

%namespace Ratatoskr.Scripts.Expression.Parser
%option noFiles

%{
public override void yyerror(string format, params object[] args)
{
//	Debug.WriteLine("Error: line {0} - " + format, yyline);
}
%}

O   [0-7]
D   [0-9]
NZ  [1-9]
L   [a-zA-Z_]
A   [a-zA-Z_0-9]
H   [a-fA-F0-9]
HP  (0[xX])
FS  (f|F|l|L)
IS  (((u|U)(l|L|ll|LL)?)|((l|L|ll|LL)(u|U)?))
CP  (u|U|L)
SP  (u8|u|U|L)
ES  ([a-fA-F0-9]+)
WS  [ \t\v\n\f]
Ty  ([0-9]{4})
TM  ([0][0-9]|1[0-2])
Td  ([0-2][0-9]|3[0-1])
Th  ([0-1][0-9]|2[0-3])
Tm  ([0-5][0-9])
Ts  ([0-5][0-9])
Tf  ([0-9]{3})

%{

%}

%%

// --- êÆêî(10êiêî) --------------------------
<INITIAL>[0]|([1-9][0-9]*) {
	yylval.term = new Terms.Term_Double(double.Parse(yytext));
	return (int)Tokens.VALUE_INTEGER;
}

// --- è¨êî ----------------------------------
<INITIAL>[0-9]+\.[0-9]+ {
	yylval.term = new Terms.Term_Double(double.Parse(yytext));
	return (int)Tokens.VALUE_DOUBLE;
}

// --- ïœêî/ä÷êî(1ëw) ------------------------
<INITIAL>[A-Za-z][A-Za-z0-9]* {
	yylval.term = new Terms.Term_Id(yytext);
	return (int)Tokens.VALUE_ID;
}

// --- ïœêî/ä÷êî(2ëw) ------------------------
<INITIAL>[A-Za-z][A-Za-z0-9]*[\.][A-Za-z][A-Za-z0-9]* {
	yylval.term = new Terms.Term_Id(yytext);
	return (int)Tokens.VALUE_ID;
}

// --- ïœêî/ä÷êî(3ëw) ------------------------
<INITIAL>[A-Za-z][A-Za-z0-9]*[\.][A-Za-z][A-Za-z0-9]*[\.][A-Za-z][A-Za-z0-9]* {
	yylval.term = new Terms.Term_Id(yytext);
	return (int)Tokens.VALUE_ID;
}

// --- ï∂éöóÒ ["xxx"] -------------------------
<INITIAL>\"[^\"]*\" {
	yylval.term = new Terms.Term_Text(yytext.Substring(1, yytext.Length - 2));
	return (int)Tokens.VALUE_TEXT;
}

// --- 16êiÉeÉLÉXÉg[%xxx%] ----------------
<INITIAL>%[^%]*% {
	yylval.term = new Terms.Term_Binary(yytext.Substring(1, yytext.Length - 2));
	return (int)Tokens.VALUE_BINTEXT;
}

// --- ê≥ãKï\åª -------------------------------
<INITIAL>\/[^/]*\/ {
	yylval.term = new Terms.Term_Regex(yytext.Substring(1, yytext.Length - 2));
	return (int)Tokens.VALUE_PATTERN;
}

// --- ê‚ëŒéûä‘ [$yyyy/MM/dd hh:mm:ss.fff$] ---
<INITIAL>\${Ty}(/{TM}){0,1}(/{Td}){0,1}([ ]{Th}){0,1}(:{Tm}){0,1}(:{Ts}){0,1}([\.]{Tf}){0,1}\$ {
	yylval.term = new Terms.Term_DateTime(yytext.Substring(1, yytext.Length - 2));
	return (int)Tokens.VALUE_TIME;
}

// --- éûä‘ä‘äu [$hh:mm:ss.fff$] ---
<INITIAL>\${Th}(:{Tm}){0,1}(:{Ts}){0,1}([\.]{Tf}){0,1}\$ {
	yylval.term = new Terms.Term_TimeSpan(yytext.Substring(1, yytext.Length - 2));
	return (int)Tokens.VALUE_TIME;
}

<INITIAL>"="  { return (int)Tokens.ARMOP_SET;      }
<INITIAL>"+"  { return (int)Tokens.ARMOP_ADD;      }
<INITIAL>"-"  { return (int)Tokens.ARMOP_SUB;      }
<INITIAL>"*"  { return (int)Tokens.ARMOP_MUL;      }
<INITIAL>"/"  { return (int)Tokens.ARMOP_SUB;      }
<INITIAL>"%"  { return (int)Tokens.ARMOP_REM;      }

<INITIAL>">"  { return (int)Tokens.RELOP_GREATER;      }
<INITIAL>"<"  { return (int)Tokens.RELOP_LESS;         }
<INITIAL>">=" { return (int)Tokens.RELOP_GREATEREQUAL; }
<INITIAL>"<=" { return (int)Tokens.RELOP_LESSEQUAL;    }

<INITIAL>"==" { return (int)Tokens.RELOP_EQUAL;   }
<INITIAL>"!=" { return (int)Tokens.RELOP_UNEQUAL; }

<INITIAL>"&&" { return (int)Tokens.LOGOP_AND; }
<INITIAL>"||" { return (int)Tokens.LOGOP_OR;  }

<INITIAL>"(" { return (int)Tokens.LP; }
<INITIAL>")" { return (int)Tokens.RP; }

<INITIAL>"[" { return (int)Tokens.LB; }
<INITIAL>"]" { return (int)Tokens.RB; }

<INITIAL>"," { return (int)Tokens.COMMA; }

<INITIAL>{WS} { }

<INITIAL>. { return (int)Tokens.error; }

%%