using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Ch12_PagesAndNavigation.Views {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class rjlGroupingAndCollectionViewSourcePage : Page {
        public rjlGroupingAndCollectionViewSourcePage() {
            this.InitializeComponent();
        }
    }
}

// ************************************************************************************
// Grouping and CollectionViewSource in code behind. 
// ************************************************************************************
//private void MainPage_Loaded(object sender, RoutedEventArgs e) {
//    MainPageViewModel vm = new MainPageViewModel();
//    var groups = vm.Activities
//        .OrderBy(i => i.Name)
//        .GroupBy(i => i.Project)
//        .Select(g => new { GroupName = string.Format("Group Title: {0}", g.Key), Items = g });

//    cvsActivities.Source = groups;
//    cvsActivities.IsSourceGrouped = true;
//    cvsActivities.ItemsPath = new PropertyPath("Items");
//    lvActivities.ItemsSource = cvsActivities.View;
//}

//<Page.Resources>
//    <CollectionViewSource x:Name="cvsActivities" IsSourceGrouped="True" />
//    <local:ListGroupStyleSelector x:Key="listGroupStyleSelector"/>
//</Page.Resources>

//<Grid Background = "{ThemeResource ApplicationPageBackgroundThemeBrush}" >
//    < Grid.ColumnDefinitions >
//        < ColumnDefinition Width="600"/>
//        <ColumnDefinition/>
//    </Grid.ColumnDefinitions>

//    <ListView x:Name="lvActivities"
//        ItemTemplate="{StaticResource listViewItemTemplate}"
//        GroupStyleSelector="{StaticResource listGroupStyleSelector}"
//        Margin="120" Width="320"/>
//</Grid>
