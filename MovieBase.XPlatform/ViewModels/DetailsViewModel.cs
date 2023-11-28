
using MovieBase.Common.Interfaces;

namespace MovieBase.XPlatform.ViewModels;
public partial class DetailsViewModel : BaseViewModel
{
    public DetailsViewModel(INavigationService navigation)
    {
        Title = "Details";
    }

    public override Task OnNavigatedTo(Dictionary<string, object> parameters)
    {
        if (parameters.TryGetValue("Title", out var value))
        {
            Title =$"{Title} {value}";
        }
        return base.OnNavigatedTo(parameters);
    }
}
