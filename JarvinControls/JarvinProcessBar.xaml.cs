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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace JarvinControls
{
    /// <summary>
    /// JarvinProcessBar.xaml 的交互逻辑
    /// </summary>
    public partial class JarvinProcessBar : UserControl
    {
        public JarvinProcessBar()
        {
            InitializeComponent();
        }
        
        static JarvinProcessBar()
        {
            ActionClickEvent = EventManager.RegisterRoutedEvent("ActionClick",
                RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(JarvinProcessBar));
        }

        Storyboard sb ;

        #region 属性
        public int ProcessValue
        {
            get { return (int)GetValue(ProcessValueProperty); }
            set { SetValue(ProcessValueProperty, value); }
        }

        public static readonly DependencyProperty ProcessValueProperty =
            DependencyProperty.Register("ProcessValue", typeof(int), typeof(JarvinProcessBar),
            new FrameworkPropertyMetadata(0));

        public string ActionTip
        {
            get { return (string)GetValue(ActionTipProperty); }
            set { SetValue(ActionTipProperty, value); }
        }

        public static readonly DependencyProperty ActionTipProperty =
            DependencyProperty.Register("ActionTip", typeof(string), typeof(JarvinProcessBar),
            new FrameworkPropertyMetadata("", new PropertyChangedCallback((sender, e) =>
            {
                (sender as JarvinProcessBar).IsReady = Visibility.Visible;
            })));

        public Visibility IsReady
        {
            get { return (Visibility)GetValue(IsReadyProperty); }
            set { SetValue(IsReadyProperty, value); }
        }

        public static readonly DependencyProperty IsReadyProperty =
            DependencyProperty.Register("IsReady", typeof(Visibility), typeof(JarvinProcessBar),
            new FrameworkPropertyMetadata(Visibility.Visible, new PropertyChangedCallback((sender, e) =>
            {

            })));

        public bool IsWaiting
        {
            get { return (bool)GetValue(IsWaitingProperty); }
            set { SetValue(IsWaitingProperty, value); }
        }

        public static readonly DependencyProperty IsWaitingProperty =
            DependencyProperty.Register("IsWaiting", typeof(bool), typeof(JarvinProcessBar),
            new FrameworkPropertyMetadata(false, new PropertyChangedCallback((sender, e) =>
            {
                if ((bool)e.NewValue)
                {
                    (sender as JarvinProcessBar).StoryboardPlay(e);
                }
            })));
        #endregion

        #region 事件
        public static readonly RoutedEvent ActionClickEvent;
        public event RoutedEventHandler ActionClick
        {
            add { AddHandler(ActionClickEvent, value); }
            remove { RemoveHandler(ActionClickEvent, value); }
        }
        #endregion
        private void Begin(object sender, MouseButtonEventArgs e)
        {
            ActionTip = "";
            IsReady = Visibility.Hidden;
            if (sb != null)
            {
                sb.Stop();
                sb = null;
                this.BeginAnimation(Rectangle.OpacityProperty, null);
            }
            
            RoutedEventArgs args = new RoutedEventArgs(JarvinProcessBar.ActionClickEvent, this);
            RaiseEvent(args);
        }

        protected void StoryboardPlay(DependencyPropertyChangedEventArgs e)
        {
            sb = new Storyboard();
            DoubleAnimation da = new DoubleAnimation();
            da.From = 1;
            da.To = 0.2;
            da.Duration = new Duration(TimeSpan.Parse("0:0:0.5"));
            da.AutoReverse = true;
            da.RepeatBehavior = RepeatBehavior.Forever;
            this.BeginAnimation(Rectangle.OpacityProperty, da);
        }

    }
}
