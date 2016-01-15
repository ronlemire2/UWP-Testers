using AdaptiveTester.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace AdaptiveTester.Views {
    /// <summary>
    /// Acknowledgement: Jeff Prosise
    /// This sample was taken from http://www.wintellect.com/devcenter/jprosise/using-adaptivetrigger-to-build-adaptive-uis-in-windows-10
    /// </summary>
    public sealed partial class ProsiseSamplePage : Page {
        public ProsiseSamplePage() {
            this.InitializeComponent();


            var recipe = new Recipe();

            recipe.UniqueId = "2001";
            recipe.Title = "French Macaroons";
            recipe.Subtitle = "French Macaroons";
            recipe.Description = "Pellentesque porta, mauris quis interdum vehicula, urna sapien ultrices velit, nec venenatis dui odio in augue. Cras posuere, enim a cursus convallis, neque turpis malesuada erat, ut adipiscing neque tortor ac erat.";
            recipe.Directions = "Pulse confectioners' sugar and almond flour in a food processor until combined. Sift mixture 2 times. Preheat oven to 375 degrees. Whisk whites with a mixer on medium speed until foamy. Add cream of tartar, and whisk until soft peaks form. Reduce speed to low, then add superfine sugar. Increase speed to high, and whisk until stiff peaks form, about 8 minutes. Sift flour mixture over whites, and fold until mixture is smooth and shiny.\r\n\r\nTransfer batter to a pastry bag fitted with a 1/2-inch plain round tip, and pipe 3/4-inch rounds 1 inch apart on parchment-lined baking sheets, dragging pastry tip to the side of rounds rather than forming peaks. Tap bottom of each sheet on work surface to release trapped air. Let stand at room temperature for 15 minutes. Reduce oven temperature to 325 degrees. Bake 1 sheet at a time, rotating halfway through, until macaroons are crisp and firm, about 10 minutes. After each batch, increase oven temperature to 375 degrees, heat for 5 minutes, then reduce to 325 degre.\r\n\r\nLet macaroons cool on sheets for 2 to 3 minutes, then transfer to a wire rack. Sandwich 2 same-size macaroons with 1 teaspoon filling. Serve immediately, or stack between layers of parchment, wrap in plastic, and freeze for up to 3 months.";
            recipe.ImagePath = "/Images/French_2_600_C.jpg";
            recipe.PrepTime = 80;

            recipe.Ingredients = new ObservableCollection<string>()
            {
                "Macaroons:",
                "1 cup confectioners' sugar",
                "3/4 cup almond flour",
                "2 large egg whites, room temperature",
                "Pinch of cream of tartar","1/4 cup superfine sugar",
                "Filling:",
                "Chocolate: Chocolate Ganache",
                "Coconut: 1 cup Swiss Meringue Buttercream mixed with 1/3 cup angel-flake coconut",
                "Peanut: Chocolate Ganache or store-bought dulce de leche jam or peanut butter",
                "Pistachio:",
                "1 cup Swiss Meringue Buttercream mixed with 1/3 cup finely chopped pistachios",
                "Raspberry: 3/4 cup seedless raspberry jam",
                "Swiss Meringue Buttercream"
            };

            this.DataContext = recipe;
        }
    }
}
