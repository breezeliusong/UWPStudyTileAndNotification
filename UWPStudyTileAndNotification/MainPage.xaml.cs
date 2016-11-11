using NotificationsExtensions;
using NotificationsExtensions.Tiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using Windows.UI.StartScreen;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWPStudyTileAndNotification
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            // In a real app, these would be initialized with actual data
            string from = "Jennifer Parker";
            string subject = "Photos from our trip";
            string body = "Check out these awesome photos I took while in New Zealand!";

            //scenario1
            //// Construct the tile content
            //TileContent content = new TileContent()
            //{
            //    Visual = new TileVisual()
            //    {
            //        TileMedium = new TileBinding()
            //        {
            //            Content = new TileBindingContentAdaptive()
            //            {
            //                Children =
            //    {
            //        new AdaptiveText()
            //        {
            //            Text = from
            //        },

            //        new AdaptiveText()
            //        {
            //            Text = subject,
            //            HintStyle= AdaptiveTextStyle.CaptionSubtle
            //        },

            //        new AdaptiveText()
            //        {
            //            Text = body,
            //            HintStyle= AdaptiveTextStyle.CaptionSubtle
            //        }
            //    }
            //            }
            //        },

            //        TileWide = new TileBinding()
            //        {
            //            Content = new TileBindingContentAdaptive()
            //            {
            //                Children =
            //    {
            //        new AdaptiveText()
            //        {
            //            Text = from,
            //            HintStyle= AdaptiveTextStyle.Subtitle
            //        },

            //        new AdaptiveText()
            //        {
            //            Text = subject,
            //            HintStyle= AdaptiveTextStyle.CaptionSubtle
            //        },

            //        new AdaptiveText()
            //        {
            //            Text = body,
            //            HintStyle= AdaptiveTextStyle.CaptionSubtle
            //        }
            //    }
            //            }
            //        }
            //    }
            //};
            //// Create the tile notification
            //notification = new TileNotification(content.GetXml());
            //notification.ExpirationTime = DateTimeOffset.UtcNow.AddMinutes(10);


            //scenario2
            // TODO - all values need to be XML escaped
            // Construct the tile content as a string
            {
                string content =
                    @"<tile>
                            <visual>
                                <binding template=""TileMedium"">
                                <text>{from}</text>
                                <text hint-style=""captionSubtle"">{subject}
                                </text>
<text hint-style=""captionSubtle"">{body}
</text>
</binding>

<binding template=""TileWide"">
<text hint-style=""subtitle"">{from}</text>
<text hint-style=""captionSubtle"">{subject}</text>
<text hint-style=""captionSubtle"">{body}</text>
</binding>
</visual>
</tile>";
                // Load the string into an XmlDocument
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(content);

                // Then create the tile notification
                notification = new TileNotification(doc);
            }
        }

        TileNotification notification;
        private void SendNotification(object sender, RoutedEventArgs e)
        {
            // And send the notification
            TileUpdateManager.CreateTileUpdaterForApplication().Update(notification);
            // If the secondary tile is pinned
            if (SecondaryTile.Exists("MySecondaryTile"))
            {
                // Get its updater
                var updater = TileUpdateManager.CreateTileUpdaterForSecondaryTile("MySecondaryTile");

                // And send the notification
                updater.Update(notification);
            }
        }


        private void Clear(object sender, RoutedEventArgs e)
        {

            TileUpdateManager.CreateTileUpdaterForApplication().Clear();
        }
    }


}
