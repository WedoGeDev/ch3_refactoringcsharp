﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Refactoring_CaloriesCalculator
{
    public partial class Form1 : Form
    {
        private PatientHistoryXMLStorage storage;
        public Patient Patient { get; set; }

        public Form1()
        {
            InitializeComponent();
            this.storage = new PatientHistoryXMLStorage();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            ClearResults();

            if (!ValidatePatientPersonalData() || !UserInputValid()) return;

            if (rbtnMale.Checked)
                Patient = new MalePatient();
            else
                Patient = new FemalePatient();

            Patient.SSN = $"{txtSSNFirstPart.Text.Trim()}-{txtSSNSecondPart.Text.Trim()}-{txtSSNThirdPart.Text.Trim()}";
            Patient.FirstName = txtFirstName.Text.Trim();
            Patient.LastName = txtLastName.Text.Trim();
            Patient.WeightInPounds = Convert.ToDouble(txtWeight.Text);
            Patient.HeightInInches = (Convert.ToDouble(txtFeet.Text) * 12)
                + Convert.ToDouble(txtInches.Text);
            Patient.Age = Convert.ToDouble(txtAge.Text);

            txtCalories.Text = Patient.DailyCaloriesRecommended().ToString();
            txtIdealWeigth.Text = Patient.IdealBodyWeight().ToString();
            txtDistance.Text = Patient.DistanceFromIdealWeight().ToString();
        }

        private bool UserInputValid()
        {
            double result;
            if (!double.TryParse(txtFeet.Text, out result))
            {
                MessageBox.Show("Altura (Pies) debe ser un valor numerico");
                txtFeet.Select();
                return false;
            }

            if (!double.TryParse(txtInches.Text, out result))
            {
                MessageBox.Show("Altura (Pulgadas) debe ser un valor numerico");
                txtInches.Select();
                return false;
            }

            if (!double.TryParse(txtWeight.Text, out result))
            {
                MessageBox.Show("Peso debe ser un valor numerico");
                txtWeight.Select();
                return false;
            }

            if (!double.TryParse(txtAge.Text, out result))
            {
                MessageBox.Show("Edad debe ser un valor numerico");
                txtAge.Select();
                return false;
            }

            if (!(Convert.ToDouble(txtFeet.Text) >= 5))
            {
                MessageBox.Show("La altura debe ser igual o mayor a 5 Piés");
                txtFeet.Select();
                return false;
            }

            return true;
        }

        private bool ValidatePatientPersonalData()
        {
            int result;
            if (!int.TryParse(txtSSNFirstPart.Text, out result) ||
                !int.TryParse(txtSSNSecondPart.Text, out result) ||
                !int.TryParse(txtSSNThirdPart.Text, out result))
            {
                MessageBox.Show("Debes ingresar un número de Seguridad social válido");
                txtSSNFirstPart.Select();
                return false;
            }

            if (txtFirstName.Text.Length < 1)
            {
                MessageBox.Show("Debes ingresar un Nombre propio del paciente.");
                txtFirstName.Select();
                return false;
            }

            if (txtLastName.Text.Length < 1)
            {
                MessageBox.Show("Debes ingresar un apellido del paciente.");
                txtFeet.Select();
                return false;
            }

            return true;
        }

        private void ClearResults()
        {
            txtDistance.Clear();
            txtIdealWeigth.Clear();
            txtCalories.Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidatePatientPersonalData() || !UserInputValid()) return;

            btnCalculate_Click(null, null);
            storage.Save(this.Patient);
        }
    }
}
