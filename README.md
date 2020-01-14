EdgeWorks
======================

As in its current state the project is only a scraper for Blizzards [World of Warcraft](https://worldofwarcraft.com/) ingame auction house.
The future purpose of it is to do some **data science** and **machine learning** on price developement.

## How to use it
First of all you have to create a client at [Blizzards developer page](https://develop.battle.net/access/clients).

After obtaining the client create a appsettings.secret.json in the Scraper project folder (and take care to keep it *secret*):
```
{
  "BlizzardClient": {
    "Name": "YourClientName",
    "ClientId": "YourClientID",
    "ClientSecret": "YourClientSecret"
  }
}

```
To setup the scraper for your enviroment edit the [appsettings.json](https://github.com/emuuu/EdgeWorks/blob/master/src/EdgeWorks.AuctionHouse.Scraper/appsettings.json). By default the data is placed in I:\Data\EdgeWorks\
The ApiSettings define which region / realm / locale is used.


### Dockerize
The scraper is ready-to-use in a [Docker](https://www.docker.com/) container:

Just edit the Volume mappings in [docker-compose.yml](https://github.com/emuuu/EdgeWorks/blob/master/src/EdgeWorks.AuctionHouse.Scraper/docker-compose.yml) and change the paths in [appsettings.json](https://github.com/emuuu/EdgeWorks/blob/master/src/EdgeWorks.AuctionHouse.Scraper/appsettings.json) to the mapped paths.
