// http://www.quut.com/c/ANSI-C-grammar-y.html
// http://www.quut.com/c/ANSI-C-grammar-l-2011.html

%using System.Diagnostics;

%namespace Ratatoskr.Scripts.PacketFilterExp.Parser

%visibility internal
%parsertype ExpressionParser

%{
    private ExpressionObject exp_ = new ExpressionObject();
%}

%union {
	public Terms.Term term;
}

%token	<term>VALUE_ID VALUE_BOOL VALUE_INTEGER VALUE_DOUBLE VALUE_TEXT VALUE_BINTEXT VALUE_PATTERN VALUE_TIME

%token	ARMOP_SET ARMOP_ADD ARMOP_SUB ARMOP_MUL ARMOP_DIV ARMOP_REM

%token 	RELOP_GREATER RELOP_LESS RELOP_GREATEREQUAL RELOP_LESSEQUAL

%token	RELOP_EQUAL RELOP_UNEQUAL

%token	LOGOP_AND LOGOP_OR

%token	ARRAY CALL REFERENCE LP RP LB RB COMMA

%%

expression_list
	: expression
	| expression_list COMMA expression
	{
		exp_.Add(Tokens.ARRAY);
	}
	;

expression
	: assignment_expression
	;

assignment_expression
	: logical_or_expression
	| assignment_expression ARMOP_SET logical_or_expression
	{
		exp_.Add(Tokens.ARMOP_SET);
	}
	;

logical_or_expression
	: logical_and_expression
	| logical_or_expression LOGOP_OR logical_and_expression
	{
		exp_.Add(Tokens.LOGOP_OR);
	}
	;

logical_and_expression
	: equality_expression
	| logical_and_expression LOGOP_AND equality_expression
	{
		exp_.Add(Tokens.LOGOP_AND);
	}
	;

equality_expression
	: relational_expression
	| equality_expression RELOP_EQUAL relational_expression
	{
		exp_.Add(Tokens.RELOP_EQUAL);
	}
	| equality_expression RELOP_UNEQUAL relational_expression
	{
		exp_.Add(Tokens.RELOP_UNEQUAL);
	}
	;

relational_expression
	: additive_expression
	| relational_expression RELOP_GREATEREQUAL additive_expression
	{
		exp_.Add(Tokens.RELOP_GREATEREQUAL);
	}
	| relational_expression RELOP_LESSEQUAL additive_expression
	{
		exp_.Add(Tokens.RELOP_LESSEQUAL);
	}
	| relational_expression RELOP_GREATER additive_expression
	{
		exp_.Add(Tokens.RELOP_GREATER);
	}
	| relational_expression RELOP_LESS additive_expression
	{
		exp_.Add(Tokens.RELOP_LESS);
	}
	;

additive_expression
	: multiplicative_expression
	| additive_expression ARMOP_ADD multiplicative_expression
	{
		exp_.Add(Tokens.ARMOP_ADD);
	}
	| additive_expression ARMOP_SUB multiplicative_expression
	{
		exp_.Add(Tokens.ARMOP_SUB);
	}
	;

multiplicative_expression
	: postfix_expression
	| multiplicative_expression ARMOP_MUL postfix_expression
	{
		exp_.Add(Tokens.ARMOP_MUL);
	}
	| multiplicative_expression ARMOP_DIV postfix_expression
	{
		exp_.Add(Tokens.ARMOP_DIV);
	}
	| multiplicative_expression ARMOP_REM postfix_expression
	{
		exp_.Add(Tokens.ARMOP_REM);
	}
	;

postfix_expression
	: box_expression
	| box_expression LP expression_list RP
	{
		exp_.Add(Tokens.CALL);
	}
	| box_expression LP RP
	{
		exp_.Add(new Terms.Term_Void());
		exp_.Add(Tokens.CALL);
	}
	;

box_expression
	: primary_expression
	| primary_expression LB expression RB
	{
		exp_.Add(Tokens.REFERENCE);
	}
	;

primary_expression
	: VALUE_ID
	{
		exp_.Add($1);
	}
	| VALUE_BOOL
	{
		exp_.Add($1);
	}
	| VALUE_INTEGER
	{
		exp_.Add($1);
	}
	| VALUE_DOUBLE
	{
		exp_.Add($1);
	}
	| VALUE_TEXT
	{
		exp_.Add($1);
	}
	| VALUE_BINTEXT
	{
		exp_.Add($1);
	}
	| VALUE_PATTERN
	{
		exp_.Add($1);
	}
	| VALUE_TIME
	{
		exp_.Add($1);
	}
	| LP expression_list RP
	;


%%

    private ExpressionParser() : base(null) { }

    public static ExpressionObject Parse(string exp)
    {
         if (exp == null)return (null);
         if (exp.Length == 0)return (null);

         var scanner = new Scanner();

         scanner.SetSource(exp, 0);
         
         var parser = new ExpressionParser();
         
         parser.Scanner = scanner;
         
         if (!parser.Parse())return (null);
         
         return (parser.exp_);
    }
