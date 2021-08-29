using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ResidentProgramSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static MainWindow _Instance;
        public static MainWindow GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new MainWindow();
            }
            return _Instance;
        }

        public MainWindow()
        {
            InitializeComponent();

            // 設定値を初期表示する
            Loaded += (s, e) => {
                var textInput = "テストテキスト";
                var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads\";
                var spinText = 5;

                TextInputText.Text = textInput;
                FolderPathText.Text = folderPath;
                SpinText.Child.Text = spinText.ToString();
            };
        }


        protected virtual void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 閉じる処理をキャンセルして非表示にする
            e.Cancel = true;
            this.Hide();
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Apply"
                + "\n"+ "テキスト＝" + TextInputText.Text
                + "\n"+ "フォルダ＝" + FolderPathText.Text
                + "\n"+ "スピン＝" + SpinText.Child.Text
            );
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        
        private void FolderPathButton_Click(object sender, RoutedEventArgs e)
        {
            using (var fbd = new System.Windows.Forms.FolderBrowserDialog()
            {
                Description = "フォルダを選択してください",
                SelectedPath = FolderPathText.Text,
            })
            {
                // OKが押されずに終わった場合はキャンセルされている
                if (fbd.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                {
                    return;
                }

                // SelectedPathで選択されたフォルダを取得する
                //MessageBox.Show($"{fbd.SelectedPath}を選択しました");
                FolderPathText.Text = fbd.SelectedPath;
            }
        }
    }
}
