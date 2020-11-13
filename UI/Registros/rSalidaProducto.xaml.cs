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
    public partial class rSalidaProducto : Window
    {
        private SalidaProductos salidaProductos = new SalidaProductos();
        public rSalidaProducto()
        {
            InitializeComponent();
            this.DataContext = salidaProductos;
            //—————————————————————————————————————[ ComboBox LibroId ]—————————————————————————————————————
            ProductoComboBox.ItemsSource = ProductosBLL.GetProductos();
            ProductoComboBox.SelectedValuePath = "ProductoId";
            ProductoComboBox.DisplayMemberPath = "Descripcion";
        }
        //——————————————————————————————————————————————————————————————[ Cargar ]———————————————————————————————————————————————————————————————
        private void Cargar()
        {
            this.DataContext = null;
            this.DataContext = salidaProductos;
        }
        //——————————————————————————————————————————————————————————————[ Limpiar ]——————————————————————————————————————————————————————————————
        private void Limpiar()
        {
            this.salidaProductos = new SalidaProductos();
            this.DataContext = salidaProductos;
        }
        //——————————————————————————————————————————————————————————————[ Validar ]——————————————————————————————————————————————————————————————
        private bool Validar()
        {
            bool Validado = true;
            if (SalidaProductoIdTextBox.Text.Length == 0)
            {
                Validado = false;
                MessageBox.Show("Transacción Fallida", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return Validado;
        }
        //——————————————————————————————————————————————————————————————[ Buscar ]———————————————————————————————————————————————————————————————
        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            SalidaProductos encontrado = SalidaProductosBLL.Buscar(int.Parse(SalidaProductoIdTextBox.Text));

            if (encontrado != null)
            {
                this.salidaProductos = encontrado;
                Cargar();
            }
            else
            {
                this.salidaProductos = new SalidaProductos();
                this.DataContext = this.salidaProductos;
                MessageBox.Show($"Esta Entrada de Libro no fue encontrada.\n\nAsegúrese que existe o cree una nueva.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Limpiar();
                SalidaProductoIdTextBox.SelectAll();
                SalidaProductoIdTextBox.Focus();
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
                if (SalidaProductoIdTextBox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (EntradaLibro Id) está vacío.\n\nDebe asignar un Id a la Entrada del Libro.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    SalidaProductoIdTextBox.Text = "0";
                    SalidaProductoIdTextBox.Focus();
                    SalidaProductoIdTextBox.SelectAll();
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
                if (FechaSalidaDatePicker.Text.Trim() == string.Empty)
                {
                    MessageBox.Show($"El Campo (Fecha) está vacío.\n\nSeleccione una fecha para la Salida del Libro.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    FechaSalidaDatePicker.Focus();
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

                var paso = SalidaProductosBLL.Guardar(salidaProductos);
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
                if (SalidaProductosBLL.Eliminar(int.Parse(SalidaProductoIdTextBox.Text)))
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
        private void SalidaLibroIdTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (SalidaProductoIdTextBox.Text.Trim() != string.Empty)
                {
                    int.Parse(SalidaProductoIdTextBox.Text);
                }
            }
            catch
            {
                MessageBox.Show($"El valor digitado en el campo (EntradaLibro Id) no es un número.\n\nPor favor, digite un número.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                SalidaProductoIdTextBox.Text = "0";
                SalidaProductoIdTextBox.Focus();
                SalidaProductoIdTextBox.SelectAll();
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