# FamilyFantasyHockeyScraper
### What is this?
In my family's Yahoo! Fantasy Hockey League, we use our own custom rules when it comes selecting Keepers. The short explaination is if you want to keep a player you drafted, you need to give up the previous round's pick in the next draft. (Ex. Must give up next year's 5th Round pick to keep a player you drafted in last year's 6th round)

There's also bunch of additional rules for players that are traded or dropped, or for Keeping players that were kept from the previous year, but I won't go over them here. The point is, we have a unique set of rules for Keepers that isn't supported by Yahoo! and therefore needs to be done externally.

This is a simple Windows Forms app that can automatically determine what pick must be given up to keep any player for each team in the league.

### How does it work?
This project uses the [HTML Agility Pack (HAP)](https://html-agility-pack.net/) to scrape our archived league for the required data such as the draft results, final team rosters, player stats, and transaction history. I can then go through each player on each team and assign a "Keep Cost" to each according to our rules. Finally I output the data into a .csv file that I can import into Google Sheets to format and share with the rest of the league.
