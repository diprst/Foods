using System.Collections.ObjectModel;

namespace Food;

public partial class Korzina : ContentPage
{
    public ObservableCollection<Product> AvailableProducts { get; set; }

    public ObservableCollection<CartItem> CartItems { get; set; }

    public Korzina()
	{
		InitializeComponent();
        AvailableProducts = new ObservableCollection<Product>
            {
                new Product { Name = "����� ������", Price = 350.0 },
                new Product { Name = "����� ���������", Price = 400.0 },
                new Product { Name = "��������", Price = 480.0 },
                new Product { Name = "���������", Price = 250.0 },
                new Product { Name = "������� ������", Price = 350.0 },
                new Product { Name = "������� �����", Price = 480.0 },
                new Product { Name = "����� ����������", Price = 480.0 },
                new Product { Name = "����� � ���������", Price = 520.0 },
                new Product { Name = "����� � ���������", Price = 480.0 }
            };

        CartItems = new ObservableCollection<CartItem>();
        BindingContext = this;
    }

    private void AddToCart(Product product)
    {
        var existingItem = CartItems.FirstOrDefault(item => item.Name == product.Name);
        if (existingItem != null)
        {
            existingItem.Quantity++;
        }
        else
        {
            
            CartItems.Add(new CartItem { Name = product.Name, Price = product.Price, Quantity = 1 });
        }
    }


    private void DecreaseQuantity(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var item = (CartItem)button.BindingContext;
        if (item.Quantity > 1)
        {
            item.Quantity--;
        }
    }


    private void IncreaseQuantity(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var item = (CartItem)button.BindingContext;
        item.Quantity++;
    }

    private void RemoveItemClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var item = (CartItem)button.BindingContext;
        CartItems.Remove(item);
    }

    private void CheckoutClicked(object sender, EventArgs e)
    {
       
        DisplayAlert("�����", "��� ����� ��� ������� ��������!", "OK");
        CartItems.Clear();
    }
    public class CartItem : Product
    {
        public int Quantity { get; set; }
        public double Total => Quantity * Price;
    }

}