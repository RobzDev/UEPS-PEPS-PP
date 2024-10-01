using System.Collections.Generic;

namespace CostosEmpresariales
{
    public partial class Form1 : Form
    {

        string option = "";
        double entrada = 0;
        double salida = 0;
        
        bool firstrow = true;

        PEPS p = new PEPS();
        UEPS u = new UEPS();
        PP pp = new PP();
        public Form1()
        {
            InitializeComponent();
            ConfigurarDataGridView();
        }
        private void ConfigurarDataGridView()
        {
            // Configuramos las columnas del DataGridView
            dtviewMI.ColumnCount = 9; // Número total de columnas

            dtviewMI.Columns[0].Name = "Fecha";
            dtviewMI.Columns[1].Name = "Entrada";
            dtviewMI.Columns[2].Name = "Salida";
            dtviewMI.Columns[3].Name = "Existencia";
            dtviewMI.Columns[4].Name = "Costo unitario";
            dtviewMI.Columns[5].Name = "Costo medio"; // Para el promedio
            dtviewMI.Columns[6].Name = "Debe";
            dtviewMI.Columns[7].Name = "Haber";
            dtviewMI.Columns[8].Name = "Saldo";

            foreach (DataGridViewColumn dt in dtviewMI.Columns)
            {
                if (dt.Index != 0)
                {
                    dt.ReadOnly = true;
                }
                else
                {
                    dt.ReadOnly = false;
                }
            }

            dtviewMI.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dtviewMI.AllowUserToAddRows = true;
            dtviewMI.AllowUserToOrderColumns = false;
        }

        private void dtviewMI_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell d = dtviewMI.Rows[e.RowIndex].Cells[e.ColumnIndex];


            if (d.Value != null && !string.IsNullOrWhiteSpace(d.Value.ToString()))
            {


                string column_name = dtviewMI.Columns[e.ColumnIndex].HeaderText;
                if (column_name == "Entrada")
                {
                    firstrow = false;
                    dtviewMI.Rows[e.RowIndex].Cells[2].ReadOnly = true;
                    entrada = Convert.ToDouble(dtviewMI.Rows[e.RowIndex].Cells[1].Value.ToString());




                    dtviewMI.Rows[e.RowIndex].Cells[4].ReadOnly = false;


                    switch (option)
                    {
                        case "PEPS":

                            p.existencia += Convert.ToDouble(dtviewMI.Rows[e.RowIndex].Cells[1].Value.ToString());
                            dtviewMI.Rows[e.RowIndex].Cells[3].Value = p.existencia.ToString();
                            dtviewMI.Rows[e.RowIndex].Cells[2].ReadOnly = true;

                            break;
                       
                        case "UEPS":
                            u.existencia += Convert.ToDouble(dtviewMI.Rows[e.RowIndex].Cells[1].Value.ToString());
                            dtviewMI.Rows[e.RowIndex].Cells[3].Value = u.existencia.ToString();
                            dtviewMI.Rows[e.RowIndex].Cells[2].ReadOnly = true;
                            break;
                        case "PP":  
                            pp.existencia += Convert.ToDouble(dtviewMI.Rows[e.RowIndex].Cells[1].Value.ToString());
                            dtviewMI.Rows[e.RowIndex].Cells[3].Value = pp.existencia.ToString();
                            dtviewMI.Rows[e.RowIndex].Cells[2].ReadOnly = true;
                            break;
                    }

                }
                else if (column_name == "Salida")
                {
                    if(firstrow == true && dtviewMI.Rows[e.RowIndex].Cells[2].Value != null)
                    {
                        dtviewMI.Rows[e.RowIndex].Cells[2].Value = null;
                        return;
                    }
                    
                    if (dtviewMI.Rows[e.RowIndex - 1].Cells[6].Value == null && dtviewMI.Rows[e.RowIndex - 1].Cells[2].Value == null)
                    {
                        dtviewMI.Rows[e.RowIndex].Cells[2].Value = null;
                        MessageBox.Show("No haz ingresado el costo unitario");
                        return;
                    }
                   
                    salida = Convert.ToDouble(dtviewMI.Rows[e.RowIndex].Cells[2].Value.ToString());
                    switch (option)
                    {

                        case "PEPS":
                            if(p.saldo == 0)
                            {
                                MessageBox.Show("Haz agotado totalmente las existencias");
                                dtviewMI.Rows[e.RowIndex].Cells[2].ReadOnly = true;
                                dtviewMI.Rows[e.RowIndex].Cells[2].Value = null;
                                return;
                            }
                            dtviewMI.Rows[e.RowIndex].Cells[1].ReadOnly = true;
                            p.existencia -= Convert.ToDouble(dtviewMI.Rows[e.RowIndex].Cells[2].Value.ToString());
                            dtviewMI.Rows[e.RowIndex].Cells[3].Value = p.existencia;
                            double haber = p.Haber(salida);
                            dtviewMI.Rows[e.RowIndex].Cells[7].Value = haber;
                            p.saldo -= haber;
                            dtviewMI.Rows[e.RowIndex].Cells[8].Value = p.saldo;
                            break;

                        case "UEPS":
                            if (u.saldo == 0)
                            {
                                MessageBox.Show("Haz agotado totalmente las existencias");
                                dtviewMI.Rows[e.RowIndex].Cells[2].ReadOnly = true;
                                dtviewMI.Rows[e.RowIndex].Cells[2].Value = null;
                                return;
                            }
                            dtviewMI.Rows[e.RowIndex].Cells[1].ReadOnly = true;
                            // Implementar la lógica de salida según UEPS (últimas entradas, primeras salidas)
                            u.existencia -= Convert.ToDouble(dtviewMI.Rows[e.RowIndex].Cells[2].Value.ToString());
                            dtviewMI.Rows[e.RowIndex].Cells[3].Value = u.existencia;
                            double haberUEPS = u.HaberUEPS(salida);
                            dtviewMI.Rows[e.RowIndex].Cells[7].Value = haberUEPS;
                            u.saldo -= haberUEPS;
                            dtviewMI.Rows[e.RowIndex].Cells[8].Value = u.saldo;
                            break;
                        case "PP":
                            if (pp.saldo == 0)
                            {
                                MessageBox.Show("Haz agotado totalmente las existencias");
                                dtviewMI.Rows[e.RowIndex].Cells[2].ReadOnly = true;
                                dtviewMI.Rows[e.RowIndex].Cells[2].Value = null;
                                return;
                            }
                            dtviewMI.Rows[e.RowIndex].Cells[1].ReadOnly = true;
                            pp.existencia -= Convert.ToDouble(dtviewMI.Rows[e.RowIndex].Cells[2].Value.ToString());
                            dtviewMI.Rows[e.RowIndex].Cells[3].Value = pp.existencia;
                            double haberPP = pp.Haber(salida);
                            dtviewMI.Rows[e.RowIndex].Cells[7].Value = haberPP;
                            pp.saldo -= haberPP;
                            dtviewMI.Rows[e.RowIndex].Cells[8].Value = pp.saldo;
                            dtviewMI.Rows[e.RowIndex].Cells[5].Value = pp.costoPromedio;
                            break;

                    }




                }
                else if (column_name == "Costo unitario")
                {

                    switch (option)
                    {
                        case "PEPS":
                            List<double> list = new List<double>();
                            list.Add(entrada);
                            list.Add(Convert.ToDouble(dtviewMI.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()));
                            double debe = p.Debe(list);
                            dtviewMI.Rows[e.RowIndex].Cells[6].Value = debe;
                            p.saldo += debe;
                            dtviewMI.Rows[e.RowIndex].Cells[8].Value = p.saldo;
                            break;

                        case "UEPS":
                            List<double> listUEPS = new List<double>();
                            listUEPS.Add(entrada);
                            listUEPS.Add(Convert.ToDouble(dtviewMI.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()));
                            double debeUEPS = u.Debe(listUEPS);
                            dtviewMI.Rows[e.RowIndex].Cells[6].Value = debeUEPS;
                            u.saldo += debeUEPS;
                            dtviewMI.Rows[e.RowIndex].Cells[8].Value = u.saldo;
                            break;
                       
                            
                        case "PP":
                            List<double> listPP = new List<double>();
                            listPP.Add(entrada);
                            listPP.Add(Convert.ToDouble(dtviewMI.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()));
                            
                            double costoUnitarioPP = Convert.ToDouble(dtviewMI.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                            double debePP = pp.Debe(listPP);
                            dtviewMI.Rows[e.RowIndex].Cells[6].Value = debePP;
                            pp.saldo += debePP;
                            dtviewMI.Rows[e.RowIndex].Cells[5].Value = pp.costoPromedio;
                            dtviewMI.Rows[e.RowIndex].Cells[8].Value = pp.saldo;
                            break;
                    }
                    

                    
                 



                }

            }


        }

        private void cbOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected_option = cbOptions.SelectedItem.ToString();
            if(selected_option == "PEPS" || selected_option == "UEPS" || selected_option == "PP")
            {
                option = selected_option;
                foreach (DataGridViewColumn dt in dtviewMI.Columns)
                {
                    if (dt.Index != 1 && dt.Index != 2 && dt.Index != 0)
                    {
                        dt.ReadOnly = true;
                    }
                    else
                    {
                        dt.ReadOnly = false;
                    }
                }
            }
           
        }

        private void dtviewMI_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            DataGridViewCell d = dtviewMI.Rows[e.RowIndex].Cells[e.ColumnIndex];
            
            if(d.OwningColumn.Name == "Salida" && firstrow == true)
            {
                d.ReadOnly = true;
                
                return;
            }
        }
    }
}
