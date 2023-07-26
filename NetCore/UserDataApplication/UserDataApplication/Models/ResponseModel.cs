namespace UserDataApplication.Models
{
    public class ResponseModel
    {
        public string? Message { get; set; }
        public List<UserDataModel> ListData { get; set; }
        public UserDataModel Data { get; set; }
        public List<Country080723> CountryData { get; set; }
        public List<State080723> StateData { get; set; }
        public List<City080723> CityData { get; set; }
    }
}
