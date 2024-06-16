## 待做：
- celeste源码D:\Personal\Download\IDMDownload\Celeste-master，DOTween
- ecs架构，行为树
- unity格斗游戏
- 游戏引擎：https://www.thisisgame.com.cn/tutorial?book=cpp-game-engine-book&lang=zh&md=Introduction.md
- [] Unity_Graphic(Graphic的具体实现)

## 待优化：
### RPG3D
1. 敌人的扇形侦察
2. 敌人选择最近的巡逻点
### RPG2D
1. player起跳不能马上控制左右=》马上控制左右
2. player下落触地速度为零=》触地带有一定惯性
3. player攻击手感：在一个动画未完成中多次按下攻击键；动画之间有明显的进入idle状态动画
4. player碰撞enemy的收击反馈 enemy被打到升天
5. 黑洞：enemy受击反馈延时到黑洞收缩后
6. crystalskill整理
7. 音效区域在立马出去又进来的情况，锁？
8. 相机移动

## 新技能设想：
1. 物体堆积（不删除gameobejct）技能

## 已解决
1. ~~OnPointer函数常有检测不到的情况~~
2. ~~大力被打击与高伤害~~