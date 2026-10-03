using Lab3_Shop_Rukoleev.Models;
using Lab3_Shop_Rukoleev.ViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;

namespace Lab3_Shop_Rukoleev
{
    public partial class MainWindow : Window
    {
        private readonly ObservableCollection<ProductViewModel> _products;

        private readonly CollectionViewSource _source;

        public ICollectionView Products { get; set; }


        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;

            _products = new ObservableCollection<ProductViewModel>();

            _source = new CollectionViewSource
            {
                Source = _products
            };

            Products = _source.View;

            _source.View.Filter = FilterProducts;

            LoadProducts();
        }


        private void LoadProducts()
        {
            try
            {
                using var db = new ShopDbContext();

                var products =
                    from p in db.Products
                    join name in db.ProductNames
                        on p.NameId equals name.Id
                    join category in db.Categories
                        on p.CategoryId equals category.Id
                    join manufacturer in db.Manufacturers
                        on p.ManufacturerId equals manufacturer.Id
                    join supplier in db.Suppliers
                        on p.SupplierId equals supplier.Id

                    select new ProductViewModel
                    {
                        Id = p.Id,

                        Article = p.Article ?? "",

                        ProductName = name.Name ?? "",

                        Category = category.Name ?? "",

                        Description = p.Description ?? "",

                        Manufacturer = manufacturer.Name ?? "",

                        Supplier = supplier.Name ?? "",

                        Price = p.Price ?? 0,

                        Count = p.Count ?? 0,

                        Discount = p.Discount ?? 0,

                        Unit = p.Unit ?? "",

                        ImagePath = p.ImagePath ?? ""
                    };

                foreach (var product in products.ToList())
                {
                    _products.Add(product);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Ошибка загрузки товаров:\n\n{ex.Message}",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }


        private bool FilterProducts(object obj)
        {
            if (obj is not ProductViewModel product)
                return false;

            string searchText = SearchTextBox?.Text?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(searchText))
                return true;

            // Разбиваем запрос на отдельные слова.
            // Например:
            // "Knauf штукатурка"
            // превращается в:
            // "Knauf" + "штукатурка"
            string[] searchWords = searchText
                .ToLower()
                .Split(
                    new[] { ' ', '\t', '\n', '\r' },
                    StringSplitOptions.RemoveEmptyEntries);


            // Все текстовые данные товара
            string searchableText = string.Join(" ",
                product.Article,
                product.ProductName,
                product.Category,
                product.Description,
                product.Manufacturer,
                product.Supplier,
                product.Unit
            ).ToLower();


            // Каждое слово должно встретиться
            // хотя бы в одном из доступных текстовых полей
            return searchWords.All(word =>
                searchableText.Contains(word));
        }


        private void SearchTextBox_TextChanged(
    object sender,
    System.Windows.Controls.TextChangedEventArgs e)
        {
            SearchHint.Visibility =
                string.IsNullOrEmpty(SearchTextBox.Text)
                    ? Visibility.Visible
                    : Visibility.Collapsed;

            Products?.Refresh();
        }
    }
}