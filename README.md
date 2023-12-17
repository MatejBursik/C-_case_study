# C#_case_study

For my case study I decided to choose case study option number 5 which is developing a webscraper using C#-based Selenium browser automation.
In this task, I am going to create a C# console application in which the user can choose out of 3 options when it comes to scrapping a website.

They can choose to scrape YouTube from where they can get basic data like link to the video, title of the video, date of upload,
uploader and number of views about 5 uploaded videos based on their inputted search term.

Then, they have the option to scrape the jobsite ictjob.be from where they will receive 5 most recently uploaded job offers
that came from their inputted search term with basic information about the offer like title, company, location, keywords and link to the page.

Lastly, the option that I choose is to scrape the website cheatography.com from were the user can retrieve 5 cheat sheet
based on their search term (topic) with information like name, number of pages and link to the cheat sheet.

If there are not 5 cheat sheets for the chosen search term (topic), it will inform the user about how many cheat sheets were retrieved.
Data from any of these options will be saved in .csv and .json files named with template of “option_search term_date” (YouTube_cat_2023-03-31 05-38-08)
