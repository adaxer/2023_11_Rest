﻿namespace MovieBase.XPlatform.Views;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute("Details", typeof(DetailsPage));
    }
}
