namespace pjSegurosVida
{
    public partial class fmrProforma : Form
    {
        public fmrProforma()
        {
            InitializeComponent();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            //Capturando los datos
            string razon= txtRazonsocial.Text;
            string tipo= cboTipo.Text;
            int cantidad = int.Parse(txtCantidad.Text);

            //Calculando el pago mesual por tipio de seguro
            double pagoMensual = 0;
            switch(tipo)
            {
                case "Inversion Clasica":
                    if (cantidad <= 0)
                        pagoMensual = 50 * cantidad;
                    else
                        pagoMensual = ((50 * cantidad) + (10 * cantidad - 10));
                    break;
                case "Inversion Platino":
                    if (cantidad <= 8)
                        pagoMensual = 80 * cantidad;
                    else 
                        pagoMensual=((80* cantidad)+ (8 * cantidad - 8));
                    break;
                case "Inversion Oro":
                    if (cantidad <= 6)
                        pagoMensual = 150 * cantidad;
                    pagoMensual = ((150 * cantidad) + (6 * cantidad - 6));
                    break;
            }
            //Imprimiendo el detalle de la proforma
            ListViewItem fila= new ListViewItem(tipo);
            fila.SubItems.Add(cantidad.ToString());
            fila.SubItems.Add(pagoMensual.ToString("0.00"));
            lvpreforma.Items.Add(fila);
        }

        private void btnGeneral_Click(object sender, EventArgs e)
        {
            //Determinar el total de personas aseguradas
            int totalAsegurados=0;
            for(int i = 0; i < lvpreforma.Items.Count; i++)
            {
                //Por si no hay nada, prevellendo un error :D
                if (lvpreforma.Items[i].SubItems[0].Text != "")
                totalAsegurados += int.Parse(lvpreforma.Items[i].SubItems[1].Text);
            }
            //Determinar el monto total acomuldao a cancelar
            double total = 0;
            for(int i = 0; i < lvpreforma.Items.Count; i++)
            {
                if (lvpreforma.Items[i].SubItems[0].Text != "")
                    total+= double.Parse(lvpreforma.Items[i].SubItems[2].Text);
            }
            //Impresion de las estadisticas
            lvEstadisticas.Items.Clear();
            string[] elemetosFila = new string[2];
            ListViewItem row;

            elemetosFila[0] = "Total de personas aseguradas";
            elemetosFila[1] = totalAsegurados.ToString();
            row= new ListViewItem(elemetosFila);
            lvEstadisticas.Items.Add(row);

            elemetosFila[0] = "Monto total a cancelar";
            elemetosFila[1]= total.ToString("C");
            row= new ListViewItem(elemetosFila);
            lvEstadisticas.Items.Add(row);
            //Ya terminamos :)
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("¿Está seguro de salir?",
                                "Control de pago",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
                this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            DialogResult r = MessageBox.Show("¿Está seguro de anular la preforma?",
                                "Seguros",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Exclamation);
            if (r == DialogResult.Yes)
                txtRazonsocial.Clear();
            cboTipo.Text = "(Seleccione tipo)";
                txtCantidad.Clear();
            txtRazonsocial.Focus();
            lvpreforma.Items.Clear();
            lvEstadisticas.Items.Clear();
        }
    }
}