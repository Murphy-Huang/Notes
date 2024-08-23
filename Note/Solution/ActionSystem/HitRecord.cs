namespace Solution.ActionSystem
{
    public class HitRecord
    {
        /// <summary>
        /// 目标
        /// </summary>
        public Character character;

        /// <summary>
        /// 还剩多少帧可以打击目标
        /// </summary>
        public int framesLeftCanHit;

        /// <summary>
        /// 还能打中多少次
        /// </summary>
        public int timesCanHit;
    }
}