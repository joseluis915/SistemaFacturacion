using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SistemaFacturacion.BLL;
using SistemaFacturacion.Entidades;

namespace SistemaFacturacion.UI.Registros
{
    public partial class rFacturacion : Window
    {
        private Facturacion facturacion = new Facturacion();
        public rFacturacion()
        {
            InitializeComponent();
            this.DataContext = facturacion;

            //—————————————————————————————————————[ VALORES DEL ComboBox ]—————————————————————————————————————
            ProductoIdComboBox.ItemsSource = ProductosBLL.GetProductos();
            ProductoIdComboBox.SelectedValuePath = "ProductoId";
            ProductoIdComboBox.DisplayMemberPath = "Descripcion";
            ProductoIdComboBox.DisplayMemberPath = "Precio";

            ClienteIdComboBox.ItemsSource = ClientesBLL.GetClientes();
            ClienteIdComboBox.SelectedValuePath = "ClienteId";
            ClienteIdComboBox.DisplayMemberPath = "NombreCompleto";

            EmpleadoIdComboBox.ItemsSource = EmpleadosBLL.GetEmpleados();
            EmpleadoIdComboBox.SelectedValuePath = "EmpleadoId";
            EmpleadoIdComboBox.DisplayMemberPath = "NombreCompleto";
        }
        //——————————————————————————————————————————————————————————————[ Cargar ]———————————————————————————————————————————————————————————————
        private void Cargar()
        {
            this.DataContext = null;
            this.DataContext = facturacion;
        }
        //——————————————————————————————————————————————————————————————[ Limpiar ]——————————————————————————————————————————————————————————————
        private void Limpiar()
        {
            this.facturacion = new Facturacion();
            this.DataContext = facturacion;
        }
        //——————————————————————————————————————————————————————————————[ Validar ]——————————————————————————————————————————————————————————————
        private bool Validar()
        {
            bool Validado = true;
            if (FacturaIdTextbox.Text.Length == 0)
            {
                Validado = false;
                MessageBox.Show("Transaccion Fallida", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return Validado;
        }
        //——————————————————————————————————————————————————————————————[ Buscar ]———————————————————————————————————————————————————————————————
        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Facturacion encontrado = FacturacionBLL.Buscar(facturacion.FacturacionId);

            if (encontrado != null)
            {
                facturacion = encontrado;
                Cargar();
            }
            else
            {
                MessageBox.Show($"Este Proyecto no fue encontrado.\n\nAsegurese que existe o cree uno nuevo.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                Limpiar();
                //—————————————————————————————————————[ Limpiar y hacer focus en el Id]—————————————————————————————————————
                FacturaIdTextbox.Text = "";
                FacturaIdTextbox.Focus();
            }
        }
        //——————————————————————————————————————————————————————————————[ Agregar Fila ]———————————————————————————————————————————————————————————————
        private void AgregarFilaButton_Click(object sender, RoutedEventArgs e)
        {
            var filaDetalle = new FacturacionDetalle
            {
                FacturacionId = this.facturacion.FacturacionId,
                ProductoId = Convert.ToInt32(ProductoIdComboBox.SelectedValue.ToString()),
                productos = ((Productos)ProductoIdComboBox.SelectedItem),
                Cantidad = Convert.ToDouble(CantidadTextBox.Text),
            };
            //——————————————————————————————[ Total ]——————————————————————————————
            facturacion.Total += Convert.ToDouble(CantidadTextBox.Text.ToString());
            //——————————————————————————————————————————————————————————————————————————
            this.facturacion.Detalle.Add(filaDetalle);
            Cargar();

            ProductoIdComboBox.SelectedIndex = -1;
            CantidadTextBox.Clear();
        }
        //——————————————————————————————————————————————————————————————[ Remover Fila ]———————————————————————————————————————————————————————————————
        private void RemoverFilaButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var detalle = (FacturacionDetalle)DetalleDataGrid.SelectedItem;
                if (DetalleDataGrid.Items.Count >= 1 && DetalleDataGrid.SelectedIndex <= DetalleDataGrid.Items.Count - 1)
                {
                    facturacion.Detalle.RemoveAt(DetalleDataGrid.SelectedIndex);
                    facturacion.Total = facturacion.Total - detalle.Cantidad;
                    Cargar();
                }
            }
            catch
            {
                MessageBox.Show("No has seleccionado ninguna Fila\n\nSeleccione la Fila a Remover.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
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

                if (FacturaIdTextbox.Text.Trim() == String.Empty)
                {
                    MessageBox.Show("El Campo (FacturacionId) esta vacio.\n\nDescriba el proyecto.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    FacturaIdTextbox.Focus();
                    return;
                }

                if (ClienteIdComboBox.Text == String.Empty)
                {
                    MessageBox.Show("El Campo (Cliente Id) esta vacio.\n\nPorfavor, Seleccione un cliente.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    ClienteIdComboBox.IsDropDownOpen = true;
                    ClienteIdComboBox.Focus();
                    return;
                }

                if (EmpleadoIdComboBox.Text == String.Empty)
                {
                    MessageBox.Show("El Campo (Empleado Id) esta vacio.\n\nPorfavor, Seleccione un Empleado.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    EmpleadoIdComboBox.IsDropDownOpen = true;
                    EmpleadoIdComboBox.Focus();
                    return;
                }

                var paso = FacturacionBLL.Guardar(this.facturacion);
                if (paso)
                {
                    Limpiar();
                    MessageBox.Show("Transaccion Exitosa", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    MessageBox.Show("Transaccion Fallida", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        //——————————————————————————————————————————————————————————————[ Eliminar ]———————————————————————————————————————————————————————————————
        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            {
                if (FacturacionBLL.Eliminar(int.Parse(FacturaIdTextbox.Text)))
                {
                    Limpiar();
                    MessageBox.Show("Registro Eliminado", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    MessageBox.Show("No se pudo eliminar el registro", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}