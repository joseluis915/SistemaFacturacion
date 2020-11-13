using System;
using System.Windows;
//Using agregados
using System.Collections.Generic;
using SistemaFacturacion.BLL;
using SistemaFacturacion.Entidades;

namespace SistemaFacturacion.UI.Consultas
{
    public partial class cProductos : Window
    {
        public cProductos()
        {
            InitializeComponent();
        }
        private void ConsultarButton_Click(object sender, RoutedEventArgs e)
        {
            var listado = new List<Productos>();

            if (CriterioTextBox.Text.Trim().Length > 0)
            {
                switch (FiltroComboBox.SelectedIndex)
                {
                    case 0:
                        try
                        {
                            listado = ProductosBLL.GetList(l => l.ProductoId == int.Parse(CriterioTextBox.Text));
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Debes ingresar un Critero valido para aplicar este filtro.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        break;
                }
            }
            else
            {
                listado = ProductosBLL.GetList(c => true);
            }

            if (DesdeDatePicker.SelectedDate != null)
                listado = ProductosBLL.GetList(c => c.FechaCreacion.Date >= DesdeDatePicker.SelectedDate);

            if (HastaDatePicker.SelectedDate != null)
                listado = ProductosBLL.GetList(c => c.FechaCreacion.Date <= HastaDatePicker.SelectedDate);

            DatosDataGrid.ItemsSource = null;
            DatosDataGrid.ItemsSource = listado;
        }
    }
}
