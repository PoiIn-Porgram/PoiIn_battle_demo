 using System;
using System.Collections;
using System.Collections.Generic;
 using System.Diagnostics;
 using System.Linq;
 using TMPro;
 using UnityEngine;
 using Debug = UnityEngine.Debug;

 enum Tag
 {
  Error,       //错误标识符
  End,         //文件结束符
  Id,          //标识符
  Num,         //常数
  HP,SP,AP,PRA,ACA,AGI,      //角色参数
  Add,Minus,Multiple,Divide,     //加号，减号，乘号，除号
  Note,    //注释符号
  TemporaryIncreaseOrDecline,              //临时增加或减少
  PermanentIncreaseOrDecrease,             //永久性增加或减少
  TemporaryMultiple,                   //临时性倍增
  PermenentMultiple,                   //永久性倍增
  Greater,GreaterEqual,Lower,LowerEqual,Equal,NotEqual,    //大于,大于等于,小于,小于等于,等于,不等于
  And,Or,       //逻辑和,逻辑或
  LeftParen,RightParen,   //()
  LBrack,RBrack,      //[]
  LBrace,RBrace,      //{}
  Comma,Colon,Semicon,  //, : ;
  Assign,     //赋值
  LineFeed,   //换行
  KW_If,KW_Else,  //关键词If,关键词Else
  KW_Round,        //关键词Round
  KW_Self,         //关键词 Self
  KW_Target,        //关键词Target
  
  KF_Normal_Attack,   //普通攻击周期函数
  KF_Dice,            //调用骰子周期函数
  KF_Instantiate,      //关键函数,召唤物品
  KF_CreateState,      //关键函数,制造状态
  KF_CancelState,      //关键函数,取消状态
  KF_DisruptCycle      //关键函数,打断周期
 }
 struct TokenStruct
{
  public Tag _tag;
  public int numValue;
}
 
 
public class scriptableObjectAnalyser : MonoBehaviour
{
 private Dictionary<string, int> status = new Dictionary<string, int>();
 
 [SerializeField][TextArea]
 private string PoiInCode = "";
 private void Start()
 {
  status = FindObjectOfType<chrecterCard>().status;
  initializeKeywordDictionary();
  token();
 
 }

 private void logQueue()
 {
  TokenStruct[] tokenStructs = TokenStructQueue.ToArray();
  foreach (TokenStruct tokenStruct in tokenStructs)
  {
   Debug.Log(tokenStruct._tag);
   if (tokenStruct._tag == Tag.Num)
   {
    Debug.Log(tokenStruct.numValue);
   }
  }
 }
 private Dictionary<string, Tag> keywordsDictionary = new Dictionary<string, Tag>();
 private Queue<TokenStruct> TokenStructQueue = new Queue<TokenStruct>();

 private void initializeKeywordDictionary()
 {
  keywordsDictionary.Add(" ",Tag.LineFeed);
  keywordsDictionary.Add(":", Tag.Colon);
  keywordsDictionary.Add(";",Tag.Semicon);
  keywordsDictionary.Add("+",Tag.Add);
  keywordsDictionary.Add("-",Tag.Minus);
  keywordsDictionary.Add("*",Tag.Multiple);
  keywordsDictionary.Add("/",Tag.Divide);
  keywordsDictionary.Add("Self",Tag.KW_Self);
  keywordsDictionary.Add("Target",Tag.KW_Target);
  keywordsDictionary.Add("HP",Tag.HP);
  keywordsDictionary.Add("SP",Tag.SP);
  keywordsDictionary.Add("AP",Tag.AP);
  keywordsDictionary.Add("PRA",Tag.PRA);
  keywordsDictionary.Add("ACA",Tag.ACA);
  keywordsDictionary.Add("AGI",Tag.AGI);
  keywordsDictionary.Add("If",Tag.KW_If);
  keywordsDictionary.Add("Else",Tag.KW_Else);
 }

 private Tag getTag(string name)
 {
  return keywordsDictionary[name];
 }
 
 int endCHr = 0;
 private int curChr = 0;

 enum curCharType
 {
  NULL,
  number,
  word,
  symbal,
  end
 }

 private curCharType thisType = curCharType.NULL;
 private bool isEnd = false;
 private char scanNextChar()
 {
      curChr++;
      if (curChr>endCHr)
      {
       logQueue();
       isEnd = true;
       thisType = curCharType.end;
       curChr--;
       return '@';
      }
      //Debug.Log(curChr);
      char ch = PoiInCode[curChr];
      if (ch == ' ')
      {
       return scanNextChar();
      }
      //index out of range
      if (ch>='0'&&ch<='9')
      {
       thisType = curCharType.number;
      }else if (ch>='a'&&ch<='z')
      {
       thisType = curCharType.word;
      }
      else if (ch>='A'&&ch<='Z')
      {
       thisType = curCharType.word;
      }
      else
      {
       thisType = curCharType.symbal;
      }
      return ch;
 }

 private string scanFullWord()
 {
  string fullWord = ""+PoiInCode[curChr];
  char ch = scanNextChar();
  while (thisType == curCharType.word)
  {
   fullWord = fullWord + PoiInCode[curChr];
   if (scanNextChar()=='_')
   {
    thisType = curCharType.word;//conjection
   }
  }
  curChr--;
  return fullWord;
 }

 private string getFullSymbal()
 {
  string fullSymbal = "" + PoiInCode[curChr];
  if (fullSymbal == "(" || fullSymbal == ")" || fullSymbal == "{"||fullSymbal=="}"||fullSymbal=="\n")
  {
   scanNextChar();
   curChr--;
   return fullSymbal;
  }
  char ch = scanNextChar();
  while (thisType ==curCharType.symbal)
  {
   if (ch == '('|| ch == ')' || ch == '{'||ch == '}'||ch == '\n')
   {
    scanNextChar();
    curChr--;
    return fullSymbal;
   }
   
   fullSymbal = fullSymbal + ch;
   
   Debug.Log(fullSymbal+fullSymbal.Length);
   if (fullSymbal.Length==2)
   {
    scanNextChar();
    curChr--;
    return fullSymbal;
   }
   scanNextChar();
  }
  curChr--;
  return fullSymbal;
 }


 public void token()
 {
  isEnd = false;
  curChr = -1;
  endCHr = PoiInCode.Length-1;
  TokenStruct tokenStruct = new TokenStruct();
  tokenStruct._tag = Tag.End;
  while (!isEnd)
  {
   //tokenStruct = new TokenStruct();
   char ch = scanNextChar();

    if (thisType == curCharType.number)
   {
    int value = 0;
    do
    {
     value = value * 10 + ch - '0';
     ch = scanNextChar();

    } while (thisType == curCharType.number);


    Debug.Log(tokenStruct._tag+"before NUmber");
    if (tokenStruct._tag==Tag.Minus)
    {
     value = -1*value;
    }
    tokenStruct.numValue = value;
    tokenStruct._tag = Tag.Num;
    TokenStructQueue.Enqueue(tokenStruct);
   }
    else if (thisType == curCharType.word)
    {
     switch (scanFullWord())
     {
      case "If":
       tokenStruct._tag = Tag.KW_If;
       break;
      case "Else":
       tokenStruct._tag = Tag.KW_Else;
       break;
      case "Self":
       tokenStruct._tag = Tag.KW_Self;
       break;
      case "Target":
       tokenStruct._tag = Tag.KW_Target;
       break;
      case "Dice":
       tokenStruct._tag = Tag.KF_Dice;
       break;
      case "Round":
       tokenStruct._tag = Tag.KW_Round;
       break;
      case "Instantiate":
       tokenStruct._tag = Tag.KF_Instantiate;
       break;
      case "CreateState":
       tokenStruct._tag = Tag.KF_CreateState;
       break;
      case "CancelState":
       tokenStruct._tag = Tag.KF_CancelState;
       break;
      case "DisruptCycle":
       tokenStruct._tag = Tag.KF_DisruptCycle;
       break;
      case "HP":
       tokenStruct._tag = Tag.HP;
       break;
      case "SP":
       tokenStruct._tag = Tag.SP;
       break;
      case "AP":
       tokenStruct._tag = Tag.AP;
       break;
      case "PRA":
       tokenStruct._tag = Tag.PRA;
       break;
      case "ACA":
       tokenStruct._tag = Tag.ACA;
       break;
      case "AGI":
       tokenStruct._tag = Tag.AGI;
       break;
      case "Normal_Attack":
       tokenStruct._tag = Tag.KF_Normal_Attack;
       break;
      default:
       tokenStruct._tag = Tag.Error;
       Debug.Log("errorChr == "+ch);
       break;
     }

     TokenStructQueue.Enqueue(tokenStruct);
     continue;
    }

    if (thisType == curCharType.symbal)
    {
     string fulls = getFullSymbal();
     switch (fulls)
     {
      case "//":
       tokenStruct._tag = Tag.Note;
       break;
      case "\n":
       tokenStruct._tag = Tag.LineFeed;
       continue;
       break;
      case "+":
       tokenStruct._tag = Tag.Add;
       break;
      case "-":
       tokenStruct._tag = Tag.Minus;
       break;
      case "*":
       tokenStruct._tag = Tag.Multiple;
       break;
      case "/":
       tokenStruct._tag = Tag.Divide;
       break;
      case "->":
       tokenStruct._tag = Tag.TemporaryIncreaseOrDecline;
       break;
      case "+-":
       tokenStruct._tag = Tag.PermanentIncreaseOrDecrease;
       break;
      case "**":
       tokenStruct._tag = Tag.TemporaryMultiple;
       break;
      case "*/":
       tokenStruct._tag = Tag.PermenentMultiple;
       break;
      case "(":
       tokenStruct._tag = Tag.LeftParen;
       break;
      case ")":
       tokenStruct._tag = Tag.RightParen;
       break;
      case "[":
       tokenStruct._tag = Tag.LBrack;
       break;
      case "]":
       tokenStruct._tag = Tag.RBrack;
       break;
      case "{":
       tokenStruct._tag = Tag.LBrace;
       break;
      case "}":
       tokenStruct._tag = Tag.RBrace;
       break;
      case ">":
       tokenStruct._tag = Tag.Greater;
       break;
      case "<":
       tokenStruct._tag = Tag.Lower;
       break;
      case ">=":
      case "=>":
       tokenStruct._tag = Tag.GreaterEqual;
       break;
      case "<=":
      case "=<":
       tokenStruct._tag = Tag.LowerEqual;
       break;
      case "==":
       tokenStruct._tag = Tag.Equal;
       break;
      case "&&":
       tokenStruct._tag = Tag.And;
       break;
      case "||":
       tokenStruct._tag = Tag.Or;
       break;
      case ",":
       tokenStruct._tag = Tag.Comma;
       break;
      case ":":
       tokenStruct._tag = Tag.Colon;
       break;
      case ";":
       tokenStruct._tag = Tag.Semicon;
       break;
      case "=":
       tokenStruct._tag = Tag.Assign;
       break;
      default:
       tokenStruct._tag = Tag.Error;
       Debug.Log("erroS"+fulls);
       break;
     }
     TokenStructQueue.Enqueue(tokenStruct);
     continue;
    }

    if (ch!='@')
    {
     tokenStruct._tag = Tag.Error;
     Debug.Log("FIN"+ch);
     TokenStructQueue.Enqueue(tokenStruct);
    }
    
  }


 }

 
 
}

