using System.Windows;
using SistemaFacturacion.UI.Consultas;
using SistemaFacturacion.UI.Registros;

namespace SistemaFacturacion
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void rEmpleadoMenuItem_Click(object sender, RoutedEventArgs e)
        {
            rEmpleados rEmpleados = new rEmpleados();
            rEmpleados.Show();
        }

        private void rProductoMenuItem_Click(object sender, RoutedEventArgs e)
        {
            rProductos rProductos = new rProductos();
            rProductos.Show();
        }
        private void rEntradaProductoMenuItem_Click(object sender, RoutedEventArgs e)
        {
            rEntradaProducto rEntradaProducto = new rEntradaProducto();
            rEntradaProducto.Show();
        }

        private void rSalidaProductoMenuItem_Click(object sender, RoutedEventArgs e)
        {
            rSalidaProducto rSalidaProducto = new rSalidaProducto();
            rSalidaProducto.Show();
        }

        private void rFacturaMenuItem_Click(object sender, RoutedEventArgs e)
        {
            rFacturacion rFacturacion = new rFacturacion();
            rFacturacion.Show();
        }

        private void cEmpleadosMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void InformacionMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Creado por:\t\t{CreadoPorLabel.Content}\n\nVersión:\t\t\t{VersionLabel.Content}\n\nCreación:\t\t{CreacionLabel.Content}\n\nUltima Modificación:\t{ModificacionLabel.Content}\n\nPara más información:\tjose_burgos3@ucne.edu.do", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
        }
              
    }
}
