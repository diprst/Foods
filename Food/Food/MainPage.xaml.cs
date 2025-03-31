using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace Food
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel(); 
        }

        private void OnAddToCartClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var product = button?.BindingContext as Product;  

            if (product != null)
            {
                var viewModel = BindingContext as MainPageViewModel;
                viewModel?.AddToCart(product);  
                DisplayAlert("Корзина", $"{product.Name} добавлен в корзину.", "OK");
            }
        }

        private void OnCartClicked(object sender, EventArgs e)
        {
            DisplayAlert("Корзина", "Открыть корзину", "OK");
        }

        private void OnOrdersClicked(object sender, EventArgs e)
        {
          
            DisplayAlert("Заказы", "Открыть заказы", "OK");
        }
    }

    public class MainPageViewModel
    {
        public ObservableCollection<Product> Products { get; set; }
        public ObservableCollection<Product> Cart { get; set; }

        public MainPageViewModel()
        {
            var productsJson = LoadJsonData("products.json"); 
            var productList = JsonConvert.DeserializeObject<ProductList>(productsJson); 
            Products = new ObservableCollection<Product>(productList.Products); 
            Cart = new ObservableCollection<Product>();
        }

        private string LoadJsonData(string filename)
        {
            var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), filename);
            if (File.Exists(filePath))
            {
                return File.ReadAllText(filePath);
            }
            return string.Empty;
        }

        public void AddToCart(Product product)
        {
            Cart.Add(product);
        }
    }

    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class ProductList
    {
        public List<Product> Products { get; set; }
    }
}

