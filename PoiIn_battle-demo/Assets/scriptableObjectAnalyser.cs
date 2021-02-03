 using System;
using System.Collections;
using System.Collections.Generic;
 using System.Diagnostics;
 using TMPro;
 using UnityEngine;

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
 private void Start()
 {
  status = FindObjectOfType<chrecterCard>().status;
 }

 private Dictionary<string, Tag> keywordsDictionary;
 private Queue<TokenStruct> TokenStructQueue = new Queue<TokenStruct>();

 private void initializeKeywordDictionary()
 {
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
 int curChr = 0;

 private char scanNextChar(ref string PoiInCode)
 {
      curChr++;
      return PoiInCode[curChr];
 }

 private string scanFullWord(ref string PoiInCode)
 {
  string fullWord = "";
  char ch = PoiInCode[curChr];
  while ((ch>='a'&&ch<='z')||(ch>='A'&&ch<='Z'))
  {
   fullWord = fullWord + PoiInCode[curChr];
   scanNextChar(ref PoiInCode);
  }
  return fullWord;
 }

 private string getFullSymbal(ref string PoiInCode)
 {
  char ch = PoiInCode[curChr];
  string fullSymbal = "";
  while (ch!=' ')
  {
   fullSymbal = fullSymbal + ch;
   ch = scanNextChar(ref PoiInCode);
  }
  return fullSymbal;
 }
 private void token(string PoiInCode)
 {
  curChr = -1;
  endCHr = PoiInCode.Length;
  
  while (curChr<=endCHr)
  {
   
   TokenStruct tokenStruct = new TokenStruct();
   
   char ch = scanNextChar(ref PoiInCode);
   
   if (ch == '\n')
   {
       tokenStruct._tag = Tag.LineFeed;
       TokenStructQueue.Enqueue(tokenStruct);
       continue;
   }

   if (ch>='0'&&ch<='9')
   {
    int value = 0;
    do
    {
     value = value * 10 + ch - '0';
     ch = scanNextChar(ref PoiInCode);
    } while (ch>='0'&&ch<='9');

    tokenStruct._tag = Tag.Num;
    tokenStruct.numValue = value;
    TokenStructQueue.Enqueue(tokenStruct);
   }
   else if ((ch>='a'&&ch<='z')||(ch>='A'&&ch<='Z'))
   {
    switch (scanFullWord(ref PoiInCode))
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
     case "//":
      tokenStruct._tag = Tag.Note;
      break;
    }
   }
   else
   {
    switch (getFullSymbal(ref PoiInCode))
    {
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
    }

    
   }




  }

 }
}

