using System;
using System.Collections.Generic;
using UnityEngine;

namespace Note.Solution.HierarchicalTaskNetwork
{
    // HTN擅长规划，其实并不擅长时时决策
    // 因此在实际开发时，建议与有限状态机结合，将实时反馈的事交给状态机，HTN放入一个状态进行复杂行为
    public class Sample0 : MonoBehaviour
    {
        HTNPlanBuilder htn;
        void Awake()
        {
            htn = new HTNPlanBuilder();
        }

        void Start()
        {
            htn.CompoundTask()
                    .Method(() => isHurt)
                        .Enemy_Hurt(this)
                        .Enemy_Die(this)
                        .Back()
                    .Method(() => curHp <= trigger)
                        .Enemy_Combo(this, 3)
                        .Enemy_Rest(this, "victory")
                        .Back()
                    .Method(() => HTNWorld.GetWorldState<float>("PlayerHp") > 0)
                        .Enemy_Check(this)
                        .Enemy_Track(this, PlayerTrans)
                        .Enemy_Atk(this)
                        .Back()
                    .Method(() => true)
                        .Enemy_Idle(this, 3f)
                    .End();
        }

    }
}