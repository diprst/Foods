namespace Food
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(Korzina), typeof(Korzina));
        }
    }
}
