namespace Solution.ActionSystem
{
    /// <summary>
    /// �����Ϣ
    /// ���ڡ���д��HitRecord�Ĺ���
    /// </summary>
    public class HitInfo
    {
        /// <summary>
        /// ͬһ��Ŀ�����д���
        /// ������һ��Ŀ���HitRecordʱ��ΪHitRecord.timesCanHit��ֵ
        /// </summary>
        public int CountHitOnSameCharacter;

        /// <summary>
        /// ���м��֡��
        /// ΪHitRecord.framesLeftCanHit��ֵ
        /// </summary>
        public int CountHitIntervalFrames;
    }
}