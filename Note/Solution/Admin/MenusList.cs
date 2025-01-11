using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Note.Solution.Admin
{
    // 界面内，菜单列表的通用实现
    public class MenuList: using UnityEngine;
    
    public class MenuList : MonoBehaviour 
    {
        public struct Menu
        {
            public string Name;
            public Action initFunc;
            public Action selectFunc;
            public Action upFunc;
            public Action downFunc;
            public bool isSelect;
            public string value;
            public int index;
            public ItemComponent component;
        }
        private struct ItemComponent
        {
            public GameObject gameObject;
            public Transform titleTXT;
            public Transform contentTXT;
        }
        private List<Menu> menus;
        private int curIndex;
        private int curSelect;
        private GameObject itemObj;
        
        private void Awake() {
            // 获取基础组件
            // 菜单缓存
            menus = new List<Menu>();
            // 临时属性
            curIndex = 0;
            curSelect = -1;
        }

        private void Update() {}

        // 设置菜单按钮选项
        public void SetMenu(string menuName, Action initAction, Action selectAction, Action upAction, Action downAction) 
        {
            Menu menu= new Menus();
            menu.name = menuName;
            menu.initFunc = initAction;
            menu.selectFunc = selectFunc;
            menu.upFunc = upAction;
            menu.downFunc = downAction;
            menu.isSelect = false;
            menu.value = "";
            menu.component = null;
            menus.Add(menu);
            menu.index = menus.Size();
        }

        // 渲染菜单，获取所有选项中的可视组件
        public void Render()
        {
            for (int i = 0; i < menus.Size(); ++i)
            {
                GameObject menuObj = Instantiate(itemObj, transform.position, Quaternion.identity);
                ItemComponent itemComponent = new ItemComponent();
                itemComponent.gameObject = menuObj;
                itemComponent.titleTXT = menuObj.transform.Find("title").GetComponent<text>();
                itemComponent.contentTXT = menuObj.transform.Find("content").GetComponent<text>();
            }
            PreSelectMenu(curIndex);
        }

        // 预选中某个菜单
        private void PreSelectMenu(int index)
        {
            if (index > 0 && index < menus.Size())
            {
                for (int i = 0; i < menus.Size(); ++i)
                {
                    menus[i].isSelect = i == index;
                }
                curIndex = index;
            }
            UpdateMenus();
        }

        // 刷新菜单信息
        private void UpdateMenus()
        {
            for (int i = 0; i < menus.Size(); ++i)
            {
                // 菜单刷新逻辑
            }
        }

        // 初始化所有菜单选项
        public void InitMenus()
        {
            for (int i = 0; i < menus.Size(); ++i)
            {
                menus[i].initFunc();
            }
        }

        // 闪烁效果
        private void Blur(Menu menu)
        {
            menu.component.titleTXT:DOFade(0.5,0.3):SetLoops(-1, DG.Tweening.LoopType.Yoyo);
            menu.component.contentTXT:DOFade(0.5,0.3):SetLoops(-1, DG.Tweening.LoopType.Yoyo);
        }

        // 取消闪烁效果
        private void CancelBlur(Menu menu)
        {
            menu.component.titleTXT:DOKill();
            menu.component.contentTXT:DOKill();
            menu.component.titleTXT.color = Color(0,0,0,0);
            menu.component.contentTXT.color = Color(0,0,0,0);
        }

        // 当前是否有确认选中的选项
        public void IsSelectMenu()
        {
            return curSelect != -1;
        }

#region 输入
        // 确认键
        public void CallSelect()
        {
            if (menus[curSelect].selectFunc != null)
            {
                if (Action selectFunc = menus[curSelect].selectFunc != null && selectFunc())
                else
                {
                    if (!IsSelectMenu())
                    {
                        curSelect = curIndex;
                        Blur(menus[curSelect]);
                    }
                    else
                    {
                        CancelBlur(menus[curSelect]);
                        curSelect = -1;
                    }
                }
            }
        }

        // 上
        public void CallUp()
        {
            if (!IsSelectMenu())
            {
                PreSelectMenu(curIndex - 1);
            }
            else 
            {
                Action upFunc = menus[curSelect].upFunc;
                if (upFunc != null)
                {
                    menus[curSelect].upFunc(menus[curSelect]);
                }
            }
        }

        // 下
        public void CallDown()
        {
            if (!IsSelectMenu())
            {
                PreSelectMenu(curIndex + 1);
            }
            else 
            {
                Action downFunc = menus[curSelect].downFunc;
                if (upFunc != null)
                {
                    menus[curSelect].downFunc(menus[curSelect]);
                }
            }
        }
#endregion
    }
}