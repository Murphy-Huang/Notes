namespace Solution.ActionSystem
{
    public class CancelTag
    {
        /// <summary>
        /// ��CancelData.Tag��Ӧ��ֻ����CancelData��tag�������tag�Ż����Cancel��ϵ
        /// </summary>
        public string Tag;

        /// <summary>
        /// ���ȼ�����
        /// ������������ڴ�֡����Cancel����ô�Ǹ����������ȼ��ᱻ�������ֵ��������
        /// </summary>
        public int PrioritizationModifier;

        /// <summary>
        /// ���ڼ���
        /// һЩcancel���������ʱ��������ض��������
        /// ������������ײ��ActionChangeInfo�У�����
        /// </summary>
        public bool Active = true;
    }
}