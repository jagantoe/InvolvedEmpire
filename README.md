# Involved Empire

Involved Empire is a Real-time strategy game where you can grow your empire and conquer 
other players through writing code.
Each player manages their own little empire that grows with each day.

### Empire
- Houses, 1 house = 1 new villager per day
- Villagers, don't do anything but can be turned into miners/footsoldiers/knights
- Gold, used to buy houses/footsoldiers/knights
- Miners, 1 miner = 1 gold per day
- Army
  - FootSoldiers, basic cheap combat unit
  - Knights, stronger expensive combat unit

### Actions: 
- GetMyEmpire - get current state of your empire
- GetAllEmpires - get the state of all empires
- GetPriceList - get the current price list of how much houses/footsoldiers/knights cost
- BuyHouse(int amount) - buy house(s) with gold
- EmployMiners(int amount) - turn villagers into miners
- TrainFootSoldiers(int amount) - turn villagers into footsoldiers at a cost
- TrainKnights(int amount) - turn villagers into knights at a cost
- Attack(int targetId) - attack another empire with your army, if they have an army of their own both sides may suffer losses

## Database

### Overview

We only 1 table in our database to store all the data we need.
The user has:
- Id, used for authentication
- Name, the name that is used for the empire
- Password, a plaintext password used to generate the authentication token, the host should add all users with pregenerated passwords, by not allowing users to choose their password we don't need to be all to worried about security
- SerializedEmpire, all the empires data

Each empire is tied to a user that we use to authenticate.
All the data we need to run the game is always loaded in memory to ensure the best performance.
Should the server go down for any reason we would lose all that memory.
To prevent that from happening we also save the state of each empire in the database.
To keep it simple we simply serialize the data and store it as a string.
When we need to load the data we simply deserialize it and store it in memory.
Each empires state is updated and saved with each new day.

### Script

    CREATE TABLE [dbo].[Users] (
        [Id]               INT            IDENTITY (1, 1) NOT NULL,
        [Name]             NVARCHAR (255) NOT NULL,
        [Password]         NVARCHAR (255) NOT NULL,
        [SerializedEmpire] NVARCHAR (MAX) NOT NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC)
    );