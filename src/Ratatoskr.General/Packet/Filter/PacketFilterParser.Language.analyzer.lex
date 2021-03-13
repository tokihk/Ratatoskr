// http://www.quut.com/c/ANSI-C-grammar-y.html
// http://www.quut.com/c/ANSI-C-grammar-l-2011.html

%using System.Diagnostics;
%using Ratatoskr.General.Packet.Filter.Terms;

%visibility internal

%namespace Ratatoskr.General.Packet.Filter

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
TY  ([0-9]{4})
TM  ([0][0-9]|1[0-2])
TD  ([0-2][0-9]|3[0-1])
Th  ([0-1][0-9]|2[0-3])
Tm  ([0-5][0-9])
Ts  ([0-5][0-9])
Tf  ([0-9]{3})

%{

%}

%%

// --- 数字(10進数) --------------------------
<INITIAL>[0]|([1-9][0-9]{0,8})(\.[0-9]{1,8}){0,1} {
	yylval.term = new Terms.Term_Number(double.Parse(yytext));
	return (int)Tokens.VALUE_NUMBER;
}

// --- 数字(16進数) --------------------------
<INITIAL>0[xX][0-9a-fA-F]{1,8} {
	yylval.term = new Terms.Term_Number((double)uint.Parse(yytext, System.Globalization.NumberStyles.HexNumber));
	return (int)Tokens.VALUE_NUMBER;
}

// --- バイナリ配列 --------------------------
<INITIAL>[\[][0-9a-fA-F]+[\]] {
	yylval.term = new Terms.Term_Binary(HexTextEncoder.ToByteArray(yytext.Substring(1, yytext.Length - 2)));
	return (int)Tokens.VALUE_BINARY;
}

// --- 時刻(ISO8601) --------------------------
<INITIAL>[$]{TY}-{TM}-{TD}T{Th}:{Tm}:{Ts}(\.{Tf})?([\+\-]{Th}:{Tm}|Z)[$] {
	yylval.term = new Terms.Term_DateTime(Term_DateTime.FormatType.ISO8601, yytext);
	return (int)Tokens.VALUE_DATETIME;
}

// --- ローカル時刻(日付ベース) --------------------------
<INITIAL>[$]{TY}(-{TM}(-{TD}([ ]{Th}(:{Tm}(:{Ts}(\.{Tf})?)?)?)?)?)?[$] {
	yylval.term = new Terms.Term_DateTime(Term_DateTime.FormatType.LocalTime_DateBase, yytext);
	return (int)Tokens.VALUE_DATETIME;
}

// --- ローカル時刻(時刻ベース) --------------------------
<INITIAL>[$]((({TY}-)?{TM}-)?{TD}[ ])?{Th}:{Tm}:{Ts}(\.{Tf})?[$] {
	yylval.term = new Terms.Term_DateTime(Term_DateTime.FormatType.LocalTime_TimeBase, yytext);
	return (int)Tokens.VALUE_DATETIME;
}

// --- 時刻(オフセット) --------------------------
<INITIAL>{Th}:{Tm}:{Ts}(\.{Tf}) {
	yylval.term = new Terms.Term_DateTimeOffset(Term_DateTimeOffset.ParseMode.Details, yytext);
	return (int)Tokens.VALUE_DATETIMEOFFSET;
}

// --- 時刻(オフセット - 単位指定) --------------------------
<INITIAL>([0]|([1-9][0-9]{0,9}))(h|hour|m|min|s|sec|ms|msec) {
	yylval.term = new Terms.Term_DateTimeOffset(Term_DateTimeOffset.ParseMode.Simple, yytext);
	return (int)Tokens.VALUE_DATETIMEOFFSET;
}

// --- 文字列 ["xxx"] -------------------------
<INITIAL>\"[^\"]*\" {
	yylval.term = new Terms.Term_Text(yytext.Substring(1, yytext.Length - 2));
	return (int)Tokens.VALUE_TEXT;
}

// --- 正規表現 -------------------------------
<INITIAL>\/[^/]*\/ {
	yylval.term = new Terms.Term_Regex(yytext.Substring(1, yytext.Length - 2));
	return (int)Tokens.VALUE_REGEX;
}

// --- ステータス -----------------------------
<INITIAL>[Pp]acket[Cc]ount {
	yylval.term = new Terms.Term_Status(Terms.Term_Status.StatusType.PacketCount);
	return (int)Tokens.VALUE_STATUS;
}
<INITIAL>[Ll]ast[Dd]elta {
	yylval.term = new Terms.Term_Status(Terms.Term_Status.StatusType.LastPacketDelta);
	return (int)Tokens.VALUE_STATUS;
}
<INITIAL>[Ii]s[Cc]ontrol {
	yylval.term = new Terms.Term_Status(Terms.Term_Status.StatusType.Packet_IsControl);
	return (int)Tokens.VALUE_STATUS;
}
<INITIAL>[Ii]s[Mm]essage {
	yylval.term = new Terms.Term_Status(Terms.Term_Status.StatusType.Packet_IsMessage);
	return (int)Tokens.VALUE_STATUS;
}
<INITIAL>[Ii]s[Dd]ata {
	yylval.term = new Terms.Term_Status(Terms.Term_Status.StatusType.Packet_IsData);
	return (int)Tokens.VALUE_STATUS;
}
<INITIAL>[Aa]lias {
	yylval.term = new Terms.Term_Status(Terms.Term_Status.StatusType.Packet_Alias);
	return (int)Tokens.VALUE_STATUS;
}
<INITIAL>[Dd]ate[Tt]ime {
	yylval.term = new Terms.Term_Status(Terms.Term_Status.StatusType.Packet_MakeTime);
	return (int)Tokens.VALUE_STATUS;
}
<INITIAL>[Cc]lass {
	yylval.term = new Terms.Term_Status(Terms.Term_Status.StatusType.Packet_Class);
	return (int)Tokens.VALUE_STATUS;
}
<INITIAL>[Ii]nformation {
	yylval.term = new Terms.Term_Status(Terms.Term_Status.StatusType.Packet_Information);
	return (int)Tokens.VALUE_STATUS;
}
<INITIAL>[Mm]ark {
	yylval.term = new Terms.Term_Status(Terms.Term_Status.StatusType.Packet_Mark);
	return (int)Tokens.VALUE_STATUS;
}
<INITIAL>[Ii]s[Ss]end {
	yylval.term = new Terms.Term_Status(Terms.Term_Status.StatusType.Packet_Data_IsSend);
	return (int)Tokens.VALUE_STATUS;
}
<INITIAL>[Ii]s[Rr]ecv {
	yylval.term = new Terms.Term_Status(Terms.Term_Status.StatusType.Packet_Data_IsRecv);
	return (int)Tokens.VALUE_STATUS;
}
<INITIAL>[Ss]ource {
	yylval.term = new Terms.Term_Status(Terms.Term_Status.StatusType.Packet_Data_Source);
	return (int)Tokens.VALUE_STATUS;
}
<INITIAL>[Dd]estination {
	yylval.term = new Terms.Term_Status(Terms.Term_Status.StatusType.Packet_Data_Destination);
	return (int)Tokens.VALUE_STATUS;
}
<INITIAL>[Dd]ataSize {
	yylval.term = new Terms.Term_Status(Terms.Term_Status.StatusType.Packet_Data_Length);
	return (int)Tokens.VALUE_STATUS;
}
<INITIAL>[Bb]it[Ss]tring {
	yylval.term = new Terms.Term_Status(Terms.Term_Status.StatusType.Packet_Data_BitString);
	return (int)Tokens.VALUE_STATUS;
}
<INITIAL>[Hh]ex[Ss]tring {
	yylval.term = new Terms.Term_Status(Terms.Term_Status.StatusType.Packet_Data_HexString);
	return (int)Tokens.VALUE_STATUS;
}
<INITIAL>[Aa]scii[Tt]ext {
	yylval.term = new Terms.Term_Status(Terms.Term_Status.StatusType.Packet_Data_AsciiText);
	return (int)Tokens.VALUE_STATUS;
}
<INITIAL>[Uu]tf8[Tt]ext {
	yylval.term = new Terms.Term_Status(Terms.Term_Status.StatusType.Packet_Data_Utf8Text);
	return (int)Tokens.VALUE_STATUS;
}
<INITIAL>[Uu]tf16[Bb]e[Tt]ext {
	yylval.term = new Terms.Term_Status(Terms.Term_Status.StatusType.Packet_Data_Utf16BeText);
	return (int)Tokens.VALUE_STATUS;
}
<INITIAL>[Uu]tf16[Ll]e[Tt]ext {
	yylval.term = new Terms.Term_Status(Terms.Term_Status.StatusType.Packet_Data_Utf16LeText);
	return (int)Tokens.VALUE_STATUS;
}
<INITIAL>[Ss]hift[Jj]is[Tt]ext {
	yylval.term = new Terms.Term_Status(Terms.Term_Status.StatusType.Packet_Data_ShiftJisText);
	return (int)Tokens.VALUE_STATUS;
}
<INITIAL>[Ee]uc[Jj]p[Tt]ext {
	yylval.term = new Terms.Term_Status(Terms.Term_Status.StatusType.Packet_Data_EucJpText);
	return (int)Tokens.VALUE_STATUS;
}

<INITIAL>"="  { return (int)Tokens.ARMOP_SET;      }
<INITIAL>"+"  { return (int)Tokens.ARMOP_ADD;      }
<INITIAL>"-"  { return (int)Tokens.ARMOP_SUB;      }
<INITIAL>"*"  { return (int)Tokens.ARMOP_MUL;      }
<INITIAL>"/"  { return (int)Tokens.ARMOP_SUB;      }
<INITIAL>"%"  { return (int)Tokens.ARMOP_REM;      }
<INITIAL>"!"  { return (int)Tokens.ARMOP_NEG;      }

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

<INITIAL>{WS} { }

<INITIAL>. { return (int)Tokens.error; }

%%