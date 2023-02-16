using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Drawing.Imaging;
using System.Globalization;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private OpenFileDialog openFileDialog;
        private FolderBrowserDialog outputFileDialog;

        private string outputDirectory;

        private char[] alphabet = new char[26];
        private char[] numericals = new char[10];
        private char[] extras = new char[30];

        private int textSize = 12; // Default text size: 12

        public void openFileForm()
        {
            // Select font file

            openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "OpenType font files (*.otf)|*.otf";
            openFileDialog.Title = "Open Font File";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;
        }

        public void outputFileForm()
        {
            // Select output folder

            outputFileDialog = new FolderBrowserDialog();
        }

        public void insertEssentialCharacters()
        {
            // Add characters to their desired arrays

            for (int i = 0; i < 26; i++)
            {
                alphabet[i] = (char)('A' + i);
            }

            for (int i = 0; i < 10; i++)
            {
                numericals[i] = (char)('0' + i);
            }

            extras[0] = '.';
            extras[1] = '"';
            extras[2] = 'é';
            extras[3] = '!';
            extras[4] = '\'';
            extras[5] = '>';
            extras[6] = '£';
            extras[7] = '+';
            extras[8] = '$';
            extras[9] = '%';
            extras[10] = '½';
            extras[11] = '&';
            extras[12] = '/';
            extras[13] = '{';
            extras[14] = '(';
            extras[15] = '[';
            extras[16] = ')';
            extras[17] = ']';
            extras[18] = '=';
            extras[19] = '}';
            extras[20] = '?';
            extras[21] = '*';
            extras[22] = '\\';
            extras[23] = '-';
            extras[24] = '_';
            extras[25] = ';';
            extras[26] = ':';
            extras[27] = '`';
            extras[28] = '~';
            extras[29] = '<';
        }

        public void createImage()
        {
            // Draw characters

            var fontCollection = new PrivateFontCollection();
            textBox2.Text = openFileDialog.FileName;

            fontCollection.AddFontFile(openFileDialog.FileName);

            int bitmapMultiplier = textSize / 12;

            var font = new Font(fontCollection.Families[0], textSize, FontStyle.Regular);

            var bitmap = new Bitmap(500 * bitmapMultiplier, 100 * bitmapMultiplier);
            var graphics = Graphics.FromImage(bitmap);

            int x = 0;
            int y = 0;

            List<string> myStrings = new List<string>() { };

            myStrings.Add("local module = {}");
            myStrings.Add("");
            myStrings.Add("module.List = {");

            for (int i = 0; i < alphabet.Length; i++)
            {
                if (x + (15 * bitmapMultiplier) > (500 * bitmapMultiplier))
                {
                    x = 0;
                    y = y + (30 * bitmapMultiplier);
                }
                graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                graphics.DrawString(alphabet[i].ToString(), font, Brushes.White, new Point(x, y));

                SizeF size = graphics.MeasureString(alphabet[i].ToString(), font);

                float widthInPixels = size.Width;
                float heightInPixels = size.Height;

                string widthString = widthInPixels.ToString("0.0", CultureInfo.InvariantCulture);
                string heightString = heightInPixels.ToString("0.0", CultureInfo.InvariantCulture);

                myStrings.Add("    [\"" + alphabet[i].ToString() + "\"] = {ImageRectOffset = Vector2.new(" + x + ", " + y + "), ImageRectSize = Vector2.new(" + widthString + ", " + heightString + ")},");

                x = x + (15 * bitmapMultiplier);
            }

            for (int i = 0; i < alphabet.Length; i++)
            {
                if (x + (15 * bitmapMultiplier) > (500 * bitmapMultiplier))
                {
                    x = 0;
                    y = y + (30 * bitmapMultiplier);
                }

                string xd = alphabet[i].ToString().ToLower();

                if (xd == "ı")
                {
                    xd = "i";
                }

                graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                graphics.DrawString(xd, font, Brushes.White, new Point(x, y));

                SizeF size = graphics.MeasureString(xd, font);

                float widthInPixels = size.Width;
                float heightInPixels = size.Height;

                string widthString = widthInPixels.ToString("0.0", CultureInfo.InvariantCulture);
                string heightString = heightInPixels.ToString("0.0", CultureInfo.InvariantCulture);

                myStrings.Add("    [\"" + xd + "\"] = {ImageRectOffset = Vector2.new(" + x + ", " + y + "), ImageRectSize = Vector2.new(" + widthString + ", " + heightString + ")},");

                x = x + (15 * bitmapMultiplier);
            }

            for (int i = 0; i < numericals.Length; i++)
            {
                if (x + (15 * bitmapMultiplier) > (500 * bitmapMultiplier))
                {
                    x = 0;
                    y = y + (30 * bitmapMultiplier);
                }

                graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                graphics.DrawString(numericals[i].ToString(), font, Brushes.White, new Point(x, y));

                SizeF size = graphics.MeasureString(numericals[i].ToString(), font);

                float widthInPixels = size.Width;
                float heightInPixels = size.Height;

                string widthString = widthInPixels.ToString("0.0", CultureInfo.InvariantCulture);
                string heightString = heightInPixels.ToString("0.0", CultureInfo.InvariantCulture);

                myStrings.Add("    [\"" + numericals[i].ToString() + "\"] = {ImageRectOffset = Vector2.new(" + x + ", " + y + "), ImageRectSize = Vector2.new(" + widthString + ", " + heightString + ")},");

                x = x + (15 * bitmapMultiplier);
            }

            for (int i = 0; i < extras.Length; i++)
            {
                if (x + (15 * bitmapMultiplier) > (500 * bitmapMultiplier))
                {
                    x = 0;
                    y = y + (30 * bitmapMultiplier);
                }

                graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                graphics.DrawString(extras[i].ToString(), font, Brushes.White, new Point(x, y));

                SizeF size = graphics.MeasureString(extras[i].ToString(), font);

                float widthInPixels = size.Width;
                float heightInPixels = size.Height;

                string widthString = widthInPixels.ToString("0.0", CultureInfo.InvariantCulture);
                string heightString = heightInPixels.ToString("0.0", CultureInfo.InvariantCulture);

                string currentExtra = extras[i].ToString();

                if (currentExtra == "\"")
                {
                    currentExtra = "\\\"";
                }
                else if (currentExtra == "\\")
                {
                    currentExtra = "\\\\";
                }

                myStrings.Add("    [\"" + currentExtra + "\"] = {ImageRectOffset = Vector2.new(" + x + ", " + y + "), ImageRectSize = Vector2.new(" + widthString + ", " + heightString + ")},");

                x = x + (15 * bitmapMultiplier);
            }

            myStrings.Add("}");
            myStrings.Add("");
            myStrings.Add("return module");

            using (StreamWriter writer = new StreamWriter(@outputDirectory + "\\outputPositions.lua"))
            {

                foreach (string s in myStrings)
                {
                    writer.WriteLine(s);
                }
            }

            graphics.Dispose();

            bitmap.Save(@outputDirectory + "\\output.png", ImageFormat.Png);

            label5.Text = "Succesfully created!";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Input font file

            openFileForm();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    textBox2.Text = openFileDialog.FileName;
                    Console.WriteLine("Received OpenType font file!");
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            // Input output directory

            outputFileForm();

            if (outputFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                outputDirectory = outputFileDialog.SelectedPath;

                Console.WriteLine("Selected directory: " + outputDirectory);

                textBox3.Text = outputDirectory;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            // Check everything, then draw stuff

            if (openFileDialog is object & openFileDialog.FileName != "")
            {
                if (outputDirectory != "")
                {
                    if (textSize % 12 == 0)
                    {
                        createImage();
                    }
                    else
                    {
                        MessageBox.Show("Please make sure font size is dividable by 12!", "Error");
                    }
                }
                else
                {
                    MessageBox.Show("Please input an output folder!", "Error");
                }
            }
            else
            {
                MessageBox.Show("Please input a font object!", "Error");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Load stuff

            insertEssentialCharacters();
            textBox1.MaxLength = 2;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // Small credits section

            MessageBox.Show("Made by MiniYear, thanks for using!", "Custom Font");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Check if input number is valid to use

            if (textBox1.Text.Length == 2)
            {
                int number = 0;

                if (Int32.TryParse(textBox1.Text, out number))
                {
                    if (number % 12 == 0)
                    {
                        textSize = number;
                    }
                    else
                    {
                        MessageBox.Show("Please make sure number you entered is dividable by 12!", "Error");
                    }
                }
                else
                {
                    MessageBox.Show("Please make sure you enter a number!", "Error");
                }
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
