using UnityEngine;

namespace Solution.ActionSystem
{
    public class Frame
    {
        /// <summary>
        /// 动画关键帧
        /// </summary>
        public float animationKeyframe;

        /// <summary>
        /// 循环帧数
        /// </summary>
        public int loopFrame;

        /// <summary>
        /// Cancel信息
        /// 角色处于这帧的时候，允许被那些动作所Cancel
        /// </summary>
        public List<CancelTag> cancelInfo;

        /// <summary>
        /// 身体碰撞框(Rect[]\Box[])
        /// 2D游戏中通常是Rect（便于aabb计算提高性能），3D游戏中是Box
        /// </summary>
        public BoxCollider[] physicalBoxes;

        /// <summary>
        /// 攻击框
        /// </summary>
        public AttackHitBox[] attackBoxes;

        /// <summary>
        /// 受击框
        /// </summary>
        public HurtHitBox[] hurtBoxes;

        /// <summary>
        /// 下一帧
        /// </summary>
        public Frame nextFrame;
    }
}