# Unity Script Framework
## 来自美工的方案2：
把探索战斗做成一种系统，全部放在地图上，通过交互，获得线索
  大地图（地图系统）：通过点击进入小地图探索，走到线索处，交互键，获得线索，放入背包。遇到敌人则进入战斗系统。
  消耗时间：在探索时消耗时间轮，时间轮消耗完则进入距离。
 主角拥有特定的战斗模板，炮灰换皮，不能更换武器类型。
文档：要什么功能
 战棋战斗需要打群架，剧情冲突问题。敌人种类：洗脑怪，和洗脑怪附身的尸体。敌人种类太少。解决方案：大召唤术。
自由探索部分：
  移动：上下左右，点击，两个都做。
 交互键：交互键和鼠标点击。
 对话会议系统：分类。
 战斗阶段：
 移动：点击
 命中率：基础数值加投骰子
部位系统：对于不同部位造成有效伤害，可以造成debuff。
调查：调查出怪物的弱点对于战斗有益。
行动点系统：移动行动点，攻击行动点。
每个角色固定。

## 程序框架化分解
- 地图系统
  - 时间限制器
  - 棋盘框架
    - 棋盘主体
    - 线索（类）的派发（实例化）
    - 玩家的寻路系统
  

- 战斗系统
    - 移动系统
      - asdw键盘移动:四个自由度,移动物体为2D贴图
      - 鼠标点击寻路方案
    - 事件动画触发状态机
    - 伤害判断系统
      - 命中率骰子
      - 命中部位和要害
    - 行动点限制器

- 敌人系统
  - 敌人生成器
  - 敌人行为器

- 玩家系统
    - 战斗模板

## 地图系统
- 时间限制器：
1. 脚本名：timeLimitation
2. 构造timePass方法，接收外部时间流逝的指令和参数。
   <br/>timeLimitation.timePass(int passedSeconds)
3. 创建一个canvas实例，利用sprite制作简单的计时器
4. 建立一个公共变量，命名为timeRest，将剩余时间赋值给timeRest
5. 要点：避免使用开销过大的脚本生命周期,建议使用协程
6. [参考资料1：实现倒计时的功能](https://blog.csdn.net/qq_42672770/article/details/105603707?utm_medium=distribute.pc_relevant.none-task-blog-title-2&spm=1001.2101.3001.4242)   
7. [参考资料2：sprite动画总结](https://blog.csdn.net/WangHaoDiablo/article/details/52838583?locationNum=10&fps=1)

- 棋盘框架
1. 棋盘框架脚本：chessBoardManager
2. 实现功能：<br/>
   1. 棋盘主体：start()时生成黑白相间的10*10棋盘（预制体），每一片上挂载脚本cell记录公开变量vector2Int作为相对位置的输出口
   2. 线索派发器：序列化一个List，命名为clueList，定义结构体，命名为clue，在inspector视窗里提供编辑clue数量，clue位置和clue种类（颜色暂定）的功能
   3. 玩家移动系统
      1. 获取玩家位置，将玩家下一步可以走的cell（十字型）变黄；射线检测，将鼠标所指的cell变蓝
      2. 输入事件检测1：如果玩家点击可行的黄色cell，将玩家角色移动到对应的cell上
      3. 输入事件检测2：玩家通过awds键向对应格子移动
3. [参考资料1：国际象棋案例](https://blog.csdn.net/kmyhy/article/details/82690409)
4. [参考资料2：生成国际象棋棋盘](https://blog.csdn.net/qq_43427963/article/details/98474354?utm_medium=distribute.pc_relevant.none-task-blog-BlogCommendFromBaidu-8.not_use_machine_learn_pai&depth_1-utm_source=distribute.pc_relevant.none-task-blog-BlogCommendFromBaidu-8.not_use_machine_learn_pai)
