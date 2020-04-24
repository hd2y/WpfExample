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

namespace WpfExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // 反射获取所有用户控件
            Type[] types = typeof(MainWindow).Assembly.GetTypes();
            List<Type> userControlTypes = new List<Type>();
            foreach (var type in types)
            {
                if (typeof(UserControl).IsAssignableFrom(type))
                {
                    userControlTypes.Add(type);
                }
            }

            // 添加 TabControl 存放用户控件
            if (this.Content is Grid grid)
            {
                TabControl tabControl = new TabControl();
                foreach (var catgory in userControlTypes.Select(t => t.FullName.Split('.')[2]).Distinct())
                {
                    var tabItem = new TabItem { Header = catgory };
                    WrapPanel wrapPanel = new WrapPanel();

                    foreach (var uc in userControlTypes.Where(t => t.FullName.Contains(catgory)).Select(t => Activator.CreateInstance(t)).Cast<UserControl>())
                    {
                        Button button = new Button { Content = uc.GetType().FullName.Split('.')[3] };
                        button.Click += (buttonSender, buttonE) =>
                        {
                            Window window = new Window();
                            window.Content = uc;
                            window.ShowDialog();
                        };
                        wrapPanel.Children.Add(button);
                    }

                    tabItem.Content = wrapPanel;
                    tabControl.Items.Add(tabItem);
                }
                grid.Children.Add(tabControl);
            }
        }
    }
}
