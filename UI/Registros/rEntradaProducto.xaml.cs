using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
//Using agregados
using SistemaFacturacion.BLL;
using SistemaFacturacion.Entidades;

namespace SistemaFacturacion.UI.Registros
{
    public partial class rEntradaProducto : Window
    {
        private EntradaProductos entradaProductos = new EntradaProductos();
        public rEntradaProducto()
        {
            InitializeComponent();
            this.DataContext = entradaProductos;
            //—————————————————————————————————————[ ComboBox LibroId ]—————————————————————————————————————
            ProductoComboBox.ItemsSource = ProductosBLL.GetProductos();
            ProductoComboBox.SelectedValuePath = "ProductoId";
            ProductoComboBox.DisplayMemberPath = "Descripcion";
        }
        //——————————————————————————————————————————————————————————————[ Cargar ]———————————————————————————————————————————————————————————————
        private void Cargar()
        {
            this.DataContext = null;
            this.DataContext = entradaProductos;
        }
        //——————————————————————————————————————————————————————————————[ Limpiar ]——————————————————————————————————————————————————————————————
        private void Limpiar()
        {
            this.entradaProductos = new EntradaProductos();
            this.DataContext = entradaProductos;
        }
        //——————————————————————————————————————————————————————————————[ Validar ]——————————————————————————————————————————————————————————————
        private bool Validar()
        {
            bool Validado = true;
            if (EntradaProductoIdTextBox.Text.Length == 0)
            {
                Validado = false;
                MessageBox.Show("Transacción Fallida", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return Validado;
        }
        //——————————————————————————————————————————————————————————————[ Buscar ]———————————————————————————————————————————————————————————————
        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            EntradaProductos encontrado = EntradaProductosBLL.Buscar(int.Parse(EntradaProductoIdTextBox.Text));

            if (encontrado != null)
            {
                this.entradaProductos = encontrado;
                Cargar();
            }
            else
            {
                this.entradaProductos = new EntradaProductos();
                this.DataContext = this.entradaProductos;
                MessageBox.Show($"Esta Entrada de Libro no fue encontrada.\n\nAsegúrese que existe o cree una nueva.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Limpiar();
                EntradaProductoIdTextBox.SelectAll();
                EntradaProductoIdTextBox.Focus();
            }
        }
        //——————————————————————————————————————————————————————————————[ Nuevo ]———————————————————————————————————————————————————————————————
        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }
        //——————————————————————————————————————————————————————————————[ Guardar ]———————————————————————————————————————————————————————————————
        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            {
                if (!Validar())
                    return;

                //———————————————————————————————————————————————————————[ VALIDAR SI ESTA VACIO ]———————————————————————————————————————————————————————
                //—————————————————————————————————[ EntradaLibro Id ]—————————————————————————————————
                if (EntradaProductoIdTextBox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (EntradaLibro Id) está vacío.\n\nDebe asignar un Id a la Entrada del Libro.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    EntradaProductoIdTextBox.Text = "0";
                    EntradaProductoIdTextBox.Focus();
                    EntradaProductoIdTextBox.SelectAll();
                    return;
                }
                //—————————————————————————————————[ Libro Id ]—————————————————————————————————
                if (ProductoComboBox.Text == string.Empty)
                {
                    MessageBox.Show("El Campo (Libro Id) está vacío.\n\nAsigne un Id al Libro.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    ProductoComboBox.IsDropDownOpen = true;
                    return;
                }
                //—————————————————————————————————[ Fecha ]—————————————————————————————————
                if (FechaEntradaDatePicker.Text.Trim() == string.Empty)
                {
                    MessageBox.Show($"El Campo (Fecha) está vacío.\n\nSeleccione una fecha para la Salida del Libro.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    FechaEntradaDatePicker.Focus();
                    return;
                }
                //—————————————————————————————————[ Cantidad ]—————————————————————————————————
                if (CantidadTextBox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (Cantidad) está vacío.\n\nEscriba la cantidad de Libros.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    CantidadTextBox.Text = "0";
                    CantidadTextBox.Focus();
                    CantidadTextBox.SelectAll();
                    return;
                }
                //———————————————————————————————————————————————————————[ VALIDAR SI ESTA VACIO - FIN ]———————————————————————————————————————————————————————

                ProductosBLL.SumarExistenciaProducto(Convert.ToInt32(ProductoComboBox.SelectedValue), Convert.ToDouble(CantidadTextBox.Text)); //-----------------

                var paso = EntradaProductosBLL.Guardar(entradaProductos);
                if (paso)
                {
                    Limpiar();
                    MessageBox.Show("Transacción Exitosa", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    MessageBox.Show("Transacción Fallida", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //——————————————————————————————————————————————————————————————[ Eliminar ]———————————————————————————————————————————————————————————————
        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            {
                if (EntradaProductosBLL.Eliminar(int.Parse(EntradaProductoIdTextBox.Text)))
                {
                    ProductosBLL.RestarExistenciaProducto(Convert.ToInt32(ProductoComboBox.SelectedValue), Convert.ToDouble(CantidadTextBox.Text)); //-----------------
                    Limpiar();
                    MessageBox.Show("Registro Eliminado", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    MessageBox.Show("No se pudo eliminar el registro", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //—————————————————————————————————————————————————————————————[ TEXT CHANGED ]—————————————————————————————————————————————————————————————

        //——————————————————————————————————————————[ EntradaLibro Id]——————————————————————————————————————————
        private void EntradaLibroIdTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (EntradaProductoIdTextBox.Text.Trim() != string.Empty)
                {
                    int.Parse(EntradaProductoIdTextBox.Text);
                }
            }
            catch
            {
                MessageBox.Show($"El valor digitado en el campo (EntradaLibro Id) no es un número.\n\nPor favor, digite un número.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                EntradaProductoIdTextBox.Text = "0";
                EntradaProductoIdTextBox.Focus();
                EntradaProductoIdTextBox.SelectAll();
            }
        }

        //——————————————————————————————————————————[ Cantidad ]——————————————————————————————————————————
        private void CantidadTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (CantidadTextBox.Text.Trim() != string.Empty)
                {
                    int.Parse(CantidadTextBox.Text);
                }
            }
            catch
            {
                MessageBox.Show($"El valor digitado en el campo (Cantidad) no es un número.\n\nPor favor, digite un número.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                CantidadTextBox.Text = "0";
                CantidadTextBox.Focus();
                CantidadTextBox.SelectAll();
            }
        }
    }
}