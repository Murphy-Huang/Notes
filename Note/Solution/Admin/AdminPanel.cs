using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Note.Solution.Admin
{
    // 管理设置界面，挂载在设置界面的背景上
    public class AdminPanel: using UnityEngine;
    
    public class AdminPanel : MonoBehaviour 
    {
        private List<View> viewList;
        private int curIndex;

        private void AddListener()
        {
            EventCenter.Instance.AddListener(AckButton);
        }
        private void RemoveListener()
        {
            EventCenter.Instance.RemoveListener(AckButton);
        }

        private void OnEnable() {
            AddListener();
        }

        private void OnDisable() {
            RemoveListener();
        }

        private void Awake() 
        {
            // 获取UI组件
        }

        private void Start() 
        {
            // 数据初始化
            InitViews();
        }

        private void InitViews()
        {
            for (int i = 0; i < 50; ++i)
            {
                View view = CreateView(i);
                if (view != null)
                {
                    viewList.Add(view);
                }
            }
        }

        private GameObject CreateView(int index)
        {
            GameObject view = null;
            if (index == ViewEnum.Main)
            {
                GameObject prefab = ABManager.Load("Prefab Name");
                view = Instantiate(prefab, transform.position, Quaternion.identity);
                view.parent = this.gameObject;
                view.AddComponent<MainView_Sample>();
            }
            if (view)
            {
                view.Init();
            }
            return view;
        }

        public void ShowView(int index, object data)
        {
            for (int i = 0; i < 50; ++i)
            {
                viewList[i]?.SetActive(i == index);
                if (i == index)
                {
                    viewList[i].Enter(transform, data);
                }
            }
            if (curIndex != index)
            {
                viewList[curIndex].Leave();
            }
            curIndex = index;
        }

        public void AckButton(Array data)
        {
            ClickButton(data[1], data[2]);
        }

        private void ClickButton(int key_type, int key_state)
        {
            View view = viewList[curIndex];
            if (key_type >= 1 && key_type <= 4)
            {
                if (key_type == 1) view.CallUp();
                if (key_type == 2) view.CallDown();
                if (key_type == 3) view.CallLeft();
                if (key_type == 4) view.CallRight();
            }
            if (key_type == 5 && key_state == 3) view.CallSelect();
            if (key_type == 6 && key_state == 3) view.CallQuit();
        }
    }
}