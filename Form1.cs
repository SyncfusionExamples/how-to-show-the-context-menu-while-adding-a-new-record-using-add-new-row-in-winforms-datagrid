using Syncfusion.Data;
using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.DataGrid.Renderers;
using Syncfusion.WinForms.DataGrid.Styles;
using Syncfusion.WinForms.GridCommon.ScrollAxis;
using Syncfusion.WinForms.ListView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Dynamic;
using System.IO;
using System.Windows.Forms;
using Syncfusion.WinForms.DataGrid.Interactivity;
using System.Linq;
using Syncfusion.WinForms.DataGrid.Events;

namespace SfDataGridDemo
{

    /// <summary>
    /// Summary description for Form1.
    /// </summary>   

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            sfDataGrid.AutoGenerateColumns = false;
            sfDataGrid.DataSource = new ViewModel().Orders;
            sfDataGrid.LiveDataUpdateMode = LiveDataUpdateMode.AllowDataShaping;
            sfDataGrid.AllowEditing = true;
            sfDataGrid.AutoSizeColumnsMode = AutoSizeColumnsMode.AllCells;
            sfDataGrid.AddNewRowPosition = RowPosition.Top;           
            GridNumericColumn gridTextColumn1 = new GridNumericColumn() { MappingName = "OrderID", HeaderText = "Order ID" };
            GridTextColumn gridTextColumn2 = new GridTextColumn() { MappingName = "CustomerID", HeaderText = "Customer ID", AllowEditing = true };
            GridTextColumn gridTextColumn3 = new GridTextColumn() { MappingName = "CustomerName", HeaderText = "Customer Name" };
            GridTextColumn gridTextColumn4 = new GridTextColumn() { MappingName = "Country", HeaderText = "Country" };
            GridTextColumn gridTextColumn5 = new GridTextColumn() { MappingName = "ShipCity", HeaderText = "Ship City" };
            GridCheckBoxColumn checkBoxColumn = new GridCheckBoxColumn() { MappingName = "IsShipped", HeaderText = "Is Shipped" };
            // enable the context menu for new records
            sfDataGrid.RecordContextMenu = new ContextMenuStrip();
            sfDataGrid.RecordContextMenu.Items.Add("Copy", null, OnBuscarMenuClicked);

            sfDataGrid.Columns.Add(gridTextColumn1);
            sfDataGrid.Columns.Add(gridTextColumn2);
            sfDataGrid.Columns.Add(gridTextColumn3);
            sfDataGrid.Columns.Add(gridTextColumn4);
            sfDataGrid.Columns.Add(gridTextColumn5);
            sfDataGrid.Columns.Add(checkBoxColumn);
            sfDataGrid.TableControl.MouseUp += SfDataGrid_MouseUp;

        }

        private void OnBuscarMenuClicked(object sender, EventArgs e)
        {
            MessageBox.Show("Clicked the Copy Context Menu");
        }

        //Get the record context while AddingNewRow menu by customization 
        private void SfDataGrid_MouseUp(object sender, MouseEventArgs e)
        {
            // get the row and column index based on the pointer position 
            var rowColIndex = sfDataGrid.TableControl.PointToCellRowColumnIndex(e.Location);

            //Check the condition is AddNewRow and Only show the context menu while pressing right button of Mouse
            if (e.Button == MouseButtons.Right && sfDataGrid.IsAddNewRowIndex(rowColIndex.RowIndex) && this.sfDataGrid.RecordContextMenu != null && !rowColIndex.IsEmpty)
            {
                ContextMenuStrip contextMenu = null;

                //set the Record contextMenu for AddNewRow
                contextMenu = this.sfDataGrid.RecordContextMenu;
                //get the location 
                var location = this.sfDataGrid.TableControl.PointToScreen(e.Location);
                if (contextMenu != null)
                {
                    //show the ContextMenu for AddNewRow
                    contextMenu.Show(location);
                }
            }
        }
    }


    public class OrderInfo : INotifyPropertyChanged
    {
        decimal? orderID;
        string customerId;
        string country;
        string customerName;
        string shippingCity;
        bool isShipped;

        public OrderInfo()
        {

        }

        public decimal? OrderID
        {
            get { return orderID; }
            set { orderID = value; this.OnPropertyChanged("OrderID"); }
        }

        public string CustomerID
        {
            get { return customerId; }
            set { customerId = value; this.OnPropertyChanged("CustomerID"); }
        }

        public string CustomerName
        {
            get { return customerName; }
            set { customerName = value; this.OnPropertyChanged("CustomerName"); }
        }

        public string Country
        {
            get { return country; }
            set { country = value; this.OnPropertyChanged("Country"); }
        }

        public string ShipCity
        {
            get { return shippingCity; }
            set { shippingCity = value; this.OnPropertyChanged("ShipCity"); }
        }

        public bool IsShipped
        {
            get { return isShipped; }
            set { isShipped = value; this.OnPropertyChanged("IsShipped"); }
        }


        public OrderInfo(decimal? orderId, string customerName, string country, string customerId, string shipCity, bool isShipped)
        {
            this.OrderID = orderId;
            this.CustomerName = customerName;
            this.Country = country;
            this.CustomerID = customerId;
            this.ShipCity = shipCity;
            this.IsShipped = isShipped;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class ViewModel
    {
        private ObservableCollection<OrderInfo> orders;
        public ObservableCollection<OrderInfo> Orders
        {
            get { return orders; }
            set { orders = value; }
        }

        public ViewModel()
        {
            orders = new ObservableCollection<OrderInfo>();
            orders.Add(new OrderInfo(1001, "Thomas Hardy", "Germany", "ALFKI", "Berlin", true));
            orders.Add(new OrderInfo(1002, "Laurence Lebihan", "Mexico", "ANATR", "Mexico", false));
            orders.Add(new OrderInfo(1003, "Antonio Moreno", "Mexico", "ANTON", "Mexico", true));
            orders.Add(new OrderInfo(1004, "Thomas Hardy", "UK", "AROUT", "London", true));
            orders.Add(new OrderInfo(1005, "Christina Berglund", "Sweden", "BERGS", "Lula", false));
        }
    }
}
