# 《方珂》

### Sacrifices Must Be Made



## 策划参考

### 白模地图

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

- 拼写错误修订。所有脚本中的 angel --> angle。
- 新增白膜敌人和弹幕预制体。
- 修订Enemy_Bullet_Circle.cs的逻辑错误。删除`SetChildSpeed(bullet)`之前的`bullet.SendMessage("SetBullet", speed)`以避免子弹幕`speed`属性被覆盖。（Line 54, 65）

