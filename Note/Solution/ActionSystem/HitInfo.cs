namespace Solution.ActionSystem
{
    /// <summary>
    /// 打击信息
    /// 用于“改写”HitRecord的规则
    /// </summary>
    public class HitInfo
    {
        /// <summary>
        /// 同一个目标命中次数
        /// 当创建一个目标的HitRecord时，为HitRecord.timesCanHit赋值
        /// </summary>
        public int countHitOnSameCharacter;

        /// <summary>
        /// 命中间隔帧数
        /// 为HitRecord.framesLeftCanHit赋值
        /// </summary>
        public int countHitIntervalFrames;
    }
}