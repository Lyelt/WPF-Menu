using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Data;

namespace WPFmenu
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public Menu menu { get; set; }

        public MainWindow()
        {
            menu = new Menu();
            InitializeComponent();
        }

        private void menuItem_MouseDoubleClick(object sender, EventArgs e)
        {

        }

        private void addMenuBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void uploadMenuBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.DefaultExt = ".xml";
            dlg.Filter = "XML Files (*.xml)|*.xml|Text Files (*.txt)|*.txt|All Files (*.*)|*.*";

            if (dlg.ShowDialog() == true)
            {
                menu = menu.Fill(dlg.FileName);              
            }
            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Ethnicity");
            foreach(var item in menu.MenuItems)
            {
                dt.Rows.Add(item.ItemName, item.ItemEthnicity);
            }

            DataView dv = new DataView(dt);
            menuItemGrid.ItemsSource = dv;
        }

        private void saveMenuBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void menuItemGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "Ethnicity")
            {
                var cb = new DataGridComboBoxColumn();
               // cb.ItemsSource = ( as )
            }
        }
    }
}
