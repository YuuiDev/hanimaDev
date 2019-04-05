using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
namespace GymManagementSystem
{
    public partial class frmFitnessMeasurementRecord : MetroFramework.Forms.MetroForm
    {
        ConnectionString cs = new ConnectionString();
        CommonClasses cc = new CommonClasses();
        public frmFitnessMeasurementRecord()
        {
            InitializeComponent();
        }
        public void GetData()
        {
            try
            {
                cc.con = new SqlConnection(cs.DBConn);
                cc.con.Open();
                cc.cmd = new SqlCommand("Select RTRIM(ID) as [ID],RTRIM(CustomerMembership.CM_ID) as [CM Id],RTRIM(CustomerMembership.CustMembershipID) as [Membership ID],RTRIM(Type) as [Membership Type],RTRIM(Customer.CustomerID) as [Member ID],RTRIM(Name) as [Member Name],RTRIM(City) as [City],RTRIM(Address) as [Address],Convert(Datetime,DateFrom,103) as [Date From],Convert(Datetime,DateTo,103) as [Date To],Convert(Datetime,Date,131) as [Date],RTRIM(Weight) as [Body Weight],RTRIM(BodyFat) as [Body Fat],RTRIM(BodyMass) as [Body Mass],(Photo) as [Photo] from FitnessMeasurement, Membership,CustomerMembership,Customer where Customer.C_ID=CustomerMembership.CustomerID and Membership.M_ID=CustomerMembership.MembershipID and FitnessMeasurement.CustMembershipID=CustomerMembership.CM_ID order by Date", cc.con);
                cc.da = new SqlDataAdapter(cc.cmd);
                cc.ds = new DataSet();
                cc.da.Fill(cc.ds, "Customer");
                dgw.DataSource = cc.ds.Tables["Customer"].DefaultView;
                cc.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void txtGuestName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cc.con = new SqlConnection(cs.DBConn);
                cc.con.Open();
                cc.cmd = new SqlCommand("Select RTRIM(ID) as [ID],RTRIM(CustomerMembership.CM_ID) as [CM Id],RTRIM(CustomerMembership.CustMembershipID) as [Membership ID],RTRIM(Type) as [Membership Type],RTRIM(Customer.CustomerID) as [Member ID],RTRIM(Name) as [Member Name],RTRIM(City) as [City],RTRIM(Address) as [Address],Convert(Datetime,DateFrom,103) as [Date From],Convert(Datetime,DateTo,103) as [Date To],Convert(Datetime,Date,131) as [Date],RTRIM(Weight) as [Body Weight],RTRIM(BodyFat) as [Body Fat],RTRIM(BodyMass) as [Body Mass],(Photo) as [Photo] from FitnessMeasurement, Membership,CustomerMembership,Customer where Customer.C_ID=CustomerMembership.CustomerID and Membership.M_ID=CustomerMembership.MembershipID and FitnessMeasurement.CustMembershipID=CustomerMembership.CM_ID and name like '" + txtMemberName.Text + "%' order by name", cc.con);
                cc.da = new SqlDataAdapter(cc.cmd);
                cc.ds = new DataSet();
                cc.da.Fill(cc.ds, "Customer");
                dgw.DataSource = cc.ds.Tables["Customer"].DefaultView;
                cc.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void Reset()
        {
            txtMemberName.Text = "";
            GetData();
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgw_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            string strRowNumber = (e.RowIndex + 1).ToString();
            SizeF size = e.Graphics.MeasureString(strRowNumber, this.Font);
            if (dgw.RowHeadersWidth < Convert.ToInt32((size.Width + 20)))
            {
               dgw.RowHeadersWidth = Convert.ToInt32((size.Width + 20));
            }
            Brush b = SystemBrushes.ControlText;
            e.Graphics.DrawString(strRowNumber, this.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2));
     
        }

        private void dgw_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                     DataGridViewRow dr = dgw.SelectedRows[0];
                    this.Hide();
                    frmFitnessMeasure frm = new frmFitnessMeasure();
                    frm.Show();
                    frm.txtID.Text = dr.Cells[0].Value.ToString();
                    frm.txtM_ID.Text = dr.Cells[1].Value.ToString();
                    frm.txtMembershipID.Text = dr.Cells[2].Value.ToString();
                    frm.txtMembershipType.Text = dr.Cells[3].Value.ToString();
                    frm.txtMemberName.Text = dr.Cells[5].Value.ToString();
                    frm.txtCity.Text = dr.Cells[6].Value.ToString();
                    frm.txtAddress.Text = dr.Cells[7].Value.ToString();
                    frm.dtpDateFrom.Text = dr.Cells[8].Value.ToString();
                    frm.dtpDateTo.Text = dr.Cells[9].Value.ToString();
                    frm.dtpDate.Text = dr.Cells[10].Value.ToString();
                    frm.txtBodyWeight.Text = dr.Cells[11].Value.ToString();
                    frm.txtBodyFat.Text = dr.Cells[12].Value.ToString();
                    frm.txtBodyMass.Text = dr.Cells[13].Value.ToString();
                    byte[] data = (byte[])dr.Cells[14].Value;
                    MemoryStream ms = new MemoryStream(data);
                    frm.Picture.Image = Image.FromStream(ms);
                    frm.btnUpdate.Enabled = true;
                    frm.btnDelete.Enabled = true;
                    frm.btnSave.Enabled = false;
                    frm.lblUser.Text = lblUser.Text;
                    lblOperation.Text = "";
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmCustomerRecord_Load(object sender, EventArgs e)
        {
            GetData();
        }

    }
}
