using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace webrequest
{
    class MethodsClass
    {
        //create reference of our web
        IWebDriver driver = new ChromeDriver();
        //declear excle file path on local PC
        string path = @"E:\MyTest.csv";
        string delimiter = ",";

        //this method get web response or status
        public void webrequestgetMethod()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            // Create a request for the URL. 		
            WebRequest request = WebRequest.Create("https://ichallengeyou.mtpixels.com/login");
            // If required by the server, set the credentials.
            //request.Credentials = CredentialCache.DefaultCredentials;
            // Get the response.
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            // Display the status.
            Console.WriteLine(response.StatusDescription);
            // Get the stream containing content returned by the server.
            Stream dataStream = response.GetResponseStream();
            // Display the status.
            Console.WriteLine(dataStream);
            // Console.WriteLine(dataStream.CopyTo();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            // Display the content.
            Console.WriteLine(responseFromServer);
            // Cleanup the streams and the response.
            reader.Close();
            dataStream.Close();
            response.Close();
            Thread.Sleep(60000);
        }

        //this Method open the given URL Link
        public void initilize()
        {
            //navigate to url page
            driver.Navigate().GoToUrl("https://ichallengeyou.mtpixels.com/login");
            Console.WriteLine("Open URL");

        }

        // This method test the valid login
        public void loginTestCases()
        {
            //find input element
            IWebElement element = driver.FindElement(By.Name("username"));

            //Perform action or input
            element.SendKeys("admin");

            //find input element
            element = driver.FindElement(By.Name("password"));

            //Perform action or input
            element.SendKeys("secret");
            driver.FindElement(By.TagName("button")).Click();
            Console.WriteLine("login successfully");
        }

        //This method show all the links on Dashboard and save it on excle file
        public void DashboardPageLinks()
        {
            driver.FindElement(By.ClassName("icon-home")).Click();
            Console.WriteLine("Click Dashboard icon");
            Thread.Sleep(2000);

            // this line show specific link for given index
            // driver.FindElements(By.TagName("a")).ToList()[1].GetAttribute("href");   

            var link = driver.FindElements(By.TagName("a")).Select(x => x.GetAttribute("href"));

            List<string> item = new List<string>();

            foreach (var Check in link)
            {
                if (Check == null)
                {
                    continue;
                }

                else
                {
                    item.Add(Check);
                }
            }

            for (int i = 0; i < item.Count(); i++)
            {
                Console.WriteLine(" " + item.ToList()[i].ToString());
                writeToFile2(item.ToList()[i].ToString());
            }

        }

        //This method show all the links in side the table body on challenge screen and save it on excle file
        public void challengePageLinks()
        {
            driver.FindElement(By.ClassName("ft-activity")).Click();
            Console.WriteLine("Click challenge icon");
            Thread.Sleep(2000);

            var link = driver.FindElement(By.Id("dTable_wrapper")).FindElements(By.TagName("a"));

            for (int i = 0; i < link.Count(); i++)
            {
                Console.WriteLine(" " + driver.FindElement(By.Id("dTable_wrapper")).FindElements(By.TagName("a")).ToList()[i].GetAttribute("href"));
                writeToFile2(driver.FindElement(By.Id("dTable_wrapper")).FindElements(By.TagName("a")).ToList()[i].GetAttribute("href"));
            }

        }

        //This method open the specific challenge description screen on new tab, 
        //show all the links on that specific screen and save it on excle file
        public void challengeDescriptionPageLinks()
        {
            driver.FindElement(By.ClassName("ft-activity")).Click();
            Console.WriteLine("Click challenge icon");
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//*[@id='dTable_length']/label/select/option[4]")).Click();
            var item = driver.FindElement(By.Id("dTable")).FindElements(By.TagName("a")).Select(x => x.GetAttribute("href"));

            for (int i = 0; i <= item.Count() - 1; i++)
            {
                var tablelink = driver.FindElement(By.Id("dTable")).FindElements(By.TagName("a")).ToList()[i].GetAttribute("href");
                Console.WriteLine("All the internal Links of that specific Link --> " + tablelink);

                string Linkvariable = "\"" + tablelink + "\"";
                string str = "window.open(" + Linkvariable + ")";
                ((IJavaScriptExecutor)driver).ExecuteScript(str);
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                Thread.Sleep(10000);
                var nexttablink = driver.FindElement(By.Id("user-area")).FindElements(By.TagName("a")).Select(x => x.GetAttribute("href"));

                List<string> link = new List<string>();

                foreach (var check in nexttablink)
                {
                    if (check == null)
                    {
                        continue;
                    }

                    else
                    {
                        link.Add(check);
                    }
                }

                for (int j = 0; j <= link.Count() - 1; j++)
                {
                    //var links = driver.FindElement(By.Id("user-area")).FindElements(By.TagName("a")).ToList()[j].GetAttribute("href");
                    Console.WriteLine(" " + link.ToList()[j].ToString());
                    writeToFile2(link.ToList()[j].ToString());
                }


                driver.Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
                Thread.Sleep(8000);
            }

            // writeToFile();
        }

        //This method show all the links in side the table body on Transaction screen and save it on excle file
        public void TransactionPageLinks()
        {
            driver.FindElement(By.ClassName("icon-wallet")).Click();
            Console.WriteLine("Click Transaction icon");
            Thread.Sleep(2000);

            var link = driver.FindElement(By.Id("dTable_wrapper")).FindElements(By.TagName("a")).Select(x => x.GetAttribute("href"));

            for (int i = 0; i < link.Count(); i++)
            {
                Console.WriteLine(" " + link.ToList()[i].ToString());
                writeToFile2(link.ToList()[i].ToString());
            }

        }

        //This method show all the links in side the table body on User screen and save it on excle file
        public void UserPageLinks()
        {
            driver.FindElement(By.ClassName("icon-users")).Click();
            Console.WriteLine("Click User icon");
            Thread.Sleep(2000);

            var link = driver.FindElement(By.Id("dTable_wrapper")).FindElements(By.TagName("a")).Select(x => x.GetAttribute("href"));

            for (int i = 0; i < link.Count(); i++)
            {
                Console.WriteLine("" + link.ToList()[i].ToString());
                writeToFile2(link.ToList()[i].ToString());
            }

        }

        //This method show all the links in side the table body on Catergory screen and save it on excle file
        public void CategoryPageLinks()
        {
            driver.FindElement(By.ClassName("icon-grid")).Click();
            Console.WriteLine("Click Category icon");
            Thread.Sleep(2000);

            var link = driver.FindElement(By.Id("dTable_wrapper")).FindElements(By.TagName("a")).Select(x => x.GetAttribute("href"));

            List<string> item = new List<string>();

            foreach (var check in link)
            {
                if (check == null)
                {
                    continue;
                }

                else
                {
                    item.Add(check);
                }
            }

            for (int i = 0; i < item.Count(); i++)
            {

                Console.WriteLine(" " + item.ToList()[i].ToString());
                writeToFile2(item.ToList()[i].ToString());
            }

        }

        //This method show all the links in side the table body on Notification screen and save it on excle file
        public void NotificationPageLinks()
        {
            driver.FindElement(By.ClassName("icon-bell")).Click();
            Console.WriteLine("Click Notification icon");
            Thread.Sleep(2000);

            var link = driver.FindElement(By.Id("dTable_wrapper")).FindElements(By.TagName("a")).Select(x => x.GetAttribute("href"));
            for (int i = 0; i < link.Count(); i++)
            {
                Console.WriteLine(" " + link.ToList()[i].ToString());
                writeToFile2(link.ToList()[i].ToString());
            }

        }

        //This method show all the links in side the table body on CustomMessage screen and save it on excle file
        public void CustomMessagePageLinks()
        {
            driver.FindElement(By.ClassName("icon-speech")).Click();
            Console.WriteLine("Click Custom Message icon");
            Thread.Sleep(2000);
            var link = driver.FindElement(By.Id("dTable_wrapper")).FindElements(By.TagName("a")).Select(x => x.GetAttribute("href"));
            List<string> fill = new List<string>();

            foreach (var item in link)
            {
                if (item == null)
                {
                    continue;
                }
                else
                    fill.Add(item);
            }

            for (int i = 0; i < fill.Count(); i++)
            {
                Console.WriteLine(" " + fill.ToList()[i].ToString());
                writeToFile2(fill.ToList()[i].ToString());
            }

        }

        //This method show all the links in side the table body on Setting screen and save it on excle file
        public void SettingPageLinks()
        {
            driver.FindElement(By.ClassName("icon-wrench")).Click();
            Console.WriteLine("Click Setting icon");
            Thread.Sleep(2000);

            var link = driver.FindElement(By.Id("dTable_wrapper")).FindElements(By.TagName("a")).Select(x => x.GetAttribute("href"));
            List<string> fill = new List<string>();

            foreach (var item in link)
            {
                if (item == null)
                {
                    continue;
                }
                else
                    fill.Add(item);
            }
            for (int i = 0; i < fill.Count(); i++)
            {
                Console.WriteLine(" " + fill.ToList()[i].ToString());
                writeToFile2(fill.ToList()[i].ToString());
            }

        }

        //This method create Excle file for the given location
        public void CreateCSVFile()
        {
            if (!File.Exists(path))
            {
                // Create a file and write column to.
                string createColumn = "Screen name" + delimiter + "URL" + Environment.NewLine;
                File.WriteAllText(path, createColumn);
            }
        }

        //This method write the data in excle file that show on console screen
        public void writeToFile()
        {
            CreateCSVFile();

            var item = driver.FindElements(By.TagName("a"));

            for (int i = 0; i < item.Count(); i++)
            {
                string appendText = getScreenName() + delimiter + driver.FindElements(By.TagName("a")).ToList()[i].GetAttribute("href") + Environment.NewLine;
                File.AppendAllText(path, appendText);
            }
        }

        public void writeToFile2(string ab)
        {
            CreateCSVFile();
            ab = getScreenName() + delimiter + ab + Environment.NewLine;
            File.AppendAllText(path, ab);
        }

        //This method can return the current screen name that has currently working 
        public string getScreenName()
        {
            var screnname = driver.FindElements(By.ClassName("active")).ToList()[0].Text;
            return screnname;

        }

        public void transactionDetail()
        {
            driver.FindElement(By.ClassName("icon-wallet")).Click();
            Console.WriteLine("Click Transaction icon");
            Thread.Sleep(2000);
            int b = 0;
            int temp = 0;
            for (int m = 2; m <= 9; m++)
            {
                var check = driver.FindElements(By.XPath("/ html / body / div[1] / div[3] / div / div / div / section / div / div / div / div[2] / div / div[2] / div / div[3] / div[2] / div / ul / li[" + m + "] / a"));
                if (check[0].Text.ToString() == "Next")
                {
                    b = temp;
                    break;
                }
                else if (check[0].Text.ToString() == "…")
                {
                    continue;
                }
                else
                {
                    temp = int.Parse(check[0].Text.ToString());
                }

            }
           
                for (int c = 0; c < b; c++)
                {
                    var tablelink = driver.FindElement(By.Id("dTable")).FindElements(By.TagName("a")).Select(x => x.GetAttribute("href"));
                    for (int i = 0; i < tablelink.Count(); i++)
                    {
                        Console.WriteLine("" + tablelink.ToList()[i].ToString());
                        writeToFile2(tablelink.ToList()[i].ToString());
                    }
                    driver.FindElement(By.Id("dTable_next")).Click();
                    Thread.Sleep(5000);
                }
          
        }

        public void alluserlink()
        {
            driver.FindElement(By.ClassName("icon-users")).Click();
            Console.WriteLine("Click User icon");
            Thread.Sleep(2000);
            int b = 0;
            int temp = 0;
            for (int m = 2; m <= 9; m++)
            {
                var check = driver.FindElements(By.XPath("/html/body/div[1]/div[3]/div/div/div/section/div/div/div/div[2]/div/div/div[3]/div[2]/div/ul/li[" + m + "]/a"));
                if (check[0].Text.ToString() == "Next")
                {
                    b = temp; break;
                }
                else if (check[0].Text.ToString() == "…")
                {
                    continue;
                }
                else
                {
                    temp = int.Parse(check[0].Text.ToString());
                }

            }
            for (int c = 0; c < b; c++)
            {
                var tablelink = driver.FindElement(By.Id("dTable")).FindElements(By.TagName("a")).Select(x => x.GetAttribute("href"));
                for (int i = 0; i < tablelink.Count(); i++)
                {
                    Console.WriteLine("" + tablelink.ToList()[i].ToString());
                    writeToFile2(tablelink.ToList()[i].ToString());
                }

                driver.FindElement(By.Id("dTable_next")).Click();
                Thread.Sleep(5000);

            }

        }



        public void AllchallengeandDescriptionLinks()
        {
            driver.FindElement(By.ClassName("ft-activity")).Click();
            Console.WriteLine("Click challenge icon");
            Thread.Sleep(2000);
            int b = 0;
            int temp = 0;
            for (int m = 2; m <= 9; m++)
            {
                var check = driver.FindElements(By.XPath("/html/body/div[1]/div[3]/div/div/div/section/div/div/div/div[2]/div/div[2]/div/div[3]/div[2]/div/ul/li[" + m + "]/a"));

                if (check[0].Text.ToString() == "Next")
                {
                    b = temp;
                    break;
                }
                else
                {
                    temp = int.Parse(check[0].Text.ToString());
                }

            }

            for (int c = 0; c < b; c++)
            {
                var tablelink = driver.FindElement(By.Id("dTable")).FindElements(By.TagName("a")).Select(x => x.GetAttribute("href"));
                for (int i = 0; i < tablelink.Count(); i++)
                {
                    var mainlink = tablelink.ToList()[i].ToString();
                    Console.WriteLine("All the internal Links of that specific Link --> " +mainlink);
                    //writeToFile2(tablelink.ToList()[i].ToString());

                    string Linkvariable = "\"" + mainlink + "\"";
                    string str = "window.open(" + Linkvariable + ")";
                    ((IJavaScriptExecutor)driver).ExecuteScript(str);
                    driver.SwitchTo().Window(driver.WindowHandles.Last());
                    Thread.Sleep(10000);
                    var nexttablink = driver.FindElement(By.Id("user-area")).FindElements(By.TagName("a")).Select(x => x.GetAttribute("href"));

                    List<string> link = new List<string>();

                    foreach (var check in nexttablink)
                    {
                        if (check == null)
                        {
                            continue;
                        }

                        else
                        {
                            link.Add(check);
                        }
                    }

                    for (int j = 0; j <= link.Count() - 1; j++)
                    {
                        //var links = driver.FindElement(By.Id("user-area")).FindElements(By.TagName("a")).ToList()[j].GetAttribute("href");
                        Console.WriteLine(" " + link.ToList()[j].ToString());
                        writeToFile2(link.ToList()[j].ToString());
                    }

                    driver.Close();
                    driver.SwitchTo().Window(driver.WindowHandles.First());
                    Thread.Sleep(8000);

                }

                driver.FindElement(By.Id("dTable_next")).Click();
                Thread.Sleep(5000);

            }

        }

    }
}

