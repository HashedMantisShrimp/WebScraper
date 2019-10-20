# WebScraper
The purpose of this console application is to showcase some of my current knowledge on c# as well as my programming principles.

-------------------------------------------------------------------------------------------------------

**Short Description**: **WebScraper** is a simple console application that makes use of **Regular Expressions** to scrape **CraigsList** items based on user choice of city and category. 

                                      ---------------------------------------------------------
                                                
**Detailed Description**: **WebScraper** asks the user to enter the name of the city from which the items will be acquired. Based on the user's city input, the program downloads the webpage's HTML content and matches that content against a **Regex** (Regular Expression) to acquire the available categories. Once found, those categories are displayed to the user and he is asked to pick one.
After the program acquires the category picked by the user, it builds a link based on the user input and downloads the HTML content within it.
Lasttly, the program matches the downloaded HTML content agaisnt a criteria which is specified by a **Regex**. The regular expression written in the code catches the pattern used by CraigsList to display the available items. Once the pattern is found and the items acquired, the items' description as well as its link are then displayed to the user.

**WebScraper** is ready to handle all exceptions caught during run-time by means of **try catch** blocks.
A **Unit Test** was also built to test the applications main functionality, the **Scraper**, which serves to ensure that so long as the regex code is written correctly, the scrapper will find and acquire the specified pattern. 

-------------------------------------------------------------------------------------------------------

**General Descritpion of the Scripts**


**[ScrapeCriteria](https://github.com/PauloB04/WebScraper/blob/master/Data/ScrapeCriteria.cs)** Is a part of the Data library. **ScrapeCriteria** Contains the properties which are used by the **ScrapeCriteriaBuilder** to "build" this class. The scrape criteria is built with the **Data, Regex** and **RegexOption** properties. There is one more property within ScrapeCriteria which is a list of **ScrapeCriteriaParts**, this list is used to act as a "comb" which contains an even more specific regex pattern.

**[ScrapeCriteriaPart](https://github.com/PauloB04/WebScraper/blob/master/Data/ScrapeCriteriaPart.cs)** Is a part of the Data library, it was created to contain the properties used to build a more specific regex pattern. A list of this class is created and used by the **ScrapeCriteria** class. The ScrapeCriteriaPart also has a builder of its own, **ScrapeCriteriaPartBuilder**, that was created to facilitate the use of this "functionality".

**[WebData](https://github.com/PauloB04/WebScraper/blob/master/Data/WebData.cs)** Is also a part of the Data library, it was created to hold the properties that are used by the **WebDataBuilder** to create a link.

**[ScrapeCriteriaBuilder](https://github.com/PauloB04/WebScraper/blob/master/Builders/ScrapeCriteriaBuilder.cs)** Is part of the Builders library, it makes use of the properties from **ScrapeCriteria** to build the regex pattern. Once built, this regex pattern is used by the **Scraper** class to match the **Data** agaisnt the specified **Regex**(regular expression).

**[ScrapeCriteriaPartBuilder](https://github.com/PauloB04/WebScraper/blob/master/Builders/ScrapeCriteriaPartBuilder.cs)** Is part of the Builders library, and not unlike the ScrapeCriteriaBuilder, it makes use of the the properties created in another class to help "build" said class. Essentially, its meant to be used within the **ScrapeCriteriaBuilder** (the creation of a ScrapeCriteria class) to build the **ScraperCriteriaParts**.

**[WebDataBuilder](https://github.com/PauloB04/WebScraper/blob/master/Builders/WebDataBuilder.cs)** Is part of the Builders library, it makes use of the properties from **WebData** to help acquire the necessary data which will be used to build a link. The data acquried from this class is then used by the **WebDownloader** class.

**[Scraper](https://github.com/PauloB04/WebScraper/blob/master/Workers/Scraper.cs)** Is part of the Workers library, it is essentially the heart or core of the application. 
**Scraper** takes the built **ScrapeCriteria** class and makes use of its properties to find the specified **Regex** within the provided **Data**. Scraper then checks for whether or not there were any **ScrapeCriteriaParts** created and, if there is at least one, it will take the matches found with the **ScrapeCriteria** properties and use them to match against its own specifed **Regex**, the resulting matches are then added to a list. If there were no **ScrapeCriteriaParts** created, the code will simply add the matches found with ScrapeCriteria to a list.

**[CategoryScraper](https://github.com/PauloB04/WebScraper/blob/master/Workers/CategoryScraper.cs)** Is part of the Workers library, this class was created to "declutter" the **program** class. 
**CategoryScraper** Creates its very own regex pattern by making use of the **ScrapeCriteria** class and with that pattern, it uses the **Scraper** class to find a list of the categories present within the downloaded CraigsList webpage. It then returns a list of the aforementioned categories.

**[WebDownloader](https://github.com/PauloB04/WebScraper/blob/master/Workers/WebDownloader.cs)** Also part of the Workers library, is a class that was created to facilitate the link-building process as well as the process of downloading a webpage's HTML content.
**WebDownloader** Takes the built **WebData** class and uses its properties to build a link, once the link has been built it then proceeds to download the content found with the link.

**[Program](https://github.com/PauloB04/WordUnscrambler/blob/master/Program.cs)** Is the part of the application that joins all of the different libraries together and uses them to run the application.
**Program** asks the user for inputs, makes use of the **WebDownloader** to acquire the **Contnet** based on the user's input, uses the **CategoryScraper** to find the categories within the acquired **Content** and makes use of the **Scraper** & **ScrapeCriteria** class to find the displayed items' name as well as link, which are the shown to the user.

**[ScraperTest](https://github.com/PauloB04/WebScraper/blob/master/WebScraper.Test.Unit/Workers/ScraperTest.cs)** Is (at least for now) the one and only **Unit Test Class** of the application, it was created to test the most important part of the business logic, which is the **Scrape** Method contained within the **Scraper** class.
**ScraperTest** makes two tests with the **Scrape** Method:

**1** It creates a string and then proceeds to build the **ScrapeCriteria** pattern which will be used to find a specific part of the string. Having built the ScrapeCriteria, it then uses the **Scraper** class to find the specified part of the string. It passes the test if the list of matches found contains exactly one element within it and if that element is an exact match to the **expectedElement**.

**2** It creates another string and then proceeds to build the **ScrapeCriteria** pattern, the difference being that this ScrapeCriteria pattern also contains two **ScraperCriteriaParts**, they in turn, will be used to find two even more specific parts within the match found with the **ScrapeCriteria**. Having built the ScrapeCriteria, it then uses the **Scraper** class to find the specified part of the string. It passes the test if the list of matches found contains exactly two elements within it and if those elements are an exact match to the **expectedElements**.
