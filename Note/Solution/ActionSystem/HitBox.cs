using UnityEngine;

namespace Solution
{
    public class HitBox
    {
        /// <summary>
        /// 碰撞框体（Rect\Box）
        /// 这个框体相对于角色坐标的偏移量
        /// </summary>
        public BoxCollider[] CollisionBoxes;

        /// <summary>
        /// 自身动作变化信息
        /// 当这个动作的优先级高于对手框的时候，那么我自己会如何改变动作，就取决于这个信息了
        /// </summary>
        public ActionChangeInfo SelfActionChangeInfo;

        /// <summary>
        /// 对手动作变化信息
        /// 当这个动作的优先级高于对手框的时候，那么对手会如何改变动作，就取决于这个信息了
        /// </summary>
        public ActionChangeInfo CounterpartActionChangeInfo;

        /// <summary>
        /// 优先级
        /// 将一个角色的攻击框碰到其他人受击框的信息列表冒泡，先根据攻击框的优先级冒泡，攻击框优先级最高的那个可能同时还碰到了很多受击框，此时冒泡出受击框优先级最高的，就是这次碰撞的“有效碰撞信息（双方的框）”
        /// 优先级对比，得出如果攻击框的优先级>（而非>=）受击框的优先级，则采用攻击方的“动作变化信息”（自身和对手那两个），否则采用受击方的
        /// </summary>
        public int Prioritization;

        /// <summary>
        /// 对称标志：碰撞框标志相同时框体才生效
        /// </summary>
        public string[] CounterpartMark;
    }
}