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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DataLoopDisplay.Views
{
    /// <summary>
    /// UCTableDisplay.xaml 的交互逻辑
    /// </summary>
    public partial class UCTableDisplay : UserControl
    {
        private SettingsReader settingsReader = new SettingsReader();
        private int displaySecondsPerPage = 10;
        public UCTableDisplay()
        {
            InitializeComponent();
            this.displaySecondsPerPage = this.settingsReader.GetDisplaySecondsPerPage();
        }

        private void gridTable_Loaded(object sender, RoutedEventArgs e)
        {
            this.Animate();
        }

        private void Animate()
        {
            scrollViewer.BeginAnimation(ScrollAnimationBehavior.VerticalOffsetProperty, null);
            DoubleAnimation verticalAnimation = new DoubleAnimation();

            verticalAnimation.From = scrollViewer.VerticalOffset;
            verticalAnimation.To = scrollViewer.ScrollableHeight;
            verticalAnimation.Duration = new Duration(TimeSpan.FromSeconds(this.displaySecondsPerPage));
            verticalAnimation.RepeatBehavior = RepeatBehavior.Forever;
            Storyboard storyboard = new Storyboard();

            storyboard.Children.Add(verticalAnimation);

            Storyboard.SetTarget(verticalAnimation, scrollViewer);
            Storyboard.SetTargetProperty(verticalAnimation, new PropertyPath(ScrollAnimationBehavior.VerticalOffsetProperty)); // Attached dependency property
            storyboard.Begin();
        }

        private void scrollViewer_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Animate();
        }
    }
}
