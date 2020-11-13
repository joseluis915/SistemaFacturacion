using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
//Using agregados
using SistemaFacturacion.BLL;
using SistemaFacturacion.Entidades;

namespace SistemaFacturacion.UI.Registros
{
    public partial class rProductos : Window
    {
        private Productos productos = new Productos();
        public rProductos()
        {
            InitializeComponent();
            this.DataContext = productos;

            //——————————————————————————[ VALORES DEL ComboBox Suplidor Id]——————————————————————————
            SuplidorComboBox.ItemsSource = SuplidoresBLL.GetSuplidores();
            SuplidorComboBox.SelectedValuePath = "SuplidorId";
            SuplidorComboBox.DisplayMemberPath = "NombreComercial";
        }
        //——————————————————————————————————————————————————————————————[ Cargar ]———————————————————————————————————————————————————————————————
        private void Cargar()
        {
            this.DataContext = null;
            this.DataContext = productos;
        }
        //——————————————————————————————————————————————————————————————[ Limpiar ]——————————————————————————————————————————————————————————————
        private void Limpiar()
        {
            this.productos = new Productos();
            this.DataContext = productos;
        }
        //——————————————————————————————————————————————————————————————[ Validar ]——————————————————————————————————————————————————————————————
        private bool Validar()
        {
            bool Validado = true;
            if (ProductoIdTextBox.Text.Length == 0)
            {
                Validado = false;
                MessageBox.Show("Transacción Fallida\n\nNo se pudo validar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return Validado;
        }
        //——————————————————————————————————————————————————————————————[ Buscar ]———————————————————————————————————————————————————————————————
        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Productos encontrado = ProductosBLL.Buscar(int.Parse((ProductoIdTextBox.Text)));

            if (encontrado != null)
            {
                this.productos = encontrado;
                Cargar();
            }
            else
            {
                this.productos = new Productos();
                this.DataContext = this.productos;

                MessageBox.Show($"Este Contacto no fue encontrado.\n\nAsegúrese que existe o cree uno nuevo.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);

                Limpiar();
                ProductoIdTextBox.SelectAll();
                ProductoIdTextBox.Focus();
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
                //—————————————————————————————————[ ProductoId ]—————————————————————————————————
                if (ProductoIdTextBox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (Contacto Id) está vacío.\n\nPorfavor, Asigne un Id al Contacto.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    ProductoIdTextBox.Text = "0";
                    ProductoIdTextBox.Focus();
                    ProductoIdTextBox.SelectAll();
                    return;
                }
                //—————————————————————————————————[ NombreCompleto ]—————————————————————————————————
                if (DescripcionTextBox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (Descripcion) está vacío.\n\nPorfavor, Asigne una Descripcion al Producto.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    DescripcionTextBox.Clear();
                    DescripcionTextBox.Focus();
                    return;
                }
                //—————————————————————————————————[ Suplidor ]—————————————————————————————————
                if (SuplidorComboBox.Text == string.Empty)
                {
                    MessageBox.Show("El Campo (Suplidor) está vacío.\n\nPorfavor, Asigne un Suplidor al Producto.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    SuplidorComboBox.IsDropDownOpen = true;
                    SuplidorComboBox.Focus();
                    return;
                }
                //—————————————————————————————————[ Precio ]—————————————————————————————————
                if (PrecioTextBox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (Precio) está vacío.\n\nPorfavor, Asigne un Precio al Producto.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    PrecioTextBox.Clear();
                    PrecioTextBox.Focus();
                    return;
                }
                //———————————————————————————————————————————————————————[ VALIDAR SI ESTA VACIO - FIN ]———————————————————————————————————————————————————————
                                
                var paso = ProductosBLL.Guardar(productos);
                if (paso)
                {
                    Limpiar();
                    MessageBox.Show("El Registro se pudo guardar satisfactoriamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    MessageBox.Show("El Registro no se pudo guardar :(", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //——————————————————————————————————————————————————————————————[ Eliminar ]———————————————————————————————————————————————————————————————
        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            {
                if (ProductosBLL.Eliminar(int.Parse(ProductoIdTextBox.Text)))
                {
                    Limpiar();
                    MessageBox.Show("Registro Eliminado", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    MessageBox.Show("No se pudo eliminar el registro", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //—————————————————————————————————————————————————————————————[ TEXT CHANGED ]—————————————————————————————————————————————————————————————

        //—————————————————————————————————[ Empleado Id ]—————————————————————————————————
        private void ProductoIdTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (ProductoIdTextBox.Text.Trim() != string.Empty)
                {
                    int.Parse(ProductoIdTextBox.Text);
                }
            }
            catch
            {
                MessageBox.Show($"El valor digitado en el campo (Empleado Id) debe ser un número.\n\nPor favor, digite un número valido.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                ProductoIdTextBox.Text = "0";
                ProductoIdTextBox.Focus();
                ProductoIdTextBox.SelectAll();
            }
        }

        //—————————————————————————————————[ Precio ]—————————————————————————————————
        private void PrecioTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (PrecioTextBox.Text.Trim() != string.Empty)
                {
                    double.Parse(PrecioTextBox.Text);
                }
            }
            catch
            {
                MessageBox.Show($"El valor digitado en el campo (Precio) debe ser un número.\n\nPor favor, digite un número valido.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                PrecioTextBox.Text = "0";
                PrecioTextBox.Focus();
                PrecioTextBox.SelectAll();
            }
        }
    }
}