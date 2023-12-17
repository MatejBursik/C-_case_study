using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Text.Json;

namespace Selenium_console_app
{
    internal class Program
    {
        public class Scrapper()
        {
            public string url;
            public string searchTerm;
            private string currentDateTime = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");

            // setup GeckoDriver
            private static FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(".", "geckodriver.exe");
            IWebDriver driver = new FirefoxDriver(service);

            // constructor
            public Scrapper(string _url, string _searchTerm) : this()
            {
                url = _url;
                searchTerm = _searchTerm;
            }

            // methods
            public void scrape1() {
                try
                {
                    string title = "";
                    string date = "";
                    string uploader = "";
                    string views = "";
                    string link = "";
                    int count = 0;

                    List<object> objectData = new List<object>(); // .json storage
                    List<List<string>> collectedData = new List<List<string>> { }; // .csv storage

                    // Navigate to a website
                    driver.Navigate().GoToUrl(url + searchTerm);
                    Thread.Sleep(5000);

                    // perform actions
                    string[] hold;
                    var videos = driver.FindElements(By.CssSelector("ytd-video-renderer"));
                    foreach (var item in videos)
                    {
                        title = item.FindElement(By.CssSelector("#video-title")).Text;
                        hold = item.FindElement(By.CssSelector("#metadata-line")).Text.Split("\n");
                        Console.WriteLine(item.FindElement(By.CssSelector("#metadata-line")).Text);
                        if (hold.Length < 2) { continue; }
                        date = hold[1];
                        uploader = item.FindElement(By.CssSelector("#channel-info")).Text;
                        views = hold[0];
                        link = item.FindElement(By.CssSelector("#video-title")).GetAttribute("href"); ;
                        
                        // .json
                        var data = new
                        {
                            Title = title,
                            Date = date,
                            Uploader = uploader,
                            Views = views,
                            Link = link
                        };
                        objectData.Add(data);

                        // .csv
                        List<string> prep = [title, date, uploader, views, link];
                        collectedData.Add(prep);

                        count++;
                        if (count == 5)
                        {
                            break;
                        }
                    }

                    if (count == 0)
                    {
                        Console.WriteLine("Unfortunately, we did not found any instances of your search.");
                    }
                    else if (count < 5)
                    {
                        Console.WriteLine("Unfortunately, we found only " + count + " instances of your search.");
                    }

                    // Wait
                    Thread.Sleep(5000);

                    // file saving
                    string fileName = "YouTube_" + searchTerm + "_" + currentDateTime;

                    // write the JSON string to the file
                    string jsonString = JsonSerializer.Serialize(objectData);
                    File.WriteAllText(fileName + ".json", jsonString);

                    // Write data to the CSV file
                    using (StreamWriter sw = new StreamWriter(fileName + ".csv"))
                    {
                        foreach (var line in collectedData)
                        {
                            // Join the elements of the line with commas and write to the file
                            sw.WriteLine(string.Join(",", line));
                        }
                    }

                    // Close the browser
                    driver.Quit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
                finally
                {
                    // Ensure the browser is closed even if an exception occurs
                    driver.Quit();
                }
            }

            public void scrape2() {
                try
                {
                    string title = "";
                    string company = "";
                    string location = "";
                    string pageLink = "";
                    string[] keywords = [""];
                    int count = 0;

                    List<object> objectData = new List<object>(); // .json storage
                    List<List<string>> collectedData = new List<List<string>> { }; // .csv storage

                    // Navigate to a website
                    driver.Navigate().GoToUrl(url + searchTerm);
                    Thread.Sleep(5000);

                    // perform actions
                    string[] hold;
                    var jobListings = driver.FindElements(By.CssSelector(".job-info"));
                    foreach (var item in jobListings)
                    {
                        hold = item.Text.Split("\n");
                        title = hold[0].Trim(new Char[] { ' ', '\r' });
                        company = hold[1].Trim(new Char[] { ' ', '\r' });
                        location = hold[2].Trim(new Char[] { ' ', '\r' });
                        pageLink = item.FindElement(By.CssSelector(".search-item-link")).GetAttribute("href");
                        keywords = hold[3].Split(", ");

                        // .json
                        var data = new
                        {
                            Title = title,
                            Company = company,
                            Location = location,
                            PageLink = pageLink,
                            Keywords = keywords
                        };
                        objectData.Add(data);

                        // .csv
                        List<string> prep = [title, company, location, pageLink];
                        foreach (var inf in keywords)
                        {
                            prep.Add(inf);
                        }
                        collectedData.Add(prep);

                        count++;
                        if (count == 5)
                        {
                            break;
                        }
                    }

                    if (count == 0)
                    {
                        Console.WriteLine("Unfortunately, we did not found any instances of your search.");
                    }
                    else if (count < 5)
                    {
                        Console.WriteLine("Unfortunately, we found only " + count + " instances of your search.");
                    }

                    // Wait
                    Thread.Sleep(5000);

                    // file saving
                    string fileName = "ictjob_" + searchTerm + "_" + currentDateTime;

                    // write the JSON string to the file
                    string jsonString = JsonSerializer.Serialize(objectData);
                    File.WriteAllText(fileName + ".json", jsonString);

                    // Write data to the CSV file
                    using (StreamWriter sw = new StreamWriter(fileName + ".csv")) {
                        foreach (var line in collectedData) {
                            // Join the elements of the line with commas and write to the file
                            sw.WriteLine(string.Join(",", line));
                        }
                    }

                    // Close the browser
                    driver.Quit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
                finally
                {
                    // Ensure the browser is closed even if an exception occurs
                    driver.Quit();
                }
            }

            public void scrape3() {
                try
                {
                    //name, number of pages, related tags, link to the cheat sheet, date of upload
                    string name = "";
                    string pages = "";
                    string link = "";
                    int count = 0;

                    List<object> objectData = new List<object>(); // .json storage
                    List<List<string>> collectedData = new List<List<string>> { }; // .csv storage

                    // Navigate to a website
                    driver.Navigate().GoToUrl(url + searchTerm);
                    Thread.Sleep(5000);

                    // perform actions
                    string[] hold;
                    var jobListings = driver.FindElements(By.CssSelector(".cheat_sheet_row"));
                    foreach (var item in jobListings)
                    {
                        name = item.FindElement(By.CssSelector("strong")).Text.Trim(new Char[] { ' ', '\r' });
                        pages = item.FindElement(By.CssSelector(".page_count")).Text.Split(" ")[0];
                        link = item.FindElement(By.CssSelector("strong a")).GetAttribute("href");

                        // .json
                        var data = new
                        {
                            Name = name,
                            Pages = pages,
                            Link = link
                        };
                        objectData.Add(data);

                        // .csv
                        List<string> prep = [name, pages, link];
                        collectedData.Add(prep);

                        count++;
                        if (count == 5)
                        {
                            break;
                        }
                    }

                    if (count == 0)
                    {
                        Console.WriteLine("Unfortunately, we did not found any instances of your search.");
                    } else if (count < 5)
                    {
                        Console.WriteLine("Unfortunately, we found only " + count + " instances of your search.");
                    }

                    // Wait
                    Thread.Sleep(5000);

                    // file saving
                    string fileName = "cheatography_" + searchTerm + "_" + currentDateTime;

                    // write the JSON string to the file
                    string jsonString = JsonSerializer.Serialize(objectData);
                    File.WriteAllText(fileName + ".json", jsonString);

                    // Write data to the CSV file
                    using (StreamWriter sw = new StreamWriter(fileName + ".csv"))
                    {
                        foreach (var line in collectedData)
                        {
                            // Join the elements of the line with commas and write to the file
                            sw.WriteLine(string.Join(",", line));
                        }
                    }

                    // Close the browser
                    driver.Quit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
                finally
                {
                    // Ensure the browser is closed even if an exception occurs
                    driver.Quit();
                }
            }
        }

        static void Main(string[] args)
        {
            bool run = true;
            while (run)
            {
                Console.WriteLine("Choose one of the options to scrape:");
                Console.WriteLine("1:\tyoutube.com");
                Console.WriteLine("2:\tictjob.be");
                Console.WriteLine("3:\tcheatography.com");
                Console.WriteLine("\n4:\tTurn OFF\n-----");

                string choice = Console.ReadLine();
                string searchTerm;
                Scrapper scrapper;

                switch (choice)
                {
                    case "1":
                        // run yourube.com web scrapper
                        Console.WriteLine("Provide your search term:");
                        searchTerm = Console.ReadLine();
                        scrapper = new Scrapper("https://www.youtube.com/results?search_query=", searchTerm);
                        scrapper.scrape1();

                        break;
                    case "2":
                        // run ictjob.be web scrapper
                        Console.WriteLine("Provide your search term:");
                        searchTerm = Console.ReadLine();
                        scrapper = new Scrapper("https://www.ictjob.be/en/search-it-jobs?keywords=", searchTerm);
                        scrapper.scrape2();

                        break;
                    case "3":
                        // run cheatography.com web scrapper
                        Console.WriteLine("Provide your search term:");
                        searchTerm = Console.ReadLine();
                        scrapper = new Scrapper("https://cheatography.com/explore/search/?q=", searchTerm);
                        scrapper.scrape3();

                        break;
                    case "4":
                        // end the app loop
                        run = false;
                        break;
                    default:
                        Console.WriteLine("You did not chose one of the options correctly");
                        break;
                }

                scrapper = null;
                Console.Clear();
            }
        }
    }
}
