import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class UserDataService {

  constructor(private http: HttpClient) { }

  GetUserData(){
    return this.http.get("http://localhost:5003/GetUserData");
  }
  GetUserDataById(id: number) {
    return this.http.get("http://localhost:5003/GetUserDataById?id=" + id)
  }
  PostUserData(data:any){
    return this.http.post("http://localhost:5003/PostUserData",data)
  }
  CountryData() {
    return this.http.get("http://localhost:5003/GetCountryData")
  }
  StateData(id: number) {
    return this.http.get("http://localhost:5003/GetStateData?id=" + id)
  }
  CityData(id: number) {
    return this.http.get("http://localhost:5003/GetCityData?id=" + id)
  }
  Delete(id:number){
    return this.http.delete("http://localhost:5003/Delete?id="+id)
  }
}
