namespace Solution.ActionSystem
{
    public class CancelTag
    {
        /// <summary>
        /// 和CancelData的tag对应，只有在CancelData的tag包含这个tag才会存在Cancel关系
        /// </summary>
        public string tag;

        /// <summary>
        /// 优先级修正
        /// 如果动作可以在此帧进行Cancel，那么那个动作的优先级会被加上这个值进行修正
        /// </summary>
        public int prioritizationModifier;

        /// <summary>
        /// 现在激活
        /// 一些cancel情况可以暂时不激活，在特定情况激活
        /// 例：当发生碰撞（ActionChangeInfo中）激活
        /// </summary>
        public bool active = true;
    }
}