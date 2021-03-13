%namespace Ratatoskr.General.BinaryText

%visibility internal
%parsertype BinaryTextParser

%{
	private BinaryTextData        parse_data_;
	private Encoding              parse_encoder_;
	private List<BinaryTextData>  value_bin_list_;
%}

%union { 
	public BinaryTextData value_bin;
	public string         value_str;
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
		value_bin_list_ = new List<BinaryTextData>();
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
		$$ = new BinaryTextData(parse_encoder_, $1);
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
		$$ = BinaryTextMacro.Run($3, value_bin_list_);
	}
	| DOLLAR LP MACRO_NAME expression_list RP
	{
		$$ = BinaryTextMacro.Run($3, value_bin_list_);
	}
	;

%%

    private BinaryTextParser() : base(null)
	{
		parse_data_ = new BinaryTextData();
		parse_encoder_ = Encoding.UTF8;
		value_bin_list_ = new List<BinaryTextData>();
	}

    public static byte[] Parse(string exp)
    {
		try {
			if (exp == null)return (null);
			if (exp.Length == 0)return (null);

			var scanner = new Scanner();

			scanner.SetSource(exp, 0);
         
			var parser = new BinaryTextParser();
         
			parser.Scanner = scanner;
         
			if (!parser.Parse())return (null);
         
            var result = parser.CurrentSemanticValue.value_bin;

            if (result == null)return (null);

            return (result.GetBytes());
		} catch {
			return (null);
		}
    }
