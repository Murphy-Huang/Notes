using UnityEngine;

namespace Solution
{
    public class HitBox
    {
        /// <summary>
        /// ��ײ���壨Rect\Box��
        /// �����������ڽ�ɫ�����ƫ����
        /// </summary>
        public BoxCollider[] CollisionBoxes;

        /// <summary>
        /// �������仯��Ϣ
        /// ��������������ȼ����ڶ��ֿ��ʱ����ô���Լ�����θı䶯������ȡ���������Ϣ��
        /// </summary>
        public ActionChangeInfo SelfActionChangeInfo;

        /// <summary>
        /// ���ֶ����仯��Ϣ
        /// ��������������ȼ����ڶ��ֿ��ʱ����ô���ֻ���θı䶯������ȡ���������Ϣ��
        /// </summary>
        public ActionChangeInfo CounterpartActionChangeInfo;

        /// <summary>
        /// ���ȼ�
        /// ��һ����ɫ�Ĺ����������������ܻ������Ϣ�б�ð�ݣ��ȸ��ݹ���������ȼ�ð�ݣ����������ȼ���ߵ��Ǹ�����ͬʱ�������˺ܶ��ܻ��򣬴�ʱð�ݳ��ܻ������ȼ���ߵģ����������ײ�ġ���Ч��ײ��Ϣ��˫���Ŀ򣩡�
        /// ���ȼ��Աȣ��ó��������������ȼ�>������>=���ܻ�������ȼ�������ù������ġ������仯��Ϣ��������Ͷ���������������������ܻ�����
        /// </summary>
        public int Prioritization;

        /// <summary>
        /// �ԳƱ�־����ײ���־��ͬʱ�������Ч
        /// </summary>
        public string[] CounterpartMark;
    }
}