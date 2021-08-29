using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ResidentProgramSample
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Thread thread;
        private bool aborted = false;

        private System.Windows.Forms.NotifyIcon notifyIcon;

        // 起動
        protected override void OnStartup(StartupEventArgs e)
        {
            Debug.WriteLine("OnStartup");

            base.OnStartup(e);

            var icon = GetResourceStream(new Uri("icon.ico", UriKind.Relative)).Stream;

            var menu = new System.Windows.Forms.ContextMenuStrip();
            menu.Items.Add("終了", null, Exit_Click);

            notifyIcon = new System.Windows.Forms.NotifyIcon
            {
                Visible = true,
                Icon = new System.Drawing.Icon(icon),
                Text = "ResidentProgramSample",
                ContextMenuStrip = menu
            };

            notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(NotifyIcon_Click);

            // 定周期処理の開始
            this.thread = new Thread(intervalProcess);
            this.thread.Start();
        }

        // アイコンクリック
        private void NotifyIcon_Click(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                ResidentProgramSample.MainWindow.GetInstance().Show();
            }
        }

        // 終了
        private void Exit_Click(object sender, EventArgs e)
        {
            // 定周期処理をアボート
            this.aborted = true;
            // タスクトレイのアイコンを削除（これがないとアイコンが残り続ける）
            notifyIcon.Dispose();
            // アプリ終了
            Shutdown();
        }

        // 定周期処理
        private void intervalProcess(){
            // アボートされるまで繰り返す
            var index = 1;
            while(!this.aborted){
                Debug.WriteLine("debug:" + index) ;
                index++;
                Thread.Sleep(5000);
            }
        }
    }
}
