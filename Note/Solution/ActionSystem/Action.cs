namespace Solution.ActionSystem
{
    public class Action
    {
        /// <summary>
        /// ������
        /// ��ͬ�Ķ���������ȫ��ȣ���Ҫ���������
        /// </summary>
        public string ActionName { get; private set; }

        /// <summary>
        /// ����֡
        /// </summary>
        public List<Frame> ActionFrame;

        /// <summary>
        /// �νӵ��¸�����
        /// </summary>
        public Action NextAction;

        /// <summary>
        /// ������Ϣ
        /// string��������
        /// </summary>
        public string AnimationInfo;

        /// <summary>
        /// ����ָ��
        /// ���Խ����������
        /// </summary>
        public Command[] Command;

        /// <summary>
        /// ����Cancel��Ķ�������Ϣ
        /// </summary>
        public List<CancelData> CancelTags;

        /// <summary>
        /// �������ȼ�
        /// </summary>
        public int Prioritization;

        /// <summary>
        /// �����Ϣ
        /// </summary>
        public HitInfo HitInfo;


        /// <summary>
        /// ������ֵ����������
        /// </summary>
        public int Multiplier;
    }
}