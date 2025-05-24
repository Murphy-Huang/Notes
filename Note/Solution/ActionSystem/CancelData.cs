namespace Solution.ActionSystem
{
    public class CancelData
    {
        /// <summary>
        /// ��CancelTag.Tag��Ӧ��һ����������Cancel�Ķ���֡�����кܶ�
        /// </summary>
        public string[] Tag;

        /// <summary>
        /// ���ȼ�����
        /// ��������������CancelDataȥCancel��Ķ�����ʱ�򣬶��������ȼ����˼���CancelTag�����ȼ�,��Ҫ����CancelData�����ȼ�
        /// </summary>
        public int PrioritizationModifier;

        /// <summary>
        /// ָ��仯
        /// һЩ������ĳЩ������Cancel���������������и�����߸��ٵ�ָ�
        /// �Ա���������ʹ��������Ҫ����Ҹ���ȷ�Ĳ�������ʹ����
        /// </summary>
        public Command[] ChangeCommand;

        /// <summary>
        /// ��ʼ֡
        /// ���������Cancel��֮ǰ�Ķ���֮���Ǵӵڼ�֡��ʼ��
        /// </summary>
        public int StartFrame;

        /// <summary>
        /// ��ʱ������CancelTag��Ϣ
        /// </summary>
        public List<(CancelTag, int)> TempCancelTag;
    }
}