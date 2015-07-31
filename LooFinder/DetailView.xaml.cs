using LooFinder.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace LooFinder
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DetailView : Page
    {
        private Toilet SelectedLoo {get; set;}

        public DetailView()
        {
            this.InitializeComponent();
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            SystemNavigationManager.GetForCurrentView().BackRequested += (s, e) =>
            {
                if (Frame.CanGoBack)
                {
                    Frame.GoBack();
                }
            };
         
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ParseHelper parseHelper = ParseHelper.Instance;
            
            var looQuery = from Toilet in parseHelper.parseToilets
                           where Toilet.ObjectId == (String)e.Parameter
                           select Toilet;

            SelectedLoo = looQuery.First();
            
            ToiletName.Text = SelectedLoo.ToiletName;
        } 

    }
}
