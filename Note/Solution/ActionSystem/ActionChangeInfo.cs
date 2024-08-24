namespace Solution.ActionSystem
{
    /// <summary>
    /// 变化类型
    /// </summary>
    public enum ActionType
    {

    }

    public class ActionChangeInfo
    {
        /// <summary>
        /// 保持现在的动作，或者变化到某个指定动作
        /// </summary>
        public ActionType ChangeAction;

        /// <summary>
        /// 变化为动作
        /// 如果类型是变化为指定名称的动作，那么如果角色存在名字符合这个的动作（可能会有好几个同名的动作，每一个都要进行一次判断），就会判断是否被允许Cancel，如果允许就会被加入候选列表
        /// </summary>
        public string ChangeToAction;

        /// <summary>
        /// 起始帧
        /// 变化为那个动作起始帧是第几帧
        /// </summary>
        public Frame StartFrame;

        /// <summary>
        /// 临时开启的CancelTag信息（实现连招）
        /// 变化动作后，临时开启若干帧（int）的CancelTag
        /// </summary>
        public List<(CancelTag, int)> TempCancelTag;
    }
}