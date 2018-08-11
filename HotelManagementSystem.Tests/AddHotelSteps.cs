using FluentAssertions;
using HotelManagementSystem.Models;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace HotelManagementSystem.Tests
{
    [Binding]
    public class AddHotelSteps
    {
        private Hotel hotel = new Hotel();
        private Hotel addHotelResponse;
        private List<Hotel> hotels = new List<Hotel>();
        private List<int> ids = new List<int>();


        [Given(@"User provided valid Id '(.*)'  and '(.*)'for hotel")]
        public void GivenUserProvidedValidIdAndForHotel(int id, string name)
        {
            ids.Add(id);
            hotel.Id = id;
            hotel.Name = name;
        }

        [Given(@"Use has added required details for hotel")]
        public void GivenUseHasAddedRequiredDetailsForHotel()
        {
            SetHotelBasicDetails();
        }

        [Given(@"user has provided a valid hotel id '(.*)' to be retrieved")]
        public void GivenUserHasProvidedAValidHotelIdToBeRetrieved(int id)
        {
            hotel.Id = id;
        }
        [Given(@"User has called AddHotel api")]
        [When(@"User calls AddHotel api")]
        public void WhenUserCallsAddHotelApi()
        {
            hotels = HotelsApiCaller.AddHotel(hotel);
        }

        [When(@"user calls GetHotelById api")]
        public void WhenUserCallsGetHotelByIdApi()
        {
            
            addHotelResponse = HotelsApiCaller.GetHotelById(hotel.Id);
        }


        [When(@"User calls GetAllHotels api")]
        public void WhenUserCallsGetAllHotelsApi()
        {
            hotels = HotelsApiCaller.GetAllHotels();
        }



        [Then(@"Hotel with name '(.*)' should be present in the response")]
        public void ThenHotelWithNameShouldBePresentInTheResponse(string name)
        {
            hotel = hotels.Find(htl => htl.Name.ToLower().Equals(name.ToLower()));
          
                
            hotel.Should().NotBeNull(string.Format("Hotel with name {0} not found in response",name));
        }

        [Then(@"Details of Hotel with id '(.*)'  should be present in the response")]
        public void ThenDetailsOfHotelWithIdShouldBePresentInTheResponse(int id)
        {
            addHotelResponse.Should().NotBeNull(string.Format("Hotel with specified id {0} does not exists", id));
        }

        [Then(@"Hotels added should be present in the response")]
        public void ThenHotelsAddedShouldBePresentInTheResponse()
        {
            foreach (int id in ids)
            {
                var hotel = hotels.Find(ht => ht.Id == id);
                hotel.Should().NotBeNull(string.Format("Hotel with Id {0} is not present", hotel));
            }
        }

        private void SetHotelBasicDetails()
        {
            hotel.ImageURLs = new List<string>() { "image1", "image2" };
            hotel.LocationCode = "Location";
            hotel.Rooms = new List<Room>() { new Room() { NoOfRoomsAvailable = 10, RoomType = "delux" } };
            hotel.Address = "Address1";
            hotel.Amenities = new List<string>() { "swimming pool", "gymnasium" };
        }
    }
}

