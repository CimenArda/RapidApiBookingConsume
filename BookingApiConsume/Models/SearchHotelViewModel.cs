namespace BookingApiConsume.Models
{
    public class SearchHotelViewModel
    {


        public Data data { get; set; }


        public class Data
        {
            public Hotel[] hotels { get; set; }

        }

        public class Hotel
        {
            public int hotel_id { get; set; }
            public string accessibilityLabel { get; set; }

            public Property1 property { get; set; }
        }

        public class Property1
        {

            public string currency { get; set; }
            public string name { get; set; }


            public float reviewScore { get; set; }

            public string[] photoUrls { get; set; }

            public string reviewScoreWord { get; set; }

        }

    }
}
