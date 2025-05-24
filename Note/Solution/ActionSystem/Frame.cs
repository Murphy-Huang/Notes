using UnityEngine;

namespace Solution.ActionSystem
{
    public class Frame
    {
        /// <summary>
        /// �����ؼ�֡
        /// </summary>
        public float AnimationKeyframe;

        /// <summary>
        /// ѭ��֡��
        /// </summary>
        public int LoopFrame;

        /// <summary>
        /// Cancel��Ϣ
        /// ��ɫ������֡��ʱ��������Щ������Cancel
        /// </summary>
        public List<CancelTag> CancelInfo;

        /// <summary>
        /// ������ײ��(Rect[]\Box[])
        /// 2D��Ϸ��ͨ����Rect������aabb����������ܣ���3D��Ϸ����Box
        /// </summary>
        public BoxCollider[] PhysicalBoxes;

        /// <summary>
        /// ������
        /// </summary>
        public AttackHitBox[] AttackBoxes;

        /// <summary>
        /// �ܻ���
        /// </summary>
        public HurtHitBox[] HurtBoxes;

        /// <summary>
        /// ��һ֡
        /// </summary>
        public Frame NextFrame;
    }
}