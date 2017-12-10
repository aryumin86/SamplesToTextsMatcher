using SamplesToTextsMatcher;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopMatcherDemo
{
    public partial class Form1 : Form
    {
        Context ctx = null;
        string pattern = string.Empty;
        string[] sample = null;
        AbstractPatternParser parser = null;
        AbstractMorfDictionary dict = null;


        public Form1()
        {
            InitializeComponent();
            parser = new ConcretePatternParser();
            dict = new ConcreteMorfDictionary();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pattern = patternTextBox.Text;
            sample = stringTextBox.Text
                .ToLower()
                .Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            try
            {
                ctx = new Context(pattern, parser, dict);

                resultLable.Text = "PROCESSING...!";
                resultLable.ForeColor = Color.Blue;

                bool res = ctx.MatchPatternToString(sample);
                if (res)
                {
                    resultLable.Text = "SUCCESS!";
                    resultLable.ForeColor = Color.Green;
                }
                else
                {
                    resultLable.Text = "FAILED!";
                    resultLable.ForeColor = Color.Red;
                }

                fullPatternTextBox.Text = ctx.Root.ResStringExpression;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
