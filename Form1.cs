using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NhlDownload
{
    public partial class Form1 : Form
    {        
        public Form1()
        {
            InitializeComponent();
            labelStatus.Text = "Downloading ...";
            labelStatus.Visible = true;
        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            int input;
            if (int.TryParse(textBoxLeagueNumber.Text, out input))
            {
                if (input <= 0) return;
                Scraper.LeagueIDCurrent = input;
            }
            else
            {
                return;
            }

            if (int.TryParse(textBoxLastLeagueNumber.Text, out input))
            {
                if (input <= 0) return;
                Scraper.LeagueIDPrevious = input;
            }
            else
            {
                return;
            }

            if (int.TryParse(textBoxLastLeagueYear.Text, out input))
            {
                if (input <= 0) return;
                Scraper.LeagueYearPrevious = input;
            }
            else
            {
                return;
            }

            CSVOutput.csvFileName = "league" + Scraper.LeagueIDCurrent.ToString() + ".csv";

            SetUIEnabled(false);
            progressBar1.Value = 0;



            labelStatus.Visible = true;

            labelStatus.Text = "Getting skater data...";
            labelStatus.Refresh();
            Scraper.ScrapeOwnedPlayers(Scraper.PlayerType.Skater);
            progressBar1.PerformStep();
            progressBar1.Refresh();

            labelStatus.Text = "Getting goalie data...";
            labelStatus.Refresh();
            Scraper.ScrapeOwnedPlayers(Scraper.PlayerType.Goalie);
            progressBar1.PerformStep();
            progressBar1.Refresh();

            labelStatus.Text = "Getting dropped player data...";
            labelStatus.Refresh();
            Scraper.ScrapeDroppedPlayers();
            progressBar1.PerformStep();
            progressBar1.Refresh();

            labelStatus.Text = "Getting draft data...";
            labelStatus.Refresh();
            Scraper.ScrapeDraftRounds();
            progressBar1.PerformStep();
            progressBar1.Refresh();

            labelStatus.Text = "Calculating Keeper pick costs...";
            labelStatus.Refresh();
            CSVOutput.DetermineKeeperPickCosts();
            progressBar1.PerformStep();
            progressBar1.Refresh();

            labelStatus.Text = "Generating CSV file...";
            labelStatus.Refresh();
            try
            {
                CSVOutput.GenerateCSV();
            }
            catch (Exception ex)
            {                
                labelStatus.Text = "Failed download!";
            }
            finally
            {
                
            }

            progressBar1.PerformStep();
            progressBar1.Refresh();

            labelStatus.Text = "Done! Downloaded to: " + CSVOutput.csvFileName;
            SetUIEnabled(true);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            labelStatus.Visible = false;
            buttonDownload.Enabled = false;
        }

        private void TextBoxLeagueNumber_Changed(object sender, EventArgs e)
        {
            EnableButtonIfReady();
        }

        private void textBoxLastLeagueNumber_TextChanged(object sender, EventArgs e)
        {
            EnableButtonIfReady();
        }

        private void textBoxLastLeagueYear_TextChanged(object sender, EventArgs e)
        {
            EnableButtonIfReady();
        }

        private void EnableButtonIfReady()
        {
            if (textBoxLeagueNumber.TextLength > 0 &&
                textBoxLastLeagueNumber.TextLength > 0 &&
                textBoxLastLeagueYear.TextLength == 4)
                buttonDownload.Enabled = true;
            else
                buttonDownload.Enabled = false;
        }

        private void SetUIEnabled(bool enabled)
        {
            textBoxLeagueNumber.Enabled = enabled;
            textBoxLastLeagueNumber.Enabled = enabled;
            textBoxLastLeagueYear.Enabled = enabled;
            buttonDownload.Enabled = enabled;

            progressBar1.UseWaitCursor = !enabled;
        }
    }
}