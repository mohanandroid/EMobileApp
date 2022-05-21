using System;
using System.Collections.Generic;
using eCups.DatabaseObjects;
using eCups.Models;
using eCups.Models.Custom;
using Xamarin.Forms.Maps;

namespace eCups.DebugData.Custom
{
    public static class FakeData
    {
        public static void Init()
        {

        }

        public static string LoremIpsum()
        {
            return "Lorem ipsum dolor sit amet, consectetur adipiscing elit." +
                " Maecenas gravida fringilla semper. Aliquam nec viverra eros," +
                " volutpat scelerisque erat. Cras maximus orci vel efficitur aliquam. " +
                "Sed eget enim suscipit erat tempor dapibus. In consequat consequat nibh, " +
                "ut aliquam nisi dictum non. Cras vel quam eget nulla porttitor porta vitae in mauris." +
                " Nunc eget imperdiet erat, quis viverra purus. Pellentesque aliquet blandit blandit." +
                " Duis venenatis porta blandit. Donec condimentum, elit tempus consectetur congue," +
                " elit neque varius felis, sit amet congue augue ex et nunc. Sed ante nunc," +
                " ultricies in gravida non, convallis et dolor.";
        } 

        public static readonly CafeObject[] DebugCafeList =
        {
            new CafeObject
            {
                ID = 0,
                Coords = new Position(53.764977, -0.358497),
                Name = "Planet Coffee",
                Address = new Address
                {
                    AddressLine1 = "162 Newland Ave",
                    City = "Hull",
                    AreaCode = "HU5 2NE"
                },
                URL = "https://www.facebook.com/PlanetcoffeeHULL/",
                Phone = "",
                Description = "the OG student coffee shop on newland, now with a more diverse clientel "
            },
            new CafeObject
            {
                ID = 1,
                Coords = new Position(53.768936, -0.353577),
                Name = "The Dancing Goat Coffee House",
                Address = new Address
                {
                    AddressLine1 = "554 Beverley Rd",
                    City = "Hull",
                    AreaCode = "HU6 7LG"
                },
                URL = "http://www.thedancinggoatcoffeehouse.co.uk/",
                Phone = "",
                Description = "from passing it looks like a go to spot for all the local Karens in your area"
            },
            new CafeObject
            {
                ID = 2,
                Coords = new Position(53.762466, -0.358564),
                Name = "The Zoo",
                Address = new Address
                {
                    AddressLine1 = "80 Newland Ave",
                    City = "Hull",
                    AreaCode = "HU5 3AB"
                },
                URL = "https://www.facebook.com/Zoo-Cafe-137005783497589/",
                Phone = "",
                Description = "Hipster vegan cafe, good if you're trying to look edgy"
            },
            new CafeObject
            {
                ID = 3,
                Coords = new Position(53.767994, -0.341233),
                Name = "Costa Coffee",
                Address = new Address
                {
                    AddressLine1 = "44 Clough Rd",
                    City = "Hull",
                    AreaCode = "HU5 1QL"
                },
                URL = "https://www.costa.co.uk/",
                Phone = "",
                Description = "Worse than starbucks by a margin"
            },
            new CafeObject
            {
                ID = 4,
                Coords = new Position(53.767582, -0.335429),
                Name = "Starbucks Coffee",
                Address = new Address
                {
                    AddressLine1 = "291 Clough Rd",
                    City = "Hull",
                    AreaCode = "HU6 7QA"
                },
                URL = "http://starbucks.co.uk/",
                Phone = "",
                Description = "Better than costa by a margin"
            },
            new CafeObject
            {
                ID = 5,
                Coords = new Position(53.723987, -0.532346),
                Name = "Wonky Tulip",
                Address = new Address
                {
                    AddressLine1 = "BizHub, Melton",
                    City = "Melton",
                    AreaCode = "HU14 1QQ"
                },
                URL = "http://starbucks.co.uk/",
                Phone = "",
                Description = "Coffee is good but as implied in the name the foam decoration is always a little bit off"
            },
            new CafeObject
            {
                ID = 6,
                Coords = new Position(53.721639, -0.529031),
                Name = "Sandwich van Lady",
                Address = new Address
                {
                    AddressLine1 = "Melton Business Park Roundabout"
                },
                URL = "http://starbucks.co.uk/",
                Phone = "",
                Description = "She's pretty nice and makes an amazing sandwich, the coffee I can't vouch for"
            },
            new CafeObject
            {
                ID = 7,
                Coords = new Position(53.724057, -0.530985),
                Name = "Someone's house",
                Address = new Address
                {
                    AddressLine1 = "might lend you a coffee on a good day"
                },
                URL = "http://starbucks.co.uk/",
                Phone = "",
                Description = "I don't know this person but, I assume they'd begrudingly spare a spoonful of Milicano for a quick drink"
            },
            new CafeObject
            {
                ID = 8,
                Coords = new Position(53.721889, -0.531913),
                Name = "Chippy Van",
                Address = new Address
                {
                    AddressLine1 = "Only there on Fridays",
                    City = "Hull",
                    AreaCode = "HU5 2NE"
                },
                URL = "http://starbucks.co.uk/",
                Phone = "",
                Description = "Sells Fish and Chips like nobodies business, pretty sure they offer instant coffee too"
            },
            new CafeObject
            {
                ID = 9,
                Coords = new Position(53.721007, -0.531973),
                Name = "Shoes Under a Tenner",
                Address = new Address
                {
                    AddressLine1 = "Some place off the melton roundabout",
                },
                URL = "http://starbucks.co.uk/",
                Phone = "",
                Description = "Looks like someones house, but I assure you they sell quality products, weeks worth of warranty on all purchases"
            },
        };

        public static readonly DealObject[] DebugDealList =
        {
            new DealObject
            {
                ID = 0,
                Coords = new Position(53.764977, -0.358497),
                Name = "Planet Coffee",
                Address = new Address
                {
                    AddressLine1 = "162 Newland Ave",
                    City = "Hull",
                    AreaCode = "HU5 2NE"
                },
                Para = "the OG student coffee shop on newland, now with a more diverse clientel "
            },
            new DealObject
            {
                ID = 1,
                Coords = new Position(53.768936, -0.353577),
                Name = "The Dancing Goat Coffee House",
                Address = new Address
                {
                    AddressLine1 = "554 Beverley Rd, Hull HU5 2NE",
                    City = "Hull",
                    AreaCode = "HU6 7LG"
                },
                Para = "from passing it looks like a go to spot for all the local Karens in your area"
            },
            new DealObject
            {
                ID = 2,
                Coords = new Position(53.762466, -0.358564),
                Name = "The Zoo",
                Address = new Address
                {
                    AddressLine1 = "80 Newland Ave",
                    City = "Hull",
                    AreaCode = "HU5 3AB"
                },
                Para = "Hipster vegan cafe, good if you're trying to look edgy"
            },
            new DealObject
            {
                ID = 3,
                Coords = new Position(53.767994, -0.341233),
                Name = "Costa Coffee",
                Address = new Address
                {
                    AddressLine1 = "44 Clough Rd",
                    City = "Hull",
                    AreaCode = "HU5 1QL"
                },
                Para = "Worse than starbucks by a margin"
            },
            new DealObject
            {
                ID = 4,
                Coords = new Position(53.767582, -0.335429),
                Name = "Starbucks Coffee",
                Address = new Address
                {
                    AddressLine1 = "291 Clough Rd",
                    City = "Hull",
                    AreaCode = "HU6 7QA"
                },
                Para = "Better than costa by a margin"
            },
            new DealObject
            {
                ID = 5,
                Coords = new Position(53.723987, -0.532346),
                Name = "Wonky Tulip",
                Address = new Address
                {
                    AddressLine1 = "BizHub",
                    City = "Hull",
                    AreaCode = "HU14 1QQ"
                },
                Para = "Coffee is good but as implied in the name the foam decoration is always a little bit off"
            },
            new DealObject
            {
                ID = 6,
                Coords = new Position(53.721639, -0.529031),
                Name = "Sandwich van Lady",
                Address = new Address
                {
                    AddressLine1 = "Melton Business Park Roundabout",
                },
                Para = "She's pretty nice and makes an amazing sandwich, the coffee I can't vouch for"
            },
            new DealObject
            {
                ID = 7,
                Coords = new Position(53.724057, -0.530985),
                Name = "Someone's house",
                Address = new Address
                {
                    AddressLine1 = "might lend you a coffee on a good day",
                },
                Para = "I don't know this person but, I assume they'd begrudingly spare a spoonful of Milicano for a quick drink"
            },
            new DealObject
            {
                ID = 8,
                Coords = new Position(53.721889, -0.531913),
                Name = "Chippy Van",
                Address = new Address
                {
                    AddressLine1 = "Only there on Fridays",
                },
                Para = "Sells Fish and Chips like nobodies business, pretty sure they offer instant coffee too"
            },
            new DealObject
            {
                ID = 9,
                Coords = new Position(53.721007, -0.531973),
                Name = "Shoes Under a Tenner",
                Address = new Address
                {
                    AddressLine1 = "Some place off the melton roundabout"
                },
                Para = "Looks like someones house, but I assure you they sell quality products, weeks worth of warranty on all purchases"
            },
        };

    public static readonly LoyaltyTierObject[] LoyaltyTierList =
        {
            new LoyaltyTierObject
            {
                ID = 1,
                ImgUrl = "starfish.png",
                Level = 1,
                LevelName = "Starfish",
                RewardBreakdown = "This level gives you 2% off your orders."
            },
            new LoyaltyTierObject
            {
                ID = 2,
                ImgUrl = "crab.png",
                Level = 2,
                LevelName = "Crab",
                RewardBreakdown = "This level gives you 5% off your orders."
            },
            new LoyaltyTierObject
            {
                ID = 3,
                ImgUrl = "jellyfish.png",
                Level = 3,
                LevelName = "Jellyfish",
                RewardBreakdown = "This level gives you 7% off your orders."
            },
            new LoyaltyTierObject
            {
                ID = 4,
                ImgUrl = "seahorse.png",
                Level = 4,
                LevelName = "Seahorse",
                RewardBreakdown = "This level gives you 10% off your orders."
            },
            new LoyaltyTierObject
            {
                ID = 5,
                ImgUrl = "octopus.png",
                Level = 5,
                LevelName = "Octopus",
                RewardBreakdown = "This level gives you 12% off your orders."
            },
            new LoyaltyTierObject
            {
                ID = 6,
                ImgUrl = "whale.png",
                Level = 6,
                LevelName = "Whale",
                RewardBreakdown = "This level gives you 15% off your orders."
            },
        };
    }
}
