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

已完成`L3_1~L3_8`：

- 第1幕：死者
- 第2幕：猎人与猎物
- 第3幕：歧途
- 第4幕：多蕨怪
- 第5幕：天然防线
- 第6幕：九孔泉
- 第7幕：“埋伏”
- 第8幕：不休回廊

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
- 新增若干**排序图层**并重新分配排序图层。
- 拼写错误修订。所有脚本中的类名`Enemy_Bulllet_Basic --> Enemy_Bullet_Basic`。
- 修改继承关系。为生成正确的子母弹效果，`Enemy_Bullet_Circle`现在继承`Enemy_Bullet_Inferior`，而不是原先的`Enemy_Bullet_Basic`。
- 修订白模敌人和弹幕预制体，重新平衡敌人和敌人弹幕数值。红眼蜘蛛、末日神官-奥术、暗骑士-巨斧、暗骑士-重剑的警戒感知距离增加；暗骑士-巨斧、暗骑士-重剑的警戒移动速度更快，并且拥有3射击轮次。

2022.11.13 12:55

- 新增第3关白模战斗关卡地图场景`L3_6~L3_8`。
- 规范化和精简化白模示例地图`BattleField`。  现在白模示例地图场景中只存在`grid`，配置好`tag`=`layer`=`Border`。
- 修订白模敌人和弹幕预制体，重新平衡敌人和敌人弹幕数值。削弱了末日蔓延者-爆种、末日蔓延者-毒刺，警戒感知距离和移动速度降低，末日蔓延者-毒刺的攻击频率降低。

2022.11.14 00:52

- 修订白模敌人和弹幕预制体，重新平衡敌人和敌人弹幕数值。略微减少了暗骑士、末日蔓延者的回复掉落量，上调末日神官-奥术、暗骑士、末日蔓延者的子弹威力。

2022.11.14 19:55

- 修订白模敌人和弹幕预制体，重新平衡敌人数值。下调暗骑士、末日蔓延者-爆种的警戒感知距离。
- 增加可调节的全屏环绕特效，颜色可调的火焰环绕和晶格环绕（可能存在渲染性能问题）。
