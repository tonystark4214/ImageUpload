using UserDataApplication.Models;

namespace UserDataApplication.Repository
{
    public interface InterfaceUserData
    {
        public ResponseModel GetUserData();

        public ResponseModel PostUserData(PostUserDataModel postData);

        public ResponseModel Delete(int id);

        public ResponseModel GetUserDataById(int id);

        public ResponseModel GetCountryData();
        public ResponseModel GetStateData(int id);
        public ResponseModel GetCityData(int id);
    }
}
