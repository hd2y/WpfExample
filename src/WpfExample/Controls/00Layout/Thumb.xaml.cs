using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfExample.Controls._00Layout
{
    /// <summary>
    /// Thumb.xaml 的交互逻辑
    /// </summary>
    public partial class Thumb : UserControl
    {
        public Thumb()
        {
            InitializeComponent();
        }

        private void Thumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            System.Windows.Controls.Primitives.Thumb thumb = (System.Windows.Controls.Primitives.Thumb)sender;
            double top = System.Windows.Controls.Canvas.GetTop(thumb) + e.VerticalChange;
            double left = System.Windows.Controls.Canvas.GetLeft(thumb) + e.HorizontalChange;
            
            //防止Thumb控件被拖出容器。
            if (top <= 0)
                top = 0;
            if (left <= 0)
                left = 0;
            if (this.Parent is Window window)
            {
                if (top >= (window.Height - SystemParameters.CaptionHeight - thumb.Height))
                    top = window.ActualHeight - SystemParameters.CaptionHeight - thumb.Height;
                if (left >= (window.ActualWidth - thumb.Width))
                    left = window.ActualWidth - thumb.Width;
            }
            
            System.Windows.Controls.Canvas.SetTop(thumb, top);
            System.Windows.Controls.Canvas.SetLeft(thumb, left);
            tt.Text = $"Top: {top}\nLeft: {left}";
        }
    }
}
