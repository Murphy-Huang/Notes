using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Note.Solution.Admin
{
    // 主界面（示例）
    public class MainView_Sample : View
    {
        GameObject grid;
        MenuList menuList;
        Transform adminPanel;
        // 备份数据
        Dictionary<string, int> tempData;

        // 创建的时候初始化一次（需要被调用）        
        public void Init()
        {
            menuList.Clear();

            grid = transform.Find("grid");
            menuList = AdminCtrl.Instance。BindMenuList(grid);
            // 添加菜单列表
            menuList.SetMenu("声音", OnInit1, OnSelect1, OnUp1, OnDown1);
            menuList.SetMenu("语言", OnInit2, OnSelect2, OnUp2, OnDown2);
            menuList.SetMenu("",)
            // 添加完后初始化所有选项
            menuList.Render();
        }
#region 外部接口
        // 获取管理设置界面（控制界面切换）
        public void Enter(Transform AdminPanel, object data)
        {
            // 备份数据
            // 页面内容初始化
        }

        public void Leave()
        {}

        public void CallUp()
        {
            menuList.CallUp();
        }

        public void CallDown()
        {
            menuList.CallDown();
        }

        public void CallSelect()
        {

        }

        public void CallQuit()
        {
            OnExit();
        }

#endregion

#region 操作回调接口
        private void Show1(Menu menu, int value)
        {
            menu.value = value;
            menuList.UpdateMenus();
        }
        private void OnInit1(Menu menu)
        {
            // TODO:数据类型需自定义
            int value = tempData["value name"];
            Show1(menu, value);
        }
        private bool OnSelect1()
        {
            return false;
        }
        private void OnUp1(Menu menu)
        {
            Show1(menu, ChangeValue("value name", true, false));
        }
        private void OnDown1(Menu menu)
        {
            Show1(menu, ChangeValue("value name", false, false));
        }


        private bool HasChange()
        {}

        private int ChangeValue(string name, bool isUp, bool isLoop)
        {}

        private IEnumerator SaveGameConfig()
        {}

        private void OnExit()
        {
            if (HasChange())
            {
                StartCoroutine(SaveGameConfig());
                adminPanel.ShowView(ViewEnum.Main);
            }
            else 
            {
                adminPanel.ShowView(ViewEnum.Main);
            }
        }
#endregion
    }
}