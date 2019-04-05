using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace GymManagementSystem
{
    public partial class frmFitnessMeasure : MetroFramework.Forms.MetroForm
    {

        ConnectionString cs = new ConnectionString();
        CommonClasses cc = new CommonClasses();
        clsFunc cf = new clsFunc();
        string st1;
        string st2;
        public frmFitnessMeasure()
        {
            InitializeComponent();
        }
      
        public void auto()
        {
            try
            {
                int Num = 0;
                cc.con = new SqlConnection(cs.DBConn);
                cc.con.Open();
                string sql = "SELECT MAX(ID+1) FROM FitnessMeasurement";
                cc.cmd = new SqlCommand(sql);
                cc.cmd.Connection = cc.con;
                if (Convert.IsDBNull(cc.cmd.ExecuteScalar()))
                {
                    Num = 1;
                    txtID.Text = Convert.ToString(Num);
                }
                else
                {
                    Num = (int)(cc.cmd.ExecuteScalar());
                    txtID.Text = Convert.ToString(Num);
                }
                cc.cmd.Dispose();
                cc.con.Close();
                cc.con.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        public void Reset()
        {
            txtAddress.Text = "";
            txtID.Text = "";
            txtCity.Text = "";
            txtMemberName.Text = "";
            txtMembershipID.Text = "";
            txtMembershipType.Text = "";
            txtM_ID.Text = "";
            txtBodyWeight.Text = "";
            txtBodyMass.Text = "";
            txtBodyFat.Text = "";
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
            btnSave.Enabled = true;
            Picture.Image = Properties.Resources.photo;
            dtpDate.Text = System.DateTime.Now.ToString();
            dtpDateFrom.Text = System.DateTime.Today.ToString();
            dtpDateTo.Text = System.DateTime.Today.ToString();
            dtpDate.Enabled = true;
            auto();
        }
       
    
        private void btnNew_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmFitnessMeasurementRecord frm = new frmFitnessMeasurementRecord();
            frm.Reset();
            frm.lblOperation.Text = "Fitness Measure";
            frm.lblUser.Text = lblUser.Text;
            frm.Show();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMemberName.Text == "")
                {
                    MessageBox.Show("Please retrieve member info", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMemberName.Focus();
                    return;
                }
                if (txtBodyWeight.Text == "")
                {
                    MessageBox.Show("Please enter body weight", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtBodyWeight.Focus();
                    return;
                }
                if (txtBodyFat.Text == "")
                {
                    MessageBox.Show("Please enter body fat", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtBodyFat.Focus();
                    return;
                }
                if (txtBodyMass.Text == "")
                {
                    MessageBox.Show("Please enter body mass", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtBodyMass.Focus();
                    return;
                }
             
                cc.con = new SqlConnection(cs.DBConn);
                cc.con.Open();
                string cb = "insert into FitnessMeasurement(ID,CustMembershipID,Weight,BodyFat,BodyMass,Date) VALUES (" + txtID.Text +"," + txtM_ID.Text + "," + txtBodyWeight.Text + "," + txtBodyFat.Text + "," + txtBodyMass.Text +",@d1)";
                cc.cmd = new SqlCommand(cb);
                cc.cmd.Connection = cc.con;
                cc.cmd.Parameters.AddWithValue("@d1", dtpDate.Value);
                cc.cmd.ExecuteReader();
                cc.con.Close();
                st1 = lblUser.Text;
                st2 = "added the new fitness measurement of member '" + txtMemberName.Text + "'";
                cf.LogFunc(st1, System.DateTime.Now, st2);
                btnSave.Enabled = false;
                MessageBox.Show("Successfully saved", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

      

        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Do you really want to delete this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                delete_records();
            }
        }
        private void delete_records()
        {

            try
            {
                int RowsAffected = 0;
                cc.con = new SqlConnection(cs.DBConn);
                cc.con.Open();
                string ct = "delete from FitnessMeasurement where ID=@d1";
                cc.cmd = new SqlCommand(ct);
                cc.cmd.Connection = cc.con;
                cc.cmd.Parameters.AddWithValue("@d1", txtID.Text);
                RowsAffected = cc.cmd.ExecuteNonQuery();
                if (RowsAffected > 0)
                {
                    st1 = lblUser.Text;
                    st2 = "deleted the fitness measurement record of member '" + txtMemberName.Text + "'";
                    cf.LogFunc(st1, System.DateTime.Now, st2);
                    MessageBox.Show("Successfully deleted", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Reset();
                    Reset();
                }
                else
                {
                    MessageBox.Show("No Record found", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Reset();
                }
                if (cc.con.State == ConnectionState.Open)
                {
                    cc.con.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMemberName.Text == "")
                {
                    MessageBox.Show("Please retrieve member info", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMemberName.Focus();
                    return;
                }
                if (txtBodyWeight.Text == "")
                {
                    MessageBox.Show("Please enter body weight", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtBodyWeight.Focus();
                    return;
                }
                if (txtBodyFat.Text == "")
                {
                    MessageBox.Show("Please enter body fat", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtBodyFat.Focus();
                    return;
                }
                if (txtBodyMass.Text == "")
                {
                    MessageBox.Show("Please enter body mass", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtBodyMass.Focus();
                    return;
                }

                cc.con = new SqlConnection(cs.DBConn);
                cc.con.Open();
                string cb = "Update FitnessMeasurement set CustMembershipID=" + txtM_ID.Text + ",Weight=" + txtBodyWeight.Text + ",BodyFat=" + txtBodyFat.Text + ",BodyMass=" + txtBodyMass.Text + ",Date=@d1 where ID=" + txtID.Text + "";
                cc.cmd = new SqlCommand(cb);
                cc.cmd.Connection = cc.con;
                cc.cmd.Parameters.AddWithValue("@d1", dtpDate.Value);
                cc.cmd.ExecuteReader();
                cc.con.Close();
                st1 = lblUser.Text;
                st2 = "updated the fitness measurement record of member '" + txtMemberName.Text + "'";
                cf.LogFunc(st1, System.DateTime.Now, st2);
                btnUpdate.Enabled = false;
                MessageBox.Show("Successfully updated", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmFitnessMeasurementRecord frm = new frmFitnessMeasurementRecord();
            frm.Reset();
            frm.lblUser.Text = lblUser.Text;
            frm.Show();
        }

        private void txtBodyWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            // allows 0-9, backspace, and decimal
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }
        }

        private void txtBodyFat_KeyPress(object sender, KeyPressEventArgs e)
        {
            // allows 0-9, backspace, and decimal
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }
        }

        private void txtBodyMass_KeyPress(object sender, KeyPressEventArgs e)
        {
            // allows 0-9, backspace, and decimal
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }
        }

    


    }
}
