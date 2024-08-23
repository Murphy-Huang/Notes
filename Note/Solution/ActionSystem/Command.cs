namespace Solution.ActionSystem
{
    public class Command
    {
        /// <summary>
        /// 指令
        /// 需要把来自设备的输入进行“翻译”，翻译的过程依赖于Config中配置
        /// </summary>
        public string command;

        /// <summary>
        /// 按下
        /// 输入‘糖’
        /// </summary>
        public bool press;

        /// <summary>
        /// 时间戳
        /// int或者其他
        /// </summary>
        public float timestamp;
    }
}