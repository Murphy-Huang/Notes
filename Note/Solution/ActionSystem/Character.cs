using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Solution.ActionSystem
{
    public class Character
    {
        /// <summary>
        /// 当前动作帧
        /// </summary>
        public Frame CurrentFrame;

        /// <summary>
        /// 掌握的动作
        /// </summary>
        public List<Action> ActionSet;

        /// <summary>
        /// 碰撞记录
        /// </summary>
        public List<HitRecord> HitRecords;

        /// <summary>
        /// 输入队列
        /// 每次输入添加到这个List，定时清理
        /// </summary>
        public List<Command> CommandList;

        /// <summary>
        /// 动作候选列表
        /// 根据优先级冒泡，得出最高CancelData对应的动作对应帧
        /// </summary>
        public List<(int, Action)> CandidateActionList;
    }
}