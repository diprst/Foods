using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Food
{
    public partial class MainPage : ContentPage
    {
        private MainViewModel viewModel;
        public MainPage()
        {
            InitializeComponent();
            viewModel = new MainViewModel();
            BindingContext = viewModel;

            DisplayProducts();
        }
    

    private void DisplayProducts()
    {
        ProductsStackLayout.Children.Clear();

        foreach (var product in viewModel.Products)
        {
            var productFrame = new Frame
            {
                BackgroundColor = Color.FromHex("#C9A6F5"),
                Padding = 5,
                CornerRadius = 10,
                Margin = new Thickness(5)
            };

            var productLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Padding = 10,
                Spacing = 10
            };

            var nameLabel = new Label
            {
                Text = product.Name,
                FontSize = 14,
                WidthRequest = 150,
                TextColor = Colors.Black
            };

            var priceLabel = new Label
            {
                Text = $"{product.Price} руб.",
                FontSize = 14,
                WidthRequest = 80,
                TextColor = Colors.Black
            };

            var addButton = new Button
            {
                Text = "Добавить",
                BackgroundColor = Color.FromHex("#5B0EB3"),
                TextColor = Colors.White,
                CornerRadius = 10
            };

            addButton.Clicked += (sender, e) => OnAddButtonClicked(sender, product);

            productLayout.Children.Add(nameLabel);
            productLayout.Children.Add(priceLabel);
            productLayout.Children.Add(addButton);

            productFrame.Content = productLayout;
            ProductsStackLayout.Children.Add(productFrame);
        }
    }

    private void OnAddButtonClicked(object sender, Product product)
    {
        viewModel.AddOrUpdateProduct(product);
        DisplayAlert("Сохранение", "Данные сохранены", "OK");
    }

        private async void GoToCartClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Korzina());
        }
    }

    public class MainViewModel
{
    private readonly JsonStorageService _jsonStorage;
    public ObservableCollection<Product> Products { get; set; }

    public MainViewModel()
    {
        _jsonStorage = new JsonStorageService();
        Products = _jsonStorage.LoadProducts();
    }

    public void AddOrUpdateProduct(Product product)
    {
        var existingProduct = Products.FirstOrDefault(p => p.Name == product.Name);
        if (existingProduct == null)
        {
            Products.Add(product);
        }
        _jsonStorage.SaveProducts(Products);
    }
}

public class Product
{
    public string Name { get; set; }
    public double Price { get; set; }
}

public class JsonStorageService
{
    private readonly string FilePath = Path.Combine(FileSystem.AppDataDirectory, "config.json");

    public ObservableCollection<Product> LoadProducts()
    {
        if (File.Exists(FilePath))
        {
            string json = File.ReadAllText(FilePath);
            var config = JsonConvert.DeserializeObject<Config>(json);
            return new ObservableCollection<Product>(config?.Products ?? new List<Product>());
        }
        else
        {
            var defaultProducts = new ObservableCollection<Product>
                {
                    new Product { Name = "Салат Цезарь", Price = 350.0 },
                    new Product { Name = "Паста Карбонара", Price = 400.0 },
                    new Product { Name = "Креветки", Price = 480.0 },
                    new Product { Name = "Греческий", Price = 250.0 },
                    new Product { Name = "Печеное Яблоко", Price = 350.0 },
                    new Product { Name = "Печеная Груша", Price = 480.0 },
                    new Product { Name = "Салат Изысканный", Price = 480.0 },
                    new Product { Name = "Салат с бастурмой", Price = 520.0 },
                    new Product { Name = "Салат с ростбифом", Price = 480.0 }
                };
            SaveProducts(defaultProducts);
            return defaultProducts;
            }
    }

    public void SaveProducts(ObservableCollection<Product> products)
    {
        var config = new Config { Products = products.ToList() };
        string json = JsonConvert.SerializeObject(config, Formatting.Indented);
        File.WriteAllText(FilePath, json);
    }
}

public class Config
{
    public List<Product> Products { get; set; }
    public List<string> Exclude { get; set; }
    public CompilerOptions CompilerOptions { get; set; }
}

public class CompilerOptions
{
    public bool NoImplicitAny { get; set; }
    public bool NoEmitOnError { get; set; }
    public bool RemoveComments { get; set; }
    public bool SourceMap { get; set; }
    public string Target { get; set; }
}
    }