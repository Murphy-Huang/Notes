namespace Solution.ActionSystem
{
    public class CancelData
    {
        /// <summary>
        /// 和CancelTag的tag对应，一个动作可以Cancel的动作帧可以有很多
        /// </summary>
        public string[] tag;

        /// <summary>
        /// 优先级修正
        /// 当这个动作走这个CancelData去Cancel别的动作的时候，动作的优先级除了加上CancelTag的优先级,还要加上CancelData的优先级
        /// </summary>
        public int prioritizationModifier;

        /// <summary>
        /// 指令变化
        /// 一些动作在某些条件下Cancel其他动作，可以有更多或者更少的指令，
        /// 以便更方便玩家使出来或者要求玩家更精确的操作才能使出来
        /// </summary>
        public Command[] changeCommand;

        /// <summary>
        /// 起始帧
        /// 当这个动作Cancel掉之前的动作之后，是从第几帧开始的
        /// </summary>
        public int startFrame;

        /// <summary>
        /// 临时开启的CancelTag信息
        /// </summary>
        public List<(CancelTag, int)> tempCancelTag;
    }
}