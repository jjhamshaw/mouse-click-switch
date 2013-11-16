using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using MouseClickSwitch.Properties;

namespace MouseClickSwitch
{
    public class SwitchButtonsTrayIcon : IDisposable
    {
        private readonly NotifyIcon _notifyIcon;
        private readonly Icon _leftHandedIcon;
        private readonly Icon _rightHandedIcon;
        private readonly bool _mouseButtonsSwapped;

        [DllImport("user32.dll")]
        private static extern Int32 SwapMouseButton(Int32 bSwap);

        public SwitchButtonsTrayIcon()
        {
            _leftHandedIcon = Resources.cursor_left;
            _rightHandedIcon = Resources.cursor_right;
            _notifyIcon = new NotifyIcon();
            _mouseButtonsSwapped = SystemInformation.MouseButtonsSwapped;
        }

        public void Display()
        {
            _notifyIcon.MouseClick += NotifyIconMouseClick;
            _notifyIcon.Icon = _mouseButtonsSwapped ? _leftHandedIcon : _rightHandedIcon;
            _notifyIcon.Text = "Youuuuuuu gotta switch!";
            _notifyIcon.Visible = true;
        }

        public void Dispose()
        {
            _notifyIcon.Dispose();
        }

        ContextMenu CreateContextMenu()
        {
            var menu = new ContextMenu();

            menu.MenuItems.Add("Exit", ExitAppOnClick);
            if (SystemInformation.MouseButtonsSwapped)
            {
                menu.MenuItems.Add("Switch to righty", UnSwapMouseButtonsOnClick);
            }
            else
            {
                menu.MenuItems.Add("Switch to lefty", SwapMouseButtonsOnClick);
            }
    
            return menu;
        }

        void UnSwapMouseButtonsOnClick(object sender, EventArgs e)
        {
            SwitchMouseButtonsForRightHandedUse();
        }

        void SwapMouseButtonsOnClick(object sender, EventArgs e)
        {
            SwitchMouseButtonsForLeftHandedUse();
        }

        void NotifyIconMouseClick(object sender, MouseEventArgs e)
        {
            if (SystemInformation.MouseButtonsSwapped)
            {
                SwitchMouseButtonsForRightHandedUse();
            }
            else
            {
                SwitchMouseButtonsForLeftHandedUse();
            }
        }

        void SwitchMouseButtonsForLeftHandedUse()
        {
            SwapMouseButton(1);
            _notifyIcon.Icon = _leftHandedIcon;
            SetTheContextMenu();
        }

        void SwitchMouseButtonsForRightHandedUse()
        {
            SwapMouseButton(0);
            _notifyIcon.Icon = _rightHandedIcon;
            SetTheContextMenu();
        }

        void SetTheContextMenu()
        {
            _notifyIcon.ContextMenu = CreateContextMenu();
        }

        void ExitAppOnClick(object sender, EventArgs eventArgs)
        {
            Application.Exit();
        }
    }
}
