# 《方珂》

### Sacrifices Must Be Made



## 策划参考

### 白模示例地图

白模瓦片贴图集原图路径：

```bash
./MemoB_Minigame_/Assets/Sprites/RawTileset.png
```

示例白模场景路径：

```bash
./MemoB_Minigame_/Assets/Scenes/BattleField
BattleField/BasicGeometry: 基本地形示例
BattleField/Topography: 带有障碍物的地形示例
```

### 白模战斗关卡地图

#### 第3关：魔物花园之地穴—困兽犹斗

第3关白模战斗关卡地图场景路径：

```bash
./MemoB_Minigame_/Assets/Scenes/L3魔物花园之地穴—困兽犹斗
```

已完成`L3_1~L3_5`：

- 第1幕：死者
- 第2幕：猎人与猎物
- 第3幕：歧途
- 第4幕：多蕨怪
- 第5幕：天然防线

### 白模敌人和弹幕

白模敌人路径：

```bash
./MemoB_Minigame_/Assets/Prefabs/敌人白模
```

已按照`{类型}.{编号}{敌人名称}`分目录存放，目录下包含：

```bash	
{类型}.{编号}{敌人名称}: 敌人预制体
{类型}.{编号}{弹幕名称}: （近战除外）敌人弹幕预制体
[{类型}.{编号}{敌人名称}]: （若干）敌人弹幕预制体的子弹幕预制体
```



## LOGS

2022.11.12 12:05

- 拼写错误修订。所有脚本中的 `angel --> angle`。
- 新增白模敌人和弹幕预制体。
- 修订Enemy_Bullet_Circle.cs的逻辑错误。删除`SetChildSpeed(bullet)`之前的`bullet.SendMessage("SetBullet", speed)`以避免子弹幕`speed`属性被覆盖。（Line 54, 65）

2022.11.12 23:31

- 新增第3关白模战斗关卡地图场景`L3_1~L3_5`。
- 拼写错误修订。所有脚本中的类名`Enemy_Bulllet_Basic --> Enemy_Bullet_Basic`。
- 修改继承关系。为生成正确的子母弹效果，`Enemy_Bullet_Circle`现在继承`Enemy_Bullet_Inferior`，而不是原先的`Enemy_Bullet_Basic`。
- 修订白模敌人和弹幕预制体，重新平衡敌人和敌人弹幕数值。红眼蜘蛛、末日神官-奥术、暗骑士-巨斧、暗骑士-重剑的警戒感知距离增加；暗骑士-巨斧、暗骑士-重剑的警戒移动速度更快，并且拥有3射击轮次。

