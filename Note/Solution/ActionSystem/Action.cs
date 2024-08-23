namespace Solution.ActionSystem
{
    public class Action
    {
        /// <summary>
        /// 动作名
        /// 不同的动作可以完全相等，需要巧妙的运用
        /// </summary>
        public string actionName { get; private set; }

        /// <summary>
        /// 动作帧
        /// </summary>
        public List<Frame> actionFrame;

        /// <summary>
        /// 衔接的下个动作
        /// </summary>
        public Action nextAction;

        /// <summary>
        /// 动画信息
        /// string或者其他
        /// </summary>
        public string animationInfo;

        /// <summary>
        /// 输入指令
        /// 可以交给玩家配置
        /// </summary>
        public Command[] command;

        /// <summary>
        /// 可以Cancel别的动作的信息
        /// </summary>
        public List<CancelData> CancelTags;

        /// <summary>
        /// 基础优先级
        /// </summary>
        public int prioritization;

        /// <summary>
        /// 打击信息
        /// </summary>
        public HitInfo hitInfo;


        /// <summary>
        /// 其他数值：攻击倍率
        /// </summary>
        public int multiplier;
    }
}