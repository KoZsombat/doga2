using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace doga2;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public int allQuantity = 0;

    public MainWindow()
    {
        InitializeComponent();
    }

    private void add_Click(object sender, RoutedEventArgs e)
    {
        if (name.Text != "" || price.Text != "" || quantity.Text != "")
        {
            if (double.TryParse(price.Text, out var priceOfItem) && int.TryParse(quantity.Text, out var quantityOfItem) && priceOfItem > 0 && quantityOfItem > 0)
            { 
                list.Items.Add($"{name?.Text} - {priceOfItem} Ft - {quantityOfItem} db");
                allQuantity += quantityOfItem;
                stats.Content = $"Összes termék darabszáma: {allQuantity} db";
                name.Text = "";
                price.Text = "";
                quantity.Text = "";
            } else
            {
                if (!double.TryParse(price.Text, out var priceOfItem2) || priceOfItem2 < 0)
                {
                    MessageBox.Show("Hibás ár!");
                } else
                    {
                    MessageBox.Show("Hibás darabszám!");
                }
            }
        } else
        {
            MessageBox.Show("Tölts ki minden mezőt!");
        }
    }

    private void delete_Click(object sender, RoutedEventArgs e)
    {
        if (list.SelectedItem == null)
        {
            MessageBox.Show("Nincs kijelölve elem!");
            return;
        }
        var selectedItem = list.SelectedItem.ToString();
        var parts = selectedItem?.Split('-');
        if (selectedItem != null && parts != null)
        {
            if (parts.Length >= 3 && int.TryParse(parts[2].Split(' ')[1], out var quantityOfItem))
            {
                allQuantity -= quantityOfItem;
            }

            list.Items.Remove(selectedItem);
            stats.Content = $"Összes termék darabszáma: {allQuantity} db";
        }
        else
        {
            MessageBox.Show("Nincs kijelölve elem!");
        }
    }

    private void save_Click(object sender, RoutedEventArgs e)
    {
        if (list.Items.Count == 0)
        {
            MessageBox.Show("Nincs semmi a listába!");
            return;
        }

        if (File.Exists("raktar.txt"))
        {
            File.Delete("raktar.txt");
        }

        File.AppendAllLines("raktar.txt", list.Items.Cast<string>());

        MessageBox.Show("Sikeres mentés!");
    }

    private void load_Click(object sender, RoutedEventArgs e)
    {
        if (!File.Exists("raktar.txt"))
        {
            MessageBox.Show("Nincs mentett lista.");
            return;
        }

        list.Items.Clear();
        File.ReadAllLines("raktar.txt").ToList().ForEach(x => list.Items.Add(x));
    }
}