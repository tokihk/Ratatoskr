// This code was generated by the Gardens Point Parser Generator
// Copyright (c) Wayne Kelly, John Gough, QUT 2005-2014
// (see accompanying GPPGcopyright.rtf)

// GPPG version 1.5.2
// DateTime: 2018/11/01 17:54:13
// Input file <BinaryText\BinaryTextParser.Language.grammar.y - 2018/11/01 17:54:09>

// options: no-lines gplex

using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Text;
using QUT.Gppg;

namespace RtsCore.Framework.BinaryText
{
internal enum Tokens {error=2,EOF=3,DOLLAR=4,LP=5,RP=6,
    COMMA=7,VALUE_BINARY=8,VALUE_TEXT=9,VALUE_CHAR_CODE_NAME=10,MACRO_NAME=11};

internal struct ValueType
{ 
	public BinaryTextData value_bin;
	public string         value_str;
}
// Abstract base class for GPLEX scanners
[GeneratedCodeAttribute( "Gardens Point Parser Generator", "1.5.2")]
internal abstract class ScanBase : AbstractScanner<ValueType,LexLocation> {
  private LexLocation __yylloc = new LexLocation();
  public override LexLocation yylloc { get { return __yylloc; } set { __yylloc = value; } }
  protected virtual bool yywrap() { return true; }
}

// Utility class for encapsulating token information
[GeneratedCodeAttribute( "Gardens Point Parser Generator", "1.5.2")]
internal class ScanObj {
  public int token;
  public ValueType yylval;
  public LexLocation yylloc;
  public ScanObj( int t, ValueType val, LexLocation loc ) {
    this.token = t; this.yylval = val; this.yylloc = loc;
  }
}

[GeneratedCodeAttribute( "Gardens Point Parser Generator", "1.5.2")]
internal class BinaryTextParser: ShiftReduceParser<ValueType, LexLocation>
{
  // Verbatim content from BinaryText\BinaryTextParser.Language.grammar.y - 2018/11/01 17:54:09
	private BinaryTextData        parse_data_;
	private Encoding              parse_encoder_;
	private List<BinaryTextData>  value_bin_list_;
  // End verbatim content from BinaryText\BinaryTextParser.Language.grammar.y - 2018/11/01 17:54:09

#pragma warning disable 649
  private static Dictionary<int, string> aliases;
#pragma warning restore 649
  private static Rule[] rules = new Rule[13];
  private static State[] states = new State[20];
  private static string[] nonTerms = new string[] {
      "expression", "term_binary", "term_macro", "$accept", "term_char_code", 
      "expression_list", };

  static BinaryTextParser() {
    states[0] = new State(new int[]{8,5,9,6,10,9,4,11},new int[]{-1,1,-2,3,-5,7,-3,10});
    states[1] = new State(new int[]{3,2});
    states[2] = new State(-1);
    states[3] = new State(new int[]{8,5,9,6,10,9,4,11,3,-2,7,-2,6,-2},new int[]{-1,4,-2,3,-5,7,-3,10});
    states[4] = new State(-3);
    states[5] = new State(-8);
    states[6] = new State(-9);
    states[7] = new State(new int[]{8,5,9,6,10,9,4,11},new int[]{-1,8,-2,3,-5,7,-3,10});
    states[8] = new State(-4);
    states[9] = new State(-10);
    states[10] = new State(-5);
    states[11] = new State(new int[]{5,12});
    states[12] = new State(new int[]{11,13});
    states[13] = new State(new int[]{6,14,8,5,9,6,10,9,4,11},new int[]{-6,15,-1,17,-2,3,-5,7,-3,10});
    states[14] = new State(-11);
    states[15] = new State(new int[]{6,16});
    states[16] = new State(-12);
    states[17] = new State(new int[]{7,18,6,-6});
    states[18] = new State(new int[]{8,5,9,6,10,9,4,11},new int[]{-6,19,-1,17,-2,3,-5,7,-3,10});
    states[19] = new State(-7);

    for (int sNo = 0; sNo < states.Length; sNo++) states[sNo].number = sNo;

    rules[1] = new Rule(-4, new int[]{-1,3});
    rules[2] = new Rule(-1, new int[]{-2});
    rules[3] = new Rule(-1, new int[]{-2,-1});
    rules[4] = new Rule(-1, new int[]{-5,-1});
    rules[5] = new Rule(-1, new int[]{-3});
    rules[6] = new Rule(-6, new int[]{-1});
    rules[7] = new Rule(-6, new int[]{-1,7,-6});
    rules[8] = new Rule(-2, new int[]{8});
    rules[9] = new Rule(-2, new int[]{9});
    rules[10] = new Rule(-5, new int[]{10});
    rules[11] = new Rule(-3, new int[]{4,5,11,6});
    rules[12] = new Rule(-3, new int[]{4,5,11,-6,6});
  }

  protected override void Initialize() {
    this.InitSpecialTokens((int)Tokens.error, (int)Tokens.EOF);
    this.InitStates(states);
    this.InitRules(rules);
    this.InitNonTerminals(nonTerms);
  }

  protected override void DoAction(int action)
  {
#pragma warning disable 162, 1522
    switch (action)
    {
      case 3: // expression -> term_binary, expression
{
		CurrentSemanticValue.value_bin = ValueStack[ValueStack.Depth-2].value_bin + ValueStack[ValueStack.Depth-1].value_bin;
	}
        break;
      case 4: // expression -> term_char_code, expression
{
		CurrentSemanticValue.value_bin = ValueStack[ValueStack.Depth-1].value_bin;
	}
        break;
      case 6: // expression_list -> expression
{
		value_bin_list_ = new List<BinaryTextData>();
		value_bin_list_.Add(ValueStack[ValueStack.Depth-1].value_bin);
	}
        break;
      case 7: // expression_list -> expression, COMMA, expression_list
{
		value_bin_list_.Insert(0, ValueStack[ValueStack.Depth-3].value_bin);
	}
        break;
      case 8: // term_binary -> VALUE_BINARY
{
		CurrentSemanticValue.value_bin = ValueStack[ValueStack.Depth-1].value_bin;
	}
        break;
      case 9: // term_binary -> VALUE_TEXT
{
		CurrentSemanticValue.value_bin = new BinaryTextData(parse_encoder_, ValueStack[ValueStack.Depth-1].value_str);
	}
        break;
      case 10: // term_char_code -> VALUE_CHAR_CODE_NAME
{
		parse_encoder_ = Encoding.GetEncoding(ValueStack[ValueStack.Depth-1].value_str);
	}
        break;
      case 11: // term_macro -> DOLLAR, LP, MACRO_NAME, RP
{
		CurrentSemanticValue.value_bin = BinaryTextMacro.Run(ValueStack[ValueStack.Depth-2].value_str, value_bin_list_);
	}
        break;
      case 12: // term_macro -> DOLLAR, LP, MACRO_NAME, expression_list, RP
{
		CurrentSemanticValue.value_bin = BinaryTextMacro.Run(ValueStack[ValueStack.Depth-3].value_str, value_bin_list_);
	}
        break;
    }
#pragma warning restore 162, 1522
  }

  protected override string TerminalToString(int terminal)
  {
    if (aliases != null && aliases.ContainsKey(terminal))
        return aliases[terminal];
    else if (((Tokens)terminal).ToString() != terminal.ToString(CultureInfo.InvariantCulture))
        return ((Tokens)terminal).ToString();
    else
        return CharToString((char)terminal);
  }


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
}
}
