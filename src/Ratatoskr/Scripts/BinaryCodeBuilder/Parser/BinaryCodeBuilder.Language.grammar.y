%namespace Ratatoskr.Scripts.BinaryCodeBuilder.Parser

%visibility internal
%parsertype BinaryCodeBuilderParser

%{
	private BinaryCode        parse_data_;
	private Encoding          parse_encoder_;
	private List<BinaryCode>  value_bin_list_;
%}

%union { 
	public BinaryCode value_bin;
	public string     value_str;
}

%token DOLLAR LP RP COMMA
%token <value_bin> VALUE_BINARY
%token <value_str> VALUE_TEXT VALUE_CHAR_CODE_NAME MACRO_NAME
%type  <value_bin> expression term_binary term_macro

%%

expression
	: term_binary
	| term_binary expression
	{
		$$ = $1 + $2;
	}
	| term_char_code expression
	{
		$$ = $2;
	}
	| term_macro
	;

expression_list
	: expression
	{
		value_bin_list_ = new List<BinaryCode>();
		value_bin_list_.Add($1);
	}
	| expression COMMA expression_list
	{
		value_bin_list_.Insert(0, $1);
	}
	;

term_binary
	: VALUE_BINARY
	{
		$$ = $1;
	}
	| VALUE_TEXT
	{
		$$ = new BinaryCode(parse_encoder_, $1);
	}
	;

term_char_code
	: VALUE_CHAR_CODE_NAME
	{
		parse_encoder_ = Encoding.GetEncoding($1);
	}
	;

term_macro
	: DOLLAR LP MACRO_NAME RP
	{
		$$ = MacroProcessor.Run($3, value_bin_list_);
	}
	| DOLLAR LP MACRO_NAME expression_list RP
	{
		$$ = MacroProcessor.Run($3, value_bin_list_);
	}
	;

%%

    private BinaryCodeBuilderParser() : base(null)
	{
		parse_data_ = new BinaryCode();
		parse_encoder_ = Encoding.UTF8;
		value_bin_list_ = new List<BinaryCode>();
	}

    public static byte[] Parse(string exp)
    {
		try {
			if (exp == null)return (null);
			if (exp.Length == 0)return (null);

			var scanner = new Scanner();

			scanner.SetSource(exp, 0);
         
			var parser = new BinaryCodeBuilderParser();
         
			parser.Scanner = scanner;
         
			if (!parser.Parse())return (null);
         
            var result = parser.CurrentSemanticValue.value_bin;

            if (result == null)return (null);

            return (result.GetBytes());
		} catch {
			return (null);
		}
    }
