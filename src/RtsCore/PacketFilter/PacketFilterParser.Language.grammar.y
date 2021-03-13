// http://www.quut.com/c/ANSI-C-grammar-y.html
// http://www.quut.com/c/ANSI-C-grammar-l-2011.html

%using System.Diagnostics;
%using RtsCore.Utility;

%namespace RtsCore.Framework.PacketFilter

%visibility internal
%parsertype PacketFilterParser

%{
    private PacketFilterObject exp_obj_ = null;
%}

%union {
	public Terms.Term term;
}

%token	<term>VALUE_BOOL VALUE_NUMBER VALUE_TEXT VALUE_BINARY VALUE_REGEX VALUE_DATETIME VALUE_DATETIMEOFFSET VALUE_STATUS

%token	ARMOP_SET ARMOP_NEG

%token	ARMOP_ADD ARMOP_SUB ARMOP_MUL ARMOP_DIV ARMOP_REM

%token 	RELOP_GREATER RELOP_LESS RELOP_GREATEREQUAL RELOP_LESSEQUAL

%token	RELOP_EQUAL RELOP_UNEQUAL

%token	LOGOP_AND LOGOP_OR

%token	LP RP

%%


expression
	: assignment_expression
	;

assignment_expression
	: logical_expression
	| logical_expression ARMOP_SET assignment_expression
	{
		exp_obj_.Add(Tokens.ARMOP_SET);
	}
	;

logical_expression
	: equality_expression
	| equality_expression LOGOP_OR logical_expression
	{
		exp_obj_.Add(Tokens.LOGOP_OR);
	}
	| equality_expression LOGOP_AND logical_expression
	{
		exp_obj_.Add(Tokens.LOGOP_AND);
	}
	;

equality_expression
	: relational_expression
	| relational_expression RELOP_EQUAL equality_expression
	{
		exp_obj_.Add(Tokens.RELOP_EQUAL);
	}
	| relational_expression RELOP_UNEQUAL equality_expression
	{
		exp_obj_.Add(Tokens.RELOP_UNEQUAL);
	}
	;

relational_expression
	: additive_expression
	| additive_expression RELOP_GREATEREQUAL relational_expression
	{
		exp_obj_.Add(Tokens.RELOP_GREATEREQUAL);
	}
	| additive_expression RELOP_LESSEQUAL relational_expression
	{
		exp_obj_.Add(Tokens.RELOP_LESSEQUAL);
	}
	| additive_expression RELOP_GREATER relational_expression
	{
		exp_obj_.Add(Tokens.RELOP_GREATER);
	}
	| additive_expression RELOP_LESS relational_expression
	{
		exp_obj_.Add(Tokens.RELOP_LESS);
	}
	;

additive_expression
	: multiplicative_expression
	| multiplicative_expression ARMOP_ADD additive_expression
	{
		exp_obj_.Add(Tokens.ARMOP_ADD);
	}
	| multiplicative_expression ARMOP_SUB additive_expression
	{
		exp_obj_.Add(Tokens.ARMOP_SUB);
	}
	;

multiplicative_expression
	: negative_expression
	| negative_expression ARMOP_MUL multiplicative_expression
	{
		exp_obj_.Add(Tokens.ARMOP_MUL);
	}
	| negative_expression ARMOP_DIV multiplicative_expression
	{
		exp_obj_.Add(Tokens.ARMOP_DIV);
	}
	| negative_expression ARMOP_REM multiplicative_expression
	{
		exp_obj_.Add(Tokens.ARMOP_REM);
	}
	;

negative_expression
	: postfix_expression
	| ARMOP_NEG negative_expression
	{
		exp_obj_.Add(Tokens.ARMOP_NEG);
	}
	;

postfix_expression
	: primary_expression
	;

primary_expression
	: VALUE_BOOL
	{
		exp_obj_.Add($1);
	}
	| VALUE_NUMBER
	{
		exp_obj_.Add($1);
	}
	| VALUE_TEXT
	{
		exp_obj_.Add($1);
	}
	| VALUE_BINARY
	{
		exp_obj_.Add($1);
	}
	| VALUE_REGEX
	{
		exp_obj_.Add($1);
	}
	| VALUE_DATETIME
	{
		exp_obj_.Add($1);
	}
	| VALUE_DATETIMEOFFSET
	{
		exp_obj_.Add($1);
	}
	| VALUE_STATUS
	{
		exp_obj_.Add($1);
	}
	| LP expression RP
	;

%%

    private PacketFilterParser(string exp_text) : base(null)
	{
		exp_obj_ = new PacketFilterObject(exp_text);
	}

    public static PacketFilterObject Parse(string exp)
    {
		try {
			if (exp == null)return (null);
			if (exp.Length == 0)return (null);

			var scanner = new Scanner();

			scanner.SetSource(exp, 0);
         
			var parser = new PacketFilterParser(exp);
         
			parser.Scanner = scanner;
         
			if (!parser.Parse())return (null);
         
			return (parser.exp_obj_);
		} catch {
			return (null);
		}
    }
