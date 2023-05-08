using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace exercise2
{
    public partial class Form1 : Form
    {
        // Define a filter constant for OpenFileDialog and SaveFileDialog
        const string FILTER = "Text Files|*.txt|All Files|*.*";

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Calculate DPS (Damage Per Second) with rounded result to 1 decimal place
        /// </summary>
        /// <param name="damage_value"></param>
        /// <param name="attack_speed"></param>
        /// <returns></returns>
        private double CalculateDPS(double damage_value, double attack_speed)
        {
            double DPS = Math.Round(damage_value/attack_speed,1);
            return DPS;
        }

        /// <summary>
        /// Process the file and calculate DPS when the button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonProcessFile_Click(object sender, EventArgs e)
        {
            string weapon_name, weapon_type;
            double damage, attack_speed;

            int i =0;

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            StreamReader reader;
            StreamWriter writer;

            openFileDialog1.Filter = FILTER;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Open the selected input file
                        reader = File.OpenText(openFileDialog1.FileName);

                        // Create a new output file
                        writer = File.CreateText(saveFileDialog1.FileName);

                        // Write the header line for the output file
                        writer.WriteLine("Weapon Name".PadRight(30) + "Weapon Type".PadRight(15) + "Weapon Damage".PadRight(15) + "Attack Speed".PadRight(15) + "DPS");

                        // Process each weapon in the input file
                        while (!reader.EndOfStream)
                        {
                            weapon_name = reader.ReadLine();
                            weapon_type = reader.ReadLine();
                            damage = double.Parse(reader.ReadLine());
                            attack_speed = double.Parse(reader.ReadLine());
                            double dps = CalculateDPS(damage, attack_speed);

                            writer.WriteLine(weapon_name.PadRight(30) + weapon_type.PadRight(15) + damage.ToString().PadRight(15) + attack_speed.ToString().PadRight(15) + dps.ToString().PadRight(5));
                            i++;
                        }
                        MessageBox.Show(i + " weapons were processed");
                        reader.Close();
                        writer.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
        /// <summary>
        /// Exit the application when the exit button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
