using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace новости
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string urlAddress = "https://www.znu.edu.ua/cms/index.php?action=news/view&start=0&site_id=27&lang=ukr";
            Regex regexNews = new Regex("(?s)<div class=\"znu-2016-new-img-list-info\">.+?</div>");

            string result = "";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (response.CharacterSet == null)
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                }

                string data = readStream.ReadToEnd();
                Match matchNews = regexNews.Match(data);

                while (matchNews.Success)
                {
                    result = Regex.Replace(matchNews.Value, @"<(.|\n)*?>", string.Empty);
                    result = result.Replace("?", "i");
                    textBox1.Text += result;
                    matchNews = matchNews.NextMatch();
                }

                response.Close();
                readStream.Close();
            }

            
        }
    }
}
