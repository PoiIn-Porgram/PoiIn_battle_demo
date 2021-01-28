# PoiIn游戏AI设计框架
- 基本框架结构
    - 战略处：战术战略思考的能力(由策划填写战略倾向配置文件)
    - 决策层：规则层的enemyAI函数
    - 运动层：多重使用实例层的角色移动函数和规则层的角色行为函数

- AI行为
    - 靠近 ```public void seek(vector3Int targetPosition)```
        - 将所有行动点用于靠近目标
    - 抵达```public void arrive(vector3Int targetPosition)```
        - 向目标贴脸，行动点可能剩余
    - 逃脱```public void flee(vector3Int fromPosition)```
        - 将所有行动点用于逃离某一目标
    

    