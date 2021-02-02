# PoiIn 字节码结构分析

### 破隐游戏循环周期分析
#### 1.  进入循环： 游戏开始，初始化人物，数据进入缓存区
#### 2.  解析器解析人物卡
#### 3.  监听玩家行为，并作出反馈
      3.1 玩家移动，扣除AP
      3.2 玩家普通攻击，判断合法性，读取普通攻击附加效果(如果附加效果中包含接收行为类的函数，则接受玩家行为)并扣除对应属性值，建立(取消)对应的状态
      3.3 玩家使用技能，判断合法性，读取技能对应的破隐码(如果附加效果中包含接收行为类的函数，则接受玩家行为)，并扣除相应的属性值，建立(取消)对应的状态
      3.4 玩家使用道具，判断合法性，读取道具有关的破隐码(如果附加效果中包含接收行为类的函数，则接受玩家行为)，并扣除相应的属性值，建立(取消)对应的状态
####  4.  进入敌人AI回合
####  5.  再次进入循环
        5.1 玩家和敌人都存活，回到循环第2步
        5.2 玩家存活，敌人死亡，战斗结束
        5.3 敌人存活，玩家死亡，战斗结束
      

### 希望实现的功能

+ 能通过字节码的编码，在武器被使用时，在技能被释放时，由引擎自动完成武器和技能的特殊效果，包括人物数值的增减（分为倍率和固定值两种），在棋盘上召唤东西，确认是否持有某物品，各种效果：中毒效果（每次轮到该角色开始时减属性值，常用的是减hp和sp）、持续时间、持续时间结束后会触发事件、麻痹效果（跳过该角色的行动轮若干次）

社长-amidu 20:21:31
还有让角色无法使用某些指令（比如禁用行动、禁用移动、禁用道具等）



### 上述功能分解后所需的具体部分

- 前端：字节码解释器，对字节码所记载的操作进行解释，包括进行词性分析，语法分析，语义分析等
- 后端：为字节码提供所需要的基础函数，包括角色参数的调用，游戏周期中等待监听用户的行为指令，骰子的调用等

### 字节码具体结构
- 关键符号
    - ```C#
       =
          //赋值语句
        +
          //算术加法符号
        -
          //算术减法符号
        >   ||  <  ||  ==
          //界符，大于，小于，等于，界符返回Bool，合法则true，非法false
        &&
          //且的逻辑连接符
        ||
          //或的逻辑连接符
        //
          //注释符号
        ->
          //将某参数在一个行动轮中临时性增加或减少，例如
        Self.PRA -> -5;
          //将自己的PRA在下一个行动轮-5
        +-  
          //将某参数永久性地相对改变某值
        Self.PRA +- +5;
          //将自己的PRA永久性的+5
        **
          //将某参数临时性地乘以某倍率，
        */
          //将某参数永久性的除以某倍率
        ()
          //算术括号符，用于改变计算式的优先级
      ```
- 关键词
    - ```C#
      Self      //自己的值标识符
        Self.HP
        Self.SP
        Self.AP
        Self.PRA
        Self.ACA
        Self.AGI
        //调用自己人物卡的属性值,返回Int
      Target    //目标的值标识符
        Target.HP
        Target.SP
        Target.AP
        Target.PRA
        Target.ACA
        Target.AGI
        //调用目标人物卡的属性值,返回Int
      ```
- 关键函数
    - ```C#
      ActionEnum(){
        throw new NotImplementedException();
      }
      //行动周期函数，标记了函数内的效果作用于哪一个破隐生命周期，例如
      Normal_Attack(){
        Self.PRA */ 1.2;
        Target.PRA */ 0.8;
      }

      Dice()  
          //调用100面骰子,返回一个int值
      Instantiate(String characterName,Vector3Int position)
          //在position位置生成一个角色名为characterName的角色

      Round(Int RoundNumber){
          throw new NotImplementedException();
      }
          //多行动轮调用函数，配合临时性的参数改变
      Enum StateTarget{
        Self,
        Target
      }
        //状态创造函数对应的枚举
      CreateState(StateTarget target,String StateName,String PoiInCode_for_State) ;

      Enum CycleName{
          MOVE,
          NORMAL_ATTACK,
          USESKILL,
          USEITEM
      }
      disruptCycle(StateTarget Target,CycleName thisCycle,Int ConsistRound);
        //参见破隐游戏循环周期分析中第三步，阻断对应目标的对应周期，持续ConsistRound个行动轮

      ```
- 关键语句
  - if语句：
    - ```C#
      if (expr){
        throw new NotImplementedException();
      }
      ```
   #### 例如：
    - ```C#
        //A装备的武器是α，α的攻击范围是1~4，所以正好可以打到B。A的操练是50，他骰出了20，差值为30。
        Damage = Dice() - Self.PRA;
        if(Damage > 0){
          return Damage;
        }
      ```
      