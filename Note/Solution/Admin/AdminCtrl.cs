using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Note.Solution.Admin
{
    // 后台控制器：单例控制设置界面的开关、绑定菜单
    public class AdminCtrl : SingleTonBase<AdminCtrl>
    {
        private bool isOpen;
        public bool IsOpen
        {
            get => isOpen;
        }

        public AdminCtrl()
        {
            isOpen = false;
        }

        public MenuList BindMenuList(GameObject gameObject)
        {
            gameObject.AddComponent<MenuList>();
            return gameObject.GetComponent<MenuList>();
        }

        public void OpenAdmin()
        {
            if (isOpen == false)
            {
                isOpen = true;
                // XXX: 实现打开界面
                GameObject adminPanel = Instantiate(AdminPanel, transform.position, Quertanion.identity);
                adminPanel.AddComponent<AdminPanel>();
            }
        }

        public void CloseAdmin()
        {
            if (isOpen)
            {
                isOpen = false;
                // TODO: 实现关闭界面
            }
        }
    }
}