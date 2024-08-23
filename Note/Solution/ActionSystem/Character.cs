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
        public Frame currentFrame;

        /// <summary>
        /// 掌握的动作
        /// </summary>
        public List<Action> actionSet;

        /// <summary>
        /// 碰撞记录
        /// </summary>
        public List<HitRecord> hitRecords;

        /// <summary>
        /// 输入队列
        /// 每次输入添加到这个List，定时清理
        /// </summary>
        public List<Command> commandList;

        /// <summary>
        /// 动作候选列表
        /// 根据优先级冒泡，得出最高CancelData对应的动作对应帧
        /// </summary>
        public List<(int, Action)> candidateActionList;
    }
}