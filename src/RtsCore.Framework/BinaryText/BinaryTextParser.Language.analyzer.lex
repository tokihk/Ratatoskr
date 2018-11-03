%namespace RtsCore.Framework.BinaryText

%visibility internal

%tokentype Tokens

%option stack, minimize, parser, verbose, persistbuffer, noembedbuffers 

ID				[a-zA-Z]+
ES				(\\(['\\]+))
WS				[ \t\v\n\f]

%{

%}

%%

/* Scanner body */

// --- �o�C�i���z�� --------------------------
<INITIAL>[0-9a-fA-F]+ {
	yylval.value_bin = new BinaryTextData(yytext);
	return (int)Tokens.VALUE_BINARY;
}

// --- 10�i�� --------------------------
<INITIAL>#([0]|([1-9][0-9]{0,7})) {
	yylval.value_bin = new BinaryTextData(ulong.Parse(yytext.Substring(1, yytext.Length - 1)));
	return (int)Tokens.VALUE_BINARY;
}

// --- ������ ["xxx"] ------------------------
<INITIAL>\'([^'\\\n]|{ES})*\' {
	yylval.value_str = yytext.Substring(1, yytext.Length - 2);
	return (int)Tokens.VALUE_TEXT;
}

// --- �����R�[�h [<xxx>] --------------------
<INITIAL>[\<][^\>]+[\>] {
	yylval.value_str = yytext.Substring(1, yytext.Length - 2);
	return (int)Tokens.VALUE_CHAR_CODE_NAME;
}

// --- �}�N���� ------
<INITIAL>[a-zA-Z]+[\-]*[a-zA-Z]* {
	yylval.value_str = yytext;
	return (int)Tokens.MACRO_NAME;
}

<INITIAL>[$]  { return (int)Tokens.DOLLAR; }
<INITIAL>[(]  { return (int)Tokens.LP; }
<INITIAL>[)]  { return (int)Tokens.RP; }

<INITIAL>[,]  { return (int)Tokens.COMMA; }

<INITIAL>{WS} { }
<INITIAL>.    { return (int)Tokens.error; }

%%