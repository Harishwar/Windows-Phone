using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Live_Tile.Resources;

namespace Live_Tile
{
    public partial class MainPage : PhoneApplicationPage
    {

        ShellTileSchedule TileSchedule = new ShellTileSchedule();
        bool TileScheduleRunning = false;

        public MainPage()
        {
            InitializeComponent();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            TileSchedule.Interval = UpdateInterval.EveryHour;
            TileSchedule.MaxUpdateCount = 50;
            TileSchedule.Recurrence = UpdateRecurrence.Interval;
            TileSchedule.RemoteImageUri = new Uri(@"http://slisweb.sjsu.edu/sites/default/files/sjsu_logo_favicon_0.png");
            TileSchedule.Start();
            TileScheduleRunning = true;
        }

        private void Manual_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/ManualTileProperties.xaml", UriKind.Relative));
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            // Stop Service
            if (!TileScheduleRunning)
            {
                Start_Click(sender, e);
            }

            TileSchedule.Stop();
            TileScheduleRunning = false;
        }
    }
}