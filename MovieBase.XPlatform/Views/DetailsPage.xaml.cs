
using MovieBase.Common.Interfaces;

namespace MovieBase.XPlatform.Views;

public partial class DetailsPage
{
    public DetailsPage(DetailsViewModel detailsViewModel)
    {
        InitializeComponent();
        BindingContext = detailsViewModel;
    }
}

