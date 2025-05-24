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
        /// ��ǰ����֡
        /// </summary>
        public Frame CurrentFrame;

        /// <summary>
        /// ���յĶ���
        /// </summary>
        public List<Action> ActionSet;

        /// <summary>
        /// ��ײ��¼
        /// </summary>
        public List<HitRecord> HitRecords;

        /// <summary>
        /// �������
        /// ÿ��������ӵ����List����ʱ����
        /// </summary>
        public List<Command> CommandList;

        /// <summary>
        /// ������ѡ�б�
        /// �������ȼ�ð�ݣ��ó����CancelData��Ӧ�Ķ�����Ӧ֡
        /// </summary>
        public List<(int, Action)> CandidateActionList;
    }
}