using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Live_Tile
{
    public partial class ManualTileProperties : PhoneApplicationPage
    {
        public ManualTileProperties()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            int newCount = 0;

            // Application Tile is always the first Tile, even if it is not pinned to Start.
            ShellTile TileToFind = ShellTile.ActiveTiles.First();

            // Application should always be found
            if (TileToFind != null)
            {
                // if Count was not entered, then assume a value of 0
                if (CountBox.Text == "")
                {
                    // A value of '0' means do not display the Count.
                    newCount = 0;
                }
                // otherwise get the numerical value for Count
                else
                {
                    newCount = int.Parse(CountBox.Text);
                }

                // set the properties to update for the Application Tile
                // Empty strings for the text values and URIs will result in the property being cleared.
                StandardTileData NewTileData = new StandardTileData
                {
                    Title = TitleBox.Text,
                    BackgroundImage = new Uri(BackgroundBox.Text, UriKind.Relative),
                    Count = newCount,
                };

                // Update the Application Tile
                TileToFind.Update(NewTileData);
            }
        }
    }
}